using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DSMarketWeb.Solution.Paginas.Inventario
{
    public partial class MantenimientoUnidadMedida : System.Web.UI.Page
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


        #region MOSTRAR EL LISTADO DE LAS UNIDADES DE MEDIDA
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
            lbPaginaActualPaginacion.Text = (NumeroPagina + 1).ToString();
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
            lbCantidadPaginasPaginacionVariable.Text = pagedDataSource.PageCount.ToString();

            //MOSTRAMOS LA CANTIDAD DE PAGINAS A MOSTRAR O NUMERO DE REGISTROS
            pagedDataSource.PageSize = (_NumeroRegistros == 0 ? _TamanioPagina : _NumeroRegistros);
            pagedDataSource.CurrentPageIndex = CurrentPage;

            //HABILITAMOS LOS BOTONES DE LA PAGINACION
            LinkPrimeraPaginaPaginacion.Enabled = !pagedDataSource.IsFirstPage;
            LinkPaginaAnterior.Enabled = !pagedDataSource.IsFirstPage;
            LinkPaginaSiguiente.Enabled = !pagedDataSource.IsLastPage;
            LinkUltipaPagina.Enabled = !pagedDataSource.IsLastPage;

            rpListadoUnidadMedida.DataSource = pagedDataSource;
            rpListadoUnidadMedida.DataBind();


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
                    lbPaginaActualPaginacion.Text = "1";

                    break;

                case 2:
                    //SEGUNDA PAGINA
                    PaginaActual = Convert.ToInt32(lbPaginaActualPaginacion.Text);
                    PaginaActual++;
                    lbPaginaActualPaginacion.Text = PaginaActual.ToString();
                    break;

                case 3:
                    //PAGINA ANTERIOR
                    PaginaActual = Convert.ToInt32(lbPaginaActualPaginacion.Text);
                    if (PaginaActual > 1)
                    {
                        PaginaActual--;
                        lbPaginaActualPaginacion.Text = PaginaActual.ToString();
                    }
                    break;

                case 4:
                    //ULTIMA PAGINA
                    lbPaginaActualPaginacion.Text = lbCantidadPaginasPaginacionVariable.Text;
                    break;


            }

        }
        private void BuscarListado() {
            string _UnidadMedida = string.IsNullOrEmpty(txtUnidadMedidaConsulta.Text.Trim()) ? null : txtUnidadMedidaConsulta.Text.Trim();

            var Buscar = ObjDataInventario.Value.BuscaUnidadMedida(
                new Nullable<decimal>(),
                _UnidadMedida);
            Paginar(ref rpListadoUnidadMedida, Buscar, 10);
            HandlePaging();
        }
        #endregion
        #region MODOS DE PANTALLA
        private void ModoConsulta() {
            btnConsultar.Enabled = true;
            btnNuevoRegistro.Enabled = true;
            btnModificarRegistro.Enabled = false;
            btnRestabelcer.Enabled = true;
        }
        private void ModoMantenimiento() {
            btnConsultar.Enabled = false;
            btnNuevoRegistro.Enabled = false;
            btnModificarRegistro.Enabled = true;
            btnRestabelcer.Enabled = true;
        }
        private void OcultarControles() {
            DivBloqueConsulta.Visible = true;
            DivBloqueMantenimiento.Visible = false;
        }
        private void MostrarControles() {
            DivBloqueConsulta.Visible = false;
            DivBloqueMantenimiento.Visible = true;
        }
        #endregion
        #region RESTABLECER LA PANTALLA
        private void RestablecerPantalla() {
            ModoConsulta();
            OcultarControles();
            txtClaveSeguridad.Text = string.Empty;
            lbClaveSeguridad.Visible = false;
            txtClaveSeguridad.Visible = false;
            txtUnidadMedidaConsulta.Text = string.Empty;
            txtUnidadmedidaMantenimiento.Text = string.Empty;
            BuscarListado();
        }
        #endregion
        #region MANTENIMIENTO DE UNIDAD DE MEDIDA
        private void MANUnidadMedida(decimal IdUnidadMedida, string Accion) {
            DSMarketWeb.Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionUnidadMedida Procesar = new Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionUnidadMedida(
                IdUnidadMedida,
                txtUnidadmedidaMantenimiento.Text,
                cbEstatus.Checked,
                (decimal)Session["IdUsuario"],
                Accion);
            Procesar.ProcesarDataUnidadMedida();
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
                lbPantallaActual.Text = "UNIDAD DE MEDIDA";
                divPaginacionUnidadMedida.Visible = false;
                ModoConsulta();
                OcultarControles();
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            BuscarListado();
        }

        protected void btnRestabelcer_Click(object sender, EventArgs e)
        {
            RestablecerPantalla();
        }



        protected void btnSeleccioanrregistros_Click(object sender, EventArgs e)
        {
            var ItemSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfIdUnidadMedida = ((HiddenField)ItemSeleccionado.FindControl("hfIdUnidadMedida")).Value.ToString();

            lbIdRegistroSeleccionadoMantenimiento.Text = hfIdUnidadMedida.ToString();

            var BuscarRegistroSeleccionado = ObjDataInventario.Value.BuscaUnidadMedida(
                Convert.ToDecimal(hfIdUnidadMedida));
            Paginar(ref rpListadoUnidadMedida, BuscarRegistroSeleccionado, 1);
            HandlePaging();
            foreach (var n in BuscarRegistroSeleccionado) {
                lbIdRegistroSeleccionadoMantenimiento.Text = n.IdUnidadMedida.ToString();
                txtUnidadmedidaMantenimiento.Text = n.UnidadMedida;
                cbEstatus.Checked = (n.Estatus0.HasValue ? n.Estatus0.Value : false);
            }
            ModoMantenimiento();
        }

        protected void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            ModoMantenimiento();
            MostrarControles();
            btmGaurdar.Visible = true;
            btnModificar.Visible = false;
            lbClaveSeguridad.Visible = false;
            txtClaveSeguridad.Visible = false;
            cbEstatus.Checked = true;
        }

        protected void btnModificarRegistro_Click(object sender, EventArgs e)
        {
            ModoMantenimiento();
            MostrarControles();
            btmGaurdar.Visible = false;
            btnModificar.Visible = true;
            lbClaveSeguridad.Visible = true;
            txtClaveSeguridad.Visible = true;
            txtClaveSeguridad.Text = string.Empty;
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

        protected void btmGaurdar_Click(object sender, EventArgs e)
        {
            MANUnidadMedida(0, "INSERT");
            ClientScript.RegisterStartupScript(GetType(), "RegistroGuardadoConExito()", "RegistroGuardadoConExito();", true);
            RestablecerPantalla();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            string _ClaveSeguridad = string.IsNullOrEmpty(txtClaveSeguridad.Text.Trim()) ? null : txtClaveSeguridad.Text.Trim();

            var Validar = ObjDataSeguridad.Value.BuscaClaveSeguridad(
                new Nullable<decimal>(),
                null,
                DSMarketWeb.Logic.Comunes.SeguridadEncriptacion.Encriptar(_ClaveSeguridad));
            if (Validar.Count() < 1) {
                ClientScript.RegisterStartupScript(GetType(), "ClaveSeguridadNoValida()", "ClaveSeguridadNoValida();", true);
            }
            else {
                MANUnidadMedida(Convert.ToDecimal(lbIdRegistroSeleccionadoMantenimiento.Text), "UPDATE");
                ClientScript.RegisterStartupScript(GetType(), "RegistroModificadoConExito()", "RegistroModificadoConExito();", true);
                RestablecerPantalla();
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            RestablecerPantalla();
        }

        protected void dlPaginacion_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }
    }
}