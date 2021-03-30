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
            ListadoUidadMedida();
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
        #region MOSTRAR EL LISTADO DE UNIDADES DE MEDIDAS
        private void ListadoUidadMedida() {
            string _Unidadmedida = string.IsNullOrEmpty(txtUnidadMedidaConsulta.Text.Trim()) ? null : txtUnidadMedidaConsulta.Text.Trim();

            var Buscar = ObjDataInventario.Value.BuscaUnidadMedida(
                new Nullable<int>(),
                _Unidadmedida);
            if (Buscar.Count() < 1)
            {
                lbCantidadRegistrosVariable.Text = "0";
                rpListadoUnidadMedida.DataSource = null;
                rpListadoUnidadMedida.DataBind();
            }
            else {
                int CantidadRegistros = Buscar.Count;
                lbCantidadRegistrosVariable.Text = CantidadRegistros.ToString("N0");
                Paginar(ref rpListadoUnidadMedida, Buscar, 10, ref lbCantidadPaginaVariable, ref LinkPrimeraPagina, ref LinkAnterior, ref LinkSiguiente, ref LinkUltimo);
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
                lbPantallaActual.Text = "UNIDAD DE MEDIDA";
                //divPaginacionUnidadMedida.Visible = false;
                ModoConsulta();
                OcultarControles();
                ListadoUidadMedida();
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            ListadoUidadMedida();
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
            lbCantidadRegistrosVariable.Text = "1";
            Paginar(ref rpListadoUnidadMedida, BuscarRegistroSeleccionado, 1, ref lbCantidadPaginaVariable, ref LinkPrimeraPagina, ref LinkAnterior, ref LinkSiguiente, ref LinkUltimo);
            HandlePaging(ref dtPaginacion, ref LinkBlbPaginaActualVariable);

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

  

        protected void LinkPrimeraPagina_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            ListadoUidadMedida();
        }

        protected void LinkAnterior_Click(object sender, EventArgs e)
        {
            CurrentPage += -1;
            ListadoUidadMedida();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref LinkBlbPaginaActualVariable, ref lbCantidadPaginaVariable);
        }

        protected void dtPaginacion_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dtPaginacion_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            ListadoUidadMedida();

        }

        protected void LinkSiguiente_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            ListadoUidadMedida();
        }

        protected void LinkUltimo_Click(object sender, EventArgs e)
        {

            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            ListadoUidadMedida();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref LinkBlbPaginaActualVariable, ref lbCantidadPaginaVariable);
        }
    }
}