using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntidadesCompartidas;

public partial class MasterPageEmpleado : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Employee"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else lblUsuario.Text = "Usuario logueado: " + ((Empleado)Session["Employee"]).Usuario;
        }

        catch 
        {
            Response.Redirect("Default.aspx");
        }
    }
}
