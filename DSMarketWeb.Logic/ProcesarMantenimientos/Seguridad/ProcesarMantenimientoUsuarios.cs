using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.ProcesarMantenimientos.Seguridad
{
    public class ProcesarMantenimientoUsuarios
    {
        readonly DSMarketWeb.Logic.Logica.LogicaSeguridad.LogicaSeguridad ObjData = new Logica.LogicaSeguridad.LogicaSeguridad();

        private decimal IdUsuario = 0;
        private decimal IdNivelAcceso = 0;
        private string Usuario = "";
        private string Clave = "";
        private string Persona = "";
        private bool Estatus = false;
        private bool CambiaClave = false;
        private decimal IdUsuarioProceso = 0;
        private string Accion = "";

        public ProcesarMantenimientoUsuarios(
            decimal IdUsuarioCON,
            decimal IdNivelAccesoCON,
            string UsuarioCON,
            string ClaveCON,
            string PersonaCON,
            bool EstatusCON,
            bool CambiaClaveCON,
            decimal IdUsuarioProcesoCON,
            string AccionCON)
        {
            IdUsuario = IdUsuarioCON;
            IdNivelAcceso = IdNivelAccesoCON;
            Usuario = UsuarioCON;
            Clave = ClaveCON;
            Persona = PersonaCON;
            Estatus = EstatusCON;
            CambiaClave = CambiaClaveCON;
            IdUsuarioProceso = IdUsuarioProcesoCON;
            Accion = AccionCON;
        }

        public void ProcesarInformacionUsuarios() {
            DSMarketWeb.Logic.Entidades.EntidadesSeguridad.EUsuarios Procesar = new Entidades.EntidadesSeguridad.EUsuarios();

            Procesar.IdUsuario = IdUsuario;
            Procesar.IdNivelAcceso = IdNivelAcceso;
            Procesar.Usuario = Usuario;
            Procesar.Clave = Clave;
            Procesar.Persona = Persona;
            Procesar.Estatus0 = Estatus;
            Procesar.CambiaClave0 = CambiaClave;
            Procesar.UsuarioAdicona = IdUsuarioProceso;
            Procesar.FechaAdiciona0 = DateTime.Now;
            Procesar.UsuarioModifica = IdUsuarioProceso;
            Procesar.FechaModifica0 = DateTime.Now;

            var MAN = ObjData.MantenimientoUsuarios(Procesar, Accion);
        }
    }
}
