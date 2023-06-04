using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_CONFIGURACIONES.GUI.Permisos
{
    public partial class GestionPermisos : Form
    {
        public GestionPermisos()
        {
            InitializeComponent();
        }

        #region METODOS
        //obtiene todos los permisos segun el id de rol
        private void CargarPermisos()
        {
            DataTable Permisos = new DataTable();
            string pIDRol = cbRol.SelectedValue.ToString();
            try
            {
                Permisos = DataManager.DBConsultas.PERMISOS(pIDRol);
                dgvPermisos.AutoGenerateColumns = false;
                dgvPermisos.DataSource = Permisos;

            }
            catch (Exception)
            {

            }
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

        #endregion

        #region EVENTOS
        //cuando se carga el formulario 
        private void GestionPermisos_Load(object sender, EventArgs e)
        {
            cargarRoles();
        }

        #endregion

        //despues de seleccionar un rol del cbRol
        private void cbRol_SelectedValueChanged(object sender, EventArgs e)
        {
            CargarPermisos();
        }

        //al dar clic en el checkbox
        private void dgvPermisos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    String ValorActual;
                    ValorActual = dgvPermisos.CurrentRow.Cells["Asignado"].Value.ToString();
                    if (ValorActual == "0")
                    {
                        CLS.Permisos oPermiso = new CLS.Permisos();
                        oPermiso.IDOpcion = dgvPermisos.CurrentRow.Cells["IDOpcion"].Value.ToString();
                        oPermiso.IDRol = cbRol.SelectedValue.ToString();
                        if (oPermiso.Insertar())
                        {
                            CargarPermisos();
                        }
                    }
                    else
                    {
                        CLS.Permisos oPermiso = new CLS.Permisos();
                        oPermiso.IDPermiso = dgvPermisos.CurrentRow.Cells["IDPermiso"].Value.ToString();
                        if (oPermiso.Eliminar())
                        {
                            CargarPermisos();
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
