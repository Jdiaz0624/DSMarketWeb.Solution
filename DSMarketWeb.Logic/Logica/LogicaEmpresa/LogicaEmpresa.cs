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
                               IdCliente = n.IdCliente,
                               IdComprobante = n.IdComprobante,
                               Comprobante = n.Comprobante,
                               Nombre = n.Nombre,
                               Telefono = n.Telefono,
                               IdTipoIdentificacion = n.IdTipoIdentificacion,
                               TipoIdentificacion = n.TipoIdentificacion,
                               RNC = n.RNC,
                               Direccion = n.Direccion,
                               Email = n.Email,
                               Comentario = n.Comentario,
                               Estatus0 = n.Estatus0,
                               Estatus = n.Estatus,
                               EnvioEmail0 = n.EnvioEmail0,
                               EnvioEmail = n.EnvioEmail,
                               UsuarioAdiciona = n.UsuarioAdiciona,
                               CreadoPor = n.CreadoPor,
                               FechaAdiciona = n.FechaAdiciona,
                               FechaCreado = n.FechaCreado,
                               UsuarioModifica = n.UsuarioModifica,
                               ModificadoPor = n.ModificadoPor,
                               FechaModifica = n.FechaModifica,
                               FechaModificado = n.FechaModificado,
                               MontoCredito = n.MontoCredito,
                               NombreEmpresa = n.NombreEmpresa,
                               RNC1 = n.RNC1,
                               Direccion1 = n.Direccion1,
                               Email1 = n.Email1,
                               Email2 = n.Email2,
                               Telefonos = n.Telefonos,
                               Facebook = n.Facebook,
                               LogoEmpresa = n.LogoEmpresa,
                               GeneradoPor = n.GeneradoPor,
                               Instagran = n.Instagran,
                               CantidadClientes = n.CantidadClientes
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
                                     IdCliente = n.IdCliente,
                                     IdComprobante = n.IdComprobante,
                                     Nombre = n.Nombre,
                                     Telefono = n.Telefono,
                                     IdTipoIdentificacion = n.IdTipoIdentificacion,
                                     RNC = n.RNC,
                                     Direccion = n.Direccion,
                                     Email = n.Email,
                                     Comentario = n.Comentario,
                                     Estatus0 = n.Estatus,
                                     UsuarioAdiciona = n.UsuarioAdiciona,
                                     FechaAdiciona = n.FechaAdiciona,
                                     UsuarioModifica = n.UsuarioModifica,
                                     FechaModifica = n.FechaModifica,
                                     MontoCredito = n.MontoCredito,
                                     EnvioEmail0 = n.EnvioEmail
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
                               IdEmpleado = n.IdEmpleado,
                               Nombre = n.Nombre,
                               Apellido = n.Apellido,
                               NombreEmpleado = n.NombreEmpleado,
                               IdTipoIdentificacion = n.IdTipoIdentificacion,
                               TipoIdentificacion = n.TipoIdentificacion,
                               NumeroIdentificacion = n.NumeroIdentificacion,
                               IdNacionalidad = n.IdNacionalidad,
                               Nacionalidad = n.Nacionalidad,
                               NSS = n.NSS,
                               Direccion = n.Direccion,
                               IdTipoEmpleado = n.IdTipoEmpleado,
                               TipoEmpleado = n.TipoEmpleado,
                               IdTioNomina = n.IdTioNomina,
                               TipoNomina = n.TipoNomina,
                               IdDepartamento = n.IdDepartamento,
                               Departamento = n.Departamento,
                               IdCargo = n.IdCargo,
                               Cargo = n.Cargo,
                               Telefono1 = n.Telefono1,
                               Telefono2 = n.Telefono2,
                               Email = n.Email,
                               IdEstadoCivil = n.IdEstadoCivil,
                               EstadoCivil = n.EstadoCivil,
                               Sueldo = n.Sueldo,
                               OtrosIngresos = n.OtrosIngresos,
                               IdFormaPago = n.IdFormaPago,
                               FormaPago = n.FormaPago,
                               FechaIngreso0 = n.FechaIngreso0,
                               FechaIngreso = n.FechaIngreso,
                               FechaNacimiento0 = n.FechaNacimiento0,
                               FechaNacimiento = n.FechaNacimiento,
                               Estatus0 = n.Estatus0,
                               Estatus = n.Estatus,
                               AplicaParaComision0 = n.AplicaParaComision0,
                               AplicaParaComision = n.AplicaParaComision,
                               LlevaImagen0 = n.LlevaImagen0,
                               LlevaImagen = n.LlevaImagen,
                               IdSexo = n.IdSexo,
                               Sexo = n.Sexo,
                               NumeroRegistro = n.NumeroRegistro,
                               PorcientoCOmisionVentas = n.PorcientoCOmisionVentas,
                               PorcientoComsiionServicio = n.PorcientoComsiionServicio,
                               Foto = n.Foto,
                               NombreEmpresa = n.NombreEmpresa,
                               RNC = n.RNC,
                               Telefonos = n.Telefonos,
                               Email1 = n.Email1,
                               Email2 = n.Email2,
                               Direccion1 = n.Direccion1,
                               Instagran = n.Instagran,
                               Facebook = n.Facebook,
                               LogoEmpresa = n.LogoEmpresa,
                               GeneradoPor = n.GeneradoPor,
                               CantidadRegistros = n.CantidadRegistros,
                               CantidadActivos = n.CantidadActivos,
                               CantidadInactivos = n.CantidadInactivos
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
                Item.NumeroRegistro,
                Accion);
            if (Empleado != null) {
                Mantenimiento = (from n in Empleado
                                 select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EEmpleado
                                 {
                                     IdEmpleado = n.IdEmpleado,
                                     Nombre = n.Nombre,
                                     Apellido = n.Apellido,
                                     IdTipoIdentificacion = n.IdTipoIdentificacion,
                                     NumeroIdentificacion = n.NumeroIdentificacion,
                                     IdNacionalidad = n.IdNacionalidad,
                                     NSS = n.NSS,
                                     Direccion = n.Direccion,
                                     IdTipoEmpleado = n.IdTipoEmpleado,
                                     IdTioNomina = n.IdTioNomina,
                                     IdDepartamento = n.IdDepartamento,
                                     IdCargo = n.IdCargo,
                                     Telefono1 = n.Telefono1,
                                     Telefono2 = n.Telefono2,
                                     Email = n.Email,
                                     IdEstadoCivil = n.IdEstadoCivil,
                                     Sueldo = n.Sueldo,
                                     OtrosIngresos = n.OtrosIngresos,
                                     IdFormaPago = n.IdFormaPago,
                                     FechaIngreso0 = n.FechaIngreso,
                                     FechaNacimiento0 = n.FechaNacimiento,
                                     Estatus0 = n.Estatus,
                                     AplicaParaComision0 = n.AplicaParaComision,
                                     PorcientoCOmisionVentas = n.PorcientoCOmisionVentas,
                                     PorcientoComsiionServicio = n.PorcientoComsiionServicio,
                                     IdSexo = n.IdSexo,
                                     LlevaImagen0 = n.LlevaImagen,
                                     NumeroRegistro = n.NumeroRegistro
                                 }).FirstOrDefault();
            }
            return Mantenimiento;
        }

        //SACAR EL ULTIMO CODIGO GENERADO
        public List<DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ESacarCodigoEmpleadoGenerado> SacarCodigoGenerado(decimal? NumeroRegistro = null) {
            ObjData.CommandTimeout = 999999999;

            var Codigo = (from n in ObjData.SP_SACAR_CODIGO_EMPLEADO_GENERADO(NumeroRegistro)
                          select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ESacarCodigoEmpleadoGenerado
                          {
                              IdEmpleado = n.IdEmpleado
                          }).ToList();
            return Codigo;

        }

        //VALIDAR FOTO DE EMPLEADO
        public List<DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EValidarFotoEmpleado> ValidarCodigoEmpleado(decimal? IdEmpleado = null, decimal? NumeroRegistro = null) {
            ObjData.CommandTimeout = 999999999;

            var Validar = (from n in ObjData.SP_VALIDAR_FOTO_EMPLEADO(IdEmpleado, NumeroRegistro)
                           select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EValidarFotoEmpleado
                           {
                               IdEmpleado = n.IdEmpleado,
                               Foto = n.Foto,
                               NumeroRegistro = n.NumeroRegistro
                           }).ToList();
            return Validar;
        }
        #endregion

        #region MANTENIMIENTO DE DEPARTAMENTOS
        //LISTADO DE DEPARTAMENTOS
        public List<DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EDepartamentos> BuscaDepartamentos(decimal? IdDepartamento = null, string Departamento = null) {
            ObjData.CommandTimeout = 999999999;

            var Listado = (from n in ObjData.SP_BUSCA_DEPARTAMENTOS(IdDepartamento, Departamento)
                           select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EDepartamentos
                           {
                               IdDepartamento = n.IdDepartamento,
                               Departamento = n.Departamento,
                               Estatus0 = n.Estatus0,
                               Estatus = n.Estatus,
                               UsuarioAdiciona = n.UsuarioAdiciona,
                               CreadoPor = n.CreadoPor,
                               FechaAdiciona = n.FechaAdiciona,
                               FechaCreado = n.FechaCreado,
                               UsuarioModifica = n.UsuarioModifica,
                               ModificadoPor = n.ModificadoPor,
                               FechaModifica = n.FechaModifica,
                               FechaModificado = n.FechaModificado,
                               CantidadRegistros = n.CantidadRegistros
                           }).ToList();
            return Listado;
        }

        //MANTENIMIENTO DE DEPARTAMENTOS
        public DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EDepartamentos MantenimientoDepartamentos(DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EDepartamentos Item, string Accion) {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EDepartamentos Mantenimiento = null;

            var Departamento = ObjData.SP_MANTENIMIENTO_DEPARTAMENTOS(
                Item.IdDepartamento,
                Item.Departamento,
                Item.Estatus0,
                Item.UsuarioAdiciona,
                Accion);
            if (Departamento != null) {
                Mantenimiento = (from n in Departamento
                                 select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EDepartamentos
                                 {
                                     IdDepartamento = n.IdDepartamento,
                                     Departamento = n.Descripcion,
                                     Estatus0 = n.Estatus,
                                     UsuarioAdiciona = n.UsuarioAdiciona,
                                     FechaAdiciona = n.FechaAdiciona,
                                     UsuarioModifica = n.UsuarioModifica,
                                     FechaModifica = n.FechaModifica
                                 }).FirstOrDefault();
            }
            return Mantenimiento;
        }
        #endregion

        #region MANTENIMIENTO DE CARGOS
        //LISTADO DE CARGOS
        public List<DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ECargos> BuscaCargos(decimal? IdCargo = null, decimal? IdDepartamento = null, string Descripcion = null) {
            ObjData.CommandTimeout = 999999999;

            var Listado = (from n in ObjData.SP_BUSCA_CARGOS(IdCargo, IdDepartamento, Descripcion)
                           select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ECargos
                           {
                               IdCargo = n.IdCargo,
                               IdDepartamento = n.IdDepartamento,
                               Departamento = n.Departamento,
                               Cargo = n.Cargo,
                               Estatus0 = n.Estatus0,
                               Estatus = n.Estatus,
                               UsuarioAdiciona = n.UsuarioAdiciona,
                               CreadoPor = n.CreadoPor,
                               FechaAdiciona = n.FechaAdiciona,
                               FechaCreado = n.FechaCreado,
                               UsuarioModifica = n.UsuarioModifica,
                               ModificadoPor = n.ModificadoPor,
                               FechaModifica = n.FechaModifica,
                               FechaModificado = n.FechaModificado,
                               CantidadRegistros = n.CantidadRegistros
                           }).ToList();
            return Listado;
        }
        //MANTENIMIENTO DE CARGOS
        public DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ECargos MantenimientoCargos(DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ECargos Item, string Accion) {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ECargos Mantenimiento = null;

            var Cargos = ObjData.SP_MANTENIMIENTO_CARGOS(
                Item.IdCargo,
                Item.IdDepartamento,
                Item.Cargo,
                Item.Estatus0,
                Item.UsuarioAdiciona,
                Accion);
            if (Cargos != null) {
                Mantenimiento = (from n in Cargos
                                 select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ECargos
                                 {
                                     IdCargo = n.IdCargo,
                                     IdDepartamento = n.IdDepartamento,
                                     Cargo = n.Descripcion,
                                     Estatus0 = n.Estatus,
                                     UsuarioAdiciona = n.UsuarioAdiciona,
                                     FechaAdiciona = n.FechaAdiciona,
                                     UsuarioModifica = n.UsuarioModifica,
                                     FechaModifica = n.FechaModifica
                                 }).FirstOrDefault();
            }
            return Mantenimiento;
        }
        #endregion

        #region MANTENIMIENTO DE TIPO DE NOMINA
        //LISTADO DE TIPO DE NOMINA
        public List<DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ETipoNomina> BuscaTipoNomina(decimal? IdTipoNomina = null, string TipoNomina = null) {
            ObjData.CommandTimeout = 999999999;

            var Listado = (from n in ObjData.SP_BUSCA_TIPO_NOMINA(IdTipoNomina, TipoNomina)
                           select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ETipoNomina
                           {
                               IdTipoNomina = n.IdTipoNomina,
                               TipoNomina = n.TipoNomina,
                               Estatus0 = n.Estatus0,
                               Estatus = n.Estatus,
                               UsuarioAdiciona = n.UsuarioAdiciona,
                               CreadoPor = n.CreadoPor,
                               FechaAdiciona = n.FechaAdiciona,
                               FechaCreado = n.FechaCreado,
                               UsuairoModifica = n.UsuairoModifica,
                               ModificadoPor = n.ModificadoPor,
                               FechaModifica = n.FechaModifica,
                               FechaModificado = n.FechaModificado,
                               CantidadRegistros = n.CantidadRegistros
                           }).ToList();
            return Listado;
        }

        //MANTENIMIENTO DE TIPO DE NOMINA
        public DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ETipoNomina MantenimientoTipoNomina(DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ETipoNomina Item, string Accion) {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ETipoNomina Mantenimiento = null;

            var TipoNomina = ObjData.SP_MANTENIMIENTO_TIPO_NOMINA(
                Item.IdTipoNomina,
                Item.TipoNomina,
                Item.Estatus0,
                (int)Item.UsuarioAdiciona,
                Accion);
            if (TipoNomina != null) {
                Mantenimiento = (from n in TipoNomina
                                 select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ETipoNomina
                                 {
                                     IdTipoNomina=n.IdTipoNomina,
                                     TipoNomina=n.Descripcion,
                                     Estatus0=n.Estatus,
                                    UsuarioAdiciona=n.UsuarioAdiciona,
                                    FechaAdiciona=n.FechaAdiciona,
                                    UsuairoModifica=n.UsuarioAdiciona,
                                    FechaModifica=n.FechaModifica
                                 }).FirstOrDefault();
            }
            return Mantenimiento;
        
        }
        #endregion

        #region MANTENIMIENTO DE COMPRA A SUPLIDORES
        //LISTADO DE COMPRA DE SUPLIDORES
        public List<DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ECompraSuplidores> BuscaCompraSuplidores(decimal? IdCompraSuplidor = null, decimal? IdTipoSuplidor = null, decimal? IdSuplidor = null, string RNCCedula = null, DateTime? FechaCreadoDesde = null, DateTime? FechaCreadoHasta = null,decimal? UsuarioProcesa = 0) {
            ObjData.CommandTimeout = 999999999;

            var Listado = (from n in ObjData.SP_BUSCA_REGISTROS_COMPRA_SUPLIDORES(IdCompraSuplidor, IdTipoSuplidor, IdSuplidor, RNCCedula, FechaCreadoDesde, FechaCreadoHasta, UsuarioProcesa)
                           select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ECompraSuplidores
                           {
                               IdCompraSuplidor=n.IdCompraSuplidor,
                               IdTipoSuplidor=n.IdTipoSuplidor,
                               TipoSuplidor=n.TipoSuplidor,
                               IdSuplidor=n.IdSuplidor,
                               Suplidor=n.Suplidor,
                               RNCCedula=n.RNCCedula,
                               IdTipoIdentificacion=n.IdTipoIdentificacion,
                               TipoIdentificacion=n.TipoIdentificacion,
                               IdTipoBienesServicios=n.IdTipoBienesServicios,
                               TipoBienesServicios=n.TipoBienesServicios,
                               CodigoTipoBienesServicio=n.CodigoTipoBienesServicio,
                               NCF=n.NCF,
                               NCFModificado=n.NCFModificado,
                               FechaComprobante0=n.FechaComprobante0,
                               FechaComprobante=n.FechaComprobante,
                               FechaPago0=n.FechaPago0,
                               FechaPago=n.FechaPago,
                               MontoFacturadoServicios=n.MontoFacturadoServicios,
                               MontoFacturadoBienes=n.MontoFacturadoBienes,
                               TotalMontoFacturado=n.TotalMontoFacturado,
                               ITBISFacturado=n.ITBISFacturado,
                               ITBISRetenido=n.ITBISRetenido,
                               ITBISSujetoProporcionalidad=n.ITBISSujetoProporcionalidad,
                               ITBISLlevadoCosto=n.ITBISLlevadoCosto,
                               ITBISPorAdelantar=n.ITBISPorAdelantar,
                               ITBISPercibidoCompras=n.ITBISPercibidoCompras,
                               IdTipoRetencionISR=n.IdTipoRetencionISR,
                               TipoRetencionISR=n.TipoRetencionISR,
                               CodigoTipoRetencionISR=n.CodigoTipoRetencionISR,
                               MontoRetencionRenta=n.MontoRetencionRenta,
                               ISRPercibidoCompras=n.ISRPercibidoCompras,
                               ImpuestoSelectivoConsumo=n.ImpuestoSelectivoConsumo,
                               OtrosImpuestosTasa=n.OtrosImpuestosTasa,
                               MontoPropinaLegal=n.MontoPropinaLegal,
                               IdFormaPago=n.IdFormaPago,
                               FormaPago=n.FormaPago,
                               CodigoTipoPago=n.CodigoTipoPago,
                               UsuarioAdiciona=n.UsuarioAdiciona,
                               CreadoPor=n.CreadoPor,
                               FechaCreado0=n.FechaCreado0,
                               FechaCreado=n.FechaCreado,
                               ActividadEconomica=n.ActividadEconomica,
                               GeneradoPor=n.GeneradoPor,
                               NombreEmpresa=n.NombreEmpresa,
                               RNC=n.RNC,
                               Direccion=n.Direccion,
                               Telefonos=n.Telefonos,
                               Email=n.Email,
                               Email2=n.Email2,
                               Instagran=n.Instagran,
                               Facebook=n.Facebook,
                               LogoEmpresa=n.LogoEmpresa,
                               CantidadRegistros=n.CantidadRegistros,
                               ValidadoDesde=n.ValidadoDesde,
                               ValidadoHasta=n.ValidadoHasta
                           }).ToList();
            return Listado;
        }

        //MANTENIMIENTO DE COMPRA DE SUPLIDORES
        public DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ECompraSuplidores MantenimientoCompraSuplidores(DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ECompraSuplidores Item, string Accion) {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ECompraSuplidores Mantenimiento = null;

            var CompraSuplidores = ObjData.SP_MANTENIMIENTO_COMPRAS_SUPLIDORES(
                Item.IdCompraSuplidor,
                Item.IdTipoSuplidor,
                Item.IdSuplidor,
                Item.RNCCedula,
                Item.IdTipoIdentificacion,
                Item.IdTipoBienesServicios,
                Item.NCF,
                Item.NCFModificado,
                Item.FechaComprobante0,
                Item.FechaPago0,
                Item.MontoFacturadoServicios,
                Item.MontoFacturadoBienes,
                Item.TotalMontoFacturado,
                Item.ITBISFacturado,
                Item.ITBISRetenido,
                Item.ITBISSujetoProporcionalidad,
                Item.ITBISLlevadoCosto,
                Item.ITBISPorAdelantar,
                Item.ITBISPercibidoCompras,
                Item.IdTipoRetencionISR,
                Item.MontoRetencionRenta,
                Item.ISRPercibidoCompras,
                Item.ImpuestoSelectivoConsumo,
                Item.OtrosImpuestosTasa,
                Item.MontoPropinaLegal,
                Item.IdFormaPago,
                Item.UsuarioAdiciona,
                Item.FechaCreado0,
                Item.ActividadEconomica,
                Accion);
            if (CompraSuplidores != null) {
                Mantenimiento = (from n in CompraSuplidores
                                 select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ECompraSuplidores
                                 {
                                     IdCompraSuplidor=n.IdCompraSuplidor,
                                     IdTipoSuplidor=n.IdTipoSuplidor,
                                     IdSuplidor=n.IdSuplidor,
                                     RNCCedula=n.RNCCedula,
                                     IdTipoIdentificacion=n.IdTipoIdentificacion,
                                     IdTipoBienesServicios=n.IdTipoBienesServicios,
                                     NCF=n.NCF,
                                     NCFModificado=n.NCFModificado,
                                     FechaComprobante0=n.FechaComprobante,
                                     FechaPago0=n.FechaPago,
                                     MontoFacturadoServicios=n.MontoFacturadoServicios,
                                     MontoFacturadoBienes=n.MontoFacturadoBienes,
                                     TotalMontoFacturado=n.TotalMontoFacturado,
                                     ITBISFacturado=n.ITBISFacturado,
                                     ITBISRetenido=n.ITBISRetenido,
                                     ITBISSujetoProporcionalidad=n.ITBISSujetoProporcionalidad,
                                     ITBISLlevadoCosto=n.ITBISLlevadoCosto,
                                     ITBISPorAdelantar=n.ITBISPorAdelantar,
                                     ITBISPercibidoCompras=n.ITBISPercibidoCompras,
                                     IdTipoRetencionISR=n.IdTipoRetencionISR,
                                     MontoRetencionRenta=n.MontoRetencionRenta,
                                     ISRPercibidoCompras=n.ISRPercibidoCompras,
                                     ImpuestoSelectivoConsumo=n.ImpuestoSelectivoConsumo,
                                     OtrosImpuestosTasa=n.OtrosImpuestosTasa,
                                     MontoPropinaLegal=n.MontoPropinaLegal,
                                     IdFormaPago=n.IdFormaPago,
                                     UsuarioAdiciona=n.UsuarioAdiciona,
                                     FechaCreado0=n.FechaCreado

                                 }).FirstOrDefault();
            }
            return Mantenimiento;
        }
        #endregion

        #region MANTENIMIENTO DE TIPO DE EMPLEADO
        //LISTADO DE TIPO DE EMPLEADO
        public List<DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ETipoEmpleado> BucaTipoEmpleado(decimal? IdTipoEmpleado = null, string Descripcion = null) {
            ObjData.CommandTimeout = 999999999;

            var Listado = (from n in ObjData.SP_BUSCA_TIPO_EMPLEADO(IdTipoEmpleado, Descripcion)
                           select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ETipoEmpleado
                           {
                               IdTipoEmpleado=n.IdTipoEmpleado,
                               TipoEmpleado = n.TipoEmpleado,
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

        //MANTENIMIENTO DE TIPO DE EMPLEADO
        public DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ETipoEmpleado MantenimientoTipoEmpleado(DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ETipoEmpleado Item, string Accion) {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ETipoEmpleado Mantenimiento = null;

            var TipoEmpleado = ObjData.SP_MANTENIMIENTO_TIPO_EMPLEADO(
                Item.IdTipoEmpleado,
                Item.TipoEmpleado,
                Item.Estatus0,
                Item.UsuarioAdiciona,
                Accion);
            if (TipoEmpleado != null) {
                Mantenimiento = (from n in TipoEmpleado
                                 select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ETipoEmpleado
                                 {
                                     IdTipoEmpleado=n.IdTipoEmpleado,
                                     TipoEmpleado=n.Descripcion,
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

        #region MANTENIMIENTO DE TIPO DE MOVIMIENTO
        //LISTADO DE TIPO DE TIPO DE MOVIMIENTOS
        public List<DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ETipoMovimiento> BuscaTipoMovimiento(decimal? IdTipoMovimiento = null, string Descripcion=  null) {
            ObjData.CommandTimeout = 999999999;

            var Listado = (from n in ObjData.SP_BUSCA_TIPO_MOVIMIENTO(IdTipoMovimiento, Descripcion)
                           select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ETipoMovimiento
                           {
                               IdTipoMovimiento=n.IdTipoMovimiento,
                               TipoMovimiento=n.TipoMovimiento,
                               Estatus0=n.Estatus0,
                               Estatus=n.Estatus,
                               UsuarioAdiciona=n.UsuarioAdiciona,
                               CreadoPor=n.CreadoPor,
                               FechaAdiciona=n.FechaAdiciona,
                               FechaCreado=n.FechaCreado,
                               UsuarioModifica=n.UsuarioModifica,
                               ModificadoPor=n.ModificadoPor,
                               FechaModificado=n.FechaModificado,
                               FechaModifica=n.FechaModifica,
                               CantidadRegistros=n.CantidadRegistros
                           }).ToList();
            return Listado;
        }

        //MANTENIMIENTO DE TIPO DE MOVIMIENTO
        public DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ETipoMovimiento MantenimientoTipoMovimiento(DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ETipoMovimiento Item, string Accion) {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ETipoMovimiento Mantenimiento = null;

            var TipoEmpleado = ObjData.SP_MANTENIMIENTO_TIPO_MOVIMIENTO(
                Item.IdTipoMovimiento,
                Item.TipoMovimiento,
                Item.Estatus0,
                Item.UsuarioAdiciona,
                Accion);
            if (TipoEmpleado != null) {
                Mantenimiento = (from n in TipoEmpleado
                                 select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ETipoMovimiento
                                 {
                                     IdTipoMovimiento=n.IdTipoMovimiento,
                                     TipoMovimiento=n.Descripcion,
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

        #region MANTENIMIENTO DE BANCOS
        //LISTDO DE BANCOS
        public List<DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EBancos> BuscaBancos(decimal? IdBanco = null, string Banco = null) {
            ObjData.CommandTimeout = 999999999;

            var Listado = (from n in ObjData.SP_BUSCA_LISTADO_BANCOS(IdBanco, Banco)
                           select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EBancos
                           {
                               IdBanco=n.IdBanco,
                               CuentaContable=n.CuentaContable,
                               Auxiliar=n.Auxiliar,
                               Banco=n.Banco,
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

        //MANTENIMIENTO DE BANCOS
        public DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EBancos MantenimientoBancos(DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EBancos Item, string Accion) {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EBancos MantenimientoBancos = null;

            var Bancos = ObjData.SP_MANTENIMIENTO_DE_BANCOS(
                Item.IdBanco,
                Item.CuentaContable,
                Item.Auxiliar,
                Item.Banco,
                Item.Estatus0,
                Item.UsuarioAdiciona,
                Accion);
            if (Bancos != null) {
                MantenimientoBancos = (from n in Bancos
                                       select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EBancos
                                       {
                                           IdBanco=n.IdBanco,
                                           CuentaContable=n.CuentaContable,
                                           Auxiliar=n.Auxiliar,
                                           Banco=n.Nombre,
                                           Estatus0=n.Estatus,
                                           UsuarioAdiciona=n.UsuarioAdiciona,
                                           FechaAdiciona=n.FechaAdiciona,
                                           UsuarioModifica=n.UsuarioModifica,
                                           FechaModifica=n.FechaModifica,
                                       }).FirstOrDefault();
            }
            return MantenimientoBancos;
        }
        #endregion

        #region MANTENIMIENTO DE RETENCIONES
        //LISTADO DE RETENCIONES
        public List<DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ERetenciones> BuscaRetenciones(decimal? IdRetencion = null, string Descripcion = null) {
            ObjData.CommandTimeout = 999999999;

            var Listado = (from n in ObjData.SP_BUSCA_RETENCIONES(IdRetencion, Descripcion)
                           select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ERetenciones
                           {
                               IdRetencion=n.IdRetencion,
                               Retencion=n.Retencion,
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

        //MANTENIMIENTO DE RETENCIONES
        public DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ERetenciones MantenimeintoRetenciones(DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ERetenciones Item, string Accion) {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ERetenciones Mantenimiento = null;

            var Retenciones = ObjData.SP_MANTENIMIENTO_RETENCIONES(
                Item.IdRetencion,
                Item.Retencion,
                Item.Estatus0,
                Item.UsuarioAdiciona,
                Accion);
            if (Retenciones != null) {
                Mantenimiento = (from n in Retenciones
                                 select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ERetenciones
                                 {
                                     IdRetencion=n.IdRetencion,
                                     Retencion=n.Descripcion,
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

        #region MANTENIMIENTO DE PORCIENTO DE RETENCIONES
        //LISTADO DE PORCIENTO DE RETENCIONES
        public List<DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EPorcientoRetenciones> BuscaPorcientoRetenciones(decimal? IdPorcientoRetencion = null, decimal? IdRetencion = null, int? Secuencia = null) {

            ObjData.CommandTimeout = 999999999;

            var Listado = (from n in ObjData.SP_BUSCA_PORCIENTO_RETENCIONES(IdPorcientoRetencion, IdRetencion, Secuencia)
                           select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EPorcientoRetenciones
                           {
                               IdPorcientoRetencion=n.IdPorcientoRetencion,
                               IdRetencion=n.IdRetencion,
                               Retencion=n.Retencion,
                               Secuencia=n.Secuencia,
                               MontoInicial=n.MontoInicial,
                               MontoFinal=n.MontoFinal,
                               MontoSumar=n.MontoSumar,
                               PorcientoCia=n.PorcientoCia,
                               PorcientoEmpleado=n.PorcientoEmpleado,
                               Estatus0=n.Estatus0,
                               Estatus=n.Estatus,
                               UsuarioAdiciona=n.UsuarioAdiciona,
                               CreadoPor=n.CreadoPor,
                               FechaAdiciona=n.FechaAdiciona,
                               FechaCreado = n.FechaCreado,
                               UsuarioModifica = n.UsuarioModifica,
                               ModificadoPor=n.ModificadoPor,
                               FechaModifica=n.FechaModifica,
                               FechaModificado=n.FechaModificado,
                               CantidadRegistros=n.CantidadRegistros
                           }).ToList();
            return Listado;
        }

        //MANTENIMIENTO DE PORCIENTO DE RETENCIONES
        public DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EPorcientoRetenciones MantenimientoPorcientoRetenciones(DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EPorcientoRetenciones Item, string Accion) {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EPorcientoRetenciones Mantenimiento = null;

            var PorcientoRetenciones = ObjData.SP_MANTENIMIENTO_PORCIENTO_RETENCIONES(
                Item.IdPorcientoRetencion,
                Item.IdRetencion,
                Item.Secuencia,
                Item.MontoInicial,
                Item.MontoFinal,
                Item.MontoSumar,
                Item.PorcientoCia,
                Item.PorcientoEmpleado,
                Item.Estatus0,
                Item.UsuarioAdiciona,
                Accion);
            if (PorcientoRetenciones != null) {
                Mantenimiento = (from n in PorcientoRetenciones
                                 select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EPorcientoRetenciones
                                 {
                                     IdPorcientoRetencion=n.IdPorcientoRetencion,
                                     IdRetencion=n.IdRetencion,
                                     Secuencia=n.Secuencia,
                                     MontoInicial=n.MontoInicial,
                                     MontoFinal=n.MontoFinal,
                                     MontoSumar=n.MontoSumar,
                                     PorcientoCia=n.PorcientoCia,
                                     PorcientoEmpleado=n.PorcientoEmpleado,
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


        #region MANTENIMIENTO DE CITAS
        //LISTADO DE CITAS ENCABEZADO
        public List<DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ECitasEncabezado> BuscaCitasEncabezado(string IdCIta = null, decimal? IdEmpleado = null, DateTime? FechaCitaDesde = null, DateTime? FechaCitaHasta = null, string NombreCliente = null, string NumeroIdentificacion = null, bool? Estatus = null) {
            ObjData.CommandTimeout = 999999999;

            var Listado = (from n in ObjData.SP_BUSCA_CITAS_ENCABEZADO(IdCIta, IdEmpleado, FechaCitaDesde, FechaCitaHasta, NombreCliente, NumeroIdentificacion, Estatus)
                           select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ECitasEncabezado
                           {
                               IdCitas=n.IdCitas,
                               IdEmpleado=n.IdEmpleado,
                               Empleado=n.Empleado,
                               FechaCita0=n.FechaCita0,
                               FechaCita=n.FechaCita,
                               Hora=n.Hora,
                               NombreCliente=n.NombreCliente,
                               Telefono=n.Telefono,
                               Direccion=n.Direccion,
                               NumeroIdentificacion=n.NumeroIdentificacion,
                               NumeroConectorCita=n.NumeroConectorCita,
                               Estatus0=n.Estatus0,
                               Estatus=n.Estatus,
                               CantidadRegistros=n.CantidadRegistros
                           }).ToList();
            return Listado;
        }

        //MANTENIMIENTO DE ENCABEZADO DE CITAS
        public DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ECitasEncabezado MantenimientoCitaEncabeZado(DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ECitasEncabezado Item, string Accion) {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ECitasEncabezado Mantenimiento = null;

            var CitaEncabezado = ObjData.SP_MANTENIMIENTO_CITAS_ENCABEZADO(
                Item.IdCitas,
                Item.IdEmpleado,
                Item.FechaCita0,
                Item.Hora,
                Item.NombreCliente,
                Item.Telefono,
                Item.Direccion,
                Item.NumeroIdentificacion,
                Item.NumeroConectorCita,
                Item.Estatus0,
                Accion);
            if (CitaEncabezado != null) {
                Mantenimiento = (from n in CitaEncabezado
                                 select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ECitasEncabezado
                                 {
                                     IdCitas=n.IdCitas,
                                     IdEmpleado=n.IdEmpleado,
                                     FechaCita0=n.FechaCita,
                                     Hora=n.Hora,
                                     NombreCliente=n.NombreCliente,
                                     Telefono=n.Telefono,
                                     Direccion=n.Direccion,
                                     NumeroIdentificacion=n.NumeroIdentificacion,
                                     NumeroConectorCita=n.NumeroConectorCita,
                                     Estatus0=n.Estatus
                                 }).FirstOrDefault();
            }
            return Mantenimiento;
        }

        //LISTADO DE CITA DETALLE
        public List<DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ECitasDetalle> BuscaCitasDetalle(decimal? NumeroConector = null) {
            ObjData.CommandTimeout = 999999999;

            var Listado = (from n in ObjData.SP_BUSCA_CITAS_DETALLE(NumeroConector)
                           select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ECitasDetalle
                           {
                               NumeroConectorCita=n.NumeroConectorCita,
                               IdProducto=n.IdProducto,
                               Precio=n.Precio,
                               DescripcionProducto=n.DescripcionProducto,
                               CantidadRegistros=n.CantidadRegistros,
                               Total=n.Total
                           }).ToList();
            return Listado;
        }

        //MANTENIMIENTO DE CITAS DETALLE
        public DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ECitasDetalle MantenimientoCitasDetalle(DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ECitasDetalle Item, string Accion) {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ECitasDetalle Mantenimiento = null;

            var CitasDetalle = ObjData.SP_MANTENIMIENTO_DATOS_CITAS_DETALLE(
                Item.NumeroConectorCita,
                Item.IdProducto,
                Item.Precio,
                Item.DescripcionProducto,
                Accion);
            if (CitasDetalle != null) {
                Mantenimiento = (from n in CitasDetalle
                                 select new DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ECitasDetalle
                                 {
                                     NumeroConectorCita=n.NumeroConector,
                                     IdProducto=n.IdProducto,
                                     Precio=n.Precio,
                                     DescripcionProducto=n.DescripcionProducto
                                 }).FirstOrDefault();
            }
            return Mantenimiento;
        }
        #endregion
    }
}
