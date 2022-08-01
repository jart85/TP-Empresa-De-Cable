using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Empresa_De_Cable
{
    public class Temporada
    {
        public int Numero { get; set; }

        private List<Episodio> Episodios;
        public Temporada() { Episodios = new List<Episodio>(); }
        public Temporada(int numero)
        {
            Numero = numero;
            Episodios = new List<Episodio>();
        }
        public Temporada(Temporada temporada)
        {
            Numero = temporada.Numero;
            Episodios = new List<Episodio>();
        }
        ~Temporada() { }

        public bool ExisteTitulo(Episodio episodio)
        {
            return Episodios.Exists(x => x.Nombre == episodio.Nombre);
        }
        public bool ExisteNumero(Episodio episodio)
        {
            return Episodios.Exists(x => x.Numero == episodio.Numero);
        }

        internal bool ModificarEpisodio(Episodio episodioOriginal, Episodio episodioModificado)
        {
            if(episodioModificado.Numero != episodioOriginal.Numero)
            {
                if(Episodios.Exists(x=>x.Numero == episodioModificado.Numero))
                {
                    return false;
                }
            }

            var t = (from e in Episodios
                     where e.Nombre == episodioOriginal.Nombre
                     && e.Numero == episodioOriginal.Numero
                     select e).FirstOrDefault();

            t.Numero = episodioModificado.Numero;
            t.Nombre = episodioModificado.Nombre;
            t.Duracion = episodioModificado.Duracion;
            return true;
        }

        public bool AgregarEpisodio(Episodio episodio)
        {
            bool ok = false;
            if (!ExisteNumero(episodio))
            {
                Episodios.Add(new Episodio(episodio.Numero,episodio.Nombre,episodio.Duracion));
                ok = true;
            }
            return ok;
        }
        public List<Episodio> RetornaEpisodios()
        {
            List<Episodio> episodiosParaMostrar = new List<Episodio>();

            foreach(Episodio e in Episodios)
            {
                episodiosParaMostrar.Add(new Episodio(e));
            }
            return episodiosParaMostrar;
        }

        public bool EliminarEpisodio(Episodio episodio)
        {
            return Episodios.Remove(Episodios.Find(x => x.Numero == episodio.Numero));
        }
    }
}
