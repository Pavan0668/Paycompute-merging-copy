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

namespace iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="IEmpPower_NRR")]
	public partial class EligibilityDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public EligibilityDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public EligibilityDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public EligibilityDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public EligibilityDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public EligibilityDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.sp_get_Vehicle_Eligibility")]
		public ISingleResult<sp_get_Vehicle_EligibilityResult> sp_get_Vehicle_Eligibility([global::System.Data.Linq.Mapping.ParameterAttribute(Name="PERNR", DbType="VarChar(10)")] string pERNR)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), pERNR);
			return ((ISingleResult<sp_get_Vehicle_EligibilityResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.sp_get_HotelRoom_Eligibility")]
		public ISingleResult<sp_get_HotelRoom_EligibilityResult> sp_get_HotelRoom_Eligibility([global::System.Data.Linq.Mapping.ParameterAttribute(Name="PERNR", DbType="VarChar(10)")] string pERNR)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), pERNR);
			return ((ISingleResult<sp_get_HotelRoom_EligibilityResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.sp_get_FlyNumber_Eligibility")]
		public ISingleResult<sp_get_FlyNumber_EligibilityResult> sp_get_FlyNumber_Eligibility([global::System.Data.Linq.Mapping.ParameterAttribute(Name="PERNR", DbType="VarChar(10)")] string pERNR)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), pERNR);
			return ((ISingleResult<sp_get_FlyNumber_EligibilityResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.sp_Get_MailList_Traveller")]
		public int sp_Get_MailList_Traveller([global::System.Data.Linq.Mapping.ParameterAttribute(Name="PERNR", DbType="VarChar(30)")] string pERNR, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="HR_Name", DbType="VarChar(100)")] ref string hR_Name, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="HR_Email", DbType="VarChar(100)")] ref string hR_Email, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="HR_Phn", DbType="VarChar(15)")] ref string hR_Phn, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="SuperVisor_Name", DbType="VarChar(100)")] ref string superVisor_Name, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="SuperVisor_Phn", DbType="VarChar(15)")] ref string superVisor_Phn, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="SuperVisor_Email", DbType="VarChar(100)")] ref string superVisor_Email, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="PERNR_Name", DbType="VarChar(100)")] ref string pERNR_Name, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="PERNR_Email", DbType="VarChar(100)")] ref string pERNR_Email, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TD_Name", DbType="VarChar(100)")] ref string tD_Name, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TD_Email", DbType="VarChar(100)")] ref string tD_Email, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TD_Phn", DbType="VarChar(15)")] ref string tD_Phn)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), pERNR, hR_Name, hR_Email, hR_Phn, superVisor_Name, superVisor_Phn, superVisor_Email, pERNR_Name, pERNR_Email, tD_Name, tD_Email, tD_Phn);
			hR_Name = ((string)(result.GetParameterValue(1)));
			hR_Email = ((string)(result.GetParameterValue(2)));
			hR_Phn = ((string)(result.GetParameterValue(3)));
			superVisor_Name = ((string)(result.GetParameterValue(4)));
			superVisor_Phn = ((string)(result.GetParameterValue(5)));
			superVisor_Email = ((string)(result.GetParameterValue(6)));
			pERNR_Name = ((string)(result.GetParameterValue(7)));
			pERNR_Email = ((string)(result.GetParameterValue(8)));
			tD_Name = ((string)(result.GetParameterValue(9)));
			tD_Email = ((string)(result.GetParameterValue(10)));
			tD_Phn = ((string)(result.GetParameterValue(11)));
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.sp_Get_MailList_HODTDTraveller")]
		public int sp_Get_MailList_HODTDTraveller([global::System.Data.Linq.Mapping.ParameterAttribute(Name="PERNR", DbType="VarChar(30)")] string pERNR, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="SPERNR", DbType="VarChar(30)")] string sPERNR, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="HR_Name", DbType="VarChar(100)")] ref string hR_Name, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="HR_Phn", DbType="VarChar(15)")] ref string hR_Phn, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="SuperVisor_Name", DbType="VarChar(100)")] ref string superVisor_Name, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="SuperVisor_Phn", DbType="VarChar(15)")] ref string superVisor_Phn, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="HR_Email", DbType="VarChar(100)")] ref string hR_Email, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="SuperVisor_Email", DbType="VarChar(100)")] ref string superVisor_Email, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="PERNR_Name", DbType="VarChar(100)")] ref string pERNR_Name, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="PERNR_Email", DbType="VarChar(100)")] ref string pERNR_Email, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TD_Name", DbType="VarChar(100)")] ref string tD_Name, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TD_Email", DbType="VarChar(100)")] ref string tD_Email, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TD_Phn", DbType="VarChar(15)")] ref string tD_Phn)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), pERNR, sPERNR, hR_Name, hR_Phn, superVisor_Name, superVisor_Phn, hR_Email, superVisor_Email, pERNR_Name, pERNR_Email, tD_Name, tD_Email, tD_Phn);
			hR_Name = ((string)(result.GetParameterValue(2)));
			hR_Phn = ((string)(result.GetParameterValue(3)));
			superVisor_Name = ((string)(result.GetParameterValue(4)));
			superVisor_Phn = ((string)(result.GetParameterValue(5)));
			hR_Email = ((string)(result.GetParameterValue(6)));
			superVisor_Email = ((string)(result.GetParameterValue(7)));
			pERNR_Name = ((string)(result.GetParameterValue(8)));
			pERNR_Email = ((string)(result.GetParameterValue(9)));
			tD_Name = ((string)(result.GetParameterValue(10)));
			tD_Email = ((string)(result.GetParameterValue(11)));
			tD_Phn = ((string)(result.GetParameterValue(12)));
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.[sp_get_master.ZTR_FLYER_NUMBER]")]
		public ISingleResult<sp_get_master_ZTR_FLYER_NUMBERResult> sp_get_master_ZTR_FLYER_NUMBER([global::System.Data.Linq.Mapping.ParameterAttribute(Name="PERNR", DbType="VarChar(50)")] string pERNR)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), pERNR);
			return ((ISingleResult<sp_get_master_ZTR_FLYER_NUMBERResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.[sp_get_master.ZTR_PASSPORT]")]
		public ISingleResult<sp_get_master_ZTR_PASSPORTResult> sp_get_master_ZTR_PASSPORT([global::System.Data.Linq.Mapping.ParameterAttribute(Name="PERNR", DbType="VarChar(50)")] string pERNR)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), pERNR);
			return ((ISingleResult<sp_get_master_ZTR_PASSPORTResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.[sp_get_master.ZTR_TRAVEL_INS]")]
		public ISingleResult<sp_get_master_ZTR_TRAVEL_INSResult> sp_get_master_ZTR_TRAVEL_INS([global::System.Data.Linq.Mapping.ParameterAttribute(Name="PERNR", DbType="VarChar(50)")] string pERNR)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), pERNR);
			return ((ISingleResult<sp_get_master_ZTR_TRAVEL_INSResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.[sp_get_master.ZTR_VISA]")]
		public ISingleResult<sp_get_master_ZTR_VISAResult> sp_get_master_ZTR_VISA([global::System.Data.Linq.Mapping.ParameterAttribute(Name="PERNR", DbType="VarChar(50)")] string pERNR)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), pERNR);
			return ((ISingleResult<sp_get_master_ZTR_VISAResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.sp_Get_MailList_Training")]
		public int sp_Get_MailList_Training([global::System.Data.Linq.Mapping.ParameterAttribute(Name="PERNR", DbType="VarChar(30)")] string pERNR, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="HR_Name", DbType="VarChar(100)")] ref string hR_Name, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="HR_Email", DbType="VarChar(100)")] ref string hR_Email, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="HR_Phn", DbType="VarChar(15)")] ref string hR_Phn, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="SuperVisor_Name", DbType="VarChar(100)")] ref string superVisor_Name, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="SuperVisor_Phn", DbType="VarChar(15)")] ref string superVisor_Phn, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="SuperVisor_Email", DbType="VarChar(100)")] ref string superVisor_Email, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="PERNR_Name", DbType="VarChar(100)")] ref string pERNR_Name, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="PERNR_Email", DbType="VarChar(100)")] ref string pERNR_Email, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TD_Name", DbType="VarChar(100)")] ref string tD_Name, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TD_Email", DbType="VarChar(100)")] ref string tD_Email, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TD_Phn", DbType="VarChar(15)")] ref string tD_Phn)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), pERNR, hR_Name, hR_Email, hR_Phn, superVisor_Name, superVisor_Phn, superVisor_Email, pERNR_Name, pERNR_Email, tD_Name, tD_Email, tD_Phn);
			hR_Name = ((string)(result.GetParameterValue(1)));
			hR_Email = ((string)(result.GetParameterValue(2)));
			hR_Phn = ((string)(result.GetParameterValue(3)));
			superVisor_Name = ((string)(result.GetParameterValue(4)));
			superVisor_Phn = ((string)(result.GetParameterValue(5)));
			superVisor_Email = ((string)(result.GetParameterValue(6)));
			pERNR_Name = ((string)(result.GetParameterValue(7)));
			pERNR_Email = ((string)(result.GetParameterValue(8)));
			tD_Name = ((string)(result.GetParameterValue(9)));
			tD_Email = ((string)(result.GetParameterValue(10)));
			tD_Phn = ((string)(result.GetParameterValue(11)));
			return ((int)(result.ReturnValue));
		}
	}
	
	public partial class sp_get_Vehicle_EligibilityResult
	{
		
		private string _VEC_TYPE;
		
		private string _VEC_CATEGORY;
		
		public sp_get_Vehicle_EligibilityResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VEC_TYPE", DbType="VarChar(50)")]
		public string VEC_TYPE
		{
			get
			{
				return this._VEC_TYPE;
			}
			set
			{
				if ((this._VEC_TYPE != value))
				{
					this._VEC_TYPE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VEC_CATEGORY", DbType="VarChar(50)")]
		public string VEC_CATEGORY
		{
			get
			{
				return this._VEC_CATEGORY;
			}
			set
			{
				if ((this._VEC_CATEGORY != value))
				{
					this._VEC_CATEGORY = value;
				}
			}
		}
	}
	
	public partial class sp_get_HotelRoom_EligibilityResult
	{
		
		private string _HOTEL_CATEGORY;
		
		private string _ROOM_CATEGORY;
		
		public sp_get_HotelRoom_EligibilityResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HOTEL_CATEGORY", DbType="VarChar(50)")]
		public string HOTEL_CATEGORY
		{
			get
			{
				return this._HOTEL_CATEGORY;
			}
			set
			{
				if ((this._HOTEL_CATEGORY != value))
				{
					this._HOTEL_CATEGORY = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ROOM_CATEGORY", DbType="VarChar(50)")]
		public string ROOM_CATEGORY
		{
			get
			{
				return this._ROOM_CATEGORY;
			}
			set
			{
				if ((this._ROOM_CATEGORY != value))
				{
					this._ROOM_CATEGORY = value;
				}
			}
		}
	}
	
	public partial class sp_get_FlyNumber_EligibilityResult
	{
		
		private string _ICNUM;
		
		public sp_get_FlyNumber_EligibilityResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ICNUM", DbType="Char(30)")]
		public string ICNUM
		{
			get
			{
				return this._ICNUM;
			}
			set
			{
				if ((this._ICNUM != value))
				{
					this._ICNUM = value;
				}
			}
		}
	}
	
	public partial class sp_get_master_ZTR_FLYER_NUMBERResult
	{
		
		private string _PERNR;
		
		private string _FRFLYNUM;
		
		private string _EMPNAME;
		
		private string _AIRLINE;
		
		private string _VALSTATUS;
		
		public sp_get_master_ZTR_FLYER_NUMBERResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PERNR", DbType="VarChar(8)")]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FRFLYNUM", DbType="VarChar(50)")]
		public string FRFLYNUM
		{
			get
			{
				return this._FRFLYNUM;
			}
			set
			{
				if ((this._FRFLYNUM != value))
				{
					this._FRFLYNUM = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EMPNAME", DbType="VarChar(50)")]
		public string EMPNAME
		{
			get
			{
				return this._EMPNAME;
			}
			set
			{
				if ((this._EMPNAME != value))
				{
					this._EMPNAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AIRLINE", DbType="VarChar(50)")]
		public string AIRLINE
		{
			get
			{
				return this._AIRLINE;
			}
			set
			{
				if ((this._AIRLINE != value))
				{
					this._AIRLINE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VALSTATUS", DbType="VarChar(50)")]
		public string VALSTATUS
		{
			get
			{
				return this._VALSTATUS;
			}
			set
			{
				if ((this._VALSTATUS != value))
				{
					this._VALSTATUS = value;
				}
			}
		}
	}
	
	public partial class sp_get_master_ZTR_PASSPORTResult
	{
		
		private string _PERNR;
		
		private string _EMPNAME;
		
		private string _PASNUM;
		
		private System.Nullable<System.DateTime> _DOI;
		
		private System.Nullable<System.DateTime> _DOE;
		
		private string _PLISS;
		
		public sp_get_master_ZTR_PASSPORTResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PERNR", DbType="VarChar(50)")]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EMPNAME", DbType="VarChar(50)")]
		public string EMPNAME
		{
			get
			{
				return this._EMPNAME;
			}
			set
			{
				if ((this._EMPNAME != value))
				{
					this._EMPNAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PASNUM", DbType="VarChar(50)")]
		public string PASNUM
		{
			get
			{
				return this._PASNUM;
			}
			set
			{
				if ((this._PASNUM != value))
				{
					this._PASNUM = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DOI", DbType="Date")]
		public System.Nullable<System.DateTime> DOI
		{
			get
			{
				return this._DOI;
			}
			set
			{
				if ((this._DOI != value))
				{
					this._DOI = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DOE", DbType="Date")]
		public System.Nullable<System.DateTime> DOE
		{
			get
			{
				return this._DOE;
			}
			set
			{
				if ((this._DOE != value))
				{
					this._DOE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PLISS", DbType="VarChar(50)")]
		public string PLISS
		{
			get
			{
				return this._PLISS;
			}
			set
			{
				if ((this._PLISS != value))
				{
					this._PLISS = value;
				}
			}
		}
	}
	
	public partial class sp_get_master_ZTR_TRAVEL_INSResult
	{
		
		private string _PERNR;
		
		private string _TRINSNO;
		
		private string _EMPNAME;
		
		private System.Nullable<System.DateTime> _DOI;
		
		private System.Nullable<System.DateTime> _DOE;
		
		private string _PLAN1;
		
		private string _PREMIUM;
		
		private string _AGENT_NAME;
		
		public sp_get_master_ZTR_TRAVEL_INSResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PERNR", DbType="VarChar(50)")]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TRINSNO", DbType="VarChar(50)")]
		public string TRINSNO
		{
			get
			{
				return this._TRINSNO;
			}
			set
			{
				if ((this._TRINSNO != value))
				{
					this._TRINSNO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EMPNAME", DbType="VarChar(50)")]
		public string EMPNAME
		{
			get
			{
				return this._EMPNAME;
			}
			set
			{
				if ((this._EMPNAME != value))
				{
					this._EMPNAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DOI", DbType="Date")]
		public System.Nullable<System.DateTime> DOI
		{
			get
			{
				return this._DOI;
			}
			set
			{
				if ((this._DOI != value))
				{
					this._DOI = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DOE", DbType="Date")]
		public System.Nullable<System.DateTime> DOE
		{
			get
			{
				return this._DOE;
			}
			set
			{
				if ((this._DOE != value))
				{
					this._DOE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PLAN1", DbType="VarChar(50)")]
		public string PLAN1
		{
			get
			{
				return this._PLAN1;
			}
			set
			{
				if ((this._PLAN1 != value))
				{
					this._PLAN1 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PREMIUM", DbType="VarChar(50)")]
		public string PREMIUM
		{
			get
			{
				return this._PREMIUM;
			}
			set
			{
				if ((this._PREMIUM != value))
				{
					this._PREMIUM = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AGENT_NAME", DbType="VarChar(50)")]
		public string AGENT_NAME
		{
			get
			{
				return this._AGENT_NAME;
			}
			set
			{
				if ((this._AGENT_NAME != value))
				{
					this._AGENT_NAME = value;
				}
			}
		}
	}
	
	public partial class sp_get_master_ZTR_VISAResult
	{
		
		private string _PERNR;
		
		private string _VINUM;
		
		private string _PASNUM;
		
		private string _EMPNAME;
		
		private string _COUNTRY;
		
		private System.Nullable<System.DateTime> _DOI;
		
		private System.Nullable<System.DateTime> _DOE;
		
		private string _VISA_TYPE;
		
		public sp_get_master_ZTR_VISAResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PERNR", DbType="VarChar(50)")]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VINUM", DbType="VarChar(50)")]
		public string VINUM
		{
			get
			{
				return this._VINUM;
			}
			set
			{
				if ((this._VINUM != value))
				{
					this._VINUM = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PASNUM", DbType="VarChar(50)")]
		public string PASNUM
		{
			get
			{
				return this._PASNUM;
			}
			set
			{
				if ((this._PASNUM != value))
				{
					this._PASNUM = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EMPNAME", DbType="VarChar(50)")]
		public string EMPNAME
		{
			get
			{
				return this._EMPNAME;
			}
			set
			{
				if ((this._EMPNAME != value))
				{
					this._EMPNAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_COUNTRY", DbType="VarChar(50)")]
		public string COUNTRY
		{
			get
			{
				return this._COUNTRY;
			}
			set
			{
				if ((this._COUNTRY != value))
				{
					this._COUNTRY = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DOI", DbType="Date")]
		public System.Nullable<System.DateTime> DOI
		{
			get
			{
				return this._DOI;
			}
			set
			{
				if ((this._DOI != value))
				{
					this._DOI = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DOE", DbType="Date")]
		public System.Nullable<System.DateTime> DOE
		{
			get
			{
				return this._DOE;
			}
			set
			{
				if ((this._DOE != value))
				{
					this._DOE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VISA_TYPE", DbType="VarChar(50)")]
		public string VISA_TYPE
		{
			get
			{
				return this._VISA_TYPE;
			}
			set
			{
				if ((this._VISA_TYPE != value))
				{
					this._VISA_TYPE = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
