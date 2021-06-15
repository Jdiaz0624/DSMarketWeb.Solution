using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Inventario
{
    public class ProcesarInformacionCapacidad
    {
        readonly DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario ObjDataCapaciadad = new Logica.LogicaInventario.LogicaInventario();

        private decimal IdCapacidad = 0;
        private string Capacidad = "";
        private bool Estatus = false;
        private string Accion = "";

        public ProcesarInformacionCapacidad(
            decimal IdCapacidadCON,
            string CapacidadCON,
            bool EstatusCON,
            string AccionCON)
        {
            IdCapacidad = IdCapacidadCON;
            Capacidad = CapacidadCON;
            Estatus = EstatusCON;
            Accion = AccionCON;
        }

        /// <summary>
        /// Procesar informacion de las capacidades
        /// </summary>
        public void ProcesarInformacion() {
            DSMarketWeb.Logic.Entidades.EntidadesInventario.ECapacidad Procesar = new Entidades.EntidadesInventario.ECapacidad();

            Procesar.IdCapacidad = IdCapacidad;
            Procesar.Capacidad = Capacidad;
            Procesar.Estatus0 = Estatus;

            var MAN = ObjDataCapaciadad.ProcesarCapacidad(Procesar, Accion);
        }
    }
}
