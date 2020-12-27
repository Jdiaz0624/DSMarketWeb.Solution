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
        Lazy<DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario> ObjDataInventario = new Lazy<Logic.Logica.LogicaInventario.LogicaInventario>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion> ObjDataConfiguracion = new Lazy<Logic.Logica.LogicaConfiguracion.LogicaConfiguracion>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaSeguridad.LogicaSeguridad> ObjDataSeguridad = new Lazy<Logic.Logica.LogicaSeguridad.LogicaSeguridad>();


        readonly PagedDataSource pagedDataSource = new PagedDataSource();
        int _PrimeraPagina, _UltimaPagina;
        private int _TamanioPagina = 10;
        /// <summary>
        /// Ete metodo es para calcular el numero de pagina        /// </summary>
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

        #region MOSTRAR LAS LISTAS DESPLEGABLES
        private void CargarTipoProductoConsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoProductoConsulta, ObjDataConfiguracion.Value.BuscaListas("TIPOPRODUCTO", null, null), true);
        }
        private void CargarCategoriasConsultas() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCategoriaConsulta, ObjDataConfiguracion.Value.BuscaListas("CATEGORIAS", ddlSeleccionarTipoProductoConsulta.SelectedValue.ToString(), null), true);
        }
        private void CargarMarcasConsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarMarcaConsulta, ObjDataConfiguracion.Value.BuscaListas("MARCAS", ddlSeleccionarTipoProductoConsulta.SelectedValue, ddlSeleccionarCategoriaConsulta.SelectedValue), true);
        }

        private void CargarTipoProductoMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoProductoMantenimiento, ObjDataConfiguracion.Value.BuscaListas("TIPOPRODUCTO", null, null));
        }
        private void CargarCategoriasMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCategoriaMantenimiento, ObjDataConfiguracion.Value.BuscaListas("CATEGORIAS", ddlSeleccionarTipoProductoMantenimiento.SelectedValue, null));
        }
        private void CargarMarcasMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarMarcasMantenimiento, ObjDataConfiguracion.Value.BuscaListas("MARCAS", ddlSeleccionarTipoProductoMantenimiento.SelectedValue, ddlSeleccionarCategoriaMantenimiento.SelectedValue));
        }
        #endregion


        #region UTILIDADES DE PANTALLA
        private void ModoConsulta() {
            btnConsultarRegistros.Enabled = true;
            btnCrearNuevoRegistro.Enabled = true;
            btnMoificarRegistro.Enabled = false;
            btnRestablecer.Enabled = true;
        }
        private void ModoMantenimiento() {
            btnConsultarRegistros.Enabled = false;
            btnCrearNuevoRegistro.Enabled = false;
            btnMoificarRegistro.Enabled = true;
            btnRestablecer.Enabled = false;
        }
        /// <summary>
        /// Este metodo es para Mostrar los controles de busqueda y ocultar los d mantenimiento
        /// </summary>
        private void OcultarControles() {
            idBloqueConsulta.Visible = false;
            idBloqueMantenimiento.Visible = true;
        }
        /// <summary>
        /// Este metodo es para ocultar los controles de busqueda y mostrar los de mantenimiento
        /// </summary>
        private void MostrarControles() {
            idBloqueConsulta.Visible = true;
            idBloqueMantenimiento.Visible = false;
        }

        private void RestablecerPantalla() {
            CargarTipoProductoConsulta();
            CargarCategoriasConsultas();
            CargarMarcasConsulta();
            txtModeloConsulta.Text = string.Empty;

            CargarTipoProductoMantenimiento();
            CargarCategoriasMantenimiento();
            CargarMarcasMantenimiento();
            txtModeloMantenimiento.Text = string.Empty;
            cbEstatusMantenimiento.Checked = true;
            lbClaveSeguridad.Visible = false;
            txtClaveSeguridad.Visible = false;

            MstarrListadoModelos();
            MostrarControles();
            ModoConsulta();
        }
        #endregion

        #region MOSTRAR EL LISTADO DE LOS MODELOS
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
            lbPaginaActualVariable.Text = (NumeroPagina + 1).ToString();
            // Now creating page number based on above first and last page index
            for (var i = _PrimeraPagina; i < _UltimaPagina; i++)
            {
                var dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }

            //data list
            dtPaginacionModelos.DataSource = dt;
            dtPaginacionModelos.DataBind();
        }
        private void Paginar(ref Repeater RptGrid, IEnumerable<object> Listado, int _NumeroRegistros)
        {
            pagedDataSource.DataSource = Listado;
            pagedDataSource.AllowPaging = true;

            ViewState["TotalPages"] = pagedDataSource.PageCount;
            // lbNumeroVariable.Text = "1";
            lbCantidadPaginasVariable.Text = pagedDataSource.PageCount.ToString();

            //MOSTRAMOS LA CANTIDAD DE PAGINAS A MOSTRAR O NUMERO DE REGISTROS
            pagedDataSource.PageSize = (_NumeroRegistros == 0 ? _TamanioPagina : _NumeroRegistros);
            pagedDataSource.CurrentPageIndex = CurrentPage;

            //HABILITAMOS LOS BOTONES DE LA PAGINACION
            LinkPrimeraPagina.Enabled = !pagedDataSource.IsFirstPage;
            LinkAnterior.Enabled = !pagedDataSource.IsFirstPage;
            LinkSiguiente.Enabled = !pagedDataSource.IsLastPage;
            LinkUltimo.Enabled = !pagedDataSource.IsLastPage;

            rpListadoModelos.DataSource = pagedDataSource;
            rpListadoModelos.DataBind();


            divPaginacionModelos.Visible = true;
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
                    LinkPrimeraPagina.Text = "1";

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
                    lbPaginaActualVariable.Text = lbCantidadPaginasVariable.Text;
                    break;


            }

        }
        private void MstarrListadoModelos() {
            decimal? _IdTipoProducto = ddlSeleccionarTipoProductoConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarTipoProductoConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _IdCategoria = ddlSeleccionarCategoriaConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarCategoriaConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _IdMarca = ddlSeleccionarMarcaConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarMarcaConsulta.SelectedValue) : new Nullable<decimal>();
            string _Modelo = string.IsNullOrEmpty(txtModeloConsulta.Text.Trim()) ? null : txtModeloConsulta.Text.Trim();

            var BuscarListadoModelos = ObjDataInventario.Value.BuscaModelos(
                _IdTipoProducto,
                _IdCategoria,
                _IdMarca,
                new Nullable<decimal>(),
                _Modelo);
            if (BuscarListadoModelos.Count() < 1) {
                lbCantidadRegistrosVariable.Text = "0";
            }
            else {
                int CantidadRegistros = 0;
                foreach (var n in BuscarListadoModelos) {
                    CantidadRegistros = Convert.ToInt32(n.CantidadRegistros);
                }
                lbCantidadRegistrosVariable.Text = CantidadRegistros.ToString("N0");
                Paginar(ref rpListadoModelos, BuscarListadoModelos, 10);
                HandlePaging();
                
            }
        }
        #endregion

        #region MAMTENIMIENTO DE MODELOS
        private void MAnModelos(decimal IdModelo, string Accion) {
            DSMarketWeb.Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionModelos Procesar = new Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionModelos(
                Convert.ToDecimal(ddlSeleccionarMarcasMantenimiento.SelectedValue),
                IdModelo,
                txtModeloMantenimiento.Text,
                cbEstatusMantenimiento.Checked,
                (decimal)Session["IdUsuario"],
                Convert.ToDecimal(ddlSeleccionarTipoProductoMantenimiento.SelectedValue),
                Convert.ToDecimal(ddlSeleccionarCategoriaMantenimiento.SelectedValue),
                Accion);
            Procesar.ProcesarDatosnModelos();
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                CargarTipoProductoConsulta();
                CargarCategoriasConsultas();
                CargarMarcasConsulta();

                CargarTipoProductoMantenimiento();
                CargarCategoriasMantenimiento();
                CargarMarcasMantenimiento();

                MostrarControles();
                divPaginacionModelos.Visible = false;
                cbEstatusMantenimiento.Checked = true;
            }
        }

        protected void ddlSeleccionarTipoProductoConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCategoriasConsultas();
            CargarMarcasConsulta();
        }

        protected void ddlSeleccionarCategoriaConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarMarcasConsulta();
        }

        protected void btnConsultarRegistros_Click(object sender, EventArgs e)
        {
            MstarrListadoModelos();
        }

        protected void btnRestablecer_Click(object sender, EventArgs e)
        {
            RestablecerPantalla();
        }

        protected void ddlSeleccionarCategoriaMantenimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarMarcasMantenimiento();
        }

        protected void ddlSeleccionarTipoProductoMantenimiento_TextChanged(object sender, EventArgs e)
        {
            CargarCategoriasMantenimiento();
            CargarMarcasConsulta();
        }

        protected void btnGuardarMantenimiento_Click(object sender, EventArgs e)
        {
            MAnModelos(0, "INSERT");
            ClientScript.RegisterStartupScript(GetType(), "RegistroGuardado()", "RegistroGuardado();", true);
            RestablecerPantalla();
        }

        protected void btnModificarMantenimiento_Click(object sender, EventArgs e)
        {
            string _ClaveSeguridad = string.IsNullOrEmpty(txtClaveSeguridad.Text.Trim()) ? null : txtClaveSeguridad.Text.Trim();

            var ValidarClaveseguridad = ObjDataSeguridad.Value.BuscaClaveSeguridad(
                new Nullable<decimal>(),
                null,
                DSMarketWeb.Logic.Comunes.SeguridadEncriptacion.Encriptar(_ClaveSeguridad));
            if (ValidarClaveseguridad.Count() < 1) {
                ClientScript.RegisterStartupScript(GetType(), "ClaveSeguridadNoValida()", "ClaveSeguridadNoValida();", true);
            }
            else {
                MAnModelos(Convert.ToDecimal(lbIdRegistroSeleccionado.Text), "UPDATE");
                ClientScript.RegisterStartupScript(GetType(), "RegistroModificado()", "RegistroModificado();", true);
                RestablecerPantalla();
            }
        }

        protected void LinkPrimeraPagina_Click(object sender, EventArgs e)
        {

            CurrentPage = 0;
            //   lbNumeroVariable.Text = CurrentPage.ToString();
            MstarrListadoModelos();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PrimeraPagina);
        }

        protected void LinkAnterior_Click(object sender, EventArgs e)
        {

            CurrentPage += -1;
            //  lbNumeroVariable.Text = CurrentPage.ToString();
            MstarrListadoModelos();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior);
        }

        protected void LinkSiguiente_Click(object sender, EventArgs e)
        {

            CurrentPage += 1;
            // lbNumeroVariable.Text = CurrentPage.ToString();

            MstarrListadoModelos();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.SiguientePagina);
        }

        protected void LinkUltimo_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            //  lbNumeroVariable.Text = CurrentPage.ToString();
            MstarrListadoModelos();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.UltimaPagina);
        }

        protected void dtPaginacionModelos_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            MstarrListadoModelos();
        }

        protected void dtPaginacionModelos_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void btnCrearNuevoRegistro_Click(object sender, EventArgs e)
        {
            OcultarControles();
            cbEstatusMantenimiento.Checked = true;
            lbClaveSeguridad.Visible = false;
            txtClaveSeguridad.Visible = false;
            btnGuardarMantenimiento.Visible = true;
            btnModificarMantenimiento.Visible = false;
        }

        protected void btnMoificarRegistro_Click(object sender, EventArgs e)
        {

            OcultarControles();
            lbClaveSeguridad.Visible = true;
            txtClaveSeguridad.Visible = true;
            txtClaveSeguridad.Text = string.Empty;
            btnGuardarMantenimiento.Visible = false;
            btnModificarMantenimiento.Visible = true;
        }

        protected void btnSeleccionarregistroRepeater_Click(object sender, EventArgs e)
        {
            //SELECCIONAR REGISTRO
            var ItemSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfIdModeloSeleccionado = ((HiddenField)ItemSeleccionado.FindControl("hfIdModeloSeleccionado")).Value.ToString();
            lbIdRegistroSeleccionado.Text = hfIdModeloSeleccionado.ToString();

            var BuscarregistroSeleccionado = ObjDataInventario.Value.BuscaModelos(
                null,
                null,
                null,
                Convert.ToDecimal(hfIdModeloSeleccionado),
                null);
            lbCantidadRegistrosVariable.Text = "1";
            foreach (var n in BuscarregistroSeleccionado) {
                CargarTipoProductoMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarTipoProductoMantenimiento, n.IdTipoProducto.ToString());

                CargarCategoriasMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarCategoriaMantenimiento, n.IdCategoria.ToString());

                CargarMarcasMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarMarcasMantenimiento, n.IdMarca.ToString());

                txtModeloMantenimiento.Text = n.Modelo;
                cbEstatusMantenimiento.Checked = (n.Estatus0.HasValue ? n.Estatus0.Value : false);
            }
            Paginar(ref rpListadoModelos, BuscarregistroSeleccionado, 1);
            HandlePaging();
            ModoMantenimiento();
        }

        protected void btnVolverAtras_Click(object sender, EventArgs e)
        {
            RestablecerPantalla();
        }
    }
}