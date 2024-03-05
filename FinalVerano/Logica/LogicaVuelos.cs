using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    public class LogicaVuelos
    {
        public static int AltaVuelo(Vuelos aVuelo) 
        {

            return PersistenciaVuelos.Alta(aVuelo);
        }
        public static string HacerCodigo(Vuelos aVuelito)
        {
            return aVuelito.CodigoVuelo = aVuelito.FechayHoraSalida.ToString() + aVuelito.AeropuertoSalida.CodigoAeropuerto;
        }
        public static List<Vuelos> ListarVuelosArribo(Aeropuerto codigo) 
        {
            return PersistenciaVuelos.ListarArribos(codigo);
        }
        public static List<Vuelos> ListarVuelosPartida(Aeropuerto codigo) 
        {
            return PersistenciaVuelos.ListarPartidas(codigo);
        }
        public static List<Vuelos> ListarTODO() 
        {
            return PersistenciaVuelos.ListarTODO();
        }
        public static Vuelos Buscar(string aCodigo)
        {
            return PersistenciaVuelos.Buscar(aCodigo);
        }

    }
}
