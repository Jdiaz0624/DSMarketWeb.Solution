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


        #region MANTENIMIENTO DE CATEGORIAS
        //LISTADO DE CATEGORIAS
        public List<DSMarketWeb.Logic.Entidades.EntidadesInventario.ECategorias> BuscaCategorias(decimal? IdCategoria = null, decimal? IdTipoProducto = null, string Descripcion = null) {
            ObjData.CommandTimeout = 999999999;

            var Listado = (from n in ObjData.SP_BUSCA_CATEGORIAS_WEB(IdCategoria, IdTipoProducto, Descripcion)
                           select new DSMarketWeb.Logic.Entidades.EntidadesInventario.ECategorias
                           {
                               IdCategoria=n.IdCategoria,
                               IdTipoProducto=n.IdTipoProducto,
                               TipoProducto=n.TipoProducto,
                               Categoria=n.Categoria,
                               Estatus0=n.Estatus0,
                               Estatus=n.Estatus,
                               UsuarioAdiciona=n.UsuarioAdiciona,
                               CreadoPor=n.CreadoPor,
                               Fechaadiciona=n.Fechaadiciona,
                               FechaCreado=n.FechaCreado,
                               UsuarioModifica=n.UsuarioModifica,
                               ModificadoPor=n.ModificadoPor,
                               FechaModifica=n.FechaModifica,
                               FechaModificado=n.FechaModificado,
                               CantidadRegistros=n.CantidadRegistros
                           }).ToList();
            return Listado;
        }

        //MANTENIMIENTO DE CATEGORIAS
        public DSMarketWeb.Logic.Entidades.EntidadesInventario.ECategorias MantenimientoCategorias(DSMarketWeb.Logic.Entidades.EntidadesInventario.ECategorias Item, string Accion) {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesInventario.ECategorias Mantenimiento = null;

            var Catrgoria = ObjData.SP_MANTENIMIENTO_CATEGORIAS(
                Item.IdCategoria,
                Item.IdTipoProducto,
                Item.Categoria,
                Item.Estatus0,
                Item.UsuarioAdiciona,
                Accion);
            if (Catrgoria != null) {
                Mantenimiento = (from n in Catrgoria
                                 select new DSMarketWeb.Logic.Entidades.EntidadesInventario.ECategorias
                                 {
                                     IdCategoria=n.IdCategoria,
                                     IdTipoProducto=n.IdTipoProducto,
                                     Categoria=n.Descripcion,
                                     Estatus0=n.Estatus,
                                     UsuarioAdiciona=n.UsuarioAdiciona,
                                     Fechaadiciona=n.Fechaadiciona,
                                     UsuarioModifica=n.UsuarioModifica,
                                     FechaModifica=n.FechaModifica
                                 }).FirstOrDefault();
            }
            return Mantenimiento;
        }
        #endregion
        #region MANTENIMIENTO DE MARCAS
        //LISTADO DE MARCAS
        public List<DSMarketWeb.Logic.Entidades.EntidadesInventario.EMarcas> BuscaMarcas(decimal? IdMarca = null, decimal? IdTipoProducto = null, decimal? IdCategoria = null, string Descripcion = null) {
            ObjData.CommandTimeout = 999999999;

            var Listado = (from n in ObjData.SP_BUSCA_MARCAS_WEB(IdMarca, IdTipoProducto, IdCategoria, Descripcion)
                           select new DSMarketWeb.Logic.Entidades.EntidadesInventario.EMarcas
                           {
                               IdMarca=n.IdMarca,
                               IdTipoProducto=n.IdTipoProducto,
                               TipoProducto=n.TipoProducto,
                               IdCateoria=n.IdCateoria,
                               Categoria=n.Categoria,
                               Marca=n.Marca,
                               Estatus0=n.Estatus0,
                               Estatus=n.Estatus,
                               UsuarioAdiciona=n.UsuarioAdiciona,
                               CreadoPor=n.CreadoPor,
                               FechaAdiciona=n.FechaAdiciona,
                               FechaCreado=n.FechaCreado,
                               UsuarioModifica=n.UsuarioModifica,
                               ModificadoPor=n.ModificadoPor,
                               FechaModifica=n.FechaModifica,
                               FechaModificado=n.FechaModificado,
                               CantidadRegistros=n.CantidadRegistros
                           }).ToList();
            return Listado;
        }

        //MANTENIMIENTO DE MARCAS
        public DSMarketWeb.Logic.Entidades.EntidadesInventario.EMarcas MantenimientoMarcas(DSMarketWeb.Logic.Entidades.EntidadesInventario.EMarcas Item, string Accion) {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesInventario.EMarcas Mantenimiento = null;

            var Marcas = ObjData.SP_MANTENIMIENTO_MARCAS(
                Item.IdMarca,
                Item.Marca,
                Item.Estatus0,
                Item.UsuarioAdiciona,
                Item.IdCateoria,
                Item.IdTipoProducto,
                Accion);
            if (Marcas != null) {
                Mantenimiento = (from n in Marcas
                                 select new DSMarketWeb.Logic.Entidades.EntidadesInventario.EMarcas
                                 {
                                     IdMarca=n.IdMarca,
                                     Marca=n.Descripcion,
                                     Estatus0=n.Estatus,
                                     UsuarioAdiciona=n.UsuarioAdiciona,
                                     FechaAdiciona=n.FechaAdiciona,
                                     UsuarioModifica=n.UsuarioModifica,
                                     FechaModifica=n.FechaModifica,
                                     IdCateoria=n.IdCategoria,
                                     IdTipoProducto=n.IdTipoProducto

                                 }).FirstOrDefault();
            }
            return Mantenimiento;
        }

        #endregion
        #region MANTENIMIENTO DE TIPO DE SUPLIDORES
        //LISTADO DE TIPO DE SUPLIDORES
        public List<DSMarketWeb.Logic.Entidades.EntidadesInventario.ETipoSuplidores> BuscaTipoSuplidores(decimal? IdTipoSuplidores = null, string Descripcion = null) {
            ObjData.CommandTimeout = 999999999;

            var Listado = (from n in ObjData.SP_BUSCA_TIPO_SUPLIDORES(IdTipoSuplidores, Descripcion)
                           select new DSMarketWeb.Logic.Entidades.EntidadesInventario.ETipoSuplidores
                           {
                               IdTipoSuplidor=n.IdTipoSuplidor,
                               TipoSuplidor=n.TipoSuplidor,
                               Estatus0=n.Estatus0,
                               Estatus=n.Estatus,
                               UsuarioAdiciona=n.UsuarioAdiciona,
                               FechaAdiciona=n.FechaAdiciona,
                               CreadoPor=n.CreadoPor,
                               FechaCreado=n.FechaCreado,
                               UsuarioModifica=n.UsuarioModifica,
                               FechaModifica=n.FechaModifica,
                               ModificadoPor=n.ModificadoPor,
                               FechaModificado=n.FechaModificado,
                               CantidadRegistros=n.CantidadRegistros   
                           }).ToList();
            return Listado;
        }

        //MANTENIMIENTO DE TIPO DE SUPLIDORES
        public DSMarketWeb.Logic.Entidades.EntidadesInventario.ETipoSuplidores MantenimientoTipoSuplidores(DSMarketWeb.Logic.Entidades.EntidadesInventario.ETipoSuplidores Item, string Accion) {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesInventario.ETipoSuplidores Mantenimiento = null;

            var TipoSuplidor = ObjData.SP_MANTENIMIENTO_TIPO_SUPLIDOR(
                Item.IdTipoSuplidor,
                Item.TipoSuplidor,
                Item.Estatus0,
                Item.UsuarioAdiciona,
                Accion);
            if (TipoSuplidor != null) {
                Mantenimiento = (from n in TipoSuplidor
                                 select new DSMarketWeb.Logic.Entidades.EntidadesInventario.ETipoSuplidores
                                 {
                                     IdTipoSuplidor=n.IdTipoSuplidor,
                                     TipoSuplidor=n.Descripcion,
                                     Estatus0=n.Estatus,
                                     UsuarioAdiciona=n.UsuarioAdiciona,
                                     FechaAdiciona=n.FechaAdiciona,
                                     UsuarioModifica=n.UsuarioModifica,
                                     FechaModifica=n.FechaModifica
                                 }).FirstOrDefault();
            }
            return Mantenimiento;
        }
        #endregion
        #region MANTENIMIENTO DE SUPLIDORES
        //LISTADO DE SUPLIDORES
        public List<DSMarketWeb.Logic.Entidades.EntidadesInventario.ESuplidores> BuscaSuplidores(decimal? IdTipoSuplidor = null, decimal? IdSuplidor = null, string Nombre = null,string RNC = null, decimal? IdUsuarioProcesa = null) {
            ObjData.CommandTimeout = 999999999;

            var Listado = (from n in ObjData.SP_BUSCA_SUPLIDORES(IdTipoSuplidor, IdSuplidor, Nombre, RNC, IdUsuarioProcesa)
                           select new DSMarketWeb.Logic.Entidades.EntidadesInventario.ESuplidores
                           {
                               IdTipoSuplidor=n.IdTipoSuplidor,
                               TipoSuplidor=n.TipoSuplidor,
                               IdSuplidor=n.IdSuplidor,
                               Suplidor=n.Suplidor,
                               Telefono=n.Telefono,
                               Email=n.Email,
                               Direccion=n.Direccion,
                               Contacto=n.Contacto,
                               Estatus0=n.Estatus0,
                               Estatus=n.Estatus,
                               UsuarioAdiciona=n.UsuarioAdiciona,
                               CreadoPor=n.CreadoPor,
                               FechaAdiciona=n.FechaAdiciona,
                               FechaCreado=n.FechaCreado,
                               UsuarioModifica=n.UsuarioModifica,
                               FechaModifica=n.FechaModifica,
                               ModificadoPor=n.ModificadoPor,
                               FechaModificado=n.FechaModificado,
                               CantidadRegistros=n.CantidadRegistros,
                               GeneradoPor=n.GeneradoPor,
                               RNC=n.RNC,
                               ActividadEconomica=n.ActividadEconomica,
                               DireccionSuplidor=n.DireccionSuplidor,
                               NombreEmpresa=n.NombreEmpresa,
                               RNCSuplidor=n.RNCSuplidor,
                               Telefonos=n.Telefonos,
                               Facebook=n.Facebook,
                               Instagran=n.Instagran,
                               LogoEmpresa=n.LogoEmpresa
                           }).ToList();
            return Listado;
        }

        //MANTENIMIENTO DE TIPO DE SUPLIDORES
        public DSMarketWeb.Logic.Entidades.EntidadesInventario.ESuplidores MantenimientoSupoSuplidores(DSMarketWeb.Logic.Entidades.EntidadesInventario.ESuplidores Item, string Accion) {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesInventario.ESuplidores Mantenimiento = null;

            var Suplidor = ObjData.SP_MANTENIMIENTO_SUPLIDORES(
                Item.IdTipoSuplidor,
                Item.IdSuplidor,
                Item.Suplidor,
                Item.Telefono,
                Item.Email,
                Item.Direccion,
                Item.Contacto,
                Item.Estatus0,
               Item.UsuarioAdiciona,
               Item.RNC,
               Item.ActividadEconomica,
                Accion);
            if (Suplidor != null) {
                Mantenimiento = (from n in Suplidor
                                 select new DSMarketWeb.Logic.Entidades.EntidadesInventario.ESuplidores
                                 {
                                     IdTipoSuplidor=n.IdTipoSuplidor,
                                     IdSuplidor=n.IdSuplidor,
                                     Suplidor=n.Nombre,
                                     Telefono=n.Telefono,
                                     Email=n.Email,
                                     Direccion=n.Direccion,
                                     Contacto=n.Contacto,
                                     Estatus0=n.Estatus,
                                     UsuarioAdiciona=n.UsuarioAdiciona,
                                     FechaAdiciona=n.FechaAdiciona,
                                     UsuarioModifica=n.UsuarioModifica,
                                     FechaModifica=n.FechaModifica,
                                     RNCSuplidor=n.RNC,
                                     ActividadEconomica=n.ActividadEconomica
                                 }).FirstOrDefault();
            }
            return Mantenimiento;
        }
        #endregion
    }
}
