using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Empresa
{
    public class ProcesarInformacionCargos
    {
        readonly DSMarketWeb.Logic.Logica.LogicaEmpresa.LogicaEmpresa ObjData = new Logica.LogicaEmpresa.LogicaEmpresa();

        private decimal IdCargo = 0;
        private decimal IdDepartamento = 0;
        private string Cargo = "";
        private bool Estatus = false;
        private decimal UsuarioProcesa = 0;
        private string Accion = "";

        public ProcesarInformacionCargos(
        decimal IdCargoCON,
        decimal IdDepartamentoCON,
        string CargoCON,
        bool EstatusCON,
        decimal UsuarioProcesaCON,
        string AccionCON)
        {
            IdCargo = IdCargoCON;
            IdDepartamento = IdDepartamentoCON;
            Cargo = CargoCON;
            Estatus = EstatusCON;
            UsuarioProcesa = UsuarioProcesaCON;
            Accion = AccionCON;
        }
        public void ProcesarDataCargo() {
            DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ECargos Procesar = new Entidades.EntidadesEmpresa.ECargos();

            Procesar.IdCargo = IdCargo;
            Procesar.IdDepartamento = IdDepartamento;
            Procesar.Cargo = Cargo;
            Procesar.Estatus0 = Estatus;
            Procesar.UsuarioAdiciona = UsuarioProcesa;
            Procesar.FechaAdiciona = DateTime.Now;
            Procesar.UsuarioModifica = UsuarioProcesa;
            Procesar.FechaModifica = DateTime.Now;

            var MAN = ObjData.MantenimientoCargos(Procesar, Accion);
        }
    }
}
