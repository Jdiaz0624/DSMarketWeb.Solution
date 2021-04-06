using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Entidades.EntidadesServicio
{
    public class EProductoEspejo
    {
		public System.Nullable<decimal> IdProducto { get; set; }

		public System.Nullable<decimal> NumeroConector { get; set; }

		public string Descripcion { get; set; }

		public System.Nullable<decimal> Cantidad { get; set; }

		public System.Nullable<bool> ProductoAcumulativo { get; set; }

		public System.Nullable<decimal> IdUsuario { get; set; }
	}
}
