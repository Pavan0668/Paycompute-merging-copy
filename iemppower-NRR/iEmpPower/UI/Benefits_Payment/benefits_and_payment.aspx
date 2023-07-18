<%@ Page Title="Benefits and Payment" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="UI_Benefits_Payment_benefits_and_payment" Codebehind="benefits_and_payment.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div>
<h2>Benefits and Payment</h2>
<p>You can generate payslip here</p>
 <span class="hidden"><asp:Button ID="btnEntryKey" runat="server" Text=""  /></span> 
<div class="list-box">
                  
          <asp:LinkButton ID="lnkPayslip" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/Benefits_Payment/PaySlipNew.aspx" >Payslip</asp:LinkButton><br />
         <%-- <asp:LinkButton ID="lnkClaimRequest" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/Benefits_Payment/form16.aspx" >Form 16</asp:LinkButton><br />
          <asp:LinkButton ID="lnkLeaveEncashment" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/Benefits_Payment/leave_encashment.aspx" >Leave Encashment</asp:LinkButton><br />--%>
          <%--<asp:LinkButton ID="lnkClaimStatement" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/Benefits_Payment/reimbursement-stmnt.aspx">Reimbursement Statement</asp:LinkButton><br />--%>
          <%--  <asp:LinkButton ID="lnkTravelRequest" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/Benefits_Payment/travel_request.aspx" >Travel Request</asp:LinkButton><br />--%>
            <%--<asp:LinkButton ID="lnkTravelExpenses" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/Benefits_Payment/travel_expenses.aspx" >Travel Expenses Report</asp:LinkButton><br />--%>
            <br />
          
                    
         <br />
          </div>
</div>
</asp:Content>

