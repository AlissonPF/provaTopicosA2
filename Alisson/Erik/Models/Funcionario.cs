using System.Runtime.InteropServices;

namespace Erik;

public class Funcionario
{
  
  public string Id { get; set; }
  public string? Nome { get; set; }
  public string? CPF { get; set; }

  public Funcionario() {
    Id = Guid.NewGuid().ToString();
  }

  public Funcionario(string nome, string cpf) {
    Id = Guid.NewGuid().ToString();
    Nome = nome;
    CPF = cpf;
  }
}
