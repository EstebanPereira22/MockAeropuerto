using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using EntidadesCompartidas;

namespace Persistencia
{
    public class PersistenciaPasaje
    {
        public static List<Pasaje> ListaPasajeCliente(Cliente pCliente) 
        {
            int identificador,precio;
            DateTime fechacompra;
            string codigovuelo;
            List<Pasaje> aPasaje = new List<Pasaje>();
            SqlDataReader oReader;
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("HistoricoDeCompras", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@pasaporte", pCliente.Pasaporte);


            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        identificador = Convert.ToInt32(oReader["identificador"]);
                        codigovuelo = (string)oReader["codigoVuelo"];
                        Vuelos avuelo = PersistenciaVuelos.Buscar(codigovuelo);
                        fechacompra = Convert.ToDateTime(oReader["fechacompra"]);
                        precio = Convert.ToInt32(oReader["precio"]);
                        
               
                        Pasaje bPasaje = new Pasaje(identificador,avuelo, pCliente, fechacompra, precio);

                        aPasaje.Add(bPasaje);

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
            return aPasaje;
        }
        public static int Agregar(Pasaje pPasaje)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("VentaPax", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@codigoVuelo", pPasaje.VueloPasaje.CodigoVuelo);
            oComando.Parameters.AddWithValue("@pasaporte", pPasaje.ClientePasaje.Pasaporte);
            oComando.Parameters.AddWithValue("@fechacompra", pPasaje.FechaCompra);
            oComando.Parameters.AddWithValue("@precio", pPasaje.VueloPasaje.AeropuertoLlegada.ImpuestoL + pPasaje.VueloPasaje.AeropuertoSalida.ImpuestoS + 
                pPasaje.VueloPasaje.Precio);


            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(oRetorno);
            //en este caso lo definimos fuera del try para poder devolverlo

            int resultado = 0;

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                resultado = (int)oRetorno.Value;

                if (resultado == -1)
                {
                    throw new Exception("No existe cliente con ese pasaporte");
                }

                else if (resultado == -2)
                {
                    throw new Exception("No existe aeropuerto con ese codigo");
                }

                else if (resultado == -3)
                {
                    throw new Exception("Error al procesar la fecha de compra");
                }

                else if (resultado == -4)
                {
                    throw new Exception("Ocurrio un error inesperado");
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            finally
            {
                oConexion.Close();
            }

            return resultado;
        }
    }
}
