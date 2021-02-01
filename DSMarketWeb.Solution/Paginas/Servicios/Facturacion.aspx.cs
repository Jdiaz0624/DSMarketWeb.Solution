using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace DSMarketWeb.Solution.Paginas.Servicios
{
    public partial class Facturacion : System.Web.UI.Page
    {
        Lazy<DSMarketWeb.Logic.Logica.LogicaEmpresa.LogicaEmpresa> ObjdataEmpresa = new Lazy<Logic.Logica.LogicaEmpresa.LogicaEmpresa>();

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
        private void HandlePaging(ref DataList NombreDataList, ref Label lbPaginaActual)
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
            lbPaginaActual.Text = (NumeroPagina + 1).ToString();
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
        private void Paginar(ref Repeater RptGrid, IEnumerable<object> Listado, int _NumeroRegistros, ref Label lbCantidadPaginas, ref LinkButton PrmeraPagina, ref LinkButton PaginaAnterior, ref LinkButton SiguientePagina, ref LinkButton UltimaPagina)
        {
            pagedDataSource.DataSource = Listado;
            pagedDataSource.AllowPaging = true;

            ViewState["TotalPages"] = pagedDataSource.PageCount;
            // lbNumeroVariable.Text = "1";
            lbCantidadPaginas.Text = pagedDataSource.PageCount.ToString();

            //MOSTRAMOS LA CANTIDAD DE PAGINAS A MOSTRAR O NUMERO DE REGISTROS
            pagedDataSource.PageSize = (_NumeroRegistros == 0 ? _TamanioPagina : _NumeroRegistros);
            pagedDataSource.CurrentPageIndex = CurrentPage;

            //HABILITAMOS LOS BOTONES DE LA PAGINACION
            PrmeraPagina.Enabled = !pagedDataSource.IsFirstPage;
            PaginaAnterior.Enabled = !pagedDataSource.IsFirstPage;
            SiguientePagina.Enabled = !pagedDataSource.IsLastPage;
            UltimaPagina.Enabled = !pagedDataSource.IsLastPage;

            RptGrid.DataSource = pagedDataSource;
            RptGrid.DataBind();


            divPaginacionClienteConsulta.Visible = true;
            divPaginacionProductoAgregar.Visible = true;
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

        #region MOSTRAR EL LISTADO DE LOS CLIENTES REGISTRADOS
        private void MostrarClientesRegistrados() {
            string _CodigoCliente = string.IsNullOrEmpty(txtCodigoClienteConsulta.Text.Trim()) ? null : txtCodigoClienteConsulta.Text.Trim();
            string _RNCCliente = string.IsNullOrEmpty(txtRNCConsultaCliente.Text.Trim()) ? null : txtRNCConsultaCliente.Text.Trim();
            string _NombreCliente = string.IsNullOrEmpty(txtNombreClienteConsulta.Text.Trim()) ? null : txtNombreClienteConsulta.Text.Trim();

            var Listado = ObjdataEmpresa.Value.BuscaClientes(
                _CodigoCliente,
                null,
                _NombreCliente,
                _RNCCliente, true, null, null);
            if (Listado.Count() < 1) {
                lbCantidadRegistrosEncontradosClientesVariables.Text = "0";
            }
            else {
                int CantidadRegistros = Listado.Count;
                lbCantidadRegistrosEncontradosClientesVariables.Text = CantidadRegistros.ToString("N0");

                Paginar(ref rpListadoClientesConsulta, Listado, 10, ref lbCantidadPaginaVAriableClienteConsulta, ref LinkPrimeraPaginaClienteConsulta, ref LinkAnteriorClienteConsulta, ref LinkSiguienteClienteConsulta, ref LinkUltimoClienteConsulta);
                HandlePaging(ref dtPaginacionClienteConsulta, ref lbPaginaActualVariavleClienteConsulta);
            }

        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                rbFacturacion.Checked = true;
                rbContado.Checked = true;
            }
        }

        protected void btnConsultarClientes_Click(object sender, EventArgs e)
        {
            MostrarClientesRegistrados();
        }

        protected void btnSeleccioanrCliente_Click(object sender, EventArgs e)
        {

        }

        protected void LinkPrimeraPaginaClienteConsulta_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            MostrarClientesRegistrados();
        }

        protected void LinkAnteriorClienteConsulta_Click(object sender, EventArgs e)
        {
            CurrentPage += -1;
            MostrarClientesRegistrados();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior,ref lbPaginaActualVariavleClienteConsulta,ref lbCantidadPaginaVAriableClienteConsulta);
        }

        protected void dtPaginacionClienteConsulta_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dtPaginacionClienteConsulta_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            MostrarClientesRegistrados();
        }

        protected void LinkSiguienteClienteConsulta_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            MostrarClientesRegistrados();
        }

        protected void ddlSeleccionarTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSeleccionarCategria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSeleccionarMarca_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnBuscarProducto_Click(object sender, EventArgs e)
        {

        }

        protected void btnSeleccionarProductoAgregar_Click(object sender, EventArgs e)
        {

        }

        protected void LinkPrimeraPaginaProductoAgregar_Click(object sender, EventArgs e)
        {

        }

        protected void LinkAnteriorProductoAgregar_Click(object sender, EventArgs e)
        {

        }

        protected void dtPaginacionProductoAgregar_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dtPaginacionProductoAgregar_ItemCommand(object source, DataListCommandEventArgs e)
        {

        }

        protected void LinkSiguienteProductoAgregar_Click(object sender, EventArgs e)
        {

        }

        protected void LinkUltimoProductoAgregar_Click(object sender, EventArgs e)
        {

        }

        protected void btnAgregarRegistro_Click(object sender, EventArgs e)
        {

        }

        protected void btnEditarRegistroAgregado_Click(object sender, EventArgs e)
        {

        }

        protected void btnEliminarRegistroAgregado_Click(object sender, EventArgs e)
        {

        }

        protected void btnRestablecerVistaPrevia_Click(object sender, EventArgs e)
        {

        }

        protected void btnSeleccionarRegistrosAgregadosHeaderRepeater_Click(object sender, EventArgs e)
        {

        }

        protected void LinkPrimeraPaginaProductosAgregados_Click(object sender, EventArgs e)
        {

        }

        protected void LinkAnteriorProductosAgregados_Click(object sender, EventArgs e)
        {

        }

        protected void dtPaginacionProductosAgregados_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dtPaginacionProductosAgregados_ItemCommand(object source, DataListCommandEventArgs e)
        {

        }

        protected void LinkSiguienteProductosAgregados_Click(object sender, EventArgs e)
        {

        }

        protected void LinkUltimoProductosAgregados_Click(object sender, EventArgs e)
        {

        }

        protected void LinkUltimoClienteConsulta_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            MostrarClientesRegistrados();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref lbPaginaActualVariavleClienteConsulta, ref lbCantidadPaginaVAriableClienteConsulta);
        }
    }
}