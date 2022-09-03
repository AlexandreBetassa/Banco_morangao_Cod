using System;
using System.Drawing;
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
                        Pause();
                    }
                }
                Console.WriteLine("Deseja efetuar nova transação: ");
                op = ColetarString();

            } while (op != "NAO");
            Console.WriteLine("Sair");
        }



        //TESTAR MENUS
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
            ContaCorrente conta = agencia.BuscarContaCorrente(SolicitarAgencia(), SolicitarNumConta());
            if (conta == null)
            {
                Console.WriteLine("NÃO EXISTEM CONTAS CADASTRADAS");
                Pause();
            }
            else
            {
                if (SolicitarSenha(conta.getNome(), conta.getSenha())) MenuConta(conta);
                else
                {
                    Console.WriteLine("Senha inválida");
                    OperacaoCancelada();
                }
            }
        }

        //solicitar senha
        static bool SolicitarSenha(String usuario, String senhaUsuario)
        {
            string senha;
            Console.WriteLine($"Por favor {usuario} informe sua senha para continuar");
            senha = ColetarString();
            if (senhaUsuario == senha) return true;
            else return false;
        }

        //Menu Conta cliente
        static void MenuConta(ContaCorrente conta)
        {
            int op;
            do
            {
                Console.Write("Informe a operação:\n(1 - Sacar / 2 - Depositar / 3 - Pagar / 4 - Solicitar empréstimo / 5 - Consultas / 6 - Transferir\n>)");
                op = ColetarValorInt();
            } while (op < 1 && op > 7);
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
                    SolicitarEmprestimo(conta);
                    break;
                case 5:
                    Console.WriteLine("### CONSULTAS ###");
                    Consultar(conta);
                    break;
                case 6:
                    Console.WriteLine("### TRANSFERIR ###");
                    Tranferir(conta);
                    break;
                case 7:
                    Console.WriteLine("### BLOQUEAR OU DESBLOQUEAR CARTÃO ###");
                    BloquearDesbloquearCartao(conta);
                    break;
            }
        }

        //Menu funcionario
        static void MenuFuncionario()
        {
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
                    agencia.getListConta();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("### FUNÇÕES GERENTES ###");

                    if (SolicitarSenha("Gerente", senhaGerente))
                    {
                        Console.Clear();
                        MenuGerente();
                    }
                    else Console.WriteLine("Senha inválida");
                    OperacaoCancelada();
                    break;
            }
        }

        //Menu gerente
        static void MenuGerente()
        {
            int op;
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
                    Console.WriteLine("\n\n### FUNCIONÁRIO CADASTRADO COM SUCESSO ###");
                    Pause();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("### LISTAR FUNCIONÁRIOS ###");
                    agencia.getListFuncionarios();
                    Console.WriteLine("\n\n### FIM ###");
                    Pause();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("### APROVAR SOLICITAÇÕES###");
                    Aprovacao();
                    Console.WriteLine("\n\n### FIM ###");
                    Pause();
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
            Console.WriteLine("Deseja tranferir de sua conta poupança ou para outra conta? 1 - Poupança / 2 - Outra / 0 - Cancelar");
            do
            {
                op = ColetarValorInt();
                if (op < 0 || op > 2) Console.WriteLine("Informe opção válida");
            } while (op < 0 || op > 2);
            if (op == 1) TransferirContaPoupanca(conta);
            else if (op == 2)
            {
                ContaCorrente contaDestino = ContaDestino();
                valor = ColetarValorFloat();
                conta.MovimentarSaida("CC", "Transferência", valor);
                contaDestino.MovimentarEntrada("CC", "Transferência", valor);
                Console.WriteLine($"\nSaldo conta titular");
                conta.getSaldoToString();
                Console.WriteLine($"\nSaldo conta destino");
                contaDestino.getSaldoToString();
                Pause();
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
            Console.WriteLine("Deseja resgatar dinheiro da poupança ou guardar? 1 - Resgatar / 2 - Guardar / 0 - Cancelar");
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
            Console.Write("### GUARDAR POUPANÇA ###\nInforme o valor: ");
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
            Console.Write("### RESGATAR POUPANÇA ###\nInforme o valor: ");
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
            Console.Write("O que deseja consultar: 1 - Saldo / 2 - Extrato");
            op = ColetarValorInt();
            if (op == 1)
            {
                conta.getSaldoToString();
                Pause();
            }
            else
            {
                conta.getExtrato();
                conta.getSaldoToString();
                Pause();
            }
        }

        //DEPOSITAR
        static void Depositar(ContaCorrente conta)
        {
            float valor;
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
            Console.Write("Informe o valor: ");
            valor = ColetarValorFloat();
            conta.MovimentarSaida("Cc", "Saque", valor);
            Console.WriteLine("SALDO APÓS TRANSAÇÃO ");
            conta.getSaldoToString();
            Pause();
        }

        //metodo para solicitar emprestimo
        static void SolicitarEmprestimo(ContaCorrente conta)
        {
            float valor;
            string confirmar;
            Console.WriteLine("Informe o valor que deseja emprestar:");
            valor = CalcularCustoEmprestimo(ColetarValorFloat(), conta.getTipoConta());
            Console.WriteLine($"Custo do empréstimo: R$ {valor.ToString("F")}");
            Console.WriteLine("Deseja confirmar o empréstimo? sim ou nao");
            do
            {
                confirmar = ColetarString();
                if (confirmar != "SIM" && confirmar != "NAO") Console.WriteLine("Informe opção válida");
            } while (confirmar != "SIM" && confirmar != "NAO");
            if (confirmar == "SIM")
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
            Console.WriteLine("Deseja bloquear ou desbloquear seu cartão? 0 - Cancelar / 1 - Bloquear / 2 - Desbloquear");
            do
            {
                confirmar = ColetarValorInt();
                if (confirmar < 0 || confirmar > 2) Console.WriteLine("Informe uma operação correta");
            } while (confirmar < 0 || confirmar > 2);

            if (confirmar == 1)
            {
                conta.HabilitarCartao(true);
                Console.WriteLine("Cartão Bloqueado");
                Pause();
            }
            else if (confirmar == 2)
            {
                conta.HabilitarCartao(false);
                Console.WriteLine("Cartão Desbloqueado");
                Pause();
            }
            else
            {
                Console.WriteLine("Operação cancelada");
                Pause();
            }
        }

        #endregion Cliente

        //REVISAR 
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
            do genero = ColetarString();
            while (genero != "MASC" && genero != "FEM");
            Console.Clear();

            Console.WriteLine("INFORME O ENDEREÇO");
            endereco = ColetarEndereco();

            return new Pessoa(nome, telefone, endereco, email, cpf, genero);
        }

        //metodo cadastrar funcionario
        static void CadastrarFuncionario()
        {
            Pessoa pessoa;
            String cargo;
            int nivelAcesso;

            pessoa = ColetarPessoa();

            Console.WriteLine("Informe o cargo do funcionário: ");
            cargo = ColetarString();

            Console.WriteLine("Informe o nível de acesso do funcionário: 1 - Funcionário / 2 - Gerente");
            do
            {
                nivelAcesso = ColetarValorInt();
                if (nivelAcesso != 1 && nivelAcesso != 2) Console.WriteLine("Informe valor válido");
            } while (nivelAcesso != 1 && nivelAcesso != 2);

            Funcionario funcionario = new Funcionario(pessoa, cargo, nivelAcesso.ToString());
            agencia.setFuncList(funcionario);
        }

        //metodo cadastrar cliente
        static void CadastrarCliente()
        {
            ContaCorrente conta;
            Pessoa pessoa;
            float renda;
            int estudante;

            Console.Write("Estudante (Informe 1 - Sim ou 2 - Não): ");
            do
            {
                estudante = ColetarValorInt();
            } while (estudante != 1 && estudante != 2);
            Console.Write("Informe a renda: ");
            renda = ColetarValorFloat();
            pessoa = ColetarPessoa();
            conta = CriarContaCorrente(renda, pessoa);

            Cliente cliente = new Cliente(pessoa, Estudante(estudante), renda.ToString(), conta);
            Console.WriteLine($"\n{cliente}\n\n{cliente.conta}");
            agencia.setListaAprovacaoContas(cliente);
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

        //REVISAR
        #region Gerente

        //finalizar aprovações de emprestimo
        static void Aprovacao()
        {
            int op;
            do
            {
                Console.WriteLine("1 - Aprovar Abertura de Contas / 2 - Aprovar Emprétimos");
                op = ColetarValorInt();
                if (op != 1 && op != 2) Console.WriteLine("Informe operação válida");
            } while (op != 1 && op != 2);
            if (op == 1) AprovacaoAberturaDeContas();
            else AprovacaoEmprestimo();
        }

        //metodo para aprovacao de abertura de contas
        static void AprovacaoAberturaDeContas()
        {
            int op;
            agencia.BuscarAprovacoesContas();

            Cliente cliente = agencia.BuscarAprovacoesContas();
            if (cliente != null)
            {
                Console.WriteLine(cliente.ToString());
                Console.WriteLine("Deseja aprovar a abertura dessa conta? 1 - Sim / 2 - Não");
                do
                {
                    op = ColetarValorInt();
                    if (op < 1 || op > 2) Console.WriteLine("Informe operação válida");
                } while (op < 1 || op > 2);

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

        //metodo para aprovação de emprestimos
        static void AprovacaoEmprestimo()
        {
            int op;
            ContaCorrente conta = agencia.getSolicitacaoEmprestimo();

            if (conta != null)
            {
                Console.WriteLine($"Número da conta: {conta.getNumConta()}\nTipo da conta: {conta.getTipoConta()}\nValor da solicitação de empréstimo: {conta.getValorSolicitacaoEmprestimo()}");
                Console.WriteLine("Deseja aprovar o empréstimo? 1 - Sim / 2 - Não");
                do
                {
                    op = ColetarValorInt();
                } while (op != 1 && op != 2);

                if (op == 1)
                {
                    conta.MovimentarEntrada("Cc", "Depósito de empréstimo", conta.getValorSolicitacaoEmprestimo());
                    conta.setValorEmprestimo(0);
                    Console.WriteLine("Aprovado");
                    agencia.DelSolicitacaoEmprestimo(conta);
                    Pause();
                }
                else
                {
                    Console.WriteLine("Empréstimo reprovado");
                    agencia.DelSolicitacaoEmprestimo(conta);
                    conta.setValorEmprestimo(0);
                    Pause();
                }
            }

        }
        #endregion Gerente

        //TRATAMENTO DE ERROS
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
        static bool VerificarContaInformada(String tipoConta)
        {
            if (tipoConta != "VIP" && tipoConta != "UNIVERSITARIA" && tipoConta != "NORMAL")
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

        static void OperacaoCancelada()
        {
            Console.WriteLine("Operação cancelada\nPressione ENTER para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        static void Pause()
        {
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        #endregion Tratamento de Erros
    }
}
