namespace Erik;

public class Folha
{
 public string Id { get; set; }
 public double Valor { get; set; }
 public double Quantidade { get; set; }
 public int Mes { get; set; }
 public int Ano { get; set; }
 public double SalarioBruto { get; set; }
 public double ImpostoIrrf { get; set; }
 public double ImpostoInns { get; set; }
 public double ImpostoFgts { get; set; }
 public double SalarioLiquido { get; set; }
 public string FuncionarioId { get; set; }
 public Funcionario Funcionario { get; set; }

 public Folha(){
    Id = Guid.NewGuid().ToString();
 }

 public Folha(int valor, int quantidade, int mes, int ano, string funcionarioId, Funcionario funcionario) {
    Id = Guid.NewGuid().ToString();
    Valor = valor;
    Quantidade = quantidade;
    Mes = mes;
    Ano = ano;
    FuncionarioId = funcionarioId;
    Funcionario = funcionario;
 }
}
