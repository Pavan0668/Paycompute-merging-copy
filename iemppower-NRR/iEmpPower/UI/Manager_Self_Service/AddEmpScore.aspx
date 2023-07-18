<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddEmpScore.aspx.cs"
    Inherits="iEmpPower.UI.Manager_Self_Service.AddEmpScore" EnableEventValidation="false" Culture="en-GB" UICulture="auto" Theme="SkinFile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .form-inline
        {
           
            padding-top: 17px;
            border-style: solid;
            border-color: rgba(0,97,124,0.3);
            border-width: thin;
        }
         .Background

        {

            background-color: Black;

            filter: alpha(opacity=90);

            opacity: 0.8;

        }

        .Popup

        {

            background-color: #FFFFFF;

            border-width: 3px;

            border-style: solid;

            border-color: black;

            padding-top: 10px;

            padding-left: 10px;

            width: 800px;

            height: 550px;

        }

        .txtscore {
            width: 120px;
            height: calc(100%) !important;
            min-height: 30px !important;
            max-height: 50px !important;
            font-size: 13px !important;
            /*padding:6px 18px !important;*/
        }

        .txtDropDownwidth_scores {
            padding: 6px 18px !important;
            height: calc(100%) !important;
            min-height: 28px !important;
            max-height: 48px !important;
            font-size: 13px !important;
            min-width: 150px !important;
            max-width: 350px !important;
            width: calc(100%) !important;
        }
            .txtDropDownwidth_scores:focus {
                border-width: 2px;
                border-color: lightblue;
                border-bottom-style: solid;
                /*padding: 12px 20px;
        margin: 8px 0;*/
                box-sizing: border-box;
            }
             .txtDropDownwidth_scores:hover{
            border-bottom-style: solid;
        border-width: 2px;
        border-color: lightblue;
        /*padding: 12px 20px;
        margin: 8px 0;*/
        box-sizing: border-box;
        }

             .lnkclass {
          float:right;
         padding-right:5px;
         display:inline;
         padding-top:3px;
            
        }
        /*.txtDropDownwidth_View {
            padding: 6px 18px !important;
            height: calc(100%) !important;
            min-height: 28px !important;
            max-height: 48px !important;
            font-size: 13px !important;
            width: 200px !important;
        }*/

        .hd-small1 {
            font-size: 13px;
            color: #FFFFFF;
            vertical-align: top;
        }
       

      
    </style>

 <script>

     function call(eventElement) {

         var target = eventElement.target;

         switch (target.mode) {

             case "month":

                 var cal = $find("CE_BE_txtBudgFrmMonth");

                 cal._visibleDate = target.date;

                 cal.set_selectedDate(target.date);

                 cal._switchMonth(target.date);

                 cal._blur.post(true);

                 cal.raiseDateSelectionChanged();

                 break;

         }

     }

     function onCalendarShown() {

         var cal = $find("CE_BE_txtBudgFrmMonth");

         //Setting the default mode to month

         cal._switchMode("months", true);

         //Iterate every month Item and attach click event to it

         if (cal._monthsBody) {

             for (var i = 0; i < cal._monthsBody.rows.length; i++) {

                 var row = cal._monthsBody.rows[i];

                 for (var j = 0; j < row.cells.length; j++) {

                     Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);

                 }

             }

         }

     }

     function onCalendarHidden() {

         var cal = $find("CE_BE_txtBudgFrmMonth");

         //Iterate every month Item and remove click event from it

         if (cal._monthsBody) {

             for (var i = 0; i < cal._monthsBody.rows.length; i++) {

                 var row = cal._monthsBody.rows[i];

                 for (var j = 0; j < row.cells.length; j++) {

                     Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);

                 }

             }

         }

     }





     function Srch_call(eventElement) {

         var Src_target = eventElement.target;

         switch (Src_target.mode) {

             case "month":

                 var cald = $find("Srch_cldr_behve");

                 cald._visibleDate = Src_target.date;

                 cald.set_selectedDate(Src_target.date);

                 cald._switchMonth(Src_target.date);

                 cald._blur.post(true);

                 cald.raiseDateSelectionChanged();

                 break;

         }

     }

     function Srch_Cal_Show() {

         var cald = $find("Srch_cldr_behve");

         //Setting the default mode to month

         cald._switchMode("months", true);

         //Iterate every month Item and attach click event to it

         if (cald._monthsBody) {

             for (var i = 0; i < cald._monthsBody.rows.length; i++) {

                 var row = cald._monthsBody.rows[i];

                 for (var j = 0; j < row.cells.length; j++) {

                     Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", Srch_call);

                 }

             }

         }

     }

     function Srch_Cal_hide() {

         var cald = $find("Srch_cldr_behve");

         //Iterate every month Item and remove click event from it

         if (cald._monthsBody) {

             for (var i = 0; i < cal._monthsBody.rows.length; i++) {

                 var row = cald._monthsBody.rows[i];

                 for (var j = 0; j < row.cells.length; j++) {

                     Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", Srch_call);

                 }

             }

         }

     }
     </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:MultiView ID="Mult_Add_Scores" runat="server">
        <asp:View ID="View_Add_Scores" runat="server">



            <div class="header">
             <div class="row clearfix">
           <div>
             <span class="HeadFontSize" style="margin-left:10px">&nbsp;Add Scores
              </span>
               <%-- <div style="float:right;margin-right:25px">--%>

                <div class="Fr">
                   <table>
                     <tr>
                      <td><asp:LinkButton ID="lnk_view" runat="server" CssClass="lnkclass" OnClick="lnk_view_Click">View Scores</asp:LinkButton>
                           </td>
                           <td><p class="paddingstyl">&nbsp;| &nbsp;</p></td>
                           <td> <asp:LinkButton ID="lnk_add_modnsub" runat="server" Text="Add masters" OnClick="lnk_add_modnsub_Click"></asp:LinkButton></td>
                            </tr>
                            </table>
                             </div>

               <%-- </div>
                  </div>
                    </div>
                         <div style="float:right">--%>
                            
                             <%--<asp:Button ID="btn_add_modnsub" runat="server" Text="Add masters" OnClick="btn_add_modnsub_Click"/>--%>
                            <%--<asp:Button ID="btn_open_mstrs_popup" runat="server" Text="Add Masters" OnClick="btn_open_mstrs_popup_Click" />--%>
                        </div> 
                             </div> 
                </div> 

           <%-- <div class="header">
                <div class="row clearfix">
                    <div class="col-xs-12 col-sm-6">

                        <span class="HeadFontSize">&nbsp;Add Scores</span>


                    </div>
                    <div style="margin-left:95% !important; margin-top: 2% !important">
                        <asp:LinkButton ID="lnk_view" runat="server" CssClass="lnkclass" OnClick="lnk_view_Click">View</asp:LinkButton></div>
                </div>

            </div>--%>


            <div class="body">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>


                        <%--<div class="form-inline">--%>
                            <div class="form-group">

                                    <div class="respovrflw" style="max-height:350px;">

                                <table style="color: #333333; border-color: #03345B; border-width: 1px; border-style: Solid; font-family: verdana; font-size: 8pt; width: 100%; float: left" class="Grid" cellpadding="4" cellspacing="1">
                                    <tbody>
                                        <tr style="color: White; background-color: #00617C; font-family: verdana; font-size: 8pt; font-weight: bold; height: 30px;">

                                            <th class="hd-small1" scope="col">Employee</th>

                                            <th class="hd-small1" scope="col">Module</th>

                                            <th class="hd-small1" scope="col">Sub Module</th>

                                            <th class="hd-small1" scope="col" style="width: 100px">For the month of</th>

                                            <th class="hd-small1" scope="col" style="width: 100px">Rating</th>

                                            <th class="hd-small1" scope="col" style="width: 40px">Remarks</th>

                                            <th style="width: 65px; background-color: #f4f8f9" rowspan="2">
                                                <asp:LinkButton ID="LK_addtotmptbl" OnClick="LK_addtotmptbl_Click" CssClass="add-Lk" ValidationGroup="add" runat="server">Add</asp:LinkButton>
                                                <br/>
                                                
                                                <asp:LinkButton ID="LK_update" ValidationGroup="add" runat="server" Visible="false" OnClick="LK_update_Click">Update</asp:LinkButton>
                                                <br/>
                                                
                                                <asp:LinkButton ID="Lk_Clear" OnClick="Lk_Clear_Click" runat="server">Clear</asp:LinkButton>
                                                
                                            </th>

                                        </tr>

                                        <tr style="background-color: #f4f8f9; font-family: verdana; font-size: 8pt; height: 25px; text-align: center">
                                            <td>
                                                <asp:DropDownList ID="DDL_Skill_Ename" CssClass="txtDropDownwidth_scores" runat="server" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="DDL_Skill_Ename_SelectedIndexChanged">
                                                </asp:DropDownList><asp:RequiredFieldValidator ValidationGroup="add" Display="Dynamic" SetFocusOnError="true" ID="rqd_valdr_Ename" runat="server" ForeColor="Red" ErrorMessage="*" ControlToValidate="DDL_Skill_Ename" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DDL_Skillmod" runat="server" CssClass=" txtDropDownwidth_scores" TabIndex="2"
                                                    OnSelectedIndexChanged="DDL_Skillmod_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rqd_valdr_Module" Display="Dynamic" SetFocusOnError="true" runat="server" ValidationGroup="add" ErrorMessage="*" ForeColor="Red" ControlToValidate="DDL_Skillmod" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>

                                            <td>
                                                <asp:DropDownList ID="DDL_Submodule" CssClass="txtDropDownwidth_scores" AutoPostBack="true" runat="server" TabIndex="3">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rqd_valdr_SbModule" Display="Dynamic" SetFocusOnError="true" runat="server" ValidationGroup="add" ErrorMessage="*" ForeColor="Red" ControlToValidate="DDL_Submodule" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>

                                            <td>

                                                <asp:TextBox ID="txtmonth_cldr" Enabled="false" CssClass="txtscore" runat="server"  TabIndex="4" AutoPostBack="true"></asp:TextBox>
                                               
                                               <%--<Ajx:CalendarExtender ID="CE_txtBudgFrmMonth" runat="server" BehaviorID="CE_BE_txtBudgFrmMonth" Enabled="True" Format="MM/yyyy"
                                             TargetControlID="txtmonth_cldr" OnClientHidden="onCalendarHidden" OnClientShown="onCalendarShown" PopupButtonID="txtmonth_cldr" 
                                                   DefaultView="Months">  </Ajx:CalendarExtender>--%>

                                                <asp:RequiredFieldValidator ID="rqd_valdr_Months" runat="server" Display="Dynamic" SetFocusOnError="true" ErrorMessage="*"
                                                    ValidationGroup="add" ControlToValidate="txtmonth_cldr" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>

                                            <td>
                                                <asp:TextBox ID="txt_Score" runat="server"  CssClass="txtscore" Placeholder="(1-5)" TabIndex="5"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rqd_valdr_Score" Display="Dynamic" SetFocusOnError="true" runat="server"
                                                     ValidationGroup="add" ErrorMessage="*" ForeColor="Red" ControlToValidate="txt_Score"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="rng_vldr" runat="server" Display="Dynamic" SetFocusOnError="true" MinimumValue="1" ValidationGroup="add" 
                                                    MaximumValue="5"  Type="Double" ErrorMessage="(1-5)" ForeColor="Red" ControlToValidate="txt_Score"></asp:RangeValidator >
                                                <Ajx:FilteredTextBoxExtender ID="filter_Score" runat="server" FilterType="Custom,Numbers"  ValidChars="." TargetControlID="txt_Score" />
                                                
                                            </td>


                                            <td>
                                                <asp:TextBox ID="txt_Score_commnt" runat="server" TextMode="MultiLine" TabIndex="6"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rqd_valdr_Remarks" Display="Dynamic" SetFocusOnError="true" runat="server" ValidationGroup="add" ErrorMessage="*" ForeColor="Red" ControlToValidate="txt_Score_commnt"></asp:RequiredFieldValidator>
                                            </td>

                                        </tr>

                                    </tbody>
                                </table>
                                        </div>
                               
                            </div>
                        <%--</div>--%>
                        <br />

                       
                        <br />
                            <div class="respovrflw">
                        <asp:GridView runat="server" ID="GV_TmpoScore" AutoGenerateColumns="false" Width="100%"
                            CssClass="Grid" OnRowDeleting="GV_TmpoScore_RowDeleting"  OnPageIndexChanging="GV_TmpoScore_PageIndexChanging" AllowPaging="true" PageSize="10" AllowSorting="true"
                            DataKeyNames="DDL_Skill_Ename,DDL_Skillmod,DDL_Submodule,txt_Score,txtmonth_cldr,txt_Score_commnt,Ename_vfield,Skillmod_vfield,Submodule_vfield,LblID"
                            GridLines="Both" OnRowCommand="GV_TmpoScore_RowCommand">
                            <Columns>
                                <asp:TemplateField  Visible="false" HeaderText="Serial number">
                                   <ItemTemplate>
                                           <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField ItemStyle-Width="105px"  HeaderText="Slno">
                                    <ItemTemplate>
                                        <asp:Label ID="GV_Row" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="DDL_Skill_Ename" ItemStyle-Width="135px" HeaderText="Employee" />
                                <asp:BoundField DataField="Ename_vfield" ItemStyle-Width="135px" Visible="false" HeaderText="Employee" />

                                <asp:BoundField DataField="DDL_Skillmod" ItemStyle-Width="105px" HeaderText="Module" />
                                <asp:BoundField DataField="Skillmod_vfield" Visible="false" ItemStyle-Width="135px" HeaderText="Employee" />

                                <asp:BoundField DataField="DDL_Submodule" ItemStyle-Width="105px" HeaderText="Sub-Module" />
                                <asp:BoundField DataField="Submodule_vfield" Visible="false" ItemStyle-Width="135px" HeaderText="Employee" />

                                 <asp:BoundField DataField="txtmonth_cldr" ItemStyle-Width="65px" HeaderText="Month" />

                                <asp:BoundField DataField="txt_Score" ItemStyle-Width="65px" HeaderText="Score" />


                                <asp:BoundField DataField="txt_Score_commnt" HeaderText="Remarks" />

                                <%-- <asp:CommandField ShowEditButton="true" ButtonType="Link" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="65px" ControlStyle-ForeColor="Blue" />--%>
                                <asp:TemplateField ItemStyle-Width="65px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>

                                        <asp:LinkButton ID="Lnk_GVedit" runat="server" ForeColor="Blue" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="bindeditdata" Text="Edit"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <%--<asp:CommandField ShowDeleteButton="True" ButtonType="Link" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="65px"  ControlStyle-ForeColor="Blue" />--%>

                                 <asp:TemplateField ItemStyle-Width="65px" ItemStyle-HorizontalAlign="Center">
                                 <ItemTemplate >
                                     <asp:LinkButton ID="Lnk_GVdelete" runat="server" ForeColor="Blue" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  CommandName="deletedata" Text="Delete"></asp:LinkButton>
                                 </ItemTemplate>
                             </asp:TemplateField>
                            </Columns>
                                    </asp:GridView>
                                    <div/>
                        <br />
                        <br />
                        <asp:Button ID="btnsavescore" runat="server" Text="Submit" Visible="false" OnClick="btnsavescore_Click" />
                                
                    </ContentTemplate>
                    <Triggers>                       
                         <asp:PostBackTrigger ControlID="btnsavescore" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </asp:View>








        <asp:View ID="View_Score_Details" runat="server">
            <div class="header">
             <div class="row clearfix">
           <div>
             <span class="HeadFontSize" style="margin-left:10px">&nbsp;View Score Details
              </span>
                <div style="float:right;margin-right:25px">
                    <asp:LinkButton ID="Lnk_Back" runat="server" CssClass="lnkclass" OnClick="Lnk_Back_Click">Back</asp:LinkButton></div>
                </div>

            </div>
                </div>




           <%-- <div class="header">
                <div class="row clearfix">
                    <div class="col-xs-12 col-sm-6">

                        <span class="HeadFontSize" style="margin-left:10px">&nbsp;View Score Details</span>


                    </div>
                    <div style="margin-left:95% !important; margin-top: 2% !important">
                        <asp:LinkButton ID="Lnk_Back" runat="server" CssClass="lnkclass" OnClick="Lnk_Back_Click">Back</asp:LinkButton></div>
                </div>

            </div>--%>


            <div class="body">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

                       
                            <div class="form-group">
                                <div>
                                <div>
                                   
                                    <asp:DropDownList ID="ddl_Srchby" runat="server" CssClass="txtDropDownwidth"  AutoPostBack="true" OnSelectedIndexChanged="ddl_Srchby_SelectedIndexChanged">
                                        <%--<asp:ListItem Value="0" Selected="True">-- Search by --</asp:ListItem>--%>
                                        <asp:ListItem Value="1">Employee name</asp:ListItem>
                                        <asp:ListItem Value="2">Module</asp:ListItem>
                                        <asp:ListItem Value="3">All records</asp:ListItem>
                                        <asp:ListItem Value="4">Month</asp:ListItem>
                                        <asp:ListItem Value="5" id="lst1" runat="server">My subordinates</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RqurdFldVldtrsrc" Display="Dynamic" runat="server" ErrorMessage="*" ControlToValidate="ddl_Srchby" ForeColor="Red"
                                        ValidationGroup="srch" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                    
                                    <asp:DropDownList ID="ddl_srch_Enam" runat="server" CssClass="txtDropDownwidth" AutoPostBack="true"></asp:DropDownList>
                                   
                                    <asp:DropDownList ID="ddl_srch_Modl" runat="server" AutoPostBack="true" CssClass="txtDropDownwidth"></asp:DropDownList>
                                  
                                    <asp:TextBox ID="txt_Vw_month" runat="server" AutoPostBack="true"></asp:TextBox>
                                    <Ajx:CalendarExtender ID="Srch_calendar" runat="server" BehaviorID="Srch_cldr_behve" Enabled="true" Format="MM/yyyy"
                                        TargetControlID="txt_Vw_month" OnClientHidden="Srch_Cal_hide" OnClientShown="Srch_Cal_Show" PopupButtonID="txt_Vw_month"
                                         DefaultView="Months"> </Ajx:CalendarExtender>
                                  
                                  
                                  
                                    <asp:Button ID="btn_Search_toview" runat="server"  Width="75px" Text="Search" OnClick="btn_Search_toview_Click" />
                                    <asp:Button ID="btn_Clr_Srch" runat="server"   Text="Reset" Width="75px" OnClick="btn_Clr_Srch_Click" />
                                          
                                    </div>
                                    </div>
                                    <br />
                                    <br />
                                    
                              <div class="respovrflw" style="max-height:350px;">
                                    <asp:GridView ID="gv_Viewscores" EmptyDataText="No Records found" runat="server" OnPageIndexChanging="gv_Viewscores_PageIndexChanging"
                                         AutoGenerateColumns="false" Width="100%" CssClass="Grid" AllowPaging="true" AllowSorting="true" PageSize="10" GridLines="Both" 
                                        PagerStyle-CssClass="cssPager" OnSorting="gv_Viewscores_Sorting">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Slno" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_VRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ENAME" HeaderText="Employee" SortExpression="sort_ename"/>
                                            <asp:BoundField DataField="MOD_name" HeaderText="Module" SortExpression="sort_mname"/>
                                            <asp:BoundField DataField="SUB__MOD_NAME" HeaderText="Sub-Module" SortExpression="sort_sname"/>
                                          
                                            <asp:BoundField DataField="SCORE" HeaderText="Scores" ItemStyle-HorizontalAlign="Center" SortExpression="sort_scores"/>
                                            <asp:BoundField DataField="REMARKS" HeaderText="Remarks"/>
                                              <asp:BoundField DataField="For_month" HeaderText="For the month of" SortExpression="sort_month"/>
                                             <asp:BoundField  DataField="Created_By" HeaderText="Created-By" SortExpression="sort_createdby"/>
                                            <asp:BoundField DataField="Created_On" HeaderText="Assessment-Date" ItemStyle-HorizontalAlign="Center" SortExpression="sort_ondate"/>
                                        </Columns>
                                    </asp:GridView>
                                  </div>
                                    <br />
                                    <br />
                                   
                                    <asp:Button ID="btn_VEXl" runat="server" Text="Export to excel" Width="110px" OnClick="btn_VEXl_Click" />
                                 <asp:Button ID="btn_VExport" runat="server" Text="Export to Pdf" Visible="false" Width="110px" OnClick="btn_VExport_Click" />
                                </div>
                            
                    </ContentTemplate>
                   
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btn_VExport" />
                        <asp:PostBackTrigger ControlID="btn_VEXl"/>                       
                    </Triggers>
                </asp:UpdatePanel>
            </div>


        </asp:View>


     
        <asp:View ID="VW_addmodnsub" runat="server">
            <div class="divfr" id="divbrdr" style="border-style:none !important">
                <asp:UpdatePanel ID="update_pnl_mod" runat="server">
                    <ContentTemplate>

                        <div id="divmodl" runat="server">
                             
                                  <div class="header">
             <div class="row clearfix">
           <div>
             <span class="HeadFontSize" style="margin-left:5px">&nbsp;Add Modules And Sub Modules
              </span>
               <div class="Fr">


                <table>
                    <tr>
                        <td>
                        <asp:LinkButton ID="LK_backtomodlpg" runat="server" CssClass="lnkclass" Text="Back" OnClick="LK_backtomodlpg_Click"></asp:LinkButton></td>
                       
                         <td>
                            <p class="paddingstyl">&nbsp;| &nbsp;</p>
                        </td>
                        <td>
                            <asp:LinkButton ID="LK_viewallmods" runat="server" Text="View" CssClass="lnkclass"  OnClick="LK_viewallmods_Click"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
                   </div>
                    </div>
                </div>
                       </div>    

                             <div class="divfr" id="div1">
                             <div class="header">
             <div class="row clearfix">
           <div>
             <span style="font-size: 20px; word-wrap: break-word">&nbsp;Add Modules
              </span>
               </div>
                 </div>
                                 </div>
                             <div class="form-inline">
                                 <div class="form-group">
                                    <div class="col-sm-3 htCr Cntrlwidth">1.View Existing Modules  :</div>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="DDL_newmodls" CssClass="txtDropDownwidth" runat="server"></asp:DropDownList>
                                 </div>
                                     </div>

                                  <br />
                                 <br />
                                <div class="form-group">
                                     <div class="col-sm-3 htCr Cntrlwidth">2. New Module :</div>
                                    <div class="col-sm-4">

                                         <asp:TextBox ID="txt_addsunmdl" CssClass="txtDropDownwidth" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RQD_addsubmodl" runat="server" ErrorMessage="*" ForeColor="Red"
                                                    Display="Dynamic" SetFocusOnError="true" ControlToValidate="txt_addsunmdl" ValidationGroup="addmodlsub"></asp:RequiredFieldValidator>

                                        </div>
                                     </div>
                                 <br />
                                 <br />
                                 <div class="form-group">
                        <div class="col-sm-6" style="margin-bottom:10px">
                <asp:Button ID="btn_addsubmodl" runat="server" Text="Add" Height="22px" Width="60px" ValidationGroup="addmodlsub" OnClick="btn_addsubmodl_Click"/>
                 
                           <asp:Button Id="btn_cancelsub" runat="server" Height="22px" Width="60px" Text="Cancel" OnClick="btn_cancelsub_Click"/>
                    </div>
                                     
                        </div>
                                 
                                 </div>
                         <%--<div class="body">
                                <table style="color: #333333; border-color: #03345B; border-width: 1px; border-style: Solid; font-family: verdana; font-size: 8pt; width: 60%;margin-left:"25px"; class="Grid" cellpadding="4" cellspacing="1">
                                    <tbody>
                                        <tr style="color: White; background-color: #00617C; font-family: verdana; font-size: 8pt; font-weight: bold; height: 30px;">

                                            <th  class="hd-small1" scope="col" style="width:50%"></th>

                                            <th class="hd-small1" scope="col" style="width:50%">New Module</th>
                                            </tr>
                                        <tr style="background-color: #f4f8f9; font-family: verdana; font-size: 8pt; height: 25px; text-align: center">
                                            <td>
                                               
                                            </td>
                                            <td>
                                                
                                            </td>
                                        </tr>
                </tbody>
                                    </table>
                           <br />
              
                </div>--%>




                       <div class="header">
             <div class="row clearfix">
           <div>
             <span style="font-size: 20px; word-wrap: break-word">&nbsp;Add Sub Modules
              </span>
               <%--<div class="Fr">


                <table>
                    <tr>
                        <td>
                        <asp:LinkButton ID="Lnk_Backtomainpg" runat="server" CssClass="lnkclass" Text="Back" OnClick="Lnk_Backtomainpg_Click"></asp:LinkButton>
                      </td>
                        <td>
                            <p class="paddingstyl">&nbsp;| &nbsp;</p>
                        </td>
                        <td>

                        <asp:LinkButton ID="Lnk_goto_submodl" runat="server" Text="Add modules" OnClick="Lnk_goto_submodl_Click"></asp:LinkButton>
                             </td>
                   <td>
                            <p class="paddingstyl">&nbsp;| &nbsp;</p>
                        </td>
                        <td>
                            <asp:LinkButton ID="LK_Viewmod_submodls" runat="server" Text="View" OnClick="LK_Viewmod_submodls_Click"></asp:LinkButton>
                            </td>
                        </tr>
                </table>
                   </div>--%>
                    </div>
                </div>
                            </div>



                            <div class="form-inline">
                                 <div class="form-group">
                                    <div class="col-sm-3 htCr Cntrlwidth">1. Modules  :</div>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="DDL_loadmodules" CssClass="txtDropDownwidth" runat="server"  AutoPostBack="true"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RQD_modDDL" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" 
                                                    SetFocusOnError="true" ControlToValidate="DDL_loadmodules" InitialValue="0" ValidationGroup="addsubmodl"></asp:RequiredFieldValidator>
                                        </div>
                                     </div>
                                 <br />
                                <br />
                                <div class="form-group">
                                     <div class="col-sm-3 htCr Cntrlwidth">2. New Sub Module :</div>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txt_newsubmodule" CssClass="txtDropDownwidth" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RQD_newsubmod" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic"
                                                    SetFocusOnError="true" ControlToValidate="txt_newsubmodule" ValidationGroup="addsubmodl"></asp:RequiredFieldValidator>


                                         </div>
                                     </div>

                                <br />
                                <br />
                                 <div class="form-group">
                        <div class="col-sm-6" style="margin-bottom:10px">
                <asp:Button ID="btn_save" runat="server" Width="60px" Height="22px" Text="Add" OnClick="btn_save_Click" ValidationGroup="addsubmodl"/>
                  <asp:Button ID="btn_Cancel" runat="server" Width="60px" Height="22px" Text="Cancel" OnClick="btn_Cancel_Click"/>

                           
                    </div>

                       <%-- <div class="body">
                                <table style="color: #333333; border-color: #03345B; border-width: 1px; border-style: Solid; font-family: verdana; font-size: 8pt; width: 60%;margin-left:"25px"; class="Grid" cellpadding="4" cellspacing="1">
                                    <tbody>
                                        <tr style="color: White; background-color: #00617C; font-family: verdana; font-size: 8pt; font-weight: bold; height: 30px;">

                                            <th  class="hd-small1" scope="col" style="width:50%">Existing Module</th>

                                            <th class="hd-small1" scope="col" style="width:50%">New Sub module</th>
                                            </tr>
                                        <tr style="background-color: #f4f8f9; font-family: verdana; font-size: 8pt; height: 25px; text-align: center">
                                            <td>
                                                
                                            </td>
                                            <td>
                                                 
                                            </td>
                                        </tr>
                </tbody>
                                    </table>
                           <br />
             
                </div>--%>


                      
                                </div>
                                <br />
                            </div>
                           </div>
                     </div>
                         <div id="div_viewall" runat="server">
                           <div class="header">
             <div class="row clearfix">
           <div>
             <span class="HeadFontSize" style="margin-left:-10px">&nbsp;Modules And Sub modules
              </span>
               <div class="Fr">


                <table>
                    <tr>
                        <td>
                        <asp:LinkButton ID="LK_backfrmvw" runat="server" CssClass="lnkclass" Text="Back" OnClick="LK_backfrmvw_Click"></asp:LinkButton>
               </td>
              
                    </tr>
                </table>
                   </div>
                    </div>
                </div>
                      </div>

                            

                                  <div class="form-group">
                                <div>
                                <div>
                                    <asp:DropDownList ID="DDL_modsub_searchby" CssClass="txtDropDownwidth" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="DDL_modsub_searchby_SelectedIndexChanged">
                                        <asp:ListItem Value="1">Module</asp:ListItem>
                                        <asp:ListItem Value="2">Sub-Module</asp:ListItem>
                                        <asp:ListItem Value="3">Employee</asp:ListItem>
                                         <asp:ListItem Value="4">All records</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="DDL_module_searchby" CssClass="txtDropDownwidth" runat="server" OnSelectedIndexChanged="DDL_module_searchby_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    <asp:DropDownList ID="DDL_subM_searchby" CssClass="txtDropDownwidth" runat="server"></asp:DropDownList>
                                    <asp:DropDownList ID="DDL_emp_searchby" CssClass="txtDropDownwidth" runat="server"></asp:DropDownList>


                                    <asp:Button ID="btn_ms_search" runat="server" Width="75px" Text="Search" OnClick="btn_ms_search_Click"/>
                                    <asp:Button ID="btn_ms_resetsrch" runat="server" Width="75px" Text="Reset" OnClick="btn_ms_resetsrch_Click"/>
                                    </div>
                                    </div>
                                     <br />
                                      <br />



                                 <%--<div class="body">--%>

                                 <asp:GridView ID="GV_allmodules" runat="server" EmptyDataText="No Records found"  OnPageIndexChanging="GV_allmodules_PageIndexChanging"
                                         AutoGenerateColumns="false" Width="100%" CssClass="Grid" AllowPaging="true" AllowSorting="true" PageSize="10" GridLines="Both" 
                                        PagerStyle-CssClass="cssPager" OnRowCommand="GV_allmodules_RowCommand" DataKeyNames="MID,SID,MOD_name,SUB__MOD_NAME,Created_By,
                                     SbCreated_By,mod_modifiedby,submod_modifiedby" OnRowDataBound="GV_allmodules_RowDataBound" OnSorting="GV_allmodules_Sorting">
                                     <Columns>

                                         <asp:TemplateField HeaderText="Slno" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                     
                                         <asp:TemplateField HeaderText="Modules" SortExpression="sort_mods">
                                             <ItemTemplate>
                                                 <asp:Label ID="LBL_modlname" runat="server" Text='<%# Eval("MOD_name") %>'></asp:Label>

                                                 <asp:TextBox ID="txt_modlname" runat="server" Text='<%# Eval("MOD_name") %>'></asp:TextBox>
                                                 </ItemTemplate>
                                         </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Sub Modules" SortExpression="sort_submods">
                                             <ItemTemplate>
                                                 <asp:Label ID="LBL_submodlname" runat="server" Text='<%# Eval("SUB__MOD_NAME") %>'></asp:Label>

                                                 <asp:TextBox ID="txt_submodlname" runat="server" Text='<%# Eval("SUB__MOD_NAME") %>'></asp:TextBox>
                                                 </ItemTemplate>
                                         </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Module Created By">
                                             <ItemTemplate>
                                                 <asp:Label ID="lbl_modcretedby" runat="server" Text='<%# Eval("Created_By") %>'></asp:Label>
                                                 </ItemTemplate>
                                         </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Sub-Module Created By">
                                             <ItemTemplate>
                                                 <asp:Label ID="lbl_subcreatedby" runat="server" Text='<%# Eval("SbCreated_By") %>'></asp:Label>
                                                 </ItemTemplate>
                                         </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Module Modified By">
                                             <ItemTemplate>
                                                 <asp:Label ID="lbl_modmodifdby" runat="server" Text='<%# Eval("mod_modifiedby") %>'></asp:Label>
                                                 </ItemTemplate>
                                         </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Sub-Module Modified By">
                                             <ItemTemplate>
                                                 <asp:Label ID="lbl_submodifiedby" runat="server" Text='<%# Eval("submod_modifiedby") %>'></asp:Label>
                                                 </ItemTemplate>
                                         </asp:TemplateField>
                                         <%--<asp:BoundField DataField="Created_By" HeaderText="Module Created By"/>
                                         <asp:BoundField DataField="SbCreated_By" HeaderText="Sub-Module Created By"/>
                                         <asp:BoundField DataField="mod_modifiedby" HeaderText="Module Modified By"/>
                                         <asp:BoundField DataField="submod_modifiedby" HeaderText="Sub-Module Modified By"/>--%>
                                         



                                         <%--<asp:BoundField DataField="MOD_name" HeaderText="Module"/>
                                         <asp:BoundField  DataField="SUB__MOD_NAME" HeaderText="Sub Module"/>--%>

                                         <asp:TemplateField>
                                             <ItemTemplate>
                                                 <asp:LinkButton ID="LK_Edit_modlsub" runat="server" Text="Edit" CommandName="editmodlsub" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                            <%-- </ItemTemplate>
                                         </asp:TemplateField>

                                         <asp:TemplateField>
                                             <ItemTemplate>--%>
                                                 <asp:LinkButton ID="LK_update_modlsub"  runat="server" Text="Update" CommandName="updatemodlsub" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                <%-- </ItemTemplate>
                                         </asp:TemplateField>

                                         <asp:TemplateField>
                                             <ItemTemplate>--%>
                                                 <asp:LinkButton ID="LK_cancel_modlsub" runat="server" Text="Cancel" CommandName="cancelmodlsub" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                 </ItemTemplate>
                                         </asp:TemplateField>
                                        
                                     </Columns>
                                 </asp:GridView>
                                      <%--</div>--%>
                                      </div>
                                 
                             </div>
                         
                             
                        </ContentTemplate>
                    <Triggers>
                        <%--<asp:PostBackTrigger ControlID="LK_exit" />--%>
                        <asp:PostBackTrigger ControlID="LK_backtomodlpg" />
                        <asp:PostBackTrigger ControlID="LK_backfrmvw" />
                        <asp:PostBackTrigger ControlID="LK_viewallmods" />
                        <%--<asp:PostBackTrigger ControlID="LK_Viewmod_submodls" />--%>
                    </Triggers>
                    </asp:UpdatePanel>
                </div>
        </asp:View>
         
    </asp:MultiView>

   
      <%--<cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btn_add_modnsub"

    CancelControlID="btn1" BackgroundCssClass="Background">

</cc1:ModalPopupExtender>
       
        <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" style = "display:none">

                   <div id="modulesdiv" runat="server">

                       <div class="header">
                <div class="row clearfix">
                    <div>

                        <span style="font-size: 20px; word-wrap: break-word">&nbsp;Add modules</span>

                    </div>
                    </div>
                    <div style="float:right;margin-right:25px">
                        <asp:LinkButton ID="LK_addsubmod" runat="server" CssClass="lnkclass" OnClick="LK_addsubmod_Click" Text="Add sub modules"></asp:LinkButton></div>
                

            </div>
                       <br />
                       <br />
                       <br />
                       
            </div>--%>



            <%--<div id="divaddsubmod" runat="server">

                       <div class="header">
                <div class="row clearfix">
                    <div>

                        <span style="font-size: 20px; word-wrap: break-word">&nbsp;Add sub modules</span>

                    </div>
                    </div>
                   
            </div>
                       <br />
                       <br />
                       <br />
                       <div class="body">
                                <table style="color: #333333; border-color: #03345B; border-width: 1px; border-style: Solid; font-family: verdana; font-size: 8pt; width: 60%;margin-left:25px"; class="Grid" cellpadding="4" cellspacing="1">
                                    <tbody>
                                        <tr style="color: White; background-color: #00617C; font-family: verdana; font-size: 8pt; font-weight: bold; height: 30px;">

                                            <th  class="hd-small1" scope="col" style="width:50%">Module</th>

                                            <th class="hd-small1" scope="col" style="width:50%">Sub module</th>
                                            </tr>
                                        <tr style="background-color: #f4f8f9; font-family: verdana; font-size: 8pt; height: 25px; text-align: center">
                                            <td>
                                                <asp:TextBox ID="txt_addmod" CssClass="txtDropDownwidth" runat="server"></asp:TextBox>
                                                
                                            </td>
                                            <td>
                                                 <asp:TextBox ID="txt_submodadd" CssClass="txtDropDownwidth" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                </tbody>
                                    </table>
                           <br />
              <div style="margin-right:25px">
                <asp:Button ID="btn_savemodl" runat="server" Text="Add" />
                           <asp:Button Id="btn_back" runat="server" Text="Back" OnClick="btn_back_Click"/>
                    </div>
                </div>
            </div>
            </asp:Panel>--%>
</asp:Content>
