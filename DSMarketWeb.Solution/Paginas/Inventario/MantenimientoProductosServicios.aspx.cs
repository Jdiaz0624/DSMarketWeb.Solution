using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DSMarketWeb.Solution.Paginas.Inventario
{
    public partial class MantenimientoProductosServicios : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                divBloqueConsulta.Visible = false;
            }
        }

        protected void cbAgregarRangoFecha_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void txtCodigoBarra_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSeleccionarTipoProductoCOnsulta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSeleccionarCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSeleccionarMarcaConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnConsultarRegistros_Click(object sender, EventArgs e)
        {
            divBloqueConsulta.Visible = false;
            divBloqueMantenimiento.Visible = true;
        }

        protected void btnNuevoConsulta_Click(object sender, EventArgs e)
        {

        }

        protected void btnModificarConsulta_Click(object sender, EventArgs e)
        {

        }

        protected void btnEliminarConsulta_Click(object sender, EventArgs e)
        {

        }

        protected void btnExportarConsulta_Click(object sender, EventArgs e)
        {

        }

        protected void gvListado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvListado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSeleccionarTipoProductoMantenimiento_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSeleccionarCategoriaMantenimiento_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSeleccionarMarcaMantenimiento_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSeleccionarMarcaMantenimiento_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void ddlSeleccionarModeloMantenimiento_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}