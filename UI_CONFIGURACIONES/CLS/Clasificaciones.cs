using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_CONFIGURACIONES.CLS
{
    internal class Clasificaciones
    {
        String _IDClasificacion;
        String _Clasificacion;

        public string IDClasificacion { get => _IDClasificacion; set => _IDClasificacion = value; }
        public string Clasificacion { get => _Clasificacion; set => _Clasificacion = value; }
        
        //METODO QUE INSERTA UN NUEVO REGISTRO EN LA BD
        public Boolean Insertar()
        {
            Boolean Resultado = false;
            String Sentencia;
            Int32 FilasInsertadas = 0;
            try
            {
                Sentencia = @"INSERT INTO Clasifiaciones(Clasificacion) VALUES('" + _Clasificacion + "');";
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


        //METODO QUE MODIFICA UN REGISTRO EN LA BD
        public Boolean Actualizar()
        {
            Boolean Resultado = false;
            String Sentencia;
            Int32 FilasInsertadas = 0;
            try
            {
                Sentencia = @"UPDATE Clasificaciones SET Clasificacion = '"+ _Clasificacion +"' WHERE IDClasificacion = "+ _IDClasificacion +";";
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

        //METODO QUE ELIMINA UN REGISTRO DE LA BD
        public Boolean Eliminar()
        {
            Boolean Resultado = false;
            String Sentencia;
            Int32 FilasEliminadas = 0;
            try
            {
                Sentencia = @"DELETE FROM Clasificaciones WHERE IDClasificacion=" + _IDClasificacion + ";";
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
