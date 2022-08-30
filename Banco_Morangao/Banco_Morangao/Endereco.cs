using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco_Morangao
{
    internal class Endereco
    {
        private String _logradouro;
        private String _numero { get; set; }
        private String _bairro { get; set; }
        private String _cidade { get; set; }
        private String _cep { get; set; }
        private String _complemento { get; set; }

        //construtor vazio
        public Endereco()
        {
        }

        //construtor de endereco sem complemento
        public Endereco(string logradouro, string numero, string bairro, string cidade, string cep)
        {
            _logradouro = Console.ReadLine();
            _numero = numero;
            _bairro = bairro;
            _cidade = cidade;
            _cep = cep;
        }

        //construtor de endereco com complemento
        public Endereco(string logradouro, string numero, string bairro, string cidade, string cep, string complemento)
        {
            _logradouro = Console.ReadLine();
            _numero = numero;
            _bairro = bairro;
            _cidade = cidade;
            _cep = cep;
            _complemento = complemento;
        }

        //metodo override toString para escrita dos dados
        public override string ToString()
        {
            return $"{_logradouro}, n°: {_numero}, Bairro: {_bairro}, Cidade: {_cidade}, CEP: {_cep}, Complemento: {_complemento}".ToString();
        }


    }
}
