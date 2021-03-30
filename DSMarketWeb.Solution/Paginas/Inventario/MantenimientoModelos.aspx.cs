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

        #region MOSTRAR EL LISTADO DE LOSMODELOS
        private void ListadoModelos() {
            decimal? _TipoProducto = ddlSeleccionarTipoProductoConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarTipoProductoConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _Categoria = ddlSeleccionarCategoriaConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarCategoriaConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _Marca = ddlSeleccionarMarcaConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarMarcaConsulta.SelectedValue) : new Nullable<decimal>();
            string _Modelo = string.IsNullOrEmpty(txtModeloConsulta.Text.Trim()) ? null : txtModeloConsulta.Text.Trim();

            var Buscar = ObjDataInventario.Value.BuscaModelos(
                _TipoProducto,
                _Categoria,
                _Marca,
                new Nullable<decimal>(),
                _Modelo);
            if (Buscar.Count() < 1) {
                lbCantidadRegistrosVariable.Text = "0";
                rpListadoModelos.DataSource = null;
                rpListadoModelos.DataBind();
            }
            else {
                int CantidadRegistros = Buscar.Count;
                lbCantidadRegistrosVariable.Text = CantidadRegistros.ToString("N0");
                Paginar(ref rpListadoModelos, Buscar, 10, ref lbCantidadPaginaVariable, ref LinkPrimeraPagina, ref LinkAnterior, ref LinkSiguiente, ref LinkUltimo);
                HandlePaging(ref dtPaginacion, ref LinkBlbPaginaActualVariable);
            }
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

          
            MostrarControles();
            ModoConsulta();
            ListadoModelos();
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
                DSMarketWeb.Logic.Comunes.SacarNombreUsuario Nombre = new Logic.Comunes.SacarNombreUsuario((decimal)Session["IdUsuario"]);
                Label lbUsuarioConectado = (Label)Master.FindControl("lbUsuarioConectado");
                lbUsuarioConectado.Text = Nombre.SacarNombre();

                Label lbPantallaActual = (Label)Master.FindControl("lbNivelAccesoPantalla");
                lbPantallaActual.Text = "MODLEOS DE PRODUCTOS";

                CargarTipoProductoConsulta();
                CargarCategoriasConsultas();
                CargarMarcasConsulta();

                CargarTipoProductoMantenimiento();
                CargarCategoriasMantenimiento();
                CargarMarcasMantenimiento();

                MostrarControles();

                cbEstatusMantenimiento.Checked = true;
                ListadoModelos();
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
            ListadoModelos();
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
            Paginar(ref rpListadoModelos, BuscarregistroSeleccionado, 1, ref lbCantidadPaginaVariable, ref LinkPrimeraPagina, ref LinkAnterior, ref LinkSiguiente, ref LinkUltimo);
            HandlePaging(ref dtPaginacion, ref LinkBlbPaginaActualVariable);

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
    
            ModoMantenimiento();
        }

        protected void LinkPrimeraPagina_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            ListadoModelos();

        }

        protected void LinkAnterior_Click(object sender, EventArgs e)
        {
            CurrentPage += -1;
            ListadoModelos();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref LinkBlbPaginaActualVariable, ref lbCantidadPaginaVariable);
        }

        protected void dtPaginacion_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dtPaginacion_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            ListadoModelos();
        }

        protected void LinkSiguiente_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            ListadoModelos();
        }

        protected void LinkUltimo_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            ListadoModelos();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref LinkBlbPaginaActualVariable, ref lbCantidadPaginaVariable);
        }

        protected void btnVolverAtras_Click(object sender, EventArgs e)
        {
            RestablecerPantalla();
        }
    }
}