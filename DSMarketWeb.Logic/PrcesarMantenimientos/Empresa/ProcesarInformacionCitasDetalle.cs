using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Empresa
{
    public class ProcesarInformacionCitasDetalle
    {
        readonly DSMarketWeb.Logic.Logica.LogicaEmpresa.LogicaEmpresa ObjdataEmpresa = new Logica.LogicaEmpresa.LogicaEmpresa();

        private decimal NumeroConector = 0;
        private decimal IdProducto = 0;
        private decimal Precio = 0;
        private string DescripcionServicio = "";
        private string Accion = "";

        public ProcesarInformacionCitasDetalle(
            decimal NumeroConectorCON,
        decimal IdProductoCON,
        decimal PrecioCON,
        string DescripcionServicioCON,
        string AccionCON)
        {
            NumeroConector = NumeroConectorCON;
            IdProducto = IdProductoCON;
            Precio = PrecioCON;
            DescripcionServicio = DescripcionServicioCON;
            Accion = AccionCON;
        }
        /// <summary>
        /// Este metodo es para procesar la informacion del detalle de las citas
        /// </summary>
        public void ProcesarInformacion() {
            DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ECitasDetalle Procesar = new Entidades.EntidadesEmpresa.ECitasDetalle();

            Procesar.NumeroConectorCita = NumeroConector;
            Procesar.IdProducto = IdProducto;
            Procesar.Precio = Precio;
            Procesar.DescripcionProducto = DescripcionServicio;
            var MAn = ObjdataEmpresa.MantenimientoCitasDetalle(Procesar, Accion);
        }
    }
}
