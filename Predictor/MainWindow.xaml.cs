using Microsoft.ML.Data;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System;

namespace Predictor
{
    public partial class MainWindow : Window
    {
        private ModelInput sampleData = new ModelInput() 
        { 
            Date = "01.01.2021", Population = 146.7f, MenPopul = 67.482f, WomenPopul = 79.218f,
            RateUnemployment = 4.490805f, PsychPopul = 1728, DrugPopul = 2170.3f, 
            NotFullFamilies = 0.349f, HomelessChildrensPopul = 0.00535f, ConvictionPopul = 0.0226f,
            EducatedPopul = 0.393f, CrimeCOF = 0.216f, MigrantPopul = 250677, GunCount = 63
        };
        private MyPredictor predictor = new MyPredictor();

        public MainWindow()
        {
            InitializeComponent();

            DataTB.DataContext = sampleData;
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

            predictor.TryLoad();

            Chart.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            Chart.ChartAreas[0].AxisX.Title = "Учетный период";
            Chart.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
            Chart.ChartAreas[0].AxisY.Title = "Кол-во преступлений, тыс";
            Chart.Series[0].Color = System.Drawing.Color.Red;
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Database|*.csv|" +
                         "Model|*.zip";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;
            PathTB.Text = ofd.FileName;

            if (ofd.FileName.EndsWith(".csv"))
            {
                predictor.GenerateModel(ofd.FileName);
                // Построение графика
                string[] s = predictor.Data.GetColumn<string>(ColumnNames.DATE).ToArray();
                DateTime[] axisXData = new DateTime[s.Length];
                for (int i = 0; i < s.Length; i++)
                {
                    axisXData[i] = DateTime.Parse(s[i]);
                }
                
                float[] axisYData = predictor.Data.GetColumn<float>(ColumnNames.CRIMES_COUNT).ToArray();
                Chart.Series[0].Points.DataBindXY(axisXData, axisYData);
                UpdateChart();
            }
            else if (ofd.FileName.EndsWith(".zip"))
            {
                predictor.UploadModel(ofd.FileName);
            }
        }

        private void PredictButton_Click(object sender, RoutedEventArgs e)
        {
            if (predictor.Loaded) CrimeTB.Text = (sampleData.CrimesCount = predictor.Predict(sampleData).Score).ToString("0.000");
            else System.Windows.Forms.MessageBox.Show("Отсутствует модель. Требуется обучение по основе базе данных");
            Chart.Series[0].Points.AddXY(DateTime.Parse(sampleData.Date), Math.Round(sampleData.CrimesCount, 3));
            UpdateChart();
        }

        private void UpdateChart()
        {
            Chart.Series[0].Sort(PointSortOrder.Ascending, "X");
            foreach (DataPoint dp in Chart.Series[0].Points)
            {
                dp.ToolTip = DateTime.FromOADate(dp.XValue).ToString("dd.MM.yyyy");
            }
        }
    }
}
