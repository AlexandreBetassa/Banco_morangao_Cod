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
        private Pessoa _pessoa;
        private bool _estudante;
        private string _renda;
        public ContaCorrente conta;

        public Cliente() { }

        public Cliente(Pessoa pessoa, bool estudante, string renda, ContaCorrente conta)
        {
            _pessoa = pessoa;
            _estudante = estudante;
            _renda = renda;
            this.conta = conta;
        }

        //metodo para coletar a renda do cliente
        public float getRenda()
        {
            return float.Parse(_renda);
        }

        public override string ToString()
        {
            //adicionar numero de conta no toString()
            return $"\nCLIENTE\n{_pessoa}\nEstudante: {_estudante}\nRenda: {_renda}\n".ToString();
        }
    }
}
