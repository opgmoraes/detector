namespace Detector.Services
{
    public class Logs
    {
        public void GerarLogRequisicao(string input)
        {
            Directory.CreateDirectory("Logs");
            string log = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") + " - " + input;
            string Caminho = @"Logs\LogRequisicao.txt";
            File.AppendAllText(Caminho, log + "\n");
        }

        public void GerarLogRequisicaoResposta(string input, string resposta)
        {
            Directory.CreateDirectory("Logs");
            string log = DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss.fff") + " - " + input + " - " + resposta;
            string Caminho = @"Logs\LogRequisicaoResposta.txt";
            File.AppendAllText(Caminho, log + "\n");
        }
    }
}
