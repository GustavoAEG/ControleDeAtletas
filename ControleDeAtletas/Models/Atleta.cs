using System;

public class Atleta
{
    public int Id { get; set; }
    public string NomeCompleto { get; set; }
    public string Apelido { get; set; }
    public DateTime DataNascimento { get; set; }
    public double Altura { get; set; }
    public double Peso { get; set; }
    public string Posicao { get; set; }
    public int NumeroCamisa { get; set; }
    public int Idade { get; set; }
    public double IMC { get; set; }
    public string ClassificacaoIMC { get; set; }

    public Atleta()
    {
    }

    public Atleta(int id, string nomeCompleto, string apelido, DateTime dataNascimento, double altura, double peso, string posicao, int numeroCamisa)
    {
        Id = id;
        NomeCompleto = nomeCompleto;
        Apelido = apelido;
        DataNascimento = dataNascimento;
        Altura = altura;
        Peso = peso;
        Posicao = posicao;
        NumeroCamisa = numeroCamisa;

        Idade = CalcularIdade();

        IMC = CalcularIMC(altura, peso);

        ClassificacaoIMC = ClassificarIMC(IMC);
    }

    private int CalcularIdade()
    {
        DateTime hoje = DateTime.Today;
        int idade = hoje.Year - DataNascimento.Year;
        if (DataNascimento.Date > hoje.AddYears(-idade))
            idade--;
        return idade;
    }

    public static double CalcularIMC(double altura, double peso)
    {
        return peso / (altura * altura);
    }

    public static string ClassificarIMC(double imc)
    {
        if (imc < 18.5)
        {
            return "Abaixo do peso normal";
        }
        else if (imc >= 18.5 && imc < 25)
        {
            return "Peso normal";
        }
        else if (imc >= 25 && imc < 30)
        {
            return "Sobrepeso";
        }
        else if (imc >= 30 && imc < 35)
        {
            return "Obesidade Classe I";

        }else if (imc >= 35 && imc < 40)
        {
            return "Obesidade Classe II";
        }
        else
        {
            return "Obesidade Classe III";
        }
    }
}
