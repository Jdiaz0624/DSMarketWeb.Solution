using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DSMarketWeb.Solution.Paginas.Inventario
{
    public partial class MantenimientoCapacidad : System.Web.UI.Page
    {
        Lazy<DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario> ObjdataInventario = new Lazy<Logic.Logica.LogicaInventario.LogicaInventario>();
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
        private void HandlePaging(ref DataList NombreDataList)
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


            NombreDataList.DataSource = dt;
            NombreDataList.DataBind();
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
            LinkPrimeraPagina.Enabled = !pagedDataSource.IsFirstPage;
            LinkAnterior.Enabled = !pagedDataSource.IsFirstPage;
            LinkSiguiente.Enabled = !pagedDataSource.IsLastPage;
            LinkUltimo.Enabled = !pagedDataSource.IsLastPage;

            RptGrid.DataSource = pagedDataSource;
            RptGrid.DataBind();


            divPaginacion.Visible = true;
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
                    lbPaginaActualVariavle.Text = "1";

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
        #endregion

        #region UTILIDADES DE MANTENIMIENTO
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
        private void Consulta_Mantenimiento() {
            DivBloqueConsulta.Visible = true;
            DivBloqueMantenimiento.Visible = false;
        }
        private void Mantenimiento_Consulta() {
            DivBloqueConsulta.Visible = false;
            DivBloqueMantenimiento.Visible = true;
        }

        private void BuscarListadoCapacidad() {
            string _Capacidad = string.IsNullOrEmpty(txtCapacidadConsulta.Text.Trim()) ? null : txtCapacidadConsulta.Text.Trim();

            var Listado = ObjdataInventario.Value.BuscaCapacdad(
                new Nullable<decimal>(),
                _Capacidad);
            if (Listado.Count() < 1)
            {
                lbCantidadRegistrosVariable.Text = "0";
            }
            else {
                int CantidadRehistros = 0;
                foreach (var n in Listado) {
                    CantidadRehistros = Convert.ToInt32(n.CantidadRegistros);
                }
                lbCantidadRegistrosVariable.Text = CantidadRehistros.ToString("N0");
                Paginar(ref rpListadoCapacidad, Listado, 10);
                HandlePaging(ref dtPaginacion);
                divPaginacion.Visible = true;
            }
        }

        private void RestablecerPantalla() {
            ModoConsulta();
            Consulta_Mantenimiento();
            txtCapacidadConsulta.Text = string.Empty;
            txtCapacidadMantenimiento.Text = string.Empty;
            txtClaveSeguridadMantenimiento.Text = string.Empty;
            cbEstatus.Checked= true;
            BuscarListadoCapacidad();
        }

        private void MAnCapacidad(decimal IdCapacidad, string Accion) {
            DSMarketWeb.Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionCapacidadEquipos Procesar = new Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionCapacidadEquipos(
                IdCapacidad,
                txtCapacidadMantenimiento.Text,
                cbEstatus.Checked,
                (decimal)Session["IdUsuario"],
                Accion);
            Procesar.ProcesarInformacionCapacidad();
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                ModoConsulta();
                Consulta_Mantenimiento();
                divPaginacion.Visible = false;

            }
        }

        protected void btnConsultarRegistros_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            BuscarListadoCapacidad();
        }

        protected void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            Mantenimiento_Consulta();
            cbEstatus.Checked = true;
            lbClaveSeguridadMantenimiento.Visible = false;
            txtClaveSeguridadMantenimiento.Visible = false;
            btnGuardar.Visible = true;
            btnModificar.Visible = false;

        }

        protected void btnModificarRegistros_Click(object sender, EventArgs e)
        {
            Mantenimiento_Consulta();
            lbClaveSeguridadMantenimiento.Visible = true;
            txtClaveSeguridadMantenimiento.Visible = true;
            btnGuardar.Visible = false;
            btnModificar.Visible = true;
        }

        protected void btnRestablecerPantalla_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            RestablecerPantalla();
        }

        protected void btnSeleccionarRegistros_Click(object sender, EventArgs e)
        {
            var ItemSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfIdCapaciad = ((HiddenField)ItemSeleccionado.FindControl("hfIdCapacidad")).Value.ToString();
            lbIdRegistroSeleccionado.Text = hfIdCapaciad.ToString();

            var BuscarIdRegistroSeleccionado = ObjdataInventario.Value.BuscaCapacdad(
               Convert.ToDecimal(hfIdCapaciad));
            lbCantidadRegistrosVariable.Text = "1";
            foreach (var n in BuscarIdRegistroSeleccionado) {
                txtCapacidadMantenimiento.Text = n.Capacidad;
                cbEstatus.Checked = (n.Estatus0.HasValue ? n.Estatus0.Value : false);
            }
            ModoMantenimiento();
            Paginar(ref rpListadoCapacidad, BuscarIdRegistroSeleccionado, 1);
            HandlePaging(ref dtPaginacion);
        }

        protected void LinkPrimeraPagina_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            BuscarListadoCapacidad();
        }

        protected void LinkAnterior_Click(object sender, EventArgs e)
        {
            CurrentPage += -1;
            BuscarListadoCapacidad();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior);
        }

        protected void dtPaginacion_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            BuscarListadoCapacidad();
        }

        protected void dtPaginacion_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void LinkSiguiente_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            BuscarListadoCapacidad();
        }

        protected void LinkUltimo_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            BuscarListadoCapacidad();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.UltimaPagina);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            MAnCapacidad(0, "INSERT");
            ClientScript.RegisterStartupScript(GetType(), "RegistroGuardado()", "RegistroGuardado();", true);
            CurrentPage = 0;
            RestablecerPantalla();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            string _ClaveSeguridad = string.IsNullOrEmpty(txtClaveSeguridadMantenimiento.Text.Trim()) ? null : txtClaveSeguridadMantenimiento.Text.Trim();
            var ValidarClave = ObjDataSeguridad.Value.BuscaClaveSeguridad(
                new Nullable<decimal>(),
                null,
                DSMarketWeb.Logic.Comunes.SeguridadEncriptacion.Encriptar(_ClaveSeguridad));
            if (ValidarClave.Count() < 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "ClaveSeguridadNoValida()", "ClaveSeguridadNoValida();", true);

            }
            else {
                MAnCapacidad(Convert.ToDecimal(lbIdRegistroSeleccionado.Text), "UPDATE");
                ClientScript.RegisterStartupScript(GetType(), "ModificarRegistro()", "ModificarRegistro();", true);
                CurrentPage = 0;
                RestablecerPantalla();
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            RestablecerPantalla();
        }
    }
}