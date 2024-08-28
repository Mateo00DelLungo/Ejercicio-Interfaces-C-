using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1._6.Dominio
{
    public class TipoCuenta
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public TipoCuenta(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
        public TipoCuenta()
        {
            
        }

    }
}
