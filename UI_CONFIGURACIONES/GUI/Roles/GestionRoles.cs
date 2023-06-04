using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_CONFIGURACIONES.GUI.Roles
{
    public partial class GestionRoles : Form
    {
        public GestionRoles()
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
                dgvRoles.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                _DATOS.DataSource = DataManager.DBConsultas.ROLES();
                dgvRoles.DataSource = _DATOS;
                dgvRoles.Columns["IDRol"].Visible = false;
                lblRegistros.Text = dgvRoles.Rows.Count.ToString() + " Registros Encontrados";
            }
            catch (Exception)
            {

            }
        }

        //PARA BUSCAR EN EL TEXTBOX DE BUSCAR
        private void Buscar()
        {
            String Buscar = txtBuscar.Text;
            DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
            DataTable Resultado = new DataTable();
            Resultado = Operacion.Consultar("SELECT * FROM Roles " +
                                            "WHERE Rol LIKE '%" + Buscar + "%';");
            dgvRoles.DataSource = Resultado;

            dgvRoles.Columns["IDRol"].Visible = false;
        }

        //PARA LIMPIAR LOS DATOS DEL PANEL DESPUES DE GUARDAR O MODIFICAR
        private void limpiar()
        {
            txtID.Clear();
            txtRol.Clear();
            panelRoles.Visible = false;
        }
        #endregion

        #region EVENTOS DE FORMULARIO ROLES
        //CUANDO SE CARGA EL FORMULARIO DE ROLES 
        private void GestionRoles_Load(object sender, EventArgs e)
        {
            panelRoles.Visible = false;
            CargarDatos();
        }

        //EVENTO PARA EL BUSCADOR DE ROLES
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

        //AL DAR CLIC EN EL BOTON DE AGREGAR REGISTRO
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            panelRoles.Visible = true;
        }

        //AL DAR CLIC EN EL BOTON DE EDITAR 
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente desea EDITAR el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                txtID.Text = dgvRoles.CurrentRow.Cells["IDRol"].Value.ToString();
                txtRol.Text = dgvRoles.CurrentRow.Cells["Rol"].Value.ToString();
                panelRoles.Visible = true;

            }
        }

        //AL DAR CLIC EN EL BOTON ELIMINAR
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente desea ELIMINAR el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CLS.Roles oRoles = new CLS.Roles();
                oRoles.IDRol = dgvRoles.CurrentRow.Cells["IDRol"].Value.ToString().ToUpper(); ;
                //Realizar la operacion de Eliminar
                if (oRoles.Eliminar())
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
        #endregion

        #region EVENTOS PANEL ROLES
        //BOTON PARA GUARDAR o EDITAR EL REGISTRO DE ROL
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Creacion del objeto entidad
            CLS.Roles oRoles = new CLS.Roles();
            //Sincronizar la entidad con la interfaz
            oRoles.Rol = txtRol.Text;
            oRoles.IDRol = txtID.Text;
            //Identificar la accion a realizar
            if (txtID.TextLength > 0)
            {
                //Realizar la operacion de actualizar
                if (oRoles.Actualizar())
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
                if (oRoles.Insertar())
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

        //PARA CERRAR EL PANEL DE ROLES
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        #endregion

    }
}
