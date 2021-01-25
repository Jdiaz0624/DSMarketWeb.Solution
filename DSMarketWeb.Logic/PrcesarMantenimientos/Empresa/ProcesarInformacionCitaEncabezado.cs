using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Empresa
{
    public class ProcesarInformacionCitaEncabezado
    {
        readonly DSMarketWeb.Logic.Logica.LogicaEmpresa.LogicaEmpresa Objdata = new Logica.LogicaEmpresa.LogicaEmpresa();

        private decimal IdCita = 0;
        private decimal IdEmpleado = 0;
        private DateTime FechaCita = DateTime.Now;
        private string Hora = "";
        private string NombreCliente = "";
        private string Telefono = "";
        private string Direccion = "";
        private string NumeroIdentificacion = "";
        private decimal NumeroConectorCita = 0;
        private bool Estatus = false;
        private string Comentario = "";
        private string Accion = "";

        public ProcesarInformacionCitaEncabezado(
            decimal IdCitaCON,
        decimal IdEmpleadoCON,
        DateTime FechaCitaCON,
        string HoraCON,
        string NombreClienteCON,
        string TelefonoCON,
        string DireccionCON,
        string NumeroIdentificacionCON,
        decimal NumeroConectorCON,
        bool EstatusCON,
        string ComentarioCON,
        string AccionCON)
        {
            IdCita = IdCitaCON;
            IdEmpleado = IdEmpleadoCON;
            FechaCita = FechaCitaCON;
            Hora = HoraCON;
            NombreCliente = NombreClienteCON;
            Telefono = TelefonoCON;
            Direccion = DireccionCON;
            NumeroIdentificacion = NumeroIdentificacionCON;
            NumeroConectorCita = NumeroConectorCON;
            Estatus = EstatusCON;
            Comentario = ComentarioCON;
            Accion = AccionCON;
        }

        /// <summary>
        /// Este metodo es para procesar la informacion del encabezado de las citas
        /// </summary>
        public void ProcesarInformacion() {
            DSMarketWeb.Logic.Entidades.EntidadesEmpresa.ECitasEncabezado Procesar = new Entidades.EntidadesEmpresa.ECitasEncabezado();

            Procesar.IdCitas = IdCita;
            Procesar.IdEmpleado = IdEmpleado;
            Procesar.FechaCita0 = FechaCita;
            Procesar.Hora = Hora;
            Procesar.NombreCliente = NombreCliente;
            Procesar.Telefono = Telefono;
            Procesar.Direccion = Direccion;
            Procesar.NumeroIdentificacion = NumeroIdentificacion;
            Procesar.NumeroConectorCita = NumeroConectorCita;
            Procesar.Estatus0 = Estatus;
            Procesar.Comentario = Comentario;

            var MAN = Objdata.MantenimientoCitaEncabeZado(Procesar, Accion);
        }
    }
}
