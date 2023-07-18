<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="hod_Local_acc_vehi_req_pro.aspx.cs" Inherits="iEmpPower.UI.Benefits_Payment.hod_Local_acc_vehi_req_pro" EnableEventValidation="false" Theme="SkinFile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>HOD - Local Req Approval </h2>
    <br />
    <asp:Label ID="lblMessage" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1"></asp:Label>
    <br />
    <cc1:TabContainer ID="tcDefalut" runat="server" ActiveTabIndex="0" AutoPostBack="True" OnActiveTabChanged="tcDefalut_ActiveTabChanged" meta:resourcekey="tcDefalutResource1">

        <cc1:TabPanel runat="server" HeaderText="Approval" ID="tabApproval" meta:resourcekey="TabPanel1Resource1">
            <HeaderTemplate>Approval</HeaderTemplate>
            <ContentTemplate>
                <div>
                    <%--Local Accomodation Approval--%>
                    <br />
                    <asp:UpdatePanel ID="pnlAccommadationApproval" runat="server" Visible="false">
                        <ContentTemplate>
                            <h3>Local Accommodation Approval </h3>
                            <br />
                            <asp:Label ID="lblMessageA" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1"></asp:Label>
                            <br />
                            <asp:GridView ID="grdAccomodationApproval" runat="server" AutoGenerateColumns="False" DataKeyNames="Accommadation_req_id"
                                Width="98%" AllowPaging="false" AllowSorting="false" OnRowDataBound="grdAccomodationApproval_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="Accommadation_req_id">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EMPLOYEE_NO" HeaderText="Employee No.">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LOCAL_ACCOMMODATION_TYPE" HeaderText="LOCAL_ACCOMMODATION_TYPE" Visible="False"
                                        SortExpression="LOCAL_ACCOMMODATION_TYPE">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
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
                                    <asp:BoundField DataField="Arrival_time" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Arrival Time">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Departure_time" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Departure Time">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Additional_service" HeaderText="Additional service">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Current_status" HeaderText="status">
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
                                    <asp:BoundField DataField="Number_of_members" HeaderText="Number_of_members">
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
                                    <asp:BoundField DataField="HotelPlaceCity" HeaderText="HotelPlaceCity" Visible="true">
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
                            <br />
                            <%--Vehicle Approval--%>
                            <h3>Vehicle Approval </h3>
                            <br />
                            <%--<asp:Label ID="lblVehiApproval" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1"></asp:Label>--%>
                            <asp:GridView ID="grdVehicleApproval" runat="server" AutoGenerateColumns="False" DataKeyNames="local_travel_req_id"
                                Width="98%" AllowPaging="false" AllowSorting="false" OnRowDataBound="grdVehicleApproval_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="local_travel_req_id">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <%--     <asp:BoundField DataField="Date_of_travel" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Travel Date"
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
                                    <asp:BoundField DataField="EMPLOYEE_NO" HeaderText="PERNR" Visible="true">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Departure_from" HeaderText="From">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Destination_to" HeaderText="To">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Carrying_any_materials" HeaderText="Materials" Visible="true">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Pickup_time" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Pickup Time" Visible="true">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Pickup_address" HeaderText="Pickup Address" Visible="true">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Drop_time" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Drop Time" Visible="true">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Drop_address" HeaderText="Drop Address" Visible="true">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Vehicle_type" HeaderText="Vehicle Type" Visible="true">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Vehicle_category" HeaderText="Vehicle Class" Visible="true">
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
                                    <asp:BoundField DataField="current_status" HeaderText="status">
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
                                    <asp:BoundField DataField="CREATEDON" HeaderText="created_on" Visible="false">
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
                                    <asp:BoundField DataField="status" HeaderText="isActive" Visible="false">
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
                            <br />
                            <div id="remarks" runat="server">
                                <asp:Label ID="RemarksLabelApproval" runat="server" Text="Remarks"></asp:Label>
                                <asp:TextBox ID="txtHOD_RemarksApproval" runat="server" Width="300px" Height="59px" TextMode="MultiLine" Style="margin-left: 25px; margin-top: 27px"></asp:TextBox>
                            </div>
                            <div class="clear"></div>
                            <div id="Div1" runat="server" class="buttonrow">
                                <asp:Button ID="btnApproveApp" runat="server" Text="Approve" OnClick="btnApproveApp_Click" />
                                <asp:Button ID="btnRejectApp" runat="server" Text="Reject" OnClick="btnRejectApp_Click" />
                                <asp:Button ID="btnClearApp" runat="server" Text="Clear" OnClick="btnClearApp_Click" />
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </ContentTemplate>
        </cc1:TabPanel>


        <cc1:TabPanel runat="server" HeaderText="Proposal" ID="tabProposal" meta:resourcekey="TabPanel1Resource1">
            <HeaderTemplate>Proposal</HeaderTemplate>
            <ContentTemplate>
                <div>
                    <br />
                    <asp:UpdatePanel ID="pnlAccommodationProposal" runat="server" Visible="False">
                        <ContentTemplate>
                            <h3>Accommodation Proposal </h3>
                            <asp:Label ID="lblMessageP" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1"></asp:Label>
                            <br />
                            <br />
                            <%--<asp:Label ID="lblAccProposal" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1"></asp:Label>--%>
                            <asp:GridView ID="grdAccommadationProposal" runat="server" AutoGenerateColumns="False" OnRowDataBound="grdAccommadationProposal_RowDataBound"
                                Width="98%" AllowPaging="True" AllowSorting="false" DataKeyNames="Accommadation_req_id">
                                <Columns>
                                    <asp:BoundField DataField="Accommadation_req_id">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="REQ_SEGMENT_ID" HeaderText="Req_Segment_Id" Visible="False"
                                        SortExpression="REQ_SEGMENT_ID">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
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
                                    <asp:BoundField DataField="EMPLOYEE_NO" HeaderText="Employee No.">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Date_of_travel" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Begin_Date">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Arrival_time" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Arrival Time">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Departure_time" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Departure Time">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Additional_service" HeaderText="Additional service">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="current_status" HeaderText="status">
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
                                    <asp:BoundField DataField="CREATEDON" HeaderText="created_on" Visible="false">
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
                                    <asp:BoundField DataField="isActive" HeaderText="isActive" Visible="false">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="REMARKS" HeaderText="Remarks">
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
                            <br />
                            <%--Vehicle Proposal--%>
                            <h3>Vehicle Proposal </h3>
                            <br />
                            <%--<asp:Label ID="lblVehiProposal" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1"></asp:Label>--%>
                            <asp:GridView ID="grdVehicleProposal" runat="server" AutoGenerateColumns="False" OnRowDataBound="grdVehicleProposal_RowDataBound"
                                Width="98%" AllowPaging="false" AllowSorting="false" DataKeyNames="local_travel_req_id">
                                <Columns>
                                    <asp:BoundField DataField="local_travel_req_id">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <%--  <asp:BoundField DataField="Date_of_travel" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Travel Date"
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
                                    <asp:BoundField DataField="EMPLOYEE_NO" HeaderText="PERNR" Visible="true">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Departure_from" HeaderText="From">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Destination_to" HeaderText="To">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Carrying_any_materials" HeaderText="Materials" Visible="true">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Pickup_time" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Pickup Time" Visible="true">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Pickup_address" HeaderText="Pickup Address" Visible="true">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Drop_time" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Drop Time" Visible="true">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Drop_address" HeaderText="Drop Address" Visible="true">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Vehicle_type" HeaderText="Vehicle Type" Visible="true">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle CssClass="gridTextStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Vehicle_category" HeaderText="Vehicle Class" Visible="true">
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
                                    <asp:BoundField DataField="current_status" HeaderText="status">
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
                                    <asp:BoundField DataField="CREATEDON" HeaderText="created_on" Visible="false">
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
                                    <asp:BoundField DataField="status" HeaderText="isActive" Visible="false">
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
                            <br />
                            <div id="Div3" runat="server">
                                <asp:Label ID="Label1" runat="server" Text="Remarks"></asp:Label>
                                <asp:TextBox ID="txtHOD_RemarkProposal" runat="server" Width="300px" Height="59px" TextMode="MultiLine" Style="margin-left: 25px; margin-top: 27px"></asp:TextBox>
                            </div>
                            <div class="clear"></div>
                            <div id="Div2" runat="server" class="buttonrow">
                                <asp:Button ID="btnApprovePro" runat="server" Text="Approve" OnClick="btnApprovePro_Click" />
                                <asp:Button ID="btnRejectPro" runat="server" Text="Reject" OnClick="btnRejectPro_Click" />
                                <asp:Button ID="btnClearPro" runat="server" Text="Cancel" OnClick="btnClearPro_Click" />
                                <asp:HiddenField ID="HiddenField2" runat="server" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </ContentTemplate>
        </cc1:TabPanel>

    </cc1:TabContainer>


</asp:Content>
