using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using EntidadesCompartidas;

namespace Persistencia
{
    public class PersistenciaCiudades
    {
        public static void Agregar(Ciudades aCiudad)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("AgregarCiudad", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@codigoCiudad", aCiudad.CodigoCiudad);
            oComando.Parameters.AddWithValue("@ciudadUbi", aCiudad.CiudadUbi);
            oComando.Parameters.AddWithValue("@paisUbi", aCiudad.PaisUbi);

            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(oRetorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oRetorno.Value;
                if (resultado == -1)
                    throw new Exception("Ya existe una ciudad con ese codigo");
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
        public static void Modificar(Ciudades aCiudad)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("ModificarCiudad", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@codigoCiudad", aCiudad.CodigoCiudad);
            oComando.Parameters.AddWithValue("@ciudadUbi", aCiudad.CiudadUbi);
            oComando.Parameters.AddWithValue("@paisUbi", aCiudad.PaisUbi);

            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(oRetorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oRetorno.Value;

                if (resultado == -1)
                    throw new Exception("No existe una ciudad con ese codigo");
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
        public static void Eliminar(Ciudades aCiudad)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("EliminarCiudad", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@codigoCiudad", aCiudad.CodigoCiudad);


            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(oRetorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oRetorno.Value;
                if (resultado == -1)
                    throw new Exception("No existe una ciudad con ese codigo");
                else if (resultado == -2)
                    throw new Exception("Esa ciudad tiene un aeropuerto, no se puede eliminar");
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
        public static Ciudades Buscar(string aCodigo)
        {
            string ciudad;
            string pais;
            Ciudades aCiudad = null;
            SqlDataReader oReader;

            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("BuscarCiudad", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;
            oComando.Parameters.AddWithValue("@codigoCiudad", aCodigo);

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();
                if (oReader.HasRows)
                {
                    if (oReader.Read())
                    {
                        ciudad = (string)oReader["ciudadUbi"];
                        pais = (string)oReader["paisUbi"];

                        aCiudad = new Ciudades(aCodigo, ciudad, pais);
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
            return aCiudad;
        }
    }
}
