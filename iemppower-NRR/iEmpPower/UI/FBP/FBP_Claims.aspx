<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="FBP_Claims.aspx.cs" 
    Inherits="iEmpPower.UI.FBP.FBP_Claims" Theme="SkinFile" EnableEventValidation="false" Culture="en-GB"
    MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        
        .Divh {
            background-color: #3470A7;
             color: #FFFFFF;
        }

    </style>
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
            <span id="bold" runat="server" style="font-weight: bold"></span>
        </div>
        <h2 style="font-size:x-large">Reimbursement List</h2>
   <asp:Label ID="lblmsg" runat="server"></asp:Label>

    <div runat="server" style="width:60%;">
    <h2 style="text-align:center">Reimbursement Details as on
                     <asp:Label ID="LblDate" runat="server" Text="Jan 2016"></asp:Label></h2></div>
<asp:GridView ID="grd_CalimsItems" runat="server" AutoGenerateColumns="false" CssClass="gridview" HeaderStyle-CssClass="Divh" Width="60%">
        <Columns>
              <asp:TemplateField HeaderText="Sl.No." ControlStyle-CssClass="col-center">
                <ItemTemplate>
                    <%# Container.DataItemIndex+1 %>
                </ItemTemplate>
                  <ItemStyle CssClass="col-center"/>
              </asp:TemplateField>
            <%--<asp:BoundField  HeaderText="Items" DataField="ALLOWANCETEXT" ItemStyle-Width="45%"/>--%>
            <asp:TemplateField HeaderText="Allowance" ItemStyle-Width="45%">
                        <ItemTemplate>

                            <%# Eval("LGART") %> - <%# Eval("ALLOWANCETEXT") %>
                        </ItemTemplate>

                    </asp:TemplateField>
               
             <%--<asp:BoundField HeaderText="Annual Entitlement" DataField="ANNUAL" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify"/>--%>
            <asp:TemplateField HeaderText="Annual Entitlement" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify">
                        <ItemTemplate>
                             <%# Convert.ToDouble(Eval("ANNUAL")).ToString("0.00") %>
                         
                        </ItemTemplate>

                    </asp:TemplateField>
                
             <asp:BoundField HeaderText="Claims Paid" DataField="BETRG" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify"/>
                
           <asp:BoundField HeaderText="Claims Pending" DataField="PENDINGAMT" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify"/>
               
            <asp:BoundField HeaderText="Balance" DataField="BALANCE" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify"/>
                
        </Columns>
    </asp:GridView>


   
     <div class="buttonrow">

          <p style="color:#00529b"> Note: Balance = Entitlement - Claims Paid - Claims Pending</p>
         <br />
            <asp:Button ID="btnApplyView" runat="server" Text="Apply / View Claim" OnClick="btnApplyView_Click" TabIndex="1"  />
        
        </div>
</asp:Content>
