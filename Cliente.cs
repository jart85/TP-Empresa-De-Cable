using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Empresa_De_Cable
{
    public class Cliente
    {
        public int DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime fechaNacimiento { get; set; }

        public Paquete Plan { get; set; }
        

        public Cliente() { }
        public Cliente(int dNI, string nombre, string apellido, DateTime fechaNacimiento)
        {
            DNI = dNI;
            Nombre = nombre;
            Apellido = apellido;
            this.fechaNacimiento = fechaNacimiento;
            

        }

        public Cliente(Cliente cliente)
        {
            this.DNI = cliente.DNI;
            this.Nombre = cliente.Nombre;
            this.Apellido = cliente.Apellido;
            this.fechaNacimiento = cliente.fechaNacimiento;

        }

        ~Cliente() { }
        
    }
}
