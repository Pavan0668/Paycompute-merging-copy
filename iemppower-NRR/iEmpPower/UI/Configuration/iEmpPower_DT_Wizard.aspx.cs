using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using System.Globalization;
using System.Data.Sql;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Xml.Schema;
using System.Text;
using iEmpPowerMaster_Load;
using System.Web.Configuration;
using System.Drawing;
using System.Threading.Tasks;
using iEmpPower.iEmpPower_DT_Wisard_Dataset;
using iEmpPower.iEmpPower_DT_Wisard_Dataset.transactiondatasetTableAdapters;


namespace iEmpPower.iEmpPower_DT_Wisard_Dataset
{
    public partial class UI_Configuration_iEmpPower_DT_Wizard : System.Web.UI.Page
    {
        #region HTML Log Path Declaration
        private string sCreateUserLogPath = ConfigurationManager.AppSettings["CreateUserLog"].ToString() + DateTime.Now.Date.ToShortDateString().Replace('/', '-') + ".html";
        private string sDrpdwnsLogPath = ConfigurationManager.AppSettings["DDLLog"].ToString() + DateTime.Now.Date.ToShortDateString().Replace('/', '-') + ".html";
        private string TransImportPath = ConfigurationManager.AppSettings["TransImportLog"].ToString() + DateTime.Now.Date.ToShortDateString().Replace('/', '-') + ".html";
        private string sOrgDtlsLogPath = ConfigurationManager.AppSettings["OrgDtlsLog"].ToString() + DateTime.Now.Date.ToShortDateString().Replace('/', '-') + ".html";
        private string sTransLogPath = ConfigurationManager.AppSettings["TransLog"].ToString() + DateTime.Now.Date.ToShortDateString().Replace('/', '-') + ".html";
        private string sPaySlipLogPath = ConfigurationManager.AppSettings["PaySlipLog"].ToString() + DateTime.Now.Date.ToShortDateString().Replace('/', '-') + ".html";
        private string sTimeStLogPath = ConfigurationManager.AppSettings["TimeStatementLog"].ToString() + DateTime.Now.Date.ToShortDateString().Replace('/', '-') + ".html";
        #endregion

        #region SAP XML Data Folder Path
        private string sExportPth = "";
        private string Dropdownfolder = ConfigurationManager.AppSettings["DropdownPath"].ToString();
        private string CreateUserfolder = ConfigurationManager.AppSettings["CreateUser"].ToString();
        private string OrgDtlfolder = ConfigurationManager.AppSettings["OrgDtlsPath"].ToString();
        private string TransImportfolder = ConfigurationManager.AppSettings["TransPath"].ToString();
        #endregion

        #region Error Log Object
        iEmpPower_DT_Wizard_Utility ObjLogError = new iEmpPower_DT_Wizard_Utility();
        #endregion

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
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        #region User Defined Methods

        private void MsgCls(string Msg, Label Lbl, Color Clr)
        {
            try
            {
                Lbl.Text = string.Empty;
                Lbl.Text = Lbl.Text + Msg;
                Lbl.ForeColor = Clr;
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }
        #endregion

        #region Page Load Events
        private void PageLoadEvents()
        {
            try
            {

            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }
        #endregion
        //================================== GENERATE USERS ==================== START ==============
        #region Btn Generate User XML
        protected void BtnGenerateUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (FU_GenerateUsers.PostedFile != null && !string.IsNullOrEmpty(FU_GenerateUsers.PostedFile.FileName))
                {
                    string XmlExt = Path.GetExtension(FU_GenerateUsers.FileName.ToString().ToUpper());
                    string UserXMLPath = Path.Combine(CreateUserfolder, FU_GenerateUsers.FileName);


                    if (XmlExt == ".xml" | XmlExt == ".XML")
                    {
                        if (!Directory.Exists(CreateUserfolder))
                        { Directory.CreateDirectory(CreateUserfolder); }
                        //-------------------------------------------------
                        if (!File.Exists(sCreateUserLogPath))
                        { File.Create(sCreateUserLogPath).Close(); }
                        //-------- SAVE TO LOCAL DRIVE --------------------
                        DirectoryInfo DirInfo = new DirectoryInfo(CreateUserfolder);
                        FileInfo[] AdminUserProfileImageFile = DirInfo.GetFiles("*" + FU_GenerateUsers.FileName + "*.*");
                        foreach (FileInfo Fl in AdminUserProfileImageFile)
                        { Fl.Delete(); }
                        FU_GenerateUsers.SaveAs(UserXMLPath);
                        //-------- CREATE USERS --------------------------
                        MembershipCreateStatus MuUserStatus = new MembershipCreateStatus();
                        using (DataSet DsUser = new DataSet())
                        {
                            DsUser.ReadXml(UserXMLPath);
                            if (DsUser.Tables.Contains("item"))
                            {
                                using (DataTable DtSuccessEmailList = new DataTable())
                                {
                                    DtSuccessEmailList.Columns.Add("Username", typeof(string));
                                    DtSuccessEmailList.Columns.Add("Password", typeof(string));
                                    DtSuccessEmailList.Columns.Add("EmailID", typeof(string));

                                    foreach (DataRow Row in DsUser.Tables["item"].Rows)
                                    {
                                        MembershipUser MuUser = Membership.CreateUser(Row["USERNAME"].ToString(), Row["PASSWORD"].ToString(), Row["EMAIL"].ToString().ToLower().Trim(), "a", "b", true, out MuUserStatus);
                                        switch (MuUserStatus)
                                        {
                                            case MembershipCreateStatus.DuplicateEmail:
                                                ObjLogError.LogError(sCreateUserLogPath, Row["EMAIL"].ToString().ToLower().Trim() + " Email already exist.");
                                                break;
                                            case MembershipCreateStatus.DuplicateProviderUserKey:
                                                break;
                                            case MembershipCreateStatus.DuplicateUserName:
                                                ObjLogError.LogError(sCreateUserLogPath, Row["USERNAME"].ToString().Trim() + " User already exist.");
                                                break;
                                            case MembershipCreateStatus.InvalidAnswer:
                                                break;
                                            case MembershipCreateStatus.InvalidEmail:
                                                ObjLogError.LogError(sCreateUserLogPath, Row["EMAIL"].ToString().ToLower().Trim() + " Email is not valid.");
                                                break;
                                            case MembershipCreateStatus.InvalidPassword:
                                                break;
                                            case MembershipCreateStatus.InvalidProviderUserKey:
                                                break;
                                            case MembershipCreateStatus.InvalidQuestion:
                                                break;
                                            case MembershipCreateStatus.InvalidUserName:
                                                ObjLogError.LogError(sCreateUserLogPath, Row["USERNAME"].ToString().ToLower().Trim() + " UserName is not valid.");
                                                break;
                                            case MembershipCreateStatus.ProviderError:
                                                break;
                                            case MembershipCreateStatus.Success:
                                                DtSuccessEmailList.Rows.Add(Row["USERNAME"].ToString().Trim(), Row["PASSWORD"].ToString(), Row["EMAIL"].ToString().ToLower().Trim());
                                                ObjLogError.LogError(sCreateUserLogPath, Row["USERNAME"].ToString().ToLower().Trim() + " - " + Row["EMAIL"].ToString().ToLower().Trim() + " - User created successfully.");
                                                break;
                                            case MembershipCreateStatus.UserRejected:
                                                ObjLogError.LogError(sCreateUserLogPath, Row["USERNAME"].ToString().ToLower().Trim() + " User rejected.");
                                                break;
                                            default:
                                                ObjLogError.LogError(sCreateUserLogPath, Row["USERNAME"].ToString().ToLower().Trim() + " - " + Row["EMAIL"].ToString().ToLower().Trim() + " - Unknown Error.");
                                                break;
                                        }
                                    }
                                    string[] MsgCC = { };

                                    //--------------------------- SENDING EMAIL NOTIFICATION - TO USERS ---------------------------------------
                                    string Mailbody = string.Empty;
                                    string AddressInfoFilePath = Server.MapPath(@"~/EmailTemplates/CreateUser.html");
                                    Mailbody = System.IO.File.ReadAllText(AddressInfoFilePath);

                                    Parallel.ForEach(DtSuccessEmailList.AsEnumerable(), DtRow => masterbl.SendMail(DtRow["EmailID"].ToString(), MsgCC, "User in IEmppower created."
                                      , Mailbody.Replace("##EMPPERNR##", DtRow["Username"].ToString()).Replace("##PASSWORD##", DtRow["Password"].ToString()).Replace("##EMAILID##", DtRow["EmailID"].ToString())));

                                    MsgCls("User Generated successfully. <b>[</b> Please check User log for details ! <b>]</b>", LblMsg, Color.Green);
                                }
                            }
                            else
                            {
                                MsgCls("No data found in User XML !", LblMsg, Color.Red);
                                ObjLogError.LogError(sCreateUserLogPath, "No Data in User XML !");
                            }
                        }
                    }
                    else
                    { MsgCls("Please select Users XML file !", LblMsg, Color.Red); }
                }
            }
            catch (Exception Ex)
            {
                MsgCls(Ex.Message, LblMsg, Color.Red);
                ObjLogError.LogError(sCreateUserLogPath, Ex.Message);
            }
        }
        #endregion
        //================================== GENERATE USERS ==================== END ==============
        //================================== IMPORT MASTERS ==================== START ==============
        #region Import Master Data
        protected void BtnImportMasters_Click(object sender, EventArgs e)
        {
            try
            {
                QueriesTableAdapter Adap = new QueriesTableAdapter();
                int? OutputResult = 0;
                if (FU_ImportMasters.PostedFile != null && !string.IsNullOrEmpty(FU_ImportMasters.PostedFile.FileName))
                {
                    string XmlExt = Path.GetExtension(FU_ImportMasters.FileName.ToString().ToUpper());
                    string ImportMastersXMLPath = Path.Combine(TransImportfolder, FU_ImportMasters.FileName);


                    if (XmlExt == ".xml" | XmlExt == ".XML")
                    {
                        if (!Directory.Exists(TransImportfolder))
                        { Directory.CreateDirectory(TransImportfolder); }
                        //-------------------------------------------------
                        if (!File.Exists(TransImportPath))
                        { File.Create(TransImportPath).Close(); }
                        //-------- SAVE TO LOCAL DRIVE --------------------
                        DirectoryInfo DirInfo = new DirectoryInfo(TransImportfolder);
                        //FileInfo[] AdminUserProfileImageFile = DirInfo.GetFiles("*" + FU_ImportMasters.FileName + "*.*");
                        FileInfo[] AdminUserProfileImageFile = DirInfo.GetFiles(FU_ImportMasters.FileName);
                        foreach (FileInfo Fl in AdminUserProfileImageFile)
                        { Fl.Delete(); }
                        FU_ImportMasters.SaveAs(ImportMastersXMLPath);

                        using (DataSet DsUser = new DataSet())
                        {
                            DsUser.ReadXml(ImportMastersXMLPath);

                            foreach (DataTable Dt in DsUser.Tables)
                            {
                                if (!Dt.TableName.StartsWith("TAB") && !Dt.TableName.StartsWith("tab") && !Dt.TableName.StartsWith("abap") && !Dt.TableName.StartsWith("values") && !Dt.TableName.StartsWith("NewDataSet"))
                                {
                                    switch (Dt.TableName.ToUpper())
                                    {
                                        #region Master Upload

                                        case "PA0006": //------------ ADDRESS INFORMATION -------
                                            #region PA0006
                                            foreach (DataRow Rows in Dt.Rows)
                                            {
                                                try
                                                {
                                                    DateTime DtBEGDA = new DateTime();
                                                    DateTime DtENDDA = new DateTime();

                                                    if (DateTime.TryParse(Rows["BEGDA"].ToString(), out DtBEGDA) && DateTime.TryParse(Rows["ENDDA"].ToString(), out DtENDDA))
                                                    {
                                                        //Adap.sp_xml_create_address_details(
                                                        //    Rows["PERNR"].ToString().Trim()
                                                        //    , Rows["UNAME"].ToString().Trim()
                                                        //    , Rows["SUBTY"].ToString().Trim()
                                                        //    , DtBEGDA
                                                        //    , DtENDDA
                                                        //    , Rows["STRAS"].ToString().Trim()
                                                        //    , Rows["LOCAT"].ToString().Trim()
                                                        //    , Rows["LAND1"].ToString().Trim() //FGBLD
                                                        //    , Rows["STATE"].ToString().Trim()
                                                        //    , Rows["ORT01"].ToString().Trim()
                                                        //    , Rows["PSTLZ"].ToString().Trim()
                                                        //    , Rows["TELNR"].ToString().Trim()
                                                        //    , "SAP" // Not Using
                                                        //    , Rows["UNAME"].ToString().Trim()
                                                        //    , DateTime.Now
                                                        //    , ""
                                                        //    , ref OutputResult);

                                                        Adap.sp_xml_create_address_details(Rows["PERNR"].ToString(), Rows["UNAME"].ToString(), Rows["SUBTY"].ToString(), DtBEGDA, DtENDDA,
                                                                                             Rows["STRAS"].ToString(), Rows["LOCAT"].ToString(),
                                                                                        Rows["LAND1"].ToString(), Rows["STATE"].ToString(), Rows["ORT01"].ToString(),
                                                                                       Rows["PSTLZ"].ToString(), Rows["TELNR"].ToString(), "Approved", Rows["UNAME"].ToString(), DateTime.Now, Rows["UNAME"].ToString(), ref OutputResult);


                                                        switch (OutputResult)
                                                        {
                                                            case 1:
                                                                ObjLogError.LogError(TransImportPath, " Table PA0006 Updated successfully for " + Rows["PERNR"].ToString());
                                                                break;
                                                            case 2:
                                                                ObjLogError.LogError(TransImportPath, " Table PA0006 Inserted successfully for " + Rows["PERNR"].ToString());
                                                                break;
                                                            case 3:
                                                                ObjLogError.LogError(TransImportPath, "Address Already exists for these date " + Rows["PERNR"].ToString());
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                                catch (Exception Ex)
                                                { ObjLogError.LogError(TransImportPath, " Error occured in table PA0006, when Inserting / Updating the record for " + Rows["PERNR"].ToString() + " " + Ex.Message); }

                                            }
                                            #endregion
                                            break;

                                        case "PA0021": // ----------- FAMILY MEMBER ------
                                            #region PA0021
                                            foreach (DataRow Rows in Dt.Rows)
                                            {
                                                try
                                                {
                                                    if (Rows["FGBDT"].ToString().Trim() == "0000-00-00" || Rows["FGBDT"].ToString() == "    -  -  ")
                                                    {
                                                        Rows["FGBDT"] = "1900-01-01";
                                                    }
                                                    DateTime DtBEGDA = new DateTime();
                                                    DateTime DtENDDA = new DateTime();
                                                    DateTime DtFGBDT = new DateTime();
                                                    if (DateTime.TryParse(Rows["BEGDA"].ToString(), out DtBEGDA) && DateTime.TryParse(Rows["ENDDA"].ToString(), out DtENDDA)
                                                         && DateTime.TryParse(Rows["FGBDT"].ToString(), out DtFGBDT))
                                                    {
                                                        //Adap.sp_xml_create_family_members( //    Rows["PERNR"].ToString().Trim()//    , Rows["FAMSA"].ToString().Trim()
                                                        //    , Rows["OBJPS"].ToString().Trim() //    , Rows["FAVOR"].ToString().Trim() //    , Rows["FANAM"].ToString().Trim()
                                                        //    , Rows["FGBNA"].ToString().Trim()//    , Rows["FINIT"].ToString().Trim() //    , Rows["FNMZU"].ToString().Trim()
                                                        //    , Rows["FVRSW"].ToString().Trim()//    , Rows["FASEX"].ToString().Trim()//    , DtFGBDT
                                                        //    , Rows["FGBOT"].ToString().Trim() //    , Rows["FGBLD"].ToString().Trim() //    , Rows["FANAT"].ToString().Trim()
                                                        //    , Rows["FANA2"].ToString().Trim()//    , Rows["FANA3"].ToString().Trim() //    , Rows["KDBSL"].ToString().Trim()
                                                        //    , Rows["KDGBR"].ToString().Trim()//    , Rows["KDZUL"].ToString().Trim() //    , DtBEGDA //    , DtENDDA
                                                        //    , Rows["UNAME"].ToString().Trim()  //    , DateTime.Now//    , ref OutputResult);

                                                        Adap.sp_xml_create_family_members(Rows["PERNR"].ToString(), Rows["FAMSA"].ToString(), Rows["OBJPS"].ToString(), Rows["FAVOR"].ToString(), Rows["FANAM"].ToString(), Rows["FGBNA"].ToString(), Rows["FINIT"].ToString(),
                                                                                        Rows["FNMZU"].ToString(), Rows["FVRSW"].ToString(), Rows["FASEX"].ToString(), DtFGBDT,
                                                                                       Rows["FGBOT"].ToString(), Rows["FGBLD"].ToString(), Rows["FANAT"].ToString(), Rows["FANA2"].ToString(),
                                                                                       Rows["FANA3"].ToString(), Rows["KDBSL"].ToString(), Rows["KDGBR"].ToString(), Rows["KDZUL"].ToString(), DtBEGDA, DtENDDA, Rows["UNAME"].ToString(), DateTime.Now, ref OutputResult);

                                                        switch (OutputResult)
                                                        {
                                                            case 1:
                                                                ObjLogError.LogError(TransImportPath, " Table PA0021 Updated successfully for " + Rows["PERNR"].ToString());
                                                                break;
                                                            case 2:
                                                                ObjLogError.LogError(TransImportPath, " Table PA0021 Inserted successfully for " + Rows["PERNR"].ToString());
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                                catch (Exception Ex)
                                                { ObjLogError.LogError(TransImportPath, " Error occured in table PA0021, when Inserting / Updating the record for " + Rows["PERNR"].ToString() + " " + Ex.Message); }
                                            }
                                            #endregion
                                            break;

                                        case "PA0009": // ----------- BANK INFO -----------------
                                            #region PA0009
                                            foreach (DataRow Rows in Dt.Rows)
                                            {
                                                try
                                                {
                                                    DateTime DtBEGDA = new DateTime();
                                                    DateTime DtENDDA = new DateTime();

                                                    if (DateTime.TryParse(Rows["BEGDA"].ToString(), out DtBEGDA) && DateTime.TryParse(Rows["ENDDA"].ToString(), out DtENDDA))
                                                    {
                                                        //Adap.sp_xml_create_bank_info(  Rows["PERNR"].ToString().Trim() , Rows["SUBTY"].ToString().Trim(), DtBEGDA , DtENDDA
                                                        //    , Rows["EMFTX"].ToString().Trim() , Rows["BKPLZ"].ToString().Trim() , Rows["BKORT"].ToString().Trim()
                                                        //    , Rows["BANKS"].ToString().Trim() , Rows["BANKL"].ToString().Trim() , Rows["BANKN"].ToString().Trim()
                                                        //    , Rows["ZLSCH"].ToString().Trim() , Rows["ZWECK"].ToString().Trim()  , Rows["WAERS"].ToString().Trim()
                                                        //    , Rows["UNAME"].ToString().Trim(), DateTime.Now , ref OutputResult);
                                                        Adap.sp_xml_create_bank_info(Rows["PERNR"].ToString(), Rows["SUBTY"].ToString(), DtBEGDA, DtENDDA, Rows["EMFTX"].ToString(),
                                                                                           Rows["BKPLZ"].ToString(), Rows["BKORT"].ToString(), Rows["BANKS"].ToString(), Rows["BANKL"].ToString(),
                                                                                           Rows["BANKN"].ToString(), Rows["ZLSCH"].ToString(), Rows["ZWECK"].ToString(), Rows["WAERS"].ToString(), Rows["UNAME"].ToString(),
                                                                                           DateTime.Now, ref OutputResult);
                                                        switch (OutputResult)
                                                        {
                                                            case 1:
                                                                ObjLogError.LogError(TransImportPath, " Table PA0009 Updated successfully for " + Rows["PERNR"].ToString());
                                                                break;
                                                            case 2:
                                                                ObjLogError.LogError(TransImportPath, " Table PA0009 Inserted successfully for " + Rows["PERNR"].ToString());
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }

                                                catch (Exception Ex)
                                                { ObjLogError.LogError(TransImportPath, " Error occured in table PA0009, when Inserting / Updating the record for " + Rows["PERNR"].ToString() + " " + Ex.Message); }
                                            }
                                            #endregion
                                            break;

                                        case "PA0185": // ----------- PERSONAL ID's ------
                                            #region PA0185
                                            foreach (DataRow Rows in Dt.Rows)
                                            {
                                                try
                                                {
                                                    DateTime DtBEGDA = new DateTime();
                                                    DateTime DtENDDA = new DateTime();

                                                    if (DateTime.TryParse(Rows["BEGDA"].ToString(), out DtBEGDA) && DateTime.TryParse(Rows["ENDDA"].ToString(), out DtENDDA))
                                                    {
                                                        //Adap.sp_xml_create_personal_ids(Rows["PERNR"].ToString().Trim() , Rows["ICTYP"].ToString().Trim(), Rows["ICNUM"].ToString().Trim().ToUpper()
                                                        //    , DtBEGDA, DtENDDA, Rows["UNAME"].ToString().Trim(), DateTime.Now, ref OutputResult);

                                                        Adap.sp_xml_create_personal_ids(Rows["PERNR"].ToString(), Rows["ICTYP"].ToString(), Rows["ICNUM"].ToString(), DtBEGDA, DtENDDA
                                                                                        , Rows["UNAME"].ToString(), DateTime.Now, ref OutputResult);

                                                        switch (OutputResult)
                                                        {
                                                            case 1:
                                                                ObjLogError.LogError(TransImportPath, " Table PA0185 Updated successfully for " + Rows["PERNR"].ToString());
                                                                break;
                                                            case 2:
                                                                ObjLogError.LogError(TransImportPath, " Table PA0185 Inserted successfully for " + Rows["PERNR"].ToString());
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                                catch (Exception Ex)
                                                { ObjLogError.LogError(TransImportPath, " Error occured in table PA0185, when Inserting / Updating the record for " + Rows["PERNR"].ToString() + " " + Ex.Message); }
                                            }
                                            #endregion
                                            break;

                                        case "PA0002": //------------ PERSONAL DATA -------------
                                            #region PA0002
                                            foreach (DataRow Rows in Dt.Rows)
                                            {
                                                try
                                                {
                                                    DateTime DtBEGDA = new DateTime();
                                                    DateTime DtENDDA = new DateTime();
                                                    DateTime DtGBDAT = new DateTime();
                                                    DateTime DtFAMDT = new DateTime(1900, 01, 01);

                                                    if (Rows["FAMDT"].ToString() == "    -  -  " || string.IsNullOrEmpty(Rows["FAMDT"].ToString().Trim()))
                                                    {
                                                        Rows["FAMDT"] = "0000-00-00";
                                                    }


                                                    if (DateTime.TryParse(Rows["BEGDA"].ToString(), out DtBEGDA) && DateTime.TryParse(Rows["ENDDA"].ToString(), out DtENDDA)
                                                        && DateTime.TryParse(Rows["GBDAT"].ToString().Trim() == "0000-00-00" ? "1900-01-01" : Rows["GBDAT"].ToString().Trim(), out DtGBDAT)
                                                        && DateTime.TryParse(Rows["FAMDT"].ToString().Trim() == "0000-00-00" ? "1900-01-01" : Rows["FAMDT"].ToString().Trim(), out DtFAMDT))
                                                    {
                                                        //Adap.sp_xml_create_personal_data( Rows["PERNR"].ToString().Trim() , Rows["UNAME"].ToString().Trim(), Rows["ANRED"].ToString().Trim(), Rows["VORNA"].ToString().Trim()
                                                        //    , Rows["NACHN"].ToString().Trim(), Rows["NAME2"].ToString().Trim(), Rows["INITS"].ToString().Trim(), Rows["RUFNM"].ToString().Trim()
                                                        //    , Rows["SPRSL"].ToString().Trim(), Rows["GESCH"].ToString().Trim(), DtGBDAT, Rows["GBORT"].ToString().Trim(), Rows["GBLND"].ToString().Trim()
                                                        //    , Rows["NATIO"].ToString().Trim(), Rows["GBDEP"].ToString().Trim(), Rows["NATI2"].ToString().Trim(), Rows["NATI3"].ToString().Trim(), Rows["FAMST"].ToString().Trim()
                                                        //    , DtFAMDT, Rows["ANZKD"].ToString().Trim(), Rows["KONFE"].ToString().Trim(), DtBEGDA , DtENDDA, Rows["UNAME"].ToString().Trim(), DateTime.Now, ref OutputResult);

                                                        Adap.sp_xml_create_personal_data(Rows["PERNR"].ToString(), Rows["UNAME"].ToString(), Rows["ANRED"].ToString(), Rows["VORNA"].ToString(),
                                                                                             Rows["NACHN"].ToString(), Rows["NAME2"].ToString(), Rows["INITS"].ToString(),
                                                                                            Rows["RUFNM"].ToString(), Rows["SPRSL"].ToString(), Rows["GESCH"].ToString(), DtGBDAT,
                                                                                           Rows["GBORT"].ToString(), Rows["GBLND"].ToString(), Rows["NATIO"].ToString(), Rows["GBDEP"].ToString(),
                                                                                           Rows["NATI2"].ToString(), Rows["NATI3"].ToString(), Rows["FAMST"].ToString(), DtFAMDT,
                                                                                           Rows["ANZKD"].ToString(), Rows["KONFE"].ToString(), DtBEGDA, DtENDDA,
                                                                                           Rows["UNAME"].ToString(), DateTime.Now, ref OutputResult);
                                                        switch (OutputResult)
                                                        {
                                                            case 1:
                                                                ObjLogError.LogError(TransImportPath, " Table PA0002 Updated successfully for " + Rows["PERNR"].ToString());
                                                                break;
                                                            case 2:
                                                                ObjLogError.LogError(TransImportPath, " Table PA0002 Inserted successfully for " + Rows["PERNR"].ToString());
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                                catch (Exception Ex)
                                                { ObjLogError.LogError(TransImportPath, " Error occured in table PA0002, when Inserting / Updating the record for " + Rows["PERNR"].ToString() + " " + Ex.Message); }
                                            }
                                            #endregion
                                            break;

                                        case "PA0105": // ----------- EMAIL ID ------
                                            #region PA0105
                                            foreach (DataRow Rows in Dt.Rows)
                                            {
                                                try
                                                {
                                                    DateTime DtBEGDA = new DateTime();
                                                    DateTime DtENDDA = new DateTime();

                                                    if (DateTime.TryParse(Rows["BEGDA"].ToString(), out DtBEGDA) && DateTime.TryParse(Rows["ENDDA"].ToString(), out DtENDDA))
                                                    {
                                                        //Adap.sp_xml_create_communication_info( Rows["PERNR"].ToString().Trim() , Rows["USRTY"].ToString().Trim()
                                                        //    , Rows["USRID"].ToString().Trim().ToLower(), DtBEGDA, DtENDDA , Rows["UNAME"].ToString().Trim()
                                                        //    , DateTime.Now,ref OutputResult);

                                                        Adap.sp_xml_create_communication_info(Rows["PERNR"].ToString(), Rows["USRTY"].ToString(), Rows["USRID"].ToString(), Rows["USRID_LONG"].ToString()
                                                         , DtBEGDA, DtENDDA, Rows["UNAME"].ToString(), DateTime.Now, ref OutputResult);

                                                        switch (OutputResult)
                                                        {
                                                            case 1:
                                                                ObjLogError.LogError(TransImportPath, " Table PA0105 Updated successfully for " + Rows["PERNR"].ToString());
                                                                break;
                                                            case 2:
                                                                ObjLogError.LogError(TransImportPath, " Table PA0105 Inserted successfully for " + Rows["PERNR"].ToString());
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                                catch (Exception Ex)
                                                { ObjLogError.LogError(TransImportPath, " Error occured in table PA0105, when Inserting / Updating the record for " + Rows["PERNR"].ToString() + " " + Ex.Message); }
                                            }
                                            #endregion
                                            break;

                                        case "PA2001": //------------ LEAVE INFO [PA2001] -------
                                            #region PA2001
                                            foreach (DataRow Rows in Dt.Rows)
                                            {
                                                try
                                                {

                                                    DateTime DtBEGDA = new DateTime();
                                                    DateTime DtENDDA = new DateTime();
                                                    TimeSpan TsBEGUZ = new TimeSpan(0, 0, 0);
                                                    TimeSpan TsENDUZ = new TimeSpan(0, 0, 0);
                                                    // && DateTime.TryParse(Rows["GBDAT"].ToString().Trim() == "0000-00-00" ? "1900-01-01" : Rows["GBDAT"].ToString().Trim(), out DtGBDAT) 
                                                    if (Rows["BEGUZ"].ToString() == "  :  :  " || string.IsNullOrEmpty(Rows["BEGUZ"].ToString().Trim()))
                                                    {
                                                        Rows["BEGUZ"] = "00:00:00";
                                                    }
                                                    if (Rows["ENDUZ"].ToString() == "  :  :  " || string.IsNullOrEmpty(Rows["ENDUZ"].ToString().Trim()))
                                                    {
                                                        Rows["ENDUZ"] = "00:00:00";
                                                    }



                                                    if (DateTime.TryParse(Rows["BEGDA"].ToString(), out DtBEGDA) && DateTime.TryParse(Rows["ENDDA"].ToString(), out DtENDDA))
                                                    {
                                                        TimeSpan.TryParse(Rows["BEGUZ"].ToString(), out TsBEGUZ);
                                                        TimeSpan.TryParse(Rows["ENDUZ"].ToString(), out TsENDUZ);

                                                        // objLeaveRequestBo.STDAZ = DtFrom == DtTo ? Rbtn_LeaveMode.SelectedValue == "0" ? "0.5" : TsTtlDays.TotalDays.ToString() : TsTtlDays.TotalDays.ToString();


                                                        //delete on hold      //Adap.sp_xml_create_leave_request(Rows["PERNR"].ToString(), DtBEGDA, DtENDDA,
                                                        //   TsBEGUZ.ToString().Trim(), TsENDUZ.ToString().Trim(), Rows["AWART"].ToString(), Rows["ABRTG"].ToString(), "Please Approve", Rows["FLAG"].ToString(), ref OutputResult);
                                                        Adap.sp_xml_create_leave_request(Rows["PERNR"].ToString(), DtBEGDA, DtENDDA,
                                                                                                                TsBEGUZ.ToString().Trim(), TsENDUZ.ToString().Trim(), Rows["AWART"].ToString(), Rows["ABRTG"].ToString(), "Please Approve", "A", ref OutputResult);

                                                        switch (OutputResult)
                                                        {
                                                            case 1:
                                                                ObjLogError.LogError(TransImportPath, " Table PA2001 inserted successfully for " + Rows["PERNR"].ToString());
                                                                break;
                                                            case 2:
                                                                ObjLogError.LogError(TransImportPath, " No working time maintained  for " + Rows["PERNR"].ToString());
                                                                break;
                                                            case 3:
                                                                ObjLogError.LogError(TransImportPath, " From date cannot be greater than To date." + Rows["PERNR"].ToString());
                                                                break;
                                                            case 4:
                                                                ObjLogError.LogError(TransImportPath, " Please select valid from and to dates lying between the leave quota range for " + Rows["PERNR"].ToString());
                                                                break;
                                                            case 5:
                                                                ObjLogError.LogError(TransImportPath, " Cannot apply on Sunday and Public Holidays." + Rows["PERNR"].ToString());
                                                                break;
                                                            case 6:
                                                                ObjLogError.LogError(TransImportPath, " Leave / Attendance already exists for these dates !" + Rows["PERNR"].ToString());
                                                                break;
                                                            case 7:
                                                                ObjLogError.LogError(TransImportPath, " Leave Quota exceeded !" + Rows["PERNR"].ToString());
                                                                break;
                                                            case 8:
                                                                ObjLogError.LogError(TransImportPath, " Maternity leave cannot be applied more than 2 times !" + Rows["PERNR"].ToString());
                                                                break;
                                                            case 17:
                                                                ObjLogError.LogError(TransImportPath, " Maternity Leave cannot be applied for half a day !" + Rows["PERNR"].ToString());
                                                                break;

                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                                catch (Exception Ex)
                                                { ObjLogError.LogError(TransImportPath, " Error occured in table PA2001, when Inserting / Updating the record for " + Rows["PERNR"].ToString() + " " + Ex.Message); }
                                            }

                                            break;
                                            #endregion

                                        case "PA2002": //------------ Attendance INFO [PA2002] -------
                                            #region PA2002
                                            foreach (DataRow Rows in Dt.Rows)
                                            {
                                                try
                                                {
                                                    DateTime DtBEGDA = new DateTime();
                                                    DateTime DtENDDA = new DateTime();
                                                    TimeSpan TsBEGUZ = new TimeSpan(0, 0, 0);
                                                    TimeSpan TsENDUZ = new TimeSpan(0, 0, 0);

                                                    if (Rows["BEGUZ"].ToString() == "  :  :  " || string.IsNullOrEmpty(Rows["BEGUZ"].ToString().Trim()))
                                                    {
                                                        Rows["BEGUZ"] = "00:00:00";
                                                    }
                                                    if (Rows["ENDUZ"].ToString() == "  :  :  " || string.IsNullOrEmpty(Rows["ENDUZ"].ToString().Trim()))
                                                    {
                                                        Rows["ENDUZ"] = "00:00:00";
                                                    }

                                                    if (DateTime.TryParse(Rows["BEGDA"].ToString(), out DtBEGDA) && DateTime.TryParse(Rows["ENDDA"].ToString(), out DtENDDA))
                                                    {
                                                        TimeSpan.TryParse(Rows["BEGUZ"].ToString(), out TsBEGUZ);
                                                        TimeSpan.TryParse(Rows["ENDUZ"].ToString(), out TsENDUZ);

                                                        //delete on hold       //Adap.sp_xml_create_attendance_request(Rows["PERNR"].ToString(), DtBEGDA, DtENDDA,
                                                        //  TsBEGUZ.ToString().Trim(), TsENDUZ.ToString().Trim(), Rows["AWART"].ToString(), Rows["ABRTG"].ToString(), "Please Approve", Rows["FLAG"].ToString(), ref OutputResult);


                                                        Adap.sp_xml_create_attendance_request(Rows["PERNR"].ToString(), DtBEGDA, DtENDDA,
                                                          TsBEGUZ.ToString().Trim(), TsENDUZ.ToString().Trim(), Rows["AWART"].ToString(), Rows["ABRTG"].ToString(), "Please Approve", "A", ref OutputResult);

                                                        switch (OutputResult)
                                                        {
                                                            case 1:
                                                                ObjLogError.LogError(TransImportPath, " Table PA2002 inserted successfully for " + Rows["PERNR"].ToString());
                                                                break;
                                                            case 2:
                                                                ObjLogError.LogError(TransImportPath, " No working time maintained  for " + Rows["PERNR"].ToString());
                                                                break;
                                                            case 3:
                                                                ObjLogError.LogError(TransImportPath, " From date cannot be greater than To date." + Rows["PERNR"].ToString());
                                                                break;
                                                            case 4:
                                                                ObjLogError.LogError(TransImportPath, " Please select valid from and to dates lying between the leave quota range for " + Rows["PERNR"].ToString());
                                                                break;
                                                            case 5:
                                                                ObjLogError.LogError(TransImportPath, " Cannot apply on Sunday and Public Holidays." + Rows["PERNR"].ToString());
                                                                break;
                                                            case 6:
                                                                ObjLogError.LogError(TransImportPath, " Leave / Attendance already exists for these dates !" + Rows["PERNR"].ToString());
                                                                break;
                                                            case 7:
                                                                ObjLogError.LogError(TransImportPath, " Leave Quota exceeded !" + Rows["PERNR"].ToString());
                                                                break;
                                                            case 8:
                                                                ObjLogError.LogError(TransImportPath, " Maternity leave cannot be applied more than 2 times !" + Rows["PERNR"].ToString());
                                                                break;
                                                            case 11:
                                                                ObjLogError.LogError(TransImportPath, " Attendance can be applied only on Sunday and Public holidays !" + Rows["PERNR"].ToString());
                                                                break;
                                                            case 12:
                                                                ObjLogError.LogError(TransImportPath, " From Time should be less than To Time." + Rows["PERNR"].ToString());
                                                                break;
                                                            case 16:
                                                                ObjLogError.LogError(TransImportPath, " Regular Attendance cannot be applied on Sunday and Public holidays." + Rows["PERNR"].ToString());
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                                catch (Exception Ex)
                                                { ObjLogError.LogError(TransImportPath, " Error occured in table PA2002, when Inserting / Updating the record for " + Rows["PERNR"].ToString() + " " + Ex.Message); }
                                            }

                                            break;
                                            #endregion

                                        case "PA2006": // ----------- LEAVE QUATO ------
                                            #region PA2006
                                            foreach (DataRow Rows in Dt.Rows)
                                            {
                                                try
                                                {
                                                    DateTime DtBEGDA = new DateTime();
                                                    DateTime DtENDDA = new DateTime();
                                                    DateTime DtDESTA = new DateTime();
                                                    DateTime DtDEEND = new DateTime();
                                                    DateTime DtAEDTM = new DateTime();


                                                    if (DateTime.TryParse(Rows["BEGDA"].ToString(), out DtBEGDA) && DateTime.TryParse(Rows["ENDDA"].ToString(), out DtENDDA)
                                                        && DateTime.TryParse(Rows["DESTA"].ToString(), out DtDESTA) && DateTime.TryParse(Rows["DEEND"].ToString(), out DtDEEND)
                                                        && DateTime.TryParse(Rows["AEDTM"].ToString(), out DtAEDTM))
                                                    {
                                                        //Adap.sp_xml_create_pa2006(
                                                        //    Rows["PERNR"].ToString().Trim(), DtBEGDA, DtENDDA, DtDESTA , DtDEEND, Rows["SUBTY"].ToString().Trim()
                                                        //    , Decimal.Parse(string.IsNullOrEmpty(Rows["ANZHL"].ToString().Trim()) ? "0.0" : Rows["ANZHL"].ToString().Trim())
                                                        //    , Decimal.Parse(string.IsNullOrEmpty(Rows["KVERB"].ToString().Trim()) ? "0.0" : Rows["KVERB"].ToString().Trim())
                                                        //    , DtAEDTM  , Rows["UNAME"].ToString().Trim()    , ref OutputResult);

                                                        Adap.sp_xml_create_pa2006(Rows["PERNR"].ToString(), DtBEGDA, DtENDDA, DtDESTA, DtDEEND,
                                                            Rows["SUBTY"].ToString(), Rows["ANZHL"].ToString().Trim(), Rows["KVERB"].ToString().Trim(),
                                                             DtAEDTM, Rows["UNAME"].ToString(), ref OutputResult);

                                                        switch (OutputResult)
                                                        {
                                                            case 1:
                                                                ObjLogError.LogError(TransImportPath, " Table PA2006 Updated successfully for " + Rows["PERNR"].ToString());
                                                                break;
                                                            case 2:
                                                                ObjLogError.LogError(TransImportPath, " Table PA2006 Inserted successfully for " + Rows["PERNR"].ToString());
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                                catch (Exception Ex)
                                                { ObjLogError.LogError(TransImportPath, " Error occured in table PA2006, when Inserting / Updating the record for " + Rows["PERNR"].ToString() + " " + Ex.Message); }
                                            }
                                            #endregion
                                            break;

                                        case "TEVEN":
                                            #region TEVEN
                                            foreach (DataRow row in Dt.Rows)
                                            {
                                                try
                                                {
                                                    bool? dd = true;
                                                    string sModifiedDate = row["AEDTM"].ToString();
                                                    if (row["AEDTM"].ToString() == "0000-00-00")
                                                    {
                                                        sModifiedDate = null;
                                                    }
                                                    string note = "Test";
                                                    string word = row["LTIME"].ToString();//00:00:00
                                                    string[] sSplit = word.Split(':');

                                                    int hr = int.Parse(sSplit[0].ToString());
                                                    int mn = int.Parse(sSplit[1].ToString());
                                                    int sc = int.Parse(sSplit[2].ToString());

                                                    DateTime dttest = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hr, mn, sc);

                                                    string curTime = dttest.ToString("hh:mm tt");




                                                    Adap.sp_xml_create_clock_in_out(row["PERNR"].ToString(), row["LDATE"].ToString(), curTime, row["SATZA"].ToString(),
                                                                          note, row["PERNR"].ToString(), row["UNAME"].ToString(), sModifiedDate, ref dd);

                                                    switch (OutputResult)
                                                    {
                                                        case 1:
                                                            ObjLogError.LogError(TransImportPath, " Table TEVEN Updated successfully for " + row["PERNR"].ToString());
                                                            break;
                                                        case 2:
                                                            ObjLogError.LogError(TransImportPath, " Table TEVEN inserted successfully for " + row["PERNR"].ToString());
                                                            break;
                                                        default:
                                                            break;
                                                    }
                                                }
                                                catch (Exception Ex)
                                                { ObjLogError.LogError(TransImportPath, " Error occured in table TEVEN, when Inserting / Updating the record for " + row["PERNR"].ToString() + " " + Ex.Message); }
                                            }
                                            #endregion
                                            break;

                                        case "_-ITCHAMPS_-HR_WSR": // ZHR_WSR changed to _-ITCHAMPS_-HR_WSR ----------- Work time rule------
                                            #region ZHR_WSR
                                            foreach (DataRow Rows in Dt.Rows)
                                            {
                                                try
                                                {

                                                    DateTime DtBEGDA = new DateTime();
                                                    DateTime DtENDDA = new DateTime();
                                                    TimeSpan TsBEGUZ = new TimeSpan(0, 0, 0);
                                                    TimeSpan TsENDUZ = new TimeSpan(0, 0, 0);
                                                    if (DateTime.TryParse(Rows["BEGDA"].ToString(), out DtBEGDA) && DateTime.TryParse(Rows["ENDDA"].ToString(), out DtENDDA))
                                                    {
                                                        TimeSpan.TryParse(Rows["NOBEG"].ToString(), out TsBEGUZ);
                                                        TimeSpan.TryParse(Rows["NOEND"].ToString(), out TsENDUZ);

                                                        Adap.sp_xml_create_ZHRWSR(Rows["PERNR"].ToString(), DtBEGDA, DtENDDA, TsBEGUZ, TsENDUZ, ref OutputResult);

                                                        switch (OutputResult)
                                                        {
                                                            case 1:
                                                                ObjLogError.LogError(TransImportPath, " Table ZHR_WSR Updated successfully for " + Rows["PERNR"].ToString());
                                                                break;
                                                            case 2:
                                                                ObjLogError.LogError(TransImportPath, " Table ZHR_WSR Inserted successfully for " + Rows["PERNR"].ToString());
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                                catch (Exception Ex)
                                                { ObjLogError.LogError(TransImportPath, " Error occured in table ZHR_WSR, when Inserting / Updating the record for " + Rows["PERNR"].ToString() + " " + Ex.Message); }
                                            }
                                            #endregion
                                            break;

                                        case "PA9333": // ----------- Employement Department------
                                            #region PA9333
                                            foreach (DataRow Rows in Dt.Rows)
                                            {
                                                try
                                                {
                                                    Adap.sp_xml_create_PA9333(Rows["PERNR"].ToString(), Rows["EMP_DEPART"].ToString(), ref OutputResult);
                                                    switch (OutputResult)
                                                    {
                                                        case 1:
                                                            ObjLogError.LogError(TransImportPath, " Table PA9333 Updated successfully for " + Rows["PERNR"].ToString());
                                                            break;
                                                        case 2:
                                                            ObjLogError.LogError(TransImportPath, " Table PA9333 inserted successfully for " + Rows["PERNR"].ToString());
                                                            break;
                                                        default:
                                                            break;
                                                    }                                                 

                                                }
                                                catch (Exception Ex)
                                                { ObjLogError.LogError(TransImportPath, " Error occured in table PA9333, when Inserting / Updating the record for " + Rows["PERNR"].ToString() + " " + Ex.Message); }
                                            }
                                            #endregion
                                            break;


                                        default:
                                            break;
                                        #endregion
                                    }
                                }
                            }
                            MsgCls("Masters data imported successfully. <b>[</b> Please check User log for details ! <b>]</b>", LblMsg, Color.Green);
                        }
                    }
                    else
                    { MsgCls("Please select Master XML file !", LblMsg, Color.Red); }
                }
            }
            catch (Exception Ex)
            {
                MsgCls(Ex.Message, LblMsg, Color.Red);
                ObjLogError.LogError(TransImportPath, Ex.Message);
            }
        }
        #endregion
        //================================== IMPORT MASTERS ==================== END ==============
        //================================== IMPORT MASTERS ==================== START ==============
        #region Import DropDownlist
        protected void BtnImportDropdownMasters_Click(object sender, EventArgs e)
        {
            try
            {
                QueriesTableAdapter Adap = new QueriesTableAdapter();

                if (FU_ImportDropDownMasters.PostedFile != null && !string.IsNullOrEmpty(FU_ImportDropDownMasters.PostedFile.FileName))
                {
                    string XmlExt = Path.GetExtension(FU_ImportDropDownMasters.FileName.ToString().ToUpper());
                    string DropDownXMLPath = Path.Combine(Dropdownfolder, FU_ImportDropDownMasters.FileName);
                    StringBuilder Sb = new StringBuilder();
                    Sb.Clear();

                    if (XmlExt == ".xml" | XmlExt == ".XML")
                    {
                        if (!Directory.Exists(Dropdownfolder))
                        { Directory.CreateDirectory(Dropdownfolder); }
                        //-------------------------------------------------
                        if (!File.Exists(sDrpdwnsLogPath))
                        { File.Create(sDrpdwnsLogPath).Close(); }
                        //-------- SAVE TO LOCAL DRIVE --------------------
                        DirectoryInfo DirInfo = new DirectoryInfo(Dropdownfolder);
                        //FileInfo[] AdminUserProfileImageFile = DirInfo.GetFiles("*" + FU_ImportDropDownMasters.FileName + "*.*");
                        FileInfo[] AdminUserProfileImageFile = DirInfo.GetFiles(FU_ImportDropDownMasters.FileName);
                        foreach (FileInfo Fl in AdminUserProfileImageFile)
                        { Fl.Delete(); }
                        FU_ImportDropDownMasters.SaveAs(DropDownXMLPath);

                        using (DataSet DsUser = new DataSet())
                        {
                            DsUser.ReadXml(DropDownXMLPath);

                            foreach (DataTable Dt in DsUser.Tables)
                            {
                                if (!Dt.TableName.StartsWith("TAB") && !Dt.TableName.StartsWith("tab") && !Dt.TableName.StartsWith("abap") && !Dt.TableName.StartsWith("values") && !Dt.TableName.StartsWith("NewDataSet"))
                                {
                                    switch (Dt.TableName.Trim().ToUpper())
                                    {
                                        #region Master Upload
                                        // case "AUFK": // -----------AUFK Order master data -------
                                        //     #region AUFK
                                        //     try
                                        //     {
                                        //         usp_xml_create_AUFKTableAdapter AUFK_Adap = new usp_xml_create_AUFKTableAdapter();
                                        //         DataTable AUFK = Dt.DefaultView.ToTable(false, "AUFNR", "KTEXT");
                                        //         using (DataTable Dt_AUFK = AUFK_Adap.AUFK_GetData(AUFK, 1))
                                        //         {
                                        //             Sb.Append("<li class=\"Gr\">--------------------------------- AUFK ------------------- Order Tables : AUFK ------------------------ "
                                        //+ "[<span class=\"G\">INSERT</span>] - " + Dt_AUFK.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                        //+ "[<span class=\"B\">UPDATE</span>] - " + Dt_AUFK.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                        //+ "[<span class=\"R\">DELETE</span>] - " + Dt_AUFK.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                        //             for (int i = 0; i < Dt_AUFK.Rows.Count; i++)
                                        //             {
                                        //                 switch (Dt_AUFK.Rows[i]["$action"].ToString())
                                        //                 {

                                        //                     case "INSERT":
                                        //                         Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                        //                             + " - <b>" + Dt_AUFK.Rows[i]["$action"].ToString() + "</b> - " + Dt_AUFK.Rows[i]["AUFNR"].ToString() + " - " + Dt_AUFK.Rows[i]["KTEXT"].ToString() + "</li>");
                                        //                         break;
                                        //                     case "UPDATE":
                                        //                         Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                        //                            + " - <b>" + Dt_AUFK.Rows[i]["$action"].ToString() + "</b> - " + Dt_AUFK.Rows[i]["AUFNR"].ToString() + " - " + Dt_AUFK.Rows[i]["KTEXT"].ToString() + "</li>");
                                        //                         break;
                                        //                     case "DELETE":
                                        //                         Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                        //                            + " - <b>" + Dt_AUFK.Rows[i]["$action"].ToString() + "</b> - " + Dt_AUFK.Rows[i]["AUFNR1"].ToString() + " - " + Dt_AUFK.Rows[i]["KTEXT1"].ToString() + "</li>");
                                        //                         break;
                                        //                     default:
                                        //                         break;
                                        //                 }
                                        //             }
                                        //         }
                                        //     }
                                        //     catch (Exception Ex)
                                        //     { ObjLogError.LogError(sDrpdwnsLogPath, " Error occured in table AUFK, when Inserting / Updating the record for AUFK - " + Ex.Message); }
                                        //     File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));

                                        //     #endregion
                                        //     break;

                                        case "T556B": // -----------T556B Order master data -------
                                            #region T556B
                                            try
                                            {
                                                usp_xml_create_T556BTableAdapter T556B_Adap = new usp_xml_create_T556BTableAdapter();
                                                DataTable T556B = Dt.DefaultView.ToTable(false, "MOPGK", "MOZKO", "KTART", "KTEXT");
                                                using (DataTable Dt_T556B = T556B_Adap.T556B_GetData(T556B, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">--------------------------------- T556B ------------------- Absence, Quota Type : T556B ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_T556B.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_T556B.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_T556B.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_T556B.Rows.Count; i++)
                                                    {
                                                        switch (Dt_T556B.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                                                    + " - <b>" + Dt_T556B.Rows[i]["$action"].ToString() + "</b> - " + Dt_T556B.Rows[i]["MOPGK"].ToString() + " - " + Dt_T556B.Rows[i]["MOZKO"].ToString() + " - " + Dt_T556B.Rows[i]["KTART"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T556B " + Dt_T556B.Rows[i]["$action"].ToString() + "</b> - " + Dt_T556B.Rows[i]["MOPGK"].ToString() + " - " + Dt_T556B.Rows[i]["MOZKO"].ToString() + " - " + Dt_T556B.Rows[i]["KTART"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T556B.Rows[i]["$action"].ToString() + "</b> - " + Dt_T556B.Rows[i]["MOPGK"].ToString() + " - " + Dt_T556B.Rows[i]["MOZKO"].ToString() + " - " + Dt_T556B.Rows[i]["KTART"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T556B " + Dt_T556B.Rows[i]["$action"].ToString() + "</b> - " + Dt_T556B.Rows[i]["MOPGK"].ToString() + " - " + Dt_T556B.Rows[i]["MOZKO"].ToString() + " - " + Dt_T556B.Rows[i]["KTART"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T556B.Rows[i]["$action"].ToString() + "</b> - " + Dt_T556B.Rows[i]["MOPGK1"].ToString() + " - " + Dt_T556B.Rows[i]["MOZKO1"].ToString() + " - " + Dt_T556B.Rows[i]["KTART1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T556B " + Dt_T556B.Rows[i]["$action"].ToString() + "</b> - " + Dt_T556B.Rows[i]["MOPGK1"].ToString() + " - " + Dt_T556B.Rows[i]["MOZKO1"].ToString() + " - " + Dt_T556B.Rows[i]["KTART1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            { ObjLogError.LogError(sDrpdwnsLogPath, " Error occured in table T556B, when Inserting / Updating the record for T556B - " + Ex.Message); }
                                            //File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));

                                            #endregion
                                            break;
                                        case "BNKA": // -----------Bank information - BANK ------
                                            #region BNKA
                                            try
                                            {
                                                usp_xml_create_BNKATableAdapter BNKA_Adap = new usp_xml_create_BNKATableAdapter();
                                                DataTable PA0002 = Dt.DefaultView.ToTable(false, "BANKS", "BANKL", "BANKA");
                                                using (DataTable Dt_BNKA = BNKA_Adap.BNKA_GetData(PA0002, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">--------------------------------- BANK ------------------- Bank Details: BANK ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_BNKA.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_BNKA.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_BNKA.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_BNKA.Rows.Count; i++)
                                                    {
                                                        switch (Dt_BNKA.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                                                    + " - <b>" + Dt_BNKA.Rows[i]["$action"].ToString() + "</b> - " + Dt_BNKA.Rows[i]["BANKS"].ToString() + " - " + Dt_BNKA.Rows[i]["BANKL"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "BNKA " + Dt_BNKA.Rows[i]["$action"].ToString() + "</b> - " + Dt_BNKA.Rows[i]["BANKS"].ToString() + " - " + Dt_BNKA.Rows[i]["BANKL"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_BNKA.Rows[i]["$action"].ToString() + "</b> - " + Dt_BNKA.Rows[i]["BANKS"].ToString() + " - " + Dt_BNKA.Rows[i]["BANKL"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "BNKA " + Dt_BNKA.Rows[i]["$action"].ToString() + "</b> - " + Dt_BNKA.Rows[i]["BANKS"].ToString() + " - " + Dt_BNKA.Rows[i]["BANKL"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_BNKA.Rows[i]["$action"].ToString() + "</b> - " + Dt_BNKA.Rows[i]["BANKS1"].ToString() + " - " + Dt_BNKA.Rows[i]["BANKL1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "BNKA " + Dt_BNKA.Rows[i]["$action"].ToString() + "</b> - " + Dt_BNKA.Rows[i]["BANKS1"].ToString() + " - " + Dt_BNKA.Rows[i]["BANKL1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            { ObjLogError.LogError(sDrpdwnsLogPath, " Error occured in table BANK, when Inserting / Updating the record for BNKA - " + Ex.Message); }
                                            //File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));

                                            #endregion
                                            break;
                                        case "T554T": // -----------Leave / Attendance types - T554T ------
                                            #region T554T
                                            try
                                            {
                                                usp_xml_create_T554TTableAdapter T554T_Adap = new usp_xml_create_T554TTableAdapter();
                                                Dt.Columns.Add("status", typeof(string)).DefaultValue = "active";
                                                DataTable T554T = Dt.DefaultView.ToTable(false, "MOABW", "AWART", "ATEXT", "status");
                                                using (DataTable Dt_T554T = T554T_Adap.T554T_GetData(T554T, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">--------------------------------- T554T ------------------- Leave / Attendance types : T554T ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_T554T.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_T554T.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_T554T.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_T554T.Rows.Count; i++)
                                                    {
                                                        switch (Dt_T554T.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                                                    + " - <b>" + Dt_T554T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T554T.Rows[i]["MOAWB"].ToString() + " - " + Dt_T554T.Rows[i]["AWART"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T554T " + Dt_T554T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T554T.Rows[i]["MOAWB"].ToString() + " - " + Dt_T554T.Rows[i]["AWART"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T554T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T554T.Rows[i]["MOAWB"].ToString() + " - " + Dt_T554T.Rows[i]["AWART"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T554T " + Dt_T554T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T554T.Rows[i]["MOAWB"].ToString() + " - " + Dt_T554T.Rows[i]["AWART"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T554T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T554T.Rows[i]["MOAWB1"].ToString() + " - " + Dt_T554T.Rows[i]["AWART1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T554T " + Dt_T554T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T554T.Rows[i]["MOAWB1"].ToString() + " - " + Dt_T554T.Rows[i]["AWART1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            { ObjLogError.LogError(sDrpdwnsLogPath, " Error occured in table T554T, when Inserting / Updating the record for T554T - " + Ex.Message); }
                                            //File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));

                                            #endregion
                                            break;


                                        case "T591S": // ----------- ADDRESS TYPE------
                                            #region T591S
                                            try
                                            {
                                                usp_xml_create_T591STableAdapter T591S_Adap = new usp_xml_create_T591STableAdapter();
                                                DataTable T591S = Dt.DefaultView.ToTable(false, "INFTY", "SUBTY", "STEXT");
                                                using (DataTable Dt_T591S = T591S_Adap.T591S_GetData(T591S, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">--------------------------------- T591S ------------------- Address Type  : T591S ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_T591S.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_T591S.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_T591S.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_T591S.Rows.Count; i++)
                                                    {
                                                        switch (Dt_T591S.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                                                    + " - <b>" + Dt_T591S.Rows[i]["$action"].ToString() + "</b> - " + Dt_T591S.Rows[i]["INFTY"].ToString() + " - " + Dt_T591S.Rows[i]["SUBTY"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T591S " + Dt_T591S.Rows[i]["$action"].ToString() + "</b> - " + Dt_T591S.Rows[i]["INFTY"].ToString() + " - " + Dt_T591S.Rows[i]["SUBTY"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T591S.Rows[i]["$action"].ToString() + "</b> - " + Dt_T591S.Rows[i]["INFTY"].ToString() + " - " + Dt_T591S.Rows[i]["SUBTY"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T591S " + Dt_T591S.Rows[i]["$action"].ToString() + "</b> - " + Dt_T591S.Rows[i]["INFTY"].ToString() + " - " + Dt_T591S.Rows[i]["SUBTY"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T591S.Rows[i]["$action"].ToString() + "</b> - " + Dt_T591S.Rows[i]["INFTY1"].ToString() + " - " + Dt_T591S.Rows[i]["SUBTY1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T591S " + Dt_T591S.Rows[i]["$action"].ToString() + "</b> - " + Dt_T591S.Rows[i]["INFTY1"].ToString() + " - " + Dt_T591S.Rows[i]["SUBTY1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)

                                            { ObjLogError.LogError(sDrpdwnsLogPath, " Error occured in table T591S, when Inserting / Updating the record for T591S - " + Ex.Message); }
                                            //File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));


                                            #endregion
                                            break;

                                        case "TCURT": // ----------- Currency Code Names masters - TCURT ------
                                            #region TCURT
                                            try
                                            {
                                                usp_xml_create_TCURTTableAdapter TCURT_Adap = new usp_xml_create_TCURTTableAdapter();
                                                DataTable TCURT = Dt.DefaultView.ToTable(false, "WAERS", "LTEXT");
                                                using (DataTable Dt_TCURT = TCURT_Adap.TCURT_GetData(TCURT, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">--------------------------------- TCURT ------------------- Currency Code Names masters  : TCURT ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_TCURT.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_TCURT.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_TCURT.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_TCURT.Rows.Count; i++)
                                                    {
                                                        switch (Dt_TCURT.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                                                    + " - <b>" + Dt_TCURT.Rows[i]["$action"].ToString() + "</b> - " + Dt_TCURT.Rows[i]["WAERS"].ToString() + " - " + Dt_TCURT.Rows[i]["LTEXT"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "TCURT " + Dt_TCURT.Rows[i]["$action"].ToString() + "</b> - " + Dt_TCURT.Rows[i]["WAERS"].ToString() + " - " + Dt_TCURT.Rows[i]["LTEXT"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_TCURT.Rows[i]["$action"].ToString() + "</b> - " + Dt_TCURT.Rows[i]["WAERS"].ToString() + " - " + Dt_TCURT.Rows[i]["LTEXT"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "TCURT " + Dt_TCURT.Rows[i]["$action"].ToString() + "</b> - " + Dt_TCURT.Rows[i]["WAERS"].ToString() + " - " + Dt_TCURT.Rows[i]["LTEXT"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_TCURT.Rows[i]["$action"].ToString() + "</b> - " + Dt_TCURT.Rows[i]["WAERS1"].ToString() + " - " + Dt_TCURT.Rows[i]["LTEXT1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "TCURT " + Dt_TCURT.Rows[i]["$action"].ToString() + "</b> - " + Dt_TCURT.Rows[i]["WAERS1"].ToString() + " - " + Dt_TCURT.Rows[i]["LTEXT1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            { ObjLogError.LogError(sDrpdwnsLogPath, " Error occured in table TCURT, when Inserting / Updating the record for TCURT - " + Ex.Message); }
                                            //File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));

                                            #endregion
                                            break;

                                        case "T001P": // ----------- Personal Area / Sub Area - T001P ------
                                            #region T001P
                                            try
                                            {
                                                usp_xml_create_T001PTableAdapter T001P_Adap = new usp_xml_create_T001PTableAdapter();
                                                DataTable T001P = Dt.DefaultView.ToTable(false, "WERKS", "BTRTL", "BTEXT", "MOABW", "MOZKO");
                                                using (DataTable Dt_T001P = T001P_Adap.T001P_GetData(T001P, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">--------------------------------- T001P ------------------- Personal Area / Sub Area  : T001P ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_T001P.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_T001P.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_T001P.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_T001P.Rows.Count; i++)
                                                    {
                                                        switch (Dt_T001P.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                                                    + " - <b>" + Dt_T001P.Rows[i]["$action"].ToString() + "</b> - " + Dt_T001P.Rows[i]["WERKS"].ToString() + " - " + Dt_T001P.Rows[i]["BTRTL"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T001P " + Dt_T001P.Rows[i]["$action"].ToString() + "</b> - " + Dt_T001P.Rows[i]["WERKS1"].ToString() + " - " + Dt_T001P.Rows[i]["BTRTL1"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T001P.Rows[i]["$action"].ToString() + "</b> - " + Dt_T001P.Rows[i]["WERKS"].ToString() + " - " + Dt_T001P.Rows[i]["BTRTL"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T001P " + Dt_T001P.Rows[i]["$action"].ToString() + "</b> - " + Dt_T001P.Rows[i]["WERKS1"].ToString() + " - " + Dt_T001P.Rows[i]["BTRTL1"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T001P.Rows[i]["$action"].ToString() + "</b> - " + Dt_T001P.Rows[i]["WERKS1"].ToString() + " - " + Dt_T001P.Rows[i]["BTRTL1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T001P " + Dt_T001P.Rows[i]["$action"].ToString() + "</b> - " + Dt_T001P.Rows[i]["WERKS1"].ToString() + " - " + Dt_T001P.Rows[i]["BTRTL1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            { ObjLogError.LogError(sDrpdwnsLogPath, " Error occured in table T001P, when Inserting / Updating the record for T001P - " + Ex.Message); }
                                            //File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));

                                            #endregion
                                            break;


                                        case "T591A": // -----------Address------
                                            #region T591A
                                            try
                                            {
                                                usp_xml_create_T591ATableAdapter T591A_Adap = new usp_xml_create_T591ATableAdapter();
                                                DataTable T591A = Dt.DefaultView.ToTable(false, "INFTY", "SUBTY", "ZEITB");
                                                using (DataTable Dt_T591A = T591A_Adap.T591A_GetData(T591A, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">--------------------------------- T591A -------------------Address  : T591A ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_T591A.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_T591A.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_T591A.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_T591A.Rows.Count; i++)
                                                    {
                                                        switch (Dt_T591A.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                                                    + " - <b>" + Dt_T591A.Rows[i]["$action"].ToString() + "</b> - " + Dt_T591A.Rows[i]["INFTY"].ToString() + " - " + Dt_T591A.Rows[i]["SUBTY"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T591A " + Dt_T591A.Rows[i]["$action"].ToString() + "</b> - " + Dt_T591A.Rows[i]["INFTY"].ToString() + " - " + Dt_T591A.Rows[i]["SUBTY"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T591A.Rows[i]["$action"].ToString() + "</b> - " + Dt_T591A.Rows[i]["INFTY"].ToString() + " - " + Dt_T591A.Rows[i]["SUBTY"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T591A " + Dt_T591A.Rows[i]["$action"].ToString() + "</b> - " + Dt_T591A.Rows[i]["INFTY"].ToString() + " - " + Dt_T591A.Rows[i]["SUBTY"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T591A.Rows[i]["$action"].ToString() + "</b> - " + Dt_T591A.Rows[i]["INFTY1"].ToString() + " - " + Dt_T591A.Rows[i]["SUBTY1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T591A " + Dt_T591A.Rows[i]["$action"].ToString() + "</b> - " + Dt_T591A.Rows[i]["INFTY1"].ToString() + " - " + Dt_T591A.Rows[i]["SUBTY1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            { ObjLogError.LogError(sDrpdwnsLogPath, " Error occured in table T591A, when Inserting / Updating the record for T59A - " + Ex.Message); }
                                            // File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));

                                            #endregion
                                            break;

                                        case "T005T": // -----------Country----
                                            #region T005T
                                            try
                                            {
                                                usp_xml_create_T005TTableAdapter T005T_Adap = new usp_xml_create_T005TTableAdapter();
                                                DataTable T005T = Dt.DefaultView.ToTable(false, "LAND1", "LANDX", "NATIO");
                                                using (DataTable Dt_T005T = T005T_Adap.T005T_GetData(T005T, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">--------------------------------- T005T ------------------- Country  : T005T ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_T005T.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_T005T.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_T005T.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_T005T.Rows.Count; i++)
                                                    {
                                                        switch (Dt_T005T.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                                                    + " - <b>" + Dt_T005T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T005T.Rows[i]["LAND1"].ToString() + " - " + Dt_T005T.Rows[i]["LANDX"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T005T " + Dt_T005T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T005T.Rows[i]["LAND1"].ToString() + " - " + Dt_T005T.Rows[i]["LANDX"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T005T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T005T.Rows[i]["LAND1"].ToString() + " - " + Dt_T005T.Rows[i]["LANDX"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T005T " + Dt_T005T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T005T.Rows[i]["LAND1"].ToString() + " - " + Dt_T005T.Rows[i]["LANDX"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T005T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T005T.Rows[i]["LAND11"].ToString() + " - " + Dt_T005T.Rows[i]["LANDX1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T005T " + Dt_T005T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T005T.Rows[i]["LAND11"].ToString() + " - " + Dt_T005T.Rows[i]["LANDX1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            { ObjLogError.LogError(sDrpdwnsLogPath, " Error occured in table T005T, when Inserting / Updating the record for T005T - " + Ex.Message); }
                                            // File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));

                                            #endregion
                                            break;

                                        case "T005U": // -----------State----
                                            #region T005U
                                            try
                                            {
                                                usp_xml_create_T005UTableAdapter T005U_Adap = new usp_xml_create_T005UTableAdapter();
                                                DataTable T005U = Dt.DefaultView.ToTable(false, "LAND1", "BLAND", "BEZEI");
                                                using (DataTable Dt_T005U = T005U_Adap.T005U_GetData(T005U, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">--------------------------------- T005U ------------------- State  : T005U ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_T005U.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_T005U.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_T005U.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_T005U.Rows.Count; i++)
                                                    {
                                                        switch (Dt_T005U.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                                                    + " - <b>" + Dt_T005U.Rows[i]["$action"].ToString() + "</b> - " + Dt_T005U.Rows[i]["LAND1"].ToString() + " - " + Dt_T005U.Rows[i]["BLAND"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T005U " + Dt_T005U.Rows[i]["$action"].ToString() + "</b> - " + Dt_T005U.Rows[i]["LAND1"].ToString() + " - " + Dt_T005U.Rows[i]["BLAND"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T005U.Rows[i]["$action"].ToString() + "</b> - " + Dt_T005U.Rows[i]["LAND1"].ToString() + " - " + Dt_T005U.Rows[i]["BLAND"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T005U " + Dt_T005U.Rows[i]["$action"].ToString() + "</b> - " + Dt_T005U.Rows[i]["LAND1"].ToString() + " - " + Dt_T005U.Rows[i]["BLAND"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T005U.Rows[i]["$action"].ToString() + "</b> - " + Dt_T005U.Rows[i]["LAND11"].ToString() + " - " + Dt_T005U.Rows[i]["BLAND1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T005U " + Dt_T005U.Rows[i]["$action"].ToString() + "</b> - " + Dt_T005U.Rows[i]["LAND11"].ToString() + " - " + Dt_T005U.Rows[i]["BLAND1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            { ObjLogError.LogError(sDrpdwnsLogPath, " Error occured in table T005U, when Inserting / Updating the record for T005U - " + Ex.Message); }
                                            //File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));

                                            #endregion
                                            break;

                                        case "T042Z": // -----------Bank Type----
                                            #region T042Z
                                            try
                                            {
                                                usp_xml_create_T042ZTableAdapter T042Z_Adap = new usp_xml_create_T042ZTableAdapter();
                                                DataTable T042Z = Dt.DefaultView.ToTable(false, "LAND1", "ZLSCH", "TEXT1");
                                                using (DataTable Dt_T042Z = T042Z_Adap.T042Z_GetData(T042Z, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">--------------------------------- T042Z -------------------Bank Type  : T042Z ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_T042Z.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_T042Z.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_T042Z.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_T042Z.Rows.Count; i++)
                                                    {
                                                        switch (Dt_T042Z.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                                                    + " - <b>" + Dt_T042Z.Rows[i]["$action"].ToString() + "</b> - " + Dt_T042Z.Rows[i]["LAND1"].ToString() + " - " + Dt_T042Z.Rows[i]["ZLSCH"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T042Z " + Dt_T042Z.Rows[i]["$action"].ToString() + "</b> - " + Dt_T042Z.Rows[i]["LAND1"].ToString() + " - " + Dt_T042Z.Rows[i]["ZLSCH"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T042Z.Rows[i]["$action"].ToString() + "</b> - " + Dt_T042Z.Rows[i]["LAND1"].ToString() + " - " + Dt_T042Z.Rows[i]["ZLSCH"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T042Z " + Dt_T042Z.Rows[i]["$action"].ToString() + "</b> - " + Dt_T042Z.Rows[i]["LAND1"].ToString() + " - " + Dt_T042Z.Rows[i]["ZLSCH"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T042Z.Rows[i]["$action"].ToString() + "</b> - " + Dt_T042Z.Rows[i]["LAND11"].ToString() + " - " + Dt_T042Z.Rows[i]["ZLSCH1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T042Z " + Dt_T042Z.Rows[i]["$action"].ToString() + "</b> - " + Dt_T042Z.Rows[i]["LAND11"].ToString() + " - " + Dt_T042Z.Rows[i]["ZLSCH1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            { ObjLogError.LogError(sDrpdwnsLogPath, " Error occured in table T042Z, when Inserting / Updating the record for T042Z - " + Ex.Message); }
                                            // File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));

                                            #endregion
                                            break;

                                        case "T5R06": // -----------Personal ID Types----
                                            #region T5R06
                                            try
                                            {
                                                usp_xml_create_T5R06TableAdapter T5R06_Adap = new usp_xml_create_T5R06TableAdapter();
                                                DataTable T5R06 = Dt.DefaultView.ToTable(false, "MOLGA", "ICTYP", "ICTXT");
                                                using (DataTable Dt_T5R06 = T5R06_Adap.T5R06_GetData(T5R06, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">--------------------------------- T5R06 ------------------- Personal ID Types  : T5R06 ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_T5R06.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_T5R06.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_T5R06.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_T5R06.Rows.Count; i++)
                                                    {
                                                        switch (Dt_T5R06.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                                                    + " - <b>" + Dt_T5R06.Rows[i]["$action"].ToString() + "</b> - " + Dt_T5R06.Rows[i]["MOLGA"].ToString() + " - " + Dt_T5R06.Rows[i]["ICTYP"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T5R06 " + Dt_T5R06.Rows[i]["$action"].ToString() + "</b> - " + Dt_T5R06.Rows[i]["MOLGA"].ToString() + " - " + Dt_T5R06.Rows[i]["ICTYP"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T5R06.Rows[i]["$action"].ToString() + "</b> - " + Dt_T5R06.Rows[i]["MOLGA"].ToString() + " - " + Dt_T5R06.Rows[i]["ICTYP"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T5R06 " + Dt_T5R06.Rows[i]["$action"].ToString() + "</b> - " + Dt_T5R06.Rows[i]["MOLGA"].ToString() + " - " + Dt_T5R06.Rows[i]["ICTYP"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T5R06.Rows[i]["$action"].ToString() + "</b> - " + Dt_T5R06.Rows[i]["MOLGA1"].ToString() + " - " + Dt_T5R06.Rows[i]["ICTYP1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T5R06 " + Dt_T5R06.Rows[i]["$action"].ToString() + "</b> - " + Dt_T5R06.Rows[i]["MOLGA1"].ToString() + " - " + Dt_T5R06.Rows[i]["ICTYP1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            { ObjLogError.LogError(sDrpdwnsLogPath, " Error occured in table T5R06, when Inserting / Updating the record for T5R06 - " + Ex.Message); }
                                            //File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));

                                            #endregion
                                            break;

                                        case "T002T": // -----------Language----
                                            #region T002T
                                            try
                                            {
                                                usp_xml_create_T002TTableAdapter T002T_Adap = new usp_xml_create_T002TTableAdapter();
                                                DataTable T002T = Dt.DefaultView.ToTable(false, "SPRSL", "SPTXT");
                                                using (DataTable Dt_T002T = T002T_Adap.T002T_GetData(T002T, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">--------------------------------- T002T ------------------- Language  : T002T ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_T002T.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_T002T.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_T002T.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_T002T.Rows.Count; i++)
                                                    {
                                                        switch (Dt_T002T.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                                                    + " - <b>" + Dt_T002T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T002T.Rows[i]["SPRSL"].ToString() + " - " + Dt_T002T.Rows[i]["SPTXT"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T002T " + Dt_T002T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T002T.Rows[i]["SPRSL"].ToString() + " - " + Dt_T002T.Rows[i]["SPTXT"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T002T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T002T.Rows[i]["SPRSL"].ToString() + " - " + Dt_T002T.Rows[i]["SPTXT"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T002T " + Dt_T002T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T002T.Rows[i]["SPRSL"].ToString() + " - " + Dt_T002T.Rows[i]["SPTXT"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T002T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T002T.Rows[i]["SPRSL1"].ToString() + " - " + Dt_T002T.Rows[i]["SPTXT1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T002T " + Dt_T002T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T002T.Rows[i]["SPRSL1"].ToString() + " - " + Dt_T002T.Rows[i]["SPTXT1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            { ObjLogError.LogError(sDrpdwnsLogPath, " Error occured in table T002T, when Inserting / Updating the record for T002T - " + Ex.Message); }
                                            //File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));

                                            #endregion
                                            break;

                                        case "T516T": // -----------Religion----
                                            #region T516T
                                            try
                                            {
                                                usp_xml_create_T516TTableAdapter T516T_Adap = new usp_xml_create_T516TTableAdapter();
                                                DataTable T516T = Dt.DefaultView.ToTable(false, "KONFE", "KTEXT");
                                                using (DataTable Dt_T516T = T516T_Adap.T516T_GetData(T516T, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">--------------------------------- T516t ------------------- Religion  : T516T ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_T516T.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_T516T.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_T516T.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_T516T.Rows.Count; i++)
                                                    {
                                                        switch (Dt_T516T.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                                                    + " - <b>" + Dt_T516T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T516T.Rows[i]["KONFE"].ToString() + " - " + Dt_T516T.Rows[i]["KTEXT"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T516T " + Dt_T516T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T516T.Rows[i]["KITXT"].ToString() + " - " + Dt_T516T.Rows[i]["KTEXT"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T516T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T516T.Rows[i]["KONFE"].ToString() + " - " + Dt_T516T.Rows[i]["KTEXT"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T516T " + Dt_T516T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T516T.Rows[i]["KITXT"].ToString() + " - " + Dt_T516T.Rows[i]["KTEXT"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T516T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T516T.Rows[i]["KONFE1"].ToString() + " - " + Dt_T516T.Rows[i]["KTEXT1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T516T " + Dt_T516T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T516T.Rows[i]["KITXT1"].ToString() + " - " + Dt_T516T.Rows[i]["KTEXT1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            { ObjLogError.LogError(sDrpdwnsLogPath, " Error occured in table T516T, when Inserting / Updating the record for T516T - " + Ex.Message); }
                                            // File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));

                                            #endregion
                                            break;


                                        case "T502T": // -----------Marital Status----
                                            #region T502T
                                            try
                                            {
                                                usp_xml_create_T502TTableAdapter T502T_Adap = new usp_xml_create_T502TTableAdapter();
                                                DataTable T502T = Dt.DefaultView.ToTable(false, "FAMST", "FTEXT");
                                                using (DataTable Dt_T502T = T502T_Adap.T502T_GetData(T502T, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">--------------------------------- T502T ------------------- Marital Status  : T502T ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_T502T.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_T502T.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_T502T.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_T502T.Rows.Count; i++)
                                                    {
                                                        switch (Dt_T502T.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                                                    + " - <b>" + Dt_T502T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T502T.Rows[i]["FAMST"].ToString() + " - " + Dt_T502T.Rows[i]["FTEXT"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T502T " + Dt_T502T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T502T.Rows[i]["FAMST"].ToString() + " - " + Dt_T502T.Rows[i]["FTEXT"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T502T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T502T.Rows[i]["FAMST"].ToString() + " - " + Dt_T502T.Rows[i]["FTEXT"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T502T " + Dt_T502T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T502T.Rows[i]["FAMST"].ToString() + " - " + Dt_T502T.Rows[i]["FTEXT"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T502T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T502T.Rows[i]["FAMST1"].ToString() + " - " + Dt_T502T.Rows[i]["FTEXT1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T502T " + Dt_T502T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T502T.Rows[i]["FAMST1"].ToString() + " - " + Dt_T502T.Rows[i]["FTEXT1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            { ObjLogError.LogError(sDrpdwnsLogPath, " Error occured in table T502T, when Inserting / Updating the record for T502T - " + Ex.Message); }
                                            //File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));

                                            #endregion
                                            break;


                                        case "T522T": // -----------Personal Data Title----
                                            #region T522T
                                            try
                                            {
                                                usp_xml_create_T522TTableAdapter T522T_Adap = new usp_xml_create_T522TTableAdapter();
                                                DataTable T522T = Dt.DefaultView.ToTable(false, "ANRED", "ATEXT", "ANRLT");
                                                using (DataTable Dt_T522T = T522T_Adap.T522T_GetData(T522T, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">--------------------------------- T522T ------------------- Personal Data Title  : T522T ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_T522T.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_T522T.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_T522T.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_T522T.Rows.Count; i++)
                                                    {
                                                        switch (Dt_T522T.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                                                    + " - <b>" + Dt_T522T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T522T.Rows[i]["ANRED"].ToString() + " - " + Dt_T522T.Rows[i]["ATEXT"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T522T " + Dt_T522T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T522T.Rows[i]["ANRED"].ToString() + " - " + Dt_T522T.Rows[i]["ATEXT"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T522T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T522T.Rows[i]["ANRED"].ToString() + " - " + Dt_T522T.Rows[i]["ATEXT"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T522T " + Dt_T522T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T522T.Rows[i]["ANRED"].ToString() + " - " + Dt_T522T.Rows[i]["ATEXT"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T522T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T522T.Rows[i]["ANRED1"].ToString() + " - " + Dt_T522T.Rows[i]["ATEXT1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T522T " + Dt_T522T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T522T.Rows[i]["ANRED1"].ToString() + " - " + Dt_T522T.Rows[i]["ATEXT1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            { ObjLogError.LogError(sDrpdwnsLogPath, " Error occured in table T522T, when Inserting / Updating the record for T522T - " + Ex.Message); }
                                            // File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));

                                            #endregion
                                            break;


                                        // case "T526": // ----------- Administrators Pooled table----
                                        //     #region T526
                                        //     try
                                        //     {
                                        //         usp_xml_create_T526TableAdapter T526_Adap = new usp_xml_create_T526TableAdapter();
                                        //         DataTable T526 = Dt.DefaultView.ToTable(false, "WERKS", "SACHX", "SACHN", "USRID");
                                        //         using (DataTable Dt_T526 = T526_Adap.T526_GetData(T526, 1))
                                        //         {
                                        //             Sb.Append("<li class=\"Gr\">--------------------------------- T526 -------------------  Administrators Pooled table  : T526 ------------------------ "
                                        //+ "[<span class=\"G\">INSERT</span>] - " + Dt_T526.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                        //+ "[<span class=\"B\">UPDATE</span>] - " + Dt_T526.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                        //+ "[<span class=\"R\">DELETE</span>] - " + Dt_T526.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                        //             for (int i = 0; i < Dt_T526.Rows.Count; i++)
                                        //             {
                                        //                 switch (Dt_T526.Rows[i]["$action"].ToString())
                                        //                 {

                                        //                     case "INSERT":
                                        //                         Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                        //                             + " - <b>" + Dt_T526.Rows[i]["$action"].ToString() + "</b> - " + Dt_T526.Rows[i]["WERKS"].ToString() + " - " + Dt_T526.Rows[i]["SACHX"].ToString() + "</li>");
                                        //                         break;
                                        //                     case "UPDATE":
                                        //                         Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                        //                            + " - <b>" + Dt_T526.Rows[i]["$action"].ToString() + "</b> - " + Dt_T526.Rows[i]["WERKS"].ToString() + " - " + Dt_T526.Rows[i]["SACHX"].ToString() + "</li>");
                                        //                         break;
                                        //                     case "DELETE":
                                        //                         Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                        //                            + " - <b>" + Dt_T526.Rows[i]["$action"].ToString() + "</b> - " + Dt_T526.Rows[i]["WERKS1"].ToString() + " - " + Dt_T526.Rows[i]["SACHX1"].ToString() + "</li>");
                                        //                         break;
                                        //                     default:
                                        //                         break;
                                        //                 }
                                        //             }
                                        //         }
                                        //     }
                                        //     catch (Exception Ex)
                                        //     { ObjLogError.LogError(sDrpdwnsLogPath, " Error occured in table T526, when Inserting / Updating the record for T526 - " + Ex.Message); }
                                        //     File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));

                                        //     #endregion
                                        //     break;


                                        case "_-ITCHAMPS_-HOLIDA": // ZHR_HOLIDAYS changed to _-ITCHAMPS_-HOLIDA----------Holiday Table----
                                            #region ZHR_HOLIDAYS
                                            try
                                            {
                                                usp_xml_create_ZHRHOLIDAYSTableAdapter ZHRHOLIDAYS_Adap = new usp_xml_create_ZHRHOLIDAYSTableAdapter();
                                                DataTable ZHRHOLIDAYS = Dt.DefaultView.ToTable(false, "ZYEAR", "ZDATE", "TXT_LONG","KLASS");
                                                using (DataTable Dt_ZHRHOLIDAYS = ZHRHOLIDAYS_Adap.ZHRHOLIDAYS_GetData(ZHRHOLIDAYS, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">--------------------------------- ZHR_HOLIDAYS ------------------- Holiday Table  : ZHR_HOLIDAYS ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_ZHRHOLIDAYS.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_ZHRHOLIDAYS.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_ZHRHOLIDAYS.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_ZHRHOLIDAYS.Rows.Count; i++)
                                                    {
                                                        switch (Dt_ZHRHOLIDAYS.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                                                    + " - <b>" + Dt_ZHRHOLIDAYS.Rows[i]["$action"].ToString() + "</b> - " + Dt_ZHRHOLIDAYS.Rows[i]["ZYEAR"].ToString() + " - " + Dt_ZHRHOLIDAYS.Rows[i]["ZDATE"].ToString() + " - " + Dt_ZHRHOLIDAYS.Rows[i]["TXT_LONG"].ToString() + " - " + Dt_ZHRHOLIDAYS.Rows[i]["KLASS"].ToString() +"</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "ZHRHOLIDAYS " + Dt_ZHRHOLIDAYS.Rows[i]["ZYEAR"].ToString() + " - " + Dt_ZHRHOLIDAYS.Rows[i]["ZDATE"].ToString() + " - " + Dt_ZHRHOLIDAYS.Rows[i]["TXT_LONG"].ToString() + " - " + Dt_ZHRHOLIDAYS.Rows[i]["KLASS"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_ZHRHOLIDAYS.Rows[i]["$action"].ToString() + "</b> - " + Dt_ZHRHOLIDAYS.Rows[i]["ZYEAR"].ToString() + " - " + Dt_ZHRHOLIDAYS.Rows[i]["ZDATE"].ToString() + " - " + Dt_ZHRHOLIDAYS.Rows[i]["TXT_LONG"].ToString() + " - " + Dt_ZHRHOLIDAYS.Rows[i]["KLASS"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "ZHRHOLIDAYS " + Dt_ZHRHOLIDAYS.Rows[i]["ZYEAR"].ToString() + " - " + Dt_ZHRHOLIDAYS.Rows[i]["ZDATE"].ToString() + " - " + Dt_ZHRHOLIDAYS.Rows[i]["TXT_LONG"].ToString() + " - " + Dt_ZHRHOLIDAYS.Rows[i]["KLASS"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_ZHRHOLIDAYS.Rows[i]["$action"].ToString() + "</b> - " + Dt_ZHRHOLIDAYS.Rows[i]["ZYEAR1"].ToString() + " - " + Dt_ZHRHOLIDAYS.Rows[i]["ZDATE1"].ToString() + " - " + Dt_ZHRHOLIDAYS.Rows[i]["TXT_LONG1"].ToString() + " - " + Dt_ZHRHOLIDAYS.Rows[i]["KLASS"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "ZHRHOLIDAYS " + Dt_ZHRHOLIDAYS.Rows[i]["ZYEAR1"].ToString() + " - " + Dt_ZHRHOLIDAYS.Rows[i]["ZDATE1"].ToString() + " - " + Dt_ZHRHOLIDAYS.Rows[i]["TXT_LONG1"].ToString() + " - " + Dt_ZHRHOLIDAYS.Rows[i]["KLASS"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            { ObjLogError.LogError(sDrpdwnsLogPath, " Error occured in table ZHR_Holidays, when Inserting / Updating the record for ZHR_Holidays - " + Ex.Message); }
                                            //File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));

                                            #endregion
                                            break;

                                        case "CSKT": // -----------Cost Center----
                                            #region CSKT
                                            try
                                            {
                                                usp_xml_create_CSKTTableAdapter CSKT_Adap = new usp_xml_create_CSKTTableAdapter();
                                                DataTable CSKT = Dt.DefaultView.ToTable(false, "KOSTL", "LTEXT");
                                                using (DataTable Dt_CSKT = CSKT_Adap.CSKT_GetData(CSKT, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">--------------------------------- CSKT --------------------Cost Center  : CSKT ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_CSKT.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_CSKT.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_CSKT.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_CSKT.Rows.Count; i++)
                                                    {
                                                        switch (Dt_CSKT.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                                                    + " - <b>" + Dt_CSKT.Rows[i]["$action"].ToString() + "</b> - " + Dt_CSKT.Rows[i]["KOSTL"].ToString() + " - " + Dt_CSKT.Rows[i]["LTEXT"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "CSKT " + Dt_CSKT.Rows[i]["$action"].ToString() + "</b> - " + Dt_CSKT.Rows[i]["KOSTL"].ToString() + " - " + Dt_CSKT.Rows[i]["LTEXT"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_CSKT.Rows[i]["$action"].ToString() + "</b> - " + Dt_CSKT.Rows[i]["KOSTL"].ToString() + " - " + Dt_CSKT.Rows[i]["LTEXT"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "CSKT " + Dt_CSKT.Rows[i]["$action"].ToString() + "</b> - " + Dt_CSKT.Rows[i]["KOSTL"].ToString() + " - " + Dt_CSKT.Rows[i]["LTEXT"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_CSKT.Rows[i]["$action"].ToString() + "</b> - " + Dt_CSKT.Rows[i]["KOSTL1"].ToString() + " - " + Dt_CSKT.Rows[i]["LTEXT1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "CSKT " + Dt_CSKT.Rows[i]["$action"].ToString() + "</b> - " + Dt_CSKT.Rows[i]["KOSTL1"].ToString() + " - " + Dt_CSKT.Rows[i]["LTEXT1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            { ObjLogError.LogError(sDrpdwnsLogPath, " Error occured in table CSKT, when Inserting / Updating the record for CSKT - " + Ex.Message); }
                                            // File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));

                                            #endregion
                                            break;


                                        case "PROJ": // -----------Project Defination----
                                            #region PROJ
                                            try
                                            {
                                                usp_xml_create_PROJTableAdapter PROJ_Adap = new usp_xml_create_PROJTableAdapter();
                                                DataTable PROJ = Dt.DefaultView.ToTable(false, "PSPNR", "PSPID", "POST1");
                                                using (DataTable Dt_PROJ = PROJ_Adap.PROJ_GetData(PROJ, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">--------------------------------- PROJ -------------------Project Defination  : PROJ ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_PROJ.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_PROJ.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_PROJ.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_PROJ.Rows.Count; i++)
                                                    {
                                                        switch (Dt_PROJ.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                                                    + " - <b>" + Dt_PROJ.Rows[i]["$action"].ToString() + "</b> - " + Dt_PROJ.Rows[i]["PSPNR"].ToString() + " - " + Dt_PROJ.Rows[i]["PSPID"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "PROJ " + Dt_PROJ.Rows[i]["$action"].ToString() + "</b> - " + Dt_PROJ.Rows[i]["PSPNR"].ToString() + " - " + Dt_PROJ.Rows[i]["PSPID"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_PROJ.Rows[i]["$action"].ToString() + "</b> - " + Dt_PROJ.Rows[i]["PSPNR"].ToString() + " - " + Dt_PROJ.Rows[i]["PSPID"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "PROJ " + Dt_PROJ.Rows[i]["$action"].ToString() + "</b> - " + Dt_PROJ.Rows[i]["PSPNR"].ToString() + " - " + Dt_PROJ.Rows[i]["PSPID"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_PROJ.Rows[i]["$action"].ToString() + "</b> - " + Dt_PROJ.Rows[i]["PSPNR1"].ToString() + " - " + Dt_PROJ.Rows[i]["PSPID1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "PROJ " + Dt_PROJ.Rows[i]["$action"].ToString() + "</b> - " + Dt_PROJ.Rows[i]["PSPNR1"].ToString() + " - " + Dt_PROJ.Rows[i]["PSPID1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            { ObjLogError.LogError(sDrpdwnsLogPath, " Error occured in table PROJ, when Inserting / Updating the record for PROJ - " + Ex.Message); }
                                            // File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));

                                            #endregion
                                            break;

                                        case "PRPS": // ----------WBS Element----
                                            #region PRPS
                                            try
                                            {
                                                usp_xml_create_PRPSTableAdapter PRPS_Adap = new usp_xml_create_PRPSTableAdapter();
                                                DataTable PRPS = Dt.DefaultView.ToTable(false, "PSPNR", "POSID", "POST1", "PSPHI");
                                                using (DataTable Dt_PRPS = PRPS_Adap.PRPS_GetData(PRPS, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">--------------------------------- PRPS -------------------WBS Element  : PRPS ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_PRPS.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_PRPS.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_PRPS.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_PRPS.Rows.Count; i++)
                                                    {
                                                        switch (Dt_PRPS.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                                                    + " - <b>" + Dt_PRPS.Rows[i]["$action"].ToString() + "</b> - " + Dt_PRPS.Rows[i]["PSPNR"].ToString() + " - " + Dt_PRPS.Rows[i]["POSID"].ToString() + " - " + Dt_PRPS.Rows[i]["PSPHI"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "PRPS " + Dt_PRPS.Rows[i]["$action"].ToString() + "</b> - " + Dt_PRPS.Rows[i]["PSPNR"].ToString() + " - " + Dt_PRPS.Rows[i]["POSID"].ToString() + " - " + Dt_PRPS.Rows[i]["PSPHI"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_PRPS.Rows[i]["$action"].ToString() + "</b> - " + Dt_PRPS.Rows[i]["PSPNR"].ToString() + " - " + Dt_PRPS.Rows[i]["POSID"].ToString() + " - " + Dt_PRPS.Rows[i]["PSPHI"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "PRPS " + Dt_PRPS.Rows[i]["$action"].ToString() + "</b> - " + Dt_PRPS.Rows[i]["PSPNR"].ToString() + " - " + Dt_PRPS.Rows[i]["POSID"].ToString() + " - " + Dt_PRPS.Rows[i]["PSPHI"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_PRPS.Rows[i]["$action"].ToString() + "</b> - " + Dt_PRPS.Rows[i]["PSPNR1"].ToString() + " - " + Dt_PRPS.Rows[i]["POSID1"].ToString() + " - " + Dt_PRPS.Rows[i]["PSPHI"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "PRPS " + Dt_PRPS.Rows[i]["$action"].ToString() + "</b> - " + Dt_PRPS.Rows[i]["PSPNR1"].ToString() + " - " + Dt_PRPS.Rows[i]["POSID1"].ToString() + " - " + Dt_PRPS.Rows[i]["PSPHI1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            { ObjLogError.LogError(sDrpdwnsLogPath, " Error occured in table PRPS, when Inserting / Updating the record for PRPS - " + Ex.Message); }
                                            //File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));

                                            #endregion
                                            break;

                                        //case "CAUFV": // -----------Network----
                                        case "AUFK": // -----------Network----
                                            #region CAUFV
                                            try
                                            {
                                                usp_xml_create_CAUFVTableAdapter CAUFV_Adap = new usp_xml_create_CAUFVTableAdapter();
                                                DataTable CAUFV = Dt.DefaultView.ToTable(false, "AUFNR", "KTEXT", "PSPEL");
                                                using (DataTable Dt_CAUFV = CAUFV_Adap.CAUFV_GetData(CAUFV, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">--------------------------------- CAUFV ------------------- Network  : CAUFV ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_CAUFV.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_CAUFV.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_CAUFV.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_CAUFV.Rows.Count; i++)
                                                    {
                                                        switch (Dt_CAUFV.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                                                    + " - <b>" + Dt_CAUFV.Rows[i]["$action"].ToString() + "</b> - " + Dt_CAUFV.Rows[i]["AUFNR"].ToString() + " - " + Dt_CAUFV.Rows[i]["PSPEL"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "CAUFV " + Dt_CAUFV.Rows[i]["$action"].ToString() + "</b> - " + Dt_CAUFV.Rows[i]["AUFNR"].ToString() + " - " + Dt_CAUFV.Rows[i]["PSPEL"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_CAUFV.Rows[i]["$action"].ToString() + "</b> - " + Dt_CAUFV.Rows[i]["AUFNR"].ToString() + " - " + Dt_CAUFV.Rows[i]["PSPEL"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "CAUFV " + Dt_CAUFV.Rows[i]["$action"].ToString() + "</b> - " + Dt_CAUFV.Rows[i]["AUFNR"].ToString() + " - " + Dt_CAUFV.Rows[i]["PSPEL"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_CAUFV.Rows[i]["$action"].ToString() + "</b> - " + Dt_CAUFV.Rows[i]["AUFNR1"].ToString() + " - " + Dt_CAUFV.Rows[i]["PSPEL1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "CAUFV " + Dt_CAUFV.Rows[i]["$action"].ToString() + "</b> - " + Dt_CAUFV.Rows[i]["AUFNR1"].ToString() + " - " + Dt_CAUFV.Rows[i]["PSPEL1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            { ObjLogError.LogError(sDrpdwnsLogPath, " Error occured in table CAUFV, when Inserting / Updating the record for CAUFV - " + Ex.Message); }
                                            // File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));

                                            #endregion
                                            break;


                                        case "AFVC": // -----------Activity Type----
                                            #region AFVC
                                            try
                                            {
                                                usp_xml_create_AFVCTableAdapter AFVC_Adap = new usp_xml_create_AFVCTableAdapter();
                                                DataTable AFVC = Dt.DefaultView.ToTable(false, "VORNR", "LTXA1", "PROJN", "AUFPL", "BEDID");
                                                using (DataTable Dt_AFVC = AFVC_Adap.AFVC_GetData(AFVC, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">--------------------------------- AFVC ------------------- Activity Type  : AFVC ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_AFVC.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_AFVC.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_AFVC.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_AFVC.Rows.Count; i++)
                                                    {
                                                        switch (Dt_AFVC.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                                                    + " - <b>" + Dt_AFVC.Rows[i]["$action"].ToString() + "</b> - " + Dt_AFVC.Rows[i]["VORNR"].ToString() + " - " + Dt_AFVC.Rows[i]["PROJN"].ToString() + " - " + Dt_AFVC.Rows[i]["AUFPL"].ToString() + " - " + Dt_AFVC.Rows[i]["BEDID"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "AFVC " + Dt_AFVC.Rows[i]["$action"].ToString() + "</b> - " + Dt_AFVC.Rows[i]["VORNR"].ToString() + " - " + Dt_AFVC.Rows[i]["PROJN"].ToString() + " - " + Dt_AFVC.Rows[i]["AUFPL"].ToString() + " - " + Dt_AFVC.Rows[i]["BEDID"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_AFVC.Rows[i]["$action"].ToString() + "</b> - " + Dt_AFVC.Rows[i]["VORNR"].ToString() + " - " + Dt_AFVC.Rows[i]["PROJN"].ToString() + " - " + Dt_AFVC.Rows[i]["AUFPL"].ToString() + " - " + Dt_AFVC.Rows[i]["BEDID"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "AFVC " + Dt_AFVC.Rows[i]["$action"].ToString() + "</b> - " + Dt_AFVC.Rows[i]["VORNR"].ToString() + " - " + Dt_AFVC.Rows[i]["PROJN"].ToString() + " - " + Dt_AFVC.Rows[i]["AUFPL"].ToString() + " - " + Dt_AFVC.Rows[i]["BEDID"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_AFVC.Rows[i]["$action"].ToString() + "</b> - " + Dt_AFVC.Rows[i]["VORNR1"].ToString() + " - " + Dt_AFVC.Rows[i]["PROJN1"].ToString() + " - " + Dt_AFVC.Rows[i]["AUFPL1"].ToString() + " - " + Dt_AFVC.Rows[i]["BEDID1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "AFVC " + Dt_AFVC.Rows[i]["$action"].ToString() + "</b> - " + Dt_AFVC.Rows[i]["VORNR1"].ToString() + " - " + Dt_AFVC.Rows[i]["PROJN1"].ToString() + " - " + Dt_AFVC.Rows[i]["AUFPL1"].ToString() + " - " + Dt_AFVC.Rows[i]["BEDID1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            { ObjLogError.LogError(sDrpdwnsLogPath, " Error occured in table AFVC, when Inserting / Updating the record for AFVC - " + Ex.Message); }
                                            // File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));

                                            #endregion
                                            break;

                                        case "AFKO": // -----------State----
                                            #region AFKO
                                            try
                                            {
                                                usp_xml_create_AFKOTableAdapter AFKO_Adap = new usp_xml_create_AFKOTableAdapter();
                                                DataTable AFKO = Dt.DefaultView.ToTable(false, "AUFNR", "AUFPL", "BEDID");
                                                using (DataTable Dt_AFKO = AFKO_Adap.AFKO_GetData(AFKO, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">--------------------------------- AFKO -------------------Order Header Data PP Orders Table : AFKO ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_AFKO.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_AFKO.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_AFKO.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_AFKO.Rows.Count; i++)
                                                    {
                                                        switch (Dt_AFKO.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                                                    + " - <b>" + Dt_AFKO.Rows[i]["$action"].ToString() + "</b> - " + Dt_AFKO.Rows[i]["AUFNR"].ToString() + " - " + Dt_AFKO.Rows[i]["AUFPL"].ToString() + " - " + Dt_AFKO.Rows[i]["BEDID"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "AFKO " + Dt_AFKO.Rows[i]["$action"].ToString() + "</b> - " + Dt_AFKO.Rows[i]["AUFNR"].ToString() + " - " + Dt_AFKO.Rows[i]["AUFPL"].ToString() + " - " + Dt_AFKO.Rows[i]["BEDID"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_AFKO.Rows[i]["$action"].ToString() + "</b> - " + Dt_AFKO.Rows[i]["AUFNR"].ToString() + " - " + Dt_AFKO.Rows[i]["AUFPL"].ToString() + " - " + Dt_AFKO.Rows[i]["BEDID"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "AFKO " + Dt_AFKO.Rows[i]["$action"].ToString() + "</b> - " + Dt_AFKO.Rows[i]["AUFNR"].ToString() + " - " + Dt_AFKO.Rows[i]["AUFPL"].ToString() + " - " + Dt_AFKO.Rows[i]["BEDID"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_AFKO.Rows[i]["$action"].ToString() + "</b> - " + Dt_AFKO.Rows[i]["AUFNR1"].ToString() + " - " + Dt_AFKO.Rows[i]["AUFPL1"].ToString() + " - " + Dt_AFKO.Rows[i]["BEDID1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "AFKO " + Dt_AFKO.Rows[i]["$action"].ToString() + "</b> - " + Dt_AFKO.Rows[i]["AUFNR1"].ToString() + " - " + Dt_AFKO.Rows[i]["AUFPL1"].ToString() + " - " + Dt_AFKO.Rows[i]["BEDID1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            { ObjLogError.LogError(sDrpdwnsLogPath, " Error occured in table AFKO, when Inserting / Updating the record for AFKO - " + Ex.Message); }
                                            //File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));

                                            #endregion
                                            break;

                                        // case "ZHR_WSR": // ----------Work Rules Timings----
                                        //     #region ZHR_WSR
                                        //     try
                                        //     {
                                        //         usp_xml_create_ZHRWSRTableAdapter ZHRWSR_Adap = new usp_xml_create_ZHRWSRTableAdapter();
                                        //         DataTable ZHRWSR = Dt.DefaultView.ToTable(false, "PERNR","BEGDA" ,"ENDDA","NOBEG", "NOEND");
                                        //         using (DataTable Dt_ZHRWSR = ZHRWSR_Adap.WSR_GetData(ZHRWSR, 1)) 
                                        //         {
                                        //             Sb.Append("<li class=\"Gr\">--------------------------------- ZHR_WSR ------------------- Holiday Table  : ZHR_WSR ------------------------ "
                                        //+ "[<span class=\"G\">INSERT</span>] - " + Dt_ZHRWSR.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                        //+ "[<span class=\"B\">UPDATE</span>] - " + Dt_ZHRWSR.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                        //+ "[<span class=\"R\">DELETE</span>] - " + Dt_ZHRWSR.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                        //             for (int i = 0; i < Dt_ZHRWSR.Rows.Count; i++)
                                        //             {
                                        //                 switch (Dt_ZHRWSR.Rows[i]["$action"].ToString())
                                        //                 {

                                        //                     case "INSERT":
                                        //                         Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                        //                             + " - <b>" + Dt_ZHRWSR.Rows[i]["$action"].ToString() + "</b> - " + Dt_ZHRWSR.Rows[i]["PERNR"].ToString() + " - " + Dt_ZHRWSR.Rows[i]["NOBEG"].ToString() + " - " + Dt_ZHRWSR.Rows[i]["NOEND"].ToString() + "</li>");
                                        //                         break;
                                        //                     case "UPDATE":
                                        //                         Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                        //                            + " - <b>" + Dt_ZHRWSR.Rows[i]["$action"].ToString() + "</b> - " + Dt_ZHRWSR.Rows[i]["PERNR"].ToString() + " - " + Dt_ZHRWSR.Rows[i]["NOBEG"].ToString() + " - " + Dt_ZHRWSR.Rows[i]["NOEND"].ToString() + "</li>");
                                        //                         break;
                                        //                     case "DELETE":
                                        //                         Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                        //                            + " - <b>" + Dt_ZHRWSR.Rows[i]["$action"].ToString() + "</b> - " + Dt_ZHRWSR.Rows[i]["PERNR1"].ToString() + " - " + Dt_ZHRWSR.Rows[i]["NOBEG1"].ToString() + " - " + Dt_ZHRWSR.Rows[i]["NOEND1"].ToString() + "</li>");
                                        //                         break;
                                        //                     default:
                                        //                         break;
                                        //                 }
                                        //             }
                                        //         }
                                        //     }
                                        //     catch (Exception Ex)
                                        //     { ObjLogError.LogError(sDrpdwnsLogPath, " Error occured in table ZHR_Holidays, when Inserting / Updating the record for ZHR_Holidays - " + Ex.Message); }
                                        //     File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));

                                        //     #endregion
                                        //     break;

                                        case "T500P": // -----------Personnel Areas information----
                                            #region T500P
                                            try
                                            {
                                                usp_xml_create_T500PTableAdapter T500P_Adap = new usp_xml_create_T500PTableAdapter();
                                                DataTable T500P = Dt.DefaultView.ToTable(false, "PERSA", "NAME1");
                                                using (DataTable Dt_T500P = T500P_Adap.T500P_GetData(T500P, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">--------------------------------- T500P -------------------Personnel Areas information  : T500P ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_T500P.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_T500P.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_T500P.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_T500P.Rows.Count; i++)
                                                    {
                                                        switch (Dt_T500P.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                                                    + " - <b>" + Dt_T500P.Rows[i]["$action"].ToString() + "</b> - " + Dt_T500P.Rows[i]["PERSA"].ToString() + " - " + Dt_T500P.Rows[i]["NAME1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T500P " + Dt_T500P.Rows[i]["$action"].ToString() + "</b> - " + Dt_T500P.Rows[i]["PERSA"].ToString() + " - " + Dt_T500P.Rows[i]["NAME1"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T500P.Rows[i]["$action"].ToString() + "</b> - " + Dt_T500P.Rows[i]["PERSA"].ToString() + " - " + Dt_T500P.Rows[i]["NAME1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T500P " + Dt_T500P.Rows[i]["$action"].ToString() + "</b> - " + Dt_T500P.Rows[i]["PERSA"].ToString() + " - " + Dt_T500P.Rows[i]["NAME1"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T500P.Rows[i]["$action"].ToString() + "</b> - " + Dt_T500P.Rows[i]["PERSA1"].ToString() + " - " + Dt_T500P.Rows[i]["NAME11"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T500P " + Dt_T500P.Rows[i]["$action"].ToString() + "</b> - " + Dt_T500P.Rows[i]["PERSA1"].ToString() + " - " + Dt_T500P.Rows[i]["NAME11"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            {
                                                //iEmpPowerScheduler.Program PR = new Program();
                                                //string Txt = PR.LogContent().Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters");
                                                ObjLogError.LogError(sDrpdwnsLogPath, " Error occured in table T500P, when Inserting / Updating the record  " + Ex.Message);
                                            }
                                            //File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));

                                            #endregion
                                            break;

                                        case "T527X": // -----------Organizational Units information----
                                            #region T527X
                                            try
                                            {
                                                usp_xml_create_T527XTableAdapter T527X_Adap = new usp_xml_create_T527XTableAdapter();
                                                DataTable T527X = Dt.DefaultView.ToTable(false, "ORGEH", "ORGTX");
                                                using (DataTable Dt_T527X = T527X_Adap.T527X_GetData(T527X, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">--------------------------------- T527X -------------------Organizational Units information  : T527X ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_T527X.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_T527X.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_T527X.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_T527X.Rows.Count; i++)
                                                    {
                                                        switch (Dt_T527X.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                                                    + " - <b>" + Dt_T527X.Rows[i]["$action"].ToString() + "</b> - " + Dt_T527X.Rows[i]["ORGEH"].ToString() + " - " + Dt_T527X.Rows[i]["ORGTX"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T527X " + Dt_T527X.Rows[i]["$action"].ToString() + "</b> - " + Dt_T527X.Rows[i]["ORGEH"].ToString() + " - " + Dt_T527X.Rows[i]["ORGTX"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T527X.Rows[i]["$action"].ToString() + "</b> - " + Dt_T527X.Rows[i]["ORGEH"].ToString() + " - " + Dt_T527X.Rows[i]["ORGTX"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T527X " + Dt_T527X.Rows[i]["$action"].ToString() + "</b> - " + Dt_T527X.Rows[i]["ORGEH"].ToString() + " - " + Dt_T527X.Rows[i]["ORGTX"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T527X.Rows[i]["$action"].ToString() + "</b> - " + Dt_T527X.Rows[i]["ORGEH1"].ToString() + " - " + Dt_T527X.Rows[i]["ORGTX1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sDrpdwnsLogPath, "T527X " + Dt_T527X.Rows[i]["$action"].ToString() + "</b> - " + Dt_T527X.Rows[i]["ORGEH1"].ToString() + " - " + Dt_T527X.Rows[i]["ORGTX1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            {
                                                //iEmpPowerScheduler.Program PR = new Program();
                                                //string Txt = PR.LogContent().Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters");
                                                ObjLogError.LogError(sDrpdwnsLogPath, " Error occured in table T527X, when Inserting / Updating the record  " + Ex.Message);
                                            }
                                            //File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));

                                            #endregion
                                            break;

                                        default:
                                            break;
                                        #endregion
                                    }
                                }
                            }
                            MsgCls("Masters Dropdown data imported successfully. <b>[</b> Please check User log for details ! <b>]</b>", LblMsg, Color.Green);
                        }
                    }
                    else
                    { MsgCls("Please select Master XML file !", LblMsg, Color.Red); }
                }
            }
            catch (Exception Ex)
            {
                MsgCls(Ex.Message, LblMsg, Color.Red);
                ObjLogError.LogError(sDrpdwnsLogPath, Ex.Message);
            }
        }
        #endregion
        //================================== IMPORT MASTERS ==================== START ==============
        //================================== ORG MASTERS ==================== START ==============
        #region Org Masters
        protected void BtnImportOrgMasters_Click(object sender, EventArgs e)
        {
            try
            {
                QueriesTableAdapter Adap = new QueriesTableAdapter();

                if (FU_ImportOrganizationMasters.PostedFile != null && !string.IsNullOrEmpty(FU_ImportOrganizationMasters.PostedFile.FileName))
                {
                    string XmlExt = Path.GetExtension(FU_ImportOrganizationMasters.FileName.ToString().ToUpper());
                    string OrgXMLPath = Path.Combine(OrgDtlfolder, FU_ImportOrganizationMasters.FileName);
                    StringBuilder Sb = new StringBuilder();
                    Sb.Clear();

                    if (XmlExt == ".xml" | XmlExt == ".XML")
                    {
                        if (!Directory.Exists(OrgDtlfolder))
                        { Directory.CreateDirectory(OrgDtlfolder); }
                        //-------------------------------------------------
                        if (!File.Exists(sOrgDtlsLogPath))
                        { File.Create(sOrgDtlsLogPath).Close(); }
                        //-------- SAVE TO LOCAL DRIVE --------------------
                        DirectoryInfo DirInfo = new DirectoryInfo(OrgDtlfolder);
                        //FileInfo[] AdminUserProfileImageFile = DirInfo.GetFiles("*" + FU_ImportOrganizationMasters.FileName + "*.*");
                        FileInfo[] OrgXMLFile = DirInfo.GetFiles(FU_ImportOrganizationMasters.FileName);
                        foreach (FileInfo Fl in OrgXMLFile)
                        { Fl.Delete(); }
                        FU_ImportOrganizationMasters.SaveAs(OrgXMLPath);

                        using (DataSet DsUser = new DataSet())
                        {
                            DsUser.ReadXml(OrgXMLPath);

                            foreach (DataTable Dt in DsUser.Tables)
                            {
                                if (!Dt.TableName.StartsWith("TAB") && !Dt.TableName.StartsWith("tab") && !Dt.TableName.StartsWith("abap") && !Dt.TableName.StartsWith("values") && !Dt.TableName.StartsWith("NewDataSet"))
                                {
                                    switch (Dt.TableName.ToUpper())
                                    {
                                        #region Master Upload
                                        case "HRP1000": // -----------HRP 1000 ------
                                            #region HRP1000
                                            try
                                            {
                                                usp_xml_create_hrp1000TableAdapter HRP1000_Adap = new usp_xml_create_hrp1000TableAdapter();
                                                DataTable HRP1000 = Dt.DefaultView.ToTable(true, "OBJID", "OTYPE", "PLVAR", "SHORT", "STEXT");
                                                using (DataTable Dt_HRP1000 = HRP1000_Adap.HRP1000_GetData(HRP1000, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">---" + "------------------------------ HRP1000 ------------------- HR Personal Info 1000: HRP1000 ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_HRP1000.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_HRP1000.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_HRP1000.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_HRP1000.Rows.Count; i++)
                                                    {
                                                        switch (Dt_HRP1000.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>" + "<b>" + Dt_HRP1000.Rows[i]["$action"].ToString() + "</b> - " + Dt_HRP1000.Rows[i]["OBJID"].ToString() + " - " + Dt_HRP1000.Rows[i]["OTYPE"].ToString() + " - " + Dt_HRP1000.Rows[i]["SHORT"].ToString() + "</li>");
                                                                ObjLogError.LogError(sOrgDtlsLogPath, "HRP1000 " + Dt_HRP1000.Rows[i]["$action"].ToString() + "</b> - " + Dt_HRP1000.Rows[i]["OBJID"].ToString() + " - " + Dt_HRP1000.Rows[i]["OTYPE"].ToString() + " - " + Dt_HRP1000.Rows[i]["SHORT"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>" + "<b>" + Dt_HRP1000.Rows[i]["$action"].ToString() + "</b> - " + Dt_HRP1000.Rows[i]["OBJID"].ToString() + " - " + Dt_HRP1000.Rows[i]["OTYPE"].ToString() + " - " + Dt_HRP1000.Rows[i]["SHORT"].ToString() + "</li>");
                                                                ObjLogError.LogError(sOrgDtlsLogPath, "HRP1000 " + Dt_HRP1000.Rows[i]["$action"].ToString() + "</b> - " + Dt_HRP1000.Rows[i]["OBJID"].ToString() + " - " + Dt_HRP1000.Rows[i]["OTYPE"].ToString() + " - " + Dt_HRP1000.Rows[i]["SHORT"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>" + "<b>" + Dt_HRP1000.Rows[i]["$action"].ToString() + "</b> - " + Dt_HRP1000.Rows[i]["OBJID1"].ToString() + " - " + Dt_HRP1000.Rows[i]["OTYPE1"].ToString() + " - " + Dt_HRP1000.Rows[i]["SHORT1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sOrgDtlsLogPath, "HRP1000 " + Dt_HRP1000.Rows[i]["$action"].ToString() + "</b> - " + Dt_HRP1000.Rows[i]["OBJID1"].ToString() + " - " + Dt_HRP1000.Rows[i]["OTYPE1"].ToString() + " - " + Dt_HRP1000.Rows[i]["SHORT1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            {
                                                ObjLogError.LogError(sOrgDtlsLogPath, " Error occured in table HRP1000, when Inserting / Updating the record for HRP1000 - " + Ex.Message);
                                                Sb.Append("<li class=\"\"><label> Error occured in table HRP1000, when Inserting / Updating the record for HRP1000 - " + Ex.Message + "</li>");
                                            }
                                            #endregion
                                            break;
                                        case "HRP1001": // -----------HRP 1001 ------
                                            #region HRP1001
                                            try
                                            {
                                                usp_xml_create_hrp1001TableAdapter HRP1001_Adap = new usp_xml_create_hrp1001TableAdapter();
                                                DataTable HRP1001 = Dt.DefaultView.ToTable(false, "OTYPE", "OBJID", "PLVAR", "RSIGN", "RELAT", "SCLAS", "SOBID");
                                                using (DataTable Dt_HRP1001 = HRP1001_Adap.HRP1001_GetData(HRP1001, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">---" + "------------------------------ HRP1001 ------------------- HR Personal Info 1001: HRP1001 ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_HRP1001.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_HRP1001.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_HRP1001.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_HRP1001.Rows.Count; i++)
                                                    {
                                                        switch (Dt_HRP1001.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label><b>" + Dt_HRP1001.Rows[i]["$action"].ToString() + "</b> - " + Dt_HRP1001.Rows[i]["OBJID"].ToString() + " - " + Dt_HRP1001.Rows[i]["RELAT"].ToString() + " - " + Dt_HRP1001.Rows[i]["SOBID"].ToString() + "</li>");
                                                                ObjLogError.LogError(sOrgDtlsLogPath, "HRP1001 " + Dt_HRP1001.Rows[i]["$action"].ToString() + "</b> - " + Dt_HRP1001.Rows[i]["OBJID"].ToString() + " - " + Dt_HRP1001.Rows[i]["RELAT"].ToString() + " - " + Dt_HRP1001.Rows[i]["SOBID"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label><b>" + Dt_HRP1001.Rows[i]["$action"].ToString() + "</b> - " + Dt_HRP1001.Rows[i]["OBJID"].ToString() + " - " + Dt_HRP1001.Rows[i]["RELAT"].ToString() + " - " + Dt_HRP1001.Rows[i]["SOBID"].ToString() + "</li>");
                                                                ObjLogError.LogError(sOrgDtlsLogPath, "HRP1001 " + Dt_HRP1001.Rows[i]["$action"].ToString() + "</b> - " + Dt_HRP1001.Rows[i]["OBJID"].ToString() + " - " + Dt_HRP1001.Rows[i]["RELAT"].ToString() + " - " + Dt_HRP1001.Rows[i]["SOBID"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label><b>" + Dt_HRP1001.Rows[i]["$action"].ToString() + "</b> - " + Dt_HRP1001.Rows[i]["OBJID1"].ToString() + " - " + Dt_HRP1001.Rows[i]["RELAT1"].ToString() + " - " + Dt_HRP1001.Rows[i]["SOBID1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sOrgDtlsLogPath, "HRP1001 " + Dt_HRP1001.Rows[i]["$action"].ToString() + "</b> - " + Dt_HRP1001.Rows[i]["OBJID1"].ToString() + " - " + Dt_HRP1001.Rows[i]["RELAT1"].ToString() + " - " + Dt_HRP1001.Rows[i]["SOBID1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            {
                                                ObjLogError.LogError(sOrgDtlsLogPath, " Error occured in table HRP1001, when Inserting / Updating the record for HRP1001 - " + Ex.Message);
                                                Sb.Append("<li class=\"\"><label> Error occured in table HRP1001, when Inserting / Updating the record for HRP1001 - " + Ex.Message + "</li>");
                                            }

                                            #endregion
                                            break;
                                        case "PA0001": // ----------- PA0001 ------
                                            #region PA0001
                                            try
                                            {
                                                usp_xml_create_PA0001TableAdapter PA0001_Adap = new usp_xml_create_PA0001TableAdapter();
                                                DataTable PA0001 = Dt.DefaultView.ToTable(false, "PERNR", "ORGEH", "PLANS", "ENAME", "WERKS", "SACHA", "SACHP", "SACHZ", "KOSTL", "BTRTL", "PERSG", "PERSK", "BUKRS");
                                                using (DataTable Dt_PA0001 = PA0001_Adap.PA0001_GetData(PA0001, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">---" + "------------------------------ PA0001 ------------------- Personal Info PA0001: PA0001 ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_PA0001.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_PA0001.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_PA0001.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_PA0001.Rows.Count; i++)
                                                    {
                                                        switch (Dt_PA0001.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label> <b>" + Dt_PA0001.Rows[i]["$action"].ToString() + "</b> - " + Dt_PA0001.Rows[i]["PERNR"].ToString() + "</li>");
                                                                ObjLogError.LogError(sOrgDtlsLogPath, "PA0001 " + Dt_PA0001.Rows[i]["$action"].ToString() + "</b> - " + Dt_PA0001.Rows[i]["PERNR"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label> <b>" + Dt_PA0001.Rows[i]["$action"].ToString() + "</b> - " + Dt_PA0001.Rows[i]["PERNR"].ToString() + "</li>");
                                                                ObjLogError.LogError(sOrgDtlsLogPath, "PA0001 " + Dt_PA0001.Rows[i]["$action"].ToString() + "</b> - " + Dt_PA0001.Rows[i]["PERNR"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label> <b>" + Dt_PA0001.Rows[i]["$action"].ToString() + "</b> - " + Dt_PA0001.Rows[i]["PERNR1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sOrgDtlsLogPath, "PA0001 " + Dt_PA0001.Rows[i]["$action"].ToString() + "</b> - " + Dt_PA0001.Rows[i]["PERNR1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            {
                                                ObjLogError.LogError(sOrgDtlsLogPath, " Error occured in table PA0001, when Inserting / Updating the record for PA0001 - " + Ex.Message);
                                                Sb.Append("<li class=\"\"><label> Error occured in table PA0001, when Inserting / Updating the record for PA0001 - " + Ex.Message + "</li>");
                                            }


                                            #endregion
                                            break;
                                        case "PA0007": // ----------- PA0007 ------
                                            #region PA0007
                                            try
                                            {
                                                usp_xml_create_PA0007TableAdapter PA0007_Adap = new usp_xml_create_PA0007TableAdapter();
                                                DataTable PA0007 = Dt.DefaultView.ToTable(false, "PERNR", "ARBST");
                                                using (DataTable Dt_PA0007 = PA0007_Adap.PA0007_GetData(PA0007, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">---" + "------------------------------ PA0007 ------------------- Personal Info PA0007: PA0007 ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_PA0007.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_PA0007.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_PA0007.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_PA0007.Rows.Count; i++)
                                                    {
                                                        switch (Dt_PA0007.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label> <b>" + Dt_PA0007.Rows[i]["$action"].ToString() + "</b> - " + Dt_PA0007.Rows[i]["PERNR"].ToString() + " - " + Dt_PA0007.Rows[i]["ARBST"].ToString() + "</li>");
                                                                ObjLogError.LogError(sOrgDtlsLogPath, "PA0007 " + Dt_PA0007.Rows[i]["$action"].ToString() + "</b> - " + Dt_PA0007.Rows[i]["PERNR"].ToString() + " - " + Dt_PA0007.Rows[i]["ARBST"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label> <b>" + Dt_PA0007.Rows[i]["$action"].ToString() + "</b> - " + Dt_PA0007.Rows[i]["PERNR"].ToString() + " - " + Dt_PA0007.Rows[i]["ARBST"].ToString() + "</li>");
                                                                ObjLogError.LogError(sOrgDtlsLogPath, "PA0007 " + Dt_PA0007.Rows[i]["$action"].ToString() + "</b> - " + Dt_PA0007.Rows[i]["PERNR"].ToString() + " - " + Dt_PA0007.Rows[i]["ARBST"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label> <b>" + Dt_PA0007.Rows[i]["$action"].ToString() + "</b> - " + Dt_PA0007.Rows[i]["PERNR1"].ToString() + " - " + Dt_PA0007.Rows[i]["ARBST1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sOrgDtlsLogPath, "PA0007 " + Dt_PA0007.Rows[i]["$action"].ToString() + "</b> - " + Dt_PA0007.Rows[i]["PERNR1"].ToString() + " - " + Dt_PA0007.Rows[i]["ARBST1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            {
                                                ObjLogError.LogError(sOrgDtlsLogPath, " Error occured in table PA0007, when Inserting / Updating the record for PA0007 - " + Ex.Message);
                                                Sb.Append("<li class=\"\"><label> Error occured in table PA0007, when Inserting / Updating the record for PA0007 - " + Ex.Message + "</li>");
                                            }


                                            #endregion
                                            break;
                                        case "PA0041": // ----------- PA0041 ------
                                            #region PA0041
                                            try
                                            {
                                                usp_xml_create_PA0041TableAdapter PA0041_Adap = new usp_xml_create_PA0041TableAdapter();
                                                DataTable PA0041 = Dt.DefaultView.ToTable(false, "PERNR", "DAT01");
                                                using (DataTable Dt_PA0041 = PA0041_Adap.PA0041_GetData(PA0041, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">---" + "------------------------------ PA0041 ------------------- Info type PA0041: PA0041 ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_PA0041.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_PA0041.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_PA0041.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_PA0041.Rows.Count; i++)
                                                    {
                                                        switch (Dt_PA0041.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label> <b>" + Dt_PA0041.Rows[i]["$action"].ToString() + "</b> - " + Dt_PA0041.Rows[i]["PERNR"].ToString() + " - " + Dt_PA0041.Rows[i]["DAT01"].ToString() + "</li>");
                                                                ObjLogError.LogError(sOrgDtlsLogPath, "PA0041 " + Dt_PA0041.Rows[i]["$action"].ToString() + "</b> - " + Dt_PA0041.Rows[i]["PERNR"].ToString() + " - " + Dt_PA0041.Rows[i]["DAT01"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label> <b>" + Dt_PA0041.Rows[i]["$action"].ToString() + "</b> - " + Dt_PA0041.Rows[i]["PERNR"].ToString() + " - " + Dt_PA0041.Rows[i]["DAT01"].ToString() + "</li>");
                                                                ObjLogError.LogError(sOrgDtlsLogPath, "PA0041 " + Dt_PA0041.Rows[i]["$action"].ToString() + "</b> - " + Dt_PA0041.Rows[i]["PERNR"].ToString() + " - " + Dt_PA0041.Rows[i]["DAT01"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label> <b>" + Dt_PA0041.Rows[i]["$action"].ToString() + "</b> - " + Dt_PA0041.Rows[i]["PERNR1"].ToString() + " - " + Dt_PA0041.Rows[i]["DAT011"].ToString() + "</li>");
                                                                ObjLogError.LogError(sOrgDtlsLogPath, "PA0041 " + Dt_PA0041.Rows[i]["$action"].ToString() + "</b> - " + Dt_PA0041.Rows[i]["PERNR1"].ToString() + " - " + Dt_PA0041.Rows[i]["DAT011"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            {
                                                ObjLogError.LogError(sOrgDtlsLogPath, " Error occured in table PA0041, when Inserting / Updating the record for PA0041 - " + Ex.Message);
                                                Sb.Append("<li class=\"\"><label> Error occured in table PA0041, when Inserting / Updating the record for PA0041 - " + Ex.Message + "</li>");
                                            }


                                            #endregion
                                            break;
                                        case "T528T": // ----------- T528T ------
                                            #region T528T
                                            try
                                            {
                                                usp_xml_create_T528TTableAdapter T528T_Adap = new usp_xml_create_T528TTableAdapter();
                                                DataTable T528T = Dt.DefaultView.ToTable(false, "OTYPE", "PLANS", "PLSTX");
                                                using (DataTable Dt_T528T = T528T_Adap.T528T_GetData(T528T, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">---" + "------------------------------ T528T ------------------- Info type T528T: T528T ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_T528T.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_T528T.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_T528T.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_T528T.Rows.Count; i++)
                                                    {
                                                        switch (Dt_T528T.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label> <b>" + Dt_T528T.Rows[i]["$action"].ToString()
                                                                    + "</b> - " + Dt_T528T.Rows[i]["OTYPE"].ToString() + " - " + Dt_T528T.Rows[i]["PLANS"].ToString() + " - " + Dt_T528T.Rows[i]["PLSXT"].ToString() + "</li>");
                                                                ObjLogError.LogError(sOrgDtlsLogPath, "T528T " + Dt_T528T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T528T.Rows[i]["OTYPE"].ToString() + " - " + Dt_T528T.Rows[i]["PLANS"].ToString() + " - " + Dt_T528T.Rows[i]["PLSXT"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label> <b>" + Dt_T528T.Rows[i]["$action"].ToString()
                                                                    + "</b> - " + Dt_T528T.Rows[i]["OTYPE"].ToString() + " - " + Dt_T528T.Rows[i]["PLANS"].ToString() + " - " + Dt_T528T.Rows[i]["PLSXT"].ToString() + "</li>");
                                                                ObjLogError.LogError(sOrgDtlsLogPath, "T528T " + Dt_T528T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T528T.Rows[i]["OTYPE"].ToString() + " - " + Dt_T528T.Rows[i]["PLANS"].ToString() + " - " + Dt_T528T.Rows[i]["PLSXT"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label> <b>" + Dt_T528T.Rows[i]["$action"].ToString()
                                                                    + "</b> - " + Dt_T528T.Rows[i]["OTYPE1"].ToString() + " - " + Dt_T528T.Rows[i]["PLANS1"].ToString() + " - " + Dt_T528T.Rows[i]["PLSXT1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sOrgDtlsLogPath, "T528T " + Dt_T528T.Rows[i]["$action"].ToString() + "</b> - " + Dt_T528T.Rows[i]["OTYPE1"].ToString() + " - " + Dt_T528T.Rows[i]["PLANS1"].ToString() + " - " + Dt_T528T.Rows[i]["PLSXT1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            {
                                                ObjLogError.LogError(sOrgDtlsLogPath, " Error occured in table HRP1001, when Inserting / Updating the record for HRP1001 - " + Ex.Message);
                                                Sb.Append("<li class=\"\"><label> Error occured in table T528T, when Inserting / Updating the record for T528T - " + Ex.Message + "</li>");
                                            }


                                            #endregion
                                            break;
                                        case "_-ITCHAMPS_-QUOTA": // ZQUOTA changed to _-ITCHAMPS_-QUOTA ----------- ZQUOTA ------
                                            #region ZQUOTA
                                            try
                                            {
                                                usp_xml_create_ZQUOTATableAdapter ZQUOTA_Adap = new usp_xml_create_ZQUOTATableAdapter();
                                                DataTable ZQUOTA = Dt.DefaultView.ToTable(false, "PERNR", "AWART", "KTART1", "KTART2", "KTART3");
                                                using (DataTable Dt_ZQUOTA = ZQUOTA_Adap.ZQUOTA_GetData(ZQUOTA, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">---" + "------------------------------ ZQUOTA ------------------- Info type ZQUOTA: ZQUOTA ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_ZQUOTA.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_ZQUOTA.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_ZQUOTA.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_ZQUOTA.Rows.Count; i++)
                                                    {
                                                        switch (Dt_ZQUOTA.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label> <b>" + Dt_ZQUOTA.Rows[i]["$action"].ToString()
                                                                    + "</b> - " + Dt_ZQUOTA.Rows[i]["PERNR"].ToString() + " - " + Dt_ZQUOTA.Rows[i]["AWART"].ToString() + " - " + Dt_ZQUOTA.Rows[i]["KTART1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sOrgDtlsLogPath, "ZQUOTA " + Dt_ZQUOTA.Rows[i]["$action"].ToString() + "</b> - " + Dt_ZQUOTA.Rows[i]["PERNR"].ToString() + " - " + Dt_ZQUOTA.Rows[i]["AWART"].ToString() + " - " + Dt_ZQUOTA.Rows[i]["KTART1"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label> <b>" + Dt_ZQUOTA.Rows[i]["$action"].ToString()
                                                                    + "</b> - " + Dt_ZQUOTA.Rows[i]["PERNR"].ToString() + " - " + Dt_ZQUOTA.Rows[i]["AWART"].ToString() + " - " + Dt_ZQUOTA.Rows[i]["KTART1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sOrgDtlsLogPath, "ZQUOTA " + Dt_ZQUOTA.Rows[i]["$action"].ToString() + "</b> - " + Dt_ZQUOTA.Rows[i]["PERNR"].ToString() + " - " + Dt_ZQUOTA.Rows[i]["AWART"].ToString() + " - " + Dt_ZQUOTA.Rows[i]["KTART1"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label> <b>" + Dt_ZQUOTA.Rows[i]["$action"].ToString()
                                                                    + "</b> - " + Dt_ZQUOTA.Rows[i]["PERNR1"].ToString() + " - " + Dt_ZQUOTA.Rows[i]["AWART1"].ToString() + " - " + Dt_ZQUOTA.Rows[i]["KTART11"].ToString() + "</li>");
                                                                ObjLogError.LogError(sOrgDtlsLogPath, "ZQUOTA " + Dt_ZQUOTA.Rows[i]["$action"].ToString() + "</b> - " + Dt_ZQUOTA.Rows[i]["PERNR1"].ToString() + " - " + Dt_ZQUOTA.Rows[i]["AWART1"].ToString() + " - " + Dt_ZQUOTA.Rows[i]["KTART11"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            {
                                                ObjLogError.LogError(sOrgDtlsLogPath, " Error occured in table HRP1001, when Inserting / Updating the record for HRP1001 - " + Ex.Message);
                                                Sb.Append("<li class=\"\"><label> Error occured in table ZQUOTA, when Inserting / Updating the record for ZQUOTA - " + Ex.Message + "</li>");
                                            }


                                            #endregion
                                            break;
                                        // case "PA0008": // ----------- PA0008 ------
                                        //     #region PA0008
                                        //     try
                                        //     {
                                        //         usp_xml_create_PA0008TableAdapter PA0008_Adap = new usp_xml_create_PA0008TableAdapter();
                                        //         DataTable DtPA0008 = Dt.DefaultView.ToTable(false, "PERNR", "SUMBB");
                                        //         using (DataTable Dt_PA0008 = PA0008_Adap.PA0008_GetData(DtPA0008, 1))
                                        //         {
                                        //             Sb.Append("<li class=\"Gr\">---" + "------------------------------ PA0008 ------------------- Info type PA0008: PA0008 ------------------------ "
                                        //+ "[<span class=\"G\">INSERT</span>] - " + Dt_PA0008.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                        //+ "[<span class=\"B\">UPDATE</span>] - " + Dt_PA0008.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                        //+ "[<span class=\"R\">DELETE</span>] - " + Dt_PA0008.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                        //             for (int i = 0; i < Dt_PA0008.Rows.Count; i++)
                                        //             {
                                        //                 switch (Dt_PA0008.Rows[i]["$action"].ToString())
                                        //                 {
                                        //                     case "INSERT":
                                        //                         Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label> <b>" + Dt_PA0008.Rows[i]["$action"].ToString() + "</b> - " + Dt_PA0008.Rows[i]["PERNR"].ToString() + " - " + Dt_PA0008.Rows[i]["SUMBB"].ToString() + "</li>");
                                        //                         break;
                                        //                     case "UPDATE":
                                        //                         Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label> <b>" + Dt_PA0008.Rows[i]["$action"].ToString() + "</b> - " + Dt_PA0008.Rows[i]["PERNR"].ToString() + " - " + Dt_PA0008.Rows[i]["SUMBB"].ToString() + "</li>");
                                        //                         break;
                                        //                     case "DELETE":
                                        //                         Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label> <b>" + Dt_PA0008.Rows[i]["$action"].ToString() + "</b> - " + Dt_PA0008.Rows[i]["PERNR1"].ToString() + " - " + Dt_PA0008.Rows[i]["SUMBB1"].ToString() + "</li>");
                                        //                         break;
                                        //                     default:
                                        //                         break;
                                        //                 }
                                        //             }
                                        //         }
                                        //     }
                                        //     catch (Exception Ex)
                                        //     { Sb.Append("<li class=\"\"><label> Error occured in table PA0008, when Inserting / Updating the record for PA0008 - " + Ex.Message + "</li>"); }


                                        //     #endregion
                                        //     break;

                                        case "T526": // ----------- Administrators Pooled table----
                                            #region T526
                                            try
                                            {
                                                usp_xml_create_T526TableAdapter T526_Adap = new usp_xml_create_T526TableAdapter();
                                                DataTable T526 = Dt.DefaultView.ToTable(false, "WERKS", "SACHX", "SACHN", "USRID");
                                                using (DataTable Dt_T526 = T526_Adap.T526_GetData(T526, 1))
                                                {
                                                    Sb.Append("<li class=\"Gr\">--------------------------------- T526 -------------------  Administrators Pooled table  : T526 ------------------------ "
                                       + "[<span class=\"G\">INSERT</span>] - " + Dt_T526.AsEnumerable().Count(s => s.Field<string>("$action") == "INSERT").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"B\">UPDATE</span>] - " + Dt_T526.AsEnumerable().Count(s => s.Field<string>("$action") == "UPDATE").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                       + "[<span class=\"R\">DELETE</span>] - " + Dt_T526.AsEnumerable().Count(s => s.Field<string>("$action") == "DELETE").ToString() + "----------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff tt") + " ------- </li>");
                                                    for (int i = 0; i < Dt_T526.Rows.Count; i++)
                                                    {
                                                        switch (Dt_T526.Rows[i]["$action"].ToString())
                                                        {

                                                            case "INSERT":
                                                                Sb.Append("<li class=\"G\"><label>" + (i + 1).ToString() + ".</label>"
                                                                    + " - <b>" + Dt_T526.Rows[i]["$action"].ToString() + "</b> - " + Dt_T526.Rows[i]["WERKS"].ToString() + " - " + Dt_T526.Rows[i]["SACHX"].ToString() + "</li>");
                                                                ObjLogError.LogError(sOrgDtlsLogPath, "T526 " + Dt_T526.Rows[i]["$action"].ToString() + "</b> - " + Dt_T526.Rows[i]["WERKS"].ToString() + " - " + Dt_T526.Rows[i]["SACHX"].ToString() + " successfully.");
                                                                break;
                                                            case "UPDATE":
                                                                Sb.Append("<li class=\"B\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T526.Rows[i]["$action"].ToString() + "</b> - " + Dt_T526.Rows[i]["WERKS"].ToString() + " - " + Dt_T526.Rows[i]["SACHX"].ToString() + "</li>");
                                                                ObjLogError.LogError(sOrgDtlsLogPath, "T526 " + Dt_T526.Rows[i]["$action"].ToString() + "</b> - " + Dt_T526.Rows[i]["WERKS"].ToString() + " - " + Dt_T526.Rows[i]["SACHX"].ToString() + " successfully.");
                                                                break;
                                                            case "DELETE":
                                                                Sb.Append("<li class=\"R\"><label>" + (i + 1).ToString() + ".</label>"
                                                                   + " - <b>" + Dt_T526.Rows[i]["$action"].ToString() + "</b> - " + Dt_T526.Rows[i]["WERKS1"].ToString() + " - " + Dt_T526.Rows[i]["SACHX1"].ToString() + "</li>");
                                                                ObjLogError.LogError(sOrgDtlsLogPath, "T526 " + Dt_T526.Rows[i]["$action"].ToString() + "</b> - " + Dt_T526.Rows[i]["WERKS1"].ToString() + " - " + Dt_T526.Rows[i]["SACHX1"].ToString() + " successfully.");
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            { ObjLogError.LogError(sOrgDtlsLogPath, " Error occured in table T526, when Inserting / Updating the record for T526 - " + Ex.Message); }
                                            // File.WriteAllText(sDrpdwnsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "DropDown Masters"));

                                            #endregion
                                            break;
                                        default:
                                            break;
                                        #endregion
                                    }
                                }
                            }
                            //File.WriteAllText(sOrgDtlsLogPath, LogContent.Replace("##CONTENT##", Sb.ToString()).Replace("##TITLE##", "Organization Masters"));
                            MsgCls("Masters data imported successfully. <b>[</b> Please check User log for details ! <b>]</b>", LblMsg, Color.Green);
                        }
                    }
                    else
                    { MsgCls("Please select Master XML file !", LblMsg, Color.Red); }
                }
            }
            catch (Exception Ex)
            {
                MsgCls(Ex.Message, LblMsg, Color.Red);
                ObjLogError.LogError(sOrgDtlsLogPath, Ex.Message);
            }
        }
        #endregion
        //================================== ORG MASTERS ==================== END ==============
        //----------------- EMAIL SENDING ---------------- START ---------------------
        #region Get Mail Body
        private string GetMailBody(string EmpPernr, string Password, string EmailID)
        {
            try
            {
                string Mailbody = string.Empty;
                string AddressInfoFilePath = Server.MapPath(@"~/EmailTemplates/CreateUser.html");

                Mailbody = System.IO.File.ReadAllText(AddressInfoFilePath);
                Mailbody = Mailbody.Replace("##EMPPERNR##", EmpPernr);
                Mailbody = Mailbody.Replace("##PASSWORD##", Password);
                Mailbody = Mailbody.Replace("##EMAILID##", EmailID);

                return Mailbody;
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); return string.Empty; }
        }

        #endregion
        //----------------- EMAIL SENDING ---------------- END -----------------------
        #region Log file content Declaration
        string LogContent = "<!DOCTYPE html><html xmlns=\"http://www.w3.org/1999/xhtml\"><head><title>IEmpPower Masters</title><style type=\"text/css\">html, body {margin:0; padding:0;}"
            + "label {padding-right:15px;color:#3e3e3e;} div {margin: 0;padding: 0;font: normal normal normal 12px/20px \"Segoe UI\", Verdana, Arial, Helvetica, sans-serif;color: #3e3e3e;}"
            + "ul {list-style: none;margin: 0;padding: 0;}.R {color: red;}.G {color: green;} .B{color:#0000ff;} .Gr{color:#4A4A4A;}</style></head><body><div style=\"width: 97%; margin: 0 auto; padding: 20px 10px;\">"
            + "<div style=\"font-size: 14px; padding: 10px 0; border-bottom: 1px solid #0026ff; margin: 0 0 20px 0;\"><b>##TITLE##</b> </div><ul>##CONTENT##</ul></div></body></html>";
        #endregion



    }
}