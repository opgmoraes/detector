using Microsoft.ML;
using Microsoft.EntityFrameworkCore;
using Detector.Models;
using Detector.Services;
using Detector.Data;
using Detector.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
                     ?? "Data Source=detector.db"));

builder.Services.AddScoped<LogRepository>();
builder.Services.AddScoped<TreinamentoRepository>();
builder.Services.AddScoped<Logs>();
builder.Services.AddScoped<Feedback>();

var pastaModelos  = Path.Combine(AppContext.BaseDirectory, "MLModels");
var caminhoModelo = Path.Combine(pastaModelos, "model.zip");
var caminhoCSV    = Path.Combine(pastaModelos, "codigo_csharp.csv");

Directory.CreateDirectory(pastaModelos);

if (!File.Exists(caminhoModelo))
{
    if (!File.Exists(caminhoCSV))
        throw new FileNotFoundException($"CSV não encontrado em: {caminhoCSV}");

    Detector.Services.ModelBuilder.Treinar(pastaModelos);
}

var mlContext = new MLContext();
var model     = mlContext.Model.Load(caminhoModelo, out _);
var engine    = mlContext.Model.CreatePredictionEngine<CodigoData, CodigoPrediction>(model);

builder.Services.AddSingleton(engine);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    DatabaseSeeder.Seed(db);
}

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
