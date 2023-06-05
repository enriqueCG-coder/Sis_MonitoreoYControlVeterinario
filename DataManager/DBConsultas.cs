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
        #region CONSULTAS PARA LOS CONTADORES DEL MAIN}
        //OBTIENE EL TOTAL DE REGISTROS DE CLIENTE 
        public static DataTable totalClientes()
        {
            DataTable Resultado = new DataTable();
            String Sentencia = @"SELECT count(*) AS Clientes FROM clientes;";
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

        //OBTIENE EL TOTAL DE REGISTROS DE MASCOTAS 
        public static DataTable totalMascotas()
        {
            DataTable Resultado = new DataTable();
            String Sentencia = @"SELECT count(*) AS Mascotas FROM Mascotas;";
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

        //OBTIENE EL TOTAL DE REGISTROS DE USUARIOS 
        public static DataTable totalUsuarios()
        {
            DataTable Resultado = new DataTable();
            String Sentencia = @"SELECT count(*) AS Usuarios FROM Usuarios;";
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

        //OBTIENE EL TOTAL DE REGISTROS DE CITAS PROGRAMADAS 
        public static DataTable totalCitas()
        {
            DataTable Resultado = new DataTable();
            String Sentencia = @"SELECT count(*) as Citas FROM Citas WHERE Estado = 'AGENDADA';";
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
        #endregion

        #region CONSULTAS SIN PARAMETROS

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

        //OBTIENE LA TABLA DE PRODUCTOS
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

        //OBTIENE LA TABLA DE PRODUCTOS
        public static DataTable getClientes()
        {
            DataTable Resultado = new DataTable();
            String Sentencia = "SELECT C.IDCliente, C.Nombres, " +
                                            "C.Apellidos, C.FechaNac, C.Genero," +
                                            "D.Nombre as Departamento, M.Nombre as Municipio, " +
                                            "C.Direccion, C.TipoDoc, C.Documento, " +
                                            "C.Correo, C.Telefono, D.Id, C.IdMunicipio FROM Clientes as C " +
                                            "INNER JOIN Municipio as M " +
                                            "ON M.Id = C.IdMunicipio " +
                                            "INNER JOIN Departamento as D " +
                                            "ON D.Id = M.IdDepartamento;";
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


        //OBTIENE LA TABLA DE CITAS
        public static DataTable getCitasAgendadas()
        {
            DataTable Resultado = new DataTable();
            String Sentencia = @"SELECT  ci.IDcita, ci.Correlativo_Cita as Correlativo, ci.Fecha, ci.Hora_Inicio, ci.Hora_Fin, ci.Motivo, ci.Estado, 
		                                    ci.IDMascota, m.Nombre as Mascota, m.Genero, r.Raza, c.IDCliente, 
                                            concat(c.Nombres, ' ', c.Apellidos) as Nombre, concat(c.TipoDoc, '-', c.Documento) as Documento 
		                                    FROM citas ci INNER JOIN mascotas m ON m.IDMascota = ci.IDMascota 
                                            INNER JOIN Razas r on m.IDRaza = r.IDraza 
		                                    INNER JOIN clientes c on c.IDCliente = m.IDCliente;";
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
        #endregion


        #region CONSULTAS CON PARAMETROS 

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

        //SE REALIZA UNA CONSULTA A LA BASE DE DATOS
        //PARA OBTENER SI EXISTE UNA CITA AGENDADA EN LA FECHA Y HORA QUE SE SELECCIONE
        public static DataTable ValidarCitas(string fecha, string horaInicio, string horaFin)
        {
            DataTable Resultado = new DataTable();
            String Sentencia = @"SELECT * FROM Citas WHERE Fecha = '" + fecha + "' AND ((Hora_Inicio >= '" + horaInicio + "' AND Hora_Inicio < '" + horaFin + "') OR (Hora_Fin > '" + horaInicio + "' AND Hora_Fin <= '" + horaFin + "'));";
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

        //OBTIENE LA TABLA DE HISTORIAL CLINICO POR EL ID DE LA MASCOTA SELECCIONADA
        public static DataTable getHistPorIDMascota(string IDMascota)
        {
            DataTable Resultado = new DataTable();
            
            String Sentencia = @"SELECT * FROM historialclinico WHERE IDMascota = " + IDMascota + " ORDER BY Fecha asc;";
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

        //OBTIENE LA TABLA DE ANIMALES SEGUBN EL ID DEL CLIENTE SELECCIONADO
        public static DataTable getMascotaByCliente(string IDCliente)
        {
            DataTable Resultado = new DataTable();

            String Sentencia = "SELECT m.IDMascota, m.Nombre, m.Genero, m.IDRaza,r.Raza, r.IDEspecie, e.Especie, m.Color, m.Rasgo, m.FechaNac " +
                                            "FROM mascotas m " +
                                            "INNER JOIN Razas r ON m.IDRaza = r.IDRaza " +
                                            "INNER JOIN Especies e ON r.IDEspecie = e.IDEspecie WHERE IDCliente = " + IDCliente + ";";
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

        //OBTIENE LOS PERMISOS SEGUN EL ID DEL ROL
        public static DataTable PERMISOS(string pIDRol)
        {
            DataTable Resultado = new DataTable();
            String Sentencia = @"SELECT a.IDOpcion, a.Opcion, 
                                a.IDClasificacion, b.Clasificacion,
                        IFNULL((SELECT IDPermiso FROM Permisos z 
                        WHERE z.IDRol =" + pIDRol + @" and z.IDOpcion),0) IDPermiso,
                        IF(IFNULL((SELECT IDPermiso FROM permisos z 
                        WHERE z.IDRol = " + pIDRol + @" and z.IDOpcion = a.IDOpcion),0)>0,1,0) Asignado
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
        #endregion
    }
}
