using System.ComponentModel.DataAnnotations;
using Erik;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDataContext>();

var app = builder.Build();

app.MapGet("/", () => "API folha de pagamento");

app.MapGet("/api/funcionario/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.Funcionarios.Any())
    {
        return Results.Ok(ctx.Funcionarios.ToList());
    }
    return Results.NotFound("Não existem Funcionarios na tabela");
});

app.MapPost("/api/funcionario/cadastrar", ([FromBody] Funcionario funcionario,
    [FromServices] AppDataContext ctx) =>
{
    List<ValidationResult> erros = new List<ValidationResult>();
    if (!Validator.TryValidateObject(
        funcionario, new ValidationContext(funcionario), erros, true
    ))
    {
        return Results.BadRequest(erros);
    }

    Funcionario? funcionarioEncontrado = ctx.Funcionarios.FirstOrDefault
        (x => x.CPF == funcionario.CPF);
    if (funcionarioEncontrado is null)
    {
        ctx.Funcionarios.Add(funcionario);
        ctx.SaveChanges();
        return Results.Created("", funcionario);
    }
    return Results.BadRequest("Já existe um funcionario com o mesmo cpf");

});
    //----------------------------------------------------------------------------

app.MapPost("/api/folha/cadastrar", ([FromBody] Folha folha, [FromServices] AppDataContext ctx) =>
{
    List<ValidationResult> erros = new List<ValidationResult>();
    if (!Validator.TryValidateObject(
        folha, new ValidationContext(folha), erros, true
    ))
    {
        return Results.BadRequest(erros);
    }

    Funcionario? funcionario = ctx.Funcionarios.FirstOrDefault(l => l.Id == folha.FuncionarioId);

    if (funcionario is null)
    {
        return Results.BadRequest("Funcionario não encontrado!");
    }

    folha.Funcionario = funcionario;
    folha.SalarioBruto = folha.Quantidade * folha.Valor;
    folha.ImpostoIrrf = FolhaController.CalcularImpostoRenda(folha.SalarioBruto);
    folha.ImpostoInns = FolhaController.CalcularDescontoINSS(folha.SalarioBruto);
    folha.ImpostoFgts = folha.SalarioBruto * 0.08;
    folha.SalarioLiquido = folha.SalarioBruto - folha.ImpostoIrrf - folha.ImpostoInns;

    ctx.Folhas.Add(folha);
    ctx.SaveChanges();

    return Results.Created("", folha);
});

app.MapGet("/api/folha/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.Folhas.Any())
    {
        List<Folha> folhaList = ctx.Folhas.ToList();
        for(int i = 0; i < folhaList.Count; i++){
            folhaList[i].Funcionario = ctx.Funcionarios.Find(folhaList[i].FuncionarioId);
        }
        return Results.Ok(folhaList);
    }
    return Results.NotFound("Não existem folhas na tabela");
});

app.MapGet("/api/folha/buscar/{cpf}/{mes}/{ano}", ([FromRoute] string cpf, int mes, int ano,
    [FromServices] AppDataContext ctx) =>
{
    Folha? folha = ctx.Folhas.FirstOrDefault(x => x.Funcionario.CPF == cpf && x.Ano.Equals(ano) && x.Mes.Equals(mes));
    if (folha is null)
    {
        return Results.NotFound("Folha não encontrada!");
    }
    folha.Funcionario = ctx.Funcionarios.Find(folha.FuncionarioId);
    return Results.Ok(folha);
});

app.Run();
