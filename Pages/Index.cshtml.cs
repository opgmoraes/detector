using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.ML;
using Detector.Models;
using Detector.Services;
using Detector.Enums;

namespace Detector.Pages
{
    public class IndexModel : PageModel
    {
        private readonly PredictionEngine<CodigoData, CodigoPrediction> _predictionEngine;

        public IndexModel(PredictionEngine<CodigoData, CodigoPrediction> predictionEngine)
        {
            _predictionEngine = predictionEngine;
        }

        [BindProperty]
        public string InputCode { get; set; } = string.Empty;

        public CodigoPrediction? PredictionResult { get; set; }

        public bool FeedbackEnviado { get; set; } = false;

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(InputCode))
                return Page();

            // Log da requisição
            var logs = new Logs();
            logs.GerarLogRequisicao(InputCode);

            // Predição
            var inputData = new CodigoData { Text = InputCode };
            PredictionResult = _predictionEngine.Predict(inputData);

            // Log da resposta
            logs.GerarLogRequisicaoResposta(InputCode, PredictionResult.PredictedLabel.ToString());

            // Feedback neutro automático (sem interação do usuário ainda)
            var feedback = new Feedback();
            feedback.GerarFeedback(InputCode, PredictionResult.PredictedLabel.ToString(), EnumTipoFeedback.Neutro);

            return Page();
        }

        public IActionResult OnPostFeedback(string RespostaIA, int TipoFeedback)
        {
            var tipoEnum = TipoFeedback switch
            {
                1 => EnumTipoFeedback.Positivo,
                2 => EnumTipoFeedback.Negativo,
                _ => EnumTipoFeedback.Neutro
            };

            var feedback = new Feedback();
            feedback.GerarFeedback(InputCode, RespostaIA, tipoEnum);

            FeedbackEnviado = true;
            return Page();
        }
    }
}
