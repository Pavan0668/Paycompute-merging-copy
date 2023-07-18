<%@ Page Title="Communication Information" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" 
    Inherits="UI_Personal_Information_communication_information" MaintainScrollPositionOnPostback="true"
    Theme="SkinFile" EnableEventValidation="false" CodeBehind="communication_information.aspx.cs" %>

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

    </style>

    <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Communication Info.</li>
                    </ol>
                </div>
                <h4 class="page-title">Communication Information
                    <asp:Label ID="Label1" runat="server" CssClass="msgboard"></asp:Label>
                </h4>
            </div>
        </div>
    </div>


        <div class="header">
        <asp:Label ID="LblMsg" runat="server" CssClass="lblMsg"></asp:Label>
    </div>



        <div class=" card-box">
            <div id="real_time_chart" class="dashboard-flot-chart">

                <div id="divbrdr" class="divfr">
                    <div class="search-section">
                        <asp:MultiView ID="MV_CommInfo" runat="server">
                            <asp:View ID="V_ViewCommInfo" runat="server">
                                <div class="DivSpacer01">
                                    &nbsp;<asp:LinkButton ID="LblAddCommInfo" runat="server" CausesValidation="false" OnClick="LblAddCommInfo_Click"  CssClass="btn btn-sm bg-brand-btn waves-effect waves-light float-right"><i class="mdi mdi-plus"></i> Add New Communication Info</asp:LinkButton>
                                </div>
                                <div  id="contact1" runat="server">
                                <div class="DivSpacer01 respovrflw">
                                    <asp:GridView ID="GV_CommInfo" runat="server" AutoGenerateColumns="False" AllowPaging="false" DataKeyNames="ID,Contact_Type,Contact_Type_ID"
                                        PageSize="10" OnRowCommand="GV_CommInfo_RowCommand" OnRowDeleting="GV_CommInfo_RowDeleting" OnRowEditing="GV_CommInfo_RowEditing"
                                        OnRowUpdating="GV_CommInfo_RowUpdating" OnRowDataBound="GV_CommInfo_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="Contact_Type_TXT" HeaderText="Communication Type"></asp:BoundField>
                                            <asp:BoundField DataField="Contact_Type_ID" HeaderText="Details">
                                                <ItemStyle />
                                            </asp:BoundField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="GVEditCommInfo" runat="server" CausesValidation="false" CommandName="EDIT"
                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  CssClass="fe-edit-1"></asp:LinkButton>
                                                </ItemTemplate>                                                
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                </div>
                                <div class="DivSpacer01 Div03">
                                    <asp:Repeater ID="RptrCommInfoPager" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>'
                                                OnClick="RptrCommInfoPagerPage_Changed" CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="DivSpacer01"></div>
                            </asp:View>



                            <asp:View ID="V_AddEditCommInfo" runat="server">
                                <div class="DivSpacer01">
                                    <div style="width: 90%; float: left"></div>
                                    <div style="width: 10%; float: right"> 
                                        <asp:LinkButton ID="LbtnBackCommInfoView" runat="server" CausesValidation="false"  OnClick="LbtnBackCommInfoView_Click" CssClass="btn btn-sm bg-brand-btn waves-effect waves-light float-right"><i class="mdi mdi-step-backward"></i>  Back</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="DivSpacer01" style="overflow-x: auto; width: 100%;">
                                    <asp:FormView ID="FV_CommInfo" Width="90%" runat="server" DataKeyNames="ID,USRID,TRANSSTATUS" OnItemCommand="FV_CommInfo_ItemCommand" OnModeChanging="FV_CommInfo_ModeChanging">

                                        <ItemTemplate>
                                            <div>
                                                <b><%# Eval("USTXT")%> information</b>
                                                <div class="DivSpacer01"></div>
                                            </div>
                                            <div class="DivSpacer03 Cb">
                                                <table class="TblCls Cb Fl">
                                                    <tr>
                                                        <td class="Td06"><%# Eval("USTXT")%></td>
                                                        <td class="Td07"><b>:</b> </td>
                                                        <td>
                                                            <%# Eval("USRID")%>
                                                        </td>

                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="BtnEdit" runat="server" Text="Edit" CausesValidation="false" CommandName="EDITCOMM" />&nbsp;                                             

                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="DivSpacer01">
                                            </div>
                                        </ItemTemplate>

                                        

                                        <EditItemTemplate>
                                                <div class="header-title">Edit <%# Eval("USTXT")%> Information</div>
                                                <hr class="HrCls"/>
                                                 <br />
                                            <div class="DivSpacer03 Cb" style="width: 100%;">


                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-md-7"><span style="color:red">*</span>
                                                            <asp:TextBox ID="TxtEditCommDetails" runat="server" autocomplete="off" CssClass="txtDropDownwidth"
                                                                MaxLength="200" placeholder="Enter ID / No." TabIndex="1" ValidationGroup="UpdateCommInfoVG" Text='<%# Bind("USRID")%>'></asp:TextBox>
                                                            <Ajx:FilteredTextBoxExtender ID="FTB_TxtEditCommDetails" runat="server" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                                ValidChars=" ./\#&()[]{};:,@-_=+" TargetControlID="TxtEditCommDetails">
                                                            </Ajx:FilteredTextBoxExtender>
                                                           
                                                            <asp:RequiredFieldValidator ID="RFV_TxtEditCommDetails" runat="server" ControlToValidate="TxtEditCommDetails" CssClass="lblValidation"
                                                                Display="Dynamic" ErrorMessage="Please enter Communication details !" ValidationGroup="UpdateCommInfoVG" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                               <div class="row">
                                                        <div class="col-md-7">
                                                        <asp:Button ID="BtnEditSubmit" runat="server" TabIndex="2" Text="Update" ValidationGroup="UpdateCommInfoVG" Width="60" CommandName="UPDATECOMM"
                                                            Enabled='<%# (Eval("TRANSSTATUS").ToString()=="Updated") ? false : true  %>'
                                                            Visible='<%# (Eval("TRANSSTATUS").ToString()=="Updated") ? false : true  %>'
                                                            OnClientClick="return validate('Update');" />&nbsp;
                                                   
                                                        <asp:Button ID="BtnEditCancel" runat="server" TabIndex="3" Text="Cancel" CommandName="CANCEL" Width="60" CausesValidation="false"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="DivSpacer01">
                                            </div>
                                        </EditItemTemplate>

                                        

                                        <InsertItemTemplate>
                                             <div class="DivSpacer03 Cb col-sm-12">

                                        <div class="form-group">
                                               <div class="header-title">Add New Communication Information</div>
                                                <hr class="HrCls"/>
                                                 <br />
                                            <div class="row">
                                                <div class="col-md-6"><span style="color:red">*</span>
                                                            <asp:DropDownList ID="DDL_Commtype" runat="server" CssClass="txtDropDownwidth"
                                                                TabIndex="4" ValidationGroup="AddCommInfoVG">
                                                            </asp:DropDownList>

                                                            <asp:RequiredFieldValidator ID="RFV_DDL_Commtype" runat="server" ControlToValidate="DDL_Commtype" Display="Dynamic" CssClass="lblValidation"
                                                                ErrorMessage="Please Select Communication Type" ForeColor="Red" InitialValue="0" ValidationGroup="AddCommInfoVG"></asp:RequiredFieldValidator>

                                                         </div>
                                                </div>
                                                <div class="row">
                                                <div class="col-md-6"><span style="color:red">*</span>
                                                            <asp:TextBox ID="TxtCommDetails" runat="server" autocomplete="off" CssClass="txtDropDownwidth"
                                                                MaxLength="200" placeholder="Enter Address Type ID / No." TabIndex="5" ValidationGroup="AddCommInfoVG"></asp:TextBox>
                                                            <Ajx:FilteredTextBoxExtender ID="FTB_TxtCommDetails" runat="server" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                                ValidChars=" ./\#&()[]{};:,@-_=+" TargetControlID="TxtCommDetails">
                                                            </Ajx:FilteredTextBoxExtender>
                                                           

                                                            <asp:RequiredFieldValidator ID="RFV_TxtCommDetails" runat="server" ControlToValidate="TxtCommDetails" CssClass="lblValidation"
                                                                Display="Dynamic" ErrorMessage="Please Enter Details" ValidationGroup="AddCommInfoVG" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                            </div>
                                            <br />
                                            <br />
                                            <div class="row">
                                                <div class="col-md-6">
                                                        <asp:Button ID="BtnSubmit" runat="server" TabIndex="6" Text="Submit" ValidationGroup="AddCommInfoVG" Width="60" CommandName="ADDCOMM" CausesValidation="true" OnClientClick="return validate('Add');" />
                                                  
                                                        <asp:Button ID="BtnCancel" runat="server" TabIndex="8" Text="Cancel" CausesValidation="false" Width="60" CommandName="CANCEL" />
                                                    </div>
                                            </div>

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
        function validate(Msg) {
            if (Page_ClientValidate())
                return confirm('Do you want to ' + Msg + ' this communication details ?');
        }
    </script>
</asp:Content>

