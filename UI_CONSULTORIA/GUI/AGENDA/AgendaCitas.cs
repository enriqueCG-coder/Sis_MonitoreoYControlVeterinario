using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_CONSULTORIA.GUI.AGENDA
{
    public partial class AgendaCitas : Form
    {
        public AgendaCitas()
        {
            InitializeComponent();
        }

        #region METODOS
        //CARGA TODAS LAS CITAS 
        public void cargarCitas()
        {
            
            DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
            DataTable Resultado = new DataTable();
            Resultado = Operacion.Consultar(@"SELECT  ci.IDcita, ci.Correlativo_Cita, ci.Fecha, ci.Hora_Inicio, ci.Hora_Fin, ci.Motivo, ci.Estado, 
		                                    ci.IDMascota, m.Nombre as Mascota, m.Genero, r.Raza, c.IDCliente, 
                                            concat(c.Nombres, ' ', c.Apellidos) as Nombre, concat(c.TipoDoc, '-', c.Documento) as Documento 
		                                    FROM citas ci INNER JOIN mascotas m ON m.IDMascota = ci.IDMascota 
                                            INNER JOIN Razas r on m.IDRaza = r.IDraza 
		                                    INNER JOIN clientes c on c.IDCliente = m.IDCliente;");
            dgvCitasDelDia.DataSource = Resultado;
            lblRegistros.Text = dgvCitasDelDia.Rows.Count.ToString() + " Registros Encontrados";

            dgvCitasDelDia.Columns["IDCita"].Visible = false;
            dgvCitasDelDia.Columns["IDMascota"].Visible = false;
            dgvCitasDelDia.Columns["IDCliente"].Visible = false;
        }
        #endregion 

        private void AgendaCitas_Load(object sender, EventArgs e)
        {
            cargarCitas();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
        //    try
        //    {
        //        UI_CONSULTORIA.GUI.AGENDA.frmInfoCita f = new UI_CONSULTORIA.GUI.AGENDA.frmInfoCita();
        //        f.ShowDialog();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        }
    }
}
