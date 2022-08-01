using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Empresa_De_Cable
{
    public class PaqueteSilver:Paquete
    {
        public override decimal PrecioMensual => PrecioMensual * (15/100+1);

        public PaqueteSilver() : base() { canales = new List<Canal>(); }
        public PaqueteSilver(decimal PrecioBase) : base(PrecioBase) {
            precioBase = PrecioBase;
            canales = new List<Canal>();
        }
        ~PaqueteSilver() { }
        public override string ToString()
        {
            return "Paquete Silver";
        }
    }
    
}
