using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace DSMarketWeb.Solution.Paginas.Inventario
{
    public partial class MantenimientoMarcas : System.Web.UI.Page
    {
        Lazy<DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario> ObjdataInventario = new Lazy<Logic.Logica.LogicaInventario.LogicaInventario>();
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
        #region MOSTRAR EL LISTADO DE LAS MARCAS
        private void ListadoMarcas() {
            decimal? _TipoProducto = ddlSeleccionarSeleccionarTipoProductoConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarSeleccionarTipoProductoConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _Categoria = ddlSeleccionarCategoriaConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarCategoriaConsulta.SelectedValue) : new Nullable<decimal>();
            string _Marca = string.IsNullOrEmpty(txtMarcaConsulta.Text.Trim()) ? null : txtMarcaConsulta.Text.Trim();

            var Buscarregistros = ObjdataInventario.Value.BuscaMarcas(
                new Nullable<decimal>(),
                _TipoProducto,
                _Categoria,
                _Marca);
            if (Buscarregistros.Count() < 1)
            {
                lbCantidadRegistrosVariable.Text = "0";
                rpListadoMarcas.DataSource = null;
                rpListadoMarcas.DataBind();
            }
            else {
                int CantidadRegistros = Buscarregistros.Count;
                lbCantidadRegistrosVariable.Text = CantidadRegistros.ToString("N0");
                Paginar(ref rpListadoMarcas, Buscarregistros, 10, ref lbCantidadPaginaVariable, ref LinkPrimeraPagina, ref LinkAnterior, ref LinkSiguiente, ref LinkUltimo);
                HandlePaging(ref dtPaginacion, ref LinkBlbPaginaActualVariable);
            }

        }
        #endregion

        #region MODOS DE PANTALLA
        private void ModoConsulta() {
            btnConsultarRegistros.Enabled = true;
            btnNuevoRegistro.Enabled = true;
            btnModificarRegistros.Enabled = false;
            btnRestablecerPantalla.Enabled = true;
        }
        private void ModoMantenimiento() {
            btnConsultarRegistros.Enabled = false;
            btnNuevoRegistro.Enabled = false;
            btnModificarRegistros.Enabled = true;
            btnRestablecerPantalla.Enabled = true;
        }
        private void OcultarControles() {
            DivBloqueConsulta.Visible = false;
            divBloqueMantenimiento.Visible = true;
        }
        private void MostrarControles() {
            DivBloqueConsulta.Visible = true;
            divBloqueMantenimiento.Visible = false;
        }
        private void RestablecerPantalla() {
            ModoConsulta();
            LimpiarPantallaConsulta();
            LimpiarControlesMantenimiento();
            MostrarControles();
            ListadoMarcas();
        }
        private void LimpiarPantallaConsulta() {
            CargarListaTipProductoConsulta();
            CargarListaCategoriaConsulta();
            txtMarcaConsulta.Text = string.Empty;
            ListadoMarcas();
         
        }
        private void LimpiarControlesMantenimiento() {
            CargarListaCategoriaMantenimiento();
            txtMarcaMantenimiento.Text = string.Empty;
            cbEstatus.Checked = true;
        }
        #endregion

        #region CARGAR LAS LISTAS DESPLEGABLES
        private void CargarListaTipProductoConsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarSeleccionarTipoProductoConsulta, ObjDataConfiguracion.Value.BuscaListas("TIPOPRODUCTO", null, null), true);
        }
        private void CargarListaCategoriaConsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCategoriaConsulta, ObjDataConfiguracion.Value.BuscaListas("CATEGORIAS", ddlSeleccionarSeleccionarTipoProductoConsulta.SelectedValue.ToString(), null), true);
        }
        private void CagarListaTipoProductoMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoProductoMantenimiento, ObjDataConfiguracion.Value.BuscaListas("TIPOPRODUCTO", null, null));
        }
        private void CargarListaCategoriaMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCategoriaMantenimiento, ObjDataConfiguracion.Value.BuscaListas("CATEGORIAS", ddlSeleccionarTipoProductoMantenimiento.SelectedValue.ToString(), null));
        }
        #endregion

        #region MANTENIMIENTO DE MARCAS
        private void MANMArcar(decimal IdMarca, string Accion) {
            DSMarketWeb.Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionMarcas Procesar = new Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionMarcas(
                IdMarca,
                txtMarcaMantenimiento.Text,
                cbEstatus.Checked,
                (decimal)Session["IdUsuario"],
                Convert.ToDecimal(ddlSeleccionarCategoriaMantenimiento.SelectedValue),
                Convert.ToDecimal(ddlSeleccionarTipoProductoMantenimiento.SelectedValue),
                Accion);
            Procesar.ProcedarDataMarcas();
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if(!IsPostBack) {
                DSMarketWeb.Logic.Comunes.SacarNombreUsuario Nombre = new Logic.Comunes.SacarNombreUsuario((decimal)Session["IdUsuario"]);
                Label lbUsuarioConectado = (Label)Master.FindControl("lbUsuarioConectado");
                lbUsuarioConectado.Text = Nombre.SacarNombre();
                Label lbPantallaActual = (Label)Master.FindControl("lbNivelAccesoPantalla");
                lbPantallaActual.Text = "MARCAS DE PRODUCTOS";
                ModoConsulta();
                MostrarControles();
                cbEstatus.Checked = true;
                lbClaveSeguridad.Visible = false;
                lbClaveSeguridad.Visible = true;
                CargarListaTipProductoConsulta();
                CargarListaCategoriaConsulta();
                CagarListaTipoProductoMantenimiento();
                CargarListaCategoriaMantenimiento();
                ListadoMarcas();
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            ListadoMarcas();
        }

        protected void btnRestablecerPantalla_Click(object sender, EventArgs e)
        {
            RestablecerPantalla();
        }


        protected void ddlSeleccionarTipoProductoMantenimiento_TextChanged(object sender, EventArgs e)
        {
            CargarListaCategoriaMantenimiento();
        }

        protected void ddlSeleccionarTipoProductoConsulta_TextChanged(object sender, EventArgs e)
        {
            CargarListaCategoriaConsulta();
        }

        protected void btnGuardarMantenimiento_Click(object sender, EventArgs e)
        {

        }

        protected void btnModificarMantenimiento_Click(object sender, EventArgs e)
        {

        }

        protected void btnConsultarRegistros_Click(object sender, EventArgs e)
        {
            ListadoMarcas();
        }

        protected void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            OcultarControles();
            lbClaveSeguridad.Visible = false;
            txtClaveSeguridad.Visible = false;
            cbEstatus.Checked = true;
            btnGuardarRegistro.Visible = true;
            btnModificarRegstro.Visible = false;
        }

        protected void btnModificarRegistros_Click(object sender, EventArgs e)
        {
            OcultarControles();
            lbClaveSeguridad.Visible = true;
            txtClaveSeguridad.Visible = true;
            btnGuardarRegistro.Visible = false;
            btnModificarRegstro.Visible = true;
        }

        protected void btnRestablecerPantalla_Click1(object sender, EventArgs e)
        {
            RestablecerPantalla();
        }

       

       

      

     

     

        protected void btnGuardarRegistro_Click(object sender, EventArgs e)
        {
            MANMArcar(0, "INSERT");
            ClientScript.RegisterStartupScript(GetType(), "GuardarRegistro()", "GuardarRegistro();", true);
            RestablecerPantalla();
        }

        protected void btnModificarRegstro_Click(object sender, EventArgs e)
        {
            string _ClaveSeguridad = string.IsNullOrEmpty(txtClaveSeguridad.Text.Trim()) ? null : txtClaveSeguridad.Text.Trim();
            var ValidarClaveSeguridad = ObjDataSeguridad.Value.BuscaClaveSeguridad(
                new Nullable<decimal>(),
                null,
                DSMarketWeb.Logic.Comunes.SeguridadEncriptacion.Encriptar(_ClaveSeguridad));
            if (ValidarClaveSeguridad.Count() < 1) {
                ClientScript.RegisterStartupScript(GetType(), "ClaveSeguridadErrornea()", "ClaveSeguridadErrornea();", true);
            }
            else {
                MANMArcar(Convert.ToDecimal(lbIdRegistroSeleccionado.Text), "UPDATE");
                ClientScript.RegisterStartupScript(GetType(), "ModificarRegistro()", "ModificarRegistro();", true);
                RestablecerPantalla();
            }
        }

        protected void ddlSeleccionarSeleccionarTipoProductoConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarListaCategoriaConsulta();
        }

        protected void ddlSeleccionarTipoProductoMantenimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarListaCategoriaMantenimiento();
        }

        protected void btnSeleccionarRegistrosRepeater_Click(object sender, EventArgs e)
        {
            var ItemSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfIdMarca = ((HiddenField)ItemSeleccionado.FindControl("hfIdMarca")).Value.ToString();
            lbIdRegistroSeleccionado.Text = hfIdMarca.ToString();

            var BuscarRegistoSeleccionado = ObjdataInventario.Value.BuscaMarcas(
                Convert.ToDecimal(hfIdMarca));
            Paginar(ref rpListadoMarcas, BuscarRegistoSeleccionado, 1, ref lbCantidadPaginaVariable, ref LinkPrimeraPagina, ref LinkAnterior, ref LinkSiguiente, ref LinkUltimo);
            HandlePaging(ref dtPaginacion, ref LinkBlbPaginaActualVariable);
            lbCantidadRegistrosVariable.Text = "1";
            foreach (var n in BuscarRegistoSeleccionado) {
                lbCantidadRegistrosVariable.Text = n.CantidadRegistros.ToString();
                CagarListaTipoProductoMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarTipoProductoMantenimiento, n.IdTipoProducto.ToString());
                CargarListaCategoriaMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarCategoriaMantenimiento, n.IdCateoria.ToString());
                txtMarcaMantenimiento.Text = n.Marca;
                cbEstatus.Checked = (n.Estatus0.HasValue ? n.Estatus0.Value : false);
            }
        
            ModoMantenimiento();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            RestablecerPantalla();
        }

        protected void LinkPrimeraPagina_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            ListadoMarcas();
        }

        protected void LinkAnterior_Click(object sender, EventArgs e)
        {
            CurrentPage += -1;
            ListadoMarcas();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref LinkBlbPaginaActualVariable, ref lbCantidadPaginaVariable);
        }

        protected void dtPaginacion_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dtPaginacion_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            ListadoMarcas();
        }

        protected void LinkSiguiente_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            ListadoMarcas();
        }

        protected void LinkUltimo_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            ListadoMarcas();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref LinkBlbPaginaActualVariable, ref lbCantidadPaginaVariable);
        }
    }
}