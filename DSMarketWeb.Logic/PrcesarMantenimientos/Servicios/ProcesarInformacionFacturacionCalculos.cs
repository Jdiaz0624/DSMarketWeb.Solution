using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Servicios
{
    public class ProcesarInformacionFacturacionCalculos
    {
        readonly DSMarketWeb.Logic.Logica.LogicaServicio.LogicaServicio ObjData = new Logica.LogicaServicio.LogicaServicio();

        private decimal NumeroRegistro = 0;
        private string NumeroConector = "";
        private decimal IdTipoIngreso = 0;
        private decimal IdTipoPago = 0;
        private decimal IdMoneda = 0;
        private decimal ImpuestoTipoPago = 0;
        private decimal ImpuestoComprobante = 0;
        private decimal MontoPagado = 0;
        private decimal Cambio = 0;
        private decimal MontoPagadoMonedaOriginal = 0;
        private string Accion = "";

        public ProcesarInformacionFacturacionCalculos(
            decimal NumeroRegistroCON,
            string NumeroConectorCON,
            decimal IdTipoIngresoCON,
            decimal IdTipoPagoCON,
            decimal IdMonedaCON,
            decimal ImpuestoTipoPagoCON,
            decimal ImpuestoComprobanteCON,
            decimal MontoPagadoCON,
            decimal CambioCON,
            decimal MontoPagadoMonedaOriginalCON,
            string AccionCON)
        {
            NumeroRegistro = NumeroRegistroCON;
            NumeroConector = NumeroConectorCON;
            IdTipoIngreso = IdTipoIngresoCON;
            IdTipoPago = IdTipoPagoCON;
            IdMoneda = IdMonedaCON;
            ImpuestoTipoPago = ImpuestoTipoPagoCON;
            ImpuestoComprobante = ImpuestoComprobanteCON;
            MontoPagado = MontoPagadoCON;
            Cambio = CambioCON;
            MontoPagadoMonedaOriginal = MontoPagadoMonedaOriginalCON;
            Accion = AccionCON;
        }
        public void ProcesarInformacion() {
            DSMarketWeb.Logic.Entidades.EntidadesServicio.EFacturacionCalculos Procesar = new Entidades.EntidadesServicio.EFacturacionCalculos();

            Procesar.NumeroRegistro = NumeroRegistro;
            Procesar.NumeroConector = NumeroConector;
            Procesar.IdTipoIngreso = IdTipoIngreso;
            Procesar.IdTipoPago = IdTipoPago;
            Procesar.IdMoneda = IdMoneda;
            Procesar.ImpuestoTipoPago = ImpuestoTipoPago;
            Procesar.ImpuestoComprobante = ImpuestoComprobante;
            Procesar.MontoPagado = MontoPagado;
            Procesar.Cambio = Cambio;
            Procesar.MontoPagadoMonedaOriginal = MontoPagadoMonedaOriginal;

            var MAn = ObjData.GuardarInformacionCalculos(Procesar, Accion);
        }
    }
}
