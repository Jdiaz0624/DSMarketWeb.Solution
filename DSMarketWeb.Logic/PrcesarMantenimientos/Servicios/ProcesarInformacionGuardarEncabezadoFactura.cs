using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Servicios
{
    public class ProcesarInformacionGuardarEncabezadoFactura
    {
        readonly DSMarketWeb.Logic.Logica.LogicaServicio.LogicaServicio ObjData = new Logica.LogicaServicio.LogicaServicio();

        private decimal NumeroFactura = 0;
        private string NumeroConector = "";
        private bool LlevaComprobanre = false;
        private decimal IdComprobanre = 0;
        private string NumeroComprobante = "";
        private string Cliente = "";
        private decimal IdTipoIdentificacion = 0;
        private string NumeroIdentificacion = "";
        private string Telefono = "";
        private string CorreoElectronico = "";
        private string Direccion = "";
        private string Comentario = "";
        private DateTime FechaFacturacion = DateTime.Now;
        private decimal IdTipoVenta = 0;
        private decimal IdTipoPlazoCredito = 0;
        private decimal NumeroPlazoTiempo = 0;
        private decimal IdTiempoPlazoCredito = 0;
        private string Estatus = "";
        private string Accion = "";

        public ProcesarInformacionGuardarEncabezadoFactura(
            decimal NumeroFacturaCON,
            string NumeroConectorCON,
            bool LlevaComprobanreCON,
            decimal IdComprobanreCON,
            string NumeroComprobanteCON,
            string ClienteCON,
            decimal IdTipoIdentificacionCON,
            string NumeroIdentificacionCON,
            string TelefonoCON,
            string CorreoElectronicoCON,
            string DireccionCON,
            string ComentarioCON,
            DateTime FechaFacturacionCON,
            decimal IdTipoVentaCON,
            decimal IdTipoPlazoCreditoCON,
            decimal NumeroPlazoTiempoCON,
            decimal IdTiempoPlazoCreditoCON,
            string EstatusCON,
            string AccionCON)
        {
            NumeroFactura = NumeroFacturaCON;
            NumeroConector = NumeroConectorCON;
            LlevaComprobanre = LlevaComprobanreCON;
            IdComprobanre = IdComprobanreCON;
            NumeroComprobante = NumeroComprobanteCON;
            Cliente = ClienteCON;
            IdTipoIdentificacion = IdTipoIdentificacionCON;
            NumeroIdentificacion = NumeroIdentificacionCON;
            Telefono = TelefonoCON;
            CorreoElectronico = CorreoElectronicoCON;
            Direccion = DireccionCON;
            Comentario = ComentarioCON;
            FechaFacturacion = FechaFacturacionCON;
            IdTipoVenta = IdTipoVentaCON;
            IdTipoPlazoCredito = IdTipoPlazoCreditoCON;
            NumeroPlazoTiempo = NumeroPlazoTiempoCON;
            IdTiempoPlazoCredito = IdTiempoPlazoCreditoCON;
            Estatus = EstatusCON;
            Accion = AccionCON;
        }

        private void ProcesarInformacion() {
            DSMarketWeb.Logic.Entidades.EntidadesServicio.EGuardarFacturacionEncabezadoFactura Procesar = new Entidades.EntidadesServicio.EGuardarFacturacionEncabezadoFactura();

            Procesar.NumeroFactura = NumeroFactura;
            Procesar.NumeroConector = NumeroConector;
            Procesar.LlevaComprobanre = LlevaComprobanre;
            Procesar.IdComprobanre = IdComprobanre;
            Procesar.NumeroComprobante = NumeroComprobante;
            Procesar.Cliente = Cliente;
            Procesar.IdTipoIdentificacion = IdTipoIdentificacion;
            Procesar.NumeroIdentificacion = NumeroIdentificacion;
            Procesar.Telefono = Telefono;
            Procesar.CorreoElectronico = CorreoElectronico;
            Procesar.Direccion = Direccion;
            Procesar.Comentario = Comentario;
            Procesar.FechaFacturacion = FechaFacturacion;
            Procesar.IdTipoVenta = IdTipoVenta;
            Procesar.IdTipoPlazoCredito = IdTipoPlazoCredito;
            Procesar.NumeroPlazoTiempo = NumeroPlazoTiempo;
            Procesar.IdTiempoPlazoCredito = IdTiempoPlazoCredito;
            Procesar.Estatus = Estatus;

            var MAN = ObjData.GuardarInformacionEncabezadoFactura(Procesar, Accion);
        }
    }
}
