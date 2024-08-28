using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1._6.Dominio
{
    public class Cuenta
    {
        public int CBU { get; set; }
        public double Saldo { get; set; }
        public Cliente _Cliente { get; set; }
        public TipoCuenta _TipoCuenta { get; set; }
        public string UltimoMov { get; set; }

        public Cuenta(int cbu, double saldo, Cliente cliente, TipoCuenta tipoCuenta, string ultimoMov)
        {
            CBU = cbu;
            Saldo = saldo;
            _Cliente = cliente;
            _TipoCuenta = tipoCuenta;
            UltimoMov = ultimoMov;
        }
        public Cuenta()
        {
            
        }

    }

}
