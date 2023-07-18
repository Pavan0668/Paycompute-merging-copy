using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Personal_Information;

/// <summary>
/// Summary description for addressinformationbl
/// </summary>
public class piaddressinformationbl
{

    public piaddressinformationbl()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    piaddressinformationdalDataContext objPIAddressDataContext = new piaddressinformationdalDataContext();

    public int Create_AddressDetails(piaddressinformationbo objPIAddrressBo, string sTCValue, ref bool? SuperVisorStatus, ref bool? HRStatus, ref string SuperVisorPernr,
                                   ref string SuperVisorMail, ref string HRPernr, ref string HRMail, ref string PernrName, ref string PernrEMail, ref string SuperVisorPhn, ref string HRPhn)
    {


        try
        {
            objPIAddressDataContext = new piaddressinformationdalDataContext();
            int iResultCode = objPIAddressDataContext.sp_pi_create_address_details(
                                                                                   objPIAddrressBo.EMPLOYEE_ID,
                                                                                   objPIAddrressBo.ADDRESS_TYPE_ID,
                                                                                   objPIAddrressBo.DATE_FROM,
                                                                                   objPIAddrressBo.DATE_TO,
                                                                                   objPIAddrressBo.ADDRESSL1,
                                                                                   objPIAddrressBo.ADDRESSL2,
                                                                                   objPIAddrressBo.GBLND,
                                                                                   objPIAddrressBo.STATE_ID,
                                                                                   objPIAddrressBo.CITY,
                                                                                   objPIAddrressBo.POSTAL_CODE,
                                                                                   objPIAddrressBo.PHONENO, sTCValue,
                                                                                   objPIAddrressBo.CREATED_ON,
                                                                                   objPIAddrressBo.CREATED_BY,
                                                                                   objPIAddrressBo.ISAPPROVED,
                                                                                   objPIAddrressBo.ISACTIVE,
                                                                                   objPIAddrressBo.APPROVED_ON,
                                                                                   objPIAddrressBo.APPROVED_BY, objPIAddrressBo.MODIFIEDON,
                                                                                   objPIAddrressBo.MODIFIED_BY, objPIAddrressBo.APPROVED_ON,
                                                                                   objPIAddrressBo.APPROVED_BY, objPIAddrressBo.ISAPPROVED,
                                                                                   objPIAddrressBo.STATUS, ref SuperVisorStatus, ref HRStatus,
                                                                                   ref HRPernr, ref HRPhn,
                                                                                  ref SuperVisorPernr, ref SuperVisorPhn, ref HRMail, ref SuperVisorMail,
                                                                                  ref PernrName, ref PernrEMail);



            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }


    public piaddressinformationcollectionbo Get_Address_Details(piaddressinformationbo objPIAddrressBo)
    {
        piaddressinformationcollectionbo objPIAddBoLst = new piaddressinformationcollectionbo();
        foreach (var vRow in objPIAddressDataContext.sp_pi_get_address_info(objPIAddrressBo.EMPLOYEE_ID, objPIAddrressBo.PageIndex, objPIAddrressBo.PageSize))
        {
            piaddressinformationbo objPIAddBo = new piaddressinformationbo();
            objPIAddBo.RowNumber = (int)vRow.RowNumber;
            objPIAddBo.ID = vRow.ID;
            objPIAddBo.ADDRESS_TYPE_ID = vRow.STEXT;
            objPIAddBo.ADDRESSL1 = vRow.STRAS;
            objPIAddBo.DATE_FROM = Convert.ToDateTime(vRow.BEGDA);
            objPIAddBo.DATE_TO = Convert.ToDateTime(vRow.ENDDA);
            objPIAddBo.RecordCnt = vRow.RecordCnt;
            //objPIAddBo.ISUPDATE = bool.Parse(vRow.isupdate.ToString());
            objPIAddBoLst.Add(objPIAddBo);
        }
        return objPIAddBoLst;
    }

    public piaddressinformationcollectionbo Get_Address_Details_Full(piaddressinformationbo objPIAddrressBo)
    {
        piaddressinformationcollectionbo objPIAddBoLst = new piaddressinformationcollectionbo();
        foreach (var vRow in objPIAddressDataContext.sp_pi_get_address_info_full(objPIAddrressBo.ID, objPIAddrressBo.EMPLOYEE_ID))
        {
            piaddressinformationbo objPIAddBo = new piaddressinformationbo();
            
            objPIAddBo.ID = vRow.ID;
            objPIAddBo.PKEY = vRow.pkey;
            objPIAddBo.EMPLOYEE_ID = vRow.PERNR;
            objPIAddBo.SUBTY = vRow.SUBTY;
            objPIAddBo.ADDRESS_TYPE_TEXT = vRow.STEXT;
            objPIAddBo.DATE_FROM = Convert.ToDateTime(vRow.BEGDA);
            objPIAddBo.DATE_TO = Convert.ToDateTime(vRow.ENDDA);
            objPIAddBo.ADDRESSL1 = vRow.STRAS;
            objPIAddBo.ADDRESSL2 = vRow.LOCAT;
            objPIAddBo.GBLND = vRow.FGBLD;
            objPIAddBo.LANDX = vRow.LANDX;
            objPIAddBo.STATE_ID = vRow.STATE;
            objPIAddBo.BEZEI = vRow.BEZEI;
            objPIAddBo.CITY = vRow.ORT01;
            objPIAddBo.POSTAL_CODE = vRow.PSTLZ;
            objPIAddBo.PHONENO = vRow.TELNR;
            objPIAddBo.STATUS = vRow.STATUS;
            objPIAddBo.TRANSSTATUS = vRow.TransStatus == null ? "" : vRow.TransStatus;
            //objPIAddBo.CREATED_ON = vRow.BEGDA;
            //objPIAddBo.ISUPDATE = bool.Parse(vRow.isupdate.ToString());
            objPIAddBoLst.Add(objPIAddBo);
        }
        return objPIAddBoLst;
    }

    public int Update_AddressDetails(piaddressinformationbo objPIAddrressBo, string sOld_SUBTY, DateTime? dtold_BEGDA, DateTime? dtold_ENDDA, ref bool? SuperVisorStatus, ref bool? HRStatus, ref string SuperVisorPernr,
                                   ref string SuperVisorMail, ref string HRPernr, ref string HRMail, ref string PernrName, ref string PernrEMail,
                                    ref string SuperVisorPhn, ref string HRPhn)
    {


        try
        {
            int iResultCode = objPIAddressDataContext.sp_pi_update_address_details(objPIAddrressBo.EMPLOYEE_ID,
                                                                                objPIAddrressBo.ADDRESS_TYPE_ID,
                                                                                objPIAddrressBo.DATE_FROM,
                                                                                objPIAddrressBo.DATE_TO,
                                                                                objPIAddrressBo.ADDRESSL1,
                                                                                objPIAddrressBo.ADDRESSL2,
                                                                                objPIAddrressBo.GBLND,
                                                                                objPIAddrressBo.STATE_ID,
                                                                                objPIAddrressBo.CITY,
                                                                                objPIAddrressBo.POSTAL_CODE,
                                                                                objPIAddrressBo.PHONENO, sOld_SUBTY, dtold_BEGDA, dtold_ENDDA,
                                                                                objPIAddrressBo.ISAPPROVED,
                                                                                objPIAddrressBo.ISACTIVE,
                                                                                objPIAddrressBo.APPROVED_ON,
                                                                                objPIAddrressBo.APPROVED_BY,
                                                                                objPIAddrressBo.MODIFIEDON,
                                                                                objPIAddrressBo.MODIFIED_BY, objPIAddrressBo.PKEY,
                                                                                ref SuperVisorStatus, ref HRStatus,
                                                                                  ref HRPernr,
                                                                                  ref SuperVisorPernr, ref HRMail, ref SuperVisorMail,
                                                                                  ref PernrName, ref PernrEMail,
                                                                                  ref SuperVisorPhn, ref HRPhn);



            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }

    public piaddressinformationcollectionbo CheckValidation_AddressDetails()
    {
        piaddressinformationcollectionbo objPIAddBoLst = new piaddressinformationcollectionbo();
        foreach (var vRow in objPIAddressDataContext.sp_conf_checkvalidation_for_address_information())
        {
            piaddressinformationbo objBo = new piaddressinformationbo();
            objBo.DESCRIPTION = vRow.name;
            objBo.VALUE = Convert.ToBoolean(vRow.values);

            objPIAddBoLst.Add(objBo);
        }
        return objPIAddBoLst;
    }


    public int Add_Update_Del_AddressDetails(piaddressinformationbo objPIAddrressBo, ref string HRMail, ref string SuperVisorMail
        , ref string PernrName, ref string PernrEMail)
    {
        try
        {
            objPIAddressDataContext = new piaddressinformationdalDataContext();
            int iResultCode = objPIAddressDataContext.usp_pi_address_details(objPIAddrressBo.ID,
                                                                                   objPIAddrressBo.EMPLOYEE_ID,
                                                                                   objPIAddrressBo.ADDRESS_TYPE_ID,
                                                                                   objPIAddrressBo.DATE_FROM,
                                                                                   objPIAddrressBo.DATE_TO,
                                                                                   objPIAddrressBo.ADDRESSL1,
                                                                                   objPIAddrressBo.ADDRESSL2,
                                                                                   objPIAddrressBo.GBLND,
                                                                                   objPIAddrressBo.STATE_ID,
                                                                                   objPIAddrressBo.CITY,
                                                                                   objPIAddrressBo.POSTAL_CODE,
                                                                                   objPIAddrressBo.PHONENO,
                                                                                   objPIAddrressBo.CREATED_BY,
                                                                                   objPIAddrressBo.CREATED_ON,
                                                                                    objPIAddrressBo.MODIFIED_BY,
                                                                                  objPIAddrressBo.MODIFIEDON,
                                                                                   objPIAddrressBo.STATUS,
                                                                                   objPIAddrressBo.Flag
                                                                                   ,ref HRMail
                                                                                   , ref SuperVisorMail
                                                                                   , ref PernrName
                                                                                   ,ref PernrEMail);
            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }

    public List<piaddressinformationbo> Approval_APIDetails_Mail(string pkey,int Id, string sts)
    {
        piaddressinformationdalDataContext objPIAddressDataContext = new piaddressinformationdalDataContext();
        List<piaddressinformationbo> AobjList = new List<piaddressinformationbo>();
        foreach (var vRow in objPIAddressDataContext.sp_pi_get_Addressinfo_mail(pkey, Id, sts))
        {
            piaddressinformationbo PIAObj = new piaddressinformationbo();

            PIAObj.ID = vRow.ID;
            PIAObj.PKEY = vRow.PKEY;
            PIAObj.ADDRESS_TYPE_TEXT = vRow.Address_Type;
            PIAObj.DATE_FROM = Convert.ToDateTime(vRow.Valid_from);
            PIAObj.DATE_TO = Convert.ToDateTime(vRow.Valid_to);
            PIAObj.ADDRESSL1 = vRow.House_number___street;
            PIAObj.ADDRESSL2 = vRow.Address_line_2;
            PIAObj.LANDX = vRow.Country_Name;
            PIAObj.BEZEI = vRow.State_Name;
            PIAObj.CITY = vRow.City;
            PIAObj.POSTAL_CODE = vRow.Postal_Code;
            PIAObj.PHONENO = vRow.Telephone_Number;

            AobjList.Add(PIAObj);

        }
        return AobjList;
    }

}