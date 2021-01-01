using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Logica.LogicaEmpresa
{
    public class LogicaEmpresa
    {
        DSMarketWeb.Data.ConexionLINQ.BDConexionEmpresaDataContext ObjData = new Data.ConexionLINQ.BDConexionEmpresaDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["DSMarketWEBConexion"].ConnectionString);

        #region MANTENIMIENTO DE CLIENTES
        //LISTADO DE CLIENTES
        public List<DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EClientes> BuscaClientes(string IdCliente = null, decimal? IdComprobante = null, string Nombre = null, string RNC = null, bool? Estatus = null, bool? EnvioEmail = null, decimal? UsuarioProcesa = null) {

            ObjData.CommandTimeout = 999999999;

            var Listado = (from n in ObjData.SP_BUSCA_CLIENTES(IdCliente, IdComprobante, Nombre, RNC, Estatus, EnvioEmail, UsuarioProcesa)
                           select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EClientes
                           {
                               IdCliente=n.IdCliente,
                               IdComprobante=n.IdComprobante,
                               Comprobante=n.Comprobante,
                               Nombre=n.Nombre,
                               Telefono=n.Telefono,
                               IdTipoIdentificacion=n.IdTipoIdentificacion,
                               TipoIdentificacion=n.TipoIdentificacion,
                               RNC=n.RNC,
                               Direccion=n.Direccion,
                               Email=n.Email,
                               Comentario=n.Comentario,
                               Estatus0=n.Estatus0,
                               Estatus=n.Estatus,
                               EnvioEmail0=n.EnvioEmail0,
                               EnvioEmail=n.EnvioEmail,
                               UsuarioAdiciona=n.UsuarioAdiciona,
                               CreadoPor=n.CreadoPor,
                               FechaAdiciona=n.FechaAdiciona,
                               FechaCreado=n.FechaCreado,
                               UsuarioModifica=n.UsuarioModifica,
                               ModificadoPor=n.ModificadoPor,
                               FechaModifica=n.FechaModifica,
                               FechaModificado=n.FechaModificado,
                               MontoCredito=n.MontoCredito,
                               NombreEmpresa=n.NombreEmpresa,
                               RNC1=n.RNC1,
                               Direccion1=n.Direccion1,
                               Email1=n.Email1,
                               Email2=n.Email2,
                               Telefonos=n.Telefonos,
                               Facebook=n.Facebook,
                               LogoEmpresa=n.LogoEmpresa,
                               GeneradoPor=n.GeneradoPor,
                               Instagran=n.Instagran,
                               CantidadClientes=n.CantidadClientes
                           }).ToList();
            return Listado;
        }

        //MANTENIMIENTO DE DE CLIENTES
        public DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EClientes MantenimientoClientes(DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EClientes Item, string Accion) {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EClientes Mantenimiento = null;

            var Cliente = ObjData.SP_MANTENIMIENTO_CLIENTES(
                Item.IdCliente,
                Item.IdComprobante,
                Item.Nombre,
                Item.Telefono,
                Item.IdTipoIdentificacion,
                Item.RNC,
                Item.Direccion,
                Item.Email,
                Item.Comentario,
                Item.Estatus0,
                Item.UsuarioAdiciona,
                Item.MontoCredito,
                Item.EnvioEmail0,
                Accion);
            if (Cliente != null) {
                Mantenimiento = (from n in Cliente
                                 select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EClientes
                                 {
                                     IdCliente=n.IdCliente,
                                     IdComprobante=n.IdComprobante,
                                     Nombre=n.Nombre,
                                     Telefono=n.Telefono,
                                     IdTipoIdentificacion=n.IdTipoIdentificacion,
                                     RNC=n.RNC,
                                     Direccion=n.Direccion,
                                     Email=n.Email,
                                     Comentario=n.Comentario,
                                     Estatus0=n.Estatus,
                                     UsuarioAdiciona=n.UsuarioAdiciona,
                                     FechaAdiciona=n.FechaAdiciona,
                                     UsuarioModifica=n.UsuarioModifica,
                                     FechaModifica=n.FechaModifica,
                                     MontoCredito=n.MontoCredito,
                                     EnvioEmail0=n.EnvioEmail
                                 }).FirstOrDefault();
            }
            return Mantenimiento;
        }
        #endregion
    }
}
