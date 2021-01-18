using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Empresa
{
    public class ProcesarInformacionRetenciones
    {
        readonly DSMarketWeb.Logic.Logica.LogicaEmpresa.LogicaEmpresa ObjData = new Logica.LogicaEmpresa.LogicaEmpresa();

        private decimal IdRetencion = 0;
        private string Descripcion = "";
        private bool Estatus = false;
        private decimal UsuarioProcesa = 0;
        private string Accion = "";

        public ProcesarInformacionRetenciones(
            decimal IdRetencionCON,
            string DescripcionCON,
            bool EstatusCON,
            decimal UsuarioProcesaCON,
            string AccionCON)
        {
            IdRetencion = IdRetencionCON;
            Descripcion = DescripcionCON;
            Estatus = EstatusCON;
            UsuarioProcesa = UsuarioProcesaCON;
            Accion = AccionCON;
        }

        /// <summary>
        /// Este metodo es para procesar la informacion de las retenciones
        /// </summary>
        public void ProcesarInformacion() {
            DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ERetenciones Procesar = new Entidades.EntidadesEmpresa.ERetenciones();

            Procesar.IdRetencion = IdRetencion;
            Procesar.Retencion = Descripcion;
            Procesar.Estatus0 = Estatus;
            Procesar.UsuarioAdiciona = UsuarioProcesa;
            Procesar.FechaAdiciona = DateTime.Now;
            Procesar.UsuarioModifica = UsuarioProcesa;
            Procesar.FechaModifica = DateTime.Now;

            var MAN = ObjData.MantenimeintoRetenciones(Procesar, Accion);
        }
    }
}
