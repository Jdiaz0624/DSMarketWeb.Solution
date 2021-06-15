using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Inventario
{
    public class ProcesarInformacionColores
    {
        readonly DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario ObjData = new Logica.LogicaInventario.LogicaInventario();

        private decimal IdColor = 0;
        private string Colores = "";
        private bool Estatus = false;
        private string Accion = "";

        public ProcesarInformacionColores(
            decimal IdColorCON,
            string ColoresCON,
            bool EstatusCON,
            string AccionCON)
        {
            IdColor = IdColorCON;
             Colores = ColoresCON;
             Estatus = EstatusCON;
             Accion = AccionCON;
        }

        public void ProcesarInformacion() {
            DSMarketWeb.Logic.Entidades.EntidadesInventario.EColores Procesar = new Entidades.EntidadesInventario.EColores();

            Procesar.IdColor = IdColor;
            Procesar.Color = Colores;
            Procesar.Estatus0 = Estatus;

            var MAN = ObjData.ProcesarColores(Procesar, Accion);
        }
    }
}
