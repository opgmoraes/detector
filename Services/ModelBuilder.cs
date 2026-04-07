using Microsoft.ML;
using Detector.Models;

namespace Detector.Services
{
    public static class ModelBuilder
    {
        public static void Treinar(string pastaModelos)
        {
            var ml = new MLContext(seed: 1);

            var data = ml.Data.LoadFromTextFile<CodigoData>(
                Path.Combine(pastaModelos, "codigo_csharp.csv"),
                hasHeader: true,
                separatorChar: ',',
                allowQuoting: true);

            var split = ml.Data.TrainTestSplit(data, testFraction: 0.2, seed: 1);

            var pipeline = ml.Transforms.Text
                .FeaturizeText("Features", nameof(CodigoData.Text))
                .Append(ml.BinaryClassification.Trainers.SdcaLogisticRegression(
                    labelColumnName: nameof(CodigoData.Label),
                    featureColumnName: "Features"));

            var model = pipeline.Fit(split.TrainSet);

            Directory.CreateDirectory(pastaModelos);
            var caminhoModelo = Path.Combine(pastaModelos, "model.zip");
            ml.Model.Save(model, split.TrainSet.Schema, caminhoModelo);
        }
    }
}
