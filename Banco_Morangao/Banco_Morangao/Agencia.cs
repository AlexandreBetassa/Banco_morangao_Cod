using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Banco_Morangao
{
    internal class Agencia
    {
        private List<Funcionario> _listFuncionario = new List<Funcionario>();
        private List<ContaCorrente> _listContaCorrente = new List<ContaCorrente>();
        private List<Cliente> _listAprovacoesCliente = new List<Cliente>();
        private List<ContaCorrente> _listAprovacoesEmprestimo = new List<ContaCorrente>();

        #region metodos da agencia

        #region metodos acesso funcionario
        //metodo para listagem de funcionarios

        public void getListFuncionarios()
        {
            foreach (var item in _listFuncionario)
                if (item != null) Console.WriteLine($"FUNCIONÁRIO: {_listFuncionario.IndexOf(item)}\n{item}\n");
                else Console.WriteLine("Não há clientes a serem listados");
        }

        //metodo para adicionar funcionario na lista
        public void setFuncList(Funcionario funcionario)
        {
            _listFuncionario.Add(funcionario);
            Console.WriteLine("Funcionário cadastrado com sucesso!!");
        }

        //metodo para remover funcionario na lista
        public void DelFuncList(Funcionario funcionario)
        {
            _listFuncionario.Remove(funcionario);
        }
        #endregion metodos acesso funcionario

        #region metodos lista contas
        //metodo para adicionar contas na lista
        public void setContaList(ContaCorrente conta)
        {
            _listContaCorrente.Add(conta);
            Console.WriteLine("Conta cadastrada com sucesso!!!");
        }

        //metodo para listagem de contas cadastradas
        public void getListConta()
        {
            foreach (var item in _listContaCorrente)
            {
                if (item != null) Console.WriteLine(item + "\n");
                else Console.WriteLine("Não há contas cadastradas");
            }
        }

        //buscar conta de usuário
        public ContaCorrente BuscarContaCorrente(string agencia, string numConta)
        {
            foreach (ContaCorrente conta in _listContaCorrente) if (conta.getAgencia() == agencia && conta.getNumConta() == numConta) return conta;
            Console.WriteLine("Usúario não encontrado");
            return null;
        }
        #endregion metodos lista contas

        #region metodos lista aprovação

        #region aprovacao contas
        //metodo para retornar contas para aprovação
        public Cliente BuscarAprovacoesContas()
        {
            foreach (var item in _listAprovacoesCliente) if (item != null) return item;
            Console.WriteLine("Não há novas aprovações a serem realizadas");
            return null;
        }
        //metodo para adicionar contas para aprovação
        public void setListaAprovacaoContas(Cliente cliente)
        {
            _listAprovacoesCliente.Add(cliente);
            Console.WriteLine("Aguarde aprovação do gerente");
        }
        //metodo para remover solicitação da lista após processada
        public void DelListaAprovacao(Cliente cliente)
        {
            _listAprovacoesCliente.Remove(cliente);
        }
        #endregion aprovacao contas

        #region metodos aprovacao emprestimo
        //metodo para buscar emprestimo na lista de aprovação
        public ContaCorrente getSolicitacaoEmprestimo()
        {
            foreach (var item in _listAprovacoesEmprestimo) if (item != null) return item;
            Console.WriteLine("Não há empréstimos a serem aprovados");
            return null;
        }

        //metodo para adicionar emprestimo na lista de aprovacao
        public void setSolicitacaoEmprestimo(ContaCorrente conta)
        {
            _listAprovacoesEmprestimo.Add(conta);
            Console.WriteLine("Aguarde aprovação");
        }

        //metodo para adicionar emprestimo na lista de aprovacao
        public void DelSolicitacaoEmprestimo(ContaCorrente conta)
        {
            _listAprovacoesEmprestimo.Remove(conta);
        }

        #endregion metodos aprovacao emprestimo

        #endregion metodos lista aprovação

        #endregion metodos da agencia

        #region Impressao
        public void Gravar_Arquivo(Agencia agencia)
        {
            Console.WriteLine("Iniciando a Gravação de Dados...");
            try
            {
                StreamWriter listaClientes = new StreamWriter("C:\\Users\\Alexandre\\Desktop\\Aulas\\Banco Morangao\\Código banco morangao\\Banco_Morangao\\Banco_Morangao\\lista_clientes.txt");  //Instancia um Objeto StreamWriter (Classe de Manipulação de Arquivos)
                foreach (var item in agencia._listContaCorrente) listaClientes.WriteLine(item.getConta() + "");
                listaClientes.Close();  // Comando para Fechar o Arquivo

            }
            catch (Exception e)
            {
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
                Console.WriteLine("FIM DA GRAVAÇÃO");
            }
        }
        public void LerArquivosListas()
        {
            try
            {
                string line;
                StreamReader sr = new StreamReader("C:\\Users\\Alexandre\\Desktop\\Aulas\\Banco Morangao\\Código banco morangao\\Banco_Morangao\\Banco_Morangao\\lista_clientes.txt");//Instancia um Objeto StreamReader (Classe de Manipulação de Leitura de Arquivos)
                line = sr.ReadLine(); //Faz a Leitura de uma linha do arquivo e atribui a string line
                while (line != null)// Laço de Repetição para fazer a leitura de linhas do arquivo até o EOF (End Of File - Fim do Arquivo)
                {
                    Console.WriteLine(line);//Imprime o retorno do arquivo no Console
                    line = sr.ReadLine(); //Faz a Leitura de linha do arquivo e atribui a string line
                }
                sr.Close();//Fecha o Arquivo
                Console.WriteLine("Fim da Leitura do Arquivo");
                Console.ReadLine();
            }
            catch (Exception)
            {

            }
        }
        public String getConta(ContaCorrente conta)
        {
            return conta.getConta();
        }
        #endregion Impressao

    }
}
