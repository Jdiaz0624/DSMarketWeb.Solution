using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Logica.LogicaInventario
{
    public class LogicaInventario
    {
        DSMarketWeb.Data.ConexionLINQ.BDConexionInventarioDataContext ObjData = new Data.ConexionLINQ.BDConexionInventarioDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["DSMarketWEBConexion"].ConnectionString);


        #region MANTENIMIENTO DE PRODUCTOS
        //LISTADO DE PRODUCTOS
        public List<DSMarketWeb.Logic.Entidades.EntidadesInventario.EProducto> BuscaProductos(decimal? IdProducto = null, decimal? NumeroConector = null, string Descripcion = null, string CodigoBarra = null, string Referencia = null, DateTime? FechaDesde = null, DateTime? FechaHasta = null, decimal? IdTipoProducto = null, decimal? IdCategoria = null, decimal? IdUnidadMedida = null, decimal? IdMarca = null, decimal? IdModelo = null, decimal? IdColor = null, decimal? IdCapacidad = null, decimal? IdCondicion = null, bool? TieneOferta = null, bool? EstatusProducto = null, string NumeroSeguimiento = null) {
            ObjData.CommandTimeout = 999999999;

            var ListadoProductos = (from n in ObjData.SP_BUSCA_PRODUCTO_WEB(IdProducto, NumeroConector, Descripcion, CodigoBarra, Referencia, FechaDesde, FechaHasta, IdTipoProducto, IdCategoria, IdUnidadMedida, IdMarca, IdModelo, IdColor, IdCapacidad, IdCondicion, TieneOferta, EstatusProducto, NumeroSeguimiento)
                                    select new DSMarketWeb.Logic.Entidades.EntidadesInventario.EProducto
                                    {
                                        IdProducto=n.IdProducto,
                                        IdMarca=n.IdMarca,
                                        Marca=n.Marca,
                                        IdModelo=n.IdModelo,
                                        Modelo=n.Modelo,
                                        IdColor=n.IdColor,
                                        Color=n.Color,
                                        IdCapacidad=n.IdCapacidad,
                                        Capacidad=n.Capacidad,
                                        IdCondicion=n.IdCondicion,
                                        Condicion=n.Condicion,
                                        NumeroConector=n.NumeroConector,
                                        IdTipoProducto=n.IdTipoProducto,
                                        Producto=n.Producto,
                                        Referencia=n.Referencia,
                                        TipoProducto=n.TipoProducto,
                                        IdCategoria=n.IdCategoria,
                                        Categoria=n.Categoria,
                                        IdUnidadMedida=n.IdUnidadMedida,
                                        UnidadMedida=n.UnidadMedida,
                                        IdTipoSuplidor=n.IdTipoSuplidor,
                                        TipoSuplidor=n.TipoSuplidor,
                                        IdSuplidor=n.IdSuplidor,
                                        Suplidor=n.Suplidor,
                                        CodigoBarra=n.CodigoBarra,
                                        PrecioCompra=n.PrecioCompra,
                                        PrecioVenta=n.PrecioVenta,
                                        Stock=n.Stock,
                                        StockMinimo=n.StockMinimo,
                                        PorcientoDescuento=n.PorcientoDescuento,
                                        AfectaOferta0=n.AfectaOferta0,
                                        AceptaOferta=n.AceptaOferta,
                                        ProductoAcumulativo0=n.ProductoAcumulativo0,
                                        ProductoAcumulativo=n.ProductoAcumulativo,
                                        LlevaImagen0=n.LlevaImagen0,
                                        LlevaImagen=n.LlevaImagen,
                                        UsuarioAdicion=n.UsuarioAdicion,
                                        CreadoPor=n.CreadoPor,
                                        FechaAdiciona=n.FechaAdiciona,
                                        FechaCreado=n.FechaCreado,
                                        UsuarioModifica=n.UsuarioModifica,
                                        ModificadoPor=n.ModificadoPor,
                                        FechaModifica=n.FechaModifica,
                                        FechaModificado=n.FechaModificado,
                                        Fecha=n.Fecha,
                                        AplicaParaImpuesto0=n.AplicaParaImpuesto0,
                                        EstatusProducto0=n.EstatusProducto0,
                                        EstatusProducto=n.EstatusProducto,
                                        AplicaParaImpuesto=n.AplicaParaImpuesto,
                                        NumeroSeguimiento=n.NumeroSeguimiento,
                                        CantidadRegistros=n.CantidadRegistros,
                                        ProductosConOferta=n.ProductosConOferta,
                                        ProductoProximoAgotarse=n.ProductoProximoAgotarse,
                                        ProductosAgostados=n.ProductosAgostados,
                                        CapilalInvertido=n.CapilalInvertido,
                                        GananciaAproximada=n.GananciaAproximada,
                                        Comentario=n.Comentario
                                    }).ToList();
            return ListadoProductos;
        }

        //MANTENIMIENTO DE PRODUCTOS
        public DSMarketWeb.Logic.Entidades.EntidadesInventario.EProducto MantenimientoProductos(DSMarketWeb.Logic.Entidades.EntidadesInventario.EProducto Item, string Accion) {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesInventario.EProducto Mantenimiento = null;

            var Producto = ObjData.SP_MANTENIMIENTO_PRODUCTO(
                Item.IdProducto,
                Item.NumeroConector,
                Item.IdTipoProducto,
                Item.IdCapacidad,
                Item.IdUnidadMedida,
                Item.IdMarca,
                Item.IdModelo,
                Item.IdTipoSuplidor,
                Item.IdSuplidor,
                Item.Producto,
                Item.CodigoBarra,
                Item.Referencia,
                Item.PrecioCompra,
                Item.PrecioVenta,
                Item.Stock,
                Item.StockMinimo,
                Item.PorcientoDescuento,
                Item.AfectaOferta0,
                Item.ProductoAcumulativo0,
                Item.LlevaImagen0,
                Item.UsuarioAdicion,
                Item.Comentario,
                Item.AplicaParaImpuesto0,
                Item.EstatusProducto0,
                Item.NumeroSeguimiento,
                Item.IdColor,
                Item.IdCondicion,
                Item.IdCapacidad,
                Accion);
            if (Producto != null) {
                Mantenimiento = (from n in Producto
                                 select new DSMarketWeb.Logic.Entidades.EntidadesInventario.EProducto
                                 {
                                     IdProducto=n.IdProducto,
                                     NumeroConector=n.NumeroConector,
                                     IdTipoProducto=n.IdTipoProducto,
                                     IdCategoria=n.IdCategoria,
                                     IdUnidadMedida=n.IdUnidadMedida,
                                     IdMarca=n.IdMarca,
                                     IdModelo=n.IdModelo,
                                     IdTipoSuplidor=n.IdTipoSuplidor,
                                     IdSuplidor=n.IdSuplidor,
                                     Producto=n.Descripcion,
                                     CodigoBarra=n.CodigoBarra,
                                     Referencia=n.Referencia,
                                     PrecioCompra=n.PrecioCompra,
                                     PrecioVenta=n.PrecioVenta,
                                     Stock=n.Stock,
                                     StockMinimo=n.StockMinimo,
                                     PorcientoDescuento=n.PorcientoDescuento,
                                     AfectaOferta0=n.AfectaOferta,
                                     ProductoAcumulativo0=n.ProductoAcumulativo,
                                     LlevaImagen0=n.LlevaImagen,
                                     UsuarioAdicion=n.UsuarioAdicion,
                                     FechaAdiciona=n.FechaAdiciona,
                                     UsuarioModifica=n.UsuarioModifica,
                                     FechaModifica=n.FechaModifica,
                                     Fecha=n.Fecha,
                                     Comentario=n.Comentario,
                                     AplicaParaImpuesto0=n.AplicaParaImpuesto,
                                     EstatusProducto0=n.EstatusProducto,
                                     NumeroSeguimiento=n.NumeroSeguimiento,
                                     IdColor=n.IdColor,
                                     IdCondicion=n.IdCondicion,
                                     IdCapacidad=n.IdCapacidad
                                 }).FirstOrDefault();
            }
            return Mantenimiento;
        }
        #endregion
    }
}
