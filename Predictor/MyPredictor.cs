using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Runtime;
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

        public void GenerateModel(string filePath)
        {
            data = context.Data.LoadFromTextFile<ModelInput>(filePath, hasHeader: true, separatorChar: ';'); // Загрузка базы
            
            //context.Data.
            // Переработка
            var pipeline = context.Transforms.Concatenate(FEATURES, ColumnNames.FACTORS)
                            .Append(context.Transforms.NormalizeMinMax(FEATURES, FEATURES))
                            .Append(context.Regression.Trainers.LbfgsPoissonRegression(ColumnNames.CRIMES_COUNT, FEATURES));

            // Создание модели
            model = pipeline.Fit(data);
            // Сохранение модели
            context.Model.Save(model, data.Schema, filePath.Replace(".csv", ".zip"));

            engine = context.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);
        }

        public void Generate(string filePath)
        {
            data = context.Data.LoadFromTextFile<ModelInput>(filePath, hasHeader: true, separatorChar: ';'); // Загрузка базы
            // Переработка
            
            for (int i = 2; i < ColumnNames.FACTORS.Length; i++)
            {
                var pipeline = context.Transforms.Concatenate(FEATURES, ColumnNames.FACTORS[0], ColumnNames.FACTORS[1])
                            .Append(context.Transforms.NormalizeMinMax(FEATURES, FEATURES))
                            .Append(context.Regression.Trainers.LbfgsPoissonRegression(ColumnNames.FACTORS[i], FEATURES));
                // Создание модели
                model = pipeline.Fit(data);

                engine = context.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);

                IDataView newData = model.Transform(data);

                for (int year = 2020; year < 2050; year++)
                {
                    for (int quarter = 0; quarter < 4; quarter++)
                    {
                        newData.GetColumn<float>(ColumnNames.YEAR).Prepend(year);
                        newData.GetColumn<float>(ColumnNames.QUARTER).Prepend(quarter);
                        newData.GetColumn<float>(ColumnNames.FACTORS[i]).Prepend(engine.Predict(new ModelInput() { Year = year, Quarter = quarter }).Score);
                    }
                }

                // Сохранение модели
                using (FileStream s = new FileStream(Environment.CurrentDirectory + "\\Resources\\" + ColumnNames.FACTORS[i] + ".zip", FileMode.OpenOrCreate))
                {
                    context.Model.Save(model, data.Schema, s);
                }
                using (FileStream s = new FileStream(Environment.CurrentDirectory + "\\Resources\\" + ColumnNames.FACTORS[i] + ".csv", FileMode.OpenOrCreate))
                {
                    context.Data.SaveAsText(newData, s, ';');
                }
            }

            
        }

        public void UploadModel(string filePath)
        {
            if (!File.Exists(filePath)) return;

            model = context.Model.Load(filePath, out _); // Загрузка модели
            engine = context.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);
        }

        public string GetAccuracy()
        {
            IDataView predictions = model.Transform(data);
            RegressionMetrics metrics = context.Regression.Evaluate(predictions, ColumnNames.CRIMES_COUNT);
           
            return (metrics.RSquared * 100).ToString("0") + "%";
        }

        public ModelOutput Predict(ModelInput inputData)
        {
            return engine.Predict(inputData);
        }
    }
}
