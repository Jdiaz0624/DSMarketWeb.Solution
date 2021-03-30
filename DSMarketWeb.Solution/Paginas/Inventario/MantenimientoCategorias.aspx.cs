using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DSMarketWeb.Solution.Paginas.Inventario
{
    public partial class MantenimientoCategorias : System.Web.UI.Page
    {
        Lazy<DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion> ObjDataConfiguracion = new Lazy<Logic.Logica.LogicaConfiguracion.LogicaConfiguracion>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario> ObjDataInventario = new Lazy<Logic.Logica.LogicaInventario.LogicaInventario>();
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
        private void CargarTipoProductoConsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoProducto, ObjDataConfiguracion.Value.BuscaListas("TIPOPRODUCTO", null, null), true);
        }
        private void CargarTipoProductoManteniiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoProductoMantenimiento, ObjDataConfiguracion.Value.BuscaListas("TIPOPRODUCTO", null, null));
        }
        #endregion
        #region MODOS DE PANTALLA
        private void ModoConsulta() {
            btnConsultar.Enabled = true;
            btnNuevoRegistro.Enabled = true;
            btnModificarRegistroSeleccionado.Enabled = false;
            btnRestabelcer.Enabled = true;


        }
        private void ModoMantenimiento() {

            btnConsultar.Enabled = false;
            btnNuevoRegistro.Enabled = false;
            btnModificarRegistroSeleccionado.Enabled = true;
            btnRestabelcer.Enabled = true;

        }

        private void MostrarControles() {
            divBloqueConsulta.Visible = false;
            divBloqueMantenimiento.Visible = true;
        }
        private void OcultarControles() {

            divBloqueConsulta.Visible = true;
            divBloqueMantenimiento.Visible = false;
        }
        #endregion
        #region MANTENIMIENTO DE PRODUCTO
        private void ManCategoria(decimal IdCategoria,string Accion) {

            DSMarketWeb.Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionCateorias Procesar = new Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionCateorias(
                IdCategoria,
                Convert.ToDecimal(ddlSeleccionarTipoProductoMantenimiento.SelectedValue),
                txtCategoriaMantenimiento.Text,
                cbEstatusMantenimiento.Checked,
                (decimal)Session["IdUsuario"],
                Accion);
            Procesar.ProcesarDatosCategoria();


        }
        #endregion
        #region RESTABLECER PANTALA
        private void RestabelcerPantalla() {
            ModoConsulta();
            OcultarControles();
            CargarTipoProductoConsulta();
            txtCategoriaFiltro.Text = string.Empty;
            MostrarListadoCategorias();
        }
        #endregion
        #region BUSCAR CATEGORIAS
        private void MostrarListadoCategorias() {
            //FILTROS
            decimal? _TipoProducto = ddlSeleccionarTipoProducto.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarTipoProducto.SelectedValue) : new Nullable<decimal>();
            string _Categoria = string.IsNullOrEmpty(txtCategoriaFiltro.Text.Trim()) ? null : txtCategoriaFiltro.Text.Trim();

            var Buscar = ObjDataInventario.Value.BuscaCategorias(
                new Nullable<decimal>(),
                _TipoProducto,
                _Categoria);
            if (Buscar.Count() < 1)
            {
                lbCantidadRegistrosVariable.Text = "0";
                rpListadoCategoria.DataSource = null;
                rpListadoCategoria.DataBind();
            }
            else {
                int CantidadRegistros = Buscar.Count;
                lbCantidadRegistrosVariable.Text = CantidadRegistros.ToString("N0");
                Paginar(ref rpListadoCategoria, Buscar, 10, ref lbCantidadPaginaVariable, ref LinkPrimeraPagina, ref LinkAnterior, ref LinkSiguiente, ref LinkUltimo);
                HandlePaging(ref dtPaginacion, ref LinkBlbPaginaActualVariable);
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                DSMarketWeb.Logic.Comunes.SacarNombreUsuario Nombre = new Logic.Comunes.SacarNombreUsuario((decimal)Session["IdUsuario"]);
                Label lbUsuarioConectado = (Label)Master.FindControl("lbUsuarioConectado");
                lbUsuarioConectado.Text = Nombre.SacarNombre();

                Label lbPantallaActual = (Label)Master.FindControl("lbNivelAccesoPantalla");
                lbPantallaActual.Text = "CATEGORIA DE PRODUCTOS";

               // divPaginacionCategorias.Visible = false;
                CargarTipoProductoConsulta();
                CargarTipoProductoManteniiento();
                ModoConsulta();
                OcultarControles();
                MostrarListadoCategorias();
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            MostrarListadoCategorias();
        }

        protected void btnRestabelcer_Click(object sender, EventArgs e)
        {
            RestabelcerPantalla();
        }




        protected void btnGuardarMantenimiento_Click(object sender, EventArgs e)
        {
            string _Accion = lbAccionTomarMantenimiento.Text;
            decimal IdCategoria = Convert.ToDecimal(lbIdRegistroSeleccionado.Text);

            if (_Accion == "INSERT") {
                ManCategoria(IdCategoria, _Accion);
                ClientScript.RegisterStartupScript(GetType(), "RegistroGuardado()", "RegistroGuardado();", true);
                RestabelcerPantalla();
            }
            else {
                string _ClaveSeguridad = string.IsNullOrEmpty(txtClaveSeguridadMantenimiento.Text.Trim()) ? null : txtClaveSeguridadMantenimiento.Text.Trim();

                var ValidarClaveSeguridad = ObjDataSeguridad.Value.BuscaClaveSeguridad(
                    new Nullable<decimal>(),
                    null,
                    DSMarketWeb.Logic.Comunes.SeguridadEncriptacion.Encriptar(_ClaveSeguridad));
                if (ValidarClaveSeguridad.Count() < 1) {
                    ClientScript.RegisterStartupScript(GetType(), "ClaveSeguridadIncorrecta()", "ClaveSeguridadIncorrecta();", true);
                }
                else {
                    ManCategoria(IdCategoria, _Accion);
                    ClientScript.RegisterStartupScript(GetType(), "RegistroModificado()", "RegistroModificado();", true);
                    RestabelcerPantalla();
                }
            }

          
        }

        protected void btnModificarMantenimiento_Click(object sender, EventArgs e)
        {
            RestabelcerPantalla();
        }

        protected void btnSeleccionarRegistrosRepeater_Click(object sender, EventArgs e)
        {
            var IdCategoriaSeleccionada = (RepeaterItem)((Button)sender).NamingContainer;
            var hfIdCategoriaSeleccionada = decimal.Parse((((HiddenField)IdCategoriaSeleccionada.FindControl("hfIdCategoria")).Value.ToString()));

            lbIdRegistroSeleccionado.Text = hfIdCategoriaSeleccionada.ToString();

            var BuscarRegistro = ObjDataInventario.Value.BuscaCategorias(
                hfIdCategoriaSeleccionada);
            lbCantidadRegistrosVariable.Text = "1";
            Paginar(ref rpListadoCategoria, BuscarRegistro, 1, ref lbCantidadPaginaVariable, ref LinkPrimeraPagina, ref LinkAnterior, ref LinkSiguiente, ref LinkUltimo);
            HandlePaging(ref dtPaginacion, ref LinkBlbPaginaActualVariable);

            foreach (var n in BuscarRegistro) {
                CargarTipoProductoConsulta();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarTipoProductoMantenimiento, n.IdTipoProducto.ToString());
                txtCategoriaMantenimiento.Text = n.Categoria;
                cbEstatusMantenimiento.Checked = (n.Estatus0.HasValue ? n.Estatus0.Value : false);
            }

            ModoMantenimiento();
           
        }

 

  

      

    

        protected void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            MostrarControles();

            txtCategoriaMantenimiento.Text = string.Empty;
            cbEstatusMantenimiento.Checked = true;
            lbAccionTomarMantenimiento.Text = "INSERT";
            lbIdRegistroSeleccionado.Text = "0";
            BloqueClaveSeguridad.Visible = false;
            txtClaveSeguridadMantenimiento.Text = ".";
            lbClaveSeguridadMantenimiento.Visible = false;
            txtClaveSeguridadMantenimiento.Visible = false;
        }

        protected void LinkPrimeraPagina_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            MostrarListadoCategorias();
        }

        protected void LinkAnterior_Click(object sender, EventArgs e)
        {
            CurrentPage += -1;
            MostrarListadoCategorias();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref LinkBlbPaginaActualVariable, ref lbCantidadPaginaVariable);
        }

        protected void dtPaginacion_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dtPaginacion_ItemCommand(object source, DataListCommandEventArgs e)
        {

            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            MostrarListadoCategorias();
        }

        protected void LinkSiguiente_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            MostrarListadoCategorias();
        }

        protected void LinkUltimo_Click(object sender, EventArgs e)
        {

            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            MostrarListadoCategorias();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref LinkBlbPaginaActualVariable, ref lbCantidadPaginaVariable);
        }

        protected void btnModificarRegistroSeleccionado_Click(object sender, EventArgs e)
        {
            MostrarControles();
            lbAccionTomarMantenimiento.Text = "UPDATE";
            BloqueClaveSeguridad.Visible = true;
            txtClaveSeguridadMantenimiento.Text = string.Empty;
            lbClaveSeguridadMantenimiento.Visible = true;
            txtClaveSeguridadMantenimiento.Visible = true;
        }

    }
}