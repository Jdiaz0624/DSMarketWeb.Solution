using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Entidades.EntidadesConfiguracion
{
    public class EBuscaImagenesSistema
    {
        public System.Nullable<int> IdLogoEmpresa { get; set; }

        public string Nombre { get; set; }

        public System.Data.Linq.Binary LogoEmpresa { get; set; }
    }
}
