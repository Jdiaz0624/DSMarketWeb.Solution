using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Seguridad
{
    public class ProcesarInformacionCantidadIntentosLogin : DSMarketWeb.Logic.Logica.LogicaSeguridad.LogicaSeguridad
    {
        private int IdCantidadIntentosLogin = 0;
        private bool Estatus = false;
        private int CantidadIntentos = 0;
        private string Accion = "";

        public ProcesarInformacionCantidadIntentosLogin(
            int IdCantidadIntentosLoginCON,
        bool EstatusCON,
        int CantidadIntentosCON,
        string AccionCON
        )
        {
            IdCantidadIntentosLogin = IdCantidadIntentosLoginCON;
            Estatus = EstatusCON;
            CantidadIntentos = CantidadIntentosCON;
            Accion = AccionCON;
        }

        public void ProcesarInformacionCantidadIntentosUsuairos() {

            DSMarketWeb.Logic.Entidades.EntidadesSeguridad.ECantidadIntentosLogin Procesar = new Entidades.EntidadesSeguridad.ECantidadIntentosLogin();

            Procesar.IdContadorBloqueo = IdCantidadIntentosLogin;
            Procesar.Estatus0 = Estatus;
            Procesar.CantidadIntentos = CantidadIntentos;

            ActualizarCantidadIntentos(Procesar, Accion);
        }
    }
}
