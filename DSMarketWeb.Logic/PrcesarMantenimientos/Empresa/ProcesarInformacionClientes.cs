using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Empresa
{
    public class ProcesarInformacionClientes
    {
        readonly DSMarketWeb.Logic.Logica.LogicaEmpresa.LogicaEmpresa ObjData = new Logica.LogicaEmpresa.LogicaEmpresa();

        private decimal IdCliente = 0;
        private decimal IdComprobante = 0;
        private string Nombre = "";
        private string Telefono = "";
        private decimal IdTipoIdentificacion = 0;
        private string RNC = "";
        private string Direccion = null;
        private string Email = null;
        private string Comentario = null;
        private bool Estatus = false;
        private decimal UsuarioProcesa = 0;
        private decimal MontoCredito = 0;
        private bool EnvioEmail = false;
        private string Accion = "";

        public ProcesarInformacionClientes(
             decimal IdClienteCON,
        decimal IdComprobanteCON,
        string NombreCON,
        string TelefonoCON,
        decimal IdTipoIdentificacionCON,
        string RNCCON,
        string DireccionCON,
        string EmailCON,
        string ComentarioCON,
        bool EstatusCON,
        decimal UsuarioProcesaCON,
        decimal MontoCreditoCON,
        bool EnvioEmailCON,
        string AccionCON)
        {
            IdCliente = IdClienteCON;
            IdComprobante = IdComprobanteCON;
            Nombre = NombreCON;
            Telefono = TelefonoCON;
            IdTipoIdentificacion = IdTipoIdentificacionCON;
            RNC = RNCCON;
            Direccion = DireccionCON;
            Email = EmailCON;
            Comentario = ComentarioCON;
            Estatus = EstatusCON;
            UsuarioProcesa = UsuarioProcesaCON;
            MontoCredito = MontoCreditoCON;
            EnvioEmail = EnvioEmailCON;
            Accion = AccionCON;
        }

        public void ProcesarDatosClientes() {
            DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EClientes Procesar = new Entidades.EntidadesEmpresa.EClientes();

            Procesar.IdCliente = IdCliente;
            Procesar.IdComprobante = IdComprobante;
            Procesar.Nombre = Nombre;
            Procesar.Telefono = Telefono;
            Procesar.IdTipoIdentificacion = IdTipoIdentificacion;
            Procesar.RNC = RNC;
            Procesar.Direccion = Direccion;
            Procesar.Email = Email;
            Procesar.Comentario = Comentario;
            Procesar.Estatus0 = Estatus;
            Procesar.UsuarioAdiciona = UsuarioProcesa;
            Procesar.FechaAdiciona = DateTime.Now;
            Procesar.UsuarioModifica = UsuarioProcesa;
            Procesar.FechaModifica = DateTime.Now;
            Procesar.MontoCredito = MontoCredito;
            Procesar.EnvioEmail0 = EnvioEmail;

            var MAN = ObjData.MantenimientoClientes(Procesar, Accion);
        }
    }
}
