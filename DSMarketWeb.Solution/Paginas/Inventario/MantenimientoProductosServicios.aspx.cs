using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

namespace DSMarketWeb.Solution.Paginas.Inventario
{
    public partial class MantenimientoProductosServicios : System.Web.UI.Page
    {

        
        Lazy<DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion> ObjDataConfiguracion = new Lazy<Logic.Logica.LogicaConfiguracion.LogicaConfiguracion>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario> ObjdataInventario = new Lazy<Logic.Logica.LogicaInventario.LogicaInventario>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaSeguridad.LogicaSeguridad> ObjDataSeguridad = new Lazy<Logic.Logica.LogicaSeguridad.LogicaSeguridad>();

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
        private void HandlePaging()
        {
            var dt = new DataTable();
            dt.Columns.Add("PageIndex"); //Start from 0
            dt.Columns.Add("PageText"); //Start from 1

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

            // Now creating page number based on above first and last page index
            for (var i = _PrimeraPagina; i < _UltimaPagina; i++)
            {
                var dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }

            rptPaging.DataSource = dt;
            rptPaging.DataBind();
        }
        private void BindDataIntoRepeater(int _NumeroRegistros = 0)
        {
            //FILTROS
            string _Descripcion = string.IsNullOrEmpty(txtDescripcionConsulta.Text.Trim()) ? null : txtDescripcionConsulta.Text.Trim();
            string _CodigoBarra = string.IsNullOrEmpty(txtCodigoBarra.Text.Trim()) ? null : txtCodigoBarra.Text.Trim();
            string _Referencia = string.IsNullOrEmpty(txtReferenciaConsulta.Text.Trim()) ? null : txtReferenciaConsulta.Text.Trim();
            string _NumeroSeguimiento = string.IsNullOrEmpty(txtNumeroSeguimientoConsulta.Text.Trim()) ? null : txtNumeroSeguimientoConsulta.Text.Trim();
            decimal? _TipoProducto = ddlSeleccionarTipoProductoCOnsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarTipoProductoCOnsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _Categoria = ddlSeleccionarCategoria.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarCategoria.SelectedValue) : new Nullable<decimal>();
            decimal? _Marca = ddlSeleccionarMarcaConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarMarcaConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _Modelo = ddlSeleccionarModelosConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarModelosConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _UnidadMedida = ddlSeleccionarUnidadMedida.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarUnidadMedida.SelectedValue) : new Nullable<decimal>();

            //MOSTRAR EL HISTORIAL DE LOS PRODUCTOS VENDIDOS Y DESCARTADOS
            if (cbProductosVendisodDescartados.Checked == true) {
                if (cbAgregarRangoFecha.Checked == true) {
                    if (string.IsNullOrEmpty(txtFechaDesdeConsulta.Text.Trim()) || string.IsNullOrEmpty(txtFechaHAstaConsulta.Text.Trim())) {
                        ClientScript.RegisterStartupScript(GetType(), "CamposFechaVAcios()", "CamposFechaVAcios();", true);

                        if (string.IsNullOrEmpty(txtFechaDesdeConsulta.Text.Trim())) {
                            ClientScript.RegisterStartupScript(GetType(), "CampoFechaDesdeVAcio()", "CampoFechaDesdeVAcio();", true);
                        }
                        if (string.IsNullOrEmpty(txtFechaHAstaConsulta.Text.Trim())) {
                            ClientScript.RegisterStartupScript(GetType(), "CampoFechaHastaVacio()", "CampoFechaHastaVacio();", true);
                        }
                    }
                    else {
                        var BuscarConRangoFecha = ObjdataInventario.Value.BuscaProductos(
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
                        if (BuscarConRangoFecha.Count() < 1)
                        {
                            divPaginacion.Visible = false;
                            lbCantidadRegistrosConsultaVariable.Text = "0";
                            lbCantidadInventidoVariable.Text = "0";
                            lbGananciaAproximadaVariable.Text = "0";
                        }
                        else
                        {
                            int CantidadRegistros = 0;
                            decimal CapitalInvertido = 0;
                            decimal GananciaAproximada = 0;
                            foreach (var n in BuscarConRangoFecha)
                            {
                                CantidadRegistros = Convert.ToInt32(n.CantidadRegistros);
                                CapitalInvertido = Convert.ToDecimal(n.CapilalInvertido);
                                GananciaAproximada = Convert.ToDecimal(n.GananciaAproximada);
                            }
                            lbCantidadRegistrosConsultaVariable.Text = CantidadRegistros.ToString("N0");
                            lbCantidadInventidoVariable.Text = CapitalInvertido.ToString("N2");
                            lbGananciaAproximadaVariable.Text = GananciaAproximada.ToString("N2");
                            Paginar(ref RVListadoProducto, BuscarConRangoFecha, 10);
                        }
                    }
                }
                else
                {
                    var BuscarSINRangoFecha = ObjdataInventario.Value.BuscaProductos(
                    new Nullable<decimal>(),
                    null,
                    _Descripcion,
                    _CodigoBarra,
                    _Referencia,
                    null,
                    null,
                    _TipoProducto,
                    _Categoria,
                    _UnidadMedida,
                    _Marca,
                    _Modelo,
                    null, null, null, null,
                    true,
                    _NumeroSeguimiento);
                    if (BuscarSINRangoFecha.Count() < 1)
                    {
                        divPaginacion.Visible = false;
                        lbCantidadRegistrosConsultaVariable.Text = "0";
                        lbCantidadInventidoVariable.Text = "0";
                        lbGananciaAproximadaVariable.Text = "0";
                    }
                    else
                    {
                        int CantidadRegistros = 0;
                        decimal CapitalInvertido = 0;
                        decimal GananciaAproximada = 0;
                        foreach (var n in BuscarSINRangoFecha)
                        {
                            CantidadRegistros = Convert.ToInt32(n.CantidadRegistros);
                            CapitalInvertido = Convert.ToDecimal(n.CapilalInvertido);
                            GananciaAproximada = Convert.ToDecimal(n.GananciaAproximada);
                        }
                        lbCantidadRegistrosConsultaVariable.Text = CantidadRegistros.ToString("N0");
                        lbCantidadInventidoVariable.Text = CapitalInvertido.ToString("N2");
                        lbGananciaAproximadaVariable.Text = GananciaAproximada.ToString("N2");
                        Paginar(ref RVListadoProducto, BuscarSINRangoFecha, 10);
                    }
                }
            }

            //MOSTRAR EL HISTORIAL DE LOS PRODUCTOS ACTIVOS
            else {
                if (cbAgregarRangoFecha.Checked == true) {
                    if (string.IsNullOrEmpty(txtFechaDesdeConsulta.Text.Trim()) || string.IsNullOrEmpty(txtFechaHAstaConsulta.Text.Trim())) {
                        ClientScript.RegisterStartupScript(GetType(), "CamposFechaVAcios()", "CamposFechaVAcios();", true);
                        if (string.IsNullOrEmpty(txtFechaDesdeConsulta.Text.Trim())) {
                            ClientScript.RegisterStartupScript(GetType(), "CampoFechaDesdeVAcio()", "CampoFechaDesdeVAcio();", true);
                        }
                        if (string.IsNullOrEmpty(txtFechaHAstaConsulta.Text.Trim())) {
                            ClientScript.RegisterStartupScript(GetType(), "CampoFechaHastaVacio()", "CampoFechaHastaVacio();", true);
                        }
                    }
                    else {
                        var BuscarRegistrosConRangoFecha = ObjdataInventario.Value.BuscaProductos(
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
                        if (BuscarRegistrosConRangoFecha.Count() < 1)
                        {
                            divPaginacion.Visible = false;
                            lbCantidadRegistrosConsultaVariable.Text = "0";
                            lbCantidadInventidoVariable.Text = "0";
                            lbGananciaAproximadaVariable.Text = "0";
                        }
                        else {
                            divPaginacion.Visible = true;
                            int CantidadRegistros = 0;
                            decimal CapitalInvertido = 0, GananciaAproximada = 0;
                            foreach (var n in BuscarRegistrosConRangoFecha) {
                                CantidadRegistros = Convert.ToInt32(n.CantidadRegistros);
                                CapitalInvertido = Convert.ToDecimal(n.CapilalInvertido);
                                GananciaAproximada = Convert.ToDecimal(n.GananciaAproximada);
                            }
                            lbCantidadRegistrosConsultaVariable.Text = CantidadRegistros.ToString("N0");
                            lbCantidadInventidoVariable.Text = CapitalInvertido.ToString("N2");
                            lbGananciaAproximadaVariable.Text = GananciaAproximada.ToString("N2");
                            Paginar(ref RVListadoProducto, BuscarRegistrosConRangoFecha, 10);
                        }
                    }
                }
                else {
                    var BuscarRegistrosSINRangoFecha = ObjdataInventario.Value.BuscaProductos(
                          new Nullable<decimal>(),
                          null,
                          _Descripcion,
                          _CodigoBarra,
                          _Referencia,
                          null,
                          null,
                          _TipoProducto,
                          _Categoria,
                          _UnidadMedida,
                          _Marca,
                          _Modelo,
                          null, null, null, null,
                          false,
                          _NumeroSeguimiento);
                    if (BuscarRegistrosSINRangoFecha.Count() < 1)
                    {
                        divPaginacion.Visible = false;
                        lbCantidadRegistrosConsultaVariable.Text = "0";
                        lbCantidadInventidoVariable.Text = "0";
                        lbGananciaAproximadaVariable.Text = "0";
                    }
                    else
                    {
                        divPaginacion.Visible = true;
                        int CantidadRegistros = 0;
                        decimal CapitalInvertido = 0, GananciaAproximada = 0;
                        foreach (var n in BuscarRegistrosSINRangoFecha)
                        {
                            CantidadRegistros = Convert.ToInt32(n.CantidadRegistros);
                            CapitalInvertido = Convert.ToDecimal(n.CapilalInvertido);
                            GananciaAproximada = Convert.ToDecimal(n.GananciaAproximada);
                        }
                        lbCantidadRegistrosConsultaVariable.Text = CantidadRegistros.ToString("N0");
                        lbCantidadInventidoVariable.Text = CapitalInvertido.ToString("N2");
                        lbGananciaAproximadaVariable.Text = GananciaAproximada.ToString("N2");
                        Paginar(ref RVListadoProducto, BuscarRegistrosSINRangoFecha, 10);
                    }
                }
            }

    

            if (cbGraficarConsulta.Checked == true) {
                divGraficoMarcas.Visible = true;
                divGraficoServicios.Visible = true;
                divTipoProducto.Visible = true;
                GenerarGraficos();
            }

       

          
        }
        private void Paginar(ref Repeater RptGrid, IEnumerable<object> Listado, int _NumeroRegistros) {
            pagedDataSource.DataSource = Listado;
            pagedDataSource.AllowPaging = true;

            ViewState["TotalPages"] = pagedDataSource.PageCount;
           // lbNumeroVariable.Text = "1";
            lbCantidadPaginasVariable.Text = pagedDataSource.PageCount.ToString();

            //MOSTRAMOS LA CANTIDAD DE PAGINAS A MOSTRAR O NUMERO DE REGISTROS
            pagedDataSource.PageSize = (_NumeroRegistros == 0 ? _TamanioPagina : _NumeroRegistros);
            pagedDataSource.CurrentPageIndex = CurrentPage;

            //HABILITAMOS LOS BOTONES DE LA PAGINACION
            lbPrimeraPagina.Enabled = !pagedDataSource.IsFirstPage;
            lbPaginaAnterior.Enabled = !pagedDataSource.IsFirstPage;
            lbPaginaGuguiente.Enabled = !pagedDataSource.IsLastPage;
            lbUltimaPagina.Enabled = !pagedDataSource.IsLastPage;

            RVListadoProducto.DataSource = pagedDataSource;
            RVListadoProducto.DataBind();

            //if (pagedDataSource.PageCount <= 1)
            //    this.Visible = false;
            ////else
            ////    this.Visible = true;
            divPaginacion.Visible = true;
        }

        enum OpcionesPaginacionValores
        {
            PrimeraPagina = 1,
            SiguientePagina = 2,
            PaginaAnterior = 3,
            UltimaPagina = 4
        }

        private void MoverValoresPaginacion(int Accion) {

            int PaginaActual = 0;
            switch (Accion) {
                
                case 1:
                    //PRIMERA PAGINA
                    lbNumeroVariable.Text = "1";
                    
                    break;

                case 2:
                    //SEGUNDA PAGINA
                    PaginaActual = Convert.ToInt32(lbNumeroVariable.Text);
                    PaginaActual++;
                    lbNumeroVariable.Text = PaginaActual.ToString();
                    break;

                case 3:
                    //PAGINA ANTERIOR
                    PaginaActual = Convert.ToInt32(lbNumeroVariable.Text);
                    if (PaginaActual > 1) {
                        PaginaActual--;
                        lbNumeroVariable.Text = PaginaActual.ToString();
                    }
                    break;

                case 4:
                    //ULTIMA PAGINA
                    lbNumeroVariable.Text = lbCantidadPaginasVariable.Text;
                    break;


            }
        
        }

        #endregion
        #region CARGAR LAS LISTAS DESPLEGABLES PARA LA PANTALLA DE MANTENIMIENTO
        private void CargarListasDesplegablesPantallaMantenimiento() {
            CargarListaTipoProductoMantenimiento();
            CargarListaCategoriaMantenimiento();
            CargarUnidadMedidaMantenimiento();
            CargarListaMarcaMantenimient();
            CargarListasModeosMantenimiento();
            CargarListaTipoSuplidorMantenimiento();
            CargarListaSuplidorMantenimiento();
            CargarListaColoresMantenimiento();
            CargarListaCondicionesMantenimiento();
            CargarListaCapacidadMantenimiento();
        }
        private void CargarListaTipoProductoMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoProductoMantenimiento, ObjDataConfiguracion.Value.BuscaListas("TIPOPRODUCTO", null, null));

        }
        private void CargarListaCategoriaMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCategoriaMantenimiento, ObjDataConfiguracion.Value.BuscaListas("CATEGORIAS", ddlSeleccionarTipoProductoMantenimiento.SelectedValue.ToString(), null));
        }
        private void CargarUnidadMedidaMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarUnidadMedidaMantenimiento, ObjDataConfiguracion.Value.BuscaListas("UNIDADMEDIDA", null, null));
        }
        private void CargarListaMarcaMantenimient() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarMarcaMantenimiento, ObjDataConfiguracion.Value.BuscaListas("MARCAS", ddlSeleccionarTipoProductoMantenimiento.SelectedValue.ToString(), ddlSeleccionarCategoriaMantenimiento.SelectedValue.ToString()));
        }
        private void CargarListasModeosMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarModeloMantenimiento, ObjDataConfiguracion.Value.BuscaListas("MODELOS", ddlSeleccionarTipoProductoMantenimiento.SelectedValue.ToString(), ddlSeleccionarCategoriaMantenimiento.SelectedValue.ToString(), ddlSeleccionarMarcaMantenimiento.SelectedValue.ToString()));
        }
        private void CargarListaTipoSuplidorMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoSuplidorMantenimiento, ObjDataConfiguracion.Value.BuscaListas("TIPOSUPLIDOR", null, null));
        }
        private void CargarListaSuplidorMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarSuplidorMantenimiento, ObjDataConfiguracion.Value.BuscaListas("SUPLIDOR", ddlSeleccionarTipoSuplidorMantenimiento.SelectedValue.ToString(), null));
        }
        private void CargarListaColoresMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarColorMantenimiento, ObjDataConfiguracion.Value.BuscaListas("COLORES", null, null));
        }
        private void CargarListaCondicionesMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCondicionMantenimiento, ObjDataConfiguracion.Value.BuscaListas("CONDICION", null, null));
        }
        private void CargarListaCapacidadMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCapacidadMantenimiento, ObjDataConfiguracion.Value.BuscaListas("CAPACIDAD", null, null));
        }
        #endregion
        #region MODOS DE PANTALLA
        private void ModoServicio() {
            ddlSeleccionarTipoProductoMantenimiento.Enabled = true;
            ddlSeleccionarCategoriaMantenimiento.Enabled = true;
            ddlSeleccionarUnidadMedidaMantenimiento.Enabled = false;
            ddlSeleccionarMarcaMantenimiento.Enabled = false;
            ddlSeleccionarModeloMantenimiento.Enabled = false;
            ddlSeleccionarTipoSuplidorMantenimiento.Enabled = false;
            ddlSeleccionarSuplidorMantenimiento.Enabled = false;
            txtDescripcionMantenimiento.Enabled = true;
            txtCodigoBarraMantenimiento.Enabled = false;
            txtReferenciaMantenimiento.Enabled = false;
            txtPrecioCompraMantenimiento.Enabled = false;
            txtPrecioVentaMantenimiento.Enabled = true;
            txtStockMantenimiento.Enabled = false;
            txtStockMinimoMantenimiento.Enabled = false;
            txtPorcientoDescuentoMantenimiento.Enabled = true;
            txtNumeroSeguimientoMantenimiento.Enabled = false;
            ddlSeleccionarColorMantenimiento.Enabled = false;
            ddlSeleccionarCondicionMantenimiento.Enabled = false;
            ddlSeleccionarCapacidadMantenimiento.Enabled = false;
            txtComentarioMantenimiento.Enabled = true;
            cbProductoAcumulativoMantenimiento.Enabled = false;
            cbAplicaImpuestoMantenimiento.Enabled = false;
            cbNoLimpiarPantalla.Enabled = true;

            txtPrecioCompraMantenimiento.Text = "0";
            txtStockMantenimiento.Text = "1";
            txtStockMinimoMantenimiento.Text = "1";
        }
        private void ModoProducto() {
            ddlSeleccionarTipoProductoMantenimiento.Enabled = true;
            ddlSeleccionarCategoriaMantenimiento.Enabled = true;
            ddlSeleccionarUnidadMedidaMantenimiento.Enabled = true;
            ddlSeleccionarMarcaMantenimiento.Enabled = true;
            ddlSeleccionarModeloMantenimiento.Enabled = true;
            ddlSeleccionarTipoSuplidorMantenimiento.Enabled = true;
            ddlSeleccionarSuplidorMantenimiento.Enabled = true;
            txtDescripcionMantenimiento.Enabled = false;
            txtCodigoBarraMantenimiento.Enabled = true;
            txtReferenciaMantenimiento.Enabled = true;
            txtPrecioCompraMantenimiento.Enabled = true;
            txtPrecioVentaMantenimiento.Enabled = true;
            txtStockMantenimiento.Enabled = true;
            txtStockMinimoMantenimiento.Enabled = true;
            txtPorcientoDescuentoMantenimiento.Enabled = true;
            txtNumeroSeguimientoMantenimiento.Enabled = true;
            ddlSeleccionarColorMantenimiento.Enabled = true;
            ddlSeleccionarCondicionMantenimiento.Enabled = true;
            ddlSeleccionarCapacidadMantenimiento.Enabled = true;
            txtComentarioMantenimiento.Enabled = true;
            cbProductoAcumulativoMantenimiento.Enabled = true;
            cbAplicaImpuestoMantenimiento.Enabled = true;
            cbNoLimpiarPantalla.Enabled = true;

            txtPrecioCompraMantenimiento.Text = string.Empty;
            txtStockMantenimiento.Text = "1";
            txtStockMinimoMantenimiento.Text = "1";
        }

        private void ModoConsulta() {
            btnConsultarRegistrosConsulta.Enabled = true;
            btnNuevoConsulta.Enabled = true;
            btnModificarConsulta.Enabled = false;
            btnEliminarConsulta.Enabled = false;
            btnSuplirConsulta.Disabled = false;
            btnExportarConsulta.Enabled = false;
            btnDescartarConsulta.Disabled = false;
            btnRestablecerPantallaConsulta.Enabled = true;
            cbGraficarConsulta.Enabled = true;
        }
        private void ModoMantenimiento() {
            btnConsultarRegistrosConsulta.Enabled = false;
            btnNuevoConsulta.Enabled = false;
            btnModificarConsulta.Enabled = true;
            btnEliminarConsulta.Enabled = true;
            btnSuplirConsulta.Disabled = true;
            btnExportarConsulta.Enabled = true;
            btnDescartarConsulta.Disabled = true;
            btnRestablecerPantallaConsulta.Enabled = true;
            cbGraficarConsulta.Enabled = false;
            
        }
        #endregion
        #region UTILIDADES
        private void SacarPorcientoDescuento(int IdPorcientoDescuento) {
            DSMarketWeb.Logic.Comunes.SacarPorcientoDescuentoPrDefectoProductos Porcientodescuento = new Logic.Comunes.SacarPorcientoDescuentoPrDefectoProductos(IdPorcientoDescuento);
            txtPorcientoDescuentoMantenimiento.Text = Porcientodescuento.SacarPorcientodescuento().ToString();
        }
        #endregion
        #region GENERAR GRAFICOS
        private void GenerarGraficos() {
            GnerarGraficoTipoProducto();
            GenerarGraficoMarcas();
            GenerarGraficoServicios();
        }
        private void GnerarGraficoTipoProducto() {
            int[] CantidadRegistros = new int[2];
            string[] NombreServicio = new string[2];
            int Contador = 0;

            int _IdProducto = 0;
            int _NumeroConector = 0;
            string _Descripcion = string.IsNullOrEmpty(txtDescripcionConsulta.Text.Trim()) ? "N/A" : txtDescripcionConsulta.Text.Trim();
            string _CodigoBarra = string.IsNullOrEmpty(txtCodigoBarra.Text.Trim()) ? "N/A" : txtCodigoBarra.Text.Trim();
            string _Referencia = string.IsNullOrEmpty(txtReferenciaConsulta.Text.Trim()) ? "N/A" : txtReferenciaConsulta.Text.Trim();
            DateTime _FechaDesde, _FechaHasta;
            if (cbAgregarRangoFecha.Checked == true) {
                _FechaDesde = Convert.ToDateTime(txtFechaDesdeConsulta.Text);
                _FechaHasta = Convert.ToDateTime(txtFechaHAstaConsulta.Text);
            }
            else {
                _FechaDesde = Convert.ToDateTime("1900-01-01");
                _FechaHasta = Convert.ToDateTime("1900-01-01");
            }
            decimal _IdTipoProducto = ddlSeleccionarTipoProductoCOnsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarTipoProductoCOnsulta.SelectedValue) : 0;
            decimal _IdCategoria = ddlSeleccionarCategoria.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarCategoria.SelectedValue) : 0;
            decimal _IdUnidadMedida = ddlSeleccionarUnidadMedida.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarUnidadMedida.SelectedValue) : 0;
            decimal _IdMarca = ddlSeleccionarMarcaConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarMarcaConsulta.SelectedValue) : 0;
            decimal _IdModelo = ddlSeleccionarModelosConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarModelosConsulta.SelectedValue) : 0;
            decimal _IdColor = 0;
            decimal _Idcapacidad = 0;
            decimal _IdCondicion = 0;
            bool _TieneOferta = false;
            string _NumeroSeguimiento = string.IsNullOrEmpty(txtNumeroSeguimientoConsulta.Text.Trim()) ? "N/A" : txtNumeroSeguimientoConsulta.Text.Trim();
            bool _EstatusProducto = false;
            if (cbProductosVendisodDescartados.Checked == true) {
                _EstatusProducto = true;
            }
            else {
                _EstatusProducto = false;
            }


            SqlConnection Conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DSMarketWEBConexion"].ConnectionString);
            SqlCommand Comando = new SqlCommand("EXEC [Inventario].[SP_BUSCA_PRODUCTO_GRAFICO_WEB] @IdProducto,@NumeroConector,@Descripcion,@CodigoBarra,@Referencia,@FechaDesde,@FechaHasta,@IdTipoProducto,@IdCategoria,@IdUnidadMedida,@IdMarca,@IdModelo,@IdColor,@IdCapacidad,@IdCondicion,@TieneOferta,@EstatusProducto,@NumeroSeguimiento,@TipoGraficoGenerar", Conexion);

            Comando.Parameters.AddWithValue("@IdProducto", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@NumeroConector", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@Descripcion", SqlDbType.VarChar);
            Comando.Parameters.AddWithValue("@CodigoBarra", SqlDbType.VarChar);
            Comando.Parameters.AddWithValue("@Referencia", SqlDbType.VarChar);
            Comando.Parameters.AddWithValue("@FechaDesde", SqlDbType.Date);
            Comando.Parameters.AddWithValue("@FechaHasta", SqlDbType.Date);
            Comando.Parameters.AddWithValue("@IdTipoProducto", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@IdCategoria", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@IdUnidadMedida", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@IdMarca", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@IdModelo", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@IdColor", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@IdCapacidad", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@IdCondicion", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@TieneOferta", SqlDbType.Bit);
            Comando.Parameters.AddWithValue("@EstatusProducto", SqlDbType.Bit);
            Comando.Parameters.AddWithValue("@NumeroSeguimiento", SqlDbType.VarChar);
            Comando.Parameters.AddWithValue("@TipoGraficoGenerar", SqlDbType.Int);

            Comando.Parameters["@IdProducto"].Value = _IdProducto;
            Comando.Parameters["@NumeroConector"].Value = _NumeroConector;
            Comando.Parameters["@Descripcion"].Value = _Descripcion;
            Comando.Parameters["@CodigoBarra"].Value = _CodigoBarra;
            Comando.Parameters["@Referencia"].Value = _Referencia;
            Comando.Parameters["@FechaDesde"].Value = _FechaDesde;
            Comando.Parameters["@FechaHasta"].Value = _FechaHasta;
            Comando.Parameters["@IdTipoProducto"].Value = _IdTipoProducto;
            Comando.Parameters["@IdCategoria"].Value = _IdCategoria;
            Comando.Parameters["@IdUnidadMedida"].Value = _IdUnidadMedida;
            Comando.Parameters["@IdMarca"].Value = _IdMarca;
            Comando.Parameters["@IdModelo"].Value = _IdModelo;
            Comando.Parameters["@IdColor"].Value = _IdColor;
            Comando.Parameters["@IdCapacidad"].Value = _Idcapacidad;
            Comando.Parameters["@IdCondicion"].Value = _IdCondicion;
            Comando.Parameters["@TieneOferta"].Value = _TieneOferta;
            Comando.Parameters["@EstatusProducto"].Value = _EstatusProducto;
            Comando.Parameters["@NumeroSeguimiento"].Value = _NumeroSeguimiento;
            Comando.Parameters["@TipoGraficoGenerar"].Value = 1;

            Conexion.Open();
            SqlDataReader Reader = Comando.ExecuteReader();
            while (Reader.Read()) {
                CantidadRegistros[Contador] = Convert.ToInt32(Reader.GetInt32(1));
                NombreServicio[Contador] = Reader.GetString(0);
                Contador++;
            }
            Reader.Close();
            Conexion.Close();

            //GraTipoProductos.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}k";
            GraTipoProductos.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            GraTipoProductos.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
            GraTipoProductos.ChartAreas["ChartArea1"].AxisX.Interval = 1;

            GraTipoProductos.Series["Serie"].Points.DataBindXY(NombreServicio, CantidadRegistros);
        }
        private void GenerarGraficoMarcas() {
            int[] CantidadRegistros = new int[10];
            string[] Marca = new string[10];
            int Contador = 0;

            int _IdProducto = 0;
            int _NumeroConector = 0;
            string _Descripcion = string.IsNullOrEmpty(txtDescripcionConsulta.Text.Trim()) ? "N/A" : txtDescripcionConsulta.Text.Trim();
            string _CodigoBarra = string.IsNullOrEmpty(txtCodigoBarra.Text.Trim()) ? "N/A" : txtCodigoBarra.Text.Trim();
            string _Referencia = string.IsNullOrEmpty(txtReferenciaConsulta.Text.Trim()) ? "N/A" : txtReferenciaConsulta.Text.Trim();
            DateTime _FechaDesde, _FechaHasta;
            if (cbAgregarRangoFecha.Checked == true)
            {
                _FechaDesde = Convert.ToDateTime(txtFechaDesdeConsulta.Text);
                _FechaHasta = Convert.ToDateTime(txtFechaHAstaConsulta.Text);
            }
            else
            {
                _FechaDesde = Convert.ToDateTime("1900-01-01");
                _FechaHasta = Convert.ToDateTime("1900-01-01");
            }
            decimal _IdTipoProducto = ddlSeleccionarTipoProductoCOnsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarTipoProductoCOnsulta.SelectedValue) : 0;
            decimal _IdCategoria = ddlSeleccionarCategoria.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarCategoria.SelectedValue) : 0;
            decimal _IdUnidadMedida = ddlSeleccionarUnidadMedida.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarUnidadMedida.SelectedValue) : 0;
            decimal _IdMarca = ddlSeleccionarMarcaConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarMarcaConsulta.SelectedValue) : 0;
            decimal _IdModelo = ddlSeleccionarModelosConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarModelosConsulta.SelectedValue) : 0;
            decimal _IdColor = 0;
            decimal _Idcapacidad = 0;
            decimal _IdCondicion = 0;
            bool _TieneOferta = false;
            string _NumeroSeguimiento = string.IsNullOrEmpty(txtNumeroSeguimientoConsulta.Text.Trim()) ? "N/A" : txtNumeroSeguimientoConsulta.Text.Trim();
            bool _EstatusProducto = false;
            if (cbProductosVendisodDescartados.Checked == true)
            {
                _EstatusProducto = true;
            }
            else
            {
                _EstatusProducto = false;
            }


            SqlConnection Conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DSMarketWEBConexion"].ConnectionString);
            SqlCommand Comando = new SqlCommand("EXEC [Inventario].[SP_BUSCA_PRODUCTO_GRAFICO_WEB] @IdProducto,@NumeroConector,@Descripcion,@CodigoBarra,@Referencia,@FechaDesde,@FechaHasta,@IdTipoProducto,@IdCategoria,@IdUnidadMedida,@IdMarca,@IdModelo,@IdColor,@IdCapacidad,@IdCondicion,@TieneOferta,@EstatusProducto,@NumeroSeguimiento,@TipoGraficoGenerar", Conexion);

            Comando.Parameters.AddWithValue("@IdProducto", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@NumeroConector", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@Descripcion", SqlDbType.VarChar);
            Comando.Parameters.AddWithValue("@CodigoBarra", SqlDbType.VarChar);
            Comando.Parameters.AddWithValue("@Referencia", SqlDbType.VarChar);
            Comando.Parameters.AddWithValue("@FechaDesde", SqlDbType.Date);
            Comando.Parameters.AddWithValue("@FechaHasta", SqlDbType.Date);
            Comando.Parameters.AddWithValue("@IdTipoProducto", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@IdCategoria", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@IdUnidadMedida", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@IdMarca", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@IdModelo", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@IdColor", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@IdCapacidad", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@IdCondicion", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@TieneOferta", SqlDbType.Bit);
            Comando.Parameters.AddWithValue("@EstatusProducto", SqlDbType.Bit);
            Comando.Parameters.AddWithValue("@NumeroSeguimiento", SqlDbType.VarChar);
            Comando.Parameters.AddWithValue("@TipoGraficoGenerar", SqlDbType.Int);

            Comando.Parameters["@IdProducto"].Value = _IdProducto;
            Comando.Parameters["@NumeroConector"].Value = _NumeroConector;
            Comando.Parameters["@Descripcion"].Value = _Descripcion;
            Comando.Parameters["@CodigoBarra"].Value = _CodigoBarra;
            Comando.Parameters["@Referencia"].Value = _Referencia;
            Comando.Parameters["@FechaDesde"].Value = _FechaDesde;
            Comando.Parameters["@FechaHasta"].Value = _FechaHasta;
            Comando.Parameters["@IdTipoProducto"].Value = _IdTipoProducto;
            Comando.Parameters["@IdCategoria"].Value = _IdCategoria;
            Comando.Parameters["@IdUnidadMedida"].Value = _IdUnidadMedida;
            Comando.Parameters["@IdMarca"].Value = _IdMarca;
            Comando.Parameters["@IdModelo"].Value = _IdModelo;
            Comando.Parameters["@IdColor"].Value = _IdColor;
            Comando.Parameters["@IdCapacidad"].Value = _Idcapacidad;
            Comando.Parameters["@IdCondicion"].Value = _IdCondicion;
            Comando.Parameters["@TieneOferta"].Value = _TieneOferta;
            Comando.Parameters["@EstatusProducto"].Value = _EstatusProducto;
            Comando.Parameters["@NumeroSeguimiento"].Value = _NumeroSeguimiento;
            Comando.Parameters["@TipoGraficoGenerar"].Value = 2;

            Conexion.Open();
            SqlDataReader Reader = Comando.ExecuteReader();
            while (Reader.Read())
            {
                CantidadRegistros[Contador] = Convert.ToInt32(Reader.GetInt32(1));
                Marca[Contador] = Reader.GetString(0);
                Contador++;
            }
            Reader.Close();
            Conexion.Close();

         //   GraMarcas.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}k";
            GraMarcas.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            GraMarcas.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
            GraMarcas.ChartAreas["ChartArea1"].AxisX.Interval = 1;

            GraMarcas.Series["Serie"].Points.DataBindXY(Marca, CantidadRegistros);
        }
        private void GenerarGraficoServicios() {
            decimal[] Precio = new decimal[10];
            string[] DescripcionServicio = new string[10];
            int Contador = 0;

            int _IdProducto = 0;
            int _NumeroConector = 0;
            string _Descripcion = string.IsNullOrEmpty(txtDescripcionConsulta.Text.Trim()) ? "N/A" : txtDescripcionConsulta.Text.Trim();
            string _CodigoBarra = string.IsNullOrEmpty(txtCodigoBarra.Text.Trim()) ? "N/A" : txtCodigoBarra.Text.Trim();
            string _Referencia = string.IsNullOrEmpty(txtReferenciaConsulta.Text.Trim()) ? "N/A" : txtReferenciaConsulta.Text.Trim();
            DateTime _FechaDesde, _FechaHasta;
            if (cbAgregarRangoFecha.Checked == true)
            {
                _FechaDesde = Convert.ToDateTime(txtFechaDesdeConsulta.Text);
                _FechaHasta = Convert.ToDateTime(txtFechaHAstaConsulta.Text);
            }
            else
            {
                _FechaDesde = Convert.ToDateTime("1900-01-01");
                _FechaHasta = Convert.ToDateTime("1900-01-01");
            }
            decimal _IdTipoProducto = ddlSeleccionarTipoProductoCOnsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarTipoProductoCOnsulta.SelectedValue) : 0;
            decimal _IdCategoria = ddlSeleccionarCategoria.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarCategoria.SelectedValue) : 0;
            decimal _IdUnidadMedida = ddlSeleccionarUnidadMedida.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarUnidadMedida.SelectedValue) : 0;
            decimal _IdMarca = ddlSeleccionarMarcaConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarMarcaConsulta.SelectedValue) : 0;
            decimal _IdModelo = ddlSeleccionarModelosConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarModelosConsulta.SelectedValue) : 0;
            decimal _IdColor = 0;
            decimal _Idcapacidad = 0;
            decimal _IdCondicion = 0;
            bool _TieneOferta = false;
            string _NumeroSeguimiento = string.IsNullOrEmpty(txtNumeroSeguimientoConsulta.Text.Trim()) ? "N/A" : txtNumeroSeguimientoConsulta.Text.Trim();
            bool _EstatusProducto = false;
            if (cbProductosVendisodDescartados.Checked == true)
            {
                _EstatusProducto = true;
            }
            else
            {
                _EstatusProducto = false;
            }


            SqlConnection Conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DSMarketWEBConexion"].ConnectionString);
            SqlCommand Comando = new SqlCommand("EXEC [Inventario].[SP_BUSCA_PRODUCTO_GRAFICO_WEB] @IdProducto,@NumeroConector,@Descripcion,@CodigoBarra,@Referencia,@FechaDesde,@FechaHasta,@IdTipoProducto,@IdCategoria,@IdUnidadMedida,@IdMarca,@IdModelo,@IdColor,@IdCapacidad,@IdCondicion,@TieneOferta,@EstatusProducto,@NumeroSeguimiento,@TipoGraficoGenerar", Conexion);

            Comando.Parameters.AddWithValue("@IdProducto", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@NumeroConector", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@Descripcion", SqlDbType.VarChar);
            Comando.Parameters.AddWithValue("@CodigoBarra", SqlDbType.VarChar);
            Comando.Parameters.AddWithValue("@Referencia", SqlDbType.VarChar);
            Comando.Parameters.AddWithValue("@FechaDesde", SqlDbType.Date);
            Comando.Parameters.AddWithValue("@FechaHasta", SqlDbType.Date);
            Comando.Parameters.AddWithValue("@IdTipoProducto", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@IdCategoria", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@IdUnidadMedida", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@IdMarca", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@IdModelo", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@IdColor", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@IdCapacidad", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@IdCondicion", SqlDbType.Decimal);
            Comando.Parameters.AddWithValue("@TieneOferta", SqlDbType.Bit);
            Comando.Parameters.AddWithValue("@EstatusProducto", SqlDbType.Bit);
            Comando.Parameters.AddWithValue("@NumeroSeguimiento", SqlDbType.VarChar);
            Comando.Parameters.AddWithValue("@TipoGraficoGenerar", SqlDbType.Int);

            Comando.Parameters["@IdProducto"].Value = _IdProducto;
            Comando.Parameters["@NumeroConector"].Value = _NumeroConector;
            Comando.Parameters["@Descripcion"].Value = _Descripcion;
            Comando.Parameters["@CodigoBarra"].Value = _CodigoBarra;
            Comando.Parameters["@Referencia"].Value = _Referencia;
            Comando.Parameters["@FechaDesde"].Value = _FechaDesde;
            Comando.Parameters["@FechaHasta"].Value = _FechaHasta;
            Comando.Parameters["@IdTipoProducto"].Value = _IdTipoProducto;
            Comando.Parameters["@IdCategoria"].Value = _IdCategoria;
            Comando.Parameters["@IdUnidadMedida"].Value = _IdUnidadMedida;
            Comando.Parameters["@IdMarca"].Value = _IdMarca;
            Comando.Parameters["@IdModelo"].Value = _IdModelo;
            Comando.Parameters["@IdColor"].Value = _IdColor;
            Comando.Parameters["@IdCapacidad"].Value = _Idcapacidad;
            Comando.Parameters["@IdCondicion"].Value = _IdCondicion;
            Comando.Parameters["@TieneOferta"].Value = _TieneOferta;
            Comando.Parameters["@EstatusProducto"].Value = _EstatusProducto;
            Comando.Parameters["@NumeroSeguimiento"].Value = _NumeroSeguimiento;
            Comando.Parameters["@TipoGraficoGenerar"].Value = 3;

            Conexion.Open();
            SqlDataReader Reader = Comando.ExecuteReader();
            while (Reader.Read())
            {
                Precio[Contador] = Convert.ToDecimal(Reader.GetDecimal(1));
                DescripcionServicio[Contador] = Reader.GetString(0);
                Contador++;
            }
            Reader.Close();
            Conexion.Close();

            GraServicios.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}k";
            GraServicios.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            GraServicios.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
            GraServicios.ChartAreas["ChartArea1"].AxisX.Interval = 1;

            GraServicios.Series["Serie"].Points.DataBindXY(DescripcionServicio, Precio);
        }
        #endregion
        #region GENERAR PROCESAR INFORMACION DEL PRODUCTO
        private void ProcesarInformacionProducto(decimal IdProducto, decimal NumeroConector, string Accion) {
            DSMarketWeb.Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionProductos Procesar = new Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionProductos(
                IdProducto,
                NumeroConector,
                Convert.ToDecimal(ddlSeleccionarTipoProductoMantenimiento.SelectedValue),
                Convert.ToDecimal(ddlSeleccionarCategoriaMantenimiento.SelectedValue),
                Convert.ToDecimal(ddlSeleccionarUnidadMedidaMantenimiento.SelectedValue),
                Convert.ToDecimal(ddlSeleccionarMarcaMantenimiento.SelectedValue),
                Convert.ToDecimal(ddlSeleccionarModeloMantenimiento.SelectedValue),
                Convert.ToDecimal(ddlSeleccionarTipoSuplidorMantenimiento.SelectedValue),
                Convert.ToDecimal(ddlSeleccionarSuplidorMantenimiento.SelectedValue),
                txtDescripcionMantenimiento.Text,
                txtCodigoBarraMantenimiento.Text,
                txtReferenciaMantenimiento.Text,
                Convert.ToDecimal(txtPrecioCompraMantenimiento.Text),
                Convert.ToDecimal(txtPrecioVentaMantenimiento.Text),
                Convert.ToDecimal(txtStockMantenimiento.Text),
                Convert.ToDecimal(txtStockMinimoMantenimiento.Text),
                Convert.ToDecimal(txtPorcientoDescuentoMantenimiento.Text),
                false,
                cbProductoAcumulativoMantenimiento.Checked,
                false,
                Convert.ToDecimal(Session["IdUsuario"]),
                DateTime.Now,
                Convert.ToDecimal(Session["IdUsuario"]),
                DateTime.Now,
                DateTime.Now,
                txtComentarioMantenimiento.Text,
                cbAplicaImpuestoMantenimiento.Checked,
                false,
                txtNumeroSeguimientoMantenimiento.Text,
                Convert.ToDecimal(ddlSeleccionarColorMantenimiento.SelectedValue),
                Convert.ToDecimal(ddlSeleccionarCondicionMantenimiento.SelectedValue),
                Convert.ToDecimal(ddlSeleccionarCapacidadMantenimiento.SelectedValue),
                Accion);
            Procesar.ProcesarProducto();
        }
        #endregion
        #region GENERAR NUMERO DE CONECTOR
        private void GenerarNumeroConector() {

            Random NumeroConector = new Random();
            int Numero = NumeroConector.Next(0, 999999999);
            lbNumeroConectorProducto.Text = Numero.ToString();
        }
        #endregion
        #region LIMPIAR CONTROLES y VOLVER ATRAS, RESTABLECER
        private void LimpiarControlesCOnsulta() {
            CargarListasDesplegablesCOnsulta();
            cbAgregarRangoFecha.Checked = false;
            cbMostrarTodoHistorialVenta.Checked = false;
            cbProductosVendisodDescartados.Checked = false;
            rbExportarPDF.Checked = true;
            txtFechaDesdeConsulta.Text = string.Empty;
            txtFechaHAstaConsulta.Text = string.Empty;
            txtDescripcionConsulta.Text = string.Empty;
            txtCodigoBarra.Text = string.Empty;
            txtReferenciaConsulta.Text = string.Empty;
            txtNumeroSeguimientoConsulta.Text = string.Empty;
        }
        private void LimpiarCOntrolesMantenimiento() {
            txtDescripcionMantenimiento.Text = string.Empty;
            txtCodigoBarraMantenimiento.Text = string.Empty;
            txtReferenciaMantenimiento.Text = string.Empty;
            txtPrecioCompraMantenimiento.Text = string.Empty;
            txtPrecioVentaMantenimiento.Text = string.Empty;
            txtStockMantenimiento.Text = "1";
            txtStockMinimoMantenimiento.Text = "1";
            txtNumeroSeguimientoMantenimiento.Text = string.Empty;
            txtComentarioMantenimiento.Text = string.Empty;
            cbProductoAcumulativoMantenimiento.Checked = false;
            cbAplicaImpuestoMantenimiento.Checked = false;
            ValidarCheckLimpiarPantalla();
            SacarPorcientoDescuento(1);
        }
        private void VolverAtras() {
            divBloqueConsulta.Visible = true;
            divBloqueDetalle.Visible = false;
            divBloqueSuplir.Visible = false;
            divBloqueDescartar.Visible = false;
            divBloqueMantenimiento.Visible = false;
            LimpiarControlesCOnsulta();
            LimpiarCOntrolesMantenimiento();
        }
        private void RestablecerPantallaCOnsulta() {
        
        
        }
        #endregion
        #region VALIDAR FNCIONES
        private void ValidarCheckLimpiarPantalla()
        {
            cbNoLimpiarPantalla.Visible = true;
            int IdConfiguracionGeneral = (int)DSMarketWeb.Logic.Comunes.ValidarConfiguracionGenera.ConceptoConfiguracionGeneral.VALIDAR_CHECK_LIMPIAR_PANTALLA_EN_MANTENIMIENTOS;
            bool Estatus = false;
            DSMarketWeb.Logic.Comunes.ValidarConfiguracionGenera Validar = new Logic.Comunes.ValidarConfiguracionGenera(IdConfiguracionGeneral);
            Estatus = Validar.SacarEstatusConfiguracionGeneral();
            if (Estatus == true)
            {
                cbNoLimpiarPantalla.Checked = true;
            }
            else
            {
                cbNoLimpiarPantalla.Checked = false;
            }


        }
        #endregion
        #region GUARDAR IMAGEN DE PRODUCTO

        /// <summary>
        /// Este metodo es para guardar la imagen la base de datos
        /// </summary>
        /// <param name="IdProducto"></param>
        /// <param name="NumeroConector"></param>
        private void GuardarImagenProdicto(decimal IdProducto, decimal NumeroConector) {
            int Tamanio = UpImagen.PostedFile.ContentLength;
            byte[] ImagenOriginal = new byte[Tamanio];
            UpImagen.PostedFile.InputStream.Read(ImagenOriginal, 0, Tamanio);
            Bitmap ImagenOriginalBinaria = new Bitmap(UpImagen.PostedFile.InputStream);

            //REDIRECCIONAR LA IMAGEN PARA COLOCARLE UN TAMAñO A VOLUNTAD
            System.Drawing.Image ImgThumbNail;
            int TamanioThumbNail = 200;
            ImgThumbNail = DSMarketWeb.Logic.Comunes.RedimencionarImagen.Redireccionar(ImagenOriginalBinaria, TamanioThumbNail);
            ImageConverter Convertidor = new ImageConverter();
            byte[] bImagenThumbNail = (byte[])Convertidor.ConvertTo(ImgThumbNail, typeof(byte[]));


            //GUARDMOS LA IMAGEN EN BASE DE DATOS
            SqlConnection Conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DSMarketWEBConexion"].ConnectionString);
            SqlCommand Comando = new SqlCommand("EXEC [Inventario].[SP_GUARDAR_FOTO_PRODUCTO] @IdProducto,@NumeroConector,@FotoProducto", Conexion);
            Comando.Parameters.Add("@IdProducto", SqlDbType.Decimal).Value = IdProducto;
            Comando.Parameters.Add("@NumeroConector", SqlDbType.Decimal).Value = NumeroConector;
            Comando.Parameters.Add("@FotoProducto", SqlDbType.Image).Value = bImagenThumbNail;
            Conexion.Open();
            Comando.ExecuteNonQuery();
            Conexion.Close();

            //VISUALIZAMOS LA IMAGEN EN EL CONTROL IMAGEN LUEGO DE HABER GUARDADO
            string ImagenDataUrl64 = "data:image/jpg;base64," + Convert.ToBase64String(bImagenThumbNail);
            IMGProducto.ImageUrl = ImagenDataUrl64;



        }
        #endregion
        #region MOSTRAR LA FOTO DEL PROEUCTO SELECCIONADO
        private void MostrarFotoProductoSeleccionado(decimal IdProducto) {
            var ValidarFotoProducto = ObjdataInventario.Value.BuscaFotoProucto(IdProducto, null);
            if (ValidarFotoProducto.Count() < 1) { }
            else {

                SqlConnection Conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DSMarketWEBConexion"].ConnectionString);
                Conexion.Open();
                string Query = "select FotoProducto from Inventario.FotoProducto where IdProducto = " + IdProducto;
                SqlCommand comando = new SqlCommand(Query, Conexion);
                comando.CommandTimeout = 0;
                byte[] IMG = (byte[])comando.ExecuteScalar();

                IMGFotoProducto.ImageUrl = "data:image/jpg;base64," + Convert.ToBase64String(IMG);
                Conexion.Close();
            }
        }
        #endregion
        #region BOTONONES DE LA PAGINACION
        private void PrimeraPagina() { }
        private void SiguientePagina() { }
        private void PaginaAnterior() { }
        private void UltimaPagina() { }
        
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                divPaginacion.Visible = false;
                ModoConsulta();
                ValidarCheckLimpiarPantalla();
                SacarPorcientoDescuento(1);
                txtStockMantenimiento.Text = "1";
                txtStockMinimoMantenimiento.Text = "1";
                txtStockMantenimiento.Enabled = false;
                txtStockMinimoMantenimiento.Enabled = false;
                txtDescripcionMantenimiento.Enabled = false;
                divBloqueConsulta.Visible = true;
                divBloqueMantenimiento.Visible = false;
                divBloqueDetalle.Visible = false;
                CargarListasDesplegablesCOnsulta();
                CargarListasDesplegablesPantallaMantenimiento();

                divGraficoMarcas.Visible = false;
                divGraficoServicios.Visible = false;
                divTipoProducto.Visible = false;
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
            CurrentPage = 0;
            lbNumeroVariable.Text = "1";
            BindDataIntoRepeater(10);
            divBloqueDetalle.Visible = false;
        }

        protected void btnNuevoConsulta_Click(object sender, EventArgs e)
        {

            divBloqueConsulta.Visible = false;
            divBloqueDetalle.Visible = false;
            divBloqueSuplir.Visible = false;
            divBloqueDescartar.Visible = false;
            divBloqueMantenimiento.Visible = true;
            btnProcesarMantenimiento.Visible = true;
            btnModificarMantenimiento.Visible = false;
            btnEliminarMantenimiento.Visible = false;
            lbClaveSeguridadMantenimiento.Visible = false;
            txtclaveSeguridadMantenimiento.Visible = false;
            ValidarCheckLimpiarPantalla();
        }

        protected void btnModificarConsulta_Click(object sender, EventArgs e)
        {
            divBloqueConsulta.Visible = false;
            divBloqueDetalle.Visible = false;
            divBloqueSuplir.Visible = false;
            divBloqueDescartar.Visible = false;
            divBloqueMantenimiento.Visible = true;
            btnProcesarMantenimiento.Visible = false;
            btnModificarMantenimiento.Visible = true;
            btnEliminarMantenimiento.Visible = false;
            lbClaveSeguridadMantenimiento.Visible = true;
            txtclaveSeguridadMantenimiento.Visible = true;
        }

        protected void btnEliminarConsulta_Click(object sender, EventArgs e)
        {
            divBloqueConsulta.Visible = false;
            divBloqueDetalle.Visible = false;
            divBloqueSuplir.Visible = false;
            divBloqueDescartar.Visible = false;
            divBloqueMantenimiento.Visible = true;
            btnProcesarMantenimiento.Visible = false;
            btnModificarMantenimiento.Visible = false;
            btnEliminarMantenimiento.Visible = true;
            lbClaveSeguridadMantenimiento.Visible = true;
            txtclaveSeguridadMantenimiento.Visible = true;
        }

        protected void btnExportarConsulta_Click(object sender, EventArgs e)
        {

        }



        protected void ddlSeleccionarTipoProductoMantenimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarListaCategoriaMantenimiento();
            CargarListaMarcaMantenimient();
            CargarListasModeosMantenimiento();

            int TipoProductoSeleciconado = Convert.ToInt32(ddlSeleccionarTipoProductoMantenimiento.SelectedValue);
            if (TipoProductoSeleciconado == 1) {
                ModoProducto();
            }
            else {
                ModoServicio();
            }
        }

        protected void ddlSeleccionarCategoriaMantenimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarListaMarcaMantenimient();
            CargarListasModeosMantenimiento();
        }

        protected void ddlSeleccionarMarcaMantenimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarListasModeosMantenimiento();
        }

        protected void ddlSeleccionarModeloMantenimiento_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnProcesarMantenimiento_Click(object sender, EventArgs e)
        {
            //GENERAMOS EL NUMERO DE CONECTOR
            GenerarNumeroConector();

            lbIdProductoSeleccionado.Text = "0";
            ProcesarInformacionProducto(Convert.ToDecimal(lbIdProductoSeleccionado.Text), Convert.ToDecimal(lbNumeroConectorProducto.Text), "INSERT");
            ClientScript.RegisterStartupScript(GetType(), "RegistroGuardadoConExito()", "RegistroGuardadoConExito();", true);

            //GUARDAMOS AL IMAGEN DEL PRODUCTO
            if (cbAgregarImagenArticulo.Checked == true) {
                DSMarketWeb.Logic.Comunes.SacarIdCreadoProducto SacarID = new Logic.Comunes.SacarIdCreadoProducto(
                    Convert.ToDecimal(lbNumeroConectorProducto.Text),
                    Convert.ToDecimal(ddlSeleccionarTipoProductoMantenimiento.SelectedValue),
                    Convert.ToDecimal(ddlSeleccionarCategoriaMantenimiento.SelectedValue),
                    Convert.ToDecimal(ddlSeleccionarMarcaMantenimiento.SelectedValue),
                    Convert.ToDecimal(ddlSeleccionarModeloMantenimiento.SelectedValue),
                    Convert.ToDecimal(ddlSeleccionarColorMantenimiento.SelectedValue),
                    Convert.ToDecimal(ddlSeleccionarCapacidadMantenimiento.SelectedValue),
                    Convert.ToDecimal(ddlSeleccionarCondicionMantenimiento.SelectedValue));

                GuardarImagenProdicto(SacarID.SacarIdProductoCreado(), Convert.ToDecimal(lbNumeroConectorProducto.Text));
                
            }


            if (cbNoLimpiarPantalla.Checked == true) {
                txtReferenciaMantenimiento.Text = string.Empty;
                GenerarNumeroConector();

                
            }

            else {
                VolverAtras();
            }
        }

        protected void btnVolverMantenimiento_Click(object sender, EventArgs e)
        {
            VolverAtras();
        }

        protected void btnModificarMantenimiento_Click(object sender, EventArgs e)
        {
            //VALIDAMOS EL CAMPO CLAVE DE SEGURIDAD
            string _ClaveSeguridad = string.IsNullOrEmpty(txtclaveSeguridadMantenimiento.Text.Trim()) ? null : txtclaveSeguridadMantenimiento.Text.Trim();

            var ValidarClave = ObjDataSeguridad.Value.BuscaClaveSeguridad(
                new Nullable<decimal>(),
                null,
                DSMarketWeb.Logic.Comunes.SeguridadEncriptacion.Encriptar(_ClaveSeguridad));
            if (ValidarClave.Count() < 1) {
                ClientScript.RegisterStartupScript(GetType(), "ClaveSeguridadIngresadaNoValida()", "ClaveSeguridadIngresadaNoValida();", true);
                txtclaveSeguridadMantenimiento.Text = string.Empty;
                txtclaveSeguridadMantenimiento.Focus();
            }
            else {
                ProcesarInformacionProducto(Convert.ToDecimal(lbIdProductoSeleccionado.Text), Convert.ToDecimal(lbNumeroConectorProducto.Text), "UPDATE");
                VolverAtras();
                ModoConsulta();
                var Buscar = ObjdataInventario.Value.BuscaProductos(
                    Convert.ToDecimal(lbIdProductoSeleccionado.Text),
                    Convert.ToDecimal(lbNumeroConectorProducto.Text));
                Paginar(ref RVListadoProducto, Buscar, 1);
            }
        }

        protected void btnEliminarMantenimiento_Click(object sender, EventArgs e)
        {

        }

        protected void cbProductoAcumulativoMantenimiento_CheckedChanged(object sender, EventArgs e)
        {
            if (cbProductoAcumulativoMantenimiento.Checked == true) {
                txtStockMantenimiento.Enabled = true;
                txtStockMinimoMantenimiento.Enabled = true;
            }
            else {
                txtStockMantenimiento.Text = "1";
                txtStockMinimoMantenimiento.Text = "1";
                txtStockMantenimiento.Enabled = false;
                txtStockMinimoMantenimiento.Enabled = false;
            }
        }

        protected void cbGraficarConsulta_CheckedChanged(object sender, EventArgs e)
        {
            if (cbGraficarConsulta.Checked == true) {
                //divGraficoMarcas.Visible = true;
                //divGraficoServicios.Visible = true;
                //divTipoProducto.Visible = true;
            }
            else {
                divGraficoMarcas.Visible = false;
                divGraficoServicios.Visible = false;
                divTipoProducto.Visible = false;
            }
        }

        protected void btnVolverDetalle_Click(object sender, EventArgs e)
        {
            divBloqueDetalle.Visible = false;
            ModoConsulta();
        }

        protected void btnRestablecerPantallaConsulta_Click(object sender, EventArgs e)
        {

        }

        protected void btnProcesarSuplirSacar_Click(object sender, EventArgs e)
        {

        }

        protected void cbAgregarImagenArticulo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAgregarImagenArticulo.Checked == true) {
                DivBloqueImagenProducto.Visible = true;
            }
            else {
                DivBloqueImagenProducto.Visible = false;
            }
        }

        protected void btnVisualizarImagen_Click(object sender, EventArgs e)
        {
            GuardarImagenProdicto(200, 300);
        }

        protected void rptPaging_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            BindDataIntoRepeater(10);
        }

        protected void rptPaging_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            var lnkPage = (LinkButton)e.Item.FindControl("lbPaging");
            if (lnkPage.CommandArgument != CurrentPage.ToString()) return;
            lnkPage.Enabled = false;
        }

        protected void lbFirst_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
         //   lbNumeroVariable.Text = CurrentPage.ToString();
            BindDataIntoRepeater(10);
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PrimeraPagina);
        }

        protected void lbPrevious_Click(object sender, EventArgs e)
        {
            CurrentPage += -1;
          //  lbNumeroVariable.Text = CurrentPage.ToString();
            BindDataIntoRepeater(10);
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior);
        }

        protected void lbNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
           // lbNumeroVariable.Text = CurrentPage.ToString();

            BindDataIntoRepeater(10);
            MoverValoresPaginacion((int)OpcionesPaginacionValores.SiguientePagina);
        }

        protected void btnSeleccionarRegistro_Click(object sender, EventArgs e)
        {
            var IdProductpSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var NumeroConectorSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfIdProducto = decimal.Parse((((HiddenField)IdProductpSeleccionado.FindControl("hfIdProducto")).Value.ToString()));
            var HfNumeroConector = decimal.Parse((((HiddenField)NumeroConectorSeleccionado.FindControl("hfNumeroConector")).Value.ToString()));

            //BUSCAMOS EL REGISTRO
            var BuscarRegistro = ObjdataInventario.Value.BuscaProductos(
                Convert.ToDecimal(hfIdProducto),
                Convert.ToDecimal(HfNumeroConector));
            Paginar(ref RVListadoProducto, BuscarRegistro, 1);
            lbNumeroVariable.Text = "1";
            divBloqueDetalle.Visible = true;
            //SACAMOS LOS DATOS DEL PRODUCTO DETALLE
            foreach (var n in BuscarRegistro) {
                txtTipoProductoDetalle.Text = n.TipoProducto;
                txtCategoriaDetalle.Text = n.Categoria;
                txtUnidadMedidaDetalle.Text = n.UnidadMedida;
                txtMarcaDetalle.Text = n.Marca;
                txtModeloDetalle.Text = n.Modelo;
                txtTipoSuplidorDetalle.Text = n.TipoSuplidor;
                txtSuplidorDetalle.Text = n.Suplidor;
                txtDescripcionDetalle.Text = n.PorcientoDescuento.ToString();
                txtCodigoBarraDetalle.Text = n.CodigoBarra;
                txtReferenciaDetalle.Text = n.Referencia;
                decimal PrecioCompra = Convert.ToDecimal(n.PrecioCompra);
                txtPrecioCompraDetalle.Text = PrecioCompra.ToString("N2");
                decimal PrecioVenta = Convert.ToDecimal(n.PrecioVenta);
                txtPrecioVentaDetalle.Text = PrecioVenta.ToString("N2");
                int Stock = Convert.ToInt32(n.Stock);
                txtStockDetalle.Text = Stock.ToString("N0");
                int StockMinimo = Convert.ToInt32(n.StockMinimo);
                txtStockMinimo.Text = StockMinimo.ToString("N0");
                txtPorcientoDescuentoDetalle.Text = n.PorcientoDescuento.ToString();
                txtNumeroSeguimientoDetalle.Text = n.NumeroSeguimiento;
                txtColorDetalle.Text = n.Color;
                txtCondcionDetalle.Text = n.Condicion;
                txtCapacidadDetalle.Text = n.Capacidad;
                txtProductoAcumulativoDetalle.Text = n.ProductoAcumulativo;
                txtAplicaParaImpuestoDetalle.Text = n.AplicaParaImpuesto;
                txtComentarioDetalle.Text = n.Comentario;
            }
            foreach (var Seleccionar in BuscarRegistro) {
                CargarListaTipoProductoMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarTipoProductoMantenimiento, Seleccionar.IdTipoProducto.ToString());
                CargarListaCategoriaMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarCategoriaMantenimiento, Seleccionar.IdCategoria.ToString());
                CargarUnidadMedidaMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarUnidadMedidaMantenimiento, Seleccionar.IdUnidadMedida.ToString());
                CargarListaMarcaMantenimient();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarMarcaMantenimiento, Seleccionar.IdMarca.ToString());
                CargarListasModeosMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarModeloMantenimiento, Seleccionar.IdModelo.ToString());
                CargarListaTipoSuplidorMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarTipoSuplidorMantenimiento, Seleccionar.IdTipoSuplidor.ToString());
                CargarListaSuplidorMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarSuplidorMantenimiento, Seleccionar.IdSuplidor.ToString());
                txtDescripcionMantenimiento.Text = Seleccionar.Producto;
                txtCodigoBarraMantenimiento.Text = Seleccionar.CodigoBarra;
                txtReferenciaMantenimiento.Text = Seleccionar.Referencia;
                txtPrecioCompraMantenimiento.Text = Seleccionar.PrecioCompra.ToString();
                txtPrecioVentaMantenimiento.Text = Seleccionar.PrecioVenta.ToString();
                txtStockMantenimiento.Text = Seleccionar.Stock.ToString();
                txtStockMinimoMantenimiento.Text = Seleccionar.StockMinimo.ToString();
                txtPorcientoDescuentoMantenimiento.Text = Seleccionar.PorcientoDescuento.ToString();
                txtNumeroSeguimientoMantenimiento.Text = Seleccionar.NumeroSeguimiento;
                CargarListaColoresMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarColorMantenimiento, Seleccionar.IdColor.ToString());
                CargarListaCondicionesMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarCondicionMantenimiento, Seleccionar.IdCondicion.ToString());
                CargarListaCapacidadMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarCapacidadMantenimiento, Seleccionar.IdCapacidad.ToString());
                txtComentarioMantenimiento.Text = Seleccionar.Comentario;
                cbProductoAcumulativoMantenimiento.Checked = (Seleccionar.ProductoAcumulativo0.HasValue ? Seleccionar.ProductoAcumulativo0.Value : false);
                cbAplicaImpuestoMantenimiento.Checked = (Seleccionar.AplicaParaImpuesto0.HasValue ? Seleccionar.AplicaParaImpuesto0.Value : false);
                cbAgregarImagenArticulo.Checked = (Seleccionar.LlevaImagen0.HasValue ? Seleccionar.LlevaImagen0.Value : false);

                
            }
            lbIdProductoSeleccionado.Text = hfIdProducto.ToString();
            lbNumeroConectorProducto.Text = HfNumeroConector.ToString();
            MostrarFotoProductoSeleccionado((decimal)hfIdProducto);
            ModoMantenimiento();
            cbGraficarConsulta.Visible = false;
            divGraficoMarcas.Visible = false;
            divGraficoServicios.Visible = false;
            divTipoProducto.Visible = false;
        }

        protected void lbLast_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
          //  lbNumeroVariable.Text = CurrentPage.ToString();
            BindDataIntoRepeater(10);
            MoverValoresPaginacion((int)OpcionesPaginacionValores.UltimaPagina);
        }
    }
}