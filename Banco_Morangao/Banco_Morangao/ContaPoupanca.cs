using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco_Morangao
{
    internal class ContaPoupanca : ContaCorrente
    {
        internal protected float _saldo;


        public override string ToString()
        {
            return $"\nCONTAPOUPANÇA\nSaldo: {_saldo.ToString("F")}".ToString();
        }
    }
}
