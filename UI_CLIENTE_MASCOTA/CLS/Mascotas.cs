using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_CLIENTE_MASCOTA.CLS
{
    internal class Mascotas
    {
        string _IDMascota;
        string _Nombre;
        int _IDRaza;
        string _Color;
        string _Rasgo;
        string _FechaNac;
        string _Genero;
        string _IDCliente;

        //inicializacion variables
        public string IDMascota { get => _IDMascota; set => _IDMascota = value; }
        public string Nombre { get => _Nombre; set => _Nombre = value; }
        public int IDRaza { get => _IDRaza; set => _IDRaza = value; }
        public string Color { get => _Color; set => _Color = value; }
        public string Rasgo { get => _Rasgo; set => _Rasgo = value; }
        public string FechaNac { get => _FechaNac; set => _FechaNac = value; }
        public string Genero { get => _Genero; set => _Genero = value; }
        public String IDCliente { get => _IDCliente; set => _IDCliente = value; }


        //METODO QUE INSERTA UN NUEVO REGISTRO EN LA BASE DE DATOS
        public Boolean Insertar()
        {
            Boolean Resultado = false;
            String Sentencia;
            Int32 FilasInsertadas = 0;
            try
            {
                Sentencia = "INSERT INTO mascotas (Nombre,Genero,IDRaza,Color,Rasgo,FechaNac,IDCliente)" +
                            "VALUES('" + _Nombre + "', '" + _Genero + "', " + _IDRaza + ",'" + _Color + "', '" + _Rasgo + "'," +
                            "'" + _FechaNac + "', " + _IDCliente + ");";
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
                Sentencia = "UPDATE mascotas " +
                            "SET Nombre= '" + _Nombre + "', Genero = '" + _Genero + "', IDRaza= " + _IDRaza + ", Color= '" + _Color + "'," +
                            "Rasgo = '" + _Rasgo + "',FechaNac= '" + _FechaNac + "', IDCliente = " + _IDCliente + " " +
                            "WHERE IDMascota = " + _IDMascota + ";";
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
                Sentencia = @"DELETE FROM Mascotas WHERE IDMascota=" + _IDMascota + ";";
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
