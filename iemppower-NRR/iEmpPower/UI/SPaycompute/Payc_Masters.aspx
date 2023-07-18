<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="Payc_Masters.aspx.cs" 
    Inherits="iEmpPower.UI.SPaycompute.Payc_Masters" UICulture="auto" Theme="SkinFile" EnableEventValidation="false"  MaintainScrollPositionOnPostback="true"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>
<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls" TagPrefix="BDP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link rel="stylesheet" href="https://bootswatch.com/slate/bootstrap.min.css">
    <link href="../../ProgressBar/plugin.css" rel="stylesheet" />
    <link href="http://www.jqueryscript.net/css/jquerysctipttop.css" rel="stylesheet" type="text/css">
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
         .gridviewNew td a {
            font-size: 16px;
            font-weight: 600;
        }

</style>

    <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Add Masters</li>
                    </ol>
                </div>
                <h4 class="page-title">Add Masters
                    <asp:Label ID="Label1" runat="server" CssClass="msgboard"></asp:Label>
                </h4>
            </div>
        </div>
    </div>

     <div class="header">
            <asp:Label ID="LblMsg" runat="server" CssClass="lblValidation"></asp:Label>
        </div>

    
    <div class=" card-box">
        <div id="real_time_chart" class="row">
                <div style="width: 99%; margin: 0 auto; padding: 5px 0 40px 0;">
                     <div class="col-sm-12"  style="width:100%">
                    <div  style="width:100%">
                        <ul class="nav nav-pills navtab-bg" >
                            <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Tab1" class="nav-link p-2" OnClick="Tab1_Click" CausesValidation="false"><i class="fe-dollar-sign" ></i>
                            Salary Component</asp:LinkButton></li>
                            <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Tab2" class="nav-link  p-2" OnClick="Tab2_Click" CausesValidation="false"><i class="fe-calendar"></i>
                            Time Sheet</asp:LinkButton></li>   
                             <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Tab3" class="nav-link  p-2" OnClick="Tab3_Click" CausesValidation="false"><i class="fe-calendar"></i>
                            PR</asp:LinkButton></li>                         
                   </ul>
                    <div class="tabcontents">
                         <div id="view1" runat="server" visible="false"  style="width:100%">
                             <br />
                          <div class="header-title">&nbsp;&nbsp;Salary Component Masters</div>
                                 <hr class="HrCls"/>
                            <br />
                             <div class="form-group">
                            <%-- <div class="row">
                    <div class="col-sm-2 htCr"><span style="color:white">*</span>  Existing Components &nbsp;</div>
                    <div class="col-sm-6">               
                     <asp:DropDownList ID="DDL_exixt_compo" CssClass="txtDropDownwidth" runat="server"></asp:DropDownList>
                     </div>
                      </div>--%>
                       
                     <div class="row">
                    <div class="col-sm-2 htCr"><span style="color:red">*</span>  Add New Component &nbsp;</div>
                    <div class="col-sm-10">                                    
                     <asp:TextBox ID="txt_nwcompo_mstrs" CssClass="txtDropDownwidth" runat="server" Placeholder="Add New Component"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="REQ_Ccode" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Csave" ControlToValidate="txt_nwcompo_mstrs" ForeColor="Red" ErrorMessage="Please Enter New Component">
                     </asp:RequiredFieldValidator>
                     <Ajx:FilteredTextBoxExtender ID="FLTR_nwcompo_mstrs" runat="server" FilterType="Numbers,UppercaseLetters,LowercaseLetters,Custom" FilterMode="ValidChars" ValidChars=". " TargetControlID="txt_nwcompo_mstrs"></Ajx:FilteredTextBoxExtender>
                    &nbsp;&nbsp;
                            <asp:Button ID="btn_save_masters_compo" runat="server" Text="Save" ValidationGroup="Csave" Width="55px" OnClick="btn_save_masters_compo_Click"/>
                            <asp:Button ID="btn_Ccancel" runat="server" Text="Cancel" OnClick="btn_Ccancel_Click" Width="55px" CausesValidation="false"/>
                        <asp:Button ID="btn_viewexistingcompo" runat="server" Text="View" OnClick="btn_viewexistingcompo_Click" Width="55px" CausesValidation="false"/>
                        <asp:Button ID="btn_reset" runat="server" Visible="false" Text="Reset" OnClick="btn_reset_Click" Width="55px" CausesValidation="false"/>
                             </div>
                        </div>
                                 <br />
                                 <div id="exis_compo" runat="server" visible="false">
                                  <h5>Current Salary Components</h5>
                                     <div class="col-md-12 text-right" id="divcnt" runat="server"></div>
                                 <asp:GridView ID="GV_existngcompo" runat="server" DataKeyNames="ID" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnPageIndexChanging="GV_existngcompo_PageIndexChanging">
                                     <Columns>
                                   <asp:TemplateField  HeaderText="Sl No.">
                                   <ItemTemplate>
                                     <asp:Label ID="lblRowNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:BoundField DataField="TXT" HeaderText="Salary Components" />
                                 <asp:BoundField DataField="begda" HeaderText="Created On"  DataFormatString="{0:dd/MM/yyyy}"/>
                                     </Columns>
                                 </asp:GridView>
                                     </div>
                                 </div>
                    <asp:UpdatePanel ID="UPD_slry_compomstr" runat="server">
                         <ContentTemplate>
                        </ContentTemplate>
                         <Triggers>
                                 <asp:PostBackTrigger ControlID="btn_save_masters_compo"/>
                                 <asp:PostBackTrigger  ControlID="btn_Ccancel"/>
                          </Triggers>
                        </asp:UpdatePanel>

                             <h5>Salary Component Mapping</h5>
                             <br />
                             <div class="form-group">
                             <div class="row">
                    <div class="col-sm-2 htCr"><span style="color:white">*</span>Select Component</div>
                    <div class="col-sm-6">               
                     <asp:DropDownList ID="DDL_slrymap" CssClass="txtDropDownwidth" runat="server"></asp:DropDownList>
                     </div>
                      </div>
                       
                    <br />

                     <div class="row">
                    <div class="col-md-2"><span style="color:white">*</span>Select Component Type</div>
                    <div class="col-md-10">                                    
                     <asp:RadioButtonList ID="rbnPFApp" runat="server" RepeatDirection="Horizontal">
                     <asp:ListItem Value="1" Selected="True">Allowance</asp:ListItem>
                      <asp:ListItem Value="0">Deduction</asp:ListItem>
                       </asp:RadioButtonList>
                     </div>
                     </div>
                        <div class="row">
                        <div class="col-sm-6"> 
                            <asp:Button ID="btn_tempmap" runat="server" Text="Add" OnClick="btn_tempmap_Click"/>                           
                            </div>
                            </div>
                                 </div>

                             <br />

                             <asp:GridView ID="Gv_tempmaping" runat="server" ShowHeaderWhenEmpty="false">
                                 <Columns>
                                     <asp:BoundField DataField="Compname" HeaderText="Salary Component" />
                                     <asp:BoundField DataField="Compotyp" HeaderText="Component Type" />
                                 </Columns>                                 
                             </asp:GridView>
                             <br />
                             <asp:Button ID="btn_cnfimmap" runat="server" Visible="false" Text="Confirm" Width="65" OnClick="btn_cnfimmap_Click"/>
                              <asp:Button ID="btn_mapcncl" runat="server" Visible="false" Text="Cancel" Width="65" OnClick="btn_mapcncl_Click"/>

                             
                             <h5>Current Salary Component Mapping</h5>
                             <div class="col-md-12 text-right" id="dvcntsalmap" runat="server"></div>
                             <asp:GridView ID="GV_slrymapg" runat="server" OnRowCommand="GV_slrymapg_RowCommand" DataKeyNames="ID,TXT,id5" 
                             AllowPaging="true" PageSize="10" OnPageIndexChanging="GV_slrymapg_PageIndexChanging" ShowHeaderWhenEmpty="false"> 
                                 <Columns>
                                     <asp:TemplateField HeaderText="Slno">
                                      <ItemTemplate>
                                         <asp:Label ID="lblsalmapRowNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                       </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:BoundField DataField="col4" HeaderText="Component" />

                                     <asp:TemplateField HeaderText="Component Type">
                                      <ItemTemplate>
                                          <asp:Label ID="lbl_mapedcompotyp" runat="server" Text='<%# Eval("col1") %>'></asp:Label>

                                          <asp:CheckBox ID="chk_compotyp" runat="server" />
                                      </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:BoundField DataField="begda" HeaderText="Created On" DataFormatString="{0:dd/MM/yyyy}"/>
                                     <asp:BoundField DataField="begda1" HeaderText="Modified On" DataFormatString="{0:dd/MM/yyyy}"/>

                                     <asp:TemplateField HeaderText="Component Type">
                                      <ItemTemplate>
                                          <asp:LinkButton ID="LK_edtsmap" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                            CommandName="editmp" ToolTip="Edit"><i class="fe-edit-1"></i></asp:LinkButton>

                                            <asp:LinkButton ID="LK_updtsmap" runat="server" ToolTip="Update" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                            CommandName="updatemp"><i class="fe-arrow-up-circle"></i></asp:LinkButton>

                                             <asp:LinkButton ID="LK_cnclsmap" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                             CommandName="cancelmp" ToolTip="Cancel"><i class="fe-x-circle"></i></asp:LinkButton>
                                           </ItemTemplate>
                                    </asp:TemplateField>
                                 </Columns>
                             </asp:GridView>
                            
                             </div>

                         <div id="info" runat="server" visible="false"  style="width:100%">

                              <br />
                            <div style="width: 99%; margin: 0 auto; padding: 5px 0 40px 0;">
                                 <ul class="nav nav-pills navtab-bg" >

                            <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Infotab1" class="nav-link p-2" OnClick="Infotab1_Click" CausesValidation="false"><i class="fe-codepen" ></i>
                            Projects</asp:LinkButton></li>
                            <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Infotab2" class="nav-link  p-2" OnClick="Infotab2_Click" CausesValidation="false"><i class="fe-grid"></i>
                            WBS</asp:LinkButton></li>  
                                       <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Infotab3" class="nav-link  p-2" OnClick="Infotab3_Click" CausesValidation="false"><i class="fe-cpu"></i>
                            Activity</asp:LinkButton></li> 
                                     <li class="nav-item font-12">
                                     <asp:LinkButton runat="server" ID="Infotab4" class="nav-link  p-2" OnClick="Infotab4_Click" CausesValidation="false"><i class="fe-briefcase"></i>
                            Attendance Type</asp:LinkButton></li> 
                                                                
                   </ul>
                    <div class="tabcontents">

                        <div id="view2" runat="server" visible="false"  style="width:100%">
                            <br />
                          <div class="header-title">&nbsp;&nbsp;Create Project</div>
                            <hr class="HrCls"/>
                            <br />
                                
                             <div class="form-group">
                    <div class="row">
                    <div class="col-sm-2 htCr"><span style="color:white">*</span>1. Project ID</div>
                    <div class="col-sm-6">
                    <asp:TextBox ID="txt_projextnID" CssClass="txtDropDownwidth" runat="server" Placeholder="Project ID" AutoComplete="off"></asp:TextBox>
                    </div>
                    </div>

                     <div class="row">
                    <div class="col-sm-2 htCr"><span style="color:red">*</span>2.Project Title</div>
                    <div class="col-sm-6">
                    <asp:TextBox ID="txt_proj_mstrs" CssClass="txtDropDownwidth" runat="server" AutoCompleteType="Disabled" Placeholder="Project Title" AutoComplete="off"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="REQ_Proj_mstrs" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Csave" ControlToValidate="txt_proj_mstrs" ForeColor="Red" ErrorMessage="Please Enter New Project">
                    </asp:RequiredFieldValidator>
                    </div>
                    </div>

                    
                    
                     <div class="row">
                     <div class="col-sm-2"><span style="color:red">*</span>3.Project Duration :</div>
                         <div class="col-sm-10">
                      <asp:TextBox ID="txtprjfrmdate" CssClass="txtDropDownwidth" runat="server" Placeholder="From Date" AutoComplete="off"></asp:TextBox>
                       <Ajx:CalendarExtender ID="CE_PrjFrmdate" PopupButtonID="txtprjfrmdate" runat="server" TargetControlID="txtprjfrmdate" Format="yyyy-MM-dd" />
                        <asp:RequiredFieldValidator ID="REQ_prjFrom" runat="server" Width="10%" ErrorMessage="*" ValidationGroup="Csave" ForeColor="Red" ControlToValidate="txtprjfrmdate" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                         <asp:TextBox ID="txt_prjtodate" CssClass="txtDropDownwidth" runat="server" Placeholder="To Date" AutoComplete="off"></asp:TextBox>
                          <Ajx:CalendarExtender ID="CE_PrjTodate" PopupButtonID="txt_prjtodate" runat="server" TargetControlID="txt_prjtodate" Format="yyyy-MM-dd" />
                          <asp:RequiredFieldValidator ID="REQ_PrjTodate" Width="10%" runat="server" ErrorMessage="*" ValidationGroup="Csave" ForeColor="Red" ControlToValidate="txt_prjtodate" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                           <asp:CompareValidator ID="cpre_prjdate_valid" ValidationGroup="Date" Font-Size="Medium" ForeColor="Red" runat="server" ControlToValidate="txtprjfrmdate" ControlToCompare="txt_prjtodate" Operator="LessThan" Type="Date" ErrorMessage="From date must be less than To date." Display="Dynamic" SetFocusOnError="true"></asp:CompareValidator>

                           
                           </div>
                                    </div>

                                
                    <div class="row">
                    <div class="col-sm-6">                   
                    <asp:Button ID="btn_addprojs" runat="server" Text="Add" Width="55px" ValidationGroup="Csave" OnClick="btn_addprojs_Click"/>                 
                    <asp:Button Id="btn_cancelsub" runat="server" Width="55px"  Text="Cancel" OnClick="btn_cancelsub_Click" CausesValidation="false"/>
                         </div>                                    
                    </div>
                    
                   
                                 </div>
                   <br />
                            <h5>Current Projects</h5>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:DropDownList ID="DDL_srchpjctby" runat="server" OnSelectedIndexChanged="DDL_srchpjctby_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0" Selected="True">- Search -</asp:ListItem>
                                        <asp:ListItem Value="1">Project</asp:ListItem>
                                        <asp:ListItem Value="2">Project Period</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;
                            <asp:DropDownList ID="DDL_srchprjcts" runat="server" Visible="false" OnSelectedIndexChanged="DDL_srchprjcts_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    &nbsp;&nbsp;
                                    <asp:TextBox ID="txt_srchprojbegda" runat="server" Visible="false" Placeholder="Project Start Date" AutoComplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="REQ_srchprojbegda" runat="server" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="srchpp"  ControlToValidate="txt_srchprojbegda" Display="Dynamic" ForeColor="red"></asp:RequiredFieldValidator>
                                    <Ajx:CalendarExtender ID="CE_srchprojbegda" runat="server" Enabled="True" Format="yyyy-MM-dd" TargetControlID="txt_srchprojbegda" PopupButtonID="txt_srchprojbegda">
                                     </Ajx:CalendarExtender>
                                    <Ajx:FilteredTextBoxExtender ID="FLT_srchpbegda" runat="server" FilterType="Custom,Numbers" ValidChars="-" FilterMode="ValidChars" TargetControlID="txt_srchprojbegda"></Ajx:FilteredTextBoxExtender>
                                     &nbsp;&nbsp;
                                    <asp:TextBox ID="txt_srchprojendda" runat="server" Visible="false" Placeholder="Project End Date" AutoComplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="REQ_srchprjtodt" runat="server" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="srchpp" ControlToValidate="txt_srchprojendda" Display="Dynamic" ForeColor="red"></asp:RequiredFieldValidator>
                                    <Ajx:CalendarExtender ID="CE_srchprojendda" runat="server" Enabled="True" Format="yyyy-MM-dd" TargetControlID="txt_srchprojendda" PopupButtonID="txt_srchprojendda">
                                     </Ajx:CalendarExtender>
                                    <Ajx:FilteredTextBoxExtender ID="TLT_srchprjendda" runat="server" FilterType="Custom,Numbers" ValidChars="-" FilterMode="ValidChars" TargetControlID="txt_srchprojendda"></Ajx:FilteredTextBoxExtender>
                                     &nbsp;&nbsp;
                                    <asp:Button ID="btn_srchproj" runat="server" Text="Search" ValidationGroup="srchpp" Visible="false" OnClick="btn_srchproj_Click"/>
                                    <asp:Button ID="btn_srchprojreset" runat="server" Text="Reset" OnClick="btn_srchprojreset_Click" Visible="false" CausesValidation="false"/>
                                     &nbsp;&nbsp;
                                     <asp:CompareValidator ID="CPV_prsrch" ValidationGroup="Date" Font-Size="Medium" ForeColor="Red" runat="server" ControlToValidate="txt_srchprojbegda" ControlToCompare="txt_srchprojendda" Operator="LessThanEqual" Type="Date" ErrorMessage="From date must be less than To date." Display="Dynamic" SetFocusOnError="true"></asp:CompareValidator>

                                    </div>
                                </div>
                            <br />
                            <div class="col-md-12 text-right" id="dvdntproj" runat="server"></div>
                            <asp:GridView ID="GV_projects" runat="server" OnRowCommand="GV_projects_RowCommand" DataKeyNames="id1,col11" AllowPaging="true" PageSize="10" OnPageIndexChanging="GV_projects_PageIndexChanging">
                                <Columns>
                                     <asp:TemplateField HeaderText="Slno">
                                      <ItemTemplate>
                                        <asp:Label ID="lblprojRowNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                       </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Project ID">
                                      <ItemTemplate>
                                          <asp:Label ID="lbl_prjctidedtmod" runat="server" Text='<%# Eval("TXT") %>'></asp:Label>

                                          <asp:TextBox ID="txt_pjctidedtmod" runat="server" Text='<%# Eval("TXT") %>'></asp:TextBox>
                                          </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Project">
                                      <ItemTemplate>
                                          <asp:Label ID="lbl_projedtmod" runat="server" Text='<%# Eval("col11") %>'></asp:Label>

                                          <asp:TextBox ID="txt_prohedtmod" runat="server" Text='<%# Eval("col11") %>'></asp:TextBox>
                                           </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Valid From">
                                       <ItemTemplate>
                                           <asp:Label ID="lbl_pbegda" runat="server" Text='<%# Eval("begda", "{0:dd/MM/yyyy}") %>'></asp:Label>

                                           <asp:TextBox ID="txt_pbegda" runat="server" Text='<%# Eval("begda", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
                                           <Ajx:CalendarExtender ID="CE_pbegda" runat="server" Enabled="True" Format="yyyy-MM-dd" TargetControlID="txt_pbegda" PopupButtonID="txt_pbegda">
                                            </Ajx:CalendarExtender>
                                          
                                            <Ajx:FilteredTextBoxExtender ID="FLT_pbegda" runat="server" FilterType="Custom,Numbers" ValidChars="-" FilterMode="ValidChars" TargetControlID="txt_pbegda"></Ajx:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                       </asp:TemplateField>
                                    <asp:TemplateField HeaderText="End Date">
                                       <ItemTemplate>
                                           <asp:Label ID="lbl_pendda" runat="server" Text='<%# Eval("endda", "{0:dd/MM/yyyy}") %>'></asp:Label>

                                           <asp:TextBox ID="txt_pendda" runat="server" Text='<%# Eval("endda", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
                                            <Ajx:CalendarExtender ID="CE_pendda" runat="server" Enabled="True" Format="yyyy-MM-dd" TargetControlID="txt_pendda" PopupButtonID="txt_pendda">
                                            </Ajx:CalendarExtender>
                                            <Ajx:FilteredTextBoxExtender ID="FLT_pendda" runat="server" FilterType="Custom,Numbers" ValidChars="-" FilterMode="ValidChars" TargetControlID="txt_pendda"></Ajx:FilteredTextBoxExtender>
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                    <asp:BoundField DataField="begda1" HeaderText="Created On" DataFormatString="{0:dd/MM/yyyy}"/>
                                    <asp:BoundField DataField="endda1" HeaderText="Modified On" DataFormatString="{0:dd/MM/yyyy}"/>
                                     <asp:TemplateField HeaderText="Action">
                                       <ItemTemplate>
                                           <asp:LinkButton ID="LK_proedit" runat="server" ToolTip="Edit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                 CommandName="edt"> <i class="fe-edit-1"></i></asp:LinkButton>

                                           <asp:LinkButton ID="LK_proupdate" runat="server" ToolTip="Update" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" 
                                          CommandName="updte"><i class="fe-arrow-up-circle"></i></asp:LinkButton>

                                           <asp:LinkButton ID="LK_procncl" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                          CommandName="cancl" ToolTip="Cancel"><i class="fe-x-circle"></i></asp:LinkButton>
                                           
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <asp:UpdatePanel ID="UPD_wbs" runat="server">
                    <ContentTemplate>  </ContentTemplate>
                    <Triggers>
                    <asp:PostBackTrigger  ControlID="btn_addprojs"/>
                    <asp:PostBackTrigger ControlID="btn_cancelsub"/>
                    </Triggers>
                    </asp:UpdatePanel>
                            </div>

                        <div id="view3" runat="server" visible="false"  style="width:100%">
                            <br />
             <div class="header-title">&nbsp;&nbsp;Add WBS</div>
                 <hr class="HrCls"/>
                            <br />
             
                    <div class="form-group">
                     <div class="row">
                     <div class="col-sm-2 htCr"><span style="color:red">*</span>  1.Select Project</div>
                     <div class="col-sm-8">
                     <asp:DropDownList ID="DDL_pojct_addwbs" CssClass="txtDropDownwidth" runat="server" OnSelectedIndexChanged="DDL_pojct_addwbs_SelectedIndexChanged" AutoPostBack="true">
                     </asp:DropDownList>
                          <asp:RequiredFieldValidator ID="RQD_ddl_proj" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="addWBS" ControlToValidate="DDL_pojct_addwbs" ForeColor="Red"  ErrorMessage="*" InitialValue="0">
                     </asp:RequiredFieldValidator>
                     [Note : Select Project to add WBS]
                    
                     </div>
                     
                     </div>


                    <div class="row">
                    <div class="col-sm-2 htCr"><span style="color:white">*</span>2.WBS ID</div>
                    <div class="col-sm-6">
                    <asp:TextBox ID="txt_wbsid" CssClass="txtDropDownwidth" runat="server" Placeholder="WBS ID" AutoComplete="off"></asp:TextBox>
                    </div>
                    </div>

                     <div class="row">
                     <div class="col-sm-2 htCr"><span style="color:red">*</span> 3.WBS Title</div>
                     <div class="col-sm-6"> 
                     <asp:TextBox ID="txt_newWBSadd" CssClass="txtDropDownwidth" runat="server" Placeholder="WBS Title" AutoComplete="off"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RQD_addWBS" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="addWBS" ControlToValidate="txt_newWBSadd" ForeColor="Red" ErrorMessage="Please Enter New WBS for selected project">
                     </asp:RequiredFieldValidator>
                     </div>
                     </div>

                         <div class="row">
                     <div class="col-sm-2"><span style="color:red">*</span>4.WBS Duration :</div>
                         <div class="col-sm-10">
                      <asp:TextBox ID="txt_wbssrtdate" CssClass="txtDropDownwidth" runat="server" Placeholder="From Date" AutoComplete="off"></asp:TextBox>
                       <Ajx:CalendarExtender ID="CE_wbssrtdate" PopupButtonID="txt_wbssrtdate" runat="server" TargetControlID="txt_wbssrtdate" Format="yyyy-MM-dd" />
                        <asp:RequiredFieldValidator ID="RQF_wbssrtdate" runat="server" Width="10%" ErrorMessage="*" ValidationGroup="addWBS" ForeColor="Red" ControlToValidate="txt_wbssrtdate" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                         <asp:TextBox ID="txt_wbsenddate" CssClass="txtDropDownwidth" runat="server" Placeholder="To Date" AutoComplete="off"></asp:TextBox>
                          <Ajx:CalendarExtender ID="CE_wbsenddate" PopupButtonID="txt_wbsenddate" runat="server" TargetControlID="txt_wbsenddate" Format="yyyy-MM-dd" />
                          <asp:RequiredFieldValidator ID="RQF_wbsenddate" Width="10%" runat="server" ErrorMessage="*" ValidationGroup="addWBS" ForeColor="Red" ControlToValidate="txt_wbsenddate" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                             <asp:CompareValidator ID="CFV_wbsenddate" ValidationGroup="Date" Font-Size="Medium" ForeColor="Red" runat="server" ControlToValidate="txt_wbssrtdate" ControlToCompare="txt_wbsenddate" Operator="LessThan" Type="Date" ErrorMessage="From date must be less than To date." Display="Dynamic" SetFocusOnError="true"></asp:CompareValidator>
                           
                             <Ajx:FilteredTextBoxExtender ID="FLT_wbsenddate" runat="server" FilterType="Custom,Numbers" ValidChars="-" FilterMode="ValidChars" TargetControlID="txt_wbsenddate"></Ajx:FilteredTextBoxExtender>
                             <Ajx:FilteredTextBoxExtender ID="FLT_wbssrtdate" runat="server" FilterType="Custom,Numbers" ValidChars="-" FilterMode="ValidChars" TargetControlID="txt_wbssrtdate"></Ajx:FilteredTextBoxExtender>
                            
                           </div>
                                    </div>

                         <div class="row">
                     <div class="col-sm-2"></div>
                         <div class="col-sm-10">
                             [Note : WBS duration should be between Project duration]
                             </div>
                             </div>


                     <div class="row">
                     <div class="col-sm-6">                     
                    <asp:Button ID="btn_saveWBS" runat="server" Text="Add" Width="55px" ValidationGroup="addWBS" OnClick="btn_saveWBS_Click"/>
                    <asp:Button ID="btn_WBScncl" runat="server" Text="Cancel" Width="55px" OnClick="btn_WBScncl_Click" CausesValidation="false"/>
                          </div>
                    </div>
                     <asp:UpdatePanel ID="UPD_projwbs" runat="server">
                     <ContentTemplate></ContentTemplate>
                    <Triggers>
                    <asp:PostBackTrigger  ControlID="btn_saveWBS"/>
                    <asp:PostBackTrigger ControlID="btn_WBScncl"/>
                    </Triggers>
                    </asp:UpdatePanel>                           
                   
                        </div>

            <br />
                            <h5>Current WBS</h5>
                            <div class="row">
                     <div class="col-sm-12">
                                    <asp:DropDownList ID="DDL_srchwbsby" runat="server" OnSelectedIndexChanged="DDL_srchwbsby_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0" Selected="True">- Search -</asp:ListItem>
                                        <asp:ListItem Value="1">WBS</asp:ListItem>
                                        <asp:ListItem Value="2">WBS Period</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;
                         <asp:DropDownList ID="DDL_srchwbs" runat="server" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="DDL_srchwbs_SelectedIndexChanged"></asp:DropDownList> 
                          &nbsp;&nbsp;
                                    <asp:TextBox ID="txt_srchwbsbegda" runat="server" Visible="false" Placeholder="WBS Start Date" AutoComplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFV_srchwbsbegda" runat="server" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="srchpwbs"  ControlToValidate="txt_srchwbsbegda" Display="Dynamic" ForeColor="red"></asp:RequiredFieldValidator>
                                    <Ajx:CalendarExtender ID="CE_srchwbsbegda" runat="server" Enabled="True" Format="yyyy-MM-dd" TargetControlID="txt_srchwbsbegda" PopupButtonID="txt_srchwbsbegda">
                                     </Ajx:CalendarExtender>
                                    <Ajx:FilteredTextBoxExtender ID="FT_srchwbsbegda" runat="server" FilterType="Custom,Numbers" ValidChars="-" FilterMode="ValidChars" TargetControlID="txt_srchwbsbegda"></Ajx:FilteredTextBoxExtender>
                                     &nbsp;&nbsp;
                                    <asp:TextBox ID="txt_srchwbsendda" runat="server" Visible="false" Placeholder="WBS End Date" AutoComplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFV_srchwbsendda" runat="server" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="srchpwbs" ControlToValidate="txt_srchwbsendda" Display="Dynamic" ForeColor="red"></asp:RequiredFieldValidator>
                                    <Ajx:CalendarExtender ID="CE_srchwbsendda" runat="server" Enabled="True" Format="yyyy-MM-dd" TargetControlID="txt_srchwbsendda" PopupButtonID="txt_srchwbsendda">
                                     </Ajx:CalendarExtender>
                                    <Ajx:FilteredTextBoxExtender ID="FT_srchwbsendda" runat="server" FilterType="Custom,Numbers" ValidChars="-" FilterMode="ValidChars" TargetControlID="txt_srchwbsendda"></Ajx:FilteredTextBoxExtender>
                                     &nbsp;&nbsp;
                                    <asp:Button ID="btn_srchwbsdates" runat="server" Text="Search" ValidationGroup="srchpwbs" Visible="false" OnClick="btn_srchwbsdates_Click"/>
                                    <asp:Button ID="btn_srcwbsreset" runat="server" Text="Reset" OnClick="btn_srcwbsreset_Click" Visible="false" CausesValidation="false"/>
                                    &nbsp;&nbsp;
                                     <asp:CompareValidator ID="CPV_wbssrch" ValidationGroup="Date" Font-Size="Medium" ForeColor="Red" runat="server" ControlToValidate="txt_srchwbsbegda" ControlToCompare="txt_srchwbsendda" Operator="LessThanEqual" Type="Date" ErrorMessage="From date must be less than To date." Display="Dynamic" SetFocusOnError="true"></asp:CompareValidator>
                                    </div>
                                </div>
                            <br />
                            <div class="col-md-12 text-right" id="dvcntwbs" runat="server"></div>
                            <asp:GridView ID="GV_wbs" runat="server" OnRowCommand="GV_wbs_RowCommand" DataKeyNames="id1,col11,TXT" AllowPaging="true" 
                                OnPageIndexChanging="GV_wbs_PageIndexChanging" PageSize="10">
                                <Columns>
                                     <asp:TemplateField HeaderText="Slno">
                                       <ItemTemplate>
                                          <asp:Label ID="lblwbsRowNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                        </ItemTemplate> 
                                       </asp:TemplateField>

                                    <asp:BoundField DataField="col11" HeaderText="Project" />
                                    <asp:TemplateField HeaderText="WBS ID">
                                       <ItemTemplate>
                                            <asp:Label ID="lbl_wbsidedtmod" runat="server" Text='<%# Eval("col15") %>'></asp:Label>

                                    <asp:TextBox ID="txt_wbsidedtmod" runat="server" Text='<%# Eval("col15") %>'></asp:TextBox>
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="WBS">
                                       <ItemTemplate>
                                           <asp:Label ID="lbl_wbsedtmod" runat="server" Text='<%# Eval("TXT") %>'></asp:Label>

                                           <asp:TextBox ID="txt_wbsedtmod" runat="server" Text='<%# Eval("TXT") %>'></asp:TextBox>
                                            </ItemTemplate>
                                       </asp:TemplateField>


                                     <asp:TemplateField HeaderText="Valid From">
                                       <ItemTemplate>
                                           <asp:Label ID="lbl_wbssda" runat="server" Text='<%# Eval("begda", "{0:dd/MM/yyyy}") %>'></asp:Label>

                                           <asp:TextBox ID="txt_wbsbegda" runat="server" Text='<%# Eval("begda", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
                                           <Ajx:CalendarExtender ID="CE_pbegda" runat="server" Enabled="True" Format="yyyy-MM-dd" TargetControlID="txt_wbsbegda"  PopupButtonID="txt_wbsbegda">
                                            </Ajx:CalendarExtender>
                                            <Ajx:FilteredTextBoxExtender ID="FLT_wbsbegda" runat="server" FilterType="Custom,Numbers" ValidChars="-" FilterMode="ValidChars" TargetControlID="txt_wbsbegda"></Ajx:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                       </asp:TemplateField>
                                    <asp:TemplateField HeaderText="End Date">
                                       <ItemTemplate>
                                           <asp:Label ID="lbl_wbsendda" runat="server" Text='<%# Eval("endda", "{0:dd/MM/yyyy}") %>'></asp:Label>

                                           <asp:TextBox ID="txt_wbsendda" runat="server" Text='<%# Eval("endda", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
                                            <Ajx:CalendarExtender ID="CE_pendda" runat="server" Enabled="True" Format="yyyy-MM-dd" TargetControlID="txt_wbsendda" PopupButtonID="txt_wbsendda">
                                            </Ajx:CalendarExtender>
                                           
                                            <Ajx:FilteredTextBoxExtender ID="FLT_wbsendda" runat="server" FilterType="Custom,Numbers" ValidChars="-" FilterMode="ValidChars" TargetControlID="txt_wbsendda"></Ajx:FilteredTextBoxExtender>
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                   
                                    <asp:BoundField DataField="begda1" HeaderText="Created On" DataFormatString="{0:dd/MM/yyyy}"/>
                                    <asp:BoundField DataField="endda1" HeaderText="Modified On" DataFormatString="{0:dd/MM/yyyy}"/>

                                     <asp:TemplateField HeaderText="Action">
                                       <ItemTemplate>
                                           <asp:LinkButton ID="LKwbsroedit"   CommandName="EDITWBS" runat="server" ToolTip="Edit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> 
                                             <i class="fe-edit-1"></i></asp:LinkButton>
                                           <asp:LinkButton ID="LKwbsroupdate" runat="server" ToolTip="Update" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                          CommandName="upwbs"><i class="fe-arrow-up-circle"></i></asp:LinkButton>
                                           <asp:LinkButton ID="LKwbsrocncl" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                          CommandName="CANCELWBS" ToolTip="Cancel" CausesValidation="false"><i class="fe-x-circle"></i></asp:LinkButton>
                                           
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            </div>
                        <div id="view4" runat="server" visible="false"  style="width:100%">
                            <br />
                             
             <div class="header-title">&nbsp;&nbsp;Add Activity</div>
                 <hr class="HrCls"/>
                            <br />
             
                    <div class="form-group">
                         <div class="row">
                     <div class="col-sm-2 htCr"><span style="color:red">*</span>  1.Select Project</div>
                     <div class="col-sm-8">
                     <asp:DropDownList ID="DDL_actproj" CssClass="txtDropDownwidth" runat="server" OnSelectedIndexChanged="DDL_actproj_SelectedIndexChanged" AutoPostBack="true">
                     </asp:DropDownList>
                         <asp:RequiredFieldValidator ID="RFV_actproj" runat="server" InitialValue="0" Display="Dynamic" SetFocusOnError="true" ValidationGroup="addactiv" ControlToValidate="DDL_actproj" ForeColor="Red"  ErrorMessage="*">
                     </asp:RequiredFieldValidator>
                     [Note : Select Project to load WBS]
                     
                     </div>                     
                     </div>

                         <div class="row">
                     <div class="col-sm-2 htCr"><span style="color:red">*</span>  2.Select WBS </div>
                     <div class="col-sm-8">
                     <asp:DropDownList ID="DDL_actwbs" CssClass="txtDropDownwidth" runat="server"></asp:DropDownList>
                         <asp:RequiredFieldValidator ID="RFV_actwbs" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="addactiv" ControlToValidate="DDL_actwbs" ForeColor="Red"  ErrorMessage="*">
                     </asp:RequiredFieldValidator>
                     [Note : Select WBS to add Activity]
                     
                     </div>                     
                     </div>

                         <div class="row">
                     <div class="col-sm-2 htCr"><span style="color:red">*</span>  3.Activity</div>
                     <div class="col-sm-8">
                         <asp:TextBox ID="txt_activity" runat="server" CssClass="txtDropDownwidth" Placeholder="Activity" AutoComplete="off"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RFV_activity" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="addactiv" ControlToValidate="txt_activity" ForeColor="Red"  ErrorMessage="Please Enter New Activity for selected WBS">
                     </asp:RequiredFieldValidator>
                        </div>
                             </div>
                        
                         <div class="row">
                     <div class="col-sm-6">                     
                    <asp:Button ID="btn_addactivity" runat="server" Text="Add" Width="55px" ValidationGroup="addactiv" OnClick="btn_addactivity_Click"/>
                    <asp:Button ID="btn_cnclactivity" runat="server" Text="Cancel" Width="55px" OnClick="btn_cnclactivity_Click" CausesValidation="false"/>
                          </div>
                    </div>
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                     <ContentTemplate></ContentTemplate>
                    <Triggers>
                    <asp:PostBackTrigger  ControlID="btn_addactivity"/>
                    <asp:PostBackTrigger ControlID="btn_cnclactivity"/>
                    </Triggers>
                    </asp:UpdatePanel>  
                        </div>

                            <br />
                             <h5>Current Activity Types</h5>
                             <div class="row">
                                <div class="col-sm-12">
                            <asp:DropDownList ID="DDL_srchacty" runat="server" OnSelectedIndexChanged="DDL_srchacty_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>&nbsp;&nbsp;
                                    <asp:Button ID="btn_srchact" runat="server" Text="Reset" OnClick="btn_srchact_Click" Visible="false"/>
                                    </div>
                                </div>
                            <br />
                            <div class="col-md-12 text-right" id="dvcntacty" runat="server"></div>
                            <asp:GridView ID="GV_acty" runat="server" AllowPaging="true" OnPageIndexChanging="GV_acty_PageIndexChanging"
                                OnRowCommand="GV_acty_RowCommand" PageSize="10" DataKeyNames="id1,col16">
                                <Columns>
                                    <asp:TemplateField HeaderText="Slno">
                                       <ItemTemplate>
                                          <asp:Label ID="lblactyRowNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                        </ItemTemplate>
                                       </asp:TemplateField>
                                    <asp:BoundField DataField="col15" HeaderText="Project" />
                                    <asp:BoundField DataField="TXT" HeaderText="WBS" />
                                    <asp:TemplateField HeaderText="Activity">
                                       <ItemTemplate>
                                    <asp:Label ID="lbl_acty" runat="server" Text='<%# Eval("col11") %>'></asp:Label>

                                    <asp:TextBox ID="txt_acty" runat="server" Text='<%# Eval("col11") %>'></asp:TextBox>
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                                                      
                                    <asp:BoundField DataField="begda" HeaderText="Valid From" DataFormatString="{0:dd/MM/yyyy}"/>
                                    <asp:BoundField DataField="endda" HeaderText="End Date" DataFormatString="{0:dd/MM/yyyy}"/>
                                    <asp:BoundField DataField="begda1" HeaderText="Created On" DataFormatString="{0:dd/MM/yyyy}"/>
                                    <asp:BoundField DataField="endda1" HeaderText="Modified On" DataFormatString="{0:dd/MM/yyyy}"/>
                                    
                                     <asp:TemplateField HeaderText="Action">  
                                      <ItemTemplate>
                                         
                                          <asp:LinkButton ID="LK_edtacty" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                           CommandName="editacty" ToolTip="Edit"><i class="fe-edit-1"></i></asp:LinkButton>

                                          <asp:LinkButton ID="LK_updacty" runat="server" ToolTip="Update" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                          CommandName="updacty"><i class="fe-arrow-up-circle"></i></asp:LinkButton>

                                          <asp:LinkButton ID="LK_cnclacty" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                          CommandName="canacty" ToolTip="Cancel"><i class="fe-x-circle"></i></asp:LinkButton>
                                       </ItemTemplate>  
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            </div>
                            
                            <div id="view5" runat="server" visible="false"  style="width:100%">
                                <br />
              
            <div class="header-title">&nbsp;&nbsp;Add Attendance Type</div>
                 <hr class="HrCls"/>
                            <br />
             
            <div class="form-group">
           
               <div class="row">
             <div class="col-sm-2 htCr"><span style="color:red">*</span> New Attendance Type</div>
             <div class="col-sm-10">           
              <asp:TextBox ID="txt_addattcytype" CssClass="txtDropDownwidth" runat="server" Placeholder="Attendance" AutoComplete="off"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RQD_Mstrsattdctyp" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="addattdctyp"  ControlToValidate="txt_addattcytype" ForeColor="Red" ErrorMessage="Please Enter New Attendance Type">
              </asp:RequiredFieldValidator>
                     &nbsp;&nbsp;   
              <asp:Button ID="btn_add_mstrattdcetyp" runat="server" Text="Add" Width="55px" ValidationGroup="addattdctyp" OnClick="btn_add_mstrattdcetyp_Click"/>
              <asp:Button ID="btn_cncl_mstrattdcetyp" runat="server"  Width="55px" Text="Cancel" OnClick="btn_cncl_mstrattdcetyp_Click"/>
                  </div>
              </div>
                
                <br />
                <h5>Current Attendance Types</h5>
                 <div class="row">
                                <div class="col-sm-12">
                            <asp:DropDownList ID="DDL_srchattd" runat="server" OnSelectedIndexChanged="DDL_srchattd_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                     &nbsp;&nbsp;
                                    <asp:Button ID="btn_srchattdrest" runat="server" Text="Reset" Visible="false" OnClick="btn_srchattdrest_Click"/>
                                    </div>
                                </div>
                            <br />
                <div class="col-md-12 text-right" id="dvcntattd" runat="server"></div>
                <asp:GridView ID="GV_atttype" runat="server" DataKeyNames="id1,col11" AllowPaging="true" PageSize="10" 
                    OnPageIndexChanging="GV_atttype_PageIndexChanging" OnRowCommand="GV_atttype_RowCommand">
                   
                    <Columns>
                        <asp:TemplateField HeaderText="Slno">
                          <ItemTemplate>
                            <asp:Label ID="lblattdRowNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                          </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Attendance Type">
                          <ItemTemplate>
                              <asp:Label ID="lbl_attdtyp" runat="server" Text='<%# Eval("col11") %>'></asp:Label>

                              <asp:TextBox ID="txt_attdtyp" runat="server" Text='<%# Eval("col11") %>'></asp:TextBox>
                               </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="begda1" HeaderText="Created On" DataFormatString="{0:dd/MM/yyyy}"/>
                        <asp:BoundField DataField="endda1" HeaderText="Modified On" DataFormatString="{0:dd/MM/yyyy}"/>

                        <asp:TemplateField HeaderText="Action">  
                          <ItemTemplate>
                             <asp:LinkButton ID="LK_attdedt" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                              CommandName="attdedit" ToolTip="Edit"><i class="fe-edit-1"></i></asp:LinkButton>

                              <asp:LinkButton ID="LK_attdupd" runat="server" ToolTip="Update" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                               CommandName="attdupd"><i class="fe-arrow-up-circle"></i></asp:LinkButton>

                                <asp:LinkButton ID="LK_attdcncl" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                 CommandName="attdcan" ToolTip="Cancel"><i class="fe-x-circle"></i></asp:LinkButton>
                             </ItemTemplate>  
                          </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate></ContentTemplate>
              <Triggers>
              <asp:PostBackTrigger ControlID="btn_add_mstrattdcetyp" />
              <asp:PostBackTrigger ControlID="btn_cncl_mstrattdcetyp"/>
              </Triggers>
              </asp:UpdatePanel>
              
                </div>
                                </div>
                            
                             </div>
                        </div>
                        </div>



                      <%--  ------------------------Master of PR----------------------------------------------%>
                                      <div id="View6" runat="server" visible="false"  style="width:100%">
                            <br />
                          <div class="header-title">&nbsp;&nbsp;Add PR</div>
                                 <hr class="HrCls"/>
                            <br />
                                <div class="row">
                                    <div class="md-2">&nbsp;&nbsp;&nbsp;&nbsp; Upload PR Details</div>
                                    <div class="col-md-3">
                                        <asp:FileUpload ID="uflPRData" runat="server" AllowMultiple="false" />
                                    </div>

                                    <div class="col-md-4">
                                        <asp:Button ID="btnUploadPRData" runat="server" Text="Check"  OnClick="btnUploadPRData_Click"/>
                                        <asp:Button ID="btnSave" runat="server" Visible="false" Text="Upload"/>
                                        <asp:Button ID="btnClear" runat="server" Visible="false" Text="Cancel"  />
                                    </div>
                                    <div class="col-md-3" style="text-align: right">
                                <%-- <asp:UpdatePanel ID="b" runat="server">--%>
                                <%-- <ContentTemplate>
                                        <asp:LinkButton ID="lnkTemplDwnld" runat="server" Text="Download Template"></asp:LinkButton>
                                      </ContentTemplate>--%>
                                      <%--<Triggers>
                                          <asp:PostBackTrigger ControlID="btnUploadPRData" />
                                          <%-- <asp:PostBackTrigger ControlID="btnSave" />
                                         <asp:PostBackTrigger ControlID="lnkTemplDwnld" />--%>
                                     <%--   </Triggers>--%>
                               <%--   </asp:UpdatePanel>--%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12" id="divgrds" runat="server" visible="false">
                                        <h4>Employee Personal Information</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_EmpInfo" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="CID" DataField="CID" />
                                                    <asp:BoundField HeaderText="C_DESC" DataField="C_DESC" />
                                                    <asp:BoundField HeaderText="A_DESCe" DataField="A_DESC" />
                                                    <asp:BoundField HeaderText="B_DESC" DataField="B_DESC" />
                                                   <%-- <asp:BoundField HeaderText="Last Name" DataField="Last_Name" />
                                                    <asp:BoundField HeaderText="Gender" DataField="Gender" />
                                                    <asp:BoundField HeaderText="Date of Birth" DataField="Date_of_Birth" />
                                                    <asp:BoundField HeaderText="Marital Status" DataField="Marital_Status" />
                                                    <asp:BoundField HeaderText="Father Name" DataField="Father_Name" />
                                                    <asp:BoundField HeaderText="Mother Name" DataField="Mother_Name" />
                                                    <asp:BoundField HeaderText="Spouse Name" DataField="Spouse_Name" />--%>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <h4>Department Information</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="gv_dept" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Employee_ID" DataField="Employee_ID" />
                                                    <asp:BoundField HeaderText="Department" DataField="Employee_Department" />
                                                    <asp:BoundField HeaderText="Grade" DataField="Grade" />
                                                    <asp:BoundField HeaderText="Branch" DataField="Branch" />
                                                    <asp:BoundField HeaderText="Division" DataField="Division" />
                                                    <asp:BoundField HeaderText="Date of Joining" DataField="Date_of_Joining" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <h4>Bank Information</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_BankInfo" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Employee_ID" DataField="Employee_ID" />
                                                    <asp:BoundField HeaderText="Bank Name" DataField="Bank_Name" />
                                                    <asp:BoundField HeaderText="Account Number" DataField="Account_Number" />
                                                    <asp:BoundField HeaderText="IFSC Code" DataField="IFSC_Code" />
                                                    <asp:BoundField HeaderText="Bank Branch" DataField="Bank_Branch" />
                                                    <asp:BoundField HeaderText="Bank District" DataField="Bank_District" />
                                                    <asp:BoundField HeaderText="Branch Country" DataField="Branch_Country" />
                                                    <asp:BoundField HeaderText="Branch State" DataField="Branch_State" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <h4>Address Information</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_AddressInfo" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Employee_ID" DataField="Employee_ID" />
                                                    <asp:BoundField HeaderText="Address Type" DataField="Address_Type" />
                                                    <asp:BoundField HeaderText="Valid From" DataField="Start_Date" />
                                                    <asp:BoundField HeaderText="To Date" DataField="End_Date" />
                                                    <asp:BoundField HeaderText="Residence Number" DataField="Residence_Number" />
                                                    <asp:BoundField HeaderText="Street" DataField="Street" />
                                                    <asp:BoundField HeaderText="Locality" DataField="Locality" />
                                                    <asp:BoundField HeaderText="District" DataField="District" />
                                                    <asp:BoundField HeaderText="Country" DataField="Country" />
                                                    <asp:BoundField HeaderText="State" DataField="State" />
                                                    <asp:BoundField HeaderText="Pincode" DataField="Pincode" />
                                                    <asp:BoundField HeaderText="STD_Code" DataField="STD_Code" />
                                                    <asp:BoundField HeaderText="Ward_Number" DataField="Ward_Number" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <h4>Contact Information</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_ContInfo" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Employee ID" DataField="Employee_ID" />
                                                    <asp:BoundField HeaderText="Contact_Type" DataField="Contact_Type" />
                                                    <asp:BoundField HeaderText="Contact_Type_ID" DataField="Contact_Type_ID" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <h4>Document Information</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_DocInfo" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Employee ID" DataField="Employee_ID" />
                                                    <asp:BoundField HeaderText="Document_Type" DataField="Document_Type" />
                                                    <asp:BoundField HeaderText="Document_Type_ID" DataField="Document_Type_ID" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <h4>Benefits Information</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_Benefits" runat="server" AutoGenerateColumns="false" CssClass="Grid respovrflw" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Employee ID" DataField="Employee_ID" />
                                                    <asp:BoundField HeaderText="ESI_Applicable" DataField="ESI_Applicable" />
                                                    <asp:BoundField HeaderText="ESI_Number" DataField="ESI_Number" />
                                                    <asp:BoundField HeaderText="ESI_Dispencary" DataField="ESI_Dispencary" />
                                                    <asp:BoundField HeaderText="PF_Applicable" DataField="PF_Applicable" />
                                                    <asp:BoundField HeaderText="PF_Number" DataField="PF_Number" />
                                                    <asp:BoundField HeaderText="PF_Number_Dept_File" DataField="PF_Number_Dept_File" />
                                                    <asp:BoundField HeaderText="Restrict_PF" DataField="Restrict_PF" />
                                                    <asp:BoundField HeaderText="Zero_Pension" DataField="Zero_Pension" />
                                                    <asp:BoundField HeaderText="Zero_PT" DataField="Zero_PT" />
                                                </Columns>
                                            </asp:GridView>
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
