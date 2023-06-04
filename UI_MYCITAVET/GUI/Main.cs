using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_MYCITAVET.GUI
{
    public partial class Main : Form
    {
        SessionManager.Session oSesion = SessionManager.Session.Instancia;

        public Main()
        {
            InitializeComponent();
            personalizarDesign();

            
        }

        private void Main_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = oSesion.Usuario;
            lblRol.Text = oSesion.Rol;
        }

        //BOTON PARA CERRAR EL SISTEMA 
        private void btncerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //BOTON PARA MINIMIZAR LA PANTALLA DEL MENU PRINCIPAL
        private void btnminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        #region METODOS MENU Y SUB MENU
        //OCULTA LOS SUBMENU 
        private void personalizarDesign()
        {
            panelSubMenuMant.Visible = false;
            pnlSubMenuCliMasc.Visible = false;
            panelSubMenuConsultoria.Visible = false;
            panelSubMenuConfig.Visible = false;
        }

        //OCULTA LOS SUBMENU SI ESTA VISIBLE Y SE PRESIONA OTRO MENU 
        private void ocultarSubMenu()
        {
            if (panelSubMenuMant.Visible == true)
            {
                panelSubMenuMant.Visible = false;
            }
            if (pnlSubMenuCliMasc.Visible == true)
            {
                pnlSubMenuCliMasc.Visible = false;
            }
            if (panelSubMenuConsultoria.Visible == true)
            {
                panelSubMenuConsultoria.Visible = false;
            }
            if (panelSubMenuConfig.Visible == true)
            {
                panelSubMenuConfig.Visible = false;
            }
        }

        private void mostrarSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                ocultarSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }
        #endregion

        #region EVENTOS QUE ABREN LOS SUB MENUS
        private void btnMantenimientos_Click(object sender, EventArgs e)
        {
            mostrarSubMenu(panelSubMenuMant);
        }

        private void btnCliMasc_Click(object sender, EventArgs e)
        {
            mostrarSubMenu(pnlSubMenuCliMasc);
        }

        private void btnConfiguraciones_Click(object sender, EventArgs e)
        {
            mostrarSubMenu(panelSubMenuConfig);
        }

        private void btnConsultoria_Click(object sender, EventArgs e)
        {
            mostrarSubMenu(panelSubMenuConsultoria);
        }

        #endregion

        #region EVENTOS DE SUB MENU MANTENIMIENTOS
        //ABRE EL FOMRULARIO DE EMPLEADOS
        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            try
            {
                UI_MANTENIMIENTOS.GUI.frmEmpleados f = new UI_MANTENIMIENTOS.GUI.frmEmpleados();
                f.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //ABRE EL FORMULARIO DE USUARIOS
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            try
            {
                UI_MANTENIMIENTOS.GUI.Usuarios.frmUsuarios f = new UI_MANTENIMIENTOS.GUI.Usuarios.frmUsuarios();
                f.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region EVENTOS PARA EL SUBMENU DE CONFIGURACIONES
        //ABRE EL FORMULARIO DE PERMISOS
        private void btnPermisos_Click(object sender, EventArgs e)
        {
            try
            {
                UI_CONFIGURACIONES.GUI.Permisos.GestionPermisos f = new UI_CONFIGURACIONES.GUI.Permisos.GestionPermisos();
                f.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //ABRE EL FORMULARIO DE ROLES 
        private void btnRoles_Click(object sender, EventArgs e)
        {
            try
            {
                UI_CONFIGURACIONES.GUI.Roles.GestionRoles f = new UI_CONFIGURACIONES.GUI.Roles.GestionRoles();
                f.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //ABRE EL FORMULARIO DE CLASIFICACIONES 
        private void btnClasificaciones_Click(object sender, EventArgs e)
        {
            try
            {
                UI_CONFIGURACIONES.GUI.Clasificaciones.GestionClasificaciones f = new UI_CONFIGURACIONES.GUI.Clasificaciones.GestionClasificaciones();
                f.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //ABRE EL FORMULARIO DE OPCIONES 
        private void btnOpciones_Click(object sender, EventArgs e)
        {
            try
            {
                UI_CONFIGURACIONES.GUI.Opciones.GestionOpciones f = new UI_CONFIGURACIONES.GUI.Opciones.GestionOpciones();
                f.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region EVENTOS PARA EL SUBMENU DE CLIENTES-MASCOTAS
        //ABRE EL FORMULARIO DE CLIENTES
        private void btnClientes_Click(object sender, EventArgs e)
        {
            try
            {
                UI_CLIENTE_MASCOTA.GUI.Cliente_Mascota.frmCliente f = new UI_CLIENTE_MASCOTA.GUI.Cliente_Mascota.frmCliente();
                f.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //ABRE EL FORMULARIO DE ESPECIES 
        private void btnEspecies_Click(object sender, EventArgs e)
        {
            try
            {
                UI_CLIENTE_MASCOTA.GUI.Especie.frmEspecie f = new UI_CLIENTE_MASCOTA.GUI.Especie.frmEspecie();
                f.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //ABRE EL FORMULARIO DE RAZAS 
        private void btnRazas_Click(object sender, EventArgs e)
        {
            try
            {
                UI_CLIENTE_MASCOTA.GUI.Raza_Especie.frmRaza_Especie f = new UI_CLIENTE_MASCOTA.GUI.Raza_Especie.frmRaza_Especie();
                f.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //ABRE EL FORMULARIO DE PRODUCTOS
        private void btnProductos_Click(object sender, EventArgs e)
        {
            try
            {
                UI_CLIENTE_MASCOTA.GUI.Producto.frmProductos f = new UI_CLIENTE_MASCOTA.GUI.Producto.frmProductos();
                f.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region EVENTOS PARA EL SUBMENU DE CONSULTORIA
        //ABRE EL FORMULARIO DE AGENDAS DE CITAS 
        private void btnAgenda_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    UI_CONSULTORIA.GUI.AGENDA.AgendaCitas f = new UI_CONSULTORIA.GUI.AGENDA.AgendaCitas();
                    f.ShowDialog();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, panel1.ClientRectangle, Color.Purple, ButtonBorderStyle.Solid);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, panel3.ClientRectangle, Color.Purple, ButtonBorderStyle.Solid);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, panel2.ClientRectangle, Color.Purple, ButtonBorderStyle.Solid);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, panel4.ClientRectangle, Color.Purple, ButtonBorderStyle.Solid);
        }
    }
}
