﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace iEmpPowerDAL.Performance_Management_System
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
	public partial class appraisal_formDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public appraisal_formDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString1"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public appraisal_formDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public appraisal_formDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public appraisal_formDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public appraisal_formDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.sp_xml_create_AppraisalID_Name")]
		public int sp_xml_create_AppraisalID_Name([global::System.Data.Linq.Mapping.ParameterAttribute(Name="APPRAISAL_ID", DbType="Char(32)")] string aPPRAISAL_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="APPRAISAL_NAME", DbType="Char(80)")] string aPPRAISAL_NAME)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), aPPRAISAL_ID, aPPRAISAL_NAME);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.sp_xml_create_Appraisal_Dropdown")]
		public int sp_xml_create_Appraisal_Dropdown([global::System.Data.Linq.Mapping.ParameterAttribute(Name="VALUE_NUM", DbType="Decimal(15,0)")] System.Nullable<decimal> vALUE_NUM, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="APPRAISAL_ID", DbType="Char(32)")] string aPPRAISAL_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="VALUE_TEXT", DbType="Char(20)")] string vALUE_TEXT)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), vALUE_NUM, aPPRAISAL_ID, vALUE_TEXT);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.sp_Load_ApprDropdown")]
		public ISingleResult<sp_Load_ApprDropdownResult> sp_Load_ApprDropdown()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<sp_Load_ApprDropdownResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.sp_apr_Load_DropDown")]
		public ISingleResult<sp_apr_Load_DropDownResult> sp_apr_Load_DropDown([global::System.Data.Linq.Mapping.ParameterAttribute(Name="APPRAISAL_NAME", DbType="Char(80)")] string aPPRAISAL_NAME, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="ELEMENT_NAME", DbType="Char(200)")] string eLEMENT_NAME)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), aPPRAISAL_NAME, eLEMENT_NAME);
			return ((ISingleResult<sp_apr_Load_DropDownResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.sp_apr_insert_appraisal")]
		public int sp_apr_insert_appraisal([global::System.Data.Linq.Mapping.ParameterAttribute(Name="APPRAISAL_ID", DbType="Char(32)")] string aPPRAISAL_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="APPRAISAL_NAME", DbType="Char(80)")] string aPPRAISAL_NAME, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="ELEMENT_NAME", DbType="Char(200)")] string eLEMENT_NAME, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="VALUE_TEXT", DbType="Char(20)")] string vALUE_TEXT, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TDLINE", DbType="Char(132)")] string tDLINE, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="APPRAISEE_ID", DbType="VarChar(10)")] string aPPRAISEE_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="APPRAISER_ID", DbType="VarChar(10)")] string aPPRAISER_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="STATUSAPPR", DbType="Char(12)")] string sTATUSAPPR, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Bit")] ref System.Nullable<bool> sendstatus)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), aPPRAISAL_ID, aPPRAISAL_NAME, eLEMENT_NAME, vALUE_TEXT, tDLINE, aPPRAISEE_ID, aPPRAISER_ID, sTATUSAPPR, sendstatus);
			sendstatus = ((System.Nullable<bool>)(result.GetParameterValue(8)));
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.sp_apr_load_appraisal")]
		public ISingleResult<sp_apr_load_appraisalResult> sp_apr_load_appraisal([global::System.Data.Linq.Mapping.ParameterAttribute(Name="APPRAISAL_ID", DbType="Char(32)")] string aPPRAISAL_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="APPRAISAL_NAME", DbType="Char(80)")] string aPPRAISAL_NAME, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="APPRAISEE_ID", DbType="VarChar(10)")] string aPPRAISEE_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="APPRAISER_ID", DbType="VarChar(10)")] string aPPRAISER_ID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), aPPRAISAL_ID, aPPRAISAL_NAME, aPPRAISEE_ID, aPPRAISER_ID);
			return ((ISingleResult<sp_apr_load_appraisalResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.sp_apr_load_appraiseeName")]
		public ISingleResult<sp_apr_load_appraiseeNameResult> sp_apr_load_appraiseeName([global::System.Data.Linq.Mapping.ParameterAttribute(Name="APPRAISEE_ID", DbType="VarChar(10)")] string aPPRAISEE_ID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), aPPRAISEE_ID);
			return ((ISingleResult<sp_apr_load_appraiseeNameResult>)(result.ReturnValue));
		}
	}
	
	public partial class sp_Load_ApprDropdownResult
	{
		
		private string _VALUE_TEXT;
		
		public sp_Load_ApprDropdownResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VALUE_TEXT", DbType="Char(20)")]
		public string VALUE_TEXT
		{
			get
			{
				return this._VALUE_TEXT;
			}
			set
			{
				if ((this._VALUE_TEXT != value))
				{
					this._VALUE_TEXT = value;
				}
			}
		}
	}
	
	public partial class sp_apr_Load_DropDownResult
	{
		
		private string _Column1;
		
		public sp_apr_Load_DropDownResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="", Storage="_Column1", DbType="VarChar(23)")]
		public string Column1
		{
			get
			{
				return this._Column1;
			}
			set
			{
				if ((this._Column1 != value))
				{
					this._Column1 = value;
				}
			}
		}
	}
	
	public partial class sp_apr_load_appraisalResult
	{
		
		private string _APPRAISAL_ID;
		
		private string _APPRAISAL_NAME;
		
		private string _ELEMENT_NAME;
		
		private string _VALUE_TEXT;
		
		private string _TDLINE;
		
		private string _APPRAISEE_ID;
		
		private string _APPRAISER_ID;
		
		private string _STATUSAPPR;
		
		public sp_apr_load_appraisalResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_APPRAISAL_ID", DbType="Char(32)")]
		public string APPRAISAL_ID
		{
			get
			{
				return this._APPRAISAL_ID;
			}
			set
			{
				if ((this._APPRAISAL_ID != value))
				{
					this._APPRAISAL_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_APPRAISAL_NAME", DbType="Char(80)")]
		public string APPRAISAL_NAME
		{
			get
			{
				return this._APPRAISAL_NAME;
			}
			set
			{
				if ((this._APPRAISAL_NAME != value))
				{
					this._APPRAISAL_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ELEMENT_NAME", DbType="Char(200)")]
		public string ELEMENT_NAME
		{
			get
			{
				return this._ELEMENT_NAME;
			}
			set
			{
				if ((this._ELEMENT_NAME != value))
				{
					this._ELEMENT_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VALUE_TEXT", DbType="Char(20)")]
		public string VALUE_TEXT
		{
			get
			{
				return this._VALUE_TEXT;
			}
			set
			{
				if ((this._VALUE_TEXT != value))
				{
					this._VALUE_TEXT = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TDLINE", DbType="Char(132)")]
		public string TDLINE
		{
			get
			{
				return this._TDLINE;
			}
			set
			{
				if ((this._TDLINE != value))
				{
					this._TDLINE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_APPRAISEE_ID", DbType="VarChar(10)")]
		public string APPRAISEE_ID
		{
			get
			{
				return this._APPRAISEE_ID;
			}
			set
			{
				if ((this._APPRAISEE_ID != value))
				{
					this._APPRAISEE_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_APPRAISER_ID", DbType="VarChar(10)")]
		public string APPRAISER_ID
		{
			get
			{
				return this._APPRAISER_ID;
			}
			set
			{
				if ((this._APPRAISER_ID != value))
				{
					this._APPRAISER_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_STATUSAPPR", DbType="Char(12)")]
		public string STATUSAPPR
		{
			get
			{
				return this._STATUSAPPR;
			}
			set
			{
				if ((this._STATUSAPPR != value))
				{
					this._STATUSAPPR = value;
				}
			}
		}
	}
	
	public partial class sp_apr_load_appraiseeNameResult
	{
		
		private string _Column1;
		
		public sp_apr_load_appraiseeNameResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="", Storage="_Column1", DbType="VarChar(81)")]
		public string Column1
		{
			get
			{
				return this._Column1;
			}
			set
			{
				if ((this._Column1 != value))
				{
					this._Column1 = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
