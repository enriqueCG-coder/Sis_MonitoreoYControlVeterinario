using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//librerias para las notificacione sde email 
using System.Net;
using System.Net.Mail;


namespace UI_CLIENTE_MASCOTA.GUI.Cliente_Mascota
{
    public partial class frmMascotas : Form
    {
        public frmMascotas()
        {
            InitializeComponent();
        }

        BindingSource _DATOS = new BindingSource();

        #region VARIABLES PARA CARGAR AL CLIENTE EN MASCOTA
        public string IDCliente { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string GeneroCliente { get; set; }
        public string TipoDoc { get; set; }
        public string Documento { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Departamento { get; set; }
        public string Municipio { get; set; }
        public string Direccion { get; set; }

        #endregion

        #region METODOS 
        //METODO PARA GENERAR UNA CADENA DE CARACTERES ALEATOREAMENTE
        static string GenerarCadenaAleatoria(Random random, string caracteres, int longitud)
        {
            char[] cadena = new char[longitud];

            for (int i = 0; i < longitud; i++)
            {
                cadena[i] = caracteres[random.Next(caracteres.Length)];
            }

            return new string(cadena);
        }

        public class Opcion
        {
            public double Valor { get; set; }
            public string Descripcion { get; set; }
        }

        //CARGA LOS TIPOS DE CITA
        private void cargarTipoCita()
        {
            List<Opcion> opciones = new List<Opcion>()
            {
                new Opcion { Valor = 0, Descripcion = "Seleccione" },
                new Opcion { Valor = 1.5, Descripcion = "Corte de pelo y baño" },
                new Opcion { Valor = 1, Descripcion = "Baño" },
                new Opcion { Valor = 0.5, Descripcion = "Consulta médica" }
            };

            cbTipoCita.DataSource = opciones;
            cbTipoCita.DisplayMember = "Descripcion";
            cbTipoCita.ValueMember = "Valor";
            cbTipoCita.SelectedIndex = 0;
        }

        //CARGA LOS GENEROS
        private void cargarGenero()
        {
            List<string> registros = new List<string>()
            {
                "HEMBRA",
                "MACHO"
            };
            List<string> registrosModificados = new List<string>(registros);
            registrosModificados.Insert(0, "Seleccione");
            cmbGenero.DataSource = registrosModificados;
            cmbGenero.SelectedIndex = 0;
        }

        //PARA LIMPIAR LOS TEXTBOX DEL PANEL MASCOTAS 
        public void limpiar()
        {
            txtID.Clear();
            txtNombre.Clear();
            txtColor.Clear();
            txtRasgo.Clear();
            dtpFechaMasc.Value = DateTime.Now;
            cmbGenero.SelectedItem = "Seleccione";
            cbEspecie.SelectedValue = 0;
            cbRaza.SelectedValue = 0;
        }

        //CARGA LOS DATOS DEL CLIENTE EN MASCOTA 
        public void cargarDatosCliente()
        {
            txtIDCliente.Text = IDCliente;
            txtNombres.Text = Nombres;
            txtApellidos.Text = Apellidos;
            txtGenero.Text = GeneroCliente;
            lblTipoDoc.Text = TipoDoc;
            txtDocumento.Text = Documento;
            txtCorreo.Text = Correo;
            txtTelefono.Text = Telefono;
            txtDepartamento.Text = Departamento;
            txtMunicipio.Text = Municipio;
            rtxtDireccion.Text = Direccion;


        }

        private void BuscarMascota()
        {
            //String Buscar = txtBuscar.Text;
            //DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
            //DataTable Resultado = new DataTable();
            //Resultado = Operacion.Consultar("SELECT E.IDEmpleado, E.Nombres, " +
            //                                "E.Apellidos, E.FechaNac, E.Genero," +
            //                                "D.Nombre as Departamento, M.Nombre as Municipio, " +
            //                                "E.Direccion, E.TipoDoc, E.Documento, " +
            //                                "E.Correo, E.Telefono, D.Id, E.IdMunicipio FROM Empleados as E " +
            //                                "INNER JOIN Municipio as M " +
            //                                "ON M.Id = E.IdMunicipio " +
            //                                "INNER JOIN Departamento as D " +
            //                                "ON D.Id = M.IdDepartamento " +
            //                                "WHERE E.Nombres LIKE '%" + Buscar + "%' OR " +
            //                                "E.Apellidos LIKE '%" + Buscar + "%' OR " +
            //                                "E.Documento LIKE '%" + Buscar + "%';");
            //dgvEmpleados.DataSource = Resultado;

            //dgvEmpleados.Columns["IDEmpleado"].Visible = false;
            //dgvEmpleados.Columns["Id"].Visible = false;
            //dgvEmpleados.Columns["IdMunicipio"].Visible = false;
        }

        //METODO PARA CARGAR LAS MASCOTAS SEGUN EL ID DEL CLIENTE EN EL DATAGRIDVIEW
        private void cargarMascotas()
        {
            try
            {
                dgvMascota.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                _DATOS.DataSource = DataManager.DBConsultas.getMascotaByCliente(IDCliente);
                dgvMascota.DataSource = _DATOS;
                lblRegistros.Text = dgvMascota.Rows.Count.ToString() + " Registros Encontrados";
                cargarDatosCliente();
            }
            catch (Exception)
            {
                //NO DEVUELVE HACE NADA 
            }
        }

        //OBTIENE LAS ESPECIES EN EL COMBOBOX DE ESPECIES
        public void obtenerEspecies()
        {
            DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
            DataTable dt = new DataTable();
            dt = Operacion.Consultar("select IDEspecie, Especie from Especies;");

            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "Seleccione";
            dt.Rows.InsertAt(dr, 0);
            cbEspecie.DataSource = dt;
            cbEspecie.DisplayMember = "Especie";
            cbEspecie.ValueMember = "IDEspecie";
        }

        //OBTIENE LAS RAZAS EN EL COMBOBOX DE RAZAS 
        public void obtenerRazas()
        {
            DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
            DataTable dt = new DataTable();
            dt = Operacion.Consultar("select IDRaza, Raza from Razas where IDEspecie ='" + cbEspecie.SelectedValue + "' ORDER BY Raza ASC;");

            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "Seleccione";
            dt.Rows.InsertAt(dr, 0);
            cbRaza.DataSource = dt;
            cbRaza.DisplayMember = "Raza";
            cbRaza.ValueMember = "IDRaza";
        }

        #endregion

        #region EVENTOS DEL FORMULARIO MASCOTAS 

        //CUANDO SE ABRE EL FORMULARIO 
        private void frmMascotas_Load(object sender, EventArgs e)
        {
            obtenerEspecies();
            cargarMascotas();
            plMascota.Visible = false;
            metodoHoras();
        }

        //CUANDO SE DA CLICK EN EL BOTON AGREGAR 
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            plMascota.Visible = true;
            cargarGenero();
        }

        //CUANDO SE DA CLICK EN EL BOTON DE EDITAR
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente desea EDITAR el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cargarGenero();
                DataGridView dgv = dgvMascota;
                txtID.Text = dgv.CurrentRow.Cells["IDMascota"].Value.ToString();
                txtNombre.Text = dgv.CurrentRow.Cells["Nombre"].Value.ToString();
                txtColor.Text = dgv.CurrentRow.Cells["Color"].Value.ToString();
                txtRasgo.Text = dgv.CurrentRow.Cells["Rasgo"].Value.ToString();
                cmbGenero.SelectedItem = dgv.CurrentRow.Cells["Genero"].Value.ToString();
                dtpFechaMasc.Value = Convert.ToDateTime(dgv.CurrentRow.Cells["FechaNacimiento"].Value.ToString());
                cbEspecie.SelectedValue = dgv.CurrentRow.Cells["IDEspecie"].Value.ToString();
                cbRaza.SelectedValue = dgv.CurrentRow.Cells["IDRaza"].Value.ToString();
                plMascota.Visible = true;
            }

        }

        //CUANDO SE DA CLICK EN EL BOTON ELIMINAR
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente desea ELIMINAR el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CLS.Mascotas M = new CLS.Mascotas();
                M.IDMascota = dgvMascota.CurrentRow.Cells["IDMascota"].Value.ToString().ToUpper();
                //Realizar la operacion de Eliminar
                if (M.Eliminar())
                {
                    MessageBox.Show("¡Registro eliminado correctamente!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargarMascotas();
                }
                else
                {
                    MessageBox.Show("¡El registro no fue eliminado!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        //CUANDO SE DA CLICK EN LA IMAGEN DE VER EN EL DATAGRIDVIEW 
        private void dgvMascota_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvMascota.Columns["Seleccionar"].Index && e.RowIndex >= 0)
            {
                DataGridViewRow filaSeleccionada = dgvMascota.Rows[e.RowIndex];
                string idMascota = filaSeleccionada.Cells["IDMascota"].Value.ToString();
                string nombreM = filaSeleccionada.Cells["Nombre"].Value.ToString();
                string generoM = filaSeleccionada.Cells["Genero"].Value.ToString();
                string raza = filaSeleccionada.Cells["Raza"].Value.ToString();
                string especie = filaSeleccionada.Cells["Especie"].Value.ToString();
                string color = filaSeleccionada.Cells["Color"].Value.ToString();
                string rasgo = filaSeleccionada.Cells["Rasgo"].Value.ToString();
                string fechaNac = filaSeleccionada.Cells["FechaNacimiento"].Value.ToString();
                UI_CLIENTE_MASCOTA.GUI.Cliente_Mascota.frmInfoMascota f = new UI_CLIENTE_MASCOTA.GUI.Cliente_Mascota.frmInfoMascota();
                f.IDMascota = idMascota;
                f.Nombre = nombreM;
                f.Genero = generoM;
                f.Raza = raza;
                f.Especie = especie;
                f.Color = color;
                f.Rasgo = rasgo;
                f.FechaNac = fechaNac;
                f.ShowDialog();
            }
            else if (e.ColumnIndex == dgvMascota.Columns["RealizarCita"].Index && e.RowIndex >= 0)
            {
                
                panelCitas.Visible = true;
                
                cargarTipoCita();
                DataGridViewRow filaSeleccionada = dgvMascota.Rows[e.RowIndex];
                string idM = filaSeleccionada.Cells["IDMascota"].Value.ToString();
                string mascota = filaSeleccionada.Cells["Nombre"].Value.ToString();
                txtIDMcita.Text = idM;
                txtMascota.Text = mascota;

                Random random = new Random();
                string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                string cadenaGenerada = GenerarCadenaAleatoria(random, caracteres, 6);
                //se le pasa la cadena generada al textbox correlativo
                txtCorrelativo.Text = "#"+"-"+cadenaGenerada;
            }
        }

        //DESPUES DE SELECCIONAR UNA ESPECIE 
        private void cbEspecie_SelectedIndexChanged(object sender, EventArgs e)
        {
            obtenerRazas();
        }

        #endregion


        #region EVENTOS DEL PANEL MASCOTAS 

        //CUANDO SE DA CLICK EN EL BOTON DE GUARDAR 
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            
            //creacion del objeto entidad
            CLS.Mascotas m = new CLS.Mascotas();
            m.Nombre = txtNombre.Text;
            m.IDRaza = Convert.ToInt32(cbRaza.SelectedValue);
            m.Color = txtColor.Text;
            m.Rasgo = txtRasgo.Text;
            m.FechaNac = Convert.ToString(dtpFechaMasc.Value.ToString("dd/MM/yyyy"));
            m.Genero = Convert.ToString(cmbGenero.SelectedItem);
            m.IDCliente = txtIDCliente.Text;
            m.IDMascota = txtID.Text;

            //validar que accion se va a realizar
            if (txtID.TextLength > 0)
            {
                //actualizar
                if (m.Actualizar())
                {
                    MessageBox.Show("¡Registro actualizado correctamente!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    plMascota.Visible = false;
                    cargarMascotas();
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
                if (m.Insertar())
                {
                    MessageBox.Show("¡Registro insertado correctamente!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargarMascotas();
                    limpiar();
                    plMascota.Visible = false;
                }
                else
                {
                    MessageBox.Show("¡El registro no fue insertado!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        //PARA CERRAR EL PANEL DE MASCOTAS 
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiar();
            plMascota.Visible = false;
        }


        #endregion



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

        private void btncerrar_Click(object sender, EventArgs e)
        {
            panelCitas.Visible = false;
        }


        private void datePickerFecha_ValueChanged(object sender, EventArgs e)
        {
            DateTime fechaSeleccionada = datePickerFecha.Value.Date;

            // Obtener las horas disponibles para la fecha seleccionada
            List<DateTime> horariosDisponibles = ObtenerHorariosDisponibles(fechaSeleccionada);

            // Llenar el ComboBox con las horas disponibles
            comboBoxInicio.DataSource = null; // Primero, desvincula la fuente de datos existente
            comboBoxInicio.Items.Clear(); // Limpia la colección Items

            // Agrega los elementos al ComboBox
            foreach (DateTime horario in horariosDisponibles)
            {
                comboBoxInicio.Items.Add(horario);
            }
        }

        private void comboBoxInicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            String fecha = Convert.ToString(datePickerFecha.Value.ToString("dd/MM/yyyy"));
            DateTime horaSeleccionada = (DateTime)comboBoxInicio.SelectedItem;
            Double time = Convert.ToDouble(cbTipoCita.SelectedValue);
            DateTime horaFinal = horaSeleccionada.AddHours(time);
            string hSeleccionada = horaSeleccionada.ToString("hh:mm tt");
            string hFinal = horaFinal.ToString("hh:mm tt");
            txtHF.Text = horaFinal.ToString("hh:mm");
            txtHIni.Text = horaSeleccionada.ToString("hh:mm");
            txtFin.Text = Convert.ToString("De: " + hSeleccionada +
                                            " a " + hFinal + ". ");


            

            // Deshabilitar las horas desde la hora seleccionada hasta la hora final
            //for (int i = comboBoxInicio.Items.Count - 1; i >= 0; i--)
            //{
            //    DateTime hora = (DateTime)comboBoxInicio.Items[i];
            //    if (hora >= horaSeleccionada && hora < horaFinal)
            //    {
            //        //comboBoxInicio.Items.RemoveAt(i);
            //    }
            //}
        }


        //METODO DE TIPO LISTA QUE OBTIENE TODOS LOS HORARIOS EN LA FECHA SELECCIONADA 
        private List<DateTime> ObtenerHorariosDisponibles(DateTime fechaSeleccionada)
        {
            List<DateTime> horariosDisponibles = new List<DateTime>();

            TimeSpan horaInicio = new TimeSpan(8, 0, 0); // 8 am
            TimeSpan horaFin = new TimeSpan(17, 0, 0); // 5 pm

            // Intervalo de tiempo entre cada horario disponible
            TimeSpan intervalo = new TimeSpan(0, 30, 0); // Media hora

            // Crear una variable de tipo DateTime con la fecha seleccionada y la hora de inicio
            DateTime horarioActual = fechaSeleccionada.Date + horaInicio;


            // Generar los horarios disponibles desde la hora de inicio hasta la hora de fin
            while (horarioActual.TimeOfDay <= horaFin)
            {
                horariosDisponibles.Add(horarioActual);
                horarioActual = horarioActual.Add(intervalo);
            }

            return horariosDisponibles;
        }

        private void metodoHoras()
        {
            // Configurar el intervalo de tiempo para el ComboBox
            // Intervalo de tiempo entre cada horario disponible
            TimeSpan intervalo = new TimeSpan(0, 30, 0); // Media hora
            DateTime horaInicio = DateTime.Today.AddHours(8); // 8 am
            DateTime horaFin = DateTime.Today.AddHours(17); // 5 pm

            while (horaInicio <= horaFin)
            {
                comboBoxInicio.Items.Add(horaInicio);
                horaInicio += intervalo;
            }
        }

        private void cbTipoCita_SelectedValueChanged(object sender, EventArgs e)
        {

        }
        

        private void btnGuardarCita_Click(object sender, EventArgs e)
        {
            //    string cbCita = Convert.ToString(cbTipoCita.SelectedItem);
            //    string dia = Convert.ToString(datePickerFecha.Value.Day);
            //    string mes = Convert.ToString(datePickerFecha.Value.Month);
            //    string anio = Convert.ToString(datePickerFecha.Value.Year);
            //    string hInicio = Convert.ToString(datePickerFecha.Value.Year);
            //    DateTime horaSeleccionada = (DateTime)comboBoxInicio.SelectedItem;
            //    string hSeleccionada = horaSeleccionada.ToString("hh");
            //    txtCorrelativo.Text = "CC-" + cbCita + "-" + dia + mes + anio + "-" + hSeleccionada + "";
            //credenciales de correo
            string remitente = "SysVeterinaria@gmail.com";
            string pass = "_SysVeterinaria987";
            string destinatario = txtCorreo.Text; 
            String fecha = Convert.ToString(datePickerFecha.Value.ToString("dd/MM/yyyy"));

            // Crear el cliente SMTP con la información del servidor
            SmtpClient clienteSmtp = new SmtpClient("smtp.gmail.com", 587);
            clienteSmtp.EnableSsl = true;
            clienteSmtp.Credentials = new NetworkCredential(remitente, pass);

            // Crear el mensaje de correo
            MailMessage mensaje = new MailMessage(remitente, destinatario);
            mensaje.Subject = "Notificación Veterinaria";
            mensaje.Body = "Correlativo "+ txtCorrelativo.Text + ". La cita de su mascota "+ txtMascota.Text +" ha sido agendada para el día "+ fecha+" " + txtFin.Text + ". Favor se le solicita ser puntual.";

            //creacion del objeto entidad
            CLS.Cita c = new CLS.Cita();
            c.IDMascota = txtIDMcita.Text;
            c.CorrelativoCita = txtCorrelativo.Text;
            c.Fecha = Convert.ToString(datePickerFecha.Value.ToString("dd/MM/yyyy"));
            c.HoraInicio = txtHIni.Text;
            c.HoraFin = txtHF.Text;
            c.Motivo = ((Opcion)cbTipoCita.SelectedItem).Descripcion;
            c.IDCita = txtIDCita.Text;


            //validar que accion se va a realizar
            if (ExisteCitaEnRango(c.Fecha, c.HoraInicio, c.HoraFin) == true)
            {
                MessageBox.Show("Ya existe una cita en el rango de horas seleccionado.");
                return;
            }
            else
            {
                if (c.Insertar())
                {
                    try
                    {
                        // Enviar el mensaje
                        clienteSmtp.Send(mensaje);
                        MessageBox.Show("Notificación enviada correctamente.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al enviar la notificación: " + ex.Message);
                    }
                    MessageBox.Show("¡Registro insertado correctamente!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargarMascotas();
                    limpiar();
                    plMascota.Visible = false;
                }
                else
                {
                    MessageBox.Show("¡El registro no fue insertado!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


        //METODO QUE VERIFICA SI EXISTEN CITAS EN EL RANGO DE HORAS SELECCIONADO 
        public Boolean ExisteCitaEnRango(String pFecha, String pHoraInicio, String pHoraFin)
        {
            Boolean result = false;
            DataTable Resultado = new DataTable();
            try
            {
                Resultado = DataManager.DBConsultas.ValidarCitas(pFecha, pHoraInicio, pHoraFin);
                if (Resultado.Rows.Count > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

    }
}
