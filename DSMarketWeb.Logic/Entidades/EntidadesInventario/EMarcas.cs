﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Entidades.EntidadesInventario
{
    public class EMarcas
    {
		public decimal? IdMarca { get; set; }

		public System.Nullable<decimal> IdTipoProducto { get; set; }

		public string TipoProducto { get; set; }

		public System.Nullable<decimal> IdCateoria { get; set; }

		public string Categoria { get; set; }

		public string Marca { get; set; }

		public System.Nullable<bool> Estatus0 { get; set; }

		public string Estatus { get; set; }

		public System.Nullable<decimal> UsuarioAdiciona { get; set; }

		public string CreadoPor { get; set; }

		public System.Nullable<System.DateTime> FechaAdiciona { get; set; }

		public string FechaCreado { get; set; }

		public System.Nullable<decimal> UsuarioModifica { get; set; }

		public string ModificadoPor { get; set; }

		public System.Nullable<System.DateTime> FechaModifica { get; set; }

		public string FechaModificado { get; set; }

		public System.Nullable<int> CantidadRegistros { get; set; }
	}
}
