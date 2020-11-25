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
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresarSistema_Click(object sender, EventArgs e)
        {
            //Response.Redirect("Paginas/MenuPrincipal.aspx");
            FormsAuthentication.RedirectFromLoginPage("", false);
     
        }
    }
}