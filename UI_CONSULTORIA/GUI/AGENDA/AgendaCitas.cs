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
            }
            catch (Exception)
            {
                //NO DEVUELVE HACE NADA 
            }
        }

        //CLASE PARA LAS VARIABLES DEL CMBTIPOCITA
        public class Opcion
        {
            public double Valor { get; set; }
            public string Descripcion { get; set; }
        }

        //CARGA LOS TIPOS DE CITA
        private void cargarTipoCita()
        {
            List<Opcion> opciones = new List<Opcion>()
            {
                new Opcion { Valor = 0, Descripcion = "Seleccione" },
                new Opcion { Valor = 1, Descripcion = "AGENDADA" },
                new Opcion { Valor = 2, Descripcion = "EN PROCESO" },
                new Opcion { Valor = 3, Descripcion = "FINALIZADA" },
                new Opcion { Valor = 4, Descripcion = "CANCELADA" }
            };

            cbEstado.DataSource = opciones;
            cbEstado.DisplayMember = "Descripcion";
            cbEstado.ValueMember = "Valor";
            cbEstado.SelectedIndex = 0;
        }

        //PARA BUSCAR EN EL TXTBUSCAR
        private void Buscar()
        {
            String Buscar = txtBuscar.Text;
            DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
            DataTable Resultado = new DataTable();
            Resultado = Operacion.Consultar("SELECT  ci.IDcita, ci.Correlativo_Cita as Correlativo, " +
                                            "concat(c.TipoDoc, '-', c.Documento) as 'Documento de Cliente', " +
                                            "ci.Fecha, ci.Hora_Inicio, ci.Hora_Fin, ci.Motivo, ci.Estado, " +
                                            "ci.IDMascota, m.Nombre as Mascota, m.Genero, r.Raza, c.IDCliente, " +
                                            "concat(c.Nombres, ' ', c.Apellidos) as Nombre " +
                                            "FROM citas ci " +
                                            "INNER JOIN mascotas m " +
                                            "ON m.IDMascota = ci.IDMascota " +
                                            "INNER JOIN Razas r " +
                                            "ON m.IDRaza = r.IDraza " +
                                            "INNER JOIN clientes c " +
                                            "ON c.IDCliente = m.IDCliente " +
                                            "WHERE ci.Correlativo_Cita LIKE '%" + Buscar + "%' OR c.Documento LIKE '%" + Buscar + "%' ;");
            dgvCitasDelDia.DataSource = Resultado;

            dgvCitasDelDia.Columns["IDCita"].Visible = false;
            dgvCitasDelDia.Columns["IDMascota"].Visible = false;
            dgvCitasDelDia.Columns["IDCliente"].Visible = false;
            dgvCitasDelDia.Columns["Mascota"].Visible = false;
            dgvCitasDelDia.Columns["Genero"].Visible = false;
            dgvCitasDelDia.Columns["Raza"].Visible = false;
            dgvCitasDelDia.Columns["Nombre"].Visible = false;
        }
        #endregion


        #region EVENTOS DEL FORMULARIO

        //CUANDO SE INICIA EL FORMULARIO INICIAL
        private void AgendaCitas_Load(object sender, EventArgs e)
        {
            cargarTipoCita();
            panelCita.Visible = false;
            cargarCitas();
        }

        //VALIDA LAS CITAS QUE SE VAN A MOSTAR EN EL DGV SEGUN EL ESTADO
        private void cbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            if (((Opcion)cbEstado.SelectedItem).Descripcion == "EN PROCESO")
            {
                _DATOS.DataSource = DataManager.DBConsultas.getCitasProcesadas();
                dgvCitasDelDia.DataSource = _DATOS;
                dgvCitasDelDia.Columns["IDCita"].Visible = false;
                dgvCitasDelDia.Columns["IDMascota"].Visible = false;
                dgvCitasDelDia.Columns["IDCliente"].Visible = false;
                dgvCitasDelDia.Columns["Mascota"].Visible = false;
                dgvCitasDelDia.Columns["Genero"].Visible = false;
                dgvCitasDelDia.Columns["Raza"].Visible = false;
                dgvCitasDelDia.Columns["Nombre"].Visible = false;
            }
            if (((Opcion)cbEstado.SelectedItem).Descripcion == "AGENDADA")
            {
                _DATOS.DataSource = DataManager.DBConsultas.getCitasAgendadas();
                dgvCitasDelDia.DataSource = _DATOS;
                dgvCitasDelDia.Columns["IDCita"].Visible = false;
                dgvCitasDelDia.Columns["IDMascota"].Visible = false;
                dgvCitasDelDia.Columns["IDCliente"].Visible = false;
                dgvCitasDelDia.Columns["Mascota"].Visible = false;
                dgvCitasDelDia.Columns["Genero"].Visible = false;
                dgvCitasDelDia.Columns["Raza"].Visible = false;
                dgvCitasDelDia.Columns["Nombre"].Visible = false;
            }

            if (((Opcion)cbEstado.SelectedItem).Descripcion == "CANCELADA")
            {
                _DATOS.DataSource = DataManager.DBConsultas.getCitasCanceladas();
                dgvCitasDelDia.DataSource = _DATOS;
                dgvCitasDelDia.Columns["IDCita"].Visible = false;
                dgvCitasDelDia.Columns["IDMascota"].Visible = false;
                dgvCitasDelDia.Columns["IDCliente"].Visible = false;
                dgvCitasDelDia.Columns["Mascota"].Visible = false;
                dgvCitasDelDia.Columns["Genero"].Visible = false;
                dgvCitasDelDia.Columns["Raza"].Visible = false;
                dgvCitasDelDia.Columns["Nombre"].Visible = false;
            }

            if (((Opcion)cbEstado.SelectedItem).Descripcion == "FINALIZADA")
            {
                _DATOS.DataSource = DataManager.DBConsultas.getCitasFinalizadas();
                dgvCitasDelDia.DataSource = _DATOS;
                dgvCitasDelDia.Columns["IDCita"].Visible = false;
                dgvCitasDelDia.Columns["IDMascota"].Visible = false;
                dgvCitasDelDia.Columns["IDCliente"].Visible = false;
                dgvCitasDelDia.Columns["Mascota"].Visible = false;
                dgvCitasDelDia.Columns["Genero"].Visible = false;
                dgvCitasDelDia.Columns["Raza"].Visible = false;
                dgvCitasDelDia.Columns["Nombre"].Visible = false;
            }

        }

        //PARA BUSCAR LA CITA 
        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            cbEstado.SelectedIndex = 0;
            if (txtBuscar.Text == "")
            {
                cargarCitas();
            }
            else
            {
                Buscar();
            }
        }
        #endregion


        #region EVENTOS DEL PANEL
        //CUANDO SE DA DOBBLE CLICK EN UN REGISTRO
        private void dgvCitasDelDia_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            panelCita.Visible = true;

        }

        //CUANDO SE DA CLICK EN EL BOTON DE PROCESAR
        private void btnEmpezar_Click(object sender, EventArgs e)
        {
            //creacion del objeto entidad
            CLS.Agenda a = new CLS.Agenda();
            if (MessageBox.Show("¿Desea comenzar la CITA SELECCIONADA?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                panelCita.Visible = false;
                a.IDCita = dgvCitasDelDia.CurrentRow.Cells["IDCita"].Value.ToString().ToUpper();
                a.ProcesarCita();
                cargarCitas();
            }
        }

        //CUANDO SE DA CLICK EN EL BOTON DE FINALIZAR
        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            //creacion del objeto entidad
            CLS.Agenda a = new CLS.Agenda();
            if (MessageBox.Show("¿La CITA SELECCIONADDA ya se finalizó?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                panelCita.Visible = false;
                a.IDCita = dgvCitasDelDia.CurrentRow.Cells["IDCita"].Value.ToString().ToUpper();
                a.FinalizarCita();
                cargarCitas();
            }
        }

        //CUANDO SE DA CLICK EN EL BOTON DE CANCELAR CITA
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //creacion del objeto entidad
            CLS.Agenda a = new CLS.Agenda();
            if (MessageBox.Show("¿Desea cancelar la CITA SELECCIONADA?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                panelCita.Visible = false;
                a.IDCita = dgvCitasDelDia.CurrentRow.Cells["IDCita"].Value.ToString().ToUpper();
                a.CancelarCita();
                cargarCitas();
            }
        }

        //PARA CERRAR EL PANEL 
        private void btncerrar_Click(object sender, EventArgs e)
        {
            panelCita.Visible = false;
        }
        #endregion



        //SE OCUPARA DESPUES
        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            //DateTime fechaSeleccionada = dtpFecha.Value.Date;
            
            //_DATOS.DataSource = DataManager.DBConsultas.getCitasPorFecha(Convert.ToString(fechaSeleccionada));
            //dgvCitasDelDia.DataSource = _DATOS;
            //dgvCitasDelDia.Columns["IDCita"].Visible = false;
            //dgvCitasDelDia.Columns["IDMascota"].Visible = false;
            //dgvCitasDelDia.Columns["IDCliente"].Visible = false;
            //dgvCitasDelDia.Columns["Mascota"].Visible = false;
            //dgvCitasDelDia.Columns["Genero"].Visible = false;
            //dgvCitasDelDia.Columns["Raza"].Visible = false;
            //dgvCitasDelDia.Columns["Nombre"].Visible = false;
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            Reportes.GUI.frmCitas reporte = new Reportes.GUI.frmCitas();
            reporte.ShowDialog();
        }
    }
}
