using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_CLIENTE_MASCOTA.CLS
{
    internal class Razas
    {
        String _IDRaza;
        int _IDEspecie;
        String _Raza;

        public string IDRaza { get => _IDRaza; set => _IDRaza = value; }
        public int IDEspecie { get => _IDEspecie; set => _IDEspecie = value; }
        public string Raza { get => _Raza; set => _Raza = value; }

        //PARA INSERTAR UN REGISTRO A LA BASE DE DATOS 
        public Boolean Insertar()
        {
            Boolean Resultado = false;
            String Sentencia;
            Int32 FilasInsertadas = 0;
            try
            {
                Sentencia = @"INSERT INTO Razas(Raza,IDEspecie) VALUES('" + _Raza + "', " + _IDEspecie + ");";
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

        //PARA ACTUALIZAR UN REGISTRO EN LA BASE DE DATOS 
        public Boolean Actualizar()
        {
            Boolean Resultado = false;
            String Sentencia;
            Int32 FilasInsertadas = 0;
            try
            {
                Sentencia = @"UPDATE Razas SET Raza='" + _Raza + "', IDEspecie = " + _IDEspecie + " WHERE IDRaza=" + _IDRaza + ";";
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
                Sentencia = @"DELETE FROM Razas WHERE IDRaza=" + _IDRaza + ";";
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
