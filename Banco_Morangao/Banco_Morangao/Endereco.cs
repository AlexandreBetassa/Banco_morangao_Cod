using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco_Morangao
{
    internal class Endereco
    {
        private String Logradouro { get; set; }
        private String Numero { get; set; }
        private String Bairro { get; set; }
        private String Cidade { get; set; }
        private String Cep { get; set; }
        private String Complemento { get; set; }

        //construtor vazio
        public Endereco()
        {
        }

        //construtor de endereco sem complemento
        public Endereco(string logradouro, string numero, string bairro, string cidade, string cep)
        {
            Logradouro = Console.ReadLine();
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            Cep = cep;
        }

        //construtor de endereco com complemento
        public Endereco(string logradouro, string numero, string bairro, string cidade, string cep, string complemento)
        {
            Logradouro = logradouro;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            Cep = cep;
            Complemento = complemento;
        }

        //metodo override toString para escrita dos dados
        public override string ToString()
        {
            return $"{Logradouro}, n°: {Numero}, Bairro: {Bairro}, Cidade: {Cidade}, CEP: {Cep}, Complemento: {Complemento}".ToString();
        }


    }
}
