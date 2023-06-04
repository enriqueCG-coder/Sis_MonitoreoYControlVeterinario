﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_CLIENTE_MASCOTA.GUI.Cliente_Mascota
{
    public partial class frmInfoMascota : Form
    {
        public frmInfoMascota()
        {
            InitializeComponent();
        }

        //VARIABLES DE MASCOTA
        public string IDMascota { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public string Raza { get; set; }
        public string Especie { get; set; }
        public string Color { get; set; }
        public string Rasgo { get; set; }
        public string FechaNac { get; set; }


        #region METODOS
        //CARGA LOS DATOS DE LA MASCOTA 
        public void cargarDatosMascota()
        {
            txtIDMascota.Text = IDMascota;
            txtNombre.Text = Nombre;
            txtSexo.Text = Genero;
            txtRaza.Text = Raza;
            txtEspecie.Text = Especie;
            txtColor.Text = Color;
            txtRasgo.Text = Rasgo;
            txtFechaNac.Text = FechaNac;
        }

        public void cargarHistorias()
        {
            dgvHistorial.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataManager.DBOperacion Operacion = new DataManager.DBOperacion();
            DataTable Resultado = new DataTable();
            Resultado = Operacion.Consultar(@"select * from historialclinico where IDMascota = "+ IDMascota+" ORDER BY Fecha asc;");
            dgvHistorial.DataSource = Resultado;
            lblRegistros.Text = dgvHistorial.Rows.Count.ToString() + " Registros Encontrados";
        }

        private void limpiarDetalle()
        {
            dtFechaHist.Value = DateTime.Now;
            txtIDHistoria.Clear();
            txtDiagnostico.Clear();
            txtPeso.Clear();
        }
        #endregion


        #region EVENTOS

        private void frmInfoMascota_Load(object sender, EventArgs e)
        {
            gbDetalleHistorial.Visible = false;
            cargarDatosMascota();
            cargarHistorias();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            gbDetalleHistorial.Visible = true;
        }

        private void btnCancelHist_Click(object sender, EventArgs e)
        {
            limpiarDetalle();
            gbDetalleHistorial.Visible = false;
        }

        #endregion

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente desea EDITAR el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                
                DataGridView dgv = dgvHistorial;
                txtIDHistoria.Text = dgv.CurrentRow.Cells["IDHistorial"].Value.ToString();
                dtFechaHist.Value = Convert.ToDateTime(dgv.CurrentRow.Cells["Fecha"].Value.ToString());
                txtDiagnostico.Text = dgv.CurrentRow.Cells["Diagnostico"].Value.ToString();
                txtPeso.Text = dgv.CurrentRow.Cells["Peso"].Value.ToString();
                gbDetalleHistorial.Visible = true;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente desea ELIMINAR el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CLS.HistorialClinico hc = new CLS.HistorialClinico();
                hc.IDHistorial = dgvHistorial.CurrentRow.Cells["IDHistorial"].Value.ToString().ToUpper();
                //Realizar la operacion de Eliminar
                if (hc.Eliminar())
                {
                    MessageBox.Show("¡Registro eliminado correctamente!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargarHistorias();
                }
                else
                {
                    MessageBox.Show("¡El registro no fue eliminado!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnGuardarHist_Click(object sender, EventArgs e)
        {
            //creacion del objeto entidad
            CLS.HistorialClinico hc = new CLS.HistorialClinico();
            hc.IDMascota = txtIDMascota.Text;
            hc.Fecha = Convert.ToString(dtFechaHist.Value.ToString("dd/MM/yyyy"));
            hc.Diagnostico = txtDiagnostico.Text;
            hc.Peso = txtPeso.Text;
            hc.IDHistorial = txtIDHistoria.Text;

            //validar que accion se va a realizar
            if (txtIDHistoria.TextLength > 0)
            {
                //actualizar
                if (hc.Actualizar())
                {
                    MessageBox.Show("¡Registro actualizado correctamente!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    gbDetalleHistorial.Visible = false;
                    cargarHistorias();
                    limpiarDetalle();
                }
                else
                {
                    MessageBox.Show("¡El registro no fue actualizado!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                //insertar
                if (hc.Insertar())
                {
                    MessageBox.Show("¡Registro insertado correctamente!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargarHistorias();
                    limpiarDetalle();
                    gbDetalleHistorial.Visible = false;
                }
                else
                {
                    MessageBox.Show("¡El registro no fue insertado!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

    }
}