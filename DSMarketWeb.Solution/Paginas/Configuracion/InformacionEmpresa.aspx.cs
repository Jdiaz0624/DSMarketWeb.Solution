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
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {

        }

        protected void btnPoliticas_Click(object sender, EventArgs e)
        {

        }

        protected void btnModificarPoliticas_Click(object sender, EventArgs e)
        {

        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {

        }
    }
}