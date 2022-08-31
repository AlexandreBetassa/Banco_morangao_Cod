using System;
using System.Net.Http.Headers;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;

namespace Banco_Morangao
{
    internal class Program
    {
        static Agencia agencia = new Agencia();

        static void Main(string[] args)
        {
            do
            {
                if (MenuSistema() == 1)
                {
                    Console.Clear();
                    Console.WriteLine("### MENU CLIENTE ###");
                    MenuCliente();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("### MENU FUNCIONÁRIO ###");
                    MenuFuncionario();
                }
            } while (true);

        }

        #region Menus
        //Menu
        static int MenuSistema()
        {
            int op;
            bool aux;
            Console.Write("Informe se voce é cliente ou funcionário: (1 - Cliente ou 2 - Funcionário)\n>");
            do
            {
                aux = int.TryParse(Console.ReadLine(), out op);
            } while ((op != 1 && op != 2) || aux == false);
            return op;
        }

        //Solicitar numero da agencia
        static String SolicitarAgencia()
        {
            bool aux;
            int agencia;

            //informe o numero da agencia
            Console.Write("Informe o número da agência: ");
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
            Console.Write("Informe o número da conta: ");
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
            ContaCorrente conta = agencia.BuscarContaCorrente(SolicitarAgencia(), SolicitarNumConta());

            Console.Write("Informe a operação:\n(1 - Sacar / 2 - Depositar / 3 - Pagar\n4 - Solicitar empréstimo / 5 - Consultas / 6 - Transferir\n>)");
            do
            {
                aux = int.TryParse(Console.ReadLine(), out op);
            } while ((op < 1 && op > 5) || aux == false);

            Console.Clear();
            switch (op)
            {
                case 1:
                    Console.WriteLine("### SACAR ###");
                    Sacar(conta);
                    break;
                case 2:
                    Console.WriteLine("### DEPOSITAR ###");
                    Depositar(conta);
                    break;
                case 3:
                    Console.WriteLine("### PAGAR ###");
                    Sacar(conta);
                    break;
                case 4:
                    Console.WriteLine("### EMPRÉSTIMO ###");
                    break;
                case 5:
                    Console.WriteLine("### CONSULTAS ###");
                    Consultar(conta);
                    break;
                case 6:
                    Console.WriteLine("### TRANSFERIR ###");
                    Tranferir(conta);
                    break;
            }
        }

        //Menu funcionario
        static void MenuFuncionario()
        {
            int op;
            bool aux;
            Console.Write("Informe a operação: 1 - Cadastrar Cliente / 2 - Listar Clientes / 3 - Menu do gerente\n> ");
            do
            {
                aux = int.TryParse(Console.ReadLine(), out op);
            } while ((op < 1 || op > 3) || aux == false);

            switch (op)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("### CADASTRAR CLIENTE ###");
                    CadastrarCliente();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("### LISTAR CLIENTES ###");
                    agencia.getListClientes();
                    agencia.getListConta();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("### FUNÇÕES GERENTES ###");
                    MenuGerente();
                    break;
            }
        }

        //Menu gerente
        static void MenuGerente()
        {
            int op;
            bool aux;
            Console.Write("Informe a operação: 1 - Cadastrar Funcionário / 2 - Listar Funcionário / 3 - Aprovações\n>");
            do
            {
                aux = int.TryParse(Console.ReadLine(), out op);
            } while ((op < 1 || op > 3) || aux == false);

            switch (op)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("### CADASTRAR FUNCIONÁRIO ###");
                    CadastrarFuncionario();
                    Console.WriteLine("\n\n### FUNCIONÁRIO CADASTRADO COM SUCESSO ###\nPRESSIONE QUALQUER TECLA...");
                    Console.ReadKey();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("### LISTAR FUNCIONÁRIOS ###");
                    agencia.getListFuncionarios();
                    Console.WriteLine("\n\n### FIM ###\nPRESSIONE QUALQUER TECLA...");
                    Console.ReadKey();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("### APROVAR SOLICITAÇÕES###");
                    Aprovacao();
                    Console.WriteLine("\n\n### FIM ###\nPRESSIONE QUALQUER TECLA...");
                    Console.ReadKey();
                    break;
            }
        }
        #endregion Menus

        #region Cliente
        //TRANSFERIR
        static void Tranferir(ContaCorrente conta)
        {
            bool aux;
            float valor;
            Console.WriteLine("INFORME OS DADOS DA CONTA CORRENTE DESTINO");
            ContaCorrente contaDestino = agencia.BuscarContaCorrente(SolicitarAgencia(), SolicitarNumConta());
            Console.Write("Informe o valor: ");
            do
            {
                aux = float.TryParse(Console.ReadLine(), out valor);
            } while (aux == false);
            conta.MovimentarSaida("Transferência", valor);
            contaDestino.MovimentarEntrada("Transferência", valor);

            Console.WriteLine($"Saldo conta titular: {conta.getSaldo()}");
            Console.WriteLine($"Saldo conta destino: {contaDestino.getSaldo()}");
        }

        //CONSULTAS
        static void Consultar(ContaCorrente conta)
        {
            int op;
            bool aux;

            Console.Write("O que deseja consultar: 1 - Saldo / 2 - Extrato");
            do
            {
                aux = int.TryParse(Console.ReadLine(), out op);
            } while (aux == false);

            if (op == 1) Console.WriteLine("Saldo Atual conta corrente: " + conta.getSaldo().ToString("F"));
            else conta.getExtrato();
        }

        //DEPOSITAR
        static void Depositar(ContaCorrente conta)
        {
            float valor;
            bool aux;
            Console.Write("Informe o valor: ");
            do
            {
                aux = float.TryParse(Console.ReadLine(), out valor);
            } while (aux == false);

            conta.MovimentarEntrada("Depósito", valor);
            Console.WriteLine("Saldo após transação: " + conta.getSaldo());
        }

        //SACAR
        static void Sacar(ContaCorrente conta)
        {
            float valor;
            bool aux;
            Console.Write("Informe o valor: ");
            do
            {
                aux = float.TryParse(Console.ReadLine(), out valor);
            } while (aux == false);

            conta.MovimentarSaida("Saque", valor);
            Console.WriteLine("Saldo após transação: " + conta.getSaldo());
        }

        #endregion Cliente

        #region Funcionário
        //metodo para coletar endereco
        static Endereco ColetarEndereco()
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
        static Pessoa ColetarPessoa()
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
            endereco = ColetarEndereco();

            return new Pessoa(nome, telefone, endereco, email, cpf, genero);
        }

        //metodo cadastrar funcionario
        static void CadastrarFuncionario()
        {
            Pessoa pessoa;
            String cargo, nivelAcesso;

            pessoa = ColetarPessoa();
            Console.WriteLine("Informe o cargo do funcionário: ");
            cargo = Console.ReadLine();

            Console.WriteLine("Informe o nível de acesso do funcionário: ");
            nivelAcesso = Console.ReadLine();

            Funcionario funcionario = new Funcionario(pessoa, cargo, nivelAcesso);
            agencia.setFuncList(funcionario);
        }

        //metodo cadastrar cliente
        static void CadastrarCliente()
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
            } while (checkRenda == false);

            pessoa = ColetarPessoa();
            conta = CriarContaCorrente(renda);

            Cliente cliente = new Cliente("Aguardando aprovação", pessoa, true, renda.ToString(), conta);
            agencia.setListaAprovacao(cliente);
        }

        //criar conta corrente
        static ContaCorrente CriarContaCorrente(float renda)
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

        #endregion Funcionário

        #region Gerente
        static void Aprovacao()
        {
            int op;
            bool aux;
            agencia.BuscarAprovacoes();
            Cliente cliente = agencia.BuscarAprovacoes();
            if (cliente != null)
            {
                Console.WriteLine(cliente.ToString());
                Console.WriteLine("Deseja aprovar a abertura dessa conta? 1 - Sim / 2 - Não");
                do
                {
                    aux = int.TryParse(Console.ReadLine(), out op);
                } while (aux == false);

                if (op == 1)
                {
                    cliente.conta.setHabilitado(true);
                    Console.WriteLine("Aprovado");
                    agencia.setContaList(cliente.conta);
                    agencia.DelListaAprovacao(cliente);
                }
                else
                {
                    cliente.conta.setHabilitado(false);
                    Console.WriteLine("Reprovado");
                    agencia.DelListaAprovacao(cliente);
                }
            }
        }

        #endregion Gerente
    }
}
