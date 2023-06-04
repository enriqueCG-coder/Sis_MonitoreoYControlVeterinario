using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_CLIENTE_MASCOTA.CLS
{
    internal class Cita
    {
        string _IDCita;
        string _IDMascota;
        string _CorrelativoCita;
        string _Fecha;
        string _HoraInicio;
        string _HoraFin;
        string _Motivo;
        string _Estado;


        public string IDCita { get => _IDCita; set => _IDCita = value; }
        public string IDMascota { get => _IDMascota; set => _IDMascota = value; }
        public string CorrelativoCita { get => _CorrelativoCita; set => _CorrelativoCita = value; }
        public string Fecha { get => _Fecha; set => _Fecha = value; }
        public string HoraInicio { get => _HoraInicio; set => _HoraInicio = value; }
        public string HoraFin { get => _HoraFin; set => _HoraFin = value; }
        public string Motivo { get => _Motivo; set => _Motivo = value; }
        public string Estado { get => _Estado; set => _Estado = value; }


        //METODO QUE INSERTA UN NUEVO REGISTRO EN LA BASE DE DATOS
        public Boolean Insertar()
        {
            Boolean Resultado = false;
            String Sentencia;
            Int32 FilasInsertadas = 0;
            try
            {
                Sentencia = "INSERT INTO Citas (IDMascota,Correlativo_Cita,Fecha,Hora_Inicio, Hora_Fin, Motivo, Estado) " +
                    "VALUES(" + _IDMascota + ", '" + _CorrelativoCita + "','" + _Fecha + "', '" + _HoraInicio + "', '" + _HoraFin + "', '" + _Motivo + "', 'AGENDADA');";
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
                Sentencia = "UPDATE Citas " +
                            "SET Correlativo_Cita= '" + _CorrelativoCita + "', Fecha = '" + _Fecha + "', Hora_Inicio= '" + _HoraInicio + "'," +
                            "Hora_Fin= '" + _HoraFin + "', Motivo='" + _Motivo + "'"+
                            "WHERE IDCita = " + _IDCita + ";";
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
        //public Boolean Eliminar()
        //{
        //    Boolean Resultado = false;
        //    String Sentencia;
        //    Int32 FilasEliminadas = 0;
        //    try
        //    {
        //        Sentencia = @"DELETE FROM HistorialClinico WHERE IDHistorial=" + _IDHistorial + ";";
        //        DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
        //        FilasEliminadas = Operacion.EjecutarSentencia(Sentencia);
        //        if (FilasEliminadas > 0)
        //        {
        //            Resultado = true;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        Resultado = false;
        //    }
        //    return Resultado;
        //}
    }
}
