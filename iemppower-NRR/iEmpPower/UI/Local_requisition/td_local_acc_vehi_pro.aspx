<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="td_local_acc_vehi_pro.aspx.cs" Inherits="iEmpPower.UI.Local_requisition.td_local_acc_vehi_pro"  EnableEventValidation="false"  Theme="SkinFile"%>
<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls" TagPrefix="BDP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
      <%-- RK: Validating the date and time --%>
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
        function ArrivalTimeTextBoxChanged() {
            var ArrivalTimeTextBox = document.getElementById("<%=ArrivalTimeTextBox.ClientID %>");
            if (!TimeValidator(ArrivalTimeTextBox)) {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Please enter correct arrival time. The time should be in 24 hour format.";
                document.getElementById("<%=lblMessageBoard.ClientID %>").style.color = "red";
                ArrivalTimeTextBox.focus();
                return false;
            }
            else {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "";
            }
        }
        function DepartureTimeTextBoxChanged() {
            var DepartureTimeTextBox = document.getElementById("<%=DepartureTimeTextBox.ClientID %>");
            if (!TimeValidator(DepartureTimeTextBox)) {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Please enter correct departure time. The time should be in 24 hour format.";
                document.getElementById("<%=lblMessageBoard.ClientID %>").style.color = "red";
                DepartureTimeTextBox.focus();
                return false;
            }
            else {
                document.getElementById('<%=lblMessageBoard.ClientID %>').innerHTML = "";
            }
        }
    </script>

    <script type ="text/javascript">
        function GenerateValidation() {
            var CheckInDateTextBox = document.getElementById("<%=CheckInDateTextBox.ClientID %>");
            var CheckOutDateTextBox = document.getElementById("<%=CheckOutDateTextBox.ClientID %>");
            var ArrivalTimeTextBox = document.getElementById("<%=ArrivalTimeTextBox.ClientID %>");
            var DepartureTimeTextBox = document.getElementById("<%=DepartureTimeTextBox.ClientID %>");

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

            //Validating the arrival time .
            if (!TimeValidator(ArrivalTimeTextBox)) {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Please enter correct arrival time. The time should be in 24 hour format.";
                document.getElementById("<%=lblMessageBoard.ClientID %>").style.color = "red";
                ArrivalTimeTextBox.focus();
                return false;
            }

            //Validating the departure time .
            if (!TimeValidator(DepartureTimeTextBox)) {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Please enter correct departure time. The time should be in 24 hour format.";
                document.getElementById("<%=lblMessageBoard.ClientID %>").style.color = "red";
                DepartureTimeTextBox.focus();
                return false;
            }

            if (!CompareDates(CheckInDateTextBox, CheckOutDateTextBox)) {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Check in date should be less than check out date.";
                document.getElementById("<%=lblMessageBoard.ClientID %>").style.color = "red";
                CheckInDateTextBox.focus();
                return false;
            }

        }
    </script>
    <%--//==========vehicle=======================--%>
    <script src="../Utilities/Validations.js" type="text/javascript"></script>

    <script type="text/javascript">
        function CheckInDateTextBoxChangedV() {
            var CheckInDateTextBoxV = document.getElementById("<%= CheckInDateTextBoxV.ClientID %>")
            if (!DateValidator(CheckInDateTextBoxV)) {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Enter valid check in  date (DD-MM-YYYY).";
                     document.getElementById("<%=lblMessageBoard.ClientID %>").style.color = "red";
                     CheckInDateTextBoxV.focus();
                     return false;
                 }
                 else {
                     document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "";
                 }
             }
             function CheckOutDateTextBoxChangedV() {
                 var CheckOutDateTextBoxV = document.getElementById("<%= CheckOutDateTextBoxV.ClientID %>")
            if (!DateValidator(CheckOutDateTextBoxV)) {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Enter valid check out date (DD-MM-YYYY).";
                document.getElementById("<%=lblMessageBoard.ClientID %>").style.color = "red";
                CheckOutDateTextBoxV.focus();
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

        function GenerateValidationV() {
            var CheckInDateTextBoxV = document.getElementById("<%=CheckInDateTextBoxV.ClientID %>");
            var CheckOutDateTextBoxV = document.getElementById("<%=CheckOutDateTextBoxV.ClientID %>");
            var PickuptimeTextBox = document.getElementById("<%=PickuptimeTextBox.ClientID %>");

            //Checking, Is chick in date empty
            if (!TextBoxEmpty(CheckInDateTextBoxV)) {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Please select the check in date.";
                document.getElementById("<%=lblMessageBoard.ClientID %>").style.color = "red";
                CheckInDateTextBoxV.focus();
                return false;
            }

            //Checking, Is chick out  date empty
            if (!TextBoxEmpty(CheckOutDateTextBoxV)) {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Please select the check out date.";
                document.getElementById("<%=lblMessageBoard.ClientID %>").style.color = "red";
                CheckOutDateTextBoxV.focus();
                return false;
            }

            //Validationg the check in date
            if (!DateValidator(CheckInDateTextBoxV)) {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Enter valid check in  date (DD-MM-YYYY).";
                document.getElementById("<%=lblMessageBoard.ClientID %>").style.color = "red";
                CheckInDateTextBoxV.focus();
                return false;
            }
            else {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "";
            }

            //validating the check out date
            if (!DateValidator(CheckOutDateTextBoxV)) {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Enter valid check out date (DD-MM-YYYY).";
                document.getElementById("<%=lblMessageBoard.ClientID %>").style.color = "red";
                CheckOutDateTextBoxV.focus();
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

            if (!CompareDates(CheckInDateTextBoxV, CheckOutDateTextBoxV)) {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Check in date should be less than check out date.";
                document.getElementById("<%=lblMessageBoard.ClientID %>").style.color = "red";
                CheckInDateTextBoxV.focus();
                return false;
            }
        }

        </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

           <h2>Proposal Approval </h2>
    <br />    
            <asp:Label ID="lblMessage" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1"></asp:Label><br />    
     <br />
       <cc1:TabContainer ID="tcDefalut" runat="server" ActiveTabIndex="0" AutoPostBack="True" onactivetabchanged="tcDefalut_ActiveTabChanged" meta:resourcekey="tcDefalutResource1" >
         
           <cc1:TabPanel runat="server" HeaderText="Accommodation" ID="tabAccommodation"  meta:resourcekey="TabPanel1Resource1">
            <HeaderTemplate>Accommodation</HeaderTemplate>
                <ContentTemplate>
                    <div>
                    <asp:UpdatePanel ID="upPnlAcc" runat="server">
                        <ContentTemplate>

                             <div>
      <h2>Local accommodation requisition</h2>
      <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard"></asp:Label>
        <div class="legend">Search </div>
        <fieldset class="search-bg">
             <div>
                    <asp:Label ID="LocalRequisitionLabel" runat="server" Text="Accommodation type :" CssClass="label" Visible="false"></asp:Label>
                    <asp:DropDownList ID="LocalRequisitionDropDownList" runat="server" Width="150px" Visible="false"></asp:DropDownList>
                    <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="SearchButton_Click" Visible="false" />
                </div>
                <div class="clear"></div>
              <%-- <asp:UpdatePanel  ID="LocalRequisitionUpdatePanel" runat="server">
                   <ContentTemplate>--%>
                       <asp:GridView ID="LocalRequisitionGridView" runat="server" AutoGenerateColumns="False"
                                Width="100%"  AllowPaging="True" AllowSorting="True" PageSize="4" OnPageIndexChanging="LocalRequisitionGridView_PageIndexChanging" 
                           OnRowDataBound="LocalRequisitionGridView_RowDataBound" OnSelectedIndexChanged="LocalRequisitionGridView_SelectedIndexChanged"
                            OnSorting="LocalRequisitionGridView_Sorting">
                                <Columns>
                                                                        <asp:BoundField DataField="Accommadation_req_id" HeaderText="Requisition Id" SortExpression="local_acc_req_id">
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
                                                                             <asp:BoundField DataField="EMPLOYEE_NO" HeaderText="Employee No." >
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField>                                                                      
                                                                            <asp:BoundField DataField="LOCAL_ACCOMMODATION_TYPE" HeaderText="LOCAL_ACCOMMODATION_TYPE" Visible="False"
                                                                                SortExpression="LOCAL_ACCOMMODATION_TYPE">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle CssClass="gridTextStyle" />
                                                                            </asp:BoundField>                                                          
                                                                         <asp:BoundField DataField="Arrival_time" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Arrival Time" >
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Departure_time" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Departure Time" >
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField>
                                                                         <asp:BoundField DataField="Additional_service" HeaderText="Additional service">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField>
                                                                            <asp:BoundField DataField="Created_by" HeaderText="created_by" Visible="false">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField> 
                                                                           <asp:BoundField DataField="Created_on" HeaderText="created_on" Visible="false">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField> 
                                                                            <asp:BoundField DataField="Modified_by" HeaderText="modified_by" Visible="false">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField> 
                                                                            <asp:BoundField DataField="Modified_on" HeaderText="modified_on" Visible="false">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField> 
                                                                            <asp:BoundField DataField="Is_active" HeaderText="isActive" Visible="false">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField> 
                                                                          <asp:BoundField DataField="HOTEL_CAT_CODE" HeaderText="HOTEL_CAT_CODE" Visible="true">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField>  
                                                                          <asp:BoundField DataField="HOTEL_CODE" HeaderText="HOTEL_CODE" Visible="true">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField>  
                                                                          <asp:BoundField DataField="ROOM_CODE" HeaderText="RoomCategoryName" Visible="true">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField> 
                                    <asp:BoundField DataField="REASON_FOR_CANCEL" HeaderText="Reason To Cancel"
                                        SortExpression="REASON_FOR_CANCEL">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField> 
                                </Columns>
                            </asp:GridView>
                <%--   </ContentTemplate>
               </asp:UpdatePanel>--%>
        </fieldset>
    <hr />
    <div class="legend">Entry </div>
    <div class="clear"></div>
    <div>
        <asp:RadioButtonList ID="AccommodationTypeRadioButtonList" runat="server" OnSelectedIndexChanged="AccommodationTypeRadioButtonList_SelectedIndexChanged" RepeatDirection="Horizontal" AutoPostBack="True">
            <asp:ListItem Text="Single" Value="1" Selected="True" ></asp:ListItem>
            <asp:ListItem Text="Group" Value="2"></asp:ListItem>
            <asp:ListItem Text="Guest" Value="3"></asp:ListItem>
        </asp:RadioButtonList>
        <div class="clear"></div>
                    <asp:Label ID="Label14" runat="server" Text="Check in date" CssClass="label" ></asp:Label>
                    <asp:TextBox ID="CheckInDateTextBox" runat="server" Width="166px" CssClass="textbox"></asp:TextBox>
                    <cc1:CalendarExtender ID="CheckInDateCalendarExtender" runat="server" PopupButtonID="CheckInDateImageButton" TargetControlID="CheckInDateTextBox" Format="dd-MM-yyyy"></cc1:CalendarExtender>
                    <asp:ImageButton ID="CheckInDateImageButton" runat="server" ImageUrl="~/images/calendar_icon.png" CssClass="gncontrol" Width="24" Height="22" />
                    <br />
                    <asp:Label ID="Label16" runat="server" Text="Check out date" CssClass="label"></asp:Label>
                    <asp:TextBox ID="CheckOutDateTextBox" runat="server" Width="166px" CssClass="textbox"></asp:TextBox>
                    <cc1:CalendarExtender ID="CheckOutDateCalendarExtender" runat="server" PopupButtonID="CheckOutDateImageButton" TargetControlID="CheckOutDateTextBox" Format="dd-MM-yyyy"></cc1:CalendarExtender>
                    <asp:ImageButton ID="CheckOutDateImageButton" runat="server" ImageUrl="~/images/calendar_icon.png" CssClass="gncontrol" Width="24" Height="22" />
                    <br />
                    
                    <asp:Label ID="Label17" runat="server" Text="Arrival time" CssClass="label"></asp:Label>
                    <asp:TextBox ID="ArrivalTimeTextBox" runat="server" CssClass="textbox"></asp:TextBox><asp:Label ID="Label18" runat="server" Text="HH:MM" CssClass="note"></asp:Label>
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
                    <asp:Label ID="Label19" runat="server" Text="Departure time" CssClass="label"></asp:Label>
                    <asp:TextBox ID="DepartureTimeTextBox" runat="server" CssClass="textbox"></asp:TextBox><asp:Label ID="Label20" runat="server" Text="HH:MM" CssClass="note"></asp:Label>
                    <cc1:MaskedEditExtender runat="server" ID="DepartureTimeMaskedEditExtender"
                        TargetControlID="DepartureTimeTextBox"
                        Mask="99:99" InputDirection="LeftToRight"
                        MessageValidatorTip="true"
                        MaskType="Time"
                        AcceptAMPM="false"
                        ErrorTooltipEnabled="True">
                    </cc1:MaskedEditExtender>
                    <br />
                    <asp:Label ID="Label21" runat="server" Text="Hotel place/city" CssClass="label" meta:resourcekey="Label12Resource1"></asp:Label>
                    <%--<asp:TextBox ID="HotelPlaceCityTextBox" runat="server" CssClass="textbox"></asp:TextBox>--%>
                    <asp:DropDownList ID="DropDownHotelPlaceCity" runat="server" CssClass="textbox" Width="206px" AutoPostBack="True"></asp:DropDownList>
                    <br /> 
                    <asp:Label ID="Label22" runat="server" Text="Hotel category" CssClass="label" meta:resourcekey="Label12Resource1"></asp:Label>
                    <asp:DropDownList ID="DropDownHotel_category" runat="server" CssClass="textbox" Width="206px" AutoPostBack="True" meta:resourcekey="DropDownHotel_categoryResource1"></asp:DropDownList>
                    <br />
                   
                    <asp:Label ID="Label24" runat="server" Text="Hotel name" CssClass="label" meta:resourcekey="Label14Resource1"></asp:Label>
                    <asp:DropDownList ID="DropDownHotel_name" runat="server" CssClass="textbox" Width="206px" AutoPostBack="True" meta:resourcekey="DropDownHotel_nameResource1"></asp:DropDownList>
                    <br />
        <asp:Label ID="Label23" runat="server" Text="Room category" CssClass="label" meta:resourcekey="Label13Resource1"></asp:Label>
                    <asp:DropDownList ID="DropDownRoom_category" runat="server" CssClass="textbox" Width="206px" AutoPostBack="True" meta:resourcekey="DropDownRoom_categoryResource1"></asp:DropDownList>
                    <br />
                    <asp:Label ID="Label25" runat="server" Text="Mode of payment" CssClass="label"></asp:Label>
                    <asp:TextBox ID="AdditionalServicesTextBox" runat="server" CssClass="textbox"></asp:TextBox>
                   
                    <div id="NumbersOfMembers" runat="server"  visible="false">
                    <asp:Label ID="Label26" runat="server" Text="Number of members" CssClass="label" ></asp:Label>
                    <asp:TextBox ID="NumberOfMembersTextBox" runat="server" CssClass="textbox"></asp:TextBox>
                    <cc1:FilteredTextBoxExtender ID="NumberOfMemberFilterTextBoxExtender" runat="server" TargetControlID="NumberOfMembersTextBox" 
                         FilterType="Numbers" ></cc1:FilteredTextBoxExtender>
                    </div>
                   
                    <asp:Panel ID="NameOfMemberPanel" runat="server" Visible="false">
                    <asp:Label ID="Label27" runat="server" Text="Name of members" CssClass="label"></asp:Label>
                    <asp:TextBox ID="NameOfMembersTextBox" runat="server" CssClass="textbox"></asp:TextBox>
                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="NameOfMembersTextBox" 
                            FilterType="Numbers,LowercaseLetters, UppercaseLetters,Custom" ValidChars=" "  ></cc1:FilteredTextBoxExtender>
                         <asp:Button ID="AddToGridButton" runat="server" Text="Add to list" OnClick="AddToGridButton_Click" />                   
                    </asp:Panel>
                    <%---start---Grid to display the member names --%>
         
                  <div style="margin-left: 164px">
                    <asp:GridView ID="EmployeeDetailsGridView" runat="server"  AutoGenerateColumns="False" Style="margin-right: 0px" OnSelectedIndexChanged="EmployeeDetailsGridView_SelectedIndexChanged" OnRowDeleting="EmployeeDetailsGridView_RowDeleting">
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
                      <asp:Panel runat="server" ID="EmployeeNamePanel" Height="200px" ScrollBars="Both" Width="450px" Style="overflow: scroll;" Visible="false" ></asp:Panel>
                      <cc1:AutoCompleteExtender ID="EmpNameAutoCompleteExtender" runat="server"  CompletionInterval="25"  EnableCaching="true" MinimumPrefixLength="1" 
                          ServiceMethod="GetEmployeeNamesAndId" ServicePath="~/UI/Benefits_Payment/EmployeeNames.asmx" CompletionListCssClass="AutoCompleteList" 
                          TargetControlID="NameOfMembersTextBox" FirstRowSelected="true" CompletionListElementID="EmployeeNamePanel"></cc1:AutoCompleteExtender>
                    <%---end-------Auto complete extender  --%>
        
                    <asp:Label ID="Label28" runat="server" Text="Remarks" CssClass="label"></asp:Label>
                    <asp:TextBox ID="RemarksTextBox" runat="server" CssClass="textbox"></asp:TextBox>
          <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="RemarksTextBox" 
                                          FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" ValidChars=" ,#@" ></cc1:FilteredTextBoxExtender>
                    <br />
                    <div class="clear"></div>
                    <div class="buttonrow">
                        <asp:Button ID="SaveButton" runat="server" Text="Approve" OnClick="SaveButton_Click" OnClientClick="return GenerateValidation()"/>
                        <asp:Button ID="CancelButton" runat="server" Text="Reject" OnClick="CancelButton_Click"/>
                    </div>
                    <br />                      
    </div>
     </div>

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
          <h2>Local vehicle requisition</h2>

      <asp:Label ID="Label1" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1"></asp:Label><br />
            <div class="legend">Search </div>
            <fieldset class="search-bg">

                 <div>
                    <asp:Label ID="Label2" runat="server" Text="Travel type :" CssClass="label" meta:resourcekey="LocalRequisitionLabelResource1" Visible="false"></asp:Label>
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="150px" meta:resourcekey="LocalRequisitionDropDownListResource1" Visible="false"></asp:DropDownList>
                    <asp:Button ID="Button1" runat="server" Text="Search" meta:resourcekey="SearchButtonResource1" OnClick="SearchButtonV_Click" Visible="false"/>
                </div>
                <br />
               <%--  <asp:UpdatePanel  ID="LocalRequisitionUpdatePanel" runat="server">
                   <ContentTemplate>--%>
                       <asp:GridView ID="LocalTravelRequisitionGridView" runat="server" AutoGenerateColumns="False"
                            OnPageIndexChanging="LocalTravelRequisitionGridView_PageIndexChanging"
                              OnSelectedIndexChanged="LocalTravelRequisitionGridView_SelectedIndexChanged"
                           OnRowDataBound="LocalTravelRequisitionGridView_RowDataBound" Width="100%"  AllowPaging="True" AllowSorting="True" 
                                             
                                PageSize="4" OnSorting="LocalTravelRequisitionGridView_Sorting">
                                <Columns>
                                                                          <asp:BoundField DataField="local_travel_req_id">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle CssClass="gridTextStyle" />
                                                                            </asp:BoundField>
                                                                         <%--   <asp:BoundField DataField="Date_of_travel" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Travel Date"
                                                                                SortExpression="Date_of_travel">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle CssClass="gridTextStyle" />
                                                                            </asp:BoundField>--%>
                                                                            <asp:BoundField DataField="VehicleNameAssigned" HeaderText="Vehicle Name"
                                                                                SortExpression="VehicleNameAssigned">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle CssClass="gridTextStyle" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Duration_from" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Duration From"
                                                                                SortExpression="Duration_from">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle CssClass="gridTextStyle" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Duration_to" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Duration To"
                                                                                SortExpression="Duration_to">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle CssClass="gridTextStyle" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Purpose_of_travel" HeaderText="Purpose" SortExpression="Travel Purpose">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle CssClass="gridTextStyle" />
                                                                            </asp:BoundField>
                                                                                 <asp:BoundField DataField="EMPLOYEE_NO" HeaderText="PERNR"  Visible="true" >
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle CssClass="gridTextStyle" />
                                                                            </asp:BoundField>
                                                                             <asp:BoundField DataField="Departure_from" HeaderText="From">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle CssClass="gridTextStyle" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Destination_to" HeaderText="To" >
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle CssClass="gridTextStyle" />
                                                                            </asp:BoundField> 
                                                                                <asp:BoundField DataField="Carrying_any_materials" HeaderText="Materials" Visible="true">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle CssClass="gridTextStyle" />
                                                                            </asp:BoundField> 
                                                                             <asp:BoundField DataField="Pickup_time" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Pickup Time"  Visible="true">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle CssClass="gridTextStyle" />
                                                                            </asp:BoundField> 
                                                                                <asp:BoundField DataField="Pickup_address" HeaderText="Pickup Address"  Visible="true">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle CssClass="gridTextStyle" />
                                                                            </asp:BoundField> 
                                                                             <asp:BoundField DataField="Drop_time" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Drop Time" Visible="true" >
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle CssClass="gridTextStyle" />
                                                                            </asp:BoundField>                                     
                                                                                <asp:BoundField DataField="Drop_address" HeaderText="Drop Address"  Visible="true">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle CssClass="gridTextStyle" />
                                                                            </asp:BoundField>                                    
                                                                                <asp:BoundField DataField="Vehicle_type" HeaderText="Vehicle Type" Visible="true">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle CssClass="gridTextStyle" />
                                                                            </asp:BoundField>                               
                                                                                <asp:BoundField DataField="Vehicle_category" HeaderText="Vehicle Class"  Visible="true">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle CssClass="gridTextStyle" />
                                                                            </asp:BoundField>                           
                                                                                <asp:BoundField DataField="Additional_services" HeaderText="Additional services" Visible="true">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle CssClass="gridTextStyle" />
                                                                            </asp:BoundField>                             
                                                                                <asp:BoundField DataField="remarks" HeaderText="Remarks" Visible="true">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle CssClass="gridTextStyle" />
                                                                            </asp:BoundField>  
                                                                               <asp:BoundField DataField="current_status" HeaderText="status"  >
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle CssClass="gridTextStyle" />
                                                                            </asp:BoundField> 
                                                                                <asp:BoundField DataField="STATUS_UPDATED_BY" HeaderText="STATUS_UPDATED_BY" Visible="false">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle CssClass="gridTextStyle" />
                                                                            </asp:BoundField> 
                                                                                <asp:BoundField DataField="CRAETEDBY" HeaderText="created_by" Visible="false">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle CssClass="gridTextStyle" />
                                                                            </asp:BoundField> 
                                                                                <asp:BoundField DataField="CREATEDON" HeaderText="created_on"  Visible="false">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle CssClass="gridTextStyle" />
                                                                            </asp:BoundField> 
                                                                                <asp:BoundField DataField="MODIFIEDBY" HeaderText="modified_by" Visible="false">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle CssClass="gridTextStyle" />
                                                                            </asp:BoundField> 
                                                                                <asp:BoundField DataField="MODIFIEDON" HeaderText="modified_on" Visible="false">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle CssClass="gridTextStyle" />
                                                                            </asp:BoundField>  
                                                                                <asp:BoundField DataField="status" HeaderText="isActive" Visible="false" >
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle CssClass="gridTextStyle" />
                                                                            </asp:BoundField>  
                                    <asp:BoundField DataField="REASON_FOR_CANCEL" HeaderText="Reason To Cancel"
                                        SortExpression="REASON_FOR_CANCEL">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                                                           <%-- <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="VehRequiLinkButton" runat="server" PostBackUrl='<%# "~/UI/Benefits_Payment/new_vehicle_requisition.aspx?RequiSegmentId="+Eval("REQ_SEGMENT_ID") + "&PreviousPage=" + "Requisition_Ticket_History.aspx"+"&TravelType=" + "1" + "&VehicleID=" + Eval("Vehicle_req_id")%>'>View Details</asp:LinkButton>
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
            <div class="clear"></div>
            <asp:RadioButtonList ID="RadioButtontraveltype" AutoPostBack="True" runat="server"  RepeatDirection="Horizontal" meta:resourcekey="RadioButtontraveltypeResource1" OnSelectedIndexChanged="RadioButtontraveltype_SelectedIndexChanged">
                <asp:ListItem Value="1" meta:resourcekey="ListItemResource1">Single</asp:ListItem>
                <asp:ListItem Value="2" meta:resourcekey="ListItemResource2">Group</asp:ListItem>
                <asp:ListItem Value="3" meta:resourcekey="ListItemResource3">Guest</asp:ListItem>
            </asp:RadioButtonList>
            <br />
                <asp:Label ID="Label5" runat="server" Text="From date" CssClass="label" ></asp:Label>
                    <asp:TextBox ID="CheckInDateTextBoxV" runat="server" Width="166px" CssClass="textbox"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="CheckInDateImageButton" TargetControlID="CheckInDateTextBoxV" Format="dd-MM-yyyy"></cc1:CalendarExtender>
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/calendar_icon.png" CssClass="gncontrol" Width="24" Height="22" />
                    <br />
                    <asp:Label ID="Label8" runat="server" Text="To date" CssClass="label"></asp:Label>
                    <asp:TextBox ID="CheckOutDateTextBoxV" runat="server" Width="166px" CssClass="textbox"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="CheckOutDateImageButton" TargetControlID="CheckOutDateTextBoxV" Format="dd-MM-yyyy"></cc1:CalendarExtender>
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/calendar_icon.png" CssClass="gncontrol" Width="24" Height="22" />
                <br />

                    <asp:Label ID="Label3" runat="server" Text="From" CssClass="label" meta:resourcekey="Label4Resource1"></asp:Label>
                    <asp:TextBox ID="FromTextBox" runat="server" CssClass="textbox" meta:resourcekey="TextBox3Resource1"></asp:TextBox>
                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="FromTextBox" 
                         FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"  ValidChars=" ,#@" ></cc1:FilteredTextBoxExtender>
                    <br />
                    <asp:Label ID="Label4" runat="server" Text="To" CssClass="label" meta:resourcekey="Label5Resource1"></asp:Label>
                    <asp:TextBox ID="ToTextBox" runat="server" CssClass="textbox" meta:resourcekey="TextBox6Resource1"></asp:TextBox>
                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="ToTextBox" 
                         FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"  ValidChars=" ,#@" ></cc1:FilteredTextBoxExtender>
                    <br />
                    <asp:Label ID="Label6" runat="server" Text="Vehicle type" CssClass="label" meta:resourcekey="Label12Resource1"></asp:Label>
                    <asp:DropDownList ID="DropDownVehicletype" runat="server" CssClass="textbox" OnSelectedIndexChanged="DropDownVehicletype_SelectedIndexChanged" Width="206px"  AutoPostBack="True" meta:resourcekey="DropDownList1Resource1"></asp:DropDownList>
                    <br />
                    <asp:Label ID="Label7" runat="server" Text="Vehicle category" CssClass="label" meta:resourcekey="Label13Resource1"></asp:Label>
                    <asp:DropDownList ID="DropDownVehiclecategory" runat="server" Enabled="False" CssClass="textbox" OnSelectedIndexChanged="DropDownVehiclecategory_SelectedIndexChanged"  Width="206px"  AutoPostBack="True" meta:resourcekey="DropDownList2Resource1"></asp:DropDownList>
                    <br />
                    <asp:Label ID="Label15" runat="server" Text="Vehicle name" CssClass="label" meta:resourcekey="Label14Resource1"></asp:Label>
                    <asp:DropDownList ID="DropDownVehiclename" runat="server" Enabled="False" CssClass="textbox" Width="206px" AutoPostBack="True" meta:resourcekey="DropDownList7Resource1"></asp:DropDownList>
                    <br />
                    <asp:Label ID="Label9" runat="server" Text="Pickup time" CssClass="label" meta:resourcekey="Label8Resource1"></asp:Label>
                    <asp:TextBox ID="PickuptimeTextBox" runat="server" CssClass="textbox" ></asp:TextBox>
                    <cc1:MaskedEditExtender runat="server" ID="MaskedEditExtender2" TargetControlID="PickuptimeTextBox" Mask="99:99" MaskType="Time"
                        ErrorTooltipEnabled="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" 
                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                    </cc1:MaskedEditExtender>
                    <br />
                    <asp:Label ID="Label10" runat="server" Text="Pickup address" CssClass="label" meta:resourcekey="Label9Resource1"></asp:Label>
                    <asp:TextBox ID="PickupaddressTextBox" runat="server" CssClass="textbox" ></asp:TextBox>
                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="PickupaddressTextBox" 
                         FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"  ValidChars=" ,#@" ></cc1:FilteredTextBoxExtender>
                    <br />
                    <asp:Label ID="Label11" runat="server" Text="Drop time" CssClass="label" meta:resourcekey="Label10Resource1"></asp:Label>
                    <asp:TextBox ID="DroptimeTextBox" runat="server" CssClass="textbox" meta:resourcekey="TextBox4Resource1"></asp:TextBox>
                    <cc1:MaskedEditExtender runat="server" ID="MaskedEditExtender3" TargetControlID="DroptimeTextBox" Mask="99:99"  MaskType="Time"
                        ErrorTooltipEnabled="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" 
                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                    </cc1:MaskedEditExtender>
                    <br />
                    <asp:Label ID="Label12" runat="server" Text="Drop address" CssClass="label" meta:resourcekey="Label11Resource1"></asp:Label>
               
                    <asp:TextBox ID="DropaddressTextBox" runat="server" CssClass="textbox" meta:resourcekey="TextBox5Resource1"></asp:TextBox>
                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="DropaddressTextBox" 
                                          FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" ValidChars=" ,#@" ></cc1:FilteredTextBoxExtender>
                    <br />
                     <asp:Label ID="Label13" runat="server" Text="Purpose of travel" CssClass="label" meta:resourcekey="Label6Resource1"></asp:Label>
                    <asp:TextBox ID="PurposeoftravelTextBox" runat="server" CssClass="textbox" meta:resourcekey="TextBox7Resource1"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="PurposeoftravelTextBox" 
                                          FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" ValidChars=" ,#@" ></cc1:FilteredTextBoxExtender>
                    <br/>
                   <asp:Panel ID="Panelmember" Visible="False" runat="server" meta:resourcekey="PanelmemberResource1">
                   <asp:Label ID="lblNumberofmembers" runat="server" Text="Number of members" CssClass="label" meta:resourcekey="Label15Resource1"></asp:Label>
                    <asp:TextBox ID="NumberofmembersTextBoxV" runat="server" CssClass="textbox"  meta:resourcekey="TextBox9Resource1"></asp:TextBox>
                   <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                        TargetControlID="NumberofmembersTextBoxV" Enabled="True" />
                    <br />                    
                   <asp:Label ID="Label29" runat="server" Text="Name of members" CssClass="label" meta:resourcekey="Label4Resource2"></asp:Label>
                    <asp:TextBox ID="NameOfMembersTextBoxV" runat="server" CssClass="textbox" meta:resourcekey="NameOfMembersTextBoxResource1"></asp:TextBox>
                   <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="NameOfMembersTextBoxV" 
                            FilterType="Numbers,LowercaseLetters, UppercaseLetters,Custom" ValidChars=" "  ></cc1:FilteredTextBoxExtender>
                   <asp:Button ID="AddToGridButtonV" runat="server" Text="Add to list" OnClick="AddToGridButtonV_Click" meta:resourcekey="AddToGridButtonResource1" />
                    
                  <div style="margin-left: 164px">
                    <asp:GridView ID="EmployeeDetailsGridViewV" runat="server"  AutoGenerateColumns="False" Style="margin-right: 0px" OnSelectedIndexChanged="EmployeeDetailsGridViewV_SelectedIndexChanged" OnRowDeleting="EmployeeDetailsGridViewV_RowDeleting" meta:resourcekey="EmployeeDetailsGridViewResource1">
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
                      <asp:Panel runat="server" ID="Panel1" Height="200px" ScrollBars="Both" Width="450px" Style="overflow: scroll;" Visible="False"
                           meta:resourcekey="EmployeeNamePanelResource1" ></asp:Panel>
                      <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  CompletionInterval="25" MinimumPrefixLength="1" 
                          ServiceMethod="GetEmployeeNamesAndId" ServicePath="~/UI/Benefits_Payment/EmployeeNames.asmx" CompletionListCssClass="AutoCompleteList" 
                          TargetControlID="NameOfMembersTextBoxV" FirstRowSelected="True" CompletionListElementID="EmployeeNamePanel" DelimiterCharacters="" 
                          Enabled="True"></cc1:AutoCompleteExtender>
                      
                    </asp:Panel>
                  
                    <asp:Label ID="Label30" runat="server" Text="Remarks" CssClass="label" meta:resourcekey="Label15Resource1"></asp:Label>
                    <asp:TextBox ID="RemarksTextBoxV" runat="server" CssClass="textbox" meta:resourcekey="TextBox9Resource1"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="RemarksTextBoxV" 
                                          FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"  ValidChars=" ,#@"></cc1:FilteredTextBoxExtender>
                  
                    <div class="clear"></div>
                    <div class="buttonrow">
                        <asp:Button ID="btnVehiApprove" runat="server" Text="Approve" meta:resourcekey="btnOkResource1" OnClick="btnVehiApprove_Click" OnClientClick="return GenerateValidationV()"/>
                        <asp:Button ID="btnVehiReject" runat="server" Text="Reject" meta:resourcekey="btnCancelResource1" OnClick="btnVehiReject_Click" style="height: 26px" />
                    </div>
    </div>
     </div>
                                      
                        </ContentTemplate>
                    </asp:UpdatePanel>    
                 </div>
                </ContentTemplate>
                </cc1:TabPanel>

           </cc1:TabContainer>

</asp:Content>
