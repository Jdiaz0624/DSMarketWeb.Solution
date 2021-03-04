using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Comunes
{
    public class ESacarNombreUsuarioConectado
    {
        readonly DSMarketWeb.Logic.Logica.LogicaSeguridad.LogicaSeguridad ObjData = new Logica.LogicaSeguridad.LogicaSeguridad();

        private decimal IdUsuario = 0;

        public ESacarNombreUsuarioConectado(decimal IdUsuarioCON) {
            IdUsuario = IdUsuarioCON;
        }

        public string SacarNombre() {
            string NombreUsuarioConectado = "";

            var Buscar = ObjData.BuscaUsuarios(IdUsuario);
            foreach (var n in Buscar) {
                NombreUsuarioConectado = n.Persona;
            }
            return NombreUsuarioConectado;
        }
    }
}
