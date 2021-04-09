using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Entidades.EntidadesServicio
{
    public class EFacturacionCalculos
    {
		public System.Nullable<decimal> NumeroRegistro { get; set; }

		public string NumeroConector { get; set; }

		public System.Nullable<decimal> IdTipoIngreso { get; set; }

		public System.Nullable<decimal> IdTipoPago { get; set; }

		public System.Nullable<decimal> IdMoneda { get; set; }

		public System.Nullable<decimal> ImpuestoTipoPago { get; set; }

		public System.Nullable<decimal> ImpuestoComprobante { get; set; }

		public System.Nullable<decimal> MontoPagado { get; set; }

		public System.Nullable<decimal> Cambio { get; set; }
		public System.Nullable<decimal> TasaMoneda { get; set; }
	}
}
