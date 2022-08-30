using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco_Morangao
{
    internal class Cartao
    {
        private bool _habilitarCartao;
        private String _senha;
        private float _limite;
        private float _saldo;
        private String _numCartao;
        private String _digVerificador;
        private DateTime _dataVencFatura;
        private DateTime _validadeCartao;

        Random r = new Random();

        //criar a senha após estiver funcionando "a fazer"
        protected Cartao(float limite, DateTime datVencFatura)
        {
            _habilitarCartao = false;
            //_senha = senha;
            _limite = limite;
            _saldo = limite;
            _numCartao = r.Next(1111111111, 999999999).ToString();
            //_digVerificador = "criar metodo";
            _dataVencFatura = datVencFatura;
            _validadeCartao = DateTime.Now; //somar mais 5 anos "a fazer"
        }



    }
}
