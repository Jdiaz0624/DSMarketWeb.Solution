using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace DSMarketWeb.Solution.Paginas.Empresa
{
    public partial class MantenimientoClientes : System.Web.UI.Page
    {
        Lazy<DSMarketWeb.Logic.Logica.LogicaEmpresa.LogicaEmpresa> ObjDataEmpresa = new Lazy<Logic.Logica.LogicaEmpresa.LogicaEmpresa>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion> ObjDataConfiguracion = new Lazy<Logic.Logica.LogicaConfiguracion.LogicaConfiguracion>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaSeguridad.LogicaSeguridad> ObjDataSeguridad = new Lazy<Logic.Logica.LogicaSeguridad.LogicaSeguridad>();

        enum TipoRegistroReporte { 
        RegistroUnico = 1,
        RegistroColectivo=0
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
            lbPaginaActualVariavle.Text = (NumeroPagina + 1).ToString();
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
        #endregion

        private void CargarListadoTipoComprobanteConsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarComprobanteConsulta, ObjDataConfiguracion.Value.BuscaListas("TIPOCOMPROBANTE", null, null), true);
        }
        private void CargarListadoTipoComprobanteMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarComprobanteMantenimiento, ObjDataConfiguracion.Value.BuscaListas("TIPOCOMPROBANTE", null, null));
        }
        private void CargarListadoTipoRNC() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoRNC, ObjDataConfiguracion.Value.BuscaListas("TIPOIDENTIFICACION", null, null));
        }

        private void ModoConsulta() {
            btnConsultarRegistros.Enabled = true;
            btnNuevoRegistro.Enabled = true;
            btnModificarRegistro.Enabled = false;
            btnEliminarRegistro.Enabled = false;
            btnExportarRegistros.Enabled = true;
            btnRestablecer.Enabled = true;
            lbRegistroUnico.Text = "0";
        }

        private void ModoMantenimiento() {
            btnConsultarRegistros.Enabled = false;
            btnNuevoRegistro.Enabled = false;
            btnModificarRegistro.Enabled = true;
            btnEliminarRegistro.Enabled = true;
            btnExportarRegistros.Enabled = true;
            btnRestablecer.Enabled = true;
            lbRegistroUnico.Text = "1";
        }

        private void Consulta_Mantenimiento() {
            DivBloqueConsulta.Visible = true;
            DivBloqueMantenimiento.Visible = false;
        }
        private void Mantenimiento_Consulta() {
            DivBloqueConsulta.Visible = false;
            DivBloqueMantenimiento.Visible = true;
        }
        private void MostrarListadoClientes() {
            string _CodigoCliente = string.IsNullOrEmpty(txtCodigoClienteConsulta.Text.Trim()) ? null : txtCodigoClienteConsulta.Text.Trim();
            decimal? _IdComprobante = ddlSeleccionarComprobanteConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarComprobanteConsulta.SelectedValue) : new Nullable<decimal>();
            string _NombreCliente = string.IsNullOrEmpty(txtNombreClienteConsulta.Text.Trim()) ? null : txtNombreClienteConsulta.Text.Trim();
            string _NumeroIdentificacion = string.IsNullOrEmpty(txtRNCConsulta.Text.Trim()) ? null : txtNombreClienteConsulta.Text.Trim();

            var Buscar = ObjDataEmpresa.Value.BuscaClientes(
                _CodigoCliente,
                _IdComprobante,
                _NombreCliente,
                _NumeroIdentificacion,
                null,
                null,
                null);
            if (Buscar.Count() < 1)
            {
                lbCantidadRegistrosVariable.Text = "0";
            }
            else {
                int CantidadRegistros = Buscar.Count;
                lbCantidadRegistrosVariable.Text = CantidadRegistros.ToString("N0");
                Paginar(ref rpListadoClientes, Buscar, 10);
                HandlePaging(ref dtPaginacion);
                divPaginacion.Visible = true;
            }
        }

        private void BloquearControlMantenimiento() {
            txtNombreClienteMantenimiento.Enabled = false;
            ddlSeleccionarComprobanteMantenimiento.Enabled = false;
            ddlSeleccionarTipoRNC.Enabled = false;
            txtNumeroIdentificacionMantenimiento.Enabled = false;
            txtTelefonoMantenimiento.Enabled = false;
            txtEmailMAntenimiento.Enabled = false;
            txtLimiteCredito.Enabled = false;
            txtComentarioMantenimiento.Enabled = false;
            txtDireccionMAntenimiento.Enabled = false;
            cbEstatus.Enabled = false;
            cbEnvioEmail.Enabled = false;
        }
        private void DesbloquearControlesMantenimiento() {
            txtNombreClienteMantenimiento.Enabled = true;
            ddlSeleccionarComprobanteMantenimiento.Enabled = true;
            ddlSeleccionarTipoRNC.Enabled = true;
            txtNumeroIdentificacionMantenimiento.Enabled = true;
            txtTelefonoMantenimiento.Enabled = true;
            txtEmailMAntenimiento.Enabled = true;
            txtLimiteCredito.Enabled = true;
            txtComentarioMantenimiento.Enabled = true;
            txtDireccionMAntenimiento.Enabled = true;
            cbEstatus.Enabled = true;
            cbEnvioEmail.Enabled = true;
        }

        private void GenerarReporte(decimal IdUsuario, string RutaReporte, string UsuarioBD, string ClaveBD, string NombreArchivo) {

            string _CodigoCliente = string.IsNullOrEmpty(txtCodigoClienteConsulta.Text.Trim()) ? null : txtCodigoClienteConsulta.Text.Trim();
            decimal? _TipoComprobante = ddlSeleccionarComprobanteConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarComprobanteConsulta.SelectedValue) : new Nullable<decimal>();
            string _NombreCliente = string.IsNullOrEmpty(txtNombreClienteConsulta.Text.Trim()) ? null : txtNombreClienteConsulta.Text.Trim();
            string _NumeroIdentificacion = string.IsNullOrEmpty(txtRNCConsulta.Text.Trim()) ? null : txtRNCConsulta.Text.Trim();
            bool? _Estatus = null;
            bool? _EnviaEmail = null;
            int RegistroUnico = Convert.ToInt32(lbRegistroUnico.Text);

            ReportDocument Reporte = new ReportDocument();

            Reporte.Load(RutaReporte);
            Reporte.Refresh();

            if (RegistroUnico == (int)TipoRegistroReporte.RegistroColectivo) {
                Reporte.SetParameterValue("@IdCliente", _CodigoCliente);
                Reporte.SetParameterValue("@IdComprobante", _TipoComprobante);
                Reporte.SetParameterValue("@Nombre", _NombreCliente);
                Reporte.SetParameterValue("@RNC", _NumeroIdentificacion);
                Reporte.SetParameterValue("@Estatus", _Estatus);
                Reporte.SetParameterValue("@EnvioEmail", _EnviaEmail);
                Reporte.SetParameterValue("@UsuarioProcesa", IdUsuario);
            }
            else if (RegistroUnico == (int)TipoRegistroReporte.RegistroUnico) {
                Reporte.SetParameterValue("@IdCliente", lbIdRegistroSeleccionado.Text);
                Reporte.SetParameterValue("@IdComprobante", null);
                Reporte.SetParameterValue("@Nombre", null);
                Reporte.SetParameterValue("@RNC", null);
                Reporte.SetParameterValue("@Estatus", null);
                Reporte.SetParameterValue("@EnvioEmail", null);
                Reporte.SetParameterValue("@UsuarioProcesa", IdUsuario);
            }
         

            Reporte.SetDatabaseLogon(UsuarioBD, ClaveBD);

            if (rbExportarPDF.Checked == true) {
                Reporte.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, NombreArchivo);
            }
            else if (rbExportarExel.Checked == true) {
                Reporte.ExportToHttpResponse(ExportFormatType.Excel, Response, true, NombreArchivo);
            }
            else if (rbExportarWord.Checked == true) {
                Reporte.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, true, NombreArchivo);
            }
        }

        private void RestablecerPantalla() {
            ModoConsulta();
            CargarListadoTipoComprobanteConsulta();
            CargarListadoTipoComprobanteMantenimiento();
            CargarListadoTipoRNC();
            txtClaveSeguridadMantenimiento.Text = string.Empty;
            txtCodigoClienteConsulta.Text = string.Empty;
            txtComentarioMantenimiento.Text = string.Empty;
            txtDireccionMAntenimiento.Text = string.Empty;
            txtEmailMAntenimiento.Text = string.Empty;
            txtLimiteCredito.Text = string.Empty;
            txtNombreClienteConsulta.Text = string.Empty;
            txtNombreClienteMantenimiento.Text = string.Empty;
            txtNumeroIdentificacionMantenimiento.Text = string.Empty;
            txtRNCConsulta.Text = string.Empty;
            txtTelefonoMantenimiento.Text = string.Empty;
            Consulta_Mantenimiento();
            rbExportarPDF.Checked = true;
            MostrarListadoClientes();

        }

        private void MAnClientes(decimal IdCliente, string Accion) {
            DSMarketWeb.Logic.PrcesarMantenimientos.Empresa.ProcesarInformacionClientes Procesar = new Logic.PrcesarMantenimientos.Empresa.ProcesarInformacionClientes(
                IdCliente,
                Convert.ToDecimal(ddlSeleccionarComprobanteMantenimiento.SelectedValue),
                txtNombreClienteMantenimiento.Text,
                txtTelefonoMantenimiento.Text,
                Convert.ToDecimal(ddlSeleccionarTipoRNC.SelectedValue),
                txtNumeroIdentificacionMantenimiento.Text,
                txtDireccionMAntenimiento.Text,
                txtEmailMAntenimiento.Text,
                txtComentarioMantenimiento.Text,
                cbEstatus.Checked,
                (decimal)Session["IdUsuario"],
                Convert.ToDecimal(txtLimiteCredito.Text),
               cbEnvioEmail.Checked,
               Accion);
            Procesar.ProcesarDatosClientes();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                //SACAMOS EL NOMBRE DE USUARIO Y EL NOMBRE DE LA PANTALLA EN LA QUE ESTAMOS
                DSMarketWeb.Logic.Comunes.SacarNombreUsuario NombreUsuario = new Logic.Comunes.SacarNombreUsuario((decimal)Session["IdUsuario"]);

                Label lbUsuarioConectado = (Label)Master.FindControl("lbUsuarioConectado");
                lbUsuarioConectado.Text = NombreUsuario.SacarNombre();


                Label lbNombrePantalla = (Label)Master.FindControl("lbNivelAccesoPantalla");
                lbNombrePantalla.Text = "MANTENIMIENTO DE CLIENTES";



                ModoConsulta();
                Consulta_Mantenimiento();
                divPaginacion.Visible = false;
                CargarListadoTipoComprobanteConsulta();
                CargarListadoTipoComprobanteMantenimiento();
                CargarListadoTipoRNC();
                rbExportarPDF.Checked = true;
                lbRegistroUnico.Text = "0";
            }
        }

        protected void btnConsultarRegistros_Click(object sender, EventArgs e)
        {
            MostrarListadoClientes();
        }

        protected void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            Mantenimiento_Consulta();
            lbClaveSeguridadMAntenimiento.Visible = false;
            txtClaveSeguridadMantenimiento.Visible = false;
            btnGuardar.Visible = true;
            btnEliminar.Visible = false;
            btnModificar.Visible = false;
            DesbloquearControlesMantenimiento();
        }

        protected void btnModificarRegistro_Click(object sender, EventArgs e)
        {
            Mantenimiento_Consulta();
            lbClaveSeguridadMAntenimiento.Visible = true;
            txtClaveSeguridadMantenimiento.Visible = true;
            btnGuardar.Visible = false;
            btnEliminar.Visible = false;
            btnModificar.Visible = true;
            DesbloquearControlesMantenimiento();
        }

        protected void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            Mantenimiento_Consulta();
            lbClaveSeguridadMAntenimiento.Visible = true;
            txtClaveSeguridadMantenimiento.Visible = true;
            btnGuardar.Visible = false;
            btnEliminar.Visible = true;
            btnModificar.Visible = false;
            BloquearControlMantenimiento();
        }

        protected void btnExportarRegistros_Click(object sender, EventArgs e)
        {
            int RegistroUnico = Convert.ToInt32(lbRegistroUnico.Text);
            decimal IdUSuario = Session["IdUsuario"] !=null ? (decimal)Session["IdUsuario"] : 0;
            string UsuarioBD = "", ClaveBD = "";
            var SacarCredencialesBD = ObjDataSeguridad.Value.SacarCredencialesBD(1);
            foreach (var n in SacarCredencialesBD) {
                UsuarioBD = n.Usuario;
                ClaveBD = DSMarketWeb.Logic.Comunes.SeguridadEncriptacion.DesEncriptar(n.Clave);
            }

            if (RegistroUnico == (int)TipoRegistroReporte.RegistroColectivo)
            {
                GenerarReporte(IdUSuario, Server.MapPath("ReporteClientes.rpt"), UsuarioBD, ClaveBD, "Reporte de Clientes");
            }
            else if (RegistroUnico == (int)TipoRegistroReporte.RegistroUnico) {
                GenerarReporte(IdUSuario, Server.MapPath("ReporteClienteUnico.rpt"), UsuarioBD, ClaveBD, "Reporte de Cliente");
            }
        }

        protected void btnRestablecerPantalla_Click(object sender, EventArgs e)
        {
            RestablecerPantalla();
        }

        protected void btnSeleccionarRegistros_Click(object sender, EventArgs e)
        {
            var ItemSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfIdCliente = ((HiddenField)ItemSeleccionado.FindControl("hfIdclientes")).Value.ToString();
            lbIdRegistroSeleccionado.Text = hfIdCliente.ToString();

            var BuscarRegistro = ObjDataEmpresa.Value.BuscaClientes(
                hfIdCliente);
            lbCantidadRegistrosVariable.Text = "1";
            foreach (var n in BuscarRegistro) {
                txtNombreClienteMantenimiento.Text = n.Nombre;
                CargarListadoTipoComprobanteMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarComprobanteMantenimiento, n.IdComprobante.ToString());
                CargarListadoTipoRNC();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarTipoRNC, n.IdTipoIdentificacion.ToString());
                txtNumeroIdentificacionMantenimiento.Text = n.RNC;
                txtTelefonoMantenimiento.Text = n.Telefono;
                txtEmailMAntenimiento.Text = n.Email;
                txtLimiteCredito.Text = n.MontoCredito.ToString();
                txtComentarioMantenimiento.Text = n.Comentario;
                txtDireccionMAntenimiento.Text = n.Direccion;
                cbEstatus.Checked = (n.Estatus0.HasValue ? n.Estatus0.Value : false);
                cbEnvioEmail.Checked = (n.EnvioEmail0.HasValue ? n.EnvioEmail0.Value : false);

            }
            Paginar(ref rpListadoClientes, BuscarRegistro, 1);
            HandlePaging(ref dtPaginacion);
            ModoMantenimiento();
        }

        protected void LinkPrimeraPagina_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            MostrarListadoClientes();
        }

        protected void LinkAnterior_Click(object sender, EventArgs e)
        {
            CurrentPage += -1;
            MostrarListadoClientes();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior);
        }

        protected void dtPaginacion_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            MostrarListadoClientes();
        }

        protected void dtPaginacion_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void LinkSiguiente_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            MostrarListadoClientes();
        }

        protected void LinkUltimo_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            MostrarListadoClientes();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.UltimaPagina);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            MAnClientes(0, "INSERT");
            ClientScript.RegisterStartupScript(GetType(), "RegistroGuardado()", "RegistroGuardado();", true);
            CurrentPage = 0;
            RestablecerPantalla();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            string _ClaveSeguridad = string.IsNullOrEmpty(txtClaveSeguridadMantenimiento.Text.Trim()) ? null : txtClaveSeguridadMantenimiento.Text.Trim();
            DSMarketWeb.Logic.Comunes.ValidarClaveSeguridad Validar = new Logic.Comunes.ValidarClaveSeguridad(_ClaveSeguridad);
            bool RespuestaValidacion = Validar.ResultadoClave();

            if (RespuestaValidacion == true) {
                MAnClientes(Convert.ToDecimal(lbIdRegistroSeleccionado.Text), "UPDATE");
                ClientScript.RegisterStartupScript(GetType(), "RegistroModificado()", "RegistroModificado();", true);
                CurrentPage = 0;
                RestablecerPantalla();
            }
            else {
                ClientScript.RegisterStartupScript(GetType(), "ClaveSeguridadNoValida()", "ClaveSeguridadNoValida();", true);
            }
          
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            string _ClaveSeguridad = string.IsNullOrEmpty(txtClaveSeguridadMantenimiento.Text.Trim()) ? null : txtClaveSeguridadMantenimiento.Text.Trim();
            DSMarketWeb.Logic.Comunes.ValidarClaveSeguridad Validar = new Logic.Comunes.ValidarClaveSeguridad(_ClaveSeguridad);
            bool RespuestaValidacion = Validar.ResultadoClave();

            if (RespuestaValidacion == true) {
                MAnClientes(Convert.ToDecimal(lbIdRegistroSeleccionado.Text), "DELETE");
                ClientScript.RegisterStartupScript(GetType(), "RegistroEliminado()", "RegistroEliminado();", true);
                CurrentPage = 0;
                RestablecerPantalla();
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "ClaveSeguridadNoValida()", "ClaveSeguridadNoValida();", true);
            }
        }

        protected void btnRestablecer_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            RestablecerPantalla();
        }
    }
}