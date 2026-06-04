using Detector.Data;
using Detector.Entidades;
using Detector.Enums;

namespace Detector.Repositories
{
    public class LogRepository
    {
        private readonly AppDbContext _db;

        public LogRepository(AppDbContext db)
        {
            _db = db;
        }

        public void GravarRequisicao(string input)
        {
            _db.LogsRequisicao.Add(new LogRequisicao
            {
                Input    = input,
                DataHora = DateTime.Now
            });
            _db.SaveChanges();
        }

        public List<LogRequisicao> ObterRequisicoes()
            => _db.LogsRequisicao.OrderByDescending(x => x.DataHora).ToList();

        public void GravarRequisicaoResposta(string input, string resposta)
        {
            _db.LogsRequisicaoResposta.Add(new LogRequisicaoResposta
            {
                Input    = input,
                Resposta = resposta,
                DataHora = DateTime.Now
            });
            _db.SaveChanges();
        }

        public List<LogRequisicaoResposta> ObterRequisicaoRespostas()
            => _db.LogsRequisicaoResposta.OrderByDescending(x => x.DataHora).ToList();

        public void GravarFeedback(string input, string respostaIA, EnumTipoFeedback tipo)
        {
            _db.LogsFeedback.Add(new LogFeedback
            {
                Input        = input,
                RespostaIA   = respostaIA,
                TipoFeedback = tipo,
                DataHora     = DateTime.Now
            });
            _db.SaveChanges();
        }

        public List<LogFeedback> ObterFeedbacks()
            => _db.LogsFeedback.OrderByDescending(x => x.DataHora).ToList();
    }
}
