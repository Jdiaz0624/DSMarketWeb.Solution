using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Servicios
{
    public class ProcesarInformacionFacturacionItems
    {
        readonly DSMarketWeb.Logic.Logica.LogicaServicio.LogicaServicio ObjData = new Logica.LogicaServicio.LogicaServicio();

        private decimal NumeroRegistro = 0;
        private string NumerodeConector = "";
        private string TipodeProducto = "";
        private string Categoria = "";
        private string ProductoAcumulativo = "";
        private decimal Precio = 0;
        private string Producto = "";
        private int Cantidad = 0;
        private int PorcientodeDescuento = 0;
        private decimal DescuentoAplicado = 0;
        private decimal ImpuestoAplicado = 0;
        private bool LlevaGarantia = false;
        private string Garantia = "";
        private string CodigodeBarras = "";
        private string Referencia = "";
        private decimal IdProductoRespaldo = 0;
        private decimal NumeroConectorRespaldo = 0;
        private decimal IdTipoProductoRespaldo = 0;
        private decimal IdCategoriaRespaldo = 0;
        private decimal IdUnidadMedidaRespaldo = 0;
        private decimal IdMarcaRespaldo = 0;
        private decimal IdModeloRespaldo = 0;
        private decimal IdTipoSuplidorRespaldo = 0;
        private decimal IdSuplidorRespaldo = 0;
        private string DescripcionRespaldo = "";
        private string CodigoBarraRespaldo = "";
        private string ReferenciaRespaldo = "";
        private decimal PrecioCompraRespaldo = 0;
        private decimal PrecioVentaRespaldo = 0;
        private decimal StockRespaldo = 0;
        private decimal StockMinimoRespaldo = 0;
        private decimal PorcientoDescuentoRespaldo = 0;
        private bool AfectaOfertaRespaldo = false;
        private bool ProductoAcumulativoRespaldo = false;
        private bool LlevaImagenRespaldo = false;
        private decimal UsuarioAdicionRespaldo = 0;
        private DateTime FechaAdicionaRespaldo = DateTime.Now;
        private decimal UsuarioModificaRespaldo = 0;
        private DateTime FechaModificaRespaldo = DateTime.Now;
        private DateTime FechaRespaldo = DateTime.Now;
        private string ComentarioRespaldo = "";
        private bool AplicaParaImpuestoRespaldo = false;
        private bool EstatusProductoRespaldo = false;
        private string NumeroSeguimientoRespaldo = "";
        private decimal IdColorRespaldo = 0;
        private decimal IdCondicionRespaldo = 0;
        private decimal IdCapacidadRespaldo = 0;
        private bool LlevaGarantiaRespaldo = false;
        private int IdTipoGarantiaRespaldo = 0;
        private string TiempoGarantiaRespaldo = "";
        private bool EstatusActual = false;
        private string Accion = "";

        public ProcesarInformacionFacturacionItems(
            decimal NumeroRegistroCON,
            string NumerodeConectorCON,
            string TipodeProductoCON,
            string CategoriaCON,
            string ProductoAcumulativoCON,
            decimal PrecioCON,
            string ProductoCON,
            int CantidadCON,
            int PorcientodeDescuentoCON,
            decimal DescuentoAplicadoCON,
            decimal ImpuestoAplicadoCON,
            bool LlevaGarantiaCON,
            string GarantiaCON,
            string CodigodeBarrasCON,
            string ReferenciaCON,
            decimal IdProductoRespaldoCON,
            decimal NumeroConectorRespaldoCON,
            decimal IdTipoProductoRespaldoCON,
            decimal IdCategoriaRespaldoCON,
            decimal IdUnidadMedidaRespaldoCON,
            decimal IdMarcaRespaldoCON,
            decimal IdModeloRespaldoCON,
            decimal IdTipoSuplidorRespaldoCON,
            decimal IdSuplidorRespaldoCON,
            string DescripcionRespaldoCON,
            string CodigoBarraRespaldoCON,
            string ReferenciaRespaldoCON,
            decimal PrecioCompraRespaldoCON,
            decimal PrecioVentaRespaldoCON,
            decimal StockRespaldoCON,
            decimal StockMinimoRespaldoCON,
            decimal PorcientoDescuentoRespaldoCON,
            bool AfectaOfertaRespaldoCON,
            bool ProductoAcumulativoRespaldoCON,
            bool LlevaImagenRespaldoCON,
            decimal UsuarioAdicionRespaldoCON,
            DateTime FechaAdicionaRespaldoCON,
            decimal UsuarioModificaRespaldoCON,
            DateTime FechaModificaRespaldoCON,
            DateTime FechaRespaldoCON,
            string ComentarioRespaldoCON,
            bool AplicaParaImpuestoRespaldoCON,
            bool EstatusProductoRespaldoCON,
            string NumeroSeguimientoRespaldoCON,
            decimal IdColorRespaldoCON,
            decimal IdCondicionRespaldoCON,
            decimal IdCapacidadRespaldoCON,
            bool LlevaGarantiaRespaldoCON,
            int IdTipoGarantiaRespaldoCON,
            string TiempoGarantiaRespaldoCON,
            bool EstatusActualCON,
            string AccionCON)
        {
            NumeroRegistro = NumeroRegistroCON;
            NumerodeConector = NumerodeConectorCON;
            TipodeProducto = TipodeProductoCON;
            Categoria = CategoriaCON;
            ProductoAcumulativo = ProductoAcumulativoCON;
            Precio = PrecioCON;
            Producto = ProductoCON;
            Cantidad = CantidadCON;
            PorcientodeDescuento = PorcientodeDescuentoCON;
            DescuentoAplicado = DescuentoAplicadoCON;
            ImpuestoAplicado = ImpuestoAplicadoCON;
            LlevaGarantia = LlevaGarantiaCON;
            Garantia = GarantiaCON;
            CodigodeBarras = CodigodeBarrasCON;
            Referencia = ReferenciaCON;
            IdProductoRespaldo = IdProductoRespaldoCON;
            NumeroConectorRespaldo = NumeroConectorRespaldoCON;
            IdTipoProductoRespaldo = IdTipoProductoRespaldoCON;
            IdCategoriaRespaldo = IdCategoriaRespaldoCON;
            IdUnidadMedidaRespaldo = IdUnidadMedidaRespaldoCON;
            IdMarcaRespaldo = IdMarcaRespaldoCON;
            IdModeloRespaldo = IdModeloRespaldoCON;
            IdTipoSuplidorRespaldo = IdTipoSuplidorRespaldoCON;
            IdSuplidorRespaldo = IdSuplidorRespaldoCON;
            DescripcionRespaldo = DescripcionRespaldoCON;
            CodigoBarraRespaldo = CodigoBarraRespaldoCON;
            ReferenciaRespaldo = ReferenciaRespaldoCON;
            PrecioCompraRespaldo = PrecioCompraRespaldoCON;
            PrecioVentaRespaldo = PrecioVentaRespaldoCON;
            StockRespaldo = StockRespaldoCON;
            StockMinimoRespaldo = StockMinimoRespaldoCON;
            PorcientoDescuentoRespaldo = PorcientoDescuentoRespaldoCON;
            AfectaOfertaRespaldo = AfectaOfertaRespaldoCON;
            ProductoAcumulativoRespaldo = ProductoAcumulativoRespaldoCON;
            LlevaImagenRespaldo = LlevaImagenRespaldoCON;
            UsuarioAdicionRespaldo = UsuarioAdicionRespaldoCON;
            FechaAdicionaRespaldo = FechaAdicionaRespaldoCON;
            UsuarioModificaRespaldo = UsuarioModificaRespaldoCON;
            FechaModificaRespaldo = FechaModificaRespaldoCON;
            FechaRespaldo = FechaRespaldoCON;
            ComentarioRespaldo = ComentarioRespaldoCON;
            AplicaParaImpuestoRespaldo = AplicaParaImpuestoRespaldoCON;
            EstatusProductoRespaldo = EstatusProductoRespaldoCON;
            NumeroSeguimientoRespaldo = NumeroSeguimientoRespaldoCON;
            IdColorRespaldo = IdColorRespaldoCON;
            IdCondicionRespaldo = IdCondicionRespaldoCON;
            IdCapacidadRespaldo = IdCapacidadRespaldoCON;
            LlevaGarantiaRespaldo = LlevaGarantiaRespaldoCON;
            IdTipoGarantiaRespaldo = IdTipoGarantiaRespaldoCON;
            TiempoGarantiaRespaldo = TiempoGarantiaRespaldoCON;
            EstatusActual = EstatusActualCON;
            Accion = AccionCON;
        }

        public void ProcesarInformacion() {
            DSMarketWeb.Logic.Entidades.EntidadesServicio.EGuardarFacturacionItem Procesar = new Entidades.EntidadesServicio.EGuardarFacturacionItem();

            Procesar.NumeroRegistro = NumeroRegistro;
            Procesar.NumerodeConector = NumerodeConector;
            Procesar.TipodeProducto = TipodeProducto;
            Procesar.Categoria = Categoria;
            Procesar.ProductoAcumulativo = ProductoAcumulativo;
            Procesar.Precio = Precio;
            Procesar.Producto = Producto;
            Procesar.Cantidad = Cantidad;
            Procesar.PorcientodeDescuento = PorcientodeDescuento;
            Procesar.DescuentoAplicado = DescuentoAplicado;
            Procesar.ImpuestoAplicado = ImpuestoAplicado;
            Procesar.LlevaGarantia = LlevaGarantia;
            Procesar.Garantia = Garantia;
            Procesar.CodigodeBarras = CodigodeBarras;
            Procesar.Referencia = Referencia;
            Procesar.IdProductoRespaldo = IdProductoRespaldo;
            Procesar.NumeroConectorRespaldo = NumeroConectorRespaldo;
            Procesar.IdTipoProductoRespaldo = IdTipoProductoRespaldo;
            Procesar.IdCategoriaRespaldo = IdCategoriaRespaldo;
            Procesar.IdUnidadMedidaRespaldo = IdUnidadMedidaRespaldo;
            Procesar.IdMarcaRespaldo = IdMarcaRespaldo;
            Procesar.IdModeloRespaldo = IdModeloRespaldo;
            Procesar.IdTipoSuplidorRespaldo = IdTipoSuplidorRespaldo;
            Procesar.IdSuplidorRespaldo = IdSuplidorRespaldo;
            Procesar.DescripcionRespaldo = DescripcionRespaldo;
            Procesar.CodigoBarraRespaldo = CodigoBarraRespaldo;
            Procesar.ReferenciaRespaldo = ReferenciaRespaldo;
            Procesar.PrecioCompraRespaldo = PrecioCompraRespaldo;
            Procesar.PrecioVentaRespaldo = PrecioVentaRespaldo;
            Procesar.StockRespaldo = StockRespaldo;
            Procesar.StockMinimoRespaldo = StockMinimoRespaldo;
            Procesar.PorcientoDescuentoRespaldo = PorcientoDescuentoRespaldo;
            Procesar.AfectaOfertaRespaldo = AfectaOfertaRespaldo;
            Procesar.ProductoAcumulativoRespaldo = ProductoAcumulativoRespaldo;
            Procesar.LlevaImagenRespaldo = LlevaImagenRespaldo;
            Procesar.UsuarioAdicionRespaldo = UsuarioAdicionRespaldo;
            Procesar.FechaAdicionaRespaldo = FechaAdicionaRespaldo;
            Procesar.UsuarioModificaRespaldo = UsuarioModificaRespaldo;
            Procesar.FechaModificaRespaldo = FechaModificaRespaldo;
            Procesar.FechaRespaldo = FechaRespaldo;
            Procesar.ComentarioRespaldo = ComentarioRespaldo;
            Procesar.AplicaParaImpuestoRespaldo = AplicaParaImpuestoRespaldo;
            Procesar.EstatusProductoRespaldo = EstatusProductoRespaldo;
            Procesar.NumeroSeguimientoRespaldo = NumeroSeguimientoRespaldo;
            Procesar.IdColorRespaldo = IdColorRespaldo;
            Procesar.IdCondicionRespaldo = IdCondicionRespaldo;
            Procesar.IdCapacidadRespaldo = IdCapacidadRespaldo;
            Procesar.LlevaGarantiaRespaldo = LlevaGarantiaRespaldo;
            Procesar.IdTipoGarantiaRespaldo = IdTipoGarantiaRespaldo;
            Procesar.TiempoGarantiaRespaldo = TiempoGarantiaRespaldo;
            Procesar.EstatusActual = EstatusActual;

            var MAN = ObjData.GuardarItemsFacturas(Procesar, Accion);
        }
    }
}
