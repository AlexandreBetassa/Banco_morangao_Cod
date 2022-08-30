using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco_Morangao
{
    internal class Funcionario
    {
        private Pessoa _pessoa { get; set; }
        private String _id { get; set; }
        private String _cargo { get; set; }
        private String _nivelAcesso { get; set; }

        public Funcionario()
        {
        }

        public Funcionario(Pessoa pessoa, string cargo, string nivelAcesso)
        {
            Random r = new Random();
            _pessoa = pessoa;
            _id = r.Next(10000, 999999).ToString();
            _cargo = cargo;
            _nivelAcesso = nivelAcesso;
        }

        public override string ToString()
        {
            return $"{_pessoa}\nID do Funcionário: {_id}\nCargo: {_cargo}\nNivel de Acesso: {_nivelAcesso}".ToString();
        }
    }
}
