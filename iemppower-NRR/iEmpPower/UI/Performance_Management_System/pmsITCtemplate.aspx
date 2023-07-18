<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="pmsITCtemplate.aspx.cs" Inherits="UI_Performance_Management_System_pmsITCtemplate" meta:resourcekey="PageResource1" uiculture="auto" Theme="SkinFile"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:Button ID="btnBack" runat="server" Text="Back To Template List"/>
    <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="javascript:window.print();" /><br />

  <h2>Performance Appraisal</h2>
  <asp:Label ID="lblMessageBoard" runat="server"  CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1"></asp:Label>
  
  <br />

  <asp:Panel ID="pnlAppraisal" runat="server" BorderColor="Black" BorderStyle="Groove" BorderWidth="1px" Width="800px">

  <div>
     <div>
      <asp:Label ID="lblReviewPeriod" runat="server" Text="Review Period : " CssClass="label"></asp:Label>
      <asp:Label ID="lblReviewPeriodData" runat="server" Text="" CssClass="label"></asp:Label>
    </div>
    <div>
      <asp:Label ID="lblEmployeeName" runat="server" Text="Employee Name : " CssClass="label"></asp:Label>
      <asp:Label ID="lblEmployeeNameData" runat="server" Text="" CssClass="label"></asp:Label>
    </div>
      <div>
          <asp:Label ID="lblEmployeeNo" runat="server" Text="Employee No. : " CssClass="label"></asp:Label>
          <asp:Label ID="lblEmployeeNoData" runat="server" Text="" CssClass="label"></asp:Label>
      </div>
     <div>
          <asp:Label ID="lblDesignation" runat="server" Text="Designation : " CssClass="label"></asp:Label>
          <asp:Label ID="lblDesignationData" runat="server" Text="" CssClass="label"></asp:Label>
      </div>
     <div>
          <asp:Label ID="lblCompetency" runat="server" Text="Competency : " CssClass="label"></asp:Label>
          <asp:Label ID="lblCompetencyData" runat="server" Text="" CssClass="label"></asp:Label>
      </div>
     <div>
          <asp:Label ID="lblLocation" runat="server" Text="Location : " CssClass="label"></asp:Label>
          <asp:Label ID="lblLocationData" runat="server" Text="" CssClass="label"></asp:Label>
      </div>
     <div>
          <asp:Label ID="lblDateOfJoining" runat="server" Text="Date Of Joining : " CssClass="label"></asp:Label>
          <asp:Label ID="lblDateOfJoiningData" runat="server" Text="" CssClass="label"></asp:Label>
      </div>
      <hr />
  </div>

  <div>
    <h3>Academic Qualifications:</h3>
      <div>
          <asp:Label ID="lblGraduation" runat="server" Text="Graduation 1 : " CssClass="label"></asp:Label>
          <asp:Label ID="lblGraduationData" runat="server" Text="" CssClass="label"></asp:Label>
      </div>
      <hr />
  </div>
  <div>
       <h3>Prior Experience to ITChamps:</h3>
       <div>
          <asp:Label ID="lblsapExperience" runat="server" Text="SAP Experience : " CssClass="label"></asp:Label>
          <asp:DropDownList ID="drpdwnsapYears" runat="server" CssClass="textbox" Width="50px"></asp:DropDownList>
          <asp:Label ID="Label1" runat="server" Text="Years"></asp:Label>
          <asp:DropDownList ID="drpdwnsapMonths" runat="server" CssClass="textbox" Width="50px"></asp:DropDownList>
          <asp:Label ID="Label2" runat="server" Text="Months"></asp:Label>
          <asp:Label ID="lblnonsapExperience" runat="server" Text="Non-SAP Experience" CssClass="label"></asp:Label>
          <asp:DropDownList ID="drpdwnnonsapYears" runat="server" CssClass="textbox" Width="50px"></asp:DropDownList>
          <asp:Label ID="Label3" runat="server" Text="Years"></asp:Label>
          <asp:DropDownList ID="drpdwnnonsapMonths" runat="server" CssClass="textbox" Width="50px"></asp:DropDownList>
          <asp:Label ID="Label4" runat="server" Text="Months"></asp:Label>
      </div>
     <hr />
  </div>

   <div>
       <div >
          <asp:Label ID="lblSkillExpertise" runat="server" Text="Skills/Expertise : " CssClass="label"></asp:Label><br />
          <asp:TextBox ID="txtSkillExpertise" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="600px" Height="150px"></asp:TextBox>
      </div>
     <div>
         <asp:Label ID="lblProjectsAssign" runat="server" Text="Projects Assigned during the Review Period: : " CssClass="labelAppr"></asp:Label><br />
          <asp:TextBox ID="txtProjectsAssign" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="600px" Height="150px"></asp:TextBox>
      </div>
     <hr />
  </div>

  <div>
   <div>
      <asp:Label ID="lblAppraiserName" runat="server" Text="Appraiser Name : " CssClass="label"></asp:Label>
      <asp:Label ID="lblAppraiserNameData" runat="server" Text="" CssClass="label"></asp:Label>
    </div>
    <div>
      <asp:Label ID="lblAppraiserDesig" runat="server" Text="Employee Name : " CssClass="label"></asp:Label>
      <asp:Label ID="lblAppraiserDesigData" runat="server" Text="" CssClass="label"></asp:Label>
    </div>
      <div>
          <asp:Label ID="lblStatus" runat="server" Text="Status : " CssClass="label"></asp:Label>
          <asp:Label ID="lblStatusData" runat="server" Text="" CssClass="label"></asp:Label>
      </div>
     <div>
          <asp:Label ID="lblSubStatus" runat="server" Text="Sub Status : " CssClass="label"></asp:Label>
          <asp:Label ID="lblsubStatusData" runat="server" Text="" CssClass="label"></asp:Label>
      </div>
      <hr />
  </div>

   <div>
  <h3>Goal Setting and Performance Evaluation</h3>
      <asp:Gridview ID="grdGoalsPerf"  runat="server"  ShowFooter="True"  width="800px" AutoGenerateColumns="False" style="margin-right: 0px">
      <Columns>
         <asp:BoundField DataField="Key_Result_Area" HeaderText="Key Result Area" />
         <asp:TemplateField HeaderText="Measure Indicators">
             <ItemTemplate>
                     <asp:TextBox ID="txtMeasure" runat="server" CssClass="textboxApr" Height="20px"></asp:TextBox>
             </ItemTemplate>         
         </asp:TemplateField>

         <asp:TemplateField HeaderText="Weightage %">
             <ItemTemplate>
                     <asp:TextBox ID="txtWeightage" runat="server" CssClass="textboxApr" Height="20px"></asp:TextBox>
             </ItemTemplate>         
         </asp:TemplateField>
      </Columns> 
      </asp:Gridview>
  </div>


  <div>
  <h3>Quarter 1:</h3>
       <asp:Label ID="Label5" runat="server" Text="Employee Comments : " CssClass="labelAppr"></asp:Label><br />
       <asp:TextBox ID="txtEmpCommentsQ1" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="600px" Height="150px"></asp:TextBox>
   
       <asp:Label ID="Label6" runat="server" Text="Manager Comments : " CssClass="labelAppr"></asp:Label><br />
       <asp:TextBox ID="txtMangCommentsQ1" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="600px" Height="150px"></asp:TextBox>
   <hr />
   <h3>Quarter 2:</h3>
       <asp:Label ID="Label7" runat="server" Text="Employee Comments : " CssClass="labelAppr"></asp:Label><br />
       <asp:TextBox ID="txtEmpCommentsQ2" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="600px" Height="150px"></asp:TextBox>
   
       <asp:Label ID="Label8" runat="server" Text="Manager Comments : " CssClass="labelAppr"></asp:Label><br />
       <asp:TextBox ID="txtMangCommentsQ2" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="600px" Height="150px"></asp:TextBox>
   <hr />
   <h3>Quarter 3:</h3>
       <asp:Label ID="Label9" runat="server" Text="Employee Comments : " CssClass="labelAppr"></asp:Label><br />
       <asp:TextBox ID="txtEmpCommentsQ3" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="600px" Height="150px"></asp:TextBox>
   
       <asp:Label ID="Label10" runat="server" Text="Manager Comments : " CssClass="labelAppr"></asp:Label><br />
       <asp:TextBox ID="txtMangCommentsQ3" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="600px" Height="150px"></asp:TextBox>
   <hr />
   <h3>Quarter 4:</h3>
       <asp:Label ID="Label11" runat="server" Text="Employee Comments : " CssClass="labelAppr"></asp:Label><br />
       <asp:TextBox ID="txtEmpCommentsQ4" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="600px" Height="150px"></asp:TextBox>
   
       <asp:Label ID="Label12" runat="server" Text="Manager Comments : " CssClass="labelAppr"></asp:Label><br />
       <asp:TextBox ID="txtMangCommentsQ4" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="600px" Height="150px"></asp:TextBox>
   <hr />
  </div>

  <div>
  <h3>Development & Training Needs :</h3>
     
     <asp:Gridview ID="grdDevTraing"  runat="server"  ShowFooter="True"  width="800px" AutoGenerateColumns="False" style="margin-right: 0px"> 
         <Columns> 
             <asp:BoundField DataField="Sl No." HeaderText="Sl No." />
             <asp:TemplateField HeaderText="Development/Training Needs">
                 <ItemTemplate>
                     <asp:TextBox ID="txtDevelopQ1" runat="server" CssClass="textboxApr" 
                         Height="50px" TextMode="MultiLine" Width="175px"></asp:TextBox>
                 </ItemTemplate>
                 <%--  <ItemTemplate>
                     <asp:TextBox ID="txtDevelopQ2" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="175px" Height="50px"></asp:TextBox>                
                </ItemTemplate>
                <ItemTemplate>
                     <asp:TextBox ID="txtDevelopQ3" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="175px" Height="50px"></asp:TextBox>              
                </ItemTemplate>
                <ItemTemplate>
                     <asp:TextBox ID="txtDevelopQ4" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="175px" Height="50px"></asp:TextBox>                
                </ItemTemplate>--%></asp:TemplateField>
             <asp:TemplateField HeaderText="Action Taken">
                 <ItemTemplate>
                     <asp:TextBox ID="txtActionQ1" runat="server" CssClass="textboxApr" 
                         Height="50px" TextMode="MultiLine" Width="175px"></asp:TextBox>
                 </ItemTemplate>
                 <%-- <ItemTemplate>
                     <asp:TextBox ID="txtActionQ2" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="175px" Height="50px"></asp:TextBox>
                </ItemTemplate>
                <ItemTemplate>
                     <asp:TextBox ID="txtActionQ3" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="175px" Height="50px"></asp:TextBox>
                </ItemTemplate>
                <ItemTemplate>
                     <asp:TextBox ID="txtActionQ4" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="175px" Height="50px"></asp:TextBox> 
                </ItemTemplate>--%></asp:TemplateField>
             <asp:TemplateField HeaderText="Timeline Specified">
                 <ItemTemplate>
                     <asp:TextBox ID="txtTimelineQ1" runat="server" CssClass="textboxApr" 
                         Height="50px" TextMode="MultiLine" Width="175px"></asp:TextBox>
                 </ItemTemplate>
                 <%--  <ItemTemplate>   
                     <asp:TextBox ID="txtTimelineQ2" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="175px" Height="50px"></asp:TextBox>             
                </ItemTemplate>
                 <ItemTemplate> 
                     <asp:TextBox ID="txtTimelineQ3" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="175px" Height="50px"></asp:TextBox>               
                </ItemTemplate>
                <ItemTemplate>   
                     <asp:TextBox ID="txtTimelineQ4" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="175px" Height="50px"></asp:TextBox>              
                </ItemTemplate>   --%></asp:TemplateField>
             <asp:TemplateField HeaderText="Evaluation &amp; Rating">
                 <ItemTemplate>
                     <asp:TextBox ID="txtEvaluationQ1" runat="server" CssClass="textboxApr" 
                         Height="50px" TextMode="MultiLine" Width="175px"></asp:TextBox>
                 </ItemTemplate>
                 <%--  <ItemTemplate>
                     <asp:TextBox ID="txtEvaluationQ2" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="175px" Height="50px"></asp:TextBox>
                </ItemTemplate>
                <ItemTemplate>
                     <asp:TextBox ID="txtEvaluationQ3" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="175px" Height="50px"></asp:TextBox>
                </ItemTemplate>
                <ItemTemplate>
                     <asp:TextBox ID="txtEvaluationQ4" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="175px" Height="50px"></asp:TextBox> 
                </ItemTemplate>--%></asp:TemplateField>

        </Columns>
    </asp:Gridview>
     <hr />
  </div>

  <div>
  <h3>Final Comments:</h3>
       <asp:Label ID="Label13" runat="server" Text="HOD Comments : " CssClass="labelAppr"></asp:Label><br />
       <asp:TextBox ID="txtFhodcommnts" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="600px" Height="150px"></asp:TextBox>
   
       <asp:Label ID="Label14" runat="server" Text="HR Comments : " CssClass="labelAppr"></asp:Label><br />
       <asp:TextBox ID="txtFhrcommnts" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="600px" Height="150px"></asp:TextBox>
   <hr />
   </div>

</asp:Panel>

 <div class="buttonrow">                           
    <asp:Button ID="btnSave" runat="server" Text="Save" /> 
    <asp:Button ID="btnSaveAndSend" runat="server" Text="Save &amp; Send" />
     <asp:Button ID="btnApprove" runat="server" Text="Approve " visible="False" />
    <asp:Button ID="btnCancel" runat="server" Text="Cancel"/>
</div><br />

</asp:Content>

