using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_MANTENIMIENTOS.GUI
{
    public partial class frmEmpleados : Form
    {

        public frmEmpleados()
        {
            InitializeComponent();
        }

        #region METODOS FORMULARIO EMPLEADOS 
        
        //metodo para llenar el datagridview de empleados
        public void llenarDataGridEmpleados()
        {
            dgvEmpleados.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
            DataTable Resultado = new DataTable();
            Resultado = Operacion.Consultar("SELECT E.IDEmpleado, E.Nombres, " +
                                            "E.Apellidos, E.FechaNac, E.Genero," +
                                            "D.Nombre as Departamento, M.Nombre as Municipio, " +
                                            "E.Direccion, E.TipoDoc, E.Documento, " +
                                            "E.Correo, E.Telefono, D.Id, E.IdMunicipio FROM Empleados as E " +
                                            "INNER JOIN Municipio as M " +
                                            "ON M.Id = E.IdMunicipio " +
                                            "INNER JOIN Departamento as D " +
                                            "ON D.Id = M.IdDepartamento;");
            dgvEmpleados.DataSource = Resultado;
            lblRegistros.Text = dgvEmpleados.Rows.Count.ToString() + " Registros Encontrados";

            dgvEmpleados.Columns["IDEmpleado"].Visible = false;
            dgvEmpleados.Columns["Id"].Visible = false;
            dgvEmpleados.Columns["IdMunicipio"].Visible = false;
        }

        //para buscar el registro en la base de datos 
        private void Buscar()
        {
            String Buscar = txtBuscar.Text;
            DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
            DataTable Resultado = new DataTable();
            Resultado = Operacion.Consultar("SELECT E.IDEmpleado, E.Nombres, " +
                                            "E.Apellidos, E.FechaNac, E.Genero," +
                                            "D.Nombre as Departamento, M.Nombre as Municipio, " +
                                            "E.Direccion, E.TipoDoc, E.Documento, " +
                                            "E.Correo, E.Telefono, D.Id, E.IdMunicipio FROM Empleados as E " +
                                            "INNER JOIN Municipio as M " +
                                            "ON M.Id = E.IdMunicipio " +
                                            "INNER JOIN Departamento as D " +
                                            "ON D.Id = M.IdDepartamento " +
                                            "WHERE E.Nombres LIKE '%" + Buscar + "%' OR " +
                                            "E.Apellidos LIKE '%" + Buscar + "%' OR " +
                                            "E.Documento LIKE '%" + Buscar + "%';");
            dgvEmpleados.DataSource = Resultado;

            dgvEmpleados.Columns["IDEmpleado"].Visible = false;
            dgvEmpleados.Columns["Id"].Visible = false;
            dgvEmpleados.Columns["IdMunicipio"].Visible = false;
        }
        #endregion

        #region EVENTOS FORMULARIO EMPLEADOS 
        //cuando se carga el formulario de empleados
        private void frmEmpleados_Load(object sender, EventArgs e)
        {
            llenarDataGridEmpleados();
        }

        //cuando se presiona una tecla cualquiera
        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                llenarDataGridEmpleados();
            }
            else
            {
                Buscar();
            }
        }

        //cuando se da clic en el boton de agregar 
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            pnlAddEmpleados.Visible = true;
            obtenerDepartamentos();
            cargarGenero();
            cargarTipoDoc();
        }


        //cuando se da clic en el boton de editar 
        private void btnEditar_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("¿Realmente desea EDITAR el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cargarGenero();
                cargarTipoDoc();
                obtenerDepartamentos();
                obtenerMunicipios();
                DataGridView dgv = dgvEmpleados;
                txtID.Text = dgv.CurrentRow.Cells["IDEmpleado"].Value.ToString();
                txtNombres.Text = dgv.CurrentRow.Cells["Nombres"].Value.ToString();
                txtApellidos.Text = dgv.CurrentRow.Cells["Apellidos"].Value.ToString();
                dtpFechaNac.Value = Convert.ToDateTime(dgv.CurrentRow.Cells["FechaNac"].Value.ToString());
                cmbGenero.SelectedItem = dgv.CurrentRow.Cells["Genero"].Value.ToString();
                cmbTipoDoc.SelectedItem = dgv.CurrentRow.Cells["TipoDoc"].Value.ToString();
                txtDocumento.Text = dgv.CurrentRow.Cells["Documento"].Value.ToString();
                txtTelefono.Text = dgv.CurrentRow.Cells["Telefono"].Value.ToString();
                txtCorreo.Text = dgv.CurrentRow.Cells["Correo"].Value.ToString();
                cbDepto.SelectedValue = dgv.CurrentRow.Cells["Id"].Value.ToString();
                cbMunicipio.SelectedValue = dgv.CurrentRow.Cells["IdMunicipio"].Value.ToString();
                rtxtDireccion.Text = dgv.CurrentRow.Cells["Direccion"].Value.ToString();
                pnlAddEmpleados.Visible = true;
            }
        }

        //CUANDO SE DA CLICK EN EL BOTON DE ELIMINAR 
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente desea ELIMINAR el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CLS.Empleados emp = new CLS.Empleados();
                emp.IDEmpleado = dgvEmpleados.CurrentRow.Cells["IDEmpleado"].Value.ToString().ToUpper();
                //Realizar la operacion de Eliminar
                if (emp.Eliminar())
                {
                    MessageBox.Show("¡Registro eliminado correctamente!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    llenarDataGridEmpleados();
                }
                else
                {
                    MessageBox.Show("¡El registro no fue eliminado!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        #endregion


        #region METODOS DE PANEL EMPLEADOS

        private void cargarGenero()
        {
            List<string> registros = new List<string>()
            {
                "FEMENINO",
                "MASCULINO"
            };
            List<string> registrosModificados = new List<string>(registros);
            registrosModificados.Insert(0, "Seleccione");
            cmbGenero.DataSource = registrosModificados;
            cmbGenero.SelectedIndex = 0;
        }

        private void cargarTipoDoc()
        {
            List<string> registros = new List<string>()
            {
                "DUI",
                "PASAPORTE"
            };
            List<string> registrosModificados = new List<string>(registros);
            registrosModificados.Insert(0, "Seleccione");
            cmbTipoDoc.DataSource = registrosModificados;
            cmbTipoDoc.SelectedIndex = 0;
        }

        //para obtener todos los departamentos en el combobox 
        public void obtenerDepartamentos()
        {
            DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
            DataTable dt = new DataTable();
            dt = Operacion.Consultar("Select Id, Nombre from Departamento ORDER BY Nombre ASC;");

            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "Seleccione";
            dt.Rows.InsertAt(dr, 0);
            cbDepto.DataSource = dt;
            cbDepto.DisplayMember = "Nombre";
            cbDepto.ValueMember = "Id";

        }

        //para obtener todos los municipios por el id de departamento 
        private void obtenerMunicipios()
        {
            DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
            DataTable dt = new DataTable();
            dt = Operacion.Consultar("select Id as IdMunicipio, Nombre from Municipio where IdDepartamento ='" + cbDepto.SelectedValue + "' ORDER BY Nombre ASC;");

            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "Seleccione";
            dt.Rows.InsertAt(dr, 0);
            cbMunicipio.DataSource = dt;
            cbMunicipio.DisplayMember = "Nombre";
            cbMunicipio.ValueMember = "IdMunicipio";
        }

        //para resetar las entradas de datos
        private void limpiar()
        {
            txtID.Clear();
            txtNombres.Clear();
            txtApellidos.Clear();
            dtpFechaNac.Value = DateTime.Now;
            cmbGenero.SelectedItem = "Seleccione";
            cbDepto.SelectedValue = 0;
            cbMunicipio.SelectedValue = 0;
            rtxtDireccion.Clear();
            cmbTipoDoc.SelectedItem = "Seleccione";
            txtDocumento.Clear();
            txtCorreo.Clear();
            txtTelefono.Clear();
        }

        #endregion


        #region EVENTOS PANEL EMPLEADOS 
        //cuando se le da en el boton de guardar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //creacion del objeto entidad
            CLS.Empleados emp = new CLS.Empleados();
            emp.Nombres = txtNombres.Text;
            emp.Apellidos = txtApellidos.Text;
            emp.FechaNac = Convert.ToString(dtpFechaNac.Value.ToString("dd/MM/yyyy"));
            emp.Genero = Convert.ToString(cmbGenero.SelectedItem);
            emp.IdMunicipio = Convert.ToInt32(cbMunicipio.SelectedValue);
            emp.Direccion = rtxtDireccion.Text;
            emp.TipoDoc = Convert.ToString(cmbTipoDoc.SelectedItem);
            emp.Documento = txtDocumento.Text;
            emp.Correo = txtCorreo.Text;
            emp.Telefono = txtTelefono.Text;
            emp.IDEmpleado = txtID.Text;

            //validar que accion se va a realizar
            if (txtID.TextLength > 0)
            {
                //actualizar
                if (emp.Actualizar())
                {
                    MessageBox.Show("¡Registro actualizado correctamente!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pnlAddEmpleados.Visible = false;
                    llenarDataGridEmpleados();
                }
                else
                {
                    MessageBox.Show("¡El registro no fue actualizado!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                //insertar
                if (emp.Insertar())
                {
                    MessageBox.Show("¡Registro insertado correctamente!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    llenarDataGridEmpleados();
                    pnlAddEmpleados.Visible = false;
                }
                else
                {
                    MessageBox.Show("¡El registro no fue insertado!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        //cuando se le da en el boton cancelar
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            pnlAddEmpleados.Visible = false;
            limpiar();
        }

        //cuando se selecciona un registro de departamento 
        private void cbDepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            obtenerMunicipios();
        }
        #endregion


        #region METODOS DE VALIDACION
        //PERMITE VALIDAR SI UNA VOCAL LLEVA TILDE 
        private bool IsTilde(char c)
        {
            // Verificar si el carácter es una tilde (á, é, í, ó, ú, Á, É, Í, Ó, Ú)
            return Regex.IsMatch(c.ToString(), "[áéíóúÁÉÍÓÚ]");
        }
        #endregion

        #region VALIDACIONES DE TEXTBOX 
        //VALIDA SI LA PALABRA LLEVA TILDE 
        private void txtNombres_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si la tecla presionada es una letra, espacio en blanco o tilde
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != (char)Keys.Back && !IsTilde(e.KeyChar))
            {
                e.Handled = true; // Rechazar la entrada de la tecla
            }
        }

        //VALIDA LA CANTIDAD DE CARACTERES DEL TEXTBOX NOMBRES
        private void txtNombres_TextChanged(object sender, EventArgs e)
        {
            // Limitar la longitud del TextBox a 25 caracteres
            if (txtNombres.Text.Length > 25)
            {
                txtNombres.Text = txtNombres.Text.Substring(0, 25);
                txtNombres.SelectionStart = 25; // Establecer la posición del cursor al final
            }
        }

        private void txtApellidos_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtCorreo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void rtxtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {

        }



        #endregion

        
    }
}
