using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Comunes 
{
    public  class ValidarClaveSeguridad 
    {
        private readonly DSMarketWeb.Logic.Logica.LogicaSeguridad.LogicaSeguridad ObjData = new Logica.LogicaSeguridad.LogicaSeguridad();

        private  string ClaveSeguridad = "";

        public ValidarClaveSeguridad(
           string ClaveSeguridadCON)
        {
            ClaveSeguridad = DSMarketWeb.Logic.Comunes.SeguridadEncriptacion.Encriptar(ClaveSeguridadCON);
        }

        public bool ResultadoClave() {
            bool Respuesta = false;

            var ValidarClave = ObjData.BuscaClaveSeguridad(
                new Nullable<decimal>(),
                null,
                ClaveSeguridad);
            if (ValidarClave.Count() < 1)
            {
                Respuesta = false;
            }
            else {
                Respuesta = true;
            }
            return Respuesta;
        }
    }
}
