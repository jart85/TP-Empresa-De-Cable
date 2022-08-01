using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Empresa_De_Cable
{
    public static class DataBase
    {
        public static List<Cliente> listaClientes{get;set;}
        
        public static List<Serie> Series;
        public static List<Canal> Canales;
        public static List<Paquete> Paquetes;
       
        
        
        public static void Inicializar()
        {
            //Genero datos para recuperarlos posteriormente y probar con mayor comodidad la interfaz de usuario
  
            listaClientes = new List<Cliente>();
            listaClientes.Add(new Cliente(77888999, "Pedro", "Gonzalez", DateTime.Now.Date));
            listaClientes.Add(new Cliente(11222333, "Mariana", "Aletti", DateTime.Now.Date));
            listaClientes.Add(new Cliente(33222111, "Victoria", "Jure", DateTime.Now.Date));
            listaClientes.Add(new Cliente(26555444, "Pablo", "Lievernschraudt", DateTime.Now.Date));
            listaClientes.Add(new Cliente(24555672, "Martina", "Muñoz", DateTime.Now.Date));
            listaClientes.Add(new Cliente(30444553, "Mariano", "Achaval", DateTime.Now.Date));

            Canales = new List<Canal>();
            Canales.AddRange(new Canal[] {new Canal(1, "Cine y series"), new Canal(2,"MoviesMax"), new Canal(3,"zPremium"),
            new Canal(4,"Planeta Series"), new Canal(5,"Adventures & ++"),
            new Canal(6, "Somos"), new Canal(7, "TVP"), new Canal(8,"PequesTV")});

            Series = new List<Serie>();
            Series.AddRange(new Serie[] { new Serie("Un amor de primavera", new DateTime(2008, 5, 7)),
            new Serie("Atrapados en Katmandu", new DateTime(2013,8,20)),
            new Serie("Troubles in heaven", new DateTime(2015,6,17)),
            new Serie("Minnesota", new DateTime(2010,4,19)),
            new Serie("Soldiers", new DateTime(2009,5,22))});
            
            
            foreach (Serie s in Series)
            {
                int i;
                for(i=0; i<5; i++)
                {
                    s.AgregarTemporada(new Temporada(i+1));
                }

            }
           

            Series[0].AgregarEpisodio(new Temporada(1), new Episodio(1, "El encuentro", new TimeSpan(0, 45, 20)));
            Series[0].AgregarEpisodio(new Temporada(1), new Episodio(2, "El piano", new TimeSpan(0, 50, 20)));
            Series[1].AgregarEpisodio(new Temporada(1), new Episodio(1, "Comienza el viaje", new TimeSpan(1, 10, 36)));
            Series[1].AgregarEpisodio(new Temporada(1), new Episodio(1, "El gurka secreto", new TimeSpan(1, 9, 24)));
            Series[2].AgregarEpisodio(new Temporada(1), new Episodio(1, "Sorpresa!", new TimeSpan(0, 40, 16)));
            Series[2].AgregarEpisodio(new Temporada(1), new Episodio(1, "Un día inolvidable", new TimeSpan(0, 45, 20)));
            Series[3].AgregarEpisodio(new Temporada(1), new Episodio(1, "Bless you", new TimeSpan(1,0,20)));
            Series[3].AgregarEpisodio(new Temporada(1), new Episodio(1, "Mike´s", new TimeSpan(1,5,15)));

            Canales[0].AgregarSerie(Series[0]);
            Canales[1].AgregarSerie(Series[1]);
            Canales[2].AgregarSerie(Series[2]);
            Canales[3].AgregarSerie(Series[3]);
            Canales[4].AgregarSerie(Series[4]);
            
            Paquetes = new List<Paquete>();

            Paquetes.Add(new PaqueteBasico());
            Paquetes.Add(new PaqueteSilver());
            Paquetes.Add(new PaquetePremium());

            Paquetes[0].AgregarCanal(Canales[0]);
            Paquetes[1].AgregarCanal(Canales[1]);
            Paquetes[2].AgregarCanal(Canales[2]);
            Paquetes[2].AgregarCanal(Canales[3]);
            Paquetes[2].AgregarCanal(Canales[4]);


            
        }

        public static List<Cliente> RetornaClientes()
        {
            Cliente clienteAux;
            List<Cliente> clientes = new List<Cliente>();
            if(listaClientes.Count > 0)
            {
                foreach(Cliente c in listaClientes)
                {
                    clienteAux = new Cliente(c);

                    if(c.Plan != null)
                    {
                        if(c.Plan is PaqueteBasico)
                        {
                            clienteAux.Plan = new PaqueteBasico();
                        }
                        else if(c.Plan is PaqueteSilver)
                        {
                            clienteAux.Plan = new PaqueteSilver();
                        }
                        else
                        {
                            clienteAux.Plan = new PaquetePremium();
                        }
                    }
                    clientes.Add(clienteAux);
                  
                }
            }
            return clientes;
        
        }

        public static List<Canal> RetornaCanalesDisponibles()
        {   //Este método retorna los canales que no están contenidos en ningún paquete
            List<Canal> canalesDisponibles = new List<Canal>();
            if (Canales.Count > 0)
            {
                foreach(Canal s in Canales)
                {
                    if(Paquetes.Count > 0)
                    {
                        bool existe = false;
                        foreach(Paquete p in Paquetes)
                        {
                            if (p.ExisteCanal(s))
                            {
                                existe = true; 
                                break;
                            }
                   
                        }
                        if (!existe)
                        {
                            canalesDisponibles.Add(new Canal(s.Numero, s.Nombre));
                        }
                    }
                    else { canalesDisponibles.Add(s); }
                }
            }
            return canalesDisponibles;
            
        }
        public static List<Canal> RetornaCanales()
        {
            List<Canal> CanalesParaMostrar = new List<Canal>();
            if (Canales.Count > 0)
            {
                foreach (Canal c in Canales)
                {
                    Canal canalAux = new Canal();
                    canalAux.Numero = c.Numero;
                    canalAux.Nombre = c.Nombre;
                    CanalesParaMostrar.Add(canalAux);
                }
                
            }
            return CanalesParaMostrar;
        }

        public static List<Serie> RetornaSeriesDisponibles()
        {
            List<Serie> seriesDisponibles = new List<Serie>();

            if(Series.Count > 0)
            {
                foreach (Serie s in Series) //para cada serie
                {
                    bool disponible = true;
                    if(Canales.Count > 0)
                    {
                        foreach (Canal c in Canales) //verifico entre las series agregadas a cada canal
                        {
                            List<Serie> seriesAux = new List<Serie>();
                            seriesAux = c.RetornaSeries();
                            if (seriesAux.Exists(x => x.Nombre == s.Nombre && x.FechaLanzamiento == s.FechaLanzamiento))
                            {
                                disponible = false; //si en algún canal encuentro la serie, indico que no está disponible
                            }
                        }
                    }
                    if (disponible)
                    {   //Si la serie está disponible, la agrego a la lista de series disponibles.
                        seriesDisponibles.Add(new Serie(s.Nombre, s.FechaLanzamiento));
                    }

                }
            }
            return seriesDisponibles;
        }

        public static bool EliminarCanal(Canal canal)
        {
            return Canales.Remove(Canales.Find(x => x.Numero == canal.Numero));
        }

        internal static bool ModificarCanal(Canal canalModificado, Canal canal)
        {
            bool ok = true;
            if (Canales.Count > 0)
            {
                
                //recorro la lista de canales, comparo el canal modificado
                //con los canales distintos al canal original
                var consulta = from c in Canales
                               where c.Numero != canal.Numero &&
                               c.Nombre != canal.Nombre
                               select c;

                foreach (Canal c in consulta)
                {
                    if (c.Nombre == canalModificado.Nombre || c.Numero == canalModificado.Numero)
                    {
                        ok = false;
                        break;
                    }
                }

                if (ok)
                {
                    Canal canalAux = new Canal();
                    canalAux = Canales.Find(x => x.Numero == canal.Numero);
                    canalAux.Numero = canalModificado.Numero;
                    canalAux.Nombre = canalModificado.Nombre;
                }
            }
            return ok;
        }

        public static bool AsignarSerie(Canal canal, Serie serieDisponible)
        {
            bool asignada= true;
            if(ValidarAsignacionDeSerie(serieDisponible))
            {
                //Agregar serie al canal.
                //Tendría que buscar primero el canal y pasarlo a una variable, de forma simple esto sería..
                asignada= Canales.Find(x => x.Numero == canal.Numero).AgregarSerie(serieDisponible);

            }
            return asignada;
        }

        private static bool ValidarAsignacionDeSerie(Serie serieDisponible)
        {
            bool esValido = true;

            //si la serie ya se encuentra asignada a algún canal, no se asigna
            if (Canales.Count > 0)
            {
                foreach (Canal c in Canales)
                {
                    List<Serie> seriesAux = c.RetornaSeries();
                    if (seriesAux.Count > 0)
                    {
                        if (seriesAux.Exists(x => x.Nombre == serieDisponible.Nombre &&
                        x.FechaLanzamiento == serieDisponible.FechaLanzamiento))
                        {
                            esValido = false;

                        }
                    }
                }
            }
            return esValido;
        }

        internal static bool DesasignarSerie(Canal canal, Serie serie)
        {
            Canal canalAux = new Canal();
            canalAux = Canales.Find(x => x.Numero == canal.Numero);
            return canalAux.QuitarSerie(serie);
      
        }

        public static List<Serie> RetornaSeries()
        {
            List<Serie> seriesParaMostrar=new List<Serie>();

            if (Series.Count > 0)//si la cantidad de series es mayor a cero
            {

                foreach (Serie s in Series) //por cada serie de la lista
                {
                
                    Serie serieAux = new Serie(s.Nombre, s.FechaLanzamiento);
                
                    List<Temporada> temporadasAux = s.RetornaTemporadas(); //pido las temporadas
                    if (temporadasAux.Count > 0)//si hay temporadas
                    {

                        foreach (Temporada temp in temporadasAux)
                        {
                            Temporada temporadaAux = new Temporada(temp);

                            serieAux.AgregarTemporada(temp);//agrego la temporada a la serie

                            List<Episodio> episodiosAux = new List<Episodio>();
                            episodiosAux = temp.RetornaEpisodios();   //a la temporada en cuestion le pido los episodios

                            
                            if(episodiosAux.Count > 0)//si tiene episodios
                            {
                                
                                foreach (Episodio ep in episodiosAux) 
                                {
                                    Episodio episodioAux = new Episodio(ep);
                                    serieAux.AgregarEpisodio(temporadaAux, episodioAux);//a la serie le agrego los episodios
                                                                                        //que esa temporada tenga.
                                }
                            }
                            
                        }
                    }
                    seriesParaMostrar.Add(serieAux); //finalmente, agrego la serie a la lista que será devuelta por esta
                }                                    //funcion               
            }
            return seriesParaMostrar;
        }//fin de la función retorna series
        public static bool IngresarSerie(Serie serie)
        {
            bool ingresoOk = false;
            if (Series.Count > 0)
            {
                if (!Series.Exists(x => x.Nombre == serie.Nombre)) ;
                Series.Add(new Serie(serie));
               
            }
            return ingresoOk;
        }
        public static bool ModificarSerie(Serie serieOriginal, Serie serieModificada)
        {
            bool modificada = false;
            Serie serieAux;
            if(Series.Count > 0)
            {
                var query = Series.Where(x=>x.Nombre != serieOriginal.Nombre).ToList();
                if(!query.Exists(x=>x.Nombre == serieModificada.Nombre))
                {
                    serieAux = Series.Find(x => x.Nombre == serieOriginal.Nombre);
                    serieAux.Nombre = serieModificada.Nombre;
                    serieAux.FechaLanzamiento = serieModificada.FechaLanzamiento;
                    modificada = true;
                }
               
            }
            return modificada;
        }
       
           
    }
}
