using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;

namespace DSMarketWeb.Solution.Paginas.Inventario
{
    public partial class MantenimientoSuplidores : System.Web.UI.Page
    {
        Lazy<DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion> ObjDataConfiguracion = new Lazy<Logic.Logica.LogicaConfiguracion.LogicaConfiguracion>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario> ObjdataInventario = new Lazy<Logic.Logica.LogicaInventario.LogicaInventario>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaSeguridad.LogicaSeguridad> ObjdataSeguridad = new Lazy<Logic.Logica.LogicaSeguridad.LogicaSeguridad>();

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

        #region CARGAR LAS LISTAS DESPLEGABLES
        public void CargarTipoSuplidorConsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoSuplidorConsulta, ObjDataConfiguracion.Value.BuscaListas("TIPOSUPLIDOR", null, null), true);
        }
        public void CargarTipoSuplidorMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoSuplidorMantenimiento, ObjDataConfiguracion.Value.BuscaListas("TIPOSUPLIDOR", null, null));
        }
        #endregion
        #region UTILIDADES DE LA PANTALLA Y MODOS
        private void ModoConsulta() {
            btnConsultarRegistros.Enabled = true;
            btnNuevoRegistro.Enabled = true;
            btnModificarRegistros.Enabled = false;
            btnExportarRegistros.Enabled = true;
            btnRestablecerPantalla.Enabled = true;
        }
        private void ModoMantenimiento() {
            btnConsultarRegistros.Enabled = false;
            btnNuevoRegistro.Enabled = false;
            btnModificarRegistros.Enabled = true;
            btnExportarRegistros.Enabled = false;
            btnRestablecerPantalla.Enabled = false;
        }
        /// <summary>
        /// Este metodo es para mostrar el bloque de la consulta y ocultar el bloque del mantenimiento
        /// </summary>
        private void Consulta_Mantenimiento() {
            DivBloqueConsulta.Visible = true;
            DivBloqueMantenimiento.Visible = false;
        }
        /// <summary>
        /// Este metodo es para mostrar el bloque del mantenimiento y ocultar el de consulta
        /// </summary>
        private void Mantenimiento_Consulta() {
            DivBloqueConsulta.Visible = false;
            DivBloqueMantenimiento.Visible = true;
        }
        private void RestablecerPantalla() {
            ModoConsulta();
            Consulta_Mantenimiento();
            CargarTipoSuplidorConsulta();
            CargarTipoSuplidorMantenimiento();
            txtNombreSuplidorConsulta.Text = string.Empty;

            txtNombreSuplidorMantenimiento.Text = string.Empty;
            txtCntactoMantenimento.Text = string.Empty;
            txtDirecconMantenimiento.Text = string.Empty;
            txtEmailMantenimiento.Text = string.Empty;
            txttelefonoSuplidorMantenimiento.Text = string.Empty;
            MostrarListadoSuplidores();
        }

        #region MOSTRAR EL LISTADO DE LOS TIPOS DE SUPLLIDROES
        private void HandlePaging()
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
            lbPaginaActualVariavle.Text = (NumeroPagina + 1).ToString();
            // Now creating page number based on above first and last page index
            for (var i = _PrimeraPagina; i < _UltimaPagina; i++)
            {
                var dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }


            dtPaginacion.DataSource = dt;
            dtPaginacion.DataBind();
        }
        private void Paginar(ref Repeater RptGrid, IEnumerable<object> Listado, int _NumeroRegistros)
        {
            pagedDataSource.DataSource = Listado;
            pagedDataSource.AllowPaging = true;

            ViewState["TotalPages"] = pagedDataSource.PageCount;
            // lbNumeroVariable.Text = "1";
            lbCantidadPaginaVAriable.Text = pagedDataSource.PageCount.ToString();

            //MOSTRAMOS LA CANTIDAD DE PAGINAS A MOSTRAR O NUMERO DE REGISTROS
            pagedDataSource.PageSize = (_NumeroRegistros == 0 ? _TamanioPagina : _NumeroRegistros);
            pagedDataSource.CurrentPageIndex = CurrentPage;

            //HABILITAMOS LOS BOTONES DE LA PAGINACION
            LinkPrimeraPagina.Enabled = !pagedDataSource.IsFirstPage;
            LinkAnterior.Enabled = !pagedDataSource.IsFirstPage;
            LinkSiguiente.Enabled = !pagedDataSource.IsLastPage;
            LinkUltimo.Enabled = !pagedDataSource.IsLastPage;

            RptGrid.DataSource = pagedDataSource;
            RptGrid.DataBind();


            divPaginacion.Visible = true;
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
                    lbPaginaActualVariavle.Text = "1";

                    break;

                case 2:
                    //SEGUNDA PAGINA
                    PaginaActual = Convert.ToInt32(lbPaginaActualVariavle.Text);
                    PaginaActual++;
                    lbPaginaActualVariavle.Text = PaginaActual.ToString();
                    break;

                case 3:
                    //PAGINA ANTERIOR
                    PaginaActual = Convert.ToInt32(lbPaginaActualVariavle.Text);
                    if (PaginaActual > 1)
                    {
                        PaginaActual--;
                        lbPaginaActualVariavle.Text = PaginaActual.ToString();
                    }
                    break;

                case 4:
                    //ULTIMA PAGINA
                    lbPaginaActualVariavle.Text = lbCantidadPaginaVAriable.Text;
                    break;


            }

        }

        private void MostrarListadoSuplidores() {
           
            decimal? _IdTipoSuplidor = ddlSeleccionarTipoSuplidorConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarTipoSuplidorConsulta.SelectedValue) : new Nullable<decimal>();
            string _NombreSuplidor = string.IsNullOrEmpty(txtNombreSuplidorConsulta.Text.Trim()) ? null : txtNombreSuplidorConsulta.Text.Trim();

            var BuscarRegistros = ObjdataInventario.Value.BuscaSuplidores(
                _IdTipoSuplidor,
                new Nullable<decimal>(),
                _NombreSuplidor,
                Convert.ToDecimal(Session["IdUsuario"]));
            int CantidadRegistros = 0;
            if (BuscarRegistros.Count() < 1) {
                lbCantidaRegistrosVariable.Text = "0";
            }
            else {
                foreach (var n in BuscarRegistros) {
                    CantidadRegistros = Convert.ToInt32(n.CantidadRegistros);
                }
                lbCantidaRegistrosVariable.Text = CantidadRegistros.ToString("N0");
                Paginar(ref rpListadoSuplidores, BuscarRegistros, 10);
                HandlePaging();
                divPaginacion.Visible = true;
            }


        }

        #endregion
        private void ExportarReporte(decimal? IdUsuario, string RutaReporte, string UsuarioBD, string ClaveBD, string NombreArchivo) {

            decimal? _IdTipoSuplidor = ddlSeleccionarTipoSuplidorConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarTipoSuplidorConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _IdSUplidor = null;
            string _NombreSuplidor = string.IsNullOrEmpty(txtNombreSuplidorConsulta.Text.Trim()) ? null : txtNombreSuplidorConsulta.Text.Trim();
           
            

            ReportDocument Reporte = new ReportDocument();

            Reporte.Load(RutaReporte);
            Reporte.Refresh();
            Reporte.SetParameterValue("@IdTipoSuplidor", _IdTipoSuplidor);
            Reporte.SetParameterValue("@IdSuplidor", _IdSUplidor);
            Reporte.SetParameterValue("@Nombre", _NombreSuplidor);
            Reporte.SetParameterValue("@IdUsuarioGenera", IdUsuario);
            Reporte.SetDatabaseLogon(UsuarioBD, ClaveBD);
            if (rbExportarPDF.Checked == true) {
                Reporte.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, NombreArchivo);
            }
            else if (rbExportarExcel.Checked == true) {
                Reporte.ExportToHttpResponse(ExportFormatType.Excel, Response, true, NombreArchivo);
            }
            else if (rbExportarWord.Checked == true) {
                Reporte.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, true, NombreArchivo);
            }
        }
        #endregion
        #region MANTENIMIENTO DE SUPLIDORES
        private void MANSuplidores(decimal IdSuplidor, string Accion) {
            DSMarketWeb.Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionSuplidores procesar = new Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionSuplidores(
                Convert.ToDecimal(ddlSeleccionarTipoSuplidorMantenimiento.SelectedValue),
                IdSuplidor,
                txtNombreSuplidorMantenimiento.Text,
                txttelefonoSuplidorMantenimiento.Text,
                txtEmailMantenimiento.Text,
                txtDirecconMantenimiento.Text,
                cbEstatusSuplidor.Checked,
                Convert.ToDecimal(Session["IdUsuario"]),
                Accion);
            procesar.ProcesarDatosSuplidor();
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                CargarTipoSuplidorConsulta();
                CargarTipoSuplidorMantenimiento();
                ModoConsulta();
                Consulta_Mantenimiento();
                divPaginacion.Visible = false;
                rbExportarPDF.Checked = true;
            }
        }

        protected void btnConsultarRegistros_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            MostrarListadoSuplidores();
        }

        protected void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            Mantenimiento_Consulta();
            btnGaurdar.Visible = true;
            btnModificar.Visible = false;
            cbEstatusSuplidor.Checked = true;
        }

        protected void btnModificarRegistros_Click(object sender, EventArgs e)
        {
            Mantenimiento_Consulta();
            btnGaurdar.Visible = false;
            btnModificar.Visible = true;
        }

        protected void btnExportarRegistros_Click(object sender, EventArgs e)
        {
            string _UsuarioBD = "", _ClaveBD = "";
            var SacaCredencialesBD = ObjdataSeguridad.Value.SacarCredencialesBD(1);
            foreach (var n in SacaCredencialesBD) {
                _UsuarioBD = n.Usuario;
                _ClaveBD = DSMarketWeb.Logic.Comunes.SeguridadEncriptacion.DesEncriptar(n.Clave);
            }

            decimal? _IdUsuario = 0;

            if (Session["IdUsuario"] != null)
            {
                _IdUsuario = Convert.ToDecimal(Session["IdUsuario"]);
            }
            else {
                _IdUsuario = 0;
            }

            ExportarReporte(_IdUsuario, Server.MapPath("ReporteSuplidores.rpt"), _UsuarioBD, _ClaveBD, "Reporte de Suplidores");
        }

        protected void btnRestablecerPantalla_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            RestablecerPantalla();
        }

        protected void btnSeleccionarRegistroBodyRepeater_Click(object sender, EventArgs e)
        {
            var ItemSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfIdSuplidorSelecionado = ((HiddenField)ItemSeleccionado.FindControl("hfIdSuplidor")).Value.ToString();

            lbIdRegistroSeleccionado.Text = hfIdSuplidorSelecionado.ToString();

            var BuscarRegistro = ObjdataInventario.Value.BuscaSuplidores(
                null,
                Convert.ToDecimal(hfIdSuplidorSelecionado),
                null, null);
            lbCantidaRegistrosVariable.Text = "0";
            foreach (var n in BuscarRegistro)
            {
                CargarTipoSuplidorMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarTipoSuplidorMantenimiento, n.IdTipoSuplidor.ToString());
                txtNombreSuplidorMantenimiento.Text = n.Suplidor;
                txttelefonoSuplidorMantenimiento.Text = n.Telefono;
                txtEmailMantenimiento.Text = n.Email;
                txtCntactoMantenimento.Text = n.Telefono;
                txtDirecconMantenimiento.Text = n.Direccion;
                cbEstatusSuplidor.Checked = (n.Estatus0.HasValue ? n.Estatus0.Value : false);
            }
            Paginar(ref rpListadoSuplidores, BuscarRegistro, 10);
            HandlePaging();
            ModoMantenimiento();
        }

        protected void btnGaurdar_Click(object sender, EventArgs e)
        {
            MANSuplidores(0, "INSERT");
            ClientScript.RegisterStartupScript(GetType(), "RegistroGuardado()", "RegistroGuardado();", true);
            RestablecerPantalla();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            string _ClaveSeguridad = string.IsNullOrEmpty(txtClaveSeguridadMantenimiento.Text.Trim()) ? null : txtClaveSeguridadMantenimiento.Text.Trim();

            var ValidarClaveSeguridad = ObjdataSeguridad.Value.BuscaClaveSeguridad(
                new Nullable<decimal>(),
                null,
                DSMarketWeb.Logic.Comunes.SeguridadEncriptacion.Encriptar(_ClaveSeguridad));
            if (ValidarClaveSeguridad.Count() < 1) {
                ClientScript.RegisterStartupScript(GetType(), "ClaveSeguridadNoValida()", "ClaveSeguridadNoValida();", true);
            }
            else {
                MANSuplidores(Convert.ToDecimal(lbIdRegistroSeleccionado.Text), "UPDATE");
                ClientScript.RegisterStartupScript(GetType(), "RegistroModificado()", "RegistroModificado()", true);
                RestablecerPantalla();
            }
        }

        protected void LinkPrimeraPagina_Click(object sender, EventArgs e)
        {

            CurrentPage = 0;
            MostrarListadoSuplidores();
        }

        protected void LinkAnterior_Click(object sender, EventArgs e)
        {
            CurrentPage += -1;
            MostrarListadoSuplidores();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior);
        }

        protected void dtPaginacion_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            MostrarListadoSuplidores();
        }

        protected void dtPaginacion_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void LinkSiguiente_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;

            MostrarListadoSuplidores();
         //   MoverValoresPaginacion((int)OpcionesPaginacionValores.SiguientePagina);
        }

        protected void LinkUltimo_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            MostrarListadoSuplidores();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.UltimaPagina);
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            RestablecerPantalla();
        }
    }
}