using Detector.Data;
using Detector.Entidades;

namespace Detector.Repositories
{
    public class TreinamentoRepository
    {
        private readonly AppDbContext _db;

        public TreinamentoRepository(AppDbContext db)
        {
            _db = db;
        }

        public List<DadoTreinamento> ObterTodos()
            => _db.DadosTreinamento.ToList();

        public void Adicionar(DadoTreinamento dado)
        {
            _db.DadosTreinamento.Add(dado);
            _db.SaveChanges();
        }

        public int TotalRegistros()
            => _db.DadosTreinamento.Count();
    }
}
