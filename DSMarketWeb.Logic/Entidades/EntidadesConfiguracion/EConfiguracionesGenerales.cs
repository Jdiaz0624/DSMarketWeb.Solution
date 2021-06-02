using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Entidades.EntidadesConfiguracion
{
    public class EConfiguracionesGenerales
    {
		public decimal? IdConfiguracion { get; set; }

		public decimal? IdModulo { get; set; }
		public string Modulo { get; set; }

		public string Descripcion { get; set; }

		public System.Nullable<bool> Estatus0 { get; set; }

		public string Estatus { get; set; }
	}
}
