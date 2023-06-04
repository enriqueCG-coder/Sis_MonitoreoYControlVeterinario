using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataManager
{
    public class DBOperacion : DBConexion
    {
        public object Datatable { get; private set; }

        public Int32 EjecutarSentencia(String pSentencia)
        {
            Int32 FilasAfectadas = 0;
            MySqlCommand Comando = new MySqlCommand();
            if (base.Conectar())
            {
                Comando.Connection = base._CONEXION;
                Comando.CommandType = System.Data.CommandType.Text;
                Comando.CommandText = pSentencia;
                try
                {
                    FilasAfectadas = Comando.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    FilasAfectadas = -1;

                }
                base.Desconectar();
            }
            return FilasAfectadas;
        }
        public DataTable Consultar(string pConsulta)
        {
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();
            MySqlCommand Comando = new MySqlCommand();
            DataTable Resultado = new DataTable();
            if (base.Conectar())
            {
                Comando.Connection = base._CONEXION;
                Comando.CommandType = System.Data.CommandType.Text;
                Comando.CommandText = pConsulta;
                Adaptador.SelectCommand = Comando;
                try
                {
                    Adaptador.Fill(Resultado);
                }
                catch (Exception)
                {

                    Resultado = new DataTable();
                }
                base.Desconectar();
            }
            return Resultado;
        }

    }
}
