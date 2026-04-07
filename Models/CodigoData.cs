using Microsoft.ML.Data;

namespace Detector.Models
{
    public class CodigoData
    {
        [LoadColumn(0)]
        public bool Label { get; set; }   // true = tem erro, false = correto

        [LoadColumn(1)]
        public string Text { get; set; } = string.Empty;  // trecho de código C#
    }
}
