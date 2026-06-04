using Detector.Repositories;

namespace Detector.Services
{
    public class Logs
    {
        private readonly LogRepository _repo;

        public Logs(LogRepository repo)
        {
            _repo = repo;
        }

        public void GerarLogRequisicao(string input)
        {
            _repo.GravarRequisicao(input);
        }

        public void GerarLogRequisicaoResposta(string input, string resposta)
        {
            _repo.GravarRequisicaoResposta(input, resposta);
        }
    }
}
