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
        #region MANTENIMIENTO DE EMPLEADOS
        //LISTADO DE EMPLEADOS
        public List<DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EEmpleado> BuscaEmpleados(string IdEmpleado = null, string NombreEmpleado = null, string NumeroIdentificacion = null, string NSS = null, DateTime? FechaIngresoDesde = null, DateTime? FechaIngresoHasta = null, bool? Estatus = null, decimal? TipoEmpleado = null, decimal? TipoNomina = null, decimal? IdDepartamento = null, decimal? IdCargo = null, decimal? IdUsuarioProcesa = null) {
            ObjData.CommandTimeout = 999999999;

            var Listado = (from n in ObjData.SP_BUSCA_EMPLEADO(IdEmpleado, NombreEmpleado, NumeroIdentificacion, NSS, FechaIngresoDesde, FechaIngresoHasta, Estatus, TipoEmpleado, TipoNomina, IdDepartamento, IdCargo, IdUsuarioProcesa)
                           select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EEmpleado
                           {
                               IdEmpleado=n.IdEmpleado,
                               Nombre=n.Nombre,
                               Apellido=n.Apellido,
                               NombreEmpleado=n.NombreEmpleado,
                               IdTipoIdentificacion=n.IdTipoIdentificacion,
                               TipoIdentificacion=n.TipoIdentificacion,
                               NumeroIdentificacion=n.NumeroIdentificacion,
                               IdNacionalidad=n.IdNacionalidad,
                               Nacionalidad=n.Nacionalidad,
                               NSS=n.NSS,
                               Direccion=n.Direccion,
                               IdTipoEmpleado=n.IdTipoEmpleado,
                               TipoEmpleado=n.TipoEmpleado,
                               IdTioNomina=n.IdTioNomina,
                               TipoNomina=n.TipoNomina,
                               IdDepartamento=n.IdDepartamento,
                               Departamento=n.Departamento,
                               IdCargo=n.IdCargo,
                               Cargo=n.Cargo,
                               Telefono1=n.Telefono1,
                               Telefono2=n.Telefono2,
                               Email=n.Email,
                               IdEstadoCivil=n.IdEstadoCivil,
                               EstadoCivil=n.EstadoCivil,
                               Sueldo=n.Sueldo,
                               OtrosIngresos=n.OtrosIngresos,
                               IdFormaPago=n.IdFormaPago,
                               FormaPago=n.FormaPago,
                               FechaIngreso0=n.FechaIngreso0,
                               FechaIngreso=n.FechaIngreso,
                               FechaNacimiento0=n.FechaNacimiento0,
                               FechaNacimiento=n.FechaNacimiento,
                               Estatus0=n.Estatus0,
                               Estatus=n.Estatus,
                               AplicaParaComision0=n.AplicaParaComision0,
                               AplicaParaComision=n.AplicaParaComision,
                               LlevaImagen0=n.LlevaImagen0,
                               LlevaImagen=n.LlevaImagen,
                               PorcientoCOmisionVentas=n.PorcientoCOmisionVentas,
                               PorcientoComsiionServicio=n.PorcientoComsiionServicio,
                               Foto=n.Foto,
                               NombreEmpresa=n.NombreEmpresa,
                               RNC=n.RNC,
                               Telefonos=n.Telefonos,
                               Email1=n.Email1,
                               Email2=n.Email2,
                               Direccion1=n.Direccion1,
                               Instagran=n.Instagran,
                               Facebook=n.Facebook,
                               LogoEmpresa=n.LogoEmpresa,
                               GeneradoPor=n.GeneradoPor,
                               CantidadRegistros=n.CantidadRegistros,
                               CantidadActivos=n.CantidadActivos,
                               CantidadInactivos=n.CantidadInactivos
                           }).ToList();
            return Listado;
        }

        //MANTENIMIENTO DE EMPLEADOS
        public DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EEmpleado MantenimientoEmpleado(DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EEmpleado Item, string Accion) {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EEmpleado Mantenimiento = null;

            var Empleado = ObjData.SP_MANTENIMIENTO_EMPLEADOS(
                Item.IdEmpleado,
                Item.Nombre,
                Item.Apellido,
                Item.IdTipoIdentificacion,
                Item.NumeroIdentificacion,
                Item.IdNacionalidad,
                Item.NSS,
                Item.Direccion,
                Item.IdTipoEmpleado,
                Item.IdTioNomina,
                Item.IdDepartamento,
                Item.IdCargo,
                Item.Telefono1,
                Item.Telefono2,
                Item.Email,
                Item.IdEstadoCivil,
                Item.Sueldo,
                Item.OtrosIngresos,
                Item.IdFormaPago,
                Item.FechaIngreso0,
                Item.FechaNacimiento0,
                Item.Estatus0,
                Item.AplicaParaComision0,
                Item.PorcientoCOmisionVentas,
                Item.PorcientoComsiionServicio,
                Item.IdSexo,
                Item.LlevaImagen0,
                Accion);
            if (Empleado != null) {
                Mantenimiento = (from n in Empleado
                                 select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EEmpleado
                                 {
                                     IdEmpleado=n.IdEmpleado,
                                     Nombre=n.Nombre,
                                     Apellido=n.Apellido,
                                     IdTipoIdentificacion=n.IdTipoIdentificacion,
                                     NumeroIdentificacion=n.NumeroIdentificacion,
                                     IdNacionalidad=n.IdNacionalidad,
                                     NSS=n.NSS,
                                     Direccion=n.Direccion,
                                     IdTipoEmpleado=n.IdTipoEmpleado,
                                     IdTioNomina=n.IdTioNomina,
                                     IdDepartamento=n.IdDepartamento,
                                     IdCargo=n.IdCargo,
                                     Telefono1=n.Telefono1,
                                     Telefono2=n.Telefono2,
                                     Email=n.Email,
                                     IdEstadoCivil=n.IdEstadoCivil,
                                     Sueldo=n.Sueldo,
                                     OtrosIngresos=n.OtrosIngresos,
                                     IdFormaPago=n.IdFormaPago,
                                     FechaIngreso0=n.FechaIngreso,
                                     FechaNacimiento0=n.FechaNacimiento,
                                     Estatus0=n.Estatus,
                                     AplicaParaComision0=n.AplicaParaComision,
                                     PorcientoCOmisionVentas=n.PorcientoCOmisionVentas,
                                     PorcientoComsiionServicio=n.PorcientoComsiionServicio,
                                     IdSexo=n.IdSexo,
                                     LlevaImagen0=n.LlevaImagen
                                 }).FirstOrDefault();
            }
            return Mantenimiento;
        }
        #endregion
    }
}
