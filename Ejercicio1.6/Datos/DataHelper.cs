using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ejercicio1._6.Datos;
namespace Ejercicio1._6.Datos
{
    public class DataHelper
    {
        //data access
        //static
        //singleton

        private static DataHelper _instance;
        private string _cnnString;
        private SqlConnection _cnn;


        private DataHelper()
        {
            _cnnString = @"Data Source=LAPTOP-LLC1ACAM\\SQL;Initial Catalog=Banco;Integrated Security=True;Trust Server Certificate=True";
            _cnn = new SqlConnection(_cnnString);
        }

        public static DataHelper GetInstance() 
        {
            if (_instance == null) 
            {
                _instance = new DataHelper();
            }
            return _instance;
        }

        //ejecuta un stored procedure
        // y mapea un datatable con los datos que deuelva
        public DataTable ExecuteSPQuery(string StoredProcedure, List<ParameterSQL>? parametros)
        {
            DataTable dt = new DataTable();

            try
            {
                _cnn.Open();
                var cmd = new SqlCommand(StoredProcedure, _cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (parametros != null)
                {
                    foreach (var param in parametros)
                    {
                        cmd.Parameters.AddWithValue(param.Name,param.Value);
                    }
                }
                dt.Load(cmd.ExecuteReader());

            }
            catch (SqlException ex)
            {
                dt = null;
                throw ex;
            }
            finally 
            {
                _cnn.Close();
            }
            return dt;

        }

        public int ExecuteSPNonQuery(string sp , List<ParameterSQL>? parametros)
        {
            //filas afectadas por el stored procedure
            int affectedRows;

            try
            {
                _cnn.Open();
                var cmd = new SqlCommand(sp ,_cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                //se agregan los parametros al procedimiento almacenado
                if (parametros != null) 
                {
                    foreach (var param in parametros) 
                    {
                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                    }
                }
                affectedRows = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {

                throw ex;
                affectedRows = 0;
            }
            finally 
            {
                _cnn.Close();
            }

            return affectedRows;
        }


    }
}
