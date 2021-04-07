using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Servicios
{
    public class ProcesarInformacionMonedas
    {
        readonly DSMarketWeb.Logic.Logica.LogicaServicio.LogicaServicio ObjData = new Logica.LogicaServicio.LogicaServicio();

        private decimal IdMoneda = 0;
        private string Descripcion = "";
        private string Sigla = "";
        private bool Estatus = false;
        private decimal Tasa = 0;
        private decimal IdUsuario = 0;
        private string Accion = "";

        public ProcesarInformacionMonedas(
            decimal IdMonedaCON,
            string DescripcionCON,
            string SiglaCON,
            bool EstatusCON,
            decimal TasaCON,
            decimal IdUsuarioCON,
            string AccionCON)
        {
            IdMoneda = IdMonedaCON;
            Descripcion = DescripcionCON;
            Sigla = SiglaCON;
            Estatus = EstatusCON;
            Tasa = TasaCON;
            IdUsuario = IdUsuarioCON;
            Accion = AccionCON;
        }

        public void ProcesarInformacion() {
            DSMarketWeb.Logic.Entidades.EntidadesServicio.EMonedas Procesar = new Entidades.EntidadesServicio.EMonedas();

            Procesar.IdMoneda = IdMoneda;
            Procesar.Moneda = Descripcion;
            Procesar.Sigla = Sigla;
            Procesar.Estatus0 = Estatus;
            Procesar.Tasa = Tasa;
            Procesar.UsuarioAdiciona = IdUsuario;
            Procesar.FechaAdiciona = DateTime.Now;
            Procesar.UsuarioModifica = IdUsuario;
            Procesar.FechaModifica = DateTime.Now;

            var MAN = ObjData.ManipularInformacionMonedas(Procesar, Accion);  
        }
    }
}
