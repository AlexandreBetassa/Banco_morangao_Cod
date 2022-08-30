using System;

namespace Banco_Morangao
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Pessoa pessoa;

            pessoa = coletarPessoa();

            Console.Clear();
            Console.WriteLine("Dados da pessoa: ");
            Console.WriteLine(pessoa);



        }

        //metodo para coletar endereco
        static Endereco coletarEndereco()
        {
            string logradouro, bairro, cidade, cep, complemento;
            int numero;

            Console.WriteLine("Informe o nome o logradouro com rua ou avenida: ");
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

        //metodo coletar dados pessoa
        static Pessoa coletarPessoa()
        {
            string nome, telefone, email, cpf, genero;
            Endereco endereco;

            Console.WriteLine("Informe o Nome da pessoa: ");
            nome = Console.ReadLine();

            Console.WriteLine("Informe o telefone: ");
            telefone = Console.ReadLine();

            Console.WriteLine("Informe o CPF: ");
            cpf = Console.ReadLine();

            Console.WriteLine("Informe o E-mail (Caso Houver):");
            email = Console.ReadLine();

            Console.WriteLine("Informe o sexo da pessoa: ");
            genero = Console.ReadLine();

            Console.WriteLine("Informe o endereço: ");
            endereco = coletarEndereco();

            return new Pessoa(nome, telefone, endereco, email, cpf, genero);
        }


    }
}
