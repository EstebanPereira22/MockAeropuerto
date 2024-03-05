using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using EntidadesCompartidas;

namespace Persistencia
{
    public class PersistenciaAeropuertos
    {
        public static void Agregar(Aeropuerto aAeropuerto)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("AgregarAeropuerto", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@codigoAeropuerto", aAeropuerto.CodigoAeropuerto);
            oComando.Parameters.AddWithValue("@codigoCiudad", aAeropuerto.CiudadAeropuerto.CodigoCiudad);
            oComando.Parameters.AddWithValue("@nombre", aAeropuerto.Nombre);
            oComando.Parameters.AddWithValue("@direccion", aAeropuerto.Direccion);
            oComando.Parameters.AddWithValue("@impuestoS", aAeropuerto.ImpuestoS);
            oComando.Parameters.AddWithValue("@impuestoL", aAeropuerto.ImpuestoL);

            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(oRetorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oRetorno.Value;
                if (resultado == -1)
                    throw new Exception("Aeropuerto ya registrado");
                else if (resultado == -2)
                    throw new Exception("Ocurrio un error inesperado");

            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            finally
            {
                oConexion.Close();
            }
        }
        public static void Modificar(Aeropuerto aAeropuerto)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("ModificarAeropuerto", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@codigoAeropuerto", aAeropuerto.CodigoAeropuerto);
            oComando.Parameters.AddWithValue("@codigoCiudad", aAeropuerto.CiudadAeropuerto.CodigoCiudad);
            oComando.Parameters.AddWithValue("@nombre", aAeropuerto.Nombre);
            oComando.Parameters.AddWithValue("@direccion", aAeropuerto.Direccion);
            oComando.Parameters.AddWithValue("@impuestoS", aAeropuerto.ImpuestoS);
            oComando.Parameters.AddWithValue("@impuestoL", aAeropuerto.ImpuestoL);

            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(oRetorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oRetorno.Value;

                if (resultado == -1)
                    throw new Exception("No hay Aeropuerto registrado con ese codigo");
                else if (resultado == -2)
                    throw new Exception("Ocurrio un error inesperado");

            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            finally
            {
                oConexion.Close();
            }
        }
        public static void Eliminar(Aeropuerto aAeropuerto)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("EliminarAeropuerto", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@codigoAeropuerto", aAeropuerto.CodigoAeropuerto);


            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(oRetorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oRetorno.Value;
                if (resultado == -1)
                    throw new Exception("No hay aeropuerto registrado con ese codigo");
                else if (resultado == -2)
                    throw new Exception("Este Aeropuerto tiene vuelos asociados, no se puede eliminar");
                else if (resultado == -3)
                    throw new Exception("Ocurrio un error inesperado");

            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            finally
            {
                oConexion.Close();
            }
        }
        public static Aeropuerto Buscar(string aCodigo)
        {
            string codigoCiudad;
            string nombre;
            string direccion;
            int impuestoS;
            int impuestoL;
            Aeropuerto aAeropuerto = null;
            SqlDataReader oReader;

            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("BuscarAeropuerto", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;
            oComando.Parameters.AddWithValue("@codigoAeropuerto", aCodigo);

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();
                if (oReader.HasRows)
                {
                    if (oReader.Read())
                    {
                        codigoCiudad = (string)oReader["codigoCiudad"] ;
                        Ciudades citity = PersistenciaCiudades.Buscar(codigoCiudad);
                        nombre = (string)oReader["nombre"];
                        direccion = (string)oReader["direccion"];
                        impuestoS = Convert.ToInt32(oReader["impuestoS"]);
                        impuestoL = Convert.ToInt32(oReader["impuestoL"]);

                        aAeropuerto = new Aeropuerto(aCodigo, citity,nombre, direccion,impuestoS,impuestoL);
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
            return aAeropuerto;
        }
        public static List<Aeropuerto> ListarAeropuerto() 
        {
             string codigoAeropuerto,nombre,direccion;
             string ciudadAeropuerto;
             int impuestoS,impuestoL;
            List<Aeropuerto> aAero = new List<Aeropuerto>();
            SqlDataReader oReader;
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("ListaAeropuerto", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        codigoAeropuerto = (string)oReader["codigoAeropuerto"];
                        ciudadAeropuerto = (string)oReader["codigoCiudad"];
                        Ciudades codigoCiudad = PersistenciaCiudades.Buscar(ciudadAeropuerto);
                        nombre = (string)oReader["nombre"];
                        direccion = (string)oReader["direccion"];
                        impuestoS = Convert.ToInt32(oReader["impuestoS"]);
                        impuestoL = Convert.ToInt32(oReader["impuestoL"]);

                        Aeropuerto bAero = new Aeropuerto(codigoAeropuerto, codigoCiudad, nombre, direccion, impuestoS, impuestoL);
                        aAero.Add(bAero);
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
            return aAero;

        }

    }
}
