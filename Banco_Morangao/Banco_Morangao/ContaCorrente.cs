using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco_Morangao
{
    internal class ContaCorrente
    {
        private bool _habilitada;
        private String _agencia;
        private String _senha;
        private String _numConta;
        private float _saldo;
        private float _limite;
        private Cartao _cartao;
        private String _tipoConta;
        private ContaPoupanca _contaPoupanca;

        Random r = new Random();

        public ContaCorrente() { }

        //adicionar ao construtor senha quando funcionamento estiver correto senha "a fazer"
        public ContaCorrente(string agencia, float saldo, string tipoConta, float renda)
        {
            _limite = calcularLimite(renda);
            _habilitada = false;
            _agencia = agencia;
            _numConta = r.Next(100000, 9999999).ToString();
            _saldo = saldo;
            _cartao = new Cartao(_limite, "10");
            _tipoConta = tipoConta;
            _contaPoupanca = new ContaPoupanca(0);
        }

        protected float calcularLimite(float renda)
        {
            float limite = renda * (float)0.3;
            return limite;
        }

        public override string ToString()
        {
            return $"Agência: {_agencia}\nNúmero conta: {_numConta}\nSaldo: {_saldo}\nLimite: {_limite}\nTipo de conta: {_tipoConta}\n\nCARTÃO\n{_cartao}".ToString();
        }

    }
}
