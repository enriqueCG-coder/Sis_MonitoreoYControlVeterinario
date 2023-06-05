using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_CLIENTE_MASCOTA.GUI.Cliente_Mascota
{
    public partial class frmInfoMascota : Form
    {
        public frmInfoMascota()
        {
            InitializeComponent();
        }

        BindingSource _DATOS = new BindingSource();

        #region VARIABLES DE LA MASCOTA
        public string IDMascota { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public string Raza { get; set; }
        public string Especie { get; set; }
        public string Color { get; set; }
        public string Rasgo { get; set; }
        public string FechaNac { get; set; }
        #endregion


        #region METODOS
        //CONTADOR Y OBTENCION DE REGISTROS EN EL DATAGRIDVIEW
        private void cargarHistorial()
        {
            try
            {
                dgvHistorial.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                _DATOS.DataSource = DataManager.DBConsultas.getHistPorIDMascota(IDMascota);
                dgvHistorial.DataSource = _DATOS;
                lblRegistros.Text = dgvHistorial.Rows.Count.ToString() + " Registros Encontrados";
            }
            catch (Exception)
            {
                //NO DEVUELVE HACE NADA 
            }
        }

        //CARGA LOS DATOS DE LA MASCOTA 
        public void cargarDatosMascota()
        {
            txtIDMascota.Text = IDMascota;
            txtNombre.Text = Nombre;
            txtSexo.Text = Genero;
            txtRaza.Text = Raza;
            txtEspecie.Text = Especie;
            txtColor.Text = Color;
            txtRasgo.Text = Rasgo;
            txtFechaNac.Text = FechaNac;
        }

        //LIMIPIA LOS CONTROLES QUE CONTIENE DATOS
        private void limpiarDetalle()
        {
            dtFechaHist.Value = DateTime.Now;
            txtIDHistoria.Clear();
            txtDiagnostico.Clear();
            txtPeso.Clear();
        }
        #endregion


        #region EVENTOS
        //CUANDO SE INICIA EL FORMULARIO
        private void frmInfoMascota_Load(object sender, EventArgs e)
        {
            gbDetalleHistorial.Visible = false;
            cargarDatosMascota();
            cargarHistorial();
        }

        //CUANDO SE DA CLICK EN EL BOTON AGREGAR
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            gbDetalleHistorial.Visible = true;
        }

        //CUANDO SE DA CLICK EN EL BOTON CANCELAR 
        private void btnCancelHist_Click(object sender, EventArgs e)
        {
            limpiarDetalle();
            gbDetalleHistorial.Visible = false;
        }

        #endregion

        //CUANDO SE DA CLICK EN EL BOTON EDITAR 
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente desea EDITAR el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                
                DataGridView dgv = dgvHistorial;
                txtIDHistoria.Text = dgv.CurrentRow.Cells["IDHistorial"].Value.ToString();
                dtFechaHist.Value = Convert.ToDateTime(dgv.CurrentRow.Cells["Fecha"].Value.ToString());
                txtDiagnostico.Text = dgv.CurrentRow.Cells["Diagnostico"].Value.ToString();
                txtPeso.Text = dgv.CurrentRow.Cells["Peso"].Value.ToString();
                gbDetalleHistorial.Visible = true;
            }
        }

        //CUANDO SE DA CLICK EN EL BOTON ELIMINAR
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente desea ELIMINAR el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CLS.HistorialClinico hc = new CLS.HistorialClinico();
                hc.IDHistorial = dgvHistorial.CurrentRow.Cells["IDHistorial"].Value.ToString().ToUpper();
                //Realizar la operacion de Eliminar
                if (hc.Eliminar())
                {
                    MessageBox.Show("¡Registro eliminado correctamente!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargarHistorial();
                }
                else
                {
                    MessageBox.Show("¡El registro no fue eliminado!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        //CUANDO SE DA CLICK EN EL BOTON GUARDAR
        private void btnGuardarHist_Click(object sender, EventArgs e)
        {
            //creacion del objeto entidad
            CLS.HistorialClinico hc = new CLS.HistorialClinico();
            hc.IDMascota = txtIDMascota.Text;
            hc.Fecha = Convert.ToString(dtFechaHist.Value.ToString("dd/MM/yyyy"));
            hc.Diagnostico = txtDiagnostico.Text;
            hc.Peso = txtPeso.Text;
            hc.IDHistorial = txtIDHistoria.Text;

            //validar que accion se va a realizar
            if (txtIDHistoria.TextLength > 0)
            {
                //actualizar
                if (hc.Actualizar())
                {
                    MessageBox.Show("¡Registro actualizado correctamente!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    gbDetalleHistorial.Visible = false;
                    cargarHistorial();
                    limpiarDetalle();
                }
                else
                {
                    MessageBox.Show("¡El registro no fue actualizado!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                //insertar
                if (hc.Insertar())
                {
                    MessageBox.Show("¡Registro insertado correctamente!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargarHistorial();
                    limpiarDetalle();
                    gbDetalleHistorial.Visible = false;
                }
                else
                {
                    MessageBox.Show("¡El registro no fue insertado!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void txtPeso_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si la tecla presionada es una letra, espacio en blanco o tilde
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                // Cancelar el evento para evitar que se ingrese el carácter no deseado
                e.Handled = true;
            }
        }

        private void txtPeso_TextChanged(object sender, EventArgs e)
        {
            int maxLength = 3; // Máximo número de caracteres permitidos

            if (txtPeso.Text.Length > maxLength)
            {
                // Mostrar un mensaje de error
                MessageBox.Show("Se ha excedido el límite máximo de caracteres.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Truncar el texto ingresado al límite máximo
                txtPeso.Text = txtPeso.Text.Substring(0, maxLength);
                txtPeso.SelectionStart = maxLength; // Establecer el cursor al final del texto válido
            }
        }

        private void txtDiagnostico_TextChanged(object sender, EventArgs e)
        {
            int maxLength = 100; // Máximo número de caracteres permitidos

            if (txtDiagnostico.Text.Length > maxLength)
            {
                // Mostrar un mensaje de error
                MessageBox.Show("Se ha excedido el límite máximo de caracteres.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Truncar el texto ingresado al límite máximo
                txtDiagnostico.Text = txtDiagnostico.Text.Substring(0, maxLength);
                txtDiagnostico.SelectionStart = maxLength; // Establecer el cursor al final del texto válido
            }
        }
    }
}
