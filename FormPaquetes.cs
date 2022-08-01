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
    public partial class FormPaquetes : Form
    {
        public FormPaquetes()
        {
            InitializeComponent();
        }

        Canal canal;
        Canal canalDePaqueteBasico;
        Canal CanalDePaqueteSilver;
        Canal canalDePaquetePremium;

        private void FormPaquetes_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
            dataGridView1.MultiSelect = false;

            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.ReadOnly = true;
            dataGridView2.MultiSelect = false;
            
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.ReadOnly = true;
            dataGridView3.MultiSelect = false;

            dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView4.ReadOnly = true;
            dataGridView4.MultiSelect = false;

            RefrescarDataGridDisponibles();
            RefrescarDataGridBasico();
            RefrescarDataGridSilver();
            RefrescarDataGridPremium();


        }

        private void RefrescarDataGridPremium()
        {
            dataGridView4.DataSource = null;
            List<Canal> canalesPremium = DataBase.Paquetes.Find(x => x is PaquetePremium).RetornaCanales();
            if(canalesPremium.Count > 0)
            {
                dataGridView4.DataSource = canalesPremium;

            }
        }

        private void RefrescarDataGridSilver()
        {
            dataGridView3.DataSource = null;
            List<Canal> CanalesSilver = DataBase.Paquetes.Find(x => x is PaqueteSilver).RetornaCanales();
            if(CanalesSilver.Count > 0)
            {
                dataGridView3.DataSource = CanalesSilver;
            }
        }

        private void RefrescarDataGridBasico()
        {
            dataGridView2.DataSource = null;
            List<Canal> CanalesBasico = DataBase.Paquetes.Find(x => x is PaqueteBasico).RetornaCanales();
            if(CanalesBasico.Count > 0)
            {
                dataGridView2.DataSource = CanalesBasico;
                
            }
           
        }

        private void RefrescarDataGridDisponibles()
        {
            //la intención de este método es mostrar todas las series que no han sido asgnadas a ningún paquete en particular
            //pedir a la base de datos
            //que muestre todos los canales que están sin asignar
            List<Canal> canalesDisponibles = new List<Canal>();
            dataGridView1.DataSource = null;
            canalesDisponibles = DataBase.RetornaCanalesDisponibles();
            if(canalesDisponibles.Count > 0)
            {
                dataGridView1.DataSource = canalesDisponibles;
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ObtenerCanaldeDataGrid(dataGridView1);
            if (canal != null)
            {
                DataBase.Paquetes.Find(x => x is PaqueteSilver).AgregarCanal(canal);
                RefrescarDataGridDisponibles();
                RefrescarDataGridSilver();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {


            ObtenerCanaldeDataGrid(dataGridView1);

            if (canal != null)
            {
                if(DataBase.Paquetes.Find(x => x is PaqueteBasico).AgregarCanal(canal))
                {
                    RefrescarDataGridDisponibles();
                    RefrescarDataGridBasico();
                    MessageBox.Show("Canal agregado exitosamente al paquete básico");
                }
                else { MessageBox.Show("No se pudo realizar la operación"); }
            }
        }
        private void ObtenerCanaldeDataGrid(DataGridView dataGrid)
        {
            if (dataGrid.DataSource != null && dataGrid.Rows.Count > 0 && dataGrid.SelectedRows.Count == 1)
            {
                canal = new Canal((Canal)dataGrid.SelectedRows[0].DataBoundItem);
            }
            else { canal = null; }
        } 
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView4_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView4.Rows.Count > 0 && dataGridView4.SelectedRows.Count ==1 && dataGridView4.DataSource != null)
            {
                canalDePaquetePremium = new Canal((Canal)dataGridView4.SelectedRows[0].DataBoundItem);
            }
            else
            {
                canalDePaquetePremium = null;
            }
            
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                if(canalDePaqueteBasico != null)
                {
                    DataBase.Paquetes.Find(x => x is PaqueteBasico).DesagregarCanal(canalDePaqueteBasico);
                    RefrescarDataGridDisponibles();
                    RefrescarDataGridBasico();
                }

            }
            catch (Exception ex )
            {

                MessageBox.Show("No se pudo realizar la operación" +  Environment.NewLine+
                    ex.Message);
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView2.Rows.Count > 0 && dataGridView2.SelectedRows.Count == 1  && dataGridView2.DataSource != null)
            {
                canalDePaqueteBasico = new Canal((Canal)dataGridView2.SelectedRows[0].DataBoundItem);
                
            }
            else { canalDePaqueteBasico = null; }
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView3.Rows.Count > 0 && dataGridView3.SelectedRows.Count>0 && dataGridView3.DataSource != null)
            {
                CanalDePaqueteSilver = new Canal((Canal)dataGridView3.SelectedRows[0].DataBoundItem);
            }
            else { CanalDePaqueteSilver = null; }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                if(CanalDePaqueteSilver != null)
                {
                    DataBase.Paquetes.Find(x => x is PaqueteSilver).DesagregarCanal(CanalDePaqueteSilver);
                    RefrescarDataGridDisponibles();
                    RefrescarDataGridSilver();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("No se pudo realizar la operación");
            }

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            try
            {
                if(canalDePaquetePremium != null)
                {
                    DataBase.Paquetes.Find(x => x is PaquetePremium).DesagregarCanal(canalDePaquetePremium);
                    RefrescarDataGridDisponibles();
                    RefrescarDataGridPremium();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                ObtenerCanaldeDataGrid(dataGridView1);
                if(canal != null)
                {
                    DataBase.Paquetes.Find(x => x is PaquetePremium).AgregarCanal(canal);
                    RefrescarDataGridDisponibles();
                    RefrescarDataGridPremium();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
