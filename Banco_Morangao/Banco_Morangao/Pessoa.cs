using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco_Morangao
{
    internal class Pessoa
    {
        private String Nome { get; set; }
        private String Telefone { get; set; }

        private Endereco endereco;
        private String Email { get; set; }
        private String Cpf { get; set; }
        private String Genero { get; set; }

        //metodo construtor vazio
        public Pessoa() { }

        //metodo construtor com parametros
        public Pessoa(string nome, string telefone, Endereco endereco, string email, string cpf, string genero)
        {
            Nome = nome;
            Telefone = telefone;
            this.endereco = endereco;
            Email = email;
            Cpf = cpf;
            Genero = genero;
        }

    }
}
