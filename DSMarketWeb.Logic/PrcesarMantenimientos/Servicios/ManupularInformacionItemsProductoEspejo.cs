using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Servicios
{
    public class ManupularInformacionItemsProductoEspejo
    {
        readonly DSMarketWeb.Logic.Logica.LogicaServicio.LogicaServicio ObjData = new Logica.LogicaServicio.LogicaServicio();

        private decimal IdProducto = 0;
        private decimal NuemroConector = 0;
        private string Descripcion = "";
        private decimal Cantidad = 0;
        private bool ProductoAcumulativo = false;
        private decimal IdUsuario = 0;
        private string Accion = "";

        public ManupularInformacionItemsProductoEspejo(
        decimal IdProductoCON,
        decimal NuemroConectorCON,
        string DescripcionCON,
        decimal CantidadCON,
        bool ProductoAcumulativoCON,
        decimal IdUsuarioCON,
        string AccionCON)
        {
            IdProducto = IdProductoCON;
            NuemroConector = NuemroConectorCON;
            Descripcion = DescripcionCON;
            Cantidad = CantidadCON;
            ProductoAcumulativo = ProductoAcumulativoCON;
            IdUsuario = IdUsuarioCON;
            Accion = AccionCON;
        }

        public void ProcesarInformacion() {
            DSMarketWeb.Logic.Entidades.EntidadesServicio.EProductoEspejo Procesar = new Entidades.EntidadesServicio.EProductoEspejo();

            Procesar.IdProducto = IdProducto;
            Procesar.NumeroConector = NuemroConector;
            Procesar.Descripcion = Descripcion;
            Procesar.Cantidad = Cantidad;
            Procesar.ProductoAcumulativo = ProductoAcumulativo;
            Procesar.IdUsuario = IdUsuario;

            var MAN = ObjData.ManipularItemsProductoEspejo(Procesar, Accion);
        }
    }
}
