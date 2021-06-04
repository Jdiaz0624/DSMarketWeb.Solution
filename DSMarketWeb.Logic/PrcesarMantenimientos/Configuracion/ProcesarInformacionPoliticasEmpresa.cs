using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Configuracion
{
    public class ProcesarInformacionPoliticasEmpresa
    {
        readonly DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion ObjData = new Logica.LogicaConfiguracion.LogicaConfiguracion();

        private decimal IdPolitica = 0;
        private string Politica1 = "";
        private string Politica2 = "";
        private string Politica3 = "";
        private string Politica4 = "";
        private string Politica5 = "";
        private string Politica6 = "";
        private string Politica7 = "";
        private string Politica8 = "";
        private string Politica9 = "";
        private string Politica10 = "";
        private string Accion = "";

        public ProcesarInformacionPoliticasEmpresa(
            decimal IdPoliticaCON,
            string Politica1CON,
            string Politica2CON,
            string Politica3CON,
            string Politica4CON,
            string Politica5CON,
            string Politica6CON,
            string Politica7CON,
            string Politica8CON,
            string Politica9CON,
            string Politica10CON,
            string AccionCON)
        {
            IdPolitica = IdPoliticaCON;
            Politica1 = Politica1CON;
            Politica2 = Politica2CON;
            Politica3 = Politica3CON;
            Politica4 = Politica4CON;
            Politica5 = Politica5CON;
            Politica6 = Politica6CON;
            Politica7 = Politica7CON;
            Politica8 = Politica8CON;
            Politica9 = Politica9CON;
            Politica10 = Politica10CON;
            Accion = AccionCON;
        }

        public void ModificarInformacion() {

            DSMarketWeb.Logic.Entidades.EntidadesConfiguracion.EPoliticaEmpresa Modificar = new Entidades.EntidadesConfiguracion.EPoliticaEmpresa();

            Modificar.IdPolitica = IdPolitica;
            Modificar.Politica1 = Politica1;
            Modificar.Politica2 = Politica2;
            Modificar.Politica3 = Politica3;
            Modificar.Politica4 = Politica4;
            Modificar.Politica5 = Politica5;
            Modificar.Politica6 = Politica6;
            Modificar.Politica7 = Politica7;
            Modificar.Politica8 = Politica8;
            Modificar.Politica9 = Politica9;
            Modificar.Politica10 = Politica10;

            var MAN = ObjData.ModificarPoliticasEmpresa(Modificar, Accion);
        }
    }
}
