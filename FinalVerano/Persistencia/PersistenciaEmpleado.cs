using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using EntidadesCompartidas;
namespace Persistencia
{
    public class PersistenciaEmpleado
    {
        public static Empleado Logueo(string eUsuario, string eContra)
        {
            string nombre;
            string labor;
            Empleado eEmpleado = null;
            SqlDataReader oReader;

            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("LogueoEmpleado", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;
            oComando.Parameters.AddWithValue("@usuario", eUsuario);
            oComando.Parameters.AddWithValue("@contra", eContra);

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();
                if (oReader.HasRows)
                {
                    if (oReader.Read())
                    {
                        nombre = (string)oReader["nombre"];
                        labor = (string)oReader["labor"];


                        eEmpleado = new Empleado(eUsuario,eContra, nombre, labor);
                    }
                }
                oReader.Close();
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            finally
            {
                oConexion.Close();
            }
            return eEmpleado;
        }
    }
}
