using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Comunes
{
    public class SacartiempoGarantia
    {
        readonly DSMarketWeb.Logic.Logica.LogicaServicio.LogicaServicio ObjData = new Logica.LogicaServicio.LogicaServicio();

        private int IdTiempoGarantia = 0;

        public SacartiempoGarantia(int IdTiempoGarantiaCON) {
            IdTiempoGarantia = IdTiempoGarantiaCON;
        }

        public string TiempoGarantia() {
            string Tiempo = "";

            var SacarTiempoGarantia = ObjData.SacartiempoGarantia(IdTiempoGarantia);
            foreach (var n in SacarTiempoGarantia) {
                Tiempo = n.TiempoGarantia.ToString();
            }
            return Tiempo;
        }
    }
}
