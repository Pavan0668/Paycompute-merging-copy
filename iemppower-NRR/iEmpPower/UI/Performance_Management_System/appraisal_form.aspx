<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="appraisal_form.aspx.cs" Inherits="UI_Performance_Management_System_appraisee_form"  culture="auto" meta:resourcekey="PageResource1" uiculture="auto" Theme="SkinFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 800px;
        }
        .style2
        {
            width:600px;
        }
         .style3
        {
            width:200px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  
    <asp:Button ID="btnBack" runat="server" Text="Back To Template List" 
        onclick="btnBack_Click"/>
        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="javascript:window.print();" /><br />

    <h2>Self Appraisal</h2>
  <asp:Label ID="lblMessageBoard" runat="server"  CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1"></asp:Label>

  <br />
<asp:Panel ID="pnlAppraisal" runat="server" BorderColor="Black" BorderStyle="Groove" BorderWidth="1px">
  <div > 
      <div>
      <asp:Label ID="lblAppraiseeName" runat="server" Text="Name of the Appraisee : " CssClass="label"></asp:Label>
      <asp:Label ID="lblAppraiseeNameData" runat="server" Text="" CssClass="label"></asp:Label>
      </div>
     <div>
      <asp:Label ID="lblPernr" runat="server" Text="Employee Code : " CssClass="label"></asp:Label>
      <asp:Label ID="lblPernrData" runat="server" Text="" CssClass="label"></asp:Label>
     </div>
     <br />
     <div>
    <asp:Label ID="lblDescription" runat="server" CssClass="label-big" Text="List down any improvements done by you in your domain and any 
    initiatives / additional responsibilities / projects taken by you or any other critical incidences worth noting down which should have 
    resulted in improvement of either Productivity, Quality, Safety, Morale, Delivery/ Service or Reduction in Cost. (YOU MAY USE ADDITIONAL PAPER IF REQUIRED)"></asp:Label> 
    </div>
 <hr />
 
      <table class="style1">
          <tr>
              <td class="style2">
               <asp:Label ID="Label1" runat="server" Text="a) Performance improvement over previous year
               (Pls write down if there are any improvements you have brought into your domain work/process in last one year) :" CssClass="labelAppr"></asp:Label> 
               </td>
              <td class="style3">
               <asp:DropDownList ID="drpdwnPerformance" runat="server" CssClass="textbox" 
                      Visible="False"></asp:DropDownList>
              </td>
          </tr>
          <tr>
              <td colspan="2">
                <asp:TextBox ID="txtPerformance" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="600px" Height="150px"></asp:TextBox>
              </td>
          </tr>
          <tr>
              <td class="style2">
               <asp:Label ID="Label2" runat="server" Text="b) Additional responsibilities / projects taken during the previous year
               (Pls write down if there are any additional responibilities taken by you during last year) :" CssClass="labelAppr"></asp:Label>
              </td>
              <td class="style3">
              <asp:DropDownList ID="drpdwnAdditional" runat="server" CssClass="textbox" 
                      Visible="False"></asp:DropDownList>
              </td>
          </tr>
          <tr>
              <td colspan="2">
                   <asp:TextBox ID="txtAdditional" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="600px" Height="150px"></asp:TextBox>
              </td>
          </tr>
          <tr>
              <td class="style2">
                  <asp:Label ID="Label3" runat="server" Text="c) Any other critical incidences
                  (Pls write down if you had taken up any assignments other than your job):"  CssClass="labelAppr"></asp:Label>
              </td>
              <td class="style3">
                <asp:DropDownList ID="drpdwncritical" runat="server" CssClass="textbox" Visible="False"></asp:DropDownList>
              </td>
          </tr>
          <tr>
              <td colspan="2">
               <asp:TextBox ID="txtcritical" runat="server" CssClass="textboxApr" TextMode="MultiLine" Width="600px" Height="150px"></asp:TextBox>
              </td>
          </tr>
      </table>
 <br />
<%-- <div class="buttonrow">
    <asp:Button ID="btnSave" runat="server" Text="Save "/>
    <asp:Button ID="btnCancel" runat="server" Text="Cancel"/>
</div>
<br />--%>
</div>
</asp:Panel>

 <div class="buttonrow">                           
    <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click"/> 
    <asp:Button ID="btnSaveAndSend" runat="server" Text="Save &amp; Send" 
         onclick="btnSaveAndSend_Click"/>
     <asp:Button ID="btnApprove" runat="server" Text="Approve " visible="False" 
         onclick="btnApprove_Click"/>
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
         onclick="btnCancel_Click"/>
</div><br />

</asp:Content>

