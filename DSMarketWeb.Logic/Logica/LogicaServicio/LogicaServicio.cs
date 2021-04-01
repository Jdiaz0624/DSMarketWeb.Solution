﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Logica.LogicaServicio
{
    public class LogicaServicio
    {
        DSMarketWeb.Data.ConexionLINQ.BDConexionServicioDataContext ObjData = new Data.ConexionLINQ.BDConexionServicioDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["DSMarketWEBConexion"].ConnectionString);

        #region MANTENIMIENTO DE TIPO DE PAGO
        //LISTADO DE TIPO DE PAGO
        public List<DSMarketWeb.Logic.Entidades.EntidadesServicio.ETipoPago> BuscaTipoPagos(decimal? IdTipoPago = null, string Descripcion = null) {
            ObjData.CommandTimeout = 999999999;

            var Listado = (from n in ObjData.SP_BUSCA_TIPO_PAGO(IdTipoPago, Descripcion)
                           select new DSMarketWeb.Logic.Entidades.EntidadesServicio.ETipoPago
                           {
                               IdTipoPago=n.IdTipoPago,
                               TipoPago=n.TipoPago,
                               Estatus0=n.Estatus0,
                               Estatus=n.Estatus,
                               UsuarioAdiciona=n.UsuarioAdiciona,
                               CreadPor=n.CreadPor,
                               FechaAdiciona=n.FechaAdiciona,
                               FechaCreado=n.FechaCreado,
                               ModificadoPor=n.ModificadoPor,
                               UsuarioModifica=n.UsuarioModifica,
                               FechaModifica=n.FechaModifica,
                               FechaModificado=n.FechaModificado,
                               BloqueaMonto0=n.BloqueaMonto0,
                               BloqueaMonto=n.BloqueaMonto,
                               ImpuestoAdicional0=n.ImpuestoAdicional0,
                               ImpuestoAdicional=n.ImpuestoAdicional,
                               PorcentajeEntero0=n.PorcentajeEntero0,
                               PorcentajeEntero=n.PorcentajeEntero,
                               Valor=n.Valor,
                               CodigoTipoPago=n.CodigoTipoPago,

                           }).ToList();
            return Listado;
        }

        //MANTENIMIENTO DE TIPO DE PAGO
        public DSMarketWeb.Logic.Entidades.EntidadesServicio.ETipoPago MantenimientoTipoPago(DSMarketWeb.Logic.Entidades.EntidadesServicio.ETipoPago Item, string Accion) {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesServicio.ETipoPago Mantenimiento = null;

            var TipoPago = ObjData.SP_MANTENIMIENTO_TIPO_PAGO(
                Item.IdTipoPago,
                Item.TipoPago,
                Item.Estatus0,
                Item.UsuarioAdiciona,
                Item.BloqueaMonto0,
                Item.ImpuestoAdicional0,
                Item.PorcentajeEntero0,
                Item.Valor,
                Item.CodigoTipoPago,
                Accion);
            if (TipoPago != null) {
                Mantenimiento = (from n in TipoPago
                                 select new DSMarketWeb.Logic.Entidades.EntidadesServicio.ETipoPago
                                 {
                                     IdTipoPago=n.IdTipoPago,
                                     TipoPago=n.Descripcion,
                                     Estatus0=n.Estatus,
                                     UsuarioAdiciona=n.UsuarioAdiciona,
                                     FechaAdiciona=n.FechaAdiciona,
                                     UsuarioModifica=n.UsuarioModifica,
                                     FechaModifica=n.FechaModifica,
                                     BloqueaMonto0=n.BloqueaMonto,
                                     ImpuestoAdicional0=n.ImpuestoAdicional,
                                     PorcentajeEntero0=n.PorcentajeEntero,
                                     Valor=n.Valor,
                                     CodigoTipoPago=n.CodigoTipoPago
                                 }).FirstOrDefault();
            }
            return Mantenimiento;
        }
        #endregion
        #region SACAR TIEMPO DE GARANTIA
        public List<DSMarketWeb.Logic.Entidades.EntidadesServicio.ESacarTiempoGarantia> SacartiempoGarantia(int? @IdTipoTiempoGarantia = null) {
            ObjData.CommandTimeout = 999999999;

            var Listado = (from n in ObjData.SP_SACAR_TIEMPO_GARANTIA(IdTipoTiempoGarantia)
                           select new DSMarketWeb.Logic.Entidades.EntidadesServicio.ESacarTiempoGarantia
                           {
                               TiempoGarantia=n.TiempoGarantia
                           }).ToList();
            return Listado;
        }
        #endregion

        #region METODOS PARA LA PANTALLA DE FACTURACION
        //ENCABEZADO DE LA FACTURA
        public DSMarketWeb.Logic.Entidades.EntidadesServicio.EGuardarFacturacionEncabezadoFactura GuardarInformacionEncabezadoFactura(DSMarketWeb.Logic.Entidades.EntidadesServicio.EGuardarFacturacionEncabezadoFactura Item, string Accion)
        {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesServicio.EGuardarFacturacionEncabezadoFactura Guardar = null;

            var InformacionEncabezadofactura = ObjData.SP_GUARDAR_INFORMACION_FACTURACION_ENCABEZADO(
                Item.NumeroFactura,
                Item.NumeroConector,
                Item.LlevaComprobanre,
                Item.IdComprobanre,
                Item.NumeroComprobante,
                Item.Cliente,
                Item.IdTipoIdentificacion,
                Item.NumeroIdentificacion,
                Item.Telefono,
                Item.CorreoElectronico,
                Item.Direccion,
                Item.Comentario,
                Item.IdTipoVenta,
                Item.IdTipoPlazoCredito,
                Item.NumeroPlazoTiempo,
                Item.IdTiempoPlazoCredito,
                Item.Estatus,
                Accion);
            if (InformacionEncabezadofactura != null) {
                Guardar = (from n in InformacionEncabezadofactura
                           select new DSMarketWeb.Logic.Entidades.EntidadesServicio.EGuardarFacturacionEncabezadoFactura
                           {
                               NumeroFactura=n.NumeroFactura,
                               NumeroConector=n.NumeroConector,
                               LlevaComprobanre=n.LlevaComprobanre,
                               IdComprobanre=n.IdComprobanre,
                               NumeroComprobante=n.NumeroComprobante,
                               Cliente=n.Cliente,
                               IdTipoIdentificacion=n.IdTipoIdentificacion,
                               NumeroIdentificacion=n.NumeroIdentificacion,
                               Telefono=n.Telefono,
                               CorreoElectronico=n.CorreoElectronico,
                               Direccion=n.Direccion,
                               Comentario=n.Comentario,
                               IdTipoVenta=n.IdTipoVenta,
                               IdTipoPlazoCredito=n.IdTipoPlazoCredito,
                               NumeroPlazoTiempo=n.NumeroPlazoTiempo,
                               IdTiempoPlazoCredito=n.IdTiempoPlazoCredito,
                               Estatus=n.Estatus
                           }).FirstOrDefault();
            }
            return Guardar;
        }


        //ITEMS DE LA FACTURA
        public DSMarketWeb.Logic.Entidades.EntidadesServicio.EGuardarFacturacionItem GuardarItemsFacturas(DSMarketWeb.Logic.Entidades.EntidadesServicio.EGuardarFacturacionItem Item, string Accion) {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesServicio.EGuardarFacturacionItem Guardar = null;

            var ItemsFactura = ObjData.SP_GUARDAR_INFORMACION_FACTURACION_ITEM(
             Item.NumerodeConector,
             Item.TipodeProducto,
             Item.Categoria,
             Item.ProductoAcumulativo,
             Item.Precio,
             Item.Producto,
             Item.Cantidad,
             Item.PorcientodeDescuento,
             Item.DescuentoAplicado,
             Item.ImpuestoAplicado,
             Item.LlevaGarantia,
             Item.Garantia,
             Item.CodigodeBarras,
             Item.Referencia,
             Item.IdProductoRespaldo,
             Item.NumeroConectorRespaldo,
             Item.IdTipoProductoRespaldo,
             Item.IdCategoriaRespaldo,
             Item.IdUnidadMedidaRespaldo,
             Item.IdMarcaRespaldo,
             Item.IdModeloRespaldo,
             Item.IdTipoSuplidorRespaldo,
             Item.IdSuplidorRespaldo,
             Item.DescripcionRespaldo,
             Item.CodigoBarraRespaldo,
             Item.ReferenciaRespaldo,
             Item.PrecioCompraRespaldo,
             Item.PrecioVentaRespaldo,
             Item.StockRespaldo,
             Item.StockMinimoRespaldo,
             Item.PorcientoDescuentoRespaldo,
             Item.AfectaOfertaRespaldo,
             Item.ProductoAcumulativoRespaldo,
             Item.LlevaImagenRespaldo,
             Item.UsuarioAdicionRespaldo,
             Item.FechaAdicionaRespaldo,
             Item.UsuarioModificaRespaldo,
             Item.FechaModificaRespaldo,
             Item.FechaRespaldo,
             Item.ComentarioRespaldo,
             Item.AplicaParaImpuestoRespaldo,
             Item.EstatusProductoRespaldo,
             Item.NumeroSeguimientoRespaldo,
             Item.IdColorRespaldo,
             Item.IdCondicionRespaldo,
             Item.IdCapacidadRespaldo,
             Item.LlevaGarantiaRespaldo,
             Item.IdTipoGarantiaRespaldo,
             Item.TiempoGarantiaRespaldo,
             Accion);
            if (ItemsFactura != null) {
                Guardar = (from n in ItemsFactura
                           select new DSMarketWeb.Logic.Entidades.EntidadesServicio.EGuardarFacturacionItem
                           {
                               NumerodeConector = n.NumerodeConector,
                               TipodeProducto = n.TipodeProducto,
                               Categoria = n.Categoria,
                               ProductoAcumulativo = n.ProductoAcumulativo,
                               Precio = n.Precio,
                               Producto = n.Producto,
                               Cantidad = n.Cantidad,
                               PorcientodeDescuento = n.PorcientodeDescuento,
                               DescuentoAplicado = n.DescuentoAplicado,
                               ImpuestoAplicado = n.ImpuestoAplicado,
                               LlevaGarantia = n.LlevaGarantia,
                               Garantia = n.Garantia,
                               CodigodeBarras = n.CodigodeBarras,
                               Referencia = n.Referencia,
                               IdProductoRespaldo = n.IdProductoRespaldo,
                               NumeroConectorRespaldo = n.NumeroConectorRespaldo,
                               IdTipoProductoRespaldo = n.IdTipoProductoRespaldo,
                               IdCategoriaRespaldo = n.IdCategoriaRespaldo,
                               IdUnidadMedidaRespaldo = n.IdUnidadMedidaRespaldo,
                               IdMarcaRespaldo = n.IdMarcaRespaldo,
                               IdModeloRespaldo = n.IdModeloRespaldo,
                               IdTipoSuplidorRespaldo = n.IdTipoSuplidorRespaldo,
                               IdSuplidorRespaldo = n.IdSuplidorRespaldo,
                               DescripcionRespaldo = n.DescripcionRespaldo,
                               CodigoBarraRespaldo = n.CodigoBarraRespaldo,
                               ReferenciaRespaldo = n.ReferenciaRespaldo,
                               PrecioCompraRespaldo = n.PrecioCompraRespaldo,
                               PrecioVentaRespaldo = n.PrecioVentaRespaldo,
                               StockRespaldo = n.StockRespaldo,
                               StockMinimoRespaldo = n.StockMinimoRespaldo,
                               PorcientoDescuentoRespaldo = n.PorcientoDescuentoRespaldo,
                               AfectaOfertaRespaldo = n.AfectaOfertaRespaldo,
                               ProductoAcumulativoRespaldo = n.ProductoAcumulativoRespaldo,
                               LlevaImagenRespaldo = n.LlevaImagenRespaldo,
                               UsuarioAdicionRespaldo = n.UsuarioAdicionRespaldo,
                               FechaAdicionaRespaldo = n.FechaAdicionaRespaldo,
                               UsuarioModificaRespaldo = n.UsuarioModificaRespaldo,
                               FechaModificaRespaldo = n.FechaModificaRespaldo,
                               FechaRespaldo = n.FechaRespaldo,
                               ComentarioRespaldo = n.ComentarioRespaldo,
                               AplicaParaImpuestoRespaldo = n.AplicaParaImpuestoRespaldo,
                               EstatusProductoRespaldo = n.EstatusProductoRespaldo,
                               NumeroSeguimientoRespaldo = n.NumeroSeguimientoRespaldo,
                               IdColorRespaldo = n.IdColorRespaldo,
                               IdCondicionRespaldo = n.IdCondicionRespaldo,
                               IdCapacidadRespaldo = n.IdCapacidadRespaldo,
                               LlevaGarantiaRespaldo = n.LlevaGarantiaRespaldo,
                               IdTipoGarantiaRespaldo = n.IdTipoGarantiaRespaldo,
                               TiempoGarantiaRespaldo = n.TiempoGarantiaRespaldo
                           }).FirstOrDefault();
            }
            return Guardar;
        }
        #endregion
    }
}
