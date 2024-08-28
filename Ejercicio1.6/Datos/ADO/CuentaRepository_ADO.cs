using Ejercicio1._6.Dominio;
using Ejercicio1._6.Interfaces;
using Ejercicio1._6.Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1._6.Datos.ADO
{
    internal class CuentaRepository_ADO : ICuentaRepository
    {
        //CRUD
        //conectar a la base de datos con HELPER
        //mapear
        private ClienteServicio _clienteManager;
        private TiposCuentaServicio _tiposCuentaManager;
        

        public bool Delete(int id)
        {
            var parametros = new List<ParameterSQL>();
            parametros.Add(new ParameterSQL("@cbu", id));

            var helper = DataHelper.GetInstance();
            return 1 == helper.ExecuteSPNonQuery("SP_BORRAR_CUENTA",parametros);

        }

        private Cuenta Mapeo(DataRow row)
        {
            int cbu = Convert.ToInt32(row["cbu"]);
            double saldo = Convert.ToDouble(row["saldo"]);

            int idcliente = Convert.ToInt32(row["cliente_dni"]);
            Cliente oCliente = _clienteManager.GetById(idcliente);

            int idtipo = Convert.ToInt32(row["tipo_cuenta"]);
            TipoCuenta oTipoCuenta = _tiposCuentaManager.GetById(idtipo);

            string ultMov = row["ultimoMovimiento"].ToString();

            Cuenta oCuenta = new Cuenta(cbu, saldo, oCliente, oTipoCuenta, ultMov);

            return oCuenta;

        }
        public List<Cuenta> GetAll()
        {
            List<Cuenta> cuentas = new List<Cuenta>();
            var helper = DataHelper.GetInstance();
            var dt = helper.ExecuteSPQuery("SP_RECUPERAR_CUENTAS", null);
            if (dt != null) 
            {
                foreach (DataRow row in dt.Rows)
                {
                    Cuenta oCuenta = Mapeo(row);
                    cuentas.Add(oCuenta);
                }
            }
                return cuentas;
        }

        public Cuenta GetById(int id)
        {
            var parametros = new List<ParameterSQL>();
            parametros.Add(new ParameterSQL("@cbu", id));
            var helper = DataHelper.GetInstance();
            var dt = helper.ExecuteSPQuery("SP_RECUPERAR_CUENTAS_POR_CBU", parametros);

            if (dt != null && dt.Rows.Count == 1)
            {
                //DataRow row = dt.Rows[0] es la primer fila de la tabla
                Cuenta oCuenta = Mapeo(dt.Rows[0]);
                return oCuenta;
            }
            return null;
        }

        public bool Save(Cuenta oCuenta)
        {
            bool result = false;
            var parametros = new List<ParameterSQL>();
            if(oCuenta != null) 
            {
                parametros.Add(new ParameterSQL("@cbu",oCuenta.CBU));
                parametros.Add(new ParameterSQL("@saldo", oCuenta.Saldo));
                parametros.Add(new ParameterSQL("@tipo_cuenta", oCuenta._TipoCuenta.Id));
                parametros.Add(new ParameterSQL("@ultimoMovimiento", oCuenta.UltimoMov));
                parametros.Add(new ParameterSQL("@cliente_dni", oCuenta._Cliente.DNI));
                DataHelper.GetInstance().ExecuteSPNonQuery("SP_GUARDAR_CUENTA", parametros);
            }
            return result;
        }
    }
}
