using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Performance_Management_System;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Manager_Self_Service;
using System.Threading;
using System.Configuration;

namespace iEmpPower.UI.Performance_Management_System
{
    public partial class frmPMSUpload : System.Web.UI.Page
    {
        string path = ConfigurationManager.AppSettings["PMS"];
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    PageLoadEvents();
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        #region Page load events
        private void PageLoadEvents()
        {
            try
            {

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }
        #endregion


        #region User Defined functions
        private void MsgCls(string Msg, Label Lbl, Color Clr)
        {
            try
            {
                Lbl.Text = string.Empty;
                Lbl.Text = Msg;
                Lbl.ForeColor = Clr;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        #region Get Email Body
        private string GetEmpUploadEmailBody(string DocID)
        {
            try
            {
                using (LoginView LVMpageEmpName = (LoginView)Master.FindControl("HeadLoginView"))
                using (Label LblEmpName = LVMpageEmpName.FindControl("lblEmployyeName") as Label)
                {
                    //string Pagename = Server.MapPath("~/MITCEnquiry.html");
                    string EmpUploadFilePath = Server.MapPath(@"." + "/EmailTemplates/EmpUploadEmailTemplate.html");
                    string Mailbody = System.IO.File.ReadAllText(EmpUploadFilePath);
                    Mailbody = Mailbody.Replace("##HEADING##", "Performance Appraisal Review 2018-19 for " + Session["EmployeeName"].ToString() + " | " + User.Identity.Name + " is pending for your action, and is now available in your performance appraisal manager inbox(MENU -> Employee Performance -> Download Appraisal Form)");
                    Mailbody = Mailbody.Replace("##EMPNAME_HEAD##", Session["EmployeeName"].ToString());
                    Mailbody = Mailbody.Replace("##EMPPERNR_HEAD##", User.Identity.Name);
                    Mailbody = Mailbody.Replace("##EMPNAME##", Session["EmployeeName"].ToString());
                    Mailbody = Mailbody.Replace("##EMPPERNR##", User.Identity.Name);
                    //Mailbody = Mailbody.Replace("##DOCID##", DocID.Contains('.') ? DocID.Split('.')[0] : DocID);
                    Mailbody = Mailbody.Replace("##SENEDATE##", DateTime.Now.ToString("dddd, dd MMM yyyy - hh:mm:ss"));
                    return Mailbody;
                }
            }
            catch (Exception Ex)
            { throw Ex; }
        }

        private string GetEmpUploadEmailBody_mgr(string DocID)
        {
            try
            {
                using (LoginView LVMpageEmpName = (LoginView)Master.FindControl("HeadLoginView"))
                using (Label LblEmpName = LVMpageEmpName.FindControl("lblEmployyeName") as Label)
                {
                    //string Pagename = Server.MapPath("~/MITCEnquiry.html");
                    string EmpUploadFilePath = Server.MapPath(@"." + "/EmailTemplates/EmpUploadEmailTemplate.html");
                    string Mailbody = System.IO.File.ReadAllText(EmpUploadFilePath);
                    Mailbody = Mailbody.Replace("##HEADING##", "Performance Appraisal Review 2018-19 for " + ViewState["ENAME"].ToString() + " | " + ViewState["PERNR"].ToString() + " is uploaded by manager " + Session["EmployeeName"].ToString() + " | " + User.Identity.Name);
                    Mailbody = Mailbody.Replace("##EMPNAME_HEAD##", Session["EmployeeName"].ToString());
                    Mailbody = Mailbody.Replace("##EMPPERNR_HEAD##", User.Identity.Name);
                    Mailbody = Mailbody.Replace("##EMPNAME##", ViewState["ENAME"].ToString());
                    Mailbody = Mailbody.Replace("##EMPPERNR##", ViewState["PERNR"].ToString());
                    //Mailbody = Mailbody.Replace("##DOCID##", DocID.Contains('.') ? DocID.Split('.')[0] : DocID);
                    Mailbody = Mailbody.Replace("##SENEDATE##", DateTime.Now.ToString("dddd, dd MMM yyyy - hh:mm:ss"));
                    return Mailbody;
                }
            }
            catch (Exception Ex)
            { throw Ex; }
        }
        #endregion

        public string GetEmail(string strPERNR)
        {
            string strEmail = string.Empty;
            try
            {
                PMSDataContext objo = new PMSDataContext();
                foreach (var vRow in objo.sp_get_Person_Emailid(strPERNR))
                {
                    if (!string.IsNullOrEmpty(vRow.USRID.Trim().ToString()))
                        strEmail = vRow.USRID.Trim().ToString();
                }
                objo.Dispose();

                return strEmail;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); return strEmail; }
        }
        #endregion

        //--------------------- UPLOAD EMPLOYEE (SELE) UPRAISAL - PDF ------------------------------ START --------
        #region Upload Employee Upraisal
        protected void BtnEmpUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (FU_EmpUpload.PostedFile != null && !string.IsNullOrEmpty(FU_EmpUpload.PostedFile.FileName))
                {
                    string FileExt = Path.GetExtension(FU_EmpUpload.FileName.ToString().ToUpper());
                    if (FileExt.ToUpper() == ".PDF")
                    {
                        string EmpUploadFilePath = Server.MapPath(@"." + "/Exported/MSS/" + FU_EmpUpload.FileName);
                        string EmpUploadFilePath1 = Server.MapPath(@"." + "/Exported/Final-Templates/" + FU_EmpUpload.FileName);
                        string filechkname = Path.GetFileName(EmpUploadFilePath);
                        if (filechkname.Substring(0, 14) == (User.Identity.Name + DateTime.Now.Year + DateTime.Now.ToString("MM")))
                        {

                            if ((!File.Exists(EmpUploadFilePath)) && (!File.Exists(EmpUploadFilePath1)))
                            {

                                string strMailToList = string.Empty, strMailCCList = string.Empty;

                                //string Pernr = "";
                                //string SuperVisorPernr = "";
                                //string HRPernr = "";
                                string HRM_MAIL = "";
                                string PernrEmail = "";
                                string SuperVisorEmail = "";

                                string HREmail = "";

                                msassignedtomedalDataContext objPIAssignTMDataContext = new msassignedtomedalDataContext();
                                objPIAssignTMDataContext.usp_PMS_get_mailIDS(User.Identity.Name.Trim()
                                     , ref  SuperVisorEmail
                                     , ref  HREmail
                                     , ref HRM_MAIL
                                    , ref  PernrEmail);
                                FU_EmpUpload.SaveAs(EmpUploadFilePath);

                                strMailToList = SuperVisorEmail;
                                strMailCCList = HREmail + ", " + HRM_MAIL + ", " + PernrEmail;
                                //         Thread email = new Thread(delegate()
                                //{
                                iEmpPowerMaster_Load.masterbl.DispatchMail(strMailToList, strMailCCList, "Performance Appraisal Review 2018-19 for " + User.Identity.Name + " | " + Session["EmployeeName"].ToString() + " is pending for your action."
                                    , strMailCCList, GetEmpUploadEmailBody(FU_EmpUpload.FileName));
                                //});
                                //         email.IsBackground = true;
                                //         email.Start();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('File uploaded successfully !');", true);
                                MsgCls("File uploaded successfully !", LblEmpUploadMsg, Color.Green);

                                if (File.Exists(Path.Combine(Server.MapPath(@"." + "/Exported/MSS/"), Path.GetFileName(EmpUploadFilePath))))
                                {

                                    string fullName = "";
                                    string partialName = User.Identity.Name.Trim() + DateTime.Now.Year + DateTime.Now.ToString("MM");
                                    // string startupPath = System.IO.Directory.GetCurrentDirectory();
                                    DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(path + "/ESS");
                                    FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles("*" + partialName + "*.*");

                                    foreach (FileInfo foundFile in filesInDir)
                                    {
                                        fullName = foundFile.FullName;
                                        File.Delete(Path.Combine(Server.MapPath(@"." + "/Exported/ESS/"), Path.GetFileName(fullName)));
                                    }
                                    //do
                                    //{

                                    //    // If file found, delete it    
                                    //    File.Delete(Path.Combine(Server.MapPath(@"." + "/Exported/ESS/"), Path.GetFileName(EmpUploadFilePath)));
                                    //} while (File.Exists(Path.Combine(Server.MapPath(@"." + "/Exported/ESS/"), Path.GetFileName(EmpUploadFilePath))));
                                    //Console.WriteLine("File deleted.");
                                }
                            }
                            else
                            { throw new Exception("-02"); }
                        }
                        else
                        { throw new Exception("-01"); }

                    }
                    else
                    { throw new Exception("-02"); }

                }
                else
                { throw new Exception("-01"); }
            }
            catch (Exception Ex)
            {
                switch (Ex.Message)
                {
                    case "-03":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Cannot upload other's Appraisal form, Invalid Document !');", true);
                        MsgCls("Cannot upload other's Appraisal form, Invalid Document !", LblEmpUploadMsg, Color.Red);
                        break;
                    case "-01":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Appraisal is not available. Invalid Document name !');", true);
                        MsgCls("Appraisal is not available. Invalid Document name !", LblEmpUploadMsg, Color.Red);
                        break;
                    case "-02":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You have already submitted..!');", true);
                        MsgCls("You have already submitted..!", LblEmpUploadMsg, Color.Red);
                        break;
                    default:
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true);
                        break;
                }
            }
        }
        #endregion
        //--------------------- UPLOAD EMPLOYEE (SELE) UPRAISAL - PDF ------------------------------ END ----------

        //--------------------- UPLOAD MANAGER UPRAISAL --------- PDF ------------------------------ START --------
        #region Upload Manager Upraisal PDF
        protected void BtnMngrUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (FU_MngrUpload.PostedFile != null && !string.IsNullOrEmpty(FU_MngrUpload.PostedFile.FileName))
                {
                    string FileExt = Path.GetExtension(FU_MngrUpload.FileName.ToString().ToUpper());
                    if (FileExt.ToUpper() == ".PDF")
                    {
                        string EmpUploadFilePath = Server.MapPath(@"." + "/Exported/Final-Templates/" + FU_MngrUpload.FileName);
                        bool valid = false;
                        msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
                        msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
                        objPIDashBoardBo.PERNR = User.Identity.Name;
                        msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_Manager(objPIDashBoardBo);
                        string fullName = Path.GetFileName(EmpUploadFilePath).Substring(0, 14);
                        if (objPIDashBoardLst.Count > 0)
                        {
                            foreach (msassignedtomebo m in objPIDashBoardLst)
                            {
                                string partialName = m.PERNR.ToString().Trim() + (DateTime.Now.Year + DateTime.Now.ToString("MM"));

                                if (partialName == fullName)
                                {
                                    ViewState["PERNR"] = m.PERNR.ToString().Trim();
                                    ViewState["ENAME"] = m.ENAME.ToString().Trim();
                                    valid = true;
                                }
                            }

                        }
                        if (valid == true)
                        {
                            if (!File.Exists(EmpUploadFilePath))
                            {

                                string strMailToList = string.Empty, strMailCCList = string.Empty;
                                string up_PERNR = FU_MngrUpload.FileName.Substring(0, 8);
                                //string Pernr = "";
                                //string SuperVisorPernr = "";
                                //string HRPernr = "";
                                string HRM_MAIL = "";
                                string PernrEmail = "";
                                string SuperVisorEmail = "";

                                string HREmail = "";

                                msassignedtomedalDataContext objPIAssignTMDataContext = new msassignedtomedalDataContext();
                                objPIAssignTMDataContext.usp_PMS_get_mailIDS(up_PERNR
                                     , ref  SuperVisorEmail
                                     , ref  HREmail
                                     , ref HRM_MAIL
                                    , ref  PernrEmail);
                                FU_MngrUpload.SaveAs(EmpUploadFilePath);

                                strMailToList = HREmail;
                                strMailCCList = SuperVisorEmail + ", " + HRM_MAIL + ", " + PernrEmail;
                                //          Thread email = new Thread(delegate()
                                //{
                                iEmpPowerMaster_Load.masterbl.DispatchMail(strMailToList, strMailCCList, "Performance Appraisal Review 2018-19 for " + ViewState["PERNR"].ToString() + " | " + ViewState["PERNR"].ToString() + " uploaded by manager: " + User.Identity.Name + " | " + Session["EmployeeName"].ToString() + "."
                                    , strMailCCList, GetEmpUploadEmailBody_mgr(FU_EmpUpload.FileName));
                                //});
                                //          email.IsBackground = true;
                                //          email.Start();
                                MsgCls("File uploaded successfully !", LblMngrUploadMsg, Color.Green);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('File uploaded successfully !');", true);

                                if (File.Exists(Path.Combine(Server.MapPath(@"." + "/Exported/Final-Templates/"), Path.GetFileName(EmpUploadFilePath))))
                                {
                                    // If file found, delete it    
                                    File.Delete(Path.Combine(Server.MapPath(@"." + "/Exported/MSS/"), Path.GetFileName(EmpUploadFilePath)));
                                    //Console.WriteLine("File deleted.");
                                }
                            }
                            else
                            { throw new Exception("-02"); }
                        }
                        else
                        { throw new Exception("-03"); }

                    }
                    else
                    { throw new Exception("-01"); }
                }
            }
            catch (Exception Ex)
            {
                switch (Ex.Message)
                {
                    case "-01":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Appraisal is not available. Invalid Document name !');", true);
                        MsgCls("Appraisal is not available. Invalid Document name !", LblMngrUploadMsg, Color.Red);
                        break;
                    case "-03":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Cannot upload other's subordinate Appraisal form !');", true);
                        MsgCls("Appraisal is not available. Invalid Document name !", LblMngrUploadMsg, Color.Red);
                        break;
                    case "-02":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You have already submitted..!');", true);
                        MsgCls("You have already submitted..!", LblMngrUploadMsg, Color.Red);
                        break;
                    default:
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true);
                        break;
                }
            }
        }

        #endregion
        //--------------------- UPLOAD MANAGER UPRAISAL --------- PDF ------------------------------ END ----------

    }
}