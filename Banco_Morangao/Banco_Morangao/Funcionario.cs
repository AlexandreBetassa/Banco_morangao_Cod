using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco_Morangao
{
    internal class Funcionario
    {
        private Pessoa Pessoa { get; set; }
        private String Id { get; set; }
        private String Cargo { get; set; }
        private String NivelAcesso { get; set; }

        public Funcionario()
        {
        }

        public Funcionario(Pessoa pessoa, string cargo, string nivelAcesso)
        {
            Random r = new Random();
            Pessoa = pessoa;
            Id = r.Next(10000, 999999).ToString();
            Cargo = cargo;
            NivelAcesso = nivelAcesso;
        }

        public override string ToString()
        {
            return $"{Pessoa}\nID do Funcionário: {Id}\nCargo: {Cargo}\nNivel de Acesso: {NivelAcesso}".ToString();
        }
    }
}
