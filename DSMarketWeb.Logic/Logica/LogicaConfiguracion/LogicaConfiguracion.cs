using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Logica.LogicaConfiguracion
{
    public class LogicaConfiguracion
    {
        DSMarketWeb.Data.ConexionLINQ.BDConexionConfiguracionDataContext ObjData = new Data.ConexionLINQ.BDConexionConfiguracionDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["DSMarketWEBConexion"].ConnectionString);

        #region MANTENIMIENTO DE INFORMACION DE EMPRESA
        //BUSCA INFORMACION DE EMPRESA
        public List<DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EInformacionEmpresa> BuscaInformacionEmpresa()
        {
            ObjData.CommandTimeout = 999999999;

            var Buscar = (from n in ObjData.SP_SACAR_INFORMACION_EMPRESA()
                          select new DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EInformacionEmpresa
                          {
                              IdInformacionEmpresa = n.IdInformacionEmpresa,
                              NombreEmpresa = n.NombreEmpresa,
                              RNC = n.RNC,
                              Direccion = n.Direccion,
                              Email = n.Email,
                              Email2 = n.Email2,
                              Facebook = n.Facebook,
                              Instagran = n.Instagran,
                              Telefonos = n.Telefonos,
                              IdLogoEmpresa = n.IdLogoEmpresa,
                              LogoEmpresa = n.LogoEmpresa
                          }).ToList();
            return Buscar;

        }

        //MANTENIMIENTO DE INFORMACION DE EMPRESA
        public DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EInformacionEmpresa ActualizarInformacion(DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EInformacionEmpresa Item, string Accion) {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EInformacionEmpresa Informacion = null;

            var Actualziar = ObjData.SP_MODIFICAR_INFORMACION_EMPRESA(
                Item.IdInformacionEmpresa,
                Item.NombreEmpresa,
                Item.RNC,
                Item.Direccion,
                Item.Email,
                Item.Email2,
                Item.Facebook,
                Item.Instagran,
                Item.Telefonos,
                Item.IdLogoEmpresa,
                Accion);
            if (Actualziar != null) {
                Informacion = (from n in Actualziar
                               select new DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EInformacionEmpresa
                               {
                                   IdInformacionEmpresa = n.IdInformacionEMpres,
                                   NombreEmpresa = n.NombreEmpresa,
                                   RNC = n.RNC,
                                   Direccion = n.Direccion,
                                   Email = n.Email,
                                   Email2 = n.Email2,
                                   Facebook = n.Facebook,
                                   Instagran = n.Instagram,
                                   Telefonos = n.Telefonos,
                                   IdLogoEmpresa = n.IdLogoEmpresa
                               }).FirstOrDefault();
            }
            return Informacion;
        }
        #endregion

        #region LISTAS
        public List<DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EListas> BuscaListas(string NombreLista = null, string PrimerFiltro = null, string SegundoFiltro = null, string TercerFiltro = null, string CuartoFiltro = null, string QuintoFiltro = null) {
            ObjData.CommandTimeout = 999999999;

            var Listado = (from n in ObjData.SP_BUSCA_LISTAS_WEB(NombreLista, PrimerFiltro, SegundoFiltro, TercerFiltro, CuartoFiltro, QuintoFiltro)
                           select new DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EListas
                           {
                               Descripcion = n.Descripcion,
                               PrimerValor = n.PrimerValor,
                               SegundoValor = n.SegundoValor,
                               TerceValor = n.TerceValor
                           }).ToList();
            return Listado;
        }
        #endregion

        #region MANTENIMIENTO DE PORCIENTO DE DESCUENTO POR DEFECTO DE PRODUCTOS
        public List<DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EPorcientoDescuentoProductoPorDefecto> BuscaPorcientoDescuentoPoeDefecto(int? IdPorcientoDescuento = null) {
            ObjData.CommandTimeout = 999999999;

            var Listado = (from n in ObjData.SP_BUSCA_PORCIENTO_DESCUENTO_PRODUCTO_POR_DEFECTO(IdPorcientoDescuento)
                           select new DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EPorcientoDescuentoProductoPorDefecto
                           {
                               IdPorcientoDescuento = n.IdPorcientoDescuento,
                               PorcientoDescuento = n.PorcientoDescuento
                           }).ToList();
            return Listado;
        }
        #endregion

        #region MANTENIMIENTO DE CONFIGURACION GENERAL
        //BUSCA CONFIGURACION GENERAL
        public List<DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EConfiguracionGeneral> BuscaConfiguracionGeneral(int? IdConfiguracionGeneral = null) {
            ObjData.CommandTimeout = 999999999;

            var Listado = (from n in ObjData.SP_BUSCA_CONFIGURACION_GENERAL(IdConfiguracionGeneral)
                           select new DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EConfiguracionGeneral
                           {
                               IdConfiguracionGeneral = n.IdConfiguracionGeneral,
                               Descripcion = n.Descripcion,
                               Estatus0 = n.Estatus0,
                               Estatus = n.Estatus,
                               CantidadActivos = n.CantidadActivos,
                               CantidadInactivos = n.CantidadInactivos
                           }).ToList();
            return Listado;
        }
        //MANTENIMIENTO DE CONFIGURACION GENERAL
        public DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EConfiguracionGeneral MantenimientoConfiguracionGeneral(DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EConfiguracionGeneral Item, string Accion) {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EConfiguracionGeneral Mantenimiento = null;

            var ConfguracionGeneral = ObjData.SP_MANTENIMIENTO_CONFIGURACION_GENERAL(
                Item.IdConfiguracionGeneral,
                Item.Descripcion,
                Item.Estatus0,
                Accion);
            if (ConfguracionGeneral != null) {
                Mantenimiento = (from n in ConfguracionGeneral
                                 select new DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EConfiguracionGeneral
                                 {
                                     IdConfiguracionGeneral = n.IdConfiguracionGeneral,
                                     Descripcion = n.Descripcion,
                                     Estatus0 = n.Estatus
                                 }).FirstOrDefault();
            }
            return Mantenimiento;
        }
        #endregion

        #region BUSCAR LAS IMAGENES POR DEFECTO DE DEL SISTEMA
        public List<DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EBuscaImagenesSistema> BuscaImagenesSistema(decimal? IdLogoSistema = null) {
            ObjData.CommandTimeout = 999999999;

            var Listado = (from n in ObjData.SP_BUSCAR_IMAGENES_DEL_SISTEMA(IdLogoSistema)
                           select new DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EBuscaImagenesSistema
                           {
                               IdLogoEmpresa = n.IdLogoEmpresa,
                               Nombre = n.Nombre,
                               LogoEmpresa = n.LogoEmpresa
                           }).ToList();
            return Listado;
        }
        #endregion

        #region MANTENIMIENTO DE IMPUESTOS DE VENTA
        //LISTADO DE IMPUESTOS DE VENTAS
        public List<DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EImpuestoVenta> BuscaImpuestoVenta(int? IdImpuesto = null) {
            ObjData.CommandTimeout = 999999999;

            var Listado = (from n in ObjData.SP_BUSCA_IMPUESTO_VENTA(IdImpuesto)
                           select new DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EImpuestoVenta
                           {
                               IdImpuesto=n.IdImpuesto,
                               Descripcion=n.Descripcion,
                               PorcientoImpuesto=n.PorcientoImpuesto,
                               Operacion0=n.Operacion0,
                               Operacion=n.Operacion
                           }).ToList();
            return Listado;
        }

        //MANTENIMIENTO DE IMPUESTO DE VENTA
        public DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EImpuestoVenta ModificarImpuestoVenta(DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EImpuestoVenta Item, string Accion) {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EImpuestoVenta Modificar = null;

            var ImpuestoVenta = ObjData.SP_MODIFICAR_IMPUESTO_VENTA(
                Item.IdImpuesto,
                Item.Descripcion,
                Item.PorcientoImpuesto,
                Item.Operacion0,
                Accion);
            if (ImpuestoVenta != null) {
                Modificar = (from n in ImpuestoVenta
                             select new DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EImpuestoVenta
                             {
                                 IdImpuesto=n.IdImpuesto,
                                 Descripcion=n.Descripcion,
                                 PorcientoImpuesto=n.PorcientoImpuesto,
                                 Operacion0=n.Operacion
                             }).FirstOrDefault();
            }
            return Modificar;
        }
        #endregion

        #region MANTENIMIENTO DE COMPROBANTES FISCALES
        //LISTADO DE COMPROBANTES FISCALES
        public List<DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EComprobantesFuscales> BuscaComprobantesFiscales(decimal? IdComprbanteFiscal = null) {
            ObjData.CommandTimeout = 999999999;

            var Listado = (from n in ObjData.SP_BUSCA_COMPROBANTES_FISCALES(IdComprbanteFiscal)
                           select new DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EComprobantesFuscales
                           {
                               IdComprobante=n.IdComprobante,
                               Comprobante=n.Comprobante,
                               Serie=n.Serie,
                               TipoComprobante=n.TipoComprobante,
                               Secuencia=n.Secuencia,
                               SecuenciaInicial=n.SecuenciaInicial,
                               SecuenciaFinal=n.SecuenciaFinal,
                               Limite=n.Limite,
                               Estatus0=n.Estatus0,
                               Estatus=n.Estatus,
                               ValidoHasta=n.ValidoHasta,
                               PorDefecto0=n.PorDefecto0,
                               PorDefecto=n.PorDefecto,
                               Posiciones=n.Posiciones,
                               CobroPorcientoAdicional=n.CobroPorcientoAdicional,
                               LibreImpuesto0=n.LibreImpuesto0,
                               LibreImpuesto=n.LibreImpuesto
                           }).ToList();
            return Listado;
        
        }

        //MANTENIMIENTO DE COMPROBANTES FISCALES
        public DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EComprobantesFuscales MantenimientoComprobantesFiscales(DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EComprobantesFuscales Item, string Accion) {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EComprobantesFuscales Mantenimiento = null;

            var ComprobanteFiscal = ObjData.SP_MANTENIMIENTO_COMPROBANTE_FISCALES(
                Item.IdComprobante,
                Item.Comprobante,
                Item.Serie,
                Item.TipoComprobante,
                (long)Item.Secuencia,
                (long)Item.SecuenciaInicial,
                (long)Item.Posiciones,
                (long)Item.Limite,
                Item.Estatus0,
                Item.ValidoHasta,
                Item.PorDefecto0,
                (long)Item.Posiciones,
                Item.CobroPorcientoAdicional,
                Item.LibreImpuesto0,
                Accion);

            if (ComprobanteFiscal != null) {
                Mantenimiento = (from n in ComprobanteFiscal
                                 select new DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EComprobantesFuscales
                                 {
                                     IdComprobante=n.IdComprobante,
                                     Comprobante=n.Descripcion,
                                     Serie=n.Serie,
                                     TipoComprobante=n.TipoComprobante,
                                     Secuencia=n.Secuencia,
                                     SecuenciaInicial=n.SecuenciaInicial,
                                     SecuenciaFinal=n.SecuenciaFinal,
                                     Limite=n.Limite,
                                     Estatus0=n.Estatus,
                                     ValidoHasta=n.ValidoHasta,
                                     PorDefecto0=n.PorDefecto,
                                     Posiciones=n.Posiciones,
                                     CobroPorcientoAdicional=n.CobroPorcientoAdicional,
                                     LibreImpuesto0=n.LibreImpuesto
                                 }).FirstOrDefault();
            }
            return Mantenimiento;
        }
        #endregion
    }
}
