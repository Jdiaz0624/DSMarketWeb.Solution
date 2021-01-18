using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Empresa
{
    public class ProcesarInformacionPorcientoRetenciones
    {
        readonly DSMarketWeb.Logic.Logica.LogicaEmpresa.LogicaEmpresa ObjData = new Logica.LogicaEmpresa.LogicaEmpresa();

        private decimal IdPorcientoRetencion = 0;
        private decimal IdRetencion = 0;
        private int Secuencia = 0;
        private decimal MontoInicial = 0;
        private decimal MontoFinal = 0;
        private decimal MontoSumar = 0;
        private decimal PorcientoCia = 0;
        private decimal PorcientoEmpleado = 0;
        private bool Estatus = false;
        private decimal UsuarioProcesa = 0;
        private string Accion = "";

        public ProcesarInformacionPorcientoRetenciones(
            decimal IdPorcientoRetencionCON,
            decimal IdRetencionCON,
            int SecuenciaCON,
            decimal MontoInicialCON,
            decimal MontoFinalCON,
            decimal MontoSumarCON,
            decimal PorcientoCiaCON,
            decimal PorcientoEmpleadoCON,
            bool EstatusCON,
            decimal UsuarioProcesaCON,
            string AccionCON)
        {
            IdPorcientoRetencion = IdPorcientoRetencionCON;
            IdRetencion = IdRetencionCON;
            Secuencia = SecuenciaCON;
            MontoInicial = MontoInicialCON;
            MontoFinal = MontoFinalCON;
            MontoSumar = MontoSumarCON;
            PorcientoCia = PorcientoCiaCON;
            PorcientoEmpleado = PorcientoEmpleadoCON;
            Estatus = EstatusCON;
            UsuarioProcesa = UsuarioProcesaCON;
            Accion = AccionCON;
        }

        /// <summary>
        /// Este metodo es para procesar la informacion del porciento de retencion
        /// </summary>
        public void ProcesarInformacion() {
            DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EPorcientoRetenciones Procesar = new Entidades.EntidadesEmpresa.EPorcientoRetenciones();

            Procesar.IdPorcientoRetencion = IdPorcientoRetencion;
            Procesar.IdRetencion = IdRetencion;
            Procesar.Secuencia = Secuencia;
            Procesar.MontoInicial = MontoInicial;
            Procesar.MontoFinal = MontoFinal;
            Procesar.MontoSumar = MontoSumar;
            Procesar.PorcientoCia = PorcientoCia;
            Procesar.PorcientoEmpleado = PorcientoEmpleado;
            Procesar.Estatus0 = Estatus;
            Procesar.UsuarioAdiciona = UsuarioProcesa;
            Procesar.FechaAdiciona = DateTime.Now;
            Procesar.UsuarioModifica = UsuarioProcesa;
            Procesar.FechaModifica = DateTime.Now;

            var MAN = ObjData.MantenimientoPorcientoRetenciones(Procesar, Accion);
        }
    }
}
