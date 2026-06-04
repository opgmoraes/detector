using Microsoft.AspNetCore.Mvc.RazorPages;
using Detector.Entidades;
using Detector.Repositories;

namespace Detector.Pages
{
    public class LogsModel : PageModel
    {
        private readonly LogRepository _repo;

        public LogsModel(LogRepository repo)
        {
            _repo = repo;
        }

        public List<LogRequisicao> LogRequisicao { get; set; } = new();
        public List<LogRequisicaoResposta> LogRequisicaoResposta { get; set; } = new();
        public List<LogFeedback> LogFeedback { get; set; } = new();

        public void OnGet()
        {
            LogRequisicao         = _repo.ObterRequisicoes();
            LogRequisicaoResposta = _repo.ObterRequisicaoRespostas();
            LogFeedback           = _repo.ObterFeedbacks();
        }
    }
}
