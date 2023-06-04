using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataManager
{
    public static class DBConsultas
    {
        //VALIDA EN LA BASE DE DATOS SI EL USUARIO Y LA CLAVE EXISTEN 
        public static DataTable VALIDAR_USUARIO(String pUsuario, String pClave)
        {
            DataTable Resultado = new DataTable();
            String Sentencia = @"SELECT u.IDUsuario, u.Usuario, u.IDRol, r.Rol FROM usuarios u 
                                inner join roles r on u.IDRol = r.IDRol where Usuario='" + pUsuario + "' AND Clave='" + pClave + "';";
            DBOperacion Consultor = new DBOperacion();
            try
            {
                Resultado = Consultor.Consultar(Sentencia);
            }
            catch (Exception)
            {
                Resultado = new DataTable();
            }

            return Resultado;
        }


        //OBTIENE LA TABLA ROLES 
        public static DataTable ROLES()
        {
            DataTable Resultado = new DataTable();
            String Sentencia = @"SELECT IDRol, Rol FROM roles;";
            DBOperacion Consultor = new DBOperacion();
            try
            {
                Resultado = Consultor.Consultar(Sentencia);
            }
            catch (Exception)
            {
                Resultado = new DataTable();
            }
            return Resultado;
        }

        public static DataTable PERMISOS(string pIDRol)
        {
            DataTable Resultado = new DataTable();
            String Sentencia = @"SELECT a.IDOpcion, a.Opcion, 
                                a.IDClasificacion, b.Clasificacion,
                        IFNULL((SELECT IDPermiso FROM Permisos z 
                        WHERE z.IDRol =" + pIDRol + @" and z.IDOpcion),0) IDPermiso,
                        IF(IFNULL((SELECT IDPermiso FROM permisos z 
                        WHERE z.IDRol = "+ pIDRol+ @" and z.IDOpcion = a.IDOpcion),0)>0,1,0) Asignado
                        FROM opciones a, clasificaciones b
                        WHERE a.IDClasificacion = b.IDClasificacion;";
            DBOperacion Consultor = new DBOperacion();
            try
            {
                Resultado = Consultor.Consultar(Sentencia);
            }
            catch (Exception)
            {
                Resultado = new DataTable();
            }
            return Resultado;
        }

        //OBTIENE LA TABLA DE ESPECIES
        public static DataTable ESPECIES()
        {
            DataTable Resultado = new DataTable();
            String Sentencia = @"SELECT IDEspecie, Especie as Especies FROM Especies;";
            DBOperacion Consultor = new DBOperacion();
            try
            {
                Resultado = Consultor.Consultar(Sentencia);
            }
            catch (Exception)
            {
                Resultado = new DataTable();
            }
            return Resultado;
        }

        //SE REALIZA UNA CONSULTA A LA BASE DE DATOS
        //PARA OBTENER SI EXISTE UNA CITA AGENDADA EN LA FECHA Y HORA QUE SE SELECCIONE
        public static DataTable ValidarCitas(string fecha, string horaInicio, string horaFin)
        {
            DataTable Resultado = new DataTable();
            String Sentencia = @"SELECT * FROM Citas WHERE Fecha = '"+fecha+"' AND ((Hora_Inicio >= '"+horaInicio+"' AND Hora_Inicio < '"+horaFin+"') OR (Hora_Fin > '"+horaInicio+"' AND Hora_Fin <= '"+horaFin+"'));";
            DBOperacion Consultor = new DBOperacion();
            try
            {
                Resultado = Consultor.Consultar(Sentencia);
            }
            catch (Exception)
            {
                Resultado = new DataTable();
            }

            return Resultado;
        }


        //OBTIENE LA TABLA DE ESPECIES
        public static DataTable getProductos()
        {
            DataTable Resultado = new DataTable();
            String Sentencia = @"SELECT * FROM Productos;";
            DBOperacion Consultor = new DBOperacion();
            try
            {
                Resultado = Consultor.Consultar(Sentencia);
            }
            catch (Exception)
            {
                Resultado = new DataTable();
            }
            return Resultado;
        }
    }
}
