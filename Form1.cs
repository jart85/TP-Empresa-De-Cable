using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP_Empresa_De_Cable
{
    public partial class Form1 : Form
    {
        private FormClientes frmClientes;
        public Form1()
        {
            InitializeComponent();
            DataBase.Inicializar();
        }

        private void paToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void paquetesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            FormPaquetes frmPaquetes = new FormPaquetes();
            frmPaquetes.MdiParent = this;
            frmPaquetes.Show();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClientes = new FormClientes();
            frmClientes.MdiParent = this;
            frmClientes.Show();
        }

        private void abonosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSuscripciones frmSuscripciones = new FormSuscripciones();
            frmSuscripciones.MdiParent = this;
            frmSuscripciones.Show();
        }

        private void seriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSeries frmSeries = new FormSeries();
            frmSeries.MdiParent = this;
            frmSeries.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void canalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCanales frmCanales = new FormCanales();
            frmCanales.Show();
        }

        private void informesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormInformes frmInformes = new FormInformes();
            frmInformes.MdiParent = this;
            frmInformes.Show();
        }
    }
}
