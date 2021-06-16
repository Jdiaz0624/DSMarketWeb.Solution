using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DSMarketWeb.Solution.Paginas
{
    public partial class MantenimientoCondiciones : System.Web.UI.Page
    {
        Lazy<DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario> ObjDataInventario = new Lazy<Logic.Logica.LogicaInventario.LogicaInventario>();

        #region MOSTRAR EL LISTADO DE LAS CONDICIONES
        private void MostrarListadoCondiciones() {
            string _Condicion = string.IsNullOrEmpty(txtCondicionesConsulta.Text.Trim()) ? null : txtCondicionesConsulta.Text.Trim();

            var Listado = ObjDataInventario.Value.BuscaCOndiciones(
                new Nullable<decimal>(),
                _Condicion);
            if (Listado.Count() < 1)
            {
                rpCondiciones.DataSource = null;
                rpCondiciones.DataBind();
            }
            else {
                Paginar(ref rpCondiciones, Listado, 10, ref lbCantidadPaginaVariable, ref LinkPrimeraPagina, ref LinkAnterior, ref LinkSiguiente, ref LinkUltimo);
                HandlePaging(ref dtPaginacion, ref LinkBlbPaginaActualVariable);
            }
        }
        #endregion
        #region RESTABLECER PANTALLA Y VOLVER ATRAS
        private void RestablecerVolverAtras() {
            txtCondicionesConsulta.Text = string.Empty;
            txtCondicionesMantenimiento.Text = string.Empty;
            btnConsultar.Enabled = true;
            btnEditar.Enabled = false;
            btnNuevo.Enabled = true;
            btnRestablecer.Enabled = true;
            CurrentPage = 0;
            MostrarListadoCondiciones();
            DivBloqueConsulta.Visible = true;
            DivBloqueMantenimiento.Visible = false;
            cbEstatusCondicionesMantenimiento.Checked = true;

        }
        #endregion
        #region PROCESAR INFORMACION DE LAS CONDICIONES
        private void ProcesarInformacionCondiciones(decimal IdCondiciones, string Accion) {
            DSMarketWeb.Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionCondiciones Procesar = new Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionCondiciones(
                IdCondiciones,
                txtCondicionesMantenimiento.Text,
                cbEstatusCondicionesMantenimiento.Checked,
                Accion);
            Procesar.ProcesarInformacion();
            RestablecerVolverAtras();
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
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                DSMarketWeb.Logic.Comunes.SacarNombreUsuario Nombre = new Logic.Comunes.SacarNombreUsuario((decimal)Session["IdUsuario"]);
                Label lbUsuarioConectado = (Label)Master.FindControl("lbUsuarioConectado");
                Label lbPantalla = (Label)Master.FindControl("lbNivelAccesoPantalla");
                lbUsuarioConectado.Text = Nombre.SacarNombre();
                lbPantalla.Text = "MANTENIMIENTO DE CONDICIONES";
                RestablecerVolverAtras();
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            MostrarListadoCondiciones();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            DivBloqueConsulta.Visible = false;
            DivBloqueMantenimiento.Visible = true;
            lbIdRegistroSeleccionado.Text = "0";
            lbAccion.Text = "INSERT";
            txtCondicionesMantenimiento.Text = string.Empty;
            cbEstatusCondicionesMantenimiento.Checked = true;
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            DivBloqueConsulta.Visible = false;
            DivBloqueMantenimiento.Visible = true;
            lbAccion.Text = "UPDATE";
        }

        protected void btnRestablecer_Click(object sender, EventArgs e)
        {
            RestablecerVolverAtras();
        }

        protected void btnSeleccionarRegistro_Click(object sender, EventArgs e)
        {
            var ItemSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfItemSeleccionado = ((HiddenField)ItemSeleccionado.FindControl("hfIdCondiciones")).Value.ToString();
            lbIdRegistroSeleccionado.Text = hfItemSeleccionado;
            var RegistroSeleccionado = ObjDataInventario.Value.BuscaCOndiciones(Convert.ToDecimal(hfItemSeleccionado));
            Paginar(ref rpCondiciones, RegistroSeleccionado, 1, ref lbCantidadPaginaVariable, ref LinkPrimeraPagina, ref LinkAnterior, ref LinkSiguiente, ref LinkUltimo);
            HandlePaging(ref dtPaginacion, ref LinkBlbPaginaActualVariable);
            foreach (var n in RegistroSeleccionado) {
                txtCondicionesMantenimiento.Text = n.Condicion;
                cbEstatusCondicionesMantenimiento.Checked = (n.Estatus0.HasValue ? n.Estatus0.Value : false);
            }
            btnConsultar.Enabled = false;
            btnNuevo.Enabled = false;
            btnEditar.Enabled = true;
            btnRestablecer.Enabled = false;
        }

        protected void LinkPrimeraPagina_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            MostrarListadoCondiciones();
        }

        protected void LinkAnterior_Click(object sender, EventArgs e)
        {
            CurrentPage += -1;
            MostrarListadoCondiciones();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref LinkBlbPaginaActualVariable, ref lbCantidadPaginaVariable);
        }

        protected void dtPaginacion_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dtPaginacion_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            MostrarListadoCondiciones();
        }

        protected void LinkSiguiente_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            MostrarListadoCondiciones();
        }

        protected void LinkUltimo_Click(object sender, EventArgs e)
        {

            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            MostrarListadoCondiciones();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref LinkBlbPaginaActualVariable, ref lbCantidadPaginaVariable);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ProcesarInformacionCondiciones(Convert.ToDecimal(lbIdRegistroSeleccionado.Text), lbAccion.Text);
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            RestablecerVolverAtras();
        }
    }
}