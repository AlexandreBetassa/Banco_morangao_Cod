using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco_Morangao
{
    internal class Pessoa
    {
        private String _nome { get; set; }
        private String _telefone { get; set; }

        private Endereco _endereco;
        private String _email { get; set; }
        private String _cpf { get; set; }
        private String _genero { get; set; }

        //metodo construtor vazio
        public Pessoa() { }

        //metodo construtor com parametros
        public Pessoa(string nome, string telefone, Endereco endereco, string email, string cpf, string genero)
        {
            _nome = nome;
            _telefone = telefone;
            this._endereco = endereco;
            _email = email;
            _cpf = cpf;
            _genero = genero;
        }

        //metodo override
        public override string ToString()
        {
            return $"Nome: {_nome}\nTelefone: {_telefone}\nEndereço: {_endereco}\nE-mail: {_email}\nCPF: {_cpf}\nGênero: {_genero}".ToString();
        }

    }
}
