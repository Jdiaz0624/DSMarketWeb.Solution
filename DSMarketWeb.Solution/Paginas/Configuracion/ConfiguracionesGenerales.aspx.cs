using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DSMarketWeb.Solution.Paginas.Configuracion
{
    public partial class ConfiguracionesGenerales : System.Web.UI.Page
    {
        Lazy<DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion> ObjDataConfiguracion = new Lazy<Logic.Logica.LogicaConfiguracion.LogicaConfiguracion>();

        #region CARGAR LOS MODULOS DEL SISTEMA
        private void ListaModulos() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarModulo, ObjDataConfiguracion.Value.BuscaListas("MODULOS", null, null));
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

        #region MOSTRAR LAS OCNFIGURACIONES GENERALES
        private void MostrarConfiguracionesGenerales(decimal? IdConfiguracion, decimal? IdModulo) {

            var BuscarInformacion = ObjDataConfiguracion.Value.BuscaConfiguracionesGenerales(IdConfiguracion, IdModulo);
            Paginar(ref rpListadoConfiguraciones, BuscarInformacion, 10, ref lbCantidadPaginaVariable, ref LinkPrimeraPagina, ref LinkAnterior, ref LinkSiguiente, ref LinkUltimo);
            HandlePaging(ref dtPaginacion, ref LinkBlbPaginaActualVariable);
           
        }
        #endregion
        #region VALIDAR LA CLAVE DE SEGURIDAD DEL SISTEMA
        private void ValidarClaveSeguridad() {
            string _ClaveSeguridad = string.IsNullOrEmpty(txtClaveSeguridad.Text.Trim()) ? null : txtClaveSeguridad.Text.Trim();

            bool Validacion = false;

            DSMarketWeb.Logic.Comunes.ValidarClaveSeguridad Validar = new Logic.Comunes.ValidarClaveSeguridad(_ClaveSeguridad);
            Validacion = Validar.ResultadoClave();
            switch (Validacion) {
                case true:
                    decimal? IdModulo = cbFiltrarPorModulo.Checked == true ? Convert.ToDecimal(ddlSeleccionarModulo.SelectedValue) : new Nullable<decimal>();
                    MostrarConfiguracionesGenerales(new Nullable<decimal>(), IdModulo);
                    cbFiltrarPorModulo.Enabled = true;
                    cbFiltrarPorModulo.Checked = false;
                    break;

                case false:
                    ClientScript.RegisterStartupScript(GetType(), "ClaveSeguridadInvalida()", "ClaveSeguridadInvalida();", true);
                    break;
            }
        }
        #endregion
        #region MODIFICAR LAS CONFIGURACIONES GENERALES
        private void ModificarConfiguracionesGenerales(decimal IdConfiguracion,decimal IdModulo) {
            //SACAMOS EL ESTATUS ACTUAL DEL REGISTRO SELECCIONADO

            bool EstatusConfiguracion = false;

            var BuscarInformacion = ObjDataConfiguracion.Value.BuscaConfiguracionesGenerales(IdConfiguracion, IdModulo);
            foreach (var n in BuscarInformacion) {
                EstatusConfiguracion = (bool)n.Estatus0;
            }

            DSMarketWeb.Logic.PrcesarMantenimientos.Configuracion.ProcesarInformacionConfiguracionesGenerales Modificar = new Logic.PrcesarMantenimientos.Configuracion.ProcesarInformacionConfiguracionesGenerales(
                IdConfiguracion,
                IdModulo,
                "",
                EstatusConfiguracion,
                "UPDATE");
            Modificar.ProcesarInformacion();

            if (cbFiltrarPorModulo.Checked == true)
            {
                MostrarConfiguracionesGenerales(new Nullable<decimal>(), Convert.ToDecimal(ddlSeleccionarModulo.SelectedValue));
            }
            else {
                MostrarConfiguracionesGenerales(new Nullable<decimal>(), new Nullable<decimal>());
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                
                ddlSeleccionarModulo.Enabled = false;
                cbFiltrarPorModulo.Enabled = false;
                cbFiltrarPorModulo.Checked = false;

                Label lbUsuarioConectado = (Label)Master.FindControl("lbUsuarioConectado");
                DSMarketWeb.Logic.Comunes.SacarNombreUsuario Nombre = new Logic.Comunes.SacarNombreUsuario((decimal)Session["IdUsuario"]);
                lbUsuarioConectado.Text = Nombre.SacarNombre();

                Label lbNivelAccesoPantalla = (Label)Master.FindControl("lbNivelAccesoPantalla");
                lbNivelAccesoPantalla.Text = "CONFIGURACIONES GENERALES";
            }
        }

        protected void cbFiltrarPorModulo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFiltrarPorModulo.Checked == true) {
                ddlSeleccionarModulo.Enabled = true;
                ListaModulos();
                CurrentPage = 0;
                MostrarConfiguracionesGenerales(new Nullable<decimal>(), Convert.ToDecimal(ddlSeleccionarModulo.SelectedValue));
            }
            else if (cbFiltrarPorModulo.Checked == false) {
                ddlSeleccionarModulo.Enabled = false;
                CurrentPage = 0;
                MostrarConfiguracionesGenerales(new Nullable<decimal>(), new Nullable<decimal>());
            }
        }

        protected void btnValidarClaveSeguridad_Click(object sender, EventArgs e)
        {
            ValidarClaveSeguridad();
        }

        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {
            var IdConfiguracionSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfIdConfiguracion = ((HiddenField)IdConfiguracionSeleccionado.FindControl("hfIdConfiguracionGeneral")).Value.ToString();

            var IdModuloSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfIdModulo = ((HiddenField)IdModuloSeleccionado.FindControl("hfIdModulo")).Value.ToString();

            ModificarConfiguracionesGenerales(Convert.ToDecimal(hfIdConfiguracion.ToString()), Convert.ToDecimal(hfIdModulo.ToString()));
        }

        protected void LinkPrimeraPagina_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            decimal? IdModulo = cbFiltrarPorModulo.Checked == true ? Convert.ToDecimal(ddlSeleccionarModulo.SelectedValue) : new Nullable<decimal>();
            MostrarConfiguracionesGenerales(new Nullable<decimal>(), IdModulo);
        }

        protected void LinkAnterior_Click(object sender, EventArgs e)
        {
            CurrentPage += -1;
            decimal? IdModulo = cbFiltrarPorModulo.Checked == true ? Convert.ToDecimal(ddlSeleccionarModulo.SelectedValue) : new Nullable<decimal>();
            MostrarConfiguracionesGenerales(new Nullable<decimal>(), IdModulo);
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref LinkBlbPaginaActualVariable, ref lbCantidadPaginaVariable);
        }

        protected void dtPaginacion_ItemDataBound(object sender, DataListItemEventArgs e)  
        {

        }

        protected void dtPaginacion_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            decimal? IdModulo = cbFiltrarPorModulo.Checked == true ? Convert.ToDecimal(ddlSeleccionarModulo.SelectedValue) : new Nullable<decimal>();
            MostrarConfiguracionesGenerales(new Nullable<decimal>(), IdModulo);
        }

        protected void LinkSiguiente_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            decimal? IdModulo = cbFiltrarPorModulo.Checked == true ? Convert.ToDecimal(ddlSeleccionarModulo.SelectedValue) : new Nullable<decimal>();
            MostrarConfiguracionesGenerales(new Nullable<decimal>(), IdModulo);
        }

        protected void LinkUltimo_Click(object sender, EventArgs e)
        {

            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            decimal? IdModulo = cbFiltrarPorModulo.Checked == true ? Convert.ToDecimal(ddlSeleccionarModulo.SelectedValue) : new Nullable<decimal>();
            MostrarConfiguracionesGenerales(new Nullable<decimal>(), IdModulo);
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref LinkBlbPaginaActualVariable, ref lbCantidadPaginaVariable);
        }

        protected void ddlSeleccionarModulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFiltrarPorModulo.Checked == true) {
                MostrarConfiguracionesGenerales(new Nullable<decimal>(), Convert.ToDecimal(ddlSeleccionarModulo.SelectedValue));
            }
        }
    }
}