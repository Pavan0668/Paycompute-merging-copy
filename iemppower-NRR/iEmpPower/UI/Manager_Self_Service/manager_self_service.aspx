<%@ Page Title="Manager Self Service" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="UI_Manager_Self_Service_manager_self_service" 
    Codebehind="manager_self_service.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div>
<h2>Manager Self Service</h2>
<p>Manage approval/rejection of requests/changes made by subordinates.
</p>
 <span class="hidden"><asp:Button ID="btnEntryKey" runat="server" Text=""  /></span> 
<div class="list-box">
                  
          <asp:LinkButton ID="LinkButton24" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/Manager_Self_Service/assignedtome.aspx" >Assigned To Me</asp:LinkButton><br /> 
          
        <%--    <asp:LinkButton ID="LinkButton28" runat="server"  CssClass="link-header"
                
              PostBackUrl="~/UI/Manager_Self_Service/assignedtorole.aspx">Assigned To My Role</asp:LinkButton><br />--%>

      <asp:LinkButton ID="LinkButton1" runat="server"  CssClass="link-header"
                
              PostBackUrl="~/UI/Manager_Self_Service/TimeSheetReview.aspx">TimeSheet Review</asp:LinkButton><br />
          
                    
         <br />
          </div>
</div>
</asp:Content>

