using Microsoft.ML.Data;

namespace Detector.Models
{
    public class CodigoPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool PredictedLabel { get; set; }   // true = erro detectado

        public float Probability { get; set; }

        public float Score { get; set; }
    }
}
