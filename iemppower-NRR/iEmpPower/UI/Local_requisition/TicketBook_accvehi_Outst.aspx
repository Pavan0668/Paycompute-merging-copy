<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="TicketBook_accvehi_Outst.aspx.cs" Inherits="iEmpPower.UI.Local_requisition.TicketBook_accvehi_Outst"  EnableEventValidation="false" Theme="SkinFile" %>

<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls" TagPrefix="BDP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <style type="text/css">
        .auto-style1 {
            width: 114px;
        }
        .auto-style3 {
            width: 134px;
        }
        .auto-style4 {
            width: 110px;
        }
        .ticket-info {
            width: 1149px;
        }
        .auto-style5 {
            width: 132px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <div>
        <span id="bold" runat="server" style="font-weight: bold"></span>
    </div>
    <h2>Outstation Ticket Booking for Accommodation/Vehicle</h2>
    <br />
     <asp:Label ID="lblMessage" runat="server" CssClass="msgboard"></asp:Label>
      
            <asp:UpdatePanel ID="pnlLocal" runat="server" Visible="true">
      <ContentTemplate>
          <br />
          
       <cc1:TabContainer ID="tcDefalut" runat="server" ActiveTabIndex="0" AutoPostBack="True" onactivetabchanged="tcDefalut_ActiveTabChanged" meta:resourcekey="tcDefalutResource1" >
         
           <cc1:TabPanel runat="server" HeaderText="Accommodation" ID="tabAccommodation"  meta:resourcekey="TabPanel1Resource1">
            <HeaderTemplate>Accommodation</HeaderTemplate>
                <ContentTemplate>
                    <div>
                    <asp:UpdatePanel ID="upPnlAcc" runat="server">
                        <ContentTemplate>

                        <div><span id="Span1" runat="server" style="font-weight: bold"></span></div>
                        <h3>Accommodation Proposal</h3>  
                        <div class="legend">Search </div>     
                        <div class="clear"></div>
                  
                           <asp:GridView ID="AccomodationRequisitionGridView" runat="server" AutoGenerateColumns="False"  Width="100%"  
                     OnRowDataBound="AccomodationRequisitionGridView_RowDataBound" OnSelectedIndexChanged="AccomodationRequisitionGridView_SelectedIndexChanged">
                                <Columns>
                                    <asp:BoundField DataField="Accommadation_req_id" HeaderText="Requisition Id">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="REQUEST_ID" HeaderText="Requisition Id" Visible="False">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="REQ_SEGMENT_ID" HeaderText="Re_Seg Id"  Visible="False">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                      <asp:BoundField DataField="EMPLOYEE_NO" HeaderText="Employee No." >
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                       <asp:BoundField DataField="Date_of_travel" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Begin_Date" >
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Check_in_date" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Check in date" >
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Check_out_date" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Check out date">
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
                                    <asp:BoundField DataField="current_status" HeaderText="current status" >
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField> 
                                       <asp:BoundField DataField="STATUS_UPDATED_BY" HeaderText="STATUS_UPDATED_BY" >
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField> 
                                        <asp:BoundField DataField="CRAETEDBY" HeaderText="created_by" Visible="False">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField> 
                                       <asp:BoundField DataField="CREATEDON" HeaderText="created_on" Visible="False">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField> 
                                        <asp:BoundField DataField="MODIFIEDBY" HeaderText="modified_by" Visible="False">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField> 
                                        <asp:BoundField DataField="MODIFIEDON" HeaderText="modified_on" Visible="False">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField> 
                                        <asp:BoundField DataField="isActive" HeaderText="isActive" Visible="False">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField> 
                                    <asp:BoundField DataField="REMARKS" HeaderText="REMARKS">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>  
                                      <asp:BoundField DataField="HOTEL_CAT_CODE" HeaderText="HOTEL_CAT_CODE">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>  
                                      <asp:BoundField DataField="HOTEL_CODE" HeaderText="HOTEL_CODE">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>  
                                      <asp:BoundField DataField="ROOM_CODE" HeaderText="RoomCategoryName">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>  
                                    <%-- <asp:TemplateField>
                                    
                                    <ItemTemplate>
                                         <asp:LinkButton ID="TravelRequiLinkButton" CommandName="Select" CommandArgument='<%# Eval("REQ_SEGMENT_ID") %>' OnCommand="DomesticTravelRequiLinkButton_Click" runat="server">Book Ticket</asp:LinkButton>
                                    </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                 
                        <hr />
 
<br />
    <br />
    <table class="ticket-info">
        <tr>
            <td class="auto-style5">
                <asp:Label ID="Label53" runat="server" Text="Emp Name" Font-Bold="True"></asp:Label>
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
            <td rowspan="2" class="auto-style4">
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
                        <div class="legend">Entry </div>
                        <div class="clear"></div>
                            <asp:Panel ID="pnlAccOut" runat="server" Enabled="false">

                           <div>    
                    <asp:CheckBox ID="CheckBox2" runat="server" Text="If guest , click the checkbox" CssClass="label" Width="200px" meta:resourcekey="CheckBox1Resource1" Visible="False" /><br />
                    <asp:Label ID="Label18" runat="server" Text="Date of travel" CssClass="label" meta:resourcekey="Label1Resource1"></asp:Label>
                    <asp:TextBox ID="TextTripStartDat" runat="server" Width="166px" onkeyup="ValidateDate(this);" CssClass="textbox"></asp:TextBox>
                    <AjaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="BirthDateImageButton1" TargetControlID="TextTripStartDat" Format="dd-MM-yyyy" Enabled="True"></AjaxToolkit:CalendarExtender>
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/calendar_icon.png" CssClass="gncontrol" Width="24px" Height="22px" />
                    <br />
                    <asp:Label ID="Label19" runat="server" Text="Check in date" CssClass="label" meta:resourcekey="Label1Resource1"></asp:Label>
                    <asp:TextBox ID="TextStartDate" runat="server" onkeyup="ValidateDate(this);" Width="166px" CssClass="textbox" ></asp:TextBox>
                    <AjaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="BirthDateImageButton" TargetControlID="TextStartDate" Format="dd-MM-yyyy" Enabled="True"></AjaxToolkit:CalendarExtender>
                    <asp:ImageButton ID="BirthDateImageButton" runat="server" ImageUrl="~/images/calendar_icon.png" CssClass="gncontrol" Width="24px" Height="22px" />
                    <br />
                    <asp:Label ID="Label20" runat="server" Text="Checkout date" CssClass="label" meta:resourcekey="Label2Resource1"></asp:Label>
                    <asp:TextBox ID="TextCheckout_date" runat="server" Width="166px" CssClass="textbox"></asp:TextBox>
                    <AjaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" PopupButtonID="BirthDateImageButtonnew" TargetControlID="TextCheckout_date" Format="dd-MM-yyyy" Enabled="True"></AjaxToolkit:CalendarExtender>
                    <asp:ImageButton ID="BirthDateImageButtonnew" runat="server" ImageUrl="~/images/calendar_icon.png" CssClass="gncontrol" Width="24px" Height="22px" />
                    <br />
                    <asp:Label ID="Label21" runat="server" Text="Arrival time" CssClass="label" meta:resourcekey="Label3Resource1"></asp:Label>
                    <asp:TextBox ID="txtArrival_time" runat="server" CssClass="textbox"  meta:resourcekey="txtArrival_timeResource1"></asp:TextBox>
                    <asp:Label ID="Label22" runat="server" Text="HH:MM" CssClass="note"></asp:Label>
                    <cc1:MaskedEditExtender runat="server" ID="MaskedEditExtender2"
                        TargetControlID="txtArrival_time"
                        Mask="99:99"
                        MaskType="Time"
                        ErrorTooltipEnabled="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                    </cc1:MaskedEditExtender>
                    <br />
                    <asp:Label ID="Label23" runat="server" Text="Departure time" CssClass="label" meta:resourcekey="Label4Resource1"></asp:Label>
                    <asp:TextBox ID="TextDeparture_time" runat="server" CssClass="textbox"  meta:resourcekey="TextDeparture_timeResource1"></asp:TextBox><asp:Label ID="Label24" runat="server" Text="HH:MM" CssClass="note"></asp:Label>
                    <cc1:MaskedEditExtender runat="server" ID="DeparturetimeMaskedEditExtender"
                        TargetControlID="TextDeparture_time"
                        Mask="99:99"
                        MaskType="Time"
                        ErrorTooltipEnabled="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                    </cc1:MaskedEditExtender>
                               <br />
                               <asp:Label ID="Label92" runat="server" CssClass="label" meta:resourcekey="Label12Resource1" Text="Hotel place/city"></asp:Label>
                               <%--<asp:TextBox ID="HotelPlaceCityTextBox" runat="server" CssClass="textbox"></asp:TextBox>--%>
                               <asp:DropDownList ID="DropDownHotelPlaceCity" runat="server" AutoPostBack="True" CssClass="textbox" OnSelectedIndexChanged="DropDownHotelPlaceCity_SelectedIndexChanged" Width="206px">
                               </asp:DropDownList>
                    <br /> 
                    <asp:Label ID="Label25" runat="server" Text="Hotel category" CssClass="label" meta:resourcekey="Label12Resource1"></asp:Label>
                    <asp:DropDownList ID="DropDownHotel_category" runat="server" CssClass="textbox" Width="206px" AutoPostBack="True" meta:resourcekey="DropDownHotel_categoryResource1"  OnSelectedIndexChanged="DropDownHotel_category_SelectedIndexChanged" ></asp:DropDownList>
                    <br />
                    <asp:Label ID="Label26" runat="server" Text="Room category" CssClass="label" meta:resourcekey="Label13Resource1"></asp:Label>
                    <asp:DropDownList ID="DropDownRoom_category" runat="server" CssClass="textbox" Width="206px" AutoPostBack="True" meta:resourcekey="DropDownRoom_categoryResource1"></asp:DropDownList>
                    <br />
                    <asp:Label ID="Label27" runat="server" Text="Hotel name" CssClass="label" meta:resourcekey="Label14Resource1"></asp:Label>
                    <asp:DropDownList ID="DropDownHotel_name" runat="server" CssClass="textbox" Width="206px" AutoPostBack="True" meta:resourcekey="DropDownHotel_nameResource1"></asp:DropDownList>
                    <br />
                    <asp:Label ID="Label28" runat="server" Text="Additional services" CssClass="label" meta:resourcekey="Label15Resource1"></asp:Label>
                    <asp:TextBox ID="TextAdditional_services" runat="server" CssClass="textbox" meta:resourcekey="TextAdditional_servicesResource1"></asp:TextBox>
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="TextAdditional_services" 
                             FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" ValidChars=" ,#@" Enabled="True" ></cc1:FilteredTextBoxExtender>
                    <br />
                    <asp:Label ID="Label29" runat="server" Text="Remarks" CssClass="label" meta:resourcekey="Label15Resource1"></asp:Label>
                    <asp:TextBox ID="RemarksTextBox" runat="server" CssClass="textbox" meta:resourcekey="TextAdditional_servicesResource1"></asp:TextBox>
                  
                    <br />
                      <%-- <div id="remarks" runat="server">
                            <asp:Label ID="RemarksLabelApproval" runat="server" Text="Remarks"></asp:Label>
                            <asp:TextBox ID="txtHOD_RemarksAcc" runat="server" Width="300px" Height="59px" TextMode="MultiLine" Style="margin-left: 25px; margin-top: 27px"></asp:TextBox>
                        </div>
                    
                    <br />--%>
        </div>

                            </asp:Panel>
                            
                    <%--For ticket booking--%>

                    <asp:Panel ID="pnlAccomTicketbooking" runat="server" CssClass="auto-style1" Visible="false"> 
                        <h3>Travel Desk Arrangement Area</h3>
                        <table align="left" cellpadding="0" cellspacing="0" class="auto-style1">
                            <tr>
                                <td>
                                  <asp:Label ID="Label17" runat="server" Text="Employee Name" CssClass="label"></asp:Label> 
                                  <asp:TextBox ID="txtEmployeeName" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                </td>
                                <td >
                                  <asp:Label ID="Label31" runat="server" Text="Department" CssClass="label"></asp:Label>
                                  <asp:TextBox ID="txtDepartment" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                </td>
                                <td >
                                  <asp:Label ID="Label32" runat="server" Text="Designation" CssClass="label"></asp:Label>
                                  <asp:TextBox ID="txtDesignation" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                </td> 
                            </tr>
                            <tr >
                                <td >
                                     <asp:Label ID="Label33" runat="server" Text="Contact Number" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtContactNumb" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtContactNumb">
        </cc1:FilteredTextBoxExtender>
                                </td>
                                <td >
                                    <asp:Label ID="Label34" runat="server" Text="Payment" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtPayment" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                </td>
                                <td  >
                                    <asp:Label ID="Label35" runat="server" Text="Tariff" CssClass="label" meta:resourcekey="Label1Resource1"></asp:Label>
                                    <asp:TextBox ID="txtTariff" runat="server" Width="140px" CssClass="textbox" ></asp:TextBox>
                                </td> 
                            </tr>
                            <tr >
                                <td >
                                     <asp:Label ID="Label36" runat="server" Text="Booking Given To" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtBookingGivenTo" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                </td>
                                <td >
                                     <asp:Label ID="Label37" runat="server" Text="Hotel Invoice No" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtHotelInvoiceNo" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                </td>
                               <td  >
                                    <asp:Label ID="Label38" runat="server" Text="Bill Date" CssClass="label" meta:resourcekey="Label1Resource1"></asp:Label>
                                    <asp:TextBox ID="txtBillDate" runat="server" Width="140px" CssClass="textbox" Enabled="false"></asp:TextBox>
                                    <AjaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" PopupButtonID="ImageButton4" TargetControlID="txtBillDate" Format="dd-MM-yyyy"></AjaxToolkit:CalendarExtender>
                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/calendar_icon.png" CssClass="gncontrol" Width="24" Height="22" />
                                </td> 
                            </tr>
                            <tr >
                                <td  >
                                    <asp:Label ID="Label39" runat="server" Text="Amount" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtAmount">
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
                        <asp:Button ID="btnAccTicketBook" runat="server" Text="Ticket Book" meta:resourcekey="btnOkResource1" OnClick="btnAccTicketBook_Click" />
                        <asp:Button ID="btnAccCancel" runat="server" Text="Cancel" meta:resourcekey="btnCancelResource1" OnClick="btnAccCancel_Click"  />                        
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
        <span id="Span2" runat="server" style="font-weight: bold"></span>
    </div>
    <h3>Vehicle Proposal</h3>
        <div class="legend">Search </div>

                <div class="clear"></div> 

                       <asp:GridView ID="VehicleRequisitionGridView" runat="server" AutoGenerateColumns="False"  Width="100%"  
                       OnRowDataBound ="VehicleRequisitionGridView_RowDataBound" OnSelectedIndexChanged="VehicleRequisitionGridView_SelectedIndexChanged">
                                <Columns>
                                    <asp:BoundField DataField="Vehicle_req_id" HeaderText="Vehicle_req_id" Visible="true">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="REQUEST_ID" HeaderText="TRIP_NO" Visible="false">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="REQ_SEGMENT_ID" HeaderText="TRIP_SEGMENT_ID"  Visible="false">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                      <asp:BoundField DataField="EMPLOYEE_NO" HeaderText="PERNR"  Visible="true" >
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Date_of_travel" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Begin_Date" >
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Duration_from" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Duration From">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="Duration_to" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Duration To" >
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
                                       <asp:BoundField DataField="Purpose_of_travel" HeaderText="Purpose" Visible="true" >
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
                                        <asp:BoundField DataField="Vehicle_name" HeaderText="Vehicle Name" Visible="true">
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
                                    <%-- <asp:TemplateField>
                                    
                                    <ItemTemplate>
                                         <asp:LinkButton ID="TravelRequiLinkButton" CommandName="Select" CommandArgument='<%# Eval("REQ_SEGMENT_ID") %>' OnCommand="DomesticTravelRequiVehLinkButton_Click" runat="server">Book Ticket</asp:LinkButton>
                                    </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>

    <hr />
  
    <table class="ticket-info">
        <tr>
            <td class="auto-style5">
                <asp:Label ID="Label54" runat="server" Text="Emp Name" Font-Bold="True"></asp:Label>
                :
                      <asp:Label ID="Label69" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="Label70" runat="server" Text=" Dept" Font-Bold="True"></asp:Label>:
                    <asp:Label ID="Label71" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="Label72" runat="server" Text="Design " Font-Bold="True"></asp:Label>
                :
                    <asp:Label ID="Label73" runat="server" Text=""></asp:Label>
                <br />
                  <asp:Label ID="Label74" runat="server" Text="D.O.B " Font-Bold="True"></asp:Label>
                :
                    <asp:Label ID="Label75" runat="server" Text="22/04/1999"></asp:Label>
                <br />
            </td>

            <td rowspan="2" class="auto-style3">
                <asp:Label ID="Label76" runat="server" Text=" Vehicle Type " Font-Bold="True"></asp:Label>
                :
     <asp:Label ID="Label77" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="Label78" runat="server" Text="Vehicle Category" Font-Bold="True"></asp:Label>:
     <asp:Label ID="Label79" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="Label80" runat="server" Text=" Vehicle Name" Font-Bold="True"></asp:Label>:
     <asp:Label ID="Label81" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="Label82" runat="server" Text=" Phone No." Font-Bold="True"></asp:Label>:
     <asp:Label ID="Label83" runat="server" Text="9986878987"></asp:Label>
                <br />
            </td>
            <td rowspan="2" class="auto-style4">
                <asp:Label ID="Label84" runat="server" Text="Travel Date" Font-Bold="True"></asp:Label>:
            <asp:Label ID="Label85" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="Label86" runat="server" Text="From" Font-Bold="True"></asp:Label>
                :
                    <asp:Label ID="Label87" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="Label88" runat="server" Text="To" Font-Bold="True"></asp:Label>:
                 <asp:Label ID="Label89" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="Label90" runat="server" Text="Email ID" Font-Bold="True"></asp:Label>:
                 <asp:Label ID="Label91" runat="server" Text="praveen@nrrs.com"></asp:Label>
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
                <div class="legend">Entry </div>
                <div class="clear"></div>
                            <asp:Panel ID="pnlVehiOut" runat="server" Enabled="false">

                       <div> 
            
                    <asp:CheckBox ID="CheckBox1" runat="server" Text="If guest , click the checkbox" CssClass="label" Width="200px" meta:resourcekey="CheckBox1Resource1" Visible="False" /><br />
                    <asp:Label ID="Label1" runat="server" Text=" Date of travel" CssClass="label" meta:resourcekey="Label1Resource1"></asp:Label>
                    <asp:TextBox ID="TextTripStartDatV" runat="server" Width="166px" onkeyup="ValidateDate(this);" CssClass="textbox"></asp:TextBox>
                    <AjaxToolkit:CalendarExtender ID="CalenderExtender" runat="server" PopupButtonID="BirthDateImageButton1" TargetControlID="TextTripStartDatV" Format="dd-MM-yyyy"></AjaxToolkit:CalendarExtender>
                    <asp:ImageButton ID="BirthDateImageButton1" runat="server" ImageUrl="~/images/calendar_icon.png" CssClass="gncontrol" Width="24" Height="22" />
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="Duration from" CssClass="label" meta:resourcekey="Label2Resource1"></asp:Label>
                    <asp:TextBox ID="TextDurationfrom" runat="server" onkeyup="ValidateDate(this);" Width="166px" CssClass="textbox"></asp:TextBox>
                    <AjaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="BirthDateImageButton2" TargetControlID="TextDurationfrom" Format="dd-MM-yyyy"></AjaxToolkit:CalendarExtender>
                    <asp:ImageButton ID="BirthDateImageButton2" runat="server" ImageUrl="~/images/calendar_icon.png" CssClass="gncontrol" Width="24" Height="22" />
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="Duration to" CssClass="label" meta:resourcekey="Label3Resource1"></asp:Label>
                    <asp:TextBox ID="TextDurationto" runat="server" onkeyup="ValidateDate(this);" Width="166px" CssClass="textbox"></asp:TextBox>
                    <AjaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="BirthDateImageButton3" TargetControlID="TextDurationto" Format="dd-MM-yyyy"></AjaxToolkit:CalendarExtender>
                    <asp:ImageButton ID="BirthDateImageButton3" runat="server" ImageUrl="~/images/calendar_icon.png" CssClass="gncontrol" Width="24" Height="22" />
                    <br />
                    <asp:Label ID="Label4" runat="server" Text="Departure from" CssClass="label" meta:resourcekey="Label4Resource1"></asp:Label>
                    <asp:TextBox ID="TextDeparturefrom" runat="server" CssClass="textbox" meta:resourcekey="TextBox3Resource1"></asp:TextBox>
                     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TextDeparturefrom" 
                         FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" ValidChars=" ,#@"  ></cc1:FilteredTextBoxExtender>
                    <br />
                    <asp:Label ID="Label5" runat="server" Text="Destination to" CssClass="label" meta:resourcekey="Label5Resource1"></asp:Label>
                    <asp:TextBox ID="TextDestinationto" runat="server" CssClass="textbox" meta:resourcekey="TextBox6Resource1"></asp:TextBox>
                     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="TextDestinationto" 
                         FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" ValidChars=" ,#@"  ></cc1:FilteredTextBoxExtender>
                    <br />
                    <asp:Label ID="Label6" runat="server" Text=" Purpose of travel" CssClass="label" meta:resourcekey="Label6Resource1"></asp:Label>
                    <asp:TextBox ID="TextPurposeoftravel" runat="server" CssClass="textbox" meta:resourcekey="TextBox7Resource1"></asp:TextBox>
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="TextPurposeoftravel" 
                         FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" ValidChars=" ,#@" ></cc1:FilteredTextBoxExtender>
                    <br />
                    <asp:Label ID="Label7" runat="server" Text="Carrying any materials" CssClass="label" meta:resourcekey="Label7Resource1"></asp:Label>
                    <asp:TextBox ID="TextCarryinganymaterials" runat="server" CssClass="textbox" meta:resourcekey="TextBox8Resource1"></asp:TextBox>
                     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="TextCarryinganymaterials" 
                         FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" ValidChars=" ,#@"  ></cc1:FilteredTextBoxExtender>
                    <br />
                    <asp:Label ID="Label8" runat="server" Text="Pickup time" CssClass="label" meta:resourcekey="Label8Resource1"></asp:Label>
                    <asp:TextBox ID="TextPickuptime" runat="server" CssClass="textbox" onkeyup="ValidateTime(this);" meta:resourcekey="TextBox2Resource1"></asp:TextBox>
                    <cc1:MaskedEditExtender runat="server" ID="MaskedEditExtender1"
                        TargetControlID="TextPickuptime"
                        Mask="99:99"
                        MessageValidatorTip="true"
                        MaskType="Time"
                        AcceptAMPM="false"
                        ErrorTooltipEnabled="True">
                    </cc1:MaskedEditExtender>
                    <br />
                    <asp:Label ID="Label9" runat="server" Text="Pickup address" CssClass="label" meta:resourcekey="Label9Resource1"></asp:Label>
                    <asp:TextBox ID="TextPickupaddress" runat="server" CssClass="textbox" meta:resourcekey="TextBox1Resource1"></asp:TextBox>
                  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="TextPickupaddress" 
                         FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" ValidChars=" ,#@"  ></cc1:FilteredTextBoxExtender>
                    <br />
                    <asp:Label ID="Label10" runat="server" Text="Drop time" CssClass="label" meta:resourcekey="Label10Resource1"></asp:Label>
                    <asp:TextBox ID="TextDroptime" runat="server" CssClass="textbox" onkeyup="ValidateTime(this);" meta:resourcekey="TextBox4Resource1"></asp:TextBox>
                    <cc1:MaskedEditExtender runat="server" ID="StartTimeTextBoxMaskedEditExtender"
                        TargetControlID="TextDroptime"
                        Mask="99:99"
                        MessageValidatorTip="true"
                        MaskType="Time"
                        AcceptAMPM="false"
                        ErrorTooltipEnabled="True">
                    </cc1:MaskedEditExtender>
                    <br />
                    <asp:Label ID="Label11" runat="server" Text="Drop address" CssClass="label" meta:resourcekey="Label11Resource1"></asp:Label>
                    <asp:TextBox ID="TextDropaddress" runat="server" CssClass="textbox" meta:resourcekey="TextBox5Resource1"></asp:TextBox>
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="TextDropaddress" 
                         FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"  ValidChars=" ,#@" ></cc1:FilteredTextBoxExtender>
                    <br />
                    <asp:Label ID="Label12" runat="server" Text="Vehicle type" CssClass="label" meta:resourcekey="Label12Resource1"></asp:Label>
                    <asp:DropDownList ID="DropDownVehicletype" runat="server" CssClass="textbox" Width="206px" OnSelectedIndexChanged="domesticDropDownMTPort_SelectedIndexChanged" AutoPostBack="True" meta:resourcekey="DropDownList1Resource1"></asp:DropDownList>
                    <br />
                    <asp:Label ID="Label13" runat="server" Text="Vehicle category" CssClass="label" meta:resourcekey="Label13Resource1"></asp:Label>
                    <asp:DropDownList ID="DropDownVehiclecategory" runat="server" CssClass="textbox" Width="206px" OnSelectedIndexChanged="domesticDropDownMCate_SelectedIndexChanged" AutoPostBack="True" meta:resourcekey="DropDownList2Resource1"></asp:DropDownList>
                    <br />
                    <asp:Label ID="Label14" runat="server" Text="Vehicle name" CssClass="label" meta:resourcekey="Label14Resource1"></asp:Label>
                    <asp:DropDownList ID="DropDownVehiclename" runat="server" CssClass="textbox" Width="206px" AutoPostBack="True" meta:resourcekey="DropDownList7Resource1"></asp:DropDownList>
                    <br />
                    <asp:Label ID="Label15" runat="server" Text="Additional services" CssClass="label" meta:resourcekey="Label15Resource1"></asp:Label>
                    <asp:TextBox ID="TextAdditionalservices" runat="server" CssClass="textbox" meta:resourcekey="TextBox9Resource1"></asp:TextBox>
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="TextAdditionalservices" 
                         FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"  ValidChars=" ,#@"></cc1:FilteredTextBoxExtender>
                    <br />
                    <br />
                    <asp:Label ID="Label16" runat="server" Text="Remarks" CssClass="label" meta:resourcekey="Label15Resource1"></asp:Label>
                    <asp:TextBox ID="TextRemarks" runat="server" CssClass="textbox" meta:resourcekey="TextBox9Resource1"></asp:TextBox>
                    <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="TextRemarks" 
                         FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"  ></cc1:FilteredTextBoxExtender>--%>
                    <br />                             
            </div>                                         
                            </asp:Panel>

                            
                      <%--For ticket booking--%>

                    <asp:Panel ID="pnlVehiTicketbooking" runat="server" CssClass="auto-style1" Visible="False"> 
                        <h3>Travel Desk Arrangement Area</h3>
                        <table align="left" cellpadding="0" cellspacing="0" class="auto-style1">
                            <tr>
                                <td>
                                  <asp:Label ID="Label30" runat="server" Text="Vehicle Source" CssClass="label"></asp:Label>
                                  <asp:DropDownList ID="drpdwnVehicleSource" runat="server" CssClass="textbox" Width="140px" AutoPostBack="True" ></asp:DropDownList>
                                </td>
                                <td >
                                  <asp:Label ID="Label40" runat="server" Text="Rate/Kms" CssClass="label"></asp:Label>
                                  <asp:TextBox ID="txtRateKms" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderR" runat="server" TargetControlID="txtRateKms" 
                                  FilterType="Custom, Numbers"  ></cc1:FilteredTextBoxExtender>
                                </td>
                                <td >
                                  <asp:Label ID="Label41" runat="server" Text="Agent Bill Number" CssClass="label"></asp:Label>
                                  <asp:TextBox ID="txtAgentBillNumb" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderA" runat="server" TargetControlID="txtAgentBillNumb" 
                                  FilterType="Custom, Numbers"  ></cc1:FilteredTextBoxExtender>
                                </td> 
                            </tr>
                            <tr >
                                <td >
                                     <asp:Label ID="Label42" runat="server" Text="Agent Name" CssClass="label"></asp:Label>
                                      <asp:DropDownList ID="drpdwnAgentName" runat="server" CssClass="textbox" AutoPostBack="True" Width="140px" ></asp:DropDownList>
                                </td>
                                <td >
                                    <asp:Label ID="Label43" runat="server" Text="Driver Name" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtDriverName" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                 <%--   <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtDriverName" 
                                     FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"  ></cc1:FilteredTextBoxExtender>--%>
                                </td>
                                <td  >
                                    <asp:Label ID="Label44" runat="server" Text="Bill Date" CssClass="label" meta:resourcekey="Label1Resource1"></asp:Label>
                                    <asp:TextBox ID="txtBillDateV" runat="server" onkeyup="ValidateDate(this);" Width="140px" CssClass="textbox" Enabled="false"></asp:TextBox>
                                    <AjaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" PopupButtonID="ImageButton2" TargetControlID="txtBillDateV" Format="dd-MM-yyyy"></AjaxToolkit:CalendarExtender>
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/calendar_icon.png" CssClass="gncontrol" Width="24" Height="22" />
                                </td> 
                            </tr>
                            <tr >
                                <td >
                                     <asp:Label ID="Label45" runat="server" Text="Booking Passed To" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtBookingPassed" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                 <%--   <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtBookingPassed" 
                                     FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"  ></cc1:FilteredTextBoxExtender>--%>
                                </td>
                                <td >
                                     <asp:Label ID="Label46" runat="server" Text="Contact Number" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtContactNumber" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderC" runat="server" TargetControlID="txtContactNumber" 
                                     FilterType="Custom, Numbers"  ></cc1:FilteredTextBoxExtender>
                                </td>
                                <td >
                                     <asp:Label ID="Label47" runat="server" Text="Agent Bill Amount" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtAgentBillAmnt" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtAgentBillAmnt" 
                                     FilterType="Custom, Numbers"  ></cc1:FilteredTextBoxExtender>
                                </td> 
                            </tr>
                            <tr >
                                <td  >
                                    <asp:Label ID="Label48" runat="server" Text="Vehicle Number" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtVehicleNumb" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                    <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtVehicleNumb" 
                                     FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"  ></cc1:FilteredTextBoxExtender>--%>
                                </td>
                                <td >
                                    <asp:Label ID="Label49" runat="server" Text="Statutory Requirements if any" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtStatutory" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                    <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtStatutory" 
                                    FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"  ></cc1:FilteredTextBoxExtender>--%>
                                </td>
                                <td >
                                    <asp:Label ID="Label50" runat="server" Text="Driver Batta" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtDriverBatta" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txtDriverBatta" 
                                     FilterType="Custom, Numbers"  ></cc1:FilteredTextBoxExtender>
                                </td> 
                            </tr>
                              <tr  >
                                <td  >
                                     <asp:Label ID="Label51" runat="server" Text="Total Cost" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtTotalCost" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>                                    
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" TargetControlID="txtTotalCost" 
                                     FilterType="Custom, Numbers"  ></cc1:FilteredTextBoxExtender>
                                </td>                                
                                <td >
                                  <asp:Label ID="Label52" runat="server" Text="Total Km" CssClass="label"></asp:Label>
                                  <asp:TextBox ID="txtTotalKm" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txtTotalKm" 
                                  FilterType="Custom, Numbers"  ></cc1:FilteredTextBoxExtender>
                                </td>
                                <td >&nbsp;</td> 
                            </tr>
                        </table>

                    </asp:Panel>
                   
                   <%-- --------------------%>

                    <div class="clear"></div>
                    <div class="buttonrow">
                        <asp:Button ID="btnVehiTicketBook" runat="server" Text="Ticket Book" OnClick="btnVehiTicketBook_Click" />                      
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" meta:resourcekey="btnCancelResource1" OnClick="btnCancel_Click" />
                    </div>
                    <br />
                                                                      
                        </ContentTemplate>
                    </asp:UpdatePanel>    
                 </div>
                </ContentTemplate>
                </cc1:TabPanel>

           </cc1:TabContainer>

    </ContentTemplate>
 </asp:UpdatePanel>

</asp:Content>
