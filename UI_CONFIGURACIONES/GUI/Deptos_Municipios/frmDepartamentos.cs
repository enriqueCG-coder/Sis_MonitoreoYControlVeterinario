using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_CONFIGURACIONES.GUI.Deptos_Municipios
{
    public partial class frmDepartamentos : Form
    {
        public frmDepartamentos()
        {
            InitializeComponent();
        }
        BindingSource _DATOS = new BindingSource();

        //CONTADOR DE REGISTROS EN EL DATAGRIDVIEW
        public void CargarDatos()
        {
            dgvDepartamentos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            _DATOS.DataSource = DataManager.DBConsultas.getDeptos();
            dgvDepartamentos.DataSource = _DATOS;

            lblRegistros.Text = dgvDepartamentos.Rows.Count.ToString() + " Registros Encontrados";
            dgvDepartamentos.Columns["Id"].Visible = false;
        }

        private void limpiar()
        {
            panelDeptos.Visible = false;
            txtID.Clear();
            txtDepartamento.Clear();
        }

        //PARA BUSCAR EN EL TXTBUSCAR
        private void Buscar()
        {
            String Buscar = txtBuscar.Text;
            DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
            DataTable Resultado = new DataTable();
            Resultado = Operacion.Consultar("SELECT Id, Nombre FROM Departamento " +
                                            "WHERE Nombre LIKE '%" + Buscar + "%' order by Nombre asc;");
            dgvDepartamentos.DataSource = Resultado;

            dgvDepartamentos.Columns["Id"].Visible = false;
        }

        #region EVENTOS DEL FORMULARIO
        private void frmDepartamentos_Load(object sender, EventArgs e)
        {
            CargarDatos();
            panelDeptos.Visible = false;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            panelDeptos.Visible = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente desea EDITAR el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                txtID.Text = dgvDepartamentos.CurrentRow.Cells["Id"].Value.ToString();
                txtDepartamento.Text = dgvDepartamentos.CurrentRow.Cells["Nombre"].Value.ToString();
                panelDeptos.Visible = true;

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente desea ELIMINAR el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CLS.Departamento d = new CLS.Departamento();
                d.ID = dgvDepartamentos.CurrentRow.Cells["Id"].Value.ToString().ToUpper(); ;
                //Realizar la operacion de Eliminar
                if (d.Eliminar())
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Creacion del objeto entidad
            CLS.Departamento d = new CLS.Departamento();
            //Sincronizar la entidad con la interfaz

            d.Depto = txtDepartamento.Text;
            d.ID = txtID.Text;

            if (!string.IsNullOrEmpty(d.Depto))
            {
                //Identificar la accion a realizar
                if (txtID.TextLength > 0)
                {
                    //Realizar la operacion de actualizar
                    if (d.Actualizar())
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
                    if (d.Insertar())
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
            else
            {
                MessageBox.Show("Complete el campo vacío", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion


    }
}
