using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DSMarketWeb.Solution.Paginas.Inventario
{
    public partial class MantenimientoModelos : System.Web.UI.Page
    {
        Lazy<DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion> ObjDataConfiguracion = new Lazy<Logic.Logica.LogicaConfiguracion.LogicaConfiguracion>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario> ObjDataInventario = new Lazy<Logic.Logica.LogicaInventario.LogicaInventario>();

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
        #region CARGAR LAS LISTAS DESPLEGABLES
        private void CargarMarcasConsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarMarca, ObjDataConfiguracion.Value.BuscaListas("MARCAS", null, null), true);
        }
        private void CargarMArcasMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarMArcaMantenimiento, ObjDataConfiguracion.Value.BuscaListas("MARCAS", null, null));
        }

        #endregion
        #region CARGAR EL LISTADO DE LOS MODELOS EN EL GRID
        private void CargarModelos() {
            decimal? _IdMarca = ddlSeleccionarMarca.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarMarca.SelectedValue) : new Nullable<decimal>();
            string _Modelo = string.IsNullOrEmpty(txtModeloConsulta.Text.Trim()) ? null : txtModeloConsulta.Text.Trim();

            var Listado = ObjDataInventario.Value.ListadoModelos(_IdMarca, new Nullable<decimal>(), _Modelo);
            if (Listado.Count() < 1)
            {
                rpListadoModelos.DataSource = null;
                rpListadoModelos.DataBind();
            }
            else {
                Paginar(ref rpListadoModelos, Listado, 10, ref lbCantidadPaginaVariable, ref LinkPrimeraPagina, ref LinkAnterior, ref LinkSiguiente, ref LinkUltimo);
                HandlePaging(ref dtPaginacion, ref LinkBlbPaginaActualVariable);
            }
        }
        #endregion
        #region PROCESAR LA INFORMACION
        private void ProcesarInformacionModelos(decimal IdModelo, string Accion) {
            DSMarketWeb.Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionModelos Procesar = new Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionModelos(
                Convert.ToDecimal(ddlSeleccionarMArcaMantenimiento.SelectedValue),
                IdModelo,
                txtModeloMantenimiento.Text,
                cbEstatusMantenimiento.Checked,
                Accion);
            Procesar.ProcesarInformacion();
            RestablecerPantallaVolver();
        }
        #endregion
        private void RestablecerPantallaVolver() {
            DivBloqueConsulta.Visible = true;
            DivBloqueMantenimiento.Visible = false;
            btnConsultar.Enabled = true;
            btnNuevo.Enabled = true;
            btnEditar.Enabled = false;
            btnRestablecer.Enabled = true;
            CargarMarcasConsulta();
            txtModeloConsulta.Text = string.Empty;
            CurrentPage = 0;
            CargarModelos();
            CargarMArcasMantenimiento();
            txtModeloMantenimiento.Text = string.Empty;
            cbEstatusMantenimiento.Checked = true;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                Label lbNombreUsuario = (Label)Master.FindControl("lbUsuarioConectado");
                Label lbNombrePantalla = (Label)Master.FindControl("lbNivelAccesoPantalla");
                DSMarketWeb.Logic.Comunes.SacarNombreUsuario NombreUsuario = new Logic.Comunes.SacarNombreUsuario((decimal)Session["IdUsuario"]);
                lbNombreUsuario.Text = NombreUsuario.SacarNombre();
                lbNombrePantalla.Text = "MANTENIMIENTO DE MODELOS";
                CargarMarcasConsulta();
                CurrentPage = 0;
                CargarModelos();
                DivBloqueMantenimiento.Visible = false;
                btnEditar.Enabled = false;
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            CargarModelos();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            DivBloqueConsulta.Visible = false;
            DivBloqueMantenimiento.Visible = true;
            CargarMArcasMantenimiento();
            txtModeloMantenimiento.Text = string.Empty;
            cbEstatusMantenimiento.Checked = true;
            lbIsRegistroSeleccionado.Text = "0";
            lbAccion.Text = "INSERT";
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            DivBloqueConsulta.Visible = false;
            DivBloqueMantenimiento.Visible = true;
            lbAccion.Text = "UPDATE";
        }

        protected void btnRestablecer_Click(object sender, EventArgs e)
        {
            RestablecerPantallaVolver();
        }

        protected void btnSeleccionarRegistros_Click(object sender, EventArgs e)
        {
            var ItemSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfIdModelo = ((HiddenField)ItemSeleccionado.FindControl("hfIdModelo")).Value.ToString();
            lbIsRegistroSeleccionado.Text = hfIdModelo;
            var BuscarRegistroSeleccionado = ObjDataInventario.Value.ListadoModelos(
                null,
                Convert.ToDecimal(hfIdModelo),
                null);
            Paginar(ref rpListadoModelos, BuscarRegistroSeleccionado, 1, ref lbCantidadPaginaVariable, ref LinkPrimeraPagina, ref LinkAnterior, ref LinkSiguiente, ref LinkUltimo);
            HandlePaging(ref dtPaginacion, ref LinkBlbPaginaActualVariable);

            btnConsultar.Enabled = false;
            btnNuevo.Enabled = false;
            btnEditar.Enabled = true;
            btnRestablecer.Enabled = true;

            foreach (var n in BuscarRegistroSeleccionado) {
                CargarMArcasMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarMArcaMantenimiento, n.IdMarca.ToString());
                txtModeloMantenimiento.Text = n.Modelo;
                cbEstatusMantenimiento.Checked = (n.Estatus0.HasValue ? n.Estatus0.Value : false);
            }
        }

        protected void LinkPrimeraPagina_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            CargarModelos();
        }

        protected void LinkAnterior_Click(object sender, EventArgs e)
        {
            CurrentPage += -1;
            CargarModelos();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref LinkBlbPaginaActualVariable, ref lbCantidadPaginaVariable);
        }

        protected void dtPaginacion_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dtPaginacion_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            CargarModelos();
        }

        protected void LinkSiguiente_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            CargarModelos();
        }

        protected void LinkUltimo_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            CargarModelos();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref LinkBlbPaginaActualVariable, ref lbCantidadPaginaVariable);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ProcesarInformacionModelos(Convert.ToDecimal(lbIsRegistroSeleccionado.Text), lbAccion.Text);
        }

        protected void btnVolverAtras_Click(object sender, EventArgs e)
        {
            RestablecerPantallaVolver();
        }
    }
}