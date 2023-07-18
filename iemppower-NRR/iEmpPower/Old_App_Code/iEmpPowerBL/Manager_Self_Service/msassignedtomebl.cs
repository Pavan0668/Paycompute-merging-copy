using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Manager_Self_Service;

/// <summary>
/// Summary description for msassignedtomebl
/// </summary>
public class msassignedtomebl
{
    public msassignedtomebl()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    msassignedtomedalDataContext objPIAssignTMDataContext = new msassignedtomedalDataContext();

    public msassignedtomecollectionbo Get_HRPERNR(string Pernr,string cc)
    {
        msassignedtomecollectionbo objAssginTMList = new msassignedtomecollectionbo();
        foreach (var vRow in objPIAssignTMDataContext.sp_get_HR_id(Pernr,cc))
        {
            msassignedtomebo objBo = new msassignedtomebo();
            objBo.PERNR = vRow.USRID.ToString();
            objAssginTMList.Add(objBo);
        }
        objPIAssignTMDataContext.Dispose();
        return objAssginTMList;
    }


    public msassignedtomecollectionbo Get_EmployeeDetails(msassignedtomebo objBo1)
    {
        msassignedtomecollectionbo objAssginTMList = new msassignedtomecollectionbo();
        foreach (var vRow in objPIAssignTMDataContext.sp_ms_load_approval_for_employee(objBo1.PERNR, objBo1.COMMENTS))
        {
            msassignedtomebo objBo = new msassignedtomebo();
            objBo.PERNR = vRow.PERNR.ToString();
            objBo.ENAME = vRow.ENAME;
            objBo.PLANS = vRow.PLANS;
            objBo.PLSXT = vRow.PLXST;
            objBo.USRID = vRow.USRID;
            objBo.S_NAME = vRow.S_ENAME;
            objBo.S_PERNR = vRow.S_PERNR.ToString();
            objBo.S_PLSXT = vRow.S_PLXST;
            objBo.S_USRID = vRow.S_USRID;
            objAssginTMList.Add(objBo);
        }
        objPIAssignTMDataContext.Dispose();
        return objAssginTMList;
    }

    public msassignedtomecollectionbo Get_Sub_Employees_Of_Manager(msassignedtomebo objAssginTMBo)
    {
        msassignedtomecollectionbo objAssginTMList = new msassignedtomecollectionbo();
        foreach (var vRow in objPIAssignTMDataContext.sp_ms_get_sub_employees(objAssginTMBo.PERNR, objAssginTMBo.COMMENTS))
        {
            msassignedtomebo objBo = new msassignedtomebo();
            objBo.PERNR = vRow.PERNR.ToString();
            objBo.ENAME = vRow.ENAME;
            objBo.PLSXT = vRow.PLSXT;
            objBo.USRID = vRow.USRID;
            objBo.PHN = vRow.PHN;
            objAssginTMList.Add(objBo);
        }
        objPIAssignTMDataContext.Dispose();
        return objAssginTMList;
    }


    public msassignedtomecollectionbo Get_Manager_HR(msassignedtomebo objAssginHMBo)
    {
        msassignedtomecollectionbo objhrmgrcnt = new msassignedtomecollectionbo();
        foreach (var vRow in objPIAssignTMDataContext.payc_mgr_hr_cnt(objAssginHMBo.PERNR, objAssginHMBo.COMMENTS))
        {
            msassignedtomebo objBo = new msassignedtomebo();
            objBo.PERNR = vRow.PERNR.ToString();
            objBo.ENAME = vRow.ENAME;
            objhrmgrcnt.Add(objBo);
        }
        objPIAssignTMDataContext.Dispose();
        return objhrmgrcnt;
    }

    public msassignedtomecollectionbo Get_Sub_Employees_Of_ManagerForMSS(string cc,msassignedtomebo objAssginTMBo)
    {
        msassignedtomecollectionbo objAssginTMList = new msassignedtomecollectionbo();
        foreach (var vRow in objPIAssignTMDataContext.sp_ms_get_sub_employeesForMSS(objAssginTMBo.PERNR,cc))
        {
            msassignedtomebo objBo = new msassignedtomebo();
            objBo.PERNR = vRow.PERNR.ToString();
            objBo.ENAME = vRow.ENAME;
            objBo.PLSXT = vRow.PLSXT;
            objBo.USRID = vRow.USRID;
            objBo.PHN = vRow.PHN;
            objAssginTMList.Add(objBo);
        }
        objPIAssignTMDataContext.Dispose();
        return objAssginTMList;
    }



    public msassignedtomecollectionbo Get_Sub_Employees_Of_Manager_Details(msassignedtomebo objAssginTMBo)
    {
        msassignedtomecollectionbo objAssginTMList = new msassignedtomecollectionbo();
        foreach (var vRow in objPIAssignTMDataContext.sp_ms_get_sub_employees_Details(objAssginTMBo.PERNR, objAssginTMBo.COMMENTS))
        {
            msassignedtomebo objBo = new msassignedtomebo();
            objBo.PERNR = vRow.PERNR.ToString();
            objBo.ENAME = vRow.ENAME;
            objBo.PLSXT = vRow.PLSXT;
            objBo.USRID = vRow.USRID;
            objBo.PHN = vRow.PHN;
            objAssginTMList.Add(objBo);
        }
        objPIAssignTMDataContext.Dispose();
        return objAssginTMList;
    }


    public msassignedtomecollectionbo Get_AllEmployees_Details()
    {
        msassignedtomecollectionbo objAssginTMList = new msassignedtomecollectionbo();
        foreach (var vRow in objPIAssignTMDataContext.sp_get_PERNR_List())
        {
            msassignedtomebo objBo = new msassignedtomebo();
            objBo.PERNR = vRow.PERNR.ToString();
            objBo.ENAME = vRow.ENAME;
            objAssginTMList.Add(objBo);
        }
        objPIAssignTMDataContext.Dispose();
        return objAssginTMList;
    }

    public msassignedtomecollectionbo Load_Pending_Approvals(string cc,msassignedtomebo objAssginTMBo, string seltype, string Year, ref int? RecCount)
    {
        msassignedtomecollectionbo objAssginTMList = new msassignedtomecollectionbo();
        foreach (var vRow in objPIAssignTMDataContext.sp_ms_load_pending_assigned_to_me(cc,objAssginTMBo.PERNR, objAssginTMBo.PageIndex, objAssginTMBo.PageSize, objAssginTMBo.Flag, seltype, Year.Trim(), ref RecCount))
        {
            msassignedtomebo objBo = new msassignedtomebo();

            objBo.RowNumber = vRow.RowNumber;
            objBo.PKEY = vRow.pkey.ToString();
            objBo.PERNR = vRow.PERNR.ToString();
            objBo.ENAME = vRow.ENAME;
            objBo.PLSXT = vRow.PLSXT;
            objBo.REVIEW = vRow.Status;
            objBo.CHANGE_APPROVAL = vRow.change_approval;
            objBo.LAST_ACTIVITY_DATE = Convert.ToDateTime(vRow.Last_Activity_Date);
            objBo.ID = int.Parse(vRow.ID.ToString());
            objBo.TableTyp = vRow.TableTyp;
            objBo.Subtype = vRow.Subtype;
            objBo.MMODON = Convert.ToDateTime(vRow.MODON);
            objAssginTMList.Add(objBo);
        }
        objPIAssignTMDataContext.Dispose();
        return objAssginTMList;
    }

    public msassignedtomecollectionbo Load_Pending_Approvals_dashb(string cc, msassignedtomebo objAssginTMBo, string seltype, string Year, ref int? RecCount,int? sortid)
    {
        msassignedtomecollectionbo objAssginTMList = new msassignedtomecollectionbo();
        foreach (var vRow in objPIAssignTMDataContext.sp_ms_load_pending_assigned_to_me_dashboard(cc, objAssginTMBo.PERNR, objAssginTMBo.PageIndex, objAssginTMBo.PageSize, objAssginTMBo.Flag, seltype, Year.Trim(), ref RecCount, sortid))
        {
            msassignedtomebo objBo = new msassignedtomebo();

            objBo.RowNumber = vRow.RowNumber;
            objBo.PKEY = vRow.pkey.ToString();
            objBo.PERNR = vRow.PERNR.ToString();
            objBo.ENAME = vRow.ENAME;
            objBo.PLSXT = vRow.PLSXT;
            objBo.REVIEW = vRow.Status;
            objBo.CHANGE_APPROVAL = vRow.change_approval;
            objBo.LAST_ACTIVITY_DATE = Convert.ToDateTime(vRow.Last_Activity_Date);
            objBo.ID = int.Parse(vRow.ID.ToString());
            objBo.TableTyp = vRow.TableTyp;
            objBo.Subtype = vRow.Subtype;
            objBo.MMODON = Convert.ToDateTime(vRow.MODON);
            objAssginTMList.Add(objBo);
        }
        objPIAssignTMDataContext.Dispose();
        return objAssginTMList;
    }

    public msassignedtomecollectionbo Load_Completed_Approvals_dashb(string cc, msassignedtomebo objAssginTMBo, string seltype, string Year, ref int? RecCount,int? sortid)
    {
        msassignedtomecollectionbo objAssginTMList = new msassignedtomecollectionbo();
        foreach (var vRow in objPIAssignTMDataContext.sp_ms_load_completed_assigned_to_me_dashboard(cc, objAssginTMBo.PERNR, objAssginTMBo.PageIndex, objAssginTMBo.PageSize, objAssginTMBo.Flag, seltype, Year.Trim(), ref RecCount, sortid))
        {
            msassignedtomebo objBo = new msassignedtomebo();
            objBo.RowNumber = vRow.RowNumber;
            objBo.PKEY = vRow.pkey.ToString();
            objBo.PERNR = vRow.PERNR.ToString();
            objBo.ENAME = vRow.ENAME;
            objBo.PLSXT = vRow.PLSXT;
            //objBo.USRID = vRow.USRID;
            objBo.REVIEW = vRow.Status;
            objBo.CHANGE_APPROVAL = vRow.change_approval;
            objBo.LAST_ACTIVITY_DATE = Convert.ToDateTime(vRow.Last_Activity_Date);

            objBo.MODIFIEDON = Convert.ToDateTime(vRow.ModifiedOn);
            objBo.ID = int.Parse(vRow.ID.ToString());
            objBo.TableTyp = vRow.TableTyp;
            objBo.AppByName = vRow.AppvdBy.ToString();
            objBo.Subtype = vRow.Subtype;
            //objBo.PHN = vRow.USRID_Ph.Trim() == null ? "" : vRow.USRID_Ph.Trim();
            objAssginTMList.Add(objBo);
        }
        objPIAssignTMDataContext.Dispose();
        return objAssginTMList;
    }


    public msassignedtomecollectionbo Load_Completed_Approvals(string cc,msassignedtomebo objAssginTMBo, string seltype, string Year, ref int? RecCount)
    {
        msassignedtomecollectionbo objAssginTMList = new msassignedtomecollectionbo();
        foreach (var vRow in objPIAssignTMDataContext.sp_ms_load_completed_assigned_to_me(cc,objAssginTMBo.PERNR, objAssginTMBo.PageIndex, objAssginTMBo.PageSize, objAssginTMBo.Flag, seltype, Year.Trim(), ref RecCount))
        {
            msassignedtomebo objBo = new msassignedtomebo();
            objBo.RowNumber = vRow.RowNumber;
            objBo.PKEY = vRow.pkey.ToString();
            objBo.PERNR = vRow.PERNR.ToString();
            objBo.ENAME = vRow.ENAME;
            objBo.PLSXT = vRow.PLSXT;
            //objBo.USRID = vRow.USRID;
            objBo.REVIEW = vRow.Status;
            objBo.CHANGE_APPROVAL = vRow.change_approval;
            objBo.LAST_ACTIVITY_DATE = Convert.ToDateTime(vRow.Last_Activity_Date);

            objBo.MODIFIEDON = Convert.ToDateTime(vRow.ModifiedOn);
            objBo.ID = int.Parse(vRow.ID.ToString());
            objBo.TableTyp = vRow.TableTyp;
            objBo.AppByName = vRow.AppvdBy.ToString();
            objBo.Subtype = vRow.Subtype;
            //objBo.PHN = vRow.USRID_Ph.Trim() == null ? "" : vRow.USRID_Ph.Trim();
            objAssginTMList.Add(objBo);
        }
        objPIAssignTMDataContext.Dispose();
        return objAssginTMList;
    }

    public void Approval_AddressDetails(msassignedtomebo objAssginTMBo, ref string strApproverName, ref string strApprovalMail, ref string strReqesterName, ref string strReqesterMail)
    {
        try
        {
            objPIAssignTMDataContext.sp_ms_approve_address_details(objAssginTMBo.ID,
                                                                     objAssginTMBo.APPROVED_BY, objAssginTMBo.Approver_Comment,
                                                                     objAssginTMBo.Flag, objAssginTMBo.MODIFIEDON, objAssginTMBo.MODON, ref strApproverName,
                                                                                                 ref strApprovalMail, ref strReqesterName, ref strReqesterMail);
            objPIAssignTMDataContext.Dispose();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }



    public void Approval_CommunticationInfoDetails(msassignedtomebo objAssginTMBo, ref string strApproverName, ref string strApprovalMail, ref string strReqesterName, ref string strReqesterMail)
    {
        try
        {
            objPIAssignTMDataContext.sp_ms_approve_communication_details(objAssginTMBo.ID,
                                                                    objAssginTMBo.APPROVED_BY, objAssginTMBo.Approver_Comment,
                                                                    objAssginTMBo.Flag, objAssginTMBo.MODIFIEDON, objAssginTMBo.MODON, ref strApproverName,
                                                                                                ref strApprovalMail, ref strReqesterName, ref strReqesterMail);
            objPIAssignTMDataContext.Dispose();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }



    public void Approval_PIDetails(msassignedtomebo objAssginTMBo, ref string strApproverName, ref string strApprovalMail, ref string strReqesterName, ref string strReqesterMail)
    {
        try
        {
            objPIAssignTMDataContext.sp_ms_approve_personal_id_details(objAssginTMBo.ID,
                                                                   objAssginTMBo.APPROVED_BY, objAssginTMBo.Approver_Comment,
                                                                   objAssginTMBo.Flag, objAssginTMBo.MODIFIEDON, objAssginTMBo.MODON, ref strApproverName,
                                                                                               ref strApprovalMail, ref strReqesterName, ref strReqesterMail);
            objPIAssignTMDataContext.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }


    public void Approval_FamilykDetails(msassignedtomebo objAssginTMBo, ref string strApproverName, ref string strApprovalMail, ref string strReqesterName, ref string strReqesterMail)
    {
        try
        {
            objPIAssignTMDataContext.sp_ms_approve_familymember_details(objAssginTMBo.ID,
                                                                    objAssginTMBo.APPROVED_BY, objAssginTMBo.Approver_Comment,
                                                                    objAssginTMBo.Flag, objAssginTMBo.MODIFIEDON, objAssginTMBo.MODON, ref strApproverName,
                                                                                                ref strApprovalMail, ref strReqesterName, ref strReqesterMail);
            objPIAssignTMDataContext.Dispose();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }


    public int Approval_PDInfoDetails(msassignedtomebo objAssginTMBo, ref string strApproverName, ref string strApprovalMail, ref string strReqesterName, ref string strReqesterMail)
    {
        try
        {
            int iResultCode = objPIAssignTMDataContext.sp_ms_approve_personal_data_details(objAssginTMBo.ID,
                                                                    objAssginTMBo.APPROVED_BY, objAssginTMBo.Approver_Comment,
                                                                    objAssginTMBo.Flag, objAssginTMBo.MODIFIEDON, objAssginTMBo.MODON, ref strApproverName,
                                                                    ref strApprovalMail, ref strReqesterName, ref strReqesterMail);
            objPIAssignTMDataContext.Dispose();
            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }



    //-------------------------- APPROVE / REJECT LEAVE REQUEST ----------------------------- BEGIN --------------------------------
    public void Mngr_Leave_Req_Approve_Reject(string cc,msassignedtomebo objAssginTMBo, ref string HR_Email, ref string Supervisor_name
        , ref string Supervisor_Email, ref string PERNR_Name, ref string PERNR_Email)
    {
        try
        {
            msassignedtomecollectionbo objAssginTMList = new msassignedtomecollectionbo();
             objPIAssignTMDataContext.usp_ms_approve_reject(
               cc, objAssginTMBo.ID
                 , objAssginTMBo.PKEY
                , objAssginTMBo.PERNR
                , objAssginTMBo.APPROVED_BY
                , objAssginTMBo.Approver_Comment
                , objAssginTMBo.Flag
                , objAssginTMBo.TableTyp
                , ref HR_Email
                , ref Supervisor_name
                , ref Supervisor_Email
                , ref PERNR_Name
                , ref PERNR_Email);
        }
        catch (Exception Ex)
        { throw Ex; }
        finally { objPIAssignTMDataContext.Dispose(); }

    }

    //-------------------------


    public int Approval_BankDetails(msassignedtomebo objAssginTMBo, ref string strApprover, ref string strApprovalMail, ref string strReqesterMail)
    {
        try
        {
            int iResultCode = objPIAssignTMDataContext.sp_ms_approve_bank_details(objAssginTMBo.PKEY,
                                                                    objAssginTMBo.APPROVED_BY, objAssginTMBo.COMMENTS,
                                                                    objAssginTMBo.APPROVED_ON, objAssginTMBo.STATUS, objAssginTMBo.PLSXT, ref strApprover,
                                                                                                ref strApprovalMail, ref strReqesterMail);
            objPIAssignTMDataContext.Dispose();
            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public int Approval_ClockIODetails(msassignedtomebo objAssginTMBo, ref string strApprover, ref string strApprovalMail, ref string strReqesterMail)
    {
        try
        {
            int iResultCode = objPIAssignTMDataContext.sp_ms_approve_clockinout_details(
                                                                     objAssginTMBo.PKEY, objAssginTMBo.APPROVED_BY, objAssginTMBo.COMMENTS, objAssginTMBo.APPROVED_ON,
                                                                      objAssginTMBo.STATUS, objAssginTMBo.ENTRY_STATUS, objAssginTMBo.PLSXT, ref strApprover,
                                                                                                ref strApprovalMail, ref strReqesterMail);
            objPIAssignTMDataContext.Dispose();
            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public int Approval_RecorWorkingDetails(string cc,msassignedtomebo objAssginTMBo, ref string strApprover, ref string strApprovalMail, ref string strReqesterMail)
    {
        try
        {
            int iResultCode = objPIAssignTMDataContext.sp_ms_approve_recordworking_details(cc,
                                                                     objAssginTMBo.PKEY, objAssginTMBo.APPROVED_BY, objAssginTMBo.COMMENTS, objAssginTMBo.APPROVED_ON,
                                                                      objAssginTMBo.STATUS, objAssginTMBo.PLSXT, ref strApprover,
                                                                                                ref strApprovalMail, ref strReqesterMail);
            objPIAssignTMDataContext.Dispose();
            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public int Approval_LeaveRequestDetails(msassignedtomebo objAssginTMBo, ref string strApprover, ref string strApprovalMail, ref string strReqesterMail)
    {
        try
        {
            int iResultCode = objPIAssignTMDataContext.sp_ms_approve_leave_request(
                                                                     objAssginTMBo.PKEY, objAssginTMBo.APPROVED_BY, objAssginTMBo.COMMENTS, objAssginTMBo.APPROVED_ON,
                                                                      objAssginTMBo.STATUS, objAssginTMBo.PLSXT, ref strApprover,
                                                                                                ref strApprovalMail, ref strReqesterMail);
            objPIAssignTMDataContext.Dispose();
            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }

    public int Approval_LeaveEncashmentDetails(msassignedtomebo objAssginTMBo, ref string strApprover, ref string strApprovalMail, ref string strReqesterMail)
    {
        try
        {
            int iResultCode = objPIAssignTMDataContext.sp_ms_approve_leave_encashment_details(objAssginTMBo.PKEY, objAssginTMBo.COMMENTS,
                                                                                                objAssginTMBo.APPROVED_ON, objAssginTMBo.APPROVED_BY,
                                                                                                objAssginTMBo.STATUS, objAssginTMBo.PLSXT, ref strApprover,
                                                                                                ref strApprovalMail, ref strReqesterMail);
            objPIAssignTMDataContext.Dispose();
            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }

    public int Approval_TravelRequestDetails(msassignedtomebo objAssginTMBo, ref string strApprover, ref string strApprovalMail, ref string strReqesterMail,
                                                                             ref string strCEO, ref string strCEOName, ref string strCEOMail,
                                                                             ref string strHRPernr, ref string strHRName, ref string strHRMail,
                                                                             ref string strPayrollPernr, ref string strPayrollName, ref string strPayrollMail,
                                                                             ref bool? Chk)
    {
        try
        {
            int iResultCode = objPIAssignTMDataContext.sp_ms_approve_travel_request_details(objAssginTMBo.PERNR, objAssginTMBo.PKEY, objAssginTMBo.COMMENTS,
                                                                                                objAssginTMBo.APPROVED_ON, objAssginTMBo.APPROVED_BY,
                                                                                                objAssginTMBo.STATUS, objAssginTMBo.PLSXT, ref strApprover,
                                                                                                ref strApprovalMail, ref strReqesterMail,
                                                                                                ref strCEO, ref strCEOName, ref strCEOMail,
                                                                                                ref strHRPernr, ref strHRName, ref strHRMail,
                                                                                                ref strPayrollPernr, ref strPayrollName, ref strPayrollMail,
                                                                                                ref Chk);
            objPIAssignTMDataContext.Dispose();
            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }

    public int fun_Get_HR_Details(string strPernr, ref bool? strHRStatus, ref string strHRPernr, ref string strHRName, ref string strHREmail, ref string strHRPhn)
    {
        try
        {
            int iResultCode = objPIAssignTMDataContext.sp_ms_get_HR_Details(strPernr, ref strHRStatus, ref strHRPernr, ref strHRName, ref strHREmail, ref strHRPhn);

            objPIAssignTMDataContext.Dispose();
            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }


    public List<msassignedtomebo> Get_Employee_Names(string prefixText)
    {
        List<msassignedtomebo> msassignedtomeboList = new List<msassignedtomebo>();
        msassignedtomedalDataContext objPIAssignTMDataContext = new msassignedtomedalDataContext();
        foreach (var vRow in objPIAssignTMDataContext.sp_get_employee_names(prefixText))
        {
            msassignedtomebo msassignedtomeboObj = new msassignedtomebo();
            msassignedtomeboObj.EMPLOYEE_NAME = vRow.ENAME;
            msassignedtomeboObj.EMPLOYEE_NO = vRow.PERNR;
            msassignedtomeboList.Add(msassignedtomeboObj);
        }
        return msassignedtomeboList;
    }

    public msassignedtomecollectionbo Load_emp_Bday()
    {
        msassignedtomecollectionbo objAssginTMList = new msassignedtomecollectionbo();
        //foreach (var vRow in objPIAssignTMDataContext.usp_sp_Get_Employee_Bday())
        //{
        //    msassignedtomebo objBo = new msassignedtomebo();
        //    objBo.PERNR = vRow.PERNR;
        //    objBo.ENAME = vRow.ENAME;
        //    objAssginTMList.Add(objBo);
        //}
        //objPIAssignTMDataContext.Dispose();
        return objAssginTMList;
    }

    public msassignedtomecollectionbo Get_OrgChartData(string PERNR,string cc)
    {
        msassignedtomecollectionbo objAssginTMList = new msassignedtomecollectionbo();
        foreach (var vRow in objPIAssignTMDataContext.usp_get_Org_Chart_data(PERNR, cc))
        {
            msassignedtomebo objBo = new msassignedtomebo();
            objBo.PERNR = vRow.PERNR.ToString();
            objBo.Manager = vRow.Manager;
            objBo.ENAME = vRow.ENAME;
            objBo.DESIG = vRow.DESIG;
            objBo.Image = vRow.Image;
            objAssginTMList.Add(objBo);
        }
        objPIAssignTMDataContext.Dispose();
        return objAssginTMList;
    }


    public msassignedtomecollectionbo Get_Designation(string Pernr)
    {
        msassignedtomecollectionbo objAssginTMList = new msassignedtomecollectionbo();
        foreach (var vRow in objPIAssignTMDataContext.usp_rrf_get_designation_of_emp(Pernr))
        {
            msassignedtomebo objBo = new msassignedtomebo();
            objBo.DESIG = vRow.PLSXT.ToString();
            objAssginTMList.Add(objBo);
        }
        objPIAssignTMDataContext.Dispose();
        return objAssginTMList;
    }

    public msassignedtomecollectionbo Get_KPIEmp(string PERNR)
    {
        msassignedtomecollectionbo objAssginTMList = new msassignedtomecollectionbo();
        foreach (var vRow in objPIAssignTMDataContext.usp_get_subordinates_for_KPI(PERNR))
        {
            msassignedtomebo objBo = new msassignedtomebo();
            objBo.PERNR = vRow.PERNR.ToString();
            objBo.ENAME = vRow.ENAME;
            objBo.PLSXT = vRow.PLSXT;
            objAssginTMList.Add(objBo);
        }
        objPIAssignTMDataContext.Dispose();
        return objAssginTMList;
    }

    public msassignedtomecollectionbo Load_emp_Bday(string PERNR, string ccode, ref bool? BDay)
    {
        msassignedtomecollectionbo objAssginTMList = new msassignedtomecollectionbo();
        foreach (var vRow in objPIAssignTMDataContext.payc_Get_Employee_Bday(PERNR, ccode, ref BDay))
        {
            msassignedtomebo objBo = new msassignedtomebo();
            //objBo.PERNR = vRow.PERNR;
            objBo.ENAME = vRow.ENAME;
            objAssginTMList.Add(objBo);
        }
        objPIAssignTMDataContext.Dispose();
        return objAssginTMList;
    }

    public msassignedtomecollectionbo Get_Leaveattd_review(msassignedtomebo objBo1)
    {
        msassignedtomecollectionbo objAssginTMList = new msassignedtomecollectionbo();
        foreach (var vRow in objPIAssignTMDataContext.payc_get_leaveattd_review(objBo1.ccode, objBo1.PERNR, objBo1.bdate, objBo1.edate,objBo1.PLSXT, objBo1.Flag))
        {
            msassignedtomebo objBo = new msassignedtomebo();
            objBo.rid = vRow.ID;
            objBo.PKEY = vRow.pkey;
            objBo.PERNR = vRow.PERNR.ToString();
            objBo.ENAME = vRow.ENAME;
            objBo.MODON = vRow.BEGDA;
            objBo.MMODON = vRow.ENDDA;
            objBo.COMMENTS = vRow.Remarks;
            objBo.S_NAME =vRow.STATUS;
            objBo.TableTyp = vRow.Ltype;
            objBo.Subtype = vRow.ATEXT;
            objBo.ttl = vRow.TTL;
            objBo.stime = vRow.BEGUZ;
            objBo.etime = vRow.ENDUZ;
            objBo.createdon = vRow.CREATED_ON;
            objAssginTMList.Add(objBo);
        }
        objPIAssignTMDataContext.Dispose();
        return objAssginTMList;
    }
}