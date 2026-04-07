namespace Detector.Models
{
    public class PredictionResponse
    {
        public bool Prediction { get; set; }
        public float Score { get; set; }
        public string Resultado => Prediction ? "Código com ERRO detectado" : "Código CORRETO";
        public bool TemErro => Prediction;
    }
}
