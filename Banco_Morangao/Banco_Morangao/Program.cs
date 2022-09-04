using System;

namespace Banco_Morangao
{
    internal class Program
    {
        static Agencia agencia = new Agencia();

        static void Main(string[] args)
        {
            PrimeiroAcesso();
            do
            {
                Console.Clear();
                if (MenuSistema() == 1)
                {
                    Console.Clear();
                    Console.WriteLine("### MENU CLIENTE ###");
                    if (agencia._listClientes.Capacity != 0) MenuCliente();
                    else Console.WriteLine("Ainda não há clientes cadastrados");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("### MENU FUNCIONÁRIO ###");
                    if (SolicitarSenha(1)) MenuFuncionario();
                    else
                    {
                        Console.WriteLine("Senha Inválida");
                        Pause();
                    }
                }
                Console.WriteLine("### MENU SISTEMA ###");
            } while (RepetirOperacao());
            Console.WriteLine("Sair");
        }

        static void PrimeiroAcesso()
        {
            Console.WriteLine("### BEM VINDO AO SISTEMA ###");
            Console.WriteLine("No primeiro acesso é necessário que um gerente de nível 2 se cadastre primeiro para acessar os recursos");
            Console.WriteLine("\n\n\t\t#### INFORMAÇÃO IMPORTANTE ####\nGUARDE TODOS OS ID's DE FUNCIONÁRIOS, CONTAS, AGÊNCIAS E SENHAS QUE CADASTRAR");
            Console.WriteLine("VOCÊ NECESSITARÁ DESSAS INFORMAÇÕES DURANTE A EXECUÇÃO DO PROGRAMA");
            Console.WriteLine();
            Pause();
            CadastrarFuncionario();
        }

        //TESTAR MENUS
        #region Menus
        //Menu
        static int MenuSistema()
        {
            Console.Write("Você é cliente ou funcionário? 1 - Cliente / 2 - Funcionário: ");
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
                if (conta.getSenha()) MenuConta(conta);
                else
                {
                    Console.WriteLine("SENHA INVÁLIDA");
                    OperacaoCancelada();
                }
            }
        }

        //solicitar senha
        static bool SolicitarSenha(int nivelAcesso)
        {
            string senha, id;
            Console.Write($"Por favor informe sua senha: ");
            senha = ColetarString();
            Console.Write($"Por favor informe seu Id: ");
            id = ColetarString();
            if (agencia.BuscarFucionario(senha, nivelAcesso, id)) return true;
            else return false;
        }

        //Menu Conta cliente
        static void MenuConta(ContaCorrente conta)
        {
            int op;
            do
            {
                do
                {
                    Console.Write("Informe a operação:\n1 - Sacar / 2 - Depositar / 3 - Pagar / 4 - Solicitar empréstimo / 5 - Consultas / 6 - Transferir / 7 - Serviços Cartão\n>");
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
                        Console.WriteLine("### SERVIÇOS CARTÃO ###");
                        ServicosCartao(conta);
                        break;
                }
                Console.WriteLine("### MENU CONTA ###");
            } while (RepetirOperacao());
        }

        //Menu funcionario
        static void MenuFuncionario()
        {
            int op;
            do
            {
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

                        if (SolicitarSenha(2))
                        {
                            Console.Clear();
                            MenuGerente();
                        }
                        else Console.WriteLine("Senha inválida");
                        OperacaoCancelada();
                        break;
                }
                Console.WriteLine("### MENU FUNCIONÁRIO ###");
            } while (RepetirOperacao());
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
            conta.MovimentarSaida("CC", "Saque/Pag", valor);
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
            valor = CalcularCustoEmprestimo(ColetarValorFloat(), conta._tipoConta);
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
                if (confirmar < 0 || confirmar > 3) Console.WriteLine("Informe uma operação correta");
            } while (confirmar < 0 || confirmar > 3);

            if (confirmar == 1)
            {
                conta.HabilitarCartao(true);
                conta.StatusCartao();
                Pause();
            }
            else if (confirmar == 2)
            {
                conta.HabilitarCartao(false);
                conta.StatusCartao();
                Pause();
            }
            else
            {
                OperacaoCancelada();
                return;
            }
            Pause();
        }

        static void ServicosCartao(ContaCorrente conta)
        {
            int op;
            Console.Write("0 - Cancelar / 1 - Bloquear/Desbloquear cartão / 2 - Consultar saldo cartão? / 3 - Consultar status do cartão: ");
            do
            {
                op = ColetarValorInt();
            } while (op < 0 || op > 3);

            switch (op)
            {
                case 1:
                    BloquearDesbloquearCartao(conta);
                    break;
                case 2:
                    ConsultarSaldoCartao(conta);
                    break;
                case 3:
                    conta.StatusCartao();
                    break;
                case 0:
                    OperacaoCancelada();
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

            Console.Write("Informe o complemento: ");
            complemento = Console.ReadLine();

            return new Endereco(logradouro, numero, bairro, cidade, cep, complemento);
        }

        //metodo coletar dados pessoa
        static Pessoa ColetarPessoa()
        {
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
            do genero = ColetarString();
            while (genero != "MASC" && genero != "FEM");
            Console.Clear();

            Console.WriteLine("INFORME O ENDEREÇO");
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
            Console.Write($"\n{cliente}\n\n{cliente.getConta()}");
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
            Console.Write("Informe o numero da agência: ");
            agencia = ColetarString();
            do
            {
                Console.Write("Informe o tipo de conta: ");
                tipoConta = ColetarString();
            } while (VerificarContaInformada(tipoConta));
            Console.Write("Terá deposito inicial? Informe o valor. Caso não houver pressione ENTER: ");
            float.TryParse(Console.ReadLine(), out saldoInicial);
            Console.Write("Informe uma senha para sua conta: Pode incluir numeros, simbolos e digitos numéricos: ");
            senha = ColetarString();
            Console.Clear();
            return new ContaCorrente(agencia, saldoInicial, tipoConta, renda, pessoa, senha);
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


            Console.WriteLine("### CADASTRO DE FUNCIONÁRIO ###");
            pessoa = ColetarPessoa();

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
            Console.WriteLine(funcionario);
            Pause();
            agencia.setFuncList(funcionario);
        }

        //finalizar aprovações de emprestimo
        static void Aprovacao()
        {
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
                    }
                    else
                    {
                        cliente.HabilitarCliente(false);
                        Console.WriteLine("Reprovado");
                        agencia.DelListaAprovacao(cliente);
                    }
                }
                else
                {
                    Console.WriteLine("Não há clientes a serem aprovados!!!");
                    break;
                }
            } while (RepetirOperacao());
            OperacaoCancelada();
        }

        //metodo para aprovação de emprestimos
        static void AprovacaoEmprestimo()
        {
            int op;
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

        //metodo para repeticao de aprovação
        static bool RepetirOperacao()
        {
            int op;
            Console.Write("Deseja efetuar nova operação? 1 - SIM / 2 - NÃO: ");
            op = AuxColetarValor1e2();
            if (op == 1)
            {
                return true;
                Console.Clear();
            }
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

        //metodo para verificar tipo de conta(BUG de msg - corrigir)
        static bool VerificarContaInformada(string tipoConta)
        {
            if (tipoConta != "VIP" && tipoConta != "UNIVERSITARIA" && tipoConta != "NORMAL")
            {
                Console.WriteLine($"Tpo de conta selecionada: {tipoConta}");
                return true;
            }
            else
            {
                Console.WriteLine($"Tipo de conta inválida: {tipoConta}");
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
                if (valor != 1 && valor != 2) Console.WriteLine("Informe valor válido");
            } while (valor != 1 && valor != 2);
            return valor;
        }

        #endregion Tratamento de Erros
    }
}
