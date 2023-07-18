using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.PR;


public class prbl
{
    prdbmlDataContext objPRDataContext = new prdbmlDataContext();
    prcollectionbo PRBo = new prcollectionbo();

    //public int getPurchaseID(prbo obj1)
    //{
    //    prcollectionbo objPIAddBoLst = new prcollectionbo();
    //    //int iResultCode = objPRDataContext.sp_pr_getPurchaseID();
    //  foreach (var vRow in objPRDataContext.sp_pr_getPurchaseID())
    //    {
    //        vRow.PurchaseID;
    //  }
    //    return iResultCode;
    //}


    public prcollectionbo getPurchaseID(ref Int32? PRID)
    {
        prcollectionbo objPRAddBoLst = new prcollectionbo();
        foreach (var vRow in objPRDataContext.sp_pr_getPurchaseIDnew(ref PRID))
        {
            prbo objBo = new prbo();
            objBo.PRID = vRow.PurchaseID;

            objPRAddBoLst.Add(objBo);
        }
        return objPRAddBoLst;
    }

    public List<prbo> getIndentorNames()
    {
        List<prbo> prboList = new List<prbo>();

        prdbmlDataContext objcontext = new prdbmlDataContext();
        foreach (var vRow in objcontext.sp_load_Indentor())
        {
            prbo objPRAddBo = new prbo();
            objPRAddBo.PERNR = vRow.PERNR;

            objPRAddBo.ENAME = vRow.ENAME;

            prboList.Add(objPRAddBo);
        }
        return prboList;

    }
    public prcollectionbo Load_employeeName(string sEmployeeId)
    {
        prdbmlDataContext objcontext = new prdbmlDataContext();
        prcollectionbo objPrList = new prcollectionbo();
        foreach (var vRow in objcontext.sp_Get_EmployeeName(sEmployeeId))
        {
            prbo objBo = new prbo();

            objBo.ENAME = vRow.ENAME;
            // objBo.WERKS = vRow.WERKS;
            //objBo.PERSK = vRow.PERSK;
            objPrList.Add(objBo);
        }
        objcontext.Dispose();
        return objPrList;
    }


    public void Create_PR_Request(prbo PRBo, ref int? PR_ID, ref bool? PrReqStatus)
    {
        try
        {
            prdbmlDataContext objcontext = new prdbmlDataContext();
            prcollectionbo objPrList = new prcollectionbo();
            objcontext.sp_pr_create_request(0, PRBo.IPERNR, PRBo.RPERNR, PRBo.PFUNC_AREA, PRBo.BTEXT, PRBo.MIS_GRPC, PRBo.MIS_GRPA, PRBo.MIS_GRPB
                , PRBo.EKGRP, PRBo.BWERKS, PRBo.SWERKS, PRBo.SUG_SUPP, PRBo.SUP_ADDRESS, PRBo.SUP_PHONE, PRBo.IN_BUDGET, PRBo.CAPITALIZED, PRBo.CAP_TEXT, PRBo.SERVICE_BUREA, PRBo.CRITICALITY
                , PRBo.PSPNR, PRBo.VERNR, PRBo.BILLABLE, PRBo.PROPOSAL, PRBo.PFID, PRBo.PFPATH, PRBo.AGREEMENT, PRBo.AFID, PRBo.AFPATH, PRBo.EMAIL_COM, PRBo.EFID, PRBo.EFPATH
                , PRBo.INVOICE, PRBo.IFID, PRBo.IFPATH, PRBo.SPART, PRBo.JUSTIFICATION, PRBo.SPL_NOTES, PRBo.CREATED_ON1, PRBo.CREATEDBY, PRBo.APP_ON1, PRBo.APPROVEDBY1
                , PRBo.HOLD_ON1, PRBo.RELEASED_ON1, PRBo.COMMENTS1, PRBo.APP_ON2, PRBo.APPROVEDBY2, PRBo.HOLD_ON2, PRBo.RELEASED_ON2, PRBo.COMMENTS2
                , PRBo.APP_ON3, PRBo.APPROVEDBY3, PRBo.HOLD_ON3, PRBo.RELEASED_ON3, PRBo.COMMENTS3
                , PRBo.APP_ON4, PRBo.APPROVEDBY4, PRBo.HOLD_ON4, PRBo.RELEASED_ON4, PRBo.COMMENTS4
                , PRBo.APP_ON5, PRBo.APPROVEDBY5, PRBo.HOLD_ON5, PRBo.RELEASED_ON5, PRBo.COMMENTS5
                , PRBo.APP_ON6, PRBo.APPROVEDBY6, PRBo.HOLD_ON6, PRBo.RELEASED_ON6, PRBo.COMMENTS6, PRBo.STATUS, PRBo.REGIONID, ref PR_ID, ref PrReqStatus);

            objcontext.Dispose();
        }
        catch (Exception Ex)
        { throw Ex; }
    }

    public void Create_PR_add_items(prbo PRBo, string Action, ref bool? PrReqStatus)
    {
        try
        {
            prdbmlDataContext objcontext = new prdbmlDataContext();
            prcollectionbo objPrList = new prcollectionbo();
            objcontext.sp_pr_add_items(PRBo.id, PRBo.BANFN_EXT, PRBo.BNFPO, PRBo.MATNR, PRBo.TXZ01, PRBo.PART_NO, PRBo.MTART, PRBo.MEINS
                , PRBo.NO_OF_UNITS, PRBo.UNIT_PRICE, PRBo.WAERS, PRBo.TAXABLE, PRBo.ITEM_NOTE, Action, ref PrReqStatus);

            objcontext.Dispose();
        }
        catch (Exception Ex)
        { throw Ex; }
    }


    public List<prbo> Load_PRDetails(string APPROVER_NO, string EmployeeName)
    {
        prdbmlDataContext objTravelRequestDataContext = new prdbmlDataContext();
        List<prbo> requisitionboList = new List<prbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_Get_PR_Requested(APPROVER_NO, EmployeeName))
        {
            prbo requisitionboObj = new prbo();
            requisitionboObj.PRID = vRow.BANFN_EXT;
            requisitionboObj.SUG_SUPP = vRow.SUG_SUPPLIER;
            requisitionboObj.IN_BUDGET = vRow.IN_BUDGET;
            requisitionboObj.CRITICALITY = vRow.CRITICALITY;
            requisitionboObj.BNFPO = vRow.BNFPO;
            requisitionboObj.UNIT_PRICE = vRow.Total.ToString();
            requisitionboObj.CREATED_ON1 = vRow.CREATED_ON;
            requisitionboObj.IPERNR = vRow.I_PERNR + " - " + vRow.ename1;
            requisitionboObj.RPERNR = vRow.R_PERNR + " - " + vRow.ename2;
            requisitionboObj.MIS_GRPC = vRow.CID;
            requisitionboObj.MIS_GRPA = vRow.MIS_GROUP_A;
            requisitionboObj.MIS_GRPB = vRow.MIS_GROUP_B;
            requisitionboObj.BWERKS = vRow.B_WERKS;
            requisitionboObj.SWERKS = vRow.S_WERKS;
            requisitionboObj.CAPITALIZED = vRow.CAPITALIZED;
            requisitionboObj.CAP_TEXT = vRow.CAP_TEXT;
            requisitionboObj.CREATEDBY = vRow.CREATED_BY;
            requisitionboObj.STATUS = vRow.STATUS;
            requisitionboObj.INRCURR = vRow.InCurr;
            requisitionboObj.WAERS = vRow.WAERS;
            requisitionboObj.TAINRAmt = decimal.Parse(vRow.TotalINRamt.ToString()).ToString("0.000");
            requisitionboObj.PSPNR = vRow.PSPNR;

            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }

    public List<prbo> Load_PRDetails_AllCurrentLastmonth(string APPROVER_NO, string EmployeeName,string month)
    {
        prdbmlDataContext objTravelRequestDataContext = new prdbmlDataContext();
        List<prbo> requisitionboList = new List<prbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_Get_PR_Requested_month(APPROVER_NO, EmployeeName, month))
        {
            prbo requisitionboObj = new prbo();
            requisitionboObj.PRID = vRow.BANFN_EXT;
            requisitionboObj.SUG_SUPP = vRow.SUG_SUPPLIER;
            requisitionboObj.IN_BUDGET = vRow.IN_BUDGET;
            requisitionboObj.CRITICALITY = vRow.CRITICALITY;
            requisitionboObj.BNFPO = vRow.BNFPO;
            requisitionboObj.UNIT_PRICE = vRow.Total.ToString();
            requisitionboObj.CREATED_ON1 = vRow.CREATED_ON;
            requisitionboObj.IPERNR = vRow.I_PERNR + " - " + vRow.ename1;
            requisitionboObj.RPERNR = vRow.R_PERNR + " - " + vRow.ename2;
            requisitionboObj.MIS_GRPC = vRow.CID;
            requisitionboObj.MIS_GRPA = vRow.MIS_GROUP_A;
            requisitionboObj.MIS_GRPB = vRow.MIS_GROUP_B;
            requisitionboObj.BWERKS = vRow.B_WERKS;
            requisitionboObj.SWERKS = vRow.S_WERKS;
            requisitionboObj.CAPITALIZED = vRow.CAPITALIZED;
            requisitionboObj.CAP_TEXT = vRow.CAP_TEXT;
            requisitionboObj.CREATEDBY = vRow.CREATED_BY;
            requisitionboObj.STATUS = vRow.STATUS;
            requisitionboObj.INRCURR = vRow.InCurr;
            requisitionboObj.WAERS = vRow.WAERS;
            requisitionboObj.TAINRAmt = decimal.Parse(vRow.TotalINRamt.ToString()).ToString("0.000");
            requisitionboObj.PSPNR = vRow.PSPNR;

            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }

    public List<prbo> Load_PRDetailsAllApproveRej(string APPROVER_NO, string EmployeeName)
    {
        prdbmlDataContext objTravelRequestDataContext = new prdbmlDataContext();
        List<prbo> requisitionboList = new List<prbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_Get_PR_Requested_allAppRej(APPROVER_NO, EmployeeName))
        {
            prbo requisitionboObj = new prbo();
            requisitionboObj.PRID = vRow.BANFN_EXT;
            requisitionboObj.SUG_SUPP = vRow.SUG_SUPPLIER;
            requisitionboObj.IN_BUDGET = vRow.IN_BUDGET;
            requisitionboObj.CRITICALITY = vRow.CRITICALITY;
            requisitionboObj.BNFPO = vRow.BNFPO;
            requisitionboObj.UNIT_PRICE = vRow.Total.ToString();
            requisitionboObj.CREATED_ON1 = vRow.CREATED_ON;
            requisitionboObj.IPERNR = vRow.I_PERNR + " - " + vRow.ename1;
            requisitionboObj.RPERNR = vRow.R_PERNR + " - " + vRow.ename2;
            requisitionboObj.MIS_GRPC = vRow.CID;
            requisitionboObj.MIS_GRPA = vRow.MIS_GROUP_A;
            requisitionboObj.MIS_GRPB = vRow.MIS_GROUP_B;
            requisitionboObj.BWERKS = vRow.B_WERKS;
            requisitionboObj.SWERKS = vRow.S_WERKS;
            requisitionboObj.CAPITALIZED = vRow.CAPITALIZED;
            requisitionboObj.CAP_TEXT = vRow.CAP_TEXT;
            requisitionboObj.CREATEDBY = vRow.CREATED_BY;
            requisitionboObj.STATUS = vRow.STATUS;
            requisitionboObj.INRCURR = vRow.InCurr;
            requisitionboObj.WAERS = vRow.WAERS;
            requisitionboObj.TAINRAmt = decimal.Parse(vRow.TotalINRamt.ToString()).ToString("0.000");
            requisitionboObj.PSPNR = vRow.PSPNR;

            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }

    public List<prbo> Load_PRDetailsAllApproveRejMC(string APPROVER_NO, string EmployeeName)
    {
        prdbmlDataContext objTravelRequestDataContext = new prdbmlDataContext();
        List<prbo> requisitionboList = new List<prbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_Get_PR_Requested_App_Completed(APPROVER_NO, EmployeeName))
        {
            prbo requisitionboObj = new prbo();
            requisitionboObj.PRID = vRow.BANFN_EXT;
            requisitionboObj.SUG_SUPP = vRow.SUG_SUPPLIER;
            requisitionboObj.IN_BUDGET = vRow.IN_BUDGET;
            requisitionboObj.CRITICALITY = vRow.CRITICALITY;
            requisitionboObj.BNFPO = vRow.BNFPO;
            requisitionboObj.UNIT_PRICE = vRow.Total.ToString();
            requisitionboObj.CREATED_ON1 = vRow.CREATED_ON;
            requisitionboObj.IPERNR = vRow.ename1; //vRow.I_PERNR + " - " + vRow.ename1;
            requisitionboObj.RPERNR = vRow.ename2; //vRow.R_PERNR + " - " + vRow.ename2;
            requisitionboObj.MIS_GRPC = vRow.CID;
            requisitionboObj.MIS_GRPA = vRow.MIS_GROUP_A;
            requisitionboObj.MIS_GRPB = vRow.MIS_GROUP_B;
            requisitionboObj.BWERKS = vRow.B_WERKS;
            requisitionboObj.SWERKS = vRow.S_WERKS;
            requisitionboObj.CAPITALIZED = vRow.CAPITALIZED;
            requisitionboObj.CAP_TEXT = vRow.CAP_TEXT;
            requisitionboObj.CREATEDBY = vRow.CREATED_BY;
            requisitionboObj.STATUS = vRow.STATUS;
            requisitionboObj.INRCURR = vRow.InCurr;
            requisitionboObj.WAERS = vRow.WAERS;
            requisitionboObj.TAINRAmt = decimal.Parse(vRow.TotalINRamt.ToString()).ToString("0.000");
            requisitionboObj.PSPNR = vRow.PSPNR;

            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }

    public List<prbo> Load_PRDetailsAllApproveRejMC_AllCurrentLastmonth(string APPROVER_NO, string EmployeeName,string month)
    {
        prdbmlDataContext objTravelRequestDataContext = new prdbmlDataContext();
        List<prbo> requisitionboList = new List<prbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_Get_PR_Requested_App_Completed_month(APPROVER_NO, EmployeeName, month))
        {
            prbo requisitionboObj = new prbo();
            requisitionboObj.PRID = vRow.BANFN_EXT;
            requisitionboObj.SUG_SUPP = vRow.SUG_SUPPLIER;
            requisitionboObj.IN_BUDGET = vRow.IN_BUDGET;
            requisitionboObj.CRITICALITY = vRow.CRITICALITY;
            requisitionboObj.BNFPO = vRow.BNFPO;
            requisitionboObj.UNIT_PRICE = vRow.Total.ToString();
            requisitionboObj.CREATED_ON1 = vRow.CREATED_ON;
            requisitionboObj.IPERNR = vRow.I_PERNR + " - " + vRow.ename1;
            requisitionboObj.RPERNR = vRow.R_PERNR + " - " + vRow.ename2;
            requisitionboObj.MIS_GRPC = vRow.CID;
            requisitionboObj.MIS_GRPA = vRow.MIS_GROUP_A;
            requisitionboObj.MIS_GRPB = vRow.MIS_GROUP_B;
            requisitionboObj.BWERKS = vRow.B_WERKS;
            requisitionboObj.SWERKS = vRow.S_WERKS;
            requisitionboObj.CAPITALIZED = vRow.CAPITALIZED;
            requisitionboObj.CAP_TEXT = vRow.CAP_TEXT;
            requisitionboObj.CREATEDBY = vRow.CREATED_BY;
            requisitionboObj.STATUS = vRow.STATUS;
            requisitionboObj.INRCURR = vRow.InCurr;
            requisitionboObj.WAERS = vRow.WAERS;
            requisitionboObj.TAINRAmt = decimal.Parse(vRow.TotalINRamt.ToString()).ToString("0.000");
            requisitionboObj.PSPNR = vRow.PSPNR;

            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }


    public List<prbo> Load_PRItemDetails(int PR_ID)
    {
        prdbmlDataContext objTravelRequestDataContext = new prdbmlDataContext();
        List<prbo> requisitionboList = new List<prbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_Get_PR_ItemsDetails(PR_ID))
        {
            prbo PrboObj = new prbo();

            PrboObj.BANFN_EXT = vRow.BANFN_EXT;
            PrboObj.IPERNR = vRow.I_PERNR;
            PrboObj.RPERNR = vRow.R_PERNR;
            PrboObj.ENAME = vRow.ENAME;
            PrboObj.PFUNC_AREA = vRow.FUNC_AREA;
            PrboObj.BTEXT = vRow.BTEXT;
            PrboObj.MIS_GRPC = vRow.MIS_GROUP_C;
            PrboObj.MIS_GRPA = vRow.MIS_GROUP_A;
            PrboObj.MIS_GRPB = vRow.MIS_GROUP_B;
            PrboObj.EKGRP = vRow.EKGRP;
            PrboObj.BWERKS = vRow.B_WERKS;
            PrboObj.SWERKS = vRow.S_WERKS;
            PrboObj.SUG_SUPP = vRow.SUG_SUPPLIER;
            PrboObj.SUP_ADDRESS = vRow.SUP_ADDRESS;
            PrboObj.SUP_PHONE = vRow.SUP_PHONE;
            PrboObj.IN_BUDGET = vRow.IN_BUDGET;
            PrboObj.CAPITALIZED = vRow.CAPITALIZED;
            PrboObj.SERVICE_BUREA = vRow.SERVICE_BUREAU;
            PrboObj.CRITICALITY = vRow.CRITICALITY;
            PrboObj.PSPNR = vRow.PSPNR;
            PrboObj.VERNR = vRow.VERNR;
            PrboObj.BILLABLE = vRow.BILLABLE;
            PrboObj.PROPOSAL = vRow.PROPOSAL;
            PrboObj.PFID = vRow.PFID;
            PrboObj.PFPATH = vRow.PFPATH == "" ? "NA" : vRow.PFPATH;
            PrboObj.AGREEMENT = vRow.AGREEMENT;
            PrboObj.AFID = vRow.AFID;
            PrboObj.AFPATH = vRow.AFPATH == "" ? "NA" : vRow.AFPATH;
            PrboObj.EMAIL_COM = vRow.EMAIL_COMMN;
            PrboObj.EFID = vRow.EFID;
            PrboObj.EFPATH = vRow.EFPATH == "" ? "NA" : vRow.EFPATH;
            PrboObj.INVOICE = vRow.INVOICE;
            PrboObj.IFID = vRow.IFID;
            PrboObj.IFPATH = vRow.IFPATH == "" ? "NA" : vRow.IFPATH;
            PrboObj.SPART = vRow.SPART;
            PrboObj.JUSTIFICATION = vRow.JUSTIFICATOION;
            PrboObj.SPL_NOTES = vRow.SPECIAL_NOTES;
            PrboObj.APPROVEDBY1 = vRow.APPROVED_BY1;
            PrboObj.APPROVEDBY1N = vRow.ENAME1;
            PrboObj.APP_ON1 = vRow.APPROVED_ON1;
            PrboObj.HOLD_ON1 = vRow.HOLD_ON1;
            PrboObj.RELEASED_ON1 = vRow.RELEASED_ON1;
            PrboObj.COMMENTS1 = vRow.COMMENTS1;
            PrboObj.CREATED_ON1 = DateTime.Parse(vRow.CREATED_ON);
            PrboObj.TOTAL = vRow.Total.ToString();


            PrboObj.APPROVEDBY2 = vRow.APPROVED_BY2;
            PrboObj.APPROVEDBY2N = vRow.ENAME2;
            PrboObj.APP_ON2 = vRow.APPROVED_ON2;
            PrboObj.HOLD_ON2 = vRow.HOLD_ON2;
            PrboObj.RELEASED_ON2 = vRow.RELEASED_ON2;
            PrboObj.COMMENTS2 = vRow.COMMENTS2;

            PrboObj.APPROVEDBY3 = vRow.APPROVED_BY3;
            PrboObj.APPROVEDBY3N = vRow.ENAME3;
            PrboObj.APP_ON3 = vRow.APPROVED_ON3;
            PrboObj.HOLD_ON3 = vRow.HOLD_ON3;
            PrboObj.RELEASED_ON3 = vRow.RELEASED_ON3;
            PrboObj.COMMENTS3 = vRow.COMMENTS3;

            PrboObj.APPROVEDBY4 = vRow.APPROVED_BY4;
            PrboObj.APPROVEDBY4N = vRow.ENAME4;
            PrboObj.APP_ON4 = vRow.APPROVED_ON4;
            PrboObj.HOLD_ON4 = vRow.HOLD_ON4;
            PrboObj.RELEASED_ON4 = vRow.RELEASED_ON4;
            PrboObj.COMMENTS4 = vRow.COMMENTS4;

            PrboObj.APPROVEDBY5 = vRow.APPROVED_BY5;
            PrboObj.APPROVEDBY5N = vRow.ENAME5;
            PrboObj.APP_ON5 = vRow.APPROVED_ON5;
            PrboObj.HOLD_ON5 = vRow.HOLD_ON5;
            PrboObj.RELEASED_ON5 = vRow.RELEASED_ON5;
            PrboObj.COMMENTS5 = vRow.COMMENTS5;

            PrboObj.APPROVEDBY6 = vRow.APPROVED_BY6;
            PrboObj.APPROVEDBY6N = vRow.ENAME6;
            PrboObj.APP_ON6 = vRow.APPROVED_ON6;
            PrboObj.HOLD_ON6 = vRow.HOLD_ON6;
            PrboObj.RELEASED_ON6 = vRow.RELEASED_ON6;
            PrboObj.COMMENTS6 = vRow.COMMENTS6;

            PrboObj.STATUS = vRow.STATUS;
            PrboObj.WAERS = vRow.WARES;
            PrboObj.MISCIDID = vRow.cidid;
            PrboObj.BWERKSID = vRow.bwerksid;
            PrboObj.SWERKSID = vRow.swerskid;
            PrboObj.PSPNRID = vRow.pspnrid;
            PrboObj.SPARTID = vRow.spartid;
            PrboObj.CAP_TEXT = vRow.CAP_TEXT;

            PrboObj.REGIONID = vRow.REGIONID;
            PrboObj.REGIONTXT = vRow.REGION_TEXT;

            PrboObj.CREATEDBY = vRow.CREATED_BY;//Newly added
            requisitionboList.Add(PrboObj);

        }
        return requisitionboList;
    }


    public List<prbo> Load_PRItem(int PR_ID)
    {
        prdbmlDataContext objTravelRequestDataContext = new prdbmlDataContext();
        List<prbo> requisitionboList = new List<prbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_Get_PR_Items(PR_ID))
        {
            prbo PrboObj = new prbo();
            PrboObj.id = vRow.ID;
            PrboObj.IBANFN_EXT = vRow.BANFN_EXT;
            PrboObj.BNFPO = vRow.BNFPO;
            // PrboObj.MATNR = vRow.MATNR;
            //PrboObj.TXZ01 = vRow.TXZ01;
            PrboObj.PART_NO = vRow.PART_NO;
            PrboObj.MTART = vRow.MTART;
            //PrboObj.MAKTX = vRow.MAKTX;
            PrboObj.MEINS = vRow.MEINS;
            PrboObj.NO_OF_UNITS = vRow.NO_OF_UNITS;
            PrboObj.UNIT_PRICE = vRow.UNIT_PRICE;
            PrboObj.WAERS = vRow.WAERS;
            if (vRow.MTART == "Product")
            {
                PrboObj.TXZ01 = vRow.MAKTX;
                PrboObj.MATNR = vRow.MATNR;
            }
            else
            {
                PrboObj.TXZ01 = vRow.TXZ01;
            }
            PrboObj.TAXABLE = vRow.TAXABLE;
            PrboObj.ITEM_NOTE = vRow.ITEM_NOTE;
            //PrboObj.TAXABLE = vRow.ITEM_NOTE;
            PrboObj.RPERNR = vRow.R_PERNR;
            PrboObj.SAKNR = vRow.SAKNR;
            requisitionboList.Add(PrboObj);

        }
        return requisitionboList;
    }

    public void Update_PR_Status(prbo objBo, ref bool? Status)
    {
        try
        {
            prdbmlDataContext objPrDataContext = new prdbmlDataContext();
            objPrDataContext.sp_update_PR_Status(objBo.BANFN_EXT, objBo.APPROVEDBY1, objBo.COMMENTS1, objBo.STATUS, objBo.SAKNR, objBo.BNFPO, ref Status);
            objPrDataContext.Dispose();
        }
        catch (Exception Ex)
        { throw Ex; }
    }

    public void FinanceUpdate_PR_Request(prbo objBo)
    {
        try
        {
            prdbmlDataContext objPrDataContext = new prdbmlDataContext();
            objPrDataContext.sp_FinanceUpdate_PR(objBo.PRID, objBo.CREATEDBY, objBo.MIS_GRPC, objBo.MIS_GRPA, objBo.MIS_GRPB, objBo.BWERKS, objBo.SWERKS, objBo.IN_BUDGET, objBo.CAPITALIZED, objBo.CAP_TEXT);

            objPrDataContext.Dispose();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    public List<prbo> Load_EmpPRDetails(string EmployeeId, string type)
    {
        prdbmlDataContext objPRDataContext = new prdbmlDataContext();
        List<prbo> requisitionboList = new List<prbo>();
        foreach (var vRow in objPRDataContext.sp_Get_EmpPR_Requested(EmployeeId, type))
        {
            prbo requisitionboObj = new prbo();
            requisitionboObj.PRID = vRow.BANFN_EXT;
            requisitionboObj.SUG_SUPP = vRow.SUG_SUPPLIER;
            requisitionboObj.IN_BUDGET = vRow.IN_BUDGET;
            requisitionboObj.CRITICALITY = vRow.CRITICALITY;
            requisitionboObj.PSPNR = vRow.PSPNR;
            requisitionboObj.BNFPO = vRow.BNFPO;
            requisitionboObj.UNIT_PRICE = vRow.Total.ToString();
            requisitionboObj.CREATED_ON1 = vRow.CREATED_ON;
            requisitionboObj.IPERNR = vRow.ename1;//vRow.I_PERNR + " - " + vRow.ename1;
            requisitionboObj.RPERNR = vRow.ename2;//vRow.R_PERNR + " - " + vRow.ename2;
            requisitionboObj.STATUS = vRow.STATUS;
            requisitionboObj.INRCURR = vRow.InCurr;
            requisitionboObj.WAERS = vRow.WAERS;
            requisitionboObj.TAINRAmt = decimal.Parse(vRow.TotalINRamt.ToString()).ToString("0.000");
            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }


    public List<prbo> LoadEmpPRRequestGridView_AllCurrentLastmonth(string EmployeeId, string type,string month)
    {
        prdbmlDataContext objPRDataContext = new prdbmlDataContext();
        List<prbo> requisitionboList = new List<prbo>();
        foreach (var vRow in objPRDataContext.sp_Get_EmpPR_Requested_month(EmployeeId, type, month))
        {
            prbo requisitionboObj = new prbo();
            requisitionboObj.PRID = vRow.BANFN_EXT;
            requisitionboObj.SUG_SUPP = vRow.SUG_SUPPLIER;
            requisitionboObj.IN_BUDGET = vRow.IN_BUDGET;
            requisitionboObj.CRITICALITY = vRow.CRITICALITY;
            requisitionboObj.PSPNR = vRow.PSPNR;
            requisitionboObj.BNFPO = vRow.BNFPO;
            requisitionboObj.UNIT_PRICE = vRow.Total.ToString();
            requisitionboObj.CREATED_ON1 = vRow.CREATED_ON;
            requisitionboObj.IPERNR = vRow.I_PERNR + " - " + vRow.ename1;
            requisitionboObj.RPERNR = vRow.R_PERNR + " - " + vRow.ename2;
            requisitionboObj.STATUS = vRow.STATUS;
            requisitionboObj.INRCURR = vRow.InCurr;
            requisitionboObj.WAERS = vRow.WAERS;
            requisitionboObj.TAINRAmt = vRow.TotalINRamt!=null?(decimal.Parse(vRow.TotalINRamt.ToString()).ToString("0.000")):"0.0";
            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }

    public List<prbo> Load_ParticularEmpPRDetails(string EmployeeId, string SelectedType, string textSearch, DateTime createdon, string type)
    {
        prdbmlDataContext objPRDataContext = new prdbmlDataContext();
        List<prbo> requisitionboList = new List<prbo>();
        foreach (var vRow in objPRDataContext.sp_Get_ParticularPR_Requested(EmployeeId, SelectedType, textSearch, createdon, type))
        {
            prbo requisitionboObj = new prbo();
            requisitionboObj.PRID = vRow.BANFN_EXT;
            requisitionboObj.SUG_SUPP = vRow.SUG_SUPPLIER;
            requisitionboObj.IN_BUDGET = vRow.IN_BUDGET;
            requisitionboObj.CRITICALITY = vRow.CRITICALITY;
            requisitionboObj.PSPNR = vRow.PSPNR;
            requisitionboObj.BNFPO = vRow.BNFPO;
            requisitionboObj.UNIT_PRICE = vRow.Total.ToString();
            requisitionboObj.CREATED_ON1 = vRow.CREATED_ON;
            requisitionboObj.IPERNR = vRow.I_PERNR + " - " + vRow.ename1;
            requisitionboObj.RPERNR = vRow.R_PERNR + " - " + vRow.ename2;
            requisitionboObj.STATUS = vRow.STATUS;
            requisitionboObj.INRCURR = vRow.InCurr;
            requisitionboObj.WAERS = vRow.WAERS;
            requisitionboObj.TAINRAmt = decimal.Parse(vRow.TotalINRamt.ToString()).ToString("0.000");
            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }



    public List<prbo> Load_ParticularMngrPRDetails(string EmployeeId, string SelectedType, string textSearch, DateTime createdon)
    {
        prdbmlDataContext objPRDataContext = new prdbmlDataContext();
        List<prbo> requisitionboList = new List<prbo>();
        foreach (var vRow in objPRDataContext.sp_Get_ManagerParticularPRDetails(EmployeeId, SelectedType, textSearch, createdon))
        {
            prbo requisitionboObj = new prbo();
            requisitionboObj.PRID = vRow.BANFN_EXT;
            requisitionboObj.SUG_SUPP = vRow.SUG_SUPPLIER;
            requisitionboObj.IN_BUDGET = vRow.IN_BUDGET;
            requisitionboObj.CRITICALITY = vRow.CRITICALITY;
            requisitionboObj.PSPNR = vRow.PSPNR;
            requisitionboObj.BNFPO = vRow.BNFPO;
            requisitionboObj.UNIT_PRICE = vRow.Total.ToString();
            requisitionboObj.CREATED_ON1 = vRow.CREATED_ON;
            requisitionboObj.IPERNR = vRow.I_PERNR + " - " + vRow.ename1;
            requisitionboObj.RPERNR = vRow.R_PERNR + " - " + vRow.ename2;
            requisitionboObj.STATUS = vRow.STATUS;
            requisitionboObj.INRCURR = vRow.InCurr;
            requisitionboObj.WAERS = vRow.WAERS;
            requisitionboObj.TAINRAmt = decimal.Parse(vRow.TotalINRamt.ToString()).ToString("0.000");
            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }

    public List<prbo> Load_ManagerParticularEmpPRDetails(string EmployeeId, string SelectedType, string textSearch, DateTime createdon)
    {
        prdbmlDataContext objTravelRequestDataContext = new prdbmlDataContext();
        List<prbo> requisitionboList = new List<prbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_Get_ManagerParticularPR_Requested(EmployeeId, SelectedType, textSearch, createdon))
        {
            prbo requisitionboObj = new prbo();
            requisitionboObj.PRID = vRow.BANFN_EXT;
            requisitionboObj.SUG_SUPP = vRow.SUG_SUPPLIER;
            requisitionboObj.IN_BUDGET = vRow.IN_BUDGET;
            requisitionboObj.CRITICALITY = vRow.CRITICALITY;
            requisitionboObj.BNFPO = vRow.BNFPO;
            requisitionboObj.UNIT_PRICE = vRow.Total.ToString();
            requisitionboObj.CREATED_ON1 = vRow.CREATED_ON;
            requisitionboObj.IPERNR = vRow.I_PERNR + " - " + vRow.ename1;
            requisitionboObj.RPERNR = vRow.R_PERNR + " - " + vRow.ename2;
            requisitionboObj.MIS_GRPC = vRow.CID;
            requisitionboObj.MIS_GRPA = vRow.MIS_GROUP_A;
            requisitionboObj.MIS_GRPB = vRow.MIS_GROUP_B;
            requisitionboObj.BWERKS = vRow.B_WERKS;
            requisitionboObj.SWERKS = vRow.S_WERKS;
            requisitionboObj.CAPITALIZED = vRow.CAPITALIZED;
            requisitionboObj.CAP_TEXT = vRow.CAP_TEXT;
            requisitionboObj.CREATEDBY = vRow.CREATED_BY;
            requisitionboObj.STATUS = vRow.STATUS;
            requisitionboObj.INRCURR = vRow.InCurr;
            requisitionboObj.WAERS = vRow.WAERS;
            requisitionboObj.TAINRAmt = decimal.Parse(vRow.TotalINRamt.ToString()).ToString("0.000");
            requisitionboObj.PSPNR = vRow.PSPNR;

            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }

    //public List<prbo> Load_ParticularEmpPRDetail(string EmployeeId, string SelectedType, string textSearch, DateTime createdon)
    //{
    //    prdbmlDataContext objPRDataContext = new prdbmlDataContext();
    //    List<prbo> requisitionboList = new List<prbo>();
    //    foreach (var vRow in objPRDataContext.sp_Get_ParticularPR_Requested1(EmployeeId, SelectedType, textSearch, createdon))
    //    {
    //        prbo requisitionboObj = new prbo();

    //        requisitionboObj.CREATED_ON1 = vRow.CREATED_ON;
    //        requisitionboList.Add(requisitionboObj);
    //    }
    //    return requisitionboList;
    //}

    public prcollectionbo Get_Requested_PRStatus(prbo objBo)
    {
        prdbmlDataContext objcontext = new prdbmlDataContext();
        prcollectionbo objPrList = new prcollectionbo();
        foreach (var vRow in objcontext.sp_Get_requested_PRStatus(objBo.BANFN_EXT, objBo.APPROVEDBY1, objBo.COMMENTS1, objBo.STATUS))
        {
            prbo objBo1 = new prbo();
            objBo1.RSTATUS = vRow.STATUS;
            objPrList.Add(objBo1);
        }
        objcontext.Dispose();
        return objPrList;
    }

    public List<prbo> Load_PRDetailsAllApproveRejMC_AllCurrentLastmonth_Rpager(string APPROVER_NO, string EmployeeName, string month, int pageindx, int pagesize, ref int? recCnt)
    {
        prdbmlDataContext objTravelRequestDataContext = new prdbmlDataContext();
        List<prbo> requisitionboList = new List<prbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_Get_PR_Requested_App_Completed_month_Rpager(APPROVER_NO, EmployeeName, month, pageindx, pagesize, ref recCnt))
        {
            prbo requisitionboObj = new prbo();
            requisitionboObj.RowNum = vRow.RowNumber;
            requisitionboObj.PRID = vRow.BANFN_EXT;
            requisitionboObj.SUG_SUPP = vRow.SUG_SUPPLIER;
            requisitionboObj.IN_BUDGET = vRow.IN_BUDGET;
            requisitionboObj.CRITICALITY = vRow.CRITICALITY;
            requisitionboObj.BNFPO = vRow.BNFPO;
            requisitionboObj.UNIT_PRICE = vRow.Total.ToString();
            requisitionboObj.CREATED_ON1 = vRow.CREATED_ON;
            requisitionboObj.IPERNR = vRow.ename1; //vRow.I_PERNR + " - " + vRow.ename1;
            requisitionboObj.RPERNR = vRow.ename2; //vRow.R_PERNR + " - " + vRow.ename2;
            requisitionboObj.MIS_GRPC = vRow.CID;
            requisitionboObj.MIS_GRPA = vRow.MIS_GROUP_A;
            requisitionboObj.MIS_GRPB = vRow.MIS_GROUP_B;
            requisitionboObj.BWERKS = vRow.B_WERKS;
            requisitionboObj.SWERKS = vRow.S_WERKS;
            requisitionboObj.CAPITALIZED = vRow.CAPITALIZED;
            requisitionboObj.CAP_TEXT = vRow.CAP_TEXT;
            requisitionboObj.CREATEDBY = vRow.CREATED_BY;
            requisitionboObj.STATUS = vRow.STATUS;
            requisitionboObj.INRCURR = vRow.InCurr;
            requisitionboObj.WAERS = vRow.WAERS;
            requisitionboObj.TAINRAmt = decimal.Parse(vRow.TotalINRamt.ToString()).ToString("0.000");
            requisitionboObj.PSPNR = vRow.PSPNR;

            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }

    public List<prbo> Load_PRDetails_AllCurrentLastmonth_Rpager(string APPROVER_NO, string EmployeeName, string month, int pageindx, int pagesize, ref int? recCnt)
    {
        prdbmlDataContext objTravelRequestDataContext = new prdbmlDataContext();
        List<prbo> requisitionboList = new List<prbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_Get_PR_Requested_month_Rpager(APPROVER_NO, EmployeeName, month, pageindx, pagesize, ref recCnt))
        {
            prbo requisitionboObj = new prbo();
            requisitionboObj.RowNum = vRow.RowNumber;
            requisitionboObj.PRID = vRow.BANFN_EXT;
            requisitionboObj.SUG_SUPP = vRow.SUG_SUPPLIER;
            requisitionboObj.IN_BUDGET = vRow.IN_BUDGET;
            requisitionboObj.CRITICALITY = vRow.CRITICALITY;
            requisitionboObj.BNFPO = vRow.BNFPO;
            requisitionboObj.UNIT_PRICE = vRow.Total.ToString();
            requisitionboObj.CREATED_ON1 = vRow.CREATED_ON;
            requisitionboObj.IPERNR = vRow.ename1; //vRow.I_PERNR + " - " + vRow.ename1;
            requisitionboObj.RPERNR = vRow.ename2; //vRow.R_PERNR + " - " + vRow.ename2;
            requisitionboObj.MIS_GRPC = vRow.CID;
            requisitionboObj.MIS_GRPA = vRow.MIS_GROUP_A;
            requisitionboObj.MIS_GRPB = vRow.MIS_GROUP_B;
            requisitionboObj.BWERKS = vRow.B_WERKS;
            requisitionboObj.SWERKS = vRow.S_WERKS;
            requisitionboObj.CAPITALIZED = vRow.CAPITALIZED;
            requisitionboObj.CAP_TEXT = vRow.CAP_TEXT;
            requisitionboObj.CREATEDBY = vRow.CREATED_BY;
            requisitionboObj.STATUS = vRow.STATUS;
            requisitionboObj.INRCURR = vRow.InCurr;
            requisitionboObj.WAERS = vRow.WAERS;
            requisitionboObj.TAINRAmt = decimal.Parse(vRow.TotalINRamt.ToString()).ToString("0.000");
            requisitionboObj.PSPNR = vRow.PSPNR;

            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }

    public List<prbo> LoadEmpPRRequestGridView_AllCurrentLastmonth_Rpager(string EmployeeId, string type, string month, int pageindx, int pagesize, ref int? recCnt)
    {
        prdbmlDataContext objPRDataContext = new prdbmlDataContext();
        List<prbo> requisitionboList = new List<prbo>();
        foreach (var vRow in objPRDataContext.sp_Get_EmpPR_Requested_month_Rpager(EmployeeId, type, month, pageindx, pagesize, ref recCnt))
        {
            prbo requisitionboObj = new prbo();
            requisitionboObj.RowNum = vRow.RowNumber;
            requisitionboObj.PRID = vRow.BANFN_EXT;
            requisitionboObj.SUG_SUPP = vRow.SUG_SUPPLIER;
            requisitionboObj.IN_BUDGET = vRow.IN_BUDGET;
            requisitionboObj.CRITICALITY = vRow.CRITICALITY;
            requisitionboObj.PSPNR = vRow.PSPNR;
            requisitionboObj.BNFPO = vRow.BNFPO;
            requisitionboObj.UNIT_PRICE = vRow.Total.ToString();
            requisitionboObj.CREATED_ON1 = vRow.CREATED_ON;
            requisitionboObj.IPERNR = vRow.ename1;//vRow.I_PERNR + " - " + vRow.ename1;
            requisitionboObj.RPERNR = vRow.ename2;//vRow.R_PERNR + " - " + vRow.ename2;
            requisitionboObj.STATUS = vRow.STATUS;
            requisitionboObj.INRCURR = vRow.InCurr;
            requisitionboObj.WAERS = vRow.WAERS;
            requisitionboObj.TAINRAmt = vRow.TotalINRamt != null ? (decimal.Parse(vRow.TotalINRamt.ToString()).ToString("0.000")) : "0.0";
            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }
}
