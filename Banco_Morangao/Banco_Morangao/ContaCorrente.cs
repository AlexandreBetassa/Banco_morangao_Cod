﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco_Morangao
{
    internal class ContaCorrente
    {
        private bool _habilitada;
        private Cliente _cliente;
        private String _agencia;
        //private String _senha;
        private String _numConta;
        private float _saldo;
        private float _limite;
        private Cartao _cartao;
        private String _tipoConta;

        Random r = new Random();

        //adicionar ao construtor senha quando funcionamento estiver correto
        public ContaCorrente(Cliente cliente, string agencia, float saldo, string tipoConta, float renda)
        {
            _limite = calcularLimite(renda);
            _habilitada = false;
            _cliente = cliente;
            _agencia = agencia;
            _numConta = r.Next(100000, 9999999).ToString();
            _saldo = saldo;
            _cartao = new Cartao(_limite, "10");
            _tipoConta = tipoConta;
        }

        private float calcularLimite(float renda)
        {
            float limite = renda * (float)1.3;
            return limite;
        }

        public override string ToString()
        {
            return $"Cliente: {_cliente}\nAgencia: {_agencia}\nNumero conta: {_numConta}\nSaldo: {_saldo}\nLimite: {_limite}\nTipo de conta: {_tipoConta}\nCARTAO\n{_cartao}".ToString();
        }

    }
}