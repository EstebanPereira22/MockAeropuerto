using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Logica;
using EntidadesCompartidas;

public partial class ABMCiudade : System.Web.UI.Page
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

        txtCodigo.Text = "";
        txtCodigo.Enabled = true;
        txtCiudad.Text = "";
        txtCiudad.Enabled = false;
        txtPais.Text = "";
        txtPais.Enabled = false;


    }
    private void ActivoBotones(bool esAlta = true)
    {
        btnModificar.Enabled = !esAlta;
        btnEliminar.Enabled = !esAlta;
        btnAgregar.Enabled = esAlta;
        btnBuscar.Enabled = false;

        txtCodigo.Enabled = false;
        txtCiudad.Enabled = true;
        txtPais.Enabled = true;
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            string aCodigo = txtCodigo.Text;

            Ciudades aCiudad = LogicaCiudades.Buscar(aCodigo);

            if (aCiudad == null)
            {
                lblError.ForeColor = Color.Red;
                lblError.Text = "No existe una ciudad con ese codigo";
                ActivoBotones();
                Session["City"] = null;
            }
            else
            {
                txtCodigo.Text = aCiudad.CodigoCiudad;
                txtCiudad.Text = aCiudad.CiudadUbi;
                txtPais.Text = aCiudad.PaisUbi;
                ActivoBotones(false);
                Session["City"] = aCiudad;
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
            Ciudades aCiudad = (Ciudades)Session["City"];
            LogicaCiudades.Eliminar(aCiudad);
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
            Ciudades aCiudad = (Ciudades)Session["City"];
            aCiudad.CodigoCiudad = txtCodigo.Text.Trim();
            aCiudad.CiudadUbi = txtCiudad.Text.Trim();
            aCiudad.PaisUbi = txtPais.Text.Trim();
            LogicaCiudades.Modificar(aCiudad);
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
            string codigo = txtCodigo.Text.Trim();
            string ciudad = txtCiudad.Text.Trim();
            string pais = txtPais.Text.Trim();

            Ciudades aCiudad = new Ciudades(codigo, ciudad, pais);
            LogicaCiudades.Agregar(aCiudad);

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