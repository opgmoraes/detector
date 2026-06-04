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
        private readonly Logs _logs;
        private readonly Feedback _feedback;

        public IndexModel(
            PredictionEngine<CodigoData, CodigoPrediction> predictionEngine,
            Logs logs,
            Feedback feedback)
        {
            _predictionEngine = predictionEngine;
            _logs = logs;
            _feedback = feedback;
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

            _logs.GerarLogRequisicao(InputCode);

            var inputData = new CodigoData { Text = InputCode };
            PredictionResult = _predictionEngine.Predict(inputData);

            _logs.GerarLogRequisicaoResposta(InputCode, PredictionResult.PredictedLabel.ToString());

            return Page();
        }

        public IActionResult OnPostFeedback(string inputCode, string respostaIA, int tipoFeedback)
        {
            var tipoEnum = tipoFeedback switch
            {
                1 => EnumTipoFeedback.Positivo,
                2 => EnumTipoFeedback.Negativo,
                _ => EnumTipoFeedback.Neutro
            };

            _feedback.GerarFeedback(inputCode, respostaIA, tipoEnum);

            FeedbackEnviado = true;
            return Page();
        }
    }
}
