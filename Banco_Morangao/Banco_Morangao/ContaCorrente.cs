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
        private List<String> _extrato = new List<String>();

        Random r = new Random();

        //metodo construtor vazio
        public ContaCorrente() { }

        //adicionar ao construtor senha quando funcionamento estiver correto senha "a fazer"
        public ContaCorrente(string agencia, float saldo, string tipoConta, float renda)
        {
            _limite = CalcularLimite(renda);
            _habilitada = false;
            _agencia = agencia;
            _numConta = r.Next(1000, 9999).ToString();
            _saldo = saldo;
            _cartao = new Cartao(_limite, "10");
            _tipoConta = tipoConta;
            _contaPoupanca = new ContaPoupanca();
        }

        protected float CalcularLimite(float renda)
        {
            float limite = renda * (float)0.3;
            return limite;
        }

        //metodo para movimentar saida de saldo da conta
        public void MovimentarSaida(string operacao, float valor)
        {
            _saldo -= valor;
            _extrato.Add($"Saida: -{valor}\tOperação: {operacao}\tData: {DateTime.Now.ToShortDateString()}");
        }

        //metodo para movimentar saida de saldo da conta
        public void MovimentarEntrada(string operacao, float valor)
        {
            _saldo += valor;
            _extrato.Add($"Deposito: + {valor}\tOperação: {operacao}\tData: {DateTime.Now.ToShortDateString()}");
        }

        //metodo para retorno de saldo
        public float getSaldo()
        {
            return _saldo;
        }

        //metodo para puxa extrato
        public void getExtrato()
        {
            Console.Clear();
            Console.WriteLine("### EXTRATO ###\n");
            Console.WriteLine($"Conta: {_numConta}\tAgência: {_agencia}\n");
            foreach (var item in _extrato) Console.WriteLine(item);
            Console.WriteLine("### FIM EXTRATO ###\nPressione ENTER para continuar...");
            Console.ReadKey();
        }

        public String getAgencia()
        {
            return _agencia;
        }
        public String getNumConta()
        {
            return _numConta;

        }

        //metodo toString
        public override string ToString()
        {
            return $"Agência: {_agencia}\nNúmero conta: {_numConta}\nSaldo: {_saldo.ToString("F")}\nLimite: {_limite.ToString("F")}\nTipo de conta: {_tipoConta}\nCONTA POUPANÇA\nSaldo: {_contaPoupanca}\n\nCARTÃO\n{_cartao}".ToString();
        }

    }
}
