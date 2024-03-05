using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    public class LogicaCiudades
    {
        public static void Agregar(Ciudades cCiudad)
        {
            PersistenciaCiudades.Agregar(cCiudad);
        }

        public static void Modificar(Ciudades cCiudad)
        {
            PersistenciaCiudades.Modificar(cCiudad);
        }

        public static void Eliminar(Ciudades cCiudad)
        {
            PersistenciaCiudades.Eliminar(cCiudad);
        }

        public static Ciudades Buscar(string aCodigo)
        {
            return PersistenciaCiudades.Buscar(aCodigo);
        }
    }
}
