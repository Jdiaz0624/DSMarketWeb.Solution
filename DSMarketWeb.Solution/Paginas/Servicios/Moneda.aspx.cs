using System;
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
    public partial class Moneda : System.Web.UI.Page
    {
        DSMarketWeb.Logic.Logica.LogicaServicio.LogicaServicio ObjDataServicio = new Logic.Logica.LogicaServicio.LogicaServicio();
        DSMarketWeb.Logic.Logica.LogicaSeguridad.LogicaSeguridad ObjdataSeguridad = new Logic.Logica.LogicaSeguridad.LogicaSeguridad();

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



            //divPaginacionProductoAgregar.Visible = true;
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
        private void ModoConsulta() {
            DivBloqueConsulta.Visible = true;
            DivBloqueMantenimiento.Visible = false;

            btnConsultar.Enabled = true;
            btnNuevo.Enabled = true;
            btnReporte.Enabled = true;
            btnEditar.Enabled = false;
        }
        private void ModoMantenimiento() {
            DivBloqueConsulta.Visible = false;
            DivBloqueMantenimiento.Visible = true;

            btnConsultar.Enabled = false;
            btnNuevo.Enabled = false;
            btnReporte.Enabled = false;
            btnEditar.Enabled = true;
        }
        private void RestablecerPantalla() {
            txtDescripcionMonedaConsulta.Text = string.Empty;
            txtMonedaMantenimiento.Text = string.Empty;
            txtSiglaMantenimiento.Text = string.Empty;
            txtTasaMantenimiento.Text = string.Empty;
            ModoConsulta();
            MostrarListadoMonedas();
        }

        private void MostrarListadoMonedas() {

            string _Moneda = string.IsNullOrEmpty(txtDescripcionMonedaConsulta.Text.Trim()) ? null : txtDescripcionMonedaConsulta.Text.Trim();

            var Listado = ObjDataServicio.BuscaMonedas(
                new Nullable<decimal>(),
                _Moneda,
                null);
            if (Listado.Count() < 1)
            {
                lbCantidadRegistrosVariable.Text = "0";
            }
            else {
               

                int CantidadRegistros = Listado.Count;
                lbCantidadRegistrosVariable.Text = CantidadRegistros.ToString("N0");
                Paginar(ref rpListadoMoneda, Listado, 10, ref lbCantidadPaginaVAriableProductosFacturar, ref LinkPrimeraPaginaMoneda, ref LinkAnteriorMoneda, ref LinkSiguienteMoneda, ref LinkUltimoMoneda);
                HandlePaging(ref dtPaginacionMoneda, ref lbPaginaActualVariavleProductosFacturar);
            }
        }

        private void MantenimientoMoneda(decimal IdMoneda, string Accion) {

            DSMarketWeb.Logic.PrcesarMantenimientos.Servicios.ProcesarInformacionMonedas Procesar = new Logic.PrcesarMantenimientos.Servicios.ProcesarInformacionMonedas(
                IdMoneda,
                txtMonedaMantenimiento.Text,
                txtSiglaMantenimiento.Text,
                cbEstatusMantenimiento.Checked,
                Convert.ToDecimal(txtTasaMantenimiento.Text),
                (decimal)Session["IdUsuario"],
                cbPorDefectoMantenimiento.Checked,
                Accion);
            Procesar.ProcesarInformacion();
            RestablecerPantalla();
        }

        private void GenerarReporte(string UsuarioBD, string ClaveBD, string RutaReporte, string NombreReporte) {

            decimal? IdMoneda = null;
            string _NombreMoneda = string.IsNullOrEmpty(txtDescripcionMonedaConsulta.Text.Trim()) ? null : txtDescripcionMonedaConsulta.Text.Trim();
            string _Sigla = null;

            ReportDocument Reporte = new ReportDocument();
            Reporte.Load(RutaReporte);
            Reporte.Refresh();

            Reporte.SetParameterValue("@IdMoneda", IdMoneda);
            Reporte.SetParameterValue("@Descripcion", _NombreMoneda);
            Reporte.SetParameterValue("@Sigla", _Sigla);

           
            Reporte.SetDatabaseLogon(UsuarioBD, ClaveBD);
            if (rbReportePDF.Checked == true) {
                Reporte.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, NombreReporte);
            }
            else if (rbReporteExcel.Checked == true) {
                Reporte.ExportToHttpResponse(ExportFormatType.Excel, Response, true, NombreReporte);
            }
            else if (rbReporteWord.Checked == true) {
                Reporte.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, true, NombreReporte);
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                rbReportePDF.Checked = true;
                DSMarketWeb.Logic.Comunes.SacarNombreUsuario NombreUsuario = new Logic.Comunes.SacarNombreUsuario((decimal)Session["IdUsuario"]);
                Label lbUsuarioConectado = (Label)Master.FindControl("lbUsuarioConectado");
                lbUsuarioConectado.Text = NombreUsuario.SacarNombre();

                Label lbNombrePantalla = (Label)Master.FindControl("lbNivelAccesoPantalla");
                lbNombrePantalla.Text = "MONEDAS";

                ModoConsulta();
                MostrarListadoMonedas();
                
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            MostrarListadoMonedas();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            ModoMantenimiento();
            cbEstatusMantenimiento.Checked = true;
            cbPorDefectoMantenimiento.Checked = false;
            lbAccionTomar.Text = "INSERT";
            lbIsMantenimiento.Text = "0";
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            ModoMantenimiento();
            cbEstatusMantenimiento.Checked = true;
            cbPorDefectoMantenimiento.Checked = false;
            lbAccionTomar.Text = "UPDATE";
        }

        protected void btnReporte_Click(object sender, EventArgs e)
        {
            string UsuarioBD = "";
            string ClaveBD = "";
            var SacarCredenciales = ObjdataSeguridad.SacarCredencialesBD(1);
            foreach (var n in SacarCredenciales) {
                UsuarioBD = n.Usuario;
                ClaveBD = DSMarketWeb.Logic.Comunes.SeguridadEncriptacion.DesEncriptar(n.Clave);
            }
            GenerarReporte(UsuarioBD, ClaveBD, Server.MapPath("ReporteMonedas.rpt"), "Reporte de Monedas");
        }

        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {
            var ItemSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfIdMonedaSeleccionada = ((HiddenField)ItemSeleccionado.FindControl("hfIdMoneda")).Value.ToString();

            lbIsMantenimiento.Text = hfIdMonedaSeleccionada;

            CurrentPage = 0;
            var Seleccionar = ObjDataServicio.BuscaMonedas(
                Convert.ToDecimal(hfIdMonedaSeleccionada), null, null);
            lbCantidadRegistrosVariable.Text = "1";
            Paginar(ref rpListadoMoneda, Seleccionar, 1, ref lbCantidadPaginaVAriableProductosFacturar, ref LinkPrimeraPaginaMoneda, ref LinkAnteriorMoneda, ref LinkSiguienteMoneda, ref LinkUltimoMoneda);
            HandlePaging(ref dtPaginacionMoneda, ref lbPaginaActualVariavleProductosFacturar);
            foreach (var n in Seleccionar) {
                txtMonedaMantenimiento.Text = n.Moneda;
                txtSiglaMantenimiento.Text = n.Sigla;
                txtTasaMantenimiento.Text = n.Tasa.ToString();
                cbEstatusMantenimiento.Checked = (n.Estatus0.HasValue ? n.Estatus0.Value : false);
                cbPorDefectoMantenimiento.Checked = (n.PorDefecto0.HasValue ? n.PorDefecto0.Value : false);
            }
            ModoMantenimiento();
            lbAccionTomar.Text = "UPDATE";
        }

        protected void LinkPrimeraPaginaMoneda_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            MostrarListadoMonedas();
        }

        protected void LinkAnteriorMoneda_Click(object sender, EventArgs e)
        {
            CurrentPage += -1;
           MostrarListadoMonedas();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref lbPaginaActualVariavleProductosFacturar, ref lbCantidadPaginaVAriableProductosFacturar);
        }

        protected void dtPaginacionMoneda_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dtPaginacionMoneda_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            MostrarListadoMonedas();
        }

        protected void LinkSiguienteMoneda_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            MostrarListadoMonedas();

        }

        protected void LinkUltimoMoneda_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            MostrarListadoMonedas();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref lbPaginaActualVariavleProductosFacturar, ref lbCantidadPaginaVAriableProductosFacturar);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            MantenimientoMoneda(Convert.ToDecimal(lbIsMantenimiento.Text), lbAccionTomar.Text);

        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            RestablecerPantalla();
        }

        protected void btnRestablecer_Click(object sender, EventArgs e)
        {
            RestablecerPantalla();
        }
    }
}