<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.Master" AutoEventWireup="true" CodeBehind="lock_lockuser.aspx.cs" MaintainScrollPositionOnPostback="true"
    Inherits="iEmpPower.UI.Configuration.lock_lockuser" EnableEventValidation="false" Theme="SkinFile" Culture="en-GB" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 
    <style>

        .gridviewNew td a {
            font-size: 16px;
            font-weight: 600;
        }

      
     
        .HrCls {
            width: 100%;
            border: 0;
            height: 1px;
            background: #333;
            background-image: linear-gradient(to right, #333, #333, #ccc);
            padding: 0;
            margin: 3px 0;
        }

        .close {
            background-color: white !important;
            border: none !important;
            font-size: small;
        }

          .popUpStyle
        {
            /*font: normal 11px auto "Trebuchet MS", Verdana;*/
            background-color: #000000;
            /*color: #4f6b72;*/
            /*padding: 6px;*/
            filter: alpha(opacity=80);
            opacity: 0.15;
        }
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }


        .modalPopupDefault
        {
            text-align: left;
            background-color: #FFFFFF;
            /* border-width: 3px;
            border-style: solid;
            border-color: black;
            padding: 10px;*/
            /*padding-left: 10px;*/
            width: auto;
            height: 80vh;
            overflow: auto;
        }
    </style>

    

    <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Company Configuration</li>
                    </ol>
                </div>
                <h4 class="page-title">Company Configuration
                    <asp:Label ID="Label1" runat="server" CssClass="msgboard"></asp:Label>
                </h4>
            </div>
        </div>
    </div>

    <div class="header">
        <asp:Label ID="LblMsg" runat="server" CssClass="lblValidation"></asp:Label>
    </div>
    <div class="card-box">
        <div id="real_time_chart" class="row">
            <div style="width: 99%; margin: 0 auto; padding: 5px 0 40px 0;">
                <div class="col-sm-12" style="width: 100%">
                    <div style="width: 100%">
                        <ul class="nav nav-pills navtab-bg">

                            <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Tab1" class="nav-link p-2" OnClick="Tab1_Click" CausesValidation="false"><i class="fe-globe" ></i>
                            About Company</asp:LinkButton></li>
                            <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Tab2" class="nav-link  p-2" OnClick="Tab2_Click" CausesValidation="false"><i class="fe-settings"></i>
                            Generate/Lock Users</asp:LinkButton></li>
                            <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Tab3" class="nav-link  p-2" OnClick="Tab3_Click" CausesValidation="false"><i class="fe-award"></i>
                            Desgn/Mgr Mapping</asp:LinkButton></li>
                            <%--<li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Tab4" class="nav-link  p-2" OnClick="Tab4_Click" CausesValidation="false"><i class="fe-briefcase"></i>
                            Manager Mapping</asp:LinkButton></li>--%>
                            <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Tab5" class="nav-link  p-2" OnClick="Tab5_Click" CausesValidation="false"><i class="fe-clock"></i>
                            Leave Quota Config.</asp:LinkButton></li>
                            <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Tab6" class="nav-link  p-2" OnClick="Tab6_Click" CausesValidation="false"><i class="fe-calendar"></i>
                            Holiday Calendar</asp:LinkButton></li>
                        </ul>
                        <div class="tabcontents">
                            <div id="view1" runat="server" visible="false" style="width: 100%">
                                <br />
                                <div class="header-title">&nbsp;&nbsp;Company Details</div>
                                <hr class="HrCls" />
                                <br />

                                <asp:FormView ID="frmCompDetails" runat="server" DataKeyNames="Company_Code" OnItemCommand="frmCompDetails_ItemCommand" Width="100%">
                                    <ItemTemplate>
                                        <div style="float: right !important">
                                            <asp:LinkButton ID="LbtnEditPerInfo" runat="server" TabIndex="1"
                                                Text="&#171;&nbsp;Edit" CausesValidation="false" CommandName="EDITINFO" CssClass="btn btn-sm bg-brand-btn waves-effect waves-light float-right"><i class="fe-edit-1"></i> Edit</asp:LinkButton>
                                        </div>

                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-3 htCr">Logo&nbsp;<b>:</b></div>
                                                <div class="col-sm-6">
                                                    <asp:Image ImageUrl='<%# Eval("EMPLOYEE_PATH")%>' runat="server" Width="180" Height="60" ID="imgLogo" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-3 htCr">Company Name&nbsp;<b>:</b></div>
                                                <div class="col-sm-6"><%# Eval("Company_Name")%></div>
                                            </div>
                                        </div>
                                        <%--<div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-3 htCr">Company Code&nbsp;<b>:</b></div>
                                                <div class="col-sm-6"><%# Eval("Company_Code")%></div>
                                            </div>
                                        </div>--%>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-3 htCr">Company Type&nbsp;<b>:</b></div>
                                                <div class="col-sm-6"><%# Eval("Company_Type_Txt")%></div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-3 htCr">Official E-Mail&nbsp;<b>:</b></div>
                                                <div class="col-sm-6"><%# Eval("Company_MailID")%></div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-3 htCr">Official Contact No.&nbsp;<b>:</b></div>
                                                <div class="col-sm-6"><%# Eval("Company_ContactNum")%></div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-3 htCr">Company Address&nbsp;<b>:</b></div>
                                                <div class="col-sm-6"><%# Eval("Company_Address")%></div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-3 htCr">District&nbsp;<b>:</b></div>
                                                <div class="col-sm-6"><%# Eval("District")%></div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-3 htCr">Country&nbsp;<b>:</b></div>
                                                <div class="col-sm-6"><%# Eval("CountryTxt")%></div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-3 htCr">State&nbsp;<b>:</b></div>
                                                <div class="col-sm-6"><%# Eval("StateTxt")%></div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-3 htCr">Pincode&nbsp;<b>:</b></div>
                                                <div class="col-sm-6"><%# Eval("Pincode")%></div>
                                            </div>
                                        </div>

                                    </ItemTemplate>
                                    <EditItemTemplate></EditItemTemplate>
                                </asp:FormView>


                                <asp:Panel ID="pnlcntrls" runat="server" Visible="false">
                                    <h5>Update Company Details
                                    <hr />
                                    </h5>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-3 htCr Cntrlwidth">1. Company Name  :</div>
                                            <div class="col-sm-8">
                                                <asp:Label ID="lblCNAMe" runat="server"></asp:Label>
                                            </div>
                                        </div>

                                        <%--<div class="row">
                                            <div class="col-sm-3 htCr Cntrlwidth">2. Company Code :</div>
                                            <div class="col-sm-8">
                                                <asp:Label ID="lblCCode" runat="server"></asp:Label>
                                            </div>
                                        </div>--%>

                                        <div class="row">
                                            <div class="col-sm-3 htCr Cntrlwidth">2. Company Type :</div>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="DDL_Ctype" CssClass="txtDropDownwidth" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>



                                        <div class="row">
                                            <div class="col-sm-3 htCr Cntrlwidth">3. Official E-Mail :</div>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txt_Cemail" CssClass="txtDropDownwidth" runat="server" MaxLength="99" Text='<%# Eval("Company_MailID")%>'> </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="REQ_Cmail" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Csave" ControlToValidate="txt_Cemail" ForeColor="Red" ErrorMessage="Please Enter E-Mail ID">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="REL_Cemail" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Csave" ControlToValidate="txt_Cemail"
                                                    ErrorMessage="Please enter valid email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">  
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3 htCr Cntrlwidth">4. Official Contact No. :</div>
                                            <div class="col-sm-8">
                                                <asp:TextBox runat="server" CssClass="txtDropDownwidth" ID="txt_Ccontctno" MaxLength="12" Text='<%# Eval("Company_ContactNum")%>'></asp:TextBox>
                                                <Ajx:FilteredTextBoxExtender ID="Filteredtxt_Ccontctno" runat="server"
                                                    TargetControlID="txt_Ccontctno" FilterType="Numbers">
                                                </Ajx:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="REQ_Ccontact" runat="server" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txt_Ccontctno" ValidationGroup="Csave" ForeColor="Red" ErrorMessage="Please Enter Contact NO">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3 htCr Cntrlwidth">5. Company Address :</div>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txt_Caddress" CssClass="txtDropDownwidth" runat="server" TextMode="MultiLine" MaxLength="249" Text='<%# Eval("Company_Address")%>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="REQ_Cadress" runat="server" ValidationGroup="Csave" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txt_Caddress" ForeColor="Red" ErrorMessage="Please Enter Company Address">
                                                </asp:RequiredFieldValidator>
                                                <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxtxt_Caddress" runat="server" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers" TargetControlID="txt_Caddress" ValidChars=" .#/," FilterMode="ValidChars" />
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3 htCr Cntrlwidth">6. District :</div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtDist" CssClass="txtDropDownwidth" runat="server" MaxLength="39" Text='<%# Eval("District")%>'></asp:TextBox>
                                                <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxtxtDist" runat="server" FilterType="UppercaseLetters,LowercaseLetters" TargetControlID="txtDist" />
                                                <asp:RequiredFieldValidator ID="RFV_txtDist" runat="server" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtDist" ValidationGroup="Csave" ForeColor="Red" ErrorMessage="Please Enter District">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <asp:HiddenField ID="stst" runat="server" />
                                        <div class="row">
                                            <div class="col-sm-3 htCr Cntrlwidth">7. Country :</div>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="DDL_Ccountry" OnSelectedIndexChanged="DDL_Ccountry_SelectedIndexChanged" AutoPostBack="true" CssClass="txtDropDownwidth" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3 htCr Cntrlwidth">8. State :</div>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="DDL_Cstate" CssClass="txtDropDownwidth" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3 htCr Cntrlwidth">9. Pincode :</div>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txt_Cpincode" CssClass="txtDropDownwidth" runat="server" MaxLength="10" Text='<%# Eval("Pincode")%>'></asp:TextBox>
                                                <Ajx:FilteredTextBoxExtender ID="FLT_Cpincode" runat="server" FilterType="Numbers" TargetControlID="txt_Cpincode" />
                                                <asp:RequiredFieldValidator ID="REQ_Cpincode" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Csave" ControlToValidate="txt_Cpincode" ForeColor="Red" ErrorMessage="Please Enter Pincode">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3 htCr Cntrlwidth">10. Logo :</div>
                                            <div class="col-sm-8">
                                                <asp:FileUpload ID="flLogo" runat="server"></asp:FileUpload>

                                                <asp:RegularExpressionValidator ID="regexValidator" runat="server"
                                                    ControlToValidate="flLogo"
                                                    ErrorMessage="Only PNG images are allowed"
                                                    ValidationExpression="(.*\.([Pp][Nn][Gg])|.*\.([Pp][Nn][Gg])$)" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Csave">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-6" style="margin-bottom: 10px">
                                            <asp:Button ID="btn_Csave" runat="server" Text="Update" ValidationGroup="Csave" CommandName="UPDATE" OnClick="btn_Csave_Click" />
                                            <asp:Button ID="btn_Ccancel" runat="server" Text="Cancel" CausesValidation="false" CommandName="CANCEL" OnClick="btn_Ccancel_Click" />
                                        </div>
                                    </div>
                                   
                                </asp:Panel>
                            </div>


                            <div id="view2" runat="server" visible="false" style="width: 100%">
                                <br />
                                <div class="header-title">&nbsp;&nbsp;Generate/Lock Users</div>
                                <hr class="HrCls" />
                                <br />
                                <div class="row">
                                    <div class="col-sm-8">
                                    <asp:DropDownList ID="DDL_srchemp_genlogby" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_srchemp_genlogby_SelectedIndexChanged">
                                         <asp:ListItem Value="0">- Search -</asp:ListItem>
                                        <asp:ListItem Value="1">Employee</asp:ListItem>
                                        <asp:ListItem Value="2">Employee Mail ID</asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:DropDownList ID="DDL_empsrchgenlog" runat="server" Visible="false" OnSelectedIndexChanged="DDL_empsrchgenlog_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:Button ID="btn_srchlog_reset" runat="server" Text="Reset" Visible="false" OnClick="btn_srchlog_reset_Click"/>
                                    </div>
                                    <div class="col-md-4 text-right" id="divgencnt" runat="server"></div>
                                </div>
                                <br />
                                <asp:GridView ID="gv_users" runat="server" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnPageIndexChanging="gv_users_PageIndexChanging"
                                    DataKeyNames="EMPID,PASSWORD,Company_MailID,Created_By,NAME" OnRowDataBound="gv_users_RowDataBound" OnRowCommand="gv_users_RowCommand ">
                                    <Columns>
                                         <asp:TemplateField HeaderText="Sl NO.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        <asp:BoundField HeaderText="Employee ID" />
                                        <asp:BoundField DataField="NAME" HeaderText="Employee Name" />
                                        <asp:BoundField HeaderText="Employee DOB" />
                                        <asp:BoundField DataField="Company_MailID" HeaderText="Employee Mail ID" />
                                        <asp:BoundField DataField="Created_By" HeaderText="" Visible="false" />
                                         <%--<asp:TemplateField HeaderText="Separation Date" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_exitdt" runat="server"></asp:TextBox>
                                                <Ajx:CalendarExtender ID="CE_exitdt" runat="server" Enabled="True" Format="yyyy-MM-dd" TargetControlID="txt_exitdt" PopupButtonID="txt_exitdt">
                                            </Ajx:CalendarExtender>
                                          
                                            <Ajx:FilteredTextBoxExtender ID="FLT_exitdt" runat="server" FilterType="Custom,Numbers" ValidChars="-" FilterMode="ValidChars" TargetControlID="txt_exitdt"></Ajx:FilteredTextBoxExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <div class="row inline" style="padding-left:16px">
                                                    <div style="padding-left: 0px; padding-top: 5px; padding-bottom: 0;" onclick="return confirm('Are you sure do you want to Generate login...?');">   
                                                <asp:LinkButton ID="lnkGenertelogin" runat="server" ToolTip="Generate Login" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="Generate"><i class="fe-user-plus"></i></asp:LinkButton>                                          
                                                        </div>
                                                   <div style="padding-left: 0px; padding-top: 5px; padding-bottom: 0;" onclick="return confirm('Are you sure do you want to Separate employee...?');">   
                                                <asp:LinkButton ID="lnkLocklogin" ToolTip="Employee Separation" runat="server" ValidationGroup="exit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="Lock"><i class="fe-user-minus"></i></asp:LinkButton>                                          
                                                       </div>
                                           &nbsp;&nbsp;&nbsp;
                                                     <div style="padding-left: 0px; padding-top: 5px; padding-bottom: 0;" onclick="return confirm('Are you sure do you want to Unlock...?');">
                                                    <asp:LinkButton ID="lnkunlock" runat="server"  ToolTip="Unlock Employee"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="TEMPUNLOCK"><i class="fe-check-circle"></i></asp:LinkButton>                                          
                                                </div>
                                                <div style="padding-left: 0px; padding-top: 5px; padding-bottom: 0;" onclick="return confirm('Are you sure do you want to temporarily Lock...?');">
                                                    <asp:LinkButton ID="lnktemplock" runat="server" ToolTip="Lock Employee"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CommandName="TEMPLOCK"><i class="fe-x-circle"></i></asp:LinkButton>                                          
                                                </div>
                                                    </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server" Text='<%#Eval("EMPID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_DOB" runat="server" Text='<%# Eval("PASSWORD")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                     

                                    </Columns>
                                </asp:GridView>
                                <br />
                                

                                <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
                                    PopupControlID="dvPopup" TargetControlID="btnpopupcomp" BackgroundCssClass="popUpStyle" CancelControlID="lnkbtnClose">
                                </cc1:ModalPopupExtender>

                                <button style="display: none;" id="btnpopupcomp" runat="server"></button>
                                <div ID="dvPopup" runat="server" class="modalPopupDefault" align="center">
                                    <div class="modal-dialog modal-lg">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5>Employee Separation Process</h5>
                                            <asp:LinkButton ID="lnkbtnClose" runat="server" Text="X" CssClass="close" data-dismiss="modal" aria-hidden="true" CausesValidation="false" />
                                        </div>
                                         <div class="modal-body">
                                             <div>
                                        <div class="form-group" style="width:100%">
                                            <div class="row col-md-12">
                                                <div class="col-md-5">Employee ID  :</div>
                                                <div class="col-md-7">
                                                    <asp:Label ID="lbl_eid" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row col-md-12">
                                                <div class="col-md-5">Employee Name :</div>
                                                <div class="col-md-7">
                                                    <asp:Label ID="lbl_ename" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-5">E-Mail ID :</div>
                                                <div class="col-md-7">
                                                    <asp:Label ID="lbl_email" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row col-md-12">
                                                <div class="col-md-5">Separation Date :</div>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txt_exitdate" runat="server" TextMode="Date" CausesValidation="true"></asp:TextBox>
                                                    <%--<Ajx:CalendarExtender ID="CE_calendr" TargetControlID="txt_exitdate" CssClass="ajax_calendar_container"
                                                        PopupButtonID="txt_exitdate" runat="server" ViewStateMode="Enabled" Format="yyyy-MM-dd"/>--%>
                                                    <asp:RequiredFieldValidator ID="REQ_exitdate" runat="server" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" InitialValue="dd/mm/yyyy" ControlToValidate="txt_exitdate" ValidationGroup="exit"></asp:RequiredFieldValidator>
                                                    <Ajx:FilteredTextBoxExtender ID="FLTR_exit" runat="server"  TargetControlID="txt_exitdate" ValidChars="/" FilterType="Custom,Numbers"></Ajx:FilteredTextBoxExtender>
                                               
                                                   
                                                     </div>
                                            </div>
                                       
                                        <div class="row">
                                            <div class="col-sm-6">
                                            <asp:Button ID="btn_exitemp" runat="server" ValidationGroup="exit" Text="Exit" Width="60px" OnClick="btn_exitemp_Click"/>
                                            <asp:Button ID="btnHide" runat="server" Text="Cancel" Width="60px" />
                                                </div>
                                        </div>
                                             </div>

                                                 <br/>
                                                 <asp:Label ID="lblmssg" runat="server" style="color:red"></asp:Label>

                                                 </div>
                                             </div>

                                    </div>
                                        </div>
                                </div>
                            </div>

                            <div id="view3" runat="server" visible="false" style="width: 100%">
                                <br />
                                <div class="header-title">&nbsp;&nbsp;Employee Designation/Manager Mapping</div>
                                <hr class="HrCls" />
                                <br /> 
                                 <div class="row">
                                    <div class="col-sm-6">
                                    <asp:DropDownList ID="DDL_configsrch" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_configsrch_SelectedIndexChanged">
                                         <asp:ListItem Value="0">- Search -</asp:ListItem>
                                        <asp:ListItem Value="1">Employee</asp:ListItem>
                                        <asp:ListItem Value="2">Designation</asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:DropDownList ID="DDL_srchtype" runat="server" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="DDL_srchtype_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:Button ID="btn_srchdesig_reset" runat="server" Text="Reset" Visible="false" OnClick="btn_srchdesig_reset_Click" />
                                    </div>
                                 <div class="col-sm-6" style="text-align: right;width:100%; padding-right: 2px">
                                    <asp:TextBox ID="txtNewDesig" placeholder="Add New Designation" runat="server" CssClass="txtDropDownwidth"></asp:TextBox> &nbsp;&nbsp;
                                    <asp:RequiredFieldValidator ID="RQF_newdesid" runat="server" ErrorMessage="*" ControlToValidate="txtNewDesig" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="addnewdesig"></asp:RequiredFieldValidator>
                                    <asp:Button ID="btnNewDesig" runat="server" Text="Add" OnClick="btnNewDesig_Click" ValidationGroup="addnewdesig"/>
                                </div>
                                </div>
                                
                                <br /> 
                                 <div class="row">
                                    <div class="col-sm-6">
                                        </div>
                                 <div class="col-sm-6" style="text-align: right;padding-right: 5px" id="divmapcnt" runat="server"></div> 
                                           </div>                  
                                <asp:GridView ID="GV_desmgrconfig" runat="server" DataKeyNames="EMPID,NAME,desigTEXT,deptid,DESCRIPTION,CRYFWRD"  AllowPaging="true" PageSize="10"
                                    OnRowCommand="GV_desmgrconfig_RowCommand" OnRowDataBound="GV_desmgrconfig_RowDataBound" OnPageIndexChanging="GV_desmgrconfig_PageIndexChanging">
                                    <Columns>
                                         <asp:TemplateField HeaderText="Sl NO.">
                                                <ItemTemplate>
                                                     <asp:Label ID="lblmpRowNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                     <asp:BoundField HeaderText="Employee ID" DataField="EMPID" />
                                     <asp:BoundField HeaderText="Employee Name" DataField="NAME" />

                                    <asp:TemplateField HeaderText="Employee Designation">  
                                    <ItemTemplate>
                                    <asp:Label ID="lbl_emdesig" runat="server" Text='<%# Bind("desigTEXT") %>'></asp:Label>
                                        
                                     <asp:DropDownList ID="DDL_designtn" runat="server" OnSelectedIndexChanged="DDL_designtn_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>    
                                    </ItemTemplate>  
                                    </asp:TemplateField>
                                      <asp:BoundField HeaderText="Manager ID" DataField="deptid" />
                                     <asp:TemplateField HeaderText="Manager Name">  
                                      <ItemTemplate>  
                                    <asp:Label ID="lbl_mgrname" runat="server" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>  
                                    
                                    <asp:DropDownList ID="DDL_mgrname" runat="server"></asp:DropDownList>
                                    </ItemTemplate>  
                                    </asp:TemplateField>  

                                      <asp:TemplateField HeaderText="Action">  
                                      <ItemTemplate>
                                          <asp:LinkButton ID="LK_vew" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                            CommandName="view" ToolTip="View"><i class="fe-eye"></i></asp:LinkButton>

                                           
                                
                                          &nbsp;&nbsp;&nbsp;
                                          <asp:LinkButton ID="LK_edt" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                           CommandName="editcn" ToolTip="Edit"><i class="fe-edit-1"></i></asp:LinkButton>

                                          <asp:LinkButton ID="LK_upd" runat="server" ToolTip="Update" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                          CommandName="updcon"><i class="fe-arrow-up-circle"></i></asp:LinkButton>

                                          <asp:LinkButton ID="LK_cncl" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                          CommandName="canconf" ToolTip="Cancel"><i class="fe-x-circle"></i></asp:LinkButton>
                                       </ItemTemplate>  
                                    </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <br />

                                <asp:UpdatePanel ID="d" runat="server">
                                  <ContentTemplate>
                                      <asp:Button ID="btnexportdesig" runat="server" Text="Export to Excel" Visible="false" OnClick="btnexport_Click" />                                   
                                    </ContentTemplate>
                                     <Triggers>
                                      <asp:PostBackTrigger ControlID="btnexportdesig" />
                                       </Triggers>
                                      </asp:UpdatePanel>

                                <cc1:ModalPopupExtender BackgroundCssClass="popUpStyle" ID="modal1" 
                                    runat="server" PopupControlID="divpopupcomp" TargetControlID="Button1" CancelControlID="LK_closeComp">
                                </cc1:ModalPopupExtender>
                                          <button style="display: none;" id="Button1" runat="server"></button>

                                <div id="divpopupcomp" runat="server" class="modalPopupDefault" align="center">
                                    <div class="modal-dialog modal-lg">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h4 class="modal-title" id="H2">Employee Designation and Manager Details</h4>

                                                <asp:LinkButton ID="LK_closeComp" class="close" data-dismiss="modal" aria-hidden="true" runat="server" Text="X" />
                                            </div>
                                            <div class="modal-body">
                                                <asp:Panel ID="pnl_empdesidmgr" runat="server">
                                                 <h4>Employee Designation Details </h4>                                                
                                                <asp:GridView ID="GV_empdesigdetails" runat="server" DataKeyNames="ID,CRYFWRD" OnRowDataBound="GV_empdesigdetails_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl NO.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                        <asp:BoundField DataField="EMPID" HeaderText="Employee ID" />
                                                        <asp:BoundField DataField="DESCRIPTION" HeaderText="Employee Name" />
                                                        <asp:BoundField DataField="desigTEXT" HeaderText="Designation" />
                                                        <asp:BoundField DataField="Date" HeaderText="Valid From" DataFormatString= "{0:dd/MM/yyyy}"/>
                                                        <asp:BoundField DataField="enddate" HeaderText="Valid To" DataFormatString= "{0:dd/MM/yyyy}"/>
                                                        <asp:BoundField DataField="Created_On" HeaderText="Updated On" DataFormatString= "{0:dd/MM/yyyy}"/>
                                                    </Columns>
                                                </asp:GridView>

                                                <br />
                                                <h4>Employee - Manager Details </h4>
                                                <asp:GridView ID="GV_empmgrdetails" runat="server" DataKeyNames="ID" OnRowDataBound="GV_empmgrdetails_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl NO.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                        <asp:BoundField DataField="EMPID" HeaderText="Employee ID" />
                                                        <asp:BoundField DataField="DESCRIPTION" HeaderText="Employee Name" />
                                                        <asp:BoundField DataField="desigTEXT" HeaderText="Manager ID" />
                                                        <asp:BoundField DataField="deptid" HeaderText="Manager Name" />
                                                        <asp:BoundField DataField="Date" HeaderText="Valid From" DataFormatString= "{0:dd/MM/yyyy}"/>
                                                        <asp:BoundField DataField="enddate" HeaderText="Valid To" DataFormatString= "{0:dd/MM/yyyy}"/>
                                                         <asp:BoundField DataField="Created_On" HeaderText="Updated On" DataFormatString= "{0:dd/MM/yyyy}"/>
                                                    </Columns>
                                                </asp:GridView>
                                                    </asp:Panel>
                                                <br />

                                                <asp:UpdatePanel ID="b" runat="server">
                                                <ContentTemplate>
                                                    <asp:Button ID="btn_expallmgrdesg" runat="server" Text="Export to Excel" OnClick="btn_expallmgrdesg_Click" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btn_expallmgrdesg" />
                                                </Triggers>
                                            </asp:UpdatePanel>


                                            </div>
                                        </div>
                                    </div>
                                </div>

                                </div>

                            <div id="view7" runat="server" visible="false" style="width: 100%">
                                <br />
                                <div class="header-title">&nbsp;&nbsp;Map Designation to Employee's</div>
                                <hr class="HrCls" />
                                <br />

                                <asp:GridView ID="GV_exprtdesg" runat="server" Visible="false" OnRowDataBound="GV_exprtdesg_RowDataBound">
                                    <Columns>
                                         <asp:TemplateField HeaderText="Sl NO.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                     <asp:BoundField HeaderText="Employee ID" DataField="EMPID" />
                                     <asp:BoundField HeaderText="Employee Name" DataField="NAME" />
                                        <asp:BoundField HeaderText="Employee Designation" DataField="desigTEXT" />
                                        <asp:BoundField HeaderText="Manager ID" DataField="deptid" />
                                        <asp:BoundField HeaderText="Manager Name" DataField="DESCRIPTION" />                                        
                                    </Columns>
                                </asp:GridView>

                               
                                <br />
                                <div class="row">
                                    <div class="col-sm-12"><span style="color:Red">*</span>Select Employee :&nbsp;&nbsp;&nbsp;
                                        <asp:DropDownList ID="ddldesigEmployee" runat="server" CssClass="txtDropDownwidth"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="REQ_ddlempdesig" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddldesigEmployee" InitialValue="0" ValidationGroup="Designation" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        &nbsp;&nbsp;&nbsp;
                                      <span style="color:Red">*</span>Select Designation  :&nbsp;&nbsp;&nbsp;
                                        <asp:DropDownList ID="ddldesig" runat="server" CssClass="txtDropDownwidth"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="REQ_ddldesig" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddldesig" InitialValue="0" ValidationGroup="Designation" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <Ajx:ListSearchExtender ID="DDL_srchdesig" runat="server" Enabled="True" PromptPosition="Top" PromptText="Search Designation" TargetControlID="ddldesig"> </Ajx:ListSearchExtender>  
                            
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnDesig" runat="server" Text="Map" OnClick="btnDesig_Click" ValidationGroup="Designation" />
                                    </div>
                                </div>
                                <div class="">
                                    <div class="">
                                        <br />
                                        <asp:GridView ID="gv_desigmapp" runat="server" AutoGenerateColumns="false" PageSize="5" Width="100%">
                                            <Columns>
                                                <asp:BoundField HeaderText="Employee ID" DataField="Employee_ID" Visible="false" />
                                                <asp:BoundField HeaderText="Employee Name" DataField="Employee_NAME" />
                                                <asp:BoundField HeaderText="Manager ID" DataField="MGR_ID" Visible="false" />
                                                <asp:BoundField HeaderText="Manager Name" DataField="MGR_NAME" />
                                            </Columns>
                                        </asp:GridView>
                                        <br />
                                        <div>
                                            <asp:Button ID="btnDesigSubmt" Visible="false" runat="server" Text="Submit" OnClick="btnDesigSubmt_Click" />

                                            <asp:GridView ID="gv_desing" runat="server" AutoGenerateColumns="false" Width="100%" Visible="false">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Designation ID" DataField="desig" Visible="false" />
                                                    <asp:BoundField HeaderText="Designation" DataField="desigTEXT" />
                                                </Columns>
                                            </asp:GridView>


                                            <h5>Current Designation Mapping</h5>
                                            <asp:GridView ID="gvCrtdesMappDb" runat="server" AutoGenerateColumns="false" PageSize="20" AllowPaging="true" OnPageIndexChanging="gvCrtdesMappDb_PageIndexChanging" Width="100%" OnRowCommand="gvCrtdesMappDb_RowCommand" DataKeyNames="ID,desig" OnRowDeleting="gvCrtdesMappDb_RowDeleting" OnRowDataBound="gvCrtdesMappDb_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Employee ID" DataField="EMPID" />
                                                    <asp:BoundField HeaderText="Employee Name" DataField="NAME" />
                                                    <%--<asp:BoundField HeaderText="Designation ID" DataField="desig" />--%>
                                                    <asp:BoundField HeaderText="Designation Name" DataField="desigTEXT" />
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkGenertelogin" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                CausesValidation="false" CommandName="Delete"><i class="fe-trash-2"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                            <br />
                                            
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div id="view4" runat="server" visible="false" style="width: 100%">
                                <br />
                                <div class="header-title">&nbsp;&nbsp;Employee - Manager Mapping</div>
                                <hr class="HrCls" />
                                <br />
                                <div class="row">
                                    <div class="col-sm-12"><span style="color:Red">*</span>Select Employee :&nbsp;&nbsp;
                                        <asp:DropDownList ID="ddlMEmployee" runat="server" CssClass="txtDropDownwidth"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="REQ_ddlemp" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlMEmployee" InitialValue="0" Display="Dynamic" ValidationGroup="Mgrmap" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    &nbsp;&nbsp;
                                     <span style="color:Red">*</span>Select Manager :&nbsp;&nbsp;
                                        <asp:DropDownList ID="ddlMManager" runat="server" CssClass="txtDropDownwidth"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="REQ_ddlmanager" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlMManager" InitialValue="0" Display="Dynamic" ValidationGroup="Mgrmap" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnAdd" runat="server" Text="Map" OnClick="btnAdd_Click" ValidationGroup="Mgrmap" />
                                    </div>
                                </div>
                                <div class="">
                                    <div class="">
                                        <br />
                                        <asp:GridView ID="GV_empmgr" runat="server" AutoGenerateColumns="false" PageSize="5" Width="100%">
                                            <Columns>
                                                <asp:BoundField HeaderText="Employee ID" DataField="Employee_ID" Visible="false" />
                                                <asp:BoundField HeaderText="Employee Name" DataField="Employee_NAME" />
                                                <asp:BoundField HeaderText="Manager ID" DataField="MGR_ID" Visible="false" />
                                                <asp:BoundField HeaderText="Manager Name" DataField="MGR_NAME" />
                                            </Columns>
                                        </asp:GridView>
                                        <br />
                                        <div>
                                            <asp:Button ID="btnSubmit" Visible="false" runat="server" Text="Submit" OnClick="btnSubmit_Click" />

                                            <h5>Current Employee - Manager Mapping</h5>
                                            <asp:GridView ID="gvmgrmappingdb" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" Width="100%" OnPageIndexChanging="gvmgrmappingdb_PageIndexChanging" OnRowCommand="gvmgrmappingdb_RowCommand" DataKeyNames="ID" OnRowDeleting="gvmgrmappingdb_RowDeleting" OnRowDataBound="gvmgrmappingdb_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Employee ID" DataField="EMPID" />
                                                    <asp:BoundField HeaderText="Employee Name" DataField="NAME" />
                                                    <asp:BoundField HeaderText="Manager ID" DataField="AppID" />
                                                    <asp:BoundField HeaderText="Manager Name" DataField="USER_NAME" />
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkGenertelogin" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false"
                                                                CommandName="Delete"><i class="fe-trash-2"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <br />
                                            <asp:UpdatePanel ID="a" runat="server">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnexprt_mgrmap" runat="server" Text="Export to Excel" OnClick="btnexprt_mgrmap_Click" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnexprt_mgrmap" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>



                            <div id="view5" runat="server" visible="false" style="width: 100%">
                                <br />
                                <div class="header-title">&nbsp;&nbsp;Leave Quota's Config</div>
                                <hr class="HrCls" />
                                <br />
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-sm-12">1. Select Leave Type  :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:DropDownList ID="ddlLeaveType" runat="server" CssClass="txtDropDownwidth" OnSelectedIndexChanged="ddlLeaveType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RDF" InitialValue="0" ControlToValidate="ddlLeaveType" ValidationGroup="abc" ErrorMessage="*" runat="server" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div id="generalleaves" runat="server">
                                    <div class="row">
                                        <div class="col-sm-12">2. Duration To Generate :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:TextBox ID="txtfrmdate" CssClass="txtDropDownwidth" runat="server" Placeholder="From Date"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CE_Frmdate_ADmi" PopupButtonID="txtfrmdate" runat="server" TargetControlID="txtfrmdate" Format="yyyy-MM-dd" />
                                            <asp:RequiredFieldValidator ID="REQ_From_attdc" runat="server" Width="10%" ErrorMessage="*" ValidationGroup="abc" ForeColor="Red" ControlToValidate="txtfrmdate" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txttodate" CssClass="txtDropDownwidth" runat="server" Placeholder="To Date"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CE_Todate_Adm" PopupButtonID="txttodate" runat="server" TargetControlID="txttodate" Format="yyyy-MM-dd" />
                                            <asp:RequiredFieldValidator ID="REQ_Todate_attdce" Width="10%" runat="server" ErrorMessage="*" ValidationGroup="abc" ForeColor="Red" ControlToValidate="txttodate" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cpre_attancedate_valid" ValidationGroup="Date" Font-Size="Medium" ForeColor="Red" runat="server" ControlToValidate="txtfrmdate" ControlToCompare="txttodate" Operator="LessThan" Type="Date" ErrorMessage="From date must be less than To date." Display="Dynamic" SetFocusOnError="true"></asp:CompareValidator>

                                            <Ajx:FilteredTextBoxExtender ID="FLT_frmdate" runat="server" FilterType="Custom,Numbers" ValidChars="-" TargetControlID="txtfrmdate"></Ajx:FilteredTextBoxExtender>
                                            <Ajx:FilteredTextBoxExtender ID="FLT_todate" runat="server" FilterType="Custom,Numbers" ValidChars="-" TargetControlID="txttodate"></Ajx:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">3. No. of Leave to generate  :&nbsp;&nbsp;
                                            <asp:TextBox ID="txtNoleave" runat="server" CssClass="txtDropDownwidth"></asp:TextBox>
                                            <Ajx:FilteredTextBoxExtender ID="FLTR_Noleave" runat="server" FilterType="Custom,Numbers" TargetControlID="txtNoleave" ValidChars="." FilterMode="ValidChars"></Ajx:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="" ControlToValidate="txtNoleave" ValidationGroup="abc" ErrorMessage="*" runat="server" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <asp:CheckBox ID="chkLQCarryfrwd" Text="&nbsp;&nbsp;Leaves Carry Forward to Next Year?" runat="server" />
                                        </div>
                                    </div>
                                        </div>
                                    <div id="compoff" runat="server" visible="false">
                                         <div class="row">
                                        <div class="col-sm-12">2. No. of days  :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:TextBox ID="txtcompoffdays" runat="server" CssClass="txtDropDownwidth"></asp:TextBox>
                                            <Ajx:FilteredTextBoxExtender ID="FLT_compoffdays" runat="server" FilterType="Numbers" TargetControlID="txtNoleave" FilterMode="ValidChars"></Ajx:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="RFV_compoff" InitialValue="" ControlToValidate="txtcompoffdays" ValidationGroup="abc" ErrorMessage="*" runat="server" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>                                            
                                        </div>
                                    </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:Button ID="btnAddLeaveQuota" runat="server" Text="Add" OnClick="btnAddLeaveQuota_Click" ValidationGroup="abc" />
                                        </div>
                                    </div>

                                </div>
                                <br />
                                <asp:GridView ID="gvleavQuota" runat="server" AutoGenerateColumns="false" PageSize="5">
                                    <Columns>
                                        <asp:BoundField HeaderText="Leave Type Id" DataField="LeaveType" />
                                        <asp:BoundField HeaderText="Leave Type" DataField="LeaveTypetxt" />
                                        <asp:BoundField HeaderText="From Date" DataField="Leavefrom" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField HeaderText="To Date" DataField="Leaveto" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField HeaderText="Total Days" DataField="period" />
                                        <asp:BoundField HeaderText="No of Leaves" DataField="qu" />
                                        <asp:TemplateField HeaderText="Carry Forward">
                                            <ItemTemplate>
                                                <%#Eval("CRYFWRD").ToString()=="1"?"Yes":"No" %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <br />
                                <div>
                                    <asp:Button ID="btnUpdateQuota" Visible="false" runat="server" Text="Submit" OnClick="btnUpdateQuota_Click" />
                                </div>

                                <h5>Current Leave Mapping</h5>
                                <asp:GridView ID="gvLeavcomquota" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%"
                                    OnRowCommand="gvLeavcomquota_RowCommand" DataKeyNames="ID,id1,frmdate,todate,period" OnRowDeleting="gvLeavcomquota_RowDeleting">
                                    <Columns>
                                        <asp:BoundField HeaderText="Leave Type" DataField="leavTEXT" />
                                        <asp:BoundField HeaderText="Total Days" DataField="period" Visible="false" />
                                        <asp:BoundField HeaderText="Entitlement Leaves" DataField="qu" />
                                        <asp:BoundField HeaderText="Start Date" DataField="frmdate" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField HeaderText="End  Date" DataField="todate" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:TemplateField HeaderText="Carry Forward">
                                            <ItemTemplate>
                                                <%#Eval("CRYFWRD").ToString()=="1"?"Yes":"No" %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField HeaderText="Designation Name" DataField="desigTEXT" />--%>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkGenertelogin" runat="server" Text="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false"
                                                    CommandName="Delete"><i class="fe-trash-2"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <br />
                                <div>
                                    <asp:Button ID="btnQuotagenerate" Visible="false" runat="server" Text="Generate Quota" OnClick="btnQuotagenerate_Click" />
                                    <asp:Button ID="btnQuotaUpdate" Visible="false" runat="server" Text="Update Quota" OnClick="btnQuotaUpdate_Click" />
                                </div>
                                <div>
                                </div>
                                <hr />
                                <div>
                                    <h5>Check Employee Leave Quota's</h5>
                                    <div>
                                        <div class="row">
                                            <div class="col-sm-12">Select Employee :&nbsp;&nbsp;
                                                <asp:DropDownList ID="ddlEmpCheckQuota" runat="server" CssClass="txtDropDownwidth"></asp:DropDownList>
                                            &nbsp;&nbsp;
                                                <asp:TextBox ID="txtYearLeaveQuotachk" placeholder="Enter Year (YYYY)" MaxLength="4" runat="server" CssClass="txtDropDownwidth"></asp:TextBox>
                                                <Ajx:FilteredTextBoxExtender ID="fgh" TargetControlID="txtYearLeaveQuotachk" FilterType="Numbers" runat="server"></Ajx:FilteredTextBoxExtender>
                                                       &nbsp;&nbsp;                              
                                                        <asp:Button ID="btnSearchEmpLeavQota" runat="server" Text="Search" OnClick="btnSearchEmpLeavQota_Click"/>
                                                &nbsp;&nbsp;
                                                <asp:Button ID="btn_lvreset" runat="server" Text="Reset" OnClick="btn_lvreset_Click" CausesValidation="false" Visible="false" />
                                                    
                                               
                                        </div>
                                      </div>
                                    </div>
                                         <asp:UpdatePanel ID="c" runat="server">
                                                <ContentTemplate>
                                                     </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnSearchEmpLeavQota" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                    <br />
                                    <asp:GridView ID="GV_LeaveQuota" runat="server" AutoGenerateColumns="False" Width="100%" EmptyDataText="No Records Found..!" ShowHeaderWhenEmpty="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Slno">
                                                <EditItemTemplate>
                                                    <asp:Label ID="LblSlno1" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="LblSlno" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ATEXT" HeaderText="Type of leave" SortExpression="ATEXT" />
                                            <asp:BoundField DataField="LEAVE_QUOTA_START_DATE" DataFormatString="{0:dd-MMM-yyyy}"
                                                HeaderText="Deduction from" SortExpression="LEAVE_QUOTA_START_DATE"></asp:BoundField>
                                            <asp:BoundField DataField="LEAVE_QUOTA_END_DATE" DataFormatString="{0:dd-MMM-yyyy}"
                                                HeaderText="Deduction to" SortExpression="LEAVE_QUOTA_END_DATE"></asp:BoundField>
                                            <asp:BoundField DataField="ANZHL" HeaderText="Entitlement" SortExpression="ANZHL"></asp:BoundField>
                                            <asp:BoundField DataField="KVERB" HeaderText="Entitlement used" SortExpression="KVERB"></asp:BoundField>
                                            <asp:BoundField DataField="AVAILABLE_DAYS" HeaderText="Available days" SortExpression="AVAILABLE_DAYS"></asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>

                            <div id="view6" runat="server" visible="false" style="width: 100%">
                                <br />
                                <div class="header-title">&nbsp;&nbsp;Add Holiday Calender</div>
                                <hr class="HrCls" />
                                <br />
                                <div class="row">
                                    <div style="width: 950px; float: left; margin-left: 15px">
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-12"><span style="color:Red">*</span>Holiday Date  :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:TextBox ID="txtHolDate" runat="server" CssClass="txtDropDownwidth"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RFV_calend" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtHolDate" Display="Dynamic" SetFocusOnError="true" ValidationGroup="holical"></asp:RequiredFieldValidator>
                                                <Ajx:CalendarExtender ID="cE_txtHolDate" runat="server" Enabled="True" Format="yyyy-MM-dd"
                                                    TargetControlID="txtHolDate" PopupButtonID="txtHolDate">
                                                </Ajx:CalendarExtender>
                                                    <Ajx:FilteredTextBoxExtender ID="FLT_addstdt" runat="server" FilterType="Custom,Numbers" TargetControlID="txtHolDate" ValidChars=" -"></Ajx:FilteredTextBoxExtender>
                                                    
                                                    </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12"><span style="color:Red">*</span>Enter Description  :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:TextBox ID="txtHolDescrip" runat="server" CssClass="txtDropDownwidth"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="REQ_hodydescptin" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtHolDescrip" Display="Dynamic" SetFocusOnError="true" ValidationGroup="holical"></asp:RequiredFieldValidator>
                                            </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <asp:CheckBox ID="CHK_holical" runat="server" Text="&nbsp;&nbsp;Restricted Holiday" />
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnAddtempholi" runat="server" Text="Add" OnClick="btnAddtempholi_Click" ValidationGroup="holical" />
                                        </div>

                                        <br />
                                        <div>
                                            <asp:GridView ID="gvHolidayCaln" runat="server" AutoGenerateColumns="false" PageSize="20">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Year" DataField="Year" ItemStyle-Width="120px" />
                                                    <asp:BoundField HeaderText="Holiday Date" DataField="Date" ItemStyle-Width="120px" />
                                                    <asp:BoundField HeaderText="Holiday Description" DataField="Des" ItemStyle-Width="160px" />
                                                    <asp:TemplateField HeaderText="Leave Type">
                                                        <ItemTemplate>
                                                            <%#Eval("type").ToString()=="1"?"Restricted Holiday":"General Holiday" %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="120px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Manager Name" DataField="type" Visible="false" />
                                                </Columns>
                                            </asp:GridView>
                                            <br />

                                            <asp:Button ID="btnUpdateHoliday" Visible="false" runat="server" Text="Submit" OnClick="btnUpdateHoliday_Click" /><br>
                                            <br />
                                             <div class="col-md-12 text-right" id="divholicnt" runat="server"></div>
                                            <asp:GridView ID="Grd_HolidayCalendar" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" OnRowCommand="Grd_HolidayCalendar_RowCommand" OnRowDeleting="Grd_HolidayCalendar_RowDeleting" Width="100%">
                                                <Columns>
                                                     <asp:TemplateField HeaderText="Sl No.">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblholiRowNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                            </ItemTemplate>
                                                            </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Holiday Date" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                                                        <ItemTemplate>
                                                            <%# Eval("Date","{0:dd-MM-yyyy}") %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="160px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Day" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                                                        <ItemTemplate>
                                                            <%# Eval("Date","{0:dddd}") %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="160px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Holiday Description">
                                                        <ItemTemplate>
                                                            <%# Eval("HR_DESCRIPTION") %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="200px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Type">
                                                        <ItemTemplate>
                                                            <%# Eval("H_type").ToString() == "1" ? "Restricted Holiday" : "General Holiday" %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="150px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkGenertelogin" runat="server" Text="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false"
                                                                CommandName="Delete"><i class="fe-trash-2"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="80px" />
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                    </div>
                                    <div class="col-md-2" style="border-left: 1px dashed black; float: right">
                                        <h5>Select Week-Off</h5>
                                        <asp:CheckBoxList ID="chkWeekends" runat="server">
                                            <asp:ListItem Value="1" Text="&nbsp;&nbsp;Sunday"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="&nbsp;&nbsp;Monday"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="&nbsp;&nbsp;Tuesday"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="&nbsp;&nbsp;Wednesday"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="&nbsp;&nbsp;Thursday"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="&nbsp;&nbsp;Friday"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="&nbsp;&nbsp;Saturday"></asp:ListItem>
                                        </asp:CheckBoxList>
                                        <div>
                                            <asp:Button ID="btnWeekends" runat="server" Text="Update" OnClick="btnWeekends_Click" />
                                        </div>
                                    </div>
                                </div>

                            </div>


                        </div>

                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
