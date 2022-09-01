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
            string senhaFuncionario = "1111";
            string senha;
            string op;
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
                    Console.WriteLine("Informe a senha de funcionário: ");
                    senha = Console.ReadLine();
                    if (senha == senhaFuncionario)
                    {
                        MenuFuncionario();
                    }
                    else
                    {
                        Console.WriteLine("Senha Inválida");
                        Console.WriteLine("PRESSIONE QUALQUER TECLA PARA RETORNAR AO INICIO");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                Console.WriteLine("Deseja efetuar nova transação: ");
                op = ColetarString();

            } while (op != "NAO");
        }

        #region Menus
        //Menu
        static int MenuSistema()
        {
            int op;
            do
            {
                Console.Write("Informe se voce é cliente ou funcionário: (1 - Cliente ou 2 - Funcionário)\n>");
                op = ColetarValorInt();
            } while (op < 1 || op > 2);
            return op;
        }

        //Solicitar numero da agencia
        static String SolicitarAgencia()
        {
            int agencia;
            Console.WriteLine("Informe o número da agência: ");
            agencia = ColetarValorInt();

            return agencia.ToString();
        }

        //Solicitar numero da conta
        static String SolicitarNumConta()
        {
            int numConta;
            //informe o numero da agencia
            Console.Write("Informe o número da conta: ");
            numConta = ColetarValorInt();

            return numConta.ToString();
        }

        //Menu do cliente
        static void MenuCliente()
        {
            int op;
            string senha;
            ContaCorrente conta = agencia.BuscarContaCorrente(SolicitarAgencia(), SolicitarNumConta());
            if (conta == null)
            {
                Console.ReadKey();
                Console.Clear();
                return;
            }
            else
            {
                int i = 0;
                Console.WriteLine($"Por favor {conta.getNome()} informe sua senha para continuar");
                senha = ColetarString();
                do
                {
                    if (i == 2)
                    {
                        Console.WriteLine("Seu número de tentativas expirou");
                        Console.WriteLine("Pressione enter para continuar...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    Console.WriteLine($"Por favor {conta.getNome()} informe sua senha para continuar");
                    senha = ColetarString();
                    if (conta.getSenha() != senha)
                    {
                        Console.WriteLine($"Senha Incorreta\nTentativa {i + 1}");
                    }
                    else
                    {
                        break;
                    }
                    i++;
                } while (i < 3);
                Console.WriteLine($"Bem vindo: {conta.getNome()}");
            }
            do
            {
                Console.Write("Informe a operação:\n(1 - Sacar / 2 - Depositar / 3 - Pagar\n4 - Solicitar empréstimo / 5 - Consultas / 6 - Transferir\n>)");
                op = ColetarValorInt();
            } while ((op < 1 && op > 5));

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
            string senha;
            string senhaGerente = "2222";
            int op;
            Console.Write("Informe a operação: 1 - Cadastrar Cliente / 2 - Listar Clientes / 3 - Menu do gerente\n> ");
            do
            {
                op = ColetarValorInt();
            } while (op < 1 || op > 3);

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
                    Console.WriteLine("Informe a senha de gerente: ");
                    senha = Console.ReadLine();
                    if (senha == senhaGerente)
                    {
                        Console.Clear();
                        MenuGerente();
                    }
                    else Console.WriteLine("Senha inválida");
                    Console.WriteLine("PRESSIONE QUALQUER TECLA PARA CONTINUAR");
                    Console.ReadKey();
                    Console.Clear();
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
                op = ColetarValorInt();
            } while (op < 1 || op > 3);

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
            float valor;
            Console.WriteLine("INFORME OS DADOS DA CONTA CORRENTE DESTINO");
            ContaCorrente contaDestino = agencia.BuscarContaCorrente(SolicitarAgencia(), SolicitarNumConta());
            Console.Write("Informe o valor: ");
            valor = ColetarValorFloat();
            conta.MovimentarSaida("Transferência", valor);
            contaDestino.MovimentarEntrada("Transferência", valor);

            Console.WriteLine($"Saldo conta titular: {conta.getSaldo()}");
            Console.WriteLine($"Saldo conta destino: {contaDestino.getSaldo()}");
        }

        //CONSULTAS
        static void Consultar(ContaCorrente conta)
        {
            int op;
            Console.Write("O que deseja consultar: 1 - Saldo / 2 - Extrato");
            op = ColetarValorInt();
            if (op == 1) Console.WriteLine("Saldo Atual conta corrente: " + conta.getSaldo().ToString("F"));
            else conta.getExtrato();
        }

        //DEPOSITAR
        static void Depositar(ContaCorrente conta)
        {
            float valor;
            Console.Write("Informe o valor: ");
            valor = ColetarValorFloat();
            conta.MovimentarEntrada("Depósito", valor);
            Console.WriteLine("Saldo após transação: " + conta.getSaldo());
        }

        //SACAR
        static void Sacar(ContaCorrente conta)
        {
            float valor;
            bool aux;
            Console.Write("Informe o valor: ");
            valor = ColetarValorFloat();
            conta.MovimentarSaida("Saque", valor);
            Console.WriteLine("Saldo após transação: " + conta.getSaldo());
        }

        //FINALIZAR ESSA PARTE AMANHÃ
        static void SolicitarEmprestimo(ContaCorrente conta)
        {
            float valor;
            string confirmar;
            Console.WriteLine("Informe o valor que deseja emprestar:");
            valor = ColetarValorFloat();
            valor = CalcularCustoEmprestimo(valor, conta.getTipoConta());
            Console.WriteLine($"Custo do empréstimo: R$ {valor.ToString("F")}");
            Console.WriteLine("Deseja confirmar o empréstimo? sim ou nao");
            do
            {
                confirmar = ColetarString();
            } while (confirmar != "SIM" && confirmar != "NAO");
            if (confirmar == "SIM")
            {
                conta.MovimentarEntrada("Empréstimo", valor);
                conta.setExtrato("Deb. Empréstimo", valor);
            }
            else
            {
                Console.WriteLine("Operação cancelada!!!");

                return;
            }
        }
        //FINALIZAR AMANHA


        static float CalcularCustoEmprestimo(float valor, string tipoConta)
        {
            if (tipoConta == "NORMAL") return valor * (float)1.5;
            else if (tipoConta == "VIP") return valor * (float)1.1;
            else return valor * (float)1.2;
        }

        #endregion Cliente

        #region Funcionário
        //metodo para coletar endereco
        static Endereco ColetarEndereco()
        {
            string logradouro, bairro, cidade, cep, complemento, numero;

            Console.WriteLine("Informe o logradouro com rua ou avenida: ");
            logradouro = ColetarString();

            Console.WriteLine("Informe o numero: ");
            numero = ColetarString();

            Console.WriteLine("Informe o bairro: ");
            bairro = ColetarString();

            Console.WriteLine("Informe a cidade: ");
            cidade = ColetarString();

            Console.WriteLine("Informe o CEP: ");
            cep = ColetarString();

            Console.WriteLine("Informe o complemento: ");
            complemento = Console.ReadLine();

            return new Endereco(logradouro, numero, bairro, cidade, cep, complemento);
        }

        //metodo coletar dados pessoa
        static Pessoa ColetarPessoa()
        {
            string nome, telefone, email, cpf, genero;
            Endereco endereco;

            Console.WriteLine("Informe o Nome da pessoa: ");
            nome = ColetarString();

            Console.WriteLine("Informe o telefone: ");
            telefone = ColetarString();

            do
            {
                Console.WriteLine("Informe o CPF: ");
                cpf = ColetarString();
            } while (VerificarCpf(cpf) == false);

            Console.WriteLine("Informe o E-mail (Caso Houver):");
            email = Console.ReadLine();

            Console.WriteLine("Informe o sexo da pessoa: ");
            genero = ColetarString();
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
            cargo = ColetarString();

            Console.WriteLine("Informe o nível de acesso do funcionário: ");
            nivelAcesso = ColetarString();

            Funcionario funcionario = new Funcionario(pessoa, cargo, nivelAcesso);
            agencia.setFuncList(funcionario);
        }

        //metodo cadastrar cliente
        static void CadastrarCliente()
        {
            ContaCorrente conta;
            Pessoa pessoa;
            float renda;
            String estudante;

            Console.WriteLine("Estudante (Informe Sim ou Nao)");
            do
            {
                estudante = ColetarString();
            } while (estudante != "SIM" && estudante != "NAO");
            Console.WriteLine("Informe a renda: ");
            renda = ColetarValorFloat();
            pessoa = ColetarPessoa();
            conta = CriarContaCorrente(renda, pessoa);

            Cliente cliente = new Cliente(pessoa, estudante, renda.ToString(), conta);
            Console.WriteLine($"\n{cliente}\n\n{cliente.conta}");
            agencia.setListaAprovacaoContas(cliente);
        }

        //criar conta corrente
        static ContaCorrente CriarContaCorrente(float renda, Pessoa pessoa)
        {
            String agencia, tipoConta, senha;
            float saldoInicial;

            Console.WriteLine("INFORME OS DADOS DA CONTA");
            Console.WriteLine("Informe o numero da agencia: ");
            agencia = ColetarString();
            do
            {
                Console.WriteLine("Informe o tipo de conta: ");
                tipoConta = ColetarString();
            } while (VerificarContaInformada(tipoConta) == false);
            Console.WriteLine("Terá deposito inicial? Informe o valor. Caso não houver pressione ENTER");
            float.TryParse(Console.ReadLine(), out saldoInicial);
            Console.WriteLine("Informe uma senha para sua conta: Pode incluir numeros, simbolos e digitos numéricos");
            senha = ColetarString();
            Console.Clear();
            return new ContaCorrente(agencia, saldoInicial, tipoConta, renda, pessoa, senha);
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

        #region Tratamento de Erros
        //metodo para coletar valores int
        static int ColetarValorInt()
        {
            bool aux;
            int valor;
            do
            {
                aux = int.TryParse(Console.ReadLine(), out valor);
                if (aux == false) Console.WriteLine("Informe opção válida");
            } while (aux == false);
            return valor;
        }

        //metodo para coletar valores float
        static float ColetarValorFloat()
        {
            bool aux;
            float valor;
            do
            {
                aux = float.TryParse(Console.ReadLine(), out valor);
                if (aux == false) Console.WriteLine("Informe opção válida");
            } while (aux == false);
            return valor;
        }

        //metodo para nao coletar string em branco
        static String ColetarString()
        {
            String texto;
            do
            {
                texto = Console.ReadLine().ToUpper();
                if (String.IsNullOrWhiteSpace(texto)) Console.WriteLine("Campo obrigatório, não deixar em branco");
            } while (String.IsNullOrWhiteSpace(texto));

            return texto;
        }

        //metodo para verificar tipo de conta
        static bool VerificarContaInformada(String tipoConta)
        {
            if (tipoConta != "Vip" || tipoConta != "Universitária" || tipoConta != "Normal")
                return true;
            else
            {
                Console.WriteLine("Tipo de conta inválida");
                return false;
            }
        }

        static bool VerificarCpf(String Cpf)
        {
            if (Cpf.Length == 11)
                return true;
            else
            {
                Console.WriteLine("CPF Inválido\nCPF possui 11 digitos");
                return false;
            }
        }

        #endregion Tratamento de Erros
    }
}
