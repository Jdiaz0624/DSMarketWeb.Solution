using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Servicios
{
    public class ProcesarInformacionTipoPago
    {
        readonly DSMarketWeb.Logic.Logica.LogicaServicio.LogicaServicio ObjDaya = new Logica.LogicaServicio.LogicaServicio();

        private decimal IdTipoPago = 0;
        private string Descripcion = "";
        private bool Estatus = false;
        private decimal UsuarioAdiciona = 0;
        private DateTime FechaAdiciona = DateTime.Now;
        private decimal UsuarioModifica = 0;
        private DateTime FechaModifica = DateTime.Now;
        private bool BloqueaMonto = false;
        private bool ImpuestoAdicional = false;
        private bool PorcentajeEntero = false;
        private decimal Valor = 0;
        private string CodigoTipoPago = "";
        private string Accion = "";

        public ProcesarInformacionTipoPago(
             decimal IdTipoPagoCON,
             string DescripcionCON,
             bool EstatusCON,
             decimal UsuarioAdicionaCON,
             DateTime FechaAdicionaCON,
             decimal UsuarioModificaCON
             DateTime FechaModificaCON,
             bool BloqueaMontoCON,
             bool ImpuestoAdicionalCON,
             bool PorcentajeEnteroCON,
             decimal ValorCON,
             string CodigoTipoPagoCON,
             string AccionCON)
            {
            IdTipoPago = IdTipoPagoCON;
            Descripcion = DescripcionCON;
            Estatus = EstatusCON;
            UsuarioAdiciona = UsuarioAdicionaCON;
            FechaAdiciona = FechaAdicionaCON;
            UsuarioModifica = UsuarioModificaCON;
            FechaModifica = FechaModificaCON;
            BloqueaMonto = BloqueaMontoCON;
            ImpuestoAdicional = ImpuestoAdicionalCON;
            PorcentajeEntero = PorcentajeEnteroCON;
            Valor = ValorCON;
            CodigoTipoPago = CodigoTipoPagoCON;
            Accion = AccionCON;
        }
        public void ProcesarInformacion() {
            DSMarketWeb.Logic.Entidades.EntidadesServicio.ETipoPago Procesar = new Entidades.EntidadesServicio.ETipoPago();

            Procesar.IdTipoPago = IdTipoPago;
            Procesar.TipoPago = Descripcion;
            Procesar.Estatus0 = Estatus;
            Procesar.UsuarioAdiciona = UsuarioAdiciona;
            Procesar.FechaAdiciona = FechaAdiciona;
            Procesar.UsuarioModifica = UsuarioModifica;
            Procesar.FechaModifica = FechaModifica;
            Procesar.BloqueaMonto0 = BloqueaMonto;
            Procesar.ImpuestoAdicional0 = ImpuestoAdicional;
            Procesar.PorcentajeEntero0 = PorcentajeEntero;
            Procesar.Valor = Valor;
            Procesar.CodigoTipoPago = CodigoTipoPago;

            var MAN = ObjDaya.MantenimientoTipoPago(Procesar, Accion);
        }


    }
}
