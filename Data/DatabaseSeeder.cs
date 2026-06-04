using Detector.Entidades;

namespace Detector.Data
{
    public static class DatabaseSeeder
    {
        public static void Seed(AppDbContext db)
        {
            if (db.DadosTreinamento.Any())
                return;

            var caminhoCSV = Path.Combine(AppContext.BaseDirectory, "MLModels", "codigo_csharp.csv");

            if (!File.Exists(caminhoCSV))
                return;

            var linhas = File.ReadAllLines(caminhoCSV)
                             .Skip(1)
                             .Where(l => !string.IsNullOrWhiteSpace(l))
                             .ToList();

            var dados = new List<DadoTreinamento>();

            foreach (var linha in linhas)
            {
                var partes = linha.Split(',', 2);
                if (partes.Length < 2) continue;

                var labelStr = partes[0].Trim('"').Trim();
                var texto    = partes[1].Trim('"').Trim();

                if (!bool.TryParse(labelStr, out var label)) continue;

                dados.Add(new DadoTreinamento
                {
                    Label    = label,
                    Text     = texto,
                    CriadoEm = DateTime.Now
                });
            }

            db.DadosTreinamento.AddRange(dados);
            db.SaveChanges();
        }
    }
}
