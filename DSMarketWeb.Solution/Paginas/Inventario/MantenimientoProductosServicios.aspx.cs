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
        }

        protected void ddlSeleccionarTipoProductoConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    

        protected void ddlSeleccionarMarcaConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSeleccionarCategoriaConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

        }

        protected void btnReporte_Click(object sender, EventArgs e)
        {

        }

        protected void btndetalle_Click(object sender, EventArgs e)
        {
            MostrarControlesDetalle();
        }

        protected void gvListado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvListado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnRegresarDetalle_Click(object sender, EventArgs e)
        {
            OcultarControlesDetalle();
        }
    }
}