using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using EntidadesCompartidas;
using System.Drawing;


public partial class VentaDePasajes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";
        if (!IsPostBack)
        {
            CargoDatos();
        }
    }

    private void CargoDatos()
    {
        try
        {
            List<Vuelos> colVuelos = LogicaVuelos.ListarTODO();
            Session["Flights"] = colVuelos;
            if (colVuelos.Count > 0)
            {
                ddlVuelo.DataSource = colVuelos;
                ddlVuelo.DataValueField = "codigoVuelo";
                ddlVuelo.DataBind();
                ddlVuelo.Items.Insert(0, new ListItem("---------"));

            }

            else
            {
                lblError.ForeColor = Color.Blue;
                lblError.Text = "No hay registros disponibles";
                ddlCliente.Enabled = false;
            }



            List<Cliente> colCliente = LogicaCliente.ListaCliente();
            Session["User"] = colCliente;
            if (colCliente.Count > 0)
            {
                ddlCliente.DataSource = colCliente;
                ddlCliente.DataValueField = "pasaporte";
                ddlCliente.DataBind();
                ddlCliente.Items.Insert(0, new ListItem("---------"));

            }

            else
            {
                lblError.ForeColor = Color.Blue;
                lblError.Text = "No hay registros disponibles";
            }

        }

        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }
    protected void btnCalcular_Click(object sender, EventArgs e)
    {
        try
        {
            string codigoVuelo = ddlVuelo.SelectedValue;
            List<Vuelos> colVuelo = (List<Vuelos>)Session["Flights"];
            if (ddlVuelo.SelectedIndex != 0)
                codigoVuelo = ddlVuelo.SelectedValue;
            else
                throw new Exception("Seleccione un vuelo");
            string pasaporte = ddlCliente.SelectedValue;
            List<Cliente>colClientes = (List<Cliente>)Session["User"];
            if(ddlCliente.SelectedIndex !=0)
                pasaporte = ddlCliente.SelectedValue;
            else
                throw new Exception("Seleccione el cliente");

                Vuelos vuelillo = LogicaVuelos.Buscar(codigoVuelo);
                Cliente clientito = LogicaCliente.Buscar(pasaporte);
                DateTime fechacompra = DateTime.Now;
                Pasaje pax = new Pasaje(0,vuelillo,clientito,fechacompra,0);
                if(pax.VueloPasaje.CodigoVuelo.Count() > vuelillo.Asientos)
                {
                   lblError.Text = "No quedan asientos disponibles para ese vuelo";
                }
                else
                    lblPrecio.Text = "El precio del pasaje que desea comprar es de: " + LogicaPasaje.Calcularprecio(pax);
           // pax = (Pasaje)Session["Pax"];
                

            }

        catch (Exception ex) 
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
            btnCompra.Enabled = false;
        }
        

    }
    protected void btnCompra_Click(object sender, EventArgs e)
    {
        try
        {
            //Pasaje pasajito = (Pasaje)Session["Pax"];
            //int numero = LogicaPasaje.Agregar(pasajito);
            string codigoVuelo = ddlVuelo.SelectedValue;
            List<Vuelos> colVuelo = (List<Vuelos>)Session["Flights"];
            if (ddlVuelo.SelectedIndex != 0)
                codigoVuelo = ddlVuelo.SelectedValue;
            else
                throw new Exception("Seleccione un vuelo");
            string pasaporte = ddlCliente.SelectedValue;
            List<Cliente>colClientes = (List<Cliente>)Session["Client"];
            if(ddlCliente.SelectedIndex !=0)
                pasaporte = ddlCliente.SelectedValue;
            else
                throw new Exception("Seleccione el cliente");

                Vuelos vuelillo = LogicaVuelos.Buscar(codigoVuelo);
                Cliente clientito = LogicaCliente.Buscar(pasaporte);
                DateTime fechacompra = DateTime.Now;
                int precio = 0;
                Pasaje pax = new Pasaje(0,vuelillo,clientito,fechacompra,precio);
                pax.Precio = LogicaPasaje.Calcularprecio(pax);
                int numero = LogicaPasaje.Agregar(pax);

                lblError.ForeColor = Color.Green;
                lblError.Text = "Pasaje cargado al usuario con exito";
        }
        

        catch(Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }
}