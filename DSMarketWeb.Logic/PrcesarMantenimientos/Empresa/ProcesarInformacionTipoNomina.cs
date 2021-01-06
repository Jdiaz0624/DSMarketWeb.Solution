using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Empresa
{
    public class ProcesarInformacionTipoNomina
    {
        readonly DSMarketWeb.Logic.Logica.LogicaEmpresa.LogicaEmpresa ObjDataEmpresa = new Logica.LogicaEmpresa.LogicaEmpresa();

        private decimal IdTipoNomina = 0;
        private string TipoNomina = "";
        private bool Estatus = false;
        private decimal UsuarioProcesa = 0;
        private string Accion = "";

        public ProcesarInformacionTipoNomina(
            decimal IdTipoNominaCON,
            string TipoNominaCON,
            bool EstatusCON,
            decimal UsuarioProcesaCON,
            string AccionCON)
        {
            IdTipoNomina = IdTipoNominaCON;
            TipoNomina = TipoNominaCON;
            Estatus = EstatusCON;
            UsuarioProcesa = UsuarioProcesaCON;
            Accion = AccionCON;
        }

        public void ProcesarDataTipoNomina() {
            DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ETipoNomina Procesar = new Entidades.EntidadesEmpresa.ETipoNomina();

            Procesar.IdTipoNomina = IdTipoNomina;
            Procesar.TipoNomina = TipoNomina;
            Procesar.Estatus0 = Estatus;
            Procesar.UsuarioAdiciona = UsuarioProcesa;
            Procesar.FechaAdiciona = DateTime.Now;
            Procesar.UsuairoModifica = UsuarioProcesa;
            Procesar.FechaModifica = DateTime.Now;

            var MAn = ObjDataEmpresa.MantenimientoTipoNomina(Procesar, Accion);
        }
    }
}
