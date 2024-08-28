using Ejercicio1._6.Dominio;
using Ejercicio1._6.Datos.ADO;
using Ejercicio1._6.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1._6.Servicios
{
    //capa intermedia entre dominio y acceso a datos
    public class TiposCuentaServicio
    {
        private ITiposCuentaRepository _repositorio;

        public TiposCuentaServicio()
        {
            _repositorio = new TiposCuentaRepository_ADO();
        }

        public List<TipoCuenta> GetAll() 
        {
            return _repositorio.GetAll();
        }
        public TipoCuenta GetById(int id) 
        {
            return _repositorio.GetById(id);
        }
        
    }
}
