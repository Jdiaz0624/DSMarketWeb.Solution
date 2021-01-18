using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DSMarketWeb.Solution.Paginas.Empresa
{
    public partial class Retenciones : System.Web.UI.Page
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


            divPaginacionRetencion.Visible = true;
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
            btnModificarRegistro.Enabled = true;
            btnRestablecerPantalla.Enabled = true;
        }
        private void ModoMantenimiento() {
            btnConsultarRegistros.Enabled = false;
            btnNuevoRegistro.Enabled = false;
            btnModificar.Enabled = true;
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

        private void ListadoRetenciones() {
            string _Retencion = string.IsNullOrEmpty(txtRetencionConsulta.Text.Trim()) ? null : txtRetencionConsulta.Text.Trim();

            var Listado = ObjDataEmpresa.Value.BuscaRetenciones(
                new Nullable<decimal>(),
                _Retencion);
            if (Listado.Count() < 1)
            {
                lbCantidadRegistrosVariable.Text = "0";
            }
            else {
                Paginar(ref rpListadoRetenciones, Listado, 10);
                HandlePaging(ref dlPaginacion);
                int CantidadRegistros = 0;
                foreach (var n in Listado) {
                    CantidadRegistros = (int)n.CantidadRegistros;
                }
                lbCantidadRegistrosVariable.Text = CantidadRegistros.ToString("N0");
                divPaginacionRetencion.Visible = true;
            }
        }

        private void Restablecerpantalla() {
            txtRetencionConsulta.Text = string.Empty;
            txtRetencionMantenimiento.Text = string.Empty;
            txtClaveSeguridad.Text = string.Empty;
            ModoConsulta();
            Consulta_Mantenimiento();
            CurrentPage = 0;
            ListadoRetenciones();
        }

        private void MANRetenciones(decimal IdRetencion, string Accion) {
            DSMarketWeb.Logic.PrcesarMantenimientos.Empresa.ProcesarInformacionRetenciones Procesar = new Logic.PrcesarMantenimientos.Empresa.ProcesarInformacionRetenciones(
                IdRetencion,
                txtRetencionMantenimiento.Text,
                cbEstatus.Checked,
                (decimal)Session["IdUsuario"],
                Accion);
            Procesar.ProcesarInformacion();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                ModoConsulta();
                Consulta_Mantenimiento();
                CurrentPage = 0;
                ListadoRetenciones();
            }
        }

        protected void btnConsultarRegistros_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            ListadoRetenciones();
        }

        protected void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            Mantenimiento_Consulta();
            btnGuardar.Visible = true;
            btnModificar.Visible = false;
            lbClaveSeguridad.Visible = false;
            txtClaveSeguridad.Visible = false;
            txtRetencionMantenimiento.Text = string.Empty;
            cbEstatus.Checked = true;
        }

        protected void btnModificarRegistro_Click(object sender, EventArgs e)
        {
            Mantenimiento_Consulta();
            btnGuardar.Visible = false;
            btnModificar.Visible = true;
            lbClaveSeguridad.Visible = true;
            txtClaveSeguridad.Visible = true;
        }

        protected void btnRestablecerPantalla_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            Restablecerpantalla();
        }

        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {
            var ItemSelecciponado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfIdRetencion = ((HiddenField)ItemSelecciponado.FindControl("hfIdRetencion")).Value.ToString();
            lbIdRegistroSeleccionado.Text = hfIdRetencion.ToString();

            var RegistroSeleccionado = ObjDataEmpresa.Value.BuscaRetenciones(
                Convert.ToDecimal(hfIdRetencion), null);
            lbCantidadRegistrosVariable.Text = "1";
            Paginar(ref rpListadoRetenciones, RegistroSeleccionado, 1);
            HandlePaging(ref dlPaginacion);
            foreach (var n in RegistroSeleccionado) {
                txtRetencionMantenimiento.Text = n.Retencion;
                cbEstatus.Checked = (n.Estatus0.HasValue ? n.Estatus0.Value : false);
            }
            ModoMantenimiento();
        }

        protected void LinkPrimeraPaginaPaginacion_Click(object sender, EventArgs e)
        {

        }

        protected void LinkPaginaAnterior_Click(object sender, EventArgs e)
        {

        }

        protected void dlPaginacion_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dlPaginacion_CancelCommand(object source, DataListCommandEventArgs e)
        {

        }

        protected void LinkPaginaSiguiente_Click(object sender, EventArgs e)
        {

        }

        protected void LinkUltipaPagina_Click(object sender, EventArgs e)
        {

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {

        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {

        }
    }
}