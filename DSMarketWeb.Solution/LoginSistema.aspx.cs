using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace DSMarketWeb.Solution
{
    public partial class LoginSistema : System.Web.UI.Page
    //
    {
        Lazy<DSMarketWeb.Logic.Logica.LogicaSeguridad.LogicaSeguridad> ObjDataSeguridad = new Lazy<Logic.Logica.LogicaSeguridad.LogicaSeguridad>();

       
        private void IngresarSistema() {
            if (string.IsNullOrEmpty(txtUsuarioLogin.Text.Trim()) || string.IsNullOrEmpty(txtClaveLogin.Text.Trim())) {
                ClientScript.RegisterStartupScript(GetType(), "CamposVacios()", "CamposVacios();", true);


            }
            else {
                string _Usuario = string.IsNullOrEmpty(txtUsuarioLogin.Text.Trim()) ? null : txtUsuarioLogin.Text.Trim();
                string _Clave = string.IsNullOrEmpty(txtClaveLogin.Text.Trim()) ? null : txtClaveLogin.Text.Trim();

                var ValidarUsuario = ObjDataSeguridad.Value.BuscaUsuarios(
                    new Nullable<decimal>(),
                    _Usuario,
                    DSMarketWeb.Logic.Comunes.SeguridadEncriptacion.Encriptar(_Clave),
                    null, null);
                if (ValidarUsuario.Count() < 1) {
                    ClientScript.RegisterStartupScript(GetType(), "UsuarioNoValido()", "UsuarioNoValido();", true);

                    bool EstatusIntentosFallidos = false;
                    int CantidadIntentosFallidos = 0;

                    var ValidarContadorUsuarios = ObjDataSeguridad.Value.BuscaIntentosFallidosLogin(1);
                    foreach (var n in ValidarContadorUsuarios) {
                        EstatusIntentosFallidos = Convert.ToBoolean(n.Estatus0);
                        CantidadIntentosFallidos = Convert.ToInt32(n.CantidadIntentos);
                    }

                    if (EstatusIntentosFallidos == true) {
                        int ContadorBloqueo = Convert.ToInt32(lbContador.Text);

                        if (ContadorBloqueo == CantidadIntentosFallidos)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "UsuarioBloqueado()", "UsuarioBloqueado();", true);

                            var SacarIdUsuario = ObjDataSeguridad.Value.BuscaUsuarios(
                                new Nullable<decimal>(),
                                _Usuario,
                                null, null, null);
                            if (SacarIdUsuario.Count() < 1) { }
                            else
                            {
                                foreach (var n in SacarIdUsuario)
                                {
                                    DSMarketWeb.Logic.PrcesarMantenimientos.Seguridad.ProcesarMantenimientoUsuarios Deshabilitar = new Logic.PrcesarMantenimientos.Seguridad.ProcesarMantenimientoUsuarios(
                                        Convert.ToDecimal(n.IdUsuario), 0, "", "", "", false, false, 0, "DISABLE");
                                    Deshabilitar.ProcesarInformacionUsuarios();
                                }
                            }
                        }
                        else {
                            ClientScript.RegisterStartupScript(GetType(), "UsuarioNoValido()", "UsuarioNoValido();", true);
                            txtUsuarioLogin.Text = string.Empty;
                            txtClaveLogin.Text = string.Empty;
                            txtUsuarioLogin.Focus();
                            ContadorBloqueo++;
                            lbContador.Text = ContadorBloqueo.ToString();
                        }
                    }
                }
                else {
                    decimal IdUsuario = 0;
                    bool EstatusUsuario = false, CambiaClaveUsuario = false;

                    foreach (var n in ValidarUsuario) {
                        IdUsuario = Convert.ToDecimal(n.IdUsuario);
                        EstatusUsuario = Convert.ToBoolean(n.Estatus0);
                        CambiaClaveUsuario = Convert.ToBoolean(n.CambiaClave0);
                    }

                    if (EstatusUsuario == true) {
                        Session["IdUsuario"] = Convert.ToDecimal(IdUsuario);
                        if (CambiaClaveUsuario == true) {
                            txtUsuarioLogin.Enabled = false;
                            txtClaveLogin.Enabled = false;
                            btnIngresarSistema.Visible = false;

                            txtNuevaClave.Visible = true;
                            txtConfirmarClave.Visible = true;
                            btnCambiarClave.Visible = true;
                            txtNuevaClave.Focus();

                        }
                        else {
                           
                            FormsAuthentication.RedirectFromLoginPage(_Usuario, true);
                        }
                    }
                    else {
                        ClientScript.RegisterStartupScript(GetType(), "UsuarioBloqueado()", "UsuarioBloqueado();", true);
                    }
                
                }
            
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresarSistema_Click(object sender, EventArgs e)
        {
            IngresarSistema();
            //Response.Redirect("Paginas/MenuPrincipal.aspx");
         //   FormsAuthentication.RedirectFromLoginPage("", false);
     
        }

        protected void btnCambiarClave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNuevaClave.Text.Trim()) || string.IsNullOrEmpty(txtConfirmarClave.Text.Trim()))
            {
                ClientScript.RegisterStartupScript(GetType(), "CamposVacios()", "CamposVacios();", true);
            }
            else {
                string _NuevaClave = string.IsNullOrEmpty(txtNuevaClave.Text.Trim()) ? null : txtNuevaClave.Text.Trim();
                string _ConfirmacionClave = string.IsNullOrEmpty(txtConfirmarClave.Text.Trim()) ? null : txtConfirmarClave.Text.Trim();

                if (_NuevaClave != _ConfirmacionClave) {
                    ClientScript.RegisterStartupScript(GetType(), "ClavesNoConcuerdan()", "ClavesNoConcuerdan();", true);
                    txtNuevaClave.Text = string.Empty;
                    txtConfirmarClave.Text = string.Empty;
                    txtNuevaClave.Focus();
                }
                else {
                    DSMarketWeb.Logic.PrcesarMantenimientos.Seguridad.ProcesarMantenimientoUsuarios CambiarClave = new Logic.PrcesarMantenimientos.Seguridad.ProcesarMantenimientoUsuarios(
                        Convert.ToDecimal(Session["IdUsuario"]),
                        0,
                        "",
                        DSMarketWeb.Logic.Comunes.SeguridadEncriptacion.Encriptar(_NuevaClave),
                        "", false, false, 0, "CHANGEPASSWORD");
                    CambiarClave.ProcesarInformacionUsuarios();

                    FormsAuthentication.RedirectFromLoginPage(txtUsuarioLogin.Text, true);


                }

            }
        }
    }
}