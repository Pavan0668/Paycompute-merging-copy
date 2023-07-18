<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="ITDeclarations.aspx.cs" 
    Inherits="iEmpPower.UI.IT.ITDeclarations" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div>
<h2>Income Tax Declaration</h2>
<p></p>
 <span class="hidden"><asp:Button ID="btnEntryKey" runat="server" Text=""  /></span> 
<div class="list-box">
         
          <div id="EmpDiv" runat="server">
          <asp:LinkButton ID="ITSec80" runat="server"  CssClass="link-header"
                  PostBackUrl="~/UI/IT/IT_Declaration.aspx">IT Declaration Section 80</asp:LinkButton><br /> 
     <asp:LinkButton ID="ITSec80C" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/IT/IT_Declaration80C.aspx">IT Declaration Section 80 C</asp:LinkButton><br /> 
     <asp:LinkButton ID="ITHousing" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/IT/IT_DeclarationHousing.aspx">IT Declaration Housing</asp:LinkButton><br /> 
     
     <asp:LinkButton ID="ITOthers" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/IT/IT_IncomeOtherSources.aspx">IT Declaration Income from Other Sources</asp:LinkButton><br /> 
     
     <asp:LinkButton ID="ITEmpView" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/IT/IT_EmpViewHistory.aspx">Employee View IT History</asp:LinkButton><br /> 
     </div>

    <div id="FinanceDiv" runat="server">
       <asp:LinkButton ID="ITAdminLock" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/IT/IT_AdminLocking.aspx">Admin IT Lock</asp:LinkButton><br /> 
     
     
      <asp:LinkButton ID="ITAppRej" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/IT/IT_AppRej.aspx">IT Admin Approve Reject</asp:LinkButton><br /> 
     
     
      <asp:LinkButton ID="ITAdminView" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/IT/IT_AdminViewAll.aspx">IT Admin View All History</asp:LinkButton><br /> 
     
     </div>
         
          </div>
</div>


</asp:Content>
