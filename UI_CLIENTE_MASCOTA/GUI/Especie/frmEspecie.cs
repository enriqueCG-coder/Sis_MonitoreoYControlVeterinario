using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_CLIENTE_MASCOTA.GUI.Especie
{
    public partial class frmEspecie : Form
    {
        public frmEspecie()
        {
            InitializeComponent();
        }
        BindingSource _DATOS = new BindingSource();


        #region METODOS
        //CONTADOR DE REGISTROS EN EL DATAGRIDVIEW
        private void CargarDatos()
        {
            try
            {
                dgvEspecies.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                _DATOS.DataSource = DataManager.DBConsultas.ESPECIES();
                dgvEspecies.DataSource = _DATOS;
                dgvEspecies.Columns["IDEspecie"].Visible = false;
                lblRegistros.Text = dgvEspecies.Rows.Count.ToString() + " Registros Encontrados";
            }
            catch (Exception)
            {

            }
        }

        //PARA BUSCAR EN EL TXTBUSCAR
        private void Buscar()
        {
            String Buscar = txtBuscar.Text;
            DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
            DataTable Resultado = new DataTable();
            Resultado = Operacion.Consultar("SELECT * FROM Especies " +
                                            "WHERE Especie LIKE '%" + Buscar + "%';");
            dgvEspecies.DataSource = Resultado;

            dgvEspecies.Columns["IDEspecie"].Visible = false;
        }

        //PARA LIMPIAR LOS DATOS DEL PANEL DESPUES DE GUARDAR O MODIFICAR
        private void limpiar()
        {
            txtID.Clear();
            txtEspecie.Clear();
            panelEspecies.Visible = false;
        }
        #endregion

        #region EVENTOS DE FORMULARIO ESPECIES
        //CUANDO SE CARGA EL FOMRULARIO DE ESPECIES
        private void frmEspecie_Load(object sender, EventArgs e)
        {
            panelEspecies.Visible = false;
            CargarDatos();
        }

        //CUANDO SE DA EN EL BOTON DE NUEVO REGISTRO
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            panelEspecies.Visible = true;
        }

        //CUANDO SE DA EN EL BOTON DE EDITAR
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente desea EDITAR el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                txtID.Text = dgvEspecies.CurrentRow.Cells["IDEspecie"].Value.ToString();
                txtEspecie.Text = dgvEspecies.CurrentRow.Cells["Especies"].Value.ToString();
                panelEspecies.Visible = true;

            }
        }

        //CUANDO SE DA EN EL BOTON DE ELIMINAR
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente desea ELIMINAR el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CLS.Especies oEspecie = new CLS.Especies();
                oEspecie.IDEspecie = dgvEspecies.CurrentRow.Cells["IDEspecie"].Value.ToString().ToUpper(); ;
                //Realizar la operacion de Eliminar
                if (oEspecie.Eliminar())
                {
                    MessageBox.Show("¡Registro eliminado correctamente!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos();
                }
                else
                {
                    MessageBox.Show("¡El registro no fue eliminado!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        //PARA BUSCAR CUANDO SE VA DIGITANDO CUALQUIER LETRA
        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                CargarDatos();
            }
            else
            {
                Buscar();
            }
        }
        #endregion


        #region EVENTOS DE PANEL
        //CUANDO SE DA EN EL BOTON DE CANCELAR DEL PANEL
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        //CUANDO SE DA EN EL BOTON DE GUARDAR DEL PANEL 
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Creacion del objeto entidad
            CLS.Especies oEspecie = new CLS.Especies();
            //Sincronizar la entidad con la interfaz
            oEspecie.Especie = txtEspecie.Text;
            oEspecie.IDEspecie = txtID.Text;
            //Identificar la accion a realizar
            if (txtID.TextLength > 0)
            {
                //Realizar la operacion de actualizar
                if (oEspecie.Actualizar())
                {
                    MessageBox.Show("¡Registro actualizado correctamente!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos();
                    limpiar();
                }
                else
                {
                    MessageBox.Show("¡El registro no fue actualizado!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                //Realizar la operacion de insertar
                if (oEspecie.Insertar())
                {
                    MessageBox.Show("¡Registro insertado correctamente!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos();
                    limpiar();
                }
                else
                {
                    MessageBox.Show("¡El registro no fue insertado!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        #endregion

    }
}
