using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_MANTENIMIENTOS.CLS
{
    internal class Usuarios
    {
        string _IDUsuario;
        string _Usuario;
        string _Clave;
        string _IDEmpleado;
        string _IDRol;

        public string IDUsuario { get => _IDUsuario; set => _IDUsuario = value; }
        public string Usuario { get => _Usuario; set => _Usuario = value; }
        public string Clave { get => _Clave; set => _Clave = value; }
        public string IDEmpleado { get => _IDEmpleado; set => _IDEmpleado = value; }
        public string IDRol { get => _IDRol; set => _IDRol = value; }

        //metodo que carga los departamentos en el combobox

        public Boolean Insertar()
        {
            Boolean Resultado = false;
            String Sentencia;
            Int32 FilasInsertadas = 0;
            try
            {
                Sentencia = @"INSERT INTO Usuarios (Usuario,Clave,IDEmpleado,IDRol)
                              VALUES('" + _Usuario + "', '" + _Clave + "', " + _IDEmpleado + ", " + _IDRol + ");";
                DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
                FilasInsertadas = Operacion.EjecutarSentencia(Sentencia);
                if (FilasInsertadas > 0)
                {
                    Resultado = true;
                }
            }
            catch (Exception)
            {
                Resultado = false;
            }
            return Resultado;
        }
        public Boolean Actualizar()
        {
            Boolean Resultado = false;
            String Sentencia;
            Int32 FilasInsertadas = 0;
            try
            {
                Sentencia = "UPDATE Usuarios " +
                            "SET Usuario= '" + _Usuario + "', Clave = '" + _Clave + "',IDEmpleado= " + _IDEmpleado + "," +
                            "IDRol = " + _IDRol + " WHERE IDUsuario = " + _IDUsuario + ";";
                DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
                FilasInsertadas = Operacion.EjecutarSentencia(Sentencia);
                if (FilasInsertadas > 0)
                {
                    Resultado = true;
                }
            }
            catch (Exception)
            {
                Resultado = false;
            }
            return Resultado;
        }

        //PARA ELIMINAR UN REGISTRO EN LA BASE DE DATOS 
        public Boolean Eliminar()
        {
            Boolean Resultado = false;
            String Sentencia;
            Int32 FilasEliminadas = 0;
            try
            {
                Sentencia = @"DELETE FROM Usuarios WHERE IDUsuario=" + _IDUsuario + ";";
                DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
                FilasEliminadas = Operacion.EjecutarSentencia(Sentencia);
                if (FilasEliminadas > 0)
                {
                    Resultado = true;
                }
            }
            catch (Exception)
            {
                Resultado = false;
            }
            return Resultado;
        }
    }
}
