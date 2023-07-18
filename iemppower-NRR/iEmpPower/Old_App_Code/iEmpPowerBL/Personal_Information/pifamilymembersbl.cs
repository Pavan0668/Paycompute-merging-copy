using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Personal_Information;

/// <summary>
/// Summary description for pifamilymembersbl
/// </summary>
public class pifamilymembersbl
{
    public pifamilymembersbl()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    pifamilymembersdalDataContext objFamilyMemberDataContext = new pifamilymembersdalDataContext();

    public int Create_FamilyMember(pifamilymembersbo objFamilyBo, ref bool? SuperVisorstatus, ref bool? HRstatus,
        ref string SuperVisorPernr, ref string SuperVisorMail, ref string HRPernr, ref string HRMail, ref string PernrEMail, ref string SuperVisorPhn, ref string HRPhn)
    {
        try
        {

            int iResultCode = objFamilyMemberDataContext.sp_pi_create_family_members(objFamilyBo.PENNR, objFamilyBo.FAMSA, objFamilyBo.FAVOR, objFamilyBo.FANAM,
                                                                                     objFamilyBo.FGBNA, objFamilyBo.FINIT, objFamilyBo.FNMZU, objFamilyBo.FVRSW,
                                                                                     Convert.ToChar(objFamilyBo.FASEX), objFamilyBo.FGBDT, objFamilyBo.FGBOT, objFamilyBo.FGBLD,
                                                                                      objFamilyBo.FANAT, objFamilyBo.FANA2, objFamilyBo.FANA3,
                                                                                      objFamilyBo.KDBSL, objFamilyBo.KDBGR, objFamilyBo.KDZUL, objFamilyBo.CREATEDON,
                                                                                      objFamilyBo.CREATED_BY, ref SuperVisorstatus, ref HRstatus,
                                                                                      ref HRPernr, ref SuperVisorPernr, ref HRMail,
                                                                                      ref SuperVisorMail, ref PernrEMail,
                                                                                      ref SuperVisorPhn, ref HRPhn);
            objFamilyMemberDataContext.Dispose();
            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public pifamilymemberscollectionbo Get_FamilyMember_Details(pifamilymembersbo objFamilyBo)
    {
        pifamilymemberscollectionbo objFamilyTypeLst = new pifamilymemberscollectionbo();
        foreach (var vRow in objFamilyMemberDataContext.sp_pi_get_family_members(objFamilyBo.PENNR, objFamilyBo.PageIndex, objFamilyBo.PageSize))
        {
            pifamilymembersbo objBo = new pifamilymembersbo();

            objBo.RowNumber = (int)vRow.RowNumber;
            objBo.ID = vRow.ID;
            objBo.FAMSA = vRow.FAMSA;
            objBo.STEXT = vRow.STEXT;
            objBo.FASEX_Name = vRow.Gender;
            objBo.FAVOR = vRow.FAVOR;
            objBo.FANAM = vRow.FANAM;
            objBo.FGBDT = DateTime.Parse(vRow.FGBDT);
            objBo.AGE = vRow.Age;
            objBo.RecordCnt = vRow.RecordCnt;

            //objBo.FINIT = vRow.FINIT;
            //objBo.FNMZU = vRow.FNMZU;
            //objBo.FVRSW = vRow.FVRSW;
            //objBo.FGBDT = Convert.ToDateTime(vRow.FGBDT);
            //objBo.FGBOT = vRow.FGBOT;
            //objBo.FGBLD = vRow.FGBLD;
            //objBo.FANAT = vRow.FANAT;
            //objBo.FANA2 = vRow.FANA2;
            //objBo.FANA3 = vRow.FANA3;
            //objBo.KDZUL = vRow.KDZUL;
            //objBo.KDBSL = vRow.KDBSL;
            //objBo.KDBGR = vRow.KDBGR;
            //objBo.OBJPS = vRow.OBJPS;
            //objBo.PKEY = vRow.PKEY;
            //objBo.STATUS = vRow.status;
            //objBo.ISUPDATE = vRow.isupdate;
            objFamilyTypeLst.Add(objBo);
        }
        objFamilyMemberDataContext.Dispose();
        return objFamilyTypeLst;
    }

    public pifamilymemberscollectionbo Get_FamilyMember_Details_Full(pifamilymembersbo objFamilyBo)
    {
        pifamilymemberscollectionbo objFamilyTypeLst = new pifamilymemberscollectionbo();
        foreach (var vRow in objFamilyMemberDataContext.sp_pi_get_family_members_full(objFamilyBo.ID, objFamilyBo.PENNR))
        {
            pifamilymembersbo objBo = new pifamilymembersbo();
           
            objBo.ID = vRow.ID;
            objBo.FAMSA = vRow.FAMSA;
            objBo.STEXT = vRow.STEXT;
            objBo.FASEX = vRow.FASEX.ToString();
            objBo.FASEX_Name = vRow.Gender;
            objBo.FAVOR = vRow.FAVOR;
            objBo.FANAM = vRow.FANAM;
            objBo.FGBDT = DateTime.Parse(vRow.FGBDT);
            objBo.KDBSL = vRow.KDBSL;
            objBo.KDBGR = vRow.KDGBR;
            objBo.KDZUL = vRow.KDZUL;
            objBo.STATUS = vRow.STATUS;
            objBo.TRANSSTATUS = vRow.TransStatus == null ? "" : vRow.TransStatus;
           

          
            objFamilyTypeLst.Add(objBo);
        }
        objFamilyMemberDataContext.Dispose();
        return objFamilyTypeLst;
    }
    public int Update_FamilyMember(pifamilymembersbo objFamilyBo, string sOldType, string sOldFirstName, string sOldLastName, ref bool? SuperVisorstatus,
        ref bool? HRstatus, ref string SuperVisorPernr, ref string SuperVisorMail, ref string HRPernr, ref string HRMail, ref string PernrEMail,
        ref string SuperVisorPhn, ref string HRPhn)
    {
        try
        {
            int iResultCode = objFamilyMemberDataContext.sp_pi_update_family_members(objFamilyBo.PENNR, objFamilyBo.FAMSA, objFamilyBo.FAVOR, objFamilyBo.FANAM,
                                                                                     objFamilyBo.FGBNA, objFamilyBo.FINIT, objFamilyBo.FNMZU, objFamilyBo.FVRSW,
                                                                                     Convert.ToChar(objFamilyBo.FASEX), objFamilyBo.FGBDT, objFamilyBo.FGBOT, objFamilyBo.FGBLD,
                                                                                      objFamilyBo.FANAT, objFamilyBo.FANA2, objFamilyBo.FANA3,
                                                                                      objFamilyBo.KDBSL, objFamilyBo.KDBGR, objFamilyBo.KDZUL,
                                                                                      sOldFirstName, sOldType, sOldLastName, objFamilyBo.MODIFIED_BY,
                                                                                      objFamilyBo.MODIFIEDON, objFamilyBo.PKEY, objFamilyBo.OBJPS, ref  SuperVisorstatus,
                                                                                      ref HRstatus,
                                                                                      ref HRPernr, ref SuperVisorPernr,
                                                                                      ref HRMail, ref SuperVisorMail, ref PernrEMail,
                                                                                      ref SuperVisorPhn, ref HRPhn);


            objFamilyMemberDataContext.Dispose();
            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public pifamilymemberscollectionbo CheckValidation_FamilyMemberDetails()
    {
        pifamilymemberscollectionbo objFamilyTypeLst = new pifamilymemberscollectionbo();
        foreach (var vRow in objFamilyMemberDataContext.sp_conf_checkvalidation_for_family_member_information())
        {
            pifamilymembersbo objBo = new pifamilymembersbo();
            objBo.DESCRIPTION = vRow.name;
            objBo.VALUE = Convert.ToBoolean(vRow.values);
            objFamilyTypeLst.Add(objBo);
        }
        objFamilyMemberDataContext.Dispose();
        return objFamilyTypeLst;
    }
    public pifamilymemberscollectionbo Check_FamilyMemberType(pifamilymembersbo objFamilyBo)
    {
        pifamilymemberscollectionbo objFamilyTypeLst = new pifamilymemberscollectionbo();
        foreach (var vRow in objFamilyMemberDataContext.sp_pi_check_family_members(objFamilyBo.PENNR))
        {
            pifamilymembersbo objBo = new pifamilymembersbo();
            objBo.FAMSA = vRow.FAMSA;
            objFamilyTypeLst.Add(objBo);
        }
        objFamilyMemberDataContext.Dispose();
        return objFamilyTypeLst;

    }

    public int Add_Update_Del_FamilyDetails(pifamilymembersbo objFamilyBo, ref string HRMail, ref string SuperVisorMail
        , ref string PernrName, ref string PernrEMail)
    {
        try
        {
            int iResultCode = objFamilyMemberDataContext.usp_pi_family_details(objFamilyBo.ID
                , objFamilyBo.PENNR
                , objFamilyBo.FAMSA
                , objFamilyBo.OBJPS
                , objFamilyBo.FAVOR
                , objFamilyBo.FANAM
                , objFamilyBo.FGBNA
                , objFamilyBo.FINIT
                , objFamilyBo.FNMZU
                , objFamilyBo.FVRSW
                , char.Parse(objFamilyBo.FASEX)
                , objFamilyBo.FGBDT
                , objFamilyBo.FGBOT
                , objFamilyBo.FGBLD
                , objFamilyBo.FANAT
                , objFamilyBo.FANA2
                , objFamilyBo.FANA3
                , objFamilyBo.KDBSL
                , objFamilyBo.KDBGR
                , objFamilyBo.KDZUL
                , objFamilyBo.BEGDA
                , objFamilyBo.ENDDA
                , objFamilyBo.CREATED_BY
                , objFamilyBo.CREATEDON
                , objFamilyBo.MODIFIED_BY
                , objFamilyBo.MODIFIEDON
                , objFamilyBo.STATUS
                , objFamilyBo.Flag
                , ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail);

            objFamilyMemberDataContext.Dispose();
            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }


    public pifamilymemberscollectionbo Get_FamilyMember_Types()
    {
        pifamilymemberscollectionbo objFamilyTypeLst = new pifamilymemberscollectionbo();
        foreach (var vRow in objFamilyMemberDataContext.sp_pi_get_family_members_type())
        {
            pifamilymembersbo objBo = new pifamilymembersbo();
            objBo.SUBTY = vRow.SUBTY;
            objBo.STEXT = vRow.STEXT;
            objFamilyTypeLst.Add(objBo);
        }
        objFamilyMemberDataContext.Dispose();
        return objFamilyTypeLst;
    }



    public List<pifamilymembersbo> Approval_FMDetails_Mail(string pkey, int Id, string sts) 
    {

        pifamilymembersdalDataContext objFamilyMemberDataContext = new pifamilymembersdalDataContext();
        List<pifamilymembersbo> AobjList = new List<pifamilymembersbo>();
        foreach (var vRow in objFamilyMemberDataContext.sp_pi_get_Familyinfo_mail(pkey, Id, sts))
        {
            pifamilymembersbo objBo = new pifamilymembersbo();


            objBo.ID = vRow.ID;
            objBo.STEXT = vRow.STEXT;
            objBo.FASEX_Name = vRow.Gender;
            objBo.FAVOR = vRow.FAVOR;
            objBo.FANAM = vRow.FANAM;
            objBo.FGBDT = DateTime.Parse(vRow.FGBDT.ToString());
            objBo.KDBSL = vRow.KDBSL;
            objBo.KDBGR = vRow.KDGBR;
            objBo.KDZUL = vRow.KDZUL;       

            AobjList.Add(objBo);

        }
        return AobjList;
    }
}