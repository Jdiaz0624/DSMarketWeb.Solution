using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Configuracion
{
    public class ProcesarInformacionComprobantesFiscales
    {
        readonly DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion ObjData = new Logica.LogicaConfiguracion.LogicaConfiguracion();

        private decimal IdComprobante = 0;
        private string Descripcion = "";
        private string Serie = "";
        private string TipoComprobante = "";
        private long Secuencia = 0;
        private long SecuenciaInicial = 0;
        private long SecuenciaFinal = 0;
        private long Limite = 0;
        private bool Estatus = false;
        private string ValidoHasta = "";
        private bool PorDefecto = false;
        private long Posiciones = 0;
        private int CobroPorcientoAdicional = 0;
        private bool LibreImpuesto = false;
        private string Accion = "";

        public ProcesarInformacionComprobantesFiscales(
         decimal IdComprobanteCON,
        string DescripcionCON,
        string SerieCON,
        string TipoComprobanteCON,
        long SecuenciaCON,
        long SecuenciaInicialCON,
        long SecuenciaFinalCON,
        long LimiteCON,
        bool EstatusCON,
        string ValidoHastaCON,
        bool PorDefectoCON,
        long PosicionesCON,
        int CobroPorcientoAdicionalCON,
        bool LibreImpuestoCON,
        string AccionCON)
        {
            IdComprobante = IdComprobanteCON;
            Descripcion = DescripcionCON;
            Serie = SerieCON;
            TipoComprobante = TipoComprobanteCON;
            Secuencia = SecuenciaCON;
            SecuenciaInicial = SecuenciaInicialCON;
            SecuenciaFinal = SecuenciaFinalCON;
            Limite = LimiteCON;
            Estatus = EstatusCON;
            ValidoHasta = ValidoHastaCON;
            PorDefecto = PorDefectoCON;
            Posiciones = PosicionesCON;
             CobroPorcientoAdicional = CobroPorcientoAdicionalCON;
            LibreImpuesto = LibreImpuestoCON;
            Accion = AccionCON;
        }

        public void ProcesarInformacion() {
            DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EComprobantesFuscales Procesar = new Entidades.EntidadesConfiguracion.EComprobantesFuscales();

            Procesar.IdComprobante = IdComprobante;
            Procesar.Comprobante = Descripcion;
            Procesar.Secuencia = Secuencia;
            Procesar.TipoComprobante = TipoComprobante;
            Procesar.Secuencia = Secuencia;
            Procesar.SecuenciaInicial = SecuenciaInicial;
            Procesar.SecuenciaFinal = SecuenciaFinal;
            Procesar.Limite = Limite;
            Procesar.Estatus0 = Estatus;
            Procesar.ValidoHasta = ValidoHasta;
            Procesar.PorDefecto0 = PorDefecto;
            Procesar.Posiciones = Posiciones;
            Procesar.CobroPorcientoAdicional = CobroPorcientoAdicional;
            Procesar.LibreImpuesto0 = LibreImpuesto;

            var MAN = ObjData.MantenimientoComprobantesFiscales(Procesar, Accion);
        }
    }
}
