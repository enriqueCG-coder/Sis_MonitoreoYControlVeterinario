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


        BindingSource _DATOS = new BindingSource();

        #region METODOS
        //CONTADOR Y OBTENCION DE REGISTROS EN EL DATAGRIDVIEW
        private void cargarCitas()
        {
            try
            {
                dgvCitasDelDia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                _DATOS.DataSource = DataManager.DBConsultas.getCitasAgendadas();
                dgvCitasDelDia.DataSource = _DATOS;
                lblRegistros.Text = dgvCitasDelDia.Rows.Count.ToString() + " Registros Encontrados";
                dgvCitasDelDia.Columns["IDCita"].Visible = false;
                dgvCitasDelDia.Columns["IDMascota"].Visible = false;
                dgvCitasDelDia.Columns["IDCliente"].Visible = false;
                dgvCitasDelDia.Columns["Mascota"].Visible = false;
                dgvCitasDelDia.Columns["Genero"].Visible = false;
                dgvCitasDelDia.Columns["Raza"].Visible = false;
                dgvCitasDelDia.Columns["Nombre"].Visible = false;
                dgvCitasDelDia.Columns["Documento"].Visible = false;
            }
            catch (Exception)
            {
                //NO DEVUELVE HACE NADA 
            }
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
