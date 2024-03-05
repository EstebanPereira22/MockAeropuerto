using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntidadesCompartidas;
using Logica;
using System.Drawing;

public partial class HistoricoDeCompras : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    
    {
        lblerror.Text = "";
        try
        {

            if (!IsPostBack)
            {
                Cliente a = (Cliente)Session["User"];  
                List<Pasaje> colComp = LogicaPasaje.ListaPasajeCliente(a);
                   


                if (colComp.Count > 0)
                {
                    gvComp.DataSource = colComp;
                    gvComp.DataBind();
                }

                else
                {
                    gvComp.DataSource = null;
                    gvComp.DataBind();

                    throw new Exception("No hay companias registradas");
                }
            }

        }

        catch (Exception ex)
        {
            lblerror.ForeColor = Color.Red;
            lblerror.Text = ex.Message;
        }
    }
}