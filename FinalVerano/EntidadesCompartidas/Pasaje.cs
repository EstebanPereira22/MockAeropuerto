using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    public class Pasaje
    {
        private int identificador;
        private Vuelos vueloPasaje;
        private Cliente clientePasaje;
        private DateTime fechaCompra;
        private int precio;

        public int Identificador
        {
            get { return identificador; }
            set { identificador = value; }
        }
        public Vuelos VueloPasaje 
        {
            get { return vueloPasaje; }

            set 
            {
                if (value == null)
                    throw new Exception("Debe ingresar el codigo del vuelo");

                vueloPasaje = value;
            }
        }
        public Cliente ClientePasaje
        {
            get { return clientePasaje; }

            set
            {
                if (value == null)
                    throw new Exception("Debe ingresar el nombre de la compania");

                clientePasaje = value;
            }
        }
        public DateTime FechaCompra
        {
            get { return fechaCompra; }
            set
            {               
                fechaCompra = value;
            }

        }
        public int Precio
        {
            get{return precio;}
            set { precio = value; }
        }
        public Pasaje(int aIdentificador,Vuelos codigoVuelo, Cliente Cliente, DateTime aFecha, int aPrecio) 
        {
            Identificador = aIdentificador;
            VueloPasaje = codigoVuelo;
            ClientePasaje = Cliente;
            FechaCompra = aFecha;
            Precio = aPrecio;
           
        }
        public override string ToString()
        {
            return ("Codigo Identificador: " + Identificador + "/nCodigo vuelo" + VueloPasaje.CodigoVuelo  + "/nPasaporte: " + ClientePasaje.Pasaporte + "/nFecha de compra: " + FechaCompra +
                "/nPrecio: "+ Precio);
        }
    }
}
