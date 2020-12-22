using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Comunes
{
    public class ValidarImagenSistema
    {
        readonly DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion Objdata = new Logica.LogicaConfiguracion.LogicaConfiguracion();

        private decimal IdLogoSistema = 0;

        public ValidarImagenSistema(
            decimal IdLogoSistemaCON)
        {
            IdLogoSistema = IdLogoSistemaCON;
        }

        public bool ValidarImagenSistemaPorDefecto() {

            bool ValidarImagen = false;

            var Vaalidar = Objdata.BuscaImagenesSistema(IdLogoSistema);
            if (Vaalidar.Count() < 1) {
                ValidarImagen = false;
            }
            else {
                ValidarImagen = true;
            }
            return ValidarImagen;
        }
    }
}
