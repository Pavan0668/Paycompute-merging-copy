using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Personal_Information;

/// <summary>
/// Summary description for pidashboardbl
/// </summary>
public class pidashboardbl
{
    public pidashboardbl()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    pidashboarddalDataContext objPIDashBoardDataContext = new pidashboarddalDataContext();

    public pidashboardcollectionbo Get_Dashboard_Details(pidashboardbo objPIDashboardBo, string Year,string ccode,ref int? RecordCnt,int? sortid)
    {
        pidashboardcollectionbo objPIDashboardBoLst = new pidashboardcollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_pi_load_dashboard(objPIDashboardBo.PERNR, objPIDashboardBo.PageIndex, objPIDashboardBo.PageSize, Year.Trim(), ccode, ref RecordCnt, sortid))
        {


            pidashboardbo objPIDashBo = new pidashboardbo();
            objPIDashBo.RowNumber = vRow.RowNumber;
            objPIDashBo.PKEY = vRow.pkey.ToString();
            objPIDashBo.PERNR = vRow.PERNR.ToString();
            //objPIDashBo.MANAGER_APPROVAL = vRow.Manager_Approval;
            objPIDashBo.CHANGE_APPROVAL = vRow.change_approval;
            objPIDashBo.REVIEW = vRow.Review;
            objPIDashBo.LAST_ACTIVITY_DATE = Convert.ToDateTime(vRow.Last_Activity_Date);
            objPIDashBo.ID = int.Parse(vRow.ID.ToString());
            objPIDashBo.TableTyp = vRow.TableTyp;
            objPIDashBo.Subtype = vRow.Subtype;
            objPIDashBo.AppPERNR = vRow.A_PERNR;
            objPIDashBo.AppByName = vRow.A_Name;

            objPIDashboardBoLst.Add(objPIDashBo);
        }
        return objPIDashboardBoLst;
    }
    public pidashboardcollectionbo Get_Dashboard__Completed_Details(pidashboardbo objPIDashboardBo, string ccode, string Year, ref int? RecordCnt,int? sortid)
    {
        pidashboardcollectionbo objPIDashboardBoLst = new pidashboardcollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_pi_load_dashboard_completed(objPIDashboardBo.PERNR, ccode, objPIDashboardBo.PageIndex, objPIDashboardBo.PageSize, Year.Trim(), ref RecordCnt, sortid))
        {
            pidashboardbo objPIDashBo = new pidashboardbo();
            objPIDashBo.RowNumber = vRow.RowNumber;
            objPIDashBo.PKEY = vRow.pkey;
            objPIDashBo.PERNR = vRow.PERNR;
            //objPIDashBo.MANAGER_APPROVAL = vRow.Manager_Approval;
            objPIDashBo.CHANGE_APPROVAL = vRow.change_approval;
            objPIDashBo.REVIEW = vRow.Review;
            objPIDashBo.LAST_ACTIVITY_DATE = Convert.ToDateTime(vRow.Last_Activity_Date);
            objPIDashBo.ID = int.Parse(vRow.ID.ToString());
            objPIDashBo.TableTyp = vRow.TableTyp;
            objPIDashBo.MODIFIEDON = Convert.ToDateTime(vRow.ModifiedOn);
            objPIDashBo.AppByName = vRow.AppBy.ToString().Trim();
            objPIDashBo.Subtype = vRow.Subtype;
            objPIDashBo.AppPERNR = vRow.A_PERNR;
            objPIDashBo.AppByName = vRow.A_Name;
            objPIDashboardBoLst.Add(objPIDashBo);
        }
        return objPIDashboardBoLst;
    }
    public piaddressinformationcollectionbo Get_Address_Details_For_Approval_For_Employee(piaddressinformationbo objPIAddrressBo)
    {
        piaddressinformationcollectionbo objPIAddBoLst = new piaddressinformationcollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_pi_get_address_approval_for_employee(objPIAddrressBo.PKEY))
        {
            piaddressinformationbo objPIAddBo = new piaddressinformationbo();
            objPIAddBo.PKEY = vRow.pkey;
            objPIAddBo.ADDRESS_TYPE_ID = vRow.STEXT;
            objPIAddBo.DATE_FROM = Convert.ToDateTime(vRow.BEGDA);
            objPIAddBo.DATE_TO = Convert.ToDateTime(vRow.ENDDA);
            objPIAddBo.ADDRESSL1 = vRow.STRAS;
            objPIAddBo.ADDRESSL2 = vRow.LOCAT;
            objPIAddBo.GBLND = vRow.LANDX;
            objPIAddBo.STATE_ID = vRow.BEZEI;
            objPIAddBo.CITY = vRow.ORT01;
            objPIAddBo.POSTAL_CODE = vRow.PSTLZ;
            objPIAddBo.PHONENO = vRow.TELNR;
            objPIAddBo.PKEY = vRow.pkey;
            objPIAddBo.COMMENTS = vRow.comments.Trim();
            objPIAddBo.ISAPPROVED = Convert.ToBoolean(vRow.isapproved);
            objPIAddBo.ISUPDATE = Convert.ToBoolean(vRow.hr_approved);

            objPIAddBoLst.Add(objPIAddBo);
        }
        objPIDashBoardDataContext.Dispose();
        return objPIAddBoLst;
    }
    public pibankinformationcollectionbo Get_Bank_Details_For_Approval_Employee(pibankinformationbo objBankInfoBo, string sStatus)
    {
        pibankinformationcollectionbo objBankInfoLst = new pibankinformationcollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_pi_get_bank_approval_for_employee(objBankInfoBo.PKEY, sStatus))
        {
            pibankinformationbo objBo = new pibankinformationbo();
            objBo.PKEY = vRow.pkey;
            objBo.PERNR = vRow.PERNR.ToString();
            objBo.BANK_TYPE_ID = vRow.STEXT;//Bank type name
            objBo.PAYEE = vRow.EMFTX;
            objBo.POSTAL_CODE = vRow.BKPLZ;
            objBo.CITY = vRow.BKORT;
            objBo.COUNTRY_NAME = vRow.LANDX;
            objBo.BANK_TYPE_NAME = vRow.BANKA;//Bank name
            objBo.BANK_ACCOUNT = vRow.BANKN;
            objBo.PURPOSE = vRow.ZWECK;
            objBo.PAYMENT_METHOD_NAME = vRow.TEXT1;
            objBo.PAYMENT_CURRENCY_NAME = vRow.LTEXT;
            objBo.COMMENTS = vRow.comments.Trim();
            objBo.ISAPPROVED = Convert.ToBoolean(vRow.isapproved);
            objBankInfoLst.Add(objBo);
        }
        objPIDashBoardDataContext.Dispose();
        return objBankInfoLst;
    }
    public pifamilymemberscollectionbo Get_FamilyMemberDetails_For_Employee(pifamilymembersbo objFamilyBo, string sStatus)
    {
        pifamilymemberscollectionbo objFamilyTypeLst = new pifamilymemberscollectionbo();
        //foreach (var vRow in objPIDashBoardDataContext.sp_pi_get_familymember_approval_for_employee(objFamilyBo.PKEY, objFamilyBo.ISAPPROVED, sStatus))
        //{
        //    pifamilymembersbo objBo = new pifamilymembersbo();
        //    objBo.PENNR = vRow.PERNR.ToString();
        //    objBo.FAMSA = vRow.FAMSA;
        //    objBo.FAVOR = vRow.FAVOR;
        //    objBo.FANAM = vRow.FANAM;
        //    objBo.FGBNA = vRow.FGBNA;
        //    if (vRow.FASEX == '1')
        //    {
        //        objBo.FASEX = "Male";
        //    }
        //    else
        //    {
        //        objBo.FASEX = "Female";
        //    }
        //    objBo.FINIT = vRow.FINIT;
        //    objBo.FNMZU = vRow.FNMZU;
        //    objBo.FVRSW = vRow.FVRSW;
        //    objBo.FGBDT = Convert.ToDateTime(vRow.FGBDT);
        //    objBo.FGBOT = vRow.FGBOT;
        //    objBo.FGBLD = vRow.FGBLD;
        //    objBo.FANAT = vRow.FANAT;
        //    objBo.FANA2 = vRow.FANA2;
        //    objBo.FANA3 = vRow.FANA3;
        //    objBo.KDZUL = vRow.KDZUL;
        //    objBo.KDBSL = vRow.KDBSL;
        //    objBo.KDBGR = vRow.KDBGR;
        //    objBo.OBJPS = vRow.OBJPS;
        //    objBo.PKEY = vRow.PKEY;
        //    objBo.COMMENTS = vRow.comments;
        //    objBo.ISAPPROVED = vRow.isapproved;
        //    objFamilyTypeLst.Add(objBo);
        //}
        //objPIDashBoardDataContext.Dispose();
        return objFamilyTypeLst;
    }

    public pipersonalidscollectionbo Get_PersonalIDS_Details_For_Approval_Employee(pipersonalidsbo objPersonalIDBo, string sStatus)
    {
        pipersonalidscollectionbo objPersonalIDsLst = new pipersonalidscollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_pi_get_personalid_approval_for_employee(objPersonalIDBo.PERNR, objPersonalIDBo.ICTYPE,
                                                                                                  objPersonalIDBo.ISAPPROVED, sStatus))
        {
            pipersonalidsbo objBo = new pipersonalidsbo();
            objBo.PERNR = vRow.PERNR.ToString();
            objBo.ICTYPE = vRow.ICTYPE;
            objBo.ICNUM = vRow.ICNUM;
            objBo.COMMENTS = vRow.comments;
            objBo.DESCRIPTION = vRow.ICTXT;
            objBo.ISAPPROVED = vRow.isapproved;
            objPersonalIDsLst.Add(objBo);
        }
        objPIDashBoardDataContext.Dispose();
        return objPersonalIDsLst;
    }
    public personaldatacollectionbo Get_PersonalData_Details_For_Approval_Employee(personaldatabo objPersonaldataBo, string sStatus)
    {
        personaldatacollectionbo objPersonaldataList = new personaldatacollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_pi_get_personaldata_approval_for_employee(objPersonaldataBo.EMPLOYEE_ID.ToString(),
                                                                                              objPersonaldataBo.ISAPPROVED, sStatus))
        {
            personaldatabo objBo = new personaldatabo();
            objBo.TITEL = vRow.TITEL;
            objBo.VORNA = vRow.VORNA;
            objBo.NACHN = vRow.NACHN;
            objBo.NAME2 = vRow.NAME2;
            objBo.INITS = vRow.INITS;
            objBo.RUFNM = vRow.RUFNM;
            objBo.SPRSL = vRow.SPRSL;
            if (vRow.GESCH == "1")
            {
                objBo.GESCH = "Male";
            }
            else if (vRow.GESCH == "2")
            {
                objBo.GESCH = "Female";
            }
            else
            {
                objBo.GESCH = "Unknown";
            }
            if (vRow.GBDAT != null)
            {
                objBo.GBDAT = Convert.ToDateTime(vRow.GBDAT.ToString("d-MMM-yyyy"));
            }
            else
            {
                objBo.GBDAT = null;
            }
            objBo.GBORT = vRow.GBORT;
            objBo.GBLND = vRow.GBLND;
            objBo.NATIO = vRow.NATI0;
            objBo.GBDEP = vRow.GBDEP;
            objBo.NATI2 = vRow.NATI2;
            objBo.NATI3 = vRow.NATI3;
            objBo.FAMST = vRow.FAMST;
            if (vRow.FAMDT == null || vRow.FAMDT.ToString() == "1/1/1900 12:00:00 AM")
            {
                objBo.FAMDT = null;
            }
            else
            {
                objBo.FAMDT = Convert.ToDateTime(vRow.FAMDT.ToString("d-MMM-yyyy"));
            }
            objBo.ANZKD = Int16.Parse(vRow.ANZKD.ToString());
            objBo.KITXT = vRow.KITXT;
            objBo.COMMENTS = vRow.comments;
            objBo.ISAPPROVED = Convert.ToBoolean(vRow.isapproved);
            objBo.EMPLOYEE_PHOTOPATH = vRow.PHOTO;
            objPersonaldataList.Add(objBo);
        }
        return objPersonaldataList;
    }
    public picommunicationinformationcollectionbo Get_Communication_Details_For_Approval_For_Employee(picommunicationinformationbo objCommInfoBo, string sStatus)
    {
        picommunicationinformationcollectionbo objComInfoLst = new picommunicationinformationcollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_pi_get_communication_approval_for_employee(objCommInfoBo.EMPLOYEE_ID, objCommInfoBo.ISAPPROVED, sStatus))
        {
            picommunicationinformationbo objBo = new picommunicationinformationbo();
            if (vRow.USRTY == "0002")
            {
                objBo.EMPLOYEE_ID = vRow.PERNR.ToString();
                objBo.BUILDING_NO = vRow.USRID;
                objBo.USRTY = vRow.USRTY;
                objBo.COMMENTS = vRow.comments;
                objBo.ISAPPROVED = Convert.ToBoolean(vRow.isapproved);
            }
            else if (vRow.USRTY == "0003")
            {
                objBo.EMPLOYEE_ID = vRow.PERNR.ToString();
                objBo.ROOM_NO = vRow.USRID;
                objBo.USRTY = vRow.USRTY;
                objBo.COMMENTS = vRow.comments;
                objBo.ISAPPROVED = Convert.ToBoolean(vRow.isapproved);

            }
            else if (vRow.USRTY == "0004")
            {
                objBo.EMPLOYEE_ID = vRow.PERNR.ToString();
                objBo.EXTENSION = vRow.USRID;
                objBo.USRTY = vRow.USRTY;
                objBo.COMMENTS = vRow.comments;
                objBo.ISAPPROVED = Convert.ToBoolean(vRow.isapproved);

            }
            else if (vRow.USRTY == "0010")
            {
                objBo.EMPLOYEE_ID = vRow.PERNR.ToString();
                objBo.EMAIL = vRow.USRID;
                objBo.USRTY = vRow.USRTY;
                objBo.COMMENTS = vRow.comments;
                objBo.ISAPPROVED = Convert.ToBoolean(vRow.isapproved);

            }
            else if (vRow.USRTY == "0006")
            {
                objBo.EMPLOYEE_ID = vRow.PERNR.ToString();
                objBo.LICENCE_NO = vRow.USRID;
                objBo.USRTY = vRow.USRTY;
                objBo.COMMENTS = vRow.comments;
                objBo.ISAPPROVED = Convert.ToBoolean(vRow.isapproved);
            }
            else if (vRow.USRTY == "MPHN")
            {
                objBo.EMPLOYEE_ID = vRow.PERNR.ToString();
                objBo.MPHN_ID = vRow.USRID;
                objBo.MPHN = vRow.USRTY;
                objBo.COMMENTS = vRow.comments;
                objBo.ISAPPROVED = Convert.ToBoolean(vRow.isapproved);
            }
            objComInfoLst.Add(objBo);
        }
        objPIDashBoardDataContext.Dispose();
        return objComInfoLst;
    }
    public wtclockinoutcorrectioncollectionbo Get_ClockIO_Details_For_Approval_Employee(wtclockinoutcorrectionbo objClockIOBo, string sStatus)
    {
        wtclockinoutcorrectioncollectionbo obClockIOLst = new wtclockinoutcorrectioncollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_pi_get_clockinout_approval_for_employee(objClockIOBo.PKEY, objClockIOBo.ISAPPROVED, sStatus))
        {
            wtclockinoutcorrectionbo objBo = new wtclockinoutcorrectionbo();
            objBo.PKEY = vRow.PKEY;
            objBo.LTIME = vRow.LTIME;
            objBo.LDATE = Convert.ToDateTime(vRow.LDATE);
            objBo.SATZA = vRow.SATZA;
            objBo.SATZA_TYPE = vRow.ZTEXT;
            objBo.NOTE = vRow.NOTE;
            objBo.APPROVEDBY = vRow.APPROVED_BY.ToString();
            objBo.COMMENTS = vRow.comments;
            objBo.ENTRY_STATUS = vRow.entrystatus;
            objBo.ISAPPROVED = Convert.ToBoolean(vRow.isapproved);
            obClockIOLst.Add(objBo);
        }
        objPIDashBoardDataContext.Dispose();
        return obClockIOLst;
    }
    public leaverequestcollectionbo Get_LeaveRequest_Details_For_Approval_For_Employee(leaverequestbo objLeaveRequestBo, string Tbltyp) 
    {
        leaverequestcollectionbo objLeaveRequestLst = new leaverequestcollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_pi_get_leaverequest_approval_for_employee(objLeaveRequestBo.LEAVE_REQ_ID,objLeaveRequestBo.PKEY.ToString(),Tbltyp))
        {
            leaverequestbo objBo = new leaverequestbo();
            //objBo.LEAVE_REQ_ID = vRow.LEAVE_REQ_ID;
            //objBo.AWART = vRow.AWART;
            //objBo.ATEXT = vRow.ATEXT;
            //objBo.BEGDA = Convert.ToDateTime(vRow.BEGDA);
            //objBo.ENDDA = vRow.ENDDA.ToString();
            //objBo.BEGUZ = vRow.BEGUZ;
            //objBo.ENDUZ = vRow.ENDUZ;
            //objBo.STDAZ = vRow.STDAZ.ToString();
            //objBo.APPROVED_BY_NAME = vRow.ENAME;//Convert.ToInt32(vRow.APPROVED_BY);
            //objBo.NOTE = vRow.NOTE;
            //objBo.PKEY = vRow.PKEY;
            //objBo.COMMENTS = vRow.approver_comments;
            objBo.Slno = vRow.Slno.ToString();
            objBo.TEXT = vRow.TEXT;
            objBo.VALUES = vRow.VALUE;

            objLeaveRequestLst.Add(objBo);
        }
        objPIDashBoardDataContext.Dispose();
        return objLeaveRequestLst;
    }


    public leaverequestcollectionbo Get_DeletionRequest_Details_For_Approval_For_Employee(leaverequestbo objLeaveRequestBo, string Tbltyp) 
    {
        leaverequestcollectionbo objLeaveRequestLst = new leaverequestcollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_pi_get_Deletionrequest_approval_for_employee(objLeaveRequestBo.LEAVE_REQ_ID, objLeaveRequestBo.PKEY.ToString(), Tbltyp))
        {
            leaverequestbo objBo = new leaverequestbo();
            //objBo.LEAVE_REQ_ID = vRow.LEAVE_REQ_ID;
            //objBo.AWART = vRow.AWART;
            //objBo.ATEXT = vRow.ATEXT;
            //objBo.BEGDA = Convert.ToDateTime(vRow.BEGDA);
            //objBo.ENDDA = vRow.ENDDA.ToString();
            //objBo.BEGUZ = vRow.BEGUZ;
            //objBo.ENDUZ = vRow.ENDUZ;
            //objBo.STDAZ = vRow.STDAZ.ToString();
            //objBo.APPROVED_BY_NAME = vRow.ENAME;//Convert.ToInt32(vRow.APPROVED_BY);
            //objBo.NOTE = vRow.NOTE;
            //objBo.PKEY = vRow.PKEY;
            //objBo.COMMENTS = vRow.approver_comments;
            objBo.Slno = vRow.Slno.ToString();
            objBo.TEXT = vRow.TEXT;
            objBo.VALUES = vRow.VALUE;

            objLeaveRequestLst.Add(objBo);
        }
        objPIDashBoardDataContext.Dispose();
        return objLeaveRequestLst;
    }

 
    public pipersonalidscollectionbo Get_PersonalIDS_Details_For_Approval(pipersonalidsbo objPersonalIDBo, int flag)
    {
        pipersonalidscollectionbo objPersonalIDsLst = new pipersonalidscollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_ms_get_personalid_approval_for_employee(objPersonalIDBo.PKEY, objPersonalIDBo.ID, objPersonalIDBo.STATUS, flag))
        {

            pipersonalidsbo objBo = new pipersonalidsbo();
            objBo.PKEY = vRow.PKEY;
            objBo.ID = vRow.ID == null ? 0 : vRow.ID;
            objBo.CREATED_BY = vRow.PERNR;
            objBo.ENAME = vRow.name;
            objBo.MODIFIEDON = vRow.modified == null ? DateTime.Today : DateTime.Parse(vRow.modified.ToString());
            objBo.PARTICULARS = vRow.Type;
            objBo.STATUS = vRow.STATUS;
            objBo.CHANGE_APPROVAL = vRow.change_approval;

            objBo.TEXT = vRow.TEXT;
            objBo.VALUES = vRow.VALUE;

            objBo.MODON = vRow.MODON == null ? DateTime.Today : vRow.MODON;
            objPersonalIDsLst.Add(objBo); 

            //pipersonalidsbo objBo = new pipersonalidsbo();
            //objBo.PERNR = vRow.PERNR.ToString();
            //objBo.ICTYPE = vRow.ICTYPE;
            //objBo.ICNUM = vRow.ICNUM;
            //objBo.COMMENTS = vRow.comments;
            //objBo.DESCRIPTION = vRow.ICTXT;
            //objBo.ISAPPROVED = vRow.isapproved;
            //objPersonalIDsLst.Add(objBo);
        }
        objPIDashBoardDataContext.Dispose();
        return objPersonalIDsLst;
    }
    public piaddressinformationcollectionbo Get_Address_completed_Details_For_Approval_For_Employee(piaddressinformationbo objPIAddrressBo, string sStatus)
    {
        piaddressinformationcollectionbo objPIAddBoLst = new piaddressinformationcollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_pi_get_address_approval_completed_for_employee(objPIAddrressBo.PKEY, sStatus))
        {
            piaddressinformationbo objPIAddBo = new piaddressinformationbo();
            objPIAddBo.ADDRESS_TYPE_ID = vRow.STEXT;
            objPIAddBo.DATE_FROM = Convert.ToDateTime(vRow.BEGDA);
            objPIAddBo.DATE_TO = Convert.ToDateTime(vRow.ENDDA);
            objPIAddBo.ADDRESSL1 = vRow.STRAS;
            objPIAddBo.ADDRESSL2 = vRow.LOCAT;
            objPIAddBo.GBLND = vRow.LANDX;
            objPIAddBo.STATE_ID = vRow.BEZEI;
            objPIAddBo.CITY = vRow.ORT01;
            objPIAddBo.POSTAL_CODE = vRow.PSTLZ;
            objPIAddBo.PHONENO = vRow.TELNR;
            objPIAddBo.PKEY = vRow.pkey;
            objPIAddBo.COMMENTS = vRow.comments.Trim();
            objPIAddBoLst.Add(objPIAddBo);

        }
        objPIDashBoardDataContext.Dispose();
        return objPIAddBoLst;
    }
    public pibankinformationcollectionbo Get_Bank_completed_Details_For_Approval_Employee(pibankinformationbo objBankInfoBo, string sStatus)
    {
        pibankinformationcollectionbo objBankInfoLst = new pibankinformationcollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_pi_get_bank_approval_completed_for_employee(objBankInfoBo.PKEY, sStatus))
        {
            pibankinformationbo objBo = new pibankinformationbo();
            objBo.PKEY = vRow.pkey;
            objBo.PERNR = vRow.PERNR.ToString();
            objBo.BANK_TYPE_ID = vRow.STEXT;//Bank type name
            objBo.PAYEE = vRow.EMFTX;
            objBo.POSTAL_CODE = vRow.BKPLZ;
            objBo.CITY = vRow.BKORT;
            objBo.COUNTRY_NAME = vRow.LANDX;
            objBo.BANK_TYPE_NAME = vRow.BANKA;//Bank name
            objBo.BANK_ACCOUNT = vRow.BANKN;
            objBo.PURPOSE = vRow.ZWECK;
            objBo.PAYMENT_METHOD_NAME = vRow.TEXT1;
            objBo.PAYMENT_CURRENCY_NAME = vRow.LTEXT;
            objBo.COMMENTS = vRow.comments.Trim();
            objBankInfoLst.Add(objBo);
        }
        objPIDashBoardDataContext.Dispose();
        return objBankInfoLst;
    }
    //public leaverequestcollectionbo Get_LeaveRequest_completed_Details_For_Approval(leaverequestbo objLeaveRequestBo)
    //{
    //    leaverequestcollectionbo objLeaveRequestLst = new leaverequestcollectionbo();
    //    foreach (var vRow in objPIDashBoardDataContext.sp_pi_get_leaverequest_approval_completed_for_employee(objLeaveRequestBo.PERNR.ToString(), objLeaveRequestBo.FROM_DATE))
    //    {
    //        leaverequestbo objBo = new leaverequestbo();
    //        objBo.LEAVE_REQ_ID = vRow.LEAVE_REQ_ID;
    //        objBo.AWART = vRow.AWART;
    //        objBo.ATEXT = vRow.ATEXT;
    //        objBo.BEGDA = Convert.ToDateTime(vRow.BEGDA);
    //        objBo.ENDDA = vRow.ENDDA.ToString();
    //        objBo.BEGUZ = vRow.BEGUZ;
    //        objBo.ENDUZ = vRow.ENDUZ;
    //        objBo.STDAZ = (vRow.STDAZ.ToString());
    //        objBo.APPROVED_BY = Convert.ToInt32(vRow.APPROVED_BY);
    //        objBo.NOTE = vRow.NOTE;
    //        objBo.PKEY = vRow.PKEY;
    //        objBo.COMMENTS = vRow.comments.Trim();
    //        objLeaveRequestLst.Add(objBo);
    //    }
    //    objPIDashBoardDataContext.Dispose();
    //    return objLeaveRequestLst;
    //}
    public wtrecordworkingtimecollectionbo Get_RecordDetails_For_Approval_Employee(wtrecordworkingtimebo objRecordBo)
    {
        wtrecordworkingtimecollectionbo objRecordLst = new wtrecordworkingtimecollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_pi_get_recordworking_approval_for_employee(objRecordBo.PKEY, objRecordBo.COMMENTS, objRecordBo.ISAPPROVED))
        {
            //wtrecordworkingtimebo objBo = new wtrecordworkingtimebo();
            //objBo.PKEY = vRow.PKEY;
            //objBo.AWART = vRow.AWART.Trim();
            //objBo.SUNDAY = vRow.Sunday;
            //objBo.MONDAY = vRow.Monday;
            //objBo.TUESDAY = vRow.Tuesday;
            //objBo.WEDNESDAY = vRow.Wednesday;
            //objBo.THURSDAY = vRow.Thursday;
            //objBo.FRIDAY = vRow.Friday;
            //objBo.SATURDAY = vRow.Saturday;
            //objBo.COMMENTS = vRow.comments;
            //objBo.ISAPPROVED = Convert.ToBoolean(vRow.isapproved);
            //objBo.KTEXT = vRow.KTEXT;
            //objBo.LTEXT = vRow.LTEXT;
            //objRecordLst.Add(objBo);

            wtrecordworkingtimebo objBo = new wtrecordworkingtimebo();
            objBo.PKEY = vRow.PKEY;
            objBo.AWART = vRow.AWART.Trim();
            objBo.SUNDAY = vRow.Sunday;
            objBo.MONDAY = vRow.Monday;
            objBo.TUESDAY = vRow.Tuesday;
            objBo.WEDNESDAY = vRow.Wednesday;
            objBo.THURSDAY = vRow.Thursday;
            objBo.FRIDAY = vRow.Friday;
            objBo.SATURDAY = vRow.Saturday;
            objBo.COMMENTS = vRow.comments;
            objBo.ISAPPROVED = Convert.ToBoolean(vRow.isapproved);
            objBo.LTEXT = vRow.Activity;
            objBo.KTEXT = vRow.WBS;
            objBo.ARBST = vRow.KTEXT;
            objBo.REMARKS = vRow.REMARKS;
            objRecordLst.Add(objBo);
        }
        objPIDashBoardDataContext.Dispose();
        return objRecordLst;
    }
    public piaddressinformationcollectionbo Get_Address_Details_For_Approval(piaddressinformationbo objPIAddrressBo, int flag)
    {
        piaddressinformationcollectionbo objPIAddBoLst = new piaddressinformationcollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_ms_get_address_approval_for_employee(objPIAddrressBo.PKEY, objPIAddrressBo.ID, objPIAddrressBo.STATUS, flag))
        {
            piaddressinformationbo objPIAddBo = new piaddressinformationbo();
            objPIAddBo.PKEY = vRow.PKEY;
            objPIAddBo.ID = vRow.ID == null ? 0 : vRow.ID;
            objPIAddBo.CREATED_BY = vRow.PERNR;
            objPIAddBo.ENAME = vRow.name;
            objPIAddBo.MODIFIEDON = vRow.modified == null ? DateTime.Today : vRow.modified;
            objPIAddBo.PARTICULARS = vRow.Type;
            objPIAddBo.STATUS = vRow.STATUS;
            objPIAddBo.CHANGE_APPROVAL = vRow.change_approval;

            objPIAddBo.MODON = vRow.MODON == null ? DateTime.Today : vRow.MODON;




            //objPIAddBo.Slno = vRow.Slno.ToString();
            objPIAddBo.TEXT = vRow.TEXT;
            objPIAddBo.VALUES = vRow.VALUE;

            objPIAddBoLst.Add(objPIAddBo);

        }
        objPIDashBoardDataContext.Dispose();
        return objPIAddBoLst;
    }
    public picommunicationinformationcollectionbo Get_Communication_Details_For_Approval(picommunicationinformationbo objCommInfoBo , int flag)
    {
        picommunicationinformationcollectionbo objComInfoLst = new picommunicationinformationcollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_ms_get_communication_approval_for_employee(objCommInfoBo.PKEY, objCommInfoBo.ID, objCommInfoBo.STATUS, flag))
        {


            picommunicationinformationbo objBo = new picommunicationinformationbo();
            objBo.PKEY = vRow.PKEY;
            objBo.ID = vRow.ID == null ? 0 : vRow.ID;
            objBo.CREATED_BY = vRow.PERNR;
            objBo.ENAME = vRow.name;
            objBo.MODIFIEDON = vRow.modified == null ? DateTime.Today : DateTime.Parse(vRow.modified.ToString());
            objBo.PARTICULARS = vRow.Type;
            objBo.STATUS = vRow.STATUS;
            objBo.CHANGE_APPROVAL = vRow.change_approval;

            objBo.TEXT = vRow.TEXT;
            objBo.VALUES = vRow.VALUE;

            objBo.MODON = vRow.MODON == null ? DateTime.Today : vRow.MODON;

            objComInfoLst.Add(objBo);
        }
            objPIDashBoardDataContext.Dispose();
        return objComInfoLst;

        ////    ////picommunicationinformationbo objBo = new picommunicationinformationbo();
        ////    ////if (vRow.USRTY == "0002")
        ////    ////{
        ////    ////    objBo.EMPLOYEE_ID = vRow.PERNR.ToString();
        ////    ////    objBo.BUILDING_NO = vRow.USRID;
        ////    ////    objBo.USRTY = vRow.USRTY;
        ////    ////    objBo.COMMENTS = vRow.comments;
        ////    ////    objBo.ISAPPROVED = Convert.ToBoolean(vRow.isapproved);
        ////    ////}
        ////    ////else if (vRow.USRTY == "0003")
        ////    ////{
        ////    ////    objBo.EMPLOYEE_ID = vRow.PERNR.ToString();
        ////    ////    objBo.ROOM_NO = vRow.USRID;
        ////    ////    objBo.USRTY = vRow.USRTY;
        ////    ////    objBo.COMMENTS = vRow.comments;
        ////    ////    objBo.ISAPPROVED = Convert.ToBoolean(vRow.isapproved);

        ////    ////}
        ////    ////else if (vRow.USRTY == "0004")
        ////    ////{
        ////    ////    objBo.EMPLOYEE_ID = vRow.PERNR.ToString();
        ////    ////    objBo.EXTENSION = vRow.USRID;
        ////    ////    objBo.USRTY = vRow.USRTY;
        ////    ////    objBo.COMMENTS = vRow.comments;
        ////    ////    objBo.ISAPPROVED = Convert.ToBoolean(vRow.isapproved);

        ////    ////}
        ////    ////else if (vRow.USRTY == "0010")
        ////    ////{
        ////    ////    objBo.EMPLOYEE_ID = vRow.PERNR.ToString();
        ////    ////    objBo.EMAIL = vRow.USRID;
        ////    ////    objBo.USRTY = vRow.USRTY;
        ////    ////    objBo.COMMENTS = vRow.comments;
        ////    ////    objBo.ISAPPROVED = Convert.ToBoolean(vRow.isapproved);

        ////    ////}
        ////    ////else if (vRow.USRTY == "0006")
        ////    ////{
        ////    ////    objBo.EMPLOYEE_ID = vRow.PERNR.ToString();
        ////    ////    objBo.LICENCE_NO = vRow.USRID;
        ////    ////    objBo.USRTY = vRow.USRTY;
        ////    ////    objBo.COMMENTS = vRow.comments;
        ////    ////    objBo.ISAPPROVED = Convert.ToBoolean(vRow.isapproved);
        ////    ////}
        ////    ////else if (vRow.USRTY == "MPHN")
        ////    ////{
        ////    ////    objBo.EMPLOYEE_ID = vRow.PERNR.ToString();
        ////    ////    objBo.MPHN_ID = vRow.USRID;
        ////    ////    objBo.MPHN = vRow.USRTY;
        ////    ////    objBo.COMMENTS = vRow.comments;
        ////    ////    objBo.ISAPPROVED = Convert.ToBoolean(vRow.isapproved);
        ////    ////}
        ////    ////objComInfoLst.Add(objBo);
        ////}
        
    }

 


    public personaldatacollectionbo Get_PersonalData_Details_For_Approval(personaldatabo objPersonaldataBo, int flag)
    {
        personaldatacollectionbo objPersonaldataList = new personaldatacollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_ms_get_personaldata_approval_for_employee(objPersonaldataBo.PKEY, objPersonaldataBo.ID, objPersonaldataBo.STATUS, flag))
        {
            personaldatabo objBo = new personaldatabo();

            objBo.PKEY = vRow.PKEY;
            objBo.ID = vRow.ID == null ? 0 : vRow.ID;
            objBo.CREATED_BY = vRow.PERNR;
            objBo.ENAME = vRow.name;
            objBo.MODIFIEDON = vRow.modified == null ? DateTime.Today : DateTime.Parse(vRow.modified.ToString());
            objBo.PARTICULARS = vRow.Type;
            objBo.STATUS = vRow.STATUS;
            objBo.CHANGE_APPROVAL = vRow.change_approval;

            objBo.TEXT = vRow.TEXT;
            objBo.VALUES = vRow.VALUE;

            objBo.MODON = vRow.MODON == null ? DateTime.Today : vRow.MODON;

            objPersonaldataList.Add(objBo);

        }
        return objPersonaldataList;
    }


    public pifamilymemberscollectionbo Get_FamilyMemberDetails_For_Approval(pifamilymembersbo objFamilyBo, int flag)
    {
        pifamilymemberscollectionbo objFamilyTypeLst = new pifamilymemberscollectionbo();

        foreach (var vRow in objPIDashBoardDataContext.sp_ms_get_familymember_approval_for_employee(objFamilyBo.PKEY, objFamilyBo.ID, objFamilyBo.STATUS, flag))
        {
            pifamilymembersbo objBo = new pifamilymembersbo();

            objBo.PKEY = vRow.PKEY;
            objBo.ID = vRow.ID == null ? 0 : vRow.ID;
            objBo.CREATED_BY = vRow.PERNR;
            objBo.ENAME = vRow.name;
            objBo.MODIFIEDON = vRow.modified == null ? DateTime.Today : DateTime.Parse(vRow.modified.ToString());
            objBo.PARTICULARS = vRow.Type;
            objBo.STATUS = vRow.STATUS;
            objBo.CHANGE_APPROVAL = vRow.change_approval;

            objBo.TEXT = vRow.TEXT;
            objBo.VALUES = vRow.VALUE;
            objBo.MODON = vRow.MODON == null ? DateTime.Today : vRow.MODON;

            objFamilyTypeLst.Add(objBo);

        }
       
        objPIDashBoardDataContext.Dispose();
        return objFamilyTypeLst;
    }

    public piaddressinformationcollectionbo Get_Address_completed_Details_For_Approval(piaddressinformationbo objPIAddrressBo, string sts, DateTime dtLateActDate, DateTime ModifiedDate)
    {
        piaddressinformationcollectionbo objPIAddBoLst = new piaddressinformationcollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_ms_get_address_approval_completed_for_employee(objPIAddrressBo.ID, objPIAddrressBo.PKEY, sts, dtLateActDate, ModifiedDate))
        {
            piaddressinformationbo objPIAddBo = new piaddressinformationbo();

            objPIAddBo.TEXT = vRow.TEXT;
            objPIAddBo.VALUES = vRow.VALUE;

         
            objPIAddBoLst.Add(objPIAddBo);
        }
        objPIDashBoardDataContext.Dispose();
        return objPIAddBoLst;
    }

    public picommunicationinformationcollectionbo Get_Communication_completed_Details_For_Approval(picommunicationinformationbo objCommInfoBo, string sts, DateTime dtLateActDate, DateTime ModifiedDate)
    {
        picommunicationinformationcollectionbo objComInfoLst = new picommunicationinformationcollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_ms_get_Communication_approval_completed_for_employee(objCommInfoBo.ID, objCommInfoBo.PKEY, sts, dtLateActDate, ModifiedDate))
        {
            picommunicationinformationbo objBo = new picommunicationinformationbo();

            objBo.TEXT = vRow.TEXT;
            objBo.VALUES = vRow.VALUE;


            objComInfoLst.Add(objBo);
        }
        objPIDashBoardDataContext.Dispose();
        return objComInfoLst;
    }

    public pifamilymemberscollectionbo Get_Family_completed_Details_For_Approval(pifamilymembersbo objCommInfoBo, string sts, DateTime dtLateActDate, DateTime ModifiedDate)
    {
        pifamilymemberscollectionbo objFamInfoLst = new pifamilymemberscollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_ms_get_Family_approval_completed_for_employee(objCommInfoBo.ID, objCommInfoBo.PKEY, sts, dtLateActDate, ModifiedDate))
        {
            pifamilymembersbo objBo = new pifamilymembersbo();

            objBo.TEXT = vRow.TEXT;
            objBo.VALUES = vRow.VALUE;


            objFamInfoLst.Add(objBo);
        }
        objPIDashBoardDataContext.Dispose();
        return objFamInfoLst;
    }

    public personaldatacollectionbo Get_PersonalData_completed_Details_For_Approval(personaldatabo objPDBo, string sts, DateTime dtLateActDate, DateTime ModifiedDate)
    {
        personaldatacollectionbo objPDLst = new personaldatacollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_ms_get_PersonalData_approval_completed_for_employee(objPDBo.ID, objPDBo.PKEY, sts, dtLateActDate, ModifiedDate))
        {
            personaldatabo objBo = new personaldatabo();

            objBo.TEXT = vRow.TEXT;
            objBo.VALUES = vRow.VALUE;


            objPDLst.Add(objBo);
        }
        objPIDashBoardDataContext.Dispose();
        return objPDLst;
    }


    public pipersonalidscollectionbo Get_PersonalIDS_completed_Details_For_Approval(pipersonalidsbo objPDBo, string sts, DateTime dtLateActDate, DateTime ModifiedDate)
    {
        pipersonalidscollectionbo objPILst = new pipersonalidscollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_ms_get_PersonalIDS_approval_completed_for_employee(objPDBo.ID, objPDBo.PKEY, sts, dtLateActDate, ModifiedDate))
        {
            pipersonalidsbo objBo = new pipersonalidsbo();

            objBo.TEXT = vRow.TEXT;
            objBo.VALUES = vRow.VALUE;


            objPILst.Add(objBo);
        }
        objPIDashBoardDataContext.Dispose();
        return objPILst;
    }
    
    

    public pibankinformationcollectionbo Get_Bank_completed_Details_For_Approval(pibankinformationbo objBankInfoBo)
    {
        pibankinformationcollectionbo objBankInfoLst = new pibankinformationcollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_ms_get_bank_approval_completed_for_employee(objBankInfoBo.PKEY, objBankInfoBo.APPROVED_BY))
        {
            pibankinformationbo objBo = new pibankinformationbo();
            objBo.PKEY = vRow.pkey;
            objBo.PERNR = vRow.PERNR.ToString();
            objBo.BANK_TYPE_ID = vRow.STEXT;//Bank type name
            objBo.PAYEE = vRow.EMFTX;
            objBo.POSTAL_CODE = vRow.BKPLZ;
            objBo.CITY = vRow.BKORT;
            objBo.COUNTRY_NAME = vRow.LANDX;
            objBo.BANK_TYPE_NAME = vRow.BANKA;//Bank name
            objBo.BANK_ACCOUNT = vRow.BANKN;
            objBo.PURPOSE = vRow.ZWECK;
            objBo.PAYMENT_METHOD_NAME = vRow.TEXT1;
            objBo.PAYMENT_CURRENCY_NAME = vRow.LTEXT;
            objBo.COMMENTS = vRow.comments.Trim();
            objBankInfoLst.Add(objBo);
        }
        objPIDashBoardDataContext.Dispose();
        return objBankInfoLst;
    }
    public pibankinformationcollectionbo Get_Bank_Details_For_Approval(pibankinformationbo objBankInfoBo)
    {
        pibankinformationcollectionbo objBankInfoLst = new pibankinformationcollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_ms_get_bank_approval_for_employee(objBankInfoBo.PKEY, objBankInfoBo.APPROVED_BY))
        {
            pibankinformationbo objBo = new pibankinformationbo();
            objBo.PKEY = vRow.pkey;
            objBo.PERNR = vRow.PERNR.ToString();
            objBo.BANK_TYPE_ID = vRow.STEXT;//Bank type name
            objBo.PAYEE = vRow.EMFTX;
            objBo.POSTAL_CODE = vRow.BKPLZ;
            objBo.CITY = vRow.BKORT;
            objBo.COUNTRY_NAME = vRow.LANDX;
            objBo.BANK_TYPE_NAME = vRow.BANKA;//Bank name
            objBo.BANK_ACCOUNT = vRow.BANKN;
            objBo.PURPOSE = vRow.ZWECK;
            objBo.PAYMENT_METHOD_NAME = vRow.TEXT1;
            objBo.PAYMENT_CURRENCY_NAME = vRow.LTEXT;
            objBo.COMMENTS = vRow.comments.Trim();
            objBo.ISAPPROVED = Convert.ToBoolean(vRow.isapproved);
            objBankInfoLst.Add(objBo);
        }
        objPIDashBoardDataContext.Dispose();
        return objBankInfoLst;
    }


    public wtclockinoutcorrectioncollectionbo Get_ClockIO_Details_For_Approval(wtclockinoutcorrectionbo objClockIOBo)
    {
        wtclockinoutcorrectioncollectionbo obClockIOLst = new wtclockinoutcorrectioncollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_ms_get_clockinout_approval_for_employee(objClockIOBo.PKEY, objClockIOBo.ISAPPROVED, objClockIOBo.APPROVEDBY))
        {
            wtclockinoutcorrectionbo objBo = new wtclockinoutcorrectionbo();
            objBo.PKEY = vRow.PKEY;
            objBo.LTIME = vRow.LTIME;
            objBo.LDATE = Convert.ToDateTime(vRow.LDATE);
            objBo.SATZA = vRow.SATZA;
            objBo.SATZA_TYPE = vRow.ZTEXT;
            objBo.NOTE = vRow.NOTE;
            objBo.APPROVEDBY = vRow.APPROVED_BY.ToString();
            objBo.COMMENTS = vRow.comments;
            objBo.ENTRY_STATUS = vRow.entrystatus;
            objBo.ISAPPROVED = Convert.ToBoolean(vRow.isapproved);
            obClockIOLst.Add(objBo);
        }
        objPIDashBoardDataContext.Dispose();
        return obClockIOLst;
    }

  

    public wtrecordworkingtimecollectionbo Get_RecordDetails_For_Approval(string cc,wtrecordworkingtimebo objRecordBo)
    {
        wtrecordworkingtimecollectionbo objRecordLst = new wtrecordworkingtimecollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_ms_get_recordworking_approval_for_employee(objRecordBo.PKEY,cc, objRecordBo.ISAPPROVED, objRecordBo.APPROVEDBY))
        {
            //wtrecordworkingtimebo objBo = new wtrecordworkingtimebo();
            //objBo.PKEY = vRow.PKEY;
            //objBo.AWART = vRow.AWART.Trim();
            //objBo.SUNDAY = vRow.Sunday;
            //objBo.MONDAY = vRow.Monday;
            //objBo.TUESDAY = vRow.Tuesday;
            //objBo.WEDNESDAY = vRow.Wednesday;
            //objBo.THURSDAY = vRow.Thursday;
            //objBo.FRIDAY = vRow.Friday;
            //objBo.SATURDAY = vRow.Saturday;
            //objBo.COMMENTS = vRow.comments;
            //objBo.ISAPPROVED = Convert.ToBoolean(vRow.isapproved);
            //objBo.LTEXT = vRow.LTEXT;
            //objBo.KTEXT = vRow.KTEXT;
            //objRecordLst.Add(objBo);


            wtrecordworkingtimebo objBo = new wtrecordworkingtimebo();
            objBo.PKEY = vRow.PKEY;
            objBo.AWART = vRow.AWART.Trim();
            objBo.SUNDAY = vRow.Sunday;
            objBo.MONDAY = vRow.Monday;
            objBo.TUESDAY = vRow.Tuesday;
            objBo.WEDNESDAY = vRow.Wednesday;
            objBo.THURSDAY = vRow.Thursday;
            objBo.FRIDAY = vRow.Friday;
            objBo.SATURDAY = vRow.Saturday;
            objBo.COMMENTS = vRow.comments;
            objBo.ISAPPROVED = Convert.ToBoolean(vRow.isapproved);
            objBo.LTEXT = vRow.Activity;
            objBo.KTEXT =   vRow.WBS;
            objBo.ARBST = vRow.KTEXT;
            objBo.REMARKS = vRow.REMARKS;
            objRecordLst.Add(objBo);
        }
        objPIDashBoardDataContext.Dispose();
        return objRecordLst;
    }
 
    public leaverequestcollectionbo Get_LeaveRequest_Details_For_Approval(leaverequestbo objLeaveRequestBo, string Tbltyp)
    {
        leaverequestcollectionbo objLeaveRequestLst = new leaverequestcollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_ms_get_leaverequest_approval_for_employee(objLeaveRequestBo.LEAVE_REQ_ID, objLeaveRequestBo.PKEY.ToString(), objLeaveRequestBo.APPROVED_BY_NAME, Tbltyp))
        {
            leaverequestbo objBo = new leaverequestbo();
            //objBo.LEAVE_REQ_ID = Guid.Parse(vRow.LEAVE_REQ_ID.ToString());
            //objBo.AWART = vRow.AWART;
            //objBo.ATEXT = vRow.ATEXT;
            //objBo.BEGDA = Convert.ToDateTime(vRow.BEGDA);
            //objBo.ENDDA = Convert.ToDateTime(vRow.ENDDA);
            //objBo.BEGUZ = vRow.BEGUZ;
            //objBo.ENDUZ = vRow.ENDUZ;
            //objBo.STDAZ = vRow.STDAZ.ToString();
            //objBo.APPROVED_BY_NAME = vRow.ENAME;//Convert.ToInt32(vRow.APPROVED_BY);
            //objBo.NOTE = vRow.NOTE;
            //objBo.PKEY = vRow.PKEY;
            //objBo.COMMENTS = vRow.comments;

            objBo.Slno = vRow.Slno.ToString();
            objBo.TEXT = vRow.TEXT;
            objBo.VALUES = vRow.VALUE;

            objLeaveRequestLst.Add(objBo);
        }
        objPIDashBoardDataContext.Dispose();
        return objLeaveRequestLst;
    }

    public leaveencashmentcollection Get_LeaveEncashment_Details_For_Approval(leaveencashmentcolumnsbo objLEBo)
    {
        leaveencashmentcollection objList = new leaveencashmentcollection();
        foreach (var vrow in objPIDashBoardDataContext.sp_ms_get_leave_encashment_approval_for_employee(objLEBo.REFERENCE_ID, "false"))
        {
            leaveencashmentcolumnsbo objBo = new leaveencashmentcolumnsbo();
            objBo.LEAVE_TYPE_DESC = vrow.ATEXT;
            objBo.DAYS_OR_HOURS = vrow.NUMBR;
            objBo.REFERENCE_ID = vrow.reference_id;
            objBo.EMPLOYEE_NAME = vrow.ENAME;
            objBo.EMPLOYEE_MAIL = vrow.USRID;

            objList.Add(objBo);
        }
        objPIDashBoardDataContext.Dispose();
        return objList;
    }

    public leaveencashmentcollection Get_LeaveEncashment_Completed_Details_For_Approval(leaveencashmentcolumnsbo objLEBo)
    {
        leaveencashmentcollection objList = new leaveencashmentcollection();
        foreach (var vrow in objPIDashBoardDataContext.sp_ms_get_leave_encashment_completed_approval_for_employee(objLEBo.REFERENCE_ID, objLEBo.APPROVED_BY))
        {
            leaveencashmentcolumnsbo objBo = new leaveencashmentcolumnsbo();
            objBo.LEAVE_TYPE_DESC = vrow.ATEXT;
            objBo.DAYS_OR_HOURS = Decimal.Parse(vrow.NUMBER.ToString());
            objBo.REFERENCE_ID = Int32.Parse(vrow.reference_id.ToString());
            objBo.CURRENT_STATUS = vrow.remarks;

            objList.Add(objBo);
        }
        objPIDashBoardDataContext.Dispose();
        return objList;
    }

    public wtrecordworkingtimecollectionbo Get_RecordDetails_Date(wtrecordworkingtimebo objRecordBo)
    {
        wtrecordworkingtimecollectionbo objRecordLst = new wtrecordworkingtimecollectionbo();
        foreach (var vRow in objPIDashBoardDataContext.sp_wt_get_recordworking_time_date(objRecordBo.PKEY,objRecordBo.COMMENTS))
        {
            wtrecordworkingtimebo objBo = new wtrecordworkingtimebo();
            objBo.FROM_DATE = Convert.ToDateTime(vRow.Fromdate);
            objRecordLst.Add(objBo);
        }

        return objRecordLst;
    }

    public travelrequestcolumnscollectionbo Get_TravelRequest_Details_For_Approval(travelrequestcolumnsbo objTRBo)
    {
        travelrequestcolumnscollectionbo objList = new travelrequestcolumnscollectionbo();
        try
        {
            foreach (var vrow in objPIDashBoardDataContext.sp_ms_get_travel_request_approval_for_employee(objTRBo.TRAVEL_REQUEST_NO, "false"))
            {
                travelrequestcolumnsbo objBo = new travelrequestcolumnsbo();
                objBo.TRAVEL_REQUEST_NO = Int64.Parse(vrow.REINR.ToString());
                objBo.TRAVEL_START_DATE = Convert.ToDateTime(vrow.DATE_BEG.ToString());
                objBo.TRAVEL_END_DATE = Convert.ToDateTime(vrow.DATE_END.ToString());
                if (vrow.VORSC != null)
                {
                    objBo.ADVANCE_AMOUNT = double.Parse(vrow.VORSC.ToString());
                }
                else
                {
                    objBo.ADVANCE_AMOUNT = 0;
                }
                if (vrow.DATVS != null)
                {
                    objBo.REQUIRED_BY_DATE = Convert.ToDateTime(vrow.DATVS.ToString());
                }
                else
                {
                    objBo.REQUIRED_BY_DATE = Convert.ToDateTime("01/01/0001");
                }
                if (vrow.ESTIMATED_COST != null)
                {
                    objBo.ESTIMATED_COST = double.Parse(vrow.ESTIMATED_COST.ToString());
                }
                else
                {
                    objBo.ESTIMATED_COST = 0;
                }
                objBo.COMMENTS = vrow.TEXT.ToString();
                objBo.TRANS_START_COUNTRY_LIST = vrow.ENAME;
                objBo.TRANS_END_COUNTRY_LIST = vrow.USRID;

                objList.Add(objBo);
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        objPIDashBoardDataContext.Dispose();
        return objList;
    }

    public travelrequestcolumnscollectionbo Get_TravelRequest_Completed_Details_For_Approval(travelrequestcolumnsbo objTRBo)
    {
        travelrequestcolumnscollectionbo objList = new travelrequestcolumnscollectionbo();
        foreach (var vrow in objPIDashBoardDataContext.sp_ms_get_travel_request_completed_approval_for_employee(objTRBo.TRAVEL_REQUEST_NO, objTRBo.APPROVED_BY))
        {
            travelrequestcolumnsbo objBo = new travelrequestcolumnsbo();
            objBo.TRAVEL_REQUEST_NO = Int64.Parse(vrow.reference_id.ToString());
            objBo.TRAVEL_START_DATE = Convert.ToDateTime(vrow.beg_date.ToString());
            objBo.TRAVEL_END_DATE = Convert.ToDateTime(vrow.end_date.ToString());
            if (vrow.advance != null)
            {
                objBo.ADVANCE_AMOUNT = double.Parse(vrow.advance.ToString());
            }
            else
            {
                objBo.ADVANCE_AMOUNT = 0;
            }
            if (vrow.req_date != null)
            {
                objBo.REQUIRED_BY_DATE = Convert.ToDateTime(vrow.req_date.ToString());
            }
            else
            {
                objBo.REQUIRED_BY_DATE = Convert.ToDateTime("01/01/0001");
            }
            if (vrow.estimated_cost != null)
            {
                objBo.ESTIMATED_COST = double.Parse(vrow.estimated_cost.ToString());
            }
            else
            {
                objBo.ESTIMATED_COST = 0;
            }
            objBo.COMMENTS = vrow.comments.ToString();
            objBo.TRAVEL_REMARKS = vrow.remarks;

            objList.Add(objBo);
        }
        objPIDashBoardDataContext.Dispose();
        return objList;
    }
}