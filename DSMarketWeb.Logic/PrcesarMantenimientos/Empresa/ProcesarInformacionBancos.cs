using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Empresa
{
    public class ProcesarInformacionBancos
    {
        readonly DSMarketWeb.Logic.Logica.LogicaEmpresa.LogicaEmpresa ObjData = new Logica.LogicaEmpresa.LogicaEmpresa();

        private decimal IdBanco = 0;
        private int CuentaContable = 0;
        private int Auxiliar = 0;
        private string NombreBanco = "";
        private bool Estatus = false;
        private decimal UsuarioProcesa = 0;
        private string Accion = "";


        public ProcesarInformacionBancos(
            decimal IdBancoCON,
        int CuentaContableCON,
        int AuxiliarCON,
        string NombreBancoCON,
        bool EstatusCON,
        decimal UsuarioProcesaCON,
        string AccionCON)
        {
            IdBanco = IdBancoCON;
            CuentaContable = CuentaContableCON;
            Auxiliar = AuxiliarCON;
            NombreBanco = NombreBancoCON;
            Estatus = EstatusCON;
            UsuarioProcesa = UsuarioProcesaCON;
            Accion = AccionCON;
        }

        public void ProcesarInformacion() {
            DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EBancos Procesar = new Entidades.EntidadesEmpresa.EBancos();

            Procesar.IdBanco = IdBanco;
            Procesar.CuentaContable = CuentaContable;
            Procesar.Auxiliar = Auxiliar;
            Procesar.Banco = NombreBanco;
            Procesar.Estatus0 = Estatus;
            Procesar.UsuarioAdiciona = UsuarioProcesa;
            Procesar.FechaAdiciona = DateTime.Now;
            Procesar.UsuarioModifica = UsuarioProcesa;
            Procesar.FechaModifica = DateTime.Now;

            var MAN = ObjData.MantenimientoBancos(Procesar, Accion);
        }
    }
}
