using Microsoft.ML.Data;

namespace Predictor
{
    public class ModelInput
    {
        [ColumnName("year"), LoadColumn(0)]
        public float Year { get; set; }


        [ColumnName("quarter"), LoadColumn(1)]
        public float Quarter { get; set; }


        [ColumnName("pop_density"), LoadColumn(2)]
        public float Pop_density { get; set; }


        [ColumnName("men_quan"), LoadColumn(3)]
        public float Men_quan { get; set; }


        [ColumnName("women_quan"), LoadColumn(4)]
        public float Women_quan { get; set; }


        [ColumnName("rate_unemp"), LoadColumn(5)]
        public float Rate_unemp { get; set; }


        [ColumnName("psych_clinic"), LoadColumn(6)]
        public float Psych_clinic { get; set; }


        [ColumnName("drug_clinic"), LoadColumn(7)]
        public float Drug_clinic { get; set; }


        [ColumnName("family_incom"), LoadColumn(8)]
        public float Family_incom { get; set; }


        [ColumnName("child_homeless"), LoadColumn(9)]
        public float Child_homeless { get; set; }


        [ColumnName("quan_conviction"), LoadColumn(10)]
        public float Quan_conviction { get; set; }


        [ColumnName("quan_education"), LoadColumn(11)]
        public float Quan_education { get; set; }


        [ColumnName("cof_crime"), LoadColumn(12)]
        public float Cof_crime { get; set; }


        [ColumnName("quan_migrant"), LoadColumn(13)]
        public float Quan_migrant { get; set; }


        [ColumnName("quan_gun"), LoadColumn(14)]
        public float Quan_gun { get; set; }


        [ColumnName("crime_count"), LoadColumn(15)]
        public float Crime_count { get; set; }


    }
}
