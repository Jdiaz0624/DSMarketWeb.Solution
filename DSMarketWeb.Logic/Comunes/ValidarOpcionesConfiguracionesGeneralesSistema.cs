using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace DSMarketWeb.Logic.Comunes
{
    public class ValidarOpcionesConfiguracionesGeneralesSistema
    {
        readonly DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion ObjData = new Logica.LogicaConfiguracion.LogicaConfiguracion();

        private decimal IdConfiguracion = 0;
        private decimal IdModulo = 0;

        public ValidarOpcionesConfiguracionesGeneralesSistema(
            decimal IdConfiguracionCON,
            decimal IdModuloCON)
        {
            IdConfiguracion = IdConfiguracionCON;
            IdModulo = IdModuloCON;
        }

        public bool ResultadoValidacion() {
            bool Resultado = false;

            var BuscarInformacion = ObjData.BuscaConfiguracionesGenerales(IdConfiguracion, IdModulo);
            foreach (var n in BuscarInformacion) {
                Resultado = (bool)n.Estatus0;
            }
            return Resultado;
        }

       
    }
}
