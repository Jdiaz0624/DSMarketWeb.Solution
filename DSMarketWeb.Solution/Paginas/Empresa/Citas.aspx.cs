using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace DSMarketWeb.Solution.Paginas.Empresa
{
    public partial class Citas : System.Web.UI.Page
    {
        Lazy<DSMarketWeb.Logic.Logica.LogicaEmpresa.LogicaEmpresa> ObjDataLogica = new Lazy<Logic.Logica.LogicaEmpresa.LogicaEmpresa>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion> ObjDataConfiguracion = new Lazy<Logic.Logica.LogicaConfiguracion.LogicaConfiguracion>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaSeguridad.LogicaSeguridad> ObjDataSeguridad = new Lazy<Logic.Logica.LogicaSeguridad.LogicaSeguridad>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario> ObjDataInventario = new Lazy<Logic.Logica.LogicaInventario.LogicaInventario>();

        #region GENERAR REPORTE DE CITAS
        private void GenerarReporteCita(decimal IdUsuarioGenera, string RutaReporte, string NombreArchivo, int TipoReporte) {

            //SACAMOS LAS CREDENCIALES DE LA BASE DE ATOS
            string UsuarioBD = "";
            string ClaveBD = "";

            var SacarCredenciales = ObjDataSeguridad.Value.SacarCredencialesBD(1);
            foreach (var n in SacarCredenciales) {
                UsuarioBD = n.Usuario;
                ClaveBD = DSMarketWeb.Logic.Comunes.SeguridadEncriptacion.DesEncriptar(n.Clave);
            }

            if (TipoReporte == 1) {
                if (cbExportarTodo.Checked == true)
                {
                    ReportDocument Reporte = new ReportDocument();
                    Reporte.Load(RutaReporte);
                    Reporte.Refresh();
                    Reporte.SetParameterValue("@IdCita", null);
                    Reporte.SetParameterValue("@FechaDesde", null);
                    Reporte.SetParameterValue("@FechaHasta", null);
                    Reporte.SetParameterValue("@UsuarioGenera", IdUsuarioGenera);
                    Reporte.SetDatabaseLogon(UsuarioBD, ClaveBD);
                    if (rbExportarPDF.Checked == true)
                    {
                        Reporte.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, NombreArchivo);
                    }
                    else if (rbExportarWord.Checked == true)
                    {
                        Reporte.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, true, NombreArchivo);
                    }
                    else if (rbEcportarExcel.Checked == true)
                    {
                        Reporte.ExportToHttpResponse(ExportFormatType.Excel, Response, true, NombreArchivo);
                    }

                }
                else {
                    if (string.IsNullOrEmpty(txtFechaDesde.Text.Trim()) || string.IsNullOrEmpty(txtFechaHasta.Text.Trim()))
                    {
                        ClientScript.RegisterStartupScript(GetType(), "CamposFechaVaciosReporte()", "CamposFechaVaciosReporte();", true);
                        if (string.IsNullOrEmpty(txtFechaDesde.Text.Trim())) {
                            ClientScript.RegisterStartupScript(GetType(), "CampoFechaDesdeVacio()", "CampoFechaDesdeVacio();", true);
                        }
                        if (string.IsNullOrEmpty(txtFechaHasta.Text.Trim())) {
                            ClientScript.RegisterStartupScript(GetType(), "CampoFechaHAstaVacio()", "CampoFechaHAstaVacio();", true);
                        }
                    }
                    else {
                        string _NumeroCita = string.IsNullOrEmpty(txtNumeroCitaConsulta.Text.Trim()) ? null : txtNumeroCitaConsulta.Text.Trim();

                        ReportDocument Reporte = new ReportDocument();
                        Reporte.Load(RutaReporte);
                        Reporte.Refresh();
                        Reporte.SetParameterValue("@IdCita", _NumeroCita);
                        Reporte.SetParameterValue("@FechaDesde", Convert.ToDateTime(txtFechaDesde.Text));
                        Reporte.SetParameterValue("@FechaHasta", Convert.ToDateTime(txtFechaHasta.Text));
                        Reporte.SetParameterValue("@UsuarioGenera", IdUsuarioGenera);
                        Reporte.SetDatabaseLogon(UsuarioBD, ClaveBD);
                        if (rbExportarPDF.Checked == true)
                        {
                            Reporte.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, NombreArchivo);
                        }
                        else if (rbExportarWord.Checked == true)
                        {
                            Reporte.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, true, NombreArchivo);
                        }
                        else if (rbEcportarExcel.Checked == true)
                        {
                            Reporte.ExportToHttpResponse(ExportFormatType.Excel, Response, true, NombreArchivo);
                        }
                    }
                }
            }
            else if(TipoReporte==2){
                ReportDocument Reporte = new ReportDocument();
                Reporte.Load(RutaReporte);
                Reporte.Refresh();
                Reporte.SetParameterValue("@IdCita", lbIdCitaSeleccionada.Text);
                Reporte.SetParameterValue("@FechaDesde", null);
                Reporte.SetParameterValue("@FechaHasta", null);
                Reporte.SetParameterValue("@UsuarioGenera", IdUsuarioGenera);
                Reporte.SetDatabaseLogon(UsuarioBD, ClaveBD);
                if (rbExportarPDF.Checked == true)
                {
                    Reporte.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, NombreArchivo);
                }
                else if (rbExportarWord.Checked == true)
                {
                    Reporte.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, true, NombreArchivo);
                }
                else if (rbEcportarExcel.Checked == true)
                {
                    Reporte.ExportToHttpResponse(ExportFormatType.Excel, Response, true, NombreArchivo);
                }
            }
        
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
        private void Paginar(ref Repeater RptGrid, IEnumerable<object> Listado, int _NumeroRegistros, ref Label lbCantidadPagina, ref LinkButton PrimeraPagina, ref LinkButton PaginaAnterior, ref LinkButton PaginaSiguiente, ref LinkButton UltimaPagina)
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
            PaginaSiguiente.Enabled = !pagedDataSource.IsLastPage;
            UltimaPagina.Enabled = !pagedDataSource.IsLastPage;

            RptGrid.DataSource = pagedDataSource;
            RptGrid.DataBind();


          //  divPaginacionBancos.Visible = true;
        }
        enum OpcionesPaginacionValores
        {
            PrimeraPagina = 1,
            SiguientePagina = 2,
            PaginaAnterior = 3,
            UltimaPagina = 4
        }
        private void MoverValoresPaginacion(int Accion, ref Label lbPaginaActual, ref Label CantidadPagina)
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
                    lbPaginaActual.Text = CantidadPagina.Text;
                    break;


            }

        }
        #endregion
        private void BloquearControlesMantenimiento() {
            txtNombreClienteMantenimiento.Enabled = false;
            txtNumeroIdentificacionMantenimiento.Enabled = false;
            txtTelefono.Enabled = false;
            txtFechaCitaMantenimiento.Enabled = false;
            txtHoraCitaMantenimiento.Enabled = false;
            ddlSeleccionarDepartamentoMantenimiento.Enabled = false;
            ddlSeleccionarEmpleadoMantenimiento.Enabled = false;
            txtDireccionMantenimiento.Enabled = false;
            cbEstatus.Enabled = false;
            txtBuscarServicio.Enabled = false;
            LinkPrimeroServicioAgregar.Enabled = false;
            LinkAnteriorServicioAgregar.Enabled = false;
            dtPaginacionServicioAgregar.Enabled = false;
            LinkSiguienteServicioAgregar.Enabled = false;
            LinkUltimoServicioAgregar.Enabled = false;
            LinkPrimeroQuitar.Enabled = false;
            LinkAnteriorQuitar.Enabled = false;
            dlPaginacionQuitar.Enabled = false;
            LinkSiguienteQuitar.Enabled = false;
            LinkUltimoQuitar.Enabled = false;
            btnBuscarServicios.Enabled = false;
            txtComentarioMantenimiento.Enabled = false;
        }
        private void DesbloquearControlesMantenimiento() {
            txtNombreClienteMantenimiento.Enabled = true;
            txtNumeroIdentificacionMantenimiento.Enabled = true;
            txtTelefono.Enabled = true;
            txtFechaCitaMantenimiento.Enabled = true;
            txtHoraCitaMantenimiento.Enabled = true;
            ddlSeleccionarDepartamentoMantenimiento.Enabled = true;
            ddlSeleccionarEmpleadoMantenimiento.Enabled = true;
            txtDireccionMantenimiento.Enabled = true;
            cbEstatus.Enabled = true;
            txtBuscarServicio.Enabled = true;
            LinkPrimeroServicioAgregar.Enabled = true;
            LinkAnteriorServicioAgregar.Enabled = true;
            dtPaginacionServicioAgregar.Enabled = true;
            LinkSiguienteServicioAgregar.Enabled = true;
            LinkUltimoServicioAgregar.Enabled = true;
            LinkPrimeroQuitar.Enabled = true;
            LinkAnteriorQuitar.Enabled = true;
            dlPaginacionQuitar.Enabled = true;
            LinkSiguienteQuitar.Enabled = true;
            LinkUltimoQuitar.Enabled = true;
            btnBuscarServicios.Enabled = true;
            txtComentarioMantenimiento.Enabled = true;
        }

        private void RestablecerPantalla() {
            Consulta_Mantenimiento();
            CargarListasDesplegablesConsulta();
            CargarListaDesplegableMantenimiento();
            rbTodos.Checked = true;
            rbExportarPDF.Checked = true;
            DivBloqueDetalleCita.Visible = false;
            DivBloqueMantenimieto.Visible = false;
            ModoConsulta();
            

            txtBuscarServicio.Text = string.Empty;
            txtClienteDetalle.Text = string.Empty;
            txtDireccionClienteDetalle.Text = string.Empty;
            txtDireccionMantenimiento.Text = string.Empty;
            txtEmpleadoDetalle.Text = string.Empty;
            txtEstatusDetalle.Text = string.Empty;
            txtFechaCitaDetalle.Text = string.Empty;
            txtFechaCitaMantenimiento.Text = string.Empty;
            txtFechaDesde.Text = string.Empty;
            txtFechaHasta.Text = string.Empty;
            txtHoraCitaDetalle.Text = string.Empty;
            txtHoraCitaMantenimiento.Text = string.Empty;
            txtNombreClienteConsulta.Text = string.Empty;
            txtNombreClienteMantenimiento.Text = string.Empty;
            txtNumeroCitaConsulta.Text = string.Empty;
            txtNumeroCitaDetalle.Text = string.Empty;
            txtNumeroIdentificacionConsulta.Text = string.Empty;
            txtNumeroIdentificacionDetalle.Text = string.Empty;
            txtNumeroIdentificacionMantenimiento.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtTelefonoDetalle.Text = string.Empty;
            CurrentPage = 0;
            ListadoCitas();
            lbTipoReporte.Text = "1";
        }

        private void CargarListasDesplegablesConsulta() {
            CargarDepartamentosConsulta();
            CargarTcnicoConsulta();
        }
        private void CargarDepartamentosConsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarDepartamentoConsulta, ObjDataConfiguracion.Value.BuscaListas("DEPARTAMENTO", null, null), true);
        }
        private void CargarTcnicoConsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarEmpleadoConsulta, ObjDataConfiguracion.Value.BuscaListas("TECNICO", ddlSeleccionarDepartamentoConsulta.SelectedValue.ToString(), null), true);
        }

        private void CargarListaDesplegableMantenimiento() {
            CargarListaDepartamentoMantenimiento();
            CargarTecnicoMantenimiento();
        }
        private void CargarListaDepartamentoMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarDepartamentoMantenimiento, ObjDataConfiguracion.Value.BuscaListas("DEPARTAMENTO", null, null));
            
        }
        private void CargarTecnicoMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarEmpleadoMantenimiento, ObjDataConfiguracion.Value.BuscaListas("TECNICO", ddlSeleccionarDepartamentoMantenimiento.SelectedValue.ToString(), null));
        }

        private void ModoConsulta() {
            btnConsultarRegistros.Enabled = true;
            btnNuevoRegistro.Enabled = true;
            btnEditarRegistro.Enabled = false;
            btnEliminarRegistro.Enabled = false;
            btnRestablecerPantalla.Enabled = true;
            btnReporte.Enabled = true;
        }

        private void ModoMantenimiento() {
            btnConsultarRegistros.Enabled = false;
            btnNuevoRegistro.Enabled = false;
            btnEditarRegistro.Enabled = true;
            btnEliminarRegistro.Enabled = true;
            btnRestablecerPantalla.Enabled = true;
            btnReporte.Enabled = true;
        }

        private void Consulta_Mantenimiento() {
            DivBloqueCOnsulta.Visible = true;
            DivBloqueDetalleCita.Visible = false;
            DivBloqueMantenimieto.Visible = false;
        }

        private void Mantenimiento_Consulta() {
            DivBloqueCOnsulta.Visible = false;
            DivBloqueDetalleCita.Visible = false;
            DivBloqueMantenimieto.Visible = true;
        }

        private void ListadoCitas() {
            bool? _Estatus = null;
            string _NumeroCita = string.IsNullOrEmpty(txtNumeroCitaConsulta.Text.Trim()) ? null : txtNumeroCitaConsulta.Text.Trim();
            string _NombreCliente = string.IsNullOrEmpty(txtNombreClienteConsulta.Text.Trim()) ? null : txtNombreClienteConsulta.Text.Trim();
            string _NumeroIdentificacion = string.IsNullOrEmpty(txtNumeroIdentificacionConsulta.Text.Trim()) ? null : txtNumeroIdentificacionConsulta.Text.Trim();
            decimal? _Empleado = ddlSeleccionarEmpleadoConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarEmpleadoConsulta.SelectedValue) : new Nullable<decimal>();
            DateTime? FechaDesde = string.IsNullOrEmpty(txtFechaDesde.Text.Trim()) ? new Nullable<DateTime>() : Convert.ToDateTime(txtFechaDesde.Text);
            DateTime? FechaHasta = string.IsNullOrEmpty(txtFechaHasta.Text.Trim()) ? new Nullable<DateTime>() : Convert.ToDateTime(txtFechaHasta.Text);


            if (rbTodos.Checked == true) {
                _Estatus = null;
            }
            else if (rbRegistrosActivos.Checked == true) {
                _Estatus = true;
            }
            else if (rbRegistrosInactivos.Checked == true) {
                _Estatus = false;
            }



            if (cbAgregarRangoFechaConsulta.Checked == true) {
                if (string.IsNullOrEmpty(txtFechaDesde.Text.Trim()) || string.IsNullOrEmpty(txtFechaHasta.Text.Trim())) { }
                else {
                    var Listado = ObjDataLogica.Value.BuscaCitasEncabezado(
                   _NumeroCita,
                   _Empleado,
                   Convert.ToDateTime(txtFechaDesde.Text),
                   Convert.ToDateTime(txtFechaHasta.Text),
                   _NombreCliente,
                   _NumeroIdentificacion,
                   _Estatus);
                    if (Listado.Count() < 1)
                    {
                        lbCantidadRegistrosVariable.Text = "0";
                        rpListadoCitasEncabezado.DataSource = null;
                        rpListadoCitasEncabezado.DataBind();
                    }
                    else
                    {
                        Paginar(ref rpListadoCitasEncabezado, Listado, 10, ref lbCantidadPaginaVariableCitaEncabezado, ref LinkPrimeraPaginaCitasEncabezado, ref LinkPaginaAnteriorCitasEncabezado, ref LinkPaginaSiguienteCitasEncabezado, ref LinkUltipaPaginaCitasEncabezado);
                        HandlePaging(ref dlPaginacionCitasEncabezado, ref lbCantidadPaginaVariableCitaEncabezado);
                        int CantidadRegistros = 0;
                        foreach (var n in Listado)
                        {
                            CantidadRegistros = (int)n.CantidadRegistros;
                        }
                        lbCantidadRegistrosVariable.Text = CantidadRegistros.ToString("N0");
                    }
                }
            }
            else {
                var Listado = ObjDataLogica.Value.BuscaCitasEncabezado(
                 _NumeroCita,
                 _Empleado,
                 null,
                 null,
                 _NombreCliente,
                 _NumeroIdentificacion,
                 _Estatus);
                if (Listado.Count() < 1)
                {
                    lbCantidadRegistrosVariable.Text = "0";
                }
                else
                {
                    Paginar(ref rpListadoCitasEncabezado, Listado, 10, ref lbCantidadPaginaVariableCitaEncabezado, ref LinkPrimeraPaginaCitasEncabezado, ref LinkPaginaAnteriorCitasEncabezado, ref LinkPaginaSiguienteCitasEncabezado, ref LinkUltipaPaginaCitasEncabezado);
                    HandlePaging(ref dlPaginacionCitasEncabezado, ref lbCantidadPaginaVariableCitaEncabezado);
                    int CantidadRegistros = 0;
                    foreach (var n in Listado)
                    {
                        CantidadRegistros = (int)n.CantidadRegistros;
                    }
                    lbCantidadRegistrosVariable.Text = CantidadRegistros.ToString("N0");
                }
            }
            GenerarGrafico();
        }

        private void GenerarGrafico() {
            int[] CantidadRegistros = new int[2];
            string[] Nombre = new string[2];
            int Contador = 0;

            //FILTROS
            string _NumeroCita = string.IsNullOrEmpty(txtNumeroCitaConsulta.Text.Trim()) ? "N/A" : txtNumeroCitaConsulta.Text.Trim();
            string _NombreCliente = string.IsNullOrEmpty(txtNombreClienteConsulta.Text.Trim()) ? "N/A" : txtNombreClienteConsulta.Text.Trim();
            string _NumeroIdentificacion = string.IsNullOrEmpty(txtNumeroIdentificacionConsulta.Text.Trim()) ? "N/A" : txtNumeroIdentificacionConsulta.Text.Trim();
            decimal? _Empleado = ddlSeleccionarEmpleadoConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarEmpleadoConsulta.SelectedValue) : 0;
            DateTime? _FechaDesde = string.IsNullOrEmpty(txtFechaDesde.Text.Trim()) ? Convert.ToDateTime("1942-01-01") : Convert.ToDateTime(txtFechaDesde.Text);
            DateTime? _FechaHasta = string.IsNullOrEmpty(txtFechaHasta.Text.Trim()) ? Convert.ToDateTime("1942-01-01") : Convert.ToDateTime(txtFechaHasta.Text);

            SqlConnection conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DSMarketWEBConexion"].ConnectionString);
            SqlCommand comando = new SqlCommand("EXEC [Empresa].[SP_GRAFICOS_CITAS_ENCABEZADO] @IdCIta,@IdEmpleado,@FechaCitaDesde,@FechaCitaHasta,@NombreCliente,@NumeroIdentificacion", conexion);

            comando.Parameters.AddWithValue("@IdCIta", SqlDbType.VarChar).Value = _NumeroCita;
            comando.Parameters.AddWithValue("@IdEmpleado", SqlDbType.Decimal).Value = _Empleado;
            comando.Parameters.AddWithValue("@FechaCitaDesde", SqlDbType.Date).Value = _FechaDesde;
            comando.Parameters.AddWithValue("@FechaCitaHasta", SqlDbType.Date).Value = _FechaHasta;
            comando.Parameters.AddWithValue("@NombreCliente", SqlDbType.VarChar).Value = _NombreCliente;
            comando.Parameters.AddWithValue("@NumeroIdentificacion", SqlDbType.VarChar).Value = _NumeroIdentificacion;

            conexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            while (reader.Read()) {
                CantidadRegistros[Contador] = Convert.ToInt32(reader.GetInt32(1));
                Nombre[Contador] = reader.GetString(0);
                Contador++;
            }
            reader.Close();
            conexion.Close();

            GraEstatusCitas.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            GraEstatusCitas.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
            GraEstatusCitas.ChartAreas["ChartArea1"].AxisX.Interval = 1;

            GraEstatusCitas.Series["Serie"].Points.DataBindXY(Nombre, CantidadRegistros);
        }

        private void MostrarServiciosAgregados(decimal NumeroConector) {
            var MostrarServiciosAgregados = ObjDataLogica.Value.BuscaCitasDetalle(NumeroConector);
            int CantidadRegistros = 0;
            decimal Total = 0;
            foreach (var n in MostrarServiciosAgregados)
            {
                CantidadRegistros = (int)n.CantidadRegistros;
                Total = (decimal)n.Total;
            }
            lbCantidadServiciosAgregadosMantenimientoVariable.Text = CantidadRegistros.ToString("N0");
            lbTotalServicioMantenimientoVariable.Text = Total.ToString("N2");
            Paginar(ref rpListadoServiciosAgregadosDetalle, MostrarServiciosAgregados, 10, ref lbCantidadPaginaVariableQuitar, ref LinkPrimeroQuitar, ref LinkAnteriorQuitar, ref LinkSiguienteQuitar, ref LinkUltimoQuitar);
            HandlePaging(ref dlPaginacionQuitar, ref lbPaginaActualVariableQuitar);
        }

        private void MostrarServiciosAgregadosDetalle(decimal NumeroConector) {
            var MostrarServiciosAgregados = ObjDataLogica.Value.BuscaCitasDetalle(NumeroConector);
            int CantidadRegistros = 0;
            decimal Total = 0;
            foreach (var n in MostrarServiciosAgregados)
            {
                CantidadRegistros = (int)n.CantidadRegistros;
                Total = (decimal)n.Total;
            }
            lbCantidadServiciosVariable.Text = CantidadRegistros.ToString("N0");
            lbTotalMontoVariable.Text = Total.ToString("N2");
            Paginar(ref rpListadoCitaDetalle, MostrarServiciosAgregados, 10, ref lbCantidadPaginaVariableCitaDetalle, ref LinkPrimeroCitaDetalle, ref LinkAnteriorCitaDetalle, ref LinkSiguienteCitaDetalle, ref LinkUltimoSiguienteDetalle);
            HandlePaging(ref dlPaginacionCitaDetalle, ref lbPaginaActualVariableCitaDetalle);
        }

        private void MANCitas(decimal IdCita, string Accion) {
            DSMarketWeb.Logic.PrcesarMantenimientos.Empresa.ProcesarInformacionCitaEncabezado ProcesarCitaEncabezado = new Logic.PrcesarMantenimientos.Empresa.ProcesarInformacionCitaEncabezado(
                IdCita,
                Convert.ToDecimal(ddlSeleccionarEmpleadoMantenimiento.SelectedValue),
                Convert.ToDateTime(txtFechaCitaMantenimiento.Text),
                txtHoraCitaMantenimiento.Text,
                txtNombreClienteMantenimiento.Text,
                txtTelefono.Text,
                txtDireccionMantenimiento.Text,
                txtNumeroIdentificacionMantenimiento.Text,
                Convert.ToDecimal(lbNumeroConectorseleccionado.Text),
                cbEstatus.Checked,
                txtComentarioMantenimiento.Text,
                Accion);
            ProcesarCitaEncabezado.ProcesarInformacion();
        }

        private void GuardarServicios(decimal NumeroConector, decimal IdProducto, decimal Precio, string Descripcion, string Accion) {
            DSMarketWeb.Logic.PrcesarMantenimientos.Empresa.ProcesarInformacionCitasDetalle GuardarDetalleCita = new Logic.PrcesarMantenimientos.Empresa.ProcesarInformacionCitasDetalle(
                NumeroConector,
                IdProducto,
                Precio,
                Descripcion,
                Accion);
            GuardarDetalleCita.ProcesarInformacion();
        }
        private void MostrarServicios() {
            string _NombreServicio = string.IsNullOrEmpty(txtBuscarServicio.Text.Trim()) ? null : txtBuscarServicio.Text.Trim();

            var BuscarServicio = ObjDataInventario.Value.BuscaProductos(
                new Nullable<decimal>(),
                null,
                _NombreServicio,
                null, null, null, null, 2, null, null, null, null, null, null, null, null, null, null, null);
            Paginar(ref rpListadoServiciosAgregar, BuscarServicio, 10, ref lbCantidadPaginaVariableAgregarServicio, ref LinkPrimeroServicioAgregar, ref LinkAnteriorServicioAgregar, ref LinkSiguienteServicioAgregar, ref LinkUltimoServicioAgregar);
            HandlePaging(ref dtPaginacionServicioAgregar, ref lbPaginaActualVariableAgregarServicio);
        }

        private void GenerarNumerConector() {
            Random NumeroConector = new Random();
            int Numero = NumeroConector.Next(0, 999999999);
            lbNumeroConectorseleccionado.Text = Numero.ToString();
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                DSMarketWeb.Logic.Comunes.SacarNombreUsuario Nombre = new Logic.Comunes.SacarNombreUsuario((decimal)Session["IdUsuario"]);
                Label lbUsuarioConectado = (Label)Master.FindControl("lbUsuarioConectado");
                lbUsuarioConectado.Text = Nombre.SacarNombre();

                Label lbPantallaActual = (Label)Master.FindControl("lbNivelAccesoPantalla");
                lbPantallaActual.Text = "CONSULTA DE CITAS";
               
                CargarListasDesplegablesConsulta();
                CargarListaDesplegableMantenimiento(); 
                rbTodos.Checked = true;
                rbExportarPDF.Checked = true;
                DivBloqueDetalleCita.Visible = false;
                DivBloqueMantenimieto.Visible = false;
                ModoConsulta();
                CurrentPage = 0;
                ListadoCitas();
                lbTipoReporte.Text = "1";
            }
        }

        protected void btnConsultarRegistros_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            ListadoCitas();
            DivBloqueDetalleCita.Visible = false;
        }

        protected void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            Mantenimiento_Consulta();
            GenerarNumerConector();
            lbAccionMantenimiento.Text = "INSERT";
            cbEstatus.Checked = true;
            DesbloquearControlesMantenimiento();
            btnGuardarCita.Text = "Guardar";
        }

        protected void btnEditarRegistro_Click(object sender, EventArgs e)
        {
            Mantenimiento_Consulta();
            lbAccionMantenimiento.Text = "UPDATE";
            DesbloquearControlesMantenimiento();
            btnGuardarCita.Text = "Modificar";
        }

        protected void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            Mantenimiento_Consulta();
            lbAccionMantenimiento.Text = "DELETE";
            BloquearControlesMantenimiento();
            btnGuardarCita.Text = "Eliminar";
        }

        protected void btnReporte_Click(object sender, EventArgs e)
        {
            string Nombrearchivo = "";
            string ReprteGenerar = "";
            if (lbTipoReporte.Text == "1")
            {
                Nombrearchivo = "Reporte de Citas General";
                ReprteGenerar = "ReporteCitasGeneral.rpt";
            }
            else {
                Nombrearchivo = "Reporte de Citas";
                ReprteGenerar = "ReporteCitasUnico.rpt";
            }
            GenerarReporteCita(
                (decimal)Session["IdUsuario"],
                Server.MapPath(ReprteGenerar),
                Nombrearchivo,
                Convert.ToInt32(lbTipoReporte.Text));
        }

        protected void btnRestablecerPantalla_Click(object sender, EventArgs e)
        {
            RestablecerPantalla();
        }

        protected void LinkPrimeraPaginaCitasEncabezado_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            ListadoCitas();
        }

        protected void LinkPaginaAnteriorCitasEncabezado_Click(object sender, EventArgs e)
        {
            CurrentPage += -1;
            ListadoCitas();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref lbPaginaActualVariableCitaEncabezado, ref lbCantidadPaginaVariableCitaEncabezado);
        }

        protected void dlPaginacionCitasEncabezado_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dlPaginacionCitasEncabezado_CancelCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            ListadoCitas();

        }

        protected void LinkPaginaSiguienteCitasEncabezado_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            ListadoCitas();
        }

        protected void LinkUltipaPaginaCitasEncabezado_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            ListadoCitas();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref lbPaginaActualVariableCitaEncabezado, ref lbCantidadPaginaVariableCitaEncabezado);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------
        protected void LinkPrimeroCitaDetalle_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            MostrarServiciosAgregadosDetalle(Convert.ToDecimal(lbNumeroConectorseleccionado.Text));
        }

        protected void LinkAnteriorCitaDetalle_Click(object sender, EventArgs e)
        {
            CurrentPage += -1;
            MostrarServiciosAgregadosDetalle(Convert.ToDecimal(lbNumeroConectorseleccionado.Text));
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref lbPaginaActualVariableCitaDetalle, ref lbCantidadPaginaVariableCitaDetalle);
        }

        protected void dlPaginacionCitaDetalle_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dlPaginacionCitaDetalle_CancelCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            MostrarServiciosAgregadosDetalle(Convert.ToDecimal(lbNumeroConectorseleccionado.Text));
        }

        protected void LinkSiguienteCitaDetalle_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            MostrarServiciosAgregadosDetalle(Convert.ToDecimal(lbNumeroConectorseleccionado.Text));
        }

        protected void LinkUltimoSiguienteDetalle_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            MostrarServiciosAgregadosDetalle(Convert.ToDecimal(lbNumeroConectorseleccionado.Text));
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref lbPaginaActualVariableCitaDetalle, ref lbCantidadPaginaVariableCitaDetalle);
        }

        protected void btnBuscarServicios_Click(object sender, EventArgs e)
        {
            MostrarServicios();
        }

        protected void btnSeleccionarServicio_Click(object sender, EventArgs e)
        {
            var IdProductoSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfIdProductoSeleccionado = ((HiddenField)IdProductoSeleccionado.FindControl("hfIdProductoSeleccionado")).Value.ToString();

            var PrecioServicioSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfPrecioServicioSeleccionado = ((HiddenField)PrecioServicioSeleccionado.FindControl("hfPrecioServicio")).Value.ToString();

            var DescripcionSericio = (RepeaterItem)((Button)sender).NamingContainer;
            var hfDescripcionSericio = ((HiddenField)DescripcionSericio.FindControl("hfDescripcionServicio")).Value.ToString();


            GuardarServicios(Convert.ToDecimal(lbNumeroConectorseleccionado.Text), Convert.ToDecimal(hfIdProductoSeleccionado), Convert.ToDecimal(hfPrecioServicioSeleccionado), hfDescripcionSericio.ToString(), "INSERT");


            MostrarServiciosAgregados(Convert.ToDecimal(lbNumeroConectorseleccionado.Text));
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        protected void LinkPrimeroServicioAgregar_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            MostrarServicios();

        }

        protected void LinkAnteriorServicioAgregar_Click(object sender, EventArgs e)
        {
            CurrentPage += -1;
            MostrarServicios();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref lbPaginaActualVariableAgregarServicio, ref lbCantidadPaginaVariableAgregarServicio);
        }

        protected void dtPaginacionServicioAgregar_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dtPaginacionServicioAgregar_CancelCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            MostrarServicios();
        }

        protected void LinkSiguienteServicioAgregar_Click(object sender, EventArgs e)
        {

            CurrentPage += 1;
            MostrarServicios();
        }

        protected void LinkUltimoServicioAgregar_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            MostrarServicios();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref lbPaginaActualVariableAgregarServicio, ref lbCantidadPaginaVariableAgregarServicio);
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        protected void LinkPrimeroQuitar_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            MostrarServiciosAgregados(Convert.ToDecimal(lbNumeroConectorseleccionado.Text));
        }

        protected void LinkAnteriorQuitar_Click(object sender, EventArgs e)
        {
            CurrentPage += -1;
            MostrarServiciosAgregados(Convert.ToDecimal(lbNumeroConectorseleccionado.Text));
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref lbPaginaActualVariableQuitar, ref lbCantidadPaginaVariableQuitar);
        }

        protected void dlPaginacionQuitar_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void dlPaginacionQuitar_CancelCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            MostrarServiciosAgregados(Convert.ToDecimal(lbNumeroConectorseleccionado.Text));
        }

        protected void LinkSiguienteQuitar_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            MostrarServiciosAgregados(Convert.ToDecimal(lbNumeroConectorseleccionado.Text));
        }

        protected void LinkUltimoQuitar_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            MostrarServiciosAgregados(Convert.ToDecimal(lbNumeroConectorseleccionado.Text));
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior, ref lbPaginaActualVariableQuitar, ref lbCantidadPaginaVariableQuitar);
        }

        protected void btnGuardarCita_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtFechaCitaMantenimiento.Text.Trim()))
            {
                ClientScript.RegisterStartupScript(GetType(), "CampoFechaCitaVacio()", "CampoFechaCitaVacio();", true);
            }
            else {
                //GUARDAMOS la cita
                string Accion = lbAccionMantenimiento.Text;

                switch (Accion)
                {
                    case "INSERT":
                        MANCitas(0, "INSERT");
                        ClientScript.RegisterStartupScript(GetType(), "RegistroGuardado()", "RegistroGuardado();", true);
                        RestablecerPantalla();
                        break;

                    case "UPDATE":
                        MANCitas(Convert.ToDecimal(lbIdCitaSeleccionada.Text), "UPDATE");
                        ClientScript.RegisterStartupScript(GetType(), "RegistroModificado()", "RegistroModificado();", true);
                        RestablecerPantalla();
                        break;

                    case "DELETE":
                        MANCitas(Convert.ToDecimal(lbIdCitaSeleccionada.Text), "DELETE");
                        GuardarServicios(Convert.ToDecimal(lbNumeroConectorseleccionado.Text), 0, 0, "", "DELETEALL");
                        ClientScript.RegisterStartupScript(GetType(), "RegistroEliminado()", "RegistroEliminado();", true);
                        RestablecerPantalla();
                        break;

                }
            }
          
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            RestablecerPantalla();
        }

        protected void ddlSeleccionarDepartamentoConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarTcnicoConsulta();
        }

        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {
            var ItemSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfIdCitaSeleccionada = ((HiddenField)ItemSeleccionado.FindControl("hfIdCitaSeleccionada")).Value.ToString();

            var NumeroConectorSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfNumeroConector = ((HiddenField)NumeroConectorSeleccionado.FindControl("hfNumeroConector")).Value.ToString();

            string NumeroCita = hfIdCitaSeleccionada.ToString();
            decimal NumeroConector = Convert.ToDecimal(hfNumeroConector);
            decimal IdDepartamento = 0;

            DivBloqueDetalleCita.Visible = true;
            var BuscarDetalle = ObjDataLogica.Value.BuscaCitasEncabezado(
                NumeroCita,
                null,
                null,
                null,
                null,
                null,
                null);
            foreach (var n in BuscarDetalle) {
                txtNumeroCitaDetalle.Text = n.IdCitas.ToString();
                txtEmpleadoDetalle.Text = n.Empleado;
                txtClienteDetalle.Text = n.NombreCliente;
                txtNumeroIdentificacionDetalle.Text = n.NumeroIdentificacion;
                txtFechaCitaDetalle.Text = n.FechaCita;
                txtHoraCitaDetalle.Text = n.Hora;
                txtTelefonoDetalle.Text = n.Telefono;
                txtEstatusDetalle.Text = n.Estatus;
                txtDireccionClienteDetalle.Text = n.Direccion;
                txtComentarioCitaDetalle.Text = n.Comentario;

                var SacarDepartamentoEmpleado = ObjDataLogica.Value.BuscaEmpleados(n.IdEmpleado.ToString(), null, null, null, null, null, null, null, null, null, null, null);
                foreach (var nDepartamento in SacarDepartamentoEmpleado) {
                    IdDepartamento = (decimal)nDepartamento.IdDepartamento;
                }

                //SACAMOS LOS DATOS DE LA PARTE DEL MANTENIMIENTO
                txtNombreClienteMantenimiento.Text = n.NombreCliente;
                txtNumeroIdentificacionMantenimiento.Text = n.NumeroIdentificacion;
                txtTelefono.Text = n.Telefono;
                DateTime FechaCita = (DateTime)n.FechaCita0;
                txtFechaCitaMantenimiento.Text = FechaCita.ToString("yyyy-MM-dd");
                txtHoraCitaMantenimiento.Text = n.Hora;
                CargarListaDepartamentoMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarDepartamentoMantenimiento, IdDepartamento.ToString());
                CargarTecnicoMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarEmpleadoMantenimiento, n.IdEmpleado.ToString());
                txtDireccionMantenimiento.Text = n.Direccion;
                cbEstatus.Checked = (n.Estatus0.HasValue ? n.Estatus0.Value : false);
                lbNumeroConectorseleccionado.Text = n.NumeroConectorCita.ToString();
                lbIdCitaSeleccionada.Text = n.IdCitas.ToString();
                txtComentarioMantenimiento.Text = n.Comentario;
            }
            Paginar(ref rpListadoCitasEncabezado, BuscarDetalle, 1, ref lbCantidadPaginaVariableCitaEncabezado, ref LinkPrimeraPaginaCitasEncabezado, ref LinkPaginaAnteriorCitasEncabezado, ref LinkPaginaSiguienteCitasEncabezado, ref LinkUltipaPaginaCitasEncabezado);
            HandlePaging(ref dlPaginacionCitasEncabezado, ref lbPaginaActualVariableCitaEncabezado);
            ModoMantenimiento();
            MostrarServiciosAgregados(Convert.ToDecimal(lbNumeroConectorseleccionado.Text));
            lbTipoReporte.Text = "2";
            MostrarServiciosAgregadosDetalle(Convert.ToDecimal(hfNumeroConector));
        }

        protected void btnQuitarServicio_Click(object sender, EventArgs e)
        {
            var IdProductoSeleccionadoQuitar = (RepeaterItem)((Button)sender).NamingContainer;
            var hfIdProductoSeleccionadoQuitar = ((HiddenField)IdProductoSeleccionadoQuitar.FindControl("hfIdProductoQuitar")).Value.ToString();

            var NumeroConectorSeleccionadoQUitar = (RepeaterItem)((Button)sender).NamingContainer;
            var hfNumeroConectorSeleccionadoQuitar = ((HiddenField)NumeroConectorSeleccionadoQUitar.FindControl("hfNumeroConectorQuitar")).Value.ToString();

            GuardarServicios(Convert.ToDecimal(hfNumeroConectorSeleccionadoQuitar), Convert.ToDecimal(hfIdProductoSeleccionadoQuitar), 0, "", "DELETE");
            MostrarServiciosAgregados(Convert.ToDecimal(hfNumeroConectorSeleccionadoQuitar));
        }

        protected void ddlSeleccionarDepartamentoMantenimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarTecnicoMantenimiento();
        }
    }
}