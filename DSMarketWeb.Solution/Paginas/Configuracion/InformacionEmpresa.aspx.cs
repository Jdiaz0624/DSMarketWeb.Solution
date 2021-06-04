using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DSMarketWeb.Solution.Paginas.Configuracion
{
    public partial class InformacionEmpresa : System.Web.UI.Page
    {
        Lazy<DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion> ObjDataConfiguracion = new Lazy<Logic.Logica.LogicaConfiguracion.LogicaConfiguracion>();

        #region MOSTRAR LA INFORMACION DE LA EMPRESA
        private void SacarInformacionEmpresa() {
            var SacarInformacion = ObjDataConfiguracion.Value.BuscaInformacionEmpresa();
            foreach (var n in SacarInformacion) {
                txtNombreEmpresa.Text = n.NombreEmpresa;
                txtRNC.Text = n.RNC;
                txtDireccion.Text = n.Direccion;
                txtTelefonos.Text = n.Telefonos;
                txtMail.Text = n.Email;
                txtMail2.Text = n.Email2;
                txtInstagram.Text = n.Instagran;
                txtSitioWeb.Text = n.Facebook;
            }
        }
        #endregion

        #region MODIFICAR INFORMACION DE EMPRESA
        private void ModificarInformacionEMpresa() {
            DSMarketWeb.Logic.PrcesarMantenimientos.Configuracion.ProcesarInformacionInformacionEmpresa Modificar = new Logic.PrcesarMantenimientos.Configuracion.ProcesarInformacionInformacionEmpresa(
                1,
                txtNombreEmpresa.Text,
                txtRNC.Text,
                txtDireccion.Text,
                txtMail.Text,
                txtMail2.Text,
                txtSitioWeb.Text,
                txtInstagram.Text,
                txtTelefonos.Text,
                1,
                "UPDATE");
            Modificar.ActualizarInformacionInformacionEmpresa();
            SacarInformacionEmpresa();
        }
        #endregion

        #region SACAR LAS POLITICAS DE LA EMPRESA
        private void SacarPoliticasEmpresa() {
            var SacarPoliticas = ObjDataConfiguracion.Value.BuscaPoliticasEmpresa(1);
            foreach (var n in SacarPoliticas) {
                txtPolitica1.Text = n.Politica1;
                txtPolitica2.Text = n.Politica2;
                txtPolitica3.Text = n.Politica3;
                txtPolitica4.Text = n.Politica4;
                txtPolitica5.Text = n.Politica5;
                txtPolitica6.Text = n.Politica6;
                txtPolitica7.Text = n.Politica7;
                txtPolitica8.Text = n.Politica8;
                txtPolitica9.Text = n.Politica9;
                txtPolitica10.Text = n.Politica10;
            }
        }
        #endregion

        #region MODIFICAR LAS POLICAS DE EMPRESA
        private void ModificarPoliticas() {
            DSMarketWeb.Logic.PrcesarMantenimientos.Configuracion.ProcesarInformacionPoliticasEmpresa Modificar = new Logic.PrcesarMantenimientos.Configuracion.ProcesarInformacionPoliticasEmpresa(
                1,
                txtPolitica1.Text,
                txtPolitica2.Text,
                txtPolitica3.Text,
                txtPolitica4.Text,
                txtPolitica5.Text,
                txtPolitica6.Text,
                txtPolitica7.Text,
                txtPolitica8.Text,
                txtPolitica9.Text,
                txtPolitica10.Text,
                "UPDATE");
            Modificar.ModificarInformacion();
            SacarPoliticasEmpresa();
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
                DivBloqueInformacionEmpresa.Visible = true;
                DivBloquePoliticasEmpresa.Visible = false;

                DSMarketWeb.Logic.Comunes.SacarNombreUsuario Nombre = new Logic.Comunes.SacarNombreUsuario((decimal)Session["IdUsuario"]);
                Label lbUsuarioConectado = (Label)Master.FindControl("lbUsuarioConectado");
                lbUsuarioConectado.Text = Nombre.SacarNombre();

                Label lbNivelAccesoPantalla = (Label)Master.FindControl("lbNivelAccesoPantalla");
                lbNivelAccesoPantalla.Text = "INFORMACION DE EMPRESA";

                SacarInformacionEmpresa();
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            bool RespuestaValidacion = false;
            string _ClaveSeguridad = string.IsNullOrEmpty(txtClaveSeguridad.Text.Trim()) ? "" : txtClaveSeguridad.Text.Trim();
            DSMarketWeb.Logic.Comunes.ValidarClaveSeguridad Validar = new Logic.Comunes.ValidarClaveSeguridad(_ClaveSeguridad);
            RespuestaValidacion = Validar.ResultadoClave();
            switch (RespuestaValidacion)
            {
                case true:
                    ModificarInformacionEMpresa();
                    break;

                case false:
                    ClientScript.RegisterStartupScript(GetType(), "CLaveSeguridadNoValida()", "CLaveSeguridadNoValida();", true);
                    break;
            }
       
        }

        protected void btnPoliticas_Click(object sender, EventArgs e)
        {
            DivBloqueInformacionEmpresa.Visible = false;
            DivBloquePoliticasEmpresa.Visible = true;
            SacarPoliticasEmpresa();
        }

        protected void btnModificarPoliticas_Click(object sender, EventArgs e)
        {
            bool RespuestaValidacion = false;
            string _ClaveSeguridad = string.IsNullOrEmpty(txtClaveSeguridadPoliticas.Text.Trim()) ? "" : txtClaveSeguridadPoliticas.Text.Trim();
            DSMarketWeb.Logic.Comunes.ValidarClaveSeguridad Validar = new Logic.Comunes.ValidarClaveSeguridad(_ClaveSeguridad);
            RespuestaValidacion = Validar.ResultadoClave();
            switch (RespuestaValidacion) {
                case true:
                    ModificarPoliticas();
                    break;

                case false:
                    ClientScript.RegisterStartupScript(GetType(), "CLaveSeguridadNoValida()", "CLaveSeguridadNoValida();", true);
                    break;
            }

            
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            DivBloqueInformacionEmpresa.Visible = true;
            DivBloquePoliticasEmpresa.Visible = false;
            SacarInformacionEmpresa();
        }
    }
}