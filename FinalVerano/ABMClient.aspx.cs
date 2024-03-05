using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Logica;
using EntidadesCompartidas;

public partial class ABMClient : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";

        if (!IsPostBack)
            LimpioFormulario();
    }
    private void LimpioFormulario()
    {

        btnModificar.Enabled = false;
        btnEliminar.Enabled = false;
        btnAgregar.Enabled = false;
        btnBuscar.Enabled = true;

        txtPasaporte.Text = "";
        txtPasaporte.Enabled = true;
        txtNombre.Text = "";
        txtNombre.Enabled = false;
        txtTarjeta.Text = "";
        txtTarjeta.Enabled = false;
        txtContra.Text = "";
        txtContra.Enabled = false;


    }
    private void ActivoBotones(bool esAlta = true)
    {
        btnModificar.Enabled = !esAlta;
        btnEliminar.Enabled = !esAlta;
        btnAgregar.Enabled = esAlta;
        btnBuscar.Enabled = false;

        txtPasaporte.Enabled = false;
        txtNombre.Enabled = true;
        txtTarjeta.Enabled = true;
        txtContra.Enabled = true;
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            string aPasaporte = txtPasaporte.Text;

            Cliente aCliente = LogicaCliente.Buscar(aPasaporte);

            if (aCliente == null)
            {
                lblError.ForeColor = Color.Red;
                lblError.Text = "No hay registro de ese pasaporte en el sistema";
                ActivoBotones();
                Session["Client"] = null;
            }
            else
            {
                txtPasaporte.Text = aCliente.Pasaporte;
                txtNombre.Text = aCliente.Nombre;
                txtTarjeta.Text = aCliente.Tarjeta;
                txtContra.Text = aCliente.Contra;
                ActivoBotones(false);
                Session["Client"] = aCliente;
            }

        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = "Error: " + ex.Message;

        }
    }
    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        LimpioFormulario();
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            Cliente aCliente = (Cliente)Session["Client"];
            LogicaCliente.Eliminar(aCliente);
            lblError.ForeColor = Color.Green;
            lblError.Text = "Eliminación exitosa";

            LimpioFormulario();
        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }
    protected void btnModificar_Click(object sender, EventArgs e)
    {
        try
        {
            Cliente aCliente = (Cliente)Session["Client"];
            aCliente.Pasaporte = txtPasaporte.Text.Trim();
            aCliente.Nombre = txtNombre.Text.Trim();
            aCliente.Tarjeta = txtTarjeta.Text.Trim();
            aCliente.Contra = txtContra.Text.Trim();
            LogicaCliente.Modificar(aCliente);
            lblError.ForeColor = Color.Green;
            lblError.Text = "Modificación exitosa";
            LimpioFormulario();
        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        try
        {
            string pasaporte = txtPasaporte.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            string tarjeta = txtTarjeta.Text.Trim();
            string contra = txtContra.Text.Trim();

            Cliente aCliente = new Cliente(pasaporte, nombre, tarjeta, contra);
            LogicaCliente.Agregar(aCliente);

            lblError.ForeColor = Color.Green;
            lblError.Text = "Alta con exito";

            LimpioFormulario();
        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }
}