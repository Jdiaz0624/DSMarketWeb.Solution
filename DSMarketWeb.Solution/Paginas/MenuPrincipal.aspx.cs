using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace DSMarketWeb.Solution.Paginas
{
    public partial class MenuPrincipal : System.Web.UI.Page
    {
        Lazy<DSMarketWeb.Logic.Logica.LogicaSeguridad.LogicaSeguridad> ObjDataLogica = new Lazy<Logic.Logica.LogicaSeguridad.LogicaSeguridad>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                if (Session["IdUsuario"] != null)
                {
                    string Usuario = "";
                    string NivelAcceso = "";
                    var SacarInformacionUsuarioConectado = ObjDataLogica.Value.BuscaUsuarios(Convert.ToDecimal(Session["IdUsuario"]), null, null, null, null);
                    foreach (var n in SacarInformacionUsuarioConectado)
                    {
                        Usuario = n.Persona;
                        NivelAcceso = n.Nivel;
                    }
                    Label lbUsuarioConectadoMasterPage = (Label)this.Master.FindControl("lbNombreUsuarioConectado");
                    lbUsuarioConectadoMasterPage.Text = Usuario;

                    Label lbNivelAccesoMasterPage = (Label)this.Master.FindControl("lbNivelAccesoUsuarioConectado");
                    lbNivelAccesoMasterPage.Text = NivelAcceso;

                    Label lbPantallaActual = (Label)this.Master.FindControl("lbPantallaPosicionada");
                    lbPantallaActual.Text = "MENU PRINCIPAL";
                }
                else {
                    FormsAuthentication.SignOut();
                    FormsAuthentication.RedirectToLoginPage();
                }
            }
        }
    }
}