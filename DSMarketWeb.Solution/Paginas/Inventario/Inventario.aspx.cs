using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DSMarketWeb.Solution.Paginas.Inventario
{
    public partial class Inventario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                DivFechaDesde.Visible = false;
                DivFechaHasta.Visible = false;
            }
        }

        protected void ddlSeleccionarTipoProductoConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSeleccionarCategoriaConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSeleccionarMarcaConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtDescripcionConsulta_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtCodigoBarraConsulta_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtReferenciaConsulta_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtCodigoProductoConulta_TextChanged(object sender, EventArgs e)
        {

        }

        protected void cbAgregarRangoFecha_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAgregarRangoFecha.Checked == true) {
                DivFechaDesde.Visible = true;
                DivFechaHasta.Visible = true;
            }
            else if (cbAgregarRangoFecha.Checked == false) {
                DivFechaDesde.Visible = false;
                DivFechaHasta.Visible = false;
            }
        }
    }
}