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
            Date = DateTime.Parse("30.04.2021"), MenCount = 67482, WomenCount = 79218,
            UnemploymentCount = 4490.805f, PsychCount = 1728, DrugCount = 2170.3f, 
            NotFullFamilies = 0.349f, CrimeCOF = 0.216f, PoliceCount = 973.845f,
            Mortality = 1679.248f, AverangeIncome = 56044
        };
        private MyPredictor predictor = new MyPredictor();

        public MainWindow()
        {
            InitializeComponent();

            DataTB.DataContext = sampleData;
            MenTB.DataContext = sampleData;
            WomenTB.DataContext = sampleData;
            UnempTB.DataContext = sampleData;
            PsychTB.DataContext = sampleData;
            DrugTB.DataContext = sampleData;
            NotFullTB.DataContext = sampleData;
            CofCrimeTB.DataContext = sampleData;
            PoliceTB.DataContext = sampleData;
            MortalityTB.DataContext = sampleData;
            IncomeTB.DataContext = sampleData;

            predictor.TryLoad();

            CrimeChart.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            CrimeChart.ChartAreas[0].AxisX.Title = "Учетный период";
            CrimeChart.ChartAreas[0].AxisX.LineColor = System.Drawing.Color.White;
            CrimeChart.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.White;
            CrimeChart.ChartAreas[0].AxisX.InterlacedColor = System.Drawing.Color.White;
            CrimeChart.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Gray;
            CrimeChart.ChartAreas[0].AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            CrimeChart.ChartAreas[0].AxisX.MajorTickMark.LineColor = System.Drawing.Color.White;

            CrimeChart.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
            CrimeChart.ChartAreas[0].AxisY.Title = "Кол-во преступлений";
            CrimeChart.ChartAreas[0].AxisY.LineColor = System.Drawing.Color.White;
            CrimeChart.ChartAreas[0].AxisY.TitleForeColor = System.Drawing.Color.White;
            CrimeChart.ChartAreas[0].AxisY.InterlacedColor = System.Drawing.Color.White;
            CrimeChart.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray;
            CrimeChart.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            CrimeChart.ChartAreas[0].AxisY.MajorTickMark.LineColor = System.Drawing.Color.White;

            CrimeChart.Series[0].Color = System.Drawing.Color.Red;
            CrimeChart.Series[0].LabelForeColor = System.Drawing.Color.White;
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
                DateTime[] axisXData = predictor.Data.GetColumn<DateTime>(ColumnNames.DATE).ToArray();
                //DateTime[] axisXData = new DateTime[s.Length];
                //for (int i = 0; i < s.Length; i++)
                //{
                //    axisXData[i] = DateTime.Parse(s[i]);
                //}
                
                float[] axisYData = predictor.Data.GetColumn<float>(ColumnNames.CRIMES_COUNT).ToArray();
                CrimeChart.Series[0].Points.DataBindXY(axisXData, axisYData);
                UpdateChart();
            }
            else if (ofd.FileName.EndsWith(".zip"))
            {
                predictor.UploadModel(ofd.FileName);
            }
        }

        private void PredictButton_Click(object sender, RoutedEventArgs e)
        {
            if (predictor.Loaded) CrimeTB.Text = (sampleData.CrimesCount = predictor.Predict(sampleData).CrimesCount).ToString("0.000");
            else System.Windows.Forms.MessageBox.Show("Отсутствует модель. Требуется обучение по основе базе данных");
            CrimeChart.Series[0].Points.AddXY(sampleData.Date, Math.Round(sampleData.CrimesCount, 3));
            UpdateChart();
        }

        private void UpdateChart()
        {
            CrimeChart.Series[0].Sort(PointSortOrder.Ascending, "X");
            foreach (DataPoint dp in CrimeChart.Series[0].Points)
            {
                dp.ToolTip = DateTime.FromOADate(dp.XValue).ToString("dd.MM.yyyy");
            }
        }
    }
}
