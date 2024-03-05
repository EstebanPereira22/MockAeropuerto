using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EntidadesCompartidas
{
    public class Ciudades
    {
        private string codigoCiudad;
        private string ciudadUbi;
        private string paisUbi;


        public string CodigoCiudad 
        {
            get { return codigoCiudad; }
            set 
            {
                if (!Regex.IsMatch(value, @"^[a-zA-Z]{6}$"))
                {
                    throw new Exception("El codigo de la ciudad debe tener 6 letras");
                }

                else codigoCiudad = value;
            }
        }
        public string CiudadUbi
        {
            get { return ciudadUbi; }
            set
            {
                if (value.Trim() == "")
                {
                    throw new Exception("El campo ciudad no puede estar vacío");
                }

                else if (value.Trim().Length > 50)
                {
                    throw new Exception("El campo ciudad no puede exceder los 50 caracteres");
                }

                else ciudadUbi = value;
            }
        }
        public string PaisUbi
        {
            get { return paisUbi; }
            set
            {
                if (value.Trim() == "")
                {
                    throw new Exception("El campo pais no puede estar vacío");
                }

                else if (value.Trim().Length > 50)
                {
                    throw new Exception("El campo pais no puede exceder los 50 caracteres");
                }

                else paisUbi = value;
            }
        }
        public Ciudades(string pCodigo, string pCiudad, string pPais) 
        {
            CodigoCiudad = pCodigo;
            CiudadUbi = pCiudad;
            PaisUbi = pPais;
        }
        public override string ToString()
        {
            return("Codigo: " + CodigoCiudad + "/nCiudad " + CiudadUbi + "/nPais " + PaisUbi);
        }

    }
}
