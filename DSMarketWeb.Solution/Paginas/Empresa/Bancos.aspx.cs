using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DSMarketWeb.Solution.Paginas.Empresa
{
    public partial class Bancos : System.Web.UI.Page
    {
        Lazy<DSMarketWeb.Logic.Logica.LogicaEmpresa.LogicaEmpresa> ObjDataEmpresa = new Lazy<Logic.Logica.LogicaEmpresa.LogicaEmpresa>();
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
            lbPaginaActualVariable.Text = (NumeroPagina + 1).ToString();
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
            lbCantidadPaginaVariable.Text = pagedDataSource.PageCount.ToString();

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


            divPaginacionBancos.Visible = true;
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
                    lbPaginaActualVariable.Text = "1";

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
            DivBloqueMatenimiento.Visible = false;
        }
        private void Mantenimiento_Consulta() {
            DivBloqueConsulta.Visible = false;
            DivBloqueMatenimiento.Visible = true;
        }

        private void ListadoBancos() {
            string _Banco = string.IsNullOrEmpty(txtNombreBancoConsulta.Text.Trim()) ? null : txtNombreBancoConsulta.Text.Trim();

            var Listado = ObjDataEmpresa.Value.BuscaBancos(
                new Nullable<decimal>(),
                _Banco);
            if (Listado.Count() < 1)
            {
                lbCantidadRegistrosVariable.Text = "0";
            }
            else {
                Paginar(ref rpListadoBancos, Listado, 10);
                HandlePaging(ref dlPaginacion);
                divPaginacionBancos.Visible = true;
                int CantidadRegistros = 0;
                foreach (var n in Listado) {
                    CantidadRegistros = (int)n.CantidadRegistros;
                }
                lbCantidadRegistrosVariable.Text = CantidadRegistros.ToString("N0");


            }


        }

        private void RestablecerPantalla() {
            txtNombreBancoConsulta.Text = string.Empty;
            txtNombreBancoMantenimiento.Text = string.Empty;
            txtCuentaContableMantenimiento.Text = string.Empty;
            txtClaveSeguridad.Text = string.Empty;
            txtAuxiliarMantenimiento.Text = string.Empty;
            ModoConsulta();
            Consulta_Mantenimiento();
            ListadoBancos();
        }

        private void MAMBancos(decimal IdBanco, string Accion) {
            DSMarketWeb.Logic.PrcesarMantenimientos.Empresa.ProcesarInformacionBancos Procesar = new Logic.PrcesarMantenimientos.Empresa.ProcesarInformacionBancos(
                IdBanco,
                Convert.ToInt32(txtCuentaContableMantenimiento.Text),
                Convert.ToInt32(txtAuxiliarMantenimiento.Text),
                txtNombreBancoMantenimiento.Text,
                cbEstatus.Checked,
                (decimal)Session["IdUsuario"],
                Accion);
            Procesar.ProcesarInformacion();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                DSMarketWeb.Logic.Comunes.SacarNombreUsuario NombreUsuario = new Logic.Comunes.SacarNombreUsuario((decimal)Session["IdUsuario"]);
                Label lbUsuarioConectado = (Label)Master.FindControl("lbUsuarioConectado");
                lbUsuarioConectado.Text = NombreUsuario.SacarNombre();

                Label lbNombrePantalla = (Label)Master.FindControl("lbNivelAccesoPantalla");
                lbNombrePantalla.Text = "MANTENIMIENTO DE BANCOS";


                ModoConsulta();
                Consulta_Mantenimiento();
                CurrentPage = 0;
                ListadoBancos();
            }
        }

        protected void btnConsultarRegistros_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            ListadoBancos();
        }

        protected void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            Mantenimiento_Consulta();
            txtNombreBancoMantenimiento.Text = string.Empty;
            txtCuentaContableMantenimiento.Text = string.Empty;
            txtAuxiliarMantenimiento.Text = string.Empty;
            cbEstatus.Checked = true;
            lbClaveSeguridad.Visible = false;
            txtClaveSeguridad.Visible = false;
            btnGuardar.Visible = true;
            btnModificar.Visible = false;
        }

        protected void btnModificarRegistro_Click(object sender, EventArgs e)
        {
            Mantenimiento_Consulta();
            lbClaveSeguridad.Visible = true;
            txtClaveSeguridad.Visible = true;
            btnGuardar.Visible = false;
            btnModificar.Visible = true;
        }

        protected void btnRestablecerPantalla_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            RestablecerPantalla();
        }

        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {
            var ItemSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfIdBanco = ((HiddenField)ItemSeleccionado.FindControl("hfIdBanco")).Value.ToString();

            lbIdRegistroSeleccionado.Text = hfIdBanco.ToString();

            var RegistroSeleccionado = ObjDataEmpresa.Value.BuscaBancos(Convert.ToDecimal(hfIdBanco), null);
            Paginar(ref rpListadoBancos, RegistroSeleccionado, 1);
            HandlePaging(ref dlPaginacion);
            lbCantidadRegistrosVariable.Text = "1";
            foreach (var n in RegistroSeleccionado) {
                txtNombreBancoMantenimiento.Text = n.Banco;
                txtCuentaContableMantenimiento.Text = n.CuentaContable.ToString();
                txtAuxiliarMantenimiento.Text = n.Auxiliar.ToString();
            }
            ModoMantenimiento();
        }

        protected void LinkPrimeraPaginaPaginacion_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            ListadoBancos();
        }

        protected void LinkPaginaAnterior_Click(object sender, EventArgs e)
        {
            CurrentPage += -1;
            ListadoBancos();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior);
        }

        protected void dlPaginacion_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dlPaginacion_CancelCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            ListadoBancos();
        }

        protected void LinkPaginaSiguiente_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            ListadoBancos();
        }

        protected void LinkUltipaPagina_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            ListadoBancos();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.UltimaPagina);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            MAMBancos(0, "INSERT");
            ClientScript.RegisterStartupScript(GetType(), "RegistroGuardado()", "RegistroGuardado();", true);
            CurrentPage = 0;
            RestablecerPantalla();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            string _ClaveSeguridad = string.IsNullOrEmpty(txtClaveSeguridad.Text.Trim()) ? null : txtClaveSeguridad.Text.Trim();
            DSMarketWeb.Logic.Comunes.ValidarClaveSeguridad Validar = new Logic.Comunes.ValidarClaveSeguridad(_ClaveSeguridad);
            bool Respuesta = Validar.ResultadoClave();
            if (Respuesta == true) {
                MAMBancos(Convert.ToDecimal(lbIdRegistroSeleccionado.Text), "UPDATE");
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