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
    
    public partial class FormClientes : Form
    {
        private Cliente cliente;
        private Cliente clienteAuxiliar;
 
        public FormClientes()
        {
            InitializeComponent();
        }

        private void FormClientes_Load(object sender, EventArgs e)
        {
            
            dataGridView1.DataSource = DataBase.listaClientes;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            RefrescarDatagridClientes();

        }

        private void RefrescarDatagridClientes()
        {
            List<Cliente> clientes = DataBase.RetornaClientes();
            dataGridView1.DataSource = null;
            if(clientes.Count > 0)
            {
                dataGridView1.DataSource = clientes;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region Alta Clientes
            if (radioButton1.Checked && textBox1.Text != "")
            {
                //código para instanciar un cliente
                cliente = new Cliente();
                cliente.DNI = Convert.ToInt32(textBox1.Text);
                cliente.Nombre = textBox2.Text.Trim();
                cliente.Apellido = textBox3.Text.Trim();
                cliente.fechaNacimiento = monthCalendar1.SelectionStart.Date;
                // No se puede ingresar un cliente con un dni que ya esté registrado en la base de datos
               
                if(!DataBase.listaClientes.Exists(x=>x.DNI == cliente.DNI))
                {
                    DataBase.listaClientes.Add(new Cliente( cliente));
                    LimpiarTextboxes();
                    RefrescarDatagridClientes();
                }
                else
                {
                    //informar problema
                    MessageBox.Show("El DNI que intenta ingresar ya existe en la base de datos.");
                    
                }
                
            }
            #endregion

            #region Modificación Clientes
            if (radioButton2.Checked)
            {
                if (clienteAuxiliar !=null)
                {
                    
                    clienteAuxiliar.Nombre = textBox2.Text;
                    clienteAuxiliar.Apellido = textBox3.Text;
                    clienteAuxiliar.fechaNacimiento = monthCalendar1.SelectionStart;

                    //guardo la modificación de los datos en la "base de datos"
                  

                    var db = from cli in DataBase.listaClientes
                             where cli.DNI == clienteAuxiliar.DNI
                             select cli;

                    db.FirstOrDefault().Nombre = clienteAuxiliar.Nombre;
                    db.FirstOrDefault().Apellido = clienteAuxiliar.Apellido;
                    db.FirstOrDefault().fechaNacimiento = clienteAuxiliar.fechaNacimiento;

                    RefrescarDatagridClientes();
                    LimpiarTextboxes();
                }
            }
            #endregion

            #region Baja Clientes
            if (clienteAuxiliar !=null && radioButton3.Checked == true)
            {
               
                DataBase.listaClientes.Remove(DataBase.listaClientes.Find(x => x.DNI == clienteAuxiliar.DNI));
                RefrescarDatagridClientes();
                LimpiarTextboxes();
            }

            #endregion
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                MessageBox.Show("Seleccione un cliente para modificar. Puede modificar cualquier dato,\n" +
                "a excepción del DNI");
                textBox1.Enabled = false;
                textBox1.Text = "";

            }
            else { textBox1.Enabled = true; }
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                MessageBox.Show("Ingrese DNI, nombre, apellido y Fecha de Nacimiento para" +
                    " registrar un nuevo cliente");
            }
            
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                MessageBox.Show("Seleccione un cliente para borrar su registro");
            }

        }
      

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count>0 && dataGridView1.SelectedRows.Count>0 && dataGridView1.DataSource != null)
            {
                clienteAuxiliar = new Cliente((Cliente)dataGridView1.SelectedRows[0].DataBoundItem);
            }
            else { clienteAuxiliar = null; }
        }
        private void LimpiarTextboxes()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

        }
    }
    
}
