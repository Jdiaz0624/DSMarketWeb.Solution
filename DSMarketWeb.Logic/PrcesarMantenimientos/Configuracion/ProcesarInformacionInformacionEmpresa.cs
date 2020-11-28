using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Configuracion
{
    public class ProcesarInformacionInformacionEmpresa : DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion
    {
        private decimal IdInformacionEMpresa = 0;
        private string NombreEMpresa = "";
        private string RNC = "";
        private string Direccion = "";
        private string Email = "";
        private string Email2 = "";
        private string Facebook = "";
        private string Instagram = "";
        private string Telefonos = "";
        private int IdLogoEmpresa = 0;
        private string Accion = "";
        public ProcesarInformacionInformacionEmpresa(
            decimal IdInformacionEMpresaCON,
            string NombreEMpresaCON,
            string RNCCON,
            string DireccionCON,
            string EmailCON,
            string Email2CON,
            string FacebookCON,
            string InstagramCON,
            string TelefonosCON,
            int IdLogoEmpresaCON,
            string AccionCON

        )
        {
            IdInformacionEMpresa = IdInformacionEMpresaCON;
            NombreEMpresa = NombreEMpresaCON;
            RNC = RNCCON;
            Direccion = DireccionCON;
            Email = EmailCON;
            Email2 = Email2CON;
            Facebook = FacebookCON;
            Instagram = InstagramCON;
            Telefonos = TelefonosCON;
            IdLogoEmpresa = IdLogoEmpresaCON;
            Accion = AccionCON;
        }

        public void ActualizarInformacionInformacionEmpresa() {
            DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EInformacionEmpresa Procesar = new Entidades.EntidadesConfiguracion.EInformacionEmpresa();

            Procesar.IdInformacionEmpresa = IdInformacionEMpresa;
            Procesar.NombreEmpresa = NombreEMpresa;
            Procesar.RNC = RNC;
            Procesar.Direccion = Direccion;
            Procesar.Email = Email;
            Procesar.Email2 = Email2;
            Procesar.Facebook = Facebook;
            Procesar.Instagran = Instagram;
            Procesar.Telefonos = Telefonos;
            Procesar.IdLogoEmpresa = IdLogoEmpresa;

            ActualizarInformacion(Procesar, Accion);
        }
    }
}
