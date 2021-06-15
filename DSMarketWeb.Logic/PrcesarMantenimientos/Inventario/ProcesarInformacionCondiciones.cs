using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Inventario
{
    public class ProcesarInformacionCondiciones
    {
        readonly DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario ObjData = new Logica.LogicaInventario.LogicaInventario();

        private decimal IdCondiciones = 0;
        private string Condicion = "";
        private bool Estatus = false;
        private string Accion = "";

        public ProcesarInformacionCondiciones(
            decimal IdCondicionesCON,
            string CondicionCON,
            bool EstatusCON,
            string AccionCON)
        {
            IdCondiciones = IdCondicionesCON;
            Condicion = CondicionCON;
            Estatus = EstatusCON;
            Accion = AccionCON;
        }

        /// <summary>
        /// Procesar Información de las condiciones
        /// </summary>
        public void ProcesarInformacion() {
            DSMarketWeb.Logic.Entidades.EntidadesInventario.ECondiciones Procesar = new Entidades.EntidadesInventario.ECondiciones();

            Procesar.IdCondicion = IdCondiciones;
            Procesar.Condicion = Condicion;
            Procesar.Estatus0 = Estatus;

            var MAN = ObjData.ProcesarCondiciones(Procesar, Accion);
        }
    }
}
