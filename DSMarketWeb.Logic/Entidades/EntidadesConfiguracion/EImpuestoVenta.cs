using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Entidades.EntidadesConfiguracion
{
    public class EImpuestoVenta
    {
		public int? IdImpuesto { get; set; }

		public string Descripcion { get; set; }

		public System.Nullable<int> PorcientoImpuesto { get; set; }

		public System.Nullable<bool> Operacion0 { get; set; }

		public string Operacion { get; set; }
	}
}
