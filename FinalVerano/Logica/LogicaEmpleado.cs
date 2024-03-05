using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
   public class LogicaEmpleado
    {
        public static Empleado Logueo(string eUsuario, string eContra)
        {
            Empleado eEmpleado = null;
            if (eEmpleado == null)
                eEmpleado = PersistenciaEmpleado.Logueo(eUsuario, eContra);

            return eEmpleado;
        }
    }
}
