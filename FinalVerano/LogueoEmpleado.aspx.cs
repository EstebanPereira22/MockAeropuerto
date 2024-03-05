using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using EntidadesCompartidas;

public partial class LogueoEmpleado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Employee"] = null;
    }
    protected void btnLog_Click(object sender, EventArgs e)
    {
        try
        {
            Empleado emplead = LogicaEmpleado.Logueo(txtUser.Text.Trim(), txtPass.Text.Trim());
            if (emplead != null)
            {
                Session["Employee"] = emplead;
                Response.Redirect("BienvenidaEmpleado.aspx");
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
