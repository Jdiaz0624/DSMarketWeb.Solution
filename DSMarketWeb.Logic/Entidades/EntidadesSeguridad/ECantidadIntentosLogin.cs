using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Entidades.EntidadesSeguridad
{
    public class ECantidadIntentosLogin
    {
		public int? IdContadorBloqueo { get; set; }

		public System.Nullable<bool> Estatus0 { get; set; }

		public string Estatus { get; set; }

		public System.Nullable<int> CantidadIntentos { get; set; }
	}
}
