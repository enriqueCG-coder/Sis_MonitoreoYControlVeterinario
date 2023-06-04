using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace DataManager
{
    public class DBConexion
    {
        String _CadenaConexion = "Server=localhost;Port=3306;Database=MYCITAVET_DB;Uid=usersql;Pwd=.12345admin;";
        protected MySqlConnection _CONEXION = new MySqlConnection();


        public Boolean Conectar()
        {
            Boolean result = false;
            try
            {
                _CONEXION.ConnectionString = _CadenaConexion;
                _CONEXION.Open();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public void Desconectar()
        {
            try
            {
                if (_CONEXION.State == System.Data.ConnectionState.Open)
                {
                    _CONEXION.Close();
                }
            }
            catch (Exception)
            {

            }
        }

        public DataTable EjecutarConsulta(string consulta)
        {
            DataTable resultado = new DataTable();

            MySqlCommand comando = new MySqlCommand(consulta, _CONEXION);
            MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
            adaptador.Fill(resultado);

            return resultado;
        }

    }
}
