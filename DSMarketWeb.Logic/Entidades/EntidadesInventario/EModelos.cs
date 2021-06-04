using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Entidades.EntidadesInventario
{
    public class EModelos
    {
		public System.Nullable<decimal> IdMarca { get; set; }
		public string Marca { get; set; }

		public decimal? IdModelo { get; set; }

		public string Modelo { get; set; }

		public string Estatus { get; set; }

		public System.Nullable<bool> Estatus0 { get; set; }
	}
}
