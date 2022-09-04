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
        internal protected List<Cliente> _listClientes = new List<Cliente>();
        private List<Funcionario> _listFuncionario = new List<Funcionario>();
        private List<ContaCorrente> _listContaCorrente = new List<ContaCorrente>();
        private List<Cliente> _listAprovacoesCliente = new List<Cliente>();
        private List<ContaCorrente> _listAprovacoesEmprestimo = new List<ContaCorrente>();

        #region metodos da agencia
        public void setListCliente(Cliente cliente)
        {
            _listClientes.Add(cliente);
        }

        public void removeListCliente(Cliente cliente)
        {
            _listClientes.Remove(cliente);

        }

        #region metodos acesso funcionario
        //metodo para listagem de funcionarios
        public void getListFuncionarios()
        {
            foreach (var item in _listFuncionario)
                if (item != null) Console.WriteLine($"FUNCIONÁRIO: {_listFuncionario}\n{item}\n");
                else Console.WriteLine("Não há funcionário a serem listados");
        }

        //metodo de verificacao de acesso de funcionario
        public bool BuscarFucionario(string senha, int nivelAcesso, string id)
        {
            foreach (var item in _listFuncionario)
            {
                if (item != null) if (item.getSenha(senha, nivelAcesso, id)) return true;
            }
            return false;
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
            foreach (ContaCorrente conta in _listContaCorrente) if (conta._agencia == agencia && conta._numConta == numConta) return conta;
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
    }
}
