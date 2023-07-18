<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" EnableEventValidation="false" Inherits="UI_Working_Time_leaverequest" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" Theme="SkinFile" CodeBehind="leaverequest.aspx.cs" %>

<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="BDP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .Disp {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="../../Utilities/ValidationMessages.js" type="text/javascript"></script>
    <script src="../../Utilities/Validations.js" type="text/javascript"></script>

    <script type="text/javascript" id="me">
        function ClientLeaveTypeSelectionChanged() {
            var LeaveTypeDdlControl = document.getElementById("<%= drpdwnTypeOfLeave.ClientID %>")
            var bdpToDate = ('<%= bdpToDate.ClientID %>');
            // bdpToDate.visibility = "hidden";
            var textbox = document.getElementById('<%= bdpToDate.ClientID %>' + '_textBox');
            var image = document.getElementById('<%= bdpToDate.ClientID %>' + '_image');
        
            //        // Leave 1/2 day value in DB is 0110
            if (LeaveTypeDdlControl.value.trim() == "0110") {
                textbox.disabled = image.disabled = true;
            }
            else {
                textbox.disabled = image.disabled = false;
            }

        }
    
        function OnSentForServiceClick(CheckBoxId, CalendarId) {
            //        alert("Control is here");
            //        checkbox = document.getElementById(CheckBox1);

            //        textbox = document.getElementById(bdpToDate + "_textBox");

            //        image = document.getElementById(bdpToDate + "_image");

            //        textbox.disabled = image.disabled = !checkbox.checked;

        }
        function ClientFromDateChanged() {

            var bdpFromDate = <%= bdpFromDate.ClientID %>.getSelectedDate();
            var bdpToDate = <%= bdpToDate.ClientID %>.getSelectedDate();
            if(bdpFromDate != null){
                document.getElementById('<%= lblMessageBoard.ClientID %>').innerText = "";
            }
            //            alert(bdpFromDate.getYear());
            //            // In javascript months indexing starts with 0. i. 0 - Jan, 1- Feb...
            //            if (bdpFromDate.getMonth() == 1 && bdpFromDate.getDate() >= 29)
            //	        { 
            //                if( bdpFromDate.getYear() % 4 == 0 || bdpFromDate.getYear() % 100 != 0 || bdpFromDate.getYear() % 400 == 0)
            //		        alert("Invalid date");
            //	        }
            var drpdwnLeaveType = document.getElementById("<%= drpdwnTypeOfLeave.ClientID %>")
            if(drpdwnLeaveType.value.trim() != "0110")
            {
                var bdpToDate = <%= bdpToDate.ClientID %>.getSelectedDate();
                if(bdpToDate != null)
                {
                    if(bdpFromDate > bdpToDate)
                    {
                        document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgFromDateGreaterThanToDate;
                    <%= bdpToDate.ClientID %>.focus()
                    return false;
                }
            }
            if(bdpFromDate != null && bdpToDate != null)
            {
                var txtFromTime = document.getElementById("<%= txtFromTime.ClientID %>")
                var txtToTime = document.getElementById("<%= txtToTime.ClientID %>")
                var txtDuration = document.getElementById("<%=txtDuration.ClientID %>")
                // The number of milliseconds in one day
                var ONE_DAY = 1000 * 60 * 60 * 24 // Changed 
                // Convert both dates to milliseconds
                var Fromdate_ms = bdpFromDate.getTime()
                var Todate_ms = bdpToDate.getTime()
                // Calculate the difference in milliseconds
                var difference_ms = Math.abs(Todate_ms - Fromdate_ms)
                // Convert back to days and return
                var days_diff = Math.round(difference_ms/ONE_DAY)
                document.getElementById("<%=hdDateDiff.ClientID %>").value = days_diff
                    if(days_diff > 0)
                    {
                        txtFromTime.disabled = false;
                        document.getElementById("<%= txtFromTime.ClientID %>").value = "";
                        txtToTime.disabled = false;
                        document.getElementById("<%= txtToTime.ClientID %>").value = "";
                        // txtDuration.disabled = true;
                        document.getElementById("<%=txtDuration.ClientID %>").value = "";
                        document.getElementById("<%=HiddenField1.ClientID %>").value = "";
                    }
                    else{
                        txtFromTime.disabled = false;
                        document.getElementById("<%= txtFromTime.ClientID %>").value = "";
                        txtToTime.disabled = false;
                        document.getElementById("<%= txtToTime.ClientID %>").value = "";
                        txtDuration.disabled = false;
                        document.getElementById("<%=txtDuration.ClientID %>").value = "";
                        document.getElementById("<%=HiddenField1.ClientID %>").value = "";
                    }
                }
            }
            else
            {document.getElementById("<%=hdDateDiff.ClientID %>").value = 0;
            }
        }
        function ClientToDateChanged() {
            var bdpFromDate = <%= bdpFromDate.ClientID %>.getSelectedDate();
        var bdpToDate = <%= bdpToDate.ClientID %>.getSelectedDate();
        if(bdpToDate != null){
            document.getElementById('<%= lblMessageBoard.ClientID %>').innerText = ""
        } 
        //            if(bdpFromDate != null)
        //            {
        //                if(bdpFromDate > bdpToDate)
        //                {
        //                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgFromDateGreaterThanToDate;
        //                    <%= bdpToDate.ClientID %>.focus()
        //                    return false;
        //                }
        //            }
        if(bdpFromDate != null && bdpToDate != null)
        {
            if(bdpFromDate > bdpToDate)
            {
                document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgFromDateGreaterThanToDate;
                <%= bdpToDate.ClientID %>.focus()
                return false;
            }
            var txtFromTime = document.getElementById("<%= txtFromTime.ClientID %>")
            var txtToTime = document.getElementById("<%= txtToTime.ClientID %>")
            var txtDuration = document.getElementById("<%=txtDuration.ClientID %>")
            // The number of milliseconds in one day
            var ONE_DAY = 1000 * 60 * 60 * 24
            // Convert both dates to milliseconds
            var Fromdate_ms = bdpFromDate.getTime()
            var Todate_ms = bdpToDate.getTime()
               
            // Calculate the difference in milliseconds
            var difference_ms = Math.abs(Todate_ms - Fromdate_ms)
            // Convert back to days and return
            var days_diff = Math.round(difference_ms/ONE_DAY)
            document.getElementById("<%=hdDateDiff.ClientID %>").value = days_diff

                if(days_diff > 0)
                {
                    txtFromTime.disabled = false;
                    document.getElementById("<%= txtFromTime.ClientID %>").value = "";
                    txtToTime.disabled = false;
                    document.getElementById("<%= txtToTime.ClientID %>").value = "";
                    //txtDuration.disabled = true;
                    document.getElementById("<%=txtDuration.ClientID %>").value = "";
                    document.getElementById("<%=HiddenField1.ClientID %>").value = "";
                }
                else{
                    txtFromTime.disabled = false;
                    document.getElementById("<%= txtFromTime.ClientID %>").value = "";
                    txtToTime.disabled = false;
                    document.getElementById("<%= txtToTime.ClientID %>").value = "";
                    txtDuration.disabled = false;
                    document.getElementById("<%=txtDuration.ClientID %>").value = "";
                    document.getElementById("<%=HiddenField1.ClientID %>").value = "";
                }
            }
        }

        function ClientFromTimeChanged() {
            document.getElementById("<%= lblMessageBoard.ClientID %>").style.color = "red";
             var datediff=document.getElementById("<%=hdDateDiff.ClientID %>").value

             var txtToTime = document.getElementById("<%= txtToTime.ClientID %>")
             var txtFromTime = document.getElementById("<%=txtFromTime.ClientID %>")
             if (txtToTime.value.length > 0 || txtToTime.value.length == 0)
                 document.getElementById('<%= lblMessageBoard.ClientID %>').innerText = "";
                var txtDuration = document.getElementById("<%=txtDuration.ClientID %>")
             if (txtToTime.value.length > 0)
             {
                 txtDuration.disabled = true;
                 document.getElementById("<%=txtDuration.ClientID %>").value = "";
                }
                else{
                    txtDuration.disabled = false;
                }
             //Calculate duration----------------------------------------------------------------------
                var txtFromTimeValue = txtFromTime.value;
                var txtToTimeValue = txtToTime.value;
                var FromTimeArray = new Array();
                FromTimeArray = txtFromTimeValue.split(' ');
                var ToTimeArray = new Array();
                ToTimeArray = txtToTimeValue.split(' ');
                if (txtFromTime.value.length > 0)
                {
                    if (!validateTime(txtFromTime)) {
                        document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgFromTimeFormat
                          return false;
                      }
                  }
                  if (txtToTime.value.length > 0)
                  {
                      if (!validateTime(txtToTime)) {
                          document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgToTimeFormat
                return false;
            }
        }
        if (txtFromTime.value.length > 0 && txtToTime.value.length > 0)
        {
            // Get from time hr and min array from FromTimeArray got above.FromTimeArray[0] holds hr:min value.
            //  so split it to get hr at 0th index and min at 1st index.
            var FromTimeHrMinArray = new Array()
            FromTimeHrMinArray = FromTimeArray[0].split(":");//makes FromTimeHrMinArray an array, [hour, minute]
            // Similarly get ToTimeHrMinArray
            var ToTimeHrMinArray = new Array()
            ToTimeHrMinArray = ToTimeArray[0].split(":");//makes ToTimeHrMinArray an array, [hour, minute]
            // First convert both from time and to time in 24 hour format.
            // i.e if PM selected then add 12 hrs to the selected value.
            var FromTimeEntered = parseInt(FromTimeHrMinArray[0],10); // Initialise
            var ToTimeEntered = parseInt(ToTimeHrMinArray[0],10); // Initialise.
            if(FromTimeArray[1].toUpperCase()=="PM" && FromTimeEntered < 12  )
            {
                FromTimeEntered =  parseInt(FromTimeHrMinArray[0]) + 12; 
            }
            if(FromTimeArray[1].toUpperCase()=="AM" && FromTimeEntered == 12  )
            {
                FromTimeEntered =  parseInt(0); 
            }
            if(ToTimeArray[1].toUpperCase()=="PM" && ToTimeEntered < 12)
            {
                ToTimeEntered =  parseInt(ToTimeHrMinArray[0]) + 12; 
            }
            // First validate that valid from and to time range is entered.
            if(FromTimeArray[1].toUpperCase() == "PM" && ToTimeArray[1].toUpperCase() == "PM"){
                // first compare hrs
                if(FromTimeEntered > ToTimeEntered){
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgFromToTimeCompare;
                    txtFromTime.focus();
                    return false;
                }                     
            }
            //                    if(FromTimeArray[1].toUpperCase() == "AM" && ToTimeArray[1].toUpperCase() == "AM"){
            //                        // first compare hrs
            //                        if(FromTimeEntered < ToTimeEntered){
            //                        document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgFromToTimeCompare;
            //                        txtFromTime.focus();
            //                        return false;
            //                        }                             
            //                    }
            if(FromTimeArray[1].toUpperCase() == ToTimeArray[1].toUpperCase()){
                // if hrs entered are same, then compare minutes.
                if(FromTimeEntered == ToTimeEntered){
                    if(FromTimeHrMinArray[1] > ToTimeHrMinArray[1])
                    {
                        document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgFromToTimeCompare;
                        txtFromTime.focus();
                        return false;
                    }
                }                                 
            }
            if(FromTimeArray[1].toUpperCase()!= ToTimeArray[1].toUpperCase()){
                if(ToTimeArray[1].toUpperCase()=="AM"){
                    if(FromTimeArray[1].toUpperCase()=="PM"){
                        document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgFromToTimeCompare;
                        txtFromTime.focus();
                        return false;
                    }  
                }
            }
            // Now calculate duration
            var answer = [];
            answer[0] = ToTimeEntered - FromTimeEntered; //makes answer array and sets first value to difference in hours
            answer[1] = ToTimeHrMinArray[1] - FromTimeHrMinArray[1]; //makes answers second value difference in minutes
            answer[0] = answer[0] * (parseInt(datediff)+ parseInt(1)) ;
            answer[1] = answer[1] * (parseInt(datediff)+ parseInt(1)) ;
            if (answer[1] < 0) {//negative number of minutes
                answer[0] = answer[0] - 1//removes 1 hour
                answer[1] = answer[1] + 60//adds 60 minutes
            }
       
            if(answer[1] > 59 )
            {
                answer[1]=(answer[1]/60).toPrecision(2)
                var sValue = answer[1].toString().split('.');
                answer[0] = answer[0]+(parseInt(sValue[0]));
                answer[1] = parseInt(sValue[1]);
            }
            if(answer[1] < 10 )
            {
                answer[1] = '0' + answer[1].toString()
            }
            answer = answer[0].toString() + ":" +answer[1].toString()//converts answer from array into string, hour:minute
      
            document.getElementById("<%=txtDuration.ClientID %>").value = answer;
            document.getElementById("<%=HiddenField1.ClientID %>").value = answer;
            
        }
    }

        
  function ClientToTimeChanged() {
      document.getElementById("<%= lblMessageBoard.ClientID %>").style.color = "red";
                var datediff=document.getElementById("<%=hdDateDiff.ClientID %>").value

                var txtToTime = document.getElementById("<%= txtToTime.ClientID %>")
                var txtFromTime = document.getElementById("<%=txtFromTime.ClientID %>")
                if (txtToTime.value.length > 0 || txtToTime.value.length == 0)
                    document.getElementById('<%= lblMessageBoard.ClientID %>').innerText = "";
                  var txtDuration = document.getElementById("<%=txtDuration.ClientID %>")
                if (txtToTime.value.length > 0)
                {
                    txtDuration.disabled = true;
                    document.getElementById("<%=txtDuration.ClientID %>").value = "";
                  }
                  else{
                      txtDuration.disabled = false;
                  }
                //Calculate duration----------------------------------------------------------------------
                  var txtFromTimeValue = txtFromTime.value;
                  var txtToTimeValue = txtToTime.value;
                  var FromTimeArray = new Array();
                  FromTimeArray = txtFromTimeValue.split(' ');
                  var ToTimeArray = new Array();
                  ToTimeArray = txtToTimeValue.split(' ');
                  if (txtFromTime.value.length > 0)
                  {
                      if (!validateTime(txtFromTime)) {
                          document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgFromTimeFormat
                return false;
            }
        }
        if (txtToTime.value.length > 0)
        {
            if (!validateTime(txtToTime)) {
                document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgToTimeFormat
                return false;
            }
        }
        if (txtFromTime.value.length > 0 && txtToTime.value.length > 0)
        {
            // Get from time hr and min array from FromTimeArray got above.FromTimeArray[0] holds hr:min value.
            //  so split it to get hr at 0th index and min at 1st index.
            var FromTimeHrMinArray = new Array()
            FromTimeHrMinArray = FromTimeArray[0].split(":");//makes FromTimeHrMinArray an array, [hour, minute]
            // Similarly get ToTimeHrMinArray
            var ToTimeHrMinArray = new Array()
            ToTimeHrMinArray = ToTimeArray[0].split(":");//makes ToTimeHrMinArray an array, [hour, minute]
            // First convert both from time and to time in 24 hour format.
            // i.e if PM selected then add 12 hrs to the selected value.
            var FromTimeEntered = parseInt(FromTimeHrMinArray[0],10); // Initialise
            var ToTimeEntered = parseInt(ToTimeHrMinArray[0],10); // Initialise.
            if(FromTimeArray[1].toUpperCase()=="PM" && FromTimeEntered < 12  )
            {
                FromTimeEntered =  parseInt(FromTimeHrMinArray[0]) + 12; 
            }
            if(FromTimeArray[1].toUpperCase()=="AM" && FromTimeEntered == 12  )
            {
                FromTimeEntered =  parseInt(0); 
            }
            if(ToTimeArray[1].toUpperCase()=="PM" && ToTimeEntered < 12)
            {
                ToTimeEntered =  parseInt(ToTimeHrMinArray[0]) + 12; 
            }
            // First validate that valid from and to time range is entered.
            if(FromTimeArray[1].toUpperCase() == "PM" && ToTimeArray[1].toUpperCase() == "PM"){
                // first compare hrs
                if(FromTimeEntered > ToTimeEntered){
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgFromToTimeCompare;
                    txtFromTime.focus();
                    return false;
                }                     
            }
            //                    if(FromTimeArray[1].toUpperCase() == "AM" && ToTimeArray[1].toUpperCase() == "AM"){
            //                        // first compare hrs
            //                        if(FromTimeEntered < ToTimeEntered){
            //                        document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgFromToTimeCompare;
            //                        txtFromTime.focus();
            //                        return false;
            //                        }                             
            //                    }
            if(FromTimeArray[1].toUpperCase() == ToTimeArray[1].toUpperCase()){
                // if hrs entered are same, then compare minutes.
                if(FromTimeEntered == ToTimeEntered){
                    if(FromTimeHrMinArray[1] > ToTimeHrMinArray[1])
                    {
                        document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgFromToTimeCompare;
                        txtFromTime.focus();
                        return false;
                    }
                }                                 
            }
            if(FromTimeArray[1].toUpperCase()!= ToTimeArray[1].toUpperCase()){
                if(ToTimeArray[1].toUpperCase()=="AM"){
                    if(FromTimeArray[1].toUpperCase()=="PM"){
                        document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgFromToTimeCompare;
                        txtFromTime.focus();
                        return false;
                    }  
                }
            }
            // Now calculate duration
            var answer = [];
            answer[0] = ToTimeEntered - FromTimeEntered; //makes answer array and sets first value to difference in hours
            answer[1] = ToTimeHrMinArray[1] - FromTimeHrMinArray[1]; //makes answers second value difference in minutes
            answer[0] = answer[0] * (parseInt(datediff)+ parseInt(1)) ;
            answer[1] = answer[1] * (parseInt(datediff)+ parseInt(1)) ;
            if (answer[1] < 0) {//negative number of minutes
                answer[0] = answer[0] - 1//removes 1 hour
                answer[1] = answer[1] + 60//adds 60 minutes
            }
       
            if(answer[1] > 59 )
            {
                answer[1]=(answer[1]/60).toPrecision(2)
                var sValue = answer[1].toString().split('.');
                answer[0] = answer[0]+(parseInt(sValue[0]));
                answer[1] = parseInt(sValue[1]);
            }
            if(answer[1] < 10 )
            {
                answer[1] = '0' + answer[1].toString()
            }
            answer = answer[0].toString() + ":" +answer[1].toString()//converts answer from array into string, hour:minute
      
            document.getElementById("<%=txtDuration.ClientID %>").value = answer;
          document.getElementById("<%=HiddenField1.ClientID %>").value = answer;
            
        }
    }
    function ClientDurationChanged() {
        var txtDuration = document.getElementById("<%=txtDuration.ClientID %>")
        var txtToTime = document.getElementById("<%= txtToTime.ClientID %>")
        var txtFromTime = document.getElementById("<%= txtFromTime.ClientID %>")
        if (txtDuration.value.length > 0)
        {
            txtFromTime.disabled = true;
            txtToTime.disabled = true;
            document.getElementById("<%=txtFromTime.ClientID %>").value = "";
            document.getElementById("<%=txtToTime.ClientID %>").value = "";
          
           document.getElementById("<%=HiddenField1.ClientID %>").value = txtDuration.value;
        }
        else{
            txtFromTime.disabled = false;
            txtToTime.disabled = false;
        }
    }
    function ClientLeaveSinceDateChanged() {
        var bdpLeaveSince = <%= bdpLeaveSince.ClientID %>.getSelectedDate();
      if(bdpLeaveSince != null){
          document.getElementById('<%= lblMessageBoard.ClientID %>').innerText = ""
        }
    }
    function validateDisplayBtnClick() {
        clearDirty();
        document.getElementById("<%= lblMessageBoard.ClientID %>").style.color = "red";
        var bdpLeaveSince = <%= bdpLeaveSince.ClientID %>.getSelectedDate()
        var CurrentDate = new Date;
        if(bdpLeaveSince == null){
            document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgEnterLeaveSince;
            (<%= bdpLeaveSince.ClientID %>).focus();
            return false;
        } 

    }
    function controlValidation() {
        clearDirty();
        document.getElementById("<%= lblMessageBoard.ClientID %>").style.color = "red";
            var BDPLiteFromdate = <%= bdpFromDate.ClientID %>.getSelectedDate();
   
            if(BDPLiteFromdate==null)
            {
                document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgFromDateNotFound;
            <%= bdpFromDate.ClientID %>.focus()
            return false;
        }
        var LeaveTypeDdlControl = document.getElementById("<%= drpdwnTypeOfLeave.ClientID %>")
            if(LeaveTypeDdlControl.value.trim() != "0110")
            {
                var BDPLiteTodate = <%= bdpToDate.ClientID %>.getSelectedDate();
            var bdpToDateCtrl = ('<%= bdpToDate.ClientID %>');
            if(BDPLiteTodate==null)
            {
                document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgToDateNotFound;
                <%= bdpToDate.ClientID %>.focus()
                return false;
            }
            if (BDPLiteFromdate > BDPLiteTodate) 
            {
                document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgFromDateGreaterThanToDate;
                <%= bdpToDate.ClientID %>.focus()
                return false;
            }
   
            var txtFromTime = document.getElementById("<%=txtFromTime.ClientID %>");
            var txtFromTimeValue = txtFromTime.value;
            var txtToTime = document.getElementById("<%=txtToTime.ClientID %>");
            var txtToTimeValue = txtToTime.value;
            var FromTimeArray = new Array();
            FromTimeArray = txtFromTimeValue.split(' ');
            var ToTimeArray = new Array();
            ToTimeArray = txtToTimeValue.split(' ');
            var txtDuration = document.getElementById("<%=txtDuration.ClientID %>")
            // The number of milliseconds in one day
            var ONE_DAY = 1000 * 60 * 60 * 24
            // Convert both dates to milliseconds
            var Fromdate_ms = BDPLiteFromdate.getTime()
            var Todate_ms = BDPLiteTodate.getTime()
            // Calculate the difference in milliseconds
            var difference_ms = Math.abs(Todate_ms - Fromdate_ms)
            // Convert back to days and return
            var days_diff = Math.round(difference_ms/ONE_DAY)
            if(days_diff >= 0)
            {
                if(txtDuration.value.length == 0 || txtDuration.disabled == true)
                {
                    if(txtFromTime.value.length == 0)
                    {
                        document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgEnterFromTime;
                   txtFromTime.focus();
                   return false
               }
               if(txtToTime.value.length == 0 )
               {
                   document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgEnterToTime;
                  txtToTime.focus();
                  return false;
              }
          }
          if (txtFromTime.value.length > 0)
          {
              if (!validateTime(txtFromTime)) {
                  document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgFromTimeFormat
                  txtFromTime.focus();
                  return false;
              }
          }
          if (txtToTime.value.length > 0)
          {
              if (!validateTime(txtToTime)) {
                  document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgToTimeFormat
                  txtToTime.focus();
                  return false;
              }
          }
          if(txtDuration.disabled == false)
          {
              if(txtDuration.value.length == 0)
              {
                  document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgEnterDuration
                   txtDuration.focus();
                   return false;
               }
           }
           
       }
   }
    var txtNoteForApprover = document.getElementById("<%=txtNoteForApprover.ClientID %>")
            if (!TextBoxEmpty(txtNoteForApprover))
            {
                document.getElementById("<%= lblMessageBoard.ClientID %>").innerText ="Enter the note for approver."
            txtNoteForApprover.focus();
            return false;
        }
    }
    function setDirty() {
        document.body.onbeforeunload = showMessage;
        //debugger;      
        document.getElementById("DirtyLabel").className = "show";
    }
    function clearDirty() {
        document.body.onbeforeunload = "";
        document.getElementById("DirtyLabel").className = "hide";
    }

    function showMessage() {
        return "If you click OK, the changes you have made will be lost."
    }
    function setControlChange() {
        if (typeof (event.srcElement) != 'undefined') {
            event.srcElement.onchange = setDirty;
        }
    }

    // document.body.onclick = setControlChange;
    //  document.body.onkeyup = setControlChange;
    function Expand_DDL(element) {
        element.style.width = 'auto';
    }

    function Compress_DDL(element, width) {
        element.style.width = '' + width + 'px';
    }
    </script>
    <span class="hidden">
        <asp:Button ID="btnEntryKey" runat="server" Text="" /></span>
    <asp:HiddenField ID="hdDateDiff" runat="server" />
    <div id="DirtyLabel" style="color: Red;" class="hide"></div>
    <div>
        <h2>Leave Request</h2>
        <asp:Label ID="lblMessageBoard" runat="server"
            meta:resourcekey="lblMessageBoardResource1"></asp:Label>
        <div class="buttonrow">
            <asp:Button ID="btnCalendar" runat="server" OnClick="btnCalendar_Click" OnClientClick="clearDirty();"
                meta:resourcekey="btnCalendarResource1" />
            <asp:Button ID="btnLeaveOverview" runat="server" OnClick="btnLeaveOverview_Click" OnClientClick="clearDirty();"
                meta:resourcekey="btnLeaveOverviewResource1" />
            <asp:Button ID="btnLeaveQuota" runat="server" OnClick="btnLeaveQuota_Click" />
            <asp:Button ID="btnShowTeamClndr" runat="server"
                OnClick="btnShowTeamClndr_Click" />
        </div>

        <asp:Panel ID="pnlLeaveOverview" runat="server"
            meta:resourcekey="pnlLeaveOverviewResource1">
            <div>
                <asp:Label ID="lblLeaveSince" runat="server" Text="Leave since" CssClass="label"
                    meta:resourcekey="lblLeaveSinceResource1"></asp:Label>
                <BDP:BDPLite ID="bdpLeaveSince" runat="server" CssClass="bold"
                    meta:resourcekey="bdpLeaveSinceResource1">
                    <TextBoxStyle CssClass="textbox" Width="170px" />
                </BDP:BDPLite>
                <asp:Button ID="btnDisplay" runat="server" Text="Display" OnClientClick="return validateDisplayBtnClick();"
                    meta:resourcekey="btnDisplayResource1" OnClick="btnDisplay_Click" />
            </div>
            <br />
            <div>
                <asp:GridView ID="grdLeaveDetails" runat="server"
                    meta:resourcekey="grdLeaveDetailsResource1" AutoGenerateColumns="False"
                    AllowPaging="True" AllowSorting="True" PageSize="5"
                    OnPageIndexChanging="grdLeaveDetails_PageIndexChanging"
                    OnSorting="grdLeaveDetails_Sorting">
                    <Columns>
                        <asp:BoundField DataField="LEAVE_REQ_ID" HeaderText="LEAVE_REQ_ID"
                            SortExpression="LEAVE_REQ_ID" />
                        <asp:BoundField DataField="ATEXT" HeaderText="Type of leave"
                            SortExpression="ATEXT" />
                        <asp:BoundField DataField="BEGDA" DataFormatString="{0:dd-MMM-yyyy}"
                            HeaderText="From" SortExpression="BEGDA" />
                        <asp:BoundField DataField="ENDDA" DataFormatString="{0:dd-MMM-yyyy}"
                            HeaderText="To" SortExpression="ENDDA" />
                        <asp:BoundField DataField="STATUS" HeaderText="Status"
                            SortExpression="STATUS" />
                        <asp:BoundField DataField="KVERB" HeaderText="Used" SortExpression="KVERB" />
                    </Columns>
                </asp:GridView>
                <br />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlCalendar" runat="server"
            meta:resourcekey="pnlCalendarResource1">

            <div style="float: left">
                <asp:Button ID="btnPrev" runat="server" Text="<<" OnClick="btnPrev_Click" OnClientClick="clearDirty();"
                    meta:resourcekey="btnPrevResource1" />
            </div>

            <div style="float: left" class="box">

                <asp:Calendar ID="Calendar1" runat="server" ShowGridLines="True"
                    ShowNextPrevMonth="False" meta:resourcekey="Calendar1Resource1"
                    OnSelectionChanged="Calendar1_SelectionChanged" BorderColor="#333333" BorderStyle="Solid" BorderWidth="1px">
                    <OtherMonthDayStyle BackColor="#E9E9E9" />
                </asp:Calendar>
            </div>
            <div style="float: left" class="box">
                <asp:Calendar ID="Calendar2" runat="server" ShowGridLines="True"
                    ShowNextPrevMonth="False" meta:resourcekey="Calendar2Resource1"
                    OnSelectionChanged="Calendar2_SelectionChanged"
                    OnDayRender="Calendar2_DayRender" BorderColor="#333333" BorderStyle="Solid" BorderWidth="1px">
                    <OtherMonthDayStyle BackColor="#E9E9E9" />
                </asp:Calendar>
            </div>
            <div style="float: left" class="box">
                <asp:Calendar ID="Calendar3" runat="server" ShowGridLines="True"
                    ShowNextPrevMonth="False" meta:resourcekey="Calendar3Resource1"
                    OnSelectionChanged="Calendar3_SelectionChanged" BorderColor="#333333" BorderStyle="Solid" BorderWidth="1px">
                    <OtherMonthDayStyle BackColor="#E9E9E9" />
                </asp:Calendar>
            </div>
            <div style="float: left">
                <asp:Button ID="btnNext" runat="server" Text=">>" OnClick="btnNext_Click" OnClientClick="clearDirty();"
                    meta:resourcekey="btnNextResource1" />
            </div>
            <div style="clear: both"></div>
            <div class="box">
                <asp:TextBox ID="txtAbsent" runat="server" Width="20px" Height="12px"
                    Style="background-color: Green" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblAbsent" runat="server" Text="Approved Leave" CssClass="label"
                    Width="110px"></asp:Label>
                <%-- <asp:TextBox ID="txtMltplEntrs" runat="server" Width="20px" Height="12px"
                    Style="background-color: Red; display:none;" meta:resourcekey="txtMltplEntrsResource1" Enabled="false"></asp:TextBox>
              <asp:Label ID="lblMultipleEntries" runat="server" Text="Multiple entries"
                    CssClass="label" meta:resourcekey="lblMultipleEntriesResource1"
                    Width="100px" ></asp:Label>--%>
                <asp:TextBox ID="txtSent" runat="server" Width="20px" Height="12px"
                    Style="background-color: Blue" meta:resourcekey="txtSentResource1" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblSent" runat="server" Text="Sent" CssClass="label"
                    meta:resourcekey="lblSentResource1" Width="50px"></asp:Label>
                <%--<asp:TextBox ID="txtDltnRqstd" runat="server" Width="20px" Height="12px" Enabled="false" Visible="false"
                    Style="background-color: Gray" meta:resourcekey="txtDltnRqstdResource1" ></asp:TextBox>
                <asp:Label ID="lblDltnRqstd" runat="server" Text="Deletion requested" Enabled="false" Visible="false"
                    CssClass="label" meta:resourcekey="lblDltnRqstdResource1"></asp:Label>--%>
            </div>
            <br />
        </asp:Panel>
        <asp:Panel ID="pnlLeaveQuota" runat="server">
            <%--<div>
        <asp:Label ID="lblLeaveType" runat="server" Text="Leave type" CssClass="label"></asp:Label>
                   <asp:DropDownList ID="drpdwnLeaveQuotaType" runat="server" Width="206px"  
        CssClass="textbox" AutoPostBack = "true" 
                       onselectedindexchanged="drpdwnLeaveQuotaType_SelectedIndexChanged">
                   </asp:DropDownList>
        </div>--%>
            <br />
            <div>
                <asp:GridView ID="grdLeaveQuotaDtls" runat="server" AutoGenerateColumns="False"
                    AllowPaging="True" AllowSorting="True" OnSorting="grdLeaveQuotaDtls_sorting">
                    <Columns>
                        <asp:BoundField DataField="ATEXT" HeaderText="Type of leave"
                            SortExpression="ATEXT" />
                        <asp:BoundField DataField="LEAVE_QUOTA_START_DATE" DataFormatString="{0:dd-MMM-yyyy}"
                            HeaderText="Deduction from" SortExpression="LEAVE_QUOTA_START_DATE" />
                        <asp:BoundField DataField="LEAVE_QUOTA_END_DATE" DataFormatString="{0:dd-MMM-yyyy}"
                            HeaderText="Deduction to" SortExpression="LEAVE_QUOTA_END_DATE" />
                        <asp:BoundField DataField="ANZHL" HeaderText="Entitlement"
                            SortExpression="ANZHL" />
                        <asp:BoundField DataField="KVERB" HeaderText="Entitlement used"
                            SortExpression="KVERB" />
                        <asp:BoundField DataField="AVAILABLE_DAYS" HeaderText="Available days"
                            SortExpression="AVAILABLE_DAYS" />
                    </Columns>
                </asp:GridView>
                <br />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlTeamCalendar" runat="server">
            <div>
                 <asp:Label ID="LabelPnlTeamCal" runat="server"
            meta:resourcekey="lblMessageBoardResource1"></asp:Label>
            </div>
            <div>
                <asp:Label ID="lblEmpDrpdwn" runat="server" Text="Display data for" CssClass="label"></asp:Label>
                <asp:DropDownList ID="drpdwnEmpList" runat="server" Width="206px" CssClass="textbox">
                </asp:DropDownList>
                <asp:Label ID="lblMonths" runat="server" Text="for" CssClass="label" Width="20px"></asp:Label>
                <asp:DropDownList ID="drpdwnMonths" runat="server" Width="206px" CssClass="textbox">
                    <asp:ListItem Value="1">January</asp:ListItem>
                    <asp:ListItem Value="2">February</asp:ListItem>
                    <asp:ListItem Value="3">March</asp:ListItem>
                    <asp:ListItem Value="4">April</asp:ListItem>
                    <asp:ListItem Value="5">May</asp:ListItem>
                    <asp:ListItem Value="6">June</asp:ListItem>
                    <asp:ListItem Value="7">July</asp:ListItem>
                    <asp:ListItem Value="8">August</asp:ListItem>
                    <asp:ListItem Value="9">September</asp:ListItem>
                    <asp:ListItem Value="10">October</asp:ListItem>
                    <asp:ListItem Value="11">November</asp:ListItem>
                    <asp:ListItem Value="12">December</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="lblYear" runat="server" Text="in" CssClass="label" Width="20px"></asp:Label>
                <asp:DropDownList ID="drpdwnYears" runat="server" Width="206px" CssClass="textbox">
                    <asp:ListItem>2011</asp:ListItem>
                    <asp:ListItem>2012</asp:ListItem>
                    <asp:ListItem>2013</asp:ListItem>
                    <asp:ListItem>2014</asp:ListItem>
                    <asp:ListItem>2015</asp:ListItem>
                    <asp:ListItem>2016</asp:ListItem>
                    <asp:ListItem>2017</asp:ListItem>
                    <asp:ListItem>2018</asp:ListItem>
                    <asp:ListItem>2019</asp:ListItem>
                    <asp:ListItem>2020</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" />
            </div>
            <br />
            <div>
                <asp:GridView ID="grdTeamClndr" runat="server" AutoGenerateColumns="False"
                    OnPageIndexChanging="grdTeamClndr_PageIndexChanging"
                    OnSorting="grdTeamClndr_Sorting" PageSize="1">
                </asp:GridView>
                <br />
            </div>
            <div style="clear: both"></div>

        </asp:Panel>
        <asp:Panel ID="pnlColorCode" runat="server">
            <div class="box">
                <asp:TextBox ID="TextBox1" runat="server" Width="20px" Height="12px"
                    Style="background-color: Green" meta:resourcekey="txtAbsentResource1" Enabled="false"></asp:TextBox>
                <asp:Label ID="Label1" runat="server" Text="Approved Leave" CssClass="label"
                    Width="60px"></asp:Label>
                <%-- <asp:TextBox ID="TextBox2" runat="server" Width="20px" Height="12px"
                    Style="background-color: Red" meta:resourcekey="txtMltplEntrsResource1" Enabled="false"></asp:TextBox>
                <asp:Label ID="Label2" runat="server" Text="Multiple entries"
                    CssClass="label" meta:resourcekey="lblMultipleEntriesResource1"
                    Width="100px"></asp:Label>--%>
                <asp:TextBox ID="TextBox3" runat="server" Width="20px" Height="12px"
                    Style="background-color: Blue" meta:resourcekey="txtSentResource1" Enabled="false"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" Text="Sent" CssClass="label"
                    meta:resourcekey="lblSentResource1" Width="50px"></asp:Label>
                <%-- <asp:TextBox ID="TextBox4" runat="server" Width="20px" Height="12px"
                    Style="background-color: Gray" meta:resourcekey="txtDltnRqstdResource1" Enabled="false"></asp:TextBox>
                <asp:Label ID="Label4" runat="server" Text="Deletion requested"
                    CssClass="label" meta:resourcekey="lblDltnRqstdResource1"></asp:Label>--%>
            </div>
            <br />
        </asp:Panel>
        <div>
            <asp:GridView ID="grdCal" runat="server"
                AutoGenerateColumns="False" OnSelectedIndexChanged="grdCal_SelectedIndexChanged"
                OnRowDataBound="grdCal_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="LEAVE_REQ_ID" HeaderText="LEAVE_REQ_ID"
                        SortExpression="LEAVE_REQ_ID" />
                    <asp:BoundField DataField="ATEXT" HeaderText="Type of leave"
                        SortExpression="ATEXT" />
                    <asp:BoundField DataField="BEGDA" DataFormatString="{0:dd-MMM-yyyy}"
                        HeaderText="From" SortExpression="BEGDA" />
                    <asp:BoundField DataField="ENDDA" DataFormatString="{0:dd-MMM-yyyy}"
                        HeaderText="To" SortExpression="ENDDA" />
                    <asp:BoundField DataField="STATUS" HeaderText="Status"
                        SortExpression="STATUS" />
                </Columns>
            </asp:GridView>
            <br />
        </div>
        <fieldset>
            <legend style="color: #999999; padding: 2px">Apply leave  </legend>

            <asp:Label ID="lblTypeOfLeave" runat="server" Text="Type of leave"
                CssClass="label" meta:resourcekey="lblTypeOfLeaveResource1"></asp:Label>
            <asp:DropDownList ID="drpdwnTypeOfLeave" runat="server"
                CssClass="textbox" meta:resourcekey="drpdwnTypeOfLeaveResource1"
                Style="font: normal normal normal 11px/17px Verdana !important; width: 206px; height: 22px;" onBlur="javascript:Compress_DDL(this,206)"
                onChange="javascript:Compress_DDL(this,206);" onMouseDown="javascript:Expand_DDL(this)"
                OnSelectedIndexChanged="drpdwnTypeOfLeave_SelectedIndexChanged">
            </asp:DropDownList>
            <%-- <cc1:CascadingDropDown ID="CCD_drpdwnTypeOfLeave" runat="server" Enabled="True" LoadingText="[LOADING LEAVE TYPES...]" PromptText="- SELECT LEAVE TYPES -"
                ServiceMethod="GetLeaveType" ServicePath="~/WebService/Service.asmx" TargetControlID="drpdwnTypeOfLeave" UseContextKey="true" ContextKey="LEAVE" Category="LeaveType">
            </cc1:CascadingDropDown>--%>
            <asp:Label ID="lblValidateTypeOfLeave" runat="server" Text="*"
                ForeColor="#FF3300" meta:resourcekey="lblValidateTypeOfLeaveResource1"></asp:Label>
            <br />
            <asp:Label ID="lblFromDate" runat="server" Text="From date" CssClass="label"
                meta:resourcekey="lblFromDateResource1"></asp:Label>
            <BDP:BDPLite ID="bdpFromDate" runat="server"
                meta:resourcekey="bdpFromDateResource1" Style="display: inline;"
                TabIndex="1">
                <TextBoxStyle CssClass="textbox" Width="170px" />
            </BDP:BDPLite>
            <asp:Label ID="lblToDate" runat="server" Text="To date" CssClass="label"
                meta:resourcekey="lblToDateResource1" Width="60px"></asp:Label>
            <BDP:BDPLite ID="bdpToDate" runat="server" RenderStyleSheet="Inline"
                meta:resourcekey="bdpToDateResource1" Style="display: inline;"
                TabIndex="2">
                <TextBoxStyle CssClass="textbox" Width="170px" />
            </BDP:BDPLite>
            <asp:Label ID="lblvalidation" runat="server" ForeColor="#FF3300" meta:resourcekey="lblValidateTypeOfLeaveResource1" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblFromTime" runat="server" Text="From time"
                CssClass="label" meta:resourcekey="lblFromTimeResource1"></asp:Label>
            <asp:TextBox ID="txtFromTime" runat="server" CssClass="textbox"
                meta:resourcekey="txtFromTimeResource1" TabIndex="3"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="txtFromTime_FilteredTextBoxExtender"
                runat="server" Enabled="True" TargetControlID="txtFromTime" ValidChars=":, ,AM,PM,am,pm,Am,Pm,aM,pM" FilterType="Custom,Numbers">
            </cc1:FilteredTextBoxExtender>
            <asp:Label ID="lblToTime" runat="server" Text="To time" CssClass="label"
                meta:resourcekey="lblToTimeResource1" Width="60px"></asp:Label>
            <asp:TextBox ID="txtToTime" runat="server" CssClass="textbox"
                meta:resourcekey="txtToTimeResource1" TabIndex="4"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="txtToTime_FilteredTextBoxExtender"
                runat="server" Enabled="True" TargetControlID="txtToTime" FilterType="Custom,Numbers" ValidChars=":, ,AM,PM,am,pm,Am,Pm,aM,pM">
            </cc1:FilteredTextBoxExtender>
            <asp:Label ID="lblTimeFormat" runat="server" Text=" ( Format : 09:30 AM / 06:00 PM )"></asp:Label>
            <br />
            <asp:Label ID="lblDuration" runat="server" Text="Duration" CssClass="label"
                meta:resourcekey="lblDurationResource1"></asp:Label>
            <asp:TextBox ID="txtDuration" runat="server" CssClass="textbox" Enabled="false"
                meta:resourcekey="txtDurationResource1" ViewStateMode="Disabled"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="txtDuration_FilteredTextBoxExtender"
                runat="server" TargetControlID="txtDuration" FilterType="Custom,Numbers" ValidChars=":">
            </cc1:FilteredTextBoxExtender>
            Hours
    <asp:HiddenField ID="HiddenField1" runat="server" />
            <br />
            <asp:Label ID="lblApprover" runat="server" Text="Approver" CssClass="label"
                meta:resourcekey="lblApproverResource1"></asp:Label>
            <asp:DropDownList ID="drpdwnApprover" runat="server" Width="206px"
                CssClass="textbox" meta:resourcekey="drpdwnApproverResource1" TabIndex="5">
            </asp:DropDownList>
            <br />
            <asp:Label ID="lblNoteForApprover" runat="server" Text="Note for approver"
                CssClass="label" meta:resourcekey="lblNoteForApproverResource1"></asp:Label>
            <asp:TextBox ID="txtNoteForApprover" runat="server" CssClass="textbox"
                meta:resourcekey="txtNoteForApproverResource1" TextMode="MultiLine"
                TabIndex="6"></asp:TextBox>
        </fieldset>
        <div class="buttonrow">
            <asp:Button ID="btnPreviousStep" runat="server" Text="Back" OnClientClick="clearDirty();"
                OnClick="btnPreviousStep_Click" />
            <asp:Button ID="btnReview" runat="server" Text="Review" OnClientClick="return controlValidation()"
                OnClick="btnReview_Click" meta:resourcekey="btnReviewResource1" />
            <asp:Button ID="btnSend" runat="server" Text="Send" OnClick="btnSend_Click" OnClientClick="clearDirty();"
                meta:resourcekey="btnSendResource1" />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" OnClientClick="clearDirty();" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                meta:resourcekey="btnCancelResource1" OnClick="btnCancel_Click" />
        </div>
        <br />
    </div>
</asp:Content>

