using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace Detector.Pages
{
    public class LogsModel : PageModel
    {
        public List<string> LogRequisicao { get; set; } = new();
        public List<string> LogRequisicaoResposta { get; set; } = new();
        public List<string> LogFeedback { get; set; } = new();

        public void OnGet()
        {
            LogRequisicao = LerLog(@"Logs\LogRequisicao.txt");
            LogRequisicaoResposta = LerLog(@"Logs\LogRequisicaoResposta.txt");
            LogFeedback = LerLog(@"Logs\LogFeedback.txt");
        }

        private static List<string> LerLog(string caminho)
        {
            if (!System.IO.File.Exists(caminho))
                return new List<string>();

            return System.IO.File.ReadAllLines(caminho)
                       .Where(l => !string.IsNullOrWhiteSpace(l))
                       .Reverse()
                       .ToList();
        }
    }
}
