<%@ Page Title="PR" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="PR.aspx.cs" Inherits="iEmpPower.UI.PR.PR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div>
<h2>Purchase Request</h2>
<p></p>
 <span class="hidden"><asp:Button ID="btnEntryKey" runat="server" Text=""  /></span> 
<div class="list-box">
         
          
          <asp:LinkButton ID="LinkButton2" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/PR/Purchase_Request.aspx">Purchase Request</asp:LinkButton><br /> 
          
            <asp:LinkButton ID="LinkButton25" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/PR/PR_Status.aspx">PR Status</asp:LinkButton><br />
          <asp:LinkButton ID="LinkButton26" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/PR/PR_ManagerAppRej.aspx">PR Approve Reject</asp:LinkButton><br />
         
          </div>
</div>

</asp:Content>
