using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace DSMarketWeb.Solution.Paginas.Empresa
{
    public partial class MantenimientoCompraSuplidores : System.Web.UI.Page
    {
        Lazy<DSMarketWeb.Logic.Logica.LogicaEmpresa.LogicaEmpresa> ObjDataEmpresa = new Lazy<Logic.Logica.LogicaEmpresa.LogicaEmpresa>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion> ObjDataConfiguracion = new Lazy<Logic.Logica.LogicaConfiguracion.LogicaConfiguracion>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaSeguridad.LogicaSeguridad> ObjDataSeguridad = new Lazy<Logic.Logica.LogicaSeguridad.LogicaSeguridad>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario> ObjDataInventario = new Lazy<Logic.Logica.LogicaInventario.LogicaInventario>();
        #region CONTROL PARA MOSTRAR LA PAGINACION
        readonly PagedDataSource pagedDataSource = new PagedDataSource();
        int _PrimeraPagina, _UltimaPagina;
        private int _TamanioPagina = 10;
        private int CurrentPage
        {
            get
            {
                if (ViewState["CurrentPage"] == null)
                {
                    return 0;
                }
                return ((int)ViewState["CurrentPage"]);
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }

        }
        private void HandlePaging(ref DataList NombreDataList)
        {
            var dt = new DataTable();
            dt.Columns.Add("IndicePagina"); //Start from 0
            dt.Columns.Add("TextoPagina"); //Start from 1

            _PrimeraPagina = CurrentPage - 5;
            if (CurrentPage > 5)
                _UltimaPagina = CurrentPage + 5;
            else
                _UltimaPagina = 10;

            // Check last page is greater than total page then reduced it to total no. of page is last index
            if (_UltimaPagina > Convert.ToInt32(ViewState["TotalPages"]))
            {
                _UltimaPagina = Convert.ToInt32(ViewState["TotalPages"]);
                _PrimeraPagina = _UltimaPagina - 10;
            }

            if (_PrimeraPagina < 0)
                _PrimeraPagina = 0;

            //AGREGAMOS LA PAGINA EN LA QUE ESTAMOS
            int NumeroPagina = (int)CurrentPage;
            lbPaginaActualVariable.Text = (NumeroPagina + 1).ToString();
            // Now creating page number based on above first and last page index
            for (var i = _PrimeraPagina; i < _UltimaPagina; i++)
            {
                var dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }


            NombreDataList.DataSource = dt;
            NombreDataList.DataBind();
        }
        private void Paginar(ref Repeater RptGrid, IEnumerable<object> Listado, int _NumeroRegistros)
        {
            pagedDataSource.DataSource = Listado;
            pagedDataSource.AllowPaging = true;

            ViewState["TotalPages"] = pagedDataSource.PageCount;
            // lbNumeroVariable.Text = "1";
            lbCantidadPaginaVariable.Text = pagedDataSource.PageCount.ToString();

            //MOSTRAMOS LA CANTIDAD DE PAGINAS A MOSTRAR O NUMERO DE REGISTROS
            pagedDataSource.PageSize = (_NumeroRegistros == 0 ? _TamanioPagina : _NumeroRegistros);
            pagedDataSource.CurrentPageIndex = CurrentPage;

            //HABILITAMOS LOS BOTONES DE LA PAGINACION
            LinkPrimeraPaginaPaginacion.Enabled = !pagedDataSource.IsFirstPage;
            LinkPaginaAnterior.Enabled = !pagedDataSource.IsFirstPage;
            LinkPaginaSiguiente.Enabled = !pagedDataSource.IsLastPage;
            LinkUltipaPagina.Enabled = !pagedDataSource.IsLastPage;

            RptGrid.DataSource = pagedDataSource;
            RptGrid.DataBind();


            divPaginacionUnidadMedida.Visible = true;
        }
        enum OpcionesPaginacionValores
        {
            PrimeraPagina = 1,
            SiguientePagina = 2,
            PaginaAnterior = 3,
            UltimaPagina = 4
        }
        private void MoverValoresPaginacion(int Accion)
        {

            int PaginaActual = 0;
            switch (Accion)
            {

                case 1:
                    //PRIMERA PAGINA
                    lbPaginaActualVariable.Text = "1";

                    break;

                case 2:
                    //SEGUNDA PAGINA
                    PaginaActual = Convert.ToInt32(lbPaginaActualVariable.Text);
                    PaginaActual++;
                    lbPaginaActualVariable.Text = PaginaActual.ToString();
                    break;

                case 3:
                    //PAGINA ANTERIOR
                    PaginaActual = Convert.ToInt32(lbPaginaActualVariable.Text);
                    if (PaginaActual > 1)
                    {
                        PaginaActual--;
                        lbPaginaActualVariable.Text = PaginaActual.ToString();
                    }
                    break;

                case 4:
                    //ULTIMA PAGINA
                    lbPaginaActualVariable.Text = lbCantidadPaginaVariable.Text;
                    break;


            }

        }
        #endregion

        #region CARGAR LAS LISTAS DESPLEGABLES
        //LISTAS DESPEGLABLES EN LA PANTALLA DE CONSULTA
        private void CargarTipoSuplidor() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoSuplidorConsulta, ObjDataConfiguracion.Value.BuscaListas("TIPOSUPLIDOR", null, null), true);
        }
        private void CargarSuplidores() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarSuplidorConsulta, ObjDataConfiguracion.Value.BuscaListas("SUPLIDOR", ddlSeleccionarTipoSuplidorConsulta.SelectedValue.ToString(), null), true); ;
        }


        //---------------------------------------------------------------------------
        private void CargarListasDesplegablesMantenimiento() {
            CargarTipoSuplidorMantenimiento();
            CargarSuplidoreMantenimiento();
            CargarListaTipoIDMantenimiento();
            CargarListaTipoBienesServiciosCOmprados();
            CargarListaTipoRetencionISR();
            CargarListaFormaPagoMantenimiento();
        }
        private void CargarTipoSuplidorMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoSuplidorMantenimiento, ObjDataConfiguracion.Value.BuscaListas("TIPOSUPLIDOR", null, null));

        }
        private void CargarSuplidoreMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarSuplidorMantenimiento, ObjDataConfiguracion.Value.BuscaListas("SUPLIDOR", ddlSeleccionarTipoSuplidorMantenimiento.SelectedValue.ToString(), null));


            var SacarSacarDatosSuplidor = ObjDataInventario.Value.BuscaSuplidores(
                null,
                Convert.ToDecimal(ddlSeleccionarSuplidorMantenimiento.SelectedValue),
                null, null, null);
            foreach (var n in SacarSacarDatosSuplidor)
            {
                txtRNCCedulaMantenimiento.Text = n.RNCSuplidor;
                txtActividadEconomicaMantenimiento.Text = n.ActividadEconomica;
            }
        }
        private void CargarListaTipoIDMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoIDMAntenimiento, ObjDataConfiguracion.Value.BuscaListas("TIPOIDENTIFICACION", null, null));
        }
        private void CargarListaTipoBienesServiciosCOmprados() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoBienesServiciosCompradosMantenimiento, ObjDataConfiguracion.Value.BuscaListas("TIPOBIENESSERVICIOS", null, null));
        }
        private void CargarListaTipoRetencionISR() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoRetencionISRMantenimiento, ObjDataConfiguracion.Value.BuscaListas("TIPORETENCIONISR", null, null));
        }
        private void CargarListaFormaPagoMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarFormaPagoMantenimiento, ObjDataConfiguracion.Value.BuscaListas("FORMAPAGO", null, null));
        }
        #endregion

        #region MOSTRAR EL LISTADO DE LAS COMPRAS
        private void MostrarListadoCOmpras() {
            if (string.IsNullOrEmpty(txtFechaDesde.Text.Trim()) || string.IsNullOrEmpty(txtFechaHastaConsullta.Text.Trim()))
            {
                ClientScript.RegisterStartupScript(GetType(), "CamposFechaVacios()", "CamposFechaVacios();", true);
                if (string.IsNullOrEmpty(txtFechaDesde.Text.Trim()))
                {
                    ClientScript.RegisterStartupScript(GetType(), "CampoFechaDesdeVacio()", "CampoFechaDesdeVacio();", true);
                }
                if (string.IsNullOrEmpty(txtFechaHastaConsullta.Text.Trim()))
                {
                    ClientScript.RegisterStartupScript(GetType(), "CampoFechaHastaVacio()", "CampoFechaHastaVacio();", true);
                }
            }
            else {
                string _NumeroIdentificacion = string.IsNullOrEmpty(txtNumeroIdentificacionConsulta.Text.Trim()) ? null : txtNumeroIdentificacionConsulta.Text.Trim();
                decimal? _TipoSuplidor = ddlSeleccionarTipoSuplidorConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarTipoSuplidorConsulta.SelectedValue) : new Nullable<decimal>();
                decimal? _Suplidor = ddlSeleccionarSuplidorConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarSuplidorConsulta.SelectedValue) : new Nullable<decimal>();

                var BuscarRegistro = ObjDataEmpresa.Value.BuscaCompraSuplidores(
                    new Nullable<decimal>(),
                    _TipoSuplidor,
                    _Suplidor,
                    _NumeroIdentificacion,
                    Convert.ToDateTime(txtFechaDesde.Text),
                    Convert.ToDateTime(txtFechaHastaConsullta.Text));
                if (BuscarRegistro.Count() < 1)
                {
                    lbCantidadRegistrosVariable.Text = "0";
                }
                else {
                    int CantidadRegistros = 0;

                    foreach (var n in BuscarRegistro) {
                        CantidadRegistros = (int)n.CantidadRegistros;
                    }
                    lbCantidadRegistrosVariable.Text = CantidadRegistros.ToString("N0");
                    Paginar(ref rpListadoCompraSuplidores, BuscarRegistro, 10);
                    HandlePaging(ref dlPaginacion);
                    divPaginacionUnidadMedida.Visible = true;
                }
            }
        }
        #endregion

        #region MANTENIMIENTO DE COMPRA DE SUPLIDORES
        private void MANCompraSuplidores(decimal IdCompraSuplidor, string Accion) {
            DSMarketWeb.Logic.PrcesarMantenimientos.Empresa.ProcesarInformacionCompraSuplidores Procesar = new Logic.PrcesarMantenimientos.Empresa.ProcesarInformacionCompraSuplidores(
                IdCompraSuplidor,
                Convert.ToDecimal(ddlSeleccionarTipoSuplidorMantenimiento.SelectedValue),
                Convert.ToDecimal(ddlSeleccionarSuplidorMantenimiento.SelectedValue),
                txtRNCCedulaMantenimiento.Text,
                Convert.ToDecimal(ddlSeleccionarTipoIDMAntenimiento.SelectedValue),
                Convert.ToDecimal(ddlSeleccionarTipoBienesServiciosCompradosMantenimiento.SelectedValue),
                txtNCFMantenimiento.Text,
                txtNCFModificadoMantenimiento.Text,
                Convert.ToDateTime(txtFechaComprobanteMantenimiento.Text),
                Convert.ToDateTime(txtFechaPagoMantenimiento.Text),
                Convert.ToDecimal(txtMontoFacturadoServiciosMantenimiento.Text),
                Convert.ToDecimal(txtMontoFacturadoBienesMantenimiento.Text),
                Convert.ToDecimal(txtTotalMntoFacturadoMantenimiento.Text),
                Convert.ToDecimal(txtITBISFacturadoMantenimiento.Text),
                Convert.ToDecimal(txtITBISRetenidoMantenimiento.Text),
                Convert.ToDecimal(txtITBISSujetoProporcionalidadMantenimiento.Text),
                Convert.ToDecimal(txtITBISLlevadoCostoMantenimiento.Text),
                Convert.ToDecimal(txtITBISPorAdelantarMantenimiento.Text),
                Convert.ToDecimal(txtITBISPercibidoComprasMantenimiento.Text),
                Convert.ToDecimal(ddlSeleccionarTipoRetencionISRMantenimiento.SelectedValue),
                Convert.ToDecimal(txtMontoRetencionRentaMantenimiento.Text),
                Convert.ToDecimal(txtISRPercibidoComprasMantenimiento.Text),
                Convert.ToDecimal(txtImpuestoSelectivoConsumoMantenimiento.Text),
                Convert.ToDecimal(txtOtrosImpuestosTasaMantenimiento.Text),
                Convert.ToDecimal(txtMontoPropinaLegalMantenimiento.Text),
                Convert.ToDecimal(ddlSeleccionarFormaPagoMantenimiento.SelectedValue),
                Convert.ToDecimal(Session["IdUsuario"]),
                DateTime.Now,
                txtActividadEconomicaMantenimiento.Text,
                Accion);
            Procesar.ProcesarDataCompraSuplidores();
        }
        #endregion

        #region GENERAR REPORTE
        private void GenerarReporte(decimal IdUsuario, string RutaReporte, string NombreArchivo, int ReporteUnico)
        {
            string UsuarioBD = "";
            string ClaveBD = "";
            var SacarCredencialesBD = ObjDataSeguridad.Value.SacarCredencialesBD(1);
            foreach (var n in SacarCredencialesBD) {
                UsuarioBD = n.Usuario;
                ClaveBD = DSMarketWeb.Logic.Comunes.SeguridadEncriptacion.DesEncriptar(n.Clave);
            }

            decimal? IdCompraSuplidor = null;
            string _NumeroIdentificacion = string.IsNullOrEmpty(txtNumeroIdentificacionConsulta.Text.Trim()) ? null : txtNumeroIdentificacionConsulta.Text.Trim();
            decimal? _TipoSuplidor = ddlSeleccionarTipoSuplidorConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarTipoSuplidorConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _Suplir = ddlSeleccionarSuplidorConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarSuplidorConsulta.SelectedValue) : new Nullable<decimal>();

            ReportDocument Reporte = new ReportDocument();
            Reporte.Load(RutaReporte);
            Reporte.Refresh();


            if (ReporteUnico == 1) {
                Reporte.SetParameterValue("@IdCompraSuplidor", Convert.ToDecimal(lbIdregistroSeleccionado.Text));
                Reporte.SetParameterValue("@IdTipoSuplidor", null);
                Reporte.SetParameterValue("@IdSuplidor", null);
                Reporte.SetParameterValue("@RNCCedula", null);
                Reporte.SetParameterValue("@FechaCreadoDesde", null);
                Reporte.SetParameterValue("@FechaCreadoHasta", null);
                Reporte.SetParameterValue("@UsuarioProcesa", IdUsuario);
            }
            else if (ReporteUnico == 0) {
                Reporte.SetParameterValue("@IdCompraSuplidor", IdCompraSuplidor);
                Reporte.SetParameterValue("@IdTipoSuplidor", _TipoSuplidor);
                Reporte.SetParameterValue("@IdSuplidor", _Suplir);
                Reporte.SetParameterValue("@RNCCedula", _NumeroIdentificacion);
                Reporte.SetParameterValue("@FechaCreadoDesde", Convert.ToDateTime(txtFechaDesde.Text));
                Reporte.SetParameterValue("@FechaCreadoHasta", Convert.ToDateTime(txtFechaHastaConsullta.Text));
                Reporte.SetParameterValue("@UsuarioProcesa", IdUsuario);
            }

            Reporte.SetDatabaseLogon(UsuarioBD, ClaveBD);
            if (rbExprtarPDF.Checked == true) {
                Reporte.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, NombreArchivo);
            }
            else if (rbExprtarWOrd.Checked == true) {
                Reporte.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, true, NombreArchivo);
            }
        }
        #endregion

        private decimal CalcularTotalMontoFacturado(decimal TotalMontoFacturadoBienes, decimal TotalMontoFacturadoServicio) {
            decimal TotalMontoFacturado = 0;

            TotalMontoFacturado = TotalMontoFacturadoBienes + TotalMontoFacturadoServicio;
            return TotalMontoFacturado;
        }

        private void CalcularTotalMontoFacturadoProceso() {
            try
            {
                if (string.IsNullOrEmpty(txtMontoFacturadoBienesMantenimiento.Text.Trim()))
                {
                    txtMontoFacturadoBienesMantenimiento.Text = "0";
                }
                if (string.IsNullOrEmpty(txtMontoFacturadoServiciosMantenimiento.Text.Trim()))
                {
                    txtMontoFacturadoServiciosMantenimiento.Text = "0";
                }

                txtTotalMntoFacturadoMantenimiento.Text = CalcularTotalMontoFacturado(Convert.ToDecimal(txtMontoFacturadoServiciosMantenimiento.Text), Convert.ToDecimal(txtMontoFacturadoBienesMantenimiento.Text)).ToString();
            }
            catch { }
        }

        private void DeshabilitarControles() {
            ddlSeleccionarTipoSuplidorMantenimiento.Enabled = false;
            ddlSeleccionarSuplidorMantenimiento.Enabled = false;
           // txtRNCCedulaMantenimiento.Enabled = false;
            ddlSeleccionarTipoIDMAntenimiento.Enabled = false;
            ddlSeleccionarTipoBienesServiciosCompradosMantenimiento.Enabled = false;
            txtNCFMantenimiento.Enabled = false;
            txtNCFModificadoMantenimiento.Enabled = false;
            txtFechaComprobanteMantenimiento.Enabled = false;
            txtFechaPagoMantenimiento.Enabled = false;
            txtMontoFacturadoServiciosMantenimiento.Enabled = false;
            txtMontoFacturadoBienesMantenimiento.Enabled = false;
            //txtTotalMntoFacturadoMantenimiento.Enabled = false;
            txtITBISFacturadoMantenimiento.Enabled = false;
            txtITBISRetenidoMantenimiento.Enabled = false;
            txtITBISSujetoProporcionalidadMantenimiento.Enabled = false;
            txtITBISLlevadoCostoMantenimiento.Enabled = false;
            txtITBISPorAdelantarMantenimiento.Enabled = false;
            txtITBISPercibidoComprasMantenimiento.Enabled = false;
            ddlSeleccionarTipoRetencionISRMantenimiento.Enabled = false;
            txtMontoRetencionRentaMantenimiento.Enabled = false;
            txtISRPercibidoComprasMantenimiento.Enabled = false;
            txtImpuestoSelectivoConsumoMantenimiento.Enabled = false;
            txtOtrosImpuestosTasaMantenimiento.Enabled = false;
            txtMontoPropinaLegalMantenimiento.Enabled = false;
            ddlSeleccionarFormaPagoMantenimiento.Enabled = false;
            //txtClaveSeguridadMantenimiento.Enabled = false;
        }
        private void HabilitarControles() {
            ddlSeleccionarTipoSuplidorMantenimiento.Enabled = true;
            ddlSeleccionarSuplidorMantenimiento.Enabled = true;
          //  txtRNCCedulaMantenimiento.Enabled = true;
            ddlSeleccionarTipoIDMAntenimiento.Enabled = true;
            ddlSeleccionarTipoBienesServiciosCompradosMantenimiento.Enabled = true;
            txtNCFMantenimiento.Enabled = true;
            txtNCFModificadoMantenimiento.Enabled = true;
            txtFechaComprobanteMantenimiento.Enabled = true;
            txtFechaPagoMantenimiento.Enabled = true;
            txtMontoFacturadoServiciosMantenimiento.Enabled = true;
            txtMontoFacturadoBienesMantenimiento.Enabled = true;
            //txtTotalMntoFacturadoMantenimiento.Enabled = true;
            txtITBISFacturadoMantenimiento.Enabled = true;
            txtITBISRetenidoMantenimiento.Enabled = true;
            txtITBISSujetoProporcionalidadMantenimiento.Enabled = true;
            txtITBISLlevadoCostoMantenimiento.Enabled = true;
            txtITBISPorAdelantarMantenimiento.Enabled = true;
            txtITBISPercibidoComprasMantenimiento.Enabled = true;
            ddlSeleccionarTipoRetencionISRMantenimiento.Enabled = true;
            txtMontoRetencionRentaMantenimiento.Enabled = true;
            txtISRPercibidoComprasMantenimiento.Enabled = true;
            txtImpuestoSelectivoConsumoMantenimiento.Enabled = true;
            txtOtrosImpuestosTasaMantenimiento.Enabled = true;
            txtMontoPropinaLegalMantenimiento.Enabled = true;
            ddlSeleccionarFormaPagoMantenimiento.Enabled = true;
          //  txtClaveSeguridadMantenimiento.Enabled = true;
        }

        private void ModoConsulta() {
            btnConsultarRegistros.Enabled = true;
            btnNuevoRegistro.Enabled = true;
            btnModificarRegistro.Enabled = false;
            btnEliminarRegistro.Enabled = false;
            btnExportarRegistro.Enabled = true;
            btnRestablecerPantalla.Enabled = true;
        }

        private void ModoMantenimiento() {
            btnConsultarRegistros.Enabled = false;
            btnNuevoRegistro.Enabled = false;
            btnModificarRegistro.Enabled = true;
            btnEliminarRegistro.Enabled = true;
            btnExportarRegistro.Enabled = true;
            btnRestablecerPantalla.Enabled = true;
        }

        /// <summary>
        /// Este metodo es para mostrar el bloque de consulta y ocultar los de mantenimiento
        /// </summary>
        private void Consulta_Mantenimiento() {
            DivBloqueConsulta.Visible = true;
            DivBloqueDetalleRegistroSeleccionado.Visible = false;
            DivBloqueMantenimiento.Visible = false;
        }
        /// <summary>
        /// Este metodo es para mostrar los bloque de mantenimiento y ocultar los de consulta
        /// </summary>
        private void Mantenimiento_Consulta() {
            DivBloqueConsulta.Visible = false;
            DivBloqueDetalleRegistroSeleccionado.Visible = false;
            DivBloqueMantenimiento.Visible = true;
        }

        private void RestablecerPantalla() {
            CargarTipoSuplidor();
            CargarSuplidores();
            CargarListasDesplegablesMantenimiento();
            txtNumeroIdentificacionConsulta.Text = string.Empty;
            Consulta_Mantenimiento();
            ModoConsulta();

            txtRNCCedulaMantenimiento.Text = string.Empty;
            txtNCFMantenimiento.Text = string.Empty;
            txtNCFModificadoMantenimiento.Text = string.Empty;
            txtFechaComprobanteMantenimiento.Text = string.Empty;
            txtFechaPagoMantenimiento.Text = string.Empty;
            txtMontoFacturadoServiciosMantenimiento.Text = string.Empty;
            txtMontoFacturadoBienesMantenimiento.Text = string.Empty;
            txtTotalMntoFacturadoMantenimiento.Text = string.Empty;
            txtITBISFacturadoMantenimiento.Text = string.Empty;
            txtITBISRetenidoMantenimiento.Text = string.Empty;
            txtITBISSujetoProporcionalidadMantenimiento.Text = string.Empty;
            txtITBISLlevadoCostoMantenimiento.Text = string.Empty;
            txtITBISPorAdelantarMantenimiento.Text = string.Empty;
            txtITBISPercibidoComprasMantenimiento.Text = string.Empty;
            txtMontoRetencionRentaMantenimiento.Text = string.Empty;
            txtISRPercibidoComprasMantenimiento.Text = string.Empty;
            txtImpuestoSelectivoConsumoMantenimiento.Text = string.Empty;
            txtOtrosImpuestosTasaMantenimiento.Text = string.Empty;
            txtMontoPropinaLegalMantenimiento.Text = string.Empty;
            txtClaveSeguridadMantenimiento.Text = string.Empty;
            txtActividadEconomicaDetale.Text = string.Empty;
            txtActividadEconomicaMantenimiento.Text = string.Empty;
            rpListadoCompraSuplidores.DataSource = null;
            rpListadoCompraSuplidores.DataBind();
            lbReporteUnico.Text = "0";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                ModoConsulta();
                CargarTipoSuplidor();
                CargarSuplidores();
                CargarListasDesplegablesMantenimiento();
                DivBloqueDetalleRegistroSeleccionado.Visible = false;
                DivBloqueMantenimiento.Visible = false;
                lbReporteUnico.Text = "0";
                rbExprtarPDF.Checked = true;
            }
        }

        protected void btnConsultarRegistros_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            MostrarListadoCOmpras();
            DivBloqueDetalleRegistroSeleccionado.Visible = false;
        }

        protected void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            Mantenimiento_Consulta();
            HabilitarControles();
            btnGuardar.Visible = true;
            btnModificar.Visible = false;
            btnEliminar.Visible = false;
            lbClaveSeguridadMantenimiento.Visible = false;
            txtClaveSeguridadMantenimiento.Visible = false;

            txtMontoFacturadoServiciosMantenimiento.Text = "0";
            txtMontoFacturadoBienesMantenimiento.Text = "0";
            txtTotalMntoFacturadoMantenimiento.Text = "0";
            txtITBISFacturadoMantenimiento.Text = "0";
            txtITBISRetenidoMantenimiento.Text = "0";
            txtITBISSujetoProporcionalidadMantenimiento.Text = "0";
            txtITBISLlevadoCostoMantenimiento.Text = "0";
            txtITBISPorAdelantarMantenimiento.Text = "0";
            txtITBISPercibidoComprasMantenimiento.Text = "0";
            txtMontoRetencionRentaMantenimiento.Text = "0";
            txtISRPercibidoComprasMantenimiento.Text = "0";
            txtImpuestoSelectivoConsumoMantenimiento.Text = "0";
            txtOtrosImpuestosTasaMantenimiento.Text = "0";
            txtMontoPropinaLegalMantenimiento.Text = "0";
            CalcularTotalMontoFacturadoProceso();
        }

        protected void btnModificarRegistro_Click(object sender, EventArgs e)
        {
            Mantenimiento_Consulta();
            HabilitarControles();
            btnGuardar.Visible = false;
            btnModificar.Visible = true;
            btnEliminar.Visible = false;
            lbClaveSeguridadMantenimiento.Visible = true;
            txtClaveSeguridadMantenimiento.Visible = true;
           // CalcularTotalMontoFacturadoProceso();
        }

        protected void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            Mantenimiento_Consulta();
            DeshabilitarControles();
            btnGuardar.Visible = false;
            btnModificar.Visible = false;
            btnEliminar.Visible = true;
            lbClaveSeguridadMantenimiento.Visible = true;
            txtClaveSeguridadMantenimiento.Visible = true;
        }

        protected void btnExportarRegistro_Click(object sender, EventArgs e)
        {
            int ReporteUnico = Convert.ToInt32(lbReporteUnico.Text);
            decimal IdUsuario = Session["IdUsuario"] != null ? (decimal)Session["IdUsuario"] : 0;
            if (ReporteUnico == 1) {
                GenerarReporte(IdUsuario, Server.MapPath("ReporteCompraSuplidorUnico.rpt"), "Reporte de Compra a Suplidor", 1);
            }
            else {
                if (string.IsNullOrEmpty(txtFechaDesde.Text.Trim()) || string.IsNullOrEmpty(txtFechaHastaConsullta.Text.Trim()))
                {
                    ClientScript.RegisterStartupScript(GetType(), "CamposFechaVacios()", "CamposFechaVacios();", true);
                    if (string.IsNullOrEmpty(txtFechaDesde.Text.Trim())) {
                        ClientScript.RegisterStartupScript(GetType(), "CampoFechaDesdeVacio()", "CampoFechaDesdeVacio();", true);
                    }
                    if (string.IsNullOrEmpty(txtFechaHastaConsullta.Text.Trim())) {
                        ClientScript.RegisterStartupScript(GetType(), "CampoFechaHastaVacio()", "CampoFechaHastaVacio();", true);
                    }
                }
                else {
                    //GENERAR
             
                  
                    GenerarReporte(IdUsuario, Server.MapPath("CompraSuplidoresDetalle.rpt"), "Compra Suplidores Detalle", 0);
                }
            }
        }

        protected void btnRestablecerPantalla_Click(object sender, EventArgs e)
        {
            RestablecerPantalla();
        }

        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {
            var ItemSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfIdCompraSuplidor = ((HiddenField)ItemSeleccionado.FindControl("hfIdCompraSupldor")).Value.ToString();
            lbIdregistroSeleccionado.Text = hfIdCompraSuplidor.ToString();
            var BuscarRegistroSeleccionadp = ObjDataEmpresa.Value.BuscaCompraSuplidores(
                Convert.ToDecimal(hfIdCompraSuplidor),
                null, null, null, null, null);
            Paginar(ref rpListadoCompraSuplidores, BuscarRegistroSeleccionadp, 1);
            HandlePaging(ref dlPaginacion);
            foreach (var n in BuscarRegistroSeleccionadp) {
                txtTipoSuplidorDetalle.Text = n.TipoSuplidor;
                txtSuplidorDetalle.Text = n.Suplidor;
                txtRNCCedulaDetalle.Text = n.RNCCedula;
                txtTipoIDDetalle.Text = n.TipoIdentificacion;
                txtTipoBienesServiciosCompradosDetalle.Text = n.TipoBienesServicios;
                txtNCFDetalle.Text = n.NCF;
                txtNCFModificadoDetalle.Text = n.NCFModificado;
                DateTime FechaComprobante = Convert.ToDateTime(n.FechaComprobante0);
                txtFechaComprobanteDetalle.Text = FechaComprobante.ToString("yyyy-MM-dd");
                DateTime FechaPago = Convert.ToDateTime(n.FechaPago0);
                txtFechaPagoDetalle.Text = FechaPago.ToString("yyyy-MM-dd");
                decimal MontoFacturadoServicio = (decimal)n.MontoFacturadoServicios;
                txtMontoFacturadoServiciosDetalle.Text = MontoFacturadoServicio.ToString("N2");
                decimal MontoFacturadoBienes = (decimal)n.MontoFacturadoBienes;
                txtMontoFacturadoBienesDetalle.Text = MontoFacturadoBienes.ToString("N2");
                decimal TotalMontoFacturado = (decimal)n.TotalMontoFacturado;
                txtTotalMntoFacturadoDetalle.Text = TotalMontoFacturado.ToString("N2");
                decimal ITBISFacturadoDetalle = (decimal)n.ITBISFacturado;
                txtITBISFacturadoDetalle.Text = ITBISFacturadoDetalle.ToString("N2");
                decimal ITBISRetenido = (decimal)n.ITBISRetenido;
                txtITBISRetenidoDetalle.Text = ITBISRetenido.ToString("N2");
                decimal ITBISSujetoPorporcionalidad = (decimal)n.ITBISSujetoProporcionalidad;
                txtITBISSujetoProporcionalidadDetalle.Text = ITBISSujetoPorporcionalidad.ToString("N2");
                decimal ITBISLlevadoAlCosto = (decimal)n.ITBISLlevadoCosto;
                txtITBISLlevadoCostoDetalle.Text = ITBISLlevadoAlCosto.ToString("N2");
                decimal ITBISPorAdelantar = (decimal)n.ITBISPorAdelantar;
                txtITBISPorAdelantarDetalle.Text = ITBISPorAdelantar.ToString("N2");
                decimal ITBISPercibidoCompras = (decimal)n.ITBISPercibidoCompras;
                txtITBISPercibidoComprasDetalle.Text = ITBISPercibidoCompras.ToString("N2");
                txtTipoRetencionISRDetalle.Text = n.TipoRetencionISR;
                decimal MontoretencionRenta = (decimal)n.MontoRetencionRenta;
                txtMontoRetencionRentaDetalle.Text = MontoretencionRenta.ToString("N2");
                decimal ISRPercibidoCompras = (decimal)n.ISRPercibidoCompras;
                txtISRPercibidoComprasDetalle.Text = ISRPercibidoCompras.ToString("N2");
                decimal ImpuestoSelecitoConsumo = (decimal)n.ImpuestoSelectivoConsumo;
                txtImpuestoSelectivoConsumoDetalle.Text = ImpuestoSelecitoConsumo.ToString();
                decimal OtrosImpuestosTasa = (decimal)n.OtrosImpuestosTasa;
                txtOtrosImpuestosTasaDetalle.Text = OtrosImpuestosTasa.ToString("N2");
                decimal Montopropinalegal = (decimal)n.MontoPropinaLegal;
                txtMontoPropinaLegalDetalle.Text = Montopropinalegal.ToString("N2");
                txtActividadEconomicaDetale.Text = n.ActividadEconomica;
                txtFormaPagoDetalle.Text = n.FormaPago;

                //////////////////////////////////
                ///
                CargarTipoSuplidorMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarTipoSuplidorMantenimiento, n.IdTipoSuplidor.ToString());
                CargarSuplidoreMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarSuplidorMantenimiento, n.IdSuplidor.ToString());
              //  txtRNCCedulaMantenimiento.Text = n.RNCCedula;
                CargarListaTipoIDMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarTipoIDMAntenimiento, n.IdTipoIdentificacion.ToString());
                CargarListaTipoBienesServiciosCOmprados();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarTipoBienesServiciosCompradosMantenimiento, n.IdTipoBienesServicios.ToString());
                txtNCFMantenimiento.Text = n.NCF;
                txtNCFModificadoMantenimiento.Text = n.NCFModificado;
                DateTime FechaComprobanteMantenimiento = Convert.ToDateTime(n.FechaComprobante0);
                txtFechaComprobanteMantenimiento.Text = FechaComprobanteMantenimiento.ToString("yyyy-MM-dd");
                DateTime FechaPagoMantenimiento = Convert.ToDateTime(n.FechaPago0);
                txtFechaPagoMantenimiento.Text = FechaPagoMantenimiento.ToString("yyyy-MM-dd");
                txtMontoFacturadoServiciosMantenimiento.Text = n.MontoFacturadoServicios.ToString();
                txtMontoFacturadoBienesMantenimiento.Text = n.MontoFacturadoBienes.ToString();
                txtTotalMntoFacturadoMantenimiento.Text = n.TotalMontoFacturado.ToString();
                txtITBISFacturadoMantenimiento.Text = n.ITBISFacturado.ToString();
                txtITBISRetenidoMantenimiento.Text = n.ITBISRetenido.ToString();
                txtITBISSujetoProporcionalidadMantenimiento.Text = n.ITBISSujetoProporcionalidad.ToString();
                txtITBISLlevadoCostoMantenimiento.Text = n.ITBISLlevadoCosto.ToString();
                txtITBISPorAdelantarMantenimiento.Text = n.ITBISPorAdelantar.ToString();
                txtITBISPercibidoComprasMantenimiento.Text = n.ITBISPercibidoCompras.ToString();
                CargarListaTipoRetencionISR();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarTipoRetencionISRMantenimiento, n.IdTipoRetencionISR.ToString());
                txtMontoRetencionRentaMantenimiento.Text = n.MontoRetencionRenta.ToString();
                txtISRPercibidoComprasMantenimiento.Text = n.ISRPercibidoCompras.ToString();
                txtImpuestoSelectivoConsumoMantenimiento.Text = n.ImpuestoSelectivoConsumo.ToString();
                txtOtrosImpuestosTasaMantenimiento.Text = n.OtrosImpuestosTasa.ToString();
                txtMontoPropinaLegalMantenimiento.Text = n.MontoPropinaLegal.ToString();
                CargarListaFormaPagoMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarFormaPagoMantenimiento, n.IdFormaPago.ToString());
              //  txtActividadEconomicaMantenimiento.Text = n.ActividadEconomica;
            }
            DivBloqueDetalleRegistroSeleccionado.Visible = true;
            ModoMantenimiento();
            lbReporteUnico.Text = "1";
        }

        protected void LinkPrimeraPaginaPaginacion_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            MostrarListadoCOmpras();
        }

        protected void LinkPaginaAnterior_Click(object sender, EventArgs e)
        {
            CurrentPage += -1;
            MostrarListadoCOmpras();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior);
        }

        protected void dlPaginacion_CancelCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            MostrarListadoCOmpras();
        }

        protected void dlPaginacion_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void LinkPaginaSiguiente_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            MostrarListadoCOmpras();
        }

        protected void LinkUltipaPagina_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            MostrarListadoCOmpras();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.UltimaPagina);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFechaComprobanteMantenimiento.Text.Trim()) || string.IsNullOrEmpty(txtFechaPagoMantenimiento.Text.Trim())) {
                ClientScript.RegisterStartupScript(GetType(), "CamposFechaVaciosMantenimiento()", "CamposFechaVaciosMantenimiento();", true);
                if (string.IsNullOrEmpty(txtFechaComprobanteMantenimiento.Text.Trim())) {
                    ClientScript.RegisterStartupScript(GetType(), "CampoFechaComprobanteVacio()", "CampoFechaComprobanteVacio();", true);
                }
                if (string.IsNullOrEmpty(txtFechaPagoMantenimiento.Text.Trim())) {
                    ClientScript.RegisterStartupScript(GetType(), "CampoFechaPagoVacio()", "CampoFechaPagoVacio();", true);
                }
            }
            else {
                MANCompraSuplidores(0, "INSERT");
                ClientScript.RegisterStartupScript(GetType(), "RegistroGuardado()", "RegistroGuardado();", true);
                RestablecerPantalla();
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFechaComprobanteMantenimiento.Text.Trim()) || string.IsNullOrEmpty(txtFechaPagoMantenimiento.Text.Trim()))
            {
                ClientScript.RegisterStartupScript(GetType(), "CamposFechaVaciosMantenimiento()", "CamposFechaVaciosMantenimiento();", true);
                if (string.IsNullOrEmpty(txtFechaComprobanteMantenimiento.Text.Trim()))
                {
                    ClientScript.RegisterStartupScript(GetType(), "CampoFechaComprobanteVacio()", "CampoFechaComprobanteVacio();", true);
                }
                if (string.IsNullOrEmpty(txtFechaPagoMantenimiento.Text.Trim()))
                {
                    ClientScript.RegisterStartupScript(GetType(), "CampoFechaPagoVacio()", "CampoFechaPagoVacio();", true);
                }
            }
            else {
                //VALIDAMOS LA CLAVE DE SEGURIDAD
                string _ClaveSeguridad = string.IsNullOrEmpty(txtClaveSeguridadMantenimiento.Text.Trim()) ? null : txtClaveSeguridadMantenimiento.Text.Trim();

                DSMarketWeb.Logic.Comunes.ValidarClaveSeguridad Validar = new Logic.Comunes.ValidarClaveSeguridad(_ClaveSeguridad);
                bool Respuesta = Validar.ResultadoClave();
                if (Respuesta == true) {
                    MANCompraSuplidores(Convert.ToDecimal(lbIdregistroSeleccionado.Text), "UPDATE");
                    ClientScript.RegisterStartupScript(GetType(), "RegistroModificado()", "RegistroModificado();", true);
                    RestablecerPantalla();
                }
                else {
                    ClientScript.RegisterStartupScript(GetType(), "ClaveSeguridadInvalida()", "ClaveSeguridadInvalida();", true);
                }
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            //VALIDAMOS LA CLAVE DE SEGURIDAD
            string _ClaveSeguridad = string.IsNullOrEmpty(txtClaveSeguridadMantenimiento.Text.Trim()) ? null : txtClaveSeguridadMantenimiento.Text.Trim();

            DSMarketWeb.Logic.Comunes.ValidarClaveSeguridad Validar = new Logic.Comunes.ValidarClaveSeguridad(_ClaveSeguridad);
            bool Respuesta = Validar.ResultadoClave();
            if (Respuesta == true)
            {
                MANCompraSuplidores(Convert.ToDecimal(lbIdregistroSeleccionado.Text), "DELETE");
                ClientScript.RegisterStartupScript(GetType(), "RegistroModificado()", "RegistroModificado();", true);
                RestablecerPantalla();
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "RegistroEliminado()", "RegistroEliminado();", true);
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            RestablecerPantalla();
        }

        protected void ddlSeleccionarTipoSuplidorMantenimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarSuplidoreMantenimiento();
        }

        protected void txtMontoFacturadoServiciosMantenimiento_TextChanged(object sender, EventArgs e)
        {
            CalcularTotalMontoFacturadoProceso();
        }

        protected void txtMontoFacturadoBienesMantenimiento_TextChanged(object sender, EventArgs e)
        {
            CalcularTotalMontoFacturadoProceso();
        }

        protected void ddlSeleccionarTipoSuplidorConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarSuplidores();
        }
    }
}