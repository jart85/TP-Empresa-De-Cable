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
    public partial class FormCanales : Form
    {
        public FormCanales()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        Canal canal;
        Serie serieDisponible;
        Serie SerieDelCanal;

        private void FormCanales_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.ReadOnly = true;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            
            dataGridView3.ReadOnly = true;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
           
            RefrescarDataGridCanales();
            RefrescarDataGridSeriesDisponibles();
            RefrescarDataGridSeriesDelCanal();


        }


        private void RefrescarDataGridSeriesDelCanal()
        {
            //Tengo que mostrar en el datagrid las series que contiene el canal seleccionado;
            //el canal que tengo seleccionado en el listado,
            //le pido las series que tiene asignadas
            dataGridView2.DataSource = null;
            List<Serie> seriesAsignadas = new List<Serie>();
            if(canal != null)
            {
                if (DataBase.Canales.Count > 0)
                {
                    seriesAsignadas = DataBase.Canales.Find(x => x.Numero == canal.Numero).RetornaSeries();
                    if(seriesAsignadas.Count > 0)
                    {
                        dataGridView2.DataSource = seriesAsignadas;
                    }

                }
            }
            
        }

        private void RefrescarDataGridSeriesDisponibles()
        {
            //si hay series disponibles, mostrarlas en el datagrid 3
            //de lo contrario, mostrar nada.
            dataGridView3.DataSource = null;
            List<Serie> SeriesDisponibles = new List<Serie>();
            SeriesDisponibles = DataBase.RetornaSeriesDisponibles();

            if( SeriesDisponibles.Count > 0)
            {
                dataGridView3.DataSource = SeriesDisponibles;
            }
        }

        private void RefrescarDataGridCanales()
        {
            dataGridView1.DataSource = null;
            List<Canal> CanalesParaMostrar = DataBase.RetornaCanales();
            if (CanalesParaMostrar.Count > 0)
            {
               dataGridView1.DataSource = CanalesParaMostrar;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            canal = new Canal();
            canal.Numero = Convert.ToInt32(textBox1.Text);
            canal.Nombre = textBox2.Text;
            if(!DataBase.Canales.Exists(x=>x.Numero == canal.Numero || x.Nombre == canal.Nombre))
            {

                DataBase.Canales.Add(new Canal(canal.Numero, canal.Nombre));
                RefrescarDataGridCanales();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Canal canalModificado = new Canal(Convert.ToInt32(textBox1.Text), textBox2.Text);

            if(canal != null)
            {
                if(DataBase.ModificarCanal(canalModificado, canal))
                {
                    MessageBox.Show("La modificación se realizó con éxito");
                    RefrescarDataGridCanales();

                }
                else {
                    MessageBox.Show("No se pudo realizar la modificación. Verifique no estar repitiendo nombre" +
                    " o número de algún otro canal existente");
                }
                

            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            canal = null;
            
            if(dataGridView1.Rows.Count > 0 && dataGridView1.SelectedRows.Count > 0)
            {
                canal = new Canal();
                canal.Nombre = ((Canal)dataGridView1.SelectedRows[0].DataBoundItem).Nombre;
                canal.Numero = ((Canal)dataGridView1.SelectedRows[0].DataBoundItem).Numero;
                RefrescarDataGridSeriesDelCanal();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(canal != null)
            {
                DataBase.EliminarCanal(canal);
                RefrescarDataGridCanales();
                RefrescarDataGridSeriesDisponibles();
                RefrescarDataGridSeriesDelCanal();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Con este boton se agregan series al canal
            if(canal !=null && serieDisponible != null)
            {
                if (DataBase.AsignarSerie(canal, serieDisponible))
                {
                    MessageBox.Show("La serie se agregó exitosamente");

                    RefrescarDataGridSeriesDelCanal();
                    RefrescarDataGridSeriesDisponibles();//obviamente, al haber asignado series,
                                                          //..tengo menos series disponibles para asignar,
                                                          //..por eso la actualización
                }
                else { MessageBox.Show("No se pudo agregar la serie, canal o serie son nulos"); }
            }
            
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            serieDisponible = null;
            if(dataGridView3.Rows.Count > 0 && dataGridView3.SelectedRows.Count == 1)
            {
                serieDisponible = new Serie((Serie)dataGridView3.SelectedRows[0].DataBoundItem);
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            SerieDelCanal = null;
            if(dataGridView2.Rows.Count > 0 && dataGridView2.SelectedRows.Count == 1 && dataGridView2.DataSource!=null)
            {
                SerieDelCanal = new Serie((Serie)dataGridView2.SelectedRows[0].DataBoundItem);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(SerieDelCanal != null)
            {
                if (DataBase.DesasignarSerie(canal,SerieDelCanal))
                {
                    MessageBox.Show("La serie se desasignó existosamente");
                    RefrescarDataGridSeriesDisponibles();
                    RefrescarDataGridSeriesDelCanal();
                }
                else { MessageBox.Show("No se pudo desasignar la serie"); }
            }
        }
    }
}
