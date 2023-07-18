<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="local_accommodation_requisition.aspx.cs" Inherits="iEmpPower.UI.Benefits_Payment.local_accommodation_requisition" Theme="SkinFile" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%-- RK: Validating the date and time --%>
    <script type="text/javascript">
        function CheckInDateTextBoxChanged() {
            var CheckInDateTextBox = document.getElementById("<%= CheckInDateTextBox.ClientID %>")
            if (!DateValidator(CheckInDateTextBox)) {
                document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Enter valid check in  date (DD-MM-YYYY).";
                document.getElementById("<%=lblMessageBoard.ClientID %>").style.color="red";
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
                document.getElementById("<%=lblMessageBoard.ClientID %>").style.color="red";
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
      <h2>Accommodation requisition</h2>
      <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard"></asp:Label>
        <div class="legend">Search </div>
        <fieldset class="search-bg">
             <div>
                    <asp:Label ID="LocalRequisitionLabel" runat="server" Text="Accommodation type :" CssClass="label"></asp:Label>
                    <asp:DropDownList ID="LocalRequisitionDropDownList" runat="server" Width="150px"></asp:DropDownList>
                    <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="SearchButton_Click" />
                </div>
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
                    <asp:Label ID="Label7" runat="server" Text="Check in date" CssClass="label" ></asp:Label>
                    <asp:TextBox ID="CheckInDateTextBox" runat="server" Width="166px" CssClass="textbox"></asp:TextBox>
                    <cc1:CalendarExtender ID="CheckInDateCalendarExtender" runat="server" PopupButtonID="CheckInDateImageButton" TargetControlID="CheckInDateTextBox" Format="dd-MM-yyyy"></cc1:CalendarExtender>
                    <asp:ImageButton ID="CheckInDateImageButton" runat="server" ImageUrl="~/images/calendar_icon.png" CssClass="gncontrol" Width="24" Height="22" />
                    <br />
                    <asp:Label ID="Label8" runat="server" Text="Check out date" CssClass="label"></asp:Label>
                    <asp:TextBox ID="CheckOutDateTextBox" runat="server" Width="166px" CssClass="textbox"></asp:TextBox>
                    <cc1:CalendarExtender ID="CheckOutDateCalendarExtender" runat="server" PopupButtonID="CheckOutDateImageButton" TargetControlID="CheckOutDateTextBox" Format="dd-MM-yyyy"></cc1:CalendarExtender>
                    <asp:ImageButton ID="CheckOutDateImageButton" runat="server" ImageUrl="~/images/calendar_icon.png" CssClass="gncontrol" Width="24" Height="22" />
                    <br />

                    

                    <asp:Label ID="Label9" runat="server" Text="Arrival time" CssClass="label"></asp:Label>
                    <asp:TextBox ID="ArrivalTimeTextBox" runat="server" CssClass="textbox"></asp:TextBox><asp:Label ID="Label11" runat="server" Text="HH:MM" CssClass="note"></asp:Label>
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
                    <asp:Label ID="Label13" runat="server" Text="Departure time" CssClass="label"></asp:Label>
                    <asp:TextBox ID="DepartureTimeTextBox" runat="server" CssClass="textbox"></asp:TextBox><asp:Label ID="Label14" runat="server" Text="HH:MM" CssClass="note"></asp:Label>
                    <cc1:MaskedEditExtender runat="server" ID="DepartureTimeMaskedEditExtender"
                        TargetControlID="DepartureTimeTextBox"
                        Mask="99:99" InputDirection="LeftToRight"
                        MessageValidatorTip="true"
                        MaskType="Time"
                        AcceptAMPM="false"
                        ErrorTooltipEnabled="True">
                    </cc1:MaskedEditExtender>
                    <br />
                    <asp:Label ID="Label16" runat="server" Text="Hotel place/city" CssClass="label" meta:resourcekey="Label12Resource1"></asp:Label>
                    <%--<asp:TextBox ID="HotelPlaceCityTextBox" runat="server" CssClass="textbox"></asp:TextBox>--%>
                    <asp:DropDownList ID="DropDownHotelPlaceCity" runat="server" CssClass="textbox" Width="206px" AutoPostBack="True"></asp:DropDownList>

                    <br /> 
                    <asp:Label ID="Label12" runat="server" Text="Hotel category" CssClass="label" meta:resourcekey="Label12Resource1"></asp:Label>
                    <asp:DropDownList ID="DropDownHotel_category" runat="server" CssClass="textbox" Width="206px" AutoPostBack="True" meta:resourcekey="DropDownHotel_categoryResource1"></asp:DropDownList>
                    <br />
                   
                    <asp:Label ID="Label2" runat="server" Text="Hotel name" CssClass="label" meta:resourcekey="Label14Resource1"></asp:Label>
                    <asp:DropDownList ID="DropDownHotel_name" runat="server" CssClass="textbox" Width="206px" AutoPostBack="True" meta:resourcekey="DropDownHotel_nameResource1"></asp:DropDownList>
                    <br />

                    <asp:Label ID="Label1" runat="server" Text="Room category" CssClass="label" meta:resourcekey="Label13Resource1"></asp:Label>
                    <asp:DropDownList ID="DropDownRoom_category" runat="server" CssClass="textbox" Width="206px" AutoPostBack="True" meta:resourcekey="DropDownRoom_categoryResource1"></asp:DropDownList>
                    <br />


                    <asp:Label ID="Label17" runat="server" Text="Mode of payment" CssClass="label"></asp:Label>
                   <%-- <asp:TextBox ID="AdditionalServicesTextBox" runat="server" CssClass="textbox"></asp:TextBox>--%>
                  <asp:DropDownList ID="DropDownAdditionalServices" runat="server" CssClass="textbox" Width="206px" AutoPostBack="True" ></asp:DropDownList>
                    <div id="NumbersOfMembers" runat="server"  visible="false">
                    <asp:Label ID="Label19" runat="server" Text="Number of members" CssClass="label" ></asp:Label>
                    <asp:TextBox ID="NumberOfMembersTextBox" runat="server" CssClass="textbox"></asp:TextBox>
                    <cc1:FilteredTextBoxExtender ID="NumberOfMemberFilterTextBoxExtender" runat="server" TargetControlID="NumberOfMembersTextBox" 
                         FilterType="Numbers" ></cc1:FilteredTextBoxExtender>
                    </div>
                   
                    <asp:Panel ID="NameOfMemberPanel" runat="server" Visible="false">
                    <asp:Label ID="Label20" runat="server" Text="Name of members" CssClass="label"></asp:Label>
                    <asp:TextBox ID="NameOfMembersTextBox" runat="server" CssClass="textbox"></asp:TextBox>
                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="NameOfMembersTextBox" 
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
        
                    <asp:Label ID="Label18" runat="server" Text="Remarks" CssClass="label"></asp:Label>
                    <asp:TextBox ID="RemarksTextBox" runat="server" CssClass="textbox"></asp:TextBox>
          <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="RemarksTextBox" 
                                          FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"  ValidChars=" ,#@"></cc1:FilteredTextBoxExtender>
                    <br />
                    <div class="clear"></div>
                    <div class="buttonrow">
                        <asp:Button ID="SaveButton" runat="server" Text="Save" OnClick="SaveButton_Click" OnClientClick="return GenerateValidation()"/>
                        <asp:Button ID="CancelButton" runat="server" Text="Cancel" OnClick="CancelButton_Click"/>
                    </div>
                    <br />                      
    </div>
     </div>
</asp:Content>
