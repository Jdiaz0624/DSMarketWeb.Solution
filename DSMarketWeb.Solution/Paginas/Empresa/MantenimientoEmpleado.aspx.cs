using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;

namespace DSMarketWeb.Solution.Paginas.Empresa
{
    public partial class MantenimientoEmpleado : System.Web.UI.Page
    {
        Lazy<DSMarketWeb.Logic.Logica.LogicaEmpresa.LogicaEmpresa> ObjDataEmpresa = new Lazy<Logic.Logica.LogicaEmpresa.LogicaEmpresa>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaSeguridad.LogicaSeguridad> ObjDataSeguridad = new Lazy<Logic.Logica.LogicaSeguridad.LogicaSeguridad>();
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
        private void CargarListasDesplegablesConsulta() {
            CargarListaTipoNominaConsulta();
            CargarListaTipoEmpleadoConsulta();
            CargarDepartamentosConsulta();
            CargarCargosConsulta();
        }
        private void CargarListaTipoNominaConsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoNominaConsulta, ObjDataConfiguracion.Value.BuscaListas("TIPONOMINA", null, null), true);
        }
        private void CargarListaTipoEmpleadoConsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoEmpleadoCOnsulta, ObjDataConfiguracion.Value.BuscaListas("TIPOEMPLEADO", null, null), true);
        }
        private void CargarDepartamentosConsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarDepartamentoCOnsulta, ObjDataConfiguracion.Value.BuscaListas("DEPARTAMENTO", null, null), true);
        }
        private void CargarCargosConsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCargoConsulta, ObjDataConfiguracion.Value.BuscaListas("CARGO", ddlSeleccionarDepartamentoCOnsulta.SelectedValue, null), true);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------
        private void CargarListasDesplegablesMantenimiento() {
            CargarListaDespTipoIdentificacionMantenimient();
            CargarListaNacionalidadMantenimiento();
            CargarListaSexoMantenimient();
            CargarListaEstadoCivilMantenimiento();
            CargarListaDepartamentoMantenimiento();
            CargarListaCargosMantenimiento();
            CargarListaTipoEmpleadoMantenimiento();
            CargarListaTipoNominaMantenimiento();
            CargarListaFormaPagoMantenimiento();
        }

        private void CargarListaDespTipoIdentificacionMantenimient() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoIdentificacionMantenimiento, ObjDataConfiguracion.Value.BuscaListas("TIPOIDENTIFICACION", null, null));
        }
        private void CargarListaNacionalidadMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarNacionalidadMantenimiento, ObjDataConfiguracion.Value.BuscaListas("NACIONALIDAD", null, null));
        }
        private void CargarListaSexoMantenimient() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarSexoMantenimiento, ObjDataConfiguracion.Value.BuscaListas("SEXO", null, null));
        }
        private void CargarListaEstadoCivilMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarEstadiCivilMantenimiento, ObjDataConfiguracion.Value.BuscaListas("ESTADOCIVIL", null, null));
        }
        private void CargarListaDepartamentoMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarDepartamentoMantenimiento, ObjDataConfiguracion.Value.BuscaListas("DEPARTAMENTO", null, null));
        }
        private void CargarListaCargosMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCargoMantenimiento, ObjDataConfiguracion.Value.BuscaListas("CARGO", ddlSeleccionarDepartamentoMantenimiento.SelectedValue.ToString(), null));
        }
        private void CargarListaTipoEmpleadoMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoEmpleadoMantenimiento, ObjDataConfiguracion.Value.BuscaListas("TIPOEMPLEADO", null, null));
        }
        private void CargarListaTipoNominaMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoNominaMantenimiento, ObjDataConfiguracion.Value.BuscaListas("TIPONOMINA", null, null));
        }
        private void CargarListaFormaPagoMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarFormaPagoMantenimiento, ObjDataConfiguracion.Value.BuscaListas("FORMAPAGOEMPLEADO", null, null));
        }
        #endregion

        #region MANTENIMIENTO DE EMPLEADOS
        private void MANEmpleados(decimal idEmpleado, string Accion) {
            if (string.IsNullOrEmpty(txtFechaIngresoMantenimiento.Text.Trim()) || string.IsNullOrEmpty(txtFechaNacimientoMantenimiento.Text.Trim())) {
                ClientScript.RegisterStartupScript(GetType(), "CamposVacios()", "CamposVacios();", true);
                if (string.IsNullOrEmpty(txtFechaIngresoMantenimiento.Text.Trim())) {
                    ClientScript.RegisterStartupScript(GetType(), "FechaIngresoVacio()", "FechaIngresoVacio();", true);
                }
                if (string.IsNullOrEmpty(txtFechaNacimientoMantenimiento.Text.Trim())) {
                    ClientScript.RegisterStartupScript(GetType(), "FechaNacimientoVacio()", "FechaNacimientoVacio();", true);
                }
            }
            else {
                DSMarketWeb.Logic.PrcesarMantenimientos.Empresa.ProcesarInformacionEmpleados Procesar = new Logic.PrcesarMantenimientos.Empresa.ProcesarInformacionEmpleados(
                    idEmpleado,
                    txtNombreMantenimiento.Text,
                    txtApellidoMAntenimiento.Text,
                    Convert.ToDecimal(ddlSeleccionarTipoIdentificacionMantenimiento.SelectedValue),
                    txtNumeroidentificacionMantenimiento.Text,
                    Convert.ToDecimal(ddlSeleccionarNacionalidadMantenimiento.SelectedValue),
                    txtxNSSMantenimiento.Text,
                    txtDireccionMAntenimiento.Text,
                    Convert.ToDecimal(ddlSeleccionarTipoEmpleadoMantenimiento.SelectedValue),
                    Convert.ToDecimal(ddlSeleccionarTipoNominaMantenimiento.SelectedValue),
                    Convert.ToDecimal(ddlSeleccionarDepartamentoMantenimiento.SelectedValue),
                    Convert.ToDecimal(ddlSeleccionarCargoMantenimiento.SelectedValue),
                    txtTelefonoMantenimiento.Text,
                    txtSegundoTelefonoMantenimiento.Text,
                    txtEmailMantenimiento.Text,
                    Convert.ToDecimal(ddlSeleccionarEstadiCivilMantenimiento.SelectedValue),
                    Convert.ToDecimal(txtSueldoMantenimiento.Text),
                    Convert.ToDecimal(txtOtrosIngresosMantenimiento.Text),
                    Convert.ToDecimal(ddlSeleccionarFormaPagoMantenimiento.SelectedValue),
                    Convert.ToDateTime(txtFechaIngresoMantenimiento.Text),
                    Convert.ToDateTime(txtFechaNacimientoMantenimiento.Text),
                    cbEstatus.Checked,
                    cbAplicaParaComision.Checked,
                    Convert.ToDecimal(txtPorcientoComisionVentasMantenimiento.Text),
                    Convert.ToDecimal(txtPorcientoComisionServicioMantenimiento.Text),
                    Convert.ToInt32(ddlSeleccionarSexoMantenimiento.SelectedValue),
                    cbLlevaFoto.Checked,
                    Accion);
                Procesar.ProcesarDatosEmpleados();
            }

        }
        #endregion

        private void ModoConsulta() {
            btnConsultarRegistros.Enabled = true;
            btnNuevoRegistro.Enabled = true;
            btnModificarRegistro.Enabled = false;
            btnExportarRegistro.Enabled = true;
            btnRestablecerPantalla.Enabled = true;
        }
        private void ModoMantenimiento() {
            btnConsultarRegistros.Enabled = false;
            btnNuevoRegistro.Enabled = false;
            btnModificarRegistro.Enabled = true;
            btnExportarRegistro.Enabled = true;
            btnRestablecerPantalla.Enabled = true;
        }
        private void Consulta_Mantenimiento() {
            DivBloqueConsulta.Visible = true;
            DivBloqueMantenimiento.Visible = false;
        }
        private void MAntenimiento_Consulta() {
            DivBloqueConsulta.Visible = false;
            DivBloqueMantenimiento.Visible = true;
        }

        private void MostrarListadoEmpleados() {
            string _CodigoEmpleado = string.IsNullOrEmpty(txtCodigoEmpleadoCosulta.Text.Trim()) ? null : txtCodigoEmpleadoCosulta.Text.Trim();
            string _NombreEmpleado = string.IsNullOrEmpty(txtNombreEmpleadoConsulta.Text.Trim()) ? null : txtNombreEmpleadoConsulta.Text.Trim();
            string _NumeroIdentificacion = string.IsNullOrEmpty(txtNumeroIdentificacionConsulta.Text.Trim()) ? null : txtNumeroIdentificacionConsulta.Text.Trim();
            string _NSS = string.IsNullOrEmpty(txtNSSConsulta.Text.Trim()) ? null : txtNSSConsulta.Text.Trim();
            decimal? _IdTipoNomina = ddlSeleccionarTipoNominaConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarTipoNominaConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _IdTipoEmpleado = ddlSeleccionarTipoEmpleadoCOnsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarTipoEmpleadoCOnsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _Departamento = ddlSeleccionarDepartamentoCOnsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarDepartamentoCOnsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _Cargo = ddlSeleccionarCargoConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarCargoConsulta.SelectedValue) : new Nullable<decimal>();
            bool? _Estatus = false;
            if (rbTodos.Checked == true) {
                _Estatus = null;
            }
            else if (rbActivos.Checked == true) {
                _Estatus = true;
            }
            else if (rbInactivos.Checked == true) {
                _Estatus = false;
            }

            var BuscarListado = ObjDataEmpresa.Value.BuscaEmpleados(
                _CodigoEmpleado,
                _NombreEmpleado,
                _NumeroIdentificacion,
                _NSS,
                null, null,
                _Estatus,
                _IdTipoEmpleado,
                _IdTipoNomina,
                _Departamento,
                _Cargo,
                0);
            if (BuscarListado.Count() < 1) {
                lbCantidadregistrosVariable.Text = "0";
                lbRegistrosActivosVariable.Text = "0";
                lbCantidadRegistrosInactivosVariable.Text = "0";
            }
            else {
                int CantidadRegistros = 0, CantidadActivos = 0, CantidadInactivos = 0;
                foreach (var n in BuscarListado) {
                    CantidadRegistros = (int)n.CantidadRegistros;
                    CantidadActivos = (int)n.CantidadActivos;
                    CantidadInactivos = (int)n.CantidadInactivos;
                }
                lbCantidadregistrosVariable.Text = CantidadRegistros.ToString("N0");
                lbRegistrosActivosVariable.Text = CantidadActivos.ToString("N0");
                lbCantidadRegistrosInactivosVariable.Text = CantidadInactivos.ToString("N0");

                Paginar(ref rpListadoEmpleado, BuscarListado, 10);
                HandlePaging(ref dtPaginacion);
                divPaginacion.Visible = true;
                GenerarGraficoEmpleados();
            }
        }
        private void GenerarGraficoEmpleados() {
            int[] CantidadRegistros = new int[2];
            string[] NombreEstatus = new string[2];
            int Contador = 0;

            SqlConnection Conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DSMarketWEBConexion"].ConnectionString);
            SqlCommand Comando = new SqlCommand("EXEC [Empresa].[SP_GENERAR_GRAFICOS_EMPLEADOS] @TipoGraficoGenerar", Conexion);

            Comando.Parameters.AddWithValue("@TipoGraficoGenerar", SqlDbType.Decimal).Value = 1;

            Conexion.Open();
            SqlDataReader Reader = Comando.ExecuteReader();
            while (Reader.Read())
            {
                CantidadRegistros[Contador] = Convert.ToInt32(Reader.GetInt32(1));
                NombreEstatus[Contador] = Reader.GetString(0);
                Contador++;
            }
            Reader.Close();
            Conexion.Close();

            //GraTipoProductos.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}k";

            GraEstatusEmpleados.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            GraEstatusEmpleados.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
            GraEstatusEmpleados.ChartAreas["ChartArea1"].AxisX.Interval = 1;

            GraEstatusEmpleados.Series["Serie"].Points.DataBindXY(NombreEstatus, CantidadRegistros);

        }

        private void RestablecerPantalla() {
            rbTodos.Checked = true;
            rbExportarPdf.Checked = true;
            txtCodigoEmpleadoCosulta.Text = string.Empty;
            txtNombreEmpleadoConsulta.Text = string.Empty;
            txtNumeroIdentificacionConsulta.Text = string.Empty;
            txtNSSConsulta.Text = string.Empty;
            CargarCargosConsulta();

            txtNombreMantenimiento.Text = string.Empty;
            txtApellidoMAntenimiento.Text = string.Empty;
            txtNumeroidentificacionMantenimiento.Text = string.Empty;
            txtxNSSMantenimiento.Text = string.Empty;
            txtEmailMantenimiento.Text = string.Empty;
            txtFechaIngresoMantenimiento.Text = string.Empty;
            txtTelefonoMantenimiento.Text = string.Empty;
            txtSegundoTelefonoMantenimiento.Text = string.Empty;
            txtFechaNacimientoMantenimiento.Text = string.Empty;
            txtSueldoMantenimiento.Text = string.Empty;
            txtOtrosIngresosMantenimiento.Text = string.Empty;
            txtDireccionMAntenimiento.Text = string.Empty;
            txtPorcientoComisionServicioMantenimiento.Text = string.Empty;
            txtPorcientoComisionVentasMantenimiento.Text = string.Empty;
            txtClaveSeguridadMantenimiento.Text = string.Empty;
            CargarListasDesplegablesMantenimiento();

            ModoConsulta();
            Consulta_Mantenimiento();
            MostrarListadoEmpleados();
            lbReporteUnico.Text = "0";
        }

        enum TipoReporteGenerar
        {
            ReporteUnico=1,
            ListadoGenea=0
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                CargarListasDesplegablesConsulta();
                CargarListasDesplegablesMantenimiento();
                rbTodos.Checked = true;
                rbExportarPdf.Checked = true;
                ModoConsulta();
                Consulta_Mantenimiento();
            }
        }

        protected void ddlSeleccionarDepartamentoCOnsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCargosConsulta();
        }

        protected void btnConsultarRegistros_Click(object sender, EventArgs e)
        {
            MostrarListadoEmpleados();
        }

        protected void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            MAntenimiento_Consulta();
            btnGuardar.Visible = true;
            btnModificar.Visible = false;
            lbClaveSeguridadMantenimiento.Visible = false;
            txtClaveSeguridadMantenimiento.Visible = false;
            cbAplicaParaComision.Checked = false;
            cbLlevaFoto.Checked = false;
            DivBloqueImagenEmpleado.Visible = false;

        }

        protected void btnModificarRegistro_Click(object sender, EventArgs e)
        {
            MAntenimiento_Consulta();
            btnGuardar.Visible = false;
            btnModificar.Visible = true;
            lbClaveSeguridadMantenimiento.Visible = true;
            txtClaveSeguridadMantenimiento.Visible = true;
        }

        protected void btnExportarRegistro_Click(object sender, EventArgs e)
        {
            int ReporteGenerar = Convert.ToInt32(lbReporteUnico.Text);

            if (ReporteGenerar == (int)TipoReporteGenerar.ListadoGenea)
            {
                //GENERAR EL LISTADO DE TODOS LOS EMPLEADOS
            }
            else if (ReporteGenerar == (int)TipoReporteGenerar.ReporteUnico) { 
            //GENERAMOS EL REPORTE DE UN REGISTRO SELECCIONADO
            }
        }

        protected void btnRestablecerPantalla_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            RestablecerPantalla();
        }

        protected void btnSeleccionarRegistros_Click(object sender, EventArgs e)
        {
            var ItemSeleccionado = (RepeaterItem)((Button)sender).NamingContainer;
            var hfIdEmpleadoSeleccionado = ((HiddenField)ItemSeleccionado.FindControl("hfIdEmpleado")).Value.ToString();
            lbIdRegistroSeleccionado.Text = hfIdEmpleadoSeleccionado.ToString();
            lbReporteUnico.Text = "1";

            var BuscarRegistroSeleccionado = ObjDataEmpresa.Value.BuscaEmpleados(
                lbIdRegistroSeleccionado.Text);
            lbCantidadregistrosVariable.Text = "1";
     
            foreach (var n in BuscarRegistroSeleccionado) {
                DateTime FechaEntrada = Convert.ToDateTime(n.FechaIngreso0);
                DateTime FechaNacimiento = Convert.ToDateTime(n.FechaNacimiento0);
                txtNombreMantenimiento.Text = n.Nombre;
                txtApellidoMAntenimiento.Text = n.Apellido;
                CargarListaDespTipoIdentificacionMantenimient();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarTipoIdentificacionMantenimiento, n.IdTipoIdentificacion.ToString());
                txtNumeroidentificacionMantenimiento.Text = n.NumeroIdentificacion;
                CargarListaNacionalidadMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarNacionalidadMantenimiento, n.IdNacionalidad.ToString());
                CargarListaSexoMantenimient();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarSexoMantenimiento, n.IdSexo.ToString());
                txtxNSSMantenimiento.Text = n.NSS;
                txtEmailMantenimiento.Text = n.Email;
                CargarListaEstadoCivilMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarEstadiCivilMantenimiento, n.IdEstadoCivil.ToString());
                txtFechaIngresoMantenimiento.Text = FechaEntrada.ToString("yyyy-MM-dd");
                CargarListaDepartamentoMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarDepartamentoMantenimiento, n.IdDepartamento.ToString());
                CargarListaCargosMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarCargoMantenimiento, n.IdCargo.ToString());
                CargarListaTipoEmpleadoMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarTipoEmpleadoMantenimiento, n.IdTipoEmpleado.ToString());
                CargarListaTipoNominaMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarTipoNominaMantenimiento, n.IdTioNomina.ToString());
                txtTelefonoMantenimiento.Text = n.Telefono1;
                txtSegundoTelefonoMantenimiento.Text = n.Telefono2;
                txtFechaNacimientoMantenimiento.Text = FechaNacimiento.ToString("yyyy-MM-dd");
                CargarListaFormaPagoMantenimiento();
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarFormaPagoMantenimiento, n.IdFormaPago.ToString());
                txtSueldoMantenimiento.Text = n.Sueldo.ToString();
                txtOtrosIngresosMantenimiento.Text = n.OtrosIngresos.ToString();
                txtDireccionMAntenimiento.Text = n.Direccion;
                txtPorcientoComisionVentasMantenimiento.Text = n.PorcientoCOmisionVentas.ToString();
                txtPorcientoComisionServicioMantenimiento.Text = n.PorcientoComsiionServicio.ToString();
                cbEstatus.Checked = (n.Estatus0.HasValue ? n.Estatus0.Value : false);
                cbAplicaParaComision.Checked = (n.AplicaParaComision0.HasValue ? n.AplicaParaComision0.Value : false);
                cbLlevaFoto.Checked = (n.LlevaImagen0.HasValue ? n.LlevaImagen0.Value : false);
            }
            Paginar(ref rpListadoEmpleado, BuscarRegistroSeleccionado, 1);
            HandlePaging(ref dtPaginacion);
            ModoMantenimiento();
        }

        protected void LinkPrimeraPagina_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            MostrarListadoEmpleados();

        }

        protected void LinkAnterior_Click(object sender, EventArgs e)
        {
            CurrentPage += -1;
            MostrarListadoEmpleados();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.PaginaAnterior);
        }

        protected void dtPaginacion_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            MostrarListadoEmpleados();
        }

        protected void dtPaginacion_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void LinkSiguiente_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            MostrarListadoEmpleados();
        }

        protected void LinkUltimo_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            MostrarListadoEmpleados();
            MoverValoresPaginacion((int)OpcionesPaginacionValores.UltimaPagina);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            MANEmpleados(0, "INSERT");
            ClientScript.RegisterStartupScript(GetType(), "RegistroGuardado()", "RegistroGuardado();", true);
            CurrentPage = 0;
            RestablecerPantalla();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            string _ClaveSeguridad = string.IsNullOrEmpty(txtClaveSeguridadMantenimiento.Text.Trim()) ? null : txtClaveSeguridadMantenimiento.Text.Trim();

            var ValidarClaveSeguridad = ObjDataSeguridad.Value.BuscaClaveSeguridad(
                new Nullable<decimal>(),
                null,
                DSMarketWeb.Logic.Comunes.SeguridadEncriptacion.Encriptar(_ClaveSeguridad));
            if (ValidarClaveSeguridad.Count() < 1) { }
            else {
                MANEmpleados(Convert.ToDecimal(lbIdRegistroSeleccionado.Text), "UPDATE");
                ClientScript.RegisterStartupScript(GetType(), "RegistroModificado()", "RegistroModificado();", true);
                CurrentPage = 0;
                RestablecerPantalla();
            }
        }

        protected void ddlSeleccionarDepartamentoMantenimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarListaCargosMantenimiento();
        }

        protected void cbAplicaParaComision_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAplicaParaComision.Checked == true) {
                txtPorcientoComisionServicioMantenimiento.Enabled = true;
                txtPorcientoComisionVentasMantenimiento.Enabled = true;

                txtPorcientoComisionServicioMantenimiento.Text = string.Empty;
                txtPorcientoComisionVentasMantenimiento.Text = string.Empty;
            }
            else {
                txtPorcientoComisionServicioMantenimiento.Enabled = false;
                txtPorcientoComisionVentasMantenimiento.Enabled = false;
                txtPorcientoComisionVentasMantenimiento.Text = "0";
                txtPorcientoComisionServicioMantenimiento.Text = "0";
            }
        }

        protected void cbLlevaFoto_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLlevaFoto.Checked == true) {
                DivBloqueImagenEmpleado.Visible = true;
            }
            else {
                DivBloqueImagenEmpleado.Visible = false;
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            RestablecerPantalla();
        }
    }
}