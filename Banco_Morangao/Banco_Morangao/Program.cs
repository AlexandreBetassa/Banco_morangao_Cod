using System;

namespace Banco_Morangao
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ContaCorrente cc = criarContaCorrente();
            Console.Clear();
            Console.WriteLine("CADASTRO DE CLIENTE");
            Console.WriteLine("DADOS DO CLIENTE CADASTRADO COM SUCESSO\n\n\n ");
            Console.WriteLine(cc);
        }


        //metodo para coletar endereco
        static Endereco coletarEndereco()
        {
            string logradouro, bairro, cidade, cep, complemento;
            int numero;
            bool checkNum;

            Console.WriteLine("Informe o nome o logradouro com rua ou avenida: ");
            logradouro = Console.ReadLine();

            Console.WriteLine("Informe o numero: ");
            do
            {
                checkNum = int.TryParse(Console.ReadLine(), out numero);
            } while (checkNum == false);

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

        //metodo cadastrar funcionario
        static Funcionario coletarFuncionario()
        {
            Pessoa pessoa;
            String cargo, nivelAcesso;

            pessoa = coletarPessoa();
            Console.WriteLine("Informe o cargo do funcionário: ");
            cargo = Console.ReadLine();

            Console.WriteLine("Informe o nível de acesso do funcionário: ");
            nivelAcesso = Console.ReadLine();

            return new Funcionario(pessoa, cargo, nivelAcesso);


        }

        //metodo cadastrar cliente
        static Cliente cadastrarCliente()
        {
            Pessoa pessoa;
            String estudante;
            float renda;
            bool checkRenda;

            pessoa = coletarPessoa();
            Console.WriteLine("Estudante (Informe Sim ou Nao)");
            estudante = Console.ReadLine();
            Console.WriteLine("Informe a renda: ");
            do
            {
                checkRenda = float.TryParse(Console.ReadLine(), out renda);
            } while (checkRenda == false);

            return new Cliente("Sim", pessoa, true, renda.ToString());
        }

        //criar conta corrente
        static ContaCorrente criarContaCorrente()
        {
            Cliente cliente;
            String agencia, tipoConta;
            float saldoInicial, renda;
            String dataVencimento;

            Console.WriteLine("INFORME OS DADOS DA CONTA");
            Console.WriteLine("Informe o numero da agencia: ");
            agencia = Console.ReadLine();
            Console.WriteLine("Informe o tipo de conta: ");
            tipoConta = Console.ReadLine();
            Console.WriteLine("Terá deposito inicial? ");
            saldoInicial = float.Parse(Console.ReadLine());
            Console.WriteLine("Informe a renda: ");
            renda = float.Parse(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("INFORME OS DADOS DO CLIENTE");
            cliente = cadastrarCliente();

            return new ContaCorrente(cliente, agencia, saldoInicial, tipoConta, renda);

        }

    }
}
