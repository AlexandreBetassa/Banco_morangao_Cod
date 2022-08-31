using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco_Morangao
{
    internal class ContaPoupanca : ContaCorrente
    {
        private float _saldo;

        public ContaPoupanca(float saldo)
        {
            _saldo = saldo;
        }

        public float getSaldo()
        {
            return _saldo;
        }

        public override string ToString()
        {
            return $"\nCONTAPOUPANÇA\n Saldo: {_saldo}".ToString();
        }
    }
}
