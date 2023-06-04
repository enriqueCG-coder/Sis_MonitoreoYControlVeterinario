using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_CONFIGURACIONES.CLS
{
    internal class Opciones
    {
        string _IDOpcion;
        string _Opcion;
        string _IDClasificacion;


        public string IDOpcion { get => _IDOpcion; set => _IDOpcion = value; }
        public string Opcion { get => _Opcion; set => _Opcion = value; }
        public string IDClasificacion { get => _IDClasificacion; set => _IDClasificacion = value; }
        

        //METODO QUE INSERTA UN NUEVO REGISTRO EN LA BD
        public Boolean Insertar()
        {
            Boolean Resultado = false;
            String Sentencia;
            Int32 FilasInsertadas = 0;
            try
            {
                Sentencia = @"INSERT INTO Opciones(Clasificacion,IDClasificacion) VALUES('" + _Opcion + "'," + _IDClasificacion + ");";
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
                Sentencia = @"UPDATE Opciones SET Opcion = '" + _Opcion + "',IDClasificacion = " + _IDClasificacion + " WHERE IDOpcion = " + _IDOpcion + ";";
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
                Sentencia = @"DELETE FROM Opciones WHERE IDOpcion=" + _IDOpcion + ";";
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
