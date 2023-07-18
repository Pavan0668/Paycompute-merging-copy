<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeBehind="td_requisitions-Orig.aspx.cs" Inherits="iEmpPower.UI.Benefits_Payment.td_requisitions" Theme="SkinFile" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .button {
            border: solid 1px #c0c0c0;
            background-color: #eeeeee;
            font-family: verdana;
            font-size: 11px;
        }

        .modalBg {
            background-color: #cccccc;
            filter: alpha(opacity=80);
            opacity: 0.8;
        }

        .modalPanel {
            background-color: #ffffff;
            border-width: 3px;
            border-style: solid;
            border-color: Gray;
            padding: 3px;
            width: 320px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
        function ValidateDate(o) {

            if (!DateValidator(o.value)) {
                var objReg = /^\d+(\.\d{1,2})?$/;
                if (!objReg.test(o.value)) {
                    document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Enter valid check in  date (DD-MM-YYYY).";
                        document.getElementById("<%=lblMessageBoard.ClientID %>").style.color = "red";
                        o.focus();
                        return false;
                    }
                    else {
                        document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "";
                    }
                }


            }
            function ValidateTime(o) {

                if (!TimeValidator(o.value)) {


                    document.getElementById("<%=lblMessageBoard.ClientID %>").innerHTML = "Please enter correct time. The time should be in 24 hour format.";
                    document.getElementById("<%=lblMessageBoard.ClientID %>").style.color = "red";
                    ArrivalTimeTextBox.focus();
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
    <h2>Requisitions</h2>
    <asp:UpdatePanel ID="pnlParent" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard"></asp:Label><br />
            <asp:UpdatePanel ID="pnlEntry" runat="server">
                <ContentTemplate>
                    <div class="clear"></div>
                    <div class="legend">Search </div>
                    <fieldset class="search-bg">
                        <div>
                            <div>
                                <asp:Label ID="TravelTypeLabel" runat="server" Text="Travel type :" CssClass="label" Width="100px"></asp:Label>
                                <asp:DropDownList ID="TravelTypeDropDownList" runat="server" Width="206px" CssClass="textbox"></asp:DropDownList>
                                <asp:Label ID="Label1" runat="server" Text="Date :" CssClass="label" Width="100px"></asp:Label>
                                <asp:TextBox ID="DateTextBox" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                                <cc1:CalendarExtender ID="DateCalenderExtender" runat="server" PopupButtonID="DateTextBox" TargetControlID="DateTextBox" Format="dd-MM-yyyy"></cc1:CalendarExtender>
                            </div>
                            <div>
                                <asp:Label ID="Label2" runat="server" Text="Employee name :" CssClass="label" Width="100px"></asp:Label>
                                <asp:TextBox ID="EmployeeNameTextBox" runat="server" CssClass="textbox"></asp:TextBox>
                                <asp:Panel runat="server" ID="EmployeeNamePanel" Height="200px" ScrollBars="Both" Width="250px" Style="overflow: scroll;"></asp:Panel>
                                <cc1:AutoCompleteExtender ID="EmpNameAutoCompleteExtender" runat="server" EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetEmployeeNames" ServicePath="~/UI/Benefits_Payment/EmployeeNames.asmx" CompletionListCssClass="AutoCompleteList" TargetControlID="EmployeeNameTextBox" FirstRowSelected="true" CompletionListElementID="EmployeeNamePanel"></cc1:AutoCompleteExtender>
                                <asp:Label ID="Label3" runat="server" Text="Employee ID :" CssClass="label" Width="100px"></asp:Label>
                                <asp:TextBox ID="EmployeeIdTextBox" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                                <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="SearchButton_Click" />
                            </div>
                        </div>
                        <div class="clear"></div>
                        <div runat="server" id="TRGridView">
                            <asp:GridView ID="TravelRequestGridView" runat="server" AutoGenerateColumns="False"
                                Width="100%" OnRowDataBound="TravelRequestGridView_RowDataBound" AllowPaging="True"
                                AllowSorting="True" OnPageIndexChanging="TravelRequestGridView_PageIndexChanging"
                                OnSelectedIndexChanged="TravelRequestGridView_SelectedIndexChanged"
                                OnSorting="TravelRequestGridView_Sorting"
                                PageSize="4">
                                <Columns>
                                    <asp:BoundField DataField="FTPT_REQUEST_ID" HeaderText="Requisition Id" SortExpression="FTPT_REQUEST_ID">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="REQ_SEGMENT_ID" HeaderText="Requisition segment id" SortExpression="REQ_SEGMENT_ID">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TRAVEL_DATE" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Travel date"
                                        SortExpression="TRAVEL_DATE">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MODE_OF_TRANSPOPRT_FZTXT" HeaderText="Transport mode"
                                        SortExpression="MODE_OF_TRANSPOPRT_FZTXT">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MEDIA_OF_CATEGORY_TEXT25" HeaderText="Category"
                                        SortExpression="MEDIA_OF_CATEGORY_TEXT25">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="REGION_TEXT25_FROM" HeaderText="From"
                                        SortExpression="REGION_TEXT25_FROM">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="REGION_TEXT25_TO" HeaderText="To"
                                        SortExpression="REGION_TEXT25_TO">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                       <asp:BoundField DataField="EMPLOYEE_NO" HeaderText="Employee No." SortExpression="EMPLOYEE_NO">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                      <asp:TemplateField>
                                        <ItemTemplate> 
                                            <asp:HyperLink ID="hypLnkEligibility" runat="server" NavigateUrl='<%# "~/UI/Benefits_Payment/Eligibility.aspx?EmployeeNumber="+Eval("EMPLOYEE_NO")  %>' Target="_blank">Eligibility</asp:HyperLink>
                                         </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </fieldset>
                    <hr />
                    <div class="legend">Entry </div>
                    <div class="clear"></div>
                    <div>
                        <div>
                            <span>
                                <asp:Label ID="Label27" runat="server" Text="Designation :" CssClass="label" Width="100"></asp:Label>
                                <asp:Label ID="DesignationLabel" runat="server" CssClass="label"></asp:Label>
                            </span>
                        </div>
                        <div>
                            <span>
                                <asp:Label ID="Label28" runat="server" Text="Phone No. :" CssClass="label" Width="100"></asp:Label>
                                <asp:Label ID="PhoneNumberLabel" runat="server" CssClass="label"></asp:Label>
                            </span>
                        </div>
                        <div>
                            <span>
                                <asp:Label ID="Label29" runat="server" Text="Email :" CssClass="label" Width="100"></asp:Label>
                                <asp:Label ID="EmailLabel" runat="server" CssClass="label"></asp:Label>
                            </span>
                        </div>

                        <div class="clear"></div>
                        <cc1:TabContainer ID="tcDefalutRequisition" runat="server" ActiveTabIndex="0"
                            AutoPostBack="True">
                            <cc1:TabPanel runat="server" HeaderText="Domestic" ID="tabDomestic">
                                <HeaderTemplate>Domestic</HeaderTemplate>
                                <ContentTemplate>
                                    <div class="clear"></div>
                                    <asp:GridView ID="DomesticRequiSegmentGridView" runat="server" DataKeyNames="REQ_SEGMENT_ID" AutoGenerateColumns="False"
                                        Width="100%" OnRowDataBound="DomesticRequiSegmentGridView_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <a href="JavaScript:divexpandcollapse('div<%#Eval("REQ_SEGMENT_ID")%>');">
                                                        <img id='imgdiv<%#Eval("REQ_SEGMENT_ID")%>' width="9px" style="border: 0" src="../../images/plus.gif"
                                                            alt="" />
                                                    </a>
                                                </ItemTemplate>
                                                <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Requisition Segment Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="RequisitionID" runat="server" Text='<%# Eval("REQ_SEGMENT_ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TRAVEL_DATE" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Travel date"
                                                SortExpression="TRAVEL_DATE">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle CssClass="gridTextStyle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TRAVEL_TIME" HeaderText="Departure"
                                                SortExpression="TRAVEL_TIME">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle CssClass="gridTextStyle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="REGION_TEXT25_FROM" HeaderText="From"
                                                SortExpression="REGION_TEXT25_FROM">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle CssClass="gridTextStyle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="REGION_TEXT25_TO" HeaderText="To"
                                                SortExpression="REGION_TEXT25_TO">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle CssClass="gridTextStyle" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="TravelRequiLinkButton" CommandArgument='<%# Eval("REQ_SEGMENT_ID") %>' OnCommand="DomesticTravelRequiLinkButton_Click" runat="server">Edit</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="AccRequiLinkButton" PostBackUrl='<%# "~/UI/Benefits_Payment/new_accommodation_requisition.aspx?RequiSegmentId="+Eval("REQ_SEGMENT_ID") + "&PreviousPage=" + "td_requisitions.aspx"+"&TravelType=" + "1"  %>' runat="server">Acc/Requisition</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="VehRequiLinkButton" PostBackUrl='<%# "~/UI/Benefits_Payment/new_vehicle_requisition.aspx?RequiSegmentId="+Eval("REQ_SEGMENT_ID") +"&PreviousPage=" + "td_requisitions.aspx"+ "&TravelType=" + "1"  %>' runat="server">Veh/Requisition</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td colspan="100%" style="padding: 0px; margin: 0px; background: #F5F5F5;">
                                                            <div id='div<%# Eval("REQ_SEGMENT_ID") %>' style="overflow: auto; height: auto; display: none; position: relative; left: 15px;">
                                                                <br />
                                                                <div style="padding-left: 5px">
                                                                    <asp:Label ID="AccomodationDetailLabel" runat="server" Text="Accomodation details" Visible="False"></asp:Label>
                                                                </div>
                                                                <asp:GridView ID="AccomodationRequisitionGridViewDomestic" runat="server" AutoGenerateColumns="False"
                                                                    Width="98%" AllowPaging="True"
                                                                    AllowSorting="false">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="Accommadation_req_id" Visible="False">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="REQ_SEGMENT_ID" HeaderText="Requisition Segment Id"
                                                                            SortExpression="REQ_SEGMENT_ID">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField>
                                                                       <%-- <asp:BoundField DataField="RoomCategoryName" HeaderText="Room Category Name"
                                                                            SortExpression="RoomCategoryName">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField>--%>
                                                                        <asp:BoundField DataField="Check_in_date" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Check In"
                                                                            SortExpression="Check_in_date">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Check_out_date" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Check Out"
                                                                            SortExpression="Check_out_date">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="VehRequiLinkButton" runat="server" PostBackUrl='<%# "~/UI/Benefits_Payment/new_accommodation_requisition.aspx?RequiSegmentId="+Eval("REQ_SEGMENT_ID") + "&PreviousPage=" + "td_requisitions.aspx"+"&TravelType=" + "1" + "&AccomID=" + Eval("Accommadation_req_id")%>'>Edit</asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                                <br />
                                                                <div style="padding-left: 5px">
                                                                    <asp:Label ID="VehicleDetailsLabel" runat="server" Text="Vehicle details" Visible="False"></asp:Label>
                                                                </div>
                                                                <asp:GridView ID="VehicleRequisitionGridViewDomestic" runat="server" AutoGenerateColumns="False"
                                                                    Width="98%" AllowPaging="false"
                                                                    AllowSorting="false">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="Vehicle_req_id" Visible="False">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="REQ_SEGMENT_ID" HeaderText="REQ SEGMENT ID"
                                                                            SortExpression="REQ_SEGMENT_ID">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Date_of_travel" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Vehicle Required On"
                                                                            SortExpression="Date_of_travel">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField>
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
                                                                        <asp:BoundField DataField="Purpose_of_travel" HeaderText="Purpose"
                                                                            SortExpression="Purpose_of_travel">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="VehRequiLinkButton" runat="server" PostBackUrl='<%# "~/UI/Benefits_Payment/new_vehicle_requisition.aspx?RequiSegmentId="+Eval("REQ_SEGMENT_ID") + "&PreviousPage=" + "td_requisitions.aspx"+"&TravelType=" + "1" + "&VehicleID=" + Eval("Vehicle_req_id")%>'>Edit</asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                    <div>
                                        <asp:GridView ID="DomesticGridView" runat="server" ShowFooter="True" Width="100%"
                                            AutoGenerateColumns="False"
                                            meta:resourcekey="grdRecordTimeResource1" Style="margin-right: 0px">

                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <table class="TableEntryForm">
                                                            <tr style="height: 45px">
                                                                <td>
                                                                    <asp:Label ID="Label17" runat="server" Visible="true" Text="Date/Travel"></asp:Label>
                                                                    <asp:TextBox ID="domesticTextDateTravel"   onkeyup="ValidateDate(this);" Visible="true" runat="server"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="StartDateCalenderExtender" runat="server" PopupButtonID="domesticTextDateTravel" TargetControlID="domesticTextDateTravel" Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label18" runat="server" Visible="true" Text="Time/ Departure"></asp:Label>
                                                                    <asp:TextBox ID="domesticTextTimeDeparture" onkeyup="ValidateTime(this);" runat="server"></asp:TextBox>
                                                                    <cc1:MaskedEditExtender runat="server" ID="StartTimeMaskedEditExtender"
                                                                        TargetControlID="domesticTextTimeDeparture"
                                                                        Mask="99:99"
                                                                        MessageValidatorTip="true"
                                                                        MaskType="Time"
                                                                        AcceptAMPM="false"
                                                                        ErrorTooltipEnabled="True">
                                                                    </cc1:MaskedEditExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label19" runat="server" Visible="true" Text="M/TPort"></asp:Label>
                                                                    <asp:DropDownList ID="domesticDropDownMTPort" OnSelectedIndexChanged="domesticDropDownMTPort_SelectedIndexChanged" Visible="true" runat="server" AutoPostBack="True"></asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label20" runat="server" Visible="true" Text="M/Cate"></asp:Label>
                                                                    <asp:DropDownList ID="domesticDropDownMCate" OnSelectedIndexChanged="domesticDropDownMCate_SelectedIndexChanged" AutoPostBack="true" Visible="true" runat="server"></asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label121" runat="server" Visible="true" Text="Veh/Name"></asp:Label>
                                                                    <asp:DropDownList ID="domesticDropDownVehName" Visible="true" runat="server"></asp:DropDownList>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="Label22" runat="server" Visible="true" Text="From"></asp:Label>
                                                                    <asp:DropDownList ID="domesticDropDownFrom" Visible="true" runat="server"></asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label23" runat="server" Visible="true" Text="To"></asp:Label>
                                                                    <asp:DropDownList ID="domesticDropDownTo" Visible="true" runat="server"></asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 45px">
                                                                <td>
                                                                    <asp:Label ID="Label24" runat="server" Visible="true" Text="F/Fly No"></asp:Label>
                                                                    <asp:TextBox ID="domesticTextFFlyNo" Visible="true" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label25" runat="server" Visible="true" Text="Remarks"></asp:Label>
                                                                    <asp:TextBox ID="domesticTextRemarks" Visible="true" runat="server"></asp:TextBox>
                                                                    <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="domesticTextRemarks" 
                                                                    FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"  ></cc1:FilteredTextBoxExtender>--%>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label26" runat="server" Visible="true" Text="Advance"></asp:Label>
                                                                    <asp:TextBox ID="domesticTextAdvance" Visible="true" runat="server"></asp:TextBox>
                                                                      <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="domesticTextAdvance" 
                                                           FilterType="Custom, Numbers" ></cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                  <td>
                                                            <asp:Label ID="Label7" runat="server" Visible="true" Text="HOD-Remarks"></asp:Label>
                                                            <asp:TextBox ID="txtHODRemarks" Visible="true" runat="server" Enabled="false" ></asp:TextBox> 
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" Visible="true" Text="TD-Remarks"></asp:Label>
                                                            <asp:TextBox ID="txtTDRemarks" Visible="true" runat="server" Enabled="false" ></asp:TextBox> 
                                                        </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="hd-small" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </cc1:TabPanel>

                            <cc1:TabPanel ID="tabInternational" runat="server" HeaderText="International">
                                <HeaderTemplate>International</HeaderTemplate>
                                <ContentTemplate>
                                    <div class="clear"></div>
                                    <asp:GridView ID="InternationalRequisitionSegmentGridView" OnRowDataBound="InternationalRequisitionSegmentGridView_RowDataBound" runat="server" DataKeyNames="REQ_SEGMENT_ID" AutoGenerateColumns="False"
                                        Width="100%">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <a href="JavaScript:divexpandcollapse('div<%#Eval("REQ_SEGMENT_ID")%>');">
                                                        <img id='imgdiv<%#Eval("REQ_SEGMENT_ID")%>' width="9px" style="border: 0" src="../../images/plus.gif"
                                                            alt="" />
                                                    </a>
                                                </ItemTemplate>
                                                <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Requisition Segment Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="RequisitionID" runat="server" Text='<%# Eval("REQ_SEGMENT_ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TRAVEL_DATE" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Travel date"
                                                SortExpression="TRAVEL_DATE">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle CssClass="gridTextStyle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="REGION_TEXT25_FROM" HeaderText="From"
                                                SortExpression="REGION_TEXT25_FROM">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle CssClass="gridTextStyle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="REGION_TEXT25_TO" HeaderText="To"
                                                SortExpression="REGION_TEXT25_TO">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle CssClass="gridTextStyle" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="InternationalTravelRequiLinkButton" CommandArgument='<%# Eval("REQ_SEGMENT_ID") %>' OnCommand="InternationalTravelRequiLinkButton_Click" runat="server">Edit</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="InternationalAccRequiLinkButton" PostBackUrl='<%# "~/UI/Benefits_Payment/new_accommodation_requisition.aspx?RequiSegmentId="+Eval("REQ_SEGMENT_ID") +"&PreviousPage=" + "td_requisitions.aspx"+ "&TravelType=" + "2"  %>' runat="server">Acc/Requisition</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="InternationalVehRequiLinkButton" PostBackUrl='<%# "~/UI/Benefits_Payment/new_vehicle_requisition.aspx?RequiSegmentId="+Eval("REQ_SEGMENT_ID") + "&PreviousPage=" + "td_requisitions.aspx"+"&TravelType=" + "2"  %>' runat="server">Veh/Requisition</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td colspan="100%" style="padding: 0px; margin: 0px; background: #F5F5F5;">
                                                            <div id='div<%# Eval("REQ_SEGMENT_ID") %>' style="overflow: auto; height: auto; display: none; position: relative; left: 15px;">
                                                                <br />
                                                                <div style="padding-left: 5px">
                                                                    <asp:Label ID="InternationalAccomodationDetialsLabel" runat="server" Text="Accomodation details" Visible="False"></asp:Label>
                                                                </div>
                                                                <asp:GridView ID="AccomodationRequisitionGridViewInternational" runat="server" AutoGenerateColumns="False"
                                                                    Width="98%">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="Accommadation_req_id" Visible="false">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="REQ_SEGMENT_ID" HeaderText="Requisition Segment Id"
                                                                            SortExpression="REQ_SEGMENT_ID">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField>
                                                                        <%--<asp:BoundField DataField="RoomCategoryName" HeaderText="Room Category Name"
                                                                            SortExpression="RoomCategoryName">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField>--%>
                                                                        <asp:BoundField DataField="Check_in_date" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Check In"
                                                                            SortExpression="Check_in_date">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Check_out_date" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Check Out"
                                                                            SortExpression="Check_out_date">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="VehRequiLinkButtonInternational" runat="server" PostBackUrl='<%# "~/UI/Benefits_Payment/new_accommodation_requisition.aspx?RequiSegmentId="+Eval("REQ_SEGMENT_ID") +"&PreviousPage=" + "td_requisitions.aspx"+ "&TravelType=" + "2" + "&AccomID=" + Eval("Accommadation_req_id")%>'>Edit</asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                                <br />
                                                                <div style="padding-left: 5px">
                                                                    <asp:Label ID="InternationalVehicleDetialsLabel" runat="server" Text="Vehicle details" Visible="False"></asp:Label>
                                                                </div>
                                                                <asp:GridView ID="VehicleRequisitionGridViewInternational" runat="server" AutoGenerateColumns="False"
                                                                    Width="98%">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="Vehicle_req_id" Visible="false">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="REQ_SEGMENT_ID" HeaderText="REQ SEGMENT ID"
                                                                            SortExpression="REQ_SEGMENT_ID">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Date_of_travel" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Vehicle Required On"
                                                                            SortExpression="Date_of_travel">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField>
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
                                                                        <asp:BoundField DataField="Purpose_of_travel" HeaderText="Purpose"
                                                                            SortExpression="Purpose_of_travel">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="gridTextStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="VehRequiLinkButtonInternational" runat="server" PostBackUrl='<%# "~/UI/Benefits_Payment/new_vehicle_requisition.aspx?RequiSegmentId="+Eval("REQ_SEGMENT_ID") + "&PreviousPage=" + "td_requisitions.aspx"+"&TravelType=" + "2" + "&VehicleID=" + Eval("Vehicle_req_id")%>'>Edit</asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                    <div>
                                        <asp:GridView ID="InternationalGridView" runat="server" ShowFooter="True" Width="100%"
                                            AutoGenerateColumns="False" meta:resourcekey="grdRecordTimeResource1" Style="margin-right: 0px">
                                            <Columns>
                                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="hd-small">

                                                    <ItemTemplate>
                                                        <table class="TableEntryForm">
                                                            <tr style="height: 45px">
                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Visible="true" Text="Date/Travel"></asp:Label>
                                                                    <asp:TextBox ID="TextDateTravel" onkeyup="ValidateDate(this);" Visible="true" runat="server"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="TextDateTravel" TargetControlID="TextDateTravel" Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Visible="true" Text="M/TPort"></asp:Label>
                                                                    <asp:DropDownList ID="DropDownMTPort" OnSelectedIndexChanged="DropDownMTPort_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label3" runat="server" Visible="true" Text="M/Cate"></asp:Label>
                                                                    <asp:DropDownList ID="DropDownMCate" Visible="true" runat="server"></asp:DropDownList>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="Label4" runat="server" Visible="true" Text="Airline"></asp:Label>
                                                                    <asp:TextBox ID="TextAirline" Visible="true" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label5" runat="server" Visible="true" Text="From"></asp:Label>
                                                                    <asp:DropDownList ID="DropDownFrom" Visible="true" runat="server"></asp:DropDownList>
                                                                </td>
                                                                <td style="width: 150px;">
                                                                    <asp:Label ID="Label6" runat="server" Visible="true" Text="To"></asp:Label>
                                                                    <asp:DropDownList ID="DropDownTo" Visible="true" runat="server"></asp:DropDownList>
                                                                </td>
                                                                <td style="width: 200px;">
                                                                    <asp:Label ID="Label9" runat="server" Visible="true" Text="Insur/Mediclaim"></asp:Label>
                                                                    <asp:TextBox ID="TextInsurMediclaim" Visible="true" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 45px">
                                                                <td>
                                                                    <asp:Label ID="Label10" runat="server" Visible="true" Text="F/Fly No"></asp:Label>
                                                                    <asp:TextBox ID="TextFFlyNo" Visible="true" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label11" runat="server" Visible="true" Text="Seat Preference"></asp:Label>
                                                                    <asp:DropDownList ID="DropDownSeatPreference" Visible="true" runat="server"></asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label12" runat="server" Visible="true" Text="Meal Preference"></asp:Label>
                                                                    <asp:DropDownList ID="DropDownMealPreference" Visible="true" runat="server"></asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label13" runat="server" Visible="true" Text="Baggage"></asp:Label>
                                                                    <asp:DropDownList ID="DropDownBaggage" Visible="true" runat="server"></asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label14" runat="server" Visible="true" Text="Hand"></asp:Label>
                                                                    <asp:DropDownList ID="DropDownHand" Visible="true" runat="server"></asp:DropDownList>
                                                                </td>                                                         
                                                            </tr>
                                                               <tr style="height: 45px">
                                                        <td>
                                                                        <asp:Label ID="Label15" runat="server" Visible="true" Text="Remarks"></asp:Label>
                                                                        <asp:TextBox ID="TextRemarks" Visible="true" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 200px;">
                                                                        <asp:Label ID="Label16" runat="server" Visible="true" Text="Advance"></asp:Label>
                                                                        <asp:TextBox ID="TextAdvance" Visible="true" runat="server"></asp:TextBox>
                                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="TextAdvance" 
                                                           FilterType="Custom, Numbers" ></cc1:FilteredTextBoxExtender>
                                                                    </td>                                                                    
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Visible="true" Text="HOD-Remarks"></asp:Label>
                                                            <asp:TextBox ID="txtHODRemarks" Visible="true" runat="server" Enabled="false" ></asp:TextBox> 
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" Visible="true" Text="TD-Remarks"></asp:Label>
                                                            <asp:TextBox ID="txtTDRemarks" Visible="true" runat="server" Enabled="false" ></asp:TextBox> 
                                                        </td>
                                                    </tr>
                                                            <tr>
                                                                <td>&nbsp;
                                                                </td>
                                                                <td>&nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </cc1:TabPanel>
                        </cc1:TabContainer>
                    </div> 
                    <div id="remarks" runat="server">
                        <asp:Label ID="RemarksLabel" runat="server" Text="Remarks"></asp:Label>
                        <asp:TextBox ID="TD_RemarksTextBox" runat="server" Width="300px" Height="50px" TextMode="MultiLine" Style="margin-left: 33px; margin-top: 30px"></asp:TextBox>
                    </div>
                    <div class="clear"></div>
                    <div id="buttoncontrols" runat="server" class="buttonrow">
                        <asp:Button ID="SendProposal" runat="server" Text="Send Proposal" OnClick="SendProposal_Click" />
                        <asp:Button ID="CancelButton" runat="server" Text="Cancel" OnClick="CancelButton_Click" />
                        <asp:HiddenField ID="hiddenRowCount" runat="server" />
                    </div>
                    <%--Implementing model pop up.--%>
                    <cc1:ModalPopupExtender BackgroundCssClass="modalBg" DropShadow="true" ID="CancelButtonModalPopupExtender" PopupControlID="CancelPanel" runat="server"
                        TargetControlID="CancelButton" CancelControlID="btnCancel">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="CancelPanel" runat="server" CssClass="modalPanel" Style="display: none">
                        <strong>Requisition/Schedule Cancel</strong><br />
                        <br />
                        <br />
                        <div>
                            Reason for cancel schedule on
             <br />
                            <asp:Label ID="Label7" runat="server" Text="From :"></asp:Label>
                            <asp:Label ID="FromLabel" runat="server"></asp:Label>
                            <asp:Label ID="Label8" runat="server" Width="30px"></asp:Label>
                            <asp:Label ID="Label21" runat="server" Text="To :"></asp:Label>
                            <asp:Label ID="ToLabel" runat="server"></asp:Label>
                            <br />
                            <asp:TextBox ID="ReasonForCancelTextBox" runat="server" TextMode="MultiLine" Width="310px" Height="50px"></asp:TextBox>
                        </div>
                        <div class="clear"></div>
                        <br />
                        <br />
                        <br />
                        <asp:Button ID="btnApply" runat="server" Text="Submit" OnClick="btnApply_Click" />&nbsp&nbsp
        <asp:Button ID="btnCancel" CssClass="button" runat="server" Text="Close" />
                        <hr />
                        <div class="clear"></div>
                    </asp:Panel>
                    <%--End of implementing model pop up.--%>
                </ContentTemplate>
                <%--End of pnlEntry update panel--%>
            </asp:UpdatePanel>
            <%--End of pnlEntry update panel--%>
        </ContentTemplate>
        <%--End of pnlParent update panel--%>
    </asp:UpdatePanel>
    <%--End of pnlParent update panel--%>
</asp:Content>
