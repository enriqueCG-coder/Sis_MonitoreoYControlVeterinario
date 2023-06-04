using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_CLIENTE_MASCOTA.GUI.Raza_Especie
{
    public partial class frmRaza_Especie : Form
    {
        public frmRaza_Especie()
        {
            InitializeComponent();
        }

        #region METODOS 
        //CONTADOR DE REGISTROS EN EL DATAGRIDVIEW
        public void CargarDatos()
        {
            dgvRaza.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
            DataTable Resultado = new DataTable();
            Resultado = Operacion.Consultar("SELECT r.IDRaza, r.Raza, e.IDEspecie, e.Especie " +
                                            "FROM Razas r INNER JOIN Especies e ON r.IDEspecie = e.IDEspecie;");
            dgvRaza.DataSource = Resultado;

            lblRegistros.Text = dgvRaza.Rows.Count.ToString() + " Registros Encontrados";
            dgvRaza.Columns["IDRaza"].Visible = false;
            dgvRaza.Columns["IDEspecie"].Visible = false;
        }

        //CARGA LAS ESPECIES EN EL COMBOBOX
        public void obtenerEspecies()
        {
            DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
            DataTable dt = new DataTable();
            dt = Operacion.Consultar("Select IDEspecie, Especie from Especies;");

            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "Seleccione";
            dt.Rows.InsertAt(dr, 0);
            cbEspecie.DataSource = dt;
            cbEspecie.DisplayMember = "Especie";
            cbEspecie.ValueMember = "IDEspecie";

        }

        //PARA LIMPIAR LOS DATOS DEL PANEL DESPUES DE GUARDAR O MODIFICAR
        private void limpiar()
        {
            txtID.Clear();
            cbEspecie.SelectedValue = 0;
            txtRaza.Clear();
            panelRazas.Visible = false;
        }

        //PARA BUSCAR EN EL TXTBUSCAR
        private void Buscar()
        {
            String Buscar = txtBuscar.Text;
            DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
            DataTable Resultado = new DataTable();
            Resultado = Operacion.Consultar("SELECT r.IDRaza, r.Raza, r.IDEspecie, e.Especie FROM Razas AS r INER JOIN Especies as E ON r.IDEspecie = e.IDEspecie " +
                                            "WHERE r.Raza LIKE '%" + Buscar + "%' OR e.Especie LIKE '%" + Buscar + "%' ;");
            dgvRaza.DataSource = Resultado;

            dgvRaza.Columns["IDRaza"].Visible = false;
            dgvRaza.Columns["IDEspecie"].Visible = false;
        }
        #endregion


        #region EVENTOS FORMULARIO
        //CUANDO SE ABRE EL FORMULARIO 
        private void frmRaza_Especie_Load(object sender, EventArgs e)
        {
            panelRazas.Visible = false;
            CargarDatos();
            obtenerEspecies();
        }

        //CUANDO SE DA CLICK EN EL BOTON AGREGAR
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            panelRazas.Visible = true;
        }

        //CUANDO SE DA CLICK EN EL BOTON DE EDITAR
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente desea EDITAR el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                txtID.Text = dgvRaza.CurrentRow.Cells["IDRaza"].Value.ToString();
                cbEspecie.SelectedValue = dgvRaza.CurrentRow.Cells["IDEspecie"].Value.ToString();
                txtRaza.Text = dgvRaza.CurrentRow.Cells["Raza"].Value.ToString();
                panelRazas.Visible = true;

            }
        }

        //CUANDO SE DA CLICK EN EL BOTON ELIMINAR
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente desea ELIMINAR el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CLS.Razas oRazas = new CLS.Razas();
                oRazas.IDRaza = dgvRaza.CurrentRow.Cells["IDRaza"].Value.ToString().ToUpper(); ;
                //Realizar la operacion de Eliminar
                if (oRazas.Eliminar())
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

        #region EVENTOS PANEL RAZA_ESPECIE
        //CUANDO SE DA CLICK EN EL BOTON GUARDAR
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Creacion del objeto entidad
            CLS.Razas oRazas = new CLS.Razas();
            //Sincronizar la entidad con la interfaz
            oRazas.IDEspecie = Convert.ToInt32(cbEspecie.SelectedValue);
            oRazas.Raza = txtRaza.Text;
            oRazas.IDRaza = txtID.Text;
            //Identificar la accion a realizar
            if (txtID.TextLength > 0)
            {
                //Realizar la operacion de actualizar
                if (oRazas.Actualizar())
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
                if (oRazas.Insertar())
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

        //CUANDO SE DA CLICK EN EL BOTON CANCELAR
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
        #endregion
    }
}
