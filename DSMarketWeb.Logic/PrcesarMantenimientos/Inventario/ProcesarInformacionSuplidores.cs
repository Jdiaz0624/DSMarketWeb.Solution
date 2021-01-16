using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Inventario
{
    public class ProcesarInformacionSuplidores
    {
        readonly DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario ObjData = new Logica.LogicaInventario.LogicaInventario();

        private decimal IdTipoSuplidor = 0;
        private decimal IdSuplidor = 0;
        private string NombreSuplidor = "";
        private string Telefono = "";
        private string Email = "";
        private string Direccion = "";
        private bool Estatus = false;
        private decimal UsuarioProcesa = 0;
        private string RNC = "";
        private string ActividadEconomica = "";
        private string Accion = "";

        public ProcesarInformacionSuplidores(
            decimal IdTipoSuplidorCON,
        decimal IdSuplidorCON,
        string NombreSuplidorCON,
        string TelefonoCON,
        string EmailCON,
        string DireccionCON,
        bool EstatusCON,
        decimal UsuarioProcesaCON,
        string RNCCON,
        string ActividadEconomicaCON,
        string AccionCON)
        {
            IdTipoSuplidor = IdTipoSuplidorCON;
            IdSuplidor = IdSuplidorCON;
            NombreSuplidor = NombreSuplidorCON;
            Telefono = TelefonoCON;
            Email = EmailCON;
            Direccion = DireccionCON;
            Estatus = EstatusCON;
            UsuarioProcesa = UsuarioProcesaCON;
            RNC = RNCCON;
            ActividadEconomica = ActividadEconomicaCON;
            Accion = AccionCON;
        }
        public void ProcesarDatosSuplidor() {
            DSMarketWeb.Logic.Entidades.EntidadesInventario.ESuplidores Procesar = new Entidades.EntidadesInventario.ESuplidores();

            Procesar.IdTipoSuplidor = IdTipoSuplidor;
            Procesar.IdSuplidor = IdSuplidor;
            Procesar.Suplidor = NombreSuplidor;
            Procesar.Telefono = Telefono;
            Procesar.Email = Email;
            Procesar.Direccion = Direccion;
            Procesar.Estatus0 = Estatus;
            Procesar.UsuarioAdiciona = UsuarioProcesa;
            Procesar.FechaAdiciona = DateTime.Now;
            Procesar.UsuarioModifica = UsuarioProcesa;
            Procesar.FechaModifica = DateTime.Now;
            Procesar.RNC = RNC;
            Procesar.ActividadEconomica = ActividadEconomica;

            var MAN = ObjData.MantenimientoSupoSuplidores(Procesar, Accion);
        }
    }
}
