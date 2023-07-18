using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Configuration;
using System.Web.Security;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected MembershipUser memUser;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.UserAgent.IndexOf("AppleWebKit") > 0)
            Request.Browser.Adapters.Clear();
        //if (Session["logindetails"] != null)
        //{
        //// NavigationMenu.MenuItemDataBound += new MenuEventHandler(NavigationMenu_MenuItemDataBound);
        if (!IsPostBack)
        {
            LoadCustomerLogo();
            LoadEmployeeDetails();
            //this.Page.LoadComplete +=new EventHandler(Page_LoadComplete);
        }

        ////Image1.Focus();
        //}
        //else
        //{Response.Redirect("~/sessionout.aspx", false);}
        divfooter.InnerText = "© 2010 - " + System.DateTime.Today.Year + ", ITChamps Software Private Limited. All rights reserved.";

        //-----------------------Html Menu Filter Starts-----------------------

        string[] UserRoles = Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name);
        if (UserRoles.Length > 0)
        {
            switch (UserRoles[0].ToUpper())
            {
                case "SUPERUSER":
                    MenuPersonalInfo.Visible = false;
                    MenuBenefitsNPayments.Visible = false;
                    MenuWorkingTime.Visible = false;
                    //MenuTravel.Visible = false;
                    //MenuTraining.Visible = false;
                    //MenuEmpPerformance.Visible = false;
                    MenuMSS.Visible = false;


                    break;
                case "PR":
                    break;
                case "FINANCE":
                    break;
                //case "": break;

                default:
                    break;
            }

        }
        else
        {
            //MenuMSS.Visible = false;
            MenuDTwizard.Visible = false;
            MenuConfig.Visible = false;
            MenuMaintainUserAccounts.Visible = false;
            MenuResetEmployeePassword.Visible = false;
        }

        msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
        msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
        objPIDashBoardBo.PERNR = HttpContext.Current.User.Identity.Name;
        msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_Manager(objPIDashBoardBo);
        if (objPIDashBoardLst.Count>0)
        {
            MenuMSS.Visible = true;
            MSSTimeShtRevw.Visible = true;
            MSSAssgndtome.Visible = true;
        }
        else
        {
             MenuMSS.Visible = false;
             MSSTimeShtRevw.Visible = false;
             MSSAssgndtome.Visible = false;
        }

        //if (Roles.IsUserInRole("SuperUser"))
        //{
        //    MenuConfig.Visible = true;
        //    MenuDTwizard.Visible = true;
        //    MenuMaintainUserAccounts.Visible = true;
        //    MenuResetEmployeePassword.Visible = true;
        //    MenuPersonalInfo.Visible = false;
        //    MenuWorkingTime.Visible = false;
        //    MenuPR.Visible = false;
        //    MenuIExpense.Visible = false;
        //    MenuTravel.Visible = false;
        //    //MenuFBP.Visible = false;
        //    MenuMSS.Visible = false;
        //}

        //else
        //{
        //    if (Roles.IsUserInRole("PR"))
        //    {
        //        MenuPRStatusforPT.Visible = true;
        //        MenuFinanceViewClaimsStatus.Visible = false;
        //        MenuFinanceViewIExpenseStatus.Visible = false;
        //    }

        //    else if (Roles.IsUserInRole("Finance"))
        //    {
        //        MenuFinanceViewClaimsStatus.Visible = true;
        //        MenuFinanceViewIExpenseStatus.Visible = true;
        //        MenuPersonalInfo.Visible = false;
        //        MenuWorkingTime.Visible = false;
        //        MenuPR.Visible = false;
        //        MenuTravel.Visible = true;
        //        MenuTravelRequest.Visible = false;
        //        MenuClaimRequest.Visible = false;
        //        MenuEditSavedReqClaims.Visible = false;
        //        MenuTravelClaimHistory.Visible = false;
        //        //MenuFBP.Visible = false;
        //        MenuMSS.Visible = false;
        //        MenuIExpenseRequest.Visible = false;
        //        MenuIExpenseStatus.Visible = false;
        //        MenuIExpenseEditSavedReq.Visible = false;

        //    }
        //    else
        //    {
        //        MenuPRStatusforPT.Visible = false;
        //        MenuFinanceViewClaimsStatus.Visible = false;
        //        MenuFinanceViewIExpenseStatus.Visible = false;
        //    }
        //    MenuConfig.Visible = false;
        //    MenuDTwizard.Visible = false;
        //    MenuMaintainUserAccounts.Visible = false;
        //    MenuResetEmployeePassword.Visible = false;
        //}


        //-----------------------Html Menu Filter Ends-----------------------
        //}

        //protected void Page_LoadComplete(object sender, EventArgs e)
        //{
        //    if (!Roles.IsUserInRole("SuperUser"))
        //    {
        //        MenuItemCollection menuItems = NavigationMenu.Items;
        //        MenuItem adminItem = new MenuItem();
        //        foreach (MenuItem menuItem in menuItems)
        //        {
        //            if (menuItem.Text == "Configuration")
        //                adminItem = menuItem;
        //        }
        //        menuItems.Remove(adminItem);
        //    }
    }
    protected void LoadCustomerLogo()
    {
        try
        {
            configurationdalDataContext context1 = new configurationdalDataContext();
            var vLogo = (from col in context1.sp_conf_get_logo()
                         select col.img_logo).ToList();
            if (vLogo.Count <= 0)
            {
                imgCustomerLogo.Visible = false;
            }
            else
            {
                imgCustomerLogo.Visible = true;
                imgCustomerLogo.ImageUrl = "~/imagelogo.ashx";
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void LoadEmployeeDetails()
    {
        //http://shawpnendu.blogspot.com/2010/02/javascript-to-read-master-page-and.html
        //string userName = Request.QueryString["username"];

        try
        {
            memUser = Membership.GetUser();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }

        try
        {
            bool status = memUser.IsOnline;

            if (memUser != null)
            {
                configurationbl objBl = new configurationbl();
                configurationcollectionbo objLst = objBl.Load_EmployeePhotoDetails(memUser.ToString());
                foreach (configurationbo objBo in objLst)
                {

                    Label lName = HeadLoginView.FindControl("lblEmployyeName") as Label;
                    lName.Text = objBo.DESCRIPTION + " | " + memUser.ToString();

                    Session.Add("sEmploreeId", memUser.ToString());
                    Session.Add("EmployeeName", objBo.DESCRIPTION);
                    //Session.Add("WERKS", objBo.WERKS);
                    //Session.Add("BURKS", objBo.BUKRS);
                    Session.Add("PERSK", objBo.PERSK);

                    if (objBo.EMPLOYEE_PATH.ToString() != "")
                    {
                        // imgEmp.ImageUrl =  objBo.EMPLOYEE_PATH;
                        //imgEmp.ImageUrl = "~/whoiswho.ashx?imagepath=" + objBo.EMPLOYEE_PATH;
                        //imgEmp.ImageUrl = "~/empPhoto.ashx?sEmploreeId=" + memUser;
                        imgEmp.ImageUrl = objBo.EMPLOYEE_PATH;
                    }
                }
            }
            else if (memUser.UserName == "" || memUser.UserName == null || !status)
            {
                //Response.Redirect("~/sessionout.aspx", false);
                Response.Redirect("~/Account/Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            //Response.Redirect("~/sessionout.aspx", false);
            Response.Redirect("~/Account/Login.aspx", false);
        }
    }

    void NavigationMenu_MenuItemDataBound(object sender, MenuEventArgs e)
    {
        if ((!Roles.IsUserInRole("SuperUser")) && (!Roles.IsUserInRole("BPO")) && (!Roles.IsUserInRole("Security")) && (!Roles.IsUserInRole("Transport")))
        {
            System.Web.UI.WebControls.Menu menu = (System.Web.UI.WebControls.Menu)sender;
            SiteMapNode mapNode = (SiteMapNode)e.Item.DataItem;

            System.Web.UI.WebControls.MenuItem itemToRemove = menu.FindItem(mapNode.Title);

            if (mapNode.Title == "Configuration" || mapNode.Title == "Maintain User Account" || mapNode.Title == "DT wizard" || mapNode.Title == "Reset Employee Password"
                || mapNode.Title == "Field Staff" || mapNode.Title == "Kannada Payslip" || mapNode.Title == "Local Vehicle Booking" || mapNode.Title == "Booking History")
            {
                System.Web.UI.WebControls.MenuItem parent = e.Item.Parent;
                if (parent != null)
                {
                    parent.ChildItems.Remove(e.Item);
                }
            }
            //For HOD designation
            requisitionbo requisitionboObject = new requisitionbo();
            travelrequestbl travelrequestBL = new travelrequestbl();
            // requisitionboObject.FTPT_REQUEST_ID1 = HttpContext.Current.User.Identity.Name;//Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            int iResult = travelrequestBL.check_designation(requisitionboObject);

            //======================================================================
            msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
            msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
            objPIDashBoardBo.PERNR = HttpContext.Current.User.Identity.Name;
            msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_Manager(objPIDashBoardBo);
            if (objPIDashBoardLst.Count > 0)
            {
                iResult = 0;
            }
            //=====================================================================

            if (iResult == 0)
            {
                if (mapNode.Url.EndsWith("acc_proposals_hod.aspx") || mapNode.Url.EndsWith("veh_proposals_hod.aspx") || mapNode.Url.EndsWith("td_requisitions.aspx")
                    || mapNode.Url.EndsWith("veh_requisitions_td.aspx") || mapNode.Url.EndsWith("acc_requisition_td.aspx") || mapNode.Url.EndsWith("td_proposals.aspx")
                    || mapNode.Url.EndsWith("ticket_booking_td.aspx") || mapNode.Url.EndsWith("acc_booking_td.aspx") || mapNode.Url.EndsWith("veh_booking_td.aspx")
                    || mapNode.Url.EndsWith("ticket_cancel_td.aspx") || mapNode.Url.EndsWith("ticket_summary_Acc_Vehi_Travel.aspx") || mapNode.Url.EndsWith("Requisition_Ticket_History.aspx")
                    || mapNode.Url.EndsWith("travellers_feedback_View.aspx") || mapNode.Url.EndsWith("td_acc_vehi_pro.aspx") || mapNode.Url.EndsWith("td_local_acc_vehi_pro.aspx")
                    || mapNode.Title == "Ticket Booking Acc/Vehi" || mapNode.Url.EndsWith("td_Bill_Updates.aspx") || mapNode.Title == "Employee's Travel Profile" || mapNode.Title == "Payroll Management"
                    || mapNode.Title == "Approval for Claim requisitions"
                    )
                {
                    System.Web.UI.WebControls.MenuItem parent = e.Item.Parent;
                    if (parent != null)
                    {
                        parent.ChildItems.Remove(e.Item);
                    }
                }
                //-----------start-----------------HOD requisition and proposals count.--------------------------------------------------------------------------------------------------
                RequisitionAndProposalCountBO count = new RequisitionAndProposalCountBO();
                travelrequestbl requisitinBL = new travelrequestbl();
                List<RequisitionAndProposalCountBO> requisitionboList = new List<RequisitionAndProposalCountBO>();
                requisitionboList = requisitinBL.GetRequisitionAndProposalsCount(Convert.ToString(HttpContext.Current.User.Identity.Name), "HOD");
                SiteMapNodeCollection mapNode2 = (SiteMapNodeCollection)mapNode.ChildNodes;
                foreach (SiteMapNode smn in mapNode2)
                {
                    if (smn.Title == "Travel Request" || smn.Key == "0b46ffe5-acb6-40c2-a3a4-4c36201a4397")
                    {
                        SiteMapNodeCollection mapNode3 = (SiteMapNodeCollection)(smn.ChildNodes);
                        foreach (SiteMapNode smn1 in mapNode3)
                        {
                            if (smn1.Title == "Requisition")
                            {
                                SiteMapNodeCollection mapNode4 = (SiteMapNodeCollection)(smn1.ChildNodes);
                                foreach (SiteMapNode smn2 in mapNode4)
                                {
                                    //if (smn2.Title == "Local")
                                    //{
                                    //    SiteMapNodeCollection mapNode5 = (SiteMapNodeCollection)(smn2.ChildNodes);
                                    //    foreach (SiteMapNode smn3 in mapNode5)
                                    //    {
                                    //        if (smn3.Title == "Local vehicle requisition" || smn3.Url == "/UI/Local_requisition/local_travel_requisition.aspx")
                                    //        {
                                    //            smn3.ReadOnly = false;
                                    //            smn3.Title = "Local vehicle requisition" + "(" + Convert.ToString(requisitionboList[0].LocalTravelRequisitionCount) + ")";
                                    //        }
                                    //        if (smn3.Title == "Local accommodation requisition" || smn3.Url=="/UI/Local_requisition/local_accommodation_requisition.aspx")
                                    //        {
                                    //            smn3.ReadOnly = false;
                                    //            smn3.Title = "Local accommodation requisition" + "(" + Convert.ToString(requisitionboList[0].LocalAccommodationRequisitionCount) + ")";
                                    //        }
                                    //    }
                                    //}
                                    if (smn2.Title == "Outstation")
                                    {
                                        SiteMapNodeCollection mapNode5 = (SiteMapNodeCollection)(smn2.ChildNodes);
                                        foreach (SiteMapNode smn3 in mapNode5)
                                        {
                                            if (smn3.Title == "HOD-Proposals" || smn3.Url == "/UI/Benefits_Payment/hod_proposals.aspx")
                                            {
                                                smn3.ReadOnly = false;
                                                smn3.Title = "HOD-Proposals" + "(" + Convert.ToString(requisitionboList[0].OutStationProposalsCount) + ")";
                                            }
                                            if (smn3.Title == "Required for approval" || smn3.Url == "/UI/Benefits_Payment/hod_requisition.aspx")
                                            {
                                                smn3.ReadOnly = false;
                                                smn3.Title = "Required for approval" + "(" + Convert.ToString(requisitionboList[0].OutStationRequisitionCount) + ")";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //-----------end-----------------HOD requisition and proposals count.--------------------------------------------------------------------------------------------------
            }

            //For travel Desk Role
            if (Roles.IsUserInRole("Travel Desk"))
            {
                if (mapNode.Title == "Proposals" || mapNode.Title == "Vehicle requisitions" || mapNode.Url.EndsWith("acc_requisition_td.aspx")
                    || mapNode.Url.EndsWith("hod_requisition.aspx") || mapNode.Url.EndsWith("travel_request.aspx") || mapNode.Url.EndsWith("new_vehicle_requisition.aspx")
                    || mapNode.Url.EndsWith("new_accommodation_requisition.aspx") || mapNode.Url.EndsWith("travellers_feedback.aspx")
                    || mapNode.Url.EndsWith("hod_requisitions.aspx") || mapNode.Url.EndsWith("hod_proposals.aspx") || mapNode.Url.EndsWith("acc_requisition_hod.aspx")
                    || mapNode.Url.EndsWith("veh_proposals_hod.aspx") || mapNode.Url.EndsWith("veh_requisition_hod.aspx") || mapNode.Url.EndsWith("acc_proposals_hod.aspx")
                    || mapNode.Title == "Claims" || mapNode.Title == "Requisition" || mapNode.Url.EndsWith("hod_acc_vehi_req_pro.aspx")
                    || mapNode.Url.EndsWith("hod_Local_acc_vehi_req_pro.aspx") || mapNode.Url.EndsWith("cancel_Requisition_Traveller.aspx") || mapNode.Title == "Training and Event Mgmt" || mapNode.Title == "Payroll Management"

                   || mapNode.Title == "Configuration" || mapNode.Title == "DT wizard" || mapNode.Title == "Personal Information" || mapNode.Title == "Benefits and Payment"
                   || mapNode.Title == "Working Time" || mapNode.Title == "Manager Self Service" || mapNode.Title == "Travel Request"
                   || mapNode.Title == "Employee Performance" || mapNode.Title == "Local Vehicle Booking" || mapNode.Title == "Booking History"
                   || mapNode.Title == "Travel Claims"
                   || mapNode.Title == "Employee's Travel Profile"
                   || mapNode.Title == "Approval for Local Acc/Vehi"
                   || mapNode.Title == "Ticket Cancel/Edit"
                   || mapNode.Title == "Requisition/Ticket History"
                   || mapNode.Title == "Travellers Feedback"
                    || mapNode.Title == "Purchase Request"
                    )
                {
                    System.Web.UI.WebControls.MenuItem parent = e.Item.Parent;
                    if (parent != null)
                    {
                        parent.ChildItems.Remove(e.Item);
                    }
                }
                //-----------start-----------------Travel desk requisition and proposals count.--------------------------------------------------------------------------------------------------
                RequisitionAndProposalCountBO count = new RequisitionAndProposalCountBO();
                travelrequestbl requisitinBL = new travelrequestbl();
                List<RequisitionAndProposalCountBO> requisitionboList = new List<RequisitionAndProposalCountBO>();
                requisitionboList = requisitinBL.GetRequisitionAndProposalsCount(Convert.ToString(HttpContext.Current.User.Identity.Name), "TD");
                SiteMapNodeCollection mapNode2 = (SiteMapNodeCollection)mapNode.ChildNodes;
                foreach (SiteMapNode smn in mapNode2)
                {
                    if (smn.Title == "Travel Request" || smn.Key == "0b46ffe5-acb6-40c2-a3a4-4c36201a4397")
                    {
                        SiteMapNodeCollection mapNode3 = (SiteMapNodeCollection)(smn.ChildNodes);
                        foreach (SiteMapNode smn1 in mapNode3)
                        {
                            if (smn1.Title == "Requisitions" || smn1.Url == "/UI/Benefits_Payment/td_requisitions.aspx")
                            {
                                smn1.ReadOnly = false;
                                smn1.Title = "Requisitions" + "(" + Convert.ToString(requisitionboList[0].OutStationRequisitionCount) + ")";
                            }
                            if (smn1.Title == "Ticket booking" || smn1.Url == "/UI/Benefits_Payment/ticket_booking_td.aspx")
                            {
                                smn1.ReadOnly = false;
                                smn1.Title = "Ticket booking" + "(" + Convert.ToString(requisitionboList[0].OutStationProposalsCount) + ")";
                            }
                        }
                    }
                }
                //-----------end-----------------Travel desk requisition and proposals count.--------------------------------------------------------------------------------------------------
            }

            // For normal employee(traveller)
            if (!Roles.IsUserInRole("Travel Desk") && !Roles.IsUserInRole("Training") && !Roles.IsUserInRole("PayrollAdmin") && !Roles.IsUserInRole("PR"))
            {
                if (iResult == 1)
                {
                    if (mapNode.Url.EndsWith("hod_requisition.aspx") || mapNode.Url.EndsWith("acc_proposals_hod.aspx") || mapNode.Url.EndsWith("hod_requisitions.aspx")
                        || mapNode.Url.EndsWith("hod_proposals.aspx") || mapNode.Url.EndsWith("acc_requisition_hod.aspx") || mapNode.Url.EndsWith("veh_proposals_hod.aspx")
                        || mapNode.Url.EndsWith("td_requisitions.aspx") || mapNode.Url.EndsWith("veh_requisitions_td.aspx") || mapNode.Url.EndsWith("acc_requisition_td.aspx")
                        || mapNode.Url.EndsWith("td_proposals.aspx") || mapNode.Url.EndsWith("ticket_booking_td.aspx") || mapNode.Url.EndsWith("acc_booking_td.aspx")
                        || mapNode.Url.EndsWith("veh_booking_td.aspx") || mapNode.Url.EndsWith("ticket_cancel_td.aspx") || mapNode.Url.EndsWith("hod_acc_vehi_req_pro.aspx")
                        || mapNode.Url.EndsWith("Requisition_Ticket_History.aspx") || mapNode.Url.EndsWith("travellers_feedback_View.aspx") || mapNode.Url.EndsWith("td_acc_vehi_pro.aspx")
                        || mapNode.Url.EndsWith("hod_Local_acc_vehi_req_pro.aspx") || mapNode.Url.EndsWith("td_local_acc_vehi_pro.aspx") || mapNode.Title == "Ticket Booking Acc/Vehi"
                        || mapNode.Url.EndsWith("td_Bill_Updates.aspx") || mapNode.Title == "Employee's Travel Profile"
                        || mapNode.Title == "Book Attendee" || mapNode.Title == "Update Attendee" || mapNode.Title == "Confirm / Reject Booking" || mapNode.Title == "Approve / Reject Booking" || mapNode.Title == "Payroll Management"
                        || mapNode.Title == "Approve claim request"
                        || mapNode.Title == "Approval for Claim requisitions"
                        || mapNode.Title == "PR Status for PT"
                        )
                    {
                        System.Web.UI.WebControls.MenuItem parent = e.Item.Parent;
                        if (parent != null)
                        {
                            parent.ChildItems.Remove(e.Item);
                        }
                    }
                }
                else
                {
                    if (mapNode.Url.EndsWith("acc_proposals_hod.aspx") || mapNode.Url.EndsWith("hod_requisitions.aspx")
                        || mapNode.Url.EndsWith("hod_proposals.aspx") || mapNode.Url.EndsWith("acc_requisition_hod.aspx") || mapNode.Url.EndsWith("veh_proposals_hod.aspx")
                        || mapNode.Url.EndsWith("td_requisitions.aspx") || mapNode.Url.EndsWith("veh_requisitions_td.aspx") || mapNode.Url.EndsWith("acc_requisition_td.aspx")
                        || mapNode.Url.EndsWith("td_proposals.aspx") || mapNode.Url.EndsWith("ticket_booking_td.aspx") || mapNode.Url.EndsWith("acc_booking_td.aspx")
                        || mapNode.Url.EndsWith("veh_booking_td.aspx") || mapNode.Url.EndsWith("ticket_cancel_td.aspx") || mapNode.Url.EndsWith("hod_acc_vehi_req_pro.aspx")
                        || mapNode.Url.EndsWith("Requisition_Ticket_History.aspx") || mapNode.Url.EndsWith("travellers_feedback_View.aspx") || mapNode.Url.EndsWith("td_acc_vehi_pro.aspx")
                        || mapNode.Url.EndsWith("hod_Local_acc_vehi_req_pro.aspx") || mapNode.Url.EndsWith("td_local_acc_vehi_pro.aspx") || mapNode.Title == "Ticket Booking Acc/Vehi"
                        || mapNode.Url.EndsWith("td_Bill_Updates.aspx") || mapNode.Title == "Employee's Travel Profile"
                        || mapNode.Title == "Book Attendee" || mapNode.Title == "Update Attendee" || mapNode.Title == "Confirm / Reject Booking" || mapNode.Title == "Payroll Management"
                        || mapNode.Title == "PR Status for PT")
                    {
                        System.Web.UI.WebControls.MenuItem parent = e.Item.Parent;
                        if (parent != null)
                        {
                            parent.ChildItems.Remove(e.Item);
                        }
                    }
                }
            }



            ////// For normal employee(trainee)
            ////if (!Roles.IsUserInRole("Training"))
            ////{
            ////    if (mapNode.Title == "Book Attendee" || mapNode.Title == "Update Attendee" || mapNode.Title == "Confirm Booking" || mapNode.Title == "Approve Booking")
            ////    {
            ////        System.Web.UI.WebControls.MenuItem parent = e.Item.Parent;
            ////        if (parent != null)
            ////        {
            ////            parent.ChildItems.Remove(e.Item);
            ////        }
            ////    }
            ////}


        }




        if (!Roles.IsUserInRole("Finance"))
        {
            System.Web.UI.WebControls.Menu menu = (System.Web.UI.WebControls.Menu)sender;
            SiteMapNode mapNode = (SiteMapNode)e.Item.DataItem;

            System.Web.UI.WebControls.MenuItem itemToRemove = menu.FindItem(mapNode.Title);

            if (mapNode.Title == "Finance View Claims Status" || mapNode.Title == "Finance View IExpense Status")
            {
                System.Web.UI.WebControls.MenuItem parent = e.Item.Parent;
                if (parent != null)
                {
                    parent.ChildItems.Remove(e.Item);
                }
            }
        }







        //--------------------Security USER-------------------------------------------------------------------------------------------------------------------------------------
        if (Roles.IsUserInRole("Security"))
        {
            System.Web.UI.WebControls.Menu menu = (System.Web.UI.WebControls.Menu)sender;
            SiteMapNode mapNode = (SiteMapNode)e.Item.DataItem;

            System.Web.UI.WebControls.MenuItem itemToRemove = menu.FindItem(mapNode.Title);

            if (mapNode.Title == "Configuration" || mapNode.Title == "DT wizard" || mapNode.Title == "Personal Information" || mapNode.Title == "Benefits and Payment"
                || mapNode.Title == "Field Staff" || mapNode.Title == "Manager Self Service" || mapNode.Title == "Travel Request" || mapNode.Title == "My account"
                || mapNode.Title == "Employee Performance" || mapNode.Title == "Local Vehicle Booking" || mapNode.Title == "Booking History" || mapNode.Title == "Payroll Management" || mapNode.Title == "Purchase Request")
            {
                System.Web.UI.WebControls.MenuItem parent = e.Item.Parent;
                if (parent != null)
                {
                    parent.ChildItems.Remove(e.Item);
                }
            }
        }
        //--------------------BPO USER-------------------------------------------------------------------------------------------------------------------------------------
        if (Roles.IsUserInRole("BPO"))
        {
            System.Web.UI.WebControls.Menu menu = (System.Web.UI.WebControls.Menu)sender;
            SiteMapNode mapNode = (SiteMapNode)e.Item.DataItem;

            System.Web.UI.WebControls.MenuItem itemToRemove = menu.FindItem(mapNode.Title);

            if (mapNode.Title == "Configuration" || mapNode.Title == "DT wizard" || mapNode.Title == "Personal Information" || mapNode.Title == "Benefits and Payment"
                || mapNode.Title == "Working Time" || mapNode.Title == "Manager Self Service" || mapNode.Title == "Travel Request" || mapNode.Title == "My account"
                || mapNode.Title == "Employee Performance" || mapNode.Title == "Local Vehicle Booking" || mapNode.Title == "Booking History" || mapNode.Title == "Payroll Management" || mapNode.Title == "Purchase Request")
            {
                System.Web.UI.WebControls.MenuItem parent = e.Item.Parent;
                if (parent != null)
                {
                    parent.ChildItems.Remove(e.Item);
                }
            }
        }


        //For training Co-Ordinator Role
        if (Roles.IsUserInRole("Training"))
        {
            System.Web.UI.WebControls.Menu menu = (System.Web.UI.WebControls.Menu)sender;
            SiteMapNode mapNode = (SiteMapNode)e.Item.DataItem;

            System.Web.UI.WebControls.MenuItem itemToRemove = menu.FindItem(mapNode.Title);



            if (mapNode.Title == "Configuration" || mapNode.Title == "DT wizard" || mapNode.Title == "Personal Information" || mapNode.Title == "Benefits and Payment"
                || mapNode.Title == "Working Time" || mapNode.Title == "Manager Self Service" || mapNode.Title == "Travel Request"
                || mapNode.Title == "Employee Performance" || mapNode.Title == "Local Vehicle Booking" || mapNode.Title == "Booking History" || mapNode.Title == "Field Staff" || mapNode.Title == "Travel Management"
                || mapNode.Title == "Book Training" || mapNode.Title == "Approve / Reject Booking" || mapNode.Title == "Payroll Management" || mapNode.Title == "Purchase Request")
            {
                System.Web.UI.WebControls.MenuItem parent = e.Item.Parent;
                if (parent != null)
                {
                    parent.ChildItems.Remove(e.Item);
                }
            }

        }




        //For Payroll Management Admin Role
        if (Roles.IsUserInRole("PayrollAdmin"))
        {
            System.Web.UI.WebControls.Menu menu = (System.Web.UI.WebControls.Menu)sender;
            SiteMapNode mapNode = (SiteMapNode)e.Item.DataItem;

            System.Web.UI.WebControls.MenuItem itemToRemove = menu.FindItem(mapNode.Title);



            if (mapNode.Title == "Configuration" || mapNode.Title == "DT wizard" || mapNode.Title == "Personal Information" || mapNode.Title == "Benefits and Payment"
                || mapNode.Title == "Working Time" || mapNode.Title == "Manager Self Service" || mapNode.Title == "Travel Request"
                || mapNode.Title == "Employee Performance" || mapNode.Title == "Local Vehicle Booking" || mapNode.Title == "Booking History" || mapNode.Title == "Field Staff" || mapNode.Title == "Travel Management"
                || mapNode.Title == "Book Training" || mapNode.Title == "Approve / Reject Booking" || mapNode.Title == "Training and Event Mgmt" || mapNode.Title == "Purchase Request")
            {
                System.Web.UI.WebControls.MenuItem parent = e.Item.Parent;
                if (parent != null)
                {
                    parent.ChildItems.Remove(e.Item);
                }
            }

        }




        //For training PR-Procurement Team
        if (Roles.IsUserInRole("PR"))
        {
            System.Web.UI.WebControls.Menu menu = (System.Web.UI.WebControls.Menu)sender;
            SiteMapNode mapNode = (SiteMapNode)e.Item.DataItem;

            System.Web.UI.WebControls.MenuItem itemToRemove = menu.FindItem(mapNode.Title);



            //if (mapNode.Title == "Configuration" || mapNode.Title == "DT wizard" || mapNode.Title == "Personal Information" || mapNode.Title == "Benefits and Payment"
            //    || mapNode.Title == "Working Time" || mapNode.Title == "Manager Self Service" || mapNode.Title == "Travel Request"
            //    || mapNode.Title == "Employee Performance" || mapNode.Title == "Local Vehicle Booking" || mapNode.Title == "Booking History" || mapNode.Title == "Field Staff" || mapNode.Title == "Travel Management"
            //    || mapNode.Title == "Book Training" || mapNode.Title == "Approve / Reject Booking" || mapNode.Title == "Payroll Management"
            //    || mapNode.Title == "Purchase Requisition" || mapNode.Title == "PR Status" || mapNode.Title == "PR Manager Approve / Reject"
            //    || mapNode.Title == "View PR Details")
            //{
            //    System.Web.UI.WebControls.MenuItem parent = e.Item.Parent;
            //    if (parent != null)
            //    {
            //        parent.ChildItems.Remove(e.Item);
            //    }
            //}

            if (mapNode.Title == "Configuration" || mapNode.Title == "DT wizard"
            || mapNode.Title == "Employee Performance" || mapNode.Title == "Local Vehicle Booking" || mapNode.Title == "Booking History" || mapNode.Title == "Field Staff"
            || mapNode.Title == "Book Training" || mapNode.Title == "Approve / Reject Booking" || mapNode.Title == "Payroll Management"
           )
            {
                System.Web.UI.WebControls.MenuItem parent = e.Item.Parent;
                if (parent != null)
                {
                    parent.ChildItems.Remove(e.Item);
                }
            }




        }







        //--------------------Transport USER-------------------------------------------------------------------------------------------------------------------------------------
        if (Roles.IsUserInRole("Transport"))
        {
            System.Web.UI.WebControls.Menu menu = (System.Web.UI.WebControls.Menu)sender;
            SiteMapNode mapNode = (SiteMapNode)e.Item.DataItem;

            System.Web.UI.WebControls.MenuItem itemToRemove = menu.FindItem(mapNode.Title);

            if (mapNode.Title == "Configuration" || mapNode.Title == "DT wizard" || mapNode.Title == "Personal Information" || mapNode.Title == "Benefits and Payment"
                || mapNode.Title == "Working Time" || mapNode.Title == "Manager Self Service" || mapNode.Title == "Travel Request" || mapNode.Title == "My account"
                 || mapNode.Title == "Field Staff" || mapNode.Title == "Employee Performance" || mapNode.Title == "Payroll Management" || mapNode.Title == "Purchase Request")
            {
                System.Web.UI.WebControls.MenuItem parent = e.Item.Parent;
                if (parent != null)
                {
                    parent.ChildItems.Remove(e.Item);
                }
            }
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        CheckSessionStatus();
    }

    protected void CheckSessionStatus()
    {
        if (Context.Session != null)
        {
            if (Session.IsNewSession)
            {
                HttpCookie newSessionIdCookie = Request.Cookies["ASP.NET_SessionId"];
                if (newSessionIdCookie != null)
                {
                    string newSessionIdCookieValue = newSessionIdCookie.Value;
                    if (newSessionIdCookieValue != string.Empty)
                    {
                        // This means Session was timed Out and New Session was started 
                        //Response.Redirect("~/sessionout.aspx", false);
                        Response.Redirect("~/Account/Login.aspx", false);
                    }
                }
            }
        }
    }
    protected void imgbtnDirectory_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/UI/Manager_Self_Service/who'swho.aspx", false);
    }
}
