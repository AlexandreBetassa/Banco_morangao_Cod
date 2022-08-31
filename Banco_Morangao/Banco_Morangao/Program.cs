using System;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;

namespace Banco_Morangao
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do
            {
                if (MenuSistema() == 1)
                {
                    MenuCliente();
                }
                else MenuFuncionario();

            } while (true);

        }

        //Menu
        static int MenuSistema()
        {
            int op;
            bool aux;

            Console.Write("Informe se voce é cliente ou funcionário: (1 - Cliente ou 2 - Funcionário)");
            do
            {
                aux = int.TryParse(Console.ReadLine(), out op);
            } while ((op != 1 || op != 2) || aux == false);
            return op;
        }

        //Solicitar numero da agencia
        static String SolicitarAgencia()
        {
            bool aux;
            int agencia;

            //informe o numero da agencia
            Console.WriteLine("Informe o número da agência: ");
            do
            {
                aux = int.TryParse(Console.ReadLine(), out agencia);
            } while (aux == false);

            return agencia.ToString();
        }

        //Solicitar numero da conta
        static String SolicitarNumConta()
        {
            bool aux;
            int numConta;

            //informe o numero da agencia
            Console.WriteLine("Informe o número da conta: ");
            do
            {
                aux = int.TryParse(Console.ReadLine(), out numConta);
            } while (aux == false);

            return numConta.ToString();
        }

        //Menu do cliente
        static void MenuCliente()
        {
            int op;
            bool aux;
            Console.Write("Informe a operação: (1 - Sacar / 2 - Depositar / 3 - Pagar / 4 - Solicitar empréstimo / 5 - Consultas)");
            do
            {
                aux = int.TryParse(Console.ReadLine(), out op);
            } while ((op < 1 || op > 5) || aux == false);

            Console.Clear();
            switch (op)
            {
                case 1:
                    Sacar();
                    break;
            }
        }

        //SACAR
        static void Sacar()
        {
            float valor;
            bool aux;
            Console.WriteLine("### SACAR ###");
            Console.Write("Informe o valor que deseja sacar: ");
            do
            {
                aux = float.TryParse(Console.ReadLine(), out valor);
            } while (aux == false);

        }

        //Menu funcionario
        static void MenuFuncionario()
        {
            int op;
            bool aux;
            Console.Write("Informe a operação: ");
            do
            {
                aux = int.TryParse(Console.ReadLine(), out op);
            } while ((op != 1 || op != 2) || aux == false);

        }




        //_________________________________________________________________________________________________
        //metodo para coletar endereco
        static Endereco coletarEndereco()
        {
            string logradouro, bairro, cidade, cep, complemento;
            int numero;
            bool checkNum;

            Console.WriteLine("Informe o logradouro com rua ou avenida: ");
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
            Console.Clear();
            Console.WriteLine("INFORME O ENDEREÇO");
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
            ContaCorrente conta;
            Pessoa pessoa;
            float renda;
            bool checkRenda;

            /* Console.WriteLine("Estudante (Informe Sim ou Nao)");
            estudante = Console.ReadLine();*/
            Console.WriteLine("Informe a renda: ");
            do
            {
                checkRenda = float.TryParse(Console.ReadLine(), out renda);
                pessoa = coletarPessoa();

            } while (checkRenda == false);
            conta = criarContaCorrente(renda);

            return new Cliente("Sim", pessoa, true, renda.ToString(), conta);
        }

        //criar conta corrente
        static ContaCorrente criarContaCorrente(float renda)
        {
            String agencia, tipoConta;
            float saldoInicial;

            Console.WriteLine("INFORME OS DADOS DA CONTA");
            Console.WriteLine("Informe o numero da agencia: ");
            agencia = Console.ReadLine();
            Console.WriteLine("Informe o tipo de conta: ");
            tipoConta = Console.ReadLine();
            Console.WriteLine("Terá deposito inicial? ");
            float.TryParse(Console.ReadLine(), out saldoInicial);
            Console.Clear();
            Console.WriteLine("INFORME OS DADOS DO CLIENTE");
            return new ContaCorrente(agencia, saldoInicial, tipoConta, renda);

        }

    }
}
