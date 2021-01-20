using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DSMarketWeb.Solution.Paginas.Empresa
{
    public partial class Citas : System.Web.UI.Page
    {
        Lazy<DSMarketWeb.Logic.Logica.LogicaEmpresa.LogicaEmpresa> ObjDataLogica = new Lazy<Logic.Logica.LogicaEmpresa.LogicaEmpresa>();
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
        private void Paginar(ref Repeater RptGrid, IEnumerable<object> Listado, int _NumeroRegistros, ref Label lbCantidadPagina, ref LinkButton PrimeraPagina, ref LinkButton PaginaAnterior, ref LinkButton PaginaSiguiente, ref LinkButton UltimaPagina)
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
            PaginaSiguiente.Enabled = !pagedDataSource.IsLastPage;
            UltimaPagina.Enabled = !pagedDataSource.IsLastPage;

            RptGrid.DataSource = pagedDataSource;
            RptGrid.DataBind();


          //  divPaginacionBancos.Visible = true;
        }
        enum OpcionesPaginacionValores
        {
            PrimeraPagina = 1,
            SiguientePagina = 2,
            PaginaAnterior = 3,
            UltimaPagina = 4
        }
        private void MoverValoresPaginacion(int Accion, ref Label lbPaginaActual, ref Label CantidadPagina)
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
                    lbPaginaActual.Text = CantidadPagina.Text;
                    break;


            }

        }
        #endregion

        private void CargarListasDesplegablesConsulta() {
            CargarDepartamentosConsulta();
            CargarTcnicoConsulta();
        }
        private void CargarDepartamentosConsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarDepartamentoConsulta, ObjDataConfiguracion.Value.BuscaListas("DEPARTAMENTO", null, null), true);
        }
        private void CargarTcnicoConsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarEmpleadoConsulta, ObjDataConfiguracion.Value.BuscaListas("TECNICO", ddlSeleccionarDepartamentoConsulta.SelectedValue.ToString(), null), true);
        }

        private void CargarListaDesplegableMantenimiento() {
            CargarListaDepartamentoMantenimiento();
            CargarTecnicoMantenimiento();
        }
        private void CargarListaDepartamentoMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarDepartamentoMantenimiento, ObjDataConfiguracion.Value.BuscaListas("DEPARTAMENTO", null, null));
            
        }
        private void CargarTecnicoMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarEmpleadoMantenimiento, ObjDataConfiguracion.Value.BuscaListas("TECNICO", ddlSeleccionarDepartamentoMantenimiento.SelectedValue.ToString(), null));
        }

        private void ModoConsulta() {
            btnConsultarRegistros.Enabled = true;
            btnNuevoRegistro.Enabled = true;
            btnEditarRegistro.Enabled = false;
            btnEliminarRegistro.Enabled = false;
            btnRestablecerPantalla.Enabled = true;
            btnReporte.Enabled = true;
        }

        private void ModificarMantenimiento() {
            btnConsultarRegistros.Enabled = false;
            btnNuevoRegistro.Enabled = false;
            btnEditarRegistro.Enabled = true;
            btnEliminarRegistro.Enabled = true;
            btnRestablecerPantalla.Enabled = true;
            btnReporte.Enabled = true;
        }

        private void ListadoCitas() {
            bool? _Estatus = null;
            string _NumeroCita = string.IsNullOrEmpty(txtNumeroCitaConsulta.Text.Trim()) ? null : txtNumeroCitaConsulta.Text.Trim();
            string _NombreCliente = string.IsNullOrEmpty(txtNombreClienteConsulta.Text.Trim()) ? null : txtNombreClienteConsulta.Text.Trim();
            string _NumeroIdentificacion = string.IsNullOrEmpty(txtNumeroIdentificacionConsulta.Text.Trim()) ? null : txtNumeroIdentificacionConsulta.Text.Trim();
            decimal? _Empleado = ddlSeleccionarEmpleadoConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarEmpleadoConsulta.SelectedValue) : new Nullable<decimal>();
            DateTime? FechaDesde = string.IsNullOrEmpty(txtFechaDesde.Text.Trim()) ? new Nullable<DateTime>() : Convert.ToDateTime(txtFechaDesde.Text);
            DateTime? FechaHasta = string.IsNullOrEmpty(txtFechaHasta.Text.Trim()) ? new Nullable<DateTime>() : Convert.ToDateTime(txtFechaHasta.Text);


            if (rbTodos.Checked == true) {
                _Estatus = null;
            }
            else if (rbRegistrosActivos.Checked == true) {
                _Estatus = true;
            }
            else if (rbRegistrosInactivos.Checked == true) {
                _Estatus = false;
            }



            if (cbAgregarRangoFechaConsulta.Checked == true) {
                if (string.IsNullOrEmpty(txtFechaDesde.Text.Trim()) || string.IsNullOrEmpty(txtFechaHasta.Text.Trim())) { }
                else {
                    var Listado = ObjDataLogica.Value.BuscaCitasEncabezado(
                   _NumeroCita,
                   _Empleado,
                   Convert.ToDateTime(txtFechaDesde.Text),
                   Convert.ToDateTime(txtFechaHasta.Text),
                   _NombreCliente,
                   _NumeroIdentificacion,
                   _Estatus);
                    if (Listado.Count() < 1)
                    {
                        lbCantidadRegistrosVariable.Text = "0";
                    }
                    else
                    {
                        Paginar(ref rpListadoCitasEncabezado, Listado, 10, ref lbCantidadPaginaVariableCitaEncabezado, ref LinkPrimeraPaginaCitasEncabezado, ref LinkPaginaAnteriorCitasEncabezado, ref LinkPaginaSiguienteCitasEncabezado, ref LinkUltipaPaginaCitasEncabezado);
                        HandlePaging(ref dlPaginacionCitasEncabezado, ref lbCantidadPaginaVariableCitaEncabezado);
                        int CantidadRegistros = 0;
                        foreach (var n in Listado)
                        {
                            CantidadRegistros = (int)n.CantidadRegistros;
                        }
                        lbCantidadRegistrosVariable.Text = CantidadRegistros.ToString("N0");
                    }
                }
            }
            else {
                var Listado = ObjDataLogica.Value.BuscaCitasEncabezado(
                 _NumeroCita,
                 _Empleado,
                 null,
                 null,
                 _NombreCliente,
                 _NumeroIdentificacion,
                 _Estatus);
                if (Listado.Count() < 1)
                {
                    lbCantidadRegistrosVariable.Text = "0";
                }
                else
                {
                    Paginar(ref rpListadoCitasEncabezado, Listado, 10, ref lbCantidadPaginaVariableCitaEncabezado, ref LinkPrimeraPaginaCitasEncabezado, ref LinkPaginaAnteriorCitasEncabezado, ref LinkPaginaSiguienteCitasEncabezado, ref LinkUltipaPaginaCitasEncabezado);
                    HandlePaging(ref dlPaginacionCitasEncabezado, ref lbCantidadPaginaVariableCitaEncabezado);
                    int CantidadRegistros = 0;
                    foreach (var n in Listado)
                    {
                        CantidadRegistros = (int)n.CantidadRegistros;
                    }
                    lbCantidadRegistrosVariable.Text = CantidadRegistros.ToString("N0");
                }
            }
          
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                CargarListasDesplegablesConsulta();
                CargarListaDepartamentoMantenimiento();
                rbTodos.Checked = true;
                rbExportarPDF.Checked = true;
                DivBloqueDetalleCita.Visible = false;
                DivBloqueMantenimieto.Visible = false;
                ModoConsulta();
                CurrentPage = 0;
                ListadoCitas();
            }
        }

        protected void btnConsultarRegistros_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            ListadoCitas();
        }

        protected void btnNuevoRegistro_Click(object sender, EventArgs e)
        {

        }

        protected void btnEditarRegistro_Click(object sender, EventArgs e)
        {

        }

        protected void btnEliminarRegistro_Click(object sender, EventArgs e)
        {

        }

        protected void btnReporte_Click(object sender, EventArgs e)
        {

        }

        protected void btnRestablecerPantalla_Click(object sender, EventArgs e)
        {

        }

        protected void LinkPrimeraPaginaCitasEncabezado_Click(object sender, EventArgs e)
        {

        }

        protected void LinkPaginaAnteriorCitasEncabezado_Click(object sender, EventArgs e)
        {

        }

        protected void dlPaginacionCitasEncabezado_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dlPaginacionCitasEncabezado_CancelCommand(object source, DataListCommandEventArgs e)
        {

        }

        protected void LinkPaginaSiguienteCitasEncabezado_Click(object sender, EventArgs e)
        {

        }

        protected void LinkUltipaPaginaCitasEncabezado_Click(object sender, EventArgs e)
        {

        }

        protected void LinkPrimeroCitaDetalle_Click(object sender, EventArgs e)
        {

        }

        protected void LinkAnteriorCitaDetalle_Click(object sender, EventArgs e)
        {

        }

        protected void dlPaginacionCitaDetalle_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dlPaginacionCitaDetalle_CancelCommand(object source, DataListCommandEventArgs e)
        {

        }

        protected void LinkSiguienteCitaDetalle_Click(object sender, EventArgs e)
        {

        }

        protected void LinkUltimoSiguienteDetalle_Click(object sender, EventArgs e)
        {

        }

        protected void btnBuscarServicios_Click(object sender, EventArgs e)
        {

        }

        protected void btnSeleccionarServicio_Click(object sender, EventArgs e)
        {

        }

        protected void LinkPrimeroServicioAgregar_Click(object sender, EventArgs e)
        {

        }

        protected void LinkAnteriorServicioAgregar_Click(object sender, EventArgs e)
        {

        }

        protected void dtPaginacionServicioAgregar_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dtPaginacionServicioAgregar_CancelCommand(object source, DataListCommandEventArgs e)
        {

        }

        protected void LinkSiguienteServicioAgregar_Click(object sender, EventArgs e)
        {

        }

        protected void LinkUltimoServicioAgregar_Click(object sender, EventArgs e)
        {

        }

        protected void LinkPrimeroQuitar_Click(object sender, EventArgs e)
        {

        }

        protected void LinkAnteriorQuitar_Click(object sender, EventArgs e)
        {

        }

        protected void dlPaginacionQuitar_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dlPaginacionQuitar_CancelCommand(object source, DataListCommandEventArgs e)
        {

        }

        protected void LinkSiguienteQuitar_Click(object sender, EventArgs e)
        {

        }

        protected void LinkUltimoQuitar_Click(object sender, EventArgs e)
        {

        }

        protected void btnGuardarCita_Click(object sender, EventArgs e)
        {

        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {

        }

        protected void ddlSeleccionarDepartamentoConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarTcnicoConsulta();
        }

        protected void ddlSeleccionarDepartamentoMantenimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarTecnicoMantenimiento();
        }
    }
}