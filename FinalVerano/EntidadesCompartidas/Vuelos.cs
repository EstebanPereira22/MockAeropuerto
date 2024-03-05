using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    public class Vuelos
    {
        private string codigoVuelo;
        private Aeropuerto aeropuertoSalida;
        private Aeropuerto aeropuertoLlegada;
        DateTime fechayhorasalida;
        DateTime fechayhorallegada;
        private int precio;
        private int asientos;

        public string CodigoVuelo
        {
            get { return codigoVuelo; }
            set { codigoVuelo = value; }
        }
      
        public Aeropuerto AeropuertoSalida
        {
            get { return aeropuertoSalida; }

            set
            {
                if (value == null)
                    throw new Exception("Debe ingresar el codigo del aeropuerto de salida");

                aeropuertoSalida = value;
            }
        }
        public Aeropuerto AeropuertoLlegada 
        {
            get { return aeropuertoLlegada; }
            set 
            {
                if (value == null)
                    throw new Exception("Debe ingresar el codigo del aeropuerto de llegada");

                aeropuertoLlegada = value;
            }
        }
        public DateTime FechayHoraSalida
        {
            get { return fechayhorasalida; }
            set{ fechayhorasalida = value;}
        }
        public DateTime FechayHoraLlegada
        {
            get { return fechayhorallegada; }
            set
            {
                if (value < fechayhorasalida)
                {
                    throw new Exception("La fecha no puede ser anterior al dia y hora que salis");
                }
                else
                {
                    fechayhorallegada = value;
                }
            }

        }
        public int Precio
        {
            get { return precio; }
            set
            {
                if (value <= 0)
                {
                    throw new Exception("El precio no puede ser menor a 0");
                }

                else
                {
                    precio = value;
                }
            }
        }
        public int Asientos
        {
            get { return asientos; }
            set
            {
                if (value < 100)
                {
                    throw new Exception("El vuelo no puede tener menor de 100 asientos");
                }

                else if (value >300)
                {
                    throw new Exception("El vuelo no puede tener mas de 300 asientos");
                }

                else
                {
                    asientos = value;
                }
            }
        }
        public Vuelos(string pCodigoVuelo, Aeropuerto pAeropuertoSalida, Aeropuerto pAeropuertoLlegada, DateTime pFechayhorasalida,
            DateTime pFechayhorallegada, int pPrecio, int pAsientos) 
        {
            CodigoVuelo = pCodigoVuelo;
            AeropuertoSalida = pAeropuertoSalida;
            AeropuertoLlegada = pAeropuertoLlegada;
            FechayHoraSalida = pFechayhorasalida;
            FechayHoraLlegada = pFechayhorallegada;
            Precio = pPrecio;
            Asientos = pAsientos;
        }
        public override string ToString()
        {
            return ("Codigo de vuelo: " + CodigoVuelo + "/nAeropuerto Salida: " + AeropuertoSalida.CodigoAeropuerto +
                "/nAeropuerto Llegada: " + AeropuertoLlegada.CodigoAeropuerto + "/nFecha y hora de salida: " + FechayHoraSalida + "/nFecha y hora de llegada: "
                + FechayHoraLlegada + "/nPrecio: " + Precio + "/nAsientos: " + Asientos);
        }
    }


}
