using Ejercicio1._6.Dominio;
using Ejercicio1._6.Interfaces;
using Ejercicio1._6.Datos.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1._6.Servicios
{
    //capa intermedia entre dominio y acceso a datos
    public class CuentaServicio
    {
        private ICuentaRepository _repositorio;
        public CuentaServicio()
        {
            _repositorio = new CuentaRepository_ADO();
        }

        public List<Cuenta> GetAll() 
        {
            return _repositorio.GetAll();
        }

        public Cuenta GetById(int id) 
        {
            return _repositorio.GetById(id);
        }

        public bool Save(Cuenta oCuenta) 
        {
            return _repositorio.Save(oCuenta);
        }

        public bool Delete(int id) 
        {
            return _repositorio.Delete(id);
        }
    }
}
