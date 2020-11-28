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
                    var SacarInformacionUsuarioConectado = ObjDataLogica.Value.BuscaUsuarios(Convert.ToDecimal(Session["IdUsuario"]), null, null, null, null);
                    foreach (var n in SacarInformacionUsuarioConectado)
                    {
                        Usuario = n.Persona;
                        lbNivelAcceso.Text = n.Nivel;
                    }
                    lbUsuarioConectado.Text = Usuario + " - ";
                }
                else {
                    FormsAuthentication.SignOut();
                    FormsAuthentication.RedirectToLoginPage();
                }
            }
        }
    }
}