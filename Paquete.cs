using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TP_Empresa_De_Cable
{
    public abstract class Paquete
    {

        protected decimal precioBase = 4500;

        
  
        public virtual decimal PrecioMensual{ get => precioBase; }

        protected List<Canal> canales;
        
        public Paquete() { canales = new List<Canal>(); }
        public Paquete(decimal PrecioBase)
        {
            precioBase = PrecioBase;
            canales = new List<Canal>();
        }
        
        ~Paquete() { }

        public bool AgregarCanal(Canal canal)
        {
            bool agregado = false;

            if (!ExisteCanal(canal))
            {
                canales.Add(new Canal(canal));
                agregado = true;
            }
            return agregado;
        }
        public bool DesagregarCanal(Canal canal)
        {
            bool desagregado = false;
            if(ExisteCanal(canal))
            {

                canales.Remove(canales.Find(x => x.Numero == canal.Numero && x.Nombre == canal.Nombre));
                desagregado = true;
            }
            return desagregado;
        }
              
        public bool ExisteCanal(Canal canal)
        {
            Canal canalAux = new Canal(canal);
            bool existe = canales.Exists(x => x.Numero == canal.Numero && x.Nombre == canal.Nombre);
            return existe;
        }
        public List<Canal> RetornaCanales()
        {
            List<Canal> canalesMuestra = new List<Canal>();

            if(canales.Count > 0)
            {
                foreach (Canal c in canales)
                {
                    canalesMuestra.Add(new Canal(c));
                }
            }
            return canalesMuestra;
        }
        public bool ModificarCanal(Canal canalOiginal, Canal canalModificado)
        {
            bool modificado = false;
            if (!ExisteCanal(canalModificado))
            {
                foreach (Canal c in canales)
                {

                    Canal canalAux1 = canales.Find(x => x.Numero == canalOiginal.Numero && x.Nombre == canalOiginal.Nombre);
                    if (canalAux1 != null)
                    {
                        canalAux1.Numero = canalModificado.Numero;
                        canalAux1.Nombre = canalModificado.Nombre;
                        modificado = true;
                    }
                }
            }
            return modificado; 
        }
        


    }
}