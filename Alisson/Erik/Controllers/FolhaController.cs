namespace Erik;

public static class FolhaController
{
    public static double CalcularDescontoINSS(double salarioBruto)
    {
        double descontoINSS = 0;

        if (salarioBruto <= 1693.72)
        {
            descontoINSS = salarioBruto * 0.08;
        }
        else if (salarioBruto <= 2822.90)
        {
            descontoINSS = salarioBruto * 0.09;
        }
        else if (salarioBruto <= 5645.80)
        {
            descontoINSS = salarioBruto * 0.11;
        }
        else
        {
            descontoINSS = 621.03;
        }

        return descontoINSS;
    }

    public static double CalcularImpostoRenda(double salarioBruto)
    {
        double impostoRenda = 0;

        if (salarioBruto <= 1903.98)
        {
            impostoRenda = 0;
        }
        else if (salarioBruto <= 2826.65)
        {
            impostoRenda = (salarioBruto * 0.075) - 142.80;
        }
        else if (salarioBruto <= 3751.05)
        {
            impostoRenda = (salarioBruto * 0.15) - 354.80;
        }
        else if (salarioBruto <= 4664.68)
        {
            impostoRenda = (salarioBruto * 0.225) - 636.13;
        }
        else
        {
            impostoRenda = (salarioBruto * 0.275) - 869.36;
        }

        return impostoRenda;
    } 
}
