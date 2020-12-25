using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Inventario
{
    public class ProcesarInformacionCateorias
    {
        readonly DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario ObjData = new Logica.LogicaInventario.LogicaInventario();

        private decimal IdCategoria = 0;
        private decimal IdTipoProducto = 0;
        private string Categoria = "";
        private bool Estatus = false;
        private decimal UsuarioProcesa = 0;
        private string Accion = "";

        public ProcesarInformacionCateorias(
            decimal IdCategoriaCON,
        decimal IdTipoProductoCON,
        string CategoriaCON,
        bool EstatusCON,
        decimal UsuarioProcesaCON,
        string AccionCON)
        {
            IdCategoria = IdCategoriaCON;
            IdTipoProducto = IdTipoProductoCON;
            Categoria = CategoriaCON;
            Estatus = EstatusCON;
            UsuarioProcesa = UsuarioProcesaCON;
            Accion = AccionCON;
        }

        public void ProcesarDatosCategoria() {
            DSMarketWeb.Logic.Entidades.EntidadesInventario.ECategorias Procesar = new Entidades.EntidadesInventario.ECategorias();

            Procesar.IdCategoria = IdCategoria;
            Procesar.IdTipoProducto = IdTipoProducto;
            Procesar.Categoria = Categoria;
            Procesar.Estatus0 = Estatus;
            Procesar.UsuarioAdiciona = UsuarioProcesa;
            Procesar.Fechaadiciona = DateTime.Now;
            Procesar.UsuarioModifica = UsuarioProcesa;
            Procesar.FechaModifica = DateTime.Now;

            var MAN = ObjData.MantenimientoCategorias(Procesar, Accion);
        }
    }
}
