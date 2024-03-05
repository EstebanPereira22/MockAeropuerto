using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    public class LogicaPasaje
    {
        public static List<Pasaje> ListaPasajeCliente(Cliente pCliente)
        {

            return PersistenciaPasaje.ListaPasajeCliente(pCliente);
        }

        public static int Agregar(Pasaje pPax)
        {
            return PersistenciaPasaje.Agregar(pPax);
        }
    
        public static int Calcularprecio(Pasaje ppe)
        {
            return ppe.Precio = ppe.VueloPasaje.AeropuertoSalida.ImpuestoS + ppe.VueloPasaje.AeropuertoLlegada.ImpuestoL + ppe.VueloPasaje.Precio;
        }
        //("en la logica de alta pasaje va a ir el control de la fecha")
    }
}
