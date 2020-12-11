using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DSMarketWeb.Solution.Paginas.Inventario
{
    public partial class MantenimientoProductosServicios : System.Web.UI.Page
    {
        Lazy<DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion> ObjDataConfiguracion = new Lazy<Logic.Logica.LogicaConfiguracion.LogicaConfiguracion>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario> ObjDataInventario = new Lazy<Logic.Logica.LogicaInventario.LogicaInventario>();
        Lazy<DSMarketWeb.Logic.Logica.LogicaSeguridad.LogicaSeguridad> ObjDataSeguridad = new Lazy<Logic.Logica.LogicaSeguridad.LogicaSeguridad>();
        private void GraficarEstadistica() {
          //  decimal[] CapitalInvertido[2]

            /*
               decimal[] MontoFacturado = new decimal[10];
            string[] NombreSupervisor = new string[10];
            int Contador = 0;

            decimal IdUsuario = Convert.ToDecimal(Session["IdUsuario"]);

            string Estatus = "";
            if (rbTodas.Checked == true)
            {
                Estatus = "NADA";
            }
            else if (rbActivas.Checked == true)
            {
                Estatus = "ACTIVO";
            }
            else if (rbCanceladas.Checked == true)
            {
                Estatus = "CANCELADA";
            }

            string _Supervisor = "";
            if (string.IsNullOrEmpty(txtCodigoSupervisor.Text.Trim()))
            {
                _Supervisor = "0";
            }
            else
            {
                _Supervisor = string.IsNullOrEmpty(txtCodigoSupervisor.Text.Trim()) ? null : txtCodigoSupervisor.Text.Trim();
            }


            string _Intermediario = "";
            if (string.IsNullOrEmpty(txtCodigoIntermediario.Text.Trim()))
            {
                _Intermediario = "0";
            }
            else
            {
                _Intermediario = string.IsNullOrEmpty(txtCodigoIntermediario.Text.Trim()) ? null : txtCodigoIntermediario.Text.Trim();
            }



            int? _Oficina = ddlSeleccionaroficina.SelectedValue != "-1" ? Convert.ToInt32(ddlSeleccionaroficina.SelectedValue) : new Nullable<int>();

            if (_Oficina == null)
            {
                _Oficina = 0;
            }

            int? _Ramo = ddlSeleccionarRamo.SelectedValue != "-1" ? Convert.ToInt32(ddlSeleccionarRamo.SelectedValue) : new Nullable<int>();
            if (_Ramo == null)
            {
                _Ramo = 0;
            }



            SqlConnection Conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UtilidadesAmigosConexion"].ConnectionString);
            SqlCommand Query = new SqlCommand("EXEC [Utililades].[SP_GENERAR_GRAFICOS_PRODUCCION] @IdUsuario,@Estatus,@FechaDesde,@FechaHasta,@CodigoSupervisor,@CodigoIntermediario,@Oficina,@Ramo,@TipoGrafico", Conexion);
            Query.Parameters.AddWithValue("@IdUsuario", SqlDbType.Decimal);
            Query.Parameters.AddWithValue("@Estatus", SqlDbType.VarChar);
            Query.Parameters.AddWithValue("@FechaDesde", SqlDbType.Date);
            Query.Parameters.AddWithValue("@FechaHasta", SqlDbType.Date);
            Query.Parameters.AddWithValue("@CodigoSupervisor", SqlDbType.VarChar);
            Query.Parameters.AddWithValue("@CodigoIntermediario", SqlDbType.VarChar);
            Query.Parameters.AddWithValue("@Oficina", SqlDbType.Int);
            Query.Parameters.AddWithValue("@Ramo", SqlDbType.Int);
            Query.Parameters.AddWithValue("@TipoGrafico", SqlDbType.Int);


            Query.Parameters["@IdUsuario"].Value = (decimal)Session["IdUsuario"];
            Query.Parameters["@Estatus"].Value = Estatus;
            Query.Parameters["@FechaDesde"].Value = Convert.ToDateTime(txtFechaDesde.Text);
            Query.Parameters["@FechaHasta"].Value = Convert.ToDateTime(txtFechaHasta.Text);
            Query.Parameters["@CodigoSupervisor"].Value = _Supervisor;
            Query.Parameters["@CodigoIntermediario"].Value = _Intermediario;
            Query.Parameters["@Oficina"].Value = _Oficina;
            Query.Parameters["@Ramo"].Value = _Ramo;
            Query.Parameters["@TipoGrafico"].Value = 1;

            Conexion.Open();

            SqlDataReader Reader = Query.ExecuteReader();
            while (Reader.Read())
            {
                MontoFacturado[Contador] = Convert.ToDecimal(Reader.GetDecimal(1));
                NombreSupervisor[Contador] = Reader.GetString(0);
                Contador++;
            }
            Reader.Close();
            Conexion.Close();
            GraSupervisores.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0,}k";
            GraSupervisores.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            GraSupervisores.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
            GraSupervisores.ChartAreas["ChartArea1"].AxisX.Interval = 1;

            GraSupervisores.Series["Serie"].Points.DataBindXY(NombreSupervisor, MontoFacturado);
             
             */
        }
        private void GenerarNumeroAleatorio() {
            Random NumeroAleatorio = new Random();
            int Numero = NumeroAleatorio.Next(0, 999999999);
            lbNumeroRegistroVariable.Text = Numero.ToString();
        }

        #region LISTAS DESPLEGABLES DE LA PANTALLA DE CONSULTA
        private void ListadoTipoProductoConsulta()
        {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoProductoConsulta, ObjDataConfiguracion.Value.BuscaListas("TIPOPRODUCTO", null, null), true);
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCategoriaConsulta, ObjDataConfiguracion.Value.BuscaListas("CATEGORIAS", ddlSeleccionarTipoProductoConsulta.SelectedValue.ToString(), null), true);
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarMarcaConsulta, ObjDataConfiguracion.Value.BuscaListas("MARCAS", ddlSeleccionarTipoProductoConsulta.SelectedValue.ToString(), ddlSeleccionarCategoriaConsulta.SelectedValue.ToString()), true);
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarModeloConsulta, ObjDataConfiguracion.Value.BuscaListas("MODELOS", ddlSeleccionarTipoProductoConsulta.SelectedValue.ToString(), ddlSeleccionarCategoriaConsulta.SelectedValue.ToString(), ddlSeleccionarMarcaConsulta.SelectedValue.ToString()), true);
        }
        private void ListadoCategoriasConsultas()
        {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCategoriaConsulta, ObjDataConfiguracion.Value.BuscaListas("CATEGORIAS", ddlSeleccionarTipoProductoConsulta.SelectedValue.ToString(), null), true);
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarMarcaConsulta, ObjDataConfiguracion.Value.BuscaListas("MARCAS", ddlSeleccionarTipoProductoConsulta.SelectedValue.ToString(), ddlSeleccionarCategoriaConsulta.SelectedValue.ToString()), true);
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarModeloConsulta, ObjDataConfiguracion.Value.BuscaListas("MODELOS", ddlSeleccionarTipoProductoConsulta.SelectedValue.ToString(), ddlSeleccionarCategoriaConsulta.SelectedValue.ToString(), ddlSeleccionarMarcaConsulta.SelectedValue.ToString()), true);
        }
        private void ListadoMarcasConsultas()
        {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarMarcaConsulta, ObjDataConfiguracion.Value.BuscaListas("MARCAS", ddlSeleccionarTipoProductoConsulta.SelectedValue.ToString(), ddlSeleccionarCategoriaConsulta.SelectedValue.ToString()), true);
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarModeloConsulta, ObjDataConfiguracion.Value.BuscaListas("MODELOS", ddlSeleccionarTipoProductoConsulta.SelectedValue.ToString(), ddlSeleccionarCategoriaConsulta.SelectedValue.ToString(), ddlSeleccionarMarcaConsulta.SelectedValue.ToString()), true);
        }
        private void ListadoModelosConsulta()
        {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarModeloConsulta, ObjDataConfiguracion.Value.BuscaListas("MODELOS", ddlSeleccionarTipoProductoConsulta.SelectedValue.ToString(), ddlSeleccionarCategoriaConsulta.SelectedValue.ToString(), ddlSeleccionarMarcaConsulta.SelectedValue.ToString()), true);
        }
        private void ListadoUnidadmedidaCOnsulta()
        {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarUnidadMedidaConsulta, ObjDataConfiguracion.Value.BuscaListas("UNIDADMEDIDA", null, null), true);
        }
        private void ListadoCapacidadCOnsulta()
        {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCapacidadConsulta, ObjDataConfiguracion.Value.BuscaListas("CAPACIDAD", null, null), true);
        }
        private void ListadoCondicionConsulta()
        {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCondicionConculta, ObjDataConfiguracion.Value.BuscaListas("CONDICION", null, null), true);
        }
        private void ListadoColoresConsulta()
        {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarColorConsulta, ObjDataConfiguracion.Value.BuscaListas("COLORES", null, null), true);
        }
        private void CargarListadosConsultas()
        {
            ListadoTipoProductoConsulta();
            ListadoCategoriasConsultas();
            ListadoMarcasConsultas();
            ListadoModelosConsulta();
            ListadoUnidadmedidaCOnsulta();
            ListadoCapacidadCOnsulta();
            ListadoCondicionConsulta();
            ListadoColoresConsulta();
        }
        #endregion

        #region LISTAS DESPLEGABLES DE LA PANTALLA DE MANTENIMIENTOS
        private void ListadoTipoProductoMantenimiento()
        {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoProductoMantenimiento, ObjDataConfiguracion.Value.BuscaListas("TIPOPRODUCTO", null, null));
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCategoriaMantenimiento, ObjDataConfiguracion.Value.BuscaListas("CATEGORIAS", ddlSeleccionarTipoProductoMantenimiento.SelectedValue.ToString(), null));
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlseleccionarMarca, ObjDataConfiguracion.Value.BuscaListas("MARCAS", ddlSeleccionarTipoProductoMantenimiento.SelectedValue.ToString(), ddlSeleccionarCategoriaMantenimiento.SelectedValue.ToString()));
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarModelo, ObjDataConfiguracion.Value.BuscaListas("MODELOS", ddlSeleccionarTipoProductoMantenimiento.SelectedValue.ToString(), ddlSeleccionarCategoriaMantenimiento.SelectedValue.ToString(), ddlseleccionarMarca.SelectedValue.ToString()));
        }
        private void ListadoUnidadmedidaMantenimiento()
        {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarUnidadMedidaMantenimiento, ObjDataConfiguracion.Value.BuscaListas("UNIDADMEDIDA", null, null));
        }
        private void ListadoCapacidadMantenimiento()
        {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCapacidadMantenimiento, ObjDataConfiguracion.Value.BuscaListas("CAPACIDAD", null, null));
        }
        private void ListadoCondicionMantenimiento()
        {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCondicionMantenimiento, ObjDataConfiguracion.Value.BuscaListas("CONDICION", null, null));
        }
        private void ListadoColoresMantenimiento()
        {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarColorMantenimiento, ObjDataConfiguracion.Value.BuscaListas("COLORES", null, null));
        }
        private void ListadoTipoSplidoresMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoSuplidor, ObjDataConfiguracion.Value.BuscaListas("TIPOSUPLIDOR", null, null));
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSuplidorMantenimiento, ObjDataConfiguracion.Value.BuscaListas("SUPLIDOR", ddlSeleccionarTipoSuplidor.SelectedValue, null));
        }
        private void ListadoSuplidoresMantenimiento() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSuplidorMantenimiento, ObjDataConfiguracion.Value.BuscaListas("SUPLIDOR", ddlSeleccionarTipoSuplidor.SelectedValue, null));
        }
        private void CargarListadosMantenimientos()
        {
            ListadoTipoProductoMantenimiento();
            ListadoUnidadmedidaMantenimiento();
            ListadoCapacidadMantenimiento();
            ListadoCondicionMantenimiento();
            ListadoColoresMantenimiento();
            ListadoTipoSplidoresMantenimiento();
            ListadoSuplidoresMantenimiento();
        }
        #endregion

        #region RESTABLECER PANTALLA
        private void RestablecerPantalla() {

            txtProductoConsulta.Text = string.Empty;
            txtcodigoBarraConsulta.Text = string.Empty;
            txtReferenciaConsulta.Text = string.Empty;
            txtNumeroSeguimientoConsulta.Text = string.Empty;
            cbAgregarRangoFechaConsulta.Checked = false;
            cbReporteInventarioCompleto.Checked = false;
            cbGraficar.Checked = false;
            CargarListadosConsultas();
            CargarListadosMantenimientos();

            txtNumeroSeguimientoMantenimiento.Text = string.Empty;
            txtPrecioCompraMantenimiento.Text = string.Empty;
            txtPrecioVentaMantenimiento.Text = string.Empty;
            txtCodigoBarraMantenimiento.Text = string.Empty;
            txtstockMantenimiento.Text = string.Empty;
            txtStockMinimoMantenimiento.Text = string.Empty;
            txtReferenciaMantenimiento.Text = string.Empty;
            txtPorcientoDescuentoMantenimiento.Text = string.Empty;
            txtDescripcionMantenimiento.Text = string.Empty;
            txtComentarioMantenimiento.Text = string.Empty;
            cbAcumulativoMantenimiento.Checked = false;
            cbAplicaImpuestoMantenimiento.Checked = false;

            MostrarListadoProductos();
            ClientScript.RegisterStartupScript(GetType(), "BotonesModoNormal()", "BotonesModoNormal();", true);
        }
        #endregion

        /// <summary>
        /// Este metodo es para configurar los controles para guardar un prducto.
        /// </summary>
        private void ControlesFacturarProductos() {
            lbTipoProductoMntenimiento.Enabled = true;
            ddlSeleccionarTipoProductoMantenimiento.Enabled = true;
            lbCategoriaMantenimiento.Enabled = true;
            ddlSeleccionarCategoriaMantenimiento.Enabled = true;
            lbSeleccionarUnidadMedida.Enabled = true;
            ddlSeleccionarUnidadMedidaMantenimiento.Enabled = true;
            lbMarcaMantenimiento.Enabled = true;
            ddlseleccionarMarca.Enabled = true;
            lbSeleccionarModeloMantenimiento.Enabled = true;
            ddlSeleccionarModelo.Enabled = true;
            lbSeleccionarColorMantenimiento.Enabled = true;
            ddlSeleccionarColorMantenimiento.Enabled = true;
            lbSeleccionarCapacidadMantenimiento.Enabled = true;
            ddlSeleccionarCapacidadMantenimiento.Enabled = true;
            lbSeleccionarCondicionMantenimiento.Enabled = true;
            ddlSeleccionarCondicionMantenimiento.Enabled = true;
            lbNumeroSeguimientoMantenimiento.Enabled = true;
            txtNumeroSeguimientoMantenimiento.Enabled = true;
            lbPrecioCompraMantenimiento.Enabled = true;
            txtPrecioCompraMantenimiento.Enabled = true;
            lbPrecioVentaMantenimiento.Enabled = true;
            txtPrecioVentaMantenimiento.Enabled = true;
            lbCodigoBarraMantenimiento.Enabled = true;
            txtCodigoBarraMantenimiento.Enabled = true;
            lbStockMantenimiento.Enabled = true;
            txtstockMantenimiento.Enabled = true;
            lbStockMinimoMantenimiento.Enabled = true;
            txtStockMinimoMantenimiento.Enabled = true;
            lbReferenciaMantenimiento.Enabled = true;
            txtReferenciaMantenimiento.Enabled = true;
            lbSeleccionarTipoSuplidorMantenimiento.Enabled = true;
            ddlSeleccionarTipoSuplidor.Enabled = true;
            lbSeleccionarSuplidorMantenimiento.Enabled = true;
            ddlSuplidorMantenimiento.Enabled = true;
            lbPorcientoDescuentoMantenimiento.Enabled = true;
            txtPorcientoDescuentoMantenimiento.Enabled = true;
            lbDescripcionMantenimiento.Enabled = true;
            txtDescripcionMantenimiento.Enabled = true;
            txtDescripcionMantenimiento.Enabled = false;
            lbComentarioMantenimiento.Enabled = true;
            txtComentarioMantenimiento.Enabled = true;
            cbAcumulativoMantenimiento.Enabled = true;
            txtstockMantenimiento.Text = "1";
            txtStockMinimoMantenimiento.Text = "1";
            txtDescripcionMantenimiento.Text = string.Empty;
            cbAcumulativoMantenimiento.Checked = false;
            txtstockMantenimiento.Enabled = false;
            txtStockMinimoMantenimiento.Enabled = false;

            txtNumeroSeguimientoMantenimiento.Text = string.Empty;
            txtPrecioCompraMantenimiento.Text = string.Empty;
            txtcodigoBarraConsulta.Text = string.Empty;
            txtReferenciaMantenimiento.Text = string.Empty;
        }

        /// <summary>
        /// Este metodo es para configurar los controles para guardar un servicio.
        /// </summary>
        private void ControlesFacturarServicios() {
            lbTipoProductoMntenimiento.Enabled = true;
            ddlSeleccionarTipoProductoMantenimiento.Enabled = true;
            lbCategoriaMantenimiento.Enabled = true;
            ddlSeleccionarCategoriaMantenimiento.Enabled = true;
            lbSeleccionarUnidadMedida.Enabled = false;
            ddlSeleccionarUnidadMedidaMantenimiento.Enabled = false;
            lbMarcaMantenimiento.Enabled = false;
            ddlseleccionarMarca.Enabled = false;
            lbSeleccionarModeloMantenimiento.Enabled = false;
            ddlSeleccionarModelo.Enabled = false;
            lbSeleccionarColorMantenimiento.Enabled = false;
            ddlSeleccionarColorMantenimiento.Enabled = false;
            lbSeleccionarCapacidadMantenimiento.Enabled = false;
            ddlSeleccionarCapacidadMantenimiento.Enabled = false;
            lbSeleccionarCondicionMantenimiento.Enabled = false;
            ddlSeleccionarCondicionMantenimiento.Enabled = false;
            lbNumeroSeguimientoMantenimiento.Enabled = false;
            txtNumeroSeguimientoMantenimiento.Enabled = false;
            lbPrecioCompraMantenimiento.Enabled = false;
            txtPrecioCompraMantenimiento.Enabled = false;
            lbPrecioVentaMantenimiento.Enabled = true;
            txtPrecioVentaMantenimiento.Enabled = true;
            lbCodigoBarraMantenimiento.Enabled = false;
            txtCodigoBarraMantenimiento.Enabled = false;
            lbStockMantenimiento.Enabled = false;
            txtstockMantenimiento.Enabled = false;
            lbStockMinimoMantenimiento.Enabled = false;
            txtStockMinimoMantenimiento.Enabled = false;
            lbReferenciaMantenimiento.Enabled = false;
            txtReferenciaMantenimiento.Enabled = false;
            lbSeleccionarTipoSuplidorMantenimiento.Enabled = false;
            ddlSeleccionarTipoSuplidor.Enabled = false;
            lbSeleccionarSuplidorMantenimiento.Enabled = false;
            ddlSuplidorMantenimiento.Enabled = false;
            lbPorcientoDescuentoMantenimiento.Enabled = true;
            txtPorcientoDescuentoMantenimiento.Enabled = true;
            lbDescripcionMantenimiento.Enabled = true;
            txtDescripcionMantenimiento.Enabled = true;
            txtDescripcionMantenimiento.Enabled = true;
            lbComentarioMantenimiento.Enabled = true;
            txtComentarioMantenimiento.Enabled = true;
            cbAcumulativoMantenimiento.Enabled = false;

            txtstockMantenimiento.Text = "1";
            txtStockMinimoMantenimiento.Text = "1";
            txtDescripcionMantenimiento.Text = string.Empty;
            cbAcumulativoMantenimiento.Checked = false;

            txtNumeroSeguimientoMantenimiento.Text = "0";
            txtPrecioCompraMantenimiento.Text = "0";
            txtcodigoBarraConsulta.Text = "0";
            txtReferenciaMantenimiento.Text = "0";
        }






        private void MostrarListadoProductos() {
            string _Producto = string.IsNullOrEmpty(txtProductoConsulta.Text.Trim()) ? null : txtProductoConsulta.Text.Trim();
            string _CodigoBarra = string.IsNullOrEmpty(txtcodigoBarraConsulta.Text.Trim()) ? null : txtcodigoBarraConsulta.Text.Trim();
            string _Referencia = string.IsNullOrEmpty(txtReferenciaConsulta.Text.Trim()) ? null : txtReferenciaConsulta.Text.Trim();
            decimal? _TipoProducto = ddlSeleccionarTipoProductoConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarTipoProductoConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _Categoria = ddlSeleccionarCategoriaConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarCategoriaConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _UnidadMedida = ddlSeleccionarUnidadMedidaConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarUnidadMedidaConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _Marca = ddlSeleccionarMarcaConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarMarcaConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _Modelo = ddlSeleccionarModeloConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarModeloConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _Color = ddlSeleccionarColorConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarColorConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _Capacidad = ddlSeleccionarCapacidadConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarCapacidadConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _Condicion = ddlSeleccionarCondicionConculta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarCondicionConculta.SelectedValue) : new Nullable<decimal>();
            string _NumeroSeguimiento = string.IsNullOrEmpty(txtNumeroSeguimientoConsulta.Text.Trim()) ? null : txtNumeroSeguimientoConsulta.Text.Trim();

            if (cbAgregarRangoFechaConsulta.Checked == true) {

                if (string.IsNullOrEmpty(txtFechaDesdeConsulta.Text.Trim()) || string.IsNullOrEmpty(txtFechaHastaConsulta.Text.Trim())) {
                    ClientScript.RegisterStartupScript(GetType(), "CamposFechaVacios()", "CamposFechaVacios();", true);
                    if (string.IsNullOrEmpty(txtFechaDesdeConsulta.Text.Trim())) {
                        ClientScript.RegisterStartupScript(GetType(), "CampoFechaDesdeVacio()", "CampoFechaDesdeVacio();", true);
                    }
                    if (string.IsNullOrEmpty(txtFechaHastaConsulta.Text.Trim())) {
                        ClientScript.RegisterStartupScript(GetType(), "CampoFechaHastaVacio()", "CampoFechaHastaVacio();", true);
                    }
                }
                else
                {
                    var Buscar = ObjDataInventario.Value.BuscaProductos(
                        new Nullable<decimal>(),
                        null,
                        _Producto,
                        _CodigoBarra,
                        _Referencia,
                        Convert.ToDateTime(txtFechaDesdeConsulta.Text),
                        Convert.ToDateTime(txtFechaHastaConsulta.Text),
                        _TipoProducto,
                        _Categoria,
                        _UnidadMedida,
                        _Marca,
                        _Modelo,
                        _Color,
                        _Capacidad,
                        _Condicion,
                        null, false,
                        _NumeroSeguimiento);
                    decimal CapitalInvertido = 0, GananciaAproximada = 0;
                  //  int TotalProductos = 0, TotalServicios = 0;
                    foreach (var n in Buscar) {
                        CapitalInvertido = Convert.ToDecimal(n.CapilalInvertido);
                        GananciaAproximada = Convert.ToDecimal(n.GananciaAproximada);
             
                    }
                    lbCapitalInvertidoVariable.Text = CapitalInvertido.ToString("N2");
                    lbGananciaAproximadaVariable.Text = GananciaAproximada.ToString("N2");
                    gvListado.DataSource = Buscar;
                    gvListado.DataBind();
                }

            }
            else {
                var Buscar = ObjDataInventario.Value.BuscaProductos(
                       new Nullable<decimal>(),
                       null,
                       _Producto,
                       _CodigoBarra,
                       _Referencia,
                       null,
                       null,
                       _TipoProducto,
                       _Categoria,
                       _UnidadMedida,
                       _Marca,
                       _Modelo,
                       _Color,
                       _Capacidad,
                       _Condicion,
                       null, false,
                       _NumeroSeguimiento);
                decimal CapitalInvertido = 0, GananciaAproximada = 0;
                //  int TotalProductos = 0, TotalServicios = 0;
                foreach (var n in Buscar)
                {
                    CapitalInvertido = Convert.ToDecimal(n.CapilalInvertido);
                    GananciaAproximada = Convert.ToDecimal(n.GananciaAproximada);

                }
                lbCapitalInvertidoVariable.Text = CapitalInvertido.ToString("N2");
                lbGananciaAproximadaVariable.Text = GananciaAproximada.ToString("N2");
                gvListado.DataSource = Buscar;
                gvListado.DataBind();
            }
        }
        private void ExportarRegistrosExel() {
            string _Producto = string.IsNullOrEmpty(txtProductoConsulta.Text.Trim()) ? null : txtProductoConsulta.Text.Trim();
            string _CodigoBarra = string.IsNullOrEmpty(txtcodigoBarraConsulta.Text.Trim()) ? null : txtcodigoBarraConsulta.Text.Trim();
            string _Referencia = string.IsNullOrEmpty(txtReferenciaConsulta.Text.Trim()) ? null : txtReferenciaConsulta.Text.Trim();
            decimal? _TipoProducto = ddlSeleccionarTipoProductoConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarTipoProductoConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _Categoria = ddlSeleccionarCategoriaConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarCategoriaConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _UnidadMedida = ddlSeleccionarUnidadMedidaConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarUnidadMedidaConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _Marca = ddlSeleccionarMarcaConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarMarcaConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _Modelo = ddlSeleccionarModeloConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarModeloConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _Color = ddlSeleccionarColorConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarColorConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _Capacidad = ddlSeleccionarCapacidadConsulta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarCapacidadConsulta.SelectedValue) : new Nullable<decimal>();
            decimal? _Condicion = ddlSeleccionarCondicionConculta.SelectedValue != "-1" ? Convert.ToDecimal(ddlSeleccionarCondicionConculta.SelectedValue) : new Nullable<decimal>();
            string _NumeroSeguimiento = string.IsNullOrEmpty(txtNumeroSeguimientoConsulta.Text.Trim()) ? null : txtNumeroSeguimientoConsulta.Text.Trim();

            if (cbAgregarRangoFechaConsulta.Checked == true) {
                if (string.IsNullOrEmpty(txtFechaDesdeConsulta.Text.Trim()) || string.IsNullOrEmpty(txtFechaHastaConsulta.Text.Trim()))
                {
                    ClientScript.RegisterStartupScript(GetType(), "CamposFechaVacios()", "CamposFechaVacios();", true);
                    if (string.IsNullOrEmpty(txtFechaDesdeConsulta.Text.Trim()))
                    {
                        ClientScript.RegisterStartupScript(GetType(), "CampoFechaDesdeVacio()", "CampoFechaDesdeVacio();", true);
                    }
                    if (string.IsNullOrEmpty(txtFechaHastaConsulta.Text.Trim()))
                    {
                        ClientScript.RegisterStartupScript(GetType(), "CampoFechaHastaVacio()", "CampoFechaHastaVacio();", true);
                    }
                }
                else
                {
                    var ExportarRegistrosExel = (from n in ObjDataInventario.Value.BuscaProductos(
                        new Nullable<decimal>(),
                        null,
                        _Producto,
                        _CodigoBarra,
                        _Referencia,
                        Convert.ToDateTime(txtFechaDesdeConsulta.Text),
                        Convert.ToDateTime(txtFechaHastaConsulta.Text),
                        _TipoProducto,
                        _Categoria,
                        _UnidadMedida,
                        _Marca,
                        _Modelo,
                        _Color,
                        _Capacidad,
                        _Condicion,
                        null, false,
                        _NumeroSeguimiento)
                                                 select new
                                                 {
                                                     IdProducto=n.IdProducto,
                                                     Producto=n.Producto,
                                                     Referencia=n.Referencia,
                                                     TipoProducto=n.TipoProducto,
                                                     Categoria=n.Categoria,
                                                     UnidadMedida=n.UnidadMedida,
                                                     TipoSuplidor=n.TipoSuplidor,
                                                     Suplidor=n.Suplidor,
                                                     CodigoBarra=n.CodigoBarra,
                                                     PrecioCompra=n.PrecioCompra,
                                                     PrecioVenta=n.PrecioVenta,
                                                     Stock=n.Stock,
                                                     StockMinimo=n.StockMinimo,
                                                     PorcientoDescuento=n.PorcientoDescuento,
                                                     ProductoAcumulativo=n.ProductoAcumulativo,
                                                     CreadoPor=n.CreadoPor,
                                                     FechaCreado=n.FechaCreado,
                                                     ModificadoPor=n.ModificadoPor,
                                                     FechaModificado=n.FechaModificado,
                                                     Fecha=n.Fecha,
                                                     EstatusProducto=n.EstatusProducto,
                                                     AplicaParaImpuesto=n.AplicaParaImpuesto,
                                                     NumeroSeguimiento=n.NumeroSeguimiento
                                                 }).ToList();

                    DSMarketWeb.Logic.Comunes.Exportar.exporttoexcel("Listado de Productos", ExportarRegistrosExel);
                }


            }
            else {
                var ExportarRegistrosExel = (from n in ObjDataInventario.Value.BuscaProductos(
                         new Nullable<decimal>(),
                         null,
                         _Producto,
                         _CodigoBarra,
                         _Referencia,
                         null,
                         null,
                         _TipoProducto,
                         _Categoria,
                         _UnidadMedida,
                         _Marca,
                         _Modelo,
                         _Color,
                         _Capacidad,
                         _Condicion,
                         null, false,
                         _NumeroSeguimiento)
                                             select new
                                             {
                                                 IdProducto = n.IdProducto,
                                                 Producto = n.Producto,
                                                 Referencia = n.Referencia,
                                                 TipoProducto = n.TipoProducto,
                                                 Categoria = n.Categoria,
                                                 UnidadMedida = n.UnidadMedida,
                                                 TipoSuplidor = n.TipoSuplidor,
                                                 Suplidor = n.Suplidor,
                                                 CodigoBarra = n.CodigoBarra,
                                                 PrecioCompra = n.PrecioCompra,
                                                 PrecioVenta = n.PrecioVenta,
                                                 Stock = n.Stock,
                                                 StockMinimo = n.StockMinimo,
                                                 PorcientoDescuento = n.PorcientoDescuento,
                                                 ProductoAcumulativo = n.ProductoAcumulativo,
                                                 CreadoPor = n.CreadoPor,
                                                 FechaCreado = n.FechaCreado,
                                                 ModificadoPor = n.ModificadoPor,
                                                 FechaModificado = n.FechaModificado,
                                                 Fecha = n.Fecha,
                                                 EstatusProducto = n.EstatusProducto,
                                                 AplicaParaImpuesto = n.AplicaParaImpuesto,
                                                 NumeroSeguimiento = n.NumeroSeguimiento
                                             }).ToList();

                DSMarketWeb.Logic.Comunes.Exportar.exporttoexcel("Listado de Productos", ExportarRegistrosExel);
            }
        }


        /// <summary>
        /// Este metodo es para mostrar los controles del detalle
        /// </summary>
        private void MostrarControlesDetalle() {
            lbProductoDetalle.Visible = true;
            txtProductoDetalle.Visible = true;
            lbTipoProductoDetalle.Visible = true;
            txtTipoProductoDetalle.Visible = true;
            lbCategoriaDetalle.Visible = true;
            txtCategoriaDetalle.Visible = true;
            lbUnidadMedidaDetalle.Visible = true;
            txtUnidadMedidaDetalle.Visible = true;
            lbTipoSuplidorDetalle.Visible = true;
            txtTipoSuplidorDetalle.Visible = true;
            lbSuplidorDetalle.Visible = true;
            txtSuplidorDetalle.Visible = true;
            lbCodigoBarraDetalle.Visible = true;
            txtCodigoBarraDetalle.Visible = true;
            lbReferenciaDetalle.Visible = true;
            txtReferenciaDetalle.Visible = true;
            lbStockDetalle.Visible = true;
            txtStockDetalle.Visible = true;
            lbStockMinimoDetalle.Visible = true;
            txtStockMinimoDetalle.Visible = true;
            lbPrecioCompraDetalle.Visible = true;
            txtPrecioCompraDetalle.Visible = true;
            lbPrecioVentaDetalle.Visible = true;
            txtPrecioVentaDetalle.Visible = true;
            lbAcumulativoDetalle.Visible = true;
            txtAcumulativoDetalle.Visible = true;
            lbAplicaDescuentoDetalle.Visible = true;
            txtAplicaDescuentoDetalle.Visible = true;
            lbPorcientoDescuentoDetalle.Visible = true;
            txtPorcientoDescuentoDetalle.Visible = true;
            lbComentarioDetalle.Visible = true;
            txtComentarioDetalle.Visible = true;
            btnRegresarDetalle.Visible = true;
        }
        /// <summary>
        /// Este metodo es para ocultar y limpiar los controles del detalle
        /// </summary>
        private void OcultarControlesDetalle() {
            lbProductoDetalle.Visible = false;
            txtProductoDetalle.Visible = false;
            lbTipoProductoDetalle.Visible = false;
            txtTipoProductoDetalle.Visible = false;
            lbCategoriaDetalle.Visible = false;
            txtCategoriaDetalle.Visible = false;
            lbUnidadMedidaDetalle.Visible = false;
            txtUnidadMedidaDetalle.Visible = false;
            lbTipoSuplidorDetalle.Visible = false;
            txtTipoSuplidorDetalle.Visible = false;
            lbSuplidorDetalle.Visible = false;
            txtSuplidorDetalle.Visible = false;
            lbCodigoBarraDetalle.Visible = false;
            txtCodigoBarraDetalle.Visible = false;
            lbReferenciaDetalle.Visible = false;
            txtReferenciaDetalle.Visible = false;
            lbStockDetalle.Visible = false;
            txtStockDetalle.Visible = false;
            lbStockMinimoDetalle.Visible = false;
            txtStockMinimoDetalle.Visible = false;
            lbPrecioCompraDetalle.Visible = false;
            txtPrecioCompraDetalle.Visible = false;
            lbPrecioVentaDetalle.Visible = false;
            txtPrecioVentaDetalle.Visible = false;
            lbAcumulativoDetalle.Visible = false;
            txtAcumulativoDetalle.Visible = false;
            lbAplicaDescuentoDetalle.Visible = false;
            txtAplicaDescuentoDetalle.Visible = false;
            lbPorcientoDescuentoDetalle.Visible = false;
            txtPorcientoDescuentoDetalle.Visible = false;
            lbComentarioDetalle.Visible = false;
            txtComentarioDetalle.Visible = false;
            btnRegresarDetalle.Visible = false;



            //LIMPIAMOS LOS CONTROLES
            txtProductoDetalle.Text = string.Empty;
            txtTipoProductoDetalle.Text = string.Empty;
            txtCategoriaDetalle.Text = string.Empty;
            txtUnidadMedidaDetalle.Text = string.Empty;
            txtTipoSuplidorDetalle.Text = string.Empty;
            txtSuplidorDetalle.Text = string.Empty;
            txtCodigoBarraDetalle.Text = string.Empty;
            txtReferenciaDetalle.Text = string.Empty;
            txtStockDetalle.Text = string.Empty;
            txtStockMinimoDetalle.Text = string.Empty;
            txtPrecioCompraDetalle.Text = string.Empty;
            txtPrecioVentaDetalle.Text = string.Empty;
            txtAcumulativoDetalle.Text = string.Empty;
            txtAplicaDescuentoDetalle.Text = string.Empty;
            txtPorcientoDescuentoDetalle.Text = string.Empty;
            txtComentarioDetalle.Text = string.Empty;
        }


        #region MANTENIMIENTO DE PRODUCTOS
        private void MantenimientoProducto(decimal IdProducto,decimal NumeroConector,decimal IdUsuario, string Accion) {
            try {
                DSMarketWeb.Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionProductos Procesar = new Logic.PrcesarMantenimientos.Inventario.ProcesarInformacionProductos(
                    IdProducto,
                    NumeroConector,
                    Convert.ToDecimal(ddlSeleccionarTipoProductoMantenimiento.SelectedValue),
                    Convert.ToDecimal(ddlSeleccionarCategoriaMantenimiento.SelectedValue),
                    Convert.ToDecimal(ddlSeleccionarUnidadMedidaMantenimiento.SelectedValue),
                    Convert.ToDecimal(ddlseleccionarMarca.SelectedValue),
                    Convert.ToDecimal(ddlSeleccionarModelo.SelectedValue),
                    Convert.ToDecimal(ddlSeleccionarTipoSuplidor.SelectedValue),
                    Convert.ToDecimal(ddlSuplidorMantenimiento.SelectedValue),
                    txtDescripcionMantenimiento.Text,
                    txtCodigoBarraMantenimiento.Text,
                    txtReferenciaMantenimiento.Text,
                    Convert.ToDecimal(txtPrecioCompraMantenimiento.Text),
                    Convert.ToDecimal(txtPrecioVentaMantenimiento.Text),
                    Convert.ToDecimal(txtstockMantenimiento.Text),
                    Convert.ToDecimal(txtStockMinimoMantenimiento.Text),
                    Convert.ToDecimal(txtPorcientoDescuentoMantenimiento.Text),
                    false,
                    cbAcumulativoMantenimiento.Checked,
                    false,
                    IdUsuario,
                    DateTime.Now,
                    IdUsuario,
                    DateTime.Now,
                    DateTime.Now,
                    txtComentarioMantenimiento.Text,
                    cbAplicaImpuestoMantenimiento.Checked,
                    false,
                    txtNumeroSeguimientoMantenimiento.Text,
                    Convert.ToDecimal(ddlSeleccionarColorMantenimiento.SelectedValue),
                    Convert.ToDecimal(ddlSeleccionarCondicionMantenimiento.SelectedValue),
                    Convert.ToDecimal(ddlSeleccionarCapacidadMantenimiento.SelectedValue),
                    Accion);
                Procesar.ProcesarProducto();

                if (Accion == "UPDATE") {
                    RestablecerPantalla();
                }
            }
            catch (Exception ex) {
                string MensajeError = "Error al realizar el mantenimiento de prducto por la razon de " + ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + MensajeError + "');", true);
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                lbIdUsuario.Text = Session["IdUsuario"].ToString();
                Panel1.BackColor = System.Drawing.Color.Red;
                lbRegistroDisponible.ForeColor = System.Drawing.Color.Green;
                CargarListadosConsultas();
                CargarListadosMantenimientos();
                GenerarNumeroAleatorio();
                ClientScript.RegisterStartupScript(GetType(), "BotonesModoNormal()", "BotonesModoNormal();", true);
            }
        }

        protected void cbAgregarRangoFechaConsulta_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAgregarRangoFechaConsulta.Checked == true) {
                txtFechaDesdeConsulta.Enabled = true;
                txtFechaHastaConsulta.Enabled = true;
            }
            else {
                txtFechaDesdeConsulta.Enabled = false;
                txtFechaHastaConsulta.Enabled = false;
            }
            ClientScript.RegisterStartupScript(GetType(), "BotonesModoNormal()", "BotonesModoNormal();", true);
        }

        protected void ddlSeleccionarTipoProductoConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCategoriaConsulta, ObjDataConfiguracion.Value.BuscaListas("CATEGORIAS", ddlSeleccionarTipoProductoConsulta.SelectedValue.ToString(), null), true);
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarMarcaConsulta, ObjDataConfiguracion.Value.BuscaListas("MARCAS", ddlSeleccionarTipoProductoConsulta.SelectedValue.ToString(), ddlSeleccionarCategoriaConsulta.SelectedValue.ToString()), true);
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarModeloConsulta, ObjDataConfiguracion.Value.BuscaListas("MODELOS", ddlSeleccionarTipoProductoConsulta.SelectedValue.ToString(), ddlSeleccionarCategoriaConsulta.SelectedValue.ToString(), ddlSeleccionarMarcaConsulta.SelectedValue.ToString()), true);
            ClientScript.RegisterStartupScript(GetType(), "BotonesModoNormal()", "BotonesModoNormal();", true);
        }

    

        protected void ddlSeleccionarMarcaConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarModeloConsulta, ObjDataConfiguracion.Value.BuscaListas("MODELOS", ddlSeleccionarTipoProductoConsulta.SelectedValue.ToString(), ddlSeleccionarCategoriaConsulta.SelectedValue.ToString(), ddlSeleccionarMarcaConsulta.SelectedValue.ToString()), true);
            ClientScript.RegisterStartupScript(GetType(), "BotonesModoNormal()", "BotonesModoNormal();", true);
        }

        protected void ddlSeleccionarCategoriaConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarMarcaConsulta, ObjDataConfiguracion.Value.BuscaListas("MARCAS", ddlSeleccionarTipoProductoConsulta.SelectedValue.ToString(), ddlSeleccionarCategoriaConsulta.SelectedValue.ToString()), true);
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarModeloConsulta, ObjDataConfiguracion.Value.BuscaListas("MODELOS", ddlSeleccionarTipoProductoConsulta.SelectedValue.ToString(), ddlSeleccionarCategoriaConsulta.SelectedValue.ToString(), ddlSeleccionarMarcaConsulta.SelectedValue.ToString()), true);
            ClientScript.RegisterStartupScript(GetType(), "BotonesModoNormal()", "BotonesModoNormal();", true);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            MostrarListadoProductos();
            ClientScript.RegisterStartupScript(GetType(), "BotonesModoNormal()", "BotonesModoNormal();", true);
        }

        protected void btnReporte_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "BotonesModoNormal()", "BotonesModoNormal();", true);
        }

        protected void btndetalle_Click(object sender, EventArgs e)
        {
            MostrarControlesDetalle();
        }

        protected void gvListado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvListado.PageIndex = e.NewPageIndex;
            MostrarListadoProductos();
            ClientScript.RegisterStartupScript(GetType(), "BotonesModoNormal()", "BotonesModoNormal();", true);
        }

        protected void gvListado_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gb = gvListado.SelectedRow;
            var ConsultarPorID = ObjDataInventario.Value.BuscaProductos(
                Convert.ToDecimal(gb.Cells[1].Text),
                Convert.ToDecimal(gb.Cells[2].Text));
            gvListado.DataSource = ConsultarPorID;
            gvListado.DataBind();
            decimal CapitalInvertido = 0, GananciaAproximada = 0;
            foreach (var n in ConsultarPorID) {
                //SACAMOS LOS DATOS DEL DETALLE

                txtProductoDetalle.Text = n.Producto;
                txtTipoProductoDetalle.Text = n.TipoProducto;
                txtCategoriaDetalle.Text = n.Categoria;
                txtUnidadMedidaDetalle.Text = n.UnidadMedida;
                txtTipoSuplidorDetalle.Text = n.TipoSuplidor;
                txtSuplidorDetalle.Text = n.Suplidor;
                txtCodigoBarraDetalle.Text = n.CodigoBarra;
                txtReferenciaDetalle.Text = n.Referencia;
                decimal stockDetalle = Convert.ToDecimal(n.Stock);
                txtStockDetalle.Text = stockDetalle.ToString("N0");
                decimal StockinimoDetalle = Convert.ToDecimal(n.StockMinimo);
                txtStockMinimoDetalle.Text = StockinimoDetalle.ToString("N0");
                decimal PrecioCompraDetalle = Convert.ToDecimal(n.PrecioCompra);
                txtPrecioCompraDetalle.Text = PrecioCompraDetalle.ToString("N2");
                decimal PrecioVentaDetalle = Convert.ToDecimal(n.PrecioVenta);
                txtPrecioVentaDetalle.Text = PrecioVentaDetalle.ToString("N2");
                txtAcumulativoDetalle.Text = n.ProductoAcumulativo;
                txtAplicaDescuentoDetalle.Text = n.AplicaParaImpuesto;
                txtPorcientoDescuentoDetalle.Text = n.PorcientoDescuento.ToString();
                txtComentarioDetalle.Text = n.Comentario;
                CapitalInvertido = Convert.ToDecimal(n.CapilalInvertido);
                GananciaAproximada = Convert.ToDecimal(n.GananciaAproximada);


                //SACAMOS LOS DATOS DEL PRODUCTOS SELECCIONADO
                #region SACAR LOS DATOS DEL PRODUCTO SELECCIONADO
                lbIdProducto.Text = gb.Cells[1].Text;
                lbNumeroConector.Text = gb.Cells[2].Text;
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarTipoProductoMantenimiento, n.IdTipoProducto.ToString());
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCategoriaMantenimiento, ObjDataConfiguracion.Value.BuscaListas("CATEGORIAS", ddlSeleccionarTipoProductoMantenimiento.SelectedValue.ToString(), null));
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarCategoriaMantenimiento, n.IdCategoria.ToString());
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarUnidadMedidaMantenimiento, n.IdUnidadMedida.ToString());                
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlseleccionarMarca, ObjDataConfiguracion.Value.BuscaListas("MARCAS", ddlSeleccionarTipoProductoMantenimiento.SelectedValue.ToString(), ddlSeleccionarCategoriaMantenimiento.SelectedValue.ToString()));
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlseleccionarMarca, n.IdMarca.ToString());           
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarModelo, ObjDataConfiguracion.Value.BuscaListas("MODELOS", ddlSeleccionarTipoProductoMantenimiento.SelectedValue.ToString(), ddlSeleccionarCategoriaMantenimiento.SelectedValue.ToString(), ddlseleccionarMarca.SelectedValue.ToString()));
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarModelo, n.IdModelo.ToString());
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarColorMantenimiento, n.IdColor.ToString());
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarCapacidadMantenimiento, n.IdCapacidad.ToString());
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarCondicionMantenimiento, n.IdCondicion.ToString());
                txtNumeroSeguimientoMantenimiento.Text = n.NumeroSeguimiento;
                txtPrecioCompraMantenimiento.Text = n.PrecioCompra.ToString();
                txtPrecioVentaMantenimiento.Text = n.PrecioVenta.ToString();
                txtCodigoBarraMantenimiento.Text = n.CodigoBarra;
                txtstockMantenimiento.Text = n.Stock.ToString();
                txtStockMinimoMantenimiento.Text = n.StockMinimo.ToString();
                txtReferenciaMantenimiento.Text = n.Referencia;
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSeleccionarTipoSuplidor, n.IdTipoSuplidor.ToString());
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSuplidorMantenimiento, ObjDataConfiguracion.Value.BuscaListas("SUPLIDOR", ddlSeleccionarTipoSuplidor.SelectedValue, null));
                DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListSeleccionar(ref ddlSuplidorMantenimiento, n.IdSuplidor.ToString());
                txtPorcientoDescuentoMantenimiento.Text = n.PorcientoDescuento.ToString();
                txtDescripcionMantenimiento.Text = n.Producto;
                txtComentarioMantenimiento.Text = n.Comentario;
                cbAplicaImpuestoMantenimiento.Checked = (n.AplicaParaImpuesto0.HasValue ? n.AplicaParaImpuesto0.Value : false);
                cbAcumulativoMantenimiento.Checked = (n.ProductoAcumulativo0.HasValue ? n.ProductoAcumulativo0.Value : false);
                #endregion
            }
           lbCapitalInvertidoVariable.Text = CapitalInvertido.ToString("N2");
           lbGananciaAproximadaVariable.Text = GananciaAproximada.ToString("N2");
           
           
           ClientScript.RegisterStartupScript(GetType(), "BotonesModoSelect()", "BotonesModoSelect();", true);
           }
           
           protected void btnRegresarDetalle_Click(object sender, EventArgs e)
           {
           OcultarControlesDetalle();
           ClientScript.RegisterStartupScript(GetType(), "BotonesModoNormal()", "BotonesModoNormal();", true);
           }
           
           protected void btnGuardarMantenimiento_Click(object sender, EventArgs e)
           {
                       lbIdProducto.Text = "0";
                       Random NumeroConector = new Random();
                       int Numero = NumeroConector.Next(0, 999999999);
                       MantenimientoProducto(Convert.ToDecimal(lbIdProducto.Text), Numero, Convert.ToDecimal(lbIdUsuario.Text), "INSERT");
                       string Mensaje = "Registro guardado con exito";
                       ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + Mensaje + "');", true);
                       if (cbNoLimpiarControles.Checked == true) { }
                       else {
                           RestablecerPantalla();
                       }
                       ClientScript.RegisterStartupScript(GetType(), "BotonesModoNormal()", "BotonesModoNormal();", true);
           
           }
           
           protected void btnModificarMantenimiento_Click(object sender, EventArgs e)
           {
            string _ClaveSeguridad = string.IsNullOrEmpty(txtClaveSeguridad.Text.Trim()) ? null : txtClaveSeguridad.Text.Trim();

            var VakudarCkave = ObjDataSeguridad.Value.BuscaClaveSeguridad(
                new Nullable<decimal>(),
                null,
                DSMarketWeb.Logic.Comunes.SeguridadEncriptacion.Encriptar(_ClaveSeguridad));
            if (VakudarCkave.Count() < 1) {
                ClientScript.RegisterStartupScript(GetType(), "ClaveSeguridadNoValida()", "ClaveSeguridadNoValida();", true);
            }
            else {
                MantenimientoProducto(Convert.ToDecimal(lbIdProducto.Text), Convert.ToDecimal(lbNumeroConector.Text), Convert.ToDecimal(lbIdUsuario.Text), "UPDATE");
                string Mensaje = "Registro Modificado con exito";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + Mensaje + "');", true);
                

            }
             ClientScript.RegisterStartupScript(GetType(), "BotonesModoNormal()", "BotonesModoNormal();", true);
        }
           
           protected void btnEliminarMantenimiento_Click(object sender, EventArgs e)
           {
            if (txtAccion.Value == "Eliminar Registro")
            {
                txtComentarioMantenimiento.Text = "Prueba";
            }
            else { 
            txtComentarioMantenimiento.Text="MAMAGUEBO";
            }
            
           ClientScript.RegisterStartupScript(GetType(), "BotonesModoNormal()", "BotonesModoNormal();", true);
           }
           
           protected void btnProcesarSuplr_Click(object sender, EventArgs e)
           {
           ClientScript.RegisterStartupScript(GetType(), "BotonesModoNormal()", "BotonesModoNormal();", true);
           }
           
           protected void btnProcesarDescartar_Click(object sender, EventArgs e)
           {
           ClientScript.RegisterStartupScript(GetType(), "BotonesModoNormal()", "BotonesModoNormal();", true);
           }
           
           protected void btnExportar_Click(object sender, EventArgs e)
           {
           ExportarRegistrosExel();
           ClientScript.RegisterStartupScript(GetType(), "BotonesModoNormal()", "BotonesModoNormal();", true);
           }
           
           protected void ddlSeleccionarTipoProductoMantenimiento_SelectedIndexChanged(object sender, EventArgs e)
           {
           DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCategoriaMantenimiento, ObjDataConfiguracion.Value.BuscaListas("CATEGORIAS", ddlSeleccionarTipoProductoMantenimiento.SelectedValue.ToString(), null));
           DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlseleccionarMarca, ObjDataConfiguracion.Value.BuscaListas("MARCAS", ddlSeleccionarTipoProductoMantenimiento.SelectedValue.ToString(), ddlSeleccionarCategoriaMantenimiento.SelectedValue.ToString()));
           DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarModelo, ObjDataConfiguracion.Value.BuscaListas("MODELOS", ddlSeleccionarTipoProductoMantenimiento.SelectedValue.ToString(), ddlSeleccionarCategoriaMantenimiento.SelectedValue.ToString(), ddlseleccionarMarca.SelectedValue.ToString()));
           
           
           int TipoProducto = Convert.ToInt32(ddlSeleccionarTipoProductoMantenimiento.SelectedValue);
           if (TipoProducto == 1) {
           ControlesFacturarProductos();
           }
           else if (TipoProducto == 2) {
           ControlesFacturarServicios();
           }
           }
           
           protected void ddlSeleccionarCategoriaMantenimiento_SelectedIndexChanged(object sender, EventArgs e)
           {
           DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlseleccionarMarca, ObjDataConfiguracion.Value.BuscaListas("MARCAS", ddlSeleccionarTipoProductoMantenimiento.SelectedValue.ToString(), ddlSeleccionarCategoriaMantenimiento.SelectedValue.ToString()));
           DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarModelo, ObjDataConfiguracion.Value.BuscaListas("MODELOS", ddlSeleccionarTipoProductoMantenimiento.SelectedValue.ToString(), ddlSeleccionarCategoriaMantenimiento.SelectedValue.ToString(), ddlseleccionarMarca.SelectedValue.ToString()));
           }
           
           protected void ddlseleccionarMarca_SelectedIndexChanged(object sender, EventArgs e)
           {
           DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarModelo, ObjDataConfiguracion.Value.BuscaListas("MODELOS", ddlSeleccionarTipoProductoMantenimiento.SelectedValue.ToString(), ddlSeleccionarCategoriaMantenimiento.SelectedValue.ToString(), ddlseleccionarMarca.SelectedValue.ToString()));
           }
           
           protected void ddlSeleccionarTipoSuplidor_SelectedIndexChanged(object sender, EventArgs e)
           {
           DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSuplidorMantenimiento, ObjDataConfiguracion.Value.BuscaListas("SUPLIDOR", ddlSeleccionarTipoSuplidor.SelectedValue, null));
           }
           
           protected void cbAcumulativoMantenimiento_CheckedChanged(object sender, EventArgs e)
           {
           if (cbAcumulativoMantenimiento.Checked == true) {
           txtstockMantenimiento.Enabled = true;
           txtStockMinimoMantenimiento.Enabled = true;
           }
           else {
           txtstockMantenimiento.Enabled = false;
           txtStockMinimoMantenimiento.Enabled = false;
           txtstockMantenimiento.Text = "1";
           txtStockMinimoMantenimiento.Text = "1";
           }
           }
           
           protected void cbGraficar_CheckedChanged(object sender, EventArgs e)
           {
           if (cbGraficar.Checked == true) {
           lbGraficoProductoServicio.Visible = true;
           GraProductoServicio.Visible = true;
           lbEstadisticaCapital.Visible = true;
           GraEstadisticaCapital.Visible = true;
           }
           else {
           lbGraficoProductoServicio.Visible = false;
           GraProductoServicio.Visible = false;
           lbEstadisticaCapital.Visible = false;
           GraEstadisticaCapital.Visible = false;
           }
           }

        protected void btnRestabelcer_Click(object sender, EventArgs e)
        {
            RestablecerPantalla();
        }
    }
}