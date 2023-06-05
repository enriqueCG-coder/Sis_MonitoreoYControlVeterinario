using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_MANTENIMIENTOS.GUI.Usuarios
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }

        #region METODOS
        private void limpiar()
        {
            txtID.Clear();
            txtIDEmpleado.Clear();
            txtNombres.Clear();
            txtApellidos.Clear();
            txtUsuario.Clear();
            txtClave.Clear();
            cbRol.SelectedValue = 0;
        }

        //obtiene los roles y los pasa al cbRoles
        private void cargarRoles()
        {
            DataTable Roles = new DataTable();
            try
            {
                Roles = DataManager.DBConsultas.ROLES();

                DataRow dr = Roles.NewRow();
                dr[0] = 0;
                dr[1] = "Seleccione";
                Roles.Rows.InsertAt(dr, 0);

                cbRol.DataSource = Roles;
                cbRol.DisplayMember = "Rol";
                cbRol.ValueMember = "IDRol";
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar roles", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                throw;
            }
        }

        //metodo para llenar el datagridview de empleados
        public void llenarDataGridEmpleados()
        {
            dgvEmpleados.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
            DataTable Resultado = new DataTable();
            Resultado = Operacion.Consultar("SELECT E.IDEmpleado, E.Nombres, " +
                                            "E.Apellidos, E.TipoDoc, E.Documento, " +
                                            "E.Correo, E.Telefono FROM Empleados as E;");
            dgvEmpleados.DataSource = Resultado;
            lblRegistros.Text = dgvEmpleados.Rows.Count.ToString() + " Registros Encontrados";

            dgvEmpleados.Columns["IDEmpleado"].Visible = false;
        }

        //metodo para llenar el datagridview de empleados
        public void cargarDatos()
        {
            dgvUsuarios.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
            DataTable Resultado = new DataTable();
            Resultado = Operacion.Consultar(@"SELECT U.IDUsuario, U.Usuario, U.Clave, U.IDEmpleado, E.Nombres, E.Apellidos, U.IDRol, R.Rol
                                            FROM Usuarios as  U 
                                            INNER JOIN Empleados E 
                                            ON E.IDEmpleado = U.IDEmpleado
                                            INNER JOIN Roles as R
                                            ON R.IDRol = U.IDRol;");
            dgvUsuarios.DataSource = Resultado;
            lblRegistros.Text = dgvUsuarios.Rows.Count.ToString() + " Registros Encontrados";

            dgvUsuarios.Columns["IDUsuario"].Visible = false;
            dgvUsuarios.Columns["IDEmpleado"].Visible = false;
            dgvUsuarios.Columns["IDRol"].Visible = false;
            dgvUsuarios.Columns["Clave"].Visible = false;

        }

        //METODO QUE PERMITE BUSCAR LOS USUARIOS EN EL DATAGRIDVIEW DE USUARIOS
        private void BuscarUsuario()
        {
            String Buscar = "";
            Buscar = txtBuscar.Text;
            DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
            DataTable Resultado = new DataTable();
            Resultado = Operacion.Consultar("SELECT U.IDUsuario, U.Usuario, U.Clave, E.IDEmpleado, E.Nombres, E.Apellidos, U.IDRol, R.Rol " +
                                            "FROM Usuarios as  U " +
                                            "INNER JOIN Empleados E " +
                                            "ON E.IDEmpleado = U.IDEmpleado " +
                                            "INNER JOIN Roles as R " +
                                            "ON R.IDRol = U.IDRol " +
                                            "WHERE U.Usuario LIKE '%" + Buscar + "%' OR E.Nombres LIKE '%" + Buscar + "%' OR E.Apellidos LIKE '%" + Buscar + "%' OR R.Rol LIKE '%" + Buscar + "%';");
            dgvUsuarios.DataSource = Resultado;

            dgvUsuarios.Columns["IDEmpleado"].Visible = false;
            dgvUsuarios.Columns["IDRol"].Visible = false;
        }


        //para buscar el registro en la base de datos 
        private void BuscarEmp()
        {
            String Buscar = tsBuscarEmp.Text;
            DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
            DataTable Resultado = new DataTable();
            Resultado = Operacion.Consultar("SELECT E.IDEmpleado, E.Nombres, " +
                                            "E.Apellidos, E.TipoDoc, E.Documento, " +
                                            "E.Correo, E.Telefono FROM Empleados as E " +
                                            "WHERE E.Nombres LIKE '%" + Buscar + "%' OR " +
                                            "E.Apellidos LIKE '%" + Buscar + "%' OR " +
                                            "E.Documento LIKE '%" + Buscar + "%';");
            dgvEmpleados.DataSource = Resultado;

            dgvEmpleados.Columns["IDEmpleado"].Visible = false;
        }
        #endregion


        #region EVENTOS DEL FORMULARIO
        //cuando se carga el formulario 
        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            panelUsuarios.Visible = false;
            panelEmpleados.Visible = false;
            cargarDatos();
        }

        //cuando se da click en el boton de agregar usuario
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            panelUsuarios.Visible = true;
            cargarRoles();
        }

        //CUANDO SE DA CLICK EN EL BOTON ELIMINAR Y SE TIENE SELECCIONADO UN REGISTRO 
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente desea ELIMINAR el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CLS.Usuarios u = new CLS.Usuarios();
                u.IDUsuario = dgvUsuarios.CurrentRow.Cells["IDUsuario"].Value.ToString().ToUpper();
                //Realizar la operacion de Eliminar
                if (u.Eliminar())
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

        //CUANDO SE DA CLICK EN EL BOTON EDITAR Y SE TIENE SELECCIONADO UN REGISTRO
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente desea EDITAR el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cargarRoles();
                DataGridView dgv = dgvUsuarios;
                txtIDEmpleado.Text = dgv.CurrentRow.Cells["IDEmpleado"].Value.ToString();
                txtNombres.Text = dgv.CurrentRow.Cells["Nombres"].Value.ToString();
                txtApellidos.Text = dgv.CurrentRow.Cells["Apellidos"].Value.ToString();
                txtID.Text = dgv.CurrentRow.Cells["IDUsuario"].Value.ToString();
                txtUsuario.Text = dgv.CurrentRow.Cells["Usuario"].Value.ToString();
                txtClave.Text = dgv.CurrentRow.Cells["Clave"].Value.ToString();
                cbRol.SelectedValue = dgv.CurrentRow.Cells["Rol"].Value.ToString();
                panelEmpleados.Visible = true;
            }
        }


        #endregion


        #region EVENTOS DEL PANEL USUARIOS
        //cuando se da clic en el boton de buscar empleados 
        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            panelEmpleados.Visible = true;
            btnCancelar.Enabled = false;
            btnGuardar.Enabled = false;
            llenarDataGridEmpleados();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiar();
            panelUsuarios.Visible = false;
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //creacion del objeto entidad
            CLS.Usuarios u = new CLS.Usuarios();
            u.Usuario = txtUsuario.Text;
            u.Clave = txtClave.Text;
            u.IDEmpleado = txtIDEmpleado.Text;
            u.IDRol = cbRol.SelectedValue.ToString();
            u.IDUsuario = txtID.Text;

            if (!string.IsNullOrEmpty(u.Clave))
            {
                //validar que accion se va a realizar
                if (txtID.TextLength > 0)
                {
                    //actualizar
                    if (u.Actualizar())
                    {
                        MessageBox.Show("¡Registro actualizado correctamente!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        panelUsuarios.Visible = false;
                        cargarDatos();
                        limpiar();
                    }
                    else
                    {
                        MessageBox.Show("¡El registro no fue actualizado!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    //insertar
                    if (u.Insertar())
                    {
                        MessageBox.Show("¡Registro insertado correctamente!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cargarDatos();
                        limpiar();
                        panelUsuarios.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("¡El registro no fue insertado!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("complete el campo vacío", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region EVENTOS DEL PANEL EMPLEADOS
        //al dar doble clic en el datagrid de buscar empleado
        private void dgvEmpleados_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow filaSeleccionada = dgvEmpleados.Rows[e.RowIndex];
            txtIDEmpleado.Text = filaSeleccionada.Cells["IDEmpleado"].Value.ToString();
            txtNombres.Text = filaSeleccionada.Cells["Nombres"].Value.ToString();
            txtApellidos.Text = filaSeleccionada.Cells["Apellidos"].Value.ToString();
            string texto = txtApellidos.Text;
            char first = texto.FirstOrDefault();
            txtUsuario.Text = "VETUSER" + txtIDEmpleado.Text + "-" + first;
            panelEmpleados.Visible = false;
            btnCancelar.Enabled = true;
            btnGuardar.Enabled = true;
        }

        private void tsBuscarEmp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (tsBuscarEmp.Text == "")
            {
                llenarDataGridEmpleados();
            }
            else
            {
                BuscarEmp();
            }
        }

        //cuando se da click en la imagen de cerrar el panel 
        private void btncerrar_Click(object sender, EventArgs e)
        {
            panelEmpleados.Visible = false;
            btnCancelar.Enabled = true;
            btnGuardar.Enabled = true;
        }



        #endregion

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                cargarDatos();
            }
            else
            {
                BuscarUsuario();
            }
        }


    }
}
