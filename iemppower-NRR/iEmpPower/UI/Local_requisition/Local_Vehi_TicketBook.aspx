<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Local_Vehi_TicketBook.aspx.cs" Inherits="iEmpPower.UI.Local_requisition.Local_Vehi_TicketBook" EnableEventValidation="false" Theme="SkinFile" %>

<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls" TagPrefix="BDP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
<%--     <script src="../Utilities/Validations.js" type="text/javascript"></script>

    <script type="text/javascript">
        function CheckInDateTextBoxChanged() {
            var CheckInDateTextBox = document.getElementById("<%= CheckInDateTextBox.ClientID %>")
            if (!DateValidator(CheckInDateTextBox)) {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Enter valid check in  date (DD-MM-YYYY).";
                     document.getElementById("<%=lblMessageBoard.ClientID %>").style.color = "red";
                     CheckInDateTextBox.focus();
                     return false;
                 }
                 else {
                     document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "";
                 }
             }
             function CheckOutDateTextBoxChanged() {
                 var CheckOutDateTextBox = document.getElementById("<%= CheckOutDateTextBox.ClientID %>")
            if (!DateValidator(CheckOutDateTextBox)) {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Enter valid check out date (DD-MM-YYYY).";
                document.getElementById("<%=lblMessageBoard.ClientID %>").style.color = "red";
                CheckOutDateTextBox.focus();
                return false;
            }
            else {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "";
            }
        }
        function PickuptimeTextBoxChanged() {
            var PickuptimeTextBox = document.getElementById("<%=PickuptimeTextBox.ClientID %>");
          if (!TimeValidator(PickuptimeTextBox)) {
              document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Please enter correct Pickup time. The time should be in 24 hour format.";
                  document.getElementById("<%=lblMessageBoard.ClientID %>").style.color = "red";
                  PickuptimeTextBox.focus();
                  return false;
              }
              else {
                  document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "";
              }
          }
          function DroptimeTextBoxChanged() {
              var DroptimeTextBox = document.getElementById("<%=DroptimeTextBox.ClientID %>");
            if (!TimeValidator(DroptimeTextBox)) {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Please enter correct drop time. The time should be in 24 hour format.";
                document.getElementById("<%=lblMessageBoard.ClientID %>").style.color = "red";
                DroptimeTextBox.focus();
                return false;
            }
            else {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "";
            }
        }

        function GenerateValidation() {
            var CheckInDateTextBox = document.getElementById("<%=CheckInDateTextBox.ClientID %>");
            var CheckOutDateTextBox = document.getElementById("<%=CheckOutDateTextBox.ClientID %>");
            var PickuptimeTextBox = document.getElementById("<%=PickuptimeTextBox.ClientID %>");

            //Checking, Is chick in date empty
            if (!TextBoxEmpty(CheckInDateTextBox)) {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Please select the check in date.";
                document.getElementById("<%=lblMessageBoard.ClientID %>").style.color = "red";
                CheckInDateTextBox.focus();
                return false;
            }

            //Checking, Is chick out  date empty
            if (!TextBoxEmpty(CheckOutDateTextBox)) {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Please select the check out date.";
                document.getElementById("<%=lblMessageBoard.ClientID %>").style.color = "red";
                CheckOutDateTextBox.focus();
                return false;
            }

            //Validationg the check in date
            if (!DateValidator(CheckInDateTextBox)) {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Enter valid check in  date (DD-MM-YYYY).";
                document.getElementById("<%=lblMessageBoard.ClientID %>").style.color = "red";
                CheckInDateTextBox.focus();
                return false;
            }
            else {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "";
            }

            //validating the check out date
            if (!DateValidator(CheckOutDateTextBox)) {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Enter valid check out date (DD-MM-YYYY).";
                document.getElementById("<%=lblMessageBoard.ClientID %>").style.color = "red";
                CheckOutDateTextBox.focus();
                return false;
            }
            else {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "";
            }

            if (!TimeValidator(PickuptimeTextBox)) {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Please enter correct Pickup time. The time should be in 24 hour format.";
                document.getElementById("<%=lblMessageBoard.ClientID %>").style.color = "red";
                PickuptimeTextBox.focus();
                return false;
            }
            else {
                document.getElementById('<%=lblMessageBoard.ClientID %>').innerHTML = "";
            }

            var DroptimeTextBox = document.getElementById("<%=DroptimeTextBox.ClientID %>");
            if (!TimeValidator(DroptimeTextBox)) {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Please enter correct drop time. The time should be in 24 hour format.";
                document.getElementById("<%=lblMessageBoard.ClientID %>").style.color = "red";
                DroptimeTextBox.focus();
                return false;
            }
            else {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "";
            }

            if (!CompareDates(CheckInDateTextBox, CheckOutDateTextBox)) {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Check in date should be less than check out date.";
                document.getElementById("<%=lblMessageBoard.ClientID %>").style.color = "red";
                CheckInDateTextBox.focus();
                return false;
            }
        }

        </script>--%>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div>
          <h2>Local Vehicle Booking</h2>

      <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1"></asp:Label><br />
        
                 <asp:UpdatePanel  ID="LocalRequisitionUpdatePanel" runat="server">
                   <ContentTemplate>

            <div class="legend">Search </div>
            <fieldset class="search-bg">

                 <div>
                    <asp:Label ID="LocalRequisitionLabel" runat="server" Text="Travel type :" CssClass="label" meta:resourcekey="LocalRequisitionLabelResource1"></asp:Label>
                    <asp:DropDownList ID="LocalRequisitionDropDownList" runat="server" Width="150px" meta:resourcekey="LocalRequisitionDropDownListResource1"></asp:DropDownList>
                    <asp:Button ID="SearchButton" runat="server" Text="Search" meta:resourcekey="SearchButtonResource1" OnClick="SearchButton_Click" />
                </div>
                <br />
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
                                </Columns>
                            </asp:GridView>
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

                    <asp:Label ID="Label1" runat="server" Text="From" CssClass="label" meta:resourcekey="Label4Resource1"></asp:Label>
                    <asp:TextBox ID="FromTextBox" runat="server" CssClass="textbox" meta:resourcekey="TextBox3Resource1"></asp:TextBox>
                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="FromTextBox" 
                         FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"  ValidChars=" ,#@" ></cc1:FilteredTextBoxExtender>
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="To" CssClass="label" meta:resourcekey="Label5Resource1"></asp:Label>
                    <asp:TextBox ID="ToTextBox" runat="server" CssClass="textbox" meta:resourcekey="TextBox6Resource1"></asp:TextBox>
                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="ToTextBox" 
                         FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"  ValidChars=" ,#@" ></cc1:FilteredTextBoxExtender>
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="Vehicle type" CssClass="label" meta:resourcekey="Label12Resource1"></asp:Label>
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
                                          FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"  ValidChars=" ,#@"></cc1:FilteredTextBoxExtender>
                    <br />
                     <asp:Label ID="Label21" runat="server" Text="Purpose of travel" CssClass="label" meta:resourcekey="Label6Resource1"></asp:Label>
                    <asp:TextBox ID="PurposeoftravelTextBox" runat="server" CssClass="textbox" meta:resourcekey="TextBox7Resource1"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="PurposeoftravelTextBox" 
                                          FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"  ValidChars=" ,#@"></cc1:FilteredTextBoxExtender>
                    <br/>
                   <asp:Panel ID="Panelmember" Visible="False" runat="server" meta:resourcekey="PanelmemberResource1">
                   <asp:Label ID="lblNumberofmembers" runat="server" Text="Number of members" CssClass="label" meta:resourcekey="Label15Resource1"></asp:Label>
                    <asp:TextBox ID="NumberofmembersTextBox" runat="server" CssClass="textbox"  meta:resourcekey="TextBox9Resource1"></asp:TextBox>
                   <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                        TargetControlID="NumberofmembersTextBox" Enabled="True" />
                    <br />                    
                   <asp:Label ID="Label4" runat="server" Text="Name of members" CssClass="label" meta:resourcekey="Label4Resource2"></asp:Label>
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
                  
                   <%--<div class="clear"></div>
                    <div class="buttonrow">
                        <asp:Button ID="SaveButton" runat="server" Text="Save" meta:resourcekey="btnOkResource1" OnClick="SaveButton_Click" OnClientClick="return GenerateValidation()"/>
                        <asp:Button ID="CancelButton" runat="server" Text="Cancel" meta:resourcekey="btnCancelResource1" OnClick="CancelButton_Click" style="height: 26px" />
                    </div>--%>
                                
</asp:Panel> 
    </div>
        <asp:Panel ID="pnlVehiTicketbooking" runat="server" CssClass="auto-style1" Visible="False"> 
                        <h3>Travel Desk Arrangement Area</h3>
                        <table align="left" cellpadding="0" cellspacing="0" class="auto-style1">
                            <tr>
                                <td>
                                  <asp:Label ID="Label6" runat="server" Text="Vehicle Source" CssClass="label"></asp:Label>
                                  <asp:DropDownList ID="drpdwnVehicleSource" runat="server" CssClass="textbox" Width="140px" AutoPostBack="True" ></asp:DropDownList>
                                </td>
                                <td >
                                       <asp:Label ID="Label29" runat="server" Text="Driver Name" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtDriverName" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                 <%--   <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtDriverName" 
                                     FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"  ></cc1:FilteredTextBoxExtender>--%>
                                </td>
                              <td  >
                                    <asp:Label ID="Label53" runat="server" Text="Bill Date" CssClass="label" meta:resourcekey="Label1Resource1"></asp:Label>
                                    <asp:TextBox ID="txtBillDateA" runat="server" Width="140px" CssClass="textbox" Enabled="false" ></asp:TextBox>
                                    <AjaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="ImageButton3" TargetControlID="txtBillDateA" Format="dd-MM-yyyy"></AjaxToolkit:CalendarExtender>
                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/calendar_icon.png" CssClass="gncontrol" Width="24" Height="22" />
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
                                  <asp:Label ID="Label9" runat="server" Text="Agent Bill Number" CssClass="label"></asp:Label>
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
                                  FilterType="Custom, Numbers"  ></cc1:FilteredTextBoxExtender>
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
                        <asp:Button ID="btnAccept" runat="server" Text="Accept" meta:resourcekey="btnOkResource1" OnClick="btnAccept_Click" />
                        <asp:Button ID="btnSendToTD" runat="server" Text="Send To TD" OnClick="btnSendToTD_Click"  />                      
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" meta:resourcekey="btnCancelResource1" OnClick="btnCancel_Click"  />
                    </div>
                    <br />
                    </asp:Panel>
                   
                   <%-- --------------------%>

                   </ContentTemplate>
               </asp:UpdatePanel>
     
    </div>

</asp:Content>
