using Detector.Enums;

namespace Detector.Entidades
{
    public class LogFeedback : Base
    {
        public string Input { get; set; } = string.Empty;
        public string RespostaIA { get; set; } = string.Empty;
        public EnumTipoFeedback TipoFeedback { get; set; }
    }
}