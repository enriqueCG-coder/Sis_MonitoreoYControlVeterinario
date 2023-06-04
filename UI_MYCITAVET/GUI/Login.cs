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
    public partial class Login : Form
    {
        SessionManager.Session oSesion = SessionManager.Session.Instancia;
        Boolean _Autorizado = false;
        public bool Autorizado { get => _Autorizado; }


        public Login()
        {
            InitializeComponent();
            txtUser.Focus();
            txtUser.Text = "VETUSER1-C";
            txtPassword.Text = "123";
        }


        #region EVENTOS
        //CUANDO SE CARGA EL FORMULARIO
        private void Login_Load(object sender, EventArgs e)
        {
            txtUser.Select();
        }

        //CUANDO SE DA CLIC EN EL BOTON DE ACCEDER
        private void btnlogin_Click(object sender, EventArgs e)
        {
            if (oSesion.IniciarSesion(txtUser.Text, txtPassword.Text))
            {
                _Autorizado = true;
                Close();
            }
            else
            {
                _Autorizado = false;
                lblMensaje.Text = "USUARIO O CLAVE INCORRECTOS";
            }
        }

        //CUANDO SE DA CLIC EN EL ICONO DE MOSTRAR
        private void pbMostrar_Click(object sender, EventArgs e)
        {
            //Pasamos nuestra imagen ocultar al frente
            pbOcultar.BringToFront();
            //Mostramos la contraseña
            txtPassword.PasswordChar = '\0';
        }

        //CUANDO SE DA CLIC EN EL ICONO DE OCULTAR
        private void pbOcultar_Click(object sender, EventArgs e)
        {
            //Imagen mostrar la mandamos al frente
            pbMostrar.BringToFront();
            //Ocultamos la contraseña
            txtPassword.PasswordChar = '☺';
        }

        //para cerrar el sistema
        private void btncerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //para minimizar la ventana 
        private void btnminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion

    }
}
