using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Inventario
{
    public class ProcesarInformacionCapacidadEquipos
    {
        readonly DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario ObjData = new Logica.LogicaInventario.LogicaInventario();

        private decimal IdCapacidad = 0;
        private string Capacidad = "";
        private bool Estatus = false;
        private decimal UsuarioProcesa = 0;
        private string Accion = "";

        public ProcesarInformacionCapacidadEquipos(
            decimal IdCapacidadCON,
            string CapacidadCON,
            bool EstatusCON,
            decimal UsuarioProcesaCON,
            string AccionCON)
        {
            IdCapacidad = IdCapacidadCON;
            Capacidad = CapacidadCON;
            Estatus = EstatusCON;
            UsuarioProcesa = UsuarioProcesaCON;
            Accion = AccionCON;
        }
        public void ProcesarInformacionCapacidad() {
            DSMarketWeb.Logic.Entidades.EntidadesInventario.ECapacidad Procesar = new Entidades.EntidadesInventario.ECapacidad();

            Procesar.IdCapacidad = IdCapacidad;
            Procesar.Capacidad = Capacidad;
            Procesar.Estatus0 = Estatus;
            Procesar.UsuarioAdiciona = UsuarioProcesa;
            Procesar.FechaAdiciona = DateTime.Now;
            Procesar.UsuarioModifica = UsuarioProcesa;
            Procesar.FechaModifica = DateTime.Now;

            var MAN = ObjData.MantenimientoCapacidad(Procesar, Accion);
        }
    }
}
