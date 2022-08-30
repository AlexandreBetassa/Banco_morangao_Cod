using System;

namespace Banco_Morangao
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Endereco endereco = coletarEndereco();
            Console.WriteLine("Endereço informado: ");
            Console.WriteLine(endereco);

        }

        //metodo para coletar endereco
        static Endereco coletarEndereco()
        {
            string logradouro, bairro, cidade, cep, complemento;
            int numero;

            Console.WriteLine("Informe o nome da rua: ");
            logradouro = Console.ReadLine();

            Console.WriteLine("Informe o numero: ");
            numero = int.Parse(Console.ReadLine());

            Console.WriteLine("Informe o bairro: ");
            bairro = Console.ReadLine();

            Console.WriteLine("Informe a cidade: ");
            cidade = Console.ReadLine();

            Console.WriteLine("Informe o CEP: ");
            cep = Console.ReadLine();

            Console.WriteLine("Informe o complemento: ");
            complemento = Console.ReadLine();

            return new Endereco(logradouro, numero.ToString(), bairro, cidade, cep, complemento);
        }



    }
}
