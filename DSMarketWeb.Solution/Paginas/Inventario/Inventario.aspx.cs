using System;
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

        enum TipoDeReporte {
        ReporteGeneral=1,
        ReporteUnico=2
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
                null);
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
            btnSuplir.Enabled = false;
            btnReporte.Enabled = true;
            btnRestablecer.Enabled = true;
            lbIdRegistro.Text = "-1";
            CurrentPage = 0;
            ListadoProductosServicios();
            rbPDF.Checked = true;
            DivFechaDesde.Visible = false;
            DivFechaHasta.Visible = false;
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
                ListadoProductosServicios();
                rbPDF.Checked = true;
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
                btnSuplir.Enabled = false;
                lbIdRegistro.Text = "-1";
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

        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        protected void btnSuplir_Click(object sender, EventArgs e)
        {

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

        protected void btnSeleccionarRegistro_Click(object sender, EventArgs e)
        {

        }
    }
}