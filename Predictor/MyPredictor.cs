using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.IO;
using System.Linq;


namespace Predictor
{
    class MyPredictor
    {

        private const string FEATURES = "Features";

        private PredictionEngine<ModelInput, ModelOutput> engine;
        private MLContext context = new MLContext();
        private ITransformer model;
        private IDataView data;

        public IDataView Data => data;
        public bool Loaded { get; set; }

        public static string MODEL_PATH = Environment.CurrentDirectory + "\\Model.zip";

        public void GenerateModel(string filePath)
        {
            if (!(Loaded = File.Exists(filePath))) return;

            data = context.Data.LoadFromTextFile<ModelInput>(filePath, hasHeader: true, separatorChar: ';'); // Загрузка базы
            // Переработка
            var pipeline = context.Transforms.Categorical.OneHotEncoding(ColumnNames.DATE, ColumnNames.DATE)
                            .Append(context.Transforms.Concatenate(FEATURES, ColumnNames.FACTORS))
                            .Append(context.Transforms.NormalizeMinMax(FEATURES, FEATURES))
                            .Append(context.Regression.Trainers.LbfgsPoissonRegression(ColumnNames.CRIMES_COUNT, FEATURES));

            // Создание модели
            model = pipeline.Fit(data);
            // Сохранение модели
            context.Model.Save(model, data.Schema, MODEL_PATH);

            engine = context.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);

            System.Windows.Forms.MessageBox.Show("Обучено.\n" +
                "Модель сохранена: " + MODEL_PATH + "\n" +
                "Точность: " + GetAccuracy());
        }

        public void UploadModel(string filePath)
        {
            if (!(Loaded = File.Exists(filePath))) return;

            model = context.Model.Load(filePath, out _); // Загрузка модели
            try
            {
                engine = context.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка предзагрузки встроенной модели. Удаление...");
                File.Delete(filePath);
            }
            System.Windows.Forms.MessageBox.Show("Модель загружена: " + filePath);
        }

        public void TryLoad()
        {
            UploadModel(MODEL_PATH);
        }

        public string GetAccuracy()
        {
            IDataView predictions = model.Transform(data);
            RegressionMetrics metrics = context.Regression.Evaluate(predictions, ColumnNames.CRIMES_COUNT);

            return metrics.RSquared + "\n" + (metrics.RSquared * 100).ToString("0") + "%";
        }

        public ModelOutput Predict(ModelInput inputData)
        {
            return engine.Predict(inputData);
        }
    }
}
