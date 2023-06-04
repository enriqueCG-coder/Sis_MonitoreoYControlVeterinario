using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_CLIENTE_MASCOTA.CLS
{
    internal class Clientes
    {
        //declaracion de variables
        String _IDCliente;
        String _Nombres;
        String _Apellidos;
        String _FechaNac;
        String _Genero;
        int _IdMunicipio;
        String _Direccion;
        String _TipoDoc;
        String _Documento;
        String _Correo;
        String _Telefono;

        //inicializacion variables
        public String IDCliente { get => _IDCliente; set => _IDCliente = value; }
        public string Nombres { get => _Nombres; set => _Nombres = value; }
        public string Apellidos { get => _Apellidos; set => _Apellidos = value; }
        public string FechaNac { get => _FechaNac; set => _FechaNac = value; }
        public string Genero { get => _Genero; set => _Genero = value; }
        public int IdMunicipio { get => _IdMunicipio; set => _IdMunicipio = value; }
        public string Direccion { get => _Direccion; set => _Direccion = value; }
        public string TipoDoc { get => _TipoDoc; set => _TipoDoc = value; }
        public string Documento { get => _Documento; set => _Documento = value; }
        public string Correo { get => _Correo; set => _Correo = value; }
        public string Telefono { get => _Telefono; set => _Telefono = value; }


        //metodo que carga los departamentos en el combobox

        public Boolean Insertar()
        {
            Boolean Resultado = false;
            String Sentencia;
            Int32 FilasInsertadas = 0;
            try
            {
                Sentencia = "INSERT INTO Clientes (Nombres,Apellidos,FechaNac," +
                                                    "Genero,IdMunicipio,Direccion," +
                                                    "TipoDoc,Documento,Correo,Telefono)" +
                            "VALUES('" + _Nombres + "', '" + _Apellidos + "', '" + _FechaNac + "' , '" + _Genero + "', " + _IdMunicipio + "," +
                                    "'" + _Direccion + "','" + _TipoDoc + "','" + _Documento + "','" + _Correo + "','" + _Telefono + "');";
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
                Sentencia = "UPDATE Clientes " +
                            "SET Nombres= '" + _Nombres + "', Apellidos = '" + _Apellidos + "', FechaNac = '"+_FechaNac+"'," +
                            "Genero = '" + _Genero + "', IdMunicipio = '" + _IdMunicipio + "', Direccion = '" + _Direccion + "'," +
                            "TipoDoc = '" + _TipoDoc + "', Documento = '" + _Documento + "',Correo = '" + _Correo + "',Telefono = '" + _Telefono + "' " +
                            "WHERE IDCliente = " + _IDCliente + ";";
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
                Sentencia = @"DELETE FROM Clientes WHERE IDCliente=" + _IDCliente + ";";
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
