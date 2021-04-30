using Microsoft.ML.Data;
using System;

namespace Predictor
{
    public class ModelInput
    {
        [ColumnName(ColumnNames.DATE), LoadColumn(0)]
        public DateTime Date { get; set; }

        [ColumnName(ColumnNames.MEN_COUNT), LoadColumn(1)]
        public float MenCount { get; set; }

        [ColumnName(ColumnNames.WOMEN_COUNT), LoadColumn(2)]
        public float WomenCount { get; set; }

        [ColumnName(ColumnNames.UNMPLOYMENT_COUNT), LoadColumn(3)]
        public float UnemploymentCount { get; set; }

        [ColumnName(ColumnNames.PSYCH_COUNT), LoadColumn(4)]
        public float PsychCount { get; set; }

        [ColumnName(ColumnNames.DRUG_COUNT), LoadColumn(5)]
        public float DrugCount { get; set; }

        [ColumnName(ColumnNames.NOT_FULL_FAMILIES), LoadColumn(6)]
        public float NotFullFamilies { get; set; }

        [ColumnName(ColumnNames.CRIME_COF), LoadColumn(7)]
        public float CrimeCOF { get; set; }

        [ColumnName(ColumnNames.POLICE_COUNT), LoadColumn(8)]
        public float PoliceCount { get; set; }

        [ColumnName(ColumnNames.MORTALITY), LoadColumn(9)]
        public float Mortality { get; set; }

        [ColumnName(ColumnNames.AVERANGE_INCOME), LoadColumn(10)]
        public float AverangeIncome { get; set; }

        [ColumnName(ColumnNames.CRIMES_COUNT), LoadColumn(11)]
        public float CrimesCount { get; set; }
    }
}
