# Detector — Detector de Erros em Código C# com ML.NET

## O que foi alterado nesta versão

### 1. Banco de Dados SQLite (substituiu arquivos .txt)
Os logs antes gravados em arquivos `.txt` agora são armazenados em um banco SQLite (`detector.db`).

| Antes (arquivo) | Agora (tabela no banco) |
|---|---|
| `Logs/LogRequisicao.txt` | `LogsRequisicao` |
| `Logs/LogRequisicaoResposta.txt` | `LogsRequisicaoResposta` |
| `Logs/LogFeedback.txt` | `LogsFeedback` |
| `MLModels/codigo_csharp.csv` | `DadosTreinamento` |

### 2. CSV substituído pelo banco
O arquivo `codigo_csharp.csv` ainda é usado **apenas na primeira execução** para popular o banco automaticamente. Depois disso, todos os dados de treinamento ficam na tabela `DadosTreinamento`.

### 3. Novas classes criadas

**Models/**
- `LogRequisicao.cs` — entidade da tabela de requisições
- `LogRequisicaoResposta.cs` — entidade da tabela de respostas
- `LogFeedback.cs` — entidade da tabela de feedbacks
- `DadoTreinamento.cs` — entidade da tabela de dados de treino (substituiu o CSV)

**Data/**
- `DetectorDbContext.cs` — contexto do Entity Framework Core
- `DatabaseSeeder.cs` — importa o CSV para o banco na primeira execução

**Repositories/**
- `LogRepository.cs` — acesso aos logs no banco
- `TreinamentoRepository.cs` — acesso aos dados de treinamento no banco

---

## Como instalar e rodar

### Requisitos
- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- Visual Studio 2022 **ou** VS Code com extensão C#

### Passo a passo

1. Clone ou extraia o projeto
2. Abra a pasta no terminal e rode:

```bash
dotnet restore
dotnet run
```

3. Acesse `https://localhost:7XXX` (a porta aparece no terminal)

O banco `detector.db` é criado automaticamente na pasta do projeto.  
O CSV é importado para o banco na primeira execução.

---

## Tecnologias
- ASP.NET Core 6 Razor Pages
- ML.NET 4.0
- Entity Framework Core 6 + SQLite
