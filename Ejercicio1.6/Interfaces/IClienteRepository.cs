using Ejercicio1._6.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1._6.Interfaces
{
    public interface IClienteRepository
    {
        List<Cliente> GetAll();

        Cliente GetById(int id);
    }
}
