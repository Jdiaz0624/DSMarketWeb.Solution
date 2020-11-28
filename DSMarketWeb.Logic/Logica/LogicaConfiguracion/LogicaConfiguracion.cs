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
                              IdInformacionEmpresa=n.IdInformacionEmpresa,
                              NombreEmpresa=n.NombreEmpresa,
                              RNC=n.RNC,
                              Direccion=n.Direccion,
                              Email=n.Email,
                              Email2=n.Email2,
                              Facebook=n.Facebook,
                              Instagran=n.Instagran,
                              Telefonos=n.Telefonos,
                              IdLogoEmpresa=n.IdLogoEmpresa,
                              LogoEmpresa=n.LogoEmpresa
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
                                   IdInformacionEmpresa=n.IdInformacionEMpres,
                                   NombreEmpresa=n.NombreEmpresa,
                                   RNC=n.RNC,
                                   Direccion=n.Direccion,
                                   Email=n.Email,
                                   Email2=n.Email2,
                                   Facebook=n.Facebook,
                                   Instagran=n.Instagram,
                                   Telefonos=n.Telefonos,
                                   IdLogoEmpresa=n.IdLogoEmpresa
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
                               Descripcion=n.Descripcion,
                               PrimerValor=n.PrimerValor,
                               SegundoValor=n.SegundoValor,
                               TerceValor=n.TerceValor
                           }).ToList();
            return Listado;
        }
        #endregion
    }
}
