using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    public class Empleado
    {
        private string usuario;
        private string contra;
        private string nombre;
        private string labor;


        public string Usuario 
        {
            get { return usuario; }
            set 
            {
                if (value.Trim().Length > 50 || value.Trim().Length == 0)
                {
                    throw new Exception("Error en el formato usuario");
                }

                else usuario = value;
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
        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (value.Trim() == "")
                {
                    throw new Exception("El campo nombre no puede estar vacio");
                }

                else if (value.Trim().Length > 50)
                {
                    throw new Exception("El campo nombre no puede exceder los 50 caracteres");
                }

                else nombre = value;
            }
        }
        public string Labor
        {
            get { return labor; }
            set
            {
                string valor = value.Trim().ToLower();
                if (valor != "vendedor" && valor != "gerente" && valor != "admin")
                {
                    throw new Exception("Error en el formato del campo labor");
                }
                else labor = value;
               
            }
        }
        public Empleado(string eUsuario, string eContra, string eNombre, string eLabor) 
        {
            Usuario = eUsuario;
            Contra = eContra;
            Nombre = eNombre;
            Labor = eLabor;
        }
        public override string ToString()
        {
            return ("Usuario: " + Usuario  + "/nContraseña: " + Contra +"/nNombre: "+ Nombre + "/nLabor: " + Labor); 
        }
    }
}
