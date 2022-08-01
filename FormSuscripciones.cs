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
    public partial class FormSuscripciones : Form
    {
        public FormSuscripciones()
        {
            InitializeComponent();
        }
        Cliente cliente;
        Paquete paquete;
        List<Cliente> clientes;
        private void FormSuscripciones_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            RefrescarDataGridClientes();
            comboBox1.DataSource = new string[] { "Quitar Plan", "Paquete Básico", "Paquete Silver", "Paquete Premium" };
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void RefrescarDataGridClientes()
        {
            dataGridView1.DataSource = null;
            clientes = DataBase.RetornaClientes();
            if(clientes.Count > 0)
            {
                dataGridView1.DataSource = clientes;
            }


        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count > 0 && dataGridView1.SelectedRows.Count > 0 && dataGridView1.DataSource != null)
            {
                cliente = new Cliente((Cliente)dataGridView1.SelectedRows[0].DataBoundItem);

            }
            else { cliente = null; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(cliente != null)
            {
                Cliente clienteAux = DataBase.listaClientes.Find(x => x.DNI == cliente.DNI);
                switch (comboBox1.SelectedItem.ToString())
                {
                    case "Paquete Básico":
                        paquete = new PaqueteBasico();
                        break;
                    case "Paquete Silver":
                        paquete = new PaqueteSilver();
                        break;
                    case "Paquete Premium":
                        paquete = new PaquetePremium();
                        break;

                    default:
                        paquete = null;
                        break;
                }
                clienteAux.Plan = paquete;
                RefrescarDataGridClientes();
            }
        }
    }
}
