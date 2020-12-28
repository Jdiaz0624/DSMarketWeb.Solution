using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Inventario
{
    public class ProcesarInformacionTipoSuplidores
    {
        readonly DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario ObjData = new Logica.LogicaInventario.LogicaInventario();

        private decimal IdTipoSuplidor = 0;
        private string Descripcion = "";
        private bool Estatus = false;
        private decimal UsuarioProcesa = 0;
        private string Accion = "";

        public ProcesarInformacionTipoSuplidores(
            decimal IdTipoSuplidorCON,
            string DescripcionCON,
            bool EstatusCON,
            decimal UsuarioProcesaCON,
            string AccionCON)
        {
            IdTipoSuplidor = IdTipoSuplidorCON;
            Descripcion = DescripcionCON;
            Estatus = EstatusCON;
            UsuarioProcesa = UsuarioProcesaCON;
            Accion = AccionCON;
        }

        public void ProcesarDatosTipoSuplidor() {
            DSMarketWeb.Logic.Entidades.EntidadesInventario.ETipoSuplidores Procesar = new Entidades.EntidadesInventario.ETipoSuplidores();

            Procesar.IdTipoSuplidor = IdTipoSuplidor;
            Procesar.TipoSuplidor = Descripcion;
            Procesar.Estatus0 = Estatus;
            Procesar.UsuarioAdiciona = UsuarioProcesa;
            Procesar.FechaAdiciona = DateTime.Now;
            Procesar.UsuarioModifica = UsuarioProcesa;
            Procesar.FechaModifica = DateTime.Now;

            var MAN = ObjData.MantenimientoTipoSuplidores(Procesar, Accion);
        }
    }
}
