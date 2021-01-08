using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DSMarketWeb.Solution.Paginas.Empresa
{
    public partial class MantenimientoCompraSuplidores : System.Web.UI.Page
    {
        Lazy<DSMarketWeb.Logic.Logica.LogicaEmpresa.LogicaEmpresa> ObjDataEmpresa = new Lazy<Logic.Logica.LogicaEmpresa.LogicaEmpresa>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion> ObjDataConfiguracion = new Lazy<Logic.Logica.LogicaConfiguracion.LogicaConfiguracion>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaSeguridad.LogicaSeguridad> ObjDataSeguridad = new Lazy<Logic.Logica.LogicaSeguridad.LogicaSeguridad>();

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


            divPaginacionUnidadMedida.Visible = true;
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

        #region CARGAR LAS LISTAS DESPLEGABLES
        //LISTAS DESPEGLABLES EN LA PANTALLA DE CONSULTA
        private void CargarTipoSuplidor() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoSuplidorConsulta, ObjDataConfiguracion.Value.BuscaListas("TIPOSUPLIDOR", null, null), true);
        }
        private void CargarSuplidores() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarSuplidorConsulta, ObjDataConfiguracion.Value.BuscaListas("SUPLIDOR", ddlSeleccionarTipoSuplidorConsulta.SelectedValue.ToString(), null), true); ;
        }
        #endregion

        #region MOSTRAR EL LISTADO DE LAS COMPRAS
        private void MostrarListadoCOmpras() {
            if (string.IsNullOrEmpty(txtFechaDesde.Text.Trim()) || string.IsNullOrEmpty(txtFechaHastaConsullta.Text.Trim()))
            {
                ClientScript.RegisterStartupScript(GetType(), "CamposFechaVacios()", "CamposFechaVacios();", true);
                if (string.IsNullOrEmpty(txtFechaDesde.Text.Trim()))
                {
                    ClientScript.RegisterStartupScript(GetType(), "CampoFechaDesdeVacio()", "CampoFechaDesdeVacio();", true);
                }
                if (string.IsNullOrEmpty(txtFechaHastaConsullta.Text.Trim()))
                {
                    ClientScript.RegisterStartupScript(GetType(), "CampoFechaHastaVacio()", "CampoFechaHastaVacio();", true);
                }
            }
            else {
                string _NumeroIdentificacion = string.IsNullOrEmpty(txtNumeroIdentificacionConsulta.Text.Trim()) ? null : txtNumeroIdentificacionConsulta.Text.Trim();
                decimal? _TipoSuplidor = ddlSeleccionarTipoSuplidorConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarTipoSuplidorConsulta.SelectedValue) : new Nullable<decimal>();
                decimal? _Suplidor = ddlSeleccionarSuplidorConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarSuplidorConsulta.SelectedValue) : new Nullable<decimal>();

                var BuscarRegistro = ObjDataEmpresa.Value.BuscaCompraSuplidores(
                    new Nullable<decimal>(),
                    _TipoSuplidor,
                    _Suplidor,
                    _NumeroIdentificacion,
                    Convert.ToDateTime(txtFechaDesde.Text),
                    Convert.ToDateTime(txtFechaHastaConsullta.Text));
                if (BuscarRegistro.Count() < 1)
                {
                    lbCantidadRegistrosVariable.Text = "0";
                }
                else {
                    int CantidadRegistros = 0;

                    foreach (var n in BuscarRegistro) {
                        CantidadRegistros = (int)n.CantidadRegistros;
                    }
                    lbCantidadRegistrosVariable.Text = CantidadRegistros.ToString("N0");
                    Paginar(ref rpListadoCompraSuplidores, BuscarRegistro, 10);
                    HandlePaging(ref dlPaginacion);
                    divPaginacionUnidadMedida.Visible = true;
                }
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                CargarTipoSuplidor();
                CargarSuplidores();
            }
        }

        protected void btnConsultarRegistros_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            MostrarListadoCOmpras();
        }

        protected void btnNuevoRegistro_Click(object sender, EventArgs e)
        {

        }

        protected void btnModificarRegistro_Click(object sender, EventArgs e)
        {

        }

        protected void btnEliminarRegistro_Click(object sender, EventArgs e)
        {

        }

        protected void btnExportarRegistro_Click(object sender, EventArgs e)
        {

        }

        protected void btnRestablecerPantalla_Click(object sender, EventArgs e)
        {

        }

        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {

        }

        protected void LinkPrimeraPaginaPaginacion_Click(object sender, EventArgs e)
        {

        }

        protected void LinkPaginaAnterior_Click(object sender, EventArgs e)
        {

        }

        protected void dlPaginacion_CancelCommand(object source, DataListCommandEventArgs e)
        {

        }

        protected void dlPaginacion_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void LinkPaginaSiguiente_Click(object sender, EventArgs e)
        {

        }

        protected void LinkUltipaPagina_Click(object sender, EventArgs e)
        {

        }

        protected void ddlSeleccionarTipoSuplidorConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarSuplidores();
        }
    }
}