using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using iEmpPowerMaster_Load;

namespace iEmpPower.UI.Configuration
{
    public partial class addData : System.Web.UI.Page
    {
        private string sCreateUserLogPath = ConfigurationManager.AppSettings["CreateUserLog"].ToString() + DateTime.Now.Date.ToShortDateString().Replace('/', '-') + ".html";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HideTabs();
                view1.Visible = true;
                Tab1.CssClass = "nav-link active p-2";
                Load_country(); Load_dropDowns();
            }
            lblcompCode.Text =  User.Identity.Name;
        }

        protected void btnUploadEmpData_Click(object sender, EventArgs e)
        {
            try
            {
                string excelPath = Server.MapPath("~/PayCompute_Files/Emp_info/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uflEmpData.FileName) + "-" + DateTime.Now.ToString("yyyy_MM_dd") + Path.GetExtension(uflEmpData.FileName));
                //Server.MapPath("~/PayCompute_Files/Emp_info/") + Path.GetFileName(uflEmpData.PostedFile.FileName);
                uflEmpData.SaveAs(excelPath);


                string conString = string.Empty;
                string extension = Path.GetExtension(uflEmpData.PostedFile.FileName);
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
                    string Emp_Info = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString().Trim();
                    DataTable dtExcelData = EmpInfoDt();

                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT Employee_ID,Salutation,First_Name,Middle_Name,Last_Name,Gender,Date_of_Birth,Marital_Status,Father_Name,Mother_Name,Spouse_Name,Bank_Name,Account_Number,IFSC_Code,Bank_Branch,Bank_District,Branch_Country,Branch_State,Address_Type,Start_Date,End_Date,Residence_Number,Street,Locality,District,Country,State,Pincode,STD_Code,Ward_Number,ESI_Applicable,ESI_Number,ESI_Dispencary,PF_Applicable,PF_Number,PF_Number_Dept_File,Restrict_PF,Zero_Pension,Zero_PT,Employee_Department,Grade,Branch,Division,Date_of_Joining FROM  [Emp_Info$] where 'Employee_ID != '''", excel_con))
                    {
                        oda.Fill(dtExcelData);
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

                    DataTable dtExcelData1 = ConTypesDt();

                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT Employee_ID,Contact_Type,Contact_Type_ID FROM [Contact_Info$] where 'Employee_ID != '''", excel_con))
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


                    DataTable dtExcelData2 = DocTypesDt();
                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT Employee_ID,Document_Type,Document_Type_ID FROM [Document_Info$] where 'Employee_ID != '''", excel_con))
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
                    excel_con.Close();


                    GV_EmpInfo.DataSource = dtExcelData;
                    GV_EmpInfo.DataBind();

                    gv_dept.DataSource = dtExcelData;
                    gv_dept.DataBind();

                    GV_BankInfo.DataSource = dtExcelData;
                    GV_BankInfo.DataBind();

                    GV_AddressInfo.DataSource = dtExcelData;
                    GV_AddressInfo.DataBind();

                    GV_ContInfo.DataSource = dtExcelData1;
                    GV_ContInfo.DataBind();

                    GV_DocInfo.DataSource = dtExcelData2;
                    GV_DocInfo.DataBind();

                    GV_Benefits.DataSource = dtExcelData;
                    GV_Benefits.DataBind();
                    divgrds.Visible = true;
                    btnSave.Visible = true;
                    btnClear.Visible = true;
                    ViewState["EmpDataDt"] = dtExcelData;
                    ViewState["EmpContDt"] = dtExcelData1;
                    ViewState["EmpDoctDt"] = dtExcelData2;
                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "')", true);
            }
        }

        protected DataTable DocTypesDt()
        {
            DataTable dtExcelData2 = new DataTable();
            dtExcelData2.Columns.AddRange(new DataColumn[3]
                    { 
                     new DataColumn("Employee_ID",typeof(string)),
                     new DataColumn("Document_Type",typeof(string)),
                     new DataColumn("Document_Type_ID",typeof(string))
                     });

            return dtExcelData2;
        }

        protected DataTable ConTypesDt()
        {
            DataTable dtExcelData1 = new DataTable();
            dtExcelData1.Columns.AddRange(new DataColumn[3]
                    { 
                     new DataColumn("Employee_ID",typeof(string)),
                     new DataColumn("Contact_Type",typeof(string)),
                     new DataColumn("Contact_Type_ID",typeof(string))
                     });
            return dtExcelData1;
        }

        protected DataTable EmpInfoDt()
        {
            DataTable dtExcelData = new DataTable();

            dtExcelData.Columns.AddRange(new DataColumn[44]
                    { 
                        new DataColumn("Employee_ID", typeof(string)),
                        new DataColumn("Salutation", typeof(string)),
                        new DataColumn("First_Name", typeof(string)),
                        new DataColumn("Middle_Name", typeof(string)),
                        new DataColumn("Last_Name", typeof(string)),
                        new DataColumn("Gender",typeof(string)),
                        new DataColumn("Date_of_Birth",typeof(string)),
                        new DataColumn("Marital_Status",typeof(string)),
                         new DataColumn("Father_Name",typeof(string)), 
                         new DataColumn("Mother_Name",typeof(string)), 
                         new DataColumn("Spouse_Name",typeof(string)),
                         new DataColumn("Bank_Name",typeof(string)),
                         new DataColumn("Account_Number",typeof(string)),
                         new DataColumn("IFSC_Code",typeof(string)),
                         new DataColumn("Bank_Branch",typeof(string)),
                         new DataColumn("Bank_District",typeof(string)),
                         new DataColumn("Branch_Country",typeof(string)),
                         new DataColumn("Branch_State",typeof(string)),
                         new DataColumn("Address_Type",typeof(string)),
                         new DataColumn("Start_Date",typeof(string)),
                         new DataColumn("End_Date",typeof(string)),
                         new DataColumn("Residence_Number",typeof(string)),
                         new DataColumn("Street",typeof(string)),
                         new DataColumn("Locality",typeof(string)),
                         new DataColumn("District",typeof(string)),
                         new DataColumn("Country",typeof(string)),
                         new DataColumn("State",typeof(string)),
                         new DataColumn("Pincode",typeof(string)),
                         new DataColumn("STD_Code",typeof(string)),
                         new DataColumn("Ward_Number",typeof(string)),
                         new DataColumn("ESI_Applicable",typeof(string)),
                         new DataColumn("ESI_Number",typeof(string)),
                         new DataColumn("ESI_Dispencary",typeof(string)),
                         new DataColumn("PF_Applicable",typeof(string)),
                         new DataColumn("PF_Number",typeof(string)),
                         new DataColumn("PF_Number_Dept_File",typeof(string)),
                         new DataColumn("Restrict_PF",typeof(string)),
                         new DataColumn("Zero_Pension",typeof(string)),
                         new DataColumn("Zero_PT",typeof(string)),
                         new DataColumn("Employee_Department",typeof(string)),
                         new DataColumn("Grade",typeof(string)),
                         new DataColumn("Branch",typeof(string)),
                         new DataColumn("Division",typeof(string)),
                         new DataColumn("Date_of_Joining",typeof(string)),
                         
                     });

            return dtExcelData;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtExcelData = EmpInfoDt();
                EmoDataBo objempBo = new EmoDataBo();
                EmpDataBL objBl = new EmpDataBL();
                bool? st = false;
                bool? st1 = false;
                bool? st2 = false;
                using (dtExcelData = (DataTable)(ViewState["EmpDataDt"]))
                {
                    if (dtExcelData.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtExcelData.Rows.Count; i++)
                        {
                            objempBo.compCode = Session["CompCode"].ToString();
                            objempBo.Employee_ID = Session["CompCode"].ToString() + dtExcelData.Rows[i]["Employee_ID"].ToString().Trim().Trim().ToLower();
                            objempBo.Salutation = dtExcelData.Rows[i]["Salutation"].ToString().Trim();
                            objempBo.First_Name = dtExcelData.Rows[i]["First_Name"].ToString().Trim();
                            objempBo.Middle_Name = dtExcelData.Rows[i]["Middle_Name"].ToString().Trim();
                            objempBo.Last_Name = dtExcelData.Rows[i]["Last_Name"].ToString().Trim();
                            objempBo.Gender = dtExcelData.Rows[i]["Gender"].ToString().Trim();
                            DateTime dob = dtExcelData.Rows[i]["Date_of_Birth"].ToString().Trim() == "" ? DateTime.MinValue : Convert.ToDateTime(dtExcelData.Rows[i]["Date_of_Birth"].ToString().Trim());
                            objempBo.Date_of_Birth = dob.ToString("yyyy-MM-dd");
                            objempBo.Material_Status = dtExcelData.Rows[i]["Marital_Status"].ToString().Trim();
                            objempBo.Father_Name = dtExcelData.Rows[i]["Father_Name"].ToString().Trim();
                            objempBo.Mother_Name = dtExcelData.Rows[i]["Mother_Name"].ToString().Trim();
                            objempBo.Spouse_Name = dtExcelData.Rows[i]["Spouse_Name"].ToString().Trim();
                            objempBo.Bank_Name = dtExcelData.Rows[i]["Bank_Name"].ToString().Trim();
                            objempBo.Account_Number = dtExcelData.Rows[i]["Account_Number"].ToString().Trim();
                            objempBo.IFSC_Code = dtExcelData.Rows[i]["IFSC_Code"].ToString().Trim();
                            objempBo.Bank_Branch = dtExcelData.Rows[i]["Bank_Branch"].ToString().Trim();
                            objempBo.Bank_District = dtExcelData.Rows[i]["Bank_District"].ToString().Trim();
                            objempBo.Branch_Country = dtExcelData.Rows[i]["Branch_Country"].ToString().Trim();
                            objempBo.Branch_State = dtExcelData.Rows[i]["Branch_State"].ToString().Trim();
                            objempBo.Address_Type = dtExcelData.Rows[i]["Address_Type"].ToString().Trim();
                            objempBo.Residence_Number = dtExcelData.Rows[i]["Residence_Number"].ToString().Trim();
                            objempBo.Street = dtExcelData.Rows[i]["Street"].ToString().Trim();
                            objempBo.Locality = dtExcelData.Rows[i]["Locality"].ToString().Trim();
                            objempBo.District = dtExcelData.Rows[i]["District"].ToString().Trim();
                            objempBo.Country = dtExcelData.Rows[i]["Country"].ToString().Trim();
                            objempBo.State = dtExcelData.Rows[i]["State"].ToString().Trim();
                            objempBo.Pincode = dtExcelData.Rows[i]["Pincode"].ToString().Trim();
                            objempBo.STD_Code = dtExcelData.Rows[i]["STD_Code"].ToString().Trim();
                            objempBo.Ward_Number = dtExcelData.Rows[i]["Ward_Number"].ToString().Trim();
                            objempBo.ESI_Applicable = dtExcelData.Rows[i]["ESI_Applicable"].ToString().Trim();
                            objempBo.ESI_Number = dtExcelData.Rows[i]["ESI_Number"].ToString().Trim();
                            objempBo.ESI_Dispencary = dtExcelData.Rows[i]["ESI_Dispencary"].ToString().Trim();
                            objempBo.PF_Applicable = dtExcelData.Rows[i]["PF_Applicable"].ToString().Trim();
                            objempBo.PF_Number = dtExcelData.Rows[i]["PF_Number"].ToString().Trim();
                            objempBo.PF_Number_Dept_File = dtExcelData.Rows[i]["PF_Number_Dept_File"].ToString().Trim();
                            objempBo.Restrict_PF = dtExcelData.Rows[i]["Restrict_PF"].ToString().Trim();
                            objempBo.Zero_Pension = dtExcelData.Rows[i]["Zero_Pension"].ToString().Trim();
                            objempBo.Zero_PT = dtExcelData.Rows[i]["Zero_PT"].ToString().Trim();

                            objempBo.EDEPT = dtExcelData.Rows[i]["Employee_Department"].ToString().Trim();
                            objempBo.EGRADE = dtExcelData.Rows[i]["Grade"].ToString().Trim();
                            objempBo.EBRANCH = dtExcelData.Rows[i]["Branch"].ToString().Trim();
                            objempBo.EDIVISION = dtExcelData.Rows[i]["Division"].ToString().Trim();
                            DateTime DOJ = dtExcelData.Rows[i]["Date_of_Joining"].ToString().Trim() == "" ? DateTime.MinValue : Convert.ToDateTime(dtExcelData.Rows[i]["Date_of_Joining"].ToString().Trim());
                            objempBo.EDOJ = DOJ.ToString("yyyy-MM-dd");                            
                             DateTime ASD = dtExcelData.Rows[i]["Start_Date"].ToString().Trim() == "" ? DateTime.MinValue : Convert.ToDateTime(dtExcelData.Rows[i]["Start_Date"].ToString().Trim());
                            objempBo._1=ASD.ToString("yyyy-MM-dd");
                            DateTime AED = dtExcelData.Rows[i]["End_Date"].ToString().Trim() == "" ? DateTime.MinValue : Convert.ToDateTime(dtExcelData.Rows[i]["End_Date"].ToString().Trim());
                            objempBo._10 = AED.ToString("yyyy-MM-dd");
                            objempBo.flg = 1;
                            objempBo.AppID = "";
                            objBl.Create_EmpInfo(objempBo, ref st);

                        }
                        //if (st == true) {
                        //    iEmpPower_DT_Wizard_Utility ObjLogError = new iEmpPower_DT_Wizard_Utility();
                        //      MembershipCreateStatus MuUserStatus = new MembershipCreateStatus();
                        //    using (DataTable DtSuccessEmailList = new DataTable())
                        //    {
                        //            MembershipUser MuUser = Membership.CreateUser(objempBo.Employee_ID.ToString(), objempBo.Date_of_Birth.ToString(), Row["EMAIL"].ToString().ToLower().Trim(), "a", "b", true, out MuUserStatus);
                        //            switch (MuUserStatus)
                        //            {
                        //                case MembershipCreateStatus.DuplicateEmail:
                        //                    ObjLogError.LogError(sCreateUserLogPath, Row["EMAIL"].ToString().ToLower().Trim() + " Email already exist.");
                        //                    break;
                        //                case MembershipCreateStatus.DuplicateProviderUserKey:
                        //                    break;
                        //                case MembershipCreateStatus.DuplicateUserName:
                        //                    ObjLogError.LogError(sCreateUserLogPath, objempBo.Employee_ID.ToString().Trim() + " User already exist.");
                        //                    break;
                        //                case MembershipCreateStatus.InvalidAnswer:
                        //                    break;
                        //                case MembershipCreateStatus.InvalidEmail:
                        //                    ObjLogError.LogError(sCreateUserLogPath, Row["EMAIL"].ToString().ToLower().Trim() + " Email is not valid.");
                        //                    break;
                        //                case MembershipCreateStatus.InvalidPassword:
                        //                    break;
                        //                case MembershipCreateStatus.InvalidProviderUserKey:
                        //                    break;
                        //                case MembershipCreateStatus.InvalidQuestion:
                        //                    break;
                        //                case MembershipCreateStatus.InvalidUserName:
                        //                    ObjLogError.LogError(sCreateUserLogPath, objempBo.Employee_ID.ToString().ToLower().Trim() + " UserName is not valid.");
                        //                    break;
                        //                case MembershipCreateStatus.ProviderError:
                        //                    break;
                        //                case MembershipCreateStatus.Success:
                        //                    DtSuccessEmailList.Rows.Add(objempBo.Employee_ID.ToString().Trim(), objempBo.Date_of_Birth.ToString(), Row["EMAIL"].ToString().ToLower().Trim());
                        //                    ObjLogError.LogError(sCreateUserLogPath, objempBo.Employee_ID.ToString().ToLower().Trim() + " - " + Row["EMAIL"].ToString().ToLower().Trim() + " - User created successfully.");
                        //                    break;
                        //                case MembershipCreateStatus.UserRejected:
                        //                    ObjLogError.LogError(sCreateUserLogPath, objempBo.Employee_ID.ToString().ToLower().Trim() + " User rejected.");
                        //                    break;
                        //                default:
                        //                    ObjLogError.LogError(sCreateUserLogPath, objempBo.Employee_ID.ToString().ToLower().Trim() + " - " + Row["EMAIL"].ToString().ToLower().Trim() + " - Unknown Error.");
                        //                    break;
                        //            }

                        //        string[] MsgCC = { };

                        //        //--------------------------- SENDING EMAIL NOTIFICATION - TO USERS ---------------------------------------
                        //        string Mailbody = string.Empty;
                        //        string AddressInfoFilePath = Server.MapPath(@"~/EmailTemplates/CreateUser.html");
                        //        Mailbody = System.IO.File.ReadAllText(AddressInfoFilePath);

                        //        Parallel.ForEach(DtSuccessEmailList.AsEnumerable(), DtRow => masterbl.SendMail(DtRow["EmailID"].ToString(), MsgCC, "User in IEmppower created."
                        //          , Mailbody.Replace("##EMPPERNR##", DtRow["Username"].ToString()).Replace("##PASSWORD##", objempBo.Date_of_Birth.ToString()).Replace("##EMAILID##", DtRow["EmailID"].ToString())));

                        //        //MsgCls("User Generated successfully. <b>[</b> Please check User log for details ! <b>]</b>", LblMsg, Color.Green);
                        //    }
                        //}
                    }


                }

                EmoDataBo bo2 = new EmoDataBo();
                DataTable dt2 = new DataTable();
                using (dt2 = (DataTable)ViewState["EmpContDt"])
                {
                    if (dt2.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt2.Rows.Count; j++)
                        {
                            bo2.compCode = Session["CompCode"].ToString();
                            bo2.Employee_ID = Session["CompCode"].ToString() + dt2.Rows[j]["Employee_ID"].ToString().Trim().ToLower();
                            bo2.TypeID = dt2.Rows[j]["Contact_Type"].ToString().Trim();
                            bo2.ID = dt2.Rows[j]["Contact_Type_ID"].ToString().Trim();
                            bo2.flg = 1;
                            bo2.loginID = "";
                            objBl.Create_Doc_Contypes(bo2, ref st1);
                        }
                    }
                }

                EmoDataBo bo3 = new EmoDataBo();
                DataTable dt3 = new DataTable();
                using (dt3 = (DataTable)ViewState["EmpDoctDt"])
                {
                    if (dt3.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt2.Rows.Count; j++)
                        {
                            bo3.compCode = Session["CompCode"].ToString();
                            bo3.Employee_ID = Session["CompCode"].ToString() + dt3.Rows[j]["Employee_ID"].ToString().Trim().ToLower();
                            bo3.TypeID = dt3.Rows[j]["Document_Type"].ToString().Trim();
                            bo3.ID = dt3.Rows[j]["Document_Type_ID"].ToString().Trim();
                            bo3.flg = 2;
                            bo3.loginID = "";
                            objBl.Create_Doc_Contypes(bo3, ref st2);
                        }
                    }
                }

                if (st == true || st1 == true || st2 == true)
                {
                    ViewState["EmpDataDt"] = null;
                    ViewState["EmpContDt"] = null;
                    ViewState["EmpDoctDt"] = null;

                    GV_EmpInfo.DataSource = (DataTable)ViewState["EmpDataDt"];
                    GV_EmpInfo.DataBind();

                    gv_dept.DataSource = (DataTable)ViewState["EmpDataDt"];
                    gv_dept.DataBind();

                    GV_BankInfo.DataSource = (DataTable)ViewState["EmpDataDt"];
                    GV_BankInfo.DataBind();

                    GV_AddressInfo.DataSource = (DataTable)ViewState["EmpDataDt"];
                    GV_AddressInfo.DataBind();

                    GV_ContInfo.DataSource = (DataTable)ViewState["EmpContDt"];
                    GV_ContInfo.DataBind();

                    GV_DocInfo.DataSource = (DataTable)ViewState["EmpDoctDt"];
                    GV_DocInfo.DataBind();

                    GV_Benefits.DataSource = (DataTable)ViewState["EmpDataDt"];
                    GV_Benefits.DataBind();
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    divgrds.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Saved Successfully')", true);
                }
            }

            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "')", true); }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["EmpDataDt"] = null;
                ViewState["EmpContDt"] = null;
                ViewState["EmpDoctDt"] = null;
                divgrds.Visible = false;
                btnSave.Visible = false;
                btnClear.Visible = false;
            }
            catch (Exception ex) { }
        }

        protected void rbtnESI_SelectedIndexChanged(object sender, EventArgs e)
        {
            esidv.Visible = rbtnESI.SelectedValue == "Yes" ? true : false;
            //txtESIDisp.Enabled = rbtnESI.SelectedValue == "Yes" ? true : false;
        }

        protected void rbnPFApp_SelectedIndexChanged(object sender, EventArgs e)
        {
            pfdv.Visible = rbnPFApp.SelectedValue == "Yes" ? true : false;
            //txtPFNumDep.Enabled = rbnPFApp.SelectedValue == "Yes" ? true : false;
        }

        public void Load_country()
        {
            try
            {
                configurationcollectionbo objLst = configurationbl.Get_Country(1);
                ddlAddCountry.DataSource = objLst;
                ddlAddCountry.DataTextField = "CountryTxt";
                ddlAddCountry.DataValueField = "Country";
                ddlAddCountry.DataBind();
                ddlAddCountry.Items.Insert(0, new ListItem(" - Select Country - ", "0"));

                ddlBnkContry.DataSource = objLst;
                ddlBnkContry.DataTextField = "CountryTxt";
                ddlBnkContry.DataValueField = "Country";
                ddlBnkContry.DataBind();
                ddlBnkContry.Items.Insert(0, new ListItem(" - Select Country - ", "0"));
            }
            catch (Exception ex) { }
        }

        public void Load_dropDowns()
        {
            try
            {
                configurationcollectionbo objLst = configurationbl.Get_AddressTypes(1);
                ddlAddType.DataSource = objLst;
                ddlAddType.DataTextField = "DDLTYPETEXT";
                ddlAddType.DataValueField = "DDLTYPE";
                ddlAddType.DataBind();
                ddlAddType.Items.Insert(0, new ListItem(" - Select Address Type - ", "0"));

                configurationcollectionbo objLst1 = configurationbl.Get_Salutations(1);
                DDL_Salutation.DataSource = objLst1;
                DDL_Salutation.DataTextField = "DDLTYPETEXT";
                DDL_Salutation.DataValueField = "DDLTYPE";
                DDL_Salutation.DataBind();
                DDL_Salutation.Items.Insert(0, new ListItem(" - Select Salutation - ", "0"));

                configurationcollectionbo objLst2 = configurationbl.Get_MaterialStatus(1);
                ddl_materlSatuts.DataSource = objLst2;
                ddl_materlSatuts.DataTextField = "DDLTYPETEXT";
                ddl_materlSatuts.DataValueField = "DDLTYPE";
                ddl_materlSatuts.DataBind();
                //ddl_materlSatuts.Items.Insert(0, new ListItem(" - Select Marital Status - ", "0"));

                configurationcollectionbo objLst3 = configurationbl.Get_ContactTypes(1);
                ddlContType.DataSource = objLst3;
                ddlContType.DataTextField = "DDLTYPETEXT";
                ddlContType.DataValueField = "DDLTYPE";
                ddlContType.DataBind();
                ddlContType.SelectedValue = "2";

                configurationcollectionbo objLst4 = configurationbl.Get_DocumtTypes(1);
                ddlDocType.DataSource = objLst4;
                ddlDocType.DataTextField = "DDLTYPETEXT";
                ddlDocType.DataValueField = "DDLTYPE";
                ddlDocType.DataBind();
                ddlDocType.Items.Insert(0, new ListItem(" - Select Document Type - ", "0"));

                configurationcollectionbo objLst5 = configurationbl.Get_Departments(Session["CompCode"].ToString().ToLower().Trim(),1);
                ddleDept.DataSource = objLst5;
                ddleDept.DataTextField = "deptdesc";
                ddleDept.DataValueField = "ID";
                ddleDept.DataBind();
                ddleDept.Items.Insert(0, new ListItem(" - Select Department - ", "0"));
            }
            catch (Exception ex) { }
        }

        public void Load_states(string cntry, int flg)
        {
            try
            {
                configurationcollectionbo objLst = configurationbl.Get_states(cntry, flg);
                //DDL_Cstate.DataSource = objLst;
                //DDL_Cstate.DataTextField = "StateTxt";
                //DDL_Cstate.DataValueField = "State";
                //DDL_Cstate.DataBind();
                //DDL_Cstate.Items.Insert(0, new ListItem(" - SELECT - ", "0"));
                //return objLst;
            }
            catch (Exception ex) { }
        }

        protected void ddlBnkContry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                configurationcollectionbo objLst = configurationbl.Get_states(ddlBnkContry.SelectedValue.ToString(), 1);
                ddl_Bnkstate.DataSource = objLst;
                ddl_Bnkstate.DataTextField = "StateTxt";
                ddl_Bnkstate.DataValueField = "State";
                ddl_Bnkstate.DataBind();
                ddl_Bnkstate.Items.Insert(0, new ListItem(" - Select State - ", "0"));
                //return objLst;
            }
            catch (Exception ex) { }
        }

        protected void ddlAddCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                configurationcollectionbo objLst = configurationbl.Get_states(ddlAddCountry.SelectedValue.ToString(), 1);
                ddlAddState.DataSource = objLst;
                ddlAddState.DataTextField = "StateTxt";
                ddlAddState.DataValueField = "State";
                ddlAddState.DataBind();
                ddlAddState.Items.Insert(0, new ListItem(" - Select State - ", "0"));
                //return objLst;
            }
            catch (Exception ex) { }
        }

        protected void btnDocType_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmpID.Text == "")
                {
                    txtEmpID.Focus();
                }
                else
                {
                    configurationbo objBo = new configurationbo();
                    configurationbl bl = new configurationbl();
                    configurationcollectionbo objLst = new configurationcollectionbo();
                    bool? status = false;

                    bl.formvalidation(Session["CompCode"].ToString(), ddlDocType.SelectedValue.ToString(), (lblcompCode.Text.Trim() + txtEmpID.Text.Trim()).ToString().ToLower(), ref status, 3);
                    if (status == false)
                    {
                        using (DataTable Dt = ViewState["tempDoctypes"] != null ? (DataTable)ViewState["tempDoctypes"] : DocTypesDt())
                        {
                            Dt.Rows.Add((lblcompCode.Text.Trim() + txtEmpID.Text.Trim()).ToString().ToLower(), ddlDocType.SelectedItem, txtDocTypeID.Text.Trim());
                            ViewState["tempDoctypes"] = Dt;
                        }
                        gv_tempDocTypes.DataSource = (DataTable)ViewState["tempDoctypes"];
                        gv_tempDocTypes.DataBind();
                        ddlDocType.SelectedIndex = -1;
                        txtDocTypeID.Text = "";
                        btnDocuntinfosubmit.Visible = gv_tempDocTypes.Rows.Count > 0 ? true : false;
                        btninfo6.Visible = gv_tempDocTypes.Rows.Count > 0 ? true : false;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Employee's Document Info already exists')", true);
                    }
                }
            }
            catch (Exception ex) { }
        }

        protected void btnContAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmpID.Text == "")
                {
                    txtEmpID.Focus();
                }
                else
                {
                    configurationbo objBo = new configurationbo();
                    configurationbl bl = new configurationbl();
                    configurationcollectionbo objLst = new configurationcollectionbo();
                    bool? status = false;

                    bl.formvalidation(Session["CompCode"].ToString(), ddlContType.SelectedValue.ToString(), (lblcompCode.Text.Trim() + txtEmpID.Text.Trim()).ToString().ToLower(), ref status, 1);
                    if (status == false)
                    {
                        using (DataTable Dt = ViewState["tempConttypes"] != null ? (DataTable)ViewState["tempConttypes"] : ConTypesDt())
                        {
                            Dt.Rows.Add((lblcompCode.Text.Trim() + txtEmpID.Text.Trim()).ToString().ToLower(), ddlContType.SelectedItem, txtCoTypeID.Text.Trim());
                            ViewState["tempConttypes"] = Dt;
                        }
                        gv_tempConttypes.DataSource = (DataTable)ViewState["tempConttypes"];
                        gv_tempConttypes.DataBind();
                        ddlContType.SelectedIndex = -1;
                        txtCoTypeID.Text = "";
                        btnCumminfosubmit.Visible = gv_tempConttypes.Rows.Count > 0 ? true : false;
                        btninfo5.Visible = gv_tempConttypes.Rows.Count > 0 ? true : false;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Contact Info type already exists')", true);

                    }
                }

            }
            catch (Exception ex) { }
        }

        protected void feildClear()
        {
            //txtEmpID.Text = "";
            DDL_Salutation.SelectedIndex = -1;
            txtFName.Text = "";
            txtMName.Text = "";
            txtLName.Text = "";
            DDL_Gender.SelectedIndex = -1;
            txtDob.Text = "";
            ddl_materlSatuts.SelectedIndex = -1;
            txtFatherName.Text = "";
            txtMotherName.Text = "";
            txtSpouseName.Text = "";
            txtbankName.Text = "";
            txtAccNum.Text = "";
            txtIFSC.Text = "";
            txtBranch.Text = "";
            txtDistrict.Text = "";
            ddlBnkContry.SelectedIndex = -1;
            ddl_Bnkstate.SelectedIndex = -1;
            ddlAddType.SelectedIndex = -1;
            txtAddline1.Text = "";
            txtAddline2.Text = "";
            txtLocality.Text = "";
            txtAddDistrict.Text = "";
            ddlAddCountry.SelectedIndex = -1;
            ddlAddState.SelectedIndex = -1;
            txtAddPincode.Text = "";
            txtAddStd.Text = "";
            txtWardNum.Text = "";
            txtaddstdate.Text = "";
            txtaddenddate.Text = "";
            rbtnESI.SelectedIndex = -1;
            txtESINum.Text = "";
            txtESIDisp.Text = "";
            rbnPFApp.SelectedIndex = -1;
            txtPFNumer.Text = "";
           // txtPFNumDep.Text = "";
            chkrespf.Checked = true;
            rbtnZeroPens.SelectedIndex = -1;
            rbtnZeroPT.SelectedIndex = -1;
            ddlContType.SelectedIndex = -1;
            ddlDocType.SelectedIndex = -1;
            txtDocTypeID.Text = "";
            txtCoTypeID.Text = "";
            ddleDept.SelectedIndex = -1;
            txteBranch.Text = "";
            txtedivision.Text = "";
            txteDOJ.Text = "";
            txteGrade.Text = "";
            //txtEmpIDbnk.Text = "";
            //txtaddEmpID.Text = "";
            //txtBenEmpid.Text = "";
            //txtcummEMPID.Text = "";
            //txtDocEMPID.Text = "";

            lblmssgpi.Text = "";
            lblbif.Text = "";
            lblainfo.Text = "";
            lblbinfo.Text = "";

            ViewState["tempDoctypes"] = null;
            ViewState["tempConttypes"] = null;
            gv_tempDocTypes.DataSource = (DataTable)ViewState["tempDoctypes"];
            gv_tempDocTypes.DataBind();
            gv_tempConttypes.DataSource = (DataTable)ViewState["tempConttypes"];
            gv_tempConttypes.DataBind();
        }

        protected void btnClearAll_Click(object sender, EventArgs e)
        {
            feildClear();
            //gv_tempConttypes.Visible = gv_tempConttypes.Rows.Count > 0 ? true : false;
            //btnCumminfosubmit.Visible = gv_tempConttypes.Rows.Count > 0 ? true : false;
            //btninfo5.Visible = gv_tempConttypes.Rows.Count > 0 ? true : false;

            //gv_tempDocTypes.Visible = gv_tempDocTypes.Rows.Count > 0 ? true : false;
            //btnDocuntinfosubmit.Visible = gv_tempDocTypes.Rows.Count > 0 ? true : false;
            //btninfo6.Visible = gv_tempDocTypes.Rows.Count > 0 ? true : false;
        }

        protected void TemplateDwnld()
        {
            try
            {
                string strURL = "~/PayCompute_Files/Template/Employee_information.xlsx";
                WebClient req = new WebClient();
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.ContentType = "application/" + Path.GetExtension(strURL);
                response.AddHeader("Content-Disposition", "attachment;filename=\"" + Path.GetFileName(strURL) + "\"" + DateTime.Now.ToShortDateString() + "\"");
                byte[] data = req.DownloadData(Server.MapPath(strURL));
                response.BinaryWrite(data);
                response.End();

            }
            catch (Exception ex)
            { }
        }

        protected void lnkTemplDwnld_Click(object sender, EventArgs e)
        {
            TemplateDwnld();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmpID.Text == "")
                {
                    txtEmpID.Focus();
                }
                else
                {


                    lblmssgpi.Text = "";
                    configurationbo objBo = new configurationbo();
                    configurationbl bl = new configurationbl();
                    configurationcollectionbo objLst = new configurationcollectionbo();
                    bool? status = false;

                    bl.formvalidation(Session["CompCode"].ToString(), "", (lblcompCode.Text.Trim() + txtEmpID.Text.Trim()).ToString().ToLower(), ref status, 6);
                    if (status == false)
                    {
                        DataTable dtExcelData = EmpInfoDt();
                        EmoDataBo objempBo = new EmoDataBo();
                        EmpDataBL objBl = new EmpDataBL();
                        bool? st = false;

                        string sname = txtSpouseName.Visible == false ? "" : txtSpouseName.Text.Trim();

                        objempBo.compCode = Session["CompCode"].ToString();
                        objempBo.Employee_ID = lblcompCode.Text.Trim() + txtEmpID.Text.Trim().ToLower();
                        objempBo.Salutation = DDL_Salutation.SelectedItem.ToString().Trim();
                        objempBo.First_Name = txtFName.Text.Trim();
                        objempBo.Middle_Name = txtMName.Text.Trim();
                        objempBo.Last_Name = txtLName.Text.Trim();
                        objempBo.Gender = DDL_Gender.SelectedItem.ToString().Trim();
                        objempBo.Date_of_Birth = txtDob.Text.Trim();
                        objempBo.Material_Status = ddl_materlSatuts.SelectedItem.ToString().Trim();
                        objempBo.Father_Name = txtFatherName.Text.Trim();
                        objempBo.Mother_Name = txtMotherName.Text.Trim();
                        objempBo.Spouse_Name = sname.ToString().Trim();
                        objempBo.Bank_Name = "";
                        objempBo.Account_Number = "";
                        objempBo.IFSC_Code = "";
                        objempBo.Bank_Branch = "";
                        objempBo.Bank_District = "";
                        objempBo.Branch_Country = "";
                        objempBo.Branch_State = "";
                        objempBo.Address_Type = "";
                        objempBo.Residence_Number = "";
                        objempBo.Street = "";
                        objempBo.Locality = "";
                        objempBo.District = "";
                        objempBo.Country = "";
                        objempBo.State = "";
                        objempBo.Pincode = "";
                        objempBo.STD_Code = "";
                        objempBo.Ward_Number = "";
                        objempBo.ESI_Applicable = "";
                        objempBo.ESI_Number = "";
                        objempBo.ESI_Dispencary = "";
                        objempBo.PF_Applicable = "";
                        objempBo.PF_Number = "";
                        objempBo.PF_Number_Dept_File = "";
                        objempBo.Restrict_PF = "";
                        objempBo.Zero_Pension = "";
                        objempBo.Zero_PT = "";
                        objempBo.flg = 1;
                        objempBo.AppID = "";

                        objempBo.EDEPT = ddleDept.SelectedItem.ToString().Trim();
                        objempBo.EGRADE = txteGrade.Text.Trim();
                        objempBo.EBRANCH = txteBranch.Text.Trim();
                        objempBo.EDIVISION = txtedivision.Text.Trim();
                        objempBo.EDOJ = txteDOJ.Text.Trim();
                        objempBo._1 = "";
                        objempBo._10 = "";
                        objBl.Create_EmpInfo(objempBo, ref st);

                        if (st == true)
                        {
                            ViewState["tempDoctypes"] = null;
                            ViewState["tempConttypes"] = null;
                            feildClear(); ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Personal Info Saved Successfully')", true);
                            btnBankinfosubmit.Focus();
                        }
                    }
                    else
                    {
                        lblmssgpi.Text = "Employee's Personal Info already exists";

                    }
                    //Response.Redirect("https://localhost:44366/swagger");
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "')", true); }
        }

        protected void btnBankinfosubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmpID.Text == "")
                {
                    txtEmpID.Focus();
                }
                else
                {

                    lblbif.Text = "";
                    configurationbo objBo = new configurationbo();
                    configurationbl bl = new configurationbl();
                    configurationcollectionbo objLst = new configurationcollectionbo();
                    bool? status = false;

                    bl.formvalidation(Session["CompCode"].ToString(), txtAccNum.Text.ToString(), (lblcompCode.Text.Trim() + txtEmpID.Text.Trim()).ToString().ToLower(), ref status, 4);
                    if (status == false)
                    {
                        EmpDataBL objBl = new EmpDataBL();
                        DataTable dtExcelData = EmpInfoDt();
                        EmoDataBo objempBo = new EmoDataBo();
                        bool? st = false;


                        objempBo.compCode = Session["CompCode"].ToString();
                        objempBo.Employee_ID = lblcompCode.Text.Trim() + txtEmpID.Text.Trim().ToLower();
                        objempBo.Salutation = "";
                        objempBo.First_Name = "";
                        objempBo.Middle_Name = "";
                        objempBo.Last_Name = "";
                        objempBo.Gender = "";
                        objempBo.Date_of_Birth = "";
                        objempBo.Material_Status = "";
                        objempBo.Father_Name = "";
                        objempBo.Mother_Name = "";
                        objempBo.Spouse_Name = "";
                        objempBo.Bank_Name = txtbankName.Text.Trim();
                        objempBo.Account_Number = txtAccNum.Text.Trim();
                        objempBo.IFSC_Code = txtIFSC.Text.Trim();
                        objempBo.Bank_Branch = txtBranch.Text.Trim();
                        objempBo.Bank_District = txtDistrict.Text.Trim();
                        objempBo.Branch_Country = ddlBnkContry.SelectedItem.ToString().Trim();
                        objempBo.Branch_State = ddl_Bnkstate.SelectedItem.ToString().Trim();
                        objempBo.Address_Type = "";
                        objempBo.Residence_Number = "";
                        objempBo.Street = "";
                        objempBo.Locality = "";
                        objempBo.District = "";
                        objempBo.Country = "";
                        objempBo.State = "";
                        objempBo.Pincode = "";
                        objempBo.STD_Code = "";
                        objempBo.Ward_Number = "";
                        objempBo.ESI_Applicable = "";
                        objempBo.ESI_Number = "";
                        objempBo.ESI_Dispencary = "";
                        objempBo.PF_Applicable = "";
                        objempBo.PF_Number = "";
                        objempBo.PF_Number_Dept_File = "";
                        objempBo.Restrict_PF = "";
                        objempBo.Zero_Pension = "";
                        objempBo.Zero_PT = "";
                        objempBo.flg = 1;
                        objempBo.AppID = "";

                        objempBo.EDEPT = "";
                        objempBo.EGRADE = "";
                        objempBo.EBRANCH = "";
                        objempBo.EDIVISION = "";
                        objempBo.EDOJ = "";
                        objempBo._1 = "";
                        objempBo._10 = "";
                        objBl.Create_EmpInfo(objempBo, ref st);

                        if (st == true)
                        {
                            ViewState["tempDoctypes"] = null;
                            ViewState["tempConttypes"] = null;
                            feildClear(); ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Bank Info Saved Successfully')", true);
                            btnAddressinfoSubmit.Focus();
                        }
                    }
                    else
                    {
                        lblbif.Text = "Employee's Bank Info already exists";

                    }
                }
            }
            catch (Exception ex) { }
        }

        protected void btnAddressinfoSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmpID.Text == "")
                {
                    txtEmpID.Focus();
                }
                else
                {
                    lblainfo.Text = "";
                    configurationbo objBo = new configurationbo();
                    configurationbl bl = new configurationbl();
                    configurationcollectionbo objLst = new configurationcollectionbo();
                    bool? status = false;

                    bl.formvalidation(Session["CompCode"].ToString(), ddlAddType.SelectedValue.ToString(), (lblcompCode.Text.Trim() + txtEmpID.Text.Trim()).ToString().ToLower(), ref status, 2);
                    if (status == false)
                    {
                        EmpDataBL objBl = new EmpDataBL();
                        DataTable dtExcelData = EmpInfoDt();
                        EmoDataBo objempBo = new EmoDataBo();
                        bool? st = false;


                        objempBo.compCode = Session["CompCode"].ToString();
                        objempBo.Employee_ID = lblcompCode.Text.Trim() + txtEmpID.Text.Trim().ToLower();
                        objempBo.Salutation = "";
                        objempBo.First_Name = "";
                        objempBo.Middle_Name = "";
                        objempBo.Last_Name = "";
                        objempBo.Gender = "";
                        objempBo.Date_of_Birth = "";
                        objempBo.Material_Status = "";
                        objempBo.Father_Name = "";
                        objempBo.Mother_Name = "";
                        objempBo.Spouse_Name = "";
                        objempBo.Bank_Name = "";
                        objempBo.Account_Number = "";
                        objempBo.IFSC_Code = "";
                        objempBo.Bank_Branch = "";
                        objempBo.Bank_District = "";
                        objempBo.Branch_Country = "";
                        objempBo.Branch_State = "";
                        objempBo.Address_Type = ddlAddType.SelectedItem.ToString().Trim();
                        objempBo.Residence_Number = txtAddline1.Text.Trim();
                        objempBo.Street = txtAddline2.Text.Trim();
                        objempBo.Locality = txtLocality.Text.Trim();
                        objempBo.District = txtAddDistrict.Text.Trim();
                        objempBo.Country = ddlAddCountry.SelectedItem.ToString().Trim();
                        objempBo.State = ddlAddState.SelectedItem.ToString().Trim();
                        objempBo.Pincode = txtAddPincode.Text.Trim();
                        objempBo.STD_Code = txtAddStd.Text.Trim();
                        objempBo.Ward_Number = txtWardNum.Text.Trim();
                        objempBo.ESI_Applicable = "";
                        objempBo.ESI_Number = "";
                        objempBo.ESI_Dispencary = "";
                        objempBo.PF_Applicable = "";
                        objempBo.PF_Number = "";
                        objempBo.PF_Number_Dept_File = "";
                        objempBo.Restrict_PF = "";
                        objempBo.Zero_Pension = "";
                        objempBo.Zero_PT = "";
                        objempBo.flg = 1;
                        objempBo.AppID = "";

                        objempBo.EDEPT = "";
                        objempBo.EGRADE = "";
                        objempBo.EBRANCH = "";
                        objempBo.EDIVISION = "";
                        objempBo.EDOJ = "";
                        objempBo._1 = txtaddstdate.Text.Trim();
                        objempBo._10 = txtaddenddate.Text.Trim();
                        objBl.Create_EmpInfo(objempBo, ref st);

                        if (st == true)
                        {
                            ViewState["tempDoctypes"] = null;
                            ViewState["tempConttypes"] = null;
                            feildClear();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Address Info Saved Successfully')", true);
                            btnBeniftsinfosubmit.Focus();
                        }
                    }
                    else
                    {
                        lblainfo.Text = "Employee's Address Info already exists";

                    }
                }
            }
            catch (Exception ex) { }
        }

        protected void btnBeniftsinfosubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmpID.Text == "")
                {
                    txtEmpID.Focus();
                }
                else
                {
                    lblbinfo.Text = "";
                    configurationbo objBo = new configurationbo();
                    configurationbl bl = new configurationbl();
                    configurationcollectionbo objLst = new configurationcollectionbo();
                    bool? status = false;

                    bl.formvalidation(Session["CompCode"].ToString(), "", (lblcompCode.Text.Trim() + txtEmpID.Text.Trim()).ToString().ToLower(), ref status, 5);
                    if (status == false)
                    {
                        EmpDataBL objBl = new EmpDataBL();
                        DataTable dtExcelData = EmpInfoDt();
                        EmoDataBo objempBo = new EmoDataBo();
                        bool? st = false;

                        string pfress = "";
                        pfress = chkrespf.Checked == true ? "1" : "0";

                        string esinul = "";
                        esinul = rbtnESI.SelectedValue == "Yes" ? txtESINum.Text.Trim() : "";

                        string esidisp = "";
                        esidisp = rbtnESI.SelectedValue == "Yes" ? txtESIDisp.Text.Trim() : "";

                        string fpnum = "";
                        fpnum = rbnPFApp.SelectedValue == "Yes" ? txtPFNumer.Text.Trim() : "";

                        string pfresval = rbnPFApp.SelectedValue == "Yes" ? (chkrespf.Checked == true ? "1" : "0") : "0";

                        objempBo.compCode = Session["CompCode"].ToString();
                        objempBo.Employee_ID = lblcompCode.Text.Trim() + txtEmpID.Text.Trim().ToString().ToLower();
                        objempBo.Salutation = "";
                        objempBo.First_Name = "";
                        objempBo.Middle_Name =
                        objempBo.Last_Name =
                        objempBo.Gender =
                        objempBo.Date_of_Birth =
                        objempBo.Material_Status =
                        objempBo.Father_Name =
                        objempBo.Mother_Name =
                        objempBo.Spouse_Name =
                        objempBo.Bank_Name =
                        objempBo.Account_Number =
                        objempBo.IFSC_Code =
                        objempBo.Bank_Branch =
                        objempBo.Bank_District =
                        objempBo.Branch_Country =
                        objempBo.Branch_State =
                        objempBo.Address_Type =
                        objempBo.Residence_Number =
                        objempBo.Street =
                        objempBo.Locality =
                        objempBo.District =
                        objempBo.Country =
                        objempBo.State =
                        objempBo.Pincode =
                        objempBo.STD_Code =
                        objempBo.Ward_Number = "";
                        objempBo.ESI_Applicable = rbtnESI.SelectedValue.ToString().Trim();
                        objempBo.ESI_Number = esinul.ToString().Trim();
                        objempBo.ESI_Dispencary = esidisp.ToString().Trim();
                        objempBo.PF_Applicable = rbnPFApp.SelectedValue.ToString().Trim();
                        objempBo.PF_Number = fpnum.ToString().Trim();
                        objempBo.PF_Number_Dept_File = "";
                        objempBo.Restrict_PF = pfress.ToString().Trim();
                        objempBo.Zero_Pension = rbtnZeroPens.SelectedValue.ToString().Trim();
                        objempBo.Zero_PT = rbtnZeroPT.SelectedValue.ToString().Trim();
                        objempBo.flg = 1;
                        objempBo.AppID = "";

                        objempBo.EDEPT =
                        objempBo.EGRADE =
                        objempBo.EBRANCH =
                        objempBo.EDIVISION =
                        objempBo.EDOJ = "";
                        objempBo._1 = "";
                        objempBo._10 = "";
                        objBl.Create_EmpInfo(objempBo, ref st);

                        if (st == true)
                        {
                            ViewState["tempDoctypes"] = null;
                            ViewState["tempConttypes"] = null;
                            feildClear(); ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Benefits Info Saved Successfully')", true);
                            btnCumminfosubmit.Focus();                            
                        }
                    }
                    else
                    {
                        lblbinfo.Text = "Employee's Benefits Info already exists";

                    }
                }

                loadcommutyps();
            }
            catch (Exception ex) { }
        }

        public void loadcommutyps()
        {
            try
            {
                configurationcollectionbo objLst3 = configurationbl.Get_ContactTypes(1);
                ddlContType.DataSource = objLst3;
                ddlContType.DataTextField = "DDLTYPETEXT";
                ddlContType.DataValueField = "DDLTYPE";
                ddlContType.DataBind();
                ddlContType.SelectedValue = "2";
            }
            catch (Exception ex) { }
        }

        protected void btnCumminfosubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmpID.Text == "")
                {
                    txtEmpID.Focus();
                }
                else
                {
                    configurationbo objBo = new configurationbo();
                    configurationbl bl = new configurationbl();
                    configurationcollectionbo objLst = new configurationcollectionbo();
                    bool? status = false;

                    bl.formvalidation(Session["CompCode"].ToString(), ddlContType.SelectedValue.ToString(), (lblcompCode.Text.Trim() + txtEmpID.Text.Trim()).ToString().ToLower(), ref status, 1);
                    if (status == false)
                    {

                        EmpDataBL objBl = new EmpDataBL();
                        EmoDataBo bo2 = new EmoDataBo();
                        DataTable dt2 = new DataTable();
                        bool? st1 = false;
                        bo2.compCode = Session["CompCode"].ToString();
                        bo2.Employee_ID = (lblcompCode.Text.Trim() + txtEmpID.Text.Trim()).ToString().ToLower();
                        bo2.TypeID = ddlContType.SelectedItem.ToString().Trim();
                        bo2.ID = txtCoTypeID.Text.ToString().Trim();
                        bo2.flg = 1;
                        bo2.loginID = "";
                        objBl.Create_Doc_Contypes(bo2, ref st1);
                       
                        if (st1 == true)
                        {
                            feildClear();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Communication Info Saved Successfully')", true);
                            btnDocuntinfosubmit.Focus();                            
                        }
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Contact Info type already exists for this employee')", true);

                    }
                }

                loadcommutyps();
            }

            catch (Exception ex) { }
        }

        protected void btnDocuntinfosubmit_Click(object sender, EventArgs e)
        {
            try
            {
                lbldocupmssg.Text = "";
                if (txtEmpID.Text == "")
                {
                    txtEmpID.Focus();
                }
                else
                {

                    configurationbo objBo = new configurationbo();
                    configurationbl bl = new configurationbl();
                    configurationcollectionbo objLst = new configurationcollectionbo();
                    bool? status = false;

                    bool? st2 = false;
                    EmpDataBL objBl = new EmpDataBL();
                    EmoDataBo bo3 = new EmoDataBo();

                    DateTime dt = DateTime.Now;
                    string dtt = "";
                    string filePath = "";
                    string ImgPath = "";
                    string ImgExtup = Path.GetExtension(file_docupload.FileName.ToString().ToUpper());
                    //local path = F:/Paycompute_Leavemodule/PRD02032020/iemppower-NRR/iEmpPower/PayCompute_Files/Payc_Empdocuments/
                    //quality path (80 port)= C:/inetpub/wwwroot/iEmp_SubexNewUI/PayCompute_Files/Payc_Empdocuments/
                    //testing (88 port) = C:/inetpub/wwwroot/iEmp_Test/PayCompute_Files/Payc_Empdocuments/
                    //PRD path (90 port)= C:/inetpub/wwwroot/TicketingTool_Test/PayCompute_Files/Payc_Empdocuments/

                    if (file_docupload.HasFile)
                    {
                        if (ImgExtup == ".JPG" | ImgExtup == ".JPEG" | ImgExtup == ".PDF" | ImgExtup == ".PNG")
                        {
                            if (ddlDocType.SelectedValue == "15")
                            {
                                if (ImgExtup == ".JPG" | ImgExtup == ".JPEG" | ImgExtup == ".PNG")
                                {

                                    dtt = dt.ToString("yyyy-MM-dd hh:mm:ss").Replace(':', '-');
                                    filePath = "C:/inetpub/wwwroot/TicketingTool_Test/PayCompute_Files/Payc_Empdocuments/"
                                       + (lblcompCode.Text.Trim() + txtEmpID.Text.Trim()).ToString().ToLower() + "-" + ddlDocType.SelectedItem.ToString().Trim() + "-" + dtt.Trim() + Path.GetExtension(file_docupload.FileName);
                                    ImgPath = "C:/inetpub/wwwroot/TicketingTool_Test/PayCompute_Files/Payc_Empdocuments/"
                                        + (lblcompCode.Text.Trim() + txtEmpID.Text.Trim()).ToString().ToLower() + "-" + ddlDocType.SelectedItem.ToString().Trim() + "-" + dtt.Trim() + Path.GetExtension(file_docupload.FileName);
                                    file_docupload.PostedFile.SaveAs(filePath);




                                    bl.formvalidation(Session["CompCode"].ToString(), ddlDocType.SelectedValue.ToString(), (lblcompCode.Text.Trim() + txtEmpID.Text.Trim()).ToString().ToLower(), ref status, 3);
                                    if (status == false)
                                    {

                                        bo3.compCode = Session["CompCode"].ToString();
                                        bo3.Employee_ID = (lblcompCode.Text.Trim() + txtEmpID.Text.Trim()).ToString().ToLower();
                                        bo3.TypeID = ddlDocType.SelectedItem.ToString().Trim();
                                        bo3.ID = txtDocTypeID.Text.Trim();
                                        bo3.flg = 2;
                                        bo3.loginID = "";
                                        bo3._10 = ImgPath.ToString().Trim();
                                        objBl.Create_Doc_Contypes(bo3, ref st2);

                                        if (st2 == true)
                                        {
                                            feildClear();
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Document Info Saved Successfully')", true);
                                            txtEmpID.Text = "";
                                            lbldocupmssg.Text = "";
                                            txtEmpID.Focus();
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Document Info already exists for this employee')", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Only JPEG,JPG,PNG formats are allowed to upload employee photo..!')", true);
                                }

                            }
                            else
                            {
                                if (ImgExtup == ".PDF")
                                {

                                    dtt = dt.ToString("yyyy-MM-dd hh:mm:ss").Replace(':', '-');
                                    filePath = "C:/inetpub/wwwroot/TicketingTool_Test/PayCompute_Files/Payc_Empdocuments/"
                                       + (lblcompCode.Text.Trim() + txtEmpID.Text.Trim()).ToString().ToLower() + "-" + ddlDocType.SelectedItem.ToString().Trim() + "-" + dtt.Trim() + Path.GetExtension(file_docupload.FileName);
                                    ImgPath = "C:/inetpub/wwwroot/TicketingTool_Test/PayCompute_Files/Payc_Empdocuments/"
                                        + (lblcompCode.Text.Trim() + txtEmpID.Text.Trim()).ToString().ToLower() + "-" + ddlDocType.SelectedItem.ToString().Trim() + "-" + dtt.Trim() + Path.GetExtension(file_docupload.FileName);
                                    file_docupload.PostedFile.SaveAs(filePath);




                                    bl.formvalidation(Session["CompCode"].ToString(), ddlDocType.SelectedValue.ToString(), (lblcompCode.Text.Trim() + txtEmpID.Text.Trim()).ToString().ToLower(), ref status, 3);
                                    if (status == false)
                                    {

                                        bo3.compCode = Session["CompCode"].ToString();
                                        bo3.Employee_ID = (lblcompCode.Text.Trim() + txtEmpID.Text.Trim()).ToString().ToLower();
                                        bo3.TypeID = ddlDocType.SelectedItem.ToString().Trim();
                                        bo3.ID = txtDocTypeID.Text.Trim();
                                        bo3.flg = 2;
                                        bo3.loginID = "";
                                        bo3._10 = ImgPath.ToString().Trim();
                                        objBl.Create_Doc_Contypes(bo3, ref st2);

                                        if (st2 == true)
                                        {
                                            feildClear();
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Document Info Saved Successfully')", true);
                                            txtEmpID.Text = "";
                                            lbldocupmssg.Text = "";
                                            txtEmpID.Focus();
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Document Info already exists for this employee')", true);
                                    }
                                }

                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Only PDF formats are allowed to upload..!')", true);
                                }

                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Only PDF and Image formats are allowed to upload..!')", true);
                        }
                    }

                    else
                    {
                        bl.formvalidation(Session["CompCode"].ToString(), ddlDocType.SelectedValue.ToString(), (lblcompCode.Text.Trim() + txtEmpID.Text.Trim()).ToString().ToLower(), ref status, 3);
                        if (status == false)
                        {

                            bo3.compCode = Session["CompCode"].ToString();
                            bo3.Employee_ID = (lblcompCode.Text.Trim() + txtEmpID.Text.Trim()).ToString().ToLower();
                            bo3.TypeID = ddlDocType.SelectedItem.ToString().Trim();
                            bo3.ID = txtDocTypeID.Text.Trim();
                            bo3.flg = 2;
                            bo3.loginID = "";
                            bo3._10 = ImgPath.ToString().Trim();
                            objBl.Create_Doc_Contypes(bo3, ref st2);

                            if (st2 == true)
                            {
                                feildClear();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Document Info Saved Successfully')", true);
                                txtEmpID.Text = "";
                                lbldocupmssg.Text = "";
                                txtEmpID.Focus();
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Document Info already exists for this employee')", true);
                        }
                    }
                    loadcommutyps();
                }
            }
            catch (Exception ex) { }
        }

        protected void Tab1_Click(object sender, EventArgs e)
        {
            HideTabs();
            view1.Visible = true;
            view2.Visible = false;
            Tab1.CssClass = "nav-link active p-2";
        }

        protected void Tab2_Click(object sender, EventArgs e)
        {
            HideTabs();
            view1.Visible = false;
            view2.Visible = true;
            Tab2.CssClass = "nav-link active p-2";
            //HideSubTabs();
            //info1.Visible = true;
            //Infotab1.CssClass = "nav-link active p-2";
        }

        protected void HideTabs()
        {
            lblmssgpi.Text = "";
            lblbif.Text = "";
            lblainfo.Text = "";
            lblbinfo.Text = "";
            view1.Visible = false;
            view2.Visible = false;

            Tab1.CssClass = "nav-link  p-2";
            Tab2.CssClass = "nav-link  p-2";
            //Tab3.CssClass = "nav-link  p-2";

        }

        //protected void Infotab1_Click(object sender, EventArgs e)
        //{
        //    HideSubTabs();
        //    info1.Visible = true;
        //    info2.Visible = false;
        //    info3.Visible = false;
        //    info4.Visible = false;
        //    info5.Visible = false;
        //    info6.Visible = false;
        //    Infotab1.CssClass = "nav-link active p-2";
        //}

        //protected void Infotab2_Click(object sender, EventArgs e)
        //{
        //    HideSubTabs();
        //    info1.Visible = false;
        //    info2.Visible = true;
        //    info3.Visible = false;
        //    info4.Visible = false;
        //    info5.Visible = false;
        //    info6.Visible = false;
        //    Infotab2.CssClass = "nav-link active p-2";
        //}

        //protected void Infotab3_Click(object sender, EventArgs e)
        //{
        //    HideSubTabs();
        //    info1.Visible = false;
        //    info2.Visible = false;
        //    info3.Visible = true;
        //    info4.Visible = false;
        //    info5.Visible = false;
        //    info6.Visible = false;
        //    Infotab3.CssClass = "nav-link active p-2";
        //}

        //protected void Infotab4_Click(object sender, EventArgs e)
        //{
        //    HideSubTabs();
        //    info1.Visible = false;
        //    info2.Visible = false;
        //    info3.Visible = false;
        //    info4.Visible = true;
        //    info5.Visible = false;
        //    info6.Visible = false;
        //    Infotab4.CssClass = "nav-link active p-2";
        //}

        //protected void Infotab5_Click(object sender, EventArgs e)
        //{
        //    HideSubTabs();
        //    info1.Visible = false;
        //    info2.Visible = false;
        //    info3.Visible = false;
        //    info4.Visible = false;
        //    info5.Visible = true;
        //    info6.Visible = false;
        //    Infotab5.CssClass = "nav-link active p-2";
        //}

        //protected void Infotab6_Click(object sender, EventArgs e)
        //{
        //    HideSubTabs();
        //    info1.Visible = false;
        //    info2.Visible = false;
        //    info3.Visible = false;
        //    info4.Visible = false;
        //    info5.Visible = false;
        //    info6.Visible = true;
        //    Infotab6.CssClass = "nav-link active p-2";
        //}

        //protected void HideSubTabs()
        //{
        //    lblmssgpi.Text = "";
        //    lblbif.Text = "";
        //    lblainfo.Text = "";
        //    lblbinfo.Text = "";
        //    info1.Visible = false;
        //    info2.Visible = false;
        //    info3.Visible = false;
        //    info4.Visible = false;
        //    info5.Visible = false;
        //    info6.Visible = false;

        //    Infotab1.CssClass = "nav-link  p-2";
        //    Infotab2.CssClass = "nav-link  p-2";
        //    Infotab3.CssClass = "nav-link  p-2";
        //    Infotab4.CssClass = "nav-link  p-2";
        //    Infotab5.CssClass = "nav-link  p-2";
        //    Infotab6.CssClass = "nav-link  p-2";

        //}

        protected void btnadddept_Click(object sender, EventArgs e)
        {
            try
            {
                configurationbo objBo = new configurationbo();
                configurationbl bl = new configurationbl();
                configurationcollectionbo objLst = new configurationcollectionbo();
                bool? status = false;
                 objBo.Descrip=txtaddpt.Text.Trim();
                 objBo.Company_Code=Session["CompCode"].ToString().ToLower().Trim();
                 objBo.flag=1;
                bl.Add_deptmasters(objBo, ref status);
                if (status == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Department added successfully');", true);
                    txtaddpt.Text = "";
                    getdept();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Department already exists');", true);
                }
            }
            catch (Exception ex) { }
        }

        public void getdept()
        {
            try
            {

                configurationcollectionbo objLst5 = configurationbl.Get_Departments(Session["CompCode"].ToString().ToLower().Trim(), 1);
                ddleDept.DataSource = objLst5;
                ddleDept.DataTextField = "deptdesc";
                ddleDept.DataValueField = "ID";
                ddleDept.DataBind();
                ddleDept.Items.Insert(0, new ListItem(" - SELECT - ", "0"));
            }
            catch (Exception ex) { }
        }

        protected void ddl_materlSatuts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_materlSatuts.SelectedValue == "0" || ddl_materlSatuts.SelectedValue == "6" || ddl_materlSatuts.SelectedValue == "4")
                {
                    txtSpouseName.Visible = false;
                }               
                else
                {
                    txtSpouseName.Visible = true;
                }
            }
            catch (Exception ex) { }
        }

        protected void ddlDocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlDocType.SelectedValue == "15")
                {
                    txtDocTypeID.Enabled = false;
                    RQF_docdescrpn.Enabled = false;
                }
                else
                {
                    txtDocTypeID.Enabled = true;
                    RQF_docdescrpn.Enabled = true;
                }
            }
            catch (Exception ex) { }
        }

        //protected void Tab3_Click(object sender, EventArgs e)
        //{
        //    HideTabs();
        //    Tab3.CssClass = "nav-link active p-2";
        //    HideSubTabs();
        //    Response.Redirect("~/UI/Configuration/UpdateEmpInfo.aspx");
        //}
    }
}