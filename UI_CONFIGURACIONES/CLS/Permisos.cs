using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_CONFIGURACIONES.CLS
{
    internal class Permisos
    {
        String _IDPermiso;
        String _IDRol;
        String _IDOpcion;

        public string IDPermiso { get => _IDPermiso; set => _IDPermiso = value; }
        public string IDRol { get => _IDRol; set => _IDRol = value; }
        public string IDOpcion { get => _IDOpcion; set => _IDOpcion = value; }

        public Boolean Insertar()
        {
            Boolean Resultado = false;
            String Sentencia;
            Int32 FilasInsertadas = 0;
            try
            {
                Sentencia = @"INSERT INTO permisos(IDRol,IDOpcion) VALUES(" + _IDRol + "," + _IDOpcion + ");";
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

        public Boolean Eliminar()
        {
            Boolean Resultado = false;
            String Sentencia;
            Int32 FilasEliminadas = 0;
            try
            {
                Sentencia = @"DELETE FROM permisos WHERE IDPermiso=" + _IDPermiso + ";";
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
