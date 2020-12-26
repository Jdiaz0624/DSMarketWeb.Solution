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
        }
        private void LimpiarPantallaConsulta() {
            CargarListaTipProductoConsulta();
            CargarListaCategoriaConsulta();
            txtMarcaConsulta.Text = string.Empty;
            BuscarListado();
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
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCategoriaConsulta, ObjDataConfiguracion.Value.BuscaListas("CATEGORIAS", null, null), true);
        }
        private void CagarListaTipoProductoMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoProductoMantenimiento, ObjDataConfiguracion.Value.BuscaListas("TIPOPRODUCTO", null, null));
        }
        private void CargarListaCategoriaMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCategoriaMantenimiento, ObjDataConfiguracion.Value.BuscaListas("CATEGORIAS", null, null));
        }
        #endregion

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

        #region MOSTRAR EL LISTADO DE LAS MARCAS
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
            dlPaginacion.DataSource = dt;
            dlPaginacion.DataBind();
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

            rpListadoMarcas.DataSource = pagedDataSource;
            rpListadoMarcas.DataBind();


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
                    LinkPrimeraPaginaPaginacion.Text = "1";

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
        private void BuscarListado() {
            decimal? IdTipoProducto = ddlSeleccionarSeleccionarTipoProductoConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarSeleccionarTipoProductoConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? IdCategoria = ddlSeleccionarCategoriaConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarCategoriaConsulta.SelectedValue) : new Nullable<decimal>();
            string _Marca = string.IsNullOrEmpty(txtMarcaConsulta.Text.Trim()) ? null : txtMarcaConsulta.Text.Trim();

            var Buscar = ObjdataInventario.Value.BuscaMarcas(
                new Nullable<decimal>(),
                IdTipoProducto,
                IdCategoria,
                _Marca);
            if (Buscar.Count() < 1) {
                lbCantidadRegistrosVariable.Text = "0";
            }
            else {
                int CantidadRegistros = 0;
                foreach (var n in Buscar) {
                    CantidadRegistros = Convert.ToInt32(n.CantidadRegistros);
                }
                lbCantidadRegistrosVariable.Text = CantidadRegistros.ToString("N0");
                Paginar(ref rpListadoMarcas, Buscar, 10);
                HandlePaging();
                divPaginacionUnidadMedida.Visible = true;
            }
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
                divPaginacionUnidadMedida.Visible = false;
                ModoConsulta();
                MostrarControles();
                cbEstatus.Checked = true;
                lbClaveSeguridad.Visible = false;
                lbClaveSeguridad.Visible = true;
                CargarListaTipProductoConsulta();
                CargarListaCategoriaConsulta();
                CagarListaTipoProductoMantenimiento();
                CargarListaCategoriaMantenimiento();
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
           
        }

        protected void btnRestablecerPantalla_Click(object sender, EventArgs e)
        {
           
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
            BuscarListado();
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

        protected void LinkPrimeraPaginaPaginacion_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            BuscarListado();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PrimeraPagina);
        }

        protected void LinkPaginaAnterior_Click(object sender, EventArgs e)
        {
            CurrentPage += -1;
            BuscarListado();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior);
        }

        protected void dlPaginacion_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dlPaginacion_CancelCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            BuscarListado();
        }

        protected void LinkPaginaSiguiente_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            BuscarListado();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.SiguientePagina);
        }

        protected void LinkUltipaPagina_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            BuscarListado();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.UltimaPagina);
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
            foreach (var n in BuscarRegistoSeleccionado) {
                lbCantidadRegistrosVariable.Text = n.CantidadRegistros.ToString();
                CagarListaTipoProductoMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarTipoProductoMantenimiento, n.IdTipoProducto.ToString());
                CargarListaCategoriaMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarCategoriaMantenimiento, n.IdCateoria.ToString());
                txtMarcaMantenimiento.Text = n.Marca;
                cbEstatus.Checked = (n.Estatus0.HasValue ? n.Estatus0.Value : false);
            }
            Paginar(ref rpListadoMarcas, BuscarRegistoSeleccionado, 1);
            HandlePaging();
            ModoMantenimiento();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            RestablecerPantalla();
        }
    }
}