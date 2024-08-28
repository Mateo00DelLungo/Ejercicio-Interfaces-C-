using Ejercicio1._6.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1._6.Interfaces
{
    public interface ICuentaRepository
    {
        List<Cuenta> GetAll();

        Cuenta GetById(int id);

        bool Save(Cuenta oCuenta);

        bool Delete(int id);

    }
}
