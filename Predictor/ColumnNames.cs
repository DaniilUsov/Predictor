namespace Predictor
{
    class ColumnNames
    {
        public const string DATE = "Date";
        public const string MEN_COUNT = "MenCount";
        public const string WOMEN_COUNT = "WomenCount";
        public const string UNMPLOYMENT_COUNT = "UnemploymentCount";
        public const string PSYCH_COUNT = "PsychCount";
        public const string DRUG_COUNT = "DrugCount";
        public const string NOT_FULL_FAMILIES = "NotFullFamilies";
        public const string CRIME_COF = "CrimeCOF";
        public const string POLICE_COUNT = "PoliceCount";
        public const string MORTALITY = "Mortality";
        public const string AVERANGE_INCOME = "AverangeIncome";
        public const string CRIMES_COUNT = "CrimesCount";

        public static readonly string[] FACTORS = 
        { 
            MEN_COUNT, WOMEN_COUNT, UNMPLOYMENT_COUNT,
            PSYCH_COUNT, DRUG_COUNT, NOT_FULL_FAMILIES, CRIME_COF,
            POLICE_COUNT, MORTALITY, AVERANGE_INCOME
        };
        public static readonly string[] ALL =
        { 
            DATE, MEN_COUNT, WOMEN_COUNT, UNMPLOYMENT_COUNT,
            PSYCH_COUNT, DRUG_COUNT, NOT_FULL_FAMILIES, CRIME_COF,
            POLICE_COUNT, MORTALITY, AVERANGE_INCOME
        };
    }
}
