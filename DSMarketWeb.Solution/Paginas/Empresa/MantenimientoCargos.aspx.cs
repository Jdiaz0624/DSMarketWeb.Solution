using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DSMarketWeb.Solution.Paginas.Empresa
{
    public partial class MantenimientoCargos : System.Web.UI.Page
    {
        Lazy<DSMarketWeb.Logic.Logica.LogicaEmpresa.LogicaEmpresa> ObjDataEmpresa = new Lazy<Logic.Logica.LogicaEmpresa.LogicaEmpresa>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaSeguridad.LogicaSeguridad> ObjdataSeguridad = new Lazy<Logic.Logica.LogicaSeguridad.LogicaSeguridad>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion> ObjDataConfiguracion = new Lazy<Logic.Logica.LogicaConfiguracion.LogicaConfiguracion>();

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

        #region CARGAR LAS LISTAS DESPLEGABLES
        private void CargarDepartamentosConsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarDepartamentoConsulta, ObjDataConfiguracion.Value.BuscaListas("DEPARTAMENTO", null, null), true);
        }
        private void CargarDepartamentosMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarDepartamentoMantenimiento, ObjDataConfiguracion.Value.BuscaListas("DEPARTAMENTO", null, null));
        }
        #endregion

        #region MOSTRAR LISTADO DE CARGOS
        private void ListadoCargos() {
            decimal? _Departamento = ddlSeleccionarDepartamentoConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarDepartamentoConsulta.SelectedValue) : new Nullable<decimal>();
            string _Cargo = string.IsNullOrEmpty(txtCargoConsulta.Text.Trim()) ? null : txtCargoConsulta.Text.Trim();

            var Listado = ObjDataEmpresa.Value.BuscaCargos(
               new Nullable<decimal>(),
               _Departamento,
               _Cargo);
            if (Listado.Count() < 1)
            {
                lbCantidadRegistrosVariable.Text = "0";
            }
            else {
                int CantidadRegsitros = 0;
                foreach (var n in Listado) {
                    CantidadRegsitros = (int)n.CantidadRegistros;
                }
                lbCantidadRegistrosVariable.Text = CantidadRegsitros.ToString("N0");
                Paginar(ref rpListadoCargo, Listado, 10);
                HandlePaging(ref dtPaginacion);
                divPaginacion.Visible = true;
            }
        }
        #endregion

        #region MANTENIMIENTO DE CARGOS
        private void MANCargos(decimal IdCargo, string Accion) {
            DSMarketWeb.Logic.PrcesarMantenimientos.Empresa.ProcesarInformacionCargos Procesar = new Logic.PrcesarMantenimientos.Empresa.ProcesarInformacionCargos(
                IdCargo,
                Convert.ToDecimal(ddlSeleccionarDepartamentoMantenimiento.SelectedValue),
                txtCargoManteimiento.Text,
                cbEstatusMantenimiento.Checked,
                (decimal)Session["IdUsuario"],
                Accion);
            Procesar.ProcesarDataCargo();
        }
        #endregion

        private void ModoConsulta() {
            btnConsultarRegistros.Enabled = true;
            btnNuevoRegistro.Enabled = true;
            btnModificarRegistro.Enabled = false;
            btnRestablecerPantalla.Enabled = true;
        }
        private void ModoMantenimiento() {
            btnConsultarRegistros.Enabled = false;
            btnNuevoRegistro.Enabled = false;
            btnModificarRegistro.Enabled = true;
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
        private void RestablecerPantalla() {
            ModoConsulta();
            CargarDepartamentosConsulta();
            txtCargoConsulta.Text = string.Empty;

            CargarDepartamentosMantenimiento();
            txtCargoManteimiento.Text = string.Empty;
            txtClaveSeguridadMantenimiento.Text = string.Empty;
            cbEstatusMantenimiento.Checked = true;
            Consulta_Mantenimiento();
            ListadoCargos();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                DSMarketWeb.Logic.Comunes.SacarNombreUsuario NombreUsuario = new Logic.Comunes.SacarNombreUsuario((decimal)Session["IdUsuario"]);
                Label lbUsuarioConectado = (Label)Master.FindControl("lbUsuarioConectado");
                lbUsuarioConectado.Text = NombreUsuario.SacarNombre();

                Label lbNombrePantalla = (Label)Master.FindControl("lbNivelAccesoPantalla");
                lbNombrePantalla.Text = "MANTENIMIENTO DE CARGOS";
                ModoConsulta();
                Consulta_Mantenimiento();
                CargarDepartamentosConsulta();
                CargarDepartamentosMantenimiento();
                divPaginacion.Visible = false;
            }
        }

        protected void btnConsultarRegistros_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            ListadoCargos();
        }

        protected void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            Mantenimiento_Consulta();
            btnGuardar.Visible = true;
            btnModificar.Visible = false;
            cbEstatusMantenimiento.Checked = true;
            lbClaveSeguridadMantenimiento.Visible = false;
            txtClaveSeguridadMantenimiento.Visible = false;
        }

        protected void btnModificarRegistro_Click(object sender, EventArgs e)
        {
            Mantenimiento_Consulta();
            btnGuardar.Visible = false;
            btnModificar.Visible = true;
            lbClaveSeguridadMantenimiento.Visible = true;
            txtClaveSeguridadMantenimiento.Visible = true;
        }

        protected void btnRestablecerPantalla_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            RestablecerPantalla();
        }

        protected void btnSeleccionarRegistro_Click(object sender, EventArgs e)
        {
            var ItemSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfIdcargo = ((HiddenField)ItemSeleccionado.FindControl("hfIdCargo")).Value.ToString();
            lbIdRegistroSeleccionado.Text = hfIdcargo.ToString();

            var BuscarregistroSelecionado = ObjDataEmpresa.Value.BuscaCargos(
                Convert.ToDecimal(hfIdcargo));
            foreach (var n in BuscarregistroSelecionado) {
                CargarDepartamentosMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarDepartamentoMantenimiento, n.IdDepartamento.ToString());
                txtCargoManteimiento.Text = n.Cargo;
                cbEstatusMantenimiento.Checked = (n.Estatus0.HasValue ? n.Estatus0.Value : false);
            }
            ModoMantenimiento();
            Paginar(ref rpListadoCargo, BuscarregistroSelecionado, 1);
            HandlePaging(ref dtPaginacion);

        }

        protected void LinkPrimeraPagina_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            ListadoCargos();
        }

        protected void LinkAnterior_Click(object sender, EventArgs e)
        {
            CurrentPage += -1;
            ListadoCargos();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior);
        }

        protected void dtPaginacion_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            ListadoCargos();
        }

        protected void dtPaginacion_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void LinkSiguiente_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            ListadoCargos();
        }

        protected void LinkUltimo_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            ListadoCargos();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.UltimaPagina);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            MANCargos(0, "INSERT");
            ClientScript.RegisterStartupScript(GetType(), "RegistroGuardado()", "RegistroGuardado();", true);
            CurrentPage = 0;
            RestablecerPantalla();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            string _ClaveSeguridad = string.IsNullOrEmpty(txtClaveSeguridadMantenimiento.Text.Trim()) ? null : txtClaveSeguridadMantenimiento.Text.Trim();

            DSMarketWeb.Logic.Comunes.ValidarClaveSeguridad Validar = new Logic.Comunes.ValidarClaveSeguridad(_ClaveSeguridad);
            bool ResultadoValidacion = Validar.ResultadoClave();

            if (ResultadoValidacion == true) {
                MANCargos(Convert.ToDecimal(lbIdRegistroSeleccionado.Text), "UPDATE");
                ClientScript.RegisterStartupScript(GetType(), "RegistroModificado()", "RegistroModificado();", true);
                CurrentPage = 0;
                RestablecerPantalla();
            }
            else {
                ClientScript.RegisterStartupScript(GetType(), "ClaveSeguridadNoValida()", "ClaveSeguridadNoValida();", true);
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            RestablecerPantalla();
        }
    }
}