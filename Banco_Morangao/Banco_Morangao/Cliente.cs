using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Banco_Morangao
{
    internal class Cliente
    {
        private bool _habilitado;
        private Pessoa _pessoa;
        private String _estudante;
        private string _renda;
        private ContaCorrente _conta;

        public Cliente() { }

        public Cliente(Pessoa pessoa, String estudante, string renda, ContaCorrente conta)
        {
            _habilitado = false;
            _pessoa = pessoa;
            _estudante = estudante;
            _renda = renda;
            _conta = conta;
        }

        //metodo getConta
        public ContaCorrente getConta()
        {
            return _conta;
        }

        //metodo para aprovar cliente
        public void HabilitarCliente(bool valor)
        {
            _habilitado = valor;
        }

        //metodo para coletar a renda do cliente
        public float getRenda()
        {
            return float.Parse(_renda);
        }

        //metodo toString
        public override string ToString()
        {
            //adicionar numero de conta no toString()
            return $"CLIENTE\n{_pessoa}\nEstudante: {_estudante}\nRenda: {_renda}\n### DADOS DA CONTA ###\n{_conta}".ToString();
        }
    }
}
