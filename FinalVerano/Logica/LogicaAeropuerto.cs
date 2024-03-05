using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    public class LogicaAeropuerto
    {
        public static void Agregar(Aeropuerto aAeropuerto)
        {
            PersistenciaAeropuertos.Agregar(aAeropuerto);
        }
        public static void Modificar(Aeropuerto aAeropuerto)
        {
            PersistenciaAeropuertos.Modificar(aAeropuerto);
        }
        public static void Eliminar(Aeropuerto aAeropuerto)
        {
            PersistenciaAeropuertos.Eliminar(aAeropuerto);
        }
        public static Aeropuerto Buscar(string aCodigo)
        {
            return PersistenciaAeropuertos.Buscar(aCodigo);
        }
        public static List<Aeropuerto> ListarAeropuerto()
        {
            return PersistenciaAeropuertos.ListarAeropuerto();


        }
    }
}
