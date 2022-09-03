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
        private String _numero;
        private String _bairro;
        private String _cidade;
        private String _cep;
        private String _complemento;

        //construtor vazio
        public Endereco()
        {
        }

        //construtor de endereco com complemento
        public Endereco(string logradouro, string numero, string bairro, string cidade, string cep, string complemento)
        {
            _logradouro = logradouro;
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
