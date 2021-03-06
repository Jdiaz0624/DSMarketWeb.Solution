﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Entidades.EntidadesEmpresa
{
    public class ECompraSuplidores
    {
		public decimal? IdCompraSuplidor {get;set;}

		public System.Nullable<decimal> IdTipoSuplidor {get;set;}

		public string TipoSuplidor {get;set;}

		public System.Nullable<decimal> IdSuplidor {get;set;}

		public string Suplidor {get;set;}

		public string RNCCedula {get;set;}

		public System.Nullable<decimal> IdTipoIdentificacion {get;set;}

		public string TipoIdentificacion {get;set;}

		public System.Nullable<decimal> IdTipoBienesServicios {get;set;}

		public string TipoBienesServicios {get;set;}

		public string CodigoTipoBienesServicio {get;set;}

		public string NCF {get;set;}

		public string NCFModificado {get;set;}

		public System.Nullable<System.DateTime> FechaComprobante0 {get;set;}

		public string FechaComprobante {get;set;}

		public System.Nullable<System.DateTime> FechaPago0 {get;set;}

		public string FechaPago {get;set;}

		public System.Nullable<decimal> MontoFacturadoServicios {get;set;}

		public System.Nullable<decimal> MontoFacturadoBienes {get;set;}

		public System.Nullable<decimal> TotalMontoFacturado {get;set;}

		public System.Nullable<decimal> ITBISFacturado {get;set;}

		public System.Nullable<decimal> ITBISRetenido {get;set;}

		public System.Nullable<decimal> ITBISSujetoProporcionalidad {get;set;}

		public System.Nullable<decimal> ITBISLlevadoCosto {get;set;}

		public System.Nullable<decimal> ITBISPorAdelantar {get;set;}

		public System.Nullable<decimal> ITBISPercibidoCompras {get;set;}

		public System.Nullable<decimal> IdTipoRetencionISR {get;set;}

		public string TipoRetencionISR {get;set;}

		public string CodigoTipoRetencionISR {get;set;}

		public System.Nullable<decimal> MontoRetencionRenta {get;set;}

		public System.Nullable<decimal> ISRPercibidoCompras {get;set;}

		public System.Nullable<decimal> ImpuestoSelectivoConsumo {get;set;}

		public System.Nullable<decimal> OtrosImpuestosTasa {get;set;}

		public System.Nullable<decimal> MontoPropinaLegal {get;set;}

		public System.Nullable<decimal> IdFormaPago {get;set;}

		public string FormaPago {get;set;}

		public string CodigoTipoPago {get;set;}

		public System.Nullable<decimal> UsuarioAdiciona {get;set;}

		public string CreadoPor {get;set;}

		public System.Nullable<System.DateTime> FechaCreado0 {get;set;}

		public string FechaCreado {get;set;}

		public string ActividadEconomica {get;set;}

		public string GeneradoPor {get;set;}

		public string NombreEmpresa {get;set;}

		public string RNC {get;set;}

		public string Direccion {get;set;}

		public string Telefonos {get;set;}

		public string Email {get;set;}

		public string Email2 {get;set;}

		public string Facebook {get;set;}

		public string Instagran {get;set;}

		public System.Data.Linq.Binary LogoEmpresa {get;set;}

		public string ValidadoDesde {get;set;}

		public string ValidadoHasta {get;set;}

		public System.Nullable<int> CantidadRegistros {get;set;}
	}
}
