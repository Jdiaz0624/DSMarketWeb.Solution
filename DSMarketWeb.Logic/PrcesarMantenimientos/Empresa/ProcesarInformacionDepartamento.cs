using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Empresa
{
    public class ProcesarInformacionDepartamento
    {
        readonly DSMarketWeb.Logic.Logica.LogicaEmpresa.LogicaEmpresa ObjData = new Logica.LogicaEmpresa.LogicaEmpresa();

        private decimal IdDepartamento = 0;
        private string Descripcion = "";
        private bool Estatus = false;
        private decimal UsuarioProcesa = 0;
        private string Accion = "";

        public ProcesarInformacionDepartamento(
            decimal IdDepartamentoCON,
        string DescripcionCON,
        bool EstatusCON,
        decimal UsuarioProcesaCON,
        string AccionCON)
        {
            IdDepartamento = IdDepartamentoCON;
            Descripcion = DescripcionCON;
            Estatus = EstatusCON;
            UsuarioProcesa = UsuarioProcesaCON;
            Accion = AccionCON;
        }
        public void ProcesarDataDepartamento() {
            DSMarketWeb.Logic.Entidades.EntidadesEmpresa.EDepartamentos Procesar = new Entidades.EntidadesEmpresa.EDepartamentos();

            Procesar.IdDepartamento = IdDepartamento;
            Procesar.Departamento = Descripcion;
            Procesar.Estatus0 = Estatus;
            Procesar.UsuarioAdiciona = UsuarioProcesa;
            Procesar.FechaAdiciona = DateTime.Now;
            Procesar.UsuarioModifica = UsuarioProcesa;
            Procesar.FechaModifica = DateTime.Now;

            var MAN = ObjData.MantenimientoDepartamentos(Procesar, Accion);
        }
    }
}
