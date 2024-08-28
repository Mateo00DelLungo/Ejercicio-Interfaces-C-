using Ejercicio1._6.Dominio;
using Ejercicio1._6.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1._6.Datos.ADO
{
    public class TiposCuentaRepository_ADO : ITiposCuentaRepository
    {
        private TipoCuenta Mapeo(DataRow row)
        {
            int id = Convert.ToInt32(row["id"]);
            string nombre = row["nombre"].ToString();
            TipoCuenta otipo = new TipoCuenta(id, nombre);
            return otipo;

        }
        public List<TipoCuenta> GetAll()
        {
            List<TipoCuenta> tipoCuentas = new List<TipoCuenta>();

            var helper = DataHelper.GetInstance();
            var dt = helper.ExecuteSPQuery("SP_RECUPERAR_TIPOS", null);

            foreach (DataRow row in dt.Rows)
            {
                TipoCuenta oTipoCuenta = Mapeo(row);
                tipoCuentas.Add(oTipoCuenta);
            }
            return tipoCuentas;
        }
        public TipoCuenta GetById(int id)
        {
            var parametros = new List<ParameterSQL>();
            parametros.Add(new ParameterSQL("@id", id));

            var helper = DataHelper.GetInstance();
            var dt = helper.ExecuteSPQuery("SP_RECUPERAR_TIPO_POR_ID", parametros);

            if (dt != null && dt.Rows.Count == 1)
            {
                DataRow row = dt.Rows[0];
                TipoCuenta oTipoCuenta = Mapeo(row);
                return oTipoCuenta;
            }
            return null;
        }
    }
}
