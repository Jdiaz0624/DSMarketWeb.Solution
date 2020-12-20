using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

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

            if (cbGraficarConsulta.Checked == true)
            {
                divTipoProducto.Visible = true;
                divGraficoMarcas.Visible = true;
                divGraficoServicios.Visible = true;
                GenerarGraficos();
            }
            else { 
            
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
        protected void Page_Load(object sender, EventArgs e)
        {
            
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
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
            MostrarListadoInventario();
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
            GridViewRow gv = gvListado.SelectedRow;
            divBloqueMantenimiento.Visible = false;
            divBloqueDetalle.Visible = true;
            cbGraficarConsulta.Checked = false;
            divGraficoMarcas.Visible = false;
            divGraficoServicios.Visible = false;
            divTipoProducto.Visible = false;
            var BuscarRegistroSeleccionado = ObjdataInventario.Value.BuscaProductos(
                Convert.ToDecimal(gv.Cells[1].Text),
                Convert.ToDecimal(gv.Cells[2].Text),
                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
            gvListado.DataSource = BuscarRegistroSeleccionado;
            gvListado.DataBind();
            foreach (var n in BuscarRegistroSeleccionado) {
                txtTipoProductoDetalle.Text = n.TipoProducto;
                txtCategoriaDetalle.Text = n.Categoria;
                txtUnidadMedidaDetalle.Text = n.UnidadMedida;
                txtMarcaDetalle.Text = n.Marca;
                txtModeloDetalle.Text = n.Modelo;
                txtTipoSuplidorDetalle.Text = n.TipoSuplidor;
                txtSuplidorDetalle.Text = n.Suplidor;
                txtDescripcionDetalle.Text = n.Producto;
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
            ModoMantenimiento();
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
        }

        protected void btnRestablecerPantallaConsulta_Click(object sender, EventArgs e)
        {

        }

        protected void btnProcesarSuplirSacar_Click(object sender, EventArgs e)
        {

        }
    }
}