using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    public class Cliente
    {
        private string pasaporte;
        private string nombre;
        private string tarjeta;
        private string contra;


        public string Pasaporte 
        {
            get { return pasaporte; }
            set 
            {
                if (value.Trim().Length != 32)
                {
                    throw new Exception("Los pasaportes tienen 32 caracteres compuesto de letras y numeros, ingrese el pasaporte nuevamente");
                }

                else pasaporte = value;
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
        public string Tarjeta
        {
            get { return tarjeta; }
            set
            {
                if (value.Trim().Length != 16)
                {
                    throw new Exception("Los tarjetas de credito tienen 16 caracteres numericos ingrese los datos nuevamente");
                }

                else tarjeta = value;
            }
        }
        public string Contra
        {
            get { return contra; }
            set
            {
                if (value.Trim() == "")
                {
                    throw new Exception("El campo contraseña no puede estar vacío");
                }

                else if (value.Trim().Length > 10)
                {
                    throw new Exception("El campo contraseña no puede exceder los 10 caracteres");
                }

                else contra = value;
            }
        }
        public Cliente(string aPasaporte, string aNombre, string aTarjeta, string aContra) 
        {
            Pasaporte = aPasaporte;
            Nombre = aNombre; ;
            Tarjeta = aTarjeta;
            Contra = aContra;
        }
        public override string ToString()
        {
            return ("Pasaporte: " + pasaporte + "/nNombre: " + nombre + "/nTarjeta: " + tarjeta +
                "/nContra: " + contra);
        }
    }
}
