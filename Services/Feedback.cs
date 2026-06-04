using Detector.Enums;
using Detector.Repositories;

namespace Detector.Services
{
    public class Feedback
    {
        private readonly LogRepository _repo;

        public Feedback(LogRepository repo)
        {
            _repo = repo;
        }

        public void GerarFeedback(string input, string respostaIA, EnumTipoFeedback feedBack)
        {
            _repo.GravarFeedback(input, respostaIA, feedBack);
        }
    }
}
