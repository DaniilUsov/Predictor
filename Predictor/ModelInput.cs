using Microsoft.ML.Data;

namespace Predictor
{
    public class ModelInput
    {
        [ColumnName(ColumnNames.YEAR), LoadColumn(0)]
        public float Year { get; set; }


        [ColumnName(ColumnNames.QUARTER), LoadColumn(1)]
        public float Quarter { get; set; }


        [ColumnName(ColumnNames.POPULATION), LoadColumn(2)]
        public float Population { get; set; }


        [ColumnName(ColumnNames.MEN_POPUL), LoadColumn(3)]
        public float MenPopul { get; set; }


        [ColumnName(ColumnNames.WOMEN_POPUL), LoadColumn(4)]
        public float WomenPopul { get; set; }


        [ColumnName(ColumnNames.RATE_UNMPLOYMENT), LoadColumn(5)]
        public float RateUnemployment { get; set; }


        [ColumnName(ColumnNames.PSYCH_POPUL), LoadColumn(6)]
        public float PsychPopul { get; set; }


        [ColumnName(ColumnNames.DRUG_POPUL), LoadColumn(7)]
        public float DrugPopul { get; set; }


        [ColumnName(ColumnNames.NOT_FULL_FAMILIES), LoadColumn(8)]
        public float NotFullFamilies { get; set; }


        [ColumnName(ColumnNames.HOMELESS_CHILDREN_POPUL), LoadColumn(9)]
        public float HomelessChildrensPopul { get; set; }


        [ColumnName(ColumnNames.CONVICTION_POPUL), LoadColumn(10)]
        public float ConvictionPopul { get; set; }


        [ColumnName(ColumnNames.EDUCATED_POPUL), LoadColumn(11)]
        public float EducatedPopul { get; set; }


        [ColumnName(ColumnNames.CRIME_COF), LoadColumn(12)]
        public float CrimeCOF { get; set; }


        [ColumnName(ColumnNames.MIGRANT_POPUL), LoadColumn(13)]
        public float MigrantPopul { get; set; }


        [ColumnName(ColumnNames.GUN_COUNT), LoadColumn(14)]
        public float GunCount { get; set; }


        [ColumnName(ColumnNames.CRIMES_COUNT), LoadColumn(15)]
        public float CrimesCount { get; set; }


    }
}
