using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Comunes
{
    public class SacarIdCreadoProducto
    {
        readonly DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario ObjData = new Logica.LogicaInventario.LogicaInventario();

        private decimal NumeroConector = 0;
        private decimal IdTipoProducto = 0;
        private decimal IdCategoria = 0;
        private decimal IdMarca = 0;
        private decimal IdModelo = 0;
        private decimal IdCcolor = 0;
        private decimal IdCapacidad = 0;
        private decimal IdCondicion = 0;

        public SacarIdCreadoProducto(
            decimal NumeroConectorCON,
            decimal IdTipoProductoCON,
            decimal IdCategoriaCON,
            decimal IdMarcaCON,
            decimal IdModeloCON,
            decimal IdCcolorCON,
            decimal IdCapacidadCON,
            decimal IdCondicionCON)
        {
            NumeroConector = NumeroConectorCON;
            IdTipoProducto = IdTipoProductoCON;
            IdCategoria = IdCategoriaCON;
            IdMarca = IdMarcaCON;
            IdModelo = IdModeloCON;
            IdCcolor = IdCcolorCON;
            IdCapacidad = IdCapacidadCON;
            IdCondicion = IdCondicionCON;
        }
        public decimal SacarIdProductoCreado() {

            decimal IdProductoSacado = 0;

            var SacarInformaciom = ObjData.SacarIdProductocreado(NumeroConector, IdTipoProducto, IdCategoria, IdMarca, IdModelo, IdCcolor, IdCapacidad, IdCondicion);
            if (SacarInformaciom.Count() < 1) {
                IdProductoSacado = 0;
            }
            else {
                foreach (var n in SacarInformaciom) {
                    IdProductoSacado = Convert.ToDecimal(n.IdProducto);
                }
            }
            return IdProductoSacado;
        }
    }
}
