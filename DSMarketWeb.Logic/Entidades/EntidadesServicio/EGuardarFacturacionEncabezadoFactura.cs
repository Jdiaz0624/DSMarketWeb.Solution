using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Entidades.EntidadesServicio
{
    public class EGuardarFacturacionEncabezadoFactura
    {
		public System.Nullable<decimal> NumeroFactura { get; set; }

		public string NumeroConector { get; set; }

		public System.Nullable<bool> LlevaComprobanre { get; set; }

		public System.Nullable<decimal> IdComprobanre { get; set; }

		public string NumeroComprobante { get; set; }

		public string Cliente { get; set; }

		public System.Nullable<decimal> IdTipoIdentificacion { get; set; }

		public string NumeroIdentificacion { get; set; }

		public string Telefono { get; set; }

		public string CorreoElectronico { get; set; }

		public string Direccion { get; set; }

		public string Comentario { get; set; }

		public System.Nullable<System.DateTime> FechaFacturacion { get; set; }

		public System.Nullable<decimal> IdTipoVenta { get; set; }

		public System.Nullable<decimal> IdTipoPlazoCredito { get; set; }

		public System.Nullable<decimal> NumeroPlazoTiempo { get; set; }

		public System.Nullable<decimal> IdTiempoPlazoCredito { get; set; }
		public string Estatus { get; set; }
	}
}
