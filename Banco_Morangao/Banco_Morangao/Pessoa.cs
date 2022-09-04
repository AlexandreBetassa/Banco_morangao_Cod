using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco_Morangao
{
    internal class Pessoa
    {
        internal protected String _nome;
        protected String _telefone;
        protected Endereco _endereco;
        protected String _email;
        protected String _cpf;
        protected String _genero;

        //metodo construtor vazio
        public Pessoa() { }

        //metodo construtor com parametros
        public Pessoa(string nome, string telefone, Endereco endereco, string email, string cpf, string genero)
        {
            _nome = nome;
            _telefone = telefone;
            _endereco = endereco;
            _email = email;
            _cpf = cpf;
            _genero = genero;
        }

        //metodo para coletar nome
        public String getNome()
        {
            return _nome;
        }

        //metodo override
        public override string ToString()
        {
            return $"Nome: {_nome}\nTelefone: {_telefone}\nEndereço: {_endereco}\nE-mail: {_email}\nCPF: {_cpf}\nGênero: {_genero}".ToString();
        }

    }
}
