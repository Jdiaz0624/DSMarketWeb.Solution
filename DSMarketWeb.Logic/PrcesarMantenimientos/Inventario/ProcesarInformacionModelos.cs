using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Inventario
{
    public class ProcesarInformacionModelos
    {
        readonly DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario ObjDataModelos = new Logica.LogicaInventario.LogicaInventario();

        private decimal IdMarca = 0;
        private decimal IdModelo = 0;
        private string Descripcion = "";
        private bool? Estatus = false;
        private decimal UsuarioProcesa = 0;
        private decimal IdTipoProducto = 0;
        private decimal IdCategoria = 0;
        private string Accion = "";

        public ProcesarInformacionModelos(
            decimal IdMarcaCON,
        decimal IdModeloCON,
        string DescripcionCON,
        bool? EstatusCON,
        decimal UsuarioProcesaCON,
        decimal IdTipoProductoCON,
        decimal IdCategoriaCON,
        string AccionCON)
        {
            DSMarketWeb.Logic.Entidades.EntidadesInventario.EModelos Procesar = new Entidades.EntidadesInventario.EModelos();

            Procesar.IdMarca = IdMarca;
            Procesar.IdModelo = IdModelo;
            Procesar.Modelo = Descripcion;
            Procesar.Estatus0 = Estatus;
            Procesar.UsuarioAdiciona = UsuarioProcesa;
            Procesar.FechaAdiciona = DateTime.Now;
            Procesar.UsuarioModifica = UsuarioProcesa;
            Procesar.FechaModifica = DateTime.Now;
            Procesar.IdTipoProducto = IdTipoProducto;
            Procesar.IdCategoria = IdCategoria;

            var MAN = ObjDataModelos.MantenimientoModelos(Procesar, Accion);
        }
    }
}
