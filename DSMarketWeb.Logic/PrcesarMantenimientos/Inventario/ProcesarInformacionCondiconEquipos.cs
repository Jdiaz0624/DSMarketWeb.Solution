using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Inventario
{
    public class ProcesarInformacionCondiconEquipos
    {
        readonly DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario ObjData = new Logica.LogicaInventario.LogicaInventario();

        private decimal IdCondicion = 0;
        private string Condicion = "";
        private bool Estatus = false;
        private decimal UsuarioProcesa = 0;
        private string Accion = "";

        public ProcesarInformacionCondiconEquipos(
            decimal IdCondicionCON,
            string CondicionCON,
            bool EstatusCON,
            decimal UsuarioProcesaCON,
            string AccionCON)
        {
            IdCondicion = IdCondicionCON;
            Condicion = CondicionCON;
            Estatus = EstatusCON;
            UsuarioProcesa = UsuarioProcesaCON;
            Accion = AccionCON;
        }
        public void ProcesarInformacionCondicion() {
            DSMarketWeb.Logic.Entidades.EntidadesInventario.ECondicion Procesar = new Entidades.EntidadesInventario.ECondicion();


            Procesar.IdCondicion = IdCondicion;
            Procesar.Condicion = Condicion;
            Procesar.Estatus0 = Estatus;
            Procesar.UsuarioAdiciona = UsuarioProcesa;
            Procesar.FechaAdiciona = DateTime.Now;
            Procesar.UsuarioModifica = UsuarioProcesa;
            Procesar.FechaModifica = DateTime.Now;

            var MAN = ObjData.MantenimientoCondicion(Procesar, Accion);
        }
    }
}
