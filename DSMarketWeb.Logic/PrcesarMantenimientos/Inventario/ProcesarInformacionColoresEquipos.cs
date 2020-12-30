using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Inventario
{
    public class ProcesarInformacionColoresEquipos
    {
        readonly DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario ObjData = new Logica.LogicaInventario.LogicaInventario();

        private decimal IdColor = 0;
        private string Color = "";
        private bool Estatus = false;
        private decimal UsuarioProcesa = 0;
        private string Accion = "";

        public ProcesarInformacionColoresEquipos(
            decimal IdColorCON,
            string ColorCON,
            bool EstatusCON,
            decimal UsuarioProcesaCON,
            string AccionCON)
        {
            IdColor = IdColorCON;
            Color = ColorCON;
            Estatus = EstatusCON;
            UsuarioProcesa = UsuarioProcesaCON;
            Accion = AccionCON;
        }
        public void ProcesarInformacionColores() {
            DSMarketWeb.Logic.Entidades.EntidadesInventario.EColores Procesar = new Entidades.EntidadesInventario.EColores();

            Procesar.IdColor = IdColor;
            Procesar.Color = Color;
            Procesar.Estatus0 = Estatus;
            Procesar.UsuarioAdiciona = UsuarioProcesa;
            Procesar.FechaAdiciona = DateTime.Now;
            Procesar.UsuarioModifica = UsuarioProcesa;
            Procesar.FechaModifica = DateTime.Now;

            var MAN = ObjData.MantenimientoColores(Procesar, Accion);
        }
    }
}
