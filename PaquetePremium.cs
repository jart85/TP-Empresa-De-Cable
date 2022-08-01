using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Empresa_De_Cable
{
    public class PaquetePremium: Paquete
    {
        public override decimal PrecioMensual => precioBase * (1/20+1);
        public PaquetePremium() : base() { canales = new List<Canal>(); }
        public PaquetePremium(decimal PrecioBase): base(PrecioBase)
        {
            precioBase = PrecioBase;
            canales = new List<Canal>();
        }
        ~PaquetePremium() { }
        public override string ToString()
        {
            return "Paquete Premium";
        }
    }

}
