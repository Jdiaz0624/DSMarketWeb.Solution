﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace DSMarketWeb.Solution.Paginas.Inventario
{
    
    public partial class Inventario : System.Web.UI.Page
    {

        Lazy<DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario> ObjDataInventario = new Lazy<Logic.Logica.LogicaInventario.LogicaInventario>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion> ObjDataConfiguracion = new Lazy<Logic.Logica.LogicaConfiguracion.LogicaConfiguracion>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaSeguridad.LogicaSeguridad> ObjDataSeguridad = new Lazy<Logic.Logica.LogicaSeguridad.LogicaSeguridad>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaServicio.LogicaServicio> ObjdataServicio = new Lazy<Logic.Logica.LogicaServicio.LogicaServicio>();

        enum TipoDeReporte {
            ReporteGeneral = 1,
            ReporteUnico = 2
        }

        enum TipoProducto {
            Producto = 1,
            Servicio = 2
        }

        /// <summary>
        /// Esta enumaración muestra todas las opciones generales de configuración registrada en base de datos
        /// </summary>
        enum OpcionesConfiguracionGeneral
        {
            ImpuestoPorDefecto = 1,
            LlevaGarantiaPorDefecto = 2,
            CampoReferenciaObligatorio = 3,
            CampoReferenciaNumerico = 4,
            ValidarCampoReferencia = 5,
            UnidaddeMedidaSeleccionable = 6,
            ModelosSeleccionable = 7,
            ColoresSelecciobales = 8,
            CondicionesSelecciobales = 9,
            CapacidadesSelecciobales = 10,
            CargarListasDesplegablesalguardar = 11,
            AutoCompletarCampoReferenciaConsulta = 12

        }

        #region VALIDAR LAS CONFIGURACIONES GENERALES DEL SISTEMA
        private void ValidarConfiguracionesGenerales() {
            bool Respuesta = false;
            //IMPUESTO
            DSMarketWeb.Logic.Comunes.ValidarOpcionesConfiguracionesGeneralesSistema ValidarImpuesto = new Logic.Comunes.ValidarOpcionesConfiguracionesGeneralesSistema((decimal)OpcionesConfiguracionGeneral.ImpuestoPorDefecto, 3);
            Respuesta = ValidarImpuesto.ResultadoValidacion();
            switch (Respuesta) {
                case true:
                    cbAplicaParaImpuestoMantenimiento.Checked = true;
                    break;

                case false:
                    cbAplicaParaImpuestoMantenimiento.Checked = false;
                    break;
            }

            //GANRANTIA
            DSMarketWeb.Logic.Comunes.ValidarOpcionesConfiguracionesGeneralesSistema ValidarGarantia = new Logic.Comunes.ValidarOpcionesConfiguracionesGeneralesSistema((decimal)OpcionesConfiguracionGeneral.LlevaGarantiaPorDefecto, 3);
            Respuesta = ValidarGarantia.ResultadoValidacion();
            switch (Respuesta)
            {
                case true:
                    cbLlevaGarantiaMantenimiento.Checked = true;
                    break;

                case false:
                    cbLlevaGarantiaMantenimiento.Checked = false;
                    break;
            }

            //UNIDAD DE MEDIDA
            DSMarketWeb.Logic.Comunes.ValidarOpcionesConfiguracionesGeneralesSistema ValidarUnidadMedida = new Logic.Comunes.ValidarOpcionesConfiguracionesGeneralesSistema((decimal)OpcionesConfiguracionGeneral.UnidaddeMedidaSeleccionable, 3);
            Respuesta = ValidarUnidadMedida.ResultadoValidacion();
            switch (Respuesta)
            {
                case true:
                    txtUnidadMedidaMantenimiento.Visible = false;
                    ddlSeleccionarUnidadMedidaMantenimiento.Visible = true;
                    CargarUnidadMedida();
                    break;

                case false:
                    txtUnidadMedidaMantenimiento.Visible = true;
                    ddlSeleccionarUnidadMedidaMantenimiento.Visible = false;
                    break;
            }


            //MODLEOS
            DSMarketWeb.Logic.Comunes.ValidarOpcionesConfiguracionesGeneralesSistema ValidarUnidarModelos = new Logic.Comunes.ValidarOpcionesConfiguracionesGeneralesSistema((decimal)OpcionesConfiguracionGeneral.ModelosSeleccionable, 3);
            Respuesta = ValidarUnidarModelos.ResultadoValidacion();
            switch (Respuesta)
            {
                case true:
                    txtModeloMantenimiento.Visible = false;
                    ddlSeleccionarModeloMantenimiento.Visible = true;
                    CargarModelos();
                    break;

                case false:
                    txtModeloMantenimiento.Visible = true;
                    ddlSeleccionarModeloMantenimiento.Visible = false;
                    break;
            }

            //COLORES
            DSMarketWeb.Logic.Comunes.ValidarOpcionesConfiguracionesGeneralesSistema ValidarUnidarColores = new Logic.Comunes.ValidarOpcionesConfiguracionesGeneralesSistema((decimal)OpcionesConfiguracionGeneral.ColoresSelecciobales, 3);
            Respuesta = ValidarUnidarColores.ResultadoValidacion();
            switch (Respuesta)
            {
                case true:
                    txtColorMantenimiento.Visible = false;
                    ddlSeleccionarColoresMantenimiento.Visible = true;
                    CargarColores();
                    break;

                case false:
                    txtColorMantenimiento.Visible = true;
                    ddlSeleccionarColoresMantenimiento.Visible = false;
                    break;
            }

            //CONDICIONES
            DSMarketWeb.Logic.Comunes.ValidarOpcionesConfiguracionesGeneralesSistema ValidarCondiciones = new Logic.Comunes.ValidarOpcionesConfiguracionesGeneralesSistema((decimal)OpcionesConfiguracionGeneral.CondicionesSelecciobales, 3);
            Respuesta = ValidarCondiciones.ResultadoValidacion();
            switch (Respuesta)
            {
                case true:
                    txtCondicionMantenimiento.Visible = false;
                    ddlSeleccionarCondicionesMantenimiento.Visible = true;
                    CargarCondiciones();
                    break;

                case false:
                    txtCondicionMantenimiento.Visible = true;
                    ddlSeleccionarCondicionesMantenimiento.Visible = false;
                    break;
            }

            //CAPACIDAD
            DSMarketWeb.Logic.Comunes.ValidarOpcionesConfiguracionesGeneralesSistema ValidarCapacidad = new Logic.Comunes.ValidarOpcionesConfiguracionesGeneralesSistema((decimal)OpcionesConfiguracionGeneral.CapacidadesSelecciobales, 3);
            Respuesta = ValidarCapacidad.ResultadoValidacion();
            switch (Respuesta)
            {
                case true:
                    txtCapacidadMantenimiento.Visible = false;
                    ddlSeleccionarCapacidadMantenimiento.Visible = true;
                    CargarCapacidad();
                    break;

                case false:
                    txtCapacidadMantenimiento.Visible = true;
                    ddlSeleccionarCapacidadMantenimiento.Visible = false;
                    break;
            }

        }
        #endregion

        #region CARGAR LAS LISTAS DESPLEGABLES DE LOS MANTENIMIENTOS
        private void CargarUnidadMedida() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarUnidadMedidaMantenimiento, ObjDataConfiguracion.Value.BuscaListas("UNIDADMEDIDA", null, null));
        }
        private void CargarModelos() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarModeloMantenimiento, ObjDataConfiguracion.Value.BuscaListas("MODELOS", ddlSeleccionarMarcaMantenimiento.SelectedValue.ToString(), null));
        }
        private void CargarColores() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarColoresMantenimiento, ObjDataConfiguracion.Value.BuscaListas("COLORES", null, null));
        }

        private void CargarCondiciones() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCondicionesMantenimiento, ObjDataConfiguracion.Value.BuscaListas("CONDICIONES", null, null));
        }
        private void CargarCapacidad() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCapacidadMantenimiento, ObjDataConfiguracion.Value.BuscaListas("CAPACIDAD", null, null));
        }
        #endregion

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
        private void HandlePaging(ref DataList NombreDataList, ref Label LbPaginaActual)
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
            LbPaginaActual.Text = (NumeroPagina + 1).ToString();
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
        private void Paginar(ref Repeater RptGrid, IEnumerable<object> Listado, int _NumeroRegistros, ref Label lbCantidadPagina, ref LinkButton PrimeraPagina, ref LinkButton PaginaAnterior, ref LinkButton SiguientePagina, ref LinkButton UltimaPagina)
        {
            pagedDataSource.DataSource = Listado;
            pagedDataSource.AllowPaging = true;

            ViewState["TotalPages"] = pagedDataSource.PageCount;
            // lbNumeroVariable.Text = "1";
            lbCantidadPagina.Text = pagedDataSource.PageCount.ToString();

            //MOSTRAMOS LA CANTIDAD DE PAGINAS A MOSTRAR O NUMERO DE REGISTROS
            pagedDataSource.PageSize = (_NumeroRegistros == 0 ? _TamanioPagina : _NumeroRegistros);
            pagedDataSource.CurrentPageIndex = CurrentPage;

            //HABILITAMOS LOS BOTONES DE LA PAGINACION
            PrimeraPagina.Enabled = !pagedDataSource.IsFirstPage;
            PaginaAnterior.Enabled = !pagedDataSource.IsFirstPage;
            SiguientePagina.Enabled = !pagedDataSource.IsLastPage;
            UltimaPagina.Enabled = !pagedDataSource.IsLastPage;

            RptGrid.DataSource = pagedDataSource;
            RptGrid.DataBind();


            // divPaginacionDetalle.Visible = true;
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

        #region LISTAS DESPLEGABLES DE LA PANTALLA DE CONSULTA
        private void CargarTipoProductos() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoProductoConsulta, ObjDataConfiguracion.Value.BuscaListas("TIPOPRODUCTO", null, null), true);
        }
        private void CargarCategorias() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCategoriaConsulta, ObjDataConfiguracion.Value.BuscaListas("CATEGORIAS", ddlSeleccionarTipoProductoConsulta.SelectedValue.ToString(), null), true);

        }
        private void CargarMarcas() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarMarcaConsulta, ObjDataConfiguracion.Value.BuscaListas("MARCAS", ddlSeleccionarTipoProductoConsulta.SelectedValue.ToString(), ddlSeleccionarCategoriaConsulta.SelectedValue.ToString()), true);
        }
        #endregion

        #region LISTAS DESPLEGABLES DE LA PANTALA DE MANTENIMIENTO
        private void ListadoTipoProductoMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoPrductoMantenimieto, ObjDataConfiguracion.Value.BuscaListas("TIPOPRODUCTO", null, null));
        }
        private void ListadoCategoriasMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCategoriaMantenimiento, ObjDataConfiguracion.Value.BuscaListas("CATEGORIAS", ddlSeleccionarTipoPrductoMantenimieto.SelectedValue.ToString(), null));
        }
        private void ListadoMarcasMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarMarcaMantenimiento, ObjDataConfiguracion.Value.BuscaListas("MARCAS", ddlSeleccionarTipoPrductoMantenimieto.SelectedValue.ToString(), ddlSeleccionarCategoriaMantenimiento.SelectedValue.ToString()));
        }
        private void ListadoTipoSuplidoresMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoSuplidorMantenimiento, ObjDataConfiguracion.Value.BuscaListas("TIPOSUPLIDOR", null, null));
        }
        private void ListadoSuplidoresMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarSuplidorMantenimiento, ObjDataConfiguracion.Value.BuscaListas("SUPLIDOR", ddlSeleccionarTipoSuplidorMantenimiento.SelectedValue.ToString(), null));
        }
        private void ListadoTiempoGarantiaMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoGarantiaMantenimiento, ObjDataConfiguracion.Value.BuscaListas("TIEMPOGARANTIA", null, null));
        }
        #endregion

        #region MOSTRAR EL LISTADO DE LOS PRODUCTOS Y SERVICIOS
        private void ListadoProductosServicios() {

            decimal? _TipoProducto = ddlSeleccionarTipoProductoConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarTipoProductoConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _Categoria = ddlSeleccionarCategoriaConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarCategoriaConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _Marca = ddlSeleccionarMarcaConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarMarcaConsulta.SelectedValue) : new Nullable<decimal>();
            string _Descripcion = string.IsNullOrEmpty(txtDescripcionConsulta.Text.Trim()) ? null : txtDescripcionConsulta.Text.Trim();
            string _CodigoBarra = string.IsNullOrEmpty(txtCodigoBarraConsulta.Text.Trim()) ? null : txtCodigoBarraConsulta.Text.Trim();
            string _Referencia = string.IsNullOrEmpty(txtReferenciaConsulta.Text.Trim()) ? null : txtReferenciaConsulta.Text.Trim();
            string _CodigoProducto = string.IsNullOrEmpty(txtCodigoProductoConulta.Text.Trim()) ? null : txtCodigoProductoConulta.Text.Trim();
            DateTime? _FechaDesde = new Nullable<DateTime>();
            DateTime? _FechaHasta = new Nullable<DateTime>();
            decimal? StockProducto = cbMostrarProductosAgotados.Checked == true ? 0 : new Nullable<decimal>();

            if (cbAgregarRangoFecha.Checked == true) {
                _FechaDesde = string.IsNullOrEmpty(txtFechaDesdeConsulta.Text.Trim()) ? new Nullable<DateTime>() : Convert.ToDateTime(txtFechaDesdeConsulta.Text);
                _FechaHasta = string.IsNullOrEmpty(txtFechaHastaConsulta.Text.Trim()) ? new Nullable<DateTime>() : Convert.ToDateTime(txtFechaHastaConsulta.Text);
            }


            var BuscarListado = ObjDataInventario.Value.BuscaProductosServicios(
                new Nullable<decimal>(),
                null,
                _TipoProducto,
                _Categoria,
                _Marca,
                null, null,
                _Descripcion,
                _CodigoBarra,
                _Referencia,
                null,
                _CodigoProducto,
                _FechaDesde,
                _FechaHasta,
                null,
                StockProducto);
            if (BuscarListado.Count() < 1)
            {
                lbCantidadRegistrosVariable.Text = "0";
                lbGananciaAproximadaVariable.Text = "0";
                lbCapitalInvertidovariable.Text = "0";
                rpListadoProductosServicios.DataSource = null;
                rpListadoProductosServicios.DataBind();
                divPaginacionDetalle.Visible = false;
            }
            else {
                int CantidadRegistros = 0;
                decimal CapitalInvertido = 0, GananciaAproximada = 0;
                CantidadRegistros = BuscarListado.Count;
                foreach (var n in BuscarListado) {

                    CapitalInvertido = (decimal)n.CapitalInvertido;
                    GananciaAproximada = (decimal)n.GananciaAproximadaTotal;
                }

                lbCantidadRegistrosVariable.Text = CantidadRegistros.ToString("N0");
                lbGananciaAproximadaVariable.Text = GananciaAproximada.ToString("N2");
                lbCapitalInvertidovariable.Text = CapitalInvertido.ToString("N2");

                Paginar(ref rpListadoProductosServicios, BuscarListado, 10, ref lbCantidadPaginaVariable, ref LinkPrimeraPagina, ref LinkAnterior, ref LinkSiguiente, ref LinkUltimo);
                HandlePaging(ref dtPaginacion, ref LinkBlbPaginaActualVariable);

                if (CantidadRegistros <= 10)
                {
                    divPaginacionDetalle.Visible = false;
                }
                else {
                    divPaginacionDetalle.Visible = true;
                }
            }

        }
        #endregion

        #region GENERAR REPORTE DE PRODUCTO
        private void GenerarReporteInventario(int TipoReporte, string RutaReporte, string NombreReprte) {
            decimal? _IdRegistro = TipoReporte == (int)TipoDeReporte.ReporteGeneral ? new Nullable<decimal>() : Convert.ToDecimal(lbIdRegistro.Text);
            string _NumeroConector = TipoReporte == (int)TipoDeReporte.ReporteGeneral ? null : lbNumeroConector.Text;
            decimal? _TipoProducto = ddlSeleccionarTipoProductoConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarTipoProductoConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _Categoria = ddlSeleccionarCategoriaConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarCategoriaConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _Marca = ddlSeleccionarMarcaConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarMarcaConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _TipoSuplidor = new Nullable<decimal>();
            decimal? _Suplidor = new Nullable<decimal>();
            string _Descripcion = string.IsNullOrEmpty(txtDescripcionConsulta.Text.Trim()) ? null : txtDescripcionConsulta.Text.Trim();
            string _CdigoBarra = string.IsNullOrEmpty(txtCodigoBarraConsulta.Text.Trim()) ? null : txtCodigoBarraConsulta.Text.Trim();
            string _Referencia = string.IsNullOrEmpty(txtReferenciaConsulta.Text.Trim()) ? null : txtReferenciaConsulta.Text.Trim();
            string _NumeroSeguimiento = null;
            string _CodigoProducto = string.IsNullOrEmpty(txtCodigoProductoConulta.Text.Trim()) ? null : txtCodigoProductoConulta.Text.Trim();
            DateTime? _FechaDesde = cbAgregarRangoFecha.Checked == true ? Convert.ToDateTime(txtFechaDesdeConsulta.Text) : new Nullable<DateTime>();
            DateTime? _FechaHasta = cbAgregarRangoFecha.Checked == true ? Convert.ToDateTime(txtFechaHastaConsulta.Text) : new Nullable<DateTime>();
            decimal? IdUuario = Session["IdUsuario"] != null ? (decimal)Session["IdUsuario"] : 0;
            decimal? StockProducto = cbMostrarProductosAgotados.Checked == true ? 0 : new Nullable<decimal>();
            string UsuarioBD = "";
            string ClaveBD = "";

            var SacarCredencialesBD = ObjDataSeguridad.Value.SacarCredencialesBD(1);
            foreach (var nCredenciales in SacarCredencialesBD) {
                UsuarioBD = nCredenciales.Usuario;
                ClaveBD = DSMarketWeb.Logic.Comunes.SeguridadEncriptacion.DesEncriptar(nCredenciales.Clave);
            }

            ReportDocument Reporte = new ReportDocument();
            Reporte.Load(RutaReporte);
            Reporte.Refresh();

            switch (TipoReporte) {
                case (int)TipoDeReporte.ReporteGeneral:
                    Reporte.SetParameterValue("@IdRegistro", _IdRegistro);
                    Reporte.SetParameterValue("@NumeroConector", _NumeroConector);
                    Reporte.SetParameterValue("@IdTipoProducto", _TipoProducto);
                    Reporte.SetParameterValue("@IdCategoria", _Categoria);
                    Reporte.SetParameterValue("@IdMarca", _Marca);
                    Reporte.SetParameterValue("@IdTipoSuplidor", _TipoSuplidor);
                    Reporte.SetParameterValue("@IdSuplidor", _Suplidor);
                    Reporte.SetParameterValue("@Descripcion", _Descripcion);
                    Reporte.SetParameterValue("@CodigoBarra", _CdigoBarra);
                    Reporte.SetParameterValue("@Referencia", _Referencia);
                    Reporte.SetParameterValue("@NumeroSeguimiento", _NumeroSeguimiento);
                    Reporte.SetParameterValue("@CodigoProducto", _CodigoProducto);
                    Reporte.SetParameterValue("@FechaIngresoDesde", _FechaDesde);
                    Reporte.SetParameterValue("@FechaIngresoHasta", _FechaHasta);
                    Reporte.SetParameterValue("@IdUsuarioGenera", IdUuario);
                    Reporte.SetParameterValue("@Stock", StockProducto);
                    break;

                case (int)TipoDeReporte.ReporteUnico:
                    Reporte.SetParameterValue("@IdRegistro", _IdRegistro);
                    Reporte.SetParameterValue("@NumeroConector", _NumeroConector);
                    Reporte.SetParameterValue("@IdTipoProducto", null);
                    Reporte.SetParameterValue("@IdCategoria", null);
                    Reporte.SetParameterValue("@IdMarca", null);
                    Reporte.SetParameterValue("@IdTipoSuplidor", null);
                    Reporte.SetParameterValue("@IdSuplidor", null);
                    Reporte.SetParameterValue("@Descripcion", null);
                    Reporte.SetParameterValue("@CodigoBarra", null);
                    Reporte.SetParameterValue("@Referencia", null);
                    Reporte.SetParameterValue("@NumeroSeguimiento", null);
                    Reporte.SetParameterValue("@CodigoProducto", null);
                    Reporte.SetParameterValue("@FechaIngresoDesde", null);
                    Reporte.SetParameterValue("@FechaIngresoHasta", null);
                    Reporte.SetParameterValue("@IdUsuarioGenera", IdUuario);
                    Reporte.SetParameterValue("@Stock", null);
                    break;
            }
            Reporte.SetDatabaseLogon(UsuarioBD, ClaveBD);
            if (rbPDF.Checked == true)
            {
                Reporte.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, NombreReprte);
            }
            else if (rbExcel.Checked == true)
            {
                Reporte.ExportToHttpResponse(ExportFormatType.Excel, Response, true, NombreReprte);
            }
            else if (rbWord.Checked == true) {
                Reporte.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, true, NombreReprte);
            }

        }
        #endregion

        #region RESTABLECER PANTALLA
        private void RestablecerPantalla() {
            CargarTipoProductos();
            CargarCategorias();
            CargarMarcas();
            txtDescripcionConsulta.Text = string.Empty;
            txtCodigoBarraConsulta.Text = string.Empty;
            txtReferenciaConsulta.Text = string.Empty;
            txtCodigoProductoConulta.Text = string.Empty;
            cbAgregarRangoFecha.Checked = false;
            //  cbReporteInventarioCompleto.Checked = false;
            btnConsultarRegistros.Enabled = true;
            btnNuevo.Enabled = true;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnSuplirItem.Visible = false;
            btnReporte.Enabled = true;
            btnRestablecer.Enabled = true;
            lbIdRegistro.Text = "-1";
            CurrentPage = 0;
            ListadoProductosServicios();
            rbPDF.Checked = true;
            DivFechaDesde.Visible = false;
            DivFechaHasta.Visible = false;
            btnDetalleItemSeleccionado.Visible = false;
            lbIdRegistro.Text = "-1";
            lbNumeroConector.Text = "-1";
            DivBloqueMantenimiento.Visible = false;
            IdBloqueConsulta.Visible = true;
        }
        #endregion

        #region PROCESAR INFORMACION DE LOS PRODUCTOS Y SERVICIOS
        private void ProcesarInformacionProductosservicios(decimal IdRegistro, string NumeroConector, string Accion) {
            int IdTipoGarantia = cbLlevaGarantiaMantenimiento.Checked == true ? Convert.ToInt32(ddlSeleccionarTipoGarantiaMantenimiento.SelectedValue) : 1;
            int TipoTiempoGarantia = cbLlevaGarantiaMantenimiento.Checked == true ? Convert.ToInt32(txtTiempoGarantiaMantenimiento.Text) : 0;

            DSMarketWeb.Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionProductos Procesar = new Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionProductos(
                IdRegistro,
                NumeroConector,
                Convert.ToDecimal(ddlSeleccionarTipoPrductoMantenimieto.SelectedValue),
                Convert.ToDecimal(ddlSeleccionarCategoriaMantenimiento.SelectedValue),
                Convert.ToDecimal(ddlSeleccionarMarcaMantenimiento.SelectedValue),
                Convert.ToDecimal(ddlSeleccionarTipoSuplidorMantenimiento.SelectedValue),
                Convert.ToDecimal(ddlSeleccionarSuplidorMantenimiento.SelectedValue),
                txtDescripcionMantenimiento.Text,
                txtCodigoBarraMantenimiento.Text,
                txtReferenciaMantenimiento.Text,
                txtNumeroSeguimientoMantenimiento.Text,
                txtCodigoProductoMantenimiento.Text,
                Convert.ToDecimal(txtPrecioCompraMantenimiento.Text),
                Convert.ToDecimal(txtPrecioVentaMantenimiento.Text),
                Convert.ToDecimal(txtStockMantenimiento.Text),
                Convert.ToDecimal(txtStockMinimoMantenimiento.Text),
                txtUnidadMedidaMantenimiento.Text,
                txtModeloMantenimiento.Text,
                txtColorMantenimiento.Text,
                txtCondicionMantenimiento.Text,
                txtCapacidadMantenimiento.Text,
                cbAplicaParaImpuestoMantenimiento.Checked,
                false,
                cbLlevaGarantiaMantenimiento.Checked,
                IdTipoGarantia,
                TipoTiempoGarantia,
                txtComentarioMantenimiento.Text,
                (decimal)Session["IdUsuario"],
                Accion);
            Procesar.ProcesarInformacion();
        }
        #endregion

        #region SACAR EL TIPO DE TIEMPO DE GARANTIA
        private int SacarTipoTiempoGarantia(int IdGarantia) {
            int TipoTiempo = 0;

            var SacarInformacion = ObjdataServicio.Value.SacartiempoGarantia(IdGarantia);
            foreach (var n in SacarInformacion) {
                TipoTiempo = (int)n.TiempoGarantia;
            }
            return TipoTiempo;
        }
        #endregion

        #region GENERAR EL NUMERO DE CONECTOR PARA GUARDAR REGISTRO
        private string GenerarNumeroConector() {
            Random NumeroRandon = new Random();

            string NumeroConector = "";
            string PrimerNumero = NumeroRandon.Next(0, 999999999).ToString();
            string SegundoNumero = NumeroRandon.Next(0, 999999999).ToString();
            string Ano = DateTime.Now.Year.ToString();
            string Month = DateTime.Now.Month.ToString().Length == 1 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString().Length == 1 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString();
            NumeroConector = PrimerNumero + Ano + Month + day + SegundoNumero;
            return NumeroConector;

        }
        #endregion

        #region UTILIDADES DE LOS CONTROLES DE LA PARTE DE MANTENIMIENTO
        private void LimpiarControlesMantenimiento() {
            cbAplicaParaImpuestoMantenimiento.Checked = false;
            cbLlevaGarantiaMantenimiento.Checked = false;
            ListadoTipoProductoMantenimiento();
            ListadoCategoriasMantenimiento();
            ListadoMarcasMantenimiento();
            ListadoTipoSuplidoresMantenimiento();
            ListadoSuplidoresMantenimiento();
            txtDescripcionMantenimiento.Text = string.Empty;
            txtCodigoBarraMantenimiento.Text = string.Empty;
            txtReferenciaMantenimiento.Text = string.Empty;
            txtNumeroSeguimientoMantenimiento.Text = string.Empty;
            txtCodigoProductoMantenimiento.Text = string.Empty;
            txtPrecioCompraMantenimiento.Text = string.Empty;
            txtPrecioVentaMantenimiento.Text = string.Empty;
            txtStockMantenimiento.Text = string.Empty;
            txtStockMinimoMantenimiento.Text = string.Empty;
            txtUnidadMedidaMantenimiento.Text = string.Empty;
            txtModeloMantenimiento.Text = string.Empty;
            txtColorMantenimiento.Text = string.Empty;
            txtCondicionMantenimiento.Text = string.Empty;
            txtCapacidadMantenimiento.Text = string.Empty;
            ListadoTiempoGarantiaMantenimiento();
            txtTiempoGarantiaMantenimiento.Text = string.Empty;
            txtComentarioMantenimiento.Text = string.Empty;
        }
        private void BloquearControlesMantenimiento() {
            cbAplicaParaImpuestoMantenimiento.Enabled = false;
            cbLlevaGarantiaMantenimiento.Enabled = false;
            ddlSeleccionarTipoPrductoMantenimieto.Enabled = false;
            ddlSeleccionarCategoriaMantenimiento.Enabled = false;
            ddlSeleccionarMarcaMantenimiento.Enabled = false;
            ddlSeleccionarTipoSuplidorMantenimiento.Enabled = false;
            ddlSeleccionarSuplidorMantenimiento.Enabled = false;
            txtDescripcionMantenimiento.Enabled = false;
            txtCodigoBarraMantenimiento.Enabled = false;
            txtReferenciaMantenimiento.Enabled = false;
            txtNumeroSeguimientoMantenimiento.Enabled = false;
            txtCodigoProductoMantenimiento.Enabled = false;
            txtPrecioCompraMantenimiento.Enabled = false;
            txtPrecioVentaMantenimiento.Enabled = false;
            txtStockMantenimiento.Enabled = false;
            txtStockMinimoMantenimiento.Enabled = false;
            txtUnidadMedidaMantenimiento.Enabled = false;
            txtModeloMantenimiento.Enabled = false;
            txtColorMantenimiento.Enabled = false;
            txtCondicionMantenimiento.Enabled = false;
            txtCapacidadMantenimiento.Enabled = false;
            ddlSeleccionarTipoGarantiaMantenimiento.Enabled = false;
            txtTiempoGarantiaMantenimiento.Enabled = false;
            txtComentarioMantenimiento.Enabled = false;
        }
        private void DesbloquearControlesMantenimiento() {
            cbAplicaParaImpuestoMantenimiento.Enabled = true;
            cbLlevaGarantiaMantenimiento.Enabled = true;
            ddlSeleccionarTipoPrductoMantenimieto.Enabled = true;
            ddlSeleccionarCategoriaMantenimiento.Enabled = true;
            ddlSeleccionarMarcaMantenimiento.Enabled = true;
            ddlSeleccionarTipoSuplidorMantenimiento.Enabled = true;
            ddlSeleccionarSuplidorMantenimiento.Enabled = true;
            txtDescripcionMantenimiento.Enabled = true;
            txtCodigoBarraMantenimiento.Enabled = true;
            txtReferenciaMantenimiento.Enabled = true;
            txtNumeroSeguimientoMantenimiento.Enabled = true;
            txtCodigoProductoMantenimiento.Enabled = true;
            txtPrecioCompraMantenimiento.Enabled = true;
            txtPrecioVentaMantenimiento.Enabled = true;
            txtStockMantenimiento.Enabled = true;
            txtStockMinimoMantenimiento.Enabled = true;
            txtUnidadMedidaMantenimiento.Enabled = true;
            txtModeloMantenimiento.Enabled = true;
            txtColorMantenimiento.Enabled = true;
            txtCondicionMantenimiento.Enabled = true;
            txtCapacidadMantenimiento.Enabled = true;
            ddlSeleccionarTipoGarantiaMantenimiento.Enabled = true;
            txtTiempoGarantiaMantenimiento.Enabled = true;
            txtComentarioMantenimiento.Enabled = true;
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                
                DSMarketWeb.Logic.Comunes.SacarNombreUsuario NombreUsuario = new Logic.Comunes.SacarNombreUsuario((decimal)Session["IdUsuario"]);
                Label lbUsuarioConectado = (Label)Master.FindControl("lbUsuarioConectado");
                lbUsuarioConectado.Text = NombreUsuario.SacarNombre();

                Label lbNombrePantalla = (Label)Master.FindControl("lbNivelAccesoPantalla");
                lbNombrePantalla.Text = "INVENTARIO";

                DivFechaDesde.Visible = false;
                DivFechaHasta.Visible = false;

                CargarTipoProductos();
                CargarCategorias();
                CargarMarcas();

                ListadoTipoProductoMantenimiento();
                ListadoCategoriasMantenimiento();
                ListadoMarcasMantenimiento();
                ListadoTipoSuplidoresMantenimiento();
                ListadoSuplidoresMantenimiento();
                ListadoTiempoGarantiaMantenimiento();
                txtTiempoGarantiaMantenimiento.Text = SacarTipoTiempoGarantia(Convert.ToInt32(ddlSeleccionarTipoGarantiaMantenimiento.SelectedValue)).ToString();
                ListadoProductosServicios();
                rbPDF.Checked = true;
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
                btnSuplirItem.Visible = false;
                btnDetalleItemSeleccionado.Visible = false;
                lbIdRegistro.Text = "-1";
                lbNumeroConector.Text = "-1";
            }
        }

        protected void ddlSeleccionarTipoProductoConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCategorias();
            CargarMarcas();
        }

        protected void ddlSeleccionarCategoriaConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarMarcas();
        }

        protected void txtDescripcionConsulta_TextChanged(object sender, EventArgs e)
        {
            if (lbIdRegistro.Text == "-1") {
                CurrentPage = 0;
                ListadoProductosServicios();
            }

        }

        protected void txtCodigoBarraConsulta_TextChanged(object sender, EventArgs e)
        {
            if (lbIdRegistro.Text == "-1") {
                CurrentPage = 0;
                ListadoProductosServicios();
            }

        }

        protected void txtReferenciaConsulta_TextChanged(object sender, EventArgs e)
        {
            if (lbIdRegistro.Text == "-1") {
                CurrentPage = 0;
                ListadoProductosServicios();
            }

        }

        protected void txtCodigoProductoConulta_TextChanged(object sender, EventArgs e)
        {
            if (lbIdRegistro.Text == "-1") {
                CurrentPage = 0;
                ListadoProductosServicios();
            }

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

        protected void btnConsultarRegistros_Click(object sender, EventArgs e)
        {
            if (lbIdRegistro.Text == "-1") {
                CurrentPage = 0;
                ListadoProductosServicios();
            }

        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            DesbloquearControlesMantenimiento();
            IdBloqueConsulta.Visible = false;
            DivBloqueMantenimiento.Visible = true;
            txtDescripcionMantenimiento.Enabled = false;
            btnGuardarRegistroMantenimientio.Visible = true;
            btnEditarRegistroMantenimiento.Visible = false;
            btnEliminarRegistroMantenimiento.Visible = false;
            lbIdRegistro.Text = "0";
            GenerarNumeroConector();
            lbSeleccionarTipoGarantiaMantenimiento.Visible = false;
            ddlSeleccionarTipoGarantiaMantenimiento.Visible = false;
            lbTiempoarantiaMantenimiento.Visible = false;
            txtTiempoGarantiaMantenimiento.Visible = false;
            lbNumeroConector.Text = GenerarNumeroConector();
            ValidarConfiguracionesGenerales();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            DesbloquearControlesMantenimiento();

            IdBloqueConsulta.Visible = false;
            DivBloqueMantenimiento.Visible = true;
            txtDescripcionMantenimiento.Enabled = false;
            btnGuardarRegistroMantenimientio.Visible = false;
            btnEditarRegistroMantenimiento.Visible = true;
            btnEliminarRegistroMantenimiento.Visible = false;
            
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            BloquearControlesMantenimiento();
            IdBloqueConsulta.Visible = false;
            DivBloqueMantenimiento.Visible = true;
            txtDescripcionMantenimiento.Enabled = false;
            btnGuardarRegistroMantenimientio.Visible = false;
            btnEditarRegistroMantenimiento.Visible = false;
            btnEliminarRegistroMantenimiento.Visible = true;
        }

        protected void btnReporte_Click(object sender, EventArgs e)
        {
            int TipoReporte = lbIdRegistro.Text == "-1" ? TipoReporte = (int)TipoDeReporte.ReporteGeneral : TipoReporte = (int)TipoDeReporte.ReporteUnico;
            string NombreReporte = lbIdRegistro.Text == "-1" ? "Reporte de Inventario General" : "Reporte de Inventario";
            string RutaReporte = lbIdRegistro.Text == "-1" ? "ReporteInventario.rpt" : "ReporteProductoUnico.rpt";
            GenerarReporteInventario(TipoReporte, Server.MapPath(RutaReporte), NombreReporte);
        }


        protected void btnRestablecer_Click(object sender, EventArgs e)
        {
            RestablecerPantalla();
        }

        protected void LinkPrimeraPagina_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            ListadoProductosServicios();
        }

        protected void LinkAnterior_Click(object sender, EventArgs e)
        {
            CurrentPage += -1;
            ListadoProductosServicios();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref LinkBlbPaginaActualVariable, ref lbCantidadPaginaVariable);

        }

        protected void dtPaginacion_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dtPaginacion_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            ListadoProductosServicios();
        }

        protected void LinkSiguiente_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            ListadoProductosServicios();
        }

        protected void LinkUltimo_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            ListadoProductosServicios();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref LinkBlbPaginaActualVariable, ref lbCantidadPaginaVariable);
        }

        protected void ddlSeleccionarTipoPrductoMantenimieto_SelectedIndexChanged(object sender, EventArgs e)
        {

            ListadoCategoriasMantenimiento();
            ListadoMarcasMantenimiento();

        }

        protected void ddlSeleccionarCategoriaMantenimiento_SelectedIndexChanged(object sender, EventArgs e)
        {

            ListadoMarcasMantenimiento();

        }

        protected void ddlSeleccionarTipoSuplidorMantenimiento_SelectedIndexChanged(object sender, EventArgs e)
        {

            ListadoSuplidoresMantenimiento();
        }

        protected void cbLlevaGarantiaMantenimiento_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLlevaGarantiaMantenimiento.Checked == true) {
                lbSeleccionarTipoGarantiaMantenimiento.Visible = true;
                ddlSeleccionarTipoGarantiaMantenimiento.Visible = true;
                lbTiempoarantiaMantenimiento.Visible = true;
                txtTiempoGarantiaMantenimiento.Visible = true;
            }
            else if (cbLlevaGarantiaMantenimiento.Checked == false) {

                lbSeleccionarTipoGarantiaMantenimiento.Visible = false;
                ddlSeleccionarTipoGarantiaMantenimiento.Visible = false;
                lbTiempoarantiaMantenimiento.Visible = false;
                txtTiempoGarantiaMantenimiento.Visible = false;
            }
        }

        protected void ddlSeleccionarTipoGarantiaMantenimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTiempoGarantiaMantenimiento.Text = SacarTipoTiempoGarantia(Convert.ToInt32(ddlSeleccionarTipoGarantiaMantenimiento.SelectedValue)).ToString();
        }

        protected void btnGuardarRegistroMantenimientio_Click(object sender, EventArgs e)
        {
            ProcesarInformacionProductosservicios(Convert.ToDecimal(lbIdRegistro.Text), lbNumeroConector.Text, "INSERT");
            LimpiarControlesMantenimiento();
            RestablecerPantalla();
        }

        protected void btnEditarRegistroMantenimiento_Click(object sender, EventArgs e)
        {
            ProcesarInformacionProductosservicios(Convert.ToDecimal(lbIdRegistro.Text), lbNumeroConector.Text, "UPDATE");
            LimpiarControlesMantenimiento();
            RestablecerPantalla();
        }

        protected void btnEliminarRegistroMantenimiento_Click(object sender, EventArgs e)
        {
            ProcesarInformacionProductosservicios(Convert.ToDecimal(lbIdRegistro.Text), lbNumeroConector.Text, "DELETE");
            LimpiarControlesMantenimiento();
            RestablecerPantalla();
        }

        protected void btnVolverAtras_Click(object sender, EventArgs e)
        {
            LimpiarControlesMantenimiento();
            RestablecerPantalla();

        }

        protected void btnProcesarSuplir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombreProductoSeleccionadoSuplir.Text.Trim()) || string.IsNullOrEmpty(txtstockMinimoSuplir.Text.Trim()) || string.IsNullOrEmpty(txtStockSuplir.Text.Trim()) || string.IsNullOrEmpty(txtCantidadProcesarSuplir.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Has dejado campos vacios que son necesarios para procesar esta información, favor de verificar.');", true);

            }
            else {
                if (rbSuplirItemsSuplir.Checked == true)
                {
                    DSMarketWeb.Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionProductos Suplir = new Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionProductos(
                        Convert.ToDecimal(lbIdRegistro.Text),
                        lbNumeroConector.Text,
                        0, 0, 0, 0, 0, "", "", "", "", "", 0, 0,
                        Convert.ToDecimal(txtCantidadProcesarSuplir.Text),
                        0, "", "", "", "", "", false, false, false, 0, 0, "", 0, "ADDPRODUCT");
                    Suplir.ProcesarInformacion();
                    var ActualizarInformacion = ObjDataInventario.Value.BuscaProductosServicios(
                        Convert.ToDecimal(lbIdRegistro.Text),
                        lbNumeroConector.Text,
                        null, null, null, null, null, null, null, null, null, null, null, null, null, null);
                    foreach (var n in ActualizarInformacion)
                    {
                        txtStockSuplir.Text = n.Stock.ToString();
                        txtstockMinimoSuplir.Text = n.StockMinimo.ToString();
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Registro Procesado con Exito.');", true);


                }
                else if (rbSacarItemsSuplir.Checked == true)
                {

                    decimal StockActual = Convert.ToDecimal(txtStockSuplir.Text);
                    decimal StockSacar = Convert.ToDecimal(txtCantidadProcesarSuplir.Text);
                    if (StockSacar > StockActual)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('La cantidad que intentas sacar supera la cantidad disponible en stock, favor de verificar.');", true);
                    }
                    else
                    {
                        DSMarketWeb.Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionProductos Sacar = new Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionProductos(
                           Convert.ToDecimal(lbIdRegistro.Text),
                           lbNumeroConector.Text,
                           0, 0, 0, 0, 0, "", "", "", "", "", 0, 0,
                           Convert.ToDecimal(txtCantidadProcesarSuplir.Text),
                           0, "", "", "", "", "", false, false, false, 0, 0, "", 0, "LESSPRODUCT");
                        Sacar.ProcesarInformacion();
                        var ActualizarInformacion = ObjDataInventario.Value.BuscaProductosServicios(
                            Convert.ToDecimal(lbIdRegistro.Text),
                            lbNumeroConector.Text,
                            null, null, null, null, null, null, null, null, null, null, null, null, null, null);
                        foreach (var n in ActualizarInformacion)
                        {
                            txtStockSuplir.Text = n.Stock.ToString();
                            txtstockMinimoSuplir.Text = n.StockMinimo.ToString();
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Registro Procesado con Exito.');", true);
                    }
                }
                else {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Favor de seleccionar una opción para procesar.');", true);
                }
                
            }
        }

        protected void btnSeleccionarRegistro_Click(object sender, EventArgs e)
        {
            var IdRegistroSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfIdRegistroSeleccionado = ((HiddenField)IdRegistroSeleccionado.FindControl("hfIdRegistro")).Value.ToString();

            var NumeroConectorSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfNumeroConectorSeleccionado = ((HiddenField)NumeroConectorSeleccionado.FindControl("hfNumeroConector")).Value.ToString();

            lbIdRegistro.Text = hfIdRegistroSeleccionado.ToString();
            lbNumeroConector.Text = hfNumeroConectorSeleccionado.ToString();

            var RegistroSeleccionado = ObjDataInventario.Value.BuscaProductosServicios(
                Convert.ToDecimal(hfIdRegistroSeleccionado),
                hfNumeroConectorSeleccionado,
                null, null, null, null, null, null, null, null, null, null, null, null, null, null);
            Paginar(ref rpListadoProductosServicios, RegistroSeleccionado, 1, ref lbCantidadPaginaVariable, ref LinkPrimeraPagina, ref LinkAnterior, ref LinkSiguiente, ref LinkUltimo);
            HandlePaging(ref dtPaginacion, ref LinkBlbPaginaActualVariable);

            int CantidadRegistro = RegistroSeleccionado.Count;
            decimal CapitalInvertido = 0, GananciaAproximada = 0, PrecioProducto = 0, Stock = 0, StockMinimo = 0;

            decimal IdTipoProductoSeleccionado = 0;
          
            foreach (var n in RegistroSeleccionado) {
                CapitalInvertido = (decimal)n.CapitalInvertido;
                GananciaAproximada = (decimal)n.GananciaAproximadaTotal;
                IdTipoProductoSeleccionado = (decimal)n.IdTipoProducto;

                //SACAMOS LOS DATOS DEL PRODUCTO SELECCIONADO PARA MOSTRARLO EN EL DETALLE
                txtTipoProductoItemSeleccionado.Text = n.TipoProducto;
                txtCategoriaItemsseleccionado.Text = n.Categoria;
                txtMarcaItemSeleccionado.Text = n.Marca;
                txtTipoSuplidorItemSeleccionado.Text = n.TipoSuplidor;
                txtSuplidorItemsSeleccionado.Text = n.Suplidor;
                txtReferenciaItemsSeleccionado.Text = n.Referencia;
                txtDescripcionItemsSeleccionado.Text = n.Descripcion;
                txtCodigoBarraItemSeleccionado.Text = n.CodigoBarra;
                txtCodigoProductoItemSeleccionado.Text = n.CodigoProducto;
                PrecioProducto = (decimal)n.PrecioVenta;
                txtPrecioItemSeleccionado.Text = PrecioProducto.ToString("N2");
                Stock = (decimal)n.Stock;
                txtStockItemSeleccionado.Text = Stock.ToString("N0");
                StockMinimo = (decimal)n.StockMinimo;
                txtStockMinimoItemSeleccionado.Text = StockMinimo.ToString("N0");
                txtEstatusItemSeleccionado.Text = n.Estatus;
                txtComentarioItemSeleccionado.Text = n.Comentario;



                //SACAMOS LOS DATOS PARA LA PARTE DE MANTENIMIENTO
                cbAplicaParaImpuestoMantenimiento.Checked = (n.AplicaParaImpuesto0.HasValue ? n.AplicaParaImpuesto0.Value : false);
                cbLlevaGarantiaMantenimiento.Checked = (n.LlevaGarantia0.HasValue ? n.LlevaGarantia0.Value : false);
                ListadoTipoProductoMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarTipoPrductoMantenimieto, n.IdTipoProducto.ToString());
                ListadoCategoriasMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarCategoriaMantenimiento, n.IdCategoria.ToString());
                ListadoMarcasMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarMarcaMantenimiento, n.IdMarca.ToString());
                ListadoTipoSuplidoresMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarTipoSuplidorMantenimiento, n.IdTipoSuplidor.ToString());
                ListadoSuplidoresMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarSuplidorMantenimiento, n.IdSuplidor.ToString());
                txtDescripcionMantenimiento.Text = n.Descripcion;
                txtCodigoBarraMantenimiento.Text = n.CodigoBarra;
                txtReferenciaMantenimiento.Text = n.Referencia;
                txtNumeroSeguimientoMantenimiento.Text = n.NumeroSeguimiento;
                txtCodigoProductoMantenimiento.Text = n.CodigoProducto;
                txtPrecioCompraMantenimiento.Text = n.PrecioCompra.ToString();
                txtPrecioVentaMantenimiento.Text = n.PrecioVenta.ToString();
                txtStockMantenimiento.Text = n.Stock.ToString();
                txtStockMinimoMantenimiento.Text = n.StockMinimo.ToString();
                txtUnidadMedidaMantenimiento.Text = n.UnidadMedida;
                txtModeloMantenimiento.Text = n.Modelo;
                txtColorMantenimiento.Text = n.Color;
                txtCondicionMantenimiento.Text = n.Condicion;
                txtCapacidadMantenimiento.Text = n.Capacidad;
                ListadoTiempoGarantiaMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarTipoGarantiaMantenimiento, n.IdTipoGarantia.ToString());
                txtTiempoGarantiaMantenimiento.Text = n.TiempoGarantia.ToString();
                txtComentarioMantenimiento.Text = n.Comentario;


                //SACAMOS LOS DATOS PARA LA INFORMACION PARA SUPLIR O SACAR
                txtNombreProductoSeleccionadoSuplir.Text = n.Descripcion;
                txtStockSuplir.Text = n.Stock.ToString();
                txtstockMinimoSuplir.Text = n.StockMinimo.ToString();
                txtCantidadProcesarSuplir.Text = "1";
  
            }

            lbCantidadRegistrosVariable.Text = CantidadRegistro.ToString("N0");
            lbCapitalInvertidovariable.Text = CapitalInvertido.ToString("N2");
            lbGananciaAproximadaVariable.Text = GananciaAproximada.ToString("N2");
            btnDetalleItemSeleccionado.Visible = true;

            btnConsultarRegistros.Enabled = false;
            btnNuevo.Enabled = false;
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
            if (IdTipoProductoSeleccionado == (decimal)TipoProducto.Producto)
            {
                btnSuplirItem.Visible = true;
            }
            else if (IdTipoProductoSeleccionado == (decimal)TipoProducto.Servicio) {
                btnSuplirItem.Visible = false;
            }
            
           
            btnReporte.Enabled = true;
            btnRestablecer.Enabled = true;

            if (cbLlevaGarantiaMantenimiento.Checked == true)
            {
                lbSeleccionarTipoGarantiaMantenimiento.Visible = true;
                ddlSeleccionarTipoGarantiaMantenimiento.Visible = true;
                lbTiempoarantiaMantenimiento.Visible = true;
                txtTiempoGarantiaMantenimiento.Visible = true;
            }
            else if (cbLlevaGarantiaMantenimiento.Checked == false)
            {
                lbSeleccionarTipoGarantiaMantenimiento.Visible = false;
                ddlSeleccionarTipoGarantiaMantenimiento.Visible = false;
                lbTiempoarantiaMantenimiento.Visible = false;
                txtTiempoGarantiaMantenimiento.Visible = false;

            }

        }
    }
}