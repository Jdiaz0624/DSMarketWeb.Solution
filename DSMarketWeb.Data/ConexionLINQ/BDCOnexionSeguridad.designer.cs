﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DSMarketWeb.Data.ConexionLINQ
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="DSMarket")]
	public partial class BDCOnexionSeguridadDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public BDCOnexionSeguridadDataContext() : 
				base(global::DSMarketWeb.Data.Properties.Settings.Default.DSMarketConnectionString1, mappingSource)
		{
			OnCreated();
		}
		
		public BDCOnexionSeguridadDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BDCOnexionSeguridadDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BDCOnexionSeguridadDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BDCOnexionSeguridadDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="Seguridad.SP_BUSCA_USUARIOS_WEB")]
		public ISingleResult<SP_BUSCA_USUARIOS_WEBResult> SP_BUSCA_USUARIOS_WEB([global::System.Data.Linq.Mapping.ParameterAttribute(Name="IdUsuario", DbType="Decimal(20,0)")] System.Nullable<decimal> idUsuario, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="UsuarioLogin", DbType="VarChar(20)")] string usuarioLogin, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Clave", DbType="VarChar(8000)")] string clave, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Usuario", DbType="VarChar(20)")] string usuario, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Persona", DbType="VarChar(100)")] string persona)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), idUsuario, usuarioLogin, clave, usuario, persona);
			return ((ISingleResult<SP_BUSCA_USUARIOS_WEBResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="Seguridad.SP_MANTENIMIENTO_USUARIO")]
		public ISingleResult<SP_MANTENIMIENTO_USUARIOResult> SP_MANTENIMIENTO_USUARIO([global::System.Data.Linq.Mapping.ParameterAttribute(Name="IdUsuario", DbType="Decimal(20,0)")] System.Nullable<decimal> idUsuario, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="IdNivelAcceso", DbType="Decimal(20,0)")] System.Nullable<decimal> idNivelAcceso, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Usuario", DbType="VarChar(20)")] string usuario, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Clave", DbType="VarChar(8000)")] string clave, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Persona", DbType="VarChar(100)")] string persona, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Estatus", DbType="Bit")] System.Nullable<bool> estatus, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="CambiaClave", DbType="Bit")] System.Nullable<bool> cambiaClave, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="UsuarioProcesa", DbType="Decimal(20,0)")] System.Nullable<decimal> usuarioProcesa, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Accion", DbType="VarChar(150)")] string accion)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), idUsuario, idNivelAcceso, usuario, clave, persona, estatus, cambiaClave, usuarioProcesa, accion);
			return ((ISingleResult<SP_MANTENIMIENTO_USUARIOResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="Seguridad.SP_BUSCA_CANTIDAD_INTENTOS_LOGIN")]
		public ISingleResult<SP_BUSCA_CANTIDAD_INTENTOS_LOGINResult> SP_BUSCA_CANTIDAD_INTENTOS_LOGIN([global::System.Data.Linq.Mapping.ParameterAttribute(Name="IdContadorBloqueo", DbType="Int")] System.Nullable<int> idContadorBloqueo)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), idContadorBloqueo);
			return ((ISingleResult<SP_BUSCA_CANTIDAD_INTENTOS_LOGINResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="Seguridad.SP_ACTUALIZAR_CANTIDAD_INTENTOS_LOGIN")]
		public ISingleResult<SP_ACTUALIZAR_CANTIDAD_INTENTOS_LOGINResult> SP_ACTUALIZAR_CANTIDAD_INTENTOS_LOGIN([global::System.Data.Linq.Mapping.ParameterAttribute(Name="IdContadorBloqueo", DbType="Int")] System.Nullable<int> idContadorBloqueo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Estatus", DbType="Bit")] System.Nullable<bool> estatus, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="CantidadIntentos", DbType="Int")] System.Nullable<int> cantidadIntentos, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Accion", DbType="VarChar(150)")] string accion)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), idContadorBloqueo, estatus, cantidadIntentos, accion);
			return ((ISingleResult<SP_ACTUALIZAR_CANTIDAD_INTENTOS_LOGINResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="Seguridad.SP_BUSCA_CLAVE_SEGURIDAD_WEB")]
		public ISingleResult<SP_BUSCA_CLAVE_SEGURIDAD_WEBResult> SP_BUSCA_CLAVE_SEGURIDAD_WEB([global::System.Data.Linq.Mapping.ParameterAttribute(Name="IdClaveSeguridad", DbType="Decimal(20,0)")] System.Nullable<decimal> idClaveSeguridad, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="IdUsuario", DbType="Decimal(20,0)")] System.Nullable<decimal> idUsuario, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Clave", DbType="VarChar(8000)")] string clave)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), idClaveSeguridad, idUsuario, clave);
			return ((ISingleResult<SP_BUSCA_CLAVE_SEGURIDAD_WEBResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="Seguridad.SP_SACAR_CREDENCIALES_BD")]
		public ISingleResult<SP_SACAR_CREDENCIALES_BDResult> SP_SACAR_CREDENCIALES_BD([global::System.Data.Linq.Mapping.ParameterAttribute(Name="IdCredencial", DbType="Int")] System.Nullable<int> idCredencial)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), idCredencial);
			return ((ISingleResult<SP_SACAR_CREDENCIALES_BDResult>)(result.ReturnValue));
		}
	}
	
	public partial class SP_BUSCA_USUARIOS_WEBResult
	{
		
		private System.Nullable<decimal> _IdUsuario;
		
		private System.Nullable<decimal> _IdNivelAcceso;
		
		private string _Nivel;
		
		private string _Usuario;
		
		private string _Clave;
		
		private string _Persona;
		
		private System.Nullable<bool> _Estatus0;
		
		private string _Estatus;
		
		private System.Nullable<bool> _CambiaClave0;
		
		private string _CambiaClave;
		
		private System.Nullable<decimal> _UsuarioAdicona;
		
		private string _CreadoPor;
		
		private System.Nullable<System.DateTime> _FechaAdiciona0;
		
		private string _FechaCreado;
		
		private System.Nullable<decimal> _UsuarioModifica;
		
		private string _ModificadoPor;
		
		private System.Nullable<System.DateTime> _FechaModifica0;
		
		private string _FechaModificado;
		
		private System.Nullable<int> _CantidadUsuarios;
		
		private System.Nullable<int> _CantidadActivos;
		
		private System.Nullable<int> _CantidadInactivos;
		
		public SP_BUSCA_USUARIOS_WEBResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdUsuario", DbType="Decimal(20,0)")]
		public System.Nullable<decimal> IdUsuario
		{
			get
			{
				return this._IdUsuario;
			}
			set
			{
				if ((this._IdUsuario != value))
				{
					this._IdUsuario = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdNivelAcceso", DbType="Decimal(20,0)")]
		public System.Nullable<decimal> IdNivelAcceso
		{
			get
			{
				return this._IdNivelAcceso;
			}
			set
			{
				if ((this._IdNivelAcceso != value))
				{
					this._IdNivelAcceso = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Nivel", DbType="VarChar(100)")]
		public string Nivel
		{
			get
			{
				return this._Nivel;
			}
			set
			{
				if ((this._Nivel != value))
				{
					this._Nivel = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Usuario", DbType="VarChar(20)")]
		public string Usuario
		{
			get
			{
				return this._Usuario;
			}
			set
			{
				if ((this._Usuario != value))
				{
					this._Usuario = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Clave", DbType="VarChar(8000)")]
		public string Clave
		{
			get
			{
				return this._Clave;
			}
			set
			{
				if ((this._Clave != value))
				{
					this._Clave = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Persona", DbType="VarChar(100)")]
		public string Persona
		{
			get
			{
				return this._Persona;
			}
			set
			{
				if ((this._Persona != value))
				{
					this._Persona = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Estatus0", DbType="Bit")]
		public System.Nullable<bool> Estatus0
		{
			get
			{
				return this._Estatus0;
			}
			set
			{
				if ((this._Estatus0 != value))
				{
					this._Estatus0 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Estatus", DbType="VarChar(8) NOT NULL", CanBeNull=false)]
		public string Estatus
		{
			get
			{
				return this._Estatus;
			}
			set
			{
				if ((this._Estatus != value))
				{
					this._Estatus = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CambiaClave0", DbType="Bit")]
		public System.Nullable<bool> CambiaClave0
		{
			get
			{
				return this._CambiaClave0;
			}
			set
			{
				if ((this._CambiaClave0 != value))
				{
					this._CambiaClave0 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CambiaClave", DbType="VarChar(2) NOT NULL", CanBeNull=false)]
		public string CambiaClave
		{
			get
			{
				return this._CambiaClave;
			}
			set
			{
				if ((this._CambiaClave != value))
				{
					this._CambiaClave = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UsuarioAdicona", DbType="Decimal(20,0)")]
		public System.Nullable<decimal> UsuarioAdicona
		{
			get
			{
				return this._UsuarioAdicona;
			}
			set
			{
				if ((this._UsuarioAdicona != value))
				{
					this._UsuarioAdicona = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreadoPor", DbType="VarChar(100)")]
		public string CreadoPor
		{
			get
			{
				return this._CreadoPor;
			}
			set
			{
				if ((this._CreadoPor != value))
				{
					this._CreadoPor = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FechaAdiciona0", DbType="DateTime")]
		public System.Nullable<System.DateTime> FechaAdiciona0
		{
			get
			{
				return this._FechaAdiciona0;
			}
			set
			{
				if ((this._FechaAdiciona0 != value))
				{
					this._FechaAdiciona0 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FechaCreado", DbType="NVarChar(4000)")]
		public string FechaCreado
		{
			get
			{
				return this._FechaCreado;
			}
			set
			{
				if ((this._FechaCreado != value))
				{
					this._FechaCreado = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UsuarioModifica", DbType="Decimal(20,0)")]
		public System.Nullable<decimal> UsuarioModifica
		{
			get
			{
				return this._UsuarioModifica;
			}
			set
			{
				if ((this._UsuarioModifica != value))
				{
					this._UsuarioModifica = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ModificadoPor", DbType="VarChar(100)")]
		public string ModificadoPor
		{
			get
			{
				return this._ModificadoPor;
			}
			set
			{
				if ((this._ModificadoPor != value))
				{
					this._ModificadoPor = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FechaModifica0", DbType="DateTime")]
		public System.Nullable<System.DateTime> FechaModifica0
		{
			get
			{
				return this._FechaModifica0;
			}
			set
			{
				if ((this._FechaModifica0 != value))
				{
					this._FechaModifica0 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FechaModificado", DbType="NVarChar(4000)")]
		public string FechaModificado
		{
			get
			{
				return this._FechaModificado;
			}
			set
			{
				if ((this._FechaModificado != value))
				{
					this._FechaModificado = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CantidadUsuarios", DbType="Int")]
		public System.Nullable<int> CantidadUsuarios
		{
			get
			{
				return this._CantidadUsuarios;
			}
			set
			{
				if ((this._CantidadUsuarios != value))
				{
					this._CantidadUsuarios = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CantidadActivos", DbType="Int")]
		public System.Nullable<int> CantidadActivos
		{
			get
			{
				return this._CantidadActivos;
			}
			set
			{
				if ((this._CantidadActivos != value))
				{
					this._CantidadActivos = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CantidadInactivos", DbType="Int")]
		public System.Nullable<int> CantidadInactivos
		{
			get
			{
				return this._CantidadInactivos;
			}
			set
			{
				if ((this._CantidadInactivos != value))
				{
					this._CantidadInactivos = value;
				}
			}
		}
	}
	
	public partial class SP_MANTENIMIENTO_USUARIOResult
	{
		
		private System.Nullable<decimal> _IdUsuario;
		
		private System.Nullable<decimal> _IdNivelAcceso;
		
		private string _Usuario;
		
		private string _Clave;
		
		private string _Persona;
		
		private System.Nullable<bool> _Estatus;
		
		private System.Nullable<bool> _CambiaClave;
		
		private System.Nullable<decimal> _UsuarioAdicona;
		
		private System.Nullable<System.DateTime> _FechaAdiciona;
		
		private System.Nullable<decimal> _UsuarioModifica;
		
		private System.Nullable<System.DateTime> _FechaModifica;
		
		public SP_MANTENIMIENTO_USUARIOResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdUsuario", DbType="Decimal(20,0)")]
		public System.Nullable<decimal> IdUsuario
		{
			get
			{
				return this._IdUsuario;
			}
			set
			{
				if ((this._IdUsuario != value))
				{
					this._IdUsuario = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdNivelAcceso", DbType="Decimal(20,0)")]
		public System.Nullable<decimal> IdNivelAcceso
		{
			get
			{
				return this._IdNivelAcceso;
			}
			set
			{
				if ((this._IdNivelAcceso != value))
				{
					this._IdNivelAcceso = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Usuario", DbType="VarChar(20)")]
		public string Usuario
		{
			get
			{
				return this._Usuario;
			}
			set
			{
				if ((this._Usuario != value))
				{
					this._Usuario = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Clave", DbType="VarChar(8000)")]
		public string Clave
		{
			get
			{
				return this._Clave;
			}
			set
			{
				if ((this._Clave != value))
				{
					this._Clave = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Persona", DbType="VarChar(100)")]
		public string Persona
		{
			get
			{
				return this._Persona;
			}
			set
			{
				if ((this._Persona != value))
				{
					this._Persona = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Estatus", DbType="Bit")]
		public System.Nullable<bool> Estatus
		{
			get
			{
				return this._Estatus;
			}
			set
			{
				if ((this._Estatus != value))
				{
					this._Estatus = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CambiaClave", DbType="Bit")]
		public System.Nullable<bool> CambiaClave
		{
			get
			{
				return this._CambiaClave;
			}
			set
			{
				if ((this._CambiaClave != value))
				{
					this._CambiaClave = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UsuarioAdicona", DbType="Decimal(20,0)")]
		public System.Nullable<decimal> UsuarioAdicona
		{
			get
			{
				return this._UsuarioAdicona;
			}
			set
			{
				if ((this._UsuarioAdicona != value))
				{
					this._UsuarioAdicona = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FechaAdiciona", DbType="DateTime")]
		public System.Nullable<System.DateTime> FechaAdiciona
		{
			get
			{
				return this._FechaAdiciona;
			}
			set
			{
				if ((this._FechaAdiciona != value))
				{
					this._FechaAdiciona = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UsuarioModifica", DbType="Decimal(20,0)")]
		public System.Nullable<decimal> UsuarioModifica
		{
			get
			{
				return this._UsuarioModifica;
			}
			set
			{
				if ((this._UsuarioModifica != value))
				{
					this._UsuarioModifica = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FechaModifica", DbType="DateTime")]
		public System.Nullable<System.DateTime> FechaModifica
		{
			get
			{
				return this._FechaModifica;
			}
			set
			{
				if ((this._FechaModifica != value))
				{
					this._FechaModifica = value;
				}
			}
		}
	}
	
	public partial class SP_BUSCA_CANTIDAD_INTENTOS_LOGINResult
	{
		
		private int _IdContadorBloqueo;
		
		private System.Nullable<bool> _Estatus0;
		
		private string _Estatus;
		
		private System.Nullable<int> _CantidadIntentos;
		
		public SP_BUSCA_CANTIDAD_INTENTOS_LOGINResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdContadorBloqueo", DbType="Int NOT NULL")]
		public int IdContadorBloqueo
		{
			get
			{
				return this._IdContadorBloqueo;
			}
			set
			{
				if ((this._IdContadorBloqueo != value))
				{
					this._IdContadorBloqueo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Estatus0", DbType="Bit")]
		public System.Nullable<bool> Estatus0
		{
			get
			{
				return this._Estatus0;
			}
			set
			{
				if ((this._Estatus0 != value))
				{
					this._Estatus0 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Estatus", DbType="VarChar(8) NOT NULL", CanBeNull=false)]
		public string Estatus
		{
			get
			{
				return this._Estatus;
			}
			set
			{
				if ((this._Estatus != value))
				{
					this._Estatus = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CantidadIntentos", DbType="Int")]
		public System.Nullable<int> CantidadIntentos
		{
			get
			{
				return this._CantidadIntentos;
			}
			set
			{
				if ((this._CantidadIntentos != value))
				{
					this._CantidadIntentos = value;
				}
			}
		}
	}
	
	public partial class SP_ACTUALIZAR_CANTIDAD_INTENTOS_LOGINResult
	{
		
		private System.Nullable<int> _IdContadorBloqueo;
		
		private System.Nullable<bool> _Estatus;
		
		private System.Nullable<int> _CantidadIntentos;
		
		public SP_ACTUALIZAR_CANTIDAD_INTENTOS_LOGINResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdContadorBloqueo", DbType="Int")]
		public System.Nullable<int> IdContadorBloqueo
		{
			get
			{
				return this._IdContadorBloqueo;
			}
			set
			{
				if ((this._IdContadorBloqueo != value))
				{
					this._IdContadorBloqueo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Estatus", DbType="Bit")]
		public System.Nullable<bool> Estatus
		{
			get
			{
				return this._Estatus;
			}
			set
			{
				if ((this._Estatus != value))
				{
					this._Estatus = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CantidadIntentos", DbType="Int")]
		public System.Nullable<int> CantidadIntentos
		{
			get
			{
				return this._CantidadIntentos;
			}
			set
			{
				if ((this._CantidadIntentos != value))
				{
					this._CantidadIntentos = value;
				}
			}
		}
	}
	
	public partial class SP_BUSCA_CLAVE_SEGURIDAD_WEBResult
	{
		
		private decimal _IdClaveSeguridad;
		
		private decimal _IdUsuario;
		
		private string _Clave;
		
		private System.Nullable<bool> _Estatus0;
		
		private string _Estatus;
		
		public SP_BUSCA_CLAVE_SEGURIDAD_WEBResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdClaveSeguridad", DbType="Decimal(20,0) NOT NULL")]
		public decimal IdClaveSeguridad
		{
			get
			{
				return this._IdClaveSeguridad;
			}
			set
			{
				if ((this._IdClaveSeguridad != value))
				{
					this._IdClaveSeguridad = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdUsuario", DbType="Decimal(20,0) NOT NULL")]
		public decimal IdUsuario
		{
			get
			{
				return this._IdUsuario;
			}
			set
			{
				if ((this._IdUsuario != value))
				{
					this._IdUsuario = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Clave", DbType="VarChar(8000)")]
		public string Clave
		{
			get
			{
				return this._Clave;
			}
			set
			{
				if ((this._Clave != value))
				{
					this._Clave = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Estatus0", DbType="Bit")]
		public System.Nullable<bool> Estatus0
		{
			get
			{
				return this._Estatus0;
			}
			set
			{
				if ((this._Estatus0 != value))
				{
					this._Estatus0 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Estatus", DbType="VarChar(8) NOT NULL", CanBeNull=false)]
		public string Estatus
		{
			get
			{
				return this._Estatus;
			}
			set
			{
				if ((this._Estatus != value))
				{
					this._Estatus = value;
				}
			}
		}
	}
	
	public partial class SP_SACAR_CREDENCIALES_BDResult
	{
		
		private int _IdCredencial;
		
		private string _Usuario;
		
		private string _Clave;
		
		public SP_SACAR_CREDENCIALES_BDResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdCredencial", DbType="Int NOT NULL")]
		public int IdCredencial
		{
			get
			{
				return this._IdCredencial;
			}
			set
			{
				if ((this._IdCredencial != value))
				{
					this._IdCredencial = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Usuario", DbType="VarChar(100)")]
		public string Usuario
		{
			get
			{
				return this._Usuario;
			}
			set
			{
				if ((this._Usuario != value))
				{
					this._Usuario = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Clave", DbType="VarChar(8000)")]
		public string Clave
		{
			get
			{
				return this._Clave;
			}
			set
			{
				if ((this._Clave != value))
				{
					this._Clave = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
