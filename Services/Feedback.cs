using Detector.Enums;

namespace Detector.Services
{
    public class Feedback
    {
        public void GerarFeedback(string input, string respostaIA, EnumTipoFeedback feedBack)
        {
            Directory.CreateDirectory("Logs");
            string tipo = feedBack.ToString();
            string log = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff")
                         + " - FEEDBACK: " + tipo
                         + " - INPUT: " + input
                         + " - RESPOSTA_IA: " + respostaIA;

            string Caminho = @"Logs\LogFeedback.txt";
            File.AppendAllText(Caminho, log + "\n");
        }
    }
}
