using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco_Morangao
{
    internal class Funcionario
    {
        private Pessoa _pessoa;
        private String _id;
        private String _cargo;
        private int _nivelAcesso;
        private String _senha;

        public Funcionario()
        {
            _pessoa = new Pessoa("ADMIN", "", new Endereco(), "", "", "");
            _id = "0";
            _cargo = "ADMIN";
            _nivelAcesso = 2;
            _senha = "0";
        }

        public Funcionario(Pessoa pessoa, string cargo, int nivelAcesso, string senha)
        {
            Random r = new Random();
            _pessoa = pessoa;
            _id = r.Next(10000, 999999).ToString();
            _cargo = cargo;
            _nivelAcesso = nivelAcesso;
            _senha = senha;
        }

        public bool getSenha(string senha, string id)
        {
            if (_senha == senha && _id == id) return true;
            else return false;
        }

        public int getNivelAcesso()
        {
            return _nivelAcesso;
        }

        public override string ToString()
        {
            return $"{_pessoa}\nID do Funcionário: {_id}\nCargo: {_cargo}\nNivel de Acesso: {_nivelAcesso}".ToString();
        }
    }
}
