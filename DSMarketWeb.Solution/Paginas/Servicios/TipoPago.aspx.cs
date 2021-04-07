using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DSMarketWeb.Solution.Paginas.Servicios
{
    public partial class TipoPago : System.Web.UI.Page
    {
        Lazy<DSMarketWeb.Logic.Logica.LogicaServicio.LogicaServicio> ObjDataServicio = new Lazy<Logic.Logica.LogicaServicio.LogicaServicio>();
        private void ModoConsulta() {
            divConsulta.Visible = true;
            DivBloqueMantenimiento.Visible = false;
        }

        private void ModoMantenimiento() {
            divConsulta.Visible = false;
            DivBloqueMantenimiento.Visible = true;
        }

        private void RestablecerPantalla() {
            txtdescripcion.Text = string.Empty;
            txtCodigoTipoPago.Text = string.Empty;
            txtPorcentajeEntero.Text = string.Empty;
            txtTipoPagoMantenimiento.Text = string.Empty;
            txtValorDecimal.Text = string.Empty;
            cbBloqueaMonto.Checked = false;
            cbEstatus.Checked = false;
            cbImpuestoAdicional.Checked = false;
            cbPorDefecto.Checked = false;
        }

        private void ListadoTipoPago() {
            string _TipoPago = string.IsNullOrEmpty(txtdescripcion.Text.Trim()) ? null : txtdescripcion.Text.Trim();

            var Listado = ObjDataServicio.Value.BuscaTipoPagos(
                new Nullable<decimal>(),
                _TipoPago);
            lbCantidadRegistrosVariable.Text = Listado.Count.ToString();
            rpListadoTipoPago.DataSource = Listado;
            rpListadoTipoPago.DataBind();
        }

        private void EditarRegistro() {
            decimal IdUsuario = Session["IdUsuario"] != null ? (decimal)Session["IdUsuario"] : 0;
            DSMarketWeb.Logic.PrcesarMantenimientos.Servicios.ProcesarInformacionTipoPago Editar = new Logic.PrcesarMantenimientos.Servicios.ProcesarInformacionTipoPago(
                Convert.ToDecimal(lbIdMantenimiento.Text),
                txtTipoPagoMantenimiento.Text,
                cbEstatus.Checked,
                IdUsuario,
                DateTime.Now,
                IdUsuario,
                DateTime.Now,
                cbBloqueaMonto.Checked,
                cbImpuestoAdicional.Checked,
                false,
                Convert.ToDecimal(txtValorDecimal.Text),
                txtCodigoTipoPago.Text,
                cbPorDefecto.Checked,
                "UPDATE");
            Editar.ProcesarInformacion();
            RestablecerPantalla();
            ModoConsulta();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                DSMarketWeb.Logic.Comunes.SacarNombreUsuario Nombre = new Logic.Comunes.SacarNombreUsuario((decimal)Session["IdUsuario"]);

                Label lbUsuarioConectado = (Label)Master.FindControl("lbUsuarioConectado");
                lbUsuarioConectado.Text = Nombre.SacarNombre();
                Label lbNombrePantalla = (Label)Master.FindControl("lbNivelAccesoPantalla");
                lbNombrePantalla.Text = "TIPO DE PAGO";

                ModoConsulta();
                ListadoTipoPago();

            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            ListadoTipoPago();
        }

        protected void btnRestablecer_Click(object sender, EventArgs e)
        {
            RestablecerPantalla();
            ModoConsulta();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            var ItemSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfIdTipoPago = ((HiddenField)ItemSeleccionado.FindControl("hfIdTipoPago")).Value.ToString();

            lbIdMantenimiento.Text = hfIdTipoPago;

            var BuscarRegistroSeleccionado = ObjDataServicio.Value.BuscaTipoPagos(
                Convert.ToDecimal(hfIdTipoPago),
                null);
            foreach (var n in BuscarRegistroSeleccionado) {
                txtTipoPagoMantenimiento.Text = n.TipoPago;
                txtValorDecimal.Text = n.Valor.ToString();
                txtPorcentajeEntero.Text = n.PorcentajeEntero;
                txtCodigoTipoPago.Text = n.CodigoTipoPago;
                cbEstatus.Checked = (n.Estatus0.HasValue ? n.Estatus0.Value : false);
                cbBloqueaMonto.Checked = (n.BloqueaMonto0.HasValue ? n.BloqueaMonto0.Value : false);
                cbImpuestoAdicional.Checked = (n.ImpuestoAdicional0.HasValue ? n.ImpuestoAdicional0.Value : false);
                cbPorDefecto.Checked = (n.PorDefecto0.HasValue ? n.PorDefecto0.Value : false);
            }
            ModoMantenimiento();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            EditarRegistro();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            RestablecerPantalla();
            ModoConsulta();
        }
    }
}