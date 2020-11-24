using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DSMarketWeb.Solution
{
    public partial class LoginSistema : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresarSistema_Click(object sender, EventArgs e)
        {
            if (txtNuevaClave.Visible == true) {
                txtNuevaClave.Visible = false;
                txtConfirmarClave.Visible = false;
                txtUsuarioLogin.Enabled = true;
                txtClaveLogin.Enabled = true;
                txtUsuarioLogin.Text = string.Empty;
                txtClaveLogin.Text = string.Empty;
            }
            else {
                txtNuevaClave.Visible = true;
                txtConfirmarClave.Visible = true;
                txtUsuarioLogin.Enabled = false;
                txtClaveLogin.Enabled = false;
            }
        }
    }
}