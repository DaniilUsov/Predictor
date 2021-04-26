using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Trainers.FastTree;

namespace Predictor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ModelInput sampleData = new ModelInput();
        private MyPredictor predictor = new MyPredictor();

        public MainWindow()
        {
            InitializeComponent();

            YearTB.DataContext = sampleData;
            QuarterTB.DataContext = sampleData;
            PopulationTB.DataContext = sampleData;
            MenTB.DataContext = sampleData;
            WomenTB.DataContext = sampleData;
            UnempTB.DataContext = sampleData;
            PsychTB.DataContext = sampleData;
            DrugTB.DataContext = sampleData;
            IncomTB.DataContext = sampleData;
            ChildHomelessTB.DataContext = sampleData;
            ConvintionTB.DataContext = sampleData;
            EducationTB.DataContext = sampleData;
            CofCrimeTB.DataContext = sampleData;
            MigrantTB.DataContext = sampleData;
            GunTB.DataContext = sampleData;
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Database|*.csv|" +
                         "Model|*.zip";
            
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

            if (ofd.FileName.EndsWith(".csv"))
            {
                predictor.Generate(ofd.FileName);

                EvaluateTextBlock.Text = "Точность: "+ predictor.GetAccuracy();
                // Построение графика
                float[] years = predictor.Data.GetColumn<float>(ColumnNames.YEAR).ToArray();
                float[] quarter = predictor.Data.GetColumn<float>(ColumnNames.QUARTER).ToArray();
                string[] axisXData = new string[years.Length];
                
                for (int i = 0; i < years.Length; i++)
                {
                    axisXData[i] = years[i].ToString() + "/" + quarter[i].ToString();
                }
                
                float[] axisYData = predictor.Data.GetColumn<float>(ColumnNames.CRIMES_COUNT).ToArray();
                Chart.Series[0].Points.DataBindXY(axisXData, axisYData);
                Chart.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                Chart.ChartAreas[0].AxisX.Interval = 1;
                Chart.ChartAreas[0].AxisY.Interval = 100;
            }
            else if (ofd.FileName.EndsWith(".zip"))
            {
                predictor.UploadModel(ofd.FileName);
            }
        }

        private void PredictButton_Click(object sender, RoutedEventArgs e)
        {
            CrimeTB.Text = predictor.Predict(sampleData).Score.ToString("0.000");
        }
    }
}
