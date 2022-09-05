using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco_Morangao
{
    internal class Cartao
    {
        internal protected bool _habilitarCartao;
        internal protected String _senha;
        internal protected float _limite;
        internal protected float _saldo;
        internal protected String _numCartao;
        internal protected String _digVerificador;
        internal protected String _dataVencFatura;
        internal protected DateTime _validadeCartao;

        Random r = new Random();

        //criar a senha após estiver funcionando "a fazer"
        public Cartao(float limite, String datVencFatura)
        {
            _habilitarCartao = false;
            _limite = limite;
            _saldo = limite;
            _numCartao = r.Next(10000, 99999).ToString();
            //_digVerificador = "criar metodo";
            _dataVencFatura = datVencFatura;
            _validadeCartao = DateTime.Now; //somar mais 5 anos "a fazer"
        }

        //metodo para desbloquear cartão
        public void setCartao(bool estado)
        {
            _habilitarCartao = estado;
        }

        public string getNumeroCartao()
        {
            return _numCartao;
        }

        public void StatusCartao()
        {

        }

        //metodo ToString
        public override string ToString()
        {
            return $"Número do Cartão: {_numCartao}\nDigito verificador: {_digVerificador}\nData vencimento da fatura: {_dataVencFatura}\nData de vencimento do cartão: {_validadeCartao.ToShortDateString()}".ToString();
        }

    }
}
