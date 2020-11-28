using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Logica.LogicaSeguridad
{
    public class LogicaSeguridad
    {
        DSMarketWeb.Data.ConexionLINQ.BDCOnexionSeguridadDataContext ObjData = new Data.ConexionLINQ.BDCOnexionSeguridadDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["DSMarketWEBConexion"].ConnectionString);

        #region MANTENIMIENTO DE USUARIOS
        //LISTADO DE USUARIOS
        public List<DSMarketWeb.Logic.Entidades.EntidadesSeguridad.EUsuario> BuscaUsuarios(decimal? IdUsuario = null, string UsuarioLogin = null, string Clave = null, string Usuario = null, string Persona = null)
        {
            ObjData.CommandTimeout = 999999999;

            var Buscar = (from n in ObjData.SP_BUSCA_USUARIOS_WEB(IdUsuario, UsuarioLogin, Clave, Usuario, Persona)
                          select new DSMarketWeb.Logic.Entidades.EntidadesSeguridad.EUsuario
                          {
                              IdUsuario = n.IdUsuario,
                              IdNivelAcceso = n.IdNivelAcceso,
                              Nivel = n.Nivel,
                              Usuario = n.Usuario,
                              Clave = n.Clave,
                              Persona = n.Persona,
                              Estatus0 = n.Estatus0,
                              Estatus = n.Estatus,
                              CambiaClave0 = n.CambiaClave0,
                              CambiaClave = n.CambiaClave,
                              UsuarioAdicona = n.UsuarioAdicona,
                              CreadoPor = n.CreadoPor,
                              FechaAdiciona0 = n.FechaAdiciona0,
                              FechaCreado = n.FechaCreado,
                              UsuarioModifica = n.UsuarioModifica,
                              ModificadoPor = n.ModificadoPor,
                              FechaModifica0 = n.FechaModifica0,
                              FechaModificado = n.FechaModificado,
                              CantidadUsuarios = n.CantidadUsuarios,
                              CantidadActivos = n.CantidadActivos,
                              CantidadInactivos = n.CantidadInactivos
                          }).ToList();
            return Buscar;
        }


        //MANTENIMIENTO DE USUARUOS
        public DSMarketWeb.Logic.Entidades.EntidadesSeguridad.EUsuario MantenimientoUsuarios(DSMarketWeb.Logic.Entidades.EntidadesSeguridad.EUsuario Item, string Accion)
        {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesSeguridad.EUsuario MAntenimiento = null;

            var Usuario = ObjData.SP_MANTENIMIENTO_USUARIO(
                Item.IdUsuario,
                Item.IdNivelAcceso,
                Item.Usuario,
                Item.Clave,
                Item.Persona,
                Item.Estatus0,
                Item.CambiaClave0,
                Item.UsuarioAdicona,
                Accion);
            if (Usuario != null)
            {
                MAntenimiento = (from n in Usuario
                                 select new DSMarketWeb.Logic.Entidades.EntidadesSeguridad.EUsuario
                                 {
                                     IdUsuario = n.IdUsuario,
                                     IdNivelAcceso = n.IdNivelAcceso,
                                     Usuario = n.Usuario,
                                     Clave = n.Clave,
                                     Persona = n.Persona,
                                     Estatus0 = n.Estatus,
                                     CambiaClave0 = n.CambiaClave,
                                     UsuarioAdicona = n.UsuarioAdicona,
                                     FechaAdiciona0 = n.FechaAdiciona,
                                     UsuarioModifica = n.UsuarioModifica,
                                     FechaModifica0 = n.FechaModifica
                                 }).FirstOrDefault();
            }
            return MAntenimiento;
        }
        #endregion
        #region MANTENIMIENTO DE CANTIDAD DE INTENTOS FALLISOD DEL LOGIN
        public List<DSMarketWeb.Logic.Entidades.EntidadesSeguridad.ECantidadIntentosLogin> BuscaIntentosFallidosLogin(int? IdContadorBloqueo = null) {
            ObjData.CommandTimeout = 99999999;

            var Buscar = (from n in ObjData.SP_BUSCA_CANTIDAD_INTENTOS_LOGIN(IdContadorBloqueo)
                          select new DSMarketWeb.Logic.Entidades.EntidadesSeguridad.ECantidadIntentosLogin
                          {
                              IdContadorBloqueo=n.IdContadorBloqueo,
                              Estatus=n.Estatus,
                              Estatus0=n.Estatus0,
                              CantidadIntentos=n.CantidadIntentos
                          }).ToList();
            return Buscar;
        }
        public DSMarketWeb.Logic.Entidades.EntidadesSeguridad.ECantidadIntentosLogin ActualizarCantidadIntentos(DSMarketWeb.Logic.Entidades.EntidadesSeguridad.ECantidadIntentosLogin Item, string Accion) {
            ObjData.CommandTimeout = 999999999;

            DSMarketWeb.Logic.Entidades.EntidadesSeguridad.ECantidadIntentosLogin Actualizar = null;

            var CantidadIntentosLogin = ObjData.SP_ACTUALIZAR_CANTIDAD_INTENTOS_LOGIN(
                Item.IdContadorBloqueo,
                Item.Estatus0
                , Item.CantidadIntentos,
                Accion);
            if (CantidadIntentosLogin != null) {
                Actualizar = (from n in CantidadIntentosLogin
                              select new DSMarketWeb.Logic.Entidades.EntidadesSeguridad.ECantidadIntentosLogin
                              {
                                  IdContadorBloqueo=n.IdContadorBloqueo,
                                  Estatus0=n.Estatus,
                                  CantidadIntentos=n.CantidadIntentos
                              }).FirstOrDefault();
            }
            return Actualizar;
        }
        #endregion
    }
}
