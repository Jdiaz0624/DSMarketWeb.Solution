using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Entidades.EntidadesServicio
{
    public class EBuscaItemsAgregadoFActura
    {
		public decimal? NumeroRegistro { get; set; }

		public string NumerodeConector { get; set; }

		public string Producto { get; set; }

		public string Garantia { get; set; }

		public System.Nullable<int> IdTipoGarantiaRespaldo { get; set; }

		public string GarantiaProducto { get; set; }

		public System.Nullable<decimal> Precio { get; set; }

		public System.Nullable<decimal> Descuento { get; set; }

		public System.Nullable<int> Cantidad { get; set; }

		public System.Nullable<decimal> Total { get; set; }

		public System.Nullable<decimal> IdTipoProductoRespaldo { get; set; }

		public System.Nullable<decimal> ImpuestoAplicado { get; set; }

		public System.Nullable<decimal> IdProductoRespaldo { get; set; }

		public System.Nullable<decimal> NumeroConectorRespaldo { get; set; }

		public System.Nullable<int> TotalProductos { get; set; }

		public System.Nullable<int> TotalServicios { get; set; }

		public System.Nullable<int> TotalItems { get; set; }

		public System.Nullable<decimal> TotalDescuento { get; set; }

		public System.Nullable<decimal> SubTotal { get; set; }

		public System.Nullable<decimal> TotalImpuesto { get; set; }

		public System.Nullable<decimal> TotalGeneral { get; set; }
	}
}
