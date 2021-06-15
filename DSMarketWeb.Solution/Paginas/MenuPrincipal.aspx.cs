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
        public enum ConceptoConfiguracionGeneralInventario
        {
            ImpuestoPorDefecto = 1,
            LlevaGarantiaPorDefecto = 2,
            CampoReferenciaObligatorio = 3,
            CampoReferenciaNumerico = 4,
            ValidarCampoReferencia = 5,
            UnidaddeMedidaSeleccionable = 6,
            ModelosSeleccionable = 7,
            ColoresSelecciobales = 8,
            CondicionesSelecciobales = 9,
            CapacidadesSelecciobales = 10,
            CargarListasDesplegablesalguardar = 11,
            AutoCompletarCampoReferenciaConsulta = 12
        }

        private void OcultarOpcionesMenuInventario() {
            bool Respuesta = false;
            LinkButton OpcionUnidadMedida = (LinkButton)Master.FindControl("LinkMantenimientoUnidadMedida");
            LinkButton OpcionModelo = (LinkButton)Master.FindControl("LinkMantenimientoModelos");
            LinkButton OpcionColor = (LinkButton)Master.FindControl("LinkMantenimientoColores");
            LinkButton OpcionCapacidad = (LinkButton)Master.FindControl("LinkMantenimientoCapacidad");
            LinkButton OpcionCondcion = (LinkButton)Master.FindControl("LinkMantenimientoCondiciones");
            //UNIDAD DE MEDIDA
            DSMarketWeb.Logic.Comunes.ValidarOpcionesConfiguracionesGeneralesSistema ValidarUnidadMedida = new Logic.Comunes.ValidarOpcionesConfiguracionesGeneralesSistema((decimal)ConceptoConfiguracionGeneralInventario.UnidaddeMedidaSeleccionable, 3);
            Respuesta = ValidarUnidadMedida.ResultadoValidacion();
            switch (Respuesta)
            {
                case true:
                    OpcionUnidadMedida.Visible = true;
                    break;

                case false:
                    OpcionUnidadMedida.Visible = false;
                    break;
            }


            //MODLEOS
            DSMarketWeb.Logic.Comunes.ValidarOpcionesConfiguracionesGeneralesSistema ValidarUnidarModelos = new Logic.Comunes.ValidarOpcionesConfiguracionesGeneralesSistema((decimal)ConceptoConfiguracionGeneralInventario.ModelosSeleccionable, 3);
            Respuesta = ValidarUnidarModelos.ResultadoValidacion();
            switch (Respuesta)
            {
                case true:
                    OpcionModelo.Visible = true;
                    break;

                case false:
                    OpcionModelo.Visible = false;
                    break;
            }

            //COLORES
            DSMarketWeb.Logic.Comunes.ValidarOpcionesConfiguracionesGeneralesSistema ValidarUnidarColores = new Logic.Comunes.ValidarOpcionesConfiguracionesGeneralesSistema((decimal)ConceptoConfiguracionGeneralInventario.ColoresSelecciobales, 3);
            Respuesta = ValidarUnidarColores.ResultadoValidacion();
            switch (Respuesta)
            {
                case true:
                    OpcionColor.Visible = true;
                    break;

                case false:
                    OpcionColor.Visible = false;
                    break;
            }

            //CONDICIONES
            DSMarketWeb.Logic.Comunes.ValidarOpcionesConfiguracionesGeneralesSistema ValidarCondiciones = new Logic.Comunes.ValidarOpcionesConfiguracionesGeneralesSistema((decimal)ConceptoConfiguracionGeneralInventario.CondicionesSelecciobales, 3);
            Respuesta = ValidarCondiciones.ResultadoValidacion();
            switch (Respuesta)
            {
                case true:
                    OpcionCondcion.Visible = true;
                    break;

                case false:
                    OpcionCondcion.Visible = false;
                    break;
            }

            //CAPACIDAD
            DSMarketWeb.Logic.Comunes.ValidarOpcionesConfiguracionesGeneralesSistema ValidarCapacidad = new Logic.Comunes.ValidarOpcionesConfiguracionesGeneralesSistema((decimal)ConceptoConfiguracionGeneralInventario.CapacidadesSelecciobales, 3);
            Respuesta = ValidarCapacidad.ResultadoValidacion();
            switch (Respuesta)
            {
                case true:
                    OpcionCapacidad.Visible = true;
                    break;

                case false:
                    OpcionCapacidad.Visible = false;
                    break;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                if (Session["IdUsuario"] != null)
                {
                    OcultarOpcionesMenuInventario();
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