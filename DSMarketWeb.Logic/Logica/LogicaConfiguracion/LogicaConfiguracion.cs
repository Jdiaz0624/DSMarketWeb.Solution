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

        #region MANTENIMIENTO DE POLITICAS DE EMPRESA
        /// <summary>
        /// Listado de Politicas de Empresa
        /// </summary>
        /// <param name="IdPolitica"></param>
        /// <returns></returns>
        public List<DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EPoliticaEmpresa> BuscaPoliticasEmpresa(decimal? IdPolitica = null) {
            ObjData.CommandTimeout = 999999999;

            var Listado = (from n in ObjData.SP_BUSCA_POLITICAS_EMPRESA(IdPolitica)
                           select new DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EPoliticaEmpresa
                           {
                               IdPolitica=n.IdPolitica,
                               Politica1=n.Politica1,
                               Politica2=n.Politica2,
                               Politica3=n.Politica3,
                               Politica4=n.Politica4,
                               Politica5=n.Politica5,
                               Politica6=n.Politica6,
                               Politica7=n.Politica7,
                               Politica8=n.Politica8,
                               Politica9=n.Politica9,
                               Politica10=n.Politica10
                           }).ToList();
            return Listado;
        }

        /// <summary>
        /// Modificar las politicas de la empresa
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="Accion"></param>
        /// <returns></returns>
        public DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EPoliticaEmpresa ModificarPoliticasEmpresa(DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EPoliticaEmpresa Item, string Accion) {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EPoliticaEmpresa Modificar = null;

            var PoliticasEmpresa = ObjData.SP_MODIFICAR_POLITICAS_EMPRESA(
                Item.IdPolitica,
                Item.Politica1,
                Item.Politica2,
                Item.Politica3,
                Item.Politica4,
                Item.Politica5,
                Item.Politica6,
                Item.Politica7,
                Item.Politica8,
                Item.Politica9,
                Item.Politica10,
                Accion);
            if (PoliticasEmpresa != null) {
                Modificar = (from n in PoliticasEmpresa
                             select new DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EPoliticaEmpresa
                             {
                                 IdPolitica=n.IdPolitica,
                                 Politica1=n.Politica1,
                                 Politica2=n.Politica2,
                                 Politica3=n.Politica3,
                                 Politica4=n.Politica4,
                                 Politica5=n.Politica5,
                                 Politica6=n.Politica6,
                                 Politica7=n.Politica7,
                                 Politica8=n.Politica8,
                                 Politica9=n.Politica9,
                                 Politica10=n.Politica10
                             }).FirstOrDefault();
            }
            return Modificar;
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


        #region MANTENIMIENTO DE CONFIGURACIONES GENERALES DEL SISTEMA
        //LISTADO DE CONFIGURACIONES GENERALES DEL SISTEMA
        public List<DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EConfiguracionesGenerales> BuscaConfiguracionesGenerales(decimal? IdConfiguracion = null, decimal? IdModulo = null) {
            ObjData.CommandTimeout = 999999999;

            var Listado = (from n in ObjData.SP_BUSCA_CONFIGURACIONES_GENERALES(IdConfiguracion, IdModulo)
                           select new DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EConfiguracionesGenerales
                           {
                              IdConfiguracion=n.IdConfiguracion,
                              IdModulo=n.IdModulo,
                              Modulo=n.Modulo,
                              Descripcion=n.Descripcion,
                              Estatus=n.Estatus,
                              Estatus0=n.Estatus0
                           }).ToList();
            return Listado;
        }

        /// <summary>
        /// Este metodo es para modificar las configuraciones generales, solo modifica de activo a inactivo.
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="Accion"></param>
        /// <returns></returns>
        public DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EConfiguracionesGenerales ModificarConfiguracionesGenerales(DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EConfiguracionesGenerales Item, string Accion) {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EConfiguracionesGenerales Modificar = null;

            var Configuraciones = ObjData.SP_MODIFICAR_CONFIGURACIONES_GENERALES(
                Item.IdConfiguracion,
                Item.IdModulo,
                Item.Descripcion,
                Item.Estatus0,
                Accion);
            if (Configuraciones != null) {
                Modificar = (from n in Configuraciones
                             select new DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EConfiguracionesGenerales
                             {
                                 IdConfiguracion=n.IdConfiguracion,
                                 IdModulo=n.IdModulo,
                                 Descripcion=n.Descripcion,
                                 Estatus0=n.Estatus
                             }).FirstOrDefault();
            }
            return Modificar;
        }
        #endregion
    }
}
