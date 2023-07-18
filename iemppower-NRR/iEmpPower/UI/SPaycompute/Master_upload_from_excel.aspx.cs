using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Configuration;
using iEmpPowerMaster_Load;
using iEmpPower.Old_App_Code.iEmpPowerBO.PR_Masterupload;
using  iEmpPower.Old_App_Code.iEmpPowerBL.PR_MasteruploadBL;


namespace iEmpPower.UI.SPaycompute
{
    public partial class Master_upload_from_excel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                view1.Visible = true;
                Tab1.CssClass = "nav-link active p-2";
            }

        }

        protected void Tab1_Click(object sender, EventArgs e)
        {
            HideTabs();
            view1.Visible = true;
            view2.Visible = false;
            view3.Visible = false;
            view4.Visible = false;
            view5.Visible = false;
            Tab1.CssClass = "nav-link active p-2";
            
        }

        protected void HideTabs()
        {
            //lblmssgpi.Text = "";
            //lblbif.Text = "";
            //lblainfo.Text = "";
            //lblbinfo.Text = "";
            view1.Visible = false;
            view2.Visible = false;
            view3.Visible = false;
            view4.Visible = false;
            view5.Visible = false;
            Tab4.CssClass = "nav-link  p-2";
            Tab5.CssClass = "nav-link  p-2";
            Tab1.CssClass = "nav-link  p-2";
            Tab2.CssClass = "nav-link  p-2";
            Tab3.CssClass = "nav-link  p-2";

        }
        protected void btnUploadPRData_Click(object sender, EventArgs e)
        {
            try
            {
                //string path = Server.MapPath("~/MyFolder/" + uflPRData.FileName);
                //uflPRData.SaveAs(Server.MapPath("~/PayCompute_Files/PR_Info/" + uflPRData.FileName));
                //string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(Server.MapPath("~/Uploads/" + uflPRData.FileName));
                string excelPath = Server.MapPath("~/PayCompute_Files/PR_Info/" + User.Identity.Name + "-" + (uflPRData.FileName) + "-" + DateTime.Now.ToString("yyyy_MM_dd") + Path.GetExtension(uflPRData.FileName));
                //Server.MapPath("~/PayCompute_Files/Emp_info/") + Path.GetFileName(uflEmpData.PostedFile.FileName);
                uflPRData.SaveAs(excelPath);


                string conString = string.Empty;
                string extension = Path.GetExtension(uflPRData.PostedFile.FileName);
                switch (extension)
                {
                    case ".xls": //Excel 97-03
                        conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07 or higher
                        conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                        break;
                }

                conString = string.Format(conString, excelPath);
                using (OleDbConnection excel_con = new OleDbConnection(conString))
                {
                    excel_con.Open();
                    // string PR = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString().Trim();
                    DataTable dtExcelData = PRDt();


                    //using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT CID ,C_DESC ,A_DESC , B_DESC , EKGRP , EKNAM , WERKS , NAME1 ,SPART ,VTEXT , REGION_ID ,REGION_TEXT,ISOCODE,ISOTXT,POSID,POST1,PSPHI,VERNR,VERNA,PBUKR,POSKI,ZZDEL_HEAD,ZZDEL_HEADNAME,ZZPERNR01,ZZENAME01,ZZROLE01,,ZZPERNR02,ZZENAME02,ZZROLE02,ZZPERNR03,ZZENAME03,ZZROLE03,ZZPERNR04,ZZENAME04,ZZROLE0,ZZPERNR05,ZZENAME05,ZZROLE05,ZZPERNR06,ZZENAME06,ZZROLE06,ZZPERNR07,ZZENAME07,ZZROLE07,STAT,Created_By,Created_on,Company_Code,Start_Date,End_Date,Updated_On,Updated_By,WBS_EXTNID,PSPNR FROM  [PR_Info$] ", excel_con))
                    //using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT CID ,C_DESC, A_DESC , B_DESC , EKGRP,EKNAM, WERKS , NAME1 ,SPART , VTEXT ,REGION_ID , REGION_TEXT , ISOCODE , ISOTXT , POSID ,POST1 , PSPHI , VERNR, VERNA, PBUKR, POSKI , ZZDEL_HEAD , ZZDEL_HEADNAME, ZZPERNR01 , ZZENAME01, ZZROLE01, ZZPERNR02, ZZENAME02 , ZZROLE02 , ZZPERNR03, ZZENAME03 ,ZZROLE03, ZZPERNR04 ,ZZENAME04 ,ZZROLE04 , ZZPERNR05 , ZZENAME05 , ZZROLE05 , ZZPERNR06 , ZZENAME06 , ZZROLE06 , ZZPERNR07 ,ZZENAME07 ,ZZENAME07 ,  ZZROLE07 , STAT , Created_By , Created_on , Company_Code , Start_Date , End_Date , Updated_On , Updated_By ,  WBS_EXTNID , PSPNR FROM  [PR_Info] ", excel_con))
                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT CID ,C_DESC, A_DESC , B_DESC  FROM  [master.ZMM_MIS$] ", excel_con))
                    {
                        oda.Fill(dtExcelData);
                        string path1 = Server.MapPath("~/MyFolder/" + uflPRData.FileName);
                        for (int i = dtExcelData.Rows.Count - 1; i >= 0; i += -1)
                        {
                            DataRow row = dtExcelData.Rows[i];
                            if (row[0] == null)
                            {
                                dtExcelData.Rows.Remove(row);
                            }
                            else if (string.IsNullOrEmpty(row[0].ToString().Trim()))
                            {
                                dtExcelData.Rows.Remove(row);
                            }
                        }
                        dtExcelData.AcceptChanges();
                    }

                    DataTable dtExcelData1 = Reqtr_Region_TypesDt();

                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT EKGRP,EKNAM FROM [master.T024$] ", excel_con))
                    {
                        oda.Fill(dtExcelData1);
                        for (int i = dtExcelData1.Rows.Count - 1; i >= 0; i += -1)
                        {
                            DataRow row = dtExcelData1.Rows[i];
                            if (row[0] == null)
                            {
                                dtExcelData1.Rows.Remove(row);
                            }
                            else if (string.IsNullOrEmpty(row[0].ToString().Trim()))
                            {
                                dtExcelData1.Rows.Remove(row);
                            }
                        }
                        dtExcelData1.AcceptChanges();
                    }


                    DataTable dtExcelData2 = Bill_to_address_Dt();
                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT WERKS,NAME1 FROM [master.T001W$] ", excel_con))
                    {
                        oda.Fill(dtExcelData2);
                        for (int i = dtExcelData2.Rows.Count - 1; i >= 0; i += -1)
                        {
                            DataRow row = dtExcelData2.Rows[i];
                            if (row[0] == null)
                            {
                                dtExcelData2.Rows.Remove(row);
                            }
                            else if (string.IsNullOrEmpty(row[0].ToString().Trim()))
                            {
                                dtExcelData2.Rows.Remove(row);
                            }
                        }
                        dtExcelData2.AcceptChanges();

                    }

                    //DataTable dtExcelData3 = ERP_Code_Dt();
                    //using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT  POSID,POST1,PSPHI,VERNR,VERNA,PBUKR,POSKI,ZZDEL_HEAD,ZZDEL_HEADNAME,ZZPERNR01,ZZENAME01,ZZROLE01,ZZPERNR02,ZZENAME02,ZZROLE02,ZZPERNR03,ZZENAME03,ZZROLE03, ZZPERNR04,ZZENAME04,ZZROLE04,ZZPERNR05,ZZENAME05,ZZROLE05,ZZPERNR06,ZZENAME06,ZZROLE06,ZZPERNR07,ZZENAME07,ZZROLE07,STAT,Created_By,Created_on,Company_Code,Start_Date,End_Date,Updated_On,Updated_By,WBS_EXTNID,PSPNR from[master.pro]", excel_con))
                    //    //,PSPHI,VERNR,VERNA,PBUKR,POSKI,ZZDEL_HEAD,ZZDEL_HEADNAME,ZZPERNR01,ZZENAME01,ZZROLE01,ZZPERNR02,ZZENAME02,ZZROLE02,ZZPERNR03,ZZENAME03,ZZROLE03,ZZPERNR04,ZZENAME04,ZZROLE04,ZZPERNR05,ZZENAME05,ZZROLE05,ZZPERNR06,ZZENAME06,ZZROLE06,ZZPERNR07,ZZENAME07,ZZROLE07,STAT,Created_By,Created_on,Company_Code,Start_Date,End_Date,Updated_On,Updated_By,WBS_EXTNID,PSPNR FROM [master.PRPS] ", excel_con))
                    //{
                    //    oda.Fill(dtExcelData3);
                    //    for (int i = dtExcelData3.Rows.Count - 1; i >= 0; i += -1)
                    //    {
                    //        DataRow row = dtExcelData3.Rows[i];
                    //        if (row[0] == null)
                    //        {
                    //            dtExcelData3.Rows.Remove(row);
                    //        }
                    //        else if (string.IsNullOrEmpty(row[0].ToString().Trim()))
                    //        {
                    //            dtExcelData3.Rows.Remove(row);
                    //        }
                    //    }
                    //    dtExcelData3.AcceptChanges();

                    //}

                    DataTable dtExcelData4 = Business_Unit_Dt();

                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT SPART,VTEXT FROM [master.TSPAT$]", excel_con))
                    {
                        oda.Fill(dtExcelData4);
                        for (int i = dtExcelData4.Rows.Count - 1; i >= 0; i += -1)
                        {
                            DataRow row = dtExcelData4.Rows[i];
                            if (row[0] == null)
                            {
                                dtExcelData4.Rows.Remove(row);
                            }
                            else if (string.IsNullOrEmpty(row[0].ToString().Trim()))
                            {
                                dtExcelData4.Rows.Remove(row);
                            }
                        }
                        dtExcelData4.AcceptChanges();
                    }

                    DataTable dtExcelData5 = RegionDt();

                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT REGION_ID,REGION_TEXT FROM [master.ZMM_PR_REGION$] ", excel_con))
                    {
                        oda.Fill(dtExcelData5);
                        for (int i = dtExcelData5.Rows.Count - 1; i >= 0; i += -1)
                        {
                            DataRow row = dtExcelData5.Rows[i];
                            if (row[0] == null)
                            {
                                dtExcelData5.Rows.Remove(row);
                            }
                            else if (string.IsNullOrEmpty(row[0].ToString().Trim()))
                            {
                                dtExcelData5.Rows.Remove(row);
                            }
                        }
                        dtExcelData5.AcceptChanges();
                    }


                    DataTable dtExcelData6 = UOM_Dt();
                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT ISOCODE,ISOTXT FROM [master.T006J$] ", excel_con))
                    {
                        oda.Fill(dtExcelData6);
                        for (int i = dtExcelData6.Rows.Count - 1; i >= 0; i += -1)
                        {
                            DataRow row = dtExcelData6.Rows[i];
                            if (row[0] == null)
                            {
                                dtExcelData6.Rows.Remove(row);
                            }
                            else if (string.IsNullOrEmpty(row[0].ToString().Trim()))
                            {
                                dtExcelData6.Rows.Remove(row);
                            }
                        }
                        dtExcelData6.AcceptChanges();
                    }


                    excel_con.Close();


                    GV_ZMM_MIS.DataSource = dtExcelData;
                    GV_ZMM_MIS.DataBind();

                    gv_Reqtr_Region.DataSource = dtExcelData1;
                    gv_Reqtr_Region.DataBind();

                    GV_Bill_to_address.DataSource = dtExcelData2;
                    GV_Bill_to_address.DataBind();


                    //GV_ERP_Code.DataSource = dtExcelData3;
                    //GV_ERP_Code.DataBind();

                    GV_Business_Unit.DataSource = dtExcelData4;
                    GV_Business_Unit.DataBind();

                    GV_Region.DataSource = dtExcelData5;
                    GV_Region.DataBind();

                    GV_UOM.DataSource = dtExcelData6;
                    GV_UOM.DataBind();
                    divgrds.Visible = true;
                    btnSave.Visible = true;
                    btnClear.Visible = true;
                    ViewState["PR"] = dtExcelData;
                    ViewState["Region_TypesDt"] = dtExcelData1;
                    ViewState["Bill_to_address"] = dtExcelData2;
                    ViewState["Business_Unit"] = dtExcelData4;
                    ViewState["RegionDt"] = dtExcelData5;
                    ViewState["UOM_Dt"] = dtExcelData6;
                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "')", true);
            }
        }


        protected DataTable PRDt()
        {
            DataTable dtExcelData = new DataTable();

            dtExcelData.Columns.AddRange(new DataColumn[4]

                    { 
                        new DataColumn("CID", typeof(string)),
                        new DataColumn("C_DESC", typeof(string)),
                        new DataColumn("A_DESC", typeof(string)),
                        new DataColumn("B_DESC", typeof(string)),
              });

            return dtExcelData;
        }
        protected DataTable Reqtr_Region_TypesDt()
        {
            DataTable dtExcelData1 = new DataTable();
            dtExcelData1.Columns.AddRange(new DataColumn[2]
                    { 
                     new DataColumn("EKGRP",typeof(string)),
                     new DataColumn("EKNAM",typeof(string)),
                    
                     });
            return dtExcelData1;
        }

        protected DataTable Bill_to_address_Dt()
        {
            DataTable dtExcelData2 = new DataTable();
            dtExcelData2.Columns.AddRange(new DataColumn[2]
                    { 
                     new DataColumn("WERKS",typeof(string)),
                     new DataColumn("NAME1",typeof(string)),
                    
                     });

            return dtExcelData2;
        }

        //protected DataTable ERP_Code_Dt()
        //{
        //    DataTable dtExcelData3 = new DataTable();

        //    dtExcelData3.Columns.AddRange(new DataColumn[40]

        //            { 

        //                 new DataColumn("POSID",typeof(string)),
        //                 new DataColumn("POST1",typeof(string)),
        //                 new DataColumn("PSPHI",typeof(string)),
        //                 new DataColumn("VERNR",typeof(string)),
        //                 new DataColumn("VERNA",typeof(string)),
        //                 new DataColumn("PBUKR",typeof(string)),
        //                 new DataColumn("POSKI",typeof(string)),
        //                 new DataColumn("ZZDEL_HEAD",typeof(string)),
        //                 new DataColumn("ZZDEL_HEADNAME",typeof(string)),
        //                 new DataColumn("ZZPERNR01",typeof(string)),
        //                 new DataColumn("ZZENAME01",typeof(string)),
        //                 new DataColumn("ZZROLE01",typeof(string)),
        //                 new DataColumn("ZZPERNR02",typeof(string)),
        //                 new DataColumn("ZZENAME02",typeof(string)),
        //                 new DataColumn("ZZROLE02",typeof(string)),
        //                 new DataColumn("ZZPERNR03",typeof(string)),
        //                 new DataColumn("ZZENAME03",typeof(string)),
        //                 new DataColumn("ZZROLE03",typeof(string)),
        //                 new DataColumn("ZZPERNR04",typeof(string)),
        //                 new DataColumn("ZZENAME04",typeof(string)),
        //                 new DataColumn("ZZROLE04",typeof(string)),
        //                 new DataColumn("ZZPERNR05",typeof(string)),
        //                 new DataColumn("ZZENAME05",typeof(string)),
        //                 new DataColumn("ZZROLE05",typeof(string)),
        //                 new DataColumn("ZZPERNR06",typeof(string)),
        //                 new DataColumn("ZZENAME06",typeof(string)),
        //                 new DataColumn("ZZROLE06",typeof(string)),
        //                 new DataColumn("ZZPERNR07",typeof(string)),
        //                 new DataColumn("ZZENAME07",typeof(string)),
        //                 new DataColumn("ZZROLE07",typeof(string)),
        //                 new DataColumn("STAT",typeof(string)),
        //                 new DataColumn("Created_By",typeof(string)),
        //                 new DataColumn("Created_on",typeof(string)),
        //                 new DataColumn("Company_Code",typeof(string)),
        //                 new DataColumn("Start_Date",typeof(string)),
        //                 new DataColumn("End_Date",typeof(string)),
        //                 new DataColumn("Updated_On",typeof(string)),
        //                 new DataColumn("Updated_By",typeof(string)),
        //                 new DataColumn("WBS_EXTNID",typeof(string)),
        //                 new DataColumn("PSPNR",typeof(string)),





        //             });

        //    return dtExcelData3;
        //}

        protected DataTable Business_Unit_Dt()
        {
            DataTable dtExcelData4 = new DataTable();
            dtExcelData4.Columns.AddRange(new DataColumn[2]
                    { 
                     new DataColumn("SPART",typeof(string)),
                     new DataColumn("VTEXT",typeof(string)),
                    
                     });
            return dtExcelData4;
        }

        protected DataTable RegionDt()
        {
            DataTable dtExcelData5 = new DataTable();
            dtExcelData5.Columns.AddRange(new DataColumn[2]
                    { 
                     new DataColumn("REGION_ID",typeof(string)),
                     new DataColumn("REGION_TEXT",typeof(string)),
                   
                     });
            return dtExcelData5;
        }

        protected DataTable UOM_Dt()
        {
            DataTable dtExcelData6 = new DataTable();
            dtExcelData6.Columns.AddRange(new DataColumn[1]
                    { 
                     new DataColumn("ISOTXT",typeof(string)),
                     
                     });

            return dtExcelData6;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtExcelData = PRDt();
                PR_ExcelUploadBO objempBo = new PR_ExcelUploadBO();
                PR_EXCELCollBL objBl = new PR_EXCELCollBL();
                bool? st = false;
                bool? st1 = false;
                bool? st2 = false;
                using (dtExcelData = (DataTable)(ViewState["PR"]))
                {
                    if (dtExcelData.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtExcelData.Rows.Count; i++)
                        {
                            objempBo.CID = dtExcelData.Rows[i]["CID"].ToString();
                            // objempBo.Employee_ID = Session["CompCode"].ToString() + dtExcelData.Rows[i]["Employee_ID"].ToString().Trim().Trim().ToLower();
                            objempBo.C_DESC = dtExcelData.Rows[i]["C_DESC"].ToString().Trim();
                            objempBo.A_DESC = dtExcelData.Rows[i]["A_DESC"].ToString().Trim();
                            objempBo.B_DESC = dtExcelData.Rows[i]["B_DESC"].ToString().Trim();
                            objBl.Create_ZMM_MIS_PR(objempBo);
                        }
                    }
                }

                        PR_ExcelUploadBO bo2 = new PR_ExcelUploadBO();
                        DataTable dt2 = new DataTable();

                        using (dt2 = (DataTable)ViewState["Region_TypesDt"])
                        {
                            if (dt2.Rows.Count > 0)
                            {
                                for (int j = 0; j < dt2.Rows.Count; j++)
                                {
                                    //bo2.compCode = Session["CompCode"].ToString();
                                    //bo2.Employee_ID = Session["CompCode"].ToString() + dt2.Rows[j]["Employee_ID"].ToString().Trim().ToLower();
                                    bo2.EKGRP = dt2.Rows[j]["EKGRP"].ToString().Trim();
                                    bo2.EKNAM = dt2.Rows[j]["EKNAM"].ToString().Trim();

                                    objBl.Create_master_T024_PR(bo2);
                                }
                            }
                        }

                        PR_ExcelUploadBO bo3 = new PR_ExcelUploadBO();
                        DataTable dt3 = new DataTable();

                        using (dt3 = (DataTable)ViewState["Bill_to_address"])
                        {
                            if (dt3.Rows.Count > 0)
                            {
                                for (int j = 0; j < dt3.Rows.Count; j++)
                                {
                                    //bo2.compCode = Session["CompCode"].ToString();
                                    //bo2.Employee_ID = Session["CompCode"].ToString() + dt2.Rows[j]["Employee_ID"].ToString().Trim().ToLower();
                                    bo3.WERKS = dt3.Rows[j]["WERKS"].ToString().Trim();
                                    bo3.NAME1 = dt3.Rows[j]["NAME1"].ToString().Trim();

                                    objBl.Create_master_T001W_PR(bo3);
                                }
                            }
                        }

                        PR_ExcelUploadBO bo4 = new PR_ExcelUploadBO();
                        DataTable dtExcelData4 = new DataTable();

                        using (dtExcelData4 = (DataTable)ViewState["Business_Unit"])
                        {
                            if (dtExcelData4.Rows.Count > 0)
                            {
                                for (int j = 0; j < dtExcelData4.Rows.Count; j++)
                                {
                                    //bo2.compCode = Session["CompCode"].ToString();
                                    //bo2.Employee_ID = Session["CompCode"].ToString() + dt2.Rows[j]["Employee_ID"].ToString().Trim().ToLower();
                                    bo4.SPART = dtExcelData4.Rows[j]["SPART"].ToString().Trim();
                                    bo4.VTEXT = dtExcelData4.Rows[j]["VTEXT"].ToString().Trim();

                                    objBl.Create_master_TSPAT_PR(bo4);
                                }
                           }
                    }


                        PR_ExcelUploadBO bo5 = new PR_ExcelUploadBO();
                        DataTable dtExcelData5 = new DataTable();

                        using (dtExcelData5 = (DataTable)ViewState["RegionDt"])
                        {
                            if (dtExcelData5.Rows.Count > 0)
                            {
                                for (int j = 0; j < dtExcelData5.Rows.Count; j++)
                                {
                                    //bo2.compCode = Session["CompCode"].ToString();
                                    //bo2.Employee_ID = Session["CompCode"].ToString() + dt2.Rows[j]["Employee_ID"].ToString().Trim().ToLower();
                                    bo5.REGION_ID = dtExcelData5.Rows[j]["REGION_ID"].ToString().Trim();
                                    bo5.REGION_TEXT = dtExcelData5.Rows[j]["REGION_TEXT"].ToString().Trim();

                                    objBl.Create_master_ZMM_PR_REGION(bo5);
                                }
                            }
                        }

                        PR_ExcelUploadBO bo6 = new PR_ExcelUploadBO();
                        DataTable dtExcelData6 = new DataTable();

                        using (dtExcelData6 = (DataTable)ViewState["UOM_Dt"])
                        {
                            if (dtExcelData6.Rows.Count > 0)
                            {
                                for (int j = 0; j < dtExcelData6.Rows.Count; j++)
                                {
                                    //bo2.compCode = Session["CompCode"].ToString();
                                    //bo2.Employee_ID = Session["CompCode"].ToString() + dt2.Rows[j]["Employee_ID"].ToString().Trim().ToLower();
                                    bo6.ISOCODE = dtExcelData6.Rows[j]["ISOCODE"].ToString().Trim();
                                    bo6.ISOTXT = dtExcelData6.Rows[j]["ISOTXT"].ToString().Trim();

                                    objBl.Create_master_T006J_PR(bo6);
                                }
                            }
                        }
                //    }
                //}


            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "')", true); }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

        }

        protected void Tab2_Click(object sender, EventArgs e)
        {
             HideTabs();
            view1.Visible = false;
            view2.Visible = true;
            Tab2.CssClass = "nav-link active p-2";
            Tab1.CssClass = "";
            view3.Visible = false;
            Tab3.CssClass = "";
            view4.Visible = false;
            Tab4.CssClass = "";
        }


        protected void btnUploadiexpenseData_Click(object sender, EventArgs e)
        {
     
            try
            {
                //string path = Server.MapPath("~/MyFolder/" + uflPRData.FileName);
                //uflPRData.SaveAs(Server.MapPath("~/PayCompute_Files/PR_Info/" + uflPRData.FileName));
                //string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(Server.MapPath("~/Uploads/" + uflPRData.FileName));
                string excelPath = Server.MapPath("~/PayCompute_Files/Iexpense_info/" + User.Identity.Name + "-" + (FileUploadiexpense.FileName) + "-" + DateTime.Now.ToString("yyyy_MM_dd") + Path.GetExtension(FileUploadiexpense.FileName));
                //Server.MapPath("~/PayCompute_Files/Emp_info/") + Path.GetFileName(uflEmpData.PostedFile.FileName);
                FileUploadiexpense.SaveAs(excelPath);


                string conString = string.Empty;
                string extension = Path.GetExtension(FileUploadiexpense.PostedFile.FileName);
                switch (extension)
                {
                    case ".xls": //Excel 97-03
                        conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07 or higher
                        conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                        break;
                }

                conString = string.Format(conString, excelPath);
                using (OleDbConnection excel_con = new OleDbConnection(conString))
                {
                    excel_con.Open();
                    // string PR = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString().Trim();
                    DataTable dtExcelDataiexp = IexpenseDt();


                    //using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT CID ,C_DESC ,A_DESC , B_DESC , EKGRP , EKNAM , WERKS , NAME1 ,SPART ,VTEXT , REGION_ID ,REGION_TEXT,ISOCODE,ISOTXT,POSID,POST1,PSPHI,VERNR,VERNA,PBUKR,POSKI,ZZDEL_HEAD,ZZDEL_HEADNAME,ZZPERNR01,ZZENAME01,ZZROLE01,,ZZPERNR02,ZZENAME02,ZZROLE02,ZZPERNR03,ZZENAME03,ZZROLE03,ZZPERNR04,ZZENAME04,ZZROLE0,ZZPERNR05,ZZENAME05,ZZROLE05,ZZPERNR06,ZZENAME06,ZZROLE06,ZZPERNR07,ZZENAME07,ZZROLE07,STAT,Created_By,Created_on,Company_Code,Start_Date,End_Date,Updated_On,Updated_By,WBS_EXTNID,PSPNR FROM  [PR_Info$] ", excel_con))
                    //using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT CID ,C_DESC, A_DESC , B_DESC , EKGRP,EKNAM, WERKS , NAME1 ,SPART , VTEXT ,REGION_ID , REGION_TEXT , ISOCODE , ISOTXT , POSID ,POST1 , PSPHI , VERNR, VERNA, PBUKR, POSKI , ZZDEL_HEAD , ZZDEL_HEADNAME, ZZPERNR01 , ZZENAME01, ZZROLE01, ZZPERNR02, ZZENAME02 , ZZROLE02 , ZZPERNR03, ZZENAME03 ,ZZROLE03, ZZPERNR04 ,ZZENAME04 ,ZZROLE04 , ZZPERNR05 , ZZENAME05 , ZZROLE05 , ZZPERNR06 , ZZENAME06 , ZZROLE06 , ZZPERNR07 ,ZZENAME07 ,ZZENAME07 ,  ZZROLE07 , STAT , Created_By , Created_on , Company_Code , Start_Date , End_Date , Updated_On , Updated_By ,  WBS_EXTNID , PSPNR FROM  [PR_Info] ", excel_con))
                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT LGART ,LGTXT  FROM  [master.T512T$] ", excel_con))
                    {
                        oda.Fill(dtExcelDataiexp);
                        string path1 = Server.MapPath("~/MyFolder/" + FileUploadiexpense.FileName);
                        for (int i = dtExcelDataiexp.Rows.Count - 1; i >= 0; i += -1)
                        {
                            DataRow row = dtExcelDataiexp.Rows[i];
                            if (row[0] == null)
                            {
                                dtExcelDataiexp.Rows.Remove(row);
                            }
                            else if (string.IsNullOrEmpty(row[0].ToString().Trim()))
                            {
                                dtExcelDataiexp.Rows.Remove(row);
                            }
                        }
                        dtExcelDataiexp.AcceptChanges();
                    }

                   
                   
                   

                    

                    excel_con.Close();


                    grdiexpense.DataSource = dtExcelDataiexp;
                    grdiexpense.DataBind();

                 
                    divgrds.Visible = false;
                    divgrdsiexpense.Visible = true;
                    btnSaveIexpense.Visible = true;
                    btnCleaIexpense.Visible = true;
                    btnUploadPRData.Visible = false;
                    view1.Visible = false;
                    //Tab2.CssClass = "nav-link active p-2";
                    ViewState["Iexpense"] = dtExcelDataiexp;
                   
                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "')", true);
            }
        }
        protected DataTable IexpenseDt()
        {
            DataTable dtExcelDataiexp = new DataTable();

            dtExcelDataiexp.Columns.AddRange(new DataColumn[2]

                    { 
                        new DataColumn("LGART", typeof(string)),
                        new DataColumn("LGTXT", typeof(string)),
                       
              });

            return dtExcelDataiexp;
        }

        protected void btnSaveIexpense_Click(object sender, EventArgs e)
        {

     
            try
            {
                DataTable dtExcelData = IexpenseDt();
                PR_ExcelUploadBO objempBo = new PR_ExcelUploadBO();
                PR_EXCELCollBL objBl = new PR_EXCELCollBL();

                using (dtExcelData = (DataTable)(ViewState["Iexpense"]))
                {
                    if (dtExcelData.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtExcelData.Rows.Count; i++)
                        {
                            objempBo.LGART = dtExcelData.Rows[i]["LGART"].ToString();
                            // objempBo.Employee_ID = Session["CompCode"].ToString() + dtExcelData.Rows[i]["Employee_ID"].ToString().Trim().Trim().ToLower();
                            objempBo.LGTXT = dtExcelData.Rows[i]["LGTXT"].ToString().Trim();

                            objBl.Create_master_T512T_Iexpense(objempBo);
                        }
                    }
                }

                       
                       

                       
                


            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "')", true); }
        }

        protected void btnCleaIexpense_Click(object sender, EventArgs e)
        {

        }

        protected void Tab3_Click(object sender, EventArgs e)
        {
            HideTabs();
            view1.Visible = false;
            view2.Visible = false;
            view3.Visible = true;
            Tab3.CssClass = "nav-link active p-2";
            view4.Visible =false;
            Tab4.CssClass = "";
        }

        protected void btnUploadFBPData_Click(object sender, EventArgs e)
        {
            try
            {

                string excelPath = Server.MapPath("~/PayCompute_Files/FBP_Info/" + User.Identity.Name + "-" + (upldfileFBP.FileName) + "-" + DateTime.Now.ToString("yyyy_MM_dd") + Path.GetExtension(upldfileFBP.FileName));
                //Server.MapPath("~/PayCompute_Files/Emp_info/") + Path.GetFileName(uflEmpData.PostedFile.FileName);
                upldfileFBP.SaveAs(excelPath);


                string conString = string.Empty;
                string extension = Path.GetExtension(upldfileFBP.PostedFile.FileName);
                switch (extension)
                {
                    case ".xls": //Excel 97-03
                        conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07 or higher
                        conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                        break;
                }

                conString = string.Format(conString, excelPath);
                using (OleDbConnection excel_con = new OleDbConnection(conString))
                {
                    excel_con.Open();
                    // string PR = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString().Trim();
                    DataTable dtExcelData = FBPHOADt();


                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT LGART ,LTEXT, Colid   FROM  [ESS.FBPHOA$] ", excel_con))
                    {
                        oda.Fill(dtExcelData);
                        string path1 = Server.MapPath("~/MyFolder/" + upldfileFBP.FileName);
                        for (int i = dtExcelData.Rows.Count - 1; i >= 0; i += -1)
                        {
                            DataRow row = dtExcelData.Rows[i];
                            if (row[0] == null)
                            {
                                dtExcelData.Rows.Remove(row);
                            }
                            else if (string.IsNullOrEmpty(row[0].ToString().Trim()))
                            {
                                dtExcelData.Rows.Remove(row);
                            }
                        }
                        dtExcelData.AcceptChanges();
                    }

                    DataTable dtExcelData1 = Annual_Entitlement_TypesDt();

                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT PERNR,AN_FBP FROM [MSS.PA9001$] ", excel_con))
                    {
                        oda.Fill(dtExcelData1);
                        for (int i = dtExcelData1.Rows.Count - 1; i >= 0; i += -1)
                        {
                            DataRow row = dtExcelData1.Rows[i];
                            if (row[0] == null)
                            {
                                dtExcelData1.Rows.Remove(row);
                            }
                            else if (string.IsNullOrEmpty(row[0].ToString().Trim()))
                            {
                                dtExcelData1.Rows.Remove(row);
                            }
                        }
                        dtExcelData1.AcceptChanges();
                    }


                    DataTable dtExcelData2 = master_T7INA9Dt();
                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT ALGRP,LGART,AMUNT FROM [master.T7INA9$] ", excel_con))
                    {
                        oda.Fill(dtExcelData2);
                        for (int i = dtExcelData2.Rows.Count - 1; i >= 0; i += -1)
                        {
                            DataRow row = dtExcelData2.Rows[i];
                            if (row[0] == null)
                            {
                                dtExcelData2.Rows.Remove(row);
                            }
                            else if (string.IsNullOrEmpty(row[0].ToString().Trim()))
                            {
                                dtExcelData2.Rows.Remove(row);
                            }
                        }
                        dtExcelData2.AcceptChanges();

                    }


                    DataTable dtExcelData4 = master_T7INA3();

                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT TRFAR,TRFGB,TRFGR,TRFST,ALGRP FROM [master.T7INA3$]", excel_con))
                    {
                        oda.Fill(dtExcelData4);
                        for (int i = dtExcelData4.Rows.Count - 1; i >= 0; i += -1)
                        {
                            DataRow row = dtExcelData4.Rows[i];
                            if (row[0] == null)
                            {
                                dtExcelData4.Rows.Remove(row);
                            }
                            else if (string.IsNullOrEmpty(row[0].ToString().Trim()))
                            {
                                dtExcelData4.Rows.Remove(row);
                            }
                        }
                        dtExcelData4.AcceptChanges();
                    }

                    DataTable dtExcelData5 = BasketTotal();

                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT PERNR,TRFAR,TRFGB,TRFGR,TRFST,BEGDA,ENDDA FROM [MSS.PA0008$] ", excel_con))
                    {
                        oda.Fill(dtExcelData5);
                        for (int i = dtExcelData5.Rows.Count - 1; i >= 0; i += -1)
                        {
                            DataRow row = dtExcelData5.Rows[i];
                            if (row[0] == null)
                            {
                                dtExcelData5.Rows.Remove(row);
                            }
                            else if (string.IsNullOrEmpty(row[0].ToString().Trim()))
                            {
                                dtExcelData5.Rows.Remove(row);
                            }
                        }
                        dtExcelData5.AcceptChanges();
                    }


                  


                    excel_con.Close();


                    GV_HOA.DataSource = dtExcelData;
                    GV_HOA.DataBind();

                    GV_Annual_Entitlement.DataSource = dtExcelData1;
                    GV_Annual_Entitlement.DataBind();

                    GV_master_T7INA9.DataSource = dtExcelData2;
                    GV_master_T7INA9.DataBind();
                    GV_master_T7INA3.DataSource = dtExcelData4;
                    GV_master_T7INA3.DataBind();

                    GV_BasketTotal.DataSource = dtExcelData5;
                    GV_BasketTotal.DataBind();
                    divgrdsFBP.Visible = true;
                    btnSaveFBP.Visible = true;
                    btnClearFBP.Visible = true;
                    ViewState["FBPHOADt"] = dtExcelData;
                    ViewState["Annual_Entitlement_TypesDt"] = dtExcelData1;
                    ViewState["master_T7INA9Dt"] = dtExcelData2;
                    ViewState["master_T7INA3"] = dtExcelData4;
                    ViewState["BasketTotal"] = dtExcelData5;
                    
                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "')", true);
            }
        }



        protected DataTable FBPHOADt()
        {
            DataTable dtExcelData = new DataTable();

            dtExcelData.Columns.AddRange(new DataColumn[3]

                    { 
                        new DataColumn("LGART", typeof(string)),
                        new DataColumn("LTEXT", typeof(string)),
                        new DataColumn("Colid", typeof(string)),
                       
              });

            return dtExcelData;
        }
        protected DataTable Annual_Entitlement_TypesDt()
        {
            DataTable dtExcelData1 = new DataTable();
            dtExcelData1.Columns.AddRange(new DataColumn[2]
                    { 
                     new DataColumn("PERNR",typeof(string)),
                     new DataColumn("AN_FBP",typeof(string)),
                    
                     });
            return dtExcelData1;
        }
        protected DataTable master_T7INA9Dt()
        {
            DataTable dtExcelData2 = new DataTable();

            dtExcelData2.Columns.AddRange(new DataColumn[3]

                    { 
                        new DataColumn("ALGRP", typeof(string)),
                        new DataColumn("LGART", typeof(string)),
                        new DataColumn("AMUNT", typeof(string)),
                       
              });

            return dtExcelData2;
        }

        protected DataTable master_T7INA3()
        {
            DataTable dtExcelData3 = new DataTable();

            dtExcelData3.Columns.AddRange(new DataColumn[5]

                    { 
                        new DataColumn("TRFAR", typeof(string)),
                        new DataColumn("TRFGB", typeof(string)),
                        new DataColumn("TRFGR", typeof(string)),
                         new DataColumn("TRFST", typeof(string)),
                        new DataColumn("ALGRP", typeof(string)),
                       
              });

            return dtExcelData3;
        }
        protected DataTable BasketTotal()
        {
            DataTable dtExcelData4 = new DataTable();

            dtExcelData4.Columns.AddRange(new DataColumn[7]

                    { 
                        new DataColumn("PERNR", typeof(string)),
                        new DataColumn("TRFAR", typeof(string)),
                        new DataColumn("TRFGB", typeof(string)),
                         new DataColumn("TRFGR", typeof(string)),
                        new DataColumn("TRFST", typeof(string)),
                         new DataColumn("BEGDA", typeof(string)),
                        new DataColumn("ENDDA", typeof(string)),
                       
              });

            return dtExcelData4;
        }
        protected void btnSaveFBP_Click(object sender, EventArgs e)
        {

            try
            {
                DataTable dtExcelData = FBPHOADt();
                PR_ExcelUploadBO objempBo = new PR_ExcelUploadBO();
                PR_EXCELCollBL objBl = new PR_EXCELCollBL();

                using (dtExcelData = (DataTable)(ViewState["FBPHOADt"]))
                {
                    if (dtExcelData.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtExcelData.Rows.Count; i++)
                        {
                            objempBo.LGART = dtExcelData.Rows[i]["LGART"].ToString();
                            // objempBo.Employee_ID = Session["CompCode"].ToString() + dtExcelData.Rows[i]["Employee_ID"].ToString().Trim().Trim().ToLower();
                            objempBo.LTEXT = dtExcelData.Rows[i]["LTEXT"].ToString().Trim();
                            objempBo.Colid = dtExcelData.Rows[i]["Colid"].ToString().Trim();

                            objBl.Create_ESS_FBPHOA(objempBo);
                        }
                    }
                }

                PR_ExcelUploadBO bo2 = new PR_ExcelUploadBO();
                DataTable dt2 = new DataTable();

                using (dt2 = (DataTable)ViewState["Annual_Entitlement_TypesDt"])
                {
                    if (dt2.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt2.Rows.Count; j++)
                        {
                            //bo2.compCode = Session["CompCode"].ToString();
                            //bo2.Employee_ID = Session["CompCode"].ToString() + dt2.Rows[j]["Employee_ID"].ToString().Trim().ToLower();
                            bo2.PERNR = dt2.Rows[j]["PERNR"].ToString().Trim();
                            bo2.AN_FBP = dt2.Rows[j]["AN_FBP"].ToString().Trim();

                            objBl.Create_FBP_master_PA9001(bo2);
                        }
                    }
                }

                PR_ExcelUploadBO bo3 = new PR_ExcelUploadBO();
                DataTable dt3 = new DataTable();

                using (dt3 = (DataTable)ViewState["master_T7INA9Dt"])
                {
                    if (dt3.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt3.Rows.Count; j++)
                        {
                            //bo2.compCode = Session["CompCode"].ToString();
                            //bo2.Employee_ID = Session["CompCode"].ToString() + dt2.Rows[j]["Employee_ID"].ToString().Trim().ToLower();
                            bo3.ALGRP = dt3.Rows[j]["ALGRP"].ToString().Trim();
                            bo3.LGART = dt3.Rows[j]["LGART"].ToString().Trim();
                            bo3.AMUNT = dt3.Rows[j]["AMUNT"].ToString().Trim();
                            objBl.Create_FBP_master_T7INA9(bo3);
                        }
                    }
                }

                PR_ExcelUploadBO bo4 = new PR_ExcelUploadBO();
                DataTable dtExcelData4 = new DataTable();

                using (dtExcelData4 = (DataTable)ViewState["master_T7INA3"])
                {
                    if (dtExcelData4.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtExcelData4.Rows.Count; j++)
                        {
                            //bo2.compCode = Session["CompCode"].ToString();
                            //bo2.Employee_ID = Session["CompCode"].ToString() + dt2.Rows[j]["Employee_ID"].ToString().Trim().ToLower();
                            bo4.TRFAR = dtExcelData4.Rows[j]["TRFAR"].ToString().Trim();
                            bo4.TRFGB = dtExcelData4.Rows[j]["TRFGB"].ToString().Trim();
                            bo4.TRFGR = dtExcelData4.Rows[j]["TRFGR"].ToString().Trim();
                         
                            bo4.TRFST = dtExcelData4.Rows[j]["TRFST"].ToString().Trim();
                            bo4.ALGRP = dtExcelData4.Rows[j]["ALGRP"].ToString().Trim();

                            objBl.Create_FBP_master_T7INA3(bo4);
                        }
                    }
                }


                PR_ExcelUploadBO bo5 = new PR_ExcelUploadBO();
                DataTable dtExcelData5 = new DataTable();

                using (dtExcelData5 = (DataTable)ViewState["BasketTotal"])
                {
                    if (dtExcelData5.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtExcelData5.Rows.Count; j++)
                        {
                            //bo2.compCode = Session["CompCode"].ToString();
                            //bo2.Employee_ID = Session["CompCode"].ToString() + dt2.Rows[j]["Employee_ID"].ToString().Trim().ToLower();
                            bo5.PERNR = dtExcelData5.Rows[j]["PERNR"].ToString().Trim();
                            bo5.TRFAR = dtExcelData5.Rows[j]["TRFAR"].ToString().Trim();
                            bo5.TRFGB = dtExcelData5.Rows[j]["TRFGB"].ToString().Trim();
                            bo5.TRFGR = dtExcelData5.Rows[j]["TRFGR"].ToString().Trim();
                            bo5.TRFST = dtExcelData5.Rows[j]["TRFST"].ToString().Trim();
                            bo5.BEGDA = Convert.ToDateTime(dtExcelData5.Rows[j]["BEGDA"].ToString().Trim());
                            //DateTime dateTime = (DateTime)bo5.BEGDA;
                            bo5.ENDDA = Convert.ToDateTime(dtExcelData5.Rows[j]["ENDDA"].ToString().Trim());



                            objBl.Create_FBP_MSS_PA0008(bo5);
                        }
                    }
                }

                //        PR_ExcelUploadBO bo6 = new PR_ExcelUploadBO();
                //        DataTable dtExcelData6 = new DataTable();

                //        using (dtExcelData6 = (DataTable)ViewState["UOM_Dt"])
                //        {
                //            if (dtExcelData6.Rows.Count > 0)
                //            {
                //                for (int j = 0; j < dtExcelData6.Rows.Count; j++)
                //                {
                //                    //bo2.compCode = Session["CompCode"].ToString();
                //                    //bo2.Employee_ID = Session["CompCode"].ToString() + dt2.Rows[j]["Employee_ID"].ToString().Trim().ToLower();
                //                    bo6.ISOCODE = dtExcelData6.Rows[j]["ISOCODE"].ToString().Trim();
                //                    bo6.ISOTXT = dtExcelData6.Rows[j]["ISOTXT"].ToString().Trim();

                //                    objBl.Create_master_T006J_PR(bo6);
                //                }
                //            }
                //        }
                ////    }
                ////}


            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "')", true); }
        }


        protected void btnClearFBP_Click(object sender, EventArgs e)
        {

        }

        protected void Tab4_Click(object sender, EventArgs e)
        {
            HideTabs();
           
            view4.Visible = true;
            Tab4.CssClass = "nav-link active p-2";
        }


        protected void btnUploadCMData_Click(object sender, EventArgs e)
        {
            try
            {
                string excelPath = Server.MapPath("~/PayCompute_Files/Cm_Info/" + User.Identity.Name + "-" + (upldfilecm.FileName) + "-" + DateTime.Now.ToString("yyyy_MM_dd") + Path.GetExtension(upldfilecm.FileName));
                //Server.MapPath("~/PayCompute_Files/Emp_info/") + Path.GetFileName(uflEmpData.PostedFile.FileName);
                upldfilecm.SaveAs(excelPath);


                string conString = string.Empty;
                string extension = Path.GetExtension(upldfilecm.PostedFile.FileName);
                switch (extension)
                {
                    case ".xls": //Excel 97-03
                        conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07 or higher
                        conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                        break;
                }

                conString = string.Format(conString, excelPath);
                using (OleDbConnection excel_con = new OleDbConnection(conString))
                {
                    excel_con.Open();
                    // string PR = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString().Trim();
                    DataTable dtExcelData = master_TCURRDt();


                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT FCURR ,TCURR, UKURS   FROM  [master.TCURR$] ", excel_con))
                    {
                        oda.Fill(dtExcelData);
                        string path1 = Server.MapPath("~/MyFolder/" + upldfilecm.FileName);
                        for (int i = dtExcelData.Rows.Count - 1; i >= 0; i += -1)
                        {
                            DataRow row = dtExcelData.Rows[i];
                            if (row[0] == null)
                            {
                                dtExcelData.Rows.Remove(row);
                            }
                            else if (string.IsNullOrEmpty(row[0].ToString().Trim()))
                            {
                                dtExcelData.Rows.Remove(row);
                            }
                        }
                        dtExcelData.AcceptChanges();
                    }

                    DataTable dtExcelData1 = master_TCURT_TypesDt();

                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT WAERS,LTEXT FROM [master.TCURT$] ", excel_con))
                    {
                        oda.Fill(dtExcelData1);
                        for (int i = dtExcelData1.Rows.Count - 1; i >= 0; i += -1)
                        {
                            DataRow row = dtExcelData1.Rows[i];
                            if (row[0] == null)
                            {
                                dtExcelData1.Rows.Remove(row);
                            }
                            else if (string.IsNullOrEmpty(row[0].ToString().Trim()))
                            {
                                dtExcelData1.Rows.Remove(row);
                            }
                        }
                        dtExcelData1.AcceptChanges();
                    }

                    DataTable dtExcelData4 = master_PRPS_Dt();


                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT POSID,POST1,PSPHI,VERNR,VERNA,PBUKR,POSKI,PSPNR,Created_By,Created_On,Company_Code,Start_Date,End_Date,Updated_On, Updated_By,WBS_EXTNID FROM  [master.PRPS$]", excel_con))
                    {
                        oda.Fill(dtExcelData4);
                        for (int i = dtExcelData4.Rows.Count - 1; i >= 0; i += -1)
                        {
                            DataRow row = dtExcelData4.Rows[i];
                            if (row[0] == null)
                            {
                                dtExcelData4.Rows.Remove(row);
                            }
                            else if (string.IsNullOrEmpty(row[0].ToString().Trim()))
                            {
                                dtExcelData4.Rows.Remove(row);
                            }
                        }
                        dtExcelData4.AcceptChanges();
                    }


                    DataTable dtExcelData2 = CountryDt();


                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT LAND1 ,LANDX, NATIO   FROM  [master.T005T$] ", excel_con))
                    {
                        oda.Fill(dtExcelData2);
                        string path1 = Server.MapPath("~/MyFolder/" + upldfilecm.FileName);
                        for (int i = dtExcelData2.Rows.Count - 1; i >= 0; i += -1)
                        {
                            DataRow row = dtExcelData2.Rows[i];
                            if (row[0] == null)
                            {
                                dtExcelData2.Rows.Remove(row);
                            }
                            else if (string.IsNullOrEmpty(row[0].ToString().Trim()))
                            {
                                dtExcelData2.Rows.Remove(row);
                            }
                        }
                        dtExcelData2.AcceptChanges();
                    }

                    DataTable dtExcelData3 = TravelRegionDt();

                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT LAND1,RGION,TEXT25 FROM [master.T706O$]", excel_con))
                    {
                        oda.Fill(dtExcelData3);
                        for (int i = dtExcelData3.Rows.Count - 1; i >= 0; i += -1)
                        {
                            DataRow row = dtExcelData3.Rows[i];
                            if (row[0] == null)
                            {
                                dtExcelData3.Rows.Remove(row);
                            }
                            else if (string.IsNullOrEmpty(row[0].ToString().Trim()))
                            {
                                dtExcelData3.Rows.Remove(row);
                            }
                        }
                        dtExcelData3.AcceptChanges();
                    }


                  


                    excel_con.Close();


                    GV_TCURR.DataSource = dtExcelData;
                    GV_TCURR.DataBind();

                    GV_TCURT.DataSource = dtExcelData1;
                    GV_TCURT.DataBind();

                    GV_PRPS.DataSource = dtExcelData4;
                    GV_PRPS.DataBind();
                    GV_Country.DataSource = dtExcelData2;
                    GV_Country.DataBind();

                    GV_trvlRegion.DataSource = dtExcelData3;
                    GV_trvlRegion.DataBind();

                   
                    divgrds.Visible = false;
                    divgrdsCM.Visible = true;
                    btnSaveCM.Visible = true;
                    btnClearCM.Visible = true;
                    //btnUploadPRData.Visible = false;
                    //view1.Visible = false;
                    ViewState["master_TCURRDt"] = dtExcelData;
                    ViewState["master_TCURT_TypesDt"] = dtExcelData1;
                    ViewState["master_PRPS_Dt"] = dtExcelData4;
                    ViewState["CountryDt"] = dtExcelData2;
                    ViewState["TravelRegionDt"] = dtExcelData3;
                   
                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "')", true);
            }
        }

        protected DataTable master_TCURRDt()
        {
            DataTable dtExcelData = new DataTable();

            dtExcelData.Columns.AddRange(new DataColumn[3]

                    { 
                        new DataColumn("FCURR", typeof(string)),
                        new DataColumn("TCURR", typeof(string)),
                        new DataColumn("UKURS", typeof(Decimal)),
                       
              });

            return dtExcelData;
        }
        protected DataTable master_TCURT_TypesDt()
        {
            DataTable dtExcelData1 = new DataTable();
            dtExcelData1.Columns.AddRange(new DataColumn[2]
                    { 
                     new DataColumn("WAERS",typeof(string)),
                     new DataColumn("LTEXT",typeof(string)),
                    
                     });
            return dtExcelData1;
        }

        protected DataTable master_PRPS_Dt()
        {
            DataTable dtExcelData4 = new DataTable();

            dtExcelData4.Columns.AddRange(new DataColumn[16]

                    { 

                         new DataColumn("POSID",typeof(string)),
                         new DataColumn("POST1",typeof(string)),
                         new DataColumn("PSPHI",typeof(string)),
                         new DataColumn("VERNR",typeof(string)),
                         new DataColumn("VERNA",typeof(string)),
                         new DataColumn("PBUKR",typeof(string)),
                         new DataColumn("POSKI",typeof(string)),
                            new DataColumn("PSPNR",typeof(string)),

                         new DataColumn("Created_By",typeof(string)),
                         new DataColumn("Created_on",typeof(string)),
                         new DataColumn("Company_Code",typeof(string)),
                         new DataColumn("Start_Date",typeof(string)),
                         new DataColumn("End_Date",typeof(string)),
                         new DataColumn("Updated_On",typeof(string)),
                         new DataColumn("Updated_By",typeof(string)),
                         new DataColumn("WBS_EXTNID",typeof(string)),
                     




                     });

            return dtExcelData4;
        }

        protected DataTable CountryDt()
        {
            DataTable dtExcelData2 = new DataTable();

            dtExcelData2.Columns.AddRange(new DataColumn[3]

                    { 
                        new DataColumn("LAND1", typeof(string)),
                        new DataColumn("LANDX", typeof(string)),
                        new DataColumn("NATIO", typeof(string)),
                       
              });

            return dtExcelData2;
        }
        protected DataTable TravelRegionDt()
        {
            DataTable dtExcelData3 = new DataTable();
            dtExcelData3.Columns.AddRange(new DataColumn[3]
                    { 
                     new DataColumn("LAND1",typeof(string)),
                     new DataColumn("RGION",typeof(string)),
                       new DataColumn("TEXT25",typeof(string))
                    
                     });
            return dtExcelData3;
        }

        protected void btnSaveCM_Click(object sender, EventArgs e)
     
        {
         try
            {
                DataTable dtExcelData = master_TCURRDt();
                PR_ExcelUploadBO objempBo = new PR_ExcelUploadBO();
                PR_EXCELCollBL objBl = new PR_EXCELCollBL();

                using (dtExcelData = (DataTable)(ViewState["master_TCURRDt"]))
                {
                    if (dtExcelData.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtExcelData.Rows.Count; i++)
                        {
                            objempBo.FCURR = dtExcelData.Rows[i]["FCURR"].ToString();
                            // objempBo.Employee_ID = Session["CompCode"].ToString() + dtExcelData.Rows[i]["Employee_ID"].ToString().Trim().Trim().ToLower();
                            objempBo.TCURR = dtExcelData.Rows[i]["TCURR"].ToString().Trim();
                            objempBo.UKURS = Convert.ToDecimal(dtExcelData.Rows[i]["UKURS"].ToString().Trim());



                            objBl.Create_CM_master_TCURR(objempBo);
                        }
                    }
                }

                PR_ExcelUploadBO bo2 = new PR_ExcelUploadBO();
                DataTable dt2 = new DataTable();

                using (dt2 = (DataTable)ViewState["master_TCURT_TypesDt"])
                {
                    if (dt2.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt2.Rows.Count; j++)
                        {
                            //bo2.compCode = Session["CompCode"].ToString();
                            //bo2.Employee_ID = Session["CompCode"].ToString() + dt2.Rows[j]["Employee_ID"].ToString().Trim().ToLower();
                            bo2.WAERS = dt2.Rows[j]["WAERS"].ToString().Trim();
                            bo2.LTEXT = dt2.Rows[j]["LTEXT"].ToString().Trim();


                            objBl.Create_CM_master_TCURT(bo2);
                        }
                    }
                }

                PR_ExcelUploadBO bo4 = new PR_ExcelUploadBO();
                DataTable dtExcelData4 = new DataTable();

                using (dtExcelData4 = (DataTable)ViewState["CountryDt"])
                {
                    if (dtExcelData4.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtExcelData4.Rows.Count; j++)
                        {
                            //bo2.compCode = Session["CompCode"].ToString();
                            //bo2.Employee_ID = Session["CompCode"].ToString() + dt2.Rows[j]["Employee_ID"].ToString().Trim().ToLower();
                            bo4.LAND1 = dtExcelData4.Rows[j]["LAND1"].ToString().Trim();

                            bo4.LANDX = dtExcelData4.Rows[j]["LANDX"].ToString().Trim();


                            bo4.NATIO = dtExcelData4.Rows[j]["NATIO"].ToString().Trim();

                            objBl.Create_Travel_master_T005T(bo4);
                        }
                    }
                }


                PR_ExcelUploadBO bo5 = new PR_ExcelUploadBO();
                DataTable dtExcelData5 = new DataTable();

                using (dtExcelData5 = (DataTable)ViewState["TravelRegionDt"])
                {
                    if (dtExcelData5.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtExcelData5.Rows.Count; j++)
                        {
                            //bo2.compCode = Session["CompCode"].ToString();
                            //bo2.Employee_ID = Session["CompCode"].ToString() + dt2.Rows[j]["Employee_ID"].ToString().Trim().ToLower();
                            bo5.LAND1 = dtExcelData5.Rows[j]["LAND1"].ToString().Trim();
                            bo5.RGION = dtExcelData5.Rows[j]["RGION"].ToString().Trim();
                            bo5.TEXT25 = dtExcelData5.Rows[j]["TEXT25"].ToString().Trim();




                            objBl.Create_Travel_master_T706O(bo5);
                        }
                    }
                }


                PR_ExcelUploadBO bo3 = new PR_ExcelUploadBO();
                DataTable dt3 = new DataTable();

                using (dt3 = (DataTable)ViewState["master_PRPS_Dt"])
                {
                    if (dt3.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt3.Rows.Count; j++)
                        {
                            //bo2.compCode = Session["CompCode"].ToString();
                            //bo2.Employee_ID = Session["CompCode"].ToString() + dt2.Rows[j]["Employee_ID"].ToString().Trim().ToLower();
                            bo3.POSID = dt3.Rows[j]["POSID"].ToString().Trim();
                            bo3.POST1 = dt3.Rows[j]["POST1"].ToString().Trim();
                            bo3.PSPHI = dt3.Rows[j]["PSPHI"].ToString().Trim();
                            bo3.VERNR = dt3.Rows[j]["VERNR"].ToString().Trim();
                            bo3.VERNA = dt3.Rows[j]["VERNA"].ToString().Trim();
                            bo3.PBUKR = dt3.Rows[j]["PBUKR"].ToString().Trim();
                            bo3.POSKI = dt3.Rows[j]["POSKI"].ToString().Trim();
                            bo3.PSPNR = Convert.ToInt32(dt3.Rows[j]["PSPNR"].ToString().Trim());
                            bo3.Created_By = dt3.Rows[j]["Created_By"].ToString().Trim();
                            bo3.Created_On = Convert.ToDateTime(dt3.Rows[j]["Created_On"].ToString().Trim());
                            bo3.Company_Code = dt3.Rows[j]["Company_Code"].ToString().Trim();
                            bo3.Start_Date = Convert.ToDateTime(dt3.Rows[j]["Start_Date"].ToString().Trim());
                            bo3.End_Date = Convert.ToDateTime(dt3.Rows[j]["End_Date"].ToString().Trim());
                            bo3.Updated_On = Convert.ToDateTime(dt3.Rows[j]["Updated_On"].ToString().Trim());
                            bo3.Updated_By = dt3.Rows[j]["Updated_By"].ToString().Trim();
                            bo3.WBS_EXTNID = dt3.Rows[j]["WBS_EXTNID"].ToString().Trim();

                            objBl.Create_CM_master_PRPS(bo3);
                        }
                    }
                }

              
               

            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "')", true); }
        }

        protected void btnClearCM_Click(object sender, EventArgs e)
        {

        }

        protected void Tab5_Click(object sender, EventArgs e)
        {
            HideTabs();

            view5.Visible = true;
            Tab5.CssClass = "nav-link active p-2";
        }


        protected void btnUploadIT_Click(object sender, EventArgs e)
        {
     
            try
            {
              
                string excelPath = Server.MapPath("~/PayCompute_Files/IT_Info/" + User.Identity.Name + "-" + (upldfileIT.FileName) + "-" + DateTime.Now.ToString("yyyy_MM_dd") + Path.GetExtension(upldfileIT.FileName));
                //Server.MapPath("~/PayCompute_Files/Emp_info/") + Path.GetFileName(uflEmpData.PostedFile.FileName);
                upldfileIT.SaveAs(excelPath);


                string conString = string.Empty;
                string extension = Path.GetExtension(upldfileIT.PostedFile.FileName);
                switch (extension)
                {
                    case ".xls": //Excel 97-03
                        conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07 or higher
                        conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                        break;
                }

                conString = string.Format(conString, excelPath);
                using (OleDbConnection excel_con = new OleDbConnection(conString))
                {
                    excel_con.Open();
                    // string PR = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString().Trim();
                    DataTable dtExcelData = IT_T7INI3Dt();


                    //using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT CID ,C_DESC ,A_DESC , B_DESC , EKGRP , EKNAM , WERKS , NAME1 ,SPART ,VTEXT , REGION_ID ,REGION_TEXT,ISOCODE,ISOTXT,POSID,POST1,PSPHI,VERNR,VERNA,PBUKR,POSKI,ZZDEL_HEAD,ZZDEL_HEADNAME,ZZPERNR01,ZZENAME01,ZZROLE01,,ZZPERNR02,ZZENAME02,ZZROLE02,ZZPERNR03,ZZENAME03,ZZROLE03,ZZPERNR04,ZZENAME04,ZZROLE0,ZZPERNR05,ZZENAME05,ZZROLE05,ZZPERNR06,ZZENAME06,ZZROLE06,ZZPERNR07,ZZENAME07,ZZROLE07,STAT,Created_By,Created_on,Company_Code,Start_Date,End_Date,Updated_On,Updated_By,WBS_EXTNID,PSPNR FROM  [PR_Info$] ", excel_con))
                    //using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT CID ,C_DESC, A_DESC , B_DESC , EKGRP,EKNAM, WERKS , NAME1 ,SPART , VTEXT ,REGION_ID , REGION_TEXT , ISOCODE , ISOTXT , POSID ,POST1 , PSPHI , VERNR, VERNA, PBUKR, POSKI , ZZDEL_HEAD , ZZDEL_HEADNAME, ZZPERNR01 , ZZENAME01, ZZROLE01, ZZPERNR02, ZZENAME02 , ZZROLE02 , ZZPERNR03, ZZENAME03 ,ZZROLE03, ZZPERNR04 ,ZZENAME04 ,ZZROLE04 , ZZPERNR05 , ZZENAME05 , ZZROLE05 , ZZPERNR06 , ZZENAME06 , ZZROLE06 , ZZPERNR07 ,ZZENAME07 ,ZZENAME07 ,  ZZROLE07 , STAT , Created_By , Created_on , Company_Code , Start_Date , End_Date , Updated_On , Updated_By ,  WBS_EXTNID , PSPNR FROM  [PR_Info] ", excel_con))
                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT ICODE ,ITEXT  FROM  [master.T7INI3$]", excel_con))
                    {
                        oda.Fill(dtExcelData);
                        string path1 = Server.MapPath("~/MyFolder/" + upldfileIT.FileName);
                        for (int i = dtExcelData.Rows.Count - 1; i >= 0; i += -1)
                        {
                            DataRow row = dtExcelData.Rows[i];
                            if (row[0] == null)
                            {
                                dtExcelData.Rows.Remove(row);
                            }
                            else if (string.IsNullOrEmpty(row[0].ToString().Trim()))
                            {
                                dtExcelData.Rows.Remove(row);
                            }
                        }
                        dtExcelData.AcceptChanges();
                    }

                    DataTable dtExcelData1 = IT_T7INI9Dt();



                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT SBSEC,SBDIV,SDVLT,TXEXM  FROM  [master.T7INI9$]", excel_con))
                    {
                        oda.Fill(dtExcelData1);
                      
                        for (int i = dtExcelData1.Rows.Count - 1; i >= 0; i += -1)
                        {
                            DataRow row = dtExcelData1.Rows[i];
                            if (row[0] == null)
                            {
                                dtExcelData1.Rows.Remove(row);
                            }
                            else if (string.IsNullOrEmpty(row[0].ToString().Trim()))
                            {
                                dtExcelData1.Rows.Remove(row);
                            }
                        }
                        dtExcelData1.AcceptChanges();
                    }
                    DataTable dtExcelData2 = IT_T7INI8Dt();
                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT SBSEC,SBDIV,SBDDS  FROM  [master.T7INI8$]", excel_con))
                    {
                        oda.Fill(dtExcelData2);

                        for (int i = dtExcelData2.Rows.Count - 1; i >= 0; i += -1)
                        {
                            DataRow row = dtExcelData2.Rows[i];
                            if (row[0] == null)
                            {
                                dtExcelData2.Rows.Remove(row);
                            }
                            else if (string.IsNullOrEmpty(row[0].ToString().Trim()))
                            {
                                dtExcelData2.Rows.Remove(row);
                            }
                        }
                        dtExcelData2.AcceptChanges();
                    }

                    DataTable dtExcelData3 = IT_T7INI4Dt();
                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT ICODE,ENDDA,BEGDA,CTGRY,WAEHI,ITLMT  FROM  [master.T7INI4$]", excel_con))
                    {
                        oda.Fill(dtExcelData3);

                        for (int i = dtExcelData3.Rows.Count - 1; i >= 0; i += -1)
                        {
                            DataRow row = dtExcelData3.Rows[i];
                            if (row[0] == null)
                            {
                                dtExcelData3.Rows.Remove(row);
                            }
                            else if (string.IsNullOrEmpty(row[0].ToString().Trim()))
                            {
                                dtExcelData3.Rows.Remove(row);
                            }
                        }
                        dtExcelData3.AcceptChanges();
                    }

                    DataTable dtExcelData4 = IT_T7INI5Dt();
                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT SBSEC,SBTDS  FROM  [master.T7INI5$]", excel_con))
                    {
                        oda.Fill(dtExcelData4);

                        for (int i = dtExcelData4.Rows.Count - 1; i >= 0; i += -1)
                        {
                            DataRow row = dtExcelData4.Rows[i];
                            if (row[0] == null)
                            {
                                dtExcelData4.Rows.Remove(row);
                            }
                            else if (string.IsNullOrEmpty(row[0].ToString().Trim()))
                            {
                                dtExcelData4.Rows.Remove(row);
                            }
                        }
                        dtExcelData4.AcceptChanges();
                    }
                    

                    excel_con.Close();


                    GV_T7INI3.DataSource = dtExcelData;
                    GV_T7INI3.DataBind();

                    GV_T7INI9.DataSource = dtExcelData1;
                    GV_T7INI9.DataBind();
                    GV_T7INI8.DataSource = dtExcelData2;
                    GV_T7INI8.DataBind();
                    GV_T7INI4.DataSource = dtExcelData3;
                    GV_T7INI4.DataBind();
                    GV_T7INI5.DataSource = dtExcelData4;
                    GV_T7INI5.DataBind();


                 
                    divgrds.Visible = false;
                    divgrdsIT.Visible = true;
                    btnSaveIT.Visible = true;
                    btnClearIT.Visible = true;
                    btnUploadPRData.Visible = false;
                    view1.Visible = false;
                    //Tab2.CssClass = "nav-link active p-2";
                    ViewState["IT_T7INI3Dt"] = dtExcelData;
                    ViewState["IT_T7INI9Dt"] = dtExcelData1;
                    ViewState["IT_T7INI8Dt"] = dtExcelData2;
                    ViewState["IT_T7INI4Dt"] = dtExcelData3;
                    ViewState["IT_T7INI5Dt"] = dtExcelData4;
                   
                   
                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "')", true);
            }
        }

        protected DataTable IT_T7INI3Dt()
        {
            DataTable dtExcelData = new DataTable();

            dtExcelData.Columns.AddRange(new DataColumn[2]

                    { 
                        new DataColumn("ICODE", typeof(string)),
                        new DataColumn("ITEXT", typeof(string)),
                       
              });

            return dtExcelData;
        }

        protected DataTable IT_T7INI9Dt()
        {
            DataTable dtExcelData1 = new DataTable();

            dtExcelData1.Columns.AddRange(new DataColumn[4]

                    { 
                        new DataColumn("SBSEC", typeof(string)),
                        new DataColumn("SBDIV", typeof(string)),
                          new DataColumn("SDVLT", typeof(string)),
                        new DataColumn("TXEXM", typeof(string)),
                       
              });

            return dtExcelData1;
        }

        protected DataTable IT_T7INI8Dt()
        {
            DataTable dtExcelData2 = new DataTable();

            dtExcelData2.Columns.AddRange(new DataColumn[3]

                    { 
                        new DataColumn("SBSEC", typeof(string)),
                        new DataColumn("SBDIV", typeof(string)),
                          new DataColumn("SBDDS", typeof(string)),
                    
              });

            return dtExcelData2;
        }

        protected DataTable IT_T7INI4Dt()
        {
            DataTable dtExcelData3 = new DataTable();

            dtExcelData3.Columns.AddRange(new DataColumn[6]

                    { 
                        new DataColumn("ICODE", typeof(string)),
                        new DataColumn("ENDDA", typeof(string)),
                        new DataColumn("BEGDA", typeof(string)),
                        new DataColumn("CTGRY", typeof(string)),
                        new DataColumn("WAEHI", typeof(string)),
                        new DataColumn("ITLMT", typeof(string)),
              });

            return dtExcelData3;
        }

        protected DataTable IT_T7INI5Dt()
        {
            DataTable dtExcelData4 = new DataTable();

            dtExcelData4.Columns.AddRange(new DataColumn[2]

                    { 
                        new DataColumn("SBSEC", typeof(string)),
                        new DataColumn("SBTDS", typeof(string)),
                       
              });

            return dtExcelData4;
        }

        protected void btnSaveIT_Click(object sender, EventArgs e)
        {

         try
            {
                DataTable dtExcelData = IT_T7INI3Dt();
                PR_ExcelUploadBO objempBo = new PR_ExcelUploadBO();
                PR_EXCELCollBL objBl = new PR_EXCELCollBL();

                using (dtExcelData = (DataTable)(ViewState["IT_T7INI3Dt"]))
                {
                    if (dtExcelData.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtExcelData.Rows.Count; i++)
                        {
                            objempBo.ICODE = dtExcelData.Rows[i]["ICODE"].ToString();
                            // objempBo.Employee_ID = Session["CompCode"].ToString() + dtExcelData.Rows[i]["Employee_ID"].ToString().Trim().Trim().ToLower();
                            objempBo.ITEXT = dtExcelData.Rows[i]["ITEXT"].ToString().Trim();


                            objBl.Create_IT_master_T7INI3(objempBo);
                        }
                    }
                }

                PR_ExcelUploadBO bo2 = new PR_ExcelUploadBO();
                DataTable dt2 = new DataTable();

                using (dt2 = (DataTable)ViewState["IT_T7INI9Dt"])
                {
                    if (dt2.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt2.Rows.Count; j++)
                        {
                            //bo2.compCode = Session["CompCode"].ToString();
                            //bo2.Employee_ID = Session["CompCode"].ToString() + dt2.Rows[j]["Employee_ID"].ToString().Trim().ToLower();
                            bo2.SBSEC = dt2.Rows[j]["SBSEC"].ToString().Trim();
                            bo2.SBDIV = dt2.Rows[j]["SBDIV"].ToString().Trim();
                            bo2.SDVLT = dt2.Rows[j]["SDVLT"].ToString().Trim();
                            bo2.TXEXM = dt2.Rows[j]["TXEXM"].ToString().Trim();

                            objBl.Create_IT_master_T7INI9(bo2);
                        }
                    }
                }

                PR_ExcelUploadBO bo3 = new PR_ExcelUploadBO();
                DataTable dt3 = new DataTable();

                using (dt3 = (DataTable)ViewState["IT_T7INI8Dt"])
                {
                    if (dt3.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt3.Rows.Count; j++)
                        {
                            //bo2.compCode = Session["CompCode"].ToString();
                            //bo2.Employee_ID = Session["CompCode"].ToString() + dt2.Rows[j]["Employee_ID"].ToString().Trim().ToLower();
                            bo3.SBSEC = dt3.Rows[j]["SBSEC"].ToString().Trim();
                            bo3.SBDIV = dt3.Rows[j]["SBDIV"].ToString().Trim();
                            bo3.SBDDS = dt3.Rows[j]["SBDDS"].ToString().Trim();
                            objBl.Create_IT_master_T7INI8(bo3);
                        }
                    }
                }

                PR_ExcelUploadBO bo4 = new PR_ExcelUploadBO();
                DataTable dtExcelData4 = new DataTable();

                using (dtExcelData4 = (DataTable)ViewState["IT_T7INI4Dt"])
                {
                    if (dtExcelData4.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtExcelData4.Rows.Count; j++)
                        {
                            //bo2.compCode = Session["CompCode"].ToString();
                            //bo2.Employee_ID = Session["CompCode"].ToString() + dt2.Rows[j]["Employee_ID"].ToString().Trim().ToLower();
                            bo4.ICODE = dtExcelData4.Rows[j]["ICODE"].ToString().Trim();
                            DateTime dateTime = (DateTime)bo4.BEGDA;
                            bo4.ENDDA = Convert.ToDateTime(dtExcelData4.Rows[j]["ENDDA"]);
                           
                            bo4.BEGDA = Convert.ToDateTime(dtExcelData4.Rows[j]["BEGDA"].ToString().Trim());
                            bo4.CTGRY = dtExcelData4.Rows[j]["CTGRY"].ToString().Trim();
                            bo4.WAEHI = dtExcelData4.Rows[j]["WAEHI"].ToString().Trim();
                            bo4.ITLMT = dtExcelData4.Rows[j]["ITLMT"].ToString().Trim();

                            objBl.Create_IT_master_T7INI4(bo4);
                        }
                    }
                }


                PR_ExcelUploadBO bo5 = new PR_ExcelUploadBO();
                DataTable dtExcelData5 = new DataTable();

                using (dtExcelData5 = (DataTable)ViewState["IT_T7INI5Dt"])
                {
                    if (dtExcelData5.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtExcelData5.Rows.Count; j++)
                        {
                            //bo2.compCode = Session["CompCode"].ToString();
                            //bo2.Employee_ID = Session["CompCode"].ToString() + dt2.Rows[j]["Employee_ID"].ToString().Trim().ToLower();
                            bo5.SBSEC = dtExcelData5.Rows[j]["SBSEC"].ToString().Trim();
                            bo5.SBTDS = dtExcelData5.Rows[j]["SBTDS"].ToString().Trim();




                            objBl.Create_IT_master_T7INI5(bo5);
                        }
                    }
                }

               

            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "')", true); }
        }

        protected void btnClearIT_Click(object sender, EventArgs e)
        {

        }

       
    }
}

