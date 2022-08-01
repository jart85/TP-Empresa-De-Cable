using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Empresa_De_Cable
{
    public class PaqueteBasico : Paquete
    {
        public override decimal PrecioMensual => base.PrecioMensual;

        public PaqueteBasico() : base() { canales = new List<Canal>(); }
        public PaqueteBasico(decimal PrecioBase) : base(PrecioBase)
        {
            
            precioBase = PrecioBase;
            canales = new List<Canal>();
        }
        ~PaqueteBasico(){ }

        public override string ToString()
        {
            return "Paquete Básico";
        }

    }
}
