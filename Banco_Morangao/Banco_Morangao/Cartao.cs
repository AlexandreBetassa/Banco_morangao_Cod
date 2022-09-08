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

        public Cartao(float limite, String datVencFatura)
        {
            _habilitarCartao = false;
            _limite = limite;
            _saldo = limite;
            _numCartao = r.Next(10000, 99999).ToString();
            _digVerificador = "0";
            _dataVencFatura = datVencFatura;
            _validadeCartao = DateTime.Now;
            _validadeCartao = _validadeCartao.AddYears(5);
        }

        //metodo para desbloquear cartão
        public void setCartao(bool estado)
        {
            _habilitarCartao = estado;
        }

        //metodo para retornar o numero do cartao
        public string getNumeroCartao()
        {
            return _numCartao;
        }

        //metodo para saber se o cartao ta bloqueado ou desbloqueado
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
