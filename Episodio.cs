using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Empresa_De_Cable
{
    public class Episodio
    {
        public int Numero { get; set; }
        public string Nombre { get; set; }
        public TimeSpan Duracion { get; set; }
        public Episodio() { }

        public Episodio(int numero,string nombre, TimeSpan duracion)
        {
            Numero = numero;
            Nombre = nombre;
            Duracion = duracion;
        }
        public Episodio(Episodio episodio)
        {
            Numero = episodio.Numero;
            Nombre = episodio.Nombre;
            Duracion = episodio.Duracion;
        }
        ~Episodio() { }
    }
}
