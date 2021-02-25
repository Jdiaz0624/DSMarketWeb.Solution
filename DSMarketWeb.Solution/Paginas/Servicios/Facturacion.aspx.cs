﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace DSMarketWeb.Solution.Paginas.Servicios
{
    public partial class Facturacion : System.Web.UI.Page
    {
        Lazy<DSMarketWeb.Logic.Logica.LogicaEmpresa.LogicaEmpresa> ObjdataEmpresa = new Lazy<Logic.Logica.LogicaEmpresa.LogicaEmpresa>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion> ObjDataConfiguracion = new Lazy<Logic.Logica.LogicaConfiguracion.LogicaConfiguracion>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaServicio.LogicaServicio> ObjDataServicio = new Lazy<Logic.Logica.LogicaServicio.LogicaServicio>();
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
        private void HandlePaging(ref DataList NombreDataList, ref Label lbPaginaActual)
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
            lbPaginaActual.Text = (NumeroPagina + 1).ToString();
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
        private void Paginar(ref Repeater RptGrid, IEnumerable<object> Listado, int _NumeroRegistros, ref Label lbCantidadPaginas, ref LinkButton PrmeraPagina, ref LinkButton PaginaAnterior, ref LinkButton SiguientePagina, ref LinkButton UltimaPagina)
        {
            pagedDataSource.DataSource = Listado;
            pagedDataSource.AllowPaging = true;

            ViewState["TotalPages"] = pagedDataSource.PageCount;
            // lbNumeroVariable.Text = "1";
            lbCantidadPaginas.Text = pagedDataSource.PageCount.ToString();

            //MOSTRAMOS LA CANTIDAD DE PAGINAS A MOSTRAR O NUMERO DE REGISTROS
            pagedDataSource.PageSize = (_NumeroRegistros == 0 ? _TamanioPagina : _NumeroRegistros);
            pagedDataSource.CurrentPageIndex = CurrentPage;

            //HABILITAMOS LOS BOTONES DE LA PAGINACION
            PrmeraPagina.Enabled = !pagedDataSource.IsFirstPage;
            PaginaAnterior.Enabled = !pagedDataSource.IsFirstPage;
            SiguientePagina.Enabled = !pagedDataSource.IsLastPage;
            UltimaPagina.Enabled = !pagedDataSource.IsLastPage;

            RptGrid.DataSource = pagedDataSource;
            RptGrid.DataBind();


            divPaginacionClienteConsulta.Visible = true;
            divPaginacionProductoAgregar.Visible = true;
        }
        enum OpcionesPaginacionValores
        {
            PrimeraPagina = 1,
            SiguientePagina = 2,
            PaginaAnterior = 3,
            UltimaPagina = 4
        }
        private void MoverValoresPaginacion(int Accion, ref Label lbPaginaActual, ref Label lbCantidadPaginas)
        {

            int PaginaActual = 0;
            switch (Accion)
            {

                case 1:
                    //PRIMERA PAGINA
                    lbPaginaActual.Text = "1";

                    break;

                case 2:
                    //SEGUNDA PAGINA
                    PaginaActual = Convert.ToInt32(lbPaginaActual.Text);
                    PaginaActual++;
                    lbPaginaActual.Text = PaginaActual.ToString();
                    break;

                case 3:
                    //PAGINA ANTERIOR
                    PaginaActual = Convert.ToInt32(lbPaginaActual.Text);
                    if (PaginaActual > 1)
                    {
                        PaginaActual--;
                        lbPaginaActual.Text = PaginaActual.ToString();
                    }
                    break;

                case 4:
                    //ULTIMA PAGINA
                    lbPaginaActual.Text = lbCantidadPaginas.Text;
                    break;


            }

        }


        #endregion
        #region MOSTRAR EL LISTADO DE LOS CLIENTES REGISTRADOS
        private void MostrarClientesRegistrados() {
            string _CodigoCliente = string.IsNullOrEmpty(txtCodigoClienteConsulta.Text.Trim()) ? null : txtCodigoClienteConsulta.Text.Trim();
            string _RNCCliente = string.IsNullOrEmpty(txtRNCConsultaCliente.Text.Trim()) ? null : txtRNCConsultaCliente.Text.Trim();
            string _NombreCliente = string.IsNullOrEmpty(txtNombreClienteConsulta.Text.Trim()) ? null : txtNombreClienteConsulta.Text.Trim();

            var Listado = ObjdataEmpresa.Value.BuscaClientes(
                _CodigoCliente,
                null,
                _NombreCliente,
                _RNCCliente, true, null, null);
            if (Listado.Count() < 1) {
                lbCantidadRegistrosEncontradosClientesVariables.Text = "0";
            }
            else {
                int CantidadRegistros = Listado.Count;
                lbCantidadRegistrosEncontradosClientesVariables.Text = CantidadRegistros.ToString("N0");

                Paginar(ref rpListadoClientesConsulta, Listado, 10, ref lbCantidadPaginaVAriableClienteConsulta, ref LinkPrimeraPaginaClienteConsulta, ref LinkAnteriorClienteConsulta, ref LinkSiguienteClienteConsulta, ref LinkUltimoClienteConsulta);
                HandlePaging(ref dtPaginacionClienteConsulta, ref lbPaginaActualVariavleClienteConsulta);
            }

        }
        #endregion
        #region CARLAR LAS LISTAS DESPLEGABLES DE LA PANTALLA
        /// <summary>
        /// Este metodo muestra todos los comprobantes activos, por ejemplo todos aquellos comprobantes disponibles para su uso.
        /// </summary>
        private void MostrarComprobantesFiscalesActivos() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarComprobante, ObjDataConfiguracion.Value.BuscaListas("NCFACTIVOS", null, null));
        }
        /// <summary>
        /// Este metodo es para mostrar el comprobante por defecto N/A en caso de que no se requiera usar algun tipo de comprobante fiscal.
        /// </summary>
        private void MostrarComprobantesFiscalesSinUso()
        {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarComprobante, ObjDataConfiguracion.Value.BuscaListas("NCFSINUSO", null, null));
        }

        private void CargarTipoProductos() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoProducto, ObjDataConfiguracion.Value.BuscaListas("TIPOPRODUCTO", null, null), true);
        }
        private void CargarCategoias() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCategria, ObjDataConfiguracion.Value.BuscaListas("CATEGORIAS", ddlSeleccionarTipoProducto.SelectedValue.ToString(), null), true);
        }
        private void CargarMarcas() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarMarca, ObjDataConfiguracion.Value.BuscaListas("MARCAS", ddlSeleccionarTipoProducto.SelectedValue.ToString(), ddlSeleccionarCategria.SelectedValue.ToString()), true);
        }
        private void CargarModelos() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarModelo, ObjDataConfiguracion.Value.BuscaListas("MODELOS", ddlSeleccionarTipoProducto.SelectedValue.ToString(), ddlSeleccionarCategria.SelectedValue.ToString(), ddlSeleccionarMarca.SelectedValue.ToString()), true);
        }
        private void CargarTipoGarantia() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoGarantiaCalculos, ObjDataConfiguracion.Value.BuscaListas("TIEMPOGARANTIA", null, null));
        }
        private void CargarTipoIngreso() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoIngreso, ObjDataConfiguracion.Value.BuscaListas("TIPOINGRESOS", null, null));
        }
        private void CargarTipoPago() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlTipoPago, ObjDataConfiguracion.Value.BuscaListas("TIPOPAGOFACTURACION", null, null));
        }

        #endregion
        #region VALIDAR EL TIPO DE PAGO 
        private void ValidarCampoTipoPago(decimal IdTipoPago) {

            bool BloqueaMonto = false;
            bool ImpuestoAdicional = false;
            bool PorcentajeENtero = false;

            var ValidarTipoPago = ObjDataServicio.Value.BuscaTipoPagos(IdTipoPago, null);
            foreach (var n in ValidarTipoPago) {
                BloqueaMonto = (bool)n.BloqueaMonto0;
                ImpuestoAdicional = (bool)n.ImpuestoAdicional0;
                PorcentajeENtero = (bool)n.PorcentajeEntero0;
            }

            if (BloqueaMonto == true)
            {
                txtMontoPagar.Enabled = true;
                txtMontoPagar.Text = "0";
            }
            else if (BloqueaMonto == false) {
                txtMontoPagar.Enabled = false;
                
            }
        }
        #endregion
        #region MOSTRAR LOS PRODUCTOS A FACTURAR
        private void MostrarProductos() {
            //FILTROS
            decimal? _TipoProducto = ddlSeleccionarTipoProducto.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarTipoProducto.SelectedValue) : new Nullable<decimal>();
            decimal? _Categoria = ddlSeleccionarCategria.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarCategria.SelectedValue) : new Nullable<decimal>();
            decimal? _Marca = ddlSeleccionarMarca.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarMarca.SelectedValue) : new Nullable<decimal>();
            decimal? _Modelo = ddlSeleccionarModelo.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarModelo.SelectedValue) : new Nullable<decimal>();
            string _Descripcion = string.IsNullOrEmpty(txtDescripcion.Text.Trim()) ? null : txtDescripcion.Text.Trim();
            string _CodigoBarras = string.IsNullOrEmpty(txtCodigoBarras.Text.Trim()) ? null : txtCodigoBarras.Text.Trim();
            string _Referencia = string.IsNullOrEmpty(txtReferencia.Text.Trim()) ? null : txtReferencia.Text.Trim();

            var BuscarRegistros = ObjDataInventario.Value.BuscaProductos(
                new Nullable<decimal>(),
                null,
                _Descripcion,
                _CodigoBarras,
                _Referencia,
                null, null,
                _TipoProducto,
                _Categoria,
                null,
                _Marca,
                _Modelo,
                null, null, null, null, null, null, null);
            int CantidadRegistros = BuscarRegistros.Count;
            lbCantidadRegistrosProductosVariable.Text = CantidadRegistros.ToString("N0");
            Paginar(ref rpListadoProductosAgregar, BuscarRegistros, 10, ref lbCantidadPaginaVAriableProductoAgregar, ref LinkPrimeraPaginaProductoAgregar, ref LinkAnteriorProductoAgregar, ref LinkSiguienteProductoAgregar, ref LinkUltimoProductoAgregar);
            HandlePaging(ref dtPaginacionProductoAgregar, ref lbPaginaActualVariavleProductoAgregar);
        }
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                rbFacturacion.Checked = true;
                rbContado.Checked = true;
                int IdConfiguracionComprobante = (int)DSMarketWeb.Logic.Comunes.ValidarConfiguracionGenera.ConceptoConfiguracionGeneral.USAR_COMPROBANTES_FISCALES;
                bool EstatusComprobantes = false;
                DSMarketWeb.Logic.Comunes.ValidarConfiguracionGenera Validar = new Logic.Comunes.ValidarConfiguracionGenera(IdConfiguracionComprobante);
                EstatusComprobantes = Validar.SacarEstatusConfiguracionGeneral();
                if (EstatusComprobantes == true) {
                    cbAgregarComprobante.Checked = true;
                    MostrarComprobantesFiscalesActivos();
                }
                else {
                    cbAgregarComprobante.Checked = false;
                    MostrarComprobantesFiscalesSinUso();
                }
                CargarTipoProductos();
                CargarCategoias();
                CargarMarcas();
                CargarModelos();
                CargarTipoGarantia();
                CargarTipoIngreso();
                CargarTipoPago();
                cbAgregarFechaManual.Checked = false;
                txtFechaManual.Enabled = false;
                
            }
        }

        protected void btnConsultarClientes_Click(object sender, EventArgs e)
        {
            MostrarClientesRegistrados();
          //  MostrarListadoCLientes();
        }

        protected void btnSeleccioanrCliente_Click(object sender, EventArgs e)
        {

        }

        protected void LinkPrimeraPaginaClienteConsulta_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            MostrarClientesRegistrados();
        }

        protected void LinkAnteriorClienteConsulta_Click(object sender, EventArgs e)
        {
            CurrentPage += -1;
            MostrarClientesRegistrados();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior,ref lbPaginaActualVariavleClienteConsulta,ref lbCantidadPaginaVAriableClienteConsulta);
        }

        protected void dtPaginacionClienteConsulta_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dtPaginacionClienteConsulta_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            MostrarClientesRegistrados();
        }

        protected void LinkSiguienteClienteConsulta_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            MostrarClientesRegistrados();
        }

        protected void ddlSeleccionarTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCategoias();
            CargarMarcas();
            CargarModelos();
        }

        protected void ddlSeleccionarCategria_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarMarcas();
            CargarModelos();
        }

        protected void ddlSeleccionarMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarModelos();
        }

        protected void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            MostrarProductos();
        }

        protected void btnSeleccionarProductoAgregar_Click(object sender, EventArgs e)
        {
            var IdProductoSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfIdProductosSeleccionado = ((HiddenField)IdProductoSeleccionado.FindControl("hfIdProductoAgregar")).Value.ToString();

            var NumeroConectorSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfNumeroConectorSeleccionado = ((HiddenField)NumeroConectorSeleccionado.FindControl("hfNumeroConectorProductoAgregar")).Value.ToString();


            var SacarRegistro = ObjDataInventario.Value.BuscaProductos(
                Convert.ToDecimal(hfIdProductosSeleccionado),
                Convert.ToDecimal(hfNumeroConectorSeleccionado),
                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
            foreach (var n in SacarRegistro) {
                txtTipoProductoVistaPrevia.Text = n.TipoProducto;
                txtCategoriaVistaPrevia.Text = n.Categoria;
                txtAcumulativoVistaPrevia.Text = n.ProductoAcumulativo;
                decimal PrecioVenta = (decimal)n.PrecioVenta;
                txtPrecioVistaPrevia.Text = PrecioVenta.ToString("N2");
                txtProductoVistaPrevia.Text = n.Producto;
                int CantidadDIsponible = (int)n.Stock;
                txtCantidadDisponibleVistaPrevia.Text = CantidadDIsponible.ToString("N0");
                txtCantidadUsarVistaPrevia.Text = "1";
                txtPorcientoDescuentoVistaPrevia.Text = n.PorcientoDescuento.ToString();
                txtDescuentoVistaPrevia.Text = "0";
                bool AplicaImpuesto = (bool)n.AplicaParaImpuesto0;
                if (AplicaImpuesto == true) {

                    int PorcientoDescuento = 0;
                    bool Operacion = false;
                    decimal PrecioVentaImpuesto = (decimal)n.PrecioVenta;
                  

                    var SacarInformacionImpuestoVenta = ObjDataConfiguracion.Value.BuscaImpuestoVenta(1);
                    foreach (var nImpuesto in SacarInformacionImpuestoVenta) {
                        PorcientoDescuento = (int)nImpuesto.PorcientoImpuesto;
                        Operacion = (bool)nImpuesto.Operacion0;
                    }
                    decimal Impuesto = PrecioVentaImpuesto * (PorcientoDescuento / 100);
                    txtImpuesto.Text = Impuesto.ToString("N2");

                }
                else {
                    txtImpuesto.Text = "0";
                }
            }
        }

        protected void LinkPrimeraPaginaProductoAgregar_Click(object sender, EventArgs e)
        {

        }

        protected void LinkAnteriorProductoAgregar_Click(object sender, EventArgs e)
        {

        }

        protected void dtPaginacionProductoAgregar_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dtPaginacionProductoAgregar_ItemCommand(object source, DataListCommandEventArgs e)
        {

        }

        protected void LinkSiguienteProductoAgregar_Click(object sender, EventArgs e)
        {

        }

        protected void LinkUltimoProductoAgregar_Click(object sender, EventArgs e)
        {

        }

        protected void btnAgregarRegistro_Click(object sender, EventArgs e)
        {

        }

        protected void btnEditarRegistroAgregado_Click(object sender, EventArgs e)
        {

        }

        protected void btnEliminarRegistroAgregado_Click(object sender, EventArgs e)
        {

        }

        protected void btnRestablecerVistaPrevia_Click(object sender, EventArgs e)
        {

        }

        protected void btnSeleccionarRegistrosAgregadosHeaderRepeater_Click(object sender, EventArgs e)
        {

        }

        protected void LinkPrimeraPaginaProductosAgregados_Click(object sender, EventArgs e)
        {

        }

        protected void LinkAnteriorProductosAgregados_Click(object sender, EventArgs e)
        {

        }

        protected void dtPaginacionProductosAgregados_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dtPaginacionProductosAgregados_ItemCommand(object source, DataListCommandEventArgs e)
        {

        }

        protected void LinkSiguienteProductosAgregados_Click(object sender, EventArgs e)
        {

        }

        protected void LinkUltimoProductosAgregados_Click(object sender, EventArgs e)
        {

        }

        protected void LinkPrimeraPaginaProductosFacturar_Click(object sender, EventArgs e)
        {

        }

        protected void LinkAnteriorProductosFacturar_Click(object sender, EventArgs e)
        {

        }

        protected void dtPaginacionProductosFacturar_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dtPaginacionProductosFacturar_ItemCommand(object source, DataListCommandEventArgs e)
        {

        }

        protected void LinkSiguienteProductosFacturar_Click(object sender, EventArgs e)
        {

        }

        protected void LinkUltimoProductosFacturar_Click(object sender, EventArgs e)
        {

        }

        protected void cbAgregarComprobante_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAgregarComprobante.Checked == true) {
                MostrarComprobantesFiscalesActivos();
            }
            else if (cbAgregarComprobante.Checked == false) {
                MostrarComprobantesFiscalesSinUso();
            }
        }

        protected void cbAgregarFechaManual_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAgregarFechaManual.Checked == true) {
                txtFechaManual.Enabled = true;
            }
            else if(cbAgregarFechaManual.Checked==false) {
                txtFechaManual.Enabled = false;
                txtFechaManual.Text = string.Empty;
            }
        }

        protected void ddlTipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidarCampoTipoPago(Convert.ToDecimal(ddlTipoPago.SelectedValue));
        }

        protected void LinkUltimoClienteConsulta_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            MostrarClientesRegistrados();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref lbPaginaActualVariavleClienteConsulta, ref lbCantidadPaginaVAriableClienteConsulta);
        }
    }
}