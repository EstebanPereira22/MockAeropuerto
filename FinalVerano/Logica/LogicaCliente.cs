using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    public class LogicaCliente
    {
       
        public static void Agregar(Cliente cCliente) 
        {
            PersistenciaCliente.Agregar(cCliente);
        }
        public static void Modificar(Cliente cCliente) 
        {
            PersistenciaCliente.Modificar(cCliente);
        }
        public static void Eliminar(Cliente cCliente) 
        {
            PersistenciaCliente.Eliminar(cCliente);
        }
        public static Cliente Buscar(string cPasaporte) 
        {
            return PersistenciaCliente.Buscar(cPasaporte);
        }
        public static Cliente Logueo(string cPasaporte,string cContra)
        {
            Cliente cCliente = null;
            if (cCliente == null)
                cCliente = PersistenciaCliente.Logueo(cPasaporte, cContra);

            return cCliente;
        }
        public static List<Cliente> ListaCliente() 
        {
            return PersistenciaCliente.ListarCliente();
        }
    }
}
