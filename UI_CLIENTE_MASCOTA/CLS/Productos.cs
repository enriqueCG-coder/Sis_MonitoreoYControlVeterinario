using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_CLIENTE_MASCOTA.CLS
{
    internal class Productos
    {
        string _ID_Producto;
        string _TipoProducto;
        string _Nombre;
        string _Marca;
        string _Descripcion;
        int _Stock;

        public string ID_Producto { get => _ID_Producto; set => _ID_Producto = value; }
        public string TipoProducto { get => _TipoProducto; set => _TipoProducto = value; }
        public string Nombre { get => _Nombre; set => _Nombre = value; }
        public string Marca { get => _Marca; set => _Marca = value; }
        public string Descripcion { get => _Descripcion; set => _Descripcion = value; }
        public int Stock { get => _Stock; set => _Stock = value; }

        //PARA INSERTAR UN REGISTRO A LA BASE DE DATOS 
        public Boolean Insertar()
        {
            Boolean Resultado = false;
            String Sentencia;
            Int32 FilasInsertadas = 0;
            try
            {
                Sentencia = "INSERT INTO Productos(TipoProducto,Nombre,Marca,Descripcion,Stock) " +
                            "VALUES('"+_TipoProducto+"','" + _Nombre + "','" + _Marca + "','" + _Descripcion + "'," + _Stock + ");";
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
        public Boolean ActualizarProd()
        {
            Boolean Resultado = false;
            String Sentencia;
            Int32 FilasInsertadas = 0;
            try
            {
                Sentencia = "UPDATE Productos SET TipoProducto='"+_TipoProducto+"', Nombre='" + _Nombre + "',Marca = '" + _Marca + "',Descripcion = '" + _Descripcion + "',Stock = '" + _Stock + "' " +
                            "WHERE Id_Producto=" + _ID_Producto + ";";
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
        public Boolean ActualizarStock()
        {
            Boolean Resultado = false;
            String Sentencia;
            Int32 FilasInsertadas = 0;
            try
            {
                Sentencia = "UPDATE Productos SET Stock = '" + _Stock + "' " +
                            "WHERE Id_Producto=" + _ID_Producto + ";";
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
                Sentencia = @"DELETE FROM Productos WHERE Id_Producto=" + _ID_Producto + ";";
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
