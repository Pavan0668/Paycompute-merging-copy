using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using iEmpPower.Old_App_Code.iEmpPowerMaster;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;
using System.IO;
using System.Text;
/// <summary>
/// Summary description for masterbl
/// </summary>
/// 
namespace iEmpPowerMaster_Load
{

    public class masterbl
    {
        public masterbl()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public mastercollectionbo Load_Tile_details(string PERNR,string ccode, int flg, bool Favrt)
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo requisitionboList = new mastercollectionbo();
            foreach (var vRow in objDataContext.usp_load_dashboard_Tile_details(PERNR,ccode,flg, Favrt))
            {
                masterbo requisitionboObj = new masterbo();
                requisitionboObj.T_icon = vRow.U_Tile_icon;
                requisitionboObj.T_Path = vRow.U_Tile_Path;
                requisitionboObj.T_Name = vRow.U_Tile_NAME;
                requisitionboObj.PERNR = vRow.PERNR;
            }
            return requisitionboList;
        }

        //Method to load Project Manager for Project Code
        public static mastercollectionbo Load_PrMngr_forPrjCode(masterbo mBo)
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            string posid = mBo.LL.ToString();
            foreach (var vRow in objDataContext.sp_master_load_PrjMangr_forPrjCode(posid))
            {
                masterbo objBo = new masterbo();
                objBo.VERNR = vRow.VERNR;
                objBo.VERNA = vRow.VERNA;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }
        //Method to particular load Unit of Measurements
        public static mastercollectionbo Load_Unit_of_Measurements(masterbo mBo)
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            string ItemName_id = mBo.LL;
            foreach (var vRow in objDataContext.sp_master_load_UnitofMeasurement(ItemName_id))
            {
                masterbo objBo = new masterbo();
                objBo.MATNR = vRow.MATNR;
                objBo.MEINS = vRow.MEINS;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        //Method to load Unit of Measurements
        public static mastercollectionbo Load_Unit_of_Measurements()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();

            foreach (var vRow in objDataContext.sp_master_load_AllUnitofMeasurement())
            {
                masterbo objBo = new masterbo();
                objBo.ISOCODE = vRow.ISOCODE;
                objBo.ISOTXT = vRow.ISOTXT;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }
        //Method to load Requestor Region

        public static mastercollectionbo Load_Requestor_Region()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_Requestor_Region())
            {
                masterbo objBo = new masterbo();
                objBo.EKGRP = vRow.EKGRP;
                objBo.EKNAM = vRow.EKNAM;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }
        //Method to load region

        public static mastercollectionbo Load_Region()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_Region())
            {
                masterbo objBo = new masterbo();
                objBo.REGION_ID = vRow.REGION_ID.Trim();
                objBo.REGION_TEXT = vRow.REGION_TEXT;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static mastercollectionbo Load_Region(string Country)
        {
            masterdalDataContext objDataContext;
            objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_get_region_name_s(Country))
            {
                masterbo objBo = new masterbo();
                objBo.TEXT25 = vRow.TEXT25;
                objBo.RGION = vRow.RGION;


                objList.Add(objBo);
            }
            return objList;
        }

        public static mastercollectionbo Load_SubFunction(string Perner)
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_SubFunction(Perner))
            {
                masterbo objBo = new masterbo();
                objBo.BTEXT = vRow.BTEXT;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public void update_favt(string PERNR, int flg, bool favt, string txt, bool? vald)
        {
            try
            {
                masterdalDataContext objExpenseDataContext = new masterdalDataContext();
                objExpenseDataContext.usp_load_dashboard_Tile_update_details(PERNR, flg, favt, txt, ref vald);
                objExpenseDataContext.Dispose();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }


        public static void PasswordReset_SentMail(string PERNR, string sEmailId, string sPassowd, string by)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                MailMessage message = new MailMessage();
                configurationbl objBl = new configurationbl();
                configurationcollectionbo objLst = objBl.Load_Mail_Server_Details();
                configurationbo objSeverDetailsBo = objLst.Find(delegate(configurationbo obj)
                {
                    return true;
                });
                NetworkCredential basicCredential = new NetworkCredential(objSeverDetailsBo.USER_NAME, objSeverDetailsBo.PASSWORD);
                MailAddress fromAddress = new MailAddress(objSeverDetailsBo.EMAIL_ID);

                // You can specify the host name or ipaddress of your server
                // Default in IIS will be localhost 
                smtpClient.Host = objSeverDetailsBo.SMTP_SERVER;


                //Default port will be 25
                smtpClient.Port = Convert.ToInt16(objSeverDetailsBo.PORT);

                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = basicCredential;

                //From address will be given as a MailAddress Object
                message.From = fromAddress;
                // To address collection of MailAddress
                message.To.Add(sEmailId);
                message.Subject = PERNR + " - Password reset by " + by + ".";

                // Message body content
                smtpClient.EnableSsl = true;
                // message.IsBodyHtml = true;
                // Send SMTP mail
                message.Body = "Your iEmpPower new password is <b>" + sPassowd + "</b><br/><br/>Please change the Password Once logged-in.<br/><br/>Best regards, <br/><br/>iEmpPower Admin<br/><br/>";
                message.Body += "<a runat='server' id='elogin' href='http://iemppower.itchamps.com/' target='_blank'>Click here to Login to IEmpPower</a><br/><br/>";

                //message.EnableSsl = true;

                smtpClient.Send(message);

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public static void DispatchMailWithAttachment(string strRecipients, string strPernr, string strModule, string strPernrMailId, string msg, string filename)
        {
            try
            {
                //strRecipients = "monica.ks@itchamps.com";
                //strPernrMailId = "latha.mg@itchamps.com";
                //SmtpClient smtpClient = new SmtpClient();
                MailMessage message = new MailMessage();
                configurationbl objBl = new configurationbl();
                configurationcollectionbo objLst = objBl.Load_Mail_Server_Details();
                configurationbo objSeverDetailsBo = objLst.Find(delegate(configurationbo obj)
                {
                    return true;
                });

                if (objSeverDetailsBo == null)
                {
                    return;
                }
                using (SmtpClient smtpClient = new SmtpClient(objSeverDetailsBo.SMTP_SERVER, int.Parse(objSeverDetailsBo.PORT)))
                {
                    NetworkCredential basicCredential = new NetworkCredential(objSeverDetailsBo.USER_NAME, objSeverDetailsBo.PASSWORD);
                    //MailAddress fromAddress = new MailAddress(objSeverDetailsBo.EMAIL_ID);

                    // You can specify the host name or ipaddress of your server
                    // Default in IIS will be localhost
                    //smtpClient.Host = objSeverDetailsBo.SMTP_SERVER;

                    ////Default port will be 25
                    //smtpClient.Port = Convert.ToInt16(objSeverDetailsBo.PORT);

                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = basicCredential;

                    smtpClient.EnableSsl = true;

                    //From address will be given as a MailAddress Object
                    message.From = new MailAddress(objSeverDetailsBo.EMAIL_ID);

                    //From address will be given as a MailAddress Object
                    //message.From = fromAddress;
                    // To address collection of MailAddress
                    message.To.Add(strRecipients.Trim());
                    if (strPernrMailId.Length > 0)
                    {
                        message.CC.Add(strPernrMailId.Trim());
                    }
                    message.Subject = strModule;
                    Attachment att = new Attachment(filename);
                    message.Attachments.Add(att);
                    // Send SMTP mail
                    message.IsBodyHtml = true;
                    message.Body = msg.ToString();
                    smtpClient.EnableSsl = true;

                    //smtpClient.Send(message);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public static void DispatchMailBPO(string strRecipients, string strPernr, string strModule, string strPernrMailId, string msg)
        {
            try
            {
                //strPernrMailId = "karthik.k@itchamps.com";
                //strRecipients = "shruthi.n@itchamps.com";
                SmtpClient smtpClient = new SmtpClient("192.168.0.250");// 210.18.51.90
                MailMessage message = new MailMessage();
                configurationbl objBl = new configurationbl();
                configurationcollectionbo objLst = objBl.Load_Mail_Server_Details();
                configurationbo objSeverDetailsBo = objLst.Find(delegate(configurationbo obj)
                {
                    return true;
                });

                if (objSeverDetailsBo == null)
                {
                    return;
                }
                NetworkCredential basicCredential = new NetworkCredential(objSeverDetailsBo.USER_NAME, objSeverDetailsBo.PASSWORD);
                MailAddress fromAddress = new MailAddress(objSeverDetailsBo.EMAIL_ID);

                // You can specify the host name or ipaddress of your server
                // Default in IIS will be localhost 
                smtpClient.Host = objSeverDetailsBo.SMTP_SERVER;

                //Default port will be 25
                smtpClient.Port = Convert.ToInt16(objSeverDetailsBo.PORT);
                //  smtpClient.EnableSsl = true;

                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = basicCredential;

                //From address will be given as a MailAddress Object
                message.From = fromAddress;
                // To address collection of MailAddress
                //strRecipients = strRecipients.Trim();
                message.To.Add(strRecipients);
                //          strPernrMailId = strPernrMailId.Trim();

                if (strPernrMailId.Length > 0)
                {
                    message.CC.Add(strPernrMailId);
                }

                message.Subject = strModule;

                // Send SMTP mail
                message.IsBodyHtml = true;
                message.Body = msg.ToString();
                //message.EnableSsl = true;

                // smtpClient.Send(message);
                message.Dispose();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        //public static void DispatchMail(string strRecipients, string strPernr, string strModule, string strPernrMailId, string msg)
        //{
        //    try
        //    {
        //        //strPernrMailId = "monica.ks@itchamps.com";
        //        //strRecipients = "divith.km@itchamps.com";

        //        string outputmail = "";

        //        string[] authorInfo = strPernrMailId.Split(',');
        //        foreach (string info in authorInfo)
        //        {
        //            if (!string.IsNullOrEmpty(info.Trim()))
        //            {
        //                if (string.IsNullOrEmpty(outputmail.Trim()))
        //                {
        //                    outputmail = info;
        //                }
        //                else
        //                {
        //                    outputmail = outputmail + "," + info;
        //                }
        //            }
        //            //Console.WriteLine("   {0}", info);
        //        }
        //        //Console.WriteLine(outputmail);
        //        //Console.ReadLine();

        //        using (MailMessage message = new MailMessage())
        //        {
        //            configurationbl objBl = new configurationbl();
        //            configurationcollectionbo objLst = objBl.Load_Mail_Server_Details();
        //            configurationbo objSeverDetailsBo = objLst.Find(delegate(configurationbo obj)
        //            { return true; });

        //            if (objSeverDetailsBo == null)
        //            { return; }
        //            using (SmtpClient smtpClient = new SmtpClient(objSeverDetailsBo.SMTP_SERVER, int.Parse(objSeverDetailsBo.PORT)))
        //            {
        //                NetworkCredential basicCredential = new NetworkCredential(objSeverDetailsBo.USER_NAME, objSeverDetailsBo.PASSWORD);
        //                //MailAddress fromAddress = new MailAddress(objSeverDetailsBo.EMAIL_ID);

        //                //////////if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["BCC"].ToString()))
        //                //////////    message.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["BCC"].ToString()));

        //                // You can specify the host name or ipaddress of your server
        //                // Default in IIS will be localhost 
        //                //smtpClient.Host = objSeverDetailsBo.SMTP_SERVER;

        //                //Default port will be 25
        //                //smtpClient.Port = Convert.ToInt16(objSeverDetailsBo.PORT);
        //                // smtpClient.EnableSsl = true;

        //                smtpClient.UseDefaultCredentials = false;
        //                smtpClient.Credentials = basicCredential;

        //                smtpClient.EnableSsl = true;

        //                //From address will be given as a MailAddress Object
        //                message.From = new MailAddress(objSeverDetailsBo.EMAIL_ID);
        //                // To address collection of MailAddress
        //                strRecipients = strRecipients.Trim();//"raghavendra.ms@itchamps.com";
        //                message.To.Add(strRecipients);
        //                //strPernrMailId = strPernrMailId.Trim();
        //                outputmail = outputmail.Trim();

        //                if (outputmail.Length > 0)
        //                {
        //                    message.CC.Add(outputmail);
        //                }
        //                //if (strPernrMailId.Length > 0)
        //                //{
        //                //    message.CC.Add(strPernrMailId);
        //                //}
        //                message.Subject =  strModule;

        //                // Send SMTP mail
        //                message.IsBodyHtml = true;
        //                message.Body =  msg.ToString();
        //                //message.EnableSsl = true;
        //                message.Body += "<br/><br/><a runat='server' id='elogin' href='http://iemppower.itchamps.com/' target='_blank'>Click here to Login to IEmpPower</a><br/><br/>";

        //             smtpClient.Send(message);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //}


        public static void DispatchMail(string strRecipients, string strPernr, string strModule, string strPernrMailId, string msg)
        {
            try
            {
                //strRecipients = "tejaswini.ct@itchamps.com";
                //strPernrMailId = "tejaswini.ct@itchamps.com";

                string outputmail = "";
                List<string> lstStrMyList = strPernrMailId.Split(',').ToList();
                List<string> lstStrFiltered = lstStrMyList.Distinct().ToList();

                foreach (string info in lstStrFiltered)
                {
                    if (!string.IsNullOrEmpty(info.Trim()))
                    {
                        if (string.IsNullOrEmpty(outputmail.Trim()))
                        {
                            outputmail = info;
                        }
                        else
                        {
                            outputmail = outputmail + "," + info;
                        }
                    }
                }

                using (MailMessage message = new MailMessage())
                {
                    configurationbl objBl = new configurationbl();
                    configurationcollectionbo objLst = objBl.Load_Mail_Server_Details();
                    configurationbo objSeverDetailsBo = objLst.Find(delegate(configurationbo obj)
                    { return true; });

                    if (objSeverDetailsBo == null)
                    { return; }
                    using (SmtpClient smtpClient = new SmtpClient(objSeverDetailsBo.SMTP_SERVER, int.Parse(objSeverDetailsBo.PORT)))
                    {
                        NetworkCredential basicCredential = new NetworkCredential(objSeverDetailsBo.USER_NAME, objSeverDetailsBo.PASSWORD);
                       
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = basicCredential;

                        smtpClient.EnableSsl = true;

                        //From address will be given as a MailAddress Object
                        message.From = new MailAddress(objSeverDetailsBo.EMAIL_ID);
                        // To address collection of MailAddress
                        strRecipients = strRecipients.Trim();//"raghavendra.ms@itchamps.com";
                        message.To.Add(strRecipients);
                        //strPernrMailId = strPernrMailId.Trim();
                        outputmail = outputmail.Trim();

                        if (outputmail.Length > 0)
                        {
                            message.CC.Add(outputmail);
                        }
                       
                        message.Subject = strModule;

                        // Send SMTP mail
                        message.IsBodyHtml = true;
                        message.Body = msg.ToString();
                        //message.EnableSsl = true;
                        message.Body += "<br/><br/><a runat='server' id='elogin' href=' http://14.98.231.134:90/Account/Login.aspx' target='_blank'>Click here to Login to IEmpPower</a><br/><br/>";

                        smtpClient.Send(message);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        

        public static void SendMail(string MsgTo, string[] MsgCC, string MsgSubject, string MsgBody)
        {
            try
            {
            //    MsgTo = "tejaswini.ct@itchamps.com";
            //    string[,] MsgBCC = new string[,] { 
            //            //{  "vrms245@gmail.com" ,"RAGHAVENDRA M.S"}
            //{"tejaswini.ct@itchamps.com"}};

            
                using (MailMessage mailmssg = new MailMessage())
                {
                    configurationbl objBl = new configurationbl();
                    configurationcollectionbo objLst = objBl.Load_Mail_Server_Details();
                    configurationbo objSeverDetailsBo = objLst.Find(delegate(configurationbo obj)
                    { return true; });
                    if (objSeverDetailsBo == null)
                    { return; }

                    using (SmtpClient smtpClient = new SmtpClient(objSeverDetailsBo.SMTP_SERVER, int.Parse(objSeverDetailsBo.PORT)))
                    {
                        NetworkCredential basicCredential = new NetworkCredential(objSeverDetailsBo.USER_NAME, objSeverDetailsBo.PASSWORD);

                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = basicCredential;
                        smtpClient.EnableSsl = true;
                        mailmssg.From = new MailAddress(objSeverDetailsBo.EMAIL_ID);
                        MsgTo = MsgTo.Trim();
                        mailmssg.To.Add(MsgTo); //------------- TO ADDRESS -----------------

                        //-------------- CC ADDRESS -----------------
                        if (MsgCC.Length > 0)
                        {
                            for (int i = 0; i < MsgCC.Length; i++)
                            { if (!string.IsNullOrEmpty(MsgCC[i].Trim())) { mailmssg.CC.Add(new MailAddress(MsgCC[i])); } }
                        }


                        mailmssg.Subject = MsgSubject;
                        mailmssg.Body = MsgBody;
                        mailmssg.IsBodyHtml = true;
                        //mailmssg.BodyEncoding = Encoding.UTF8;
                        
                        //mailmssg.Body += "<br/><br/><a runat='server' id='elogin' href='http://iemppower.itchamps.com/' target='_blank'>Click here to Login to IEmpPower</a><br/><br/>";

                        smtpClient.Send(mailmssg);
                        //SMTPClient.Dispose();
                        //MailMsg.Dispose();                        
                    }
                }
            }
            catch (Exception Ex)
            { throw Ex; }
        }


        public static void CreateUser_SentMail(string PERNR, string sEmailId, string sPassowd)
        {
            try
            {

                SmtpClient smtpClient = new SmtpClient();
                MailMessage message = new MailMessage();
                configurationbl objBl = new configurationbl();
                configurationcollectionbo objLst = objBl.Load_Mail_Server_Details();
                configurationbo objSeverDetailsBo = objLst.Find(delegate(configurationbo obj)
                {
                    return true;
                });
                NetworkCredential basicCredential = new NetworkCredential(objSeverDetailsBo.USER_NAME, objSeverDetailsBo.PASSWORD);
                MailAddress fromAddress = new MailAddress(objSeverDetailsBo.EMAIL_ID);

                // You can specify the host name or ipaddress of your server
                // Default in IIS will be localhost 
                smtpClient.Host = objSeverDetailsBo.SMTP_SERVER;


                //Default port will be 25
                smtpClient.Port = Convert.ToInt16(objSeverDetailsBo.PORT);

                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = basicCredential;

                //From address will be given as a MailAddress Object
                message.From = fromAddress;
                // To address collection of MailAddress
                message.To.Add(sEmailId);
                message.Subject = "Your password is " + sPassowd;

                // Message body content

                // message.IsBodyHtml = true;
                // Send SMTP mail
                message.Body = "Dear " + PERNR + "," + "Your password is " + sPassowd + "." + " \nBest regards";
                //message.EnableSsl = true;
                smtpClient.EnableSsl = true;
                message.Body += "<br/><br/><a runat='server' id='elogin' href='http://iemppower.itchamps.com/' target='_blank'>Click here to Login to IEmpPower</a><br/><br/>";

                smtpClient.Send(message);

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public static mastercollectionbo Load_Country_Nationality()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            var Nationality = objDataContext.sp_master_load_country_nationality();
            foreach (var vRow in Nationality)
            {
                masterbo objBo = new masterbo();
                objBo.LAND1 = vRow.LAND1.Trim();
                objBo.LANDX = vRow.LANDX.Trim();
                objBo.NATIO = vRow.NATIO.Trim();
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static mastercollectionbo Load_States()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_states())
            {
                masterbo objBo = new masterbo();
                objBo.LAND1 = vRow.LAND1.Trim();
                objBo.BLAND = vRow.BLAND.Trim();
                objBo.BEZEI = vRow.BEZEI.Trim();
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static mastercollectionbo Load_Address_Type()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_address_type())
            {
                masterbo objBo = new masterbo();
                objBo.SUBTY = vRow.SUBTY;
                objBo.STEXT = vRow.STEXT;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static mastercollectionbo Load_Personal_ID_Type()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_personal_id_type())
            {
                masterbo objBo = new masterbo();
                objBo.ICTYP = vRow.ICTYP;
                objBo.ICTXT = vRow.ICTXT;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static mastercollectionbo Load_Family_Member_Type()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_member_types())
            {
                masterbo objBo = new masterbo();
                objBo.SUBTY = vRow.SUBTY;
                objBo.STEXT = vRow.STEXT;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static mastercollectionbo Load_NamePrefix_OtherTitle()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_name_prefix_othertitle())
            {
                masterbo objBo = new masterbo();
                objBo.ART = char.Parse(vRow.ART.ToString());
                objBo.TTOUT = vRow.TTOUT.Trim();
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static mastercollectionbo Load_Bank_Types()
        {
            masterdalDataContext objDataContext;

            objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();

            foreach (var vRow in objDataContext.sp_master_load_bank_types())
            {
                masterbo objBo = new masterbo();
                objBo.SUBTY = vRow.SUBTY;
                objBo.STEXT = vRow.STEXT;

                objList.Add(objBo);
            }
            return objList;
        }

        public static mastercollectionbo Load_Bank_Keys(masterbo mBo)
        {
            masterdalDataContext objDataContext;

            objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();

            foreach (var vRow in objDataContext.sp_master_load_bank_keys(mBo.LAND1))
            {
                masterbo objBo = new masterbo();
                //objBo.LAND1 = vRow.BANKS;
                objBo.BANKL = vRow.BANKL;
                objBo.BANKA = vRow.BANKA;
                objList.Add(objBo);
            }
            return objList;
        }

        public static mastercollectionbo Load_Payment_Method(masterbo mBo)
        {
            masterdalDataContext objDataContext;

            objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();

            foreach (var vRow in objDataContext.sp_master_load_payment_method(mBo.LAND1))
            {
                masterbo objBo = new masterbo();
                //objBo.LAND1 = vRow.LAND1;
                objBo.ZLSCH = vRow.ZLSCH.ToString();
                objBo.TEXT1 = vRow.TEXT1;
                objList.Add(objBo);
            }
            return objList;
        }

        public static mastercollectionbo Load_Payment_Currency()
        {
            masterdalDataContext objDataContext;
            objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_payment_currency())
            {
                masterbo objBo = new masterbo();
                objBo.WAERS = vRow.WAERS;
                // objBo.LTEXT = vRow.LTEXT;
                objBo.LTEXT = vRow.WAERS + " - " + vRow.LTEXT;
                objBo.WARESTXT = vRow.WAERS + " - " + vRow.LTEXT;

                objList.Add(objBo);
            }
            return objList;
        }

        //Method to load language dropdown
        public static mastercollectionbo Load_Language()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_language_dropdown())
            {
                masterbo objBo = new masterbo();
                objBo.SPRSL = Convert.ToString(vRow.SPRSL);
                objBo.SPTXT = vRow.SPTXT;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        // Method to load selected country Region(State,province,county)
        public static mastercollectionbo Load_States_For_Slctd_Country(masterbo mBo)
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_slctd_country_states(mBo.LAND1))
            {
                masterbo objBo = new masterbo();
                //   objBo. = vRow.LAND1.Trim();
                objBo.BLAND = vRow.BLAND.Trim();
                objBo.BEZEI = vRow.BEZEI.Trim();
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        // Method to load religion dropdown
        public static mastercollectionbo Load_Religion()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_religion())
            {
                masterbo objBo = new masterbo();
                objBo.KITXT = vRow.KITXT.Trim();
                objBo.KTEXT = vRow.KTEXT.Trim();
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }
        // Method to load religion dropdown
        public static mastercollectionbo Load_MArital_Status()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_marital_status())
            {
                masterbo objBo = new masterbo();
                objBo.FAMST = vRow.FAMST.Trim();
                objBo.FTEXT = vRow.FTEXT.Trim();
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        // Method to load all time event type
        public static mastercollectionbo Load_Time_Event_Types()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_time_events())
            {
                masterbo objBo = new masterbo();

                objBo.SUBTY = vRow.DOMVALUE_L;
                objBo.STEXT = vRow.DDTEXT;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        //method to load attendence and absence type dropdown
        //public static mastercollectionbo Load_Attendence_abs_Types(string PERNR)
        //{
        //    masterdalDataContext objDataContext = new masterdalDataContext();
        //    mastercollectionbo objList = new mastercollectionbo();
        //    foreach (var vRow in objDataContext.sp_master_load_att_abs(PERNR))
        //    {
        //        masterbo objBo = new masterbo();
        //        objBo.AWART = vRow.AWART;
        //        objBo.ATEXT = vRow.ATEXT;
        //        //objBo.TC = vRow.MOABW.ToString();
        //        objList.Add(objBo);
        //    }
        //    objDataContext.Dispose();
        //    return objList;
        //}
        // Method to load all time event type

        //method to load attendence and absence type dropdown
        public static mastercollectionbo Load_Attendence_abs_Types_Leave(string type, string PERNR,string cmpcode)
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_att_abs_Leave(type, PERNR, cmpcode))
            {
                masterbo objBo = new masterbo();
                objBo.AWART = vRow.AWART;
                objBo.ATEXT = vRow.ATEXT;
                //objBo.TC = vRow.MOABW.ToString();
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }
        // Method to load all time event type



        public static mastercollectionbo Load_Title()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_title())
            {
                masterbo objBo = new masterbo();
                //Here i am not created new object variable because of
                // i have used already created object variable, like
                // SUBTY==ANRED and STEXT==ATEXT
                objBo.SUBTY = vRow.ANRED.ToString();
                objBo.STEXT = vRow.ATEXT;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        //method to load attendence and absence type dropdown
        public static mastercollectionbo Load_Attendence_abs_Types(string cc)
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_att_abs(cc))
            {
                masterbo objBo = new masterbo();
                objBo.pspnr = vRow.MOAWB;
                objBo.ATEXT = vRow.ATEXT;
                objBo.TC = vRow.MOAWB.ToString();
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static mastercollectionbo Load_CostCenter()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_costcenter())
            {
                masterbo objBo = new masterbo();
                objBo.CC_TEXT = vRow.CC_Text;
                objBo.KOSTL = vRow.KOSTL;
                objBo.LTEXT = vRow.LTEXT;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static mastercollectionbo Load_Order(string cc,DateTime begda,DateTime endda)
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_order(cc, begda, endda))
            {
                masterbo objBo = new masterbo();
                objBo.ORDER_TEXT =  vRow.POST1;////.Order_Text;
                objBo.pspnr = vRow.PSPNR;////.AUFNR;
                objBo.KTEXT = vRow.POST1;////.KTEXT;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static mastercollectionbo Load_Network(string wbs)
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_network(wbs))
            {
                masterbo objBo = new masterbo();
                objBo.RNPLNR = vRow.KTEXT;
                objBo.AUFNR = vRow.AUFNR.ToString();
                objBo.KTEXT = vRow.KTEXT;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static mastercollectionbo Load_Acttype(string network, string wbs)
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_acttype(network, wbs))
            {
                masterbo objBo = new masterbo();
                objBo.Activitytype = vRow.LTXA1;
                objBo.LSTAR = vRow.VORNR;
                objBo.MCTXT = vRow.LTXA1;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static mastercollectionbo Load_Wbs(string cc,string project)
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_wbs(cc,project))
            {
                masterbo objBo = new masterbo();
                objBo.RPROJ = vRow.POSID;
                objBo.pspnr = vRow.PSPNR;
                objBo.POST1 = vRow.POSID;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static mastercollectionbo Load_Acty(string cc, int wbs)
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_acty(cc, wbs))
            {
                masterbo objBo = new masterbo();
                objBo.pspnr = vRow.ID;
                objBo.POST1 = vRow.Activity;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }


        public static mastercollectionbo Load_Travel_Activity()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_travel_request_activities())
            {
                masterbo objBo = new masterbo();
                objBo.ACTIVITY_TYPE = vRow.KZTKT.ToString();
                objBo.ACTIVITY = vRow.TKTXT;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static List<string> LoadHours()
        {
            List<string> hours = new List<string>();
            hours.Add("00");
            hours.Add("01");
            hours.Add("02");
            hours.Add("03");
            hours.Add("04");
            hours.Add("05");
            hours.Add("06");
            hours.Add("07");
            hours.Add("08");
            hours.Add("09");
            hours.Add("10");
            hours.Add("11");
            hours.Add("12");
            hours.Add("13");
            hours.Add("14");
            hours.Add("15");
            hours.Add("16");
            hours.Add("17");
            hours.Add("18");
            hours.Add("19");
            hours.Add("20");
            hours.Add("21");
            hours.Add("22");
            hours.Add("23");

            return hours;
        }

        public static List<string> LoadMinutes()
        {
            List<string> minutes = new List<string>();
            minutes.Add("00");
            minutes.Add("05");
            minutes.Add("10");
            minutes.Add("15");
            minutes.Add("20");
            minutes.Add("25");
            minutes.Add("30");
            minutes.Add("35");
            minutes.Add("40");
            minutes.Add("45");
            minutes.Add("50");
            minutes.Add("55");

            return minutes;
        }

        public static mastercollectionbo Load_Trip_Type_Statutory()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_trip_type_statutory())
            {
                masterbo objBo = new masterbo();
                objBo.KZREA = vRow.KZREA;
                objBo.RETXT = vRow.RETXT;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static mastercollectionbo Load_Trip_Type_Enterprise()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_trip_type_enterprise())
            {
                masterbo objBo = new masterbo();
                objBo.BEREI = vRow.BEREI;
                objBo.TEXT25 = vRow.TEXT25;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static mastercollectionbo Load_Vehicle_Types()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_vehicle_type())
            {
                masterbo objBo = new masterbo();
                objBo.KZPMF = vRow.KZPMF;
                objBo.FZTXT = vRow.FZTXT;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static mastercollectionbo Load_Vehicle_Classes()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_vehicle_class())
            {
                masterbo objBo = new masterbo();
                objBo.PKWKL = vRow.PKWKL;
                objBo.TEXT25 = vRow.TEXT25;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static mastercollectionbo Load_Expenses_Receipts()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_expenses_receipts())
            {
                masterbo objBo = new masterbo();
                objBo.SPKZL = vRow.SPKZL;
                objBo.SPTXT = vRow.SPTXT;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static mastercollectionbo Load_Provider_Category()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_provider_category())
            {
                masterbo objBo = new masterbo();
                objBo.PROVIDER = vRow.PROVIDER;
                objBo.NAME = vRow.NAME;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static String readHtmlPage(string url)
        {
            String result = "";
            String strPost = "x=1&y=2&z=YouPostedOk";
            StreamWriter myWriter = null;

            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url.ToString());
            objRequest.Method = "POST";
            objRequest.ContentLength = strPost.Length;
            objRequest.ContentType = "application/x-www-form-urlencoded";

            try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(strPost);
                myWriter.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Make sure you are connected to internet");
                return e.Message;
            }

            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                Console.WriteLine(result.ToString());

                // Close and clean up the StreamReader
                sr.Close();
            }
            return result;
        }

        public static mastercollectionbo Load_encashable_leave_Types()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_encashable_leaves())
            {
                masterbo objBo = new masterbo();
                objBo.AWART = vRow.AWART.Trim();
                objBo.ATEXT = vRow.ATEXT;
                objBo.TC = vRow.MOAWB.ToString();
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }
        public static mastercollectionbo Load_Sub_Type()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_sub_type())
            {
                masterbo objBo = new masterbo();
                objBo.SUBTY = vRow.SUBTY;
                objBo.STEXT = vRow.STEXT;
                objBo.TC = vRow.TC;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }
        public static mastercollectionbo Load_Property_Types()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_property_type())
            {
                masterbo objBo = new masterbo();
                objBo.AWART = vRow.type_value;
                objBo.ATEXT = vRow.type_name;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }
        public static mastercollectionbo Load_Project(string createdby)
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_Project(createdby))
            {
                masterbo objBo = new masterbo();

                objBo.WBSID = vRow.WBSID;
                // objBo.PROJECT = vRow.PROJECT;
                objBo.WBS = vRow.WBS + " - " + vRow.WBSID;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }
        public static mastercollectionbo Load_MIS_GRPC()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_MISC())
            {
                masterbo objBo = new masterbo();
                objBo.CID = vRow.CID;
                objBo.C_DESC = vRow.C_DESC;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }
        //Method to Load MIS Group A and B
        public static mastercollectionbo Load_MIS_AB(masterbo mBo)
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_MISAB(mBo.LL))
            {
                masterbo objBo = new masterbo();

                objBo.A_DESC = vRow.A_DESC;
                objBo.B_DESC = vRow.B_DESC;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }
        //Method to Load Main Function
        public static mastercollectionbo Load_MainFunction(string Perner)
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_MainFunction(Perner))
            {
                masterbo objBo = new masterbo();
                objBo.FUNC_AREA = vRow.FUNC_AREA;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        //Method to load Item Decription PR

        public static mastercollectionbo Load_ItemDesc()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();

            foreach (var vRow in objDataContext.sp_master_load_CategoryItem_Desc())
            {
                masterbo objBo = new masterbo();
                objBo.MATNR = vRow.MATNR;
                objBo.MAKTX = vRow.MAKTX;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static mastercollectionbo Load_Expensetype_travel(string schema, string pernr)
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_ExpenseType_travel(schema, pernr))
            {
                masterbo objBo = new masterbo();
                objBo.SPTXT = vRow.SPTXT;
                objBo.SPKZL = vRow.SPKZL;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }
       
        
        public static mastercollectionbo Load_ExpenseType(string task)
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_ExpenseType(task))
            {
                masterbo objBo = new masterbo();
                objBo.LGART = vRow.LGART;
                objBo.LGTXT = vRow.LGART + " - " + vRow.LGTXT;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }


        //Method to load Project Code
        public static mastercollectionbo Load_ERP_ProjectCode()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_ProjectCode())
            {
                masterbo objBo = new masterbo();
                objBo.POSID = vRow.POSID;
                objBo.POST1 = vRow.POST1 + " - " + vRow.POSID;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }


        public static mastercollectionbo Load_Country()
        {
            masterdalDataContext objDataContext;
            objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_country_nationality())
            {
                masterbo objBo = new masterbo();
                objBo.LANDX = vRow.LANDX;
                objBo.LAND1 = vRow.LAND1;


                objList.Add(objBo);
            }
            return objList;
        }

        //Method to load Business Unit
        public static mastercollectionbo Load_Business_Unit(string entity)
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_master_load_BusinessUnit(entity))
            {
                masterbo objBo = new masterbo();
                objBo.SPART = vRow.SPART;

                objBo.VTEXT = vRow.SPART + " - " + vRow.VTEXT;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        //Method to load Ship Bill to address dropdown

        public static mastercollectionbo Load_BillShipTo_Address()
        {
            masterdalDataContext objDataContext = new masterdalDataContext();
            mastercollectionbo objList = new mastercollectionbo();
            foreach (var vRow in objDataContext.sp_get_BillShipToAddress())
            {
                masterbo objBo = new masterbo();
                objBo.WERKS = vRow.WERKS;
                objBo.NAME1 = vRow.NAME1;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

    }
}