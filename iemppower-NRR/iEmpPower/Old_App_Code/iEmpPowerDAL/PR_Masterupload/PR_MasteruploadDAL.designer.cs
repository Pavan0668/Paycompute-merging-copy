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

namespace iEmpPower.Old_App_Code.iEmpPowerDAL.PR_Masterupload
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Paycompute_merging")]
	public partial class PR_MasteruploadDALDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public PR_MasteruploadDALDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["Paycompute_NewUIPRDConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public PR_MasteruploadDALDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PR_MasteruploadDALDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PR_MasteruploadDALDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PR_MasteruploadDALDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_PR_uploadExcell_master_T001W")]
		public int usp_PR_uploadExcell_master_T001W([global::System.Data.Linq.Mapping.ParameterAttribute(Name="WERKS", DbType="Char(4)")] string wERKS, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="NAME1", DbType="VarChar(50)")] string nAME1)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), wERKS, nAME1);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_PR_uploadExcell_master_T006J")]
		public int usp_PR_uploadExcell_master_T006J([global::System.Data.Linq.Mapping.ParameterAttribute(Name="ISOCODE", DbType="Char(4)")] string iSOCODE, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="ISOTXT", DbType="VarChar(30)")] string iSOTXT)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iSOCODE, iSOTXT);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_PR_uploadExcell_master_T024")]
		public int usp_PR_uploadExcell_master_T024([global::System.Data.Linq.Mapping.ParameterAttribute(Name="EKGRP", DbType="Char(3)")] string eKGRP, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="EKNAM", DbType="VarChar(50)")] string eKNAM)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), eKGRP, eKNAM);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_PR_uploadExcell_master_TSPAT")]
		public int usp_PR_uploadExcell_master_TSPAT([global::System.Data.Linq.Mapping.ParameterAttribute(Name="SPART", DbType="Char(4)")] string sPART, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="VTEXT", DbType="VarChar(30)")] string vTEXT)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sPART, vTEXT);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_PR_uploadExcell_master_ZMM_MIS")]
		public int usp_PR_uploadExcell_master_ZMM_MIS([global::System.Data.Linq.Mapping.ParameterAttribute(Name="CID", DbType="VarChar(10)")] string cID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="C_DESC", DbType="VarChar(MAX)")] string c_DESC, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="A_DESC", DbType="VarChar(MAX)")] string a_DESC, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="B_DESC", DbType="VarChar(MAX)")] string b_DESC)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), cID, c_DESC, a_DESC, b_DESC);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_PR_uploadExcell_master_ZMM_PR_REGION")]
		public int usp_PR_uploadExcell_master_ZMM_PR_REGION([global::System.Data.Linq.Mapping.ParameterAttribute(Name="REGION_ID", DbType="Char(5)")] string rEGION_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="REGION_TEXT", DbType="VarChar(25)")] string rEGION_TEXT)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), rEGION_ID, rEGION_TEXT);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_iexpense_uploadExcell_master_T512T")]
		public int usp_iexpense_uploadExcell_master_T512T([global::System.Data.Linq.Mapping.ParameterAttribute(Name="LGART", DbType="VarChar(4)")] string lGART, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="LGTXT", DbType="VarChar(30)")] string lGTXT)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), lGART, lGTXT);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_master_uploadExcell_FBP_master_T7INA3")]
		public int usp_master_uploadExcell_FBP_master_T7INA3([global::System.Data.Linq.Mapping.ParameterAttribute(Name="TRFAR", DbType="Char(4)")] string tRFAR, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TRFGB", DbType="Char(4)")] string tRFGB, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TRFGR", DbType="Char(8)")] string tRFGR, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TRFST", DbType="Char(2)")] string tRFST, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="ALGRP", DbType="Char(4)")] string aLGRP)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), tRFAR, tRFGB, tRFGR, tRFST, aLGRP);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_master_uploadExcell_FBP_MSS_PA0008")]
		public int usp_master_uploadExcell_FBP_MSS_PA0008([global::System.Data.Linq.Mapping.ParameterAttribute(Name="PERNR", DbType="VarChar(8)")] string pERNR, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TRFAR", DbType="VarChar(2)")] string tRFAR, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TRFGB", DbType="VarChar(2)")] string tRFGB, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TRFGR", DbType="VarChar(8)")] string tRFGR, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TRFST", DbType="VarChar(2)")] string tRFST, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="BEGDA", DbType="Date")] System.Nullable<System.DateTime> bEGDA, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="ENDDA", DbType="Date")] System.Nullable<System.DateTime> eNDDA)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), pERNR, tRFAR, tRFGB, tRFGR, tRFST, bEGDA, eNDDA);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_master_uploadExcell_FBP_PA9001")]
		public int usp_master_uploadExcell_FBP_PA9001([global::System.Data.Linq.Mapping.ParameterAttribute(Name="PERNR", DbType="VarChar(10)")] string pERNR, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="AN_FBP", DbType="VarChar(13)")] string aN_FBP)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), pERNR, aN_FBP);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_master_uploadExcell_FBP_T7INA9")]
		public int usp_master_uploadExcell_FBP_T7INA9([global::System.Data.Linq.Mapping.ParameterAttribute(Name="ALGRP", DbType="VarChar(4)")] string aLGRP, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="LGART", DbType="VarChar(4)")] string lGART, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="AMUNT", DbType="VarChar(50)")] string aMUNT)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), aLGRP, lGART, aMUNT);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_master_uploadExcell_FBP_FBPHOA")]
		public int usp_master_uploadExcell_FBP_FBPHOA([global::System.Data.Linq.Mapping.ParameterAttribute(Name="LGART", DbType="Char(4)")] string lGART, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="LTEXT", DbType="VarChar(100)")] string lTEXT, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Colid", DbType="VarChar(50)")] string colid)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), lGART, lTEXT, colid);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_master_uploadExcell_T7INI3_IT")]
		public int usp_master_uploadExcell_T7INI3_IT([global::System.Data.Linq.Mapping.ParameterAttribute(Name="ICODE", DbType="VarChar(2)")] string iCODE, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="ITEXT", DbType="VarChar(100)")] string iTEXT)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iCODE, iTEXT);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_master_uploadExcell_T7INI4_IT")]
		public int usp_master_uploadExcell_T7INI4_IT([global::System.Data.Linq.Mapping.ParameterAttribute(Name="ICODE", DbType="VarChar(2)")] string iCODE, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="ENDDA", DbType="Date")] System.Nullable<System.DateTime> eNDDA, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="BEGDA", DbType="Date")] System.Nullable<System.DateTime> bEGDA, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="CTGRY", DbType="VarChar(2)")] string cTGRY, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="WAEHI", DbType="VarChar(50)")] string wAEHI, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="ITLMT", DbType="VarChar(50)")] string iTLMT)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iCODE, eNDDA, bEGDA, cTGRY, wAEHI, iTLMT);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_master_uploadExcell_T7INI8_IT")]
		public int usp_master_uploadExcell_T7INI8_IT([global::System.Data.Linq.Mapping.ParameterAttribute(Name="SBSEC", DbType="VarChar(2)")] string sBSEC, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="SBDIV", DbType="VarChar(2)")] string sBDIV, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="SBDDS", DbType="VarChar(200)")] string sBDDS)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sBSEC, sBDIV, sBDDS);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_master_uploadExcell_T7INI9_IT")]
		public int usp_master_uploadExcell_T7INI9_IT([global::System.Data.Linq.Mapping.ParameterAttribute(Name="SBSEC", DbType="VarChar(50)")] string sBSEC, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="SBDIV", DbType="VarChar(50)")] string sBDIV, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="SDVLT", DbType="VarChar(50)")] string sDVLT, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TXEXM", DbType="VarChar(50)")] string tXEXM)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sBSEC, sBDIV, sDVLT, tXEXM);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_master_uploadExcell_T7INI5_IT")]
		public int usp_master_uploadExcell_T7INI5_IT([global::System.Data.Linq.Mapping.ParameterAttribute(Name="SBSEC", DbType="VarChar(50)")] string sBSEC, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="SBTDS", DbType="Char(20)")] string sBTDS)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sBSEC, sBTDS);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_master_uploadExcell_master_PRPS_CM")]
		public int usp_master_uploadExcell_master_PRPS_CM(
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="POSID", DbType="VarChar(30)")] string pOSID, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="POST1", DbType="VarChar(50)")] string pOST1, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="PSPHI", DbType="VarChar(30)")] string pSPHI, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="VERNR", DbType="VarChar(8)")] string vERNR, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="VERNA", DbType="VarChar(50)")] string vERNA, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="PBUKR", DbType="VarChar(4)")] string pBUKR, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="POSKI", DbType="VarChar(16)")] string pOSKI, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="PSPNR", DbType="Int")] System.Nullable<int> pSPNR, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="Created_By", DbType="VarChar(20)")] string created_By, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="Created_On", DbType="DateTime")] System.Nullable<System.DateTime> created_On, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="Company_Code", DbType="VarChar(10)")] string company_Code, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="Start_Date", DbType="Date")] System.Nullable<System.DateTime> start_Date, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="End_Date", DbType="Date")] System.Nullable<System.DateTime> end_Date, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="Updated_On", DbType="Date")] System.Nullable<System.DateTime> updated_On, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="Updated_By", DbType="VarChar(10)")] string updated_By, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="WBS_EXTNID", DbType="VarChar(MAX)")] string wBS_EXTNID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), pOSID, pOST1, pSPHI, vERNR, vERNA, pBUKR, pOSKI, pSPNR, created_By, created_On, company_Code, start_Date, end_Date, updated_On, updated_By, wBS_EXTNID);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_master_uploadExcell_master_TCURR_CM")]
		public int usp_master_uploadExcell_master_TCURR_CM([global::System.Data.Linq.Mapping.ParameterAttribute(Name="FCURR", DbType="VarChar(5)")] string fCURR, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TCURR", DbType="VarChar(5)")] string tCURR, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="UKURS", DbType="Decimal(10,5)")] System.Nullable<decimal> uKURS)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), fCURR, tCURR, uKURS);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_master_uploadExcell_master_TCURT_CM")]
		public int usp_master_uploadExcell_master_TCURT_CM([global::System.Data.Linq.Mapping.ParameterAttribute(Name="WAERS", DbType="VarChar(5)")] string wAERS, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="LTEXT", DbType="VarChar(40)")] string lTEXT)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), wAERS, lTEXT);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_master_uploadExcell_master_T005T_Travel")]
		public int usp_master_uploadExcell_master_T005T_Travel([global::System.Data.Linq.Mapping.ParameterAttribute(Name="LAND1", DbType="Char(3)")] string lAND1, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="LANDX", DbType="Char(15)")] string lANDX, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="NATIO", DbType="Char(15)")] string nATIO)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), lAND1, lANDX, nATIO);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_master_uploadExcell_master_T706O_Travel")]
		public int usp_master_uploadExcell_master_T706O_Travel([global::System.Data.Linq.Mapping.ParameterAttribute(Name="LAND1", DbType="VarChar(5)")] string lAND1, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="RGION", DbType="VarChar(50)")] string rGION, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TEXT25", DbType="VarChar(50)")] string tEXT25)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), lAND1, rGION, tEXT25);
			return ((int)(result.ReturnValue));
		}
	}
}
#pragma warning restore 1591
