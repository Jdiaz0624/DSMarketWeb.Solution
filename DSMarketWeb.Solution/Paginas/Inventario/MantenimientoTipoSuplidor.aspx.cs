using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace DSMarketWeb.Solution.Paginas.Inventario
{
    public partial class MantenimientoTipoSuplidor : System.Web.UI.Page
    {
        Lazy<DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario> ObjDataInventario = new Lazy<Logic.Logica.LogicaInventario.LogicaInventario>();
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


        #region MOSTRAR LISTADO DE TIPO DE SUPLIDORES
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
            lbPaginaActualVariavle.Text = (NumeroPagina + 1).ToString();
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
            lbCantidadPaginaVAriable.Text = pagedDataSource.PageCount.ToString();

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


            divPaginacionTipoSuplidor.Visible = true;
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
                    PaginaActual = Convert.ToInt32(lbPaginaActualVariavle.Text);
                    PaginaActual++;
                    lbPaginaActualVariavle.Text = PaginaActual.ToString();
                    break;

                case 3:
                    //PAGINA ANTERIOR
                    PaginaActual = Convert.ToInt32(lbPaginaActualVariavle.Text);
                    if (PaginaActual > 1)
                    {
                        PaginaActual--;
                        lbPaginaActualVariavle.Text = PaginaActual.ToString();
                    }
                    break;

                case 4:
                    //ULTIMA PAGINA
                    lbPaginaActualVariavle.Text = lbCantidadPaginaVAriable.Text;
                    break;


            }

        }

        private void BuscarListado() {
            string _TipoSuplidor = string.IsNullOrEmpty(txtTipoSuplidorConsulta.Text.Trim()) ? null : txtTipoSuplidorConsulta.Text.Trim();

            var Buscar = ObjDataInventario.Value.BuscaTipoSuplidores(
                new Nullable<decimal>(),
                _TipoSuplidor);
            if (Buscar.Count() < 1) {
                lbCantidadRegistrsVariable.Text = "0";
            }
            else {
                int CantidadRegistros = 0;
                foreach (var n in Buscar) {
                    CantidadRegistros = Convert.ToInt32(n.CantidadRegistros);
                }
                lbCantidadRegistrsVariable.Text = CantidadRegistros.ToString("N0");
                Paginar(ref rpListadoTipoSuplidores, Buscar, 10);
                HandlePaging();
                divPaginacionTipoSuplidor.Visible = true;
            
            }
        }
        #endregion

        #region UTILIDADES DE LA PANTALLA
        private void ModoConsulta() {
            btnConsultar.Enabled = true;
            btnNuevoRegistro.Enabled = true;
            btnModificarRegistro.Enabled = false;
            btnRestablecerPantalla.Enabled = true;
        }
        private void ModoMantenimiento() {
            btnConsultar.Enabled = false;
            btnNuevoRegistro.Enabled = false;
            btnModificarRegistro.Enabled = true;
            btnRestablecerPantalla.Enabled = false;
        }
        private void OcultarCOntrolesConsulta() {
            DivBloqueConsulta.Visible = false;
            DivBloqueMantenimiento.Visible = true;
        }
        private void MostrarControlesConsulta() {
            DivBloqueConsulta.Visible = true;
            DivBloqueMantenimiento.Visible = false;
        }
        private void RestablecerPantalla() {
            ModoConsulta();
            txtTipoSuplidorConsulta.Text = string.Empty;
            MostrarControlesConsulta();
            txtTipoSuplidorMantenimiento.Text = string.Empty;
            txtClaveseguridad.Text = string.Empty;
            lbClaveSegruidd.Visible = false;
            txtClaveseguridad.Visible = false;
            BuscarListado();
        }
        #endregion

        #region MANTENIMIENTO DE TIPO DE SUPLIDORES
        private void MANSupldiores(decimal IdTipoSuplidor, string Accion) {
            DSMarketWeb.Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionTipoSuplidores Procesar = new Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionTipoSuplidores(
                IdTipoSuplidor,
                txtTipoSuplidorMantenimiento.Text,
                cbEstatus.Checked,
                (decimal)Session["IdUsuario"],
                Accion);
            Procesar.ProcesarDatosTipoSuplidor();
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                ModoConsulta();
                MostrarControlesConsulta();
                divPaginacionTipoSuplidor.Visible = false;
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            BuscarListado();
        }

        protected void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            OcultarCOntrolesConsulta();
            lbClaveSegruidd.Visible = false;
            txtClaveseguridad.Visible = false;
            cbEstatus.Checked = true;
            btnGuardar.Visible = true;
            btnModificar.Visible = false;
        }

        protected void btnModificarRegistro_Click(object sender, EventArgs e)
        {
            OcultarCOntrolesConsulta();
            lbClaveSegruidd.Visible = true;
            txtClaveseguridad.Visible = true;
            txtClaveseguridad.Text = string.Empty;
            btnGuardar.Visible = false;
            btnModificar.Visible = true;
        }

        protected void btnRestablecerPantalla_Click(object sender, EventArgs e)
        {
            RestablecerPantalla();
        }

        protected void btnSeleccionarRegistro_Click(object sender, EventArgs e)
        {
            var ItemSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfIdTipoSuplidor = ((HiddenField)ItemSeleccionado.FindControl("hfIdTipoSuplidor")).Value.ToString();

            lbIdRegistroSeleccionado.Text = hfIdTipoSuplidor.ToString();

            var BuscarregistroSeleccionado = ObjDataInventario.Value.BuscaTipoSuplidores(Convert.ToDecimal(hfIdTipoSuplidor));
            foreach (var n in BuscarregistroSeleccionado) {
                txtTipoSuplidorMantenimiento.Text = n.TipoSuplidor;
                cbEstatus.Checked = (n.Estatus0.HasValue ? n.Estatus0.Value : false);
            }
            Paginar(ref rpListadoTipoSuplidores, BuscarregistroSeleccionado, 1);
            HandlePaging();
            lbCantidadRegistrsVariable.Text = "1";
            ModoMantenimiento();
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

        protected void dlPaginacion_CancelCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            BuscarListado();
        }

        protected void dlPaginacion_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            MANSupldiores(0,"INSERT");
            ClientScript.RegisterStartupScript(GetType(), "RegistroGuardado()", "RegistroGuardado();", true);
            RestablecerPantalla();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            string _ClaveSeguridad = string.IsNullOrEmpty(txtClaveseguridad.Text.Trim()) ? null : txtClaveseguridad.Text.Trim();

            var VaidarClaveSeguridad = ObjDataSeguridad.Value.BuscaClaveSeguridad(
                new Nullable<decimal>(),
                null,
                DSMarketWeb.Logic.Comunes.SeguridadEncriptacion.Encriptar(_ClaveSeguridad));
            if (VaidarClaveSeguridad.Count() < 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "ClaveSeguridaNoValida()", "ClaveSeguridaNoValida();", true);
            }
            else {
                MANSupldiores(Convert.ToDecimal(lbIdRegistroSeleccionado.Text), "UPDATE");
                ClientScript.RegisterStartupScript(GetType(), "RegistroModificado()", "RegistroModificado();", true);
                RestablecerPantalla();
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            RestablecerPantalla();
        }
    }
}