using Microsoft.ML;
using Detector.Models;
using Detector.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Treina o modelo se ainda não existir
var pastaModelos = Path.Combine(AppContext.BaseDirectory, "MLModels");
Directory.CreateDirectory(pastaModelos);

if (!File.Exists(Path.Combine(pastaModelos, "model.zip")))
    ModelBuilder.Treinar(pastaModelos);

var mlContext = new MLContext();
var modelPath = Path.Combine(pastaModelos, "model.zip");
var model = mlContext.Model.Load(modelPath, out _);
var engine = mlContext.Model.CreatePredictionEngine<CodigoData, CodigoPrediction>(model);

builder.Services.AddSingleton(engine);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.MapPost("/predict", (PredictRequest request, PredictionEngine<CodigoData, CodigoPrediction> engine) =>
{
    var prediction = engine.Predict(new CodigoData { Text = request.Text });
    return Results.Ok(prediction);
});

app.Run();
