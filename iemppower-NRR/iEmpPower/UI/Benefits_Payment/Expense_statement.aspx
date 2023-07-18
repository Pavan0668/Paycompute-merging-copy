<%@ Page Title="Expense statement" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Expense_statement.aspx.cs" Inherits="iEmpPower.UI.Local_requisition.Expense_statement" Theme="SkinFile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls" TagPrefix="BDP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
         });


       
         </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
         <h2>Expense statement</h2>
         <asp:Label ID="lblMessageBoard" runat="server"  CssClass="msgboard" ></asp:Label><br />

        <div class="legend">Entry </div>
        <div class="clear"></div>
        <div style="float: left">
        <asp:Label ID="lblEmployeeID" runat="server" Text="Employee ID" CssClass="label" ></asp:Label>            
     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtEmployeeID">
        </cc1:FilteredTextBoxExtender>
        <asp:TextBox ID="txtEmployeeID" runat="server" CssClass="textbox" ></asp:TextBox>
            <%-- AutoPostBack="true" OnSelectedIndexChanged ="StateDropDownList_SelectedIndexChanged">--%><br />
        <asp:Label ID="lblFrom" runat="server" Text="From" CssClass="label"></asp:Label>
        <asp:TextBox ID="txtFromDate" runat="server" Width="170px"  CssClass="textbox" ></asp:TextBox>
        <AjaxToolkit:CalendarExtender ID="CalExtFrom" runat="server" PopupButtonID="FromDateImageButton" TargetControlID="txtFromDate" Format="dd-MMM-yyyy"></AjaxToolkit:CalendarExtender>
        <asp:ImageButton ID="FromDateImageButton" runat="server" ImageUrl="~/images/calendar_icon.png" CssClass="gncontrol" Width="24" Height="22" /> <br/>

        <asp:Label ID="lblTo" runat="server" Text="To" CssClass="label"></asp:Label>           
        <asp:TextBox ID="txtToDate" runat="server" Width="170px"  CssClass="textbox" ></asp:TextBox>
        <AjaxToolkit:CalendarExtender ID="CalExtTo" runat="server" PopupButtonID="ToDateImageButton" TargetControlID="txtToDate" Format="dd-MMM-yyyy"></AjaxToolkit:CalendarExtender>
        <asp:ImageButton ID="ToDateImageButton" runat="server" ImageUrl="~/images/calendar_icon.png" CssClass="gncontrol" Width="24" Height="22" />
            
        <asp:Button ID="btnNext" runat="server" Text="Next" Width="60px" OnClick="btnNext_Click"/>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="60px" OnClick="btnCancel_Click"/>
        <br />           
        
        <div id="divBasicInfo" runat="server" visible="false" >
        <asp:Label ID="lblEmpName" runat="server" Text="Name" CssClass="label" ></asp:Label>
        <asp:TextBox ID="txtEmpName" runat="server" CssClass="textbox" Enabled="false" ></asp:TextBox><br />
        <asp:Label ID="lblState" runat="server" Text="State" CssClass="label" ></asp:Label>
        <asp:TextBox ID="txtState" runat="server" CssClass="textbox" Enabled="false" ></asp:TextBox><br />
        <asp:Label ID="lblDesignation" runat="server" Text="Designation" CssClass="label" ></asp:Label>
        <asp:TextBox ID="txtDesignation" runat="server" CssClass="textbox" Enabled="false"  ></asp:TextBox><br />
        <asp:Label ID="lblHeadQuarters" runat="server" Text="Head quarters" CssClass="label" ></asp:Label>
        <asp:TextBox ID="txtHeadQuarters" runat="server" CssClass="textbox" Enabled="false"  ></asp:TextBox><br />
        <asp:Label ID="lblReportTo" runat="server" Text="Report To" CssClass="label" ></asp:Label>
        <asp:TextBox ID="txtReportTo" runat="server" CssClass="textbox" Enabled="false"  ></asp:TextBox> 
            <br />
            <br />
        </div>

         </div> 
        <div class="clear"></div>

          <div id="job1" runat="server" visible="false"><%--<asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                   Via
                               </td>
                              
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>            
                        <asp:DropDownList ID="ViaDropDownList" runat="server">

                        </asp:DropDownList>
                    </ItemTemplate>
                   
                </asp:TemplateField>--%>
             <asp:UpdatePanel ID="ippnlGrd" runat="server" >
               <ContentTemplate>
         <asp:GridView ID="grdExpenseStatement" runat="server" AutoGenerateColumns="false" Width="1300px" HeaderStyle-HorizontalAlign="Center">
            <Columns>
               <asp:TemplateField>
                   <HeaderTemplate >
                       <table>
                           <tr>
                               <td>
                                   Date
                               </td>
                               </tr>
                         
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                             
                       <asp:Label ID="lblDate" runat="server" Width="80" Text="01-Mar-2014"></asp:Label>
                    </ItemTemplate>
                   
                </asp:TemplateField>

                   <asp:TemplateField  Visible="false">
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                   To State
                               </td>
                              
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>            
                        <asp:DropDownList ID="StateDropDownList" runat="server"  Width="80">  <%-- AutoPostBack="true" OnSelectedIndexChanged ="StateDropDownList_SelectedIndexChanged">--%>
                        </asp:DropDownList>
                    </ItemTemplate>
                   
                </asp:TemplateField> 


                    <asp:TemplateField>
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                  Travel From
                               </td>
                               
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                       <asp:DropDownList ID="TravelFromDropDownList" runat="server"  Width="80"></asp:DropDownList>
                     
                    </ItemTemplate>
                   
                </asp:TemplateField>

                
             
                 <%--<asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                   Via
                               </td>
                              
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>            
                        <asp:DropDownList ID="ViaDropDownList" runat="server">

                        </asp:DropDownList>
                    </ItemTemplate>
                   
                </asp:TemplateField>--%>


                             

                 <asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                   Travel To
                               </td>
                              
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                             
                        <asp:DropDownList ID="TravelToDropDownList" runat="server"  Width="80"><%-- AutoPostBack="true" OnSelectedIndexChanged ="TravelToDropDownList_SelectedIndexChanged">--%></asp:DropDownList>
                    </ItemTemplate>
                   
                </asp:TemplateField>



                <asp:TemplateField>
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                   Place of Work
                               </td>
                              
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                        <asp:DropDownList ID="drpdwnPlaceWork" runat="server"  Width="80" ></asp:DropDownList>      
                     <%--  <asp:TextBox ID="txtPlaceWork" Width="120" runat="server"></asp:TextBox>--%>
                    </ItemTemplate>
                   
                </asp:TemplateField>
                 <asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                  Night Halt
                               </td>
                              
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                          <asp:DropDownList ID="drpdwnNightHalt" runat="server"  Width="50" AutoPostBack="true" OnSelectedIndexChanged ="drpdwnNightHalt_SelectedIndexChanged"></asp:DropDownList>    
                      <%-- <asp:TextBox ID="txtNightHalt" Width="120" runat="server"></asp:TextBox>--%>
                    </ItemTemplate>
                   
                </asp:TemplateField>
                 <%--<asp:TemplateField>
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                   State
                               </td>
                               
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                       <asp:DropDownList ID="drpdwnState" runat="server" > </asp:DropDownList>                      
                    </ItemTemplate>
                   
                </asp:TemplateField>--%>
                 <asp:TemplateField>
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                  Type of Place     <%-- Before it was Category--%>
                               </td>                               
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                             
                         <asp:DropDownList ID="CategoryDropDownList" runat="server"  Width="60" >
                              <%--<asp:ListItem Text="Select" ></asp:ListItem>         
                              <asp:ListItem Text="BORD" ></asp:ListItem>
                              <asp:ListItem Text="LODG" ></asp:ListItem>--%>
                         </asp:DropDownList>
                    </ItemTemplate>
                   
                </asp:TemplateField>
                 <asp:TemplateField>
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                  Category of Place
                               </td>                               
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                             
                         <asp:DropDownList ID="CategoryofplaceDropDownList" runat="server" Width="60">
                              <asp:ListItem Text="Select" ></asp:ListItem>         
                              <asp:ListItem Text="Metro" ></asp:ListItem>
                              <asp:ListItem Text="C1" ></asp:ListItem>
                              <asp:ListItem Text="C2" ></asp:ListItem>
                              <asp:ListItem Text="Hill Station" ></asp:ListItem>
                              <asp:ListItem Text="Guest Policy" ></asp:ListItem>
                         </asp:DropDownList>
                    </ItemTemplate>
                   
                </asp:TemplateField>
               
                 <asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                  Distance in kms
                               </td>
                               
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                             
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtDistance">
        </cc1:FilteredTextBoxExtender>
                       <asp:TextBox ID="txtDistance" Width="50" runat="server"  AutoPostBack="True" OnTextChanged="txtDistance_TextChanged"   Text="0" ></asp:TextBox>
                    </ItemTemplate>
                   
                </asp:TemplateField>
                 <asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                   Mode
                               </td>
                               
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                       <asp:DropDownList ID="drpdwnMode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpdwnMode_SelectedIndexChanged"   Width="60">
                             <asp:ListItem Text="Select" ></asp:ListItem>
                            <%-- <asp:ListItem Text="None" ></asp:ListItem>--%>
                             <asp:ListItem Text="Rail" ></asp:ListItem>
                             <asp:ListItem Text="Road" Selected="True"></asp:ListItem>
                             <asp:ListItem Text="Air" ></asp:ListItem>
                       </asp:DropDownList>
                      
                    </ItemTemplate>
                   
                </asp:TemplateField>
                 <asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                  Tickets Prod
                               </td>
                              
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                             
                       <asp:CheckBox ID="chkTicketProd" runat="server" />
                    </ItemTemplate>
                   
                </asp:TemplateField>
                 <asp:TemplateField   >
                   <HeaderTemplate>
                        <table>
                           <tr>
                               <td>
                                   Total Fare
                               </td>
                               </tr>
                          
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                             
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtFare">
        </cc1:FilteredTextBoxExtender>
                       <asp:TextBox ID="txtFare" Width="50" runat="server" AutoPostBack="True" OnTextChanged="txtFare_TextChanged"  Text="0" ></asp:TextBox>
                    </ItemTemplate>
                   
                </asp:TemplateField>
                 <asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                   DA-Boarding Amount
                               </td>
                               </tr>
                         
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                             
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtDAgrd">
        </cc1:FilteredTextBoxExtender>
                       <asp:TextBox ID="txtDAgrd" Width="50" runat="server" AutoPostBack="True" OnTextChanged="txtDAgrd_TextChanged" Text="0" ></asp:TextBox>
                    </ItemTemplate>
                   
                </asp:TemplateField>
                 <asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                   DA-Lodging Bills
                               </td>                              
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                               <asp:CheckBox ID="chkLodgeBills" runat="server" /> 
                    </ItemTemplate>
                   
                </asp:TemplateField>
                 <asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                  DA-Lodging Bill Amount
                               </td>
                              
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                             
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderr14" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtBillAmount">
        </cc1:FilteredTextBoxExtender>
                       <asp:TextBox ID="txtBillAmount" Width="50" runat="server" AutoPostBack="True" OnTextChanged="txtBillAmount_TextChanged" Text="0"></asp:TextBox>
                    </ItemTemplate>
                   
                </asp:TemplateField>
              <%--   <asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                   Others
                               </td>
                               </tr>
                          
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                             
                       <asp:TextBox ID="txtOthers" Width="70" runat="server"></asp:TextBox>
                    </ItemTemplate>
                   
                </asp:TemplateField>--%>
                 <asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                   Total
                               </td>
                               </tr>
                          
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                             
                       <asp:TextBox ID="txtTotal" Width="50" runat="server" Enabled="false" Text="0"></asp:TextBox>
                    </ItemTemplate>
                     <FooterTemplate>
                         
                     </FooterTemplate>
                   
                </asp:TemplateField>
                 <asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                  Remarks
                               </td>
                               
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                             
                       <asp:TextBox ID="txtRemarks" Width="80" runat="server"></asp:TextBox>
                    </ItemTemplate>
                   
                </asp:TemplateField>
                </Columns>

        </asp:GridView>
                   
                   </ContentTemplate>
                 </asp:UpdatePanel><br />
            <br />
  </div>

         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
         <asp:Label ID="lblgvtot" runat="server" style="font-weight: 700" Text=" Total:" Visible="False"></asp:Label>
         &nbsp;&nbsp;&nbsp;
         &nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Label ID="lblTotfare" runat="server" style="font-weight: 700"></asp:Label>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Label ID="lblTotDABoard" runat="server" style="font-weight: 700"></asp:Label>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
         <asp:Label ID="lblTotDALodg" runat="server" style="font-weight: 700"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTotGrid" runat="server" style="font-weight: 700"></asp:Label>
&nbsp;<div class="clear"></div>

<div id="job2" runat="server" visible="false" >
      <div class="legend">Other Claims</div>
        <div class="clear"></div>
      <%-- AutoPostBack="true" OnSelectedIndexChanged ="TravelToDropDownList_SelectedIndexChanged">--%>                           
  <%--  <asp:TextBox ID="txtPlaceWork" Width="120" runat="server"></asp:TextBox>--%>
        
        <asp:Label ID="lblStationaries" runat="server" Text="Stationaries :" CssClass="label" Width="200px"  ></asp:Label>
            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtStationaries">
        </cc1:FilteredTextBoxExtender>
        <asp:TextBox ID="txtStationaries" runat="server" CssClass="textbox"  Text="0" ></asp:TextBox><br />
        <asp:Label ID="lblCourier" runat="server" Text="Courier :" CssClass="label"  Width="200px" ></asp:Label>   
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtCourier">
        </cc1:FilteredTextBoxExtender>
        <asp:TextBox ID="txtCourier" runat="server" CssClass="textbox" Text="0"  ></asp:TextBox><br />
        <asp:Label ID="lblPAndT" runat="server" Text="Telephone Expenses :" CssClass="label"  Width="200px" ></asp:Label>
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtPAndT">
        </cc1:FilteredTextBoxExtender>
        <asp:TextBox ID="txtPAndT" runat="server" CssClass="textbox"  Text="0" ></asp:TextBox><br />
        <asp:Label ID="lblEmail" runat="server" Text="Internet : " CssClass="label" Width="200px"  ></asp:Label>
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtEmail">
        </cc1:FilteredTextBoxExtender>
        <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" Text="0"  ></asp:TextBox><br />
        <asp:Label ID="lblCompitatorProductPurchase" runat="server" Text="Competitor product purchase :" CssClass="label"  Width="200px" ></asp:Label>
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtCompitatorProductPurchase">
        </cc1:FilteredTextBoxExtender>
        <asp:TextBox ID="txtCompitatorProductPurchase" runat="server" CssClass="textbox"  Text="0" ></asp:TextBox> <br />
       
        <asp:Label ID="lblMarketDevelopmentExpences" runat="server" Text="Market Development Expenses :" CssClass="label" Width="200px"  ></asp:Label>           
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtMarketDevelopmentExpences">
        </cc1:FilteredTextBoxExtender>
        <asp:TextBox ID="txtMarketDevelopmentExpences" runat="server" CssClass="textbox" Text="0"  ></asp:TextBox> <br />
        <asp:Label ID="lblBusPass" runat="server" Text="Bus / Train Pass :" CssClass="label" Width="200px"  ></asp:Label>
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtBusPass">
        </cc1:FilteredTextBoxExtender>
        <asp:TextBox ID="txtBusPass" runat="server" CssClass="textbox"  Text="0" ></asp:TextBox> <br />
        <asp:Label ID="lblConveyance" runat="server" Text="Conveyance :" CssClass="label" Width="200px"  ></asp:Label>
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtConveyance">
        </cc1:FilteredTextBoxExtender>
        <asp:TextBox ID="txtConveyance" runat="server" CssClass="textbox" Text="0"  ></asp:TextBox> <br />
        <asp:Label ID="lblJcMeetings" runat="server" Text="Jc Meetings :" CssClass="label" Width="200px"  ></asp:Label>
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtJcMeetings">
        </cc1:FilteredTextBoxExtender>
        <asp:TextBox ID="txtJcMeetings" runat="server" CssClass="textbox" Text="0"  ></asp:TextBox> <br /> 
            <asp:Label ID="lblOthers" runat="server" Text="Others :" CssClass="label"  Width="200px" ></asp:Label>
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtOthers">
        </cc1:FilteredTextBoxExtender>
        <asp:TextBox ID="txtOthers" runat="server" CssClass="textbox" Text="0"  ></asp:TextBox>         
      <%-- <asp:TextBox ID="txtNightHalt" Width="120" runat="server"></asp:TextBox>--%>
        </div>
        </div>
              <div class="clear"></div>
       <div id="job" runat="server" visible="false" >
      <div class="legend">Deduction Recommendation from Reporting Officer</div>
        <div class="clear"></div>
           <%--<asp:TemplateField>
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                   State
                               </td>
                               
                           </tr>
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                       <asp:DropDownList ID="drpdwnState" runat="server" > </asp:DropDownList>                      
                    </ItemTemplate>
                   
                </asp:TemplateField>--%>                                    
            <%-- Before it was Category--%>
           <asp:UpdatePanel ID="uppnlDA" runat="server" >
               <ContentTemplate>
        <asp:Label ID="lblDA" runat="server" Text="DA :" CssClass="label" Width="200px"  ></asp:Label>
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtDA">
        </cc1:FilteredTextBoxExtender>   
        <asp:TextBox ID="txtDA" runat="server" CssClass="textbox" AutoPostBack="true" Text="0" OnTextChanged="txtDA_TextChanged"></asp:TextBox><br />
        <asp:Label ID="lblTA" runat="server" Text="TA :" CssClass="label"  Width="200px" ></asp:Label>
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtTA">
        </cc1:FilteredTextBoxExtender> 
        <asp:TextBox ID="txtTA" runat="server" CssClass="textbox" AutoPostBack="true" Text="0"  OnTextChanged="txtTA_TextChanged"></asp:TextBox><br />
        <asp:Label ID="lblOtherExpDeduc" runat="server" Text="Other Expenses :" CssClass="label" Width="200px" ></asp:Label>                    
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtOtherExpDeduc">
        </cc1:FilteredTextBoxExtender>
        <asp:TextBox ID="txtOtherExpDeduc" runat="server" CssClass="textbox" AutoPostBack="true" Text="0" OnTextChanged="txtOtherExpDeduc_TextChanged" ></asp:TextBox><br />
       <%-- <asp:Label ID="Label9" runat="server" Text="Teliphone Deductions : " CssClass="label"></asp:Label>
        <asp:TextBox ID="TextBox8" runat="server" CssClass="textbox" ></asp:TextBox><br />--%>
        <asp:Label ID="lblTotDeduc" runat="server" Text="Total Deductions :" CssClass="label" Width="200px"></asp:Label>
        <asp:TextBox ID="txtTotInchgPersDeduc" runat="server" CssClass="textbox" Enabled="false" Text="0"></asp:TextBox>
           </ContentTemplate>
           </asp:UpdatePanel>
           <%-- <asp:ListItem Text="None" ></asp:ListItem>--%>       <%--   <asp:TemplateField   >
                   <HeaderTemplate>
                       <table>
                           <tr>
                               <td>
                                   Others
                               </td>
                               </tr>
                          
                       </table>
                      
                       </HeaderTemplate>                 
                   <ItemTemplate>           
                             
                       <asp:TextBox ID="txtOthers" Width="70" runat="server"></asp:TextBox>
                    </ItemTemplate>
                   
                </asp:TemplateField>--%>

           
            <div class="clear"></div>

           <div class="buttonrow">
             
             <asp:Button ID="btnCalculate" runat="server" Text="Calculate" Width="80px" OnClick="btnCalculate_Click"/>           
         </div>
           <div class="clear"></div>
           <span style="right :1690px;" >

            <strong>Grand&nbsp;total:</strong>
              <asp:TextBox ID="txtGrandTotal" runat="server" Enabled="false"></asp:TextBox></span>     
        <div class="clear">


          <br />
        </div>

<div class="legend">Entered by Person details </div>
        <div class="clear"></div>
         <div id="Panel3data">                        
         
         <asp:Label ID="Label14" runat="server" Text="Entered By" CssClass="label" ></asp:Label>
         &nbsp;&nbsp;&nbsp;
         <asp:TextBox ID="txtApprovedBy" runat="server" Text="" CssClass="textbox" ></asp:TextBox>
             
             
             <br />
             <%-- AutoPostBack="true" OnSelectedIndexChanged ="TravelToDropDownList_SelectedIndexChanged">--%>
         <asp:Label ID="Label15" runat="server" Text="Entered On" CssClass="label" ></asp:Label>
          <asp:Label ID="lblApprovedOn" runat="server" Text="" CssClass="label" ></asp:Label>
             <%--  <asp:TextBox ID="txtPlaceWork" Width="120" runat="server"></asp:TextBox>--%><%-- <asp:TextBox ID="txtNightHalt" Width="120" runat="server"></asp:TextBox>--%> 
             <br />
        </div>
     

       <div class="clear"><br /></div>      

      
            <div class="buttonrow">
             <asp:Button ID="btnClearRow" runat="server" Text="Clear Row" Visible="false" OnClick="btnClearRow_Click" />
             <asp:Button ID="btnSave" runat="server" Text="Save" Width="60px" OnClick="btnSave_Click" Visible="false" />           
         </div>
        </div>
</asp:Content>
