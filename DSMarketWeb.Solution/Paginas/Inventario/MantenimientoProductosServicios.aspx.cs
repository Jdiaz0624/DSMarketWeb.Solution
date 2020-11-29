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
        private void EjemploGrafico() {
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

        private void ListadoTipoProductoConsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarTipoProductoConsulta, ObjDataConfiguracion.Value.BuscaListas("TIPOPRODUCTO", null, null), true);
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCategoriaConsulta, ObjDataConfiguracion.Value.BuscaListas("CATEGORIAS", ddlSeleccionarTipoProductoConsulta.SelectedValue.ToString(), null), true);
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarMarcaConsulta, ObjDataConfiguracion.Value.BuscaListas("MARCAS", ddlSeleccionarTipoProductoConsulta.SelectedValue.ToString(), ddlSeleccionarCategoriaConsulta.SelectedValue.ToString()), true);
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarModeloConsulta, ObjDataConfiguracion.Value.BuscaListas("MODELOS", ddlSeleccionarTipoProductoConsulta.SelectedValue.ToString(), ddlSeleccionarCategoriaConsulta.SelectedValue.ToString(), ddlSeleccionarMarcaConsulta.SelectedValue.ToString()), true);
        }
        private void ListadoCategoriasConsultas() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCategoriaConsulta, ObjDataConfiguracion.Value.BuscaListas("CATEGORIAS", ddlSeleccionarTipoProductoConsulta.SelectedValue.ToString(), null), true);
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarMarcaConsulta, ObjDataConfiguracion.Value.BuscaListas("MARCAS", ddlSeleccionarTipoProductoConsulta.SelectedValue.ToString(), ddlSeleccionarCategoriaConsulta.SelectedValue.ToString()), true);
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarModeloConsulta, ObjDataConfiguracion.Value.BuscaListas("MODELOS", ddlSeleccionarTipoProductoConsulta.SelectedValue.ToString(), ddlSeleccionarCategoriaConsulta.SelectedValue.ToString(), ddlSeleccionarMarcaConsulta.SelectedValue.ToString()), true);
        }
        private void ListadoMarcasConsultas() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarMarcaConsulta, ObjDataConfiguracion.Value.BuscaListas("MARCAS", ddlSeleccionarTipoProductoConsulta.SelectedValue.ToString(), ddlSeleccionarCategoriaConsulta.SelectedValue.ToString()), true);
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarModeloConsulta, ObjDataConfiguracion.Value.BuscaListas("MODELOS", ddlSeleccionarTipoProductoConsulta.SelectedValue.ToString(), ddlSeleccionarCategoriaConsulta.SelectedValue.ToString(), ddlSeleccionarMarcaConsulta.SelectedValue.ToString()), true);
        }
        private void ListadoModelosConsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarModeloConsulta, ObjDataConfiguracion.Value.BuscaListas("MODELOS", ddlSeleccionarTipoProductoConsulta.SelectedValue.ToString(), ddlSeleccionarCategoriaConsulta.SelectedValue.ToString(),ddlSeleccionarMarcaConsulta.SelectedValue.ToString()), true);
        }
        private void ListadoUnidadmedidaCOnsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarUnidadMedidaConsulta, ObjDataConfiguracion.Value.BuscaListas("UNIDADMEDIDA", null, null), true);
        }
        private void ListadoCapacidadCOnsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCapacidadConsulta, ObjDataConfiguracion.Value.BuscaListas("CAPACIDAD", null, null), true);
        }
        private void ListadoCondicionConsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarCondicionConculta, ObjDataConfiguracion.Value.BuscaListas("CONDICION", null, null), true);
        }
        private void ListadoColoresConsulta() {
            DSMarketWeb.Logic.Comunes.UtilidadDrop.DropDownListLlena(ref ddlSeleccionarColorConsulta, ObjDataConfiguracion.Value.BuscaListas("COLORES", null, null), true);
        }

        private void CargarListadosConsultas() {
            ListadoTipoProductoConsulta();
            ListadoCategoriasConsultas();
            ListadoMarcasConsultas();
            ListadoModelosConsulta();
            ListadoUnidadmedidaCOnsulta();
            ListadoCapacidadCOnsulta();
            ListadoCondicionConsulta();
            ListadoColoresConsulta();
        }
        private void CargarListadosMantenimientos() { }

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
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                Panel1.BackColor = System.Drawing.Color.Red;
                lbRegistroDisponible.ForeColor = System.Drawing.Color.Green;
                CargarListadosConsultas();
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
                Convert.ToDecimal(gb.Cells[1].Text));
            gvListado.DataSource = ConsultarPorID;
            gvListado.DataBind();

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
            }
            ClientScript.RegisterStartupScript(GetType(), "BotonesModoSelect()", "BotonesModoSelect();", true);
        }

        protected void btnRegresarDetalle_Click(object sender, EventArgs e)
        {
            OcultarControlesDetalle();
            ClientScript.RegisterStartupScript(GetType(), "BotonesModoNormal()", "BotonesModoNormal();", true);
        }

        protected void btnGuardarMantenimiento_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "BotonesModoNormal()", "BotonesModoNormal();", true);
        }

        protected void btnModificarMantenimiento_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "BotonesModoNormal()", "BotonesModoNormal();", true);
        }

        protected void btnEliminarMantenimiento_Click(object sender, EventArgs e)
        {
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
    }
}