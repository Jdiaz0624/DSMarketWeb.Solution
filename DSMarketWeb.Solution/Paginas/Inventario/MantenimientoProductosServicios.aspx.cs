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
        Lazy<DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion> ObjDataConfiguracion = new Lazy<Logic.Logica.LogicaConfiguracion.LogicaConfiguracion>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario> ObjdataInventario = new Lazy<Logic.Logica.LogicaInventario.LogicaInventario>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaSeguridad.LogicaSeguridad> ObjDataSeguridad = new Lazy<Logic.Logica.LogicaSeguridad.LogicaSeguridad>();

        #region CARGAR LAS LISTAS DESPLEGABLES EN LA PANTALLA DE CONSULTA
        private void CargarTipoProductosConsultas() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoProductoCOnsulta, ObjDataConfiguracion.Value.BuscaListas("TIPOPRODUCTO", null, null), true);
        }
        private void CargarListasCategoriasCOnsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCategoria, ObjDataConfiguracion.Value.BuscaListas("CATEGORIAS", ddlSeleccionarTipoProductoCOnsulta.SelectedValue.ToString(), null), true);
        }
        private void CargarUnidadMedidaConsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarUnidadMedida, ObjDataConfiguracion.Value.BuscaListas("UNIDADMEDIDA", null, null), true);
        }
        private void CargarMarcasConsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarMarcaConsulta, ObjDataConfiguracion.Value.BuscaListas("MARCAS", ddlSeleccionarTipoProductoCOnsulta.SelectedValue.ToString(), ddlSeleccionarCategoria.SelectedValue.ToString()), true);
        }
        private void CargarModelosConsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarModelosConsulta, ObjDataConfiguracion.Value.BuscaListas("MODELOS", ddlSeleccionarTipoProductoCOnsulta.SelectedValue.ToString(), ddlSeleccionarCategoria.SelectedValue.ToString(), ddlSeleccionarMarcaConsulta.SelectedValue.ToString()), true);
        }
        private void CargarListasDesplegablesCOnsulta() {
            CargarTipoProductosConsultas();
            CargarListasCategoriasCOnsulta();
            CargarUnidadMedidaConsulta();
            CargarMarcasConsulta();
            CargarModelosConsulta();
        }
        #endregion
        #region MOSTRAR EL LISTADO DE INVENTARIO
        private void MostrarListadoInventario() {
            //CONTROLES DE FILTROS
            string _Descripcion = string.IsNullOrEmpty(txtDescripcionConsulta.Text.Trim()) ? null : txtDescripcionConsulta.Text.Trim();
            string _CodigoBarra = string.IsNullOrEmpty(txtCodigoBarra.Text.Trim()) ? null : txtCodigoBarra.Text.Trim();
            string _Referencia = string.IsNullOrEmpty(txtReferenciaConsulta.Text.Trim()) ? null : txtReferenciaConsulta.Text.Trim();
            string _NumeroSeguimiento = string.IsNullOrEmpty(txtNumeroSeguimientoConsulta.Text.Trim()) ? null : txtNumeroSeguimientoConsulta.Text.Trim();
            decimal? _TipoProducto = ddlSeleccionarTipoProductoCOnsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarTipoProductoCOnsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _Categoria = ddlSeleccionarCategoria.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarCategoria.SelectedValue) : new Nullable<decimal>();
            decimal? _Marca = ddlSeleccionarMarcaConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarMarcaConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _Modelo = ddlSeleccionarModelosConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarModelosConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _UnidadMedida = ddlSeleccionarUnidadMedida.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarUnidadMedida.SelectedValue) : new Nullable<decimal>();

            //REALIZAMOS LA CONSULLTA AGREGANDO RANGO DE FECHA
            if (cbAgregarRangoFecha.Checked == true) {
                //BUSCAMOS LOS REGISTROS VENDIDOS Y DESCARTADOS MEDIANTE EL RANGO DE FECHA
                if (cbProductosVendisodDescartados.Checked == true) {
                    if (string.IsNullOrEmpty(txtFechaDesdeConsulta.Text.Trim()) || string.IsNullOrEmpty(txtFechaHAstaConsulta.Text.Trim()))
                    {
                        ClientScript.RegisterStartupScript(GetType(), "CamposFechaVAcios()", "CamposFechaVAcios();", true);
                        if (string.IsNullOrEmpty(txtFechaDesdeConsulta.Text.Trim()))
                        {
                            ClientScript.RegisterStartupScript(GetType(), "CampoFechaDesdeVAcio()", "CampoFechaDesdeVAcio();", true);
                        }
                        if (string.IsNullOrEmpty(txtFechaHAstaConsulta.Text.Trim()))
                        {
                            ClientScript.RegisterStartupScript(GetType(), "CampoFechaHastaVacio()", "CampoFechaHastaVacio();", true);
                        }
                    }
                    else
                    {
                        var Buscar = ObjdataInventario.Value.BuscaProductos(
                            new Nullable<decimal>(),
                            null,
                            _Descripcion,
                            _CodigoBarra,
                            _Referencia,
                            Convert.ToDateTime(txtFechaDesdeConsulta.Text),
                            Convert.ToDateTime(txtFechaHAstaConsulta.Text),
                            _TipoProducto,
                            _Categoria,
                            _UnidadMedida,
                            _Marca,
                            _Modelo,
                            null, null, null, null,
                            true,
                            _NumeroSeguimiento);
                        gvListado.DataSource = Buscar;
                        gvListado.DataBind();
                        int CantidadRegistros = 0;
                        decimal CapitalInvertido = 0, GananciaAproximada = 0;
                        if (Buscar.Count() < 1)
                        {
                            lbCantidadRegistrosConsultaVariable.Text = "0";
                            lbCantidadInventidoVariable.Text = "0";
                            lbGananciaAproximadaVariable.Text = "0";
                        }
                        else
                        {
                            foreach (var n in Buscar)
                            {
                                CantidadRegistros = Convert.ToInt32(n.CantidadRegistros);
                                CapitalInvertido = Convert.ToDecimal(n.CapilalInvertido);
                                GananciaAproximada = Convert.ToDecimal(n.GananciaAproximada);
                            }
                            lbCantidadRegistrosConsultaVariable.Text = CantidadRegistros.ToString("N0");
                            lbCantidadInventidoVariable.Text = CapitalInvertido.ToString("N2");
                            lbGananciaAproximadaVariable.Text = GananciaAproximada.ToString("N2");
                        }

                    }
                }
                //BUSCAMOS LOS REGISTROS DEL INVENTARIO APLICANDO RANGO DE FECHA
                else
                {
                    if (string.IsNullOrEmpty(txtFechaDesdeConsulta.Text.Trim()) || string.IsNullOrEmpty(txtFechaHAstaConsulta.Text.Trim()))
                    {
                        ClientScript.RegisterStartupScript(GetType(), "CamposFechaVAcios()", "CamposFechaVAcios();", true);
                        if (string.IsNullOrEmpty(txtFechaDesdeConsulta.Text.Trim()))
                        {
                            ClientScript.RegisterStartupScript(GetType(), "CampoFechaDesdeVAcio()", "CampoFechaDesdeVAcio();", true);
                        }
                        if (string.IsNullOrEmpty(txtFechaHAstaConsulta.Text.Trim()))
                        {
                            ClientScript.RegisterStartupScript(GetType(), "CampoFechaHastaVacio()", "CampoFechaHastaVacio();", true);
                        }
                    }
                    else {
                        var Buscar = ObjdataInventario.Value.BuscaProductos(
                            new Nullable<decimal>(),
                            null,
                            _Descripcion,
                            _CodigoBarra,
                            _Referencia,
                            Convert.ToDateTime(txtFechaDesdeConsulta.Text),
                            Convert.ToDateTime(txtFechaHAstaConsulta.Text),
                            _TipoProducto,
                            _Categoria,
                            _UnidadMedida,
                            _Marca,
                            _Modelo,
                            null, null, null, null,
                            false,
                            _NumeroSeguimiento);
                        gvListado.DataSource = Buscar;
                        gvListado.DataBind();
                        int CantidadRegistros = 0;
                        decimal CapitalInvertido = 0, GananciaAproximada = 0;
                        if (Buscar.Count() < 1) {
                            lbCantidadRegistrosConsultaVariable.Text = "0";
                            lbCantidadInventidoVariable.Text = "0";
                            lbGananciaAproximadaVariable.Text = "0";
                        }
                        else {
                            foreach (var n in Buscar) {
                                CantidadRegistros = Convert.ToInt32(n.CantidadRegistros);
                                CapitalInvertido = Convert.ToDecimal(n.CapilalInvertido);
                                GananciaAproximada = Convert.ToDecimal(n.GananciaAproximada);
                            }
                            lbCantidadRegistrosConsultaVariable.Text = CantidadRegistros.ToString("N0");
                            lbCantidadInventidoVariable.Text = CapitalInvertido.ToString("N2");
                            lbGananciaAproximadaVariable.Text = GananciaAproximada.ToString("N2");
                        }
                        
                    }
                }
            }
            //REALIZMAOS LA BUSQUEDA SIN APLICAR RANGO DE FECHA
            else {
                //BUSCAMOS LOS PRODUCTOS DESCARTADOS Y VENDIDOS SIN APLICAR RANGO DE FECHA
                if (cbProductosVendisodDescartados.Checked == true) {
                    var Listado = ObjdataInventario.Value.BuscaProductos(
                            new Nullable<decimal>(),
                            null,
                            _Descripcion,
                            _CodigoBarra,
                            _Referencia,
                            null, null,
                            _TipoProducto,
                            _Categoria,
                            _UnidadMedida,
                            _Marca,
                            _Modelo,
                            null, null, null,
                            null, true,
                            _NumeroSeguimiento);
                    gvListado.DataSource = Listado;
                    gvListado.DataBind();
                    int CantidadRegistros = 0;
                    decimal CapitalInvertido = 0, GananciaAproximada = 0;
                    if (Listado.Count() < 1)
                    {
                        lbCantidadRegistrosConsultaVariable.Text = "0";
                        lbCantidadInventidoVariable.Text = "0";
                        lbGananciaAproximadaVariable.Text = "0";
                    }
                    else
                    {
                        foreach (var n in Listado)
                        {
                            CantidadRegistros = Convert.ToInt32(n.CantidadRegistros);
                            CapitalInvertido = Convert.ToDecimal(n.CapilalInvertido);
                            GananciaAproximada = Convert.ToDecimal(n.GananciaAproximada);
                        }
                        lbCantidadRegistrosConsultaVariable.Text = CantidadRegistros.ToString("N0");
                        lbCantidadInventidoVariable.Text = CapitalInvertido.ToString("N2");
                        lbGananciaAproximadaVariable.Text = GananciaAproximada.ToString("N2");
                    }

                }
               //BUSCAMOS LOS PRODUCTOS DEL INVENTARIO SIN APLICAR EL RANGO DE FECHA
                else {
                    var Listado = ObjdataInventario.Value.BuscaProductos(
                        new Nullable<decimal>(),
                        null,
                        _Descripcion,
                        _CodigoBarra,
                        _Referencia,
                        null, null,
                        _TipoProducto,
                        _Categoria,
                        _UnidadMedida,
                        _Marca,
                        _Modelo,
                        null, null, null,
                        null, false,
                        _NumeroSeguimiento);
                    gvListado.DataSource = Listado;
                    gvListado.DataBind();
                    int CantidadRegistros = 0;
                    decimal CapitalInvertido = 0, GananciaAproximada = 0;
                    if (Listado.Count() < 1) {
                        lbCantidadRegistrosConsultaVariable.Text = "0";
                        lbCantidadInventidoVariable.Text = "0";
                        lbGananciaAproximadaVariable.Text = "0";
                    }
                    else {
                        foreach (var n in Listado) {
                            CantidadRegistros = Convert.ToInt32(n.CantidadRegistros);
                            CapitalInvertido = Convert.ToDecimal(n.CapilalInvertido);
                            GananciaAproximada = Convert.ToDecimal(n.GananciaAproximada);
                        }
                        lbCantidadRegistrosConsultaVariable.Text = CantidadRegistros.ToString("N0");
                        lbCantidadInventidoVariable.Text = CapitalInvertido.ToString("N2");
                        lbGananciaAproximadaVariable.Text = GananciaAproximada.ToString("N2");
                    }
                }
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                divBloqueConsulta.Visible = true;
                divBloqueMantenimiento.Visible = false;

                CargarListasDesplegablesCOnsulta();
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
            CargarListasCategoriasCOnsulta();
            CargarMarcasConsulta();
            CargarModelosConsulta();
        }

        protected void ddlSeleccionarCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarMarcasConsulta();
            CargarModelosConsulta();
        }

        protected void ddlSeleccionarMarcaConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarModelosConsulta();
        }

        protected void btnConsultarRegistros_Click(object sender, EventArgs e)
        {
            MostrarListadoInventario();
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
            gvListado.PageIndex = e.NewPageIndex;
            MostrarListadoInventario();
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

        protected void btnProcesarMantenimiento_Click(object sender, EventArgs e)
        {

        }

        protected void btnVolverMantenimiento_Click(object sender, EventArgs e)
        {

        }
    }
}