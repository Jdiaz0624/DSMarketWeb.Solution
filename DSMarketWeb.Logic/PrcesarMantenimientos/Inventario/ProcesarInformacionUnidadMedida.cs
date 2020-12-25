using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Inventario
{
    public class ProcesarInformacionUnidadMedida
    {
        readonly DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario ObjData = new Logica.LogicaInventario.LogicaInventario();

        private decimal IdUnidadMedida = 0;
        private string Descripcion = "";
        private bool Estatus = false;
        private decimal UsuarioProcesa = 0;
        private string Accion = "";

        public ProcesarInformacionUnidadMedida(
            decimal IdUnidadMedidaCON,
        string DescripcionCON,
        bool EstatusCON,
        decimal UsuarioProcesaCON,
        string AccionCON)
        {
            IdUnidadMedida = IdUnidadMedidaCON;
            Descripcion = DescripcionCON;
            Estatus = EstatusCON;
            UsuarioProcesa = UsuarioProcesaCON;
            Accion = AccionCON;
        }

        public void ProcesarDataUnidadMedida() {
            DSMarketWeb.Logic.Entidades.EntidadesInventario.EUnidadMedida Procesar = new Entidades.EntidadesInventario.EUnidadMedida();

            Procesar.IdUnidadMedida = IdUnidadMedida;
            Procesar.UnidadMedida = Descripcion;
            Procesar.Estatus0 = Estatus;
            Procesar.UsuarioAdiciona = UsuarioProcesa;
            Procesar.FechaAdiciona = DateTime.Now;
            Procesar.UsuarioModifica = UsuarioProcesa;
            Procesar.FechaModifica = DateTime.Now;

            var MAN = ObjData.MantenimientoUnidadMEdida(Procesar, Accion);
        }
    }      
}          
           