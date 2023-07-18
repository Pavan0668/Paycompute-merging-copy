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
              <%--  <asp:LinkButton ID="LinkButton12" runat="server"  CssClass="link-header" PostBackUrl="~/UI/Configuration/Configuration.aspx"><b>Configuration</b></asp:LinkButton><br /> --%>
         <a href="~/UI/Configuration/Configuration.aspx" runat="server" class="link-header"><b>Configuration</b></a>
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
                  
        
          <asp:LinkButton ID="LinkButton19" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/Working_Time/leaverequest.aspx">Leave Request</asp:LinkButton><br />
          <asp:LinkButton ID="LinkButton16" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/Working_Time/recordworking_time.aspx">Record Working Time</asp:LinkButton><br />
         <asp:LinkButton ID="LinkButton13" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/Manager_Self_Service/TimeSheetReview_Employees.aspx">TimeSheet Overview</asp:LinkButton><br /> </div>
          
         <div class="list-box">
          <h4>Benefits Payment</h4>
                  
         
           <asp:LinkButton ID="lnkPayslip" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/Benefits_Payment/PaySlipNew.aspx" >Payslip</asp:LinkButton><br />
              <asp:LinkButton ID="lnkCTC" runat="server"  CssClass="link-header" Visible="false"
                  PostBackUrl="~/UI/Benefits_Payment/Ctc.aspx" >CTC</asp:LinkButton><br />
          <br />
         
          </div>
            
        

          <div class="list-box">
          <h4>Manager Self Service</h4>
          
          <asp:LinkButton ID="LinkButton24" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/Manager_Self_Service/assignedtome.aspx" >Assigned To Me</asp:LinkButton><br /> 
          
           <asp:LinkButton ID="LinkButton12" runat="server"  CssClass="link-header"
                
              PostBackUrl="~/UI/Manager_Self_Service/TimeSheetReview.aspx">TimeSheet Review</asp:LinkButton><br />
                  


           <h4>My account</h4>
          <asp:LinkButton ID="Lbtn_MaintainUsrAcc" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/UserAccount/createuseraccount.aspx" >Maintain User Account</asp:LinkButton><br /> 
           <asp:LinkButton ID="Lbtn_ResetEmpPswd" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/UserAccount/ResetEmployeePassword.aspx">Reset Employee Password</asp:LinkButton><br />

          
            <asp:LinkButton ID="Lbtn_ChangePswd" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/UserAccount/changepassword.aspx">Change password</asp:LinkButton><br />

       <asp:LinkButton ID="Lbtn_AccSecurityQue" runat="server"  CssClass="link-header" 
                  PostBackUrl="~/UI/UserAccount/createquestionandanswer.aspx">Account Security Question</asp:LinkButton><br />

 
        <asp:LinkButton ID="Lbtn_Directory" runat="server"  CssClass="link-header"
                
              PostBackUrl="~/UI/Manager_Self_Service/who'swho.aspx">Directory</asp:LinkButton><br />

         <br />
          </div>
          



</div>
</div>
</asp:Content>

