using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Entidades.EntidadesSeguridad
{
    public class EBuscaClaveSeguridad
    {
		public decimal? IdClaveSeguridad { get; set; }

		public decimal IdUsuario { get; set; }

		public string Clave { get; set; }

		public System.Nullable<bool> Estatus0 { get; set; }

		public string Estatus { get; set; }
	}
}
