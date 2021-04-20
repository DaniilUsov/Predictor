using System;
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
        private Input input = new Input();
        private PredictionEngine<Input, Output> predictor;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadScriptButton_Click(object sender, RoutedEventArgs e)
        {
            var context = new MLContext(12345);
            ITransformer model = null;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter =
                    "Database|*.csv|" +
                    "Model|*.zip";
            
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

            if (ofd.FileName.EndsWith(".csv"))
            {
                // Загрузка базы
                var data = context.Data.LoadFromTextFile<Input>(ofd.FileName, hasHeader: true, separatorChar: ',');
                // Переработка
                var pipeline =
                    context.Transforms.Concatenate("Features", "Lenght", "Weight")
                    .Append(context.Regression.Trainers.FastTree(numberOfTrees: 200, minimumExampleCountPerLeaf: 4));
                // Моделирование
                model = pipeline.Fit(data);

                context.Model.Save(model, data.Schema, ofd.FileName.Replace(".csv", ".zip"));
            } 
            else if (ofd.FileName.EndsWith(".zip"))
            {
                model = context.Model.Load(ofd.FileName, out _);
            }

            // Создание предсказателя
            predictor = context.Model.CreatePredictionEngine<Input, Output>(model);

        }

        public class Input
        {
            [LoadColumn(0)]
            public float Lenght;

            [LoadColumn(1)]
            public float Weight;

            [LoadColumn(2), ColumnName("Label")]
            public float Sex;
        }

        public class Output
        {
            [ColumnName("Score")]
            public float Sex;
        }

        private void Predict()
        {
            var prediction = predictor.Predict(input);

            SexTextBlock.Text = prediction.Sex.ToString();
        }

        private void LenghtTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (predictor == null || LenghtTextBox.Text.Length == 0) return;

            input.Lenght = int.Parse(LenghtTextBox.Text);
            Predict();
        }

        private void WeightTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (predictor == null || WeightTextBox.Text.Length == 0) return;

            input.Weight = int.Parse(WeightTextBox.Text);
            Predict();
        }
    }
}
