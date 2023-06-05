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
    public partial class frmMunicipios : Form
    {
        public frmMunicipios()
        {
            InitializeComponent();
        }

        BindingSource _DATOS = new BindingSource();

        private void limpiar()
        {
            cbDepto.SelectedIndex = 0;
            txtMunicipio.Clear();
            txtBuscar.Clear();
            panelMunicipios.Visible = false;
        }

        //PARA BUSCAR EN EL TXTBUSCAR
        private void Buscar()
        {
            String Buscar = txtBuscar.Text;
            DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
            DataTable Resultado = new DataTable();
            Resultado = Operacion.Consultar("SELECT m.Id, m.Nombre,m.IdDepartamento, d.Nombre as Departamento FROM Municipio m INNER JOIN Departamento d on m.IdDepartamento = d.Id " +
                                            "WHERE m.Nombre LIKE '%" + Buscar + "%' OR d.Nombre LIKE '%" + Buscar + "%' order by d.Nombre asc;");
            dgvMunicipios.DataSource = Resultado;

            dgvMunicipios.Columns["Id"].Visible = false;
            dgvMunicipios.Columns["IdDepartamento"].Visible = false;
        }
        //CARGA LOS DEPARTAMENTOS EN EL COMBOBOX
        public void obtenerDeptos()
        {
            DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
            DataTable dt = new DataTable();
            dt = Operacion.Consultar("Select Id, Nombre from Departamento;");

            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "Seleccione";
            dt.Rows.InsertAt(dr, 0);
            cbDepto.DataSource = dt;
            cbDepto.DisplayMember = "Nombre";
            cbDepto.ValueMember = "Id";

        }

        //CONTADOR DE REGISTROS EN EL DATAGRIDVIEW
        public void CargarDatos()
        {
            dgvMunicipios.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            _DATOS.DataSource = DataManager.DBConsultas.getMunicipios();
            dgvMunicipios.DataSource = _DATOS;

            lblRegistros.Text = dgvMunicipios.Rows.Count.ToString() + " Registros Encontrados";
            dgvMunicipios.Columns["Id"].Visible = false;
            dgvMunicipios.Columns["IdDepartamento"].Visible = false;
        }


        #region EVENTOS DEL FORMULARIO
        private void frmMunicipios_Load(object sender, EventArgs e)
        {
            panelMunicipios.Visible = false;
            CargarDatos();
            obtenerDeptos();
        }

        #endregion

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            panelMunicipios.Visible = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente desea EDITAR el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                txtID.Text = dgvMunicipios.CurrentRow.Cells["Id"].Value.ToString();
                cbDepto.SelectedValue = dgvMunicipios.CurrentRow.Cells["IdDepartamento"].Value.ToString();
                txtMunicipio.Text = dgvMunicipios.CurrentRow.Cells["Nombre"].Value.ToString();
                panelMunicipios.Visible = true;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente desea ELIMINAR el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CLS.Municipios m = new CLS.Municipios();
                m.ID = dgvMunicipios.CurrentRow.Cells["Id"].Value.ToString().ToUpper(); ;
                //Realizar la operacion de Eliminar
                if (m.Eliminar())
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
            CLS.Municipios m = new CLS.Municipios();
            //Sincronizar la entidad con la interfaz
            m.Depto = Convert.ToInt32(cbDepto.SelectedValue);
            m.Nombre = txtMunicipio.Text;
            m.ID = txtID.Text;

            if (!string.IsNullOrEmpty(m.Nombre) && m.Depto > 0)
            {
                //Identificar la accion a realizar
                if (txtID.TextLength > 0)
                {
                    //Realizar la operacion de actualizar
                    if (m.Actualizar())
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
                    if (m.Insertar())
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
                MessageBox.Show("Complete los campos vacío", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
