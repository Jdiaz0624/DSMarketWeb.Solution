using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Comunes
{
    public class ValidarConfiguracionGenera
    {
        readonly DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion ObjData = new Logica.LogicaConfiguracion.LogicaConfiguracion();

       public enum ConceptoConfiguracionGeneral
        {
            USAR_COMPROBANTES_FISCALES = 1,
            FACTURA_INGLES = 2,
            FACTURA_PUNTO_VENTA = 3,
            ENVIO_EMAIL_CUADRE_CAJA = 4,
            VALIDAR_USO_DE_COMPROBANTES_FISCALES = 6,
            VALIDAR_CAMPO_ESPECIAL_UNICO_INVENTARIO = 10,
            CAMPO_ESPECIAL_PRODUCTO_NUMERICO = 11,
            VALIDAR_CAMPO_ESPECIAL_PRODUCTO = 12,
            CAMPO_ESPECIAL_PRODUCTO_OBLIGATORIO = 13,
            CALCULAR_COMISIONES = 14,
            REALIZAR_VENTAS_A_CREDITO = 7,
            MOSTRAR_CHECK_LIMPIAR_PANTALLA_EN_MANTENIMIENTOS = 8,
            VALIDAR_CHECK_LIMPIAR_PANTALLA_EN_MANTENIMIENTOS = 9,
            AGREGAR_GARANTIA = 5
        }

        private int IdConfiguracionGeneral = 0;
        private string Descripcion = "";
        private bool Estatus = false;
        private string Accion = "";

        public ValidarConfiguracionGenera() { }
        /// <summary>
        /// Este constructor es para recibir el id de la configuracion general y mandar el resultado.
        /// </summary>
        /// <param name="IdConfiguracionGeneralCON"></param>
        public ValidarConfiguracionGenera(
            int IdConfiguracionGeneralCON)
        {
            IdConfiguracionGeneral = IdConfiguracionGeneralCON;
        }

        /// <summary>
        /// Este Constructor es para recibir las variables para realizar el mantenimiento de las configuraciones generales.
        /// </summary>
        /// <param name="IdConfiguracionGeneralCON"></param>
        /// <param name="DescripcionCON"></param>
        /// <param name="EstatusCON"></param>
        /// <param name="AccionCON"></param>
        public ValidarConfiguracionGenera(
             int IdConfiguracionGeneralCON,
             string DescripcionCON,
             bool EstatusCON,
             string AccionCON)
        {
            IdConfiguracionGeneral = IdConfiguracionGeneralCON;
            Descripcion = DescripcionCON;
            Estatus = EstatusCON;
            Accion = AccionCON;
        }

        public bool SacarEstatusConfiguracionGeneral() {
            bool Estatus = false;
            var SacarInfrmacion = ObjData.BuscaConfiguracionGeneral(IdConfiguracionGeneral);
            foreach (var n in SacarInfrmacion) {
                Estatus = Convert.ToBoolean(n.Estatus0);
            }
            return Estatus;
        }

        public void MantenimientoConfiguracionGeneral() {
            DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EConfiguracionGeneral Mantenimiento = new Entidades.EntidadesConfiguracion.EConfiguracionGeneral();

            Mantenimiento.IdConfiguracionGeneral = IdConfiguracionGeneral;
            Mantenimiento.Descripcion = Descripcion;
            Mantenimiento.Estatus0 = Estatus;

            var MAN = ObjData.MantenimientoConfiguracionGeneral(Mantenimiento, Accion);
        }
    }
}
