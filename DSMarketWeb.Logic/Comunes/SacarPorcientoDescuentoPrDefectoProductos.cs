using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Comunes
{
    public class SacarPorcientoDescuentoPrDefectoProductos
    {
        readonly DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion ObjData = new Logica.LogicaConfiguracion.LogicaConfiguracion();

        private int IdPorcientoDescuento = 0;

        public SacarPorcientoDescuentoPrDefectoProductos(
            int IdPorcientoDescuentoCON)
        {
            IdPorcientoDescuento = IdPorcientoDescuentoCON;
        }

        public int SacarPorcientodescuento() {
            int Resultado = 0;

            var SacarInformacion = ObjData.BuscaPorcientoDescuentoPoeDefecto(IdPorcientoDescuento);
            if (SacarInformacion.Count() < 1) {
                Resultado = 0;
            }
            else {
                foreach (var n in SacarInformacion) {
                    Resultado = Convert.ToInt32(n.PorcientoDescuento);
                }
            }
            return Resultado;
        }
    }
}
