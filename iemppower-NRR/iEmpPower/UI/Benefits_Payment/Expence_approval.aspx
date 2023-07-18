<%@ Page Title="Expence approval" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Expence_approval.aspx.cs" Inherits="iEmpPower.UI.Local_requisition.Expence_approval" Theme="SkinFile" EnableEventValidation="false"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

            //Job submission button highlight
            $("#job").css("background-color", "#ef2b27");

            $("#accPR").click(function () {
                $("#accPRContent").slideToggle("slow");

            });
            $("#Panel2").click(function () {
                $("#Panel2data").slideToggle("slow");

            });
            $("#Panel3").click(function () {
                $("#Panel3data").slideToggle("slow");

            });
        });
         </script>
   
    <style type="text/css">
        .textbox
        {}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
         <h2>Expense approval</h2>
         <%--<asp:BoundField DataField="GRAND_TOTAL" HeaderText="Amount" />--%> 
       <asp:Label ID="lblMessageBoard" runat="server"  CssClass="msgboard" ></asp:Label><br />
         <div class="clear"></div>
        <div>
          <div class="legend">Pending for approval</div>
         
         <br />
         <span class="auto-style2">&nbsp;&nbsp; Date As On-</span>
       <asp:Label ID="lblDate" runat="server" Font-Bold="True" ></asp:Label>  <br /><br />
        <span class="auto-style1"><strong>&nbsp;&nbsp; Total No. Of Records-</strong></span>
       <asp:Label ID="lblTotalRecords" runat="server" Font-Bold="True" ForeColor="#006600" ></asp:Label>  
         <br />
         <br />
        
       <div>
             <%--           <asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                  
              
                               </td>
                               </tr>
                         
                       </table>
                       
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                             
                      <asp:CheckBox ID="CheckBox2" runat="server" />
                    </ItemTemplate>
                   
                </asp:TemplateField>--%>
                <div>
          
                <asp:GridView ID="grdSearch" runat="server" CssClass="gridview" 
                    AllowPaging="True" AutoGenerateColumns="False" 
                    AllowSorting="True" Width="100%" 
                     PageSize="5" OnPageIndexChanging="grdSearch_PageIndexChanging" OnRowDataBound="grdSearch_RowDataBound" OnSelectedIndexChanged="grdSearch_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" />
                     <Columns>
                    <asp:BoundField DataField="PERNR" HeaderText="Employee ID" />
                    <asp:BoundField DataField="ENAME" HeaderText="Employee Name" />
                    <asp:BoundField DataField="BEGDA" HeaderText="From Date" DataFormatString="{0:dd-MMM-yyyy}" />
                    <asp:BoundField DataField="ENDDA" HeaderText="To Date" DataFormatString="{0:dd-MMM-yyyy}" />
                    <asp:BoundField DataField="PLSXT" HeaderText="Designation" />
                    <asp:BoundField DataField="WERKS" HeaderText="State Name" />
                    <asp:BoundField DataField="STEXT" HeaderText="Head quarters" />
                    <asp:BoundField DataField="REPORT_TO" HeaderText="Reports to" />
                    <asp:BoundField DataField="" HeaderText="Company Name" Visible="false"/>
                    <asp:BoundField DataField="PANDT" HeaderText="Total Tel Exp" />
                    <asp:BoundField DataField="TA_DEDUCTION" HeaderText="Total TA Exp" />
                    <asp:BoundField DataField="DA_DEDUCTION" HeaderText="Total DA Exp" />
                    <asp:BoundField DataField="OTR_EXP_DEDUCT" HeaderText="Total OTH Exp" />
                    <asp:BoundField DataField="GRAND_TOTAL" HeaderText="Actual Claimed" />
                    <asp:BoundField DataField="TOT_DEDUC" HeaderText="Actual Disallowed" />
                    <asp:BoundField DataField="AMOUNT_ALLOWED" HeaderText="Actual Allowed" />
                    <%--<asp:BoundField DataField="GRAND_TOTAL" HeaderText="Amount" />--%>

            </Columns>
                    <EditRowStyle BackColor="#2461BF" />

<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="12pt" Font-Names="verdana" />

<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />

<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />

<RowStyle BackColor="#EFF3FB" />

<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
                 
            </div>
             <%--           <asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                  
              
                               </td>
                               </tr>
                         
                       </table>
                       
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                             
                      <asp:CheckBox ID="CheckBox2" runat="server" />
                    </ItemTemplate>
                   
                </asp:TemplateField>--%>
         </div>

        
   </div>



         <div class="clear"></div>
        <br />
                <div class="legend">Entry </div>
        <div class="clear"></div>
        <div style="float: left;">
        <asp:Label ID="Label3" runat="server" Text="Employee ID" CssClass="label" ></asp:Label>
        <asp:TextBox ID="txtEmployeeID" runat="server" CssClass="textbox" ></asp:TextBox><br />
        <asp:Label ID="Label11" runat="server" Text="From" CssClass="label"></asp:Label>
        <asp:TextBox ID="txtFromDate" runat="server" Width="166px"  CssClass="textbox" ></asp:TextBox>
        <AjaxToolkit:CalendarExtender ID="CalenderExtender" runat="server" PopupButtonID="BirthDateImageButton" TargetControlID="txtFromDate" Format="dd-MMM-yyyy"></AjaxToolkit:CalendarExtender>
        <asp:ImageButton ID="BirthDateImageButton" runat="server" ImageUrl="~/images/calendar_icon.png" CssClass="gncontrol" Width="24" Height="22" /> <br/>
            
        <asp:Label ID="Label12" runat="server" Text="To" CssClass="label"></asp:Label>            
        <asp:TextBox ID="txtToDate" runat="server" Width="166px"  CssClass="textbox" ></asp:TextBox>
        <AjaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="BirthDateImageButton1" TargetControlID="txtToDate" Format="dd-MMM-yyyy"></AjaxToolkit:CalendarExtender>
        <asp:ImageButton ID="BirthDateImageButton1" runat="server" ImageUrl="~/images/calendar_icon.png" CssClass="gncontrol" Width="24" Height="22" /> 
           
        <asp:Button ID="btnNext" runat="server" Text="Next" Width="60px" OnClick="btnNext_Click" />
        <asp:Button ID="btnCancel1" runat="server" Text="Cancel" Width="60px" OnClick="btnCancel1_Click" />
        <br />

       <div id="divBasicInfo" runat="server" visible="false">
        <asp:Label ID="Label1" runat="server" Text="Name" CssClass="label"></asp:Label>
        <asp:TextBox ID="txtEmpName" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox><br />
      
        <asp:Label ID="Label2" runat="server" Text="Designation" CssClass="label" ></asp:Label>
        <asp:TextBox ID="txtDesignation" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>

            <br />
        <asp:Label ID="Label4" runat="server" Text="Report to" CssClass="label" ></asp:Label> 
        <asp:TextBox ID="txtReportTo" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox><br />
      </div> 
        </div>
     
        <div class="clear"> <br /> </div>

         <div id="job1" runat="server" visible="false">  <%--           <asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                  
              
                               </td>
                               </tr>
                         
                       </table>
                       
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                             
                      <asp:CheckBox ID="CheckBox2" runat="server" />
                    </ItemTemplate>
                   
                </asp:TemplateField>--%>
                   <asp:UpdatePanel ID="uppnlGrd" runat="server" >
               <ContentTemplate>
         <asp:GridView ID="grdExpenseStatement" runat="server" AllowPaging="true" AutoGenerateColumns="false"  Width="959px" PageSize="31"  >
            <Columns>
      <%--           <asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                  
              
                               </td>
                               </tr>
                         
                       </table>
                       
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                             
                      <asp:CheckBox ID="CheckBox2" runat="server" />
                    </ItemTemplate>
                   
                </asp:TemplateField>--%>
               <asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                   Date
                               </td>
                               </tr>
                          
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                             
                       <asp:Label ID="lblDate" runat="server" Text="01-Mar-2014" Width="80"></asp:Label>
                    </ItemTemplate>
                   
                </asp:TemplateField>
                <asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                  Category
                               </td>
                               <td>
                                  
                               </td>
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                             
                       <asp:TextBox ID="txtCategory" Width="60" runat="server"></asp:TextBox>
                    </ItemTemplate>
                   
                </asp:TemplateField>
                 <asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                 
                               </td>
                               <td>
                                     <%--<asp:CheckBox ID="CheckBox3" runat="server" />--%>
                               </td>
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                         <asp:CheckBox ID="chkbxDA" runat="server" AutoPostBack="true"  OnCheckedChanged ="chkbxDA_CheckedChanged" />
                    </ItemTemplate>
                   
                </asp:TemplateField>
                <asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                 Actual DA
                               </td>
                               <td>
                                    
                               </td>
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                       
                         
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtActualDA">
        </cc1:FilteredTextBoxExtender>
                       <asp:TextBox ID="txtActualDA" Width="80" runat="server" Text="0" ></asp:TextBox>
                    </ItemTemplate>
                   
                </asp:TemplateField>
               
                <asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                 Allowed DA
                               </td>
                               <td>
                                    
                               </td>
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                       
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtAllowedDA">
        </cc1:FilteredTextBoxExtender>
                       <asp:TextBox ID="txtAllowedDA" Width="80" runat="server" Text="0"  AutoPostBack="True" OnTextChanged="txtAllowedDA_TextChanged"  ></asp:TextBox>
                    </ItemTemplate>
                   
                </asp:TemplateField>

                  <asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                 Disallowed DA
                               </td>
                               <td>
                                    
                               </td>
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                       
                       <asp:TextBox ID="txtEligibleDA" Width="100" runat="server" Text="0"></asp:TextBox>
                    </ItemTemplate>
                   
                </asp:TemplateField>


                <asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                 Mode 
                               </td>
                               <td>
                                    
                               </td>
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                       
                       <asp:DropDownList ID="drpdwnMode" runat="server">
                         <asp:ListItem Text="Select" ></asp:ListItem>
                         <asp:ListItem Text="None" ></asp:ListItem>
                           <asp:ListItem Text="Rail" ></asp:ListItem><%--newly added--%>
                           <asp:ListItem Text="Road" ></asp:ListItem>
                           <asp:ListItem Text="Air" ></asp:ListItem><%--newly added--%>
                           </asp:DropDownList>

                    </ItemTemplate>
                   
                </asp:TemplateField>
                <asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                
                               </td>
                               <td>
                                   <%--<asp:CheckBox ID="CheckBox5" runat="server" />--%>
                               </td>
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>     
                    <asp:CheckBox ID="chkbxTA" runat="server" AutoPostBack="true" OnCheckedChanged="chkbxTA_CheckedChanged"  />
                    </ItemTemplate>
                   
                </asp:TemplateField>
                 <asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                Actual TA
                               </td>
                               <td>
                                  
                               </td>
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                       
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtActualTA">
        </cc1:FilteredTextBoxExtender>
                    <asp:TextBox ID="txtActualTA" Width="80" runat="server" Text="0" ></asp:TextBox>
                    </ItemTemplate>
                   
                </asp:TemplateField>
                 <asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                Allowed TA
                               </td>
                               <td>
                                  
                               </td>
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                       
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtAllowedTA">
        </cc1:FilteredTextBoxExtender>
                    <asp:TextBox ID="txtAllowedTA" Width="80" runat="server" Text="0"   AutoPostBack="True" OnTextChanged="txtAllowedTA_TextChanged" ></asp:TextBox>
                    </ItemTemplate>
                   
                </asp:TemplateField>

                  <asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                 Disallowed TA
                               </td>
                               <td>
                                    
                               </td>
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                       
                       <asp:TextBox ID="txtEligibleTA" Width="100" runat="server" Text="0"   ></asp:TextBox>
                    </ItemTemplate>
                   
                </asp:TemplateField>

                 <asp:TemplateField>
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td align="center">
                                  Remarks
                               </td>
                               
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                             
                      <%-- <asp:TextBox ID="txtRemarks" Width="120" runat="server"></asp:TextBox>--%>
                         <asp:DropDownList ID="drpdwnRemarks" runat="server" Width="400">
                            <asp:ListItem Text="" ></asp:ListItem>
                            
                            <asp:ListItem Text="Lodging   Bill  Not  Attached"></asp:ListItem>
                            <asp:ListItem Text="Excess  Amount  Claimed  In  Fare"></asp:ListItem>
                            <asp:ListItem Text="Bills  Not  Attached"></asp:ListItem>
                            <asp:ListItem Text="Kilometer  Not Mentioned"></asp:ListItem>
                            <asp:ListItem Text="Telephone Bill Not  Attached"></asp:ListItem>
                            <asp:ListItem Text="Excess  Amount Claimed In Telephone Charges"></asp:ListItem>
                            <asp:ListItem Text="Total  Error"></asp:ListItem>
                            <asp:ListItem Text="Excess  Amount Claimed  In  DA"></asp:ListItem>
                            <asp:ListItem Text="Limit Crossed In Others"></asp:ListItem>
                            <asp:ListItem Text="Bill Is Not Attached For Telephone"></asp:ListItem>
                            <asp:ListItem Text="Bill Is Not Attached For Lodging and Boarding"></asp:ListItem>
                            <asp:ListItem Text="Bill Is Not Attached For Other Expenses"></asp:ListItem>
                            <asp:ListItem Text="DA Amount Claimed Over The Limits"></asp:ListItem>
                            <asp:ListItem Text="TA Amount Claimed Over The Limits"></asp:ListItem>
                            <asp:ListItem Text="Telephone Charges  Claimed Over The Limits"></asp:ListItem>
                            <asp:ListItem Text="Other Expenses  Claimed Over The Limits"></asp:ListItem>
                            <asp:ListItem Text="Ticket Not Attached"></asp:ListItem>
                            <asp:ListItem Text="Excess Claimed in Distance"></asp:ListItem>
                            <asp:ListItem Text="TA is not allowed in HQ"></asp:ListItem>
                            <asp:ListItem Text="The same has been paid in salary"></asp:ListItem>
                            <asp:ListItem Text="Bill credentials are incorrect"></asp:ListItem>
                            <asp:ListItem Text="Lodging   Bill  Not  Attached + Excess  Amount Claimed  In  DA"></asp:ListItem>
                            <asp:ListItem Text="Excess Claimed in Distance + Excess  Amount Claimed  In  DA"></asp:ListItem>
                            <asp:ListItem Text="Excess  Amount  Claimed  In  Fare + Excess  Amount Claimed  In  DA"></asp:ListItem>
                         </asp:DropDownList>
                    </ItemTemplate>
                   
                </asp:TemplateField>

                </Columns>
        </asp:GridView>
                   </ContentTemplate>
                       </asp:UpdatePanel>
                  </div>

         <%--<asp:CheckBox ID="CheckBox3" runat="server" />--%><%--newly added--%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
         <asp:Label ID="lblgvtot" runat="server" style="font-weight: 700" Text=" Total:" Visible="False"></asp:Label>
         &nbsp;
         <asp:Label ID="lblActDATot" runat="server" style="font-weight: 700"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Label ID="lblAllDATot" runat="server" style="font-weight: 700"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Label ID="lblDisDATot" runat="server" style="font-weight: 700"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Label ID="lblActTATot" runat="server" style="font-weight: 700"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Label ID="lblAllTATot" runat="server" style="font-weight: 700"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Label ID="lblDisTATot" runat="server" style="font-weight: 700"></asp:Label>
&nbsp;
         <div class="clear"> <br /> </div>
            <div id="job" runat="server" visible="false" >
       
 
     <%--newly added--%>
           

        <div class="legend">Other Claims</div>
           <div class="clear"> <br /> </div>
           
                          <%--<asp:CheckBox ID="CheckBox5" runat="server" />--%>                     <%-- <asp:TextBox ID="txtRemarks" Width="120" runat="server"></asp:TextBox>--%>

            <div style="float: left; width:660px;">
                          <%--<asp:CheckBox ID="CheckBox3" runat="server" />--%>
        <asp:Label ID="Label18" runat="server" Text="Type of Expenses   " Font-Bold ="true" CssClass="label" Width="230px" ></asp:Label>       
        <asp:Label ID="Label20" runat="server" Text="   Actual" Font-Bold="true"  CssClass="label" Width="90px"></asp:Label>
        <asp:Label ID="Label19" runat="server" Text="   Allowed" Font-Bold="True"  CssClass="label"  Width="90px" ></asp:Label>  
        <asp:Label ID="Label6" runat="server" Text="Disallowed" Font-Bold="True"  CssClass="label"  ></asp:Label>                     
        <br />        

        <asp:Label ID="lblStationaries" runat="server" Text="Stationaries :" CssClass="label" Width="200px"  ></asp:Label>     
         
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtStationaries">
        </cc1:FilteredTextBoxExtender>                   
        <asp:TextBox ID="txtStationaries" runat="server" CssClass="textbox" Text="0" Height="22px"   Width="100px" ></asp:TextBox>
                          <%--newly added--%> 
                 <asp:TextBox ID="txtAllowedStationaries" runat="server" CssClass="textbox" Text="0" Height="22px"  Width="100px" AutoPostBack="True" OnTextChanged="txtAllowedStationaries_TextChanged"  ></asp:TextBox>
         
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtStationaries">
        </cc1:FilteredTextBoxExtender>                   
        &nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Label ID="lblDisAllowedStationaries" runat="server" Text="0.0" Font-Bold="True"  CssClass="label"  ></asp:Label> 
                <br />
        <asp:Label ID="lblCourier" runat="server" Text="Courier :" CssClass="label"  Width="200px" ></asp:Label>
       
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtCourier">
        </cc1:FilteredTextBoxExtender>
        <asp:TextBox ID="txtCourier" runat="server" CssClass="textbox"  Text="0"   Width="100px"></asp:TextBox>
                          <%--newly added--%>
                <asp:TextBox ID="txtAllowedCourier" runat="server" CssClass="textbox" Text="0" Height="22px"   Width="100px" AutoPostBack="True" OnTextChanged="txtAllowedCourier_TextChanged"  ></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblDisAllowedCourier" runat="server" Text="0.0" Font-Bold="True"  CssClass="label" ></asp:Label> 
                <br />
        <asp:Label ID="lblPAndT" runat="server" Text="Telephone Expenses :" CssClass="label"  Width="200px" ></asp:Label>
       
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtPAndT">
        </cc1:FilteredTextBoxExtender>
        <asp:TextBox ID="txtPAndT" runat="server" CssClass="textbox"  Text="0"   Width="100px"></asp:TextBox>
                          <%--<asp:CheckBox ID="CheckBox5" runat="server" />--%>
                 <asp:TextBox ID="txtAllowedPAndT" runat="server" CssClass="textbox" Text="0" Height="22px"   Width="100px" AutoPostBack="True" OnTextChanged="txtAllowedPAndT_TextChanged"  ></asp:TextBox>
                 &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblDisAllowedPAndT" runat="server" Text="0.0" Font-Bold="True"  CssClass="label" ></asp:Label> 
                <br />
        <asp:Label ID="lblEmail" runat="server" Text="Internet : " CssClass="label" Width="200px"  ></asp:Label>
        
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtEmail">
        </cc1:FilteredTextBoxExtender>
        <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" Text="0"    Width="100px"></asp:TextBox>
                          <%-- <asp:TextBox ID="txtRemarks" Width="120" runat="server"></asp:TextBox>--%>
                 <asp:TextBox ID="txtAllowedEmail" runat="server" CssClass="textbox" Text="0" Height="22px"   Width="100px" AutoPostBack="True" OnTextChanged="txtAllowedEmail_TextChanged"  ></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Label ID="lblDisAllowedEmail" runat="server" Text="0.0" Font-Bold="True"  CssClass="label"  ></asp:Label> 
                <br />
        <asp:Label ID="lblCompitatorProductPurchase" runat="server" Text="Compitator product purchase :" CssClass="label"  Width="200px" ></asp:Label>
        
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtCompitatorProductPurchase">
        </cc1:FilteredTextBoxExtender>
        <asp:TextBox ID="txtCompitatorProductPurchase" runat="server" CssClass="textbox" Text="0"    Width="100px"></asp:TextBox>
                          <%--<asp:CheckBox ID="CheckBox3" runat="server" />--%>
                <asp:TextBox ID="txtAllowedCompitatorProductPurchase" runat="server" CssClass="textbox" Text="0" Height="22px"   Width="100px" AutoPostBack="True" OnTextChanged="txtAllowedCompitatorProductPurchase_TextChanged"  ></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblDisAllowedCompitatorProductPurchase" runat="server" Text="0.0" Font-Bold="True"  CssClass="label"  ></asp:Label> 
                 <br />
        
        <asp:Label ID="lblMarketDevelopmentExpences" runat="server" Text="Market Development Expences :" CssClass="label" Width="200px"  ></asp:Label>
        
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtMarketDevelopmentExpences">
        </cc1:FilteredTextBoxExtender>
        <asp:TextBox ID="txtMarketDevelopmentExpences" runat="server" CssClass="textbox" Text="0"    Width="100px"></asp:TextBox>
                          <%--newly added--%>
                <asp:TextBox ID="txtAllowedMarketDevelopmentExpences" runat="server" CssClass="textbox" Text="0" Height="22px"   Width="100px" AutoPostBack="True" OnTextChanged="txtAllowedMarketDevelopmentExpences_TextChanged"  ></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblDisAllowedMarketDevelopmentExpences" runat="server" Text="0.0" Font-Bold="True"  CssClass="label"></asp:Label> 
                 <br />
        <asp:Label ID="lblBusPass" runat="server" Text="Bus / Train Pass :" CssClass="label" Width="200px"  ></asp:Label>
       
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtBusPass">
        </cc1:FilteredTextBoxExtender>
        <asp:TextBox ID="txtBusPass" runat="server" CssClass="textbox" Text="0"    Width="100px"></asp:TextBox>
                          <%--newly added--%>
                 <asp:TextBox ID="txtAllowedBusPass" runat="server" CssClass="textbox" Text="0" Height="22px"   Width="100px" AutoPostBack="True" OnTextChanged="txtAllowedBusPass_TextChanged"  ></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblDisAllowedBusPass" runat="server" Text="0.0" Font-Bold="True"  CssClass="label" ></asp:Label>
                 <br />
        <asp:Label ID="lblConveyance" runat="server" Text="Conveyance :" CssClass="label" Width="200px"  ></asp:Label>
       
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtConveyance">
        </cc1:FilteredTextBoxExtender>
        <asp:TextBox ID="txtConveyance" runat="server" CssClass="textbox" Text="0"    Width="100px"></asp:TextBox>
                          <%--<asp:CheckBox ID="CheckBox5" runat="server" />--%>
                 <asp:TextBox ID="txtAllowedConveyance" runat="server" CssClass="textbox" Text="0" Height="22px"   Width="100px" AutoPostBack="True" OnTextChanged="txtAllowedConveyance_TextChanged"  ></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Label ID="lblDisAllowedConveyance" runat="server" Text="0.0" Font-Bold="True"  CssClass="label"></asp:Label> 
                 <br />
        <asp:Label ID="lblJcMeetings" runat="server" Text="Jc Meetings :" CssClass="label" Width="200px"  ></asp:Label>
       
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtJcMeetings">
        </cc1:FilteredTextBoxExtender>
        <asp:TextBox ID="txtJcMeetings" runat="server" CssClass="textbox"  Text="0"   Width="100px"></asp:TextBox> 
                          <%-- <asp:TextBox ID="txtRemarks" Width="120" runat="server"></asp:TextBox>--%>
                 <asp:TextBox ID="txtAllowedJcMeetings" runat="server" CssClass="textbox" Text="0" Height="22px"   Width="100px" AutoPostBack="True" OnTextChanged="txtAllowedJcMeetings_TextChanged"  ></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblDisAllowedJcMeetings" runat="server" Text="0.0" Font-Bold="True"  CssClass="label"></asp:Label> 
                <br />  
                <asp:Label ID="lblOthers" runat="server" Text="Others :" CssClass="label"  Width="200px" ></asp:Label>
       
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtOthers">
        </cc1:FilteredTextBoxExtender>
        <asp:TextBox ID="txtOthers" runat="server" CssClass="textbox" Text="0"    Width="100px"></asp:TextBox> 
                          <%--<asp:CheckBox ID="CheckBox3" runat="server" />--%>
                 <asp:TextBox ID="txtAllowedOthers" runat="server" CssClass="textbox" Text="0"   Width="100px" Height="22px"  ></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Label ID="lblDisAllowedOthers" runat="server" Text="0.0" Font-Bold="True"  CssClass="label"></asp:Label> 
                <br />
                <hr style="text-align:left;width:513px;"/>
                <asp:Label ID="lblTotal" runat="server" Text="Total :" CssClass="label"  Width="200px" ></asp:Label>
       
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtTotal">
        </cc1:FilteredTextBoxExtender>
        <asp:TextBox ID="txtTotal" runat="server" CssClass="textbox" Text="0"   Width="100px" ></asp:TextBox> 
                          <%--newly added--%>
                 <asp:TextBox ID="txtAllowed" runat="server" CssClass="textbox" Text="0" Height="22px"    Width="100px" ></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Label ID="lblDisAllowed" runat="server" Text="0.0" Font-Bold="True"  CssClass="label"></asp:Label> 
                <br />
            </div>

                <%--newly added--%><%--<asp:CheckBox ID="CheckBox5" runat="server" />--%> 

                <div class="clear"> <br /> </div>

                 <div class="buttonrow">
             
             <asp:Button ID="btnCalculate" runat="server" Text="Calculate" Width="80px" OnClick="btnCalculate_Click"/>           
         </div>
                 <div class="legend">Grand Total </div><br /> 
                <%-- <asp:TextBox ID="txtRemarks" Width="120" runat="server"></asp:TextBox>--%> 
              
        <div style="float: left; width: 450px;">
                               <%--<asp:CheckBox ID="CheckBox3" runat="server" />--%>
             <asp:UpdatePanel ID="uppnlDA" runat="server" >
               <ContentTemplate>
        <asp:Label ID="Label16" runat="server" Text="Type of Expenses   " Font-Bold ="true" CssClass="label" Width="250px" ></asp:Label>       
        <asp:Label ID="Label17" runat="server" Text="   Value" Font-Bold="true"  CssClass="label" ></asp:Label>
           
        <asp:Label ID="Label5" runat="server" Text="Actual Amount Claimed" CssClass="label" Width="250px"></asp:Label>                   
     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtActualAmtClaimed">
        </cc1:FilteredTextBoxExtender>
        <asp:TextBox ID="txtActualAmtClaimed" runat="server" CssClass="textbox"  Text="0" ></asp:TextBox><br />

        <asp:Label ID="Label7" runat="server" Text="Deduction Recommendation from Reporting Officer" CssClass="label" Width="250px"></asp:Label>
     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtInchargePersonDeductions">
        </cc1:FilteredTextBoxExtender>
        <asp:TextBox ID="txtInchargePersonDeductions" runat="server" CssClass="textbox" Text="0"  ></asp:TextBox><br />
      
        <asp:Label ID="Label8" runat="server" Text="Eligibility Deduction" CssClass="label" Width="250px"></asp:Label>
     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtCorOfficeDeduction">
        </cc1:FilteredTextBoxExtender>
        <asp:TextBox ID="txtCorOfficeDeduction" runat="server" CssClass="textbox" Text="0" AutoPostBack="true" OnTextChanged ="txtCorOfficeDeduction_TextChanged"></asp:TextBox> <br />

        <asp:Label ID="Label9" runat="server" Text="Total Deduction" CssClass="label" Width="250px" ></asp:Label> 
        <asp:TextBox ID="txtTotalDeduction" runat="server" CssClass="textbox" Text="0" Enabled="false"  ></asp:TextBox><br />

        <asp:Label ID="Label10" runat="server" Text="Amount Allowed" CssClass="label" Width="250px" ></asp:Label>
        <asp:TextBox ID="txtAmntAllowed" runat="server" CssClass="textbox" Text="0" Enabled="false"  ></asp:TextBox>
                </ContentTemplate>
                 </asp:UpdatePanel>
       </div>



          <div class="clear"><br /></div>
     
             
        <div class="legend">Approval Person details </div>
        <div class="clear"></div>
         <div id="Panel3data">                        
         <asp:Label ID="Label13" runat="server" Text="Approved" CssClass="label" ></asp:Label>
         &nbsp;&nbsp;
         <asp:CheckBox ID="chkApproved" runat="server" AutoPostBack="True" OnCheckedChanged="chkApproved_CheckedChanged" /> <br />
         <asp:Label ID="Label14" runat="server" Text="Approved By" CssClass="label" ></asp:Label>
         &nbsp;&nbsp;&nbsp;
         <asp:TextBox ID="txtApprovedBy" runat="server" Text="" CssClass="textbox" ></asp:TextBox>
             
             
             <br />
             <%--newly added--%>
         <asp:Label ID="Label15" runat="server" Text="Approved On" CssClass="label" ></asp:Label>
          <asp:Label ID="lblApprovedOn" runat="server" Text="" CssClass="label" ></asp:Label>
             <%--newly added--%><%--<asp:CheckBox ID="CheckBox5" runat="server" />--%> 
             <br />
        </div>
     

       <div class="clear"><br /></div>      
     
             <div id="job2" runat="server" visible="false"  class="buttonrow">
                 <%-- <asp:TextBox ID="txtRemarks" Width="120" runat="server"></asp:TextBox>--%>
             <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click"/>
             <asp:Button ID="btnCancel" runat="server" Text="Cancel"/>
             <asp:Button ID="btnEdit" runat="server" Text="Edit" Visible="False"/>
                 <%--<asp:CheckBox ID="CheckBox3" runat="server" />--%>
         </div>
                   
     <div class="clear"></div>
    </div>
  </div> 
</asp:Content>
