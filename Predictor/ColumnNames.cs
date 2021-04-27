namespace Predictor
{
    class ColumnNames
    {
        public const string DATE = "Date";
        public const string POPULATION = "Population";
        public const string MEN_POPUL = "MenPopul";
        public const string WOMEN_POPUL = "WomenPopul";
        public const string RATE_UNMPLOYMENT = "RateUnemployment";
        public const string PSYCH_POPUL = "PsychPopul";
        public const string DRUG_POPUL = "DrugPopul";
        public const string NOT_FULL_FAMILIES = "NotFullFamilies";
        public const string HOMELESS_CHILDREN_POPUL = "HomelessChildrensPopul";
        public const string CONVICTION_POPUL = "ConvictionPopul";
        public const string EDUCATED_POPUL = "EducatedPopul";
        public const string CRIME_COF = "CrimeCOF";
        public const string MIGRANT_POPUL = "MigrantPopul";
        public const string GUN_COUNT = "GunCount";
        public const string CRIMES_COUNT = "CrimesCount";

        public static readonly string[] FACTORS = { DATE, POPULATION, MEN_POPUL,
                                                    WOMEN_POPUL, RATE_UNMPLOYMENT, PSYCH_POPUL,
                                                    DRUG_POPUL, NOT_FULL_FAMILIES, HOMELESS_CHILDREN_POPUL,
                                                    CONVICTION_POPUL, EDUCATED_POPUL, CRIME_COF,
                                                    MIGRANT_POPUL, GUN_COUNT };
        public static readonly string[] ALL = { DATE, POPULATION, MEN_POPUL,
                                                WOMEN_POPUL, RATE_UNMPLOYMENT, PSYCH_POPUL,
                                                DRUG_POPUL, NOT_FULL_FAMILIES, HOMELESS_CHILDREN_POPUL,
                                                CONVICTION_POPUL, EDUCATED_POPUL, CRIME_COF,
                                                MIGRANT_POPUL, GUN_COUNT, CRIMES_COUNT};
    }
}
