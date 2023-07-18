<%@ Page Title="iEmpPower - Sitemap" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="sitemap" Codebehind="sitemap.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>

<h2>
        Sitemap
    </h2>
    
    <div class="link-block" >
         <div style="padding-top: 10px; padding-bottom: 10px"> 
         <asp:LinkButton ID="LinkButton11" runat="server"  CssClass="link-header" PostBackUrl="~/Default.aspx"><b>Home</b></asp:LinkButton><br /><br />
                <asp:LinkButton ID="LinkButton12" runat="server"  CssClass="link-header" PostBackUrl="~/UI/Configuration/Configuration.aspx"><b>Configuration</b></asp:LinkButton><br /> 
         </div>

          <div class="list-box">
           <h4><b>Personal Information</b></h4>
          <asp:LinkButton ID="LinkButton2" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/Personal_Information/address_information.aspx">Address Information</asp:LinkButton><br /> 
          
            <asp:LinkButton ID="LinkButton25" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/Personal_Information/bank_information.aspx">Bank Information</asp:LinkButton><br />
          <asp:LinkButton ID="LinkButton26" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/Personal_Information/communication_information.aspx">Communication Information</asp:LinkButton><br />
          <asp:LinkButton ID="LinkButton27" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/Personal_Information/family_members.aspx">Family members / Dependants</asp:LinkButton><br />
          <asp:LinkButton ID="LinkButton3"  CssClass="link-header" runat="server" 
                  PostBackUrl="~/UI/Personal_Information/personal_data.aspx">Personal Data</asp:LinkButton><br />
          <asp:LinkButton ID="LinkButton7" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/Personal_Information/personal_ids.aspx">Personal IDs</asp:LinkButton><br />
          <asp:LinkButton ID="LinkButton8" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/Manager_Self_Service/addressproof.aspx">Address proof</asp:LinkButton><br />
          <asp:LinkButton ID="LinkButton9" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/Manager_Self_Service/employmentproof.aspx">Employment proof</asp:LinkButton><br />
         
          </div>
          


          <div class="list-box">
           <h4>Working Time</h4>
                  
          <asp:LinkButton ID="LinkButton15" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/Working_Time/clockinout_correction.aspx">Clock In / Out correction</asp:LinkButton><br />
          <asp:LinkButton ID="LinkButton16" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/Working_Time/recordworking_time.aspx">Record Working Time</asp:LinkButton><br />
          <asp:LinkButton ID="LinkButton17" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/Working_Time/timestatement.aspx">Time Statements</asp:LinkButton><br />
          <asp:LinkButton ID="LinkButton18"  CssClass="link-header" runat="server" 
                  PostBackUrl="~/UI/Working_Time/payslip.aspx">Payslip</asp:LinkButton><br />
          <asp:LinkButton ID="LinkButton19" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/Working_Time/leaverequest.aspx">Leave Request</asp:LinkButton><br />
          
          <h4>Benefits Payment</h4>
                  
         
          <asp:LinkButton ID="LinkButton14"  CssClass="link-header" runat="server" 
                  PostBackUrl="~/UI/Working_Time/payslip.aspx">Payslip</asp:LinkButton><br />
          <br />
         
          </div>

            
          <div class="list-box">
             <h4>Performance Appraisal</h4>
          <%--   <asp:LinkButton ID="LinkButton20" runat="server"  CssClass="link-header" PostBackUrl="~/UI/Performance_Management_System/PMS.aspx">Appraisal</asp:LinkButton><br />--%>
        <%--  <asp:LinkButton ID="LinkButton20" runat="server"  CssClass="link-header" PostBackUrl="~/UI/Performance_Management_System/appraisal_form.aspx">Self Appraisal</asp:LinkButton><br />
          <asp:LinkButton ID="LinkButton21" runat="server"  CssClass="link-header" PostBackUrl="~/UI/Performance_Management_System/appraisal_review_form.aspx">Appraisal review</asp:LinkButton><br />
      --%>   <%-- <asp:LinkButton ID="LinkButton13" runat="server"  CssClass="link-header">G and O Settings</asp:LinkButton><br />
          <asp:LinkButton ID="LinkButton14" runat="server"  CssClass="link-header">Level 1 Appraisal review (Revert support)</asp:LinkButton><br />
          <asp:LinkButton ID="LinkButton20" runat="server"  CssClass="link-header">Level 2 Appraisal review (Revert support)</asp:LinkButton><br />
          <asp:LinkButton ID="LinkButton21" runat="server"  CssClass="link-header">HR Manager review (Revert support)</asp:LinkButton><br />
          <asp:LinkButton ID="LinkButton22" runat="server"  CssClass="link-header">Competency configuration</asp:LinkButton><br />
          <asp:LinkButton ID="LinkButton23" runat="server"  CssClass="link-header">Competency mapping</asp:LinkButton><br />--%>
          </div>
                  
          <%--<div class="list-box">
             <h4>Employee Performance</h4>
          <asp:LinkButton ID="LinkButton4" runat="server"  CssClass="link-header">G and O Settings</asp:LinkButton><br />
          <asp:LinkButton ID="LinkButton5" runat="server"  CssClass="link-header">Self appraisal</asp:LinkButton><br />
          <asp:LinkButton ID="LinkButton13" runat="server"  CssClass="link-header">G and O Settings</asp:LinkButton><br />
          <asp:LinkButton ID="LinkButton14" runat="server"  CssClass="link-header">Level 1 Appraisal review (Revert support)</asp:LinkButton><br />
          <asp:LinkButton ID="LinkButton20" runat="server"  CssClass="link-header">Level 2 Appraisal review (Revert support)</asp:LinkButton><br />
          <asp:LinkButton ID="LinkButton21" runat="server"  CssClass="link-header">HR Manager review (Revert support)</asp:LinkButton><br />
          <asp:LinkButton ID="LinkButton22" runat="server"  CssClass="link-header">Competency configuration</asp:LinkButton><br />
          <asp:LinkButton ID="LinkButton23" runat="server"  CssClass="link-header">Competency mapping</asp:LinkButton><br />
          </div>--%>

          <div class="list-box">
          <h4>Manager Self Service</h4>
          
          <asp:LinkButton ID="LinkButton24" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/Manager_Self_Service/assignedtome.aspx" >Assigned To Me</asp:LinkButton><br /> 
          
            <asp:LinkButton ID="LinkButton28" runat="server"  CssClass="link-header">Assigned To My Role</asp:LinkButton><br />
          
          
         
        
           <h4>My account</h4>
          <asp:LinkButton ID="LinkButton1" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/UserAccount/createuseraccount.aspx" >Maintain User Account</asp:LinkButton><br /> 
          
            <asp:LinkButton ID="LinkButton6" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/UserAccount/changepassword.aspx">Change password</asp:LinkButton><br />
          <asp:LinkButton ID="LinkButton10" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/UserAccount/createquestionandanswer.aspx">Account Security Question</asp:LinkButton><br />
             <asp:LinkButton ID="LinkButton4" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/Configuration/Login_Details.aspx">Employee Activity</asp:LinkButton><br />
                    <asp:LinkButton ID="LinkButton5" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/Benefits_Payment/kannadaPayslip.aspx">Kannada Payslip</asp:LinkButton><br />

         <br />
          </div>
          



</div>
</div>
</asp:Content>

