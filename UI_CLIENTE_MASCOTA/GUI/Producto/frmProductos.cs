using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_CLIENTE_MASCOTA.GUI.Producto
{
    public partial class frmProductos : Form
    {
        public frmProductos()
        {
            InitializeComponent();
        }

        BindingSource _DATOS = new BindingSource();

        #region METODOS
        //CONTADOR DE REGISTROS EN EL DATAGRIDVIEW
        private void cargarProductos()
        {
            try
            {
                dgvProductos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                _DATOS.DataSource = DataManager.DBConsultas.getProductos();
                dgvProductos.DataSource = _DATOS;
                dgvProductos.Columns["Id_Producto"].Visible = false;
                lblRegistros.Text = dgvProductos.Rows.Count.ToString() + " Registros Encontrados";
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
            Resultado = Operacion.Consultar("SELECT * FROM Productos " +
                                            "WHERE Nombre LIKE '%" + Buscar + "%' OR TipoProducto LIKE '%" + Buscar + "%' OR Marca  LIKE '%" + Buscar + "%';");
            dgvProductos.DataSource = Resultado;

            dgvProductos.Columns["Id_Producto"].Visible = false;
        }

        //PARA LIMPIAR LOS DATOS DEL PANEL DESPUES DE GUARDAR O MODIFICAR
        private void limpiar()
        {
            txtID.Clear();
            cbTipoProd.SelectedIndex = 0;
            txtNombre.Clear();
            txtMarca.Clear();
            txtDescripcion.Clear();
            txtCantidad.Clear();
            panelProducto.Visible = false;
        }

        //carga los generos
        private void cargarTipoProd()
        {
            List<string> registros = new List<string>()
            {
                "VACUNA",
                "MEDICAMENTO",
                "LIMPIEZA"
            };
            List<string> registrosModificados = new List<string>(registros);
            registrosModificados.Insert(0, "Seleccione");
            cbTipoProd.DataSource = registrosModificados;
            cbTipoProd.SelectedIndex = 0;
        }

        #endregion

        #region EVENTOS 
        //CUANDO SE CARGA EL FORMULARIO
        private void frmProductos_Load(object sender, EventArgs e)
        {
            cargarTipoProd();
            cargarProductos();
        }

        //CUANDO SE DA CLICK EN EL BOTON DE AGREGAR NUEVO REGISTRO
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            panelProducto.Visible = true;
        }

        //CUANDO SE DA CLICK EN EL BOTON EDITAR 
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente desea EDITAR el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataGridView dgv = dgvProductos;
                txtID.Text = dgv.CurrentRow.Cells["Id_Producto"].Value.ToString();
                cbTipoProd.SelectedItem = dgv.CurrentRow.Cells["TipoProducto"].Value.ToString();
                txtNombre.Text = dgv.CurrentRow.Cells["Nombre"].Value.ToString();
                txtMarca.Text = dgv.CurrentRow.Cells["Marca"].Value.ToString();
                txtDescripcion.Text = dgv.CurrentRow.Cells["Descripcion"].Value.ToString();
                txtCantidad.Text = dgv.CurrentRow.Cells["Stock"].Value.ToString();
                panelProducto.Visible = true;

            }
        }

        //CUANDO SE DA CLICK EN EL BOTON ELIMINAR
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente desea ELIMINAR el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CLS.Productos prd = new CLS.Productos();
                prd.ID_Producto = dgvProductos.CurrentRow.Cells["Id_Producto"].Value.ToString().ToUpper();
                //Realizar la operacion de Eliminar
                if (prd.Eliminar())
                {
                    MessageBox.Show("¡Registro eliminado correctamente!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargarProductos();
                }
                else
                {
                    MessageBox.Show("¡El registro no fue eliminado!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        //PARA BUSCAR REGISTROS EN LA CAJA DE TEXTO 
        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                cargarProductos();
            }
            else
            {
                Buscar();
            }
        }

        #endregion

        #region EVENTOS DEL PANEL
        //CUANDO SE DA CLICK EN EL BOTON CANCELAR DEL PANEL 
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiar();
            panelProducto.Visible = false;
        }

        //CUANDO SE DA CLICK EN EL BOTON GUARDAR DEL PANEL 
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Creacion del objeto entidad
            CLS.Productos prd = new CLS.Productos();
            //Sincronizar la entidad con la interfaz
            prd.TipoProducto = Convert.ToString(cbTipoProd.SelectedItem);
            prd.Nombre = txtNombre.Text;
            prd.Marca = txtMarca.Text;
            prd.Descripcion = txtDescripcion.Text;
            prd.Stock = Convert.ToInt32(txtCantidad.Text);
            prd.ID_Producto = txtID.Text;
            //Identificar la accion a realizar
            if (txtID.TextLength > 0)
            {
                //Realizar la operacion de actualizar
                if (prd.ActualizarProd())
                {
                    MessageBox.Show("¡Registro actualizado correctamente!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargarProductos();
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
                if (prd.Insertar())
                {
                    MessageBox.Show("¡Registro insertado correctamente!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargarProductos();
                    limpiar();
                }
                else
                {
                    MessageBox.Show("¡El registro no fue insertado!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        #endregion


        #region VALIDACIONES 

        #endregion

    }
}
