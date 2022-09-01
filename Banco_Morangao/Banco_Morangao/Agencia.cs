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
        private List<Cliente> _listAprovacoesCliente = new List<Cliente>();

        public Agencia()
        {
        }

        public void getListClientes()
        {
            foreach (var item in _listCliente)
            {
                if (item != null)
                {
                    Console.WriteLine(item);
                }
                else
                {
                    Console.WriteLine("Não há clientes a serem listados");
                    Console.WriteLine("Pressione Enter para continuar...");
                    Console.Clear();
                    Console.ReadKey();
                }
            }
        }
        //metodo para listagem de funcionarios
        public void getListFuncionarios()
        {
            foreach (var item in _listFuncionario)
                if (item != null)
                {
                    Console.WriteLine($"FUNCIONÁRIO: {_listFuncionario.IndexOf(item)}\n{item}\n");
                }
                else
                {
                    Console.WriteLine("Não há clientes a serem listados");
                    Console.WriteLine("Pressione Enter para continuar...");
                    Console.Clear();
                    Console.ReadKey();
                }

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
            Console.WriteLine("Funcionário cadastrado com sucesso!!");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
        //metodo para adicionar contas na lista
        public void setContaList(ContaCorrente conta)
        {
            _listContaCorrente.Add(conta);
            Console.WriteLine("Conta cadastrada com sucesso!!!");
            Console.WriteLine("Pressione Enter para continuar..");
            Console.ReadKey();
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
        //metodo para listagem de contas cadastradas
        public void getListConta()
        {
            foreach (var item in _listContaCorrente)
            {
                if (item != null) Console.WriteLine(item + "\n");
                else Console.WriteLine("Não há contas cadastradas");
            }
        }
        //metodo para retornar contas para aprovação
        public Cliente BuscarAprovacoes()
        {
            foreach (var item in _listAprovacoesCliente)
            {
                if (item != null)
                {
                    return item;
                }
            }
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
        //buscar conta de usuário
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
