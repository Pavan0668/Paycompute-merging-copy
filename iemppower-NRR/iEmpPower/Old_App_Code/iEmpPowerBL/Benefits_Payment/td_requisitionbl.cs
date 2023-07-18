using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment;
using System.Globalization;

public class td_requisitionbl
{
    public int Create_ProposalRequest(requisitionbo objBo, ref int? ProposalID, ref string ProposalSegmentID)
    {
        int iResult = -1;
        try
        {
            td_requisitionsdalDataContext objtd_requisitionblDataContext = new td_requisitionsdalDataContext();
            iResult = objtd_requisitionblDataContext.sp_create_proposal_mst_dtl(objBo.FTPT_REQUEST_ID_FOR_PROPOSAL,objBo.REQ_SEGMENT_ID_FOR_PROPOSAL, objBo.EMPLOYEE_NO, objBo.CREATED_BY, objBo.CHK, objBo.TRAVEL_DATE_ALL,
            objBo.TRAVEL_TIME, objBo.ISACTIVE, objBo.CURRENT_STATUS, objBo.MODE_OF_TRANSPOPRT_KZPMF, objBo.MEDIA_OF_CATEGORY_PKWKL, objBo.VEHICLE_NAME_VHNUM_ALL, objBo.REGION_RGION_FROM,
            objBo.REGION_RGION_TO, objBo.FLYNUM, objBo.ADVANCE, objBo.AIRLINE, objBo.VISA_REQUIRED_ALL, objBo.FR_EXCHANGE, objBo.INSUR_MEDICLAIM, objBo.SEAT_PREFERENCE,
            objBo.MEAL_PREFERENCE, objBo.BAGGAGE, objBo.HAND, objBo.REMARKS, ref ProposalID, ref ProposalSegmentID, objBo.ARRIVAL_DATE.ToString(), objBo.ARRIVAL_TIME, objBo.REASON);
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }

        return iResult;
    }

    public int UpdateRequistionStatus(requisitionbo objBo)
    {
       int iResult = -1;
        try
        {
            if (objBo.FTPT_REQUEST_ID_FOR_PROPOSAL.Contains("|"))
            {
                string[] strFTP = objBo.FTPT_REQUEST_ID_FOR_PROPOSAL.Split('|');
                string[] strFTPSEGid = objBo.REQ_SEGMENT_ID_FOR_PROPOSAL.Split('|');
                string[] strNO = objBo.EMPLOYEE_NO.Split('|');

                for (int i = 0; i < strFTP.Length; i++)
                {
                    string FTP = strFTP[i];
                    string FTPid = strFTPSEGid[i];
                    string NO = strNO[i];

                    td_requisitionsdalDataContext objtd_requisitionblDataContext = new td_requisitionsdalDataContext();
                    iResult = objtd_requisitionblDataContext.sp_update_requisition_status_mst_dtl_for_traveldesk(Convert.ToInt32(FTP), NO, Convert.ToInt32(FTPid), objBo.REMARKS, objBo.REASON_FOR_CANCEL, objBo.CURRENT_STATUS);
                    objtd_requisitionblDataContext.Dispose();
                }
            }
            else
            {
                td_requisitionsdalDataContext objtd_requisitionblDataContext = new td_requisitionsdalDataContext();
                iResult = objtd_requisitionblDataContext.sp_update_requisition_status_mst_dtl_for_traveldesk(Convert.ToInt32(objBo.FTPT_REQUEST_ID_FOR_PROPOSAL), objBo.EMPLOYEE_NO, Convert.ToInt32(objBo.REQ_SEGMENT_ID_FOR_PROPOSAL), objBo.REMARKS, objBo.REASON_FOR_CANCEL, objBo.CURRENT_STATUS);
                objtd_requisitionblDataContext.Dispose();
            }

        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        return iResult;
    }
}
