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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Paycompute_NewUIPRD")]
	public partial class mstimesheetreviewDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public mstimesheetreviewDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public mstimesheetreviewDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public mstimesheetreviewDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public mstimesheetreviewDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public mstimesheetreviewDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.sp_ms_Load_TimeSheetReview_Employees")]
		public ISingleResult<sp_ms_Load_TimeSheetReview_EmployeesResult> sp_ms_Load_TimeSheetReview_Employees([global::System.Data.Linq.Mapping.ParameterAttribute(Name="PERNR", DbType="VarChar(50)")] string pERNR, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(10)")] string compcode, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="FromDate", DbType="VarChar(50)")] string fromDate, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="ToDate", DbType="VarChar(50)")] string toDate, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(250)")] string pspnr, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> flag)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), pERNR, compcode, fromDate, toDate, pspnr, flag);
			return ((ISingleResult<sp_ms_Load_TimeSheetReview_EmployeesResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.sp_ms_Load_TimeSheetReview")]
		public ISingleResult<sp_ms_Load_TimeSheetReviewResult> sp_ms_Load_TimeSheetReview([global::System.Data.Linq.Mapping.ParameterAttribute(Name="PERNR", DbType="VarChar(50)")] string pERNR, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(10)")] string compcode, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="FromDate", DbType="Date")] System.Nullable<System.DateTime> fromDate, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="ToDate", DbType="Date")] System.Nullable<System.DateTime> toDate, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="MngPernr", DbType="VarChar(20)")] string mngPernr, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(250)")] string pspnr, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> flag)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), pERNR, compcode, fromDate, toDate, mngPernr, pspnr, flag);
			return ((ISingleResult<sp_ms_Load_TimeSheetReviewResult>)(result.ReturnValue));
		}
	}
	
	public partial class sp_ms_Load_TimeSheetReview_EmployeesResult
	{
		
		private string _PERNR;
		
		private string _ENAME;
		
		private string _PROJ;
		
		private string _WBS;
		
		private string _Activity;
		
		private string _Attd;
		
		private string _REMARKS;
		
		private string _status;
		
		private System.Nullable<System.DateTime> _WORKINGDATE;
		
		private string _CATSHOURS;
		
		public sp_ms_Load_TimeSheetReview_EmployeesResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PERNR", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ENAME", DbType="VarChar(152)")]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PROJ", DbType="VarChar(100)")]
		public string PROJ
		{
			get
			{
				return this._PROJ;
			}
			set
			{
				if ((this._PROJ != value))
				{
					this._PROJ = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WBS", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string WBS
		{
			get
			{
				return this._WBS;
			}
			set
			{
				if ((this._WBS != value))
				{
					this._WBS = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Activity", DbType="VarChar(MAX)")]
		public string Activity
		{
			get
			{
				return this._Activity;
			}
			set
			{
				if ((this._Activity != value))
				{
					this._Activity = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Attd", DbType="VarChar(25) NOT NULL", CanBeNull=false)]
		public string Attd
		{
			get
			{
				return this._Attd;
			}
			set
			{
				if ((this._Attd != value))
				{
					this._Attd = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_REMARKS", DbType="VarChar(MAX)")]
		public string REMARKS
		{
			get
			{
				return this._REMARKS;
			}
			set
			{
				if ((this._REMARKS != value))
				{
					this._REMARKS = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_status", DbType="VarChar(10)")]
		public string status
		{
			get
			{
				return this._status;
			}
			set
			{
				if ((this._status != value))
				{
					this._status = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WORKINGDATE", DbType="Date")]
		public System.Nullable<System.DateTime> WORKINGDATE
		{
			get
			{
				return this._WORKINGDATE;
			}
			set
			{
				if ((this._WORKINGDATE != value))
				{
					this._WORKINGDATE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CATSHOURS", DbType="VarChar(50)")]
		public string CATSHOURS
		{
			get
			{
				return this._CATSHOURS;
			}
			set
			{
				if ((this._CATSHOURS != value))
				{
					this._CATSHOURS = value;
				}
			}
		}
	}
	
	public partial class sp_ms_Load_TimeSheetReviewResult
	{
		
		private string _PERNR;
		
		private string _ENAME;
		
		private string _CATSHOURS;
		
		private string _status;
		
		private System.Nullable<System.DateTime> _WORKINGDATE;
		
		private string _KTEXT;
		
		private string _ATEXT;
		
		private string _Activity;
		
		private string _POST1;
		
		private string _REMARKS;
		
		private string _EMP_DEPT;
		
		public sp_ms_Load_TimeSheetReviewResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PERNR", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ENAME", DbType="VarChar(152)")]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CATSHOURS", DbType="VarChar(50)")]
		public string CATSHOURS
		{
			get
			{
				return this._CATSHOURS;
			}
			set
			{
				if ((this._CATSHOURS != value))
				{
					this._CATSHOURS = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_status", DbType="VarChar(10)")]
		public string status
		{
			get
			{
				return this._status;
			}
			set
			{
				if ((this._status != value))
				{
					this._status = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WORKINGDATE", DbType="Date")]
		public System.Nullable<System.DateTime> WORKINGDATE
		{
			get
			{
				return this._WORKINGDATE;
			}
			set
			{
				if ((this._WORKINGDATE != value))
				{
					this._WORKINGDATE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_KTEXT", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string KTEXT
		{
			get
			{
				return this._KTEXT;
			}
			set
			{
				if ((this._KTEXT != value))
				{
					this._KTEXT = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ATEXT", DbType="VarChar(25) NOT NULL", CanBeNull=false)]
		public string ATEXT
		{
			get
			{
				return this._ATEXT;
			}
			set
			{
				if ((this._ATEXT != value))
				{
					this._ATEXT = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Activity", DbType="VarChar(MAX)")]
		public string Activity
		{
			get
			{
				return this._Activity;
			}
			set
			{
				if ((this._Activity != value))
				{
					this._Activity = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_POST1", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string POST1
		{
			get
			{
				return this._POST1;
			}
			set
			{
				if ((this._POST1 != value))
				{
					this._POST1 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_REMARKS", DbType="VarChar(MAX)")]
		public string REMARKS
		{
			get
			{
				return this._REMARKS;
			}
			set
			{
				if ((this._REMARKS != value))
				{
					this._REMARKS = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EMP_DEPT", DbType="VarChar(MAX) NOT NULL", CanBeNull=false)]
		public string EMP_DEPT
		{
			get
			{
				return this._EMP_DEPT;
			}
			set
			{
				if ((this._EMP_DEPT != value))
				{
					this._EMP_DEPT = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
