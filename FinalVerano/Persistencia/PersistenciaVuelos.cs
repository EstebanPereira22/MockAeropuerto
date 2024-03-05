using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using EntidadesCompartidas;

namespace Persistencia
{
    public class PersistenciaVuelos
    {
        public static int Alta(Vuelos aVuelo)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("AltaVuelos", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@codigoVuelo", aVuelo.CodigoVuelo);
            oComando.Parameters.AddWithValue("@codigoAeropuertoS", aVuelo.AeropuertoSalida.CodigoAeropuerto);
            oComando.Parameters.AddWithValue("@codigoAeropuertoL", aVuelo.AeropuertoLlegada.CodigoAeropuerto);
            oComando.Parameters.AddWithValue("@fechayhorasalida", aVuelo.FechayHoraSalida);
            oComando.Parameters.AddWithValue("@fechayhorallegada", aVuelo.FechayHoraLlegada);
            oComando.Parameters.AddWithValue("@precio", aVuelo.Precio);
            oComando.Parameters.AddWithValue("@asientos", aVuelo.Asientos);

            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(oRetorno);
            int resultado = 0;

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                resultado = (int)oRetorno.Value;

                if (resultado == -1)
                {
                    throw new Exception("La fecha de salida del viaje debe ser posterior al dia de hoy");
                }

                else if (resultado == -2)
                {
                    throw new Exception("El codigo de su aeropuerto de salida no existe");
                }

                else if (resultado == -3)
                {
                    throw new Exception("El codigo de su aeropuerto de llegada no existe");
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
        public static List<Vuelos> ListarArribos(Aeropuerto codigoAeropuerto)
        {
            string codigoVuelo;
            string aeropuertoSalida;
            DateTime fechayhoraSalida;
            DateTime fechayhoraLlegada;
            int precio, asientos;
            List<Vuelos> vuelos = new List<Vuelos>();
            SqlDataReader oReader;
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("Arribos", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;
            oComando.Parameters.AddWithValue("@codigoAeropuertoA", codigoAeropuerto.CodigoAeropuerto);

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();

                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        codigoVuelo = (string)oReader["codigoVuelo"];
                        aeropuertoSalida = (string)oReader["codigoAeropuertoSalida"];
                        Aeropuerto codigoSalida = PersistenciaAeropuertos.Buscar(aeropuertoSalida);
                        fechayhoraSalida = Convert.ToDateTime(oReader["fechayhorasalida"]);
                        fechayhoraLlegada = Convert.ToDateTime(oReader["fechayhorallegada"]);
                        precio = Convert.ToInt32(oReader["precio"]);
                        asientos = Convert.ToInt32(oReader["asientos"]);

                        Vuelos vVuelo = new Vuelos(codigoVuelo, codigoSalida, codigoAeropuerto, fechayhoraSalida, fechayhoraLlegada,
                            precio, asientos);
                        vuelos.Add(vVuelo);
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

            return vuelos;
        }
        public static List<Vuelos> ListarPartidas(Aeropuerto codigoAeropuerto)
        {
            string codigoVuelo;
            string aeropuertoLlegada;
            DateTime fechayhoraSalida;
            DateTime fechayhoraLlegada;
            int precio, asientos;
            List<Vuelos> vuelos = new List<Vuelos>();
            SqlDataReader oReader;
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("Partidas", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;
            oComando.Parameters.AddWithValue("@codigoAeropuertoS", codigoAeropuerto.CodigoAeropuerto);

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();

                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        codigoVuelo = (string)oReader["codigoVuelo"];
                        aeropuertoLlegada = (string)oReader["codigoAeropuertoLlegada"];
                        Aeropuerto codigoLlegada = PersistenciaAeropuertos.Buscar(aeropuertoLlegada);
                        fechayhoraSalida = Convert.ToDateTime(oReader["fechayhorasalida"]);
                        fechayhoraLlegada = Convert.ToDateTime(oReader["fechayhorallegada"]);
                        precio = Convert.ToInt32(oReader["precio"]);
                        asientos = Convert.ToInt32(oReader["asientos"]);

                        Vuelos vVuelo = new Vuelos(codigoVuelo, codigoAeropuerto, codigoLlegada, fechayhoraSalida, fechayhoraLlegada,
                            precio, asientos);
                        vuelos.Add(vVuelo);
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

            return vuelos;
        }
        public static Vuelos Buscar(string codigoVuelo) 
        {
            string codigoaeropuertoS,codigoaeropuertoL;
            DateTime fechayhorasalida;
            DateTime fechayhorallegada;
            int precio, asientos;
            Vuelos aVuelo = null;
            SqlDataReader oReader;

            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("BuscarVuelo", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;
            oComando.Parameters.AddWithValue("@codigoVuelo", codigoVuelo);

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();
                if (oReader.HasRows) 
                {
                    if (oReader.Read()) 
                    {
                        codigoaeropuertoS = (string)oReader["codigoAeropuertoSalida"];
                        Aeropuerto aeropuertoS = PersistenciaAeropuertos.Buscar(codigoaeropuertoS);
                        codigoaeropuertoL = (string)oReader["codigoAeropuertoLlegada"];
                        Aeropuerto aeropuertoL = PersistenciaAeropuertos.Buscar(codigoaeropuertoL);
                        fechayhorasalida = Convert.ToDateTime(oReader["fechayhorasalida"]);
                        fechayhorallegada = Convert.ToDateTime(oReader["fechayhorallegada"]);
                        precio = Convert.ToInt32(oReader["precio"]);
                        asientos = Convert.ToInt32(oReader["asientos"]);

                        aVuelo = new Vuelos(codigoVuelo, aeropuertoS, aeropuertoL, fechayhorasalida, fechayhorasalida,
                            precio, asientos);
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
            return aVuelo;
        
        }
        public static List<Vuelos> ListarTODO() 
        {

            string codigoVuelo;
            string aeropuertoSalida;
            string aeropuertoLlegada;
            DateTime fechayhoraSalida;
            DateTime fechayhoraLlegada;
            int precio, asientos;
            List<Vuelos> vuelos = new List<Vuelos>();
            SqlDataReader oReader;
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("ListaVuelos", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        codigoVuelo = (string)oReader["codigoVuelo"];
                        aeropuertoSalida = (string)oReader["codigoAeropuertoSalida"];
                        Aeropuerto codigoSalida = PersistenciaAeropuertos.Buscar(aeropuertoSalida);
                        aeropuertoLlegada = (string)oReader["codigoAeropuertoLlegada"];
                        Aeropuerto codigoLlegada = PersistenciaAeropuertos.Buscar(aeropuertoLlegada);
                        fechayhoraSalida = Convert.ToDateTime(oReader["fechayhorasalida"]);
                        fechayhoraLlegada = Convert.ToDateTime(oReader["fechayhorallegada"]);
                        precio = Convert.ToInt32(oReader["precio"]);
                        asientos = Convert.ToInt32(oReader["asientos"]);

                        Vuelos vVuelo = new Vuelos(codigoVuelo, codigoSalida, codigoLlegada, fechayhoraSalida, fechayhoraLlegada,
                            precio, asientos);
                        vuelos.Add(vVuelo);
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

            return vuelos;
        }

    }
}
