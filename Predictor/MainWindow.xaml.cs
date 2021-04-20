using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace Predictor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ModelInput sampleData = new ModelInput()
        {
            Year = 2010,
            Quarter = 1,
            Pop_density = 142.9F,
            Men_quan = 65.734F,
            Women_quan = 77.166F,
            Rate_unemp = 3.061938F,
            Psych_clinic = 1232F,
            Drug_clinic = 1074F,
            Family_incom = 0.22F,
            Child_homeless = 0.0032F,
            Quan_conviction = 0.014F,
            Quan_education = 0.361F,
            Cof_crime = 0.13F,
            Quan_migrant = 254089F,
            Quan_gun = 252F,
        };
        private PredictionEngine<ModelInput, ModelOutput> predictor;

        public MainWindow()
        {
            InitializeComponent();
            LenghtTextBox.Text = sampleData.Pop_density.ToString();
            WeightTextBox.Text = sampleData.Quan_migrant.ToString();
        }

        private void LoadScriptButton_Click(object sender, RoutedEventArgs e)
        {
            var context = new MLContext(1234);
            ITransformer model = null;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter =
                    "Database|*.csv|" +
                    "Model|*.zip";
            
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

            if (ofd.FileName.EndsWith(".csv"))
            {
                // Загрузка базы
                var data = context.Data.LoadFromTextFile<ModelInput>(ofd.FileName, hasHeader: true, separatorChar: ';');
                // Переработка
                var split = context.Data.TrainTestSplit(data, testFraction: 0.2);

                var pipeline = context.Transforms.Concatenate("Features", "year", "quarter", "pop_density", "men_quan", "women_quan", "rate_unemp", "psych_clinic", "drug_clinic", "family_incom", "child_homeless", "quan_conviction", "quan_education", "cof_crime", "quan_migrant", "quan_gun")
                                      .Append(context.Transforms.NormalizeMinMax("Features", "Features"))
                                      .AppendCacheCheckpoint(context)
                    
                                      .Append(context.Regression.Trainers.LbfgsPoissonRegression("crime_count", "Features", historySize: 500));

                model = pipeline.Fit(split.TrainSet);

                var predictions = model.Transform(split.TestSet);

                var metrics = context.Regression.Evaluate(predictions, "crime_count");

                EvaluateTextBlock.Text =
                    "RSquared: " + metrics.RSquared +
                    "Loss: " + metrics.LossFunction +
                    "Squared error" + metrics.MeanSquaredError;

                context.Model.Save(model, data.Schema, ofd.FileName.Replace(".csv", ".zip"));
            }
            else if (ofd.FileName.EndsWith(".zip"))
            {
                model = context.Model.Load(ofd.FileName, out _);
            }

            // Создание предсказателя
            predictor = context.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);

        }

        private void Predict()
        {
            var prediction = predictor.Predict(sampleData);

            SexTextBlock.Text = prediction.Score.ToString();
        }

        private void LenghtTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (predictor == null || LenghtTextBox.Text.Length == 0) return;

            sampleData.Pop_density = float.Parse(LenghtTextBox.Text);
            Predict();
        }

        private void WeightTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (predictor == null || WeightTextBox.Text.Length == 0) return;

            sampleData.Quan_migrant = float.Parse(WeightTextBox.Text);
            Predict();
        }
    }
}
