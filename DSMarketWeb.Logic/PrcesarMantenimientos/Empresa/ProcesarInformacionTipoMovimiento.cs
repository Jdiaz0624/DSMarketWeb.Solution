using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Empresa
{
    public class ProcesarInformacionTipoMovimiento
    {
        readonly DSMarketWeb.Logic.Logica.LogicaEmpresa.LogicaEmpresa ObjData = new Logica.LogicaEmpresa.LogicaEmpresa();
        

        private decimal IdTipoMovimiento = 0;
        private string TipoMovimiento = "";
        private bool Estatus = false;
        private decimal UsuarioProceso=0;
        private string Accion = "";

        public ProcesarInformacionTipoMovimiento(
            decimal IdTipoMovimientoCON,
        string TipoMovimientoCON,
        bool EstatusCON,
        decimal UsuarioProcesoCON,
        string AccionCON)
        {
            IdTipoMovimiento = IdTipoMovimientoCON;
            TipoMovimiento = TipoMovimientoCON;
            Estatus = EstatusCON;
            UsuarioProceso = UsuarioProcesoCON;
            Accion = AccionCON;
        }
        public void ProcesarInformacion() {
            DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ETipoMovimiento Procesar = new Entidades.EntidadesEmpresa.ETipoMovimiento();

            Procesar.IdTipoMovimiento = IdTipoMovimiento;
            Procesar.TipoMovimiento = TipoMovimiento;
            Procesar.Estatus0 = Estatus;
            Procesar.UsuarioAdiciona = UsuarioProceso;
            Procesar.FechaAdiciona = DateTime.Now;
            Procesar.UsuarioModifica = UsuarioProceso;
            Procesar.FechaModifica = DateTime.Now;

            var MAN = ObjData.MantenimientoTipoMovimiento(Procesar, Accion);
        }
    }
}
