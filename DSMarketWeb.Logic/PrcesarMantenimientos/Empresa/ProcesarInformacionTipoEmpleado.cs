using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Empresa
{
    public class ProcesarInformacionTipoEmpleado
    {
        readonly DSMarketWeb.Logic.Logica.LogicaEmpresa.LogicaEmpresa ObjDataEmpresa = new Logica.LogicaEmpresa.LogicaEmpresa();

        private decimal IdTipoEmpleado = 0;
        private string TipoEmpleado = "";
        private bool Estatus = false;
        private decimal UsuarioProcesa = 0;
        private string Accion = "";

        public ProcesarInformacionTipoEmpleado(
            decimal IdTipoEmpleadoCON,
        string TipoEmpleadoCON,
        bool EstatusCON,
        decimal UsuarioProcesaCON,
        string AccionCON)
        {
            IdTipoEmpleado = IdTipoEmpleadoCON;
            TipoEmpleado = TipoEmpleadoCON;
            Estatus = EstatusCON;
            UsuarioProcesa = UsuarioProcesaCON;
            Accion = AccionCON;
        }
        public void ProcesarInformacionTipoEmplado() {
            DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ETipoEmpleado Procesar = new Entidades.EntidadesEmpresa.ETipoEmpleado();

            Procesar.IdTipoEmpleado = IdTipoEmpleado;
            Procesar.TipoEmpleado = TipoEmpleado;
            Procesar.Estatus0 = Estatus;
            Procesar.UsuarioAdiciona = UsuarioProcesa;
            Procesar.FechaAdiciona = DateTime.Now;
            Procesar.UsuarioModifica = UsuarioProcesa;
            Procesar.FechaModifica = DateTime.Now;

            var MAN = ObjDataEmpresa.MantenimientoTipoEmpleado(Procesar, Accion);
        }
    }
}
