using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace TP_Empresa_De_Cable
{
    public partial class FormSeries : Form
    {
        public FormSeries()
        {
            InitializeComponent();
        }
        Serie serie;
        Temporada temporada;
        Episodio episodio;
        string informacion;
        DialogResult eleccionDeUsuario;
     
        
        
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormSeries_Load(object sender, EventArgs e)
        {
            
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
    
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.ReadOnly = true;
  
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.ReadOnly = true;

            RefrescarDataGridSeries();
            
           
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            //Agrega una serie al listado de series

            serie = new Serie(textBox1.Text.Trim(),dateTimePicker1.Value );
         
            
            if(DataBase.IngresarSerie(serie))
            {
               
                RefrescarDataGridSeries();
                textBox1.Clear();
                
            }
            else { MessageBox.Show("Ya existe una serie con el mismo nombre"); }
        }

      
        private void button2_Click(object sender, EventArgs e)
        {
            
            //al pulsar este boton se modifican en la clase estática
            if(serie !=null )
            {

                eleccionDeUsuario = MessageBox.Show("Cuidado, tenga en cuenta que se modificará tanto el título de la serie" +
                    " como la fecha, por los nuevos valores que usted haya ingresado y seleccionado (respectivamente).\n" +
                    " Confirma la operación?", "Importante!",MessageBoxButtons.YesNo);

                if(eleccionDeUsuario == DialogResult.No)
                {
                    return;
                }
                
                Serie serieModificada = new Serie();
                serieModificada.Nombre = textBox1.Text;
                serieModificada.FechaLanzamiento = dateTimePicker1.Value;
                
                if (DataBase.ModificarSerie(serie, serieModificada))
                {
                    informacion = "Serie Modificada con exito";
                    RefrescarDataGridSeries();
                }
                else { informacion = "Ya existe una serie con ese nombre en la base de datos"; }
            }
            else { informacion = "No hay series para modificar"; }

            MessageBox.Show(informacion);
        }

        
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            //Esta función determina si el objeto "serie" es una instancia de clase, o si apunta a null.
            if (dataGridView1.Rows.Count > 0 && dataGridView1.SelectedRows.Count ==1 && dataGridView1.DataSource !=null) //Si la fuente de datos del data grid NO es nula
            {
                //"serie" apunta a la serie a la que hace referencia la fila seleccionada en el datagrid
                serie = ((Serie)dataGridView1.CurrentRow.DataBoundItem);

            }
            else { serie = null; }//Si la propiedad datasource del datagrid apunta a null, "serie" apunta a null
            //Los cambios de "serie" tienen como consecuencia el cambio en las temporadas y episodios que se muestran...
            
            RefrescarDataGridTemporadas();//...tal como se puede ver en este metodo
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(serie != null)
            {
                try
                {
                    temporada = new Temporada(Convert.ToInt32(textBox3.Text));
                    if (DataBase.Series.Find(x=>x.Nombre==serie.Nombre).AgregarTemporada(temporada))
                    {
                        RefrescarDataGridTemporadas();
                        informacion = "Temporada agregada exitosamente";
                    }
                    else {
                        informacion = "ya existe una temporada con ese numero"
                            + "le sugerimos que ingrese la temporada con el número: " +
                            Convert.ToString(DataBase.Series.Find(x => x.Nombre == serie.Nombre)
                            .RetornaTemporadas().Max(x => x.Numero) + 1); }
                }
                catch (FormatException) when (textBox3.Text == "")
                {
                    informacion="El campo \"Número\" no puede estar vacío, y solo puede contener números ";
                }
                catch (Exception ex)
                {

                    informacion = ex.Message;
                }
                
            }
            else { informacion = "No hay una serie seleccionada a la cual agregarle temporadas"; }
            MessageBox.Show(informacion);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            
            if(dataGridView2.Rows.Count>0 && dataGridView2.SelectedRows.Count == 1 && dataGridView2.DataSource !=null)
            {
                temporada = ((Temporada)dataGridView2.CurrentRow.DataBoundItem);
            }
            else
            {
                temporada = null;
            }
            RefrescarDataGridEpisodios();

        }

      

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (dataGridView1.CurrentRow !=null)
            {
                eleccionDeUsuario = MessageBox.Show("Cuidado, si borra la serie se perderá toda" +
                    " la información de sus temporadas y episodios. Confirma la operación?", "Aviso importante!",
                    MessageBoxButtons.YesNo);
                if(eleccionDeUsuario == DialogResult.No)
                {
                    return;
                }

                DataBase.Series.Remove(DataBase.Series.Find(x=>x.Nombre == serie.Nombre));
                RefrescarDataGridSeries();

                informacion = "Serie eliminada exitosamente";
            }
            else { informacion = "No hay series para eliminar"; }
            MessageBox.Show(informacion);
        }
        private void RefrescarDataGridSeries()
        {

            dataGridView1.DataSource = null;//Aquí se dispara el evento selection changed del datagridview 1.
            List<Serie> seriesVista = DataBase.RetornaSeries();
            
                
            if (seriesVista.Count > 0)
            {
             dataGridView1.DataSource = seriesVista; //Aquí se vuelve a desencadenar selection

            }                                        //changed del datagrid 1, ver lo que pasa ahí(es importante)

        }
        private void RefrescarDataGridTemporadas()
        {
            
            dataGridView2.DataSource = null;//Analogamente a datagrid 1, aquí se dispara selection changed en el dgvw2
            
            if (serie != null)//Si "serie" no es nulo, 
            {
                List<Temporada> temporadasVista = serie.RetornaTemporadas();
                
                if(temporadasVista.Count > 0)
                {
                    dataGridView2.DataSource = temporadasVista;
                    
                }


            }
            else { textBox3.Text = "0"; }
            
        }
        private void RefrescarDataGridEpisodios()
        {
            
            dataGridView3.DataSource = null;//Aquí se desencadena selecition changed del dgvw de temporadas...
                                    //...que a su vez va a invocar el método refrescar grd episodios
            if (temporada != null)          //...que a su vez también va a desencadenar el evento selection...
            {                               //...changed del datagrid de episodios
                List<Episodio> episodiosVista = temporada.RetornaEpisodios();

                if (episodiosVista.Count > 0)
                {
                    dataGridView3.DataSource = episodiosVista;
                }
               
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_RowEnter_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView3.Rows.Count > 0 && dataGridView3.SelectedRows.Count > 0 && dataGridView3.DataSource != null)
            {
                episodio = new Episodio((Episodio)dataGridView3.SelectedRows[0].DataBoundItem);
                
            }
            else { episodio = null; }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(temporada != null)
            {
               
                try
                {
                    Temporada temporadaAuxiliar = new Temporada(Convert.ToInt32(textBox3.Text));
                    if(DataBase.Series.Find(x=>x.Nombre==serie.Nombre).ModificarDatosTemporada(temporada, temporadaAuxiliar))
                    {
                        informacion = "La temporada se ha modificado con éxito";
                        RefrescarDataGridTemporadas();
                    }
                    else {
                        informacion = "No se pudo modificar el número de temporada, el número que " +
                            "intentó ingresar ya existe, utilize otro";
                    }
                }
                catch (FormatException)
                {
                    informacion = "El número de temporada ingresado no tiene un formato válido";
                }
                catch (Exception ex)
                {

                   informacion = ex.Message;
                }
                
                
            }
            else { informacion = "No hay temporada para modificar"; }

            MessageBox.Show(informacion);
        }

        private void button9_Click(object sender, EventArgs e)
        {  
            if(temporada != null)
            {
                eleccionDeUsuario = MessageBox.Show("Está seguro que desea eliminar la temporada?" +
                    "Si la elimina, todos los episodios que la temporada contiene se eliminarán también", "Aviso importante!",
                    MessageBoxButtons.YesNo);
                if(eleccionDeUsuario == DialogResult.No)
                {
                    return;
                }

                if (DataBase.Series.Find(x=>x.Nombre==serie.Nombre).EliminarTemporada(temporada))
                {
                    informacion = "La temporada se ha eliminado con éxito";
                    RefrescarDataGridSeries();
                    //RefrescarDataGridTemporadas();
                }
                else { informacion ="Hubo un problema, no se pudo eliminar la temporada"; }
                
            }
            else { informacion = "No hay temporadas para eliminar"; }
            MessageBox.Show(informacion);
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(serie != null && temporada != null)
            {
                episodio = new Episodio();
                episodio.Numero = Convert.ToInt32(textBox2.Text);
                episodio.Nombre = textBox4.Text;
                episodio.Duracion = new TimeSpan(Decimal.ToInt32(numericUpDown1.Value),
                    Decimal.ToInt32(numericUpDown2.Value),
                    Decimal.ToInt32(numericUpDown3.Value));
                
                if (DataBase.Series.Find(x => x.Nombre == serie.Nombre).AgregarEpisodio(temporada,episodio))
                {
                    informacion = "Episodio agregado exitosamente";
                    RefrescarDataGridEpisodios();
                }
                else {
                    informacion = "El episodio no pudo agregarse, verifique que no exista otro episodio con el mismo" +
                        " número en la misma temporada, u otro episodio con el mismo nombre en la misma serie";
                }

            }
            else { informacion = "No se ha seleccionado  temporada  a la cuál agregar el episodio"; }
            MessageBox.Show(informacion);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (episodio != null)
            {
                Episodio episodioAuxiliar = new Episodio();
                episodioAuxiliar.Numero = Convert.ToInt32(textBox2.Text);
                episodioAuxiliar.Nombre =textBox4.Text;
                episodioAuxiliar.Duracion = new TimeSpan(Decimal.ToInt32(numericUpDown1.Value),
                    Decimal.ToInt32(numericUpDown2.Value),Decimal.ToInt32(numericUpDown3.Value));
                if(DataBase.Series.Find(x=>x.Nombre == x.Nombre).
                    ModificarEpisodio(temporada, episodio, episodioAuxiliar)){
                    RefrescarDataGridEpisodios();
                    informacion = "La modificación se realizó exitosamente";
                }
                else {
                    informacion = "No se pudo realizar la modificación, verifique " +
                        "los datos ingresados";
                }

            }
            else { informacion = "No hay episodios seleccionados para modificar"; }
            MessageBox.Show(informacion);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(episodio == null)
            {
                MessageBox.Show("No hay episodios seleccionados para eliminar.");
                return;
            }
            eleccionDeUsuario = MessageBox.Show("Está seguro respecto a eliminar este episodio? \n" +
                "Los datos se perderán definitivamente.", "Aviso Importante!", MessageBoxButtons.YesNo);
            {
                if(eleccionDeUsuario == DialogResult.Yes)
                {
                    if(DataBase.Series.Find(x => x.Nombre == serie.Nombre).EliminarEpisodio(temporada, episodio))
                    {
                        RefrescarDataGridEpisodios();
                        informacion = "El episodio se pudo eliminar exitosamente!";

                    }
                    else { informacion = "No se pudo eliminar el episodio"; }
                }
                else { informacion="Ok! Operación cancelada!"; }
            }
            MessageBox.Show(informacion);
        }
    }
}
