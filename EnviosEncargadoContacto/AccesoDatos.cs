using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EnviosEncargadoContacto
{
    internal sealed class AccesoDatos
    {
        private SqlConnection dbconnection = null;
        private SqlCommand cmd;

        public AccesoDatos()
        {
            CloseConnection();
        }

        private void OpenConnection()
        {
            dbconnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConexion"].ConnectionString);
            dbconnection.Open();
        }

        public void CloseConnection()
        {
            if (dbconnection != null && dbconnection.State == ConnectionState.Open)
            {
                dbconnection.Close();
                dbconnection.Dispose();
            }
        }

        internal void ExecuteCommandQuery(string query)
        {
            cmd = new SqlCommand()
            {
                CommandType = CommandType.Text,
                CommandText = query
            };
        }

        internal string SelectString()
        {
            DataTable dt = new DataTable();
            OpenConnection();
            cmd.Connection = dbconnection;
            dt.Load(cmd.ExecuteReader());
            CloseConnection();
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return "0";
        }

        internal int InsertUpdateDelete()
        {
            int contar = 0;
            try
            {
                OpenConnection();
                cmd.Connection = dbconnection;
                contar = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                var x = ex.Message;
            }
            finally
            {
                CloseConnection();
            }
            return contar;
        }

        internal void ExecuteCommandSP(string name)
        {
            cmd = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = name
            };
        }

        internal SqlDataReader ExecuteReader()
        {
            OpenConnection();
            cmd.Connection = dbconnection;
            var reader = cmd.ExecuteReader();
            return reader;
        }

        internal void AddParameter(string name, object value, SqlDbType type)
        {
            SqlParameter param = new SqlParameter()
            {
                ParameterName = name,
                Value = value,
                SqlDbType = type
            };
            cmd.Parameters.Add(param);
        }
    }
}
