using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using EntidadesCompartidas;

namespace Persistencia
{
    public class PersistenciaCliente
    {
        public static void Agregar(Cliente aCliente)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("AgregarCliente", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@pasaporte", aCliente.Pasaporte);
            oComando.Parameters.AddWithValue("@nombre", aCliente.Nombre);
            oComando.Parameters.AddWithValue("@tarjeta", aCliente.Tarjeta);
            oComando.Parameters.AddWithValue("@contra", aCliente.Contra);

            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(oRetorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oRetorno.Value;
                if (resultado == -1)
                    throw new Exception("Pasaporte ya registrado");
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
        public static void Modificar(Cliente aCliente)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("ModificarCliente", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@pasaporte", aCliente.Pasaporte);
            oComando.Parameters.AddWithValue("@nombre", aCliente.Nombre);
            oComando.Parameters.AddWithValue("@tarjeta", aCliente.Tarjeta);
            oComando.Parameters.AddWithValue("@contra", aCliente.Contra);

            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(oRetorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oRetorno.Value;

                if (resultado == -1)
                    throw new Exception("No hay cliente registrado con ese pasaporte");
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
        public static void Eliminar(Cliente aCliente)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("EliminarCliente", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@pasaporte", aCliente.Pasaporte);


            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(oRetorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oRetorno.Value;
                if (resultado == -1)
                    throw new Exception("No hay cliente registrado con ese pasaporte");
                else if (resultado == -2)
                    throw new Exception("Este cliente tiene un pasaje activo, no se puede eliminar");
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
        public static Cliente Buscar(string aPasaporte)
        {
            string nombre;
            string tarjeta;
            string contra;
            Cliente aCliente = null;
            SqlDataReader oReader;

            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("BuscarCliente", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;
            oComando.Parameters.AddWithValue("@pasaporte", aPasaporte);

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();
                if (oReader.HasRows)
                {
                    if (oReader.Read())
                    {
                        nombre = (string)oReader["nombre"];
                        tarjeta = (string)oReader["tarjeta"];
                        contra = (string)oReader["contra"];

                        aCliente = new Cliente(aPasaporte, nombre, tarjeta,contra);
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
            return aCliente;
        }
        public static Cliente Logueo(string aPasaporte,string aContra)
        {
            string nombre;
            string tarjeta;
            Cliente aCliente = null;
            SqlDataReader oReader;

            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("LogueoCliente", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;
            oComando.Parameters.AddWithValue("@pasaporte", aPasaporte);
            oComando.Parameters.AddWithValue("@contra", aContra);

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();
                if (oReader.HasRows)
                {
                    if (oReader.Read())
                    {
                        nombre = (string)oReader["nombre"];
                        tarjeta = (string)oReader["tarjeta"];
                        

                        aCliente = new Cliente(aPasaporte, nombre, tarjeta, aContra);
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
            return aCliente;
        }
        public static List<Cliente> ListarCliente() 
        {
            string pasaporte,nombre,tarjeta,contra;
            List<Cliente> clientes = new List<Cliente>();
            SqlDataReader oReader;
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("ListaClientes", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        pasaporte = (string)oReader["pasaporte"];
                        nombre = (string)oReader["nombre"];
                        tarjeta = (string)oReader["tarjeta"];
                        contra = (string)oReader["contra"];

                        Cliente cCliente = new Cliente(pasaporte,nombre,tarjeta,contra);
                        clientes.Add(cCliente);
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

            return clientes;
        }
    }
}
