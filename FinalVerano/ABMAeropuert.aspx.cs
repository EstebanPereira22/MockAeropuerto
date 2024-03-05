using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Logica;
using EntidadesCompartidas;

public partial class ABMAeropuert : System.Web.UI.Page
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

        txtCodigoAeropuerto.Text = "";
        txtCodigoAeropuerto.Enabled = true;
        txtCodigoCiudadA.Text = "";
        txtCodigoCiudadA.Enabled = false;
        txtNombre.Text = "";
        txtNombre.Enabled = false;
        txtDireccion.Text = "";
        txtDireccion.Enabled = false;
        txtImpuestoS.Text = "";
        txtImpuestoS.Enabled = false;
        txtImpuestoL.Text = "";
        txtImpuestoL.Enabled = false;


    }
    private void ActivoBotones(bool esAlta = true)
    {
        btnModificar.Enabled = !esAlta;
        btnEliminar.Enabled = !esAlta;
        btnAgregar.Enabled = esAlta;
        btnBuscar.Enabled = false;

        txtCodigoAeropuerto.Enabled = false;
        txtCodigoCiudadA.Enabled = true;
        txtNombre.Enabled = true;
        txtDireccion.Enabled = true;
        txtImpuestoS.Enabled = true;
        txtImpuestoL.Enabled = true;
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            string aCodigo = txtCodigoAeropuerto.Text;

            Aeropuerto aAeropuerto = LogicaAeropuerto.Buscar(aCodigo);

            if (aAeropuerto == null)
            {
                lblError.ForeColor = Color.Red;
                lblError.Text = "No existe un aeropuerto con ese codigo";
                ActivoBotones();
                Session["Airport"] = null;
            }
            else
            {
                txtCodigoAeropuerto.Text = aAeropuerto.CodigoAeropuerto;
                txtCodigoCiudadA.Text = aAeropuerto.CiudadAeropuerto.CodigoCiudad;
                txtNombre.Text = aAeropuerto.Nombre;
                txtDireccion.Text = aAeropuerto.Direccion;
                txtImpuestoS.Text = Convert.ToInt32(aAeropuerto.ImpuestoS).ToString();
                txtImpuestoL.Text = Convert.ToInt32(aAeropuerto.ImpuestoL).ToString();
                ActivoBotones(false);
                Session["Airport"] = aAeropuerto;
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
            Aeropuerto aAeropuerto = (Aeropuerto)Session["Airport"];
            LogicaAeropuerto.Eliminar(aAeropuerto);
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
            Aeropuerto aAeropuerto = (Aeropuerto)Session["Airport"];
            aAeropuerto.CodigoAeropuerto = txtCodigoAeropuerto.Text.Trim();
            aAeropuerto.CiudadAeropuerto.CodigoCiudad = txtCodigoCiudadA.Text.Trim();
            aAeropuerto.Nombre = txtNombre.Text.Trim();
            aAeropuerto.Direccion = txtDireccion.Text.Trim();
            aAeropuerto.ImpuestoS = Convert.ToInt32(txtImpuestoS.Text);
            aAeropuerto.ImpuestoL = Convert.ToInt32(txtImpuestoL.Text);
            LogicaAeropuerto.Modificar(aAeropuerto);
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
            string codigoAeropuerto = txtCodigoAeropuerto.Text.Trim();
            string codigoCiudad = txtCodigoCiudadA.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            string direccion = txtDireccion.Text.Trim();
            int impuestoS = Convert.ToInt32(txtImpuestoS.Text);
            int impuestoL = Convert.ToInt32(txtImpuestoL.Text);
            Ciudades ciudad = LogicaCiudades.Buscar(codigoCiudad);
            if (ciudad != null)
            {
                Aeropuerto aAeropuerto = new Aeropuerto(codigoAeropuerto, ciudad, nombre, direccion, impuestoS, impuestoL);
                LogicaAeropuerto.Agregar(aAeropuerto);

                lblError.ForeColor = Color.Green;
                lblError.Text = "Alta con exito";

                LimpioFormulario();
            }
            else
            {
                lblError.ForeColor = Color.Red;
                lblError.Text = "Error en el codigo de la ciudad";
            }

        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }
}