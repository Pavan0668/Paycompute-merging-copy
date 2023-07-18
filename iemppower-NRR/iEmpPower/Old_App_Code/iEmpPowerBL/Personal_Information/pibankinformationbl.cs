using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Personal_Information;

/// <summary>
/// Summary description for pibankinformationbl
/// </summary>
public class pibankinformationbl
{
    public pibankinformationbl()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    pibankinformationdalDataContext objBankInfoDataContext = new pibankinformationdalDataContext();
    public int Create_BankInfo(pibankinformationbo objBankInfoBo, ref bool? SuperVisorstatus, ref bool? HRstatus, ref string SuperVisorPernr,
                               ref string SuperVisorMail, ref string HRPernr, ref string HRMail, ref string PernrEMail, ref string SuperVisorPhn, ref string HRPhn)
    {
        try
        {
            objBankInfoDataContext = new pibankinformationdalDataContext();
            int iResultCode = objBankInfoDataContext.sp_pi_create_bank_info(objBankInfoBo.PERNR, objBankInfoBo.BANK_TYPE_ID,
                                                                                   objBankInfoBo.PAYEE, objBankInfoBo.POSTAL_CODE,
                                                                                   objBankInfoBo.CITY,
                                                                                   objBankInfoBo.BANK_COUNTRY, objBankInfoBo.BANK_KEY,
                                                                                   objBankInfoBo.BANK_ACCOUNT, Convert.ToChar(objBankInfoBo.PAYMENT_METHOD),
                                                                                   objBankInfoBo.PURPOSE, objBankInfoBo.PAYMENT_CURRENCY,
                                                                                   objBankInfoBo.CREATED_ON,
                                                                                   objBankInfoBo.CREATED_BY,
                                                                                   objBankInfoBo.ISAPPROVED,
                                                                                   objBankInfoBo.ISACTIVE,
                                                                                   objBankInfoBo.MODIFIEDON,
                                                                                   objBankInfoBo.MODIFIED_BY,
                                                                                   ref  SuperVisorstatus, ref HRstatus,
                                                                                   ref HRPernr, ref SuperVisorPernr,
                                                                                   ref HRMail, ref SuperVisorMail, ref PernrEMail,
                                                                                   ref SuperVisorPhn, ref HRPhn);



            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public int Update_BankInfo(pibankinformationbo objBankInfoBo, ref bool? SuperVisorstatus, ref bool? HRstatus,
                       ref string SuperVisorPernr, ref string SuperVisorMail, ref string HRPernr, ref string HRMail, ref string PernrEMail,
                        ref string SuperVisorPhn, ref string HRPhn)
    {
        try
        {
            objBankInfoDataContext = new pibankinformationdalDataContext();
            int iResultCode = objBankInfoDataContext.sp_pi_update_bank_info(objBankInfoBo.PERNR, objBankInfoBo.BANK_TYPE_ID,
                                                                                   objBankInfoBo.PAYEE, objBankInfoBo.POSTAL_CODE,
                                                                                   objBankInfoBo.CITY,
                                                                                   objBankInfoBo.BANK_COUNTRY, objBankInfoBo.BANK_KEY,
                                                                                   objBankInfoBo.BANK_ACCOUNT, Convert.ToChar(objBankInfoBo.PAYMENT_METHOD),
                                                                                   objBankInfoBo.PURPOSE, objBankInfoBo.PAYMENT_CURRENCY,
                                                                                   objBankInfoBo.OLD_BANK_ACCOUNT_NO,
                                                                                   objBankInfoBo.MODIFIEDON,
                                                                                   objBankInfoBo.MODIFIED_BY, objBankInfoBo.PKEY,
                                                                                   ref  SuperVisorstatus, ref HRstatus,
                                                                                   ref HRPernr, ref SuperVisorPernr,
                                                                                   ref HRMail, ref SuperVisorMail, ref PernrEMail,
                                                                                   ref SuperVisorPhn, ref HRPhn);
            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public pibankinformationcollectionbo Get_Bank_Details(pibankinformationbo objBankInfoBo)
    {
        pibankinformationcollectionbo objBankInfoLst = new pibankinformationcollectionbo();
        foreach (var vRow in objBankInfoDataContext.sp_pi_get_bank_info(objBankInfoBo.PERNR.ToString(), objBankInfoBo.PageIndex, objBankInfoBo.PageSize))
        {
            pibankinformationbo objBo = new pibankinformationbo();
            objBo.RowNumber = (int)vRow.RowNumber;
            objBo.ID = vRow.ID;
            objBo.PKEY = vRow.pkey;
            objBo.BANK_TYPE_NAME = vRow.STEXT;
            objBo.BANK_COUNTRY = vRow.BANKS;
            objBo.BANK_KEY = vRow.BANKL;
            objBo.BANK_ACCOUNT = vRow.BANKN;
            objBo.PAYMENT_CURRENCY = vRow.WAERS;
            objBo.COUNTRY_NAME = vRow.LANDX;
            objBo.PAYMENT_METHOD_NAME = vRow.TEXT1;
            objBo.PAYMENT_CURRENCY_NAME = vRow.LTEXT;
            objBo.RecordCnt = vRow.RecordCnt;   
            objBankInfoLst.Add(objBo);
        }
        return objBankInfoLst;
    }

    public pibankinformationcollectionbo Get_Bank_Details_Full(pibankinformationbo objBankInfoBo)
    {
        pibankinformationcollectionbo objBankInfoLst = new pibankinformationcollectionbo();
        foreach (var vRow in objBankInfoDataContext.sp_pi_get_bank_info_full(objBankInfoBo.PERNR.ToString(), objBankInfoBo.PKEY))
        {
            pibankinformationbo objBo = new pibankinformationbo();
           
            objBo.ID = vRow.ID;
            objBo.PERNR = vRow.PERNR;
            objBo.BANK_TYPE_NAME = vRow.STEXT;
            objBo.PAYEE = vRow.EMFTX;
            objBo.BANK_COUNTRY = vRow.BANKS;
            objBo.CITY = vRow.BKORT;
            objBo.BANK_KEY = vRow.BANKL;
            objBo.BANK_ACCOUNT = vRow.BANKN;
            objBo.PAYMENT_CURRENCY = vRow.WAERS;
            objBo.COUNTRY_NAME = vRow.LANDX;
            objBo.PAYMENT_METHOD_NAME = vRow.TEXT1;
            objBo.PAYMENT_CURRENCY_NAME = vRow.LTEXT;
            objBo.POSTAL_CODE = vRow.BKPLZ;
            objBo.PURPOSE = vRow.ZWECK;
           
            objBankInfoLst.Add(objBo);
        }
        return objBankInfoLst;
    }


    public pibankinformationcollectionbo CheckValidation_BankInfoDetails()
    {

        pibankinformationcollectionbo objBankInfoLst = new pibankinformationcollectionbo();
        foreach (var vRow in objBankInfoDataContext.sp_conf_checkvalidation_for_bank_info())
        {
            pibankinformationbo objBo = new pibankinformationbo();

            objBo.DESCRIPTION = vRow.name;
            objBo.VALUE = Convert.ToBoolean(vRow.values);

            objBankInfoLst.Add(objBo);
        }
        return objBankInfoLst;
    }
}