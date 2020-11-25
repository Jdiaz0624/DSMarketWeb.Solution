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

        }

        protected void cbAgregarRangoFechaConsulta_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAgregarRangoFechaConsulta.Checked == true) {
                txtFechaDesdeConsulta.Enabled = true;
                txtFechaHastaConsulta.Enabled = true;
            }
            else {
                txtFechaDesdeConsulta.Enabled = false;
                txtFechaHastaConsulta.Enabled = false;
            }
        }

        protected void ddlSeleccionarTipoProductoConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    

        protected void ddlSeleccionarMarcaConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSeleccionarCategoriaConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

        }

        protected void btnReporte_Click(object sender, EventArgs e)
        {

        }

        protected void btndetalle_Click(object sender, EventArgs e)
        {

        }

        protected void gvListado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvListado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}