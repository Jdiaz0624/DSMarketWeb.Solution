﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace DSMarketWeb.Solution.MasterPage
{
    public partial class PaginaMaestra : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void linkCerrarCesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        //INICIO DEL MODULO DE SERVICIO
        #region MODULO DE SERVICIO
        protected void LinkFacturacion_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                Response.Redirect("~/Paginas/Servicios/Facturacion.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

        }

        protected void LinkTipoPago_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                Response.Redirect("~/Paginas/Servicios/TipoPago.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        protected void LinkHistorialFacturacion_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }
        #endregion
        //FIN DEL MODULO DE SERVICIO



        //INICIO DEL MODULO DE INVENTARIO
        #region MDULO DE INVENTARIO
        protected void LinkProductos_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                Response.Redirect("~/Paginas/Inventario/Inventario.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

        }

        protected void LinkMantenimientoCategoria_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                Response.Redirect("~/Paginas/Inventario/MantenimientoCategorias.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

        }

        protected void LinkMantenimientomarcas_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                Response.Redirect("~/Paginas/Inventario/MantenimientoMarcas.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

        }

        protected void LinkTipoSuplidores_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                Response.Redirect("~/Paginas/Inventario/MantenimientoTipoSuplidor.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

        }

        protected void LinkSuplidores_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                Response.Redirect("~/Paginas/Inventario/MantenimientoSuplidores.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

        }
        #endregion
        //FIN DEL MODULO DE INVENTARIO

        //INICIO DEL MODULO DE CAJA
        #region MODULO DE CAJA
        protected void LinkCaja_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

        }

        protected void LinkCuadreCaja_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

        }

        protected void LinkConfigurarCaja_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

        }
        #endregion

        #region MODULO DE EMPRESA
        protected void LinkClientes_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                Response.Redirect("~/Paginas/Empresa/MantenimientoClientes.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

        }

        protected void LinkEmpleados_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                Response.Redirect("~/Paginas/Empresa/MantenimientoEmpleado.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

        }

        protected void LinkDepartamentos_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                Response.Redirect("~/Paginas/Empresa/MantenimientoDepartamento.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

        }

        protected void LinkCargos_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                Response.Redirect("~/Paginas/Empresa/MantenimientoCargos.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

        }

        protected void LinkTipoNomina_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                Response.Redirect("~/Paginas/Empresa/MantenimientoTipoNomina.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

        }

        protected void LinkCompraSuplidores_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                Response.Redirect("~/Paginas/Empresa/MantenimientoCompraSuplidores.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

        }

        protected void LinkTipoMovimiento_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                Response.Redirect("~/Paginas/Empresa/TipoMovimiento.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

        }

        protected void LinkTipoEmpleado_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                Response.Redirect("~/Paginas/Empresa/TipoEmpleado.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

        }

        protected void LinkBancos_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                Response.Redirect("~/Paginas/Empresa/Bancos.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

        }

        protected void LinkCalculoNomina_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

        }

        protected void LinkArchivoBanco_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

        }

        protected void LinkRetenciones_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                Response.Redirect("~/Paginas/Empresa/Retenciones.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

        }

        protected void LinkPorcientoRetenciones_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                Response.Redirect("~/Paginas/Empresa/PorcientoRetenciones.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

        }
        #endregion

        protected void LinkCatalogoCuentas_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null) {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
   
        }

        protected void LinkComprobantesFiscales_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null) {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        
        }

        protected void LinkReporteProductos_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null) {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
   
        }

        protected void LinkReporteMarcas_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null) {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
          

        }

        protected void LinkReporteModelos_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null) {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
         
        }

        protected void LinkHistorialCuadreCaja_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null) {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
     
        }

        protected void LinkAntiguedadSaldo_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null) {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
          
        }

        protected void LinkReporteVentas_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null) {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

        }

        protected void LinkReporteTSS_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null) {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

        }

        protected void LinkReporteDGII_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null) {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

        }

        protected void LinkReporteFinancieros_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null) {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
       
        }

        protected void LinkRutaReportes_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null) {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

        }

        protected void LinkPorcientoDescuentoProductos_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null) {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        protected void LinkRUtaArchivostxt_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null) {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        protected void LinkMantenimientoGeneral_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null) {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
                Response.Redirect("~/Paginas/Configuracion/ConfiguracionesGenerales.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
            
        }

        protected void LinkImpuestos_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null) {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        protected void LinkBackupBD_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null) {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        protected void LinkMail_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null) {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        protected void LinkInformacionEmpresa_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null) {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
                Response.Redirect("~/Paginas/Configuracion/InformacionEmpresa.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        protected void LinkUsuarios_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null) {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        protected void LinkPermisoUsuairos_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null) {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        protected void LinkClaveSeguridad_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null) {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Esta Opción no esta disponible por el momento');", true);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        protected void lbPaginaInicioSistema_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null) {
                Response.Redirect("~/Paginas/MenuPrincipal.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        protected void LinkCitas_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                Response.Redirect("~/Paginas/Empresa/Citas.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        protected void LinkHistorialCotizaciones_Click(object sender, EventArgs e)
        {

        }

        protected void LinkPerfilesUsuarios_Click(object sender, EventArgs e)
        {

        }

        protected void LinkMoneda_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                Response.Redirect("~/Paginas/Servicios/Moneda.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        protected void LinkMantenimientoModelos_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                Response.Redirect("~/Paginas/Inventario/MantenimientoModelos.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        protected void LinkMantenimientoUnidadMedida_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                Response.Redirect("~/Paginas/Inventario/MantenimientoUnidadMedida.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        protected void LinkMantenimientoColores_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                Response.Redirect("~/Paginas/Inventario/MantenimientoColores.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        protected void LinkMantenimientoCondiciones_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                Response.Redirect("~/Paginas/Inventario/MantenimientoCondiciones.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        protected void LinkMantenimientoCapacidad_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                Response.Redirect("~/Paginas/Inventario/MantenimientoCapacidad.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }
    }
}