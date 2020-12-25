using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Inventario
{
    public class ProcesarInformacionMarcas
    {
        readonly DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario ObjData = new Logica.LogicaInventario.LogicaInventario();

        private decimal IdMarca = 0;
        private string Descripcion = "";
        private bool Esttaus = false;
        private decimal UsuarioProcesa = 0;
        private decimal IdCategoria = 0;
        private decimal IdTipoProducto = 0;
        private string Accion = "";

        public ProcesarInformacionMarcas(
            decimal IdMarcaCON,
        string DescripcionCON,
        bool EsttausCON,
        decimal UsuarioProcesaCON,
        decimal IdCategoriaCON,
        decimal IdTipoProductoCON,
        string AccionCON)
        {
            IdMarca = IdMarcaCON;
            Descripcion = DescripcionCON;
            Esttaus = EsttausCON;
            UsuarioProcesa = UsuarioProcesaCON;
            IdCategoria = IdCategoriaCON;
            IdTipoProducto = IdTipoProductoCON;
            Accion = AccionCON;
        }
        public void ProcedarDataMarcas() {
            DSMarketWeb.Logic.Entidades.EntidadesInventario.EMarcas Procesar = new Entidades.EntidadesInventario.EMarcas();

            Procesar.IdMarca = IdMarca;
            Procesar.Marca = Descripcion;
            Procesar.Estatus0 = Esttaus;
            Procesar.UsuarioAdiciona = UsuarioProcesa;
            Procesar.FechaAdiciona = DateTime.Now;
            Procesar.UsuarioModifica = UsuarioProcesa;
            Procesar.FechaModifica = DateTime.Now;
            Procesar.IdCateoria = IdCategoria;
            Procesar.IdTipoProducto = IdTipoProducto;

            var MAN = ObjData.MantenimientoMarcas(Procesar, Accion);
        }
    }
}
