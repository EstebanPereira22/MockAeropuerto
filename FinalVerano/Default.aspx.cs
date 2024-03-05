using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntidadesCompartidas;
using Logica;
using System.Drawing;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         try
        {
            if (!IsPostBack)
            {
                Session["User"] = null;
                Session["Employee"] = null;

                CargoDatos();
            }
        }
        catch (Exception ex)
        {
            lblerror.Text = ex.Message;
        }

    }

    private void CargoDatos()
    {
        try
        {
            List<Aeropuerto> colAeropuertos = LogicaAeropuerto.ListarAeropuerto();
            Session["Airports"] = colAeropuertos;
            if (colAeropuertos.Count > 0)
            {
                ddlAeropuertos.DataSource = colAeropuertos;
                ddlAeropuertos.DataValueField = "codigoAeropuerto";
                ddlAeropuertos.DataBind();
                ddlAeropuertos.Items.Insert(0, new ListItem("---------"));

            }

            else
            {
                lblerror.ForeColor = Color.Blue;
                lblerror.Text = "No hay registros disponibles";
                
            }


        }

        catch (Exception ex)
        {
            lblerror.ForeColor = Color.Red;
            lblerror.Text = ex.Message;
        }
    }
    protected void  btnListar_Click(object sender, EventArgs e)
    {
        try
        {
            string codigoAeropuerto = ddlAeropuertos.SelectedValue;
            Aeropuerto aeropuertillo = LogicaAeropuerto.Buscar(codigoAeropuerto);
            List<Vuelos> listadepartidas = LogicaVuelos.ListarVuelosPartida(aeropuertillo);
            Session["Partidas"] = listadepartidas;
            if (listadepartidas.Count > 0)
            {
                gvPartidas.DataSource = listadepartidas;
                gvPartidas.DataBind();

            }
            else 
            {
                gvPartidas.DataSource = null;
                gvPartidas.DataBind();
                throw new Exception("No hay partidas");
            }
                
               
            List<Vuelos> listadearribos = LogicaVuelos.ListarVuelosArribo(aeropuertillo);
            Session["Arribos"] = listadearribos;
            if (listadearribos.Count > 0)
            {
                gvArribos.DataSource = listadearribos;
                gvArribos.DataBind();
            }

            else 
            {
                gvArribos.DataSource = null;
                gvArribos.DataBind();

                throw new Exception("No hay arribos");

            }
        }
            
                
           
        catch (Exception ex)
        {
            lblerror.ForeColor = Color.Red;
            lblerror.Text = ex.Message;
        }
    }
}
