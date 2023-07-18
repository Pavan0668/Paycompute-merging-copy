using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBL.Common;
using iEmpPower.Old_App_Code.iEmpPowerBL.Local_requisition;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;

namespace iEmpPower.UI.Benefits_Payment
{
    public partial class hod_Local_acc_vehi_req_pro : System.Web.UI.Page
    {
      protected  MembershipUser memUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessageA.Text = "";
            lblMessageP.Text = "";

            if (!IsPostBack)
            {
                LoadApprovalAccVehiDetails();
            }
        }

        protected void tcDefalut_ActiveTabChanged(object sender, EventArgs e)
        {
            ClearControls();
            //grdAccommadationProposal.SelectedIndex = -1;
            //grdVehicleProposal.SelectedIndex = -1;
            //grdAccomodationApproval.SelectedIndex = -1;
            //grdVehicleApproval.SelectedIndex = -1;

            if (tcDefalut.ActiveTab == tabApproval)
            {
                LoadApprovalAccVehiDetails();
            }
            else
            {
                LoadProposalAccVehiDetails();
            }
        }

        public void LoadApprovalAccVehiDetails()
        {
            try
            {
                grdAccomodationApproval.SelectedIndex = -1;
                grdVehicleApproval.SelectedIndex = -1;
                //Bind domestic gridview
                grdAccomodationApproval.DataSource = null;
                grdAccomodationApproval.DataBind();
                grdVehicleApproval.DataSource = null;
                grdVehicleApproval.DataBind();
                //Bind domestic accommodation gridview
                List<accomodation_requisitionbo> accomodation_requisitionboList = new List<accomodation_requisitionbo>();
                local_accommodation_requisitionbl accomodation_requisitionblObj = new local_accommodation_requisitionbl();
                //Bind domestic vehicle gridview
                List<vehicle_requisitionbo> vehicle_requisitionboboList = new List<vehicle_requisitionbo>();
                local_travel_requisitionbl vehicle_requisitionblObj = new local_travel_requisitionbl();
                //==============================
                //get sub employees
                msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
                msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
                objPIDashBoardBo.PERNR = User.Identity.Name;
                msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_Manager(objPIDashBoardBo);

                if (objPIDashBoardLst.Count > 0)
                {
                    foreach (var vrow in objPIDashBoardLst)
                    {
                        string strEmployeeId = vrow.PERNR;
                        List<accomodation_requisitionbo> accomodation_requisitionboList1 = new List<accomodation_requisitionbo>();
                        accomodation_requisitionboList1 = accomodation_requisitionblObj.Get_Local_Accommodation_Separate_Details(  strEmployeeId, "1");
                        accomodation_requisitionboList.AddRange(accomodation_requisitionboList1);

                        List<vehicle_requisitionbo> vehicle_requisitionboboList1 = new List<vehicle_requisitionbo>();
                        vehicle_requisitionboboList1 = vehicle_requisitionblObj.Get_Local_Vehicle_Separate_Details( strEmployeeId, "1");
                        vehicle_requisitionboboList.AddRange(vehicle_requisitionboboList1);
                    }

                    if (accomodation_requisitionboList.Count != 0)
                    {
                        grdAccomodationApproval.DataSource = accomodation_requisitionboList;
                        grdAccomodationApproval.DataBind();
                        grdAccomodationApproval.Visible = true;
                        pnlAccommadationApproval.Visible = true;
                    }
                    else
                    {
                        grdAccomodationApproval.DataSource = null;
                        grdAccomodationApproval.DataBind();
                        grdAccomodationApproval.Visible = false;
                    }
                    if (vehicle_requisitionboboList.Count != 0)
                    {
                        grdVehicleApproval.DataSource = vehicle_requisitionboboList;
                        grdVehicleApproval.DataBind();
                        grdVehicleApproval.Visible = true;
                        pnlAccommadationApproval.Visible = true;
                    }
                    else
                    {
                        grdVehicleApproval.DataSource = null;
                        grdVehicleApproval.DataBind();
                        grdVehicleApproval.Visible = false;
                    }
                }
                //==============================
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }


        public void LoadProposalAccVehiDetails()
        {
            try
            {
                grdAccommadationProposal.SelectedIndex = -1;
                grdVehicleProposal.SelectedIndex = -1;
                //Bind domestic gridview
                grdAccommadationProposal.DataSource = null;
                grdAccommadationProposal.DataBind();
                grdVehicleProposal.DataSource = null;
                grdVehicleProposal.DataBind();
                //Bind domestic accommodation gridview
                List<accomodation_requisitionbo> accomodation_requisitionboList = new List<accomodation_requisitionbo>();
                local_accommodation_requisitionbl accomodation_requisitionblObj = new local_accommodation_requisitionbl();
                //Bind domestic vehicle gridview
                List<vehicle_requisitionbo> vehicle_requisitionboboList = new List<vehicle_requisitionbo>();
                local_travel_requisitionbl vehicle_requisitionblObj = new local_travel_requisitionbl();
                //==============================
                //get sub employees
                msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
                msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
                objPIDashBoardBo.PERNR = User.Identity.Name;
                msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_Manager(objPIDashBoardBo);

                if (objPIDashBoardLst.Count > 0)
                {
                    foreach (var vrow in objPIDashBoardLst)
                    {
                        List<requisitionbo> requisitionboList1 = new List<requisitionbo>();
                        string strEmployeeId = vrow.PERNR;
                        List<accomodation_requisitionbo> accomodation_requisitionboList1 = new List<accomodation_requisitionbo>();
                        accomodation_requisitionboList1 = accomodation_requisitionblObj.Get_Local_Accommodation_Separate_Details( strEmployeeId, "6");
                        accomodation_requisitionboList.AddRange(accomodation_requisitionboList1);

                        List<vehicle_requisitionbo> vehicle_requisitionboboList1 = new List<vehicle_requisitionbo>();
                        vehicle_requisitionboboList1 = vehicle_requisitionblObj.Get_Local_Vehicle_Separate_Details( strEmployeeId, "6");
                        vehicle_requisitionboboList.AddRange(vehicle_requisitionboboList1);
                    }

                    if (accomodation_requisitionboList.Count != 0)
                    {
                        grdAccommadationProposal.DataSource = accomodation_requisitionboList;
                        grdAccommadationProposal.DataBind();
                        grdAccommadationProposal.Visible = true;
                        pnlAccommodationProposal.Visible = true;
                    }
                    else
                    {
                        grdAccommadationProposal.DataSource = null;
                        grdAccommadationProposal.DataBind();
                        grdAccommadationProposal.Visible = false;
                    }
                    if (vehicle_requisitionboboList.Count != 0)
                    {
                        grdVehicleProposal.DataSource = vehicle_requisitionboboList;
                        grdVehicleProposal.DataBind();
                        grdVehicleProposal.Visible = true;
                        pnlAccommodationProposal.Visible = true;
                    }
                    else
                    {
                        grdVehicleApproval.DataSource = null;
                        grdVehicleApproval.DataBind();
                        grdVehicleApproval.Visible = false;
                    }
                }
                //==============================

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }


        protected void grdAccomodationApproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdAccomodationApproval, "Select$" + e.Row.RowIndex);
            }
        }

        protected void grdVehicleApproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdVehicleApproval, "Select$" + e.Row.RowIndex);
            }
        }

        protected void grdAccommadationProposal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdAccommadationProposal, "Select$" + e.Row.RowIndex);
            }
        }

        protected void grdVehicleProposal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdVehicleProposal, "Select$" + e.Row.RowIndex);
            }
        }

        protected void btnApproveApp_Click(object sender, EventArgs e)
        {
            try
            {
                //if (txtHOD_RemarksApproval.Text == "" || txtHOD_RemarksApproval.Text == string.Empty)
                //{
                //    lblMessageA.ForeColor = System.Drawing.Color.Red;
                //    lblMessageA.Text = "Please enter Remarks";
                //    return;
                //}

                int count = UpdateRequisitionStatus((int)ReuisitionStatus.RequisitionApprroved);
                if (count == 0)
                {
                    lblMessageA.ForeColor = System.Drawing.Color.Green;
                    lblMessageA.Text = "Local Requisitions approved successfully.";
                    txtHOD_RemarksApproval.Text = "";
                }
                else
                {
                    lblMessageA.ForeColor = System.Drawing.Color.Red;
                    lblMessageA.Text = "Unknown error occured.";
                }
            }
            catch (Exception ex)
            {
                lblMessageA.Text = ex.Message;
                lblMessageA.ForeColor = System.Drawing.Color.Red;
            }
        }
        
        int UpdateRequisitionStatus(int UpdateStatus)
        {
            memUser = Membership.GetUser();

            int iResult = 0;

            //Accommodation Approval
            if (grdAccomodationApproval.SelectedValue != null)
            {
                GridViewRow grdrowAcc = grdAccomodationApproval.SelectedRow;
                accomodation_requisitionbo objbo = new accomodation_requisitionbo();
                accomodation_requisitionbl objbl = new accomodation_requisitionbl();

                objbo.Accommadation_req_id = Convert.ToInt32(grdrowAcc.Cells[0].Text.ToString().Trim());
                objbo.Remarks = grdrowAcc.Cells[16].Text.ToString().Trim() + "|" + txtHOD_RemarksApproval.Text;
                objbo.MODIFIEDBY = memUser.UserName;

                if (UpdateStatus == (int)ReuisitionStatus.RequisitionApprroved)
                {
                    //get the enum value of 'New' status
                    objbo.current_status = Convert.ToString((int)Enum.Parse(typeof(ReuisitionStatus), ReuisitionStatus.RequisitionApprroved.ToString()));
                    iResult = objbl.HODStatusUpdateSeparateAccomm(objbo, "local");
                }
                else if (UpdateStatus == (int)ReuisitionStatus.RequisitionRejected)
                {
                    //get the enum value of 'RequisitionRejected' status
                    objbo.current_status = Convert.ToString((int)Enum.Parse(typeof(ReuisitionStatus), ReuisitionStatus.RequisitionRejected.ToString()));
                    iResult = objbl.HODStatusUpdateSeparateAccomm(objbo, "local");
                }
                else if (UpdateStatus == (int)ReuisitionStatus.RequisitionCancelledByHOD)
                {
                    //get the enum value of 'hod cancel' status
                    objbo.current_status = Convert.ToString((int)Enum.Parse(typeof(ReuisitionStatus), ReuisitionStatus.RequisitionCancelledByHOD.ToString()));
                    iResult = objbl.HODStatusUpdateSeparateAccomm(objbo, "local");
                }
                if (UpdateStatus == (int)ReuisitionStatus.NotBooked)
                {
                    //get the enum value of 'New' status
                    objbo.current_status = Convert.ToString((int)Enum.Parse(typeof(ReuisitionStatus), ReuisitionStatus.NotBooked.ToString()));
                    iResult = objbl.HODStatusUpdateSeparateAccomm(objbo, "local");
                }
                else if (UpdateStatus == (int)ReuisitionStatus.ProposalReject)
                {
                    //get the enum value of 'New' status
                    objbo.current_status = Convert.ToString((int)Enum.Parse(typeof(ReuisitionStatus), ReuisitionStatus.ProposalReject.ToString()));
                    iResult = objbl.HODStatusUpdateSeparateAccomm(objbo, "local");
                }
            }

            //Vehicle Approval
            if (grdVehicleApproval.SelectedValue != null)
            {
                GridViewRow grdrowVehi = grdVehicleApproval.SelectedRow;

                vehicle_requisitionbo objbo = new vehicle_requisitionbo();
                vehicle_requisitionbl objbl = new vehicle_requisitionbl();

                objbo.Vehicle_req_id = Convert.ToInt32(grdrowVehi.Cells[0].Text.ToString().Trim());
                objbo.remarks = grdrowVehi.Cells[18].Text.ToString().Trim() + "|" + txtHOD_RemarksApproval.Text;
                objbo.MODIFIEDBY = memUser.UserName;

                if (UpdateStatus == (int)ReuisitionStatus.RequisitionApprroved)
                {
                    //get the enum value of 'New' status
                    objbo.current_status = Convert.ToString((int)Enum.Parse(typeof(ReuisitionStatus), ReuisitionStatus.RequisitionApprroved.ToString()));
                    iResult = objbl.HODStatusUpdateSeparateVehi(objbo, "local");
                }
                else if (UpdateStatus == (int)ReuisitionStatus.RequisitionRejected)
                {
                    //get the enum value of 'RequisitionRejected' status
                    objbo.current_status = Convert.ToString((int)Enum.Parse(typeof(ReuisitionStatus), ReuisitionStatus.RequisitionRejected.ToString()));
                    iResult = objbl.HODStatusUpdateSeparateVehi(objbo, "local");
                }
                else if (UpdateStatus == (int)ReuisitionStatus.RequisitionCancelledByHOD)
                {
                    //get the enum value of 'hod cancel' status
                    objbo.current_status = Convert.ToString((int)Enum.Parse(typeof(ReuisitionStatus), ReuisitionStatus.RequisitionCancelledByHOD.ToString()));
                    iResult = objbl.HODStatusUpdateSeparateVehi(objbo, "local");
                }
                if (UpdateStatus == (int)ReuisitionStatus.NotBooked)
                {
                    //get the enum value of 'New' status
                    objbo.current_status = Convert.ToString((int)Enum.Parse(typeof(ReuisitionStatus), ReuisitionStatus.NotBooked.ToString()));
                    iResult = objbl.HODStatusUpdateSeparateVehi(objbo, "local");
                }
                else if (UpdateStatus == (int)ReuisitionStatus.ProposalReject)
                {
                    //get the enum value of 'New' status
                    objbo.current_status = Convert.ToString((int)Enum.Parse(typeof(ReuisitionStatus), ReuisitionStatus.ProposalReject.ToString()));
                    iResult = objbl.HODStatusUpdateSeparateVehi(objbo, "local");
                }
            }

            ClearControls();
            LoadApprovalAccVehiDetails();
            return iResult;
        }


        int UpdateProposalStatus(int UpdateStatus)
        {
            memUser = Membership.GetUser();

            int iResult = 0;

            //Accommodation Approval
            if (grdAccommadationProposal.SelectedValue != null)
            {
                GridViewRow grdrowAcc = grdAccommadationProposal.SelectedRow;
                accomodation_requisitionbo objbo = new accomodation_requisitionbo();
                accomodation_requisitionbl objbl = new accomodation_requisitionbl();

                objbo.Accommadation_req_id = Convert.ToInt32(grdrowAcc.Cells[0].Text.ToString().Trim());
                objbo.Remarks = grdrowAcc.Cells[16].Text.ToString().Trim() + "|" + txtHOD_RemarksApproval.Text;
                objbo.MODIFIEDBY = memUser.UserName;

                if (UpdateStatus == (int)ReuisitionStatus.RequisitionApprroved)
                {
                    //get the enum value of 'New' status
                    objbo.current_status = Convert.ToString((int)Enum.Parse(typeof(ReuisitionStatus), ReuisitionStatus.RequisitionApprroved.ToString()));
                    iResult = objbl.HODStatusUpdateSeparateAccomm(objbo, "local");
                }
                else if (UpdateStatus == (int)ReuisitionStatus.RequisitionRejected)
                {
                    //get the enum value of 'RequisitionRejected' status
                    objbo.current_status = Convert.ToString((int)Enum.Parse(typeof(ReuisitionStatus), ReuisitionStatus.RequisitionRejected.ToString()));
                    iResult = objbl.HODStatusUpdateSeparateAccomm(objbo, "local");
                }
                else if (UpdateStatus == (int)ReuisitionStatus.RequisitionCancelledByHOD)
                {
                    //get the enum value of 'hod cancel' status
                    objbo.current_status = Convert.ToString((int)Enum.Parse(typeof(ReuisitionStatus), ReuisitionStatus.RequisitionCancelledByHOD.ToString()));
                    iResult = objbl.HODStatusUpdateSeparateAccomm(objbo, "local");
                }
                if (UpdateStatus == (int)ReuisitionStatus.NotBooked)
                {
                    //get the enum value of 'New' status
                    objbo.current_status = Convert.ToString((int)Enum.Parse(typeof(ReuisitionStatus), ReuisitionStatus.NotBooked.ToString()));
                    iResult = objbl.HODStatusUpdateSeparateAccomm(objbo, "local");
                }
                else if (UpdateStatus == (int)ReuisitionStatus.ProposalReject)
                {
                    //get the enum value of 'New' status
                    objbo.current_status = Convert.ToString((int)Enum.Parse(typeof(ReuisitionStatus), ReuisitionStatus.ProposalReject.ToString()));
                    iResult = objbl.HODStatusUpdateSeparateAccomm(objbo, "local");
                }
            }

            //Vehicle Approval
            if (grdVehicleProposal.SelectedValue != null)
            {
                GridViewRow grdrowVehi = grdVehicleProposal.SelectedRow;

                vehicle_requisitionbo objbo = new vehicle_requisitionbo();
                vehicle_requisitionbl objbl = new vehicle_requisitionbl();

                objbo.Vehicle_req_id = Convert.ToInt32(grdrowVehi.Cells[0].Text.ToString().Trim());
                objbo.remarks = grdrowVehi.Cells[18].Text.ToString().Trim() + "|" + txtHOD_RemarksApproval.Text;
                objbo.MODIFIEDBY = memUser.UserName;

                if (UpdateStatus == (int)ReuisitionStatus.RequisitionApprroved)
                {
                    //get the enum value of 'New' status
                    objbo.current_status = Convert.ToString((int)Enum.Parse(typeof(ReuisitionStatus), ReuisitionStatus.RequisitionApprroved.ToString()));
                    iResult = objbl.HODStatusUpdateSeparateVehi(objbo, "local");
                }
                else if (UpdateStatus == (int)ReuisitionStatus.RequisitionRejected)
                {
                    //get the enum value of 'RequisitionRejected' status
                    objbo.current_status = Convert.ToString((int)Enum.Parse(typeof(ReuisitionStatus), ReuisitionStatus.RequisitionRejected.ToString()));
                    iResult = objbl.HODStatusUpdateSeparateVehi(objbo, "local");
                }
                else if (UpdateStatus == (int)ReuisitionStatus.RequisitionCancelledByHOD)
                {
                    //get the enum value of 'hod cancel' status
                    objbo.current_status = Convert.ToString((int)Enum.Parse(typeof(ReuisitionStatus), ReuisitionStatus.RequisitionCancelledByHOD.ToString()));
                    iResult = objbl.HODStatusUpdateSeparateVehi(objbo, "local");
                }
                if (UpdateStatus == (int)ReuisitionStatus.NotBooked)
                {
                    //get the enum value of 'New' status
                    objbo.current_status = Convert.ToString((int)Enum.Parse(typeof(ReuisitionStatus), ReuisitionStatus.NotBooked.ToString()));
                    iResult = objbl.HODStatusUpdateSeparateVehi(objbo, "local");
                }
                else if (UpdateStatus == (int)ReuisitionStatus.ProposalReject)
                {
                    //get the enum value of 'New' status
                    objbo.current_status = Convert.ToString((int)Enum.Parse(typeof(ReuisitionStatus), ReuisitionStatus.ProposalReject.ToString()));
                    iResult = objbl.HODStatusUpdateSeparateVehi(objbo, "local");
                }
            }

            ClearControls();
            LoadProposalAccVehiDetails();
            return iResult;
        }

        protected void btnRejectApp_Click(object sender, EventArgs e)
        {
            try
            {
                //if (txtHOD_RemarksApproval.Text == "" || txtHOD_RemarksApproval.Text == string.Empty)
                //{
                //    lblMessageA.ForeColor = System.Drawing.Color.Red;
                //    lblMessage.Text = "Please enter Remarks";
                //    return;
                //}

                int count = UpdateRequisitionStatus((int)ReuisitionStatus.RequisitionRejected);
                if (count == 0)
                {
                    lblMessageA.ForeColor = System.Drawing.Color.Green;
                    lblMessageA.Text =   "Local Requisitions Rejected successfully.";
                    txtHOD_RemarksApproval.Text = "";
                }
                else
                {
                    lblMessageA.ForeColor = System.Drawing.Color.Red;
                    lblMessageA.Text = "Unknown error occured.";
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void btnClearApp_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void btnApprovePro_Click(object sender, EventArgs e)
        {
            try
            {
                //if (txtHOD_RemarkProposal.Text == "" || txtHOD_RemarkProposal.Text == string.Empty)
                //{
                //    lblMessageP.ForeColor = System.Drawing.Color.Red;
                //    lblMessageP.Text = "Please enter Remarks";
                //    return;
                //}

                int count = UpdateProposalStatus((int)ReuisitionStatus.NotBooked);
                if (count ==0)
                {
                    lblMessageP.ForeColor = System.Drawing.Color.Green;
                    lblMessageP.Text =   "Local Proposal approved successfully.";
                    txtHOD_RemarkProposal.Text = "";
                }
                else
                {
                    lblMessageP.ForeColor = System.Drawing.Color.Red;
                    lblMessageP.Text = "Unknown error occured.";
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnRejectPro_Click(object sender, EventArgs e)
        {
            try
            {
                //if (txtHOD_RemarkProposal.Text == "" || txtHOD_RemarkProposal.Text == string.Empty)
                //{
                //    lblMessageP.ForeColor = System.Drawing.Color.Red;
                //    lblMessageP.Text = "Please enter Remarks";
                //    return;
                //}

                int count = UpdateProposalStatus((int)ReuisitionStatus.ProposalReject);
                if (count == 0)
                {
                    lblMessageP.ForeColor = System.Drawing.Color.Green;
                    lblMessageP.Text =  "Local Proposal Rejected successfully.";
                    txtHOD_RemarkProposal.Text = "";
                }
                else
                {
                    lblMessageP.ForeColor = System.Drawing.Color.Red;
                    lblMessageP.Text = "Unknown error occured.";
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnClearPro_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        void ClearControls()
        {
            txtHOD_RemarkProposal.Text = "";
            txtHOD_RemarksApproval.Text = "";
            lblMessageA.Text = "";
            lblMessageP.Text = "";
            lblMessage.Text = "";
        }

    }
}