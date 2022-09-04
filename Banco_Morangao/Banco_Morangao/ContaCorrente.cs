using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco_Morangao
{
    internal class ContaCorrente
    {
        internal protected Pessoa _pessoa;
        internal protected String _agencia;
        private String _senha;
        internal protected String _numConta;
        private float _saldo;
        private float _limite;
        private Cartao _cartao;
        internal protected String _tipoConta;
        private ContaPoupanca _contaPoupanca;
        private float _valorSolicitacaoEmprestimo;
        private List<String> _extrato = new List<String>();

        Random r = new Random();

        //metodo construtor vazio
        public ContaCorrente() { }

        //Construtor
        public ContaCorrente(string agencia, float saldo, string tipoConta, float renda, Pessoa pessoa, String senha)
        {
            _limite = CalcularLimite(renda);
            _agencia = agencia;
            _numConta = r.Next(1000, 9999).ToString();
            _saldo = saldo;
            _cartao = new Cartao(_limite, "10");
            _tipoConta = tipoConta;
            _contaPoupanca = new ContaPoupanca();
            _pessoa = pessoa;
            _senha = senha;
            _contaPoupanca._saldo = 0;
        }

        //metodo para calcular limite
        protected float CalcularLimite(float renda)
        {
            float limite = renda * (float)0.3;
            return limite;
        }

        //metodo para movimentar saida de saldo da conta
        public bool MovimentarSaida(string conta, string operacao, float valor)
        {
            if (conta == "CC")
                if (valor > _saldo + _limite)
                {
                    Console.WriteLine("Você não possui saldo em sua conta");
                    return false;
                }
                else
                {
                    _saldo -= valor;
                    _extrato.Add($"Conta: {conta}\tSaida: -{valor}\tOperação: {operacao}\tData: {DateTime.Now.ToShortDateString()}");
                    return true;
                }
            else
            {
                if (_contaPoupanca._saldo <= 0 || _contaPoupanca._saldo < valor)
                {
                    Console.WriteLine("Você não possui saldo em sua conta poupança");
                    return false;
                }
                else
                {
                    _contaPoupanca._saldo -= valor;
                    _extrato.Add($"Conta: {conta}\tSaida: -{valor}\tOperação: {operacao}\tData: {DateTime.Now.ToShortDateString()}");
                    return true;
                }
            }
        }

        //metodo para movimentar saida de saldo da conta
        public void MovimentarEntrada(string conta, string operacao, float valor)
        {
            if (conta == "CC") _saldo += valor;
            else _contaPoupanca._saldo += valor;
            _extrato.Add($"Conta: {conta}\tEntrada: +{valor}\tOperação: {operacao}\tData: {DateTime.Now.ToShortDateString()}");
        }

        //metodo para retorno de saldo + limite
        public void getSaldoToString()
        {
            Console.WriteLine($"Saldo Cc: {_saldo.ToString("F")}\nSaldo Cc + Limite: {(_saldo + _limite).ToString("F")}\nSaldo Poupança: {_contaPoupanca._saldo.ToString("F")}");
        }

        //metodo para retornar senha
        public bool getSenha()
        {
            string senha;
            Console.Write($"Por favor {_pessoa._nome} informe sua senha para continuar: ");
            senha = Console.ReadLine();
            if (_senha == senha) return true;
            else return false;
        }

        #region Extrato
        //metodo para inserir debito de emprestimo no extrato
        public void setExtrato(string operacao, float valor)
        {
            _extrato.Add($"Saida: -{valor}\tOperação: {operacao}\tData: {DateTime.Now.ToShortDateString()}");
        }

        //metodo para puxa extrato
        public void getExtrato()
        {
            Console.Clear();
            Console.WriteLine("### EXTRATO ###\n");
            Console.WriteLine($"Conta: {_numConta}\tAgência: {_agencia}\n");
            foreach (var item in _extrato) Console.WriteLine(item);
        }
        #endregion Extrato

        ///ver o que pode ser melhorado a respeito
        #region Emprestimno
        //metodo para adicionar valor na solicitacao de emprestimo
        public void setValorEmprestimo(float valor)
        {
            _valorSolicitacaoEmprestimo = valor;
        }

        //metodo para buscar solicitacao de emprestimo
        public float getValorSolicitacaoEmprestimo()
        {
            return _valorSolicitacaoEmprestimo;
        }

        #endregion Emprestimo

        //metodo para saber saldo conta poupanca
        public float getSaldoPoupança()
        {
            return _contaPoupanca._saldo;
        }

        #region Cartao
        //metodo para verificar status do cartao
        public String StatusCartao()
        {
            if (_cartao._habilitarCartao == true) return "Habilitado";
            else return "Bloqueado";
        }

        //metodo para saber saldo cartão
        public float SaldoCartao()
        {
            return _cartao._saldo;
        }

        //metodo para habilitar cartao
        public void HabilitarCartao(bool estado)
        {
            _cartao.setCartao(estado);
        }

        #endregion Cartao

        //metodo toString
        public override string ToString()
        {
            return $"Agência: {_agencia}\nNúmero conta: {_numConta}\nSaldo: {_saldo.ToString("F")}\nLimite: {_limite.ToString("F")}\nTipo de conta: {_tipoConta}\nCONTA POUPANÇA\nSaldo: {_contaPoupanca}\n\nCARTÃO\n{_cartao}".ToString();
        }

    }
}
