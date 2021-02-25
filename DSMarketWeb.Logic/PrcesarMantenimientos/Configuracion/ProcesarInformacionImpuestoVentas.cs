using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Configuracion
{
    public class ProcesarInformacionImpuestoVentas
    {
        readonly DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion ObjData = new Logica.LogicaConfiguracion.LogicaConfiguracion();

        private int IdImpuesto = 0;
        private string Descripcion = "";
        private int PorcientoImpuesto = 0;
        private bool Operacion = false;
        private string Accion = "";

        public ProcesarInformacionImpuestoVentas(
            int IdImpuestoCON,
        string DescripcionCON,
        int PorcientoImpuestoCON,
        bool OperacionCON,
        string AccionCON)
        {
            IdImpuesto = IdImpuestoCON;
            Descripcion = DescripcionCON;
            PorcientoImpuesto = PorcientoImpuestoCON;
            Operacion = OperacionCON;
            Accion = AccionCON;
        }
        public void ProcesarInformacion() {
            DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EImpuestoVenta Procesar = new Entidades.EntidadesConfiguracion.EImpuestoVenta();

            Procesar.IdImpuesto = IdImpuesto;
            Procesar.Descripcion = Descripcion;
            Procesar.PorcientoImpuesto = PorcientoImpuesto;
            Procesar.Operacion0 = Operacion;

            var MAN = ObjData.ModificarImpuestoVenta(Procesar, Accion);
        }
    }
}
