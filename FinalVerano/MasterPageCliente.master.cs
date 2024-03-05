using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntidadesCompartidas;

public partial class MasterPageCliente : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["User"] == null)
            {
                Response.Redirect("Default.aspx");
                
            }
            else lblUser.Text = "Usuario logueado: " + ((Cliente)Session["User"]).Nombre;
        }

        catch
        {
            Response.Redirect("Default.aspx");
        }
    }
}
