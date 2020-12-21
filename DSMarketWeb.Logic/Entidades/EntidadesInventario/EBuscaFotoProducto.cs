using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Entidades.EntidadesInventario
{
    public class EBuscaFotoProducto
    {
		public decimal? IdProducto { get; set; }

		public decimal NumeroConector { get; set; }

		public string TipoProducto { get; set; }

		public string Categoria { get; set; }

		public string Producto { get; set; }

		public System.Data.Linq.Binary FotoProducto { get; set; }
	}
}
