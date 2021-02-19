using System;
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
    }
}
