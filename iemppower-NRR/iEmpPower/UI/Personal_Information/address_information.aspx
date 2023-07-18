<%@ Page Title="Address Information" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" EnableEventValidation="false" MaintainScrollPositionOnPostback="true"
    Inherits="iEmpPower.UI_Employee_Performance_address_information" Theme="SkinFile" Culture="en-Gb" CodeBehind="address_information.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <style>
 .HrCls
        {
            width: 100%;
            border: 0;
            height: 1px;
            background: #333;
            background-image: linear-gradient(to right, #333, #333, #ccc);
            padding: 0;
            margin: 3px 0;
        }
 .btn {
  padding: 10px 20px; /* Adjust the padding values as needed */
}
</style>

    <div>
        <span class="hidden">
            <asp:Button ID="btnEntryKey" runat="server" Text="" /></span>

        <div class="row">
            <div class="col-12">
                <div class="page-title-box">
                    <div class="page-title-right">
                        <ol class="breadcrumb m-0">
                            <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                            <li class="breadcrumb-item active">Address Info.</li>
                        </ol>
                    </div>
                    <h4 class="page-title">Address Information
                    <asp:Label ID="Label1" runat="server" CssClass="msgboard"></asp:Label>
                    </h4>
                </div>
            </div>
        </div>

         <div class="header">
            <asp:Label ID="LblMsg" runat="server" CssClass="lblValidation"></asp:Label>
        </div>

        <div class=" card-box">
            <div id="real_time_chart" class="dashboard-flot-chart">

                <div id="divbrdr" class="divfr">
                        <asp:MultiView ID="MV_AddressInfo" runat="server">

                            <asp:View ID="V_ViewAddressInfo" runat="server">
                                <div class="DivSpacer01">
                                <asp:LinkButton ID="LblAddAddressInfo" runat="server" CausesValidation="false" CssClass="btn btn-sm bg-brand-btn waves-effect waves-light float-right"  OnClick="LblAddAddressInfo_Click"><i class="mdi mdi-plus"></i>Add New Address info</asp:LinkButton>
                                </div>
                                <div class="DivSpacer01">
                                    <asp:GridView ID="GV_AddressInfo" runat="server" AutoGenerateColumns="False" AllowPaging="false" DataKeyNames="ID"
                                        PageSize="10" OnRowCommand="GV_AddressInfo_RowCommand" OnRowEditing="GV_AddressInfo_RowEditing" OnRowUpdating="GV_AddressInfo_RowUpdating">
                                        <Columns>                                           
                                            <asp:BoundField DataField="Address_Typetxt" HeaderText="Address Type" />
                                            <asp:BoundField DataField="ResNo" HeaderText="Address Details" />
                                            <%--<asp:BoundField DataField="Locality" HeaderText="Locality" />--%>
                                            <asp:BoundField DataField="District" HeaderText="District" />
                                            <%--<asp:BoundField DataField="StateTxt" HeaderText="State" />--%>
                                            <%--<asp:BoundField DataField="CountryTxt" HeaderText="Country" />--%>
                                            <%--<asp:BoundField DataField="Pincode" HeaderText="Pincode" />--%>
                                            <asp:BoundField DataField="startdate" HeaderText="Valid From" DataFormatString = "{0:dd/MMM/yyyy}"/>
                                            <asp:BoundField DataField="enddate" HeaderText="To Date" DataFormatString = "{0:dd/MMM/yyyy}"/>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="GVViewAddressInfo" runat="server" CausesValidation="false" CommandName="VIEW" 
                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-xs btn-warning"><i class="fe-eye"></i></asp:LinkButton>
                                                    &nbsp;
                                       
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="row col-md-12">
                                    <div class="col-md-3" style="margin-top: 5px" id="divpendingrecordcount" runat="server"></div>
                                    <div class="col-md-9 DivSpacer01 Div02 repeater text-xl-right">
                                        <asp:Repeater ID="RptrAddressInfoPager" runat="server">
                                            <ItemTemplate>
                                                <ul class="pagination pagination-rounded" style="display: inline-block">
                                                    <li class='paginate_button page-item <%# Convert.ToBoolean(Eval("Enabled")) ? "" : "active" %>'>
                                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="RptrLeaveOverviewPagerPage_Changed" CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'></asp:LinkButton>
                                                        </li>
                                                </ul>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="DivSpacer01"></div>
                            </asp:View>

                            <asp:View ID="V_AddEditAddressInfo" runat="server">
                                <div class="DivSpacer01 Cb">
                                    <div style="width: 90%; float: left"></div>
                                    <div style="width: 10%; float: right">       
                                        <asp:LinkButton ID="LbtnBackAddressInfoView" runat="server" CausesValidation="false" CssClass="btn btn-sm bg-brand-btn waves-effect waves-light float-right" OnClick="LbtnBackAddressInfoView_Click"><i class="mdi mdi-step-backward"></i>  Back</asp:LinkButton>
                                    </div>
                                </div>
                                <div style="width: 100%">
                                    <asp:FormView ID="FV_AddressInfo" runat="server" DataKeyNames="ID,TRANSSTATUS,SUBTY" OnItemCommand="FV_AddressInfo_ItemCommand"
                                        OnModeChanging="FV_AddressInfo_ModeChanging" Width="100%">
                                        <ItemTemplate>
                                            <div>
                                               <div class="header-title"><%# Eval("ADDRESS_TYPE_TEXT")%> Address Information</div>
                                                <hr class="HrCls"/>
                                                <br />
                                                <div class="DivSpacer01"></div>
                                            </div>
                                            <div class="DivSpacer03 Cb">

                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-2 htCr"> Address Type  &nbsp;<b>:</b>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <%# Eval("ADDRESS_TYPE_TEXT")%>
                                                        </div>
                                                    </div>



                                                    <div class="row">
                                                        <div class="col-sm-2 htCr">Address line 1  &nbsp;<b>:</b>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <%# Eval("ADDRESSL1")%>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-2 htCr">Address line 2    &nbsp;<b>:</b>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <%# Eval("ADDRESSL2")%>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-2 htCr">District    &nbsp;<b>:</b>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <%# Eval("CITY")%>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-2 htCr">State Name  &nbsp;<b>:</b>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <%# Eval("BEZEI")%>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-2 htCr">Country Name   &nbsp;<b>:</b>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <%# Eval("LANDX")%>
                                                        </div>
                                                    </div>

                                                     <div class="row">
                                                        <div class="col-sm-2 htCr">Valid From   &nbsp;<b>:</b>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <%# Eval("DATE_FROM", "{0:dd/MM/yyyy}")%>
                                                        </div>
                                                    </div>

                                                     <div class="row">
                                                        <div class="col-sm-2 htCr">To Date   &nbsp;<b>:</b>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <%# Eval("DATE_TO", "{0:dd/MM/yyyy}")%>
                                                        </div>
                                                    </div>


                                                    <div class="row">
                                                        <div class="col-sm-2 htCr"> Postal Code   &nbsp;<b>:</b>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <%# Eval("POSTAL_CODE")%>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-2 htCr">STD Code   &nbsp;<b>:</b>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <%# Eval("PHONENO")%>
                                                        </div>
                                                    </div>
                                                </div>

                                                 <div class="row">
                                                        <div class="col-sm-6">
                                                        <asp:Button ID="Button1" runat="server" Text="Edit" CausesValidation="false" TabIndex="1"
                                                            Enabled='<%# (Eval("TRANSSTATUS").ToString()=="Updated") ? false : true  %>'
                                                            Visible='<%# (Eval("TRANSSTATUS").ToString()=="Updated") ? false : true  %>'
                                                            CommandName="EDITADDRESS"/>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>


                                        <EditItemTemplate>
                                               <div class="header-title">Edit <%# Eval("ADDRESS_TYPE_TEXT")%> Address Information</div>
                                                <hr class="HrCls"/>
                                                <br />
                                          
                                            <div class="DivSpacer03 Cb">

                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-2 htCr">&nbsp;&nbsp;&nbsp;Address Type <b>:</b></div>
                                                        <div class="col-sm-6">
                                                            <%# Eval("ADDRESS_TYPE_TEXT")%>
                                                            <asp:HiddenField ID="HF_ID" runat="server" Value='<%# Eval("ID")%>' />
                                                            <asp:HiddenField ID="HF_Cntry" runat="server" Value='<%# Eval("GBLND")%>' />
                                                            <asp:HiddenField ID="HF_State" runat="server" Value='<%# Eval("STATE_ID")%>' />
                                                            <asp:HiddenField ID="HF_statept" runat="server" />
                                                            <asp:HiddenField ID="HF_SUBTY" runat="server" Value='<%# Eval("SUBTY")%>' />
                                                        </div>
                                                    </div>
                                                    <div>

                                                        <div class="form-group col-md-12">

                                                            <div class="">
                                                                <div class="row">
                                                                </div>
                                                            </div>
                                                            <div class="">
                                                                <div class="row">
                                                                    <div class="col-md-2"><span style="color:red">*</span>Address Line 1 :</div>
                                                                    <div class="col-md-4">
                                                                        <asp:TextBox ID="txtAddline1" MaxLength="190" TextMode="MultiLine" CssClass="txtDropDownwidth" runat="server" Text='<%# Bind("ADDRESSL1") %>'></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="UpdateAddrInfoVG" ControlToValidate="txtAddline1" ForeColor="Red" ErrorMessage="*">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="col-md-2"><span style="color:white">*</span>Address Line 2 :</div>
                                                                    <div class="col-md-4">
                                                                        <asp:TextBox ID="txtAddline2" MaxLength="190" TextMode="MultiLine" CssClass="txtDropDownwidth" runat="server" Text='<%# Bind("ADDRESSL2") %>'></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="">
                                                                <div class="row">
                                                                    <div class="col-md-2"><span style="color:red">*</span>Country :</div>
                                                                    <div class="col-md-4">
                                                                        <asp:DropDownList ID="ddlAddCountry" CssClass="txtDropDownwidth" runat="server" OnSelectedIndexChanged="ddlAddCountry_SelectedIndexChanged" AutoPostBack="true">
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="REQ_addcntry" runat="server" Display="Dynamic" InitialValue="0" SetFocusOnError="true" ValidationGroup="UpdateAddrInfoVG" ControlToValidate="ddlAddCountry" ForeColor="Red" ErrorMessage="*">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="col-md-2"><span style="color:red">*</span>State :</div>
                                                                    <div class="col-md-4">
                                                                        <asp:DropDownList ID="ddlAddState" CssClass="txtDropDownwidth" runat="server">
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="REQ_addstate" runat="server" Display="Dynamic" InitialValue="0" SetFocusOnError="true" ValidationGroup="UpdateAddrInfoVG" ControlToValidate="ddlAddState" ForeColor="Red" ErrorMessage="*">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="">
                                                                <div class="row">
                                                                    <div class="col-md-2"><span style="color:red">*</span>Valid From :</div>
                                                                    <div class="col-md-4">
                                                                        <asp:TextBox ID="txtaddstartdt" runat="server" CssClass="txtDropDownwidth" Text='<%# Eval("DATE_FROM", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RQF_frmdate" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="UpdateAddrInfoVG" ControlToValidate="txtaddstartdt" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                        <Ajx:CalendarExtender ID="CE_frmdt" runat="server" Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtaddstartdt" PopupButtonID="txtaddstartdt"></Ajx:CalendarExtender>
                                                         <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" FilterType="Custom,Numbers" TargetControlID="txtaddstartdt" ValidChars=" -"></Ajx:FilteredTextBoxExtender>
                                                                         </div>
                                                                    <div class="col-md-2"><span style="color:red">*</span>To Date :</div>
                                                                    <div class="col-md-4">
                                                                        <asp:TextBox ID="txtaddenddt" runat="server" CssClass="txtDropDownwidth" Text='<%# Eval("DATE_TO","{0:yyyy-MM-dd}") %>'></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RQF_enddt" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="UpdateAddrInfoVG" ControlToValidate="txtaddenddt" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                        <Ajx:CalendarExtender ID="CE_enddt" runat="server" Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtaddenddt" PopupButtonID="txtaddenddt"></Ajx:CalendarExtender>
                                                        <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom,Numbers" TargetControlID="txtaddenddt" ValidChars=" -"></Ajx:FilteredTextBoxExtender>
                                                                         </div>
                                                                </div>
                                                            </div>


                                                            <div class="">
                                                                <div class="row">
                                                                    <div class="col-md-2"><span style="color:white">*</span>District :</div>
                                                                    <div class="col-md-4">
                                                                        <asp:TextBox ID="txtAddDistrict" CssClass="txtDropDownwidth" runat="server" Text='<%# Bind("CITY") %>'></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-2"><span style="color:white">*</span>Pincode :</div>
                                                                    <div class="col-md-4">
                                                                        <asp:TextBox ID="txtAddPincode" CssClass="txtDropDownwidth" runat="server" Text='<%# Bind("POSTAL_CODE") %>'></asp:TextBox>
                                                                        
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="">
                                                                <div class="row">
                                                                    <div class="col-md-2"><span style="color:white">*</span>STD Code :</div>
                                                                    <div class="col-md-4">
                                                                        <asp:TextBox ID="txtAddStd" Text='<%# Bind("PHONENO") %>' CssClass="txtDropDownwidth" runat="server"></asp:TextBox>
                                                                        
                                                                    </div>
                                                                     <div class="col-md-6">
                                                                    <asp:CompareValidator ID="CV_dates" ValidationGroup="Date" Font-Size="Medium" ForeColor="Red" runat="server" ControlToValidate="txtaddstartdt" ControlToCompare="txtaddenddt" Operator="LessThan" Type="Date" ErrorMessage="From date must be less than To date." Display="Dynamic" SetFocusOnError="true"></asp:CompareValidator>
                                                                         </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                         <div class="row">
                                                            <div class="col-md-6">
                                                                 <asp:UpdatePanel runat="server" ID="a">
                                                                    <ContentTemplate> 
                                                                <asp:Button ID="BtnEditSubmit" runat="server" TabIndex="11" Text="Update" ValidationGroup="UpdateAddrInfoVG" CommandName="UPDATEADDRESS" OnClientClick="return validate('Update');" />&nbsp;                                               
                                                                <asp:Button ID="BtnEditCancel" runat="server" TabIndex="12" Text="Cancel" CommandName="CANCEL"  CausesValidation="false"/>
                                                               </ContentTemplate> 
                                                                <Triggers>
                                                                <asp:PostBackTrigger ControlID="BtnEditSubmit"/>
                                                                <asp:PostBackTrigger ControlID="BtnEditCancel"/>
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                        <div class="Fr" style="width: 99%; color: Red;">
                                                            <asp:Literal ID="LtrMsg" runat="server"></asp:Literal>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="DivSpacer01">
                                                </div>
                                        </EditItemTemplate>



                                        <InsertItemTemplate>
                                            <asp:HiddenField ID="stst" runat="server" />
                                             <div class="header-title">&nbsp;&nbsp;Add new address information</div>
                                                <hr class="HrCls"/>
                                            <br />
                                            <div class="DivSpacer03 Cb">


                                                <div class="form-group col-md-12">

                                                    <div class="">
                                                        <div class="row">
                                                            <div class="col-md-2"><span style="color:red">*</span>Address Type :</div>
                                                            <div class="col-md-4">
                                                                <asp:DropDownList ID="DDL_Address" CssClass="txtDropDownwidth" runat="server"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="REQ_addtyp" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="AddAddrInfoVG" ControlToValidate="DDL_Address" InitialValue="0" ForeColor="Red" ErrorMessage="*">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-md-2"><span style="color:red">*</span>Locality :</div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtLocality" CssClass="txtDropDownwidth" runat="server"></asp:TextBox>

                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="AddAddrInfoVG" ControlToValidate="txtLocality" ForeColor="Red" ErrorMessage="*">
                                                                </asp:RequiredFieldValidator>
                                                                <Ajx:FilteredTextBoxExtender ID="FLTR_Locality" runat="server" FilterType="Numbers,UppercaseLetters,LowercaseLetters,Custom" FilterMode="ValidChars" ValidChars=". " TargetControlID="txtLocality"></Ajx:FilteredTextBoxExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="">
                                                        <div class="row">
                                                            <div class="col-md-2"><span style="color:red">*</span>Address Line 1 :</div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtAddline1" MaxLength="190" TextMode="MultiLine" CssClass="txtDropDownwidth" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="AddAddrInfoVG" ControlToValidate="txtAddline1" ForeColor="Red" ErrorMessage="*">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-md-2">Address Line 2 :</div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtAddline2" MaxLength="190" TextMode="MultiLine" CssClass="txtDropDownwidth" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="">
                                                        <div class="row">
                                                            <div class="col-md-2"><span style="color:red">*</span>Country :</div>
                                                            <div class="col-md-4">
                                                                <asp:DropDownList ID="ddlAddCountry" CssClass="txtDropDownwidth" runat="server" OnSelectedIndexChanged="ddlAddCountry_SelectedIndexChanged" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="Req_newcntry" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="AddAddrInfoVG" ControlToValidate="ddlAddCountry" InitialValue="0" ForeColor="Red" ErrorMessage="*">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-md-2"><span style="color:red">*</span>State :</div>
                                                            <div class="col-md-4">
                                                                <asp:DropDownList ID="ddlAddState" CssClass="txtDropDownwidth" runat="server"></asp:DropDownList>
                                                                 <asp:RequiredFieldValidator ID="REQ_nwstate" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="AddAddrInfoVG" ControlToValidate="ddlAddState" InitialValue="0" ForeColor="Red" ErrorMessage="*">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                     <div class="">
                                                        <div class="row">
                                                            <div class="col-md-2"><span style="color:red">*</span>Valid From :</div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtbedda" runat="server" CssClass="txtDropDownwidth"></asp:TextBox>
                                                                 <asp:RequiredFieldValidator ID="RQF_nwfrmdate" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="AddAddrInfoVG" ControlToValidate="txtbedda" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                        <Ajx:CalendarExtender ID="CE_begda" runat="server" Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtbedda" PopupButtonID="txtbedda"></Ajx:CalendarExtender>
                                                        <Ajx:FilteredTextBoxExtender ID="FLT_addstdt" runat="server" FilterType="Custom,Numbers" TargetControlID="txtbedda" ValidChars=" -"></Ajx:FilteredTextBoxExtender>
                                                                </div>
                                                            <div class="col-md-2"><span style="color:red">*</span>To Date :</div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtendda" runat="server" CssClass="txtDropDownwidth"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="AddAddrInfoVG" ControlToValidate="txtendda" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                        <Ajx:CalendarExtender ID="CE_endda" runat="server" Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtendda" PopupButtonID="txtendda"></Ajx:CalendarExtender>
                                                       <Ajx:FilteredTextBoxExtender ID="FLT_addenda" runat="server" FilterType="Custom,Numbers" TargetControlID="txtendda" ValidChars=" -"></Ajx:FilteredTextBoxExtender>
                                                                 </div>
                                                        </div>
                                                    </div>

                                                    <div class="">
                                                        <div class="row">
                                                            <div class="col-md-2"><span style="color:white">*</span>District :</div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtAddDistrict" CssClass="txtDropDownwidth" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-2"><span style="color:white">*</span>Pincode :</div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtAddPincode" CssClass="txtDropDownwidth" runat="server"></asp:TextBox>
                                                                <Ajx:FilteredTextBoxExtender ID="FLTR_AddPincode" runat="server" FilterType="Numbers" TargetControlID="txtAddPincode"></Ajx:FilteredTextBoxExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="">
                                                        <div class="row">
                                                            <div class="col-md-2"><span style="color:white">*</span>STD Code :</div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtAddStd" CssClass="txtDropDownwidth" runat="server"></asp:TextBox>
                                                                <Ajx:FilteredTextBoxExtender ID="FLTR_AddStd" runat="server" FilterType="Numbers" TargetControlID="txtAddStd"></Ajx:FilteredTextBoxExtender>
                                                            </div>
                                                            <div class="col-md-2"><span style="color:white">*</span>Ward Number :</div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtWardNum" CssClass="txtDropDownwidth" runat="server"></asp:TextBox>
                                                                <Ajx:FilteredTextBoxExtender ID="FLTR_WardNum" runat="server" FilterType="Numbers" TargetControlID="txtWardNum"></Ajx:FilteredTextBoxExtender>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <asp:CompareValidator ID="CV_dates" ValidationGroup="Date" Font-Size="Medium" ForeColor="Red" runat="server" ControlToValidate="txtbedda" ControlToCompare="txtendda" Operator="LessThan" Type="Date" ErrorMessage="From date must be less than To date." Display="Dynamic" SetFocusOnError="true"></asp:CompareValidator>
                                                </div>

                                                <div class="row">
                                                    
                                                    <div class="col-sm-6">
                                                        <asp:Button ID="BtnSubmit" Width="70px" runat="server" TabIndex="23" Text="Submit" ValidationGroup="AddAddrInfoVG" CommandName="ADDADDRESS"
                                                            OnClientClick="return  validate('Add');" />
                                                    
                                                        <asp:Button ID="btnCancel" Width="70px" runat="server" TabIndex="25" Text="Cancel" CommandName="CANCEL" CausesValidation="false"/>
                                                       
                                                    </div>
                                                    <div class="col-sm-1">
                                                         <asp:Button ID="BtnClear" runat="server" Visible="false" style="width: 70px" TabIndex="24" Text="Clear" CommandName="CLEAR" OnClientClick="Clear();" />                                                       
                                                     </div>
                                                     </div>
                                                    <asp:UpdatePanel runat="server" ID="a">
                                             <ContentTemplate>    
                                                     </ContentTemplate> 
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="BtnSubmit"/>
                                                    <asp:PostBackTrigger ControlID="btnCancel"/>
                                                     <asp:PostBackTrigger ControlID="BtnClear"/>
                                                </Triggers>
                                                </asp:UpdatePanel>
                                                   

                                                    <div class="Fr" style="width: 99%; color: Red;">
                                                        <asp:Literal ID="LtrMsg" runat="server"></asp:Literal>
                                                    </div>

                                                </div>

                                            </div>
                                            <div class="DivSpacer01">
                                            </div>
                                        </InsertItemTemplate>
                                    </asp:FormView>
                                </div>

                            </asp:View>

                        </asp:MultiView>

                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">

        $(function () {
            $('select[id$="DDLCountryName"]').on('change', function () {
                if ($(this).val().length > 0) {
                    $('select[id$="DDLStateName"]').val('0').prop('disabled', false);
                }
            });
        });

        function validate(Msg) {
            if (Page_ClientValidate())
                return confirm('Do you want to ' + Msg + ' this address details ?');
        }

        function Clear() {
            document.getElementById("MainContent_FV_AddressInfo_DDL_Address").selectedIndex = "";
            document.getElementById("MainContent_FV_AddressInfo_txtWardNum").value = "";
            document.getElementById("MainContent_FV_AddressInfo_txtAddStd").value = "";
            document.getElementById("MainContent_FV_AddressInfo_txtAddline1").value = "";
            document.getElementById("MainContent_FV_AddressInfo_txtAddline2").value = "";
            document.getElementById("MainContent_FV_AddressInfo_DDLCountryName").selectedIndex = "";
            document.getElementById("MainContent_FV_AddressInfo_DDLStateName").selectedIndex = "";
            document.getElementById("MainContent_FV_AddressInfo_TxtCity").value = "";
            document.getElementById("MainContent_FV_AddressInfo_TxtPostalCode").value = "";
            document.getElementById("MainContent_FV_AddressInfo_txtAddDistrict").value = "";
            document.getElementById("MainContent_FV_AddressInfo_txtLocality").value = "";

        }
    </script>
</asp:Content>

