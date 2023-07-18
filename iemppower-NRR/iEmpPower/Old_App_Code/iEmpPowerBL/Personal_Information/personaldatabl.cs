using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Personal_Information;

/// <summary>
/// Summary description for personaldatabl
/// </summary>
public class personaldatabl
{
    public personaldatabl()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    personaldatadalDataContext objPersonalDataContext = new personaldatadalDataContext();

    public personaldatacollectionbo Get_Name_Details(personaldatabo objPersonaldataBo)
    {
        personaldatacollectionbo objPersonaldataList = new personaldatacollectionbo();
        foreach (var vRow in objPersonalDataContext.sp_pi_load_personal_name_data(objPersonaldataBo.ID, objPersonaldataBo.EMPLOYEE_ID))
        {
            personaldatabo objBo = new personaldatabo();
            objBo.ID = vRow.ID;
            objBo.PERNR = vRow.PERNR;
            objBo.TITEL = vRow.TITEL;
           // objBo.PHOTO = "~/EmpImages/EmpImage.jpg";
            objBo.PHOTO = vRow.PHOTO;
            objBo.TITEL_TEXT = vRow.ATEXT;
            objBo.VORNA = vRow.VORNA;
            objBo.NACHN = vRow.NACHN;
            objBo.NAME2 = vRow.NAME2;
            objBo.INITS = vRow.INITS;
            objBo.RUFNM = vRow.RUFNM;
            objBo.SPRSL = vRow.SPRSL;
            objBo.SPTXT = vRow.SPTXT;
            objBo.GESCH = vRow.GESCH; // -- Gender Key
            objBo.Gender = vRow.Gender; // -- Gender
            objBo.GBDAT = DateTime.Parse(vRow.DOB); // -- DOB
            objBo.KONFE = vRow.KONFE; // -- Religion Key
            objBo.KONFE_Txt = vRow.KTEXT;
            objBo.GBORT = vRow.GBORT; // -- PLACE
            objBo.GBLND = vRow.GBLND;
            objBo.LANDX = vRow.LANDX == null ? "" : vRow.LANDX;
            objBo.GBDEP = vRow.GBDEP;
            objBo.BLAND = vRow.BLAND;
            objBo.NATIO = vRow.NATI0;
            objBo.NATIO_Txt = vRow.NATIO;
            objBo.NATI2 = vRow.NATI2;
            objBo.LANDX2 = vRow.LANDX2;
            objBo.NATI3 = vRow.NATI3;
            objBo.LANDX3 = vRow.LANDX3;
            objBo.FAMST = vRow.FAMST;
            objBo.FAMST_Text = vRow.FTEXT;
            objBo.FAMDT = vRow.FAMDT;
            objBo.ANZKD = vRow.ANZKD;
            objBo.STATUS = vRow.STATUS;
            objBo.TRANSSTATUS = vRow.TransStatus == null ? "" : vRow.TransStatus;

            objPersonaldataList.Add(objBo);
        }
        return objPersonaldataList;
    }
    public personaldatacollectionbo Get_HR_Details(personaldatabo objPersonaldataBo)
    {
        personaldatacollectionbo objPersonaldataList = new personaldatacollectionbo();
        foreach (var vRow in objPersonalDataContext.sp_pi_load_personal_hr_data(objPersonaldataBo.EMPLOYEE_ID))
        {
            personaldatabo objBo = new personaldatabo();
            //objBo.PERNR = vRow.p;
            objBo.SPRSL = vRow.SPRSL;
            objBo.GESCH = vRow.GESCH;
            if (vRow.GBDAT != null)
            {
                objBo.GBDAT = Convert.ToDateTime(vRow.GBDAT);
            }

            objBo.GBORT = vRow.GBORT;
            objBo.GBLND = vRow.GBLND;
            objBo.NATIO = vRow.NATI0;
            objBo.GBDEP = vRow.GBDEP;
            objBo.NATI2 = vRow.NATI2;
            objBo.NATI3 = vRow.NATI3;
            objBo.FAMST = vRow.FAMST;

            if (vRow.FAMDT != null)
            {
                objBo.FAMDT = Convert.ToDateTime(vRow.FAMDT);
            }
            objBo.ANZKD = vRow.ANZKD;
            objBo.KITXT = vRow.KONFE;
            objBo.ISUPDATE = bool.Parse(vRow.isupdate.ToString());
            //if (vRow.PHOTO != null)
            //{
            objBo.EMPLOYEE_PHOTOPATH = vRow.PHOTO;//vRow.ImageData.ToArray();
            //}
            //else
            //{
            //    objBo.LOGO = null;
            //}
            objPersonaldataList.Add(objBo);
        }
        return objPersonaldataList;
    }
    public int Update_Personal_Data(personaldatabo objPersonaldataBo, ref bool? SuperVisorstatus, ref bool? HRStatus, ref string SuperVisorPernr,
                                                                                    ref string SuperVisorEmail, ref string HRPernr,
                                                                                    ref string HREmail, ref string PernrEmail, ref string SuperVisorPhn, ref string HRPhn)
    {
        try
        {
            objPersonalDataContext = new personaldatadalDataContext();
            int iResultCode = objPersonalDataContext.sp_pi_update_personal_data(objPersonaldataBo.EMPLOYEE_ID,
                                                                                objPersonaldataBo.TITEL,
                                                                                objPersonaldataBo.VORNA,
                                                                                objPersonaldataBo.NACHN,
                                                                                objPersonaldataBo.NAME2,
                                                                                objPersonaldataBo.INITS,
                                                                                objPersonaldataBo.RUFNM,
                                                                                objPersonaldataBo.SPRSL,
                                                                                objPersonaldataBo.GESCH,
                                                                                objPersonaldataBo.GBDAT,
                                                                                objPersonaldataBo.GBORT,
                                                                                objPersonaldataBo.GBLND,
                                                                                objPersonaldataBo.NATIO,
                                                                                objPersonaldataBo.GBDEP,
                                                                                objPersonaldataBo.NATI2,
                                                                                objPersonaldataBo.NATI3,
                                                                                objPersonaldataBo.FAMST,
                                                                                objPersonaldataBo.FAMDT,
                                                                                objPersonaldataBo.ANZKD,
                                                                                objPersonaldataBo.KITXT,
                                                                                objPersonaldataBo.MODIFIEDON,
                                                                                objPersonaldataBo.MODIFIED_BY, objPersonaldataBo.EMPLOYEE_PHOTOPATH,
                                                                                ref  SuperVisorstatus, ref HRStatus,
                                                                                ref HRPernr, ref SuperVisorPernr,
                                                                                ref HREmail, ref SuperVisorEmail, ref PernrEmail,
                                                                                ref SuperVisorPhn, ref HRPhn);

            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public personaldatacollectionbo CheckValidation_Personal_Data()
    {

        personaldatacollectionbo objPersonalDataLst = new personaldatacollectionbo();
        foreach (var vRow in objPersonalDataContext.sp_conf_checkvalidation_for_personal_data_information())
        {
            personaldatabo objBo = new personaldatabo();

            objBo.DESCRIPTION = vRow.name;
            objBo.VALUE = Convert.ToBoolean(vRow.values);
            objPersonalDataLst.Add(objBo);
        }
        objPersonalDataContext.Dispose();
        return objPersonalDataLst;
    }


    public int Add_Update_Personal_Data(personaldatabo objPersonaldataBo, ref string HRMail, ref string SuperVisorMail
        , ref string PernrName, ref string PernrEMail)
    {
        try
        {
            objPersonalDataContext = new personaldatadalDataContext();
            int iResultCode = objPersonalDataContext.usp_pi_personal_details(objPersonaldataBo.ID
                                                                                , objPersonaldataBo.PERNR
                                                                                , objPersonaldataBo.PHOTO
                                                                                , objPersonaldataBo.TITEL
                                                                                , objPersonaldataBo.VORNA
                                                                                , objPersonaldataBo.NACHN
                                                                                , objPersonaldataBo.NAME2
                                                                                , objPersonaldataBo.INITS
                                                                                , objPersonaldataBo.RUFNM
                                                                                , objPersonaldataBo.SPRSL
                                                                                , objPersonaldataBo.GESCH
                                                                                , objPersonaldataBo.GBDAT
                                                                                , objPersonaldataBo.GBORT
                                                                                , objPersonaldataBo.GBLND
                                                                                , objPersonaldataBo.NATIO
                                                                                , objPersonaldataBo.GBDEP
                                                                                , objPersonaldataBo.NATI2
                                                                                , objPersonaldataBo.NATI3
                                                                                , objPersonaldataBo.FAMST
                                                                                , objPersonaldataBo.FAMDT
                                                                                , objPersonaldataBo.ANZKD
                                                                                , objPersonaldataBo.KONFE
                                                                                , objPersonaldataBo.BEGDA
                                                                                , objPersonaldataBo.ENDDA
                                                                                , objPersonaldataBo.CREATED_BY
                                                                                , objPersonaldataBo.CREATED_ON
                                                                                , objPersonaldataBo.MODIFIED_BY
                                                                                , objPersonaldataBo.MODIFIEDON
                                                                                , objPersonaldataBo.STATUS
                                                                                , objPersonaldataBo.Flag
                                                                                , ref HRMail
                                                                                , ref SuperVisorMail
                                                                                , ref PernrName
                                                                                , ref PernrEMail);

            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }

    public List<personaldatabo> Approval_PDDetails_Mail(string pkey, int Id, string sts) 
    {

        objPersonalDataContext = new personaldatadalDataContext();
        List<personaldatabo> AobjList = new List<personaldatabo>();
        foreach (var vRow in objPersonalDataContext.sp_pi_get_PersonalDatainfo_mail(pkey, Id, sts))
        {
            personaldatabo objBo = new personaldatabo();

            objBo.ID = vRow.id;
            objBo.TITEL_TEXT = vRow.ATEXT;
            objBo.VORNA = vRow.VORNA;
            objBo.NACHN = vRow.NACHN;
            objBo.NAME2 = vRow.NAME2;
            objBo.INITS = vRow.INITS;
            objBo.SPTXT = vRow.SPTXT;
            objBo.Gender = vRow.Gender; // -- Gender
            objBo.GBDAT = DateTime.Parse(vRow.GBDAT.ToString()); // -- DOB
            
            objBo.KONFE_Txt = vRow.KTEXT;
            objBo.GBORT = vRow.GBORT; // -- PLACE
            
            objBo.LANDX = vRow.LANDX;
            objBo.NATIO_Txt = vRow.NATIO;
            objBo.LANDX2 = vRow.LANDX2;
            objBo.LANDX3 = vRow.LANDX3;
            objBo.FAMST_Text = vRow.FTEXT;
            objBo.FAMDT = vRow.FAMDT;
            objBo.ANZKD = vRow.ANZKD;
            objBo.BEZEI = vRow.BEZEI;

            AobjList.Add(objBo);

        }
        return AobjList;
    }

}