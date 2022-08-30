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
        private string _habilitado;
        private Pessoa _pessoa;
        private bool _estudante;
        private string _renda;
        //private Conta conta;

        public Cliente() { }

        public Cliente(string habilitado, Pessoa pessoa, bool estudante, string renda/*, Conta conta*/)
        {
            _habilitado = habilitado;   
            _pessoa = pessoa;
            _estudante = estudante;
            _renda = renda;
            //Conta conta = conta
        }


        public override string ToString()
        {
            //adicionar numero de conta no toString()
            return $"Status do cliente: {_habilitado}\nCliente: {_pessoa}\nEstudante: {_estudante}\nRenda: {_renda}".ToString();
        }
    }
}
