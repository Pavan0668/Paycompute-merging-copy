<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="appraisal_review_form.aspx.cs" Inherits="UI_Performance_Management_System_appraisal_review_form" meta:resourcekey="PageResource1" uiculture="auto" Theme="SkinFile"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<script type = "text/javascript">
    function PrintPanel() {
        var panel = document.getElementById("<%=pnlAppraisalReview.ClientID %>");
        var printWindow = window.open('', 'pnlAppraisalReview', 'left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1,font-size=2');
        printWindow.document.write('<html><head><title>DIV Contents</title>');
        printWindow.document.write('</head><body >');
        printWindow.document.write(panel.innerHTML);
        printWindow.document.write('</body></html>');
        printWindow.document.close();
        setTimeout(function () {
            printWindow.print();
        }, 500);
        return false;
    }
    </script>

        <asp:Button ID="btnBack" runat="server" Text="Back To Template List" onclick="btnBack_Click" /> 
        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="return PrintPanel();" /><br />

    <h2>Appraisal Review</h2>
  <asp:Label ID="lblMessageBoard" runat="server"  CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1"></asp:Label>
 
<asp:Panel ID="pnlAppraisalReview" runat="server" BorderColor="Black" BorderStyle="Groove" BorderWidth="1px">
    <br />
  <div>
      <asp:Label ID="lblPerformance" runat="server" Text="Performance appraisal for the year : " CssClass="label"></asp:Label>
      <asp:Label ID="lblPerformanceData" runat="server" Text="2013" CssClass="label"></asp:Label>
      <br />
      <asp:Label ID="lblEmployeeName" runat="server" Text="Name of the Employee : " CssClass="label"></asp:Label>
      <asp:Label ID="lblEmployeeNameData" runat="server" Text="SHRUTHI N" CssClass="label"></asp:Label>
       <br />
      <asp:Label ID="lblPernr" runat="server" Text="Employee Code : " CssClass="label"></asp:Label>
      <asp:Label ID="lblPernrData" runat="server" Text="00000103" CssClass="label"></asp:Label>
      <br />
      <asp:Label ID="Label1" runat="server" Text="Designation : " CssClass="label"></asp:Label>
      <asp:Label ID="lblDesignation" runat="server" Text="SOFTWARE ENGINEER" CssClass="label"></asp:Label>
       <br />
       <asp:Label ID="Label2" runat="server" Text="Department : " CssClass="label"></asp:Label>
      <asp:Label ID="lblDepartment" runat="server" Text="IT" CssClass="label"></asp:Label>
       <br />
      <asp:Label ID="Label3" runat="server" Text="Location : " CssClass="label"></asp:Label>
      <asp:Label ID="lblLocation" runat="server" Text="MYSORE" CssClass="label"></asp:Label>
       <br />
       <asp:Label ID="Label4" runat="server" Text="Service Start Date : " CssClass="label"></asp:Label>
      <asp:Label ID="lblServiceDate" runat="server" Text="27/MAY/2013" CssClass="label"></asp:Label>
       <br />
      <asp:Label ID="Label5" runat="server" Text="Name of the Appraiser : " CssClass="label"></asp:Label>
      <asp:Label ID="lblAppraiserName" runat="server" Text="RAJESH KUTNIKAR" CssClass="label"></asp:Label>
       <br />
       <asp:Label ID="Label6" runat="server" Text="Appraiser Designation : " CssClass="label"></asp:Label>
      <asp:Label ID="lblAppraiserDesignation" runat="server" Text="DIRECTOR" CssClass="label"></asp:Label>
      <hr />
      </div>

      <div class="divAppr">
      <p><b>IMPORTANT INSTRUCTIONS:</b> <br />
1. The Performance Review is uniformly applicable to all i.e., Managers, Executives, Officers and other staff members.<br />
2. The objectives of this exercise are <br />
&nbsp;&nbsp;&nbsp;
1) To review performance on critical factors which has direct bearing on quality of performance <br />
&nbsp;&nbsp;&nbsp;
2) To communicate expectations to the appraisee, <br />
&nbsp;&nbsp;&nbsp;
3) To clarify functional responsibilities & enable to set goals for the current performance year. <br />
3. There is no self-appraisal for the review parameters No.1 to 7. However, review parameter No. 8 is for the
appraisee to record the improvements done, additioanl responsibilities / initiatives / specific projects taken during
last year. But the rating for the same will be given by the superior. <br />
4. The superior has to carry out the appraisal process in presence of the appraisee. Before rating, the appraiser has
to discuss thoroughly with the appraisee about each performance parameter. <br />
5. In case of any difference between the appraiser and the appraisee, they should approach the appraiser’s
superior for resolving the difference. <br />
6. The focus should be on the performance shown in the entire performance year. <br />
7. Deputation / Transfer: In case any person is deputed / transferred in the course of the performance year, the
performance review will be done by the superior under whom he has spent greater period of the year. If the
appraisee has spent 6 months each under two persons, then both the superiors will conduct the performance
review. <br />
8. Each statement in the performance review has to be rated on a five point scale as given below.</p>
<hr />
   </div>

   <div class="divAppr">
   <table style="border-color:Black; border-style:groove; border-width:1px; font-weight:bold ">
    <tr>
   <td>Outstanding: Performance is exceptional and far exceeds expectations.</td>
   <td>5</td>
   </tr>

   <tr>
   <td>Excellent: Performance is excellent and exceeds expectations.</td>
   <td>4</td>
   </tr>

   <tr>
   <td>Very Good: Performance is consistent, clearly meets job requirement.</td>
   <td>3</td>
   </tr>

   <tr>
   <td>Good: Performance is good, just meets job requirement.</td>
   <td>2</td>
   </tr>

   <tr>
   <td>Average: Performance is inconsistent, meets job requirement occasionally.</td>
   <td>1</td>
   </tr>

   <tr>
   <td>Poor: Performance is not satisfactory & does not meet job requirement.</td>
   <td>0</td>
   </tr>
   
   </table>  
  </div>

 <%-- --1----%>
   <hr />
   <div>

   <p><b>1. CUSTOMER / SUPPLIER ORIENTATION</b></p>

   <div>
       <asp:Label ID="lblC11" runat="server" CssClass="labelAppr"  Width="630px"
       Text="What is the appraisee’s level of awareness of the needs of his customer / supplier"></asp:Label>
        <asp:DropDownList ID="drpdwnCust1" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>

     <div>
       <asp:Label ID="lblC12" runat="server" CssClass="labelAppr"  Width="630px"
       Text="What is the appraisee’s speed of response towards fulfilling the needs of his customers / suppliers."></asp:Label>
        <asp:DropDownList ID="drpdwnCust2" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>
   
   <div>
       <asp:Label ID="lblC13" runat="server" CssClass="labelAppr" Width="630px"
       Text="To what extent does the appraisee communicate openly and transparently with his customers / suppliers"></asp:Label>
        <asp:DropDownList ID="drpdwnCust3" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>
   
   <div>
       <asp:Label ID="lblC14" runat="server" CssClass="labelAppr"  Width="630px"
       Text="To what extent does the appraisee act upon the feedback received from his customers / suppliers"></asp:Label>
        <asp:DropDownList ID="drpdwnCust4" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>

   </div>

   <div class="divAppr">
   <b>NOTE :</b>   <br />    
&nbsp;&nbsp;&nbsp;a) Customers / Suppliers include both internal and external. Every individual might not have Customers / Suppliers, but all have internal
          Customers / Suppliers. Emphasis should be laid on his dealings with both. <br />
&nbsp;&nbsp;&nbsp;b) <b>Awareness of the needs :</b> Did the appraisee know what he had to deliver to his Customers / Suppliers and did he make efforts to identify the same. <br />
&nbsp;&nbsp;&nbsp;c) <b>Speed Of response:</b> How quickly did the appraisee act upon the demands / requirements of the Customers / Suppliers. <br />
&nbsp;&nbsp;&nbsp;d) <b>Communicate openly and transparently:</b> Did the appraisee take the Customers / Suppliers in confidence. <br />
&nbsp;&nbsp;&nbsp;e) <b>Act upon the feedback:</b> Was the appraisee flexible to make changes that may have been pointed out by his Customers / Suppliers. <br />
   </div>

   <%----2----%>
      <hr />
   <div>

   <p><b>2. COMMITMENT TO QUALITY</b></p>

   <div>
       <asp:Label ID="lblC21" runat="server" CssClass="labelAppr"  Width="500px"
       Text="To what extent does the appraisee provide work of absolute quality"></asp:Label>
        <asp:DropDownList ID="drpdwnCommit1" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>

     <div>
       <asp:Label ID="lblC22" runat="server" CssClass="labelAppr"  Width="500px"
       Text="To what extent does the appraisee strive towards improving himself in his work"></asp:Label>
        <asp:DropDownList ID="drpdwnCommit2" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>
   
   <div>
       <asp:Label ID="lblC23" runat="server" CssClass="labelAppr" Width="500px"
       Text="What is the level of consistency in the quality of the appraisee’s work"></asp:Label>
        <asp:DropDownList ID="drpdwnCommit3" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>
   
   <div>
       <asp:Label ID="lblC24" runat="server" CssClass="labelAppr"  Width="500px"
       Text="To what extent is the appraisee involved in Quality Improvement Activities"></asp:Label>
        <asp:DropDownList ID="drpdwnCommit4" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>

   </div>

   <div class="divAppr">
   <b>NOTE :</b>   <br />    
&nbsp;&nbsp;&nbsp;a) <b>Provide work of absolute quality:</b> This factor tries to measure the quality of the appraisee’s work / performance. <br />
&nbsp;&nbsp;&nbsp;b) <b>Strive towards improving himself:</b> This factor tries to measure the appraisee’ s intrinsic desire to continuously do better or display higher level of performance <br />
&nbsp;&nbsp;&nbsp;c) <b>Consistency in the quality of his work:</b> This factor tries to measure the appraisee’ s ability to maintain a standard level of quality, especially under pressure or stress <br />
&nbsp;&nbsp;&nbsp;d) <b>Involved in Quality Improvement Activities:</b> This factor tries to measure the appraisee’ s involvement in various quality improvement activities like Kaizon, Small Group Activity etc. <br />
 </div>

  <%----3----%>
      <hr />
   <div>

   <p><b>3. TEAM WORK & LEADERSHIP ABILITIES</b></p>

   <div>
       <asp:Label ID="lblC31" runat="server" CssClass="labelAppr"  Width="600px"
       Text="To what extent does the appraisee contribute towards team effectiveness"></asp:Label>
        <asp:DropDownList ID="drpdwnTeam1" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>

     <div>
       <asp:Label ID="lblC32" runat="server" CssClass="labelAppr"  Width="600px"
       Text="How do you rate the appraisee on the ability to develop interpersonal relationships"></asp:Label>
        <asp:DropDownList ID="drpdwnTeam2" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>
   
   <div>
       <asp:Label ID="lblC33" runat="server" CssClass="labelAppr" Width="600px"
       Text="How do you rate the appraisee on the ability to inspire his team members to attain team goals"></asp:Label>
        <asp:DropDownList ID="drpdwnTeam3" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>
   
   <div>
       <asp:Label ID="lblC34" runat="server" CssClass="labelAppr"  Width="600px"
       Text="How do you rate the appraisee on the ability to provide direction to his team"></asp:Label>
        <asp:DropDownList ID="drpdwnTeam4" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>

   </div>

   <div class="divAppr">
   <b>NOTE :</b>   <br />    
&nbsp;&nbsp;&nbsp;a) <b>Contribute towards team effectiveness:</b> The appraisee’s ability to identify with the team objective and work collectively with the team members to achieve the same.<br />
&nbsp;&nbsp;&nbsp;b) <b>Develop interpersonal relationships:</b> The appraisee’s ability to get along with others, give due respect to all, be a patient listener etc.<br />
&nbsp;&nbsp;&nbsp;c) <b>Inspire his team members:</b> The appraisee’s ability to inject among the team members and enthusiasm and drive to achieve the goals set.<br />
&nbsp;&nbsp;&nbsp;d) <b>Provide direction:</b> The appraisee’s ability to provide guidance and show the way for attaining the goals set.<br />
 </div>

   <%----4----%>
      <hr />
   <div>

   <p><b>4. KNOWLEDGE & SKILL</b></p>

   <div>
       <asp:Label ID="lblC41" runat="server" CssClass="labelAppr"  Width="680px"
       Text="How would you rate the appraisee on the Knowledge and skills required to perform the functions"></asp:Label>
        <asp:DropDownList ID="drpdwnKnow1" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>

     <div>
       <asp:Label ID="lblC42" runat="server" CssClass="labelAppr"  Width="680px"
       Text="To what extent has the appraisee updated his knowledge and learnt new skills"></asp:Label>
        <asp:DropDownList ID="drpdwnKnow2" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>
   
   <div>
       <asp:Label ID="lblC43" runat="server" CssClass="labelAppr" Width="680px"
       Text="To what extent does the appraisee apply his knowledge and skills in fulfilling the job responsibilities"></asp:Label>
        <asp:DropDownList ID="drpdwnKnow3" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>
   
   <div>
       <asp:Label ID="lblC44" runat="server" CssClass="labelAppr"  Width="680px"
       Text="To what extent does the appraisee share his knowledge and skills and make his expertise available to others."></asp:Label>
        <asp:DropDownList ID="drpdwnKnow4" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>

   </div>

   <div class="divAppr">
   <b>NOTE :</b>   <br />    
    &nbsp;&nbsp;&nbsp;a) <b>Knowledge and skills required to perform the functions:</b> Consider the appraisee’s knowledge of various procedures, methods of work that he handles. Also look at the skills possessed by him to successfully fulfill his duties<br />
    &nbsp;&nbsp;&nbsp;b) <b>Updated his knowledge and learnt new skills:</b> Consider the amount of time and effort the appraisee has spent to keep abreast with the new developments in his area of work.<br />
    &nbsp;&nbsp;&nbsp;c) <b>Apply his knowledge and skills:</b> Consider the appraisee’s ability to put into use knowledge and skills that he has acquired.<br />
    &nbsp;&nbsp;&nbsp;d) <b>Share his knowledge and skills and make his expertise available:</b> Consider the appraisee’s effort to impart his knowledge and skills to others through structured training programmes or in day to day to work.<br />
    </div>

  <%----5----%>
      <hr />
   <div>

   <p><b>5. SUBORDINATE DEVELOPMENT</b></p>

   <div>
       <asp:Label ID="lblC51" runat="server" CssClass="labelAppr"  Width="680px"
       Text="To what extent does the appraisee delegate responsibility and authority to his subordinates"></asp:Label>
        <asp:DropDownList ID="drpdwnSub1" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>

     <div>
       <asp:Label ID="lblC52" runat="server" CssClass="labelAppr"  Width="680px"
       Text="To what extent does the appraisee take responsibility of the training and developmental needs of his subordinates."></asp:Label>
        <asp:DropDownList ID="drpdwnSub2" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>
   
   <div>
       <asp:Label ID="lblC53" runat="server" CssClass="labelAppr" Width="680px"
       Text="To what extent is the appraisee accessible to his subordinates."></asp:Label>
        <asp:DropDownList ID="drpdwnSub3" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>
   
   <div>
       <asp:Label ID="lblC54" runat="server" CssClass="labelAppr"  Width="680px"
       Text="To what extent does the appraisee extend support to his subordinates in achieving the targets to them."></asp:Label>
        <asp:DropDownList ID="drpdwnSub4" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>

   </div>

   <div class="divAppr">
   <b>NOTE :</b>   <br />    
    &nbsp;&nbsp;&nbsp;a) <b>Delegate responsibility and authority:</b> the appraisee’s ability to share his work with his subordinates by assigning appropriate tasks, in turn exposing them to his functional areas.<br />
    &nbsp;&nbsp;&nbsp;b) <b>Take responsibility of the training and developmental needs:</b> The appraisee’s involvement in identifying the areas in which the subordinates needs improvement and taking steps to see that inputs are provided.<br />
    &nbsp;&nbsp;&nbsp;c) <b> Accessible to his subordinates:</b> The appraisee’s ability to make the subordinates feel free to approach him whenever required.<br />
    &nbsp;&nbsp;&nbsp;d) <b>Extend support to his subordinates:</b> The appraisee’s ability to monitor the progress made by his subordinates in their tasks, ask for problems faced by them and provides support as and when required.<br />
    </div>
  
    <%----6----%>
      <hr />
   <div>

   <p><b>6. PERSONAL ATTRIBUTES</b></p>

   <div>
       <asp:Label ID="lblC61" runat="server" CssClass="labelAppr"  Width="630px"
       Text="To what extent does the appraisee take initiative."></asp:Label>
        <asp:DropDownList ID="drpdwnPers1" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>

     <div>
       <asp:Label ID="lblC62" runat="server" CssClass="labelAppr"  Width="630px"
       Text="What is the level of self confidence of the appraisee"></asp:Label>
        <asp:DropDownList ID="drpdwnPers2" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>
   
   <div>
       <asp:Label ID="lblC63" runat="server" CssClass="labelAppr" Width="630px"
       Text="To what extent does the appraisee take ownership / responsibility for the outcome of his decision"></asp:Label>
        <asp:DropDownList ID="drpdwnPers3" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>
   <%--
   <div>
       <asp:Label ID="lblC64" runat="server" CssClass="labelAppr"  Width="630px"
       Text="How would you rate the appraisee on his Win-Win negotiating skills"></asp:Label>
        <asp:DropDownList ID="drpdwnScore64" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>--%>
      
   <div>
       <asp:Label ID="lblC65" runat="server" CssClass="labelAppr" Width="630px"
       Text="How would you rate the appraisee on his ability to communicate effectively"></asp:Label>
        <asp:DropDownList ID="drpdwnPers4" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>
   
 <%--  <div>
       <asp:Label ID="lblC66" runat="server" CssClass="labelAppr"  Width="630px"
       Text="How would you rate the appraisee on his discipline and self control"></asp:Label>
        <asp:DropDownList ID="drpdwnScore66" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>--%>
   </div>

   <div class="divAppr">
   <b>NOTE :</b>   <br />    
&nbsp;&nbsp;&nbsp;a) <b>Initiative:</b> being a self starter, has the drive to take up extra work without being told.<br />
&nbsp;&nbsp;&nbsp;b) <b>Self Confidence:</b> having full belief in oneself and one’s capabilities<br />
&nbsp;&nbsp;&nbsp;c) <b>Ownership / responsibility:</b> The appraisee’s ability to take the responsibility to whatever he has been assigned.<br />
<%--&nbsp;&nbsp;&nbsp;d) <b>Win Win Negotiation:</b> The appraisee’s ability to srike a fair balance in any given situation.<br />--%>
&nbsp;&nbsp;&nbsp;d) <b>Communication:</b> The appraisee’s ability to communicate effectively what is intended from him/her.<br />
<%--&nbsp;&nbsp;&nbsp;f) <b>Discipline & Self Control:</b> To what extend the appraisee exhibits self discipline & control and contributes to uphold the morale.<br />--%>
 </div> 

    <%----7----%>
      <hr />
   <div>

   <p><b>7. JOB PERFORMANCE</b></p>

   <div>
       <asp:Label ID="lblC71" runat="server" CssClass="labelAppr"  Width="600px"
       Text="To what extent does the appraisee been able to achieve goals set for him in the current year"></asp:Label>
        <asp:DropDownList ID="drpdwnJob1" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>

     <div>
       <asp:Label ID="lblC72" runat="server" CssClass="labelAppr"  Width="600px"
       Text="How would you rate the appraisee on his level of effectiveness and efficiency?"></asp:Label>
        <asp:DropDownList ID="drpdwnJob2" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>
   
   <%--<div>
       <asp:Label ID="lblC73" runat="server" CssClass="labelAppr" Width="600px"
       Text="How would you rate the appraisee on his analytical skills and problem solving abilities?"></asp:Label>
        <asp:DropDownList ID="drpdwnScore73" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>--%>
   
   <div>
       <asp:Label ID="lblC74" runat="server" CssClass="labelAppr"  Width="600px"
       Text="To what extent does the appraisee effectively utilize the resources that are at his disposal?"></asp:Label>
        <asp:DropDownList ID="drpdwnJob3" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>
      
 <%--  <div>
       <asp:Label ID="lblC75" runat="server" CssClass="labelAppr" Width="600px"
       Text="To what extent does the appraisee contributed to cost reduction / cutting"></asp:Label>
        <asp:DropDownList ID="drpdwnScore75" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>--%>
   
   <div>
       <asp:Label ID="lblC76" runat="server" CssClass="labelAppr"  Width="600px"
       Text="To what extent does the appraisee take sound decisions based on facts and figures?"></asp:Label>
        <asp:DropDownList ID="drpdwnJob4" runat="server" CssClass="textbox"></asp:DropDownList>
   </div>
   </div>

   <div class="divAppr">
   <b>NOTE :</b>   <br />    
&nbsp;&nbsp;&nbsp;a) <b>Achieve goals set out:</b> This rating should be the same or close to the rating assigned to the appraisee at the end of the year in the goal setting form.<br />
&nbsp;&nbsp;&nbsp;b) <b>Effectiveness and efficiency:</b> To what extent does the appraisee is effective and efficient in his work<br />
<%--&nbsp;&nbsp;&nbsp;c) <b>Analytical skills and problem solving abilities:</b> Did the appraisee diagnose problems systematically, prevent problems by planning and thinking positively using SOP’s, checklists and find corrective long term solutions.<br />
&nbsp;&nbsp;&nbsp;d) <b>Cost Cutting / reduction:</b> Review the appraisee on the basis of Cost Cutting / reduction that he has been able to achieve.<br />--%>
&nbsp;&nbsp;&nbsp;c) <b>Effectively utilize the resources:</b> Did the appraisee make optimum use of the resources at his command such as men, material, machines, money and information.<br />
&nbsp;&nbsp;&nbsp;d) <b>Sound decision based on facts and figures:</b> Did the appraisee take decisions and substantiate the decisions with data rather than just relying on his experience and gut feeling.<br />
 </div> 
 
 <hr />
 <div>
 Based on the review of all the above parameters, list down the following which enables the aprraisee to improve both at the personal level and functional level.
 <p><b>Strengths</b></p><br />
   <asp:TextBox ID="txtStrengths" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="600px" Height="150px"></asp:TextBox>
 <p><b>Area For Improvements</b></p><br />
   <asp:TextBox ID="txtImprovements" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="600px" Height="150px"></asp:TextBox>
  <p><b>Training Recommendations: ( Will be reviewed and considered during subsequent training calendar)</b></p><br />
    <asp:TextBox ID="txtTraining" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="600px" Height="150px"></asp:TextBox>
 </div>

 </asp:Panel>
 
 <div class="buttonrow">                           
    <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" /> 
    <asp:Button ID="btnSaveAndSend" runat="server" Text="Save &amp; Send" onclick="btnSaveAndSend_Click" />
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" onclick="btnCancel_Click"/>
</div><br />

</asp:Content>

