using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Configuracion
{
    public class ProcesarInformacionConfiguracionesGenerales
    {
        readonly DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion ObjData = new Logica.LogicaConfiguracion.LogicaConfiguracion();

        private decimal IdConfiguracion = 0;
        private decimal IdModulo = 0;
        private string Descripcion = "";
        private bool Estatus = false;
        private string Accion = "";

        public ProcesarInformacionConfiguracionesGenerales(
            decimal IdConfiguracionCON,
        decimal IdModuloCON,
        string DescripcionCON,
        bool EstatusCON,
        string AccionCON)
        {
            IdConfiguracion = IdConfiguracionCON;
            IdModulo = IdModuloCON;
            Descripcion = DescripcionCON;
            Estatus = EstatusCON;
            Accion = AccionCON;
        }

        public void ProcesarInformacion() {
            DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EConfiguracionesGenerales Procesar = new Entidades.EntidadesConfiguracion.EConfiguracionesGenerales();

            Procesar.IdConfiguracion = IdConfiguracion;
            Procesar.IdModulo = IdModulo;
            Procesar.Descripcion = Descripcion;
            Procesar.Estatus0 = Estatus;

            var MAN = ObjData.ModificarConfiguracionesGenerales(Procesar, Accion);
        }
    }
}
