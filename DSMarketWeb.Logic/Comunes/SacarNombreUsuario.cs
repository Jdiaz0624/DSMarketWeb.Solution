using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Comunes
{
    public class SacarNombreUsuario
    {
        readonly DSMarketWeb.Logic.Logica.LogicaSeguridad.LogicaSeguridad ObjData = new Logica.LogicaSeguridad.LogicaSeguridad();

        public decimal Idusuario = 0;

        public SacarNombreUsuario(decimal IdUsuarioCON) {
            Idusuario = IdUsuarioCON;
        }

        public string SacarNombre() {
            string NombreUsuario = "";

            var Buscar = ObjData.BuscaUsuarios(Idusuario);
            foreach (var n in Buscar) {
                NombreUsuario = n.Persona;
            }
            return NombreUsuario;
        }
    }
}
