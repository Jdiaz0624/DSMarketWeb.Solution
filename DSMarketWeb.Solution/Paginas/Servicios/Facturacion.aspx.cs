using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Web.Security;

namespace DSMarketWeb.Solution.Paginas.Servicios
{
    public partial class Facturacion : System.Web.UI.Page
    {
        Lazy<DSMarketWeb.Logic.Logica.LogicaEmpresa.LogicaEmpresa> ObjdataEmpresa = new Lazy<Logic.Logica.LogicaEmpresa.LogicaEmpresa>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion> ObjDataConfiguracion = new Lazy<Logic.Logica.LogicaConfiguracion.LogicaConfiguracion>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaServicio.LogicaServicio> ObjDataServicio = new Lazy<Logic.Logica.LogicaServicio.LogicaServicio>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario> ObjDataInventario = new Lazy<Logic.Logica.LogicaInventario.LogicaInventario>();

        enum CodigosBloqueoYDesbloqueo { 
        BloquearControles = 1,
        DesbloquearControles=2
        }

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
        #region BUSCAR CLIENTES REGISTRADOS
        private void BuscarClientes() {

            string _CodigoCliente = string.IsNullOrEmpty(txtCodigoClienteBuscar.Text.Trim()) ? null : txtCodigoClienteBuscar.Text.Trim();
            string _RNCCedulaCLiente = string.IsNullOrEmpty(txtRNCCedulaCliente.Text.Trim()) ? null : txtRNCCedulaCliente.Text.Trim();

            var Buscar = ObjdataEmpresa.Value.BuscaClientes(
                _CodigoCliente,
                null,
                null,
                _RNCCedulaCLiente,
                null,
                null,
                null);
            if (Buscar.Count() < 1) {
                ClientScript.RegisterStartupScript(GetType(), "CLienteNoEncontrado()", "CLienteNoEncontrado();", true);
            }
            else {
                cbAgregarComprobante.Checked = true;
                MostrarComprobantesFiscalesActivos();
                CargarTipoIdentificacion();
                foreach (var n in Buscar) {
                    DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarComprobante, n.IdComprobante.ToString());
                    txtNombreCliente.Text = n.Nombre;
                    DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarTipoRNC, n.IdTipoIdentificacion.ToString());
                    txtRNCCliente.Text = n.RNC;
                    txtTelefonoCliente.Text = n.Telefono;
                    txtMailCliente.Text = n.Email;
                    txtDireccion.Text = n.Direccion;
                    lbCodigoClienteSeleccionado.Text = n.IdCliente.ToString();
                    lbLimiteCreditoClienteSeleccionado.Text = n.MontoCredito.ToString();
                    decimal MontoCredito = (decimal)n.MontoCredito;
                    txtCodigoClienteSeleccionado.Text = n.IdCliente.ToString();
                    txtLimiteCreditoClienteSeleccionado.Text = MontoCredito.ToString("N2");
                }
                BloquearDesbloquearControles((int)CodigosBloqueoYDesbloqueo.BloquearControles);
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
        //private void CargarTipoGarantia() {
        //   
        //}
        private void CargarTipoIngreso() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoIngreso, ObjDataConfiguracion.Value.BuscaListas("TIPOINGRESOS", null, null));
        }
        private void CargarTipoPago() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlTipoPago, ObjDataConfiguracion.Value.BuscaListas("TIPOPAGOFACTURACION", null, null));
        }

        private void CargarTipoIdentificacion() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoRNC, ObjDataConfiguracion.Value.BuscaListas("TIPOIDENTIFICACION", null, null));
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

            var BuscarProductosFacturar = ObjDataInventario.Value.BuscaProductosFacturar(
                new Nullable<decimal>(),
                null,
                _Descripcion,
                _CodigoBarras,
                _Referencia,
                _TipoProducto,
                null,
                null,
                _Marca,
                _Modelo,
                null, null, null, null);
            int CantidadRegistros = BuscarProductosFacturar.Count;
            lbCantidadRegistrosProductosVariable.Text = CantidadRegistros.ToString("N0");
            Paginar(ref rpListadoProductosAgregar, BuscarProductosFacturar, 3, ref lbCantidadPaginaVAriableProductoAgregar, ref LinkPrimeraPaginaProductoAgregar, ref LinkAnteriorProductoAgregar, ref LinkSiguienteProductoAgregar, ref LinkUltimoProductoAgregar);
            HandlePaging(ref dtPaginacionProductoAgregar, ref lbPaginaActualVariavleProductoAgregar);
        }
        #endregion
        #region FUNCIONES PARA BLOQUEAR Y DESBLOQUER CONTROLES
        private void BloquearDesbloquearCampoDescuento(decimal PorcientoDescuento) {

            if (PorcientoDescuento == 0) {
                DivDescuento.Visible = false;
            }
            else {
                DivDescuento.Visible = true;
            }
        }
        private void CalcularDescuentoMAximoProducto(int Cantidad, decimal PorcientoDescuento) {

            try {
                decimal DescuentoMaximo = 0;
                decimal PorcientoDescuentoProcesado = PorcientoDescuento / 100;
                decimal PrecioProducto = Convert.ToDecimal(txtPrecioVistaPrevia.Text);
                DescuentoMaximo = (PorcientoDescuentoProcesado * PrecioProducto) * Cantidad;
                txtDescuentoMaximoVistaPrevia.Text = Math.Round(DescuentoMaximo, 2).ToString("N2");
            }
            catch (Exception) {
                txtDescuentoMaximoVistaPrevia.Text = "ERROR EN CAMPO CANTIDAD";
            }

        }
        #endregion
        #region LETRERO
        private void EncabezadoPantalla(string NombreEncabezado) {
            decimal IdUsuario = Session["Idusuario"] != null ? (decimal)Session["IdUsuario"] : 0;

            if (IdUsuario == 0) {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
            else {
                DSMarketWeb.Logic.Comunes.SacarNombreUsuario Nombre = new Logic.Comunes.SacarNombreUsuario((decimal)Session["IdUsuario"]);
                Label lbUsuarioConectado = (Label)Master.FindControl("lbUsuarioConectado");
                lbUsuarioConectado.Text = Nombre.SacarNombre();

                Label lbPantallaActual = (Label)Master.FindControl("lbNivelAccesoPantalla");
                lbPantallaActual.Text = NombreEncabezado;
            }

           
        }
        #endregion
        #region VALIDAR SI LOS COMPROBANTES ESTAN ACTIVOS
        /// <summary>
        /// Este metodo es para validar si los comprobantes estan activos o inactivos en la configuración general.
        /// </summary>
        private void ValidarComprobantesFiscales() {
            int IdConfiguracionComprobante = (int)DSMarketWeb.Logic.Comunes.ValidarConfiguracionGenera.ConceptoConfiguracionGeneral.USAR_COMPROBANTES_FISCALES;
            bool EstatusComprobantes = false;
            DSMarketWeb.Logic.Comunes.ValidarConfiguracionGenera Validar = new Logic.Comunes.ValidarConfiguracionGenera(IdConfiguracionComprobante);
            EstatusComprobantes = Validar.SacarEstatusConfiguracionGeneral();
            if (EstatusComprobantes == true)
            {
                cbAgregarComprobante.Checked = true;
                MostrarComprobantesFiscalesActivos();
            }
            else
            {
                cbAgregarComprobante.Checked = false;
                MostrarComprobantesFiscalesSinUso();
            }
        }
        #endregion
        #region BLOQUEAR Y DESBLOQUEAR CONTROLES
        private void BloquearDesbloquearControles(int CodigoOperacion) {

            if (CodigoOperacion == (int)CodigosBloqueoYDesbloqueo.BloquearControles) {
                lblCodigoCliente.Visible = true;
                txtCodigoClienteSeleccionado.Visible = true;
                lbLimiteCredito.Visible = true;
                txtLimiteCreditoClienteSeleccionado.Visible = true;

                cbAgregarComprobante.Enabled = false;
                cbBuscarCliente.Enabled = false;
                txtCodigoClienteBuscar.Enabled = false;
                txtRNCCedulaCliente.Enabled = false;
                ddlSeleccionarComprobante.Enabled = false;
                txtNombreCliente.Enabled = false;
                ddlSeleccionarTipoRNC.Enabled = false;
                txtRNCCliente.Enabled = false;
                txtTelefonoCliente.Enabled = false;
                txtMailCliente.Enabled = false;
                txtDireccion.Enabled = false;
                DivBloqueAgregarClientes.Visible = false;
                DivBotonQuitarCliente.Visible = true;

                


            }
            else if (CodigoOperacion == (int)CodigosBloqueoYDesbloqueo.DesbloquearControles) {
                cbAgregarComprobante.Enabled = true;
                ValidarComprobantesFiscales();
                cbBuscarCliente.Enabled = true;
                cbBuscarCliente.Checked = false;
                txtCodigoClienteBuscar.Enabled = true;
                txtCodigoClienteBuscar.Text = string.Empty;
                txtRNCCedulaCliente.Enabled = true;
                txtRNCCedulaCliente.Text = string.Empty;
                ddlSeleccionarComprobante.Enabled = true;
                txtNombreCliente.Enabled = true;
                txtNombreCliente.Text = string.Empty;
                ddlSeleccionarTipoRNC.Enabled = true;
                CargarTipoIdentificacion();
                txtRNCCliente.Enabled = true;
                txtRNCCliente.Text = string.Empty;
                txtTelefonoCliente.Enabled = true;
                txtTelefonoCliente.Text = string.Empty;
                txtMailCliente.Enabled = true;
                txtMailCliente.Text = string.Empty;
                txtDireccion.Enabled = true;
                txtDireccion.Text = string.Empty;
                txtComentario.Text = string.Empty;
                txtCodigoClienteSeleccionado.Text = string.Empty;
                txtLimiteCreditoClienteSeleccionado.Text = string.Empty;
                lbLimiteCreditoClienteSeleccionado.Text = "0";
                lbCodigoClienteSeleccionado.Text = "1";
                DivBloqueAgregarClientes.Visible = false;
                DivBotonQuitarCliente.Visible = false;

                lblCodigoCliente.Visible = false;
                txtCodigoClienteSeleccionado.Visible = false;
                lbLimiteCredito.Visible = false;
                txtLimiteCreditoClienteSeleccionado.Visible = false;
            }
        
        }
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                DivBloqueAgregarClientes.Visible = false;
                lbCodigoClienteSeleccionado.Text = "1";
                lbLimiteCreditoClienteSeleccionado.Text = "0";

                lblCodigoCliente.Visible = false;
                txtCodigoClienteSeleccionado.Visible = false;
                lbLimiteCredito.Visible = false;
                txtLimiteCreditoClienteSeleccionado.Visible = false;

                EncabezadoPantalla("FACTURACION");
                rbContado.Checked = true;
                ValidarComprobantesFiscales();
                CargarTipoIdentificacion();
                CargarTipoProductos();
                CargarCategoias();
                CargarMarcas();
                CargarModelos();
             //   CargarTipoGarantia();
                CargarTipoIngreso();
                CargarTipoPago();
                txtFechaManual.Enabled = false;
                
            }
        }




        protected void LinkPrimeraPaginaClienteConsulta_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;

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

            bool ProductoAcumulativo = false;

            var SacarRegistro = ObjDataInventario.Value.BuscaProductos(
                Convert.ToDecimal(hfIdProductosSeleccionado),
                Convert.ToDecimal(hfNumeroConectorSeleccionado),
                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
            foreach (var n in SacarRegistro) {
                ProductoAcumulativo = (bool)n.ProductoAcumulativo0;
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

            if (ProductoAcumulativo == true)
            {
                txtCantidadUsarVistaPrevia.Enabled = true;
                txtCantidadUsarVistaPrevia.Text = "1";
            }
            else if (ProductoAcumulativo == false) {
                txtCantidadUsarVistaPrevia.Enabled = false;
            }

            decimal PorcientoDescuentoProcesar = Convert.ToDecimal(txtPorcientoDescuentoVistaPrevia.Text);
            BloquearDesbloquearCampoDescuento(PorcientoDescuentoProcesar);
            CalcularDescuentoMAximoProducto(Convert.ToInt32(txtCantidadUsarVistaPrevia.Text), Convert.ToDecimal(txtPorcientoDescuentoVistaPrevia.Text));
        }

        protected void LinkPrimeraPaginaProductoAgregar_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            MostrarProductos();
        }

        protected void LinkAnteriorProductoAgregar_Click(object sender, EventArgs e)
        {
            CurrentPage += -1;
            MostrarProductos();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref lbPaginaActualVariavleProductoAgregar, ref lbCantidadPaginaVAriableProductoAgregar);
        }

        protected void dtPaginacionProductoAgregar_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dtPaginacionProductoAgregar_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            MostrarProductos();
        }

        protected void LinkSiguienteProductoAgregar_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            MostrarProductos();
        }

        protected void LinkUltimoProductoAgregar_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            MostrarProductos();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref lbPaginaActualVariavleProductoAgregar, ref lbCantidadPaginaVAriableProductoAgregar);
        }

        protected void btnAgregarRegistro_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCantidadUsarVistaPrevia.Text.Trim())) {
                txtCantidadUsarVistaPrevia.Text = "1";
            }
            if (string.IsNullOrEmpty(txtDescuentoVistaPrevia.Text.Trim())) {
                txtDescuentoVistaPrevia.Text = "0";
            }
            CalcularDescuentoMAximoProducto(Convert.ToInt32(txtCantidadUsarVistaPrevia.Text), Convert.ToDecimal(txtPorcientoDescuentoVistaPrevia.Text));

            int CantidadDisponible = Convert.ToInt32(txtCantidadDisponibleVistaPrevia.Text);
            int CantidadUsar = Convert.ToInt32(txtCantidadUsarVistaPrevia.Text);

            if (CantidadUsar > CantidadDisponible || CantidadUsar ==0 || CantidadUsar <0)
            {
                DivLetreroRojo.Visible = true;
                if (CantidadUsar == 0)
                {
                    lbLetreroRojos.Text = "La cantidad a procesar no puede ser igual a cero, favor de verificar.";
                }
                else if (CantidadUsar < 0) {
                    lbLetreroRojos.Text = "La cantidad que intentas procesar es menor a cero, favor de verificar.";
                }
                else
                {

                    lbLetreroRojos.Text = "La cantidad que intentas procesar supera la cantidad disponible en almacen, favor de verificar.";

                }
                lbLetreroRojos.ForeColor = System.Drawing.Color.Red;
            }
            else {
                DivLetreroRojo.Visible = false;

                decimal DescuentoAplicado = Convert.ToDecimal(txtDescuentoVistaPrevia.Text);
                decimal DescuentoMAximo = Convert.ToDecimal(txtDescuentoMaximoVistaPrevia.Text);

                if (DescuentoAplicado > DescuentoMAximo || DescuentoAplicado < 0) {

                    DivLetreroRojo.Visible = true;

                    if (DescuentoAplicado < 0) {
                        lbLetreroRojos.Text = "El descuento aplicado no puede ser un numero menor a cero, favor de verificar.";
                    }
                    else {
                        lbLetreroRojos.Text = "El descuento no puede ser mayor al descuento maximo aplicado por el sistema, favor de verificar.";
                    }
                    lbLetreroRojos.ForeColor = System.Drawing.Color.Red;
                }
                else {
                    DivLetreroRojo.Visible = false;

                    //PROCESAR
                }

            }

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


        protected void ddlTipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidarCampoTipoPago(Convert.ToDecimal(ddlTipoPago.SelectedValue));
        }

        protected void txtCantidadUsarVistaPrevia_TextChanged(object sender, EventArgs e)
        {
            try {
                CalcularDescuentoMAximoProducto(Convert.ToInt32(txtCantidadUsarVistaPrevia.Text), Convert.ToDecimal(txtPorcientoDescuentoVistaPrevia.Text));
            }
            catch (Exception) {
                txtDescuentoMaximoVistaPrevia.Text = "ERROR EN CAMPO CANTIDAD";
            }
        }

      

        protected void cbBuscarCliente_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBuscarCliente.Checked == true) {
                DivBloqueAgregarClientes.Visible = true;
            }
            else if (cbBuscarCliente.Checked == false) {
                DivBloqueAgregarClientes.Visible = false;
            }
        }

        protected void btnConsultarRegistros_Click(object sender, EventArgs e)
        {
            BuscarClientes();
        }

        protected void btnQuitar_Click(object sender, EventArgs e)
        {
            BloquearDesbloquearControles((int)CodigosBloqueoYDesbloqueo.DesbloquearControles);
        }


    }
}