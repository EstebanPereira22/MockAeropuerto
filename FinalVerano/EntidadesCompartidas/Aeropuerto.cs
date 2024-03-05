using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EntidadesCompartidas
{
    public class Aeropuerto
    {
        private string codigoAeropuerto;
        private Ciudades ciudadAeropuerto;
        private string nombre;
        private string direccion;
        private int impuestoS;
        private int impuestoL;

        public string CodigoAeropuerto 
        {
            get { return codigoAeropuerto; }
            set 
            {
                if (!Regex.IsMatch(value, @"^[a-zA-Z]{3}$"))
                {
                    throw new Exception("El código de los aeropuertos debe estar compuesto por 3 letras");
                }

                else codigoAeropuerto = value;
            } 
        }
        public Ciudades CiudadAeropuerto 
        {
            get { return ciudadAeropuerto; }
            set 
            {
                if (value == null)
                    throw new Exception("Debe ingresar el codigo de la ciudad");

                else ciudadAeropuerto = value;
            }
        }
        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (value.Trim() == "")
                {
                    throw new Exception("El campo nombre no puede estar vacia");
                }

                else if (value.Trim().Length > 50)
                {
                    throw new Exception("El campo nombre no puede exceder los 50 caracteres");
                }

                else nombre = value;
            }
        }
        public string Direccion
        {
            get { return direccion; }
            set
            {
                if (value.Trim() == "")
                {
                    throw new Exception("La direccion no puede estar vacía");
                }

                else if (value.Trim().Length > 50)
                {
                    throw new Exception("El campo direccion no puede exceder los 50 caracteres");
                }

                else direccion = value;
            }
        }
        public int ImpuestoS
        {
            get { return impuestoS; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("El importe del impuesto no puede ser menor a 0");
                }
                else  impuestoS = value;
                
            }
        }
        public int ImpuestoL
        {
            get { return impuestoL; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("El importe del impuesto no puede ser menor a 0");
                }
                else impuestoL = value;

            }
        }
        public Aeropuerto(string aCodigoAeropuerto, Ciudades Ciudad, string aNombre, string aDireccion,
            int aImpuestoS, int aImpuestoL) 
        {
            CodigoAeropuerto = aCodigoAeropuerto;
            CiudadAeropuerto = Ciudad;
            Nombre = aNombre;
            Direccion = aDireccion;
            ImpuestoS = aImpuestoS;
            ImpuestoL = aImpuestoL;
        }
        public override string ToString()
        {
            return ("Codigo Aeropuerto: " + codigoAeropuerto + "/nCodigo de la Ciudad: " + ciudadAeropuerto.CodigoCiudad + "/nNombre del Aeropuerto: " + nombre +
                "/nDireccion: "+ direccion +  "Impuesto del aeropuerto de salida: " + impuestoS + "Impuesto del aeropuerto de llegada: "
                + impuestoL);
        }

    }
}
