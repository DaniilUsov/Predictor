using Microsoft.ML.Data;

namespace Predictor
{
    public class ModelOutput
    {
        [ColumnName("Score")]
        public float CrimesCount { get; set; }
    }
}
