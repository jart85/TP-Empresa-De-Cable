using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Empresa_De_Cable
{
    public class Serie
    {
        public string Nombre { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        private List<Temporada> Temporadas;
        
        public Serie() { Temporadas = new List<Temporada>(); }
        public Serie(string nombre, DateTime fechaLanzamiento)
        {
            Nombre = nombre;
            FechaLanzamiento = fechaLanzamiento;
            Temporadas = new List<Temporada>();
        }

        public Serie(Serie serie)
        {
            Nombre = serie.Nombre;
            FechaLanzamiento = new DateTime(serie.FechaLanzamiento.Year,
                serie.FechaLanzamiento.Month, serie.FechaLanzamiento.Day);
        }
        ~Serie() { }
        private bool ExisteEpisodio(Episodio episodio) //Si el episodio existe retorna verdadero.
        {
            //que es lo que quiero saber? quiero saber si el titulo está repetido en la serie, y quiero saber si el numero está repetido en
            //la temporada
            foreach(Temporada t in Temporadas)
            {
                
                if (t.ExisteTitulo(episodio))
                {
                    return true;
                }
            }
            return false;
        }
        public bool AgregarEpisodio(Temporada temporada, Episodio episodio)//
        {
            if (!ExisteEpisodio(episodio))//Si el episodio NO existe, lo agrega a la temporada indicada.
            {
                Temporada temporadaAux = Temporadas.Find(x => x.Numero == temporada.Numero);
                return temporadaAux.AgregarEpisodio(episodio);
                
            }
            return false;
        }
        public bool AgregarTemporada(Temporada temporada) //Agrega una nueva temporada, le asigna un número por defecto
        {   //Agrega una temporada a la lista. Le asigna un número por defecto si el que el ojeto tiene es "cero".
            if(temporada.Numero == 0)
            {
                temporada.Numero = 1;

            }
            if (!ExisteTemporada(temporada))
            {
                Temporadas.Add(new Temporada(temporada.Numero));
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ExisteTemporada(Temporada temporada)
        {
            return Temporadas.Exists(x => x.Numero == temporada.Numero);
        }

        public bool ModificarDatosTemporada(Temporada temporadaOriginal, Temporada temporadaModificada)
        {
            //esta función está pensada SOLO para modificar datos DE LA TEMPORADA,
            //...no el contenido (los episodios) de la misma.
            if (!ExisteTemporada(temporadaModificada))
            {
                Temporadas.Where(x => x.Numero == temporadaOriginal.Numero).First()
                    .Numero = temporadaModificada.Numero;
                return true;
            }
            else { return false; }
        }
        public bool ModificarEpisodio(Temporada temporada,Episodio episodioOrignal, Episodio episodioModificado)
        {
            //busca la temporada a la cual corresponde el episodio, y le pasa el episodio
            //...original y el modificado para guardar los cambios.
            if (ExisteEpisodio(episodioModificado)) { return false; }
            
            return (Temporadas.Where(x => x.Numero == temporada.Numero).First())
                .ModificarEpisodio(episodioOrignal, episodioModificado);
        }
        public bool EliminarEpisodio(Temporada temporada, Episodio episodio)
        {
            return Temporadas.Find(x => x.Numero == temporada.Numero).EliminarEpisodio(episodio);
        }

        public bool EliminarTemporada(Temporada temporada)
        {
            return Temporadas.Remove(Temporadas.Find(x=>x.Numero == temporada.Numero));
        }
        public List<Temporada> RetornaTemporadas()
        {
            List<Temporada> temporadasParaMostrar = new List<Temporada>();
            foreach(Temporada s in Temporadas)
            {
                Temporada temporadaAuxiliar = new Temporada(s.Numero);
                temporadasParaMostrar.Add(temporadaAuxiliar);   
            }
            return temporadasParaMostrar;
        }
        
        public List<Episodio> RetornaEpisodios(Temporada temporada)
        {
            List<Episodio> episodiosVista = new List<Episodio>();
            Temporada temporadaAux = Temporadas.Find(x => x.Numero == temporada.Numero);
            if (temporadaAux !=null)
            {
                episodiosVista = temporadaAux.RetornaEpisodios();
            }
            
            return episodiosVista;
        }
    }
}
