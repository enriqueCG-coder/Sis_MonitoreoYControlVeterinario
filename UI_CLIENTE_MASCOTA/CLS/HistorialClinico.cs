using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_CLIENTE_MASCOTA.CLS
{
    internal class HistorialClinico
    {
        string _IDHistorial;
        string _IDMascota;
        string _Fecha;
        string _Diagnostico;
        string _Peso;

        //inicializacion variables
        public string IDHistorial { get => _IDHistorial; set => _IDHistorial = value; }
        public string IDMascota { get => _IDMascota; set => _IDMascota = value; }
        public string Fecha { get => _Fecha; set => _Fecha = value; }
        public string Diagnostico { get => _Diagnostico; set => _Diagnostico = value; }
        public string Peso { get => _Peso; set => _Peso = value; }

        //METODO QUE INSERTA UN NUEVO REGISTRO EN LA BASE DE DATOS
        public Boolean Insertar()
        {
            Boolean Resultado = false;
            String Sentencia;
            Int32 FilasInsertadas = 0;
            try
            {
                Sentencia = "INSERT INTO HistorialClinico (IDMascota,Fecha,Diagnostico,Peso) VALUES(" + _IDMascota + ", '" + _Fecha + "','" + _Diagnostico + "', '" + _Peso + "');";
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
                Sentencia = "UPDATE HistorialClinico " +
                            "SET Fecha= '" + _Fecha + "', Diagnostico = '" + _Diagnostico + "', Peso= '" + _Peso + "'"+
                            "WHERE IDHistorial = " + _IDHistorial + ";";
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
                Sentencia = @"DELETE FROM HistorialClinico WHERE IDHistorial=" + _IDHistorial + ";";
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
