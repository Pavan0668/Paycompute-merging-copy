<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="TicketBook_accvehi_Local.aspx.cs" Inherits="iEmpPower.UI.Local_requisition.TicketBook_accvehi_outlocal"  EnableEventValidation="false" Theme="SkinFile" %>

<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls" TagPrefix="BDP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .ticket-info {
            width: 1178px;
            height: 112px;
        }
        .auto-style1 {
            width: 313px;
        }
        .auto-style2 {
            width: 342px;
        }
        .auto-style3 {
            width: 466px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <script src="../../Utilities/ValidationMessages.js" type="text/javascript"></script>
    <script src="../../Utilities/Validations.js" type="text/javascript"></script>
    <script type="text/javascript">
        function setHourglass() {
            document.body.style.cursor = 'wait';
        }
    </script>
    <script type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);
            if (div.style.display == "none") {
                div.style.display = "inline";
                img.src = "../../images/minus.gif";
            } else {
                div.style.display = "none";
                img.src = "../../images/plus.gif";
            }
        }
    </script>
    
    <script type="text/javascript">
        //international
        function ValidateDate(obj) {

            if (!DateValidator(obj.value)) {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Enter valid check in  date (DD-MM-YYYY).";
                document.getElementById("<%=lblMessageBoard.ClientID %>").style.color = "red";
                obj.focus();
                return false;
            }
            else {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "";
            }
        }

        function ValidateTime(obj) {

            if (!TimeValidator(obj.value)) {

                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Please enter correct arrival time. The time should be in 24 hour format.";
                document.getElementById("<%=lblMessageBoard.ClientID %>").style.color = "red";
                obj.focus();
                return false;
            }
            else {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "";
            }
        }
    </script>
    <div>
        <span id="bold" runat="server" style="font-weight: bold"></span>
    </div>
    <h2>Local Ticket Booking for Accommodation/Vehicle</h2>
  
      <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard"></asp:Label>

            <asp:UpdatePanel ID="pnlLocal" runat="server" Visible="true">
      <ContentTemplate>
          <br />
         
            <cc1:TabContainer ID="tcDefalut" runat="server" ActiveTabIndex="1" AutoPostBack="True" onactivetabchanged="tcDefalut_ActiveTabChanged" meta:resourcekey="tcDefalutResource1" >
         
           <cc1:TabPanel runat="server" HeaderText="Accommodation" ID="tabAccommodation"  meta:resourcekey="TabPanel1Resource1">
            <HeaderTemplate>Accommodation</HeaderTemplate>
                <ContentTemplate>
                    <div>
                    <asp:UpdatePanel ID="upPnlAcc" runat="server">
                        <ContentTemplate>

                            <div>
    
        <div class="legend">Search </div>
        <fieldset class="search-bg">
           <%--  <div>
                    <asp:Label ID="Label11" runat="server" Text="Accommodation type :" CssClass="label"></asp:Label>
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="150px"></asp:DropDownList>
                    <asp:Button ID="SearchButtonA" runat="server" Text="Search" OnClick="SearchButtonA_Click" />
                </div>--%>
                <div class="clear"></div>
              <%-- <asp:UpdatePanel  ID="LocalRequisitionUpdatePanel" runat="server">
                   <ContentTemplate>--%>
                       <asp:GridView ID="LocalRequisitionGridView" runat="server" AutoGenerateColumns="False"
                                Width="100%"  AllowPaging="True" AllowSorting="True" PageSize="4" OnPageIndexChanging="LocalRequisitionGridView_PageIndexChanging" 
                           OnRowDataBound="LocalRequisitionGridView_RowDataBound" OnSelectedIndexChanged="LocalRequisitionGridView_SelectedIndexChanged"
                            OnSorting="LocalRequisitionGridView_Sorting">
                                <Columns>
                                    <asp:BoundField DataField="local_acc_req_id" HeaderText="Requisition Id" SortExpression="local_acc_req_id">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Check_in_date" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Check in date" SortExpression="Check_in_date">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Check_out_date" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Check out date" SortExpression="Check_out_date">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="HotelPlaceCity" HeaderText="Hotel" SortExpression="HotelPlaceCity">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Number_of_members" HeaderText="Number of members" SortExpression="Number_of_members">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="current_status" HeaderText="Status" SortExpression="current_status">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="EMPLOYEE_NO" HeaderText="EMPLOYEE No." SortExpression="EMPLOYEE_NO">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                     </asp:BoundField>
                                    <%--<asp:TemplateField>
                                    
                                    <ItemTemplate>
                                         <asp:LinkButton ID="TravelRequiLinkButton" CommandName="Select" CommandArgument='<%# Eval("local_acc_req_id") %>' OnCommand="DomesticTravelRequiLinkButton_Click" runat="server">Book Ticket</asp:LinkButton>
                                    </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                <%--   </ContentTemplate>
               </asp:UpdatePanel>--%>
        </fieldset>
    <hr />
    <div class="legend">Entry </div>
    <div class="clear"></div>
                                <asp:Panel ID="pnlAccLocal" runat="server" Enabled="false">
                                    <div>
        <asp:RadioButtonList ID="AccommodationTypeRadioButtonList" runat="server" OnSelectedIndexChanged="AccommodationTypeRadioButtonList_SelectedIndexChanged" RepeatDirection="Horizontal" AutoPostBack="True">
            <asp:ListItem Text="Single" Value="1" Selected="True" ></asp:ListItem>
            <asp:ListItem Text="Group" Value="2"></asp:ListItem>
            <asp:ListItem Text="Guest" Value="3"></asp:ListItem>
        </asp:RadioButtonList>

<br />
    <br />
    <table class="ticket-info">
        <tr>
            <td class="auto-style2">
                <asp:Label ID="Label11" runat="server" Text="Emp Name" Font-Bold="True"></asp:Label>
                :
                      <asp:Label ID="LabelEmpName" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="Label55" runat="server" Text=" Dept" Font-Bold="True"></asp:Label>:
                    <asp:Label ID="LabelDept" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="Label56" runat="server" Text="Design " Font-Bold="True"></asp:Label>
                :
                    <asp:Label ID="LabelDesign" runat="server" Text=""></asp:Label>
                <br />
                  <asp:Label ID="Label57" runat="server" Text="D.O.B " Font-Bold="True"></asp:Label>
                :
                    <asp:Label ID="Label58" runat="server" Text="22/04/1999"></asp:Label>
                <br />
            </td>

            <td rowspan="2" class="auto-style3">
                <asp:Label ID="Label59" runat="server" Text=" Vehicle Type " Font-Bold="True"></asp:Label>
                :
     <asp:Label ID="LabelVehicleType" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="Label60" runat="server" Text="Vehicle Category" Font-Bold="True"></asp:Label>:
     <asp:Label ID="LabelVehicleCategory" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="Label61" runat="server" Text=" Vehicle Name" Font-Bold="True"></asp:Label>:
     <asp:Label ID="LabelVehicleName" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="Label62" runat="server" Text=" Phone No." Font-Bold="True"></asp:Label>:
     <asp:Label ID="Label63" runat="server" Text="9986878987"></asp:Label>
                <br />
            </td>
            <td rowspan="2">
                <asp:Label ID="Label64" runat="server" Text="Travel Date" Font-Bold="True"></asp:Label>:
            <asp:Label ID="LabelTravelDate" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="Label65" runat="server" Text="From" Font-Bold="True"></asp:Label>
                :
                    <asp:Label ID="LabelPANCard" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="Label66" runat="server" Text="To" Font-Bold="True"></asp:Label>:
                 <asp:Label ID="LabelVoterID" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="Label67" runat="server" Text="Email ID" Font-Bold="True"></asp:Label>:
                 <asp:Label ID="Label68" runat="server" Text="praveen@nrrs.com"></asp:Label>
                <br />
                <%--  <asp:Label ID="Label37" runat="server" Text="Departure" Font-Bold="True"></asp:Label>:
            <asp:Label ID="LabelDeparture" runat="server" Text=""></asp:Label>
                <br />
            <asp:Label ID="Label1" runat="server" Text="Passport" Font-Bold="True"></asp:Label>:
            <asp:Label ID="LabelPassport" runat="server" Text=""></asp:Label>
                <br />--%>
            </td>
        </tr>
    </table>
        <div class="clear"></div>
                    <asp:Label ID="Label12" runat="server" Text="Check in date" CssClass="label" ></asp:Label>
                    <asp:TextBox ID="CheckInDateTextBoxA" runat="server" Width="166px" CssClass="textbox"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImageButton1" TargetControlID="CheckInDateTextBoxA" Format="dd-MM-yyyy"></cc1:CalendarExtender>
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/calendar_icon.png" CssClass="gncontrol" Width="24" Height="22" />
                    <br />
                    <asp:Label ID="Label13" runat="server" Text="Check out date" CssClass="label"></asp:Label>
                    <asp:TextBox ID="CheckOutDateTextBoxA" runat="server" Width="166px" CssClass="textbox"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="ImageButton2" TargetControlID="CheckOutDateTextBoxA" Format="dd-MM-yyyy"></cc1:CalendarExtender>
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/calendar_icon.png" CssClass="gncontrol" Width="24" Height="22" />
                    <br />
                    <asp:Label ID="Label14" runat="server" Text="Arrival time" CssClass="label"></asp:Label>
                    <asp:TextBox ID="ArrivalTimeTextBox" runat="server" CssClass="textbox"></asp:TextBox><asp:Label ID="Label16" runat="server" Text="HH:MM" CssClass="note"></asp:Label>
                    <cc1:MaskedEditExtender runat="server" ID="ArrivalTimeMaskedEditExtender"
                        TargetControlID="ArrivalTimeTextBox"
                        Mask="99:99"
                        InputDirection="RightToLeft"
                        MessageValidatorTip="true"
                        MaskType="Time"
                        AcceptAMPM="false"
                        ErrorTooltipEnabled="True">
                    </cc1:MaskedEditExtender>
                    <br />
                    <asp:Label ID="Label23" runat="server" Text="Departure time" CssClass="label"></asp:Label>
                    <asp:TextBox ID="DepartureTimeTextBox" runat="server" CssClass="textbox"></asp:TextBox><asp:Label ID="Label24" runat="server" Text="HH:MM" CssClass="note"></asp:Label>
                    <cc1:MaskedEditExtender runat="server" ID="DepartureTimeMaskedEditExtender"
                        TargetControlID="DepartureTimeTextBox"
                        Mask="99:99" InputDirection="LeftToRight"
                        MessageValidatorTip="true"
                        MaskType="Time"
                        AcceptAMPM="false"
                        ErrorTooltipEnabled="True">
                    </cc1:MaskedEditExtender>
                    <br />
                    <asp:Label ID="Label32" runat="server" Text="Hotel place/city" CssClass="label" meta:resourcekey="Label12Resource1"></asp:Label>
                    <asp:TextBox ID="HotelPlaceCityTextBox" runat="server" CssClass="textbox"></asp:TextBox>
                    <br /> 
                    <asp:Label ID="Label38" runat="server" Text="Hotel category" CssClass="label" meta:resourcekey="Label12Resource1"></asp:Label>
                    <asp:DropDownList ID="DropDownHotel_category" runat="server" CssClass="textbox" Width="206px" AutoPostBack="True" meta:resourcekey="DropDownHotel_categoryResource1"></asp:DropDownList>
                    <br />
                    <asp:Label ID="Label39" runat="server" Text="Room category" CssClass="label" meta:resourcekey="Label13Resource1"></asp:Label>
                    <asp:DropDownList ID="DropDownRoom_category" runat="server" CssClass="textbox" Width="206px" AutoPostBack="True" meta:resourcekey="DropDownRoom_categoryResource1"></asp:DropDownList>
                    <br />
                    <asp:Label ID="Label40" runat="server" Text="Hotel name" CssClass="label" meta:resourcekey="Label14Resource1"></asp:Label>
                    <asp:DropDownList ID="DropDownHotel_name" runat="server" CssClass="textbox" Width="206px" AutoPostBack="True" meta:resourcekey="DropDownHotel_nameResource1"></asp:DropDownList>
                    <br />
                    <asp:Label ID="Label41" runat="server" Text="Mode of payment" CssClass="label"></asp:Label>
                    <asp:TextBox ID="AdditionalServicesTextBox" runat="server" CssClass="textbox"></asp:TextBox>
                  
                    <div id="NumbersOfMembers" runat="server"  visible="false">
                    <asp:Label ID="Label42" runat="server" Text="Number of members" CssClass="label" ></asp:Label>
                    <asp:TextBox ID="NumberOfMembersTextBoxA" runat="server" CssClass="textbox"></asp:TextBox>
                    <cc1:FilteredTextBoxExtender ID="NumberOfMemberFilterTextBoxExtender" runat="server" TargetControlID="NumberOfMembersTextBoxA" 
                         FilterType="Numbers" ></cc1:FilteredTextBoxExtender>
                    </div>
                   
                    <asp:Panel ID="NameOfMemberPanel" runat="server" Visible="false">
                    <asp:Label ID="Label43" runat="server" Text="Name of members" CssClass="label"></asp:Label>
                    <asp:TextBox ID="NameOfMembersTextBoxA" runat="server" CssClass="textbox"></asp:TextBox>
                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="NameOfMembersTextBoxA" 
                            FilterType="Numbers,LowercaseLetters, UppercaseLetters,Custom" ValidChars=" "  ></cc1:FilteredTextBoxExtender>
                         <asp:Button ID="Button2" runat="server" Text="Add to list" OnClick="AddToGridButton_Click" />                   
                    </asp:Panel>
                    <%---start---Grid to display the member names --%>
         
                  <div style="margin-left: 164px">
                    <asp:GridView ID="EmployeeDetailsGridViewA" runat="server"  AutoGenerateColumns="False" Style="margin-right: 0px" OnSelectedIndexChanged="EmployeeDetailsGridViewA_SelectedIndexChanged" OnRowDeleting="EmployeeDetailsGridViewA_RowDeleting">
                         <Columns>
                           <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ShowHeader="False" HeaderStyle-Width="20">
                                <ItemTemplate>
                                    <asp:CheckBox ID="EmployeeNameCheckBox" runat="server"  AutoPostBack="true"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="250" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="left">
                                <HeaderTemplate>Employee name</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="EmployeeNameLabel" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderStyle-Width="120">
                                <HeaderTemplate>Employee ID</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="EmployeeIdLabel" runat="server" Width="120" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderStyle-Width="120">
                                <ItemTemplate>
                                    <span onclick="return confirm('Are you sure to delete?')"></span>
                                    <asp:LinkButton ID="DeleteLinkButton" runat="server" Text="Delete"  CommandName="Delete" ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                         </Columns>
                    </asp:GridView>
                      </div>
                    <%---end---Grid to display the member names --%>
                    <%---start-------Auto complete extender  --%>
                      <asp:Panel runat="server" ID="Panel1" Height="200px" ScrollBars="Both" Width="450px" Style="overflow: scroll;" Visible="false" ></asp:Panel>
                      <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  CompletionInterval="25"  EnableCaching="true" MinimumPrefixLength="1" 
                          ServiceMethod="GetEmployeeNamesAndId" ServicePath="~/UI/Benefits_Payment/EmployeeNames.asmx" CompletionListCssClass="AutoCompleteList" 
                          TargetControlID="NameOfMembersTextBoxA" FirstRowSelected="true" CompletionListElementID="EmployeeNamePanel"></cc1:AutoCompleteExtender>
                    <%---end-------Auto complete extender  --%>
        
                    <asp:Label ID="Label44" runat="server" Text="Remarks" CssClass="label"></asp:Label>
                    <asp:TextBox ID="RemarksTextBoxA" runat="server" CssClass="textbox"></asp:TextBox>
          <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="RemarksTextBoxA" 
                                          FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" ValidChars=" ,#@" ></cc1:FilteredTextBoxExtender>
                    <br />
                    <div class="clear"></div> 
                    <br />                      
    </div>
                                </asp:Panel>
     </div>
                            <asp:Panel ID="pnlAccomTicketbooking" runat="server" CssClass="auto-style1" Visible="false"> 
                        <h3>Travel Desk Arrangement Area</h3>
                        <table align="left" cellpadding="0" cellspacing="0" class="auto-style1">
                            <tr>
                                <td>
                                  <asp:Label ID="Label45" runat="server" Text="Employee Name" CssClass="label"></asp:Label> 
                                  <asp:TextBox ID="txtEmployeeName" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                </td>
                                <td >
                                  <asp:Label ID="Label46" runat="server" Text="Department" CssClass="label"></asp:Label>
                                  <asp:TextBox ID="txtDepartment" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                </td>
                                <td >
                                  <asp:Label ID="Label47" runat="server" Text="Designation" CssClass="label"></asp:Label>
                                  <asp:TextBox ID="txtDesignation" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                </td> 
                            </tr>
                            <tr >
                                <td >
                                     <asp:Label ID="Label48" runat="server" Text="Contact Number" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtContactNumb" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtContactNumb">
        </cc1:FilteredTextBoxExtender>
                                </td>
                                <td >
                                    <asp:Label ID="Label49" runat="server" Text="Payment" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtPayment" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                </td>
                                <td  >
                                    <asp:Label ID="Label50" runat="server" Text="Tariff" CssClass="label" meta:resourcekey="Label1Resource1"></asp:Label>
                                    <asp:TextBox ID="txtTariff" runat="server" Width="140px" CssClass="textbox" ></asp:TextBox>
                                </td> 
                            </tr>
                            <tr >
                                <td >
                                     <asp:Label ID="Label51" runat="server" Text="Booking Given To" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtBookingGivenTo" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                </td>
                                <td >
                                     <asp:Label ID="Label52" runat="server" Text="Hotel Invoice No" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtHotelInvoiceNo" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                </td>
                               <td  >
                                    <asp:Label ID="Label53" runat="server" Text="Bill Date" CssClass="label" meta:resourcekey="Label1Resource1"></asp:Label>
                                    <asp:TextBox ID="txtBillDateA" runat="server" Width="140px" CssClass="textbox" Enabled="false" ></asp:TextBox>
                                    <AjaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="ImageButton3" TargetControlID="txtBillDateA" Format="dd-MM-yyyy"></AjaxToolkit:CalendarExtender>
                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/calendar_icon.png" CssClass="gncontrol" Width="24" Height="22" />
                                </td> 
                            </tr>
                            <tr >
                                <td  >
                                    <asp:Label ID="Label54" runat="server" Text="Amount" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtAmount">
        </cc1:FilteredTextBoxExtender>
                                </td>
                                <td >&nbsp;</td> 
                                <td >&nbsp;</td> 
                            </tr>                             
                        </table>

                    </asp:Panel>
                   
                   <%-- --------------------%>
                    <div class="clear"></div>
                    <div class="buttonrow">
                        <asp:Button ID="btnAccTicketBool" runat="server" Text="Ticket Book"  OnClick="btnAccTicketBool_Click" />
                        <asp:Button ID="btnCancelA" runat="server" Text="Cancel" meta:resourcekey="btnCancelResource1" OnClick="btnCancel_Click" />                        
                    </div>
                    <br />

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                </ContentTemplate>
                </cc1:TabPanel>
           
           <cc1:TabPanel runat="server" HeaderText="Vehicle" ID="tabVehicle" meta:resourcekey="TabPanel1Resource1">
            <HeaderTemplate>Vehicle</HeaderTemplate>
            <ContentTemplate>
          <div>
                    <asp:UpdatePanel ID="upPnlVehi" runat="server">
                        <ContentTemplate> 
    <div>

      <asp:Label ID="Label1" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1"></asp:Label><br />
            <div class="legend">Search </div>
            <fieldset class="search-bg">

             <%--    <div>
                    <asp:Label ID="LocalRequisitionLabel" runat="server" Text="Travel type :" CssClass="label" meta:resourcekey="LocalRequisitionLabelResource1"></asp:Label>
                    <asp:DropDownList ID="LocalRequisitionDropDownList" runat="server" Width="150px" meta:resourcekey="LocalRequisitionDropDownListResource1"></asp:DropDownList>
                    <asp:Button ID="SearchButton" runat="server" Text="Search" meta:resourcekey="SearchButtonResource1" OnClick="SearchButton_Click" />
                </div>--%>
                <br />
               <%--  <asp:UpdatePanel  ID="LocalRequisitionUpdatePanel" runat="server">
                   <ContentTemplate>--%>
                       <asp:GridView ID="LocalTravelRequisitionGridView" runat="server" AutoGenerateColumns="False"
                            OnPageIndexChanging="LocalTravelRequisitionGridView_PageIndexChanging"
                              OnSelectedIndexChanged="LocalTravelRequisitionGridView_SelectedIndexChanged"
                           OnRowDataBound="LocalTravelRequisitionGridView_RowDataBound" Width="100%"  AllowPaging="True" AllowSorting="True" 
                                             
                                PageSize="4" OnSorting="LocalTravelRequisitionGridView_Sorting">
                                <Columns>
                                    <asp:BoundField DataField="local_travel_req_id" HeaderText="Requisition Id" SortExpression="local_travel_req_id">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="Date_of_travel" DataFormatString="{0:dd-MM-yyyy}" HeaderText="From date" SortExpression="Date_of_travel">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="To_Date" DataFormatString="{0:dd-MM-yyyy}" HeaderText="To date" SortExpression="To_Date">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                   <asp:BoundField DataField="Departure_from"  HeaderText="From" SortExpression="Departure_from">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Destination_to" HeaderText="To" SortExpression="Destination_to">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                      <asp:BoundField DataField="Pickup_time" HeaderText="Pickup time" SortExpression="Pickup_time">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="Drop_time" HeaderText="Drop time" SortExpression="Drop_time">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="current_status" HeaderText="Status" SortExpression="current_status">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EMPLOYEE_NO" HeaderText="EMPLOYEE No." SortExpression="EMPLOYEE_NO">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                     </asp:BoundField>
                                   <%-- <asp:TemplateField>
                                    
                                    <ItemTemplate>
                                         <asp:LinkButton ID="TravelRequiLinkButton" CommandName="Select" CommandArgument='<%# Eval("local_travel_req_id") %>' OnCommand="DomesticTravelRequiVehLinkButton_Click" runat="server">Book Ticket</asp:LinkButton>
                                    </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                 <%--  </ContentTemplate>
               </asp:UpdatePanel>--%>
                </fieldset>
      <hr />
            <div class="legend">Entry </div>
            <div class="clear"></div>
            <div> 
             <asp:Panel ID="pnl" Enabled="false" runat="server">    
            <div class="clear"></div>
            <asp:RadioButtonList ID="RadioButtontraveltype" AutoPostBack="True" runat="server"  RepeatDirection="Horizontal" meta:resourcekey="RadioButtontraveltypeResource1" >
                <asp:ListItem Value="1" meta:resourcekey="ListItemResource1">Single</asp:ListItem>
                <asp:ListItem Value="2" meta:resourcekey="ListItemResource2">Group</asp:ListItem>
                <asp:ListItem Value="3" meta:resourcekey="ListItemResource3">Guest</asp:ListItem>
            </asp:RadioButtonList>
            <br />
                 
    <table class="ticket-info">
        <tr>
            <td>
                <asp:Label ID="Label69" runat="server" Text="Emp Name" Font-Bold="True"></asp:Label>
                :
                      <asp:Label ID="Label70" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="Label71" runat="server" Text=" Dept" Font-Bold="True"></asp:Label>:
                    <asp:Label ID="Label72" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="Label73" runat="server" Text="Design " Font-Bold="True"></asp:Label>
                :
                    <asp:Label ID="Label74" runat="server" Text=""></asp:Label>
                <br />
                  <asp:Label ID="Label75" runat="server" Text="D.O.B " Font-Bold="True"></asp:Label>
                :
                    <asp:Label ID="Label76" runat="server" Text="22/04/1999"></asp:Label>
                <br />
            </td>

            <td rowspan="2">
                <asp:Label ID="Label77" runat="server" Text=" Vehicle Type " Font-Bold="True"></asp:Label>
                :
     <asp:Label ID="Label78" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="Label79" runat="server" Text="Vehicle Category" Font-Bold="True"></asp:Label>:
     <asp:Label ID="Label80" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="Label81" runat="server" Text=" Vehicle Name" Font-Bold="True"></asp:Label>:
     <asp:Label ID="Label82" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="Label83" runat="server" Text=" Phone No." Font-Bold="True"></asp:Label>:
     <asp:Label ID="Label84" runat="server" Text="9986878987"></asp:Label>
                <br />
            </td>
            <td rowspan="2">
                <asp:Label ID="Label85" runat="server" Text="Travel Date" Font-Bold="True"></asp:Label>:
            <asp:Label ID="Label86" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="Label87" runat="server" Text="From" Font-Bold="True"></asp:Label>
                :
                    <asp:Label ID="Label88" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="Label89" runat="server" Text="To" Font-Bold="True"></asp:Label>:
                 <asp:Label ID="Label90" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="Label91" runat="server" Text="Email ID" Font-Bold="True"></asp:Label>:
                 <asp:Label ID="Label92" runat="server" Text="praveen@nrrs.com"></asp:Label>
                <br />
                <%--  <asp:Label ID="Label37" runat="server" Text="Departure" Font-Bold="True"></asp:Label>:
            <asp:Label ID="LabelDeparture" runat="server" Text=""></asp:Label>
                <br />
            <asp:Label ID="Label1" runat="server" Text="Passport" Font-Bold="True"></asp:Label>:
            <asp:Label ID="LabelPassport" runat="server" Text=""></asp:Label>
                <br />--%>
            </td>
        </tr>
    </table>


                <asp:Label ID="Label5" runat="server" Text="From date" CssClass="label" ></asp:Label>
                    <asp:TextBox ID="CheckInDateTextBox" runat="server" Width="166px" CssClass="textbox"></asp:TextBox>
                    <cc1:CalendarExtender ID="CheckInDateCalendarExtender" runat="server" PopupButtonID="CheckInDateImageButton" TargetControlID="CheckInDateTextBox" Format="dd-MM-yyyy"></cc1:CalendarExtender>
                    <asp:ImageButton ID="CheckInDateImageButton" runat="server" ImageUrl="~/images/calendar_icon.png" CssClass="gncontrol" Width="24" Height="22" />
                    <br />
                    <asp:Label ID="Label8" runat="server" Text="To date" CssClass="label"></asp:Label>
                    <asp:TextBox ID="CheckOutDateTextBox" runat="server" Width="166px" CssClass="textbox"></asp:TextBox>
                    <cc1:CalendarExtender ID="CheckOutDateCalendarExtender" runat="server" PopupButtonID="CheckOutDateImageButton" TargetControlID="CheckOutDateTextBox" Format="dd-MM-yyyy"></cc1:CalendarExtender>
                    <asp:ImageButton ID="CheckOutDateImageButton" runat="server" ImageUrl="~/images/calendar_icon.png" CssClass="gncontrol" Width="24" Height="22" />
                <br />

                    <asp:Label ID="Label2" runat="server" Text="From" CssClass="label" meta:resourcekey="Label4Resource1"></asp:Label>
                    <asp:TextBox ID="FromTextBox" runat="server" CssClass="textbox" meta:resourcekey="TextBox3Resource1"></asp:TextBox>
                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="FromTextBox" 
                         FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"  ValidChars=" ,#@" ></cc1:FilteredTextBoxExtender>
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="To" CssClass="label" meta:resourcekey="Label5Resource1"></asp:Label>
                    <asp:TextBox ID="ToTextBox" runat="server" CssClass="textbox" meta:resourcekey="TextBox6Resource1"></asp:TextBox>
                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="ToTextBox" 
                         FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" ValidChars=" ,#@"  ></cc1:FilteredTextBoxExtender>
                    <br />
                    <asp:Label ID="Label4" runat="server" Text="Vehicle type" CssClass="label" meta:resourcekey="Label12Resource1"></asp:Label>
                    <asp:DropDownList ID="DropDownVehicletype" runat="server" CssClass="textbox" Width="206px"  meta:resourcekey="DropDownList1Resource1"></asp:DropDownList>
                    <br />
                    <asp:Label ID="Label7" runat="server" Text="Vehicle category" CssClass="label" meta:resourcekey="Label13Resource1"></asp:Label>
                    <asp:DropDownList ID="DropDownVehiclecategory" runat="server" Enabled="False" CssClass="textbox" Width="206px" meta:resourcekey="DropDownList2Resource1"></asp:DropDownList>
                    <br />
                    <asp:Label ID="Label15" runat="server" Text="Vehicle name" CssClass="label" meta:resourcekey="Label14Resource1"></asp:Label>
                    <asp:DropDownList ID="DropDownVehiclename" runat="server" Enabled="False" CssClass="textbox" Width="206px" meta:resourcekey="DropDownList7Resource1"></asp:DropDownList>
                    <br />
                    <asp:Label ID="Label17" runat="server" Text="Pickup time" CssClass="label" meta:resourcekey="Label8Resource1"></asp:Label>
                    <asp:TextBox ID="PickuptimeTextBox" runat="server" CssClass="textbox" ></asp:TextBox>
                    <cc1:MaskedEditExtender runat="server" ID="MaskedEditExtender2" TargetControlID="PickuptimeTextBox" Mask="99:99" MaskType="Time"
                        ErrorTooltipEnabled="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" 
                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                    </cc1:MaskedEditExtender>
                    <br />
                    <asp:Label ID="Label18" runat="server" Text="Pickup address" CssClass="label" meta:resourcekey="Label9Resource1"></asp:Label>
                    <asp:TextBox ID="PickupaddressTextBox" runat="server" CssClass="textbox" ></asp:TextBox>
                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="PickupaddressTextBox" 
                         FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"  ValidChars=" ,#@" ></cc1:FilteredTextBoxExtender>
                    <br />
                    <asp:Label ID="Label19" runat="server" Text="Drop time" CssClass="label" meta:resourcekey="Label10Resource1"></asp:Label>
                    <asp:TextBox ID="DroptimeTextBox" runat="server" CssClass="textbox" meta:resourcekey="TextBox4Resource1"></asp:TextBox>
                    <cc1:MaskedEditExtender runat="server" ID="MaskedEditExtender3" TargetControlID="DroptimeTextBox" Mask="99:99"  MaskType="Time"
                        ErrorTooltipEnabled="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" 
                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                    </cc1:MaskedEditExtender>
                    <br />
                    <asp:Label ID="Label20" runat="server" Text="Drop address" CssClass="label" meta:resourcekey="Label11Resource1"></asp:Label>
               
                    <asp:TextBox ID="DropaddressTextBox" runat="server" CssClass="textbox" meta:resourcekey="TextBox5Resource1"></asp:TextBox>
                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="DropaddressTextBox" 
                                          FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" ValidChars=" ,#@" ></cc1:FilteredTextBoxExtender>
                    <br />
                     <asp:Label ID="Label21" runat="server" Text="Purpose of travel" CssClass="label" meta:resourcekey="Label6Resource1"></asp:Label>
                    <asp:TextBox ID="PurposeoftravelTextBox" runat="server" CssClass="textbox" meta:resourcekey="TextBox7Resource1"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="PurposeoftravelTextBox" 
                                          FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" ValidChars=" ,#@" ></cc1:FilteredTextBoxExtender>
                    <br/>
                   <asp:Panel ID="Panelmember" Visible="False" runat="server" meta:resourcekey="PanelmemberResource1">
                   <asp:Label ID="lblNumberofmembers" runat="server" Text="Number of members" CssClass="label" meta:resourcekey="Label15Resource1"></asp:Label>
                    <asp:TextBox ID="NumberofmembersTextBox" runat="server" CssClass="textbox"  meta:resourcekey="TextBox9Resource1"></asp:TextBox>
                   <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                        TargetControlID="NumberofmembersTextBox" Enabled="True" />
                    <br />                    
                   <asp:Label ID="Label6" runat="server" Text="Name of members" CssClass="label" meta:resourcekey="Label4Resource2"></asp:Label>
                    <asp:TextBox ID="NameOfMembersTextBox" runat="server" CssClass="textbox" meta:resourcekey="NameOfMembersTextBoxResource1"></asp:TextBox>
                         <asp:Button ID="AddToGridButton" runat="server" Text="Add to list" meta:resourcekey="AddToGridButtonResource1" />
                    
                  <div style="margin-left: 164px">
                    <asp:GridView ID="EmployeeDetailsGridView" runat="server"  AutoGenerateColumns="False" Style="margin-right: 0px" meta:resourcekey="EmployeeDetailsGridViewResource1">
                         <Columns>
                           <asp:TemplateField ShowHeader="False" meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <asp:CheckBox ID="EmployeeNameCheckBox" runat="server"  AutoPostBack="True" meta:resourcekey="EmployeeNameCheckBoxResource1"/>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField meta:resourcekey="TemplateFieldResource2">
                                <HeaderTemplate>Employee name</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="EmployeeNameLabel" runat="server" meta:resourcekey="EmployeeNameLabelResource1" ></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Width="250px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                              <asp:TemplateField meta:resourcekey="TemplateFieldResource3">
                                <HeaderTemplate>Employee ID</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="EmployeeIdLabel" runat="server" Width="120px" meta:resourcekey="EmployeeIdLabelResource1" ></asp:Label>
                                </ItemTemplate>
                                  <HeaderStyle Width="120px" />
                            </asp:TemplateField>
                              <asp:TemplateField meta:resourcekey="TemplateFieldResource4">
                                <ItemTemplate>
                                    <span onclick="return confirm('Are you sure to delete?')"></span>
                                    <asp:LinkButton ID="DeleteLinkButton" runat="server" Text="Delete"  CommandName="Delete" meta:resourcekey="DeleteLinkButtonResource1" ></asp:LinkButton>
                                </ItemTemplate>
                                  <HeaderStyle Width="120px" />
                            </asp:TemplateField>
                         </Columns>
                    </asp:GridView>
                      </div>
                      <asp:Panel runat="server" ID="EmployeeNamePanel" Height="200px" ScrollBars="Both" Width="450px" Style="overflow: scroll;" Visible="False"
                           meta:resourcekey="EmployeeNamePanelResource1" ></asp:Panel>
                      <cc1:AutoCompleteExtender ID="EmpNameAutoCompleteExtender" runat="server"  CompletionInterval="25" MinimumPrefixLength="1" 
                          ServiceMethod="GetEmployeeNamesAndId" ServicePath="~/UI/Benefits_Payment/EmployeeNames.asmx" CompletionListCssClass="AutoCompleteList" 
                          TargetControlID="NameOfMembersTextBox" FirstRowSelected="True" CompletionListElementID="EmployeeNamePanel" DelimiterCharacters="" 
                          Enabled="True"></cc1:AutoCompleteExtender>
                      
                    </asp:Panel>
                  
                    <asp:Label ID="Label22" runat="server" Text="Remarks" CssClass="label" meta:resourcekey="Label15Resource1"></asp:Label>
                    <asp:TextBox ID="RemarksTextBox" runat="server" CssClass="textbox" meta:resourcekey="TextBox9Resource1"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="RemarksTextBox" 
                                          FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"  ValidChars=" ,#@"></cc1:FilteredTextBoxExtender>
                   
                                
</asp:Panel> 
    </div>

        <asp:Panel ID="pnlVehiTicketbooking" runat="server" CssClass="auto-style1" Visible="False"> 
                        <h3>Travel Desk Arrangement Area</h3>
                        <table align="left" cellpadding="0" cellspacing="0" class="auto-style1">
                            <tr>
                                <td>
                                  <asp:Label ID="Label9" runat="server" Text="Vehicle Source" CssClass="label"></asp:Label>
                                  <asp:DropDownList ID="drpdwnVehicleSource" runat="server" CssClass="textbox" Width="140px" >
                                      <asp:ListItem>Company</asp:ListItem>
                                      <asp:ListItem>Outside</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td >
                                    <asp:Label ID="Label29" runat="server" Text="Driver Name" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtDriverName" runat="server" CssClass="textbox" Width="140px"></asp:TextBox> 
                                </td>
                                <td >                                    
                                    <asp:Label ID="Label34" runat="server" Text="Bill Date" CssClass="label" meta:resourcekey="Label1Resource1"></asp:Label>
                                    <asp:TextBox ID="txtBillDate" runat="server" onkeyup="ValidateDate(this);" Width="140px" CssClass="textbox" Enabled="false"></asp:TextBox>
                                    <AjaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" PopupButtonID="ImageButton4" TargetControlID="txtBillDate" Format="dd-MM-yyyy"></AjaxToolkit:CalendarExtender>
                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/calendar_icon.png" CssClass="gncontrol" Width="24" Height="22" />
                                 </td> 
                            </tr>
                            <tr >
                                <td >  
                                    <asp:Label ID="Label30" runat="server" Text="Contact Number" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtContactNumber" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderC" runat="server" TargetControlID="txtContactNumber" 
                                     FilterType="Custom, Numbers"  ></cc1:FilteredTextBoxExtender>
                                </td>
                                <td >
                                  <asp:Label ID="Label36" runat="server" Text="Total Km" CssClass="label"></asp:Label>
                                  <asp:TextBox ID="txtTotalKm" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txtTotalKm" 
                                  FilterType="Custom, Numbers"  ></cc1:FilteredTextBoxExtender>                                 
                                </td>
                                <td  >
                                        <asp:Label ID="Label35" runat="server" Text="Driver Batta" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtDriverBatta" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txtDriverBatta" 
                                     FilterType="Custom, Numbers"  ></cc1:FilteredTextBoxExtender>
                                </td> 
                            </tr>
                           
                            <tr >
                                <td  >
                                    <asp:Label ID="Label27" runat="server" Text="Vehicle Number" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtVehicleNumb" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                    <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtVehicleNumb" 
                                     FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"  ></cc1:FilteredTextBoxExtender>--%>
                                </td>
                                <td >
                                    <asp:Label ID="Label31" runat="server" Text="Statutory Requirements if any" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtStatutory" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                    <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtStatutory" 
                                    FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"  ></cc1:FilteredTextBoxExtender>--%>
                                </td>
                                <td >
                                  <asp:Label ID="Label10" runat="server" Text="Agent Bill Number" CssClass="label"></asp:Label>
                                  <asp:TextBox ID="txtAgentBillNumb" runat="server" CssClass="textbox" Width="140px" Enabled="false"></asp:TextBox>
                                  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderA" runat="server" TargetControlID="txtAgentBillNumb" 
                                  FilterType="Custom, Numbers"  ></cc1:FilteredTextBoxExtender>                                   
                                </td> 
                            </tr> 
                            <tr >
                                <td >
                                  <asp:Label ID="Label28" runat="server" Text="Rate/Kms" CssClass="label"></asp:Label>
                                  <asp:TextBox ID="txtRateKms" runat="server" CssClass="textbox" Width="140px" Enabled="false"></asp:TextBox>
                                  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderR" runat="server" TargetControlID="txtRateKms" Enabled="false"
                                  FilterType="Custom, Numbers" ValidChars="." ></cc1:FilteredTextBoxExtender>
                                </td>
                                <td >
                                     <asp:Label ID="Label25" runat="server" Text="Agent Name" CssClass="label"></asp:Label>
                                      <asp:DropDownList ID="drpdwnAgentName" runat="server" CssClass="textbox" AutoPostBack="True" Width="140px"  Enabled="false"></asp:DropDownList>                                                            
                                </td>
                                <td >
                                     <asp:Label ID="Label33" runat="server" Text="Agent Bill Amount" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtAgentBillAmnt" runat="server" CssClass="textbox" Width="140px" Enabled="false"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtAgentBillAmnt" 
                                     FilterType="Custom, Numbers"  ></cc1:FilteredTextBoxExtender>
                                </td> 
                            </tr>
                              <tr  >
                                <td  >
                                     <asp:Label ID="Label37" runat="server" Text="Total Cost" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtTotalCost" runat="server" CssClass="textbox" Width="140px" Enabled="false"></asp:TextBox>                                    
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" TargetControlID="txtTotalCost" 
                                     FilterType="Custom, Numbers"  ></cc1:FilteredTextBoxExtender>
                                </td>                                
                                <td >
                                     <asp:Label ID="Label26" runat="server" Text="Booking Passed To" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtBookingPassed" runat="server" CssClass="textbox" Width="140px" Enabled="false"></asp:TextBox>
                                 <%--   <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtBookingPassed" 
                                     FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"  ></cc1:FilteredTextBoxExtender>--%>
                                </td>
                                <td >&nbsp;</td> 
                            </tr>
                        </table>

                    <div class="clear"></div>
                    <div class="buttonrow">
                        <asp:Button ID="btnVehiTicketBook" runat="server" Text="Ticket Book" meta:resourcekey="btnOkResource1" OnClick="btnVehiTicketBook_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" meta:resourcekey="btnCancelResource1" OnClick="btnCancel_Click"  />
                    </div>
                    <br />
                    </asp:Panel>
                   
                   <%-- --------------------%>     
    </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>    
                 </div>
                </ContentTemplate>
                </cc1:TabPanel>

           </cc1:TabContainer>
    </ContentTemplate>
 </asp:UpdatePanel>
             <%--End of pnlEntryTicket update panel--%>

</asp:Content>
