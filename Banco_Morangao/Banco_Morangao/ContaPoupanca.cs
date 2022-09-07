using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco_Morangao
{
    internal class ContaPoupanca : ContaCorrente
    {

        public ContaPoupanca()
        {
            _saldo = 0;
        }


        public override string ToString()
        {
            return $"\nCONTAPOUPANÇA\nSaldo: {_saldo.ToString("F")}".ToString();
        }
    }
}
