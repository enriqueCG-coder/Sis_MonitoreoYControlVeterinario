using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;

namespace UI_CLIENTE_MASCOTA.GUI.Cliente_Mascota
{
    public partial class frmCliente : Form
    {
        public frmCliente()
        {
            InitializeComponent();
        }

        Color color2 = Color.FromArgb(80, 17, 104); 
        Color color1 = Color.FromArgb(91, 87, 165);


        #region METODOS

        //CARGA LOS DATOS EN EL DATAGRIDVIEW
        public void cargarDatos()
        {
            dgvCliente.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
            DataTable Resultado = new DataTable();
            Resultado = Operacion.Consultar("SELECT C.IDCliente, C.Nombres, " +
                                            "C.Apellidos, C.FechaNac, C.Genero," +
                                            "D.Nombre as Departamento, M.Nombre as Municipio, " +
                                            "C.Direccion, C.TipoDoc, C.Documento, " +
                                            "C.Correo, C.Telefono, D.Id, C.IdMunicipio FROM Clientes as C " +
                                            "INNER JOIN Municipio as M " +
                                            "ON M.Id = C.IdMunicipio " +
                                            "INNER JOIN Departamento as D " +
                                            "ON D.Id = M.IdDepartamento;");
            dgvCliente.DataSource = Resultado;
            lblRegistros.Text = dgvCliente.Rows.Count.ToString() + " Registros Encontrados";
            dgvCliente.Columns["IDCliente"].Visible = false;
            dgvCliente.Columns["IDMunicipio"].Visible = false;
            dgvCliente.Columns["FechaNac"].Visible = false;
            dgvCliente.Columns["ID"].Visible = false;
            dgvCliente.Columns["Departamento"].Visible = false;
            dgvCliente.Columns["Municipio"].Visible = false;
            dgvCliente.Columns["Direccion"].Visible = false;

        }

        //BUSCA LOS DATOS EN EL DATGRIDVIEW
        private void Buscar()
        {
            String Buscar = "";
            Buscar = txtBuscar.Text;
            DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
            DataTable Resultado = new DataTable();
            Resultado = Operacion.Consultar("SELECT C.IDCliente, C.Nombres, C.Apellidos, C.FechaNac, C.Genero, D.Nombre as Departamento, M.Nombre as Municipio, " +
                                            "C.Direccion, C.TipoDoc, C.Documento,C.Correo, C.Telefono, D.Id, C.IdMunicipio " +
                                            "FROM Clientes as C " +
                                            "INNER JOIN Municipio as M " +
                                            "ON M.Id = C.IdMunicipio " +
                                            "INNER JOIN Departamento as D " +
                                            "ON D.Id = M.IdDepartamento " +
                                            "WHERE C.Nombres LIKE '%" + Buscar + "%' " +
                                            "OR C.Apellidos LIKE '%" + Buscar + "%' " +
                                            "OR C.Correo LIKE '%" + Buscar + "%' " +
                                            "OR C.Telefono LIKE '%" + Buscar + "%' " +
                                            "OR C.Documento LIKE '%" + Buscar + "%';");
            dgvCliente.DataSource = Resultado;

            dgvCliente.Columns["IDCliente"].Visible = false;
            dgvCliente.Columns["IDMunicipio"].Visible = false;
            dgvCliente.Columns["FechaNac"].Visible = false;
            dgvCliente.Columns["ID"].Visible = false;
            dgvCliente.Columns["Departamento"].Visible = false;
            dgvCliente.Columns["Municipio"].Visible = false;
            dgvCliente.Columns["Direccion"].Visible = false;
        }

        //carga los generos
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

        //carga los tipos de documento
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

        //PARA OBTENER TODOS LOS DEPARTAMENTOS EN EL COMBOBOX 
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

        //PARA OBTENER TODOS LOS MUNICIPIOS POR EL ID DE DEPARTAMENTO 
        public void obtenerMunicipios()
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

        //PARA RESETAR LAS ENTRADAS DE DATOS
        private void limpiar()
        {
            txtID.Clear();
            txtNombres.Clear();
            txtApellidos.Clear();
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

        #region EVENTOS DE FORMULARIO

        //SUCEDE CUANDO SE CARGA EL FORMULARIO
        private void frmCliente_Mascota_Load(object sender, EventArgs e)
        {
            cargarDatos();
            plCliente.Visible = false;
        }

        //SUCEDE CUANDO SE DA CLICK EN EL BOTON AGREGAR NUEVO REGISTRO
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            plCliente.Visible = true;
            obtenerDepartamentos();
            cargarGenero();
            cargarTipoDoc();
        }

        //SUCEDE CUANDO SE DA CLIC EN LA IMAGEN DEL REGISTRO EN EL DATAGRIDVIEW
        private void dgvCliente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == dgvCliente.Columns["Imagen"].Index && e.RowIndex >= 0)
            {
                DataGridViewRow filaSeleccionada = dgvCliente.Rows[e.RowIndex];
                string idcliente = filaSeleccionada.Cells["IDCliente"].Value.ToString();
                string nombres = filaSeleccionada.Cells["Nombres"].Value.ToString();
                string apellidos = filaSeleccionada.Cells["Apellidos"].Value.ToString();
                string genero = filaSeleccionada.Cells["Genero"].Value.ToString();
                string tipoDoc = filaSeleccionada.Cells["TipoDoc"].Value.ToString();
                string documento = filaSeleccionada.Cells["Documento"].Value.ToString();
                string telefono = filaSeleccionada.Cells["Telefono"].Value.ToString();
                string correo = filaSeleccionada.Cells["Correo"].Value.ToString();
                string departamento = filaSeleccionada.Cells["Departamento"].Value.ToString();
                string municipio = filaSeleccionada.Cells["Municipio"].Value.ToString();
                string direccion = filaSeleccionada.Cells["Direccion"].Value.ToString();
                UI_CLIENTE_MASCOTA.GUI.Cliente_Mascota.frmMascotas f = new UI_CLIENTE_MASCOTA.GUI.Cliente_Mascota.frmMascotas();
                f.IDCliente = idcliente;
                f.Nombres = nombres;
                f.Apellidos = apellidos;
                f.GeneroCliente = genero;
                f.TipoDoc = tipoDoc;
                f.Documento = documento;
                f.Telefono = telefono;
                f.Correo = correo;
                f.Departamento = departamento;
                f.Municipio = municipio;
                f.Direccion = direccion;
                f.ShowDialog();
            }
        }

        //SUCEDE CUANDO SE DA CLICK EN EL BOTON EDITAR 
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente desea EDITAR el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cargarGenero();
                cargarTipoDoc();
                obtenerDepartamentos();
                obtenerMunicipios();
                DataGridView dgv = dgvCliente;
                txtID.Text = dgv.CurrentRow.Cells["IDCliente"].Value.ToString();
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
                plCliente.Visible = true;
            }
        }

        //SUCEDE CUANDO SE DA CLICK EN EL BOTON ELIMINAR
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente desea ELIMINAR el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CLS.Clientes oCli = new CLS.Clientes();
                oCli.IDCliente = dgvCliente.CurrentRow.Cells["IDCliente"].Value.ToString().ToUpper();
                //Realizar la operacion de Eliminar
                if (oCli.Eliminar())
                {
                    MessageBox.Show("¡Registro eliminado correctamente!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargarDatos();
                }
                else
                {
                    MessageBox.Show("¡El registro no fue eliminado!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        //SIRVE PARA BUSCAR UN REGISTRO 
        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                cargarDatos();
            }
            else
            {
                Buscar();
            }
        }

        #endregion

        #region EVENTOS PANEL CLIENTES

        //SUCEDE CUANDO SE DA CLICK EN EL BOTON CANCELAR DEL PANEL
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiar();
            plCliente.Visible = false;
        }

        //SUCEDE CUANDO SE DA CLICK EN EL BOTON GUARDAR DEL PANEL 
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //creacion del objeto entidad
            CLS.Clientes cli = new CLS.Clientes();
            cli.Nombres = txtNombres.Text;
            cli.Apellidos = txtApellidos.Text;
            cli.FechaNac = Convert.ToString(dtpFechaNac.Value.ToString("dd/MM/yyyy"));
            cli.Genero = Convert.ToString(cmbGenero.SelectedItem);
            cli.IdMunicipio = Convert.ToInt32(cbMunicipio.SelectedValue);
            cli.Direccion = rtxtDireccion.Text;
            cli.TipoDoc = Convert.ToString(cmbTipoDoc.SelectedItem);
            cli.Documento = txtDocumento.Text;
            cli.Correo = txtCorreo.Text;
            cli.Telefono = txtTelefono.Text;
            cli.IDCliente = txtID.Text;

            //validar que accion se va a realizar
            if (txtID.TextLength > 0)
            {
                //actualizar
                if (cli.Actualizar())
                {
                    MessageBox.Show("¡Registro actualizado correctamente!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    plCliente.Visible = false;
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
                if (cli.Insertar())
                {
                    MessageBox.Show("¡Registro insertado correctamente!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargarDatos();
                    limpiar();
                    plCliente.Visible = false;
                }
                else
                {
                    MessageBox.Show("¡El registro no fue insertado!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        //CUANDO SE SELECCIONA UN REGISTRO DE DEPARTAMENTO 
        private void cbDepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            obtenerMunicipios();
        }

        #endregion


        

    }
}
