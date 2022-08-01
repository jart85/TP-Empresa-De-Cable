using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Empresa_De_Cable
{
    public class Canal
    {
        public int Numero { get; set; }
        public string Nombre { get; set; }
        private List<Serie> Series;

        public Canal() { }
        public Canal(int numero, string nombre)
        {
            Numero = numero;
            Nombre = nombre;
            Series = new List<Serie>();
        }
        public Canal(Canal canal)
        {
            Numero = canal.Numero;
            Nombre = canal.Nombre;
        }
        ~Canal() { }


        public bool AgregarSerie(Serie serie)
        {
            bool agregada = false;
            Serie serieAux = new Serie(serie.Nombre, serie.FechaLanzamiento);

            if(!(Series.Exists(x=>x.Nombre == serie.Nombre && x.FechaLanzamiento == serie.FechaLanzamiento))){

                Series.Add(serie);
                agregada = true;
            }
            return agregada;
        }
        public bool QuitarSerie(Serie serie)
        {
            bool eliminada;
            eliminada = Series.Remove(Series.Find(x => x.Nombre == serie.Nombre && 
            x.FechaLanzamiento == serie.FechaLanzamiento));
            return eliminada;
        }
        public List <Serie> RetornaSeries()
        {
            List<Serie> seriesParaMostrar = new List<Serie>();

            if (Series.Count > 0)
            {

                foreach(Serie s in Series)
                {
                    Serie serieAux = new Serie(s.Nombre, s.FechaLanzamiento);
                    seriesParaMostrar.Add(serieAux);
                }
            }
            return seriesParaMostrar;
        }

    }
}
