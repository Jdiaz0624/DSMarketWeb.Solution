using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Inventario
{
    public class ProcesarInformacionUnidadMedida
    {
        readonly DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario ObjData = new Logica.LogicaInventario.LogicaInventario();

        private decimal IdUnidadMedida = 0;
        private string UnidadMedida = "";
        private bool Estatus = false;
        private string Accion = "";

        public ProcesarInformacionUnidadMedida(
            decimal IdUnidadMedidaCON,
            string UnidadMedidaCON,
            bool EstatusCON,
            string AccionCON)
        {

            IdUnidadMedida = IdUnidadMedidaCON;
            UnidadMedida = UnidadMedidaCON;
            Estatus = EstatusCON;
            Accion = AccionCON;
        }

        public void ProcesarInformacion() {
            DSMarketWeb.Logic.Entidades.EntidadesInventario.EUnidadMedida Procesar = new Entidades.EntidadesInventario.EUnidadMedida();

            Procesar.IdUnidadMedida = IdUnidadMedida;
            Procesar.UnidadMedida = UnidadMedida;
            Procesar.Estatus0 = Estatus;

            var MAN = ObjData.ProcesarUnidadMedida(Procesar, Accion);
        }
    }
}
