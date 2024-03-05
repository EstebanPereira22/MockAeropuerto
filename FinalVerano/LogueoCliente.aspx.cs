using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using Logica;
using EntidadesCompartidas;

public partial class LogueoCliente : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";
    }
    protected void btnLog_Click(object sender, EventArgs e)
    {
        try 
        {
            Cliente client = LogicaCliente.Logueo(txtUser.Text.Trim(), txtPass.Text.Trim());
            if (client != null)
            {
                Session["User"] = client;
                Response.Redirect("HistoricoDeCompras.aspx");
            }
            else
                lblError.Text = "Datos incorrectos";
        }

        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}