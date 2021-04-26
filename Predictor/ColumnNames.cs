using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Predictor
{
    class ColumnNames
    {
        public const string YEAR = "Year";
        public const string QUARTER = "Quarter";
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

        public static readonly string[] FACTORS = { YEAR, QUARTER, POPULATION, MEN_POPUL,
                                                    WOMEN_POPUL, RATE_UNMPLOYMENT, PSYCH_POPUL,
                                                    DRUG_POPUL, NOT_FULL_FAMILIES, HOMELESS_CHILDREN_POPUL,
                                                    CONVICTION_POPUL, EDUCATED_POPUL, CRIME_COF,
                                                    MIGRANT_POPUL, GUN_COUNT };
    }
}
