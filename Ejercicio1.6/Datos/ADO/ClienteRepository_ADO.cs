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
    public class ClienteRepository_ADO : IClienteRepository
    {
        private Cliente Mapeo(DataRow row)
        {
            int dni = Convert.ToInt32(row["dni"]);
            string nombre = row["nombre"].ToString();
            string apellido = row["apellido"].ToString();

            Cliente oCliente = new Cliente(dni, nombre, apellido);
            return oCliente;
        }
        public List<Cliente> GetAll()
        {
            List<Cliente> clientes = new List<Cliente>();
            //llama al datahelper 
            var helper = DataHelper.GetInstance();
            //mapea la dt con los datos del sp 
            var dt = helper.ExecuteSPQuery("SP_RECUPERAR_CLIENTES", null);

            foreach (DataRow row in dt.Rows)
            {
                Cliente oCliente = Mapeo(row);
                clientes.Add(oCliente);
            }

            return clientes;
        }
        public Cliente GetById(int id)
        {

            var parametros = new List<ParameterSQL>();
            parametros.Add(new ParameterSQL("@dni", id));

            var helper = DataHelper.GetInstance();
            DataTable dt = helper.ExecuteSPQuery("SP_RECUPERAR_CLIENTE_POR_DNI", parametros);

            if (dt != null && dt.Rows.Count == 1)
            {
                DataRow row = dt.Rows[0]; //nos aseguramos q sea la primer fila
                Cliente oCliente = Mapeo(row);
                return oCliente;
            }
            return null;
        }
    }
}
