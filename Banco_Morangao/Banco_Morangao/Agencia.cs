using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Banco_Morangao
{
    internal class Agencia
    {
        private List<Agencia> _listAgencia = new List<Agencia>(); //verificar necessidade
        private List<Cliente> _listCliente = new List<Cliente>();
        private List<Funcionario> _listFuncionario = new List<Funcionario>();
        private List<ContaCorrente> _listContaCorrente = new List<ContaCorrente>();


        public Agencia()
        {
        }

        //metodo para listagem de clientes
        public void getListClientes()
        {
            foreach (var item in _listAgencia) Console.WriteLine(item);
        }
        //metodo para listagem de funcionarios
        public void getListFuncionarios()
        {
            foreach (var item in _listFuncionario) Console.WriteLine(item);
        }

        //metodo para adicionar cliente na lista
        public void setClienteList(Cliente cliente)
        {
            _listCliente.Add(cliente);
        }
        //metodo para adicionar funcionario na lista
        public void setFuncList(Funcionario funcionario)
        {
            _listFuncionario.Add(funcionario);
        }
        //metodo para adicionar contas na lista
        public void setContaList(ContaCorrente conta)
        {
            _listContaCorrente.Add(conta);
        }
        //metodo para remover cliente da lista
        public void DelClienteList(Cliente cliente)
        {
            _listCliente.Remove(cliente);
        }
        //metodo para remover funcionario na lista
        public void DelFuncList(Funcionario funcionario)
        {
            _listFuncionario.Remove(funcionario);
        }

        public List<Cliente> ListaClientes()
        {
            return _listCliente;    
        }
        public ContaCorrente BuscarContaCorrente(string agencia, string numConta)
        {
            foreach (ContaCorrente conta in _listContaCorrente)
            {
                if (conta.getAgencia() == agencia && conta.getNumConta() == numConta)
                {
                    return conta;
                }
            }
            Console.WriteLine("Usúario não encontrado");
            return null;
        }
    }
}
