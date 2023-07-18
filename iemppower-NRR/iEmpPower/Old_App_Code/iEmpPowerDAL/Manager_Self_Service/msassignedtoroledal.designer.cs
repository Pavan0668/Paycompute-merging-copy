﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18063
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace iEmpPower.Old_App_Code.iEmpPowerDAL.Manager_Self_Service
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="iEmpPower")]
	public partial class msassignedtoroledalDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public msassignedtoroledalDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public msassignedtoroledalDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public msassignedtoroledalDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public msassignedtoroledalDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public msassignedtoroledalDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.sp_ms_load_completed_assigned_to_role")]
		public ISingleResult<sp_ms_load_completed_assigned_to_roleResult> sp_ms_load_completed_assigned_to_role([global::System.Data.Linq.Mapping.ParameterAttribute(Name="PERNR", DbType="VarChar(10)")] string pERNR)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), pERNR);
			return ((ISingleResult<sp_ms_load_completed_assigned_to_roleResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.sp_ms_load_pending_assigned_to_role")]
		public ISingleResult<sp_ms_load_pending_assigned_to_roleResult> sp_ms_load_pending_assigned_to_role([global::System.Data.Linq.Mapping.ParameterAttribute(Name="PERNR", DbType="VarChar(10)")] string pERNR)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), pERNR);
			return ((ISingleResult<sp_ms_load_pending_assigned_to_roleResult>)(result.ReturnValue));
		}
	}
	
	public partial class sp_ms_load_completed_assigned_to_roleResult
	{
		
		private string _pkey;
		
		private string _PERNR;
		
		private string _change_approval;
		
		private string _Status;
		
		private System.Nullable<System.DateTime> _Last_Activity_Date;
		
		private string _ENAME;
		
		private string _PLSXT;
		
		private string _USRID;
		
		public sp_ms_load_completed_assigned_to_roleResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_pkey", DbType="VarChar(50)")]
		public string pkey
		{
			get
			{
				return this._pkey;
			}
			set
			{
				if ((this._pkey != value))
				{
					this._pkey = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PERNR", DbType="VarChar(10)")]
		public string PERNR
		{
			get
			{
				return this._PERNR;
			}
			set
			{
				if ((this._PERNR != value))
				{
					this._PERNR = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_change_approval", DbType="VarChar(50)")]
		public string change_approval
		{
			get
			{
				return this._change_approval;
			}
			set
			{
				if ((this._change_approval != value))
				{
					this._change_approval = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Status", DbType="VarChar(50)")]
		public string Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				if ((this._Status != value))
				{
					this._Status = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Last_Activity_Date", DbType="DateTime")]
		public System.Nullable<System.DateTime> Last_Activity_Date
		{
			get
			{
				return this._Last_Activity_Date;
			}
			set
			{
				if ((this._Last_Activity_Date != value))
				{
					this._Last_Activity_Date = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ENAME", DbType="VarChar(100)")]
		public string ENAME
		{
			get
			{
				return this._ENAME;
			}
			set
			{
				if ((this._ENAME != value))
				{
					this._ENAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PLSXT", DbType="VarChar(50)")]
		public string PLSXT
		{
			get
			{
				return this._PLSXT;
			}
			set
			{
				if ((this._PLSXT != value))
				{
					this._PLSXT = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_USRID", DbType="VarChar(100)")]
		public string USRID
		{
			get
			{
				return this._USRID;
			}
			set
			{
				if ((this._USRID != value))
				{
					this._USRID = value;
				}
			}
		}
	}
	
	public partial class sp_ms_load_pending_assigned_to_roleResult
	{
		
		private string _pkey;
		
		private string _PERNR;
		
		private string _change_approval;
		
		private string _Status;
		
		private System.Nullable<System.DateTime> _Last_Activity_Date;
		
		private string _ENAME;
		
		private string _PLSXT;
		
		private string _USRID;
		
		public sp_ms_load_pending_assigned_to_roleResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_pkey", DbType="VarChar(50)")]
		public string pkey
		{
			get
			{
				return this._pkey;
			}
			set
			{
				if ((this._pkey != value))
				{
					this._pkey = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PERNR", DbType="VarChar(10)")]
		public string PERNR
		{
			get
			{
				return this._PERNR;
			}
			set
			{
				if ((this._PERNR != value))
				{
					this._PERNR = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_change_approval", DbType="VarChar(50)")]
		public string change_approval
		{
			get
			{
				return this._change_approval;
			}
			set
			{
				if ((this._change_approval != value))
				{
					this._change_approval = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Status", DbType="VarChar(50)")]
		public string Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				if ((this._Status != value))
				{
					this._Status = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Last_Activity_Date", DbType="DateTime")]
		public System.Nullable<System.DateTime> Last_Activity_Date
		{
			get
			{
				return this._Last_Activity_Date;
			}
			set
			{
				if ((this._Last_Activity_Date != value))
				{
					this._Last_Activity_Date = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ENAME", DbType="VarChar(100)")]
		public string ENAME
		{
			get
			{
				return this._ENAME;
			}
			set
			{
				if ((this._ENAME != value))
				{
					this._ENAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PLSXT", DbType="VarChar(50)")]
		public string PLSXT
		{
			get
			{
				return this._PLSXT;
			}
			set
			{
				if ((this._PLSXT != value))
				{
					this._PLSXT = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_USRID", DbType="VarChar(100)")]
		public string USRID
		{
			get
			{
				return this._USRID;
			}
			set
			{
				if ((this._USRID != value))
				{
					this._USRID = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
