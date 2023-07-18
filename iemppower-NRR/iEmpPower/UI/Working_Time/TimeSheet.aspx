<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="TimeSheet.aspx.cs" Theme="SkinFile" Inherits="iEmpPower.UI.Working_Time.TimeSheet" %>

<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="BDP" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="../../Utilities/ValidationMessages.js" type="text/javascript"></script>
    <script src="../../Utilities/Validations.js" type="text/javascript"></script>
<script type="text/javascript">
    function ValidateEmptyValue() {
        clearDirty();
        var hours = document.getElementById("<%= hdHours.ClientID %>").value
        document.getElementById("<%= lblMessageBoard.ClientID %>").style.color = "red";
        var dd = new Array();
        //        var txt = '<%= grdRecordTime.ClientID %>';
        var gv = document.getElementById("<%= grdRecordTime.ClientID %>");
        var tb = gv.getElementsByTagName("input");


        dd = gv.getElementsByTagName("select");
        for (var i = 2; i < dd.length; i = i + 3) {
            if (dd.item(i).value == "0") {
                document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgSelectAttributeType
                dd[i].focus();
                return false;
            }
        }

        for (var j = 3; j < tb.length; j = j + 10) {
            if (tb[j].type == "text") {
                if (isNaN(tb[j].value)) {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgHoursFormat;
                    tb[j].focus();
                    return false;
                }
                if (parseFloat(tb[j].value) == "0") {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgZeroNotHours;
                    tb[j].focus();
                    return false;
                }
            }
        }
        for (var j = 4; j < tb.length; j = j + 10) {
            if (isNaN(tb[j].value)) {
                document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgHoursFormat;
                tb[j].focus();
                return false;
            }
            if (parseFloat(tb[j].value) == "0") {
                document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgZeroNotHours;
                tb[j].focus();
                return false;
            }
        }
        for (var j = 5; j < tb.length; j = j + 10) {
            if (isNaN(tb[j].value)) {
                document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgHoursFormat;
                    tb[j].focus();
                    return false;
                }
                if (parseFloat(tb[j].value) == "0") {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgZeroNotHours;
                    tb[j].focus();
                    return false;
                }
            }
            for (var j = 6; j < tb.length; j = j + 10) {
                if (isNaN(tb[j].value)) {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgHoursFormat;
                    tb[j].focus();
                    return false;
                }
                if (parseFloat(tb[j].value) == "0") {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgZeroNotHours;
                    tb[j].focus();
                    return false;
                }
            }
            for (var j = 7; j < tb.length; j = j + 10) {
                if (isNaN(tb[j].value)) {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgHoursFormat;
                    tb[j].focus();
                    return false;
                }
                if (parseFloat(tb[j].value) == "0") {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgZeroNotHours;
                    tb[j].focus();
                    return false;
                }
            }
            for (var k = 8; k < tb.length; k = k + 10) {
                if (isNaN(tb[k].value)) {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgHoursFormat;
                   tb[k].focus();
                   return false;
               }
               if (parseFloat(tb[k].value) == "0") {
                   document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgZeroNotHours;
                    tb[k].focus();
                    return false;
                }
            }

        }
        function validateTextBox(textBoxId) {
            var textBoxRef = document.getElementById(textBoxId);
            var floatValue = parseFloat(textBoxRef.value);

            if (isNaN(floatValue) == true)
                textBoxRef.value = '0';
            else
                textBoxRef.value = floatValue;
        }
        function setFocus(Target) {
            if (window.event.keyCode == "13")
                document.getElementById(Target).focus();
        }

        function setDirty() {
            document.body.onbeforeunload = showMessage;
            //debugger;      
            document.getElementById("DirtyLabel").className = "show";
        }
        function clearDirty() {
            document.body.onbeforeunload = "";
            document.getElementById("DirtyLabel").className = "hide";
        }

        function showMessage() {
            return "If you click OK, the changes you have made will be lost."
        }
        function setControlChange() {
            if (typeof (event.srcElement) != 'undefined') {
                event.srcElement.onchange = setDirty;
            }
        }

    </script>
         <div id="DirtyLabel" style="color: Red;" class="hide">   </div> 
 <span class="hidden"><asp:Button ID="btnEntryKey" runat="server" Text=""  /></span> 
<div>


    <asp:HiddenField ID="hdHours" runat="server" />
<h2>Record Working Time</h2>
    <asp:Label ID="lblMessageBoard" runat="server"  CssClass="msgboard" 
        meta:resourcekey="lblMessageBoardResource1" ></asp:Label><br />
   
    <div style="float:left">
     <asp:Button ID="btnPrev" runat="server" Text="<<" onclick="btnPrev_Click" OnClientClick ="clearDirty();"
            meta:resourcekey="btnPrevResource1" />
     </div>
    
    <div style="float:left" class="box">

        <asp:Calendar ID="Calendar1" runat="server" ShowGridLines="True" 
            ShowNextPrevMonth="False" meta:resourcekey="Calendar1Resource1" 
            BorderColor="#333333" BorderStyle="Solid" BorderWidth="1px">
            <OtherMonthDayStyle BackColor="#E9E9E9" /></asp:Calendar> 
    </div>
    <div style="float:left" class="box">
        <asp:Calendar ID="Calendar2" runat="server" ShowGridLines="True" 
            ShowNextPrevMonth="False" meta:resourcekey="Calendar2Resource1"
            BorderColor="#333333" BorderStyle="Solid" BorderWidth="1px"
            >
            <OtherMonthDayStyle BackColor="#E9E9E9" />
        </asp:Calendar>
    </div>
    <div style="float:left" class="box">
        <asp:Calendar ID="Calendar3" runat="server" ShowGridLines="True" 
            ShowNextPrevMonth="False" meta:resourcekey="Calendar3Resource1"
            BorderColor="#333333" BorderStyle="Solid" BorderWidth="1px"
            >
            <OtherMonthDayStyle BackColor="#E9E9E9" /></asp:Calendar>
    </div>
       <div style="float:left">
    <asp:Button ID="btnNext" runat="server" Text=">>" onclick="btnNext_Click" OnClientClick ="clearDirty();"
               meta:resourcekey="btnNextResource1" />
    </div>
     <div class="clear"></div>
     <div class="box">
    <asp:TextBox ID="txtAbsent" runat="server" Width="20px" Height="12px" 
            style="background-color:Green" Enabled="false"></asp:TextBox>
    <asp:Label ID="lblAbsent" runat="server" Text="Absent" CssClass="label" 
            meta:resourcekey="lblAbsentResource1" Width="60px"></asp:Label>
    <asp:TextBox ID="txtMltplEntrs" runat="server" Width="20px" Height="12px" 
            style="background-color:Red" Enabled="false"></asp:TextBox>
    <asp:Label ID="lblMultipleEntries" runat="server" Text="Multiple entries" 
            CssClass="label" Width="100px"></asp:Label>
    <asp:TextBox ID="txtSent" runat="server" Width="20px" Height="12px" 
            style="background-color:Blue"  Enabled="false"></asp:TextBox>
    <asp:Label ID="lblSent" runat="server" Text="Sent" CssClass="label" 
            meta:resourcekey="lblSentResource1" Width="50px"></asp:Label>
    <asp:TextBox ID="txtDltnRqstd" runat="server" Width="20px" Height="12px" 
            style="background-color:Gray"  Enabled="false"></asp:TextBox>
    <asp:Label ID="lblDltnRqstd" runat="server" Text="Deletion requested" 
            CssClass="label"></asp:Label>
            </div>
      
      <asp:HiddenField ID="hiddenRowCount" runat="server" />
        <BDP:IsDateValidator ID="IsDateValidator1" runat="server" ControlToValidate="bdpFrom"
            ErrorMessage="Select valid date."  ForeColor="Red" ></BDP:IsDateValidator><br />
            <div class="clear"></div>
       <div style="clear:both">
   
            <asp:Label ID="lblFrom" runat="server" Text="Week from" CssClass="label" 
            Width="80px" meta:resourcekey="lblFromResource1"></asp:Label>
        <asp:Button ID="btnPreviousWeek" runat="server" Text="&lt;" Font-Bold="True" OnClientClick ="clearDirty();"
                onclick="btnPreviousWeek_Click" meta:resourcekey="btnPreviousWeekResource1" 
             />
        <BDP:BDPLite ID="bdpFrom" runat="server" meta:resourcekey="bdpFromResource1" 
                style="display: inline;"  >
            <TextBoxStyle CssClass="textbox" Width="120px" />
        </BDP:BDPLite>
         <asp:Label ID="lblTo" runat="server" Text="To" CssClass="label" Width="40px" 
                meta:resourcekey="lblToResource1"></asp:Label>
        <BDP:BDPLite ID="bdpTo" runat="server" meta:resourcekey="bdpToResource1" 
                style="display: inline;"   >
            <TextBoxStyle CssClass="textbox" Width="120px" />
        </BDP:BDPLite>
        
        <asp:Button ID="btnNextWeek" runat="server" Text="&gt;" onclick="btnNextWeek_Click" meta:resourcekey="btnNextWeekResource1" 
         OnClientClick ="clearDirty();"    />
        <asp:Button ID="btnGo" runat="server" Text="Go" OnClientClick ="clearDirty();"
                meta:resourcekey="btnGoResource1"  />
         </div>
   
   
    <br />
    <div>
    
     
    <asp:gridview ID="grdRecordTime"  runat="server"  ShowFooter="True"  width="100%"
            AutoGenerateColumns="False" onrowcreated="grdRecordTime_RowCreated" 
                onrowdatabound="grdRecordTime_RowDataBound" 
            meta:resourcekey="grdRecordTimeResource1"> 
           
        <Columns> 
         <asp:TemplateField HeaderText="Cost center"  HeaderStyle-CssClass="hd-small"
                > 
             <ItemTemplate> 
                <asp:DropDownList ID="drpdwnCostCenter" runat="server" Width="100px"           
                     AppendDataBoundItems="True" > 
                </asp:DropDownList>                
            </ItemTemplate> 
        </asp:TemplateField>
        
          <asp:TemplateField HeaderText="Order" 
                HeaderStyle-CssClass="hd-small" > 
             <ItemTemplate> 
                <asp:DropDownList ID="drpdwnOrder" runat="server" Width="100px" AppendDataBoundItems="True"> 
                </asp:DropDownList> 
           </ItemTemplate> 

<HeaderStyle CssClass="hd-small"></HeaderStyle>

<ItemStyle Width="30px"></ItemStyle>
        </asp:TemplateField> 
     
             <asp:TemplateField HeaderText="Attendance/Absence type" ItemStyle-Width="30" 
                HeaderStyle-CssClass="hd-small" meta:resourcekey="TemplateFieldResource1"> 
             <ItemTemplate> 
                <asp:DropDownList ID="drpdwnAttabsType" runat="server" 
                     AppendDataBoundItems="True" 
                     meta:resourcekey="drpdwnAttabsTypeResource1"> 
                </asp:DropDownList> 
            </ItemTemplate> 

<HeaderStyle CssClass="hd-small"></HeaderStyle>

<ItemStyle Width="30px"></ItemStyle>
        </asp:TemplateField>
         

  <asp:TemplateField HeaderText="Staff Grade" HeaderStyle-CssClass="hd-small" 
                meta:resourcekey="TemplateFieldResource5">
   <ItemTemplate>
    <asp:TextBox ID="txtStaffGrade" runat="server" Width="30px" style="text-align:right;" 
           meta:resourcekey="txtStaffGradeResource1"></asp:TextBox>

   </ItemTemplate>
     <ItemStyle HorizontalAlign="Center" /> 
    <FooterStyle HorizontalAlign="Center" /> 
            <FooterTemplate> 
            <div>
            <asp:Label ID="lblStaffGrade" runat="server" Text="Actual" CssClass="label" 
                    Width="40px" meta:resourcekey="lblStaffGradeResource1" ></asp:Label>
            </div>
            </FooterTemplate> 

<HeaderStyle CssClass="hd-small"></HeaderStyle>

  </asp:TemplateField>
  <asp:TemplateField HeaderText="Hours" HeaderStyle-CssClass="hd-small" 
                HeaderStyle-Width="30" meta:resourcekey="TemplateFieldResource6" >
   <ItemTemplate>
     <asp:TextBox ID="txtTotal" runat="server" Width="30px" ReadOnly="True" style="text-align:right;" 
           meta:resourcekey="txtTotalResource1"></asp:TextBox>
   </ItemTemplate>
     <ItemStyle HorizontalAlign="Center" /> 
   <FooterStyle HorizontalAlign="Center" /> 
            <FooterTemplate> 
            <div>
            <asp:Label ID="lblHours" runat="server" CssClass="label" Width="40px" style="text-align:right;"
                    meta:resourcekey="lblHoursResource1" ></asp:Label>
            </div>
   </FooterTemplate> 

<HeaderStyle CssClass="hd-small"></HeaderStyle>
  </asp:TemplateField>
          <asp:TemplateField HeaderText="SUN" HeaderStyle-CssClass="hd-small" 
                meta:resourcekey="TemplateFieldResource13"> 
            <ItemTemplate>
    <asp:TextBox ID="txtSUN" runat="server" CssClass="textbox" Width="30px" MaxLength="5" style="text-align:right;" 
                    meta:resourcekey="txtSUNResource1"  ></asp:TextBox>
       <cc1:FilteredTextBoxExtender ID="txtSUN_FilteredTextBoxExtender" runat="server" 
           Enabled="True" TargetControlID="txtSUN" FilterType="Custom, Numbers" ValidChars=".">
       </cc1:FilteredTextBoxExtender>
   </ItemTemplate>
     <ItemStyle HorizontalAlign="Center" /> 
  <FooterStyle HorizontalAlign="Center" /> 
            <FooterTemplate> 
            <div>
            <asp:Label ID="lblSun" runat="server" CssClass="label" Width="40px" style="text-align:right;"
                     ></asp:Label>
            </div>
   </FooterTemplate> 
          
<HeaderStyle CssClass="hd-small"></HeaderStyle>
         </asp:TemplateField> 

  <asp:TemplateField HeaderText="MON" HeaderStyle-CssClass="hd-small" 
                meta:resourcekey="TemplateFieldResource7">
   <ItemTemplate>
       <asp:TextBox ID="txtMON" runat="server" Width="30px" MaxLength="5" 
           meta:resourcekey="txtMONResource1"  style="text-align:right;" ></asp:TextBox>
       <cc1:FilteredTextBoxExtender ID="txtMON_FilteredTextBoxExtender" runat="server" 
           Enabled="True" TargetControlID="txtMON" FilterType="Custom, Numbers" 
           ValidChars=".">
       </cc1:FilteredTextBoxExtender>
   </ItemTemplate>
     <ItemStyle HorizontalAlign="Center" /> 
  <FooterStyle HorizontalAlign="Center" /> 
            <FooterTemplate> 
            <div>
            <asp:Label ID="lblMon" runat="server" CssClass="label" Width="40px" style="text-align:right;"
                    meta:resourcekey="lblMonResource1" ></asp:Label>
            </div>
   </FooterTemplate> 

<HeaderStyle CssClass="hd-small"></HeaderStyle>
  </asp:TemplateField>
  <asp:TemplateField HeaderText="TUE" HeaderStyle-CssClass="hd-small" 
                meta:resourcekey="TemplateFieldResource8">
   <ItemTemplate>
    <asp:TextBox ID="txtTUE" runat="server" Width="30px" MaxLength="5" style="text-align:right;" 
           meta:resourcekey="txtTUEResource1"  ></asp:TextBox>
       <cc1:FilteredTextBoxExtender ID="txtTUE_FilteredTextBoxExtender" runat="server" 
           Enabled="True" TargetControlID="txtTUE" FilterType="Custom, Numbers" 
           ValidChars=".">
       </cc1:FilteredTextBoxExtender>
   </ItemTemplate>
     <ItemStyle HorizontalAlign="Center" /> 
  <FooterStyle HorizontalAlign="Center" /> 
            <FooterTemplate> 
            <div>
            <asp:Label ID="lblTues" runat="server" CssClass="label" Width="40px" style="text-align:right;"
                    meta:resourcekey="lblTuesResource1" ></asp:Label>
            </div>
   </FooterTemplate> 

<HeaderStyle CssClass="hd-small"></HeaderStyle>
  </asp:TemplateField>
  <asp:TemplateField HeaderText="WED" HeaderStyle-CssClass="hd-small" 
                meta:resourcekey="TemplateFieldResource9">
   <ItemTemplate>
    <asp:TextBox ID="txtWED" runat="server" Width="30px" MaxLength="5" style="text-align:right;" 
           meta:resourcekey="txtWEDResource1"  ></asp:TextBox>
       <cc1:FilteredTextBoxExtender ID="txtWED_FilteredTextBoxExtender" runat="server" 
           Enabled="True" TargetControlID="txtWED" FilterType="Custom, Numbers" 
           ValidChars=".">
       </cc1:FilteredTextBoxExtender>
   </ItemTemplate>
     <ItemStyle HorizontalAlign="Center" /> 
 <FooterStyle HorizontalAlign="Center" /> 
            <FooterTemplate> 
            <div>
            <asp:Label ID="lblWed" runat="server" CssClass="label" Width="40px" style="text-align:right;"
                    meta:resourcekey="lblWedResource1" ></asp:Label>
            </div>
   </FooterTemplate> 
                  
<HeaderStyle CssClass="hd-small"></HeaderStyle>
                  
  </asp:TemplateField>
  <asp:TemplateField HeaderText="THU" HeaderStyle-CssClass="hd-small" 
                meta:resourcekey="TemplateFieldResource10">
   <ItemTemplate>
    <asp:TextBox ID="txtTHU" runat="server" Width="30px" MaxLength="5" style="text-align:right;" 
           meta:resourcekey="txtTHUResource1"  ></asp:TextBox>
       <cc1:FilteredTextBoxExtender ID="txtTHU_FilteredTextBoxExtender" runat="server" 
           Enabled="True" TargetControlID="txtTHU" FilterType="Custom, Numbers" 
           ValidChars=".">
       </cc1:FilteredTextBoxExtender>
   </ItemTemplate>
     <ItemStyle HorizontalAlign="Center" /> 
  <FooterStyle HorizontalAlign="Center" /> 
            <FooterTemplate> 
            <div>
            <asp:Label ID="lblThu" runat="server" CssClass="label" Width="40px" style="text-align:right;"
                    meta:resourcekey="lblThuResource1" ></asp:Label>
            </div>
   </FooterTemplate> 

<HeaderStyle CssClass="hd-small"></HeaderStyle>

  </asp:TemplateField>
  <asp:TemplateField HeaderText="FRI" HeaderStyle-CssClass="hd-small" 
                meta:resourcekey="TemplateFieldResource11">
   <ItemTemplate>
    <asp:TextBox ID="txtFRI" runat="server" Width="30px"  MaxLength="5" style="text-align:right;" 
           meta:resourcekey="txtFRIResource1"  ></asp:TextBox>
       <cc1:FilteredTextBoxExtender ID="txtFRI_FilteredTextBoxExtender" runat="server" 
           Enabled="True" TargetControlID="txtFRI" FilterType="Custom, Numbers" 
           ValidChars=".">
       </cc1:FilteredTextBoxExtender>
   </ItemTemplate>
     <ItemStyle HorizontalAlign="Center" /> 
  <FooterStyle HorizontalAlign="Center" /> 
            <FooterTemplate> 
            <div>
            <asp:Label ID="lblFri" runat="server" CssClass="label" Width="40px" style="text-align:right;"
                    meta:resourcekey="lblFriResource1" ></asp:Label>
            </div>
   </FooterTemplate> 

<HeaderStyle CssClass="hd-small"></HeaderStyle>

  </asp:TemplateField>
        <asp:TemplateField HeaderText="SAT" HeaderStyle-CssClass="hd-small" 
                meta:resourcekey="TemplateFieldResource12"> 
            <ItemTemplate>
    <asp:TextBox ID="txtSAT" runat="server" CssClass="textbox" Width="30px" MaxLength="5" style="text-align:right;" 
                    meta:resourcekey="txtSATResource1"  ></asp:TextBox>
       <cc1:FilteredTextBoxExtender ID="txtSAT_FilteredTextBoxExtender" runat="server" 
           Enabled="True" TargetControlID="txtSAT" FilterType="Custom, Numbers" ValidChars=".">
       </cc1:FilteredTextBoxExtender>
   </ItemTemplate>
     <ItemStyle HorizontalAlign="Center" /> 
       <FooterStyle HorizontalAlign="Center" /> 
            <FooterTemplate> 
            <div>
            <asp:Label ID="lblSAt" runat="server" CssClass="label" Width="40px" style="text-align:right;"
                    meta:resourcekey="lblSAtResource1" ></asp:Label>
            </div>
   </FooterTemplate>      

<HeaderStyle CssClass="hd-small"></HeaderStyle>
     </asp:TemplateField> 


         <asp:TemplateField meta:resourcekey="TemplateFieldResource14">
                <ItemTemplate>
                      <asp:Button ID="ButtonRemove" runat="server" Text="Remove" OnClientClick ="clearDirty();"
                          onclick="ButtonRemove_Click" meta:resourcekey="ButtonRemoveResource1"/> 
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" /> 
            <FooterTemplate> 
                 <asp:Button ID="ButtonAdd" runat="server" Text="Add Row" 
                     onclick="ButtonAdd_Click" OnClientClick="return ValidateEmptyValue()" 
                     meta:resourcekey="ButtonAddResource1" /> 
            </FooterTemplate> 

            </asp:TemplateField>
        </Columns> 
</asp:gridview>
        <br />

<%--<div >
     <asp:Label ID="lblDescription" runat="server" Text="Description : " Width="100px" CssClass="label" ></asp:Label>
        <asp:TextBox ID="txtDescription" runat="server" CssClass="textbox" Width="450px" Height="50px" TextMode="MultiLine" ></asp:TextBox>
</div>--%>
     
    <div class="clear"></div>      
</div>
        <div class="buttonrow"> 
            <asp:Button ID="btnPreviousStep" runat="server" Text="Previous Step" OnClientClick ="clearDirty();"
                 onclick="btnPreviousStep_Click" 
                meta:resourcekey="btnPreviousStepResource1"   />
             <asp:Button ID="btnReview" runat="server" Text="Review"  OnClientClick="return ValidateEmptyValue()"
                 onclick="btnReview_Click" meta:resourcekey="btnReviewResource1" 
                style="width: 60px"/>
             <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" OnClientClick ="clearDirty();"
                meta:resourcekey="btnSaveResource1" />
             <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick ="clearDirty();"
                onclick="btnCancel_Click" meta:resourcekey="btnCancelResource1"   />
             <asp:Button ID="btnExit" runat="server" Text="Exit" 
                 onclick="btnExit_Click" meta:resourcekey="btnExitResource1"   />
            
</div><br />
</div>

</asp:Content>
