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
                    var SacarInformacionUsuarioConectado = ObjDataLogica.Value.BuscaUsuarios(Convert.ToDecimal(Session["IdUsuario"]), null, null, null, null);

                    Label lbUsuarioConectado = (Label)Master.FindControl("lbUsuarioConectado");
                    lbUsuarioConectado.Text = "";

                    Label lbNivelAcceso = (Label)Master.FindControl("lbNivelAccesoPantalla");
                    lbNivelAcceso.Text = "";

                    foreach (var n in SacarInformacionUsuarioConectado)
                    {
                        lbUsuarioConectado.Text = n.Persona;
                        lbNivelAcceso.Text = n.Nivel;
                    }
                   // lbUsuarioConectado.Text = Usuario + " - ";
                }
                else {
                    FormsAuthentication.SignOut();
                    FormsAuthentication.RedirectToLoginPage();
                }
            }
        }
    }
}