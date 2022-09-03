using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco_Morangao
{
    internal class ContaCorrente
    {
        private Pessoa _pessoa;
        private bool _habilitada;
        private String _agencia;
        private String _senha;
        private String _numConta;
        private float _saldo;
        private float _limite;
        private Cartao _cartao;
        private String _tipoConta;
        private ContaPoupanca _contaPoupanca;
        private float _valorSolicitacaoEmprestimo;
        private List<String> _extrato = new List<String>();

        Random r = new Random();

        //metodo construtor vazio
        public ContaCorrente() { }

        //adicionar ao construtor senha quando funcionamento estiver correto senha "a fazer"
        public ContaCorrente(string agencia, float saldo, string tipoConta, float renda, Pessoa pessoa, String senha)
        {
            _limite = CalcularLimite(renda);
            _habilitada = false;
            _agencia = agencia;
            _numConta = r.Next(1000, 9999).ToString();
            _saldo = saldo;
            _cartao = new Cartao(_limite, "10");
            _tipoConta = tipoConta;
            _contaPoupanca = new ContaPoupanca();
            _pessoa = pessoa;
            _senha = senha;
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
            Console.WriteLine($"Saldo Cc: {_saldo.ToString("F")}\nSaldo Cc + Limite: {_saldo + _limite.ToString("F")}\nSaldo Poupança: {_contaPoupanca._saldo.ToString("F")}");
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

        //metodo para retornar agencia
        public String getAgencia()
        {
            return _agencia;
        }

        //metodo para retornar numero da conta
        public String getNumConta()
        {
            return _numConta;

        }

        //metodo para aprovar conta
        public void setHabilitado(bool valor)
        {
            _habilitada = valor;
        }

        //metodo para retornar nome
        public String getNome()
        {
            return _pessoa.getNome();
        }

        //metodo para retornar senha
        public String getSenha()
        {
            return _senha;
        }

        //metodo para retornar tipo de conta
        public String getTipoConta()
        {
            return _tipoConta;
        }

        //metodo para inserir debito de emprestimo no extrato
        public void setExtrato(string operacao, float valor)
        {
            _extrato.Add($"Saida: -{valor}\tOperação: {operacao}\tData: {DateTime.Now.ToShortDateString()}");
        }

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

        public String getConta()
        {
            return $"{_pessoa};{_habilitada};{_agencia};{_senha};{_numConta};{_saldo};{_limite};{_cartao.getNumeroCartao()};{_tipoConta}";
        }

        public float getSaldoPoupança()
        {
            return _contaPoupanca._saldo;
        }

        public void HabilitarCartao(bool estado)
        {
            _cartao.setCartao(estado);
        }

        //metodo toString
        public override string ToString()
        {
            return $"Agência: {_agencia}\nNúmero conta: {_numConta}\nSaldo: {_saldo.ToString("F")}\nLimite: {_limite.ToString("F")}\nTipo de conta: {_tipoConta}\nCONTA POUPANÇA\nSaldo: {_contaPoupanca}\n\nCARTÃO\n{_cartao}".ToString();
        }

    }
}
