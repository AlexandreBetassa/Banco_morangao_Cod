using System;
using System.Net.Http.Headers;
using System.Net.WebSockets;

namespace Banco_Morangao
{
    internal class Program
    {
        static Agencia agencia = new Agencia();

        static void Main(string[] args)
        {
            agencia.setFuncList(new Funcionario());
            Console.WriteLine(agencia);
            Inicio();
            do
            {
                int op = MenuSistema();
                switch (op)
                {
                    case 0:
                        Console.Clear();
                        Inicio();
                        break;
                    case 1:
                        Console.WriteLine("\t### MENU CLIENTE ###");
                        if (agencia._listClientes.Capacity != 0) LoginCliente();
                        else Console.WriteLine("Ainda não há clientes cadastrados");
                        Pause();
                        break;
                    case 2:
                        Console.WriteLine("\t### MENU FUNCIONÁRIO ###");
                        Funcionario funcionario = LoginFuncionario();
                        if (funcionario != null)
                            MenuFuncionario(funcionario);
                        else
                        {
                            Console.WriteLine("Senha Inválida");
                            Pause();
                        }
                        break;
                    default:
                        break;
                }
                Console.WriteLine("### MENU SISTEMA ###");
            } while (RepetirOperacao());
            Console.WriteLine("Sair");
        }

        public static void Inicio()
        {
            Console.WriteLine("\t\t### INFORMAÇÃO IMPORTANTE ###");
            Console.WriteLine("GUARDE TODOS OS IDs DE CADASTROS E SENHAS QUE FIZER\nPOR PADRÃO EXISTE UM FUNCIONÁRIO GENÉRICO DE NIVEL DE ACESSO 2 CADASTRADO");
            Console.WriteLine("PARA QUE POSSA EFETUAR AS TRANSAÇÕES DE TESTE: SENHA: 0 / ID DE FUNCIONÁRIO: 0");
            Console.WriteLine("CASO PREFERIR PODE EFETUAR CADASTROS DE NOVOS FUNCIONÁRIOS E CLIENTES COM ESSE ACESSO GENÉRICO");
            Pause();
        }

        //TESTAR MENUS
        #region Menus
        //Menu
        static int MenuSistema()
        {
            Console.Clear();
            Console.WriteLine("### MENU SISTEMA ###");
            Console.Write("Você é cliente ou funcionário? 1 - Cliente / 2 - Funcionário / 0 - Mensagem Inicial: ");
            int op = AuxColetarValor1e2();
            return op;
        }

        //Solicitar numero da agencia
        static String SolicitarAgencia()
        {
            int agencia;
            Console.Write("Informe o número da agência: ");
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
        static void LoginCliente()
        {
            string senha;
            Console.Clear();
            Console.WriteLine("### LOGIN CLIENTE ####");
            ContaCorrente conta = agencia.BuscarContaCorrente(SolicitarAgencia(), SolicitarNumConta());
            if (conta == null)
            {
                Console.WriteLine("NÃO EXISTEM CONTAS CADASTRADAS");
                Pause();
            }
            else
            {
                Console.Write($"Por favor {conta._pessoa._nome} informe sua senha para continuar: ");
                senha = Console.ReadLine();
                if (conta.getSenha(senha)) MenuConta(conta);
                else
                {
                    Console.WriteLine("SENHA INVÁLIDA OU CLIENTE NÃO LOCALIZADO!!!");
                    OperacaoCancelada();
                }
            }
        }

        //solicitar senha
        static Funcionario LoginFuncionario()
        {
            string senha, id;
            Console.Clear();
            Console.WriteLine("### LOGIN FUNCIONÁRIO ###");
            Console.Write("Por favor informe seu Id: ");
            id = ColetarString();
            Console.Write("Por favor informe sua senha: ");
            senha = ColetarString();
            Funcionario funcionario = agencia.BuscarFucionario(senha, id);
            if (funcionario != null) return funcionario;
            else return null;
        }

        //Menu Conta cliente
        static void MenuConta(ContaCorrente conta)
        {
            int op;
            do
            {
                Console.Clear();
                Console.WriteLine("### OPÇÕES CONTA ###");
                Console.WriteLine("Informe a operação:0 - Sair / 1 - Sacar / 2 - Depositar / 3 - Pagar / 4 - Solicitar empréstimo / 5 - Consultas / 6 - Transferir / 7 - Serviços Cartão: ");
                op = ColetarValorInt();
                Console.Clear();
                switch (op)
                {
                    case 0:
                        OperacaoCancelada();
                        break;
                    case 1:
                        Sacar(conta);
                        break;
                    case 2:
                        Depositar(conta);
                        break;
                    case 3:
                        Sacar(conta);
                        break;
                    case 4:
                        SolicitarEmprestimo(conta);
                        break;
                    case 5:
                        Consultar(conta);
                        break;
                    case 6:
                        Tranferir(conta);
                        break;
                    case 7:
                        ServicosCartao(conta);
                        break;
                    default:
                        Console.WriteLine("### OPÇÃO INVÁLIDA ###");
                        break;
                }
            } while (op != 0);
        }

        //Menu funcionario
        static void MenuFuncionario(Funcionario funcionario)
        {
            int op;
            Console.Clear();
            do
            {
                Console.WriteLine("### OPÇÕES DO FUNCIONÁRIO ###");
                Console.Write("Informe a operação: 0 - Sair / 1 - Cadastrar Cliente / 2 - Listar Clientes / 3 - Menu do gerente:  ");
                op = ColetarValorInt();

                switch (op)
                {
                    case 0:
                        Console.WriteLine("Sair");
                        break;
                    case 1:
                        Console.Clear();
                        CadastrarCliente();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("### LISTAR CLIENTES ###");
                        agencia.getListConta();
                        break;
                    case 3:
                        Console.Clear();
                        if (funcionario.getNivelAcesso() == 2) MenuGerente();
                        else Console.WriteLine("### VOCÊ NÃO POSSUI ACESSO AS OPÇÕES DE GERENTE ###\nOPERAÇÃO CANCELADA");
                        Pause();
                        break;
                    default:
                        Console.WriteLine("### OPÇÃO INVÁLIDA ###");
                        break;
                }
                Console.Clear();
            } while (op != 0);
        }

        //Menu gerente
        static void MenuGerente()
        {
            Console.Clear();
            int op;
            Console.WriteLine("### OPÇÕES DE GERENTE ###");
            Console.Write("Informe a operação: 0 - Sair / 1 - Cadastrar Funcionário / 2 - Listar Funcionário / 3 - Aprovações: ");
            op = ColetarValorInt();
            switch (op)
            {
                case 0:
                    OperacaoCancelada();
                    break;
                case 1:
                    CadastrarFuncionario();
                    Pause();
                    break;
                case 2:
                    Console.WriteLine("### LISTAR FUNCIONÁRIOS ###");
                    agencia.getListFuncionarios();
                    Console.WriteLine("\n\n### FIM ###");
                    Pause();
                    break;
                case 3:
                    Aprovacao();
                    Pause();
                    break;
                default:
                    Console.WriteLine("### OPÇÃO INVÁLIDA ###");
                    break;
            }
        }
        #endregion Menus

        //TESTAR CLIENTES
        #region Cliente

        #region Transferir
        //TRANSFERIR
        static void Tranferir(ContaCorrente conta)
        {
            float valor;
            int op;
            Console.Clear();
            Console.WriteLine("### TRANSFERÊNCIAS ###");
            Console.WriteLine("Deseja tranferir para sua conta poupança ou para outra conta? 1 - Poupança / 2 - Outra / 0 - Cancelar");
            do
            {
                op = ColetarValorInt();
                if (op < 0 || op > 2) Console.WriteLine("Informe opção válida");
            } while (op < 0 || op > 2);
            if (op == 1) TransferirContaPoupanca(conta);
            else if (op == 2)
            {
                Console.Clear();
                Console.WriteLine("### TRANSFERÊNCIAS ENTRE CONTAS ###");
                ContaCorrente contaDestino = ContaDestino();
                if (contaDestino != null)
                {
                    Console.Write("Informe o valor a ser transferido: ");
                    valor = ColetarValorFloat();
                    conta.MovimentarSaida("CC", "Transferência", valor);
                    contaDestino.MovimentarEntrada("CC", "Transferência", valor);
                    Console.WriteLine($"\n### TITULAR ###");
                    conta.getSaldoToString();
                    Console.WriteLine($"\n### DESTINO ###");
                    Console.WriteLine($"Saldo conta destino");
                    contaDestino.getSaldoToString();
                    Pause();
                }
                else Console.WriteLine("CLIENTE NÃO LOCALIZADO");
            }
            else OperacaoCancelada();
        }

        //metodo para buscar conta corrente destino
        static ContaCorrente ContaDestino()
        {
            Console.WriteLine("INFORME OS DADOS DA CONTA CORRENTE DESTINO");
            return agencia.BuscarContaCorrente(SolicitarAgencia(), SolicitarNumConta());
        }

        //metodo para tranferencia em conta poupança
        static void TransferirContaPoupanca(ContaCorrente conta)
        {
            int op;
            Console.Clear();
            Console.WriteLine("### TRANSFERÊNCIA EM CONTA POUPANÇA ###");
            Console.WriteLine("Deseja resgatar dinheiro da poupança ou guardar? 0 - Cancelar / 1 - Resgatar / 2 - Guardar ");
            do
            {
                op = ColetarValorInt();
                if (op < 1 || op > 2) Console.WriteLine("Informe operação válida");
            } while (op < 0 || op > 2);

            if (op == 1) ResgatarValorPoupanca(conta);
            else if (op == 2) DepositarValorPoupanca(conta);
            else OperacaoCancelada();
        }

        //metodo para depositar valor na conta poupanca
        static void DepositarValorPoupanca(ContaCorrente conta)
        {
            float valor;
            Console.Clear();
            Console.Write("### DEPÓSITO EM POUPANÇA ###\nInforme o valor: ");
            valor = ColetarValorFloat();
            if (conta.MovimentarSaida("CC", "Guardar Poup", valor)) conta.MovimentarEntrada("CP", "Guardar Poup", valor);
            else Console.WriteLine("Erro na solicitação");
            Console.WriteLine($"saldo conta poupança: {conta.getSaldoPoupança()}");
            Pause();
        }

        //metodo para resgatar valor na conta poupanca
        static void ResgatarValorPoupanca(ContaCorrente conta)
        {
            float valor;
            Console.Clear();
            Console.Write("### SAQUE EM CONTA POUPANÇA ###\nInforme o valor: ");
            valor = ColetarValorFloat();
            if (conta.MovimentarSaida("CP", "Resgate Poup", valor)) conta.MovimentarEntrada("CC", "Resgate Poup", valor);
            else Console.WriteLine("Erro na solicitação");
            Console.WriteLine($"saldo conta poupança: {conta.getSaldoPoupança()}");
            Pause();
        }
        #endregion Transferir

        //CONSULTAS
        static void Consultar(ContaCorrente conta)
        {
            int op;
            Console.Clear();
            Console.WriteLine("### CONSULTAS ###");
            Console.Write("O que deseja consultar 0 - Sair / 1 - Saldo / 2 - Extrato: ");
            op = AuxColetarValor1e2();
            Console.Clear();
            if (op == 1)
            {
                conta.getSaldoToString();
                Pause();
            }
            else if (op == 2)
            {
                conta.getSaldoToString();
                conta.getExtrato();
                Pause();
            }
            else OperacaoCancelada();
        }

        //DEPOSITAR
        static void Depositar(ContaCorrente conta)
        {
            float valor;
            Console.Clear();
            Console.WriteLine("### DEPÓSITOS ###");
            Console.Write("Informe o valor: ");
            valor = ColetarValorFloat();
            conta.MovimentarEntrada("CC", "Depósito", valor);
            Console.WriteLine("SALDO APÓS TRANSAÇÃO ");
            conta.getSaldoToString();
            Pause();
        }

        //SACAR
        static void Sacar(ContaCorrente conta)
        {
            float valor;
            Console.Clear();
            Console.WriteLine("### SAQUES ###");
            Console.Write("Informe o valor: ");
            valor = ColetarValorFloat();
            conta.MovimentarSaida("CC", "Saque/Pag", valor);
            Console.WriteLine("SALDO APÓS TRANSAÇÃO ");
            conta.getSaldoToString();
            Pause();
        }

        //metodo para solicitar emprestimo
        static void SolicitarEmprestimo(ContaCorrente conta)
        {
            float valor;
            int confirmar;
            Console.Clear();
            Console.WriteLine("### SOLICITAÇÃO DE EMPRÉSTIMO ###");
            Console.WriteLine("Informe o valor que deseja emprestar:");
            valor = CalcularCustoEmprestimo(ColetarValorFloat(), conta._tipoConta);
            Console.WriteLine($"Custo do empréstimo: R$ {valor.ToString("F")}");
            Console.Write("Deseja confirmar o empréstimo? 1 - Sim / 2 - Não: ");
            confirmar = AuxColetarValor1e2();
            if (confirmar == 1)
            {
                conta.setValorEmprestimo(valor);
                agencia.setSolicitacaoEmprestimo(conta);
                Pause();
            }
            else OperacaoCancelada();
        }

        //metodo para calcular custo do emprestimo
        static float CalcularCustoEmprestimo(float valor, string tipoConta)
        {
            if (tipoConta == "NORMAL") return valor * (float)1.5;
            else if (tipoConta == "VIP") return valor * (float)1.1;
            else return valor * (float)1.2;
        }

        static void BloquearDesbloquearCartao(ContaCorrente conta)
        {
            int confirmar;
            Console.Clear();
            Console.WriteLine("### BLOQUEAR / DESBLOQUEAR CARTÃO ###");
            Console.WriteLine("Deseja bloquear ou desbloquear seu cartão? 0 - Cancelar / 1 - Bloquear / 2 - Desbloquear");
            confirmar = ColetarValorInt();

            if (confirmar == 1)
            {
                conta.HabilitarCartao(false);
                Console.WriteLine($"Cartão: {conta.StatusCartao()}");
                Pause();
            }
            else if (confirmar == 2)
            {
                conta.HabilitarCartao(true);
                Console.WriteLine($"Cartão: {conta.StatusCartao()}");
                Pause();
            }
            else
            {
                OperacaoCancelada();
                return;
            }
        }

        static void ServicosCartao(ContaCorrente conta)
        {
            int op;
            Console.Clear();
            Console.WriteLine("### SERVIÇOS DO CARTÃO ###");
            Console.Write("0 - Cancelar / 1 - Bloquear/Desbloquear cartão / 2 - Consultar saldo cartão / 3 - Consultar status do cartão: ");
            op = ColetarValorInt();

            switch (op)
            {
                case 0:
                    OperacaoCancelada();
                    break;
                case 1:
                    BloquearDesbloquearCartao(conta);
                    break;
                case 2:
                    ConsultarSaldoCartao(conta);
                    break;
                case 3:
                    Console.WriteLine($"Cartão: {conta.StatusCartao()}");
                    Pause();
                    break;
            }
        }

        static void ConsultarSaldoCartao(ContaCorrente conta)
        {
            Console.WriteLine($"Saldo cartão: {conta.SaldoCartao().ToString("F")}");
            Pause();
        }

        #endregion Cliente

        //REVISAR 
        #region Funcionário
        //metodo para coletar endereco
        static Endereco ColetarEndereco()
        {
            Console.WriteLine("\n### INFORME O ENDEREÇO ###");

            string logradouro, bairro, cidade, cep, complemento, numero;

            Console.Write("Informe o logradouro com rua ou avenida: ");
            logradouro = ColetarString();

            Console.Write("Informe o numero: ");
            numero = ColetarString();

            Console.Write("Informe o bairro: ");
            bairro = ColetarString();

            Console.Write("Informe a cidade: ");
            cidade = ColetarString();

            Console.Write("Informe o CEP: ");
            cep = ColetarString();

            Console.Write("Informe o complemento (Caso necessário): ");
            complemento = Console.ReadLine();
            return new Endereco(logradouro, numero, bairro, cidade, cep, complemento);
        }

        //metodo coletar dados pessoa
        static Pessoa ColetarPessoa()
        {
            Console.WriteLine("\n### INFORME OS DADOS DA PESSOA ###");
            string nome, telefone, email, cpf, genero;
            Endereco endereco;

            Console.Write("Informe o Nome da pessoa: ");
            nome = ColetarString();

            Console.Write("Informe o telefone: ");
            telefone = ColetarString();

            do
            {
                Console.Write("Informe o CPF (11 - Digitos): ");
                cpf = ColetarString();
            } while (VerificarCpf(cpf) == false);

            Console.Write("Informe o E-mail (Caso Houver): ");
            email = Console.ReadLine();

            Console.Write("Informe o sexo da pessoa (Masc)(Fem): ");
            do
            {
                genero = ColetarString();
                if (genero != "MASC" && genero != "FEM") Console.WriteLine("Informe somente Masc ou Fem");
            } while (genero != "MASC" && genero != "FEM");
            endereco = ColetarEndereco();
            return new Pessoa(nome, telefone, endereco, email, cpf, genero);
        }

        //metodo cadastrar cliente
        static void CadastrarCliente()
        {
            ContaCorrente conta;
            Pessoa pessoa;
            float renda;
            int estudante;

            Console.Clear();
            Console.WriteLine("### CADASTRO DE CLIENTES ###");
            Console.Write("Estudante (Informe 1 - Sim ou 2 - Não): ");
            do
            {
                estudante = ColetarValorInt();
            } while (estudante != 1 && estudante != 2);
            Console.Write("Informe a renda: ");
            renda = ColetarValorFloat();
            pessoa = ColetarPessoa();
            Console.Clear();
            conta = CriarContaCorrente(renda, pessoa);
            Console.Clear();
            Cliente cliente = new Cliente(pessoa, Estudante(estudante), renda.ToString(), conta);
            agencia.setListaAprovacaoContas(cliente);
            Console.Clear();
            Console.WriteLine($"{cliente}");
            Console.WriteLine("### AGUARDE APROVAÇÃO DO GERENTE ###");
            Pause();
        }

        //metodo para verificaçao se é estudante
        static String Estudante(int valor)
        {
            if (valor == 1) return "SIM";
            else return "NÃO";
        }

        //criar conta corrente
        static ContaCorrente CriarContaCorrente(float renda, Pessoa pessoa)
        {
            String tipoConta, senha;
            float saldoInicial;
            Console.WriteLine("#### INFORME OS DADOS DA CONTA ###");

            Console.Write("Informe o tipo de conta Normal / Universitaria / Vip: ");
            do
            {
                tipoConta = ColetarString();
                if (VerificarContaInformada(tipoConta) == false) Console.WriteLine("Informe somente uma das opções: Normal / Universitaria / Vip");
            } while (VerificarContaInformada(tipoConta) == false);
            Console.Write("Terá deposito inicial? Informe o valor. Caso não houver pressione ENTER: ");
            float.TryParse(Console.ReadLine(), out saldoInicial);
            Console.Write("Informe uma senha para sua conta: Pode incluir numeros, simbolos e digitos numéricos: ");
            senha = ColetarString();
            return new ContaCorrente(agencia.NumAgencia.ToString(), saldoInicial, tipoConta, renda, pessoa, senha);
        }

        #endregion Funcionário

        //REVISAR
        #region Gerente

        //metodo cadastrar funcionario
        static void CadastrarFuncionario()
        {
            Pessoa pessoa;
            String cargo, senha;
            int nivelAcesso;

            pessoa = ColetarPessoa();
            Console.Clear();
            Console.Write("Informe o cargo do funcionário: ");
            cargo = ColetarString();

            Console.Write("Informe o nível de acesso do funcionário: 1 - Funcionário / 2 - Gerente: ");
            do
            {
                nivelAcesso = ColetarValorInt();
                if (nivelAcesso != 1 && nivelAcesso != 2) Console.WriteLine("Informe valor válido");
            } while (nivelAcesso != 1 && nivelAcesso != 2);

            Console.Write("Informe uma senha para sua conta: Pode incluir numeros, simbolos e digitos numéricos: ");
            senha = ColetarString();

            Funcionario funcionario = new Funcionario(pessoa, cargo, nivelAcesso, senha);
            agencia.setFuncList(funcionario);
            Console.Clear();
            Console.WriteLine(funcionario + "\n\n### CADASTRADO COM SUCESSO ###\n");
            Pause();
        }

        //finalizar aprovações de emprestimo
        static void Aprovacao()
        {
            Console.Clear();
            Console.WriteLine("### APROVAÇÕES ###");
            Console.WriteLine("1 - Aprovar Abertura de Conta / 2 - Aprovar Empréstimo");
            int op = AuxColetarValor1e2();
            if (op == 1) AprovacaoAberturaDeContas();
            else AprovacaoEmprestimo();
        }

        //metodo para aprovacao de abertura de contas (BUG - EM CORREÇÃO)
        static void AprovacaoAberturaDeContas()
        {
            int op;
            do
            {
                Console.Clear();
                Console.WriteLine("### APROVAÇÕES DE ABERTURA DE CONTAS ###");
                Cliente cliente = agencia.BuscarAprovacoesContas();
                if (cliente != null)
                {
                    Console.WriteLine(cliente.ToString());
                    Console.Write("Deseja aprovar a abertura dessa conta? 1 - Sim / 2 - Não: ");
                    op = AuxColetarValor1e2();
                    if (op == 1)
                    {
                        cliente.HabilitarCliente(true);
                        Console.WriteLine("Aprovado");
                        agencia.setListCliente(cliente);
                        agencia.setContaList(cliente.getConta());
                        agencia.DelListaAprovacao(cliente);
                        Pause();
                    }
                    else
                    {
                        cliente.HabilitarCliente(false);
                        Console.WriteLine("Reprovado");
                        agencia.DelListaAprovacao(cliente);
                        Pause();
                    }
                }
                else
                {
                    Console.WriteLine("Não há clientes a serem aprovados!!!");
                    break;
                }
                Console.Clear();
                Console.WriteLine("#### APROVAÇÃO DE CONTAS ###");
            } while (RepetirOperacao());
        }

        //metodo para aprovação de emprestimos
        static void AprovacaoEmprestimo()
        {
            int op;
            Console.Clear();
            Console.WriteLine("### APROVAÇÕES DE EMPRÉSTIMOS###");
            ContaCorrente conta = agencia.getSolicitacaoEmprestimo();
            if (conta != null)
            {
                Console.WriteLine($"Número da conta: {conta._numConta}\nTipo da conta: {conta._tipoConta}\nValor da solicitação de empréstimo: {conta.getValorSolicitacaoEmprestimo()}");
                Console.Write("Deseja aprovar o empréstimo? 1 - Sim / 2 - Não: ");
                op = AuxColetarValor1e2();
                if (op == 1)
                {
                    conta.MovimentarEntrada("CC", "Depósito de empréstimo", conta.getValorSolicitacaoEmprestimo());
                    conta.setValorEmprestimo(0);
                    Console.WriteLine("Aprovado");
                    agencia.DelSolicitacaoEmprestimo(conta);
                }
                else
                {
                    Console.WriteLine("Empréstimo reprovado");
                    agencia.DelSolicitacaoEmprestimo(conta);
                    conta.setValorEmprestimo(0);
                }
            }

        }
        #endregion Gerente

        //TRATAMENTO DE ERROS
        #region Tratamento de Erros

        //metodo para repeticao de aprovação
        static bool RepetirOperacao()
        {
            int op;
            Console.Write("Deseja efetuar nova operação? 1 - SIM / 2 - NÃO: ");
            op = AuxColetarValor1e2();
            if (op == 1) return true;
            else return false;
        }
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
                if (aux == false || valor < 0) Console.WriteLine("Informe valor válido");
            } while (aux == false || valor < 0);
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
        static bool VerificarContaInformada(string tipoConta)
        {
            if (tipoConta == "VIP" || tipoConta == "UNIVERSITARIA" || tipoConta == "NORMAL") return true;
            else return false;
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

        //metodo para pausa e msg de operação cancelada
        static void OperacaoCancelada()
        {
            Console.WriteLine("Operação cancelada\nPressione ENTER para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        //metodo para pausa
        static void Pause()
        {
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        //metodo auxiliar para operações de 1 ou 2
        static int AuxColetarValor1e2()
        {
            int valor;
            do
            {
                valor = ColetarValorInt();
                if (valor < 0 && valor > 2) Console.WriteLine("Informe valor válido");
            } while (valor < 0 && valor > 2);
            return valor;
        }

        #endregion Tratamento de Erros
    }
}
