using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Comunes
{
    public class ValidarImagenSistema
    {
        readonly DSMarketWeb.Logic.Logica.LogicaConfiguracion.LogicaConfiguracion Objdata = new Logica.LogicaConfiguracion.LogicaConfiguracion();
        readonly DSMarketWeb.Logic.Logica.LogicaEmpresa.LogicaEmpresa ObjDataEmpresa = new Logica.LogicaEmpresa.LogicaEmpresa();

        private decimal IdLogoSistema = 0;

        //FOTO DE EMPLEADO
        private decimal IdEmpleado = 0;
        private decimal NumeroRegistro = 0;

        /// <summary>
        /// Este constructor es para validar la imagen del sistema
        /// </summary>
        /// <param name="IdLogoSistemaCON"></param>
        public ValidarImagenSistema(
            decimal IdLogoSistemaCON)
        {
            IdLogoSistema = IdLogoSistemaCON;
        }

        /// <summary>
        /// Este constructor es para validar la imagen del empleado
        /// </summary>
        /// <param name="IdEmpleadoCON"></param>
        /// <param name="NumeroRegistroCON"></param>
        public ValidarImagenSistema(
            decimal IdEmpleadoCON,
            decimal NumeroRegistroCON)
        {
            IdEmpleado = IdEmpleadoCON;
            NumeroRegistro = NumeroRegistroCON;
        }

        public bool ValidarImagenSistemaPorDefecto() {

            bool ValidarImagen = false;

            var Vaalidar = Objdata.BuscaImagenesSistema(IdLogoSistema);
            if (Vaalidar.Count() < 1) {
                ValidarImagen = false;
            }
            else {
                ValidarImagen = true;
            }
            return ValidarImagen;
        }

        public bool ValidarFotoEmpleados() {
            bool Resultado = false;

            var Validar = ObjDataEmpresa.ValidarCodigoEmpleado(IdEmpleado, NumeroRegistro);
            if (Validar.Count() < 1)
            {
                Resultado = false;
            }
            else {
                Resultado = true;
            }
            return Resultado;
        }
    }
}
