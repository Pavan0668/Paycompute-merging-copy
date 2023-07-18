<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Performance_Mngmt_System.aspx.cs"
     Inherits="iEmpPower.UI.Performance_Management_System.Performance_Mngmt_System" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>

        <h2>Performance Management System</h2>
<p></p>
 <span class="hidden"><asp:Button ID="btnEntryKey" runat="server" Text=""  /></span> 
         <div class="list-box">
           <asp:LinkButton ID="LinkButton2" runat="server"  CssClass="link-header" 
            PostBackUrl="~/UI/Performance_Management_System/frmPMSDownload.aspx">Download Appraisal Form</asp:LinkButton><br /> 
          
            <asp:LinkButton ID="LinkButton25" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/Performance_Management_System/frmPMSUpload.aspx">Upload Appraisal Form</asp:LinkButton><br />

             
              </div>
          

    </div>
</asp:Content>
