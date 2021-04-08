using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Comunes
{
    public class SacarTasaCambio
    {
        readonly DSMarketWeb.Logic.Logica.LogicaServicio.LogicaServicio ObjData = new Logica.LogicaServicio.LogicaServicio();

        private decimal IdMoneda = 0;

        public SacarTasaCambio(decimal IdMonedaCON) {
            IdMoneda = IdMonedaCON;
        }

        public decimal TasaMoneda() {

            decimal Tasa = 0;

            var SacarInformacion = ObjData.BuscaMonedas(
                IdMoneda, null, null);
            foreach (var n in SacarInformacion) {
                Tasa = (decimal)n.Tasa;
            }
            return Tasa;
        }
    }
}
