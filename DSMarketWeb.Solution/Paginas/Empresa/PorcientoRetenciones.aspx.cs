using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DSMarketWeb.Solution.Paginas.Empresa
{
    public partial class PorcientoRetenciones : System.Web.UI.Page
    {
        Lazy<DSMarketWeb.Logic.Logica.LogicaEmpresa.LogicaEmpresa> ObjDataEmpresa = new Lazy<Logic.Logica.LogicaEmpresa.LogicaEmpresa>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaSeguridad.LogicaSeguridad> ObjDataSeguridad = new Lazy<Logic.Logica.LogicaSeguridad.LogicaSeguridad>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion> ObjDataConfiguracion = new Lazy<Logic.Logica.LogicaConfiguracion.LogicaConfiguracion>();

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


            divPaginacionPorcientoRetencion.Visible = true;
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

        private void CargarlistaRetencionConsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarRetencionConsulta, ObjDataConfiguracion.Value.BuscaListas("RETENCIONES", null, null), true);
        }
        private void CargarListaRetencionMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarRetencionMantenimiento, ObjDataConfiguracion.Value.BuscaListas("RETENCIONES", null, null));
        }
        private void ModoConsulta() {
            btnConsularRegistros.Enabled = true;
            btnNuevoregistro.Enabled = true;
            btnModificarRegistro.Enabled = false;
            btnRestablecer.Enabled = true;
        }
        private void ModoMantenimiento() {
            btnConsularRegistros.Enabled = false;
            btnNuevoregistro.Enabled = false;
            btnModificarRegistro.Enabled = true;
            btnRestablecer.Enabled = true;
        }
        private void Consulta_Mantenimiento() {
            DivBloqueConsulta.Visible = true;
            DivBloqueMantenimiento.Visible = false;
        }

        private void Mantenimiento_Consulta() {
            DivBloqueConsulta.Visible = false;
            DivBloqueMantenimiento.Visible = true;
        }

        private void ListadoRetenciones() {
            decimal? _Retencion = ddlSeleccionarRetencionConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarRetencionConsulta.SelectedValue) : new Nullable<decimal>();

            var Listado = ObjDataEmpresa.Value.BuscaPorcientoRetenciones(
                new Nullable<decimal>(),
                _Retencion,
                null);
            if (Listado.Count() < 1)
            {
                lbCantidadRegistrosVariable.Text = "0";
            }
            else {
                Paginar(ref rpListadoPorcientoRetencion, Listado, 10);
                HandlePaging(ref dlPaginacion);
                int CantidadRegistros = 0;
                foreach (var n in Listado) {
                    CantidadRegistros = (int)n.CantidadRegistros;
                }
                lbCantidadRegistrosVariable.Text = CantidadRegistros.ToString("N0");
                divPaginacionPorcientoRetencion.Visible = true;
            }
        }

        private void RestablecerPantalla() {
            CargarlistaRetencionConsulta();
            CargarListaRetencionMantenimiento();
            txtClaveseguridadMantenimiento.Text = string.Empty;
            txtMontoFinalMantenimiento.Text = string.Empty;
            txtMontoInicialMantenimiento.Text = string.Empty;
            txtMontoSumarMantenimiento.Text = string.Empty;
            txtPorcientoCia.Text = string.Empty;
            txtPorcientoEmpleado.Text = string.Empty;
            ModoConsulta();
            Consulta_Mantenimiento();
            ListadoRetenciones();
        }

        private void MANListadoPorcientoRetenciones(decimal IdPorcientoRetencion, int Secuencia, string Accion) {
            DSMarketWeb.Logic.PrcesarMantenimientos.Empresa.ProcesarInformacionPorcientoRetenciones Procesar = new Logic.PrcesarMantenimientos.Empresa.ProcesarInformacionPorcientoRetenciones(
                IdPorcientoRetencion,
                Convert.ToDecimal(ddlSeleccionarRetencionMantenimiento.SelectedValue),
                Secuencia,
                Convert.ToDecimal(txtMontoInicialMantenimiento.Text),
                Convert.ToDecimal(txtMontoFinalMantenimiento.Text),
                Convert.ToDecimal(txtMontoSumarMantenimiento.Text),
                Convert.ToDecimal(txtPorcientoCia.Text),
                Convert.ToDecimal(txtPorcientoEmpleado.Text),
                cbEstatusMantenimiento.Checked,
                (decimal)Session["IdUsuario"],
                Accion);
            Procesar.ProcesarInformacion();

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                CargarlistaRetencionConsulta();
                CargarListaRetencionMantenimiento();
                ModoConsulta();
                Consulta_Mantenimiento();
                ListadoRetenciones();
                btnNuevoregistro.Visible = false;
                btnModificarRegistro.Visible = false;
            }
        }

        protected void btnConsularRegistros_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            ListadoRetenciones();
        }

        protected void btnNuevoregistro_Click(object sender, EventArgs e)
        {
            Mantenimiento_Consulta();
            txtMontoFinalMantenimiento.Text = string.Empty;
            txtMontoInicialMantenimiento.Text = string.Empty;
            txtMontoSumarMantenimiento.Text = string.Empty;
            txtPorcientoCia.Text = string.Empty;
            txtPorcientoEmpleado.Text = string.Empty;
            cbEstatusMantenimiento.Checked = true;
            lbClaveSeguridadMantenimiento.Visible = false;
            txtClaveseguridadMantenimiento.Visible = false;
            btnGuardar.Visible = true;
            btnModificar.Visible = false;
        }

        protected void btnModificarRegistro_Click(object sender, EventArgs e)
        {
            Mantenimiento_Consulta();
            lbClaveSeguridadMantenimiento.Visible = false;
            txtClaveseguridadMantenimiento.Visible = false;
            btnGuardar.Visible = false;
            btnModificar.Visible = true;
        }

        protected void btnRestablecer_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            RestablecerPantalla();
        }

        protected void btnSeleccionarRegistro_Click(object sender, EventArgs e)
        {
            var IdPorcientoRetencionSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfIdPorcientoRetencion = ((HiddenField)IdPorcientoRetencionSeleccionado.FindControl("hfIdPorcientoRetencion")).Value.ToString();


            var RegistroSeleccionado = ObjDataEmpresa.Value.BuscaPorcientoRetenciones(
                Convert.ToDecimal(hfIdPorcientoRetencion), null, null);
            Paginar(ref rpListadoPorcientoRetencion, RegistroSeleccionado, 1);
            HandlePaging(ref dlPaginacion);
            lbCantidadRegistrosVariable.Text = "1";
            foreach (var n in RegistroSeleccionado) {
                CargarListaRetencionMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarRetencionMantenimiento, n.IdRetencion.ToString());
                decimal MontoIncial = (decimal)n.MontoInicial;
                txtMontoInicialMantenimiento.Text = MontoIncial.ToString();
                txtMontoFinalMantenimiento.Text = n.MontoFinal.ToString();
                txtMontoSumarMantenimiento.Text = n.MontoSumar.ToString();
                txtPorcientoCia.Text = n.PorcientoCia.ToString();
                txtPorcientoEmpleado.Text = n.PorcientoEmpleado.ToString();
            }
            ModoMantenimiento();

        }

        protected void LinkPrimeraPaginaPaginacion_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            ListadoRetenciones();
        }

        protected void LinkPaginaAnterior_Click(object sender, EventArgs e)
        {
            CurrentPage += -1;
            ListadoRetenciones();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior);
        }

        protected void dlPaginacion_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dlPaginacion_CancelCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            ListadoRetenciones();
        }

        protected void LinkPaginaSiguiente_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            ListadoRetenciones();
        }

        protected void LinkUltipaPagina_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            ListadoRetenciones();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.UltimaPagina);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            MANListadoPorcientoRetenciones(0, 0, "INSERT");
            ClientScript.RegisterStartupScript(GetType(), "RegistroGuardado()", "RegistroGuardado();", true);
            CurrentPage = 0;
            ListadoRetenciones();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            string _ClaveSeguridad = string.IsNullOrEmpty(txtClaveseguridadMantenimiento.Text.Trim()) ? null : txtClaveseguridadMantenimiento.Text.Trim();
            DSMarketWeb.Logic.Comunes.ValidarClaveSeguridad Validar = new Logic.Comunes.ValidarClaveSeguridad(_ClaveSeguridad);
            bool Respuesta = Validar.ResultadoClave();
            if (Respuesta == true) {
                MANListadoPorcientoRetenciones(Convert.ToDecimal(lbIdRegistroSeleccionado.Text), Convert.ToInt32(lbSecuenciaSeleccionada.Text), "UPDATE");
                ClientScript.RegisterStartupScript(GetType(), "RegistroModificado()", "RegistroModificado();", true);
                CurrentPage = 0;
                ListadoRetenciones();
            }
            else {
                ClientScript.RegisterStartupScript(GetType(), "ClaveSeguridadNoValida()", "ClaveSeguridadNoValida();", true);
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            RestablecerPantalla();
        }
    }
}