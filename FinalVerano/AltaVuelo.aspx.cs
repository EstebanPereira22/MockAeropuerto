using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntidadesCompartidas;
using Logica;
using System.Drawing;

public partial class AltaVuelo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";

        if (!IsPostBack)
        {

            CargoDatos();

            txtFechaSalida.Attributes.Add("Type", "Date");
            txtHoraSalida.Attributes.Add("Type", "Time");
            txtFechaLlegada.Attributes.Add("Type", "Date");
            txtHoraLlegada.Attributes.Add("Type", "Time");

            txtFechaSalida.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtHoraSalida.Text = DateTime.Now.ToString("HH:mm");
            txtFechaLlegada.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtHoraLlegada.Text = DateTime.Now.ToString("HH:mm");

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
                ddlAeropuertoS.DataSource = colAeropuertos;
                ddlAeropuertoS.DataValueField = "codigoAeropuerto";
                ddlAeropuertoS.DataBind();
                ddlAeropuertoS.Items.Insert(0, new ListItem("---------"));

            }

            else
            {
                lblError.ForeColor = Color.Blue;
                lblError.Text = "No hay registros disponibles";
                ddlAeropuertoL.Enabled = false;
            }



            List<Aeropuerto> colAeropuertos2 = LogicaAeropuerto.ListarAeropuerto();
            Session["Airports2"] = colAeropuertos2;
            if (colAeropuertos.Count > 0)
            {
                ddlAeropuertoL.DataSource = colAeropuertos;
                ddlAeropuertoL.DataValueField = "codigoAeropuerto";
                ddlAeropuertoL.DataBind();
                ddlAeropuertoL.Items.Insert(0, new ListItem("---------"));

            }

            else
            {
                lblError.ForeColor = Color.Blue;
                lblError.Text = "No hay registros disponibles";
                ddlAeropuertoL.Enabled = false;
            }

        }

        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }
    protected void btnAlta_Click(object sender, EventArgs e)
    {
        try
        {
            string aeropuertoS = ddlAeropuertoS.SelectedValue;
            string aeropuertoL = ddlAeropuertoL.SelectedValue;
            DateTime fechaLlegada = Convert.ToDateTime(txtFechaLlegada.Text);
            int horaL = Convert.ToInt32(txtHoraLlegada.Text.Substring(0, 2));
            int minutoL = Convert.ToInt32(txtHoraLlegada.Text.Substring(3, 2));
            TimeSpan intervaloL = new TimeSpan(horaL, minutoL, 0);
            fechaLlegada = fechaLlegada.Date + intervaloL;

            DateTime fechaSalida = Convert.ToDateTime(txtFechaSalida.Text);
            int horaS = Convert.ToInt32(txtHoraSalida.Text.Substring(0, 2));
            int minutoS = Convert.ToInt32(txtHoraSalida.Text.Substring(3, 2));
            TimeSpan intervaloS = new TimeSpan(horaS, minutoS, 0);
            fechaSalida = fechaSalida.Date + intervaloS;

            int precio = Convert.ToInt32(txtPrecio.Text);
            int asientos = Convert.ToInt32(txtAsientos.Text);
            

            List<Aeropuerto> colAeropuerto = (List<Aeropuerto>)Session["Airports"];
            Aeropuerto aAero = null;
            if (ddlAeropuertoS.SelectedIndex != 0)
                aeropuertoS = ddlAeropuertoS.SelectedValue;
            else
                throw new Exception("Seleccione un aeropuerto");
            foreach (Aeropuerto a in colAeropuerto)
            {
                if (a.CodigoAeropuerto == aeropuertoS)
                {
                    aAero = a;
                    break;
                }
            }


            List<Aeropuerto> colAeropuerto2 = (List<Aeropuerto>)Session["Airports2"];
            Aeropuerto aAero2 = null;
            if (ddlAeropuertoL.SelectedIndex != 0)
                aeropuertoL = ddlAeropuertoL.SelectedValue;
            else
                throw new Exception("Seleccione un aeropuerto");
            foreach (Aeropuerto a in colAeropuerto)
            {
                if (a.CodigoAeropuerto == aeropuertoL)
                {
                    aAero2= a;
                    break;
                }
            }
            string codigovuelo = "autogenerado";
            Vuelos vuelillo = new Vuelos(codigovuelo,aAero, aAero2, fechaSalida, fechaLlegada, precio, asientos);
            vuelillo.CodigoVuelo = LogicaVuelos.HacerCodigo(vuelillo);
            int numero = LogicaVuelos.AltaVuelo(vuelillo);
            
            lblError.ForeColor = Color.Green;
            lblError.Text = "Vuelo agregado con exito";
        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }
}