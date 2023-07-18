<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="FBP_Apply_ViewClaims.aspx.cs"
    Inherits="iEmpPower.UI.FBP.FBP_Apply_ViewClaims" Theme="SkinFile" EnableEventValidation="false" Culture="en-GB" 
    MaintainScrollPositionOnPostback="true"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .Divh {
            background-color: #3470A7;
            color: #FFFFFF;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <%--   <div>
        <span id="bold" runat="server" style="font-weight: bold"></span>
    </div>--%>
    <h2>Reimbursement List</h2>
    <asp:Label ID="lblmsg" runat="server"></asp:Label><br />
    &nbsp;&nbsp;
    <asp:Button ID="btnNewFbpclaims" runat="server" Text="New FBP Claims" OnClick="btnNewFbpclaims_Click" />
    <br /><br />
    <asp:GridView ID="grd_SavedFbpClaims" runat="server" AutoGenerateColumns="false" CssClass="gridview" HeaderStyle-CssClass="Divh" Width="100%"
         OnRowCommand="grd_SavedFbpClaims_RowCommand" DataKeyNames="FBPC_IC,LGART">
        <Columns>
            <asp:TemplateField HeaderText="Sl.No." ControlStyle-CssClass="col-center">
                <ItemTemplate>
                    <%# Container.DataItemIndex+1 %>
                </ItemTemplate>
            
                <ItemStyle CssClass="col-center" />
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Fbp Claim Id" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                <ItemTemplate>
                     <%# Eval("FBPC_IC") %>
                </ItemTemplate>

            </asp:TemplateField>
            <asp:TemplateField HeaderText="Reimbursement" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                <ItemTemplate>
                     <%# Eval("LGART") %>
                </ItemTemplate>

            </asp:TemplateField>
             <asp:TemplateField HeaderText="Date" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                <ItemTemplate>
                      <%# Eval("BEGDA") %>
                </ItemTemplate>
             </asp:TemplateField>
               <asp:TemplateField HeaderText="Amount" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                <ItemTemplate>
                      <%# Eval("BETRG") %>
                </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="Created on" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify">
                <ItemTemplate>
                     <%# Eval("CREATED_ON") %>
                </ItemTemplate>
              
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Status" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify">
                <ItemTemplate>
                     <%# Eval("STATUS") %>
                </ItemTemplate>
              
            </asp:TemplateField>
            <asp:TemplateField>
               <ItemTemplate>
                    <asp:LinkButton ID="LbtnFbpclaimsView" runat="server" CausesValidation="False" CommandName="EDITFBP" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>Edit</asp:LinkButton>

                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
    <br/>

    <table id="tblreimbursement" class="tablebody" border="0" cellpadding="1" cellspacing="1" runat="server" visible="false">

        <tr>
            <td>
                <asp:Label ID="lblPlan" runat="server" Text="Reimbursements" CssClass="label"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlPlan" runat="server" Width="150px" CssClass="textbox" OnSelectedIndexChanged="ddlPlan_SelectedIndexChanged" AutoPostBack="true">
                      <asp:ListItem Text="0" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="1200" Value="1200"></asp:ListItem>
                      <asp:ListItem Text="1220" Value="1220"></asp:ListItem>
                      <asp:ListItem Text="1225" Value="1225"></asp:ListItem>
                      <asp:ListItem Text="1215" Value="1215"></asp:ListItem>
                      <asp:ListItem Text="1205" Value="1205"></asp:ListItem>
                      <asp:ListItem Text="1210" Value="1210"></asp:ListItem>
                      <asp:ListItem Text="1230" Value="1230"></asp:ListItem>

                </asp:DropDownList></td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="grd_CalimsItems" runat="server" AutoGenerateColumns="false" CssClass="gridview" HeaderStyle-CssClass="Divh" Width="70%">
        <Columns>
            <asp:BoundField HeaderText="Opening Balance" DataField="Opening_Balance" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" />

            <asp:BoundField HeaderText="Entitlement" DataField="Entitlement" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" />

            <asp:BoundField HeaderText="Claims Paid" DataField="Claims_Paid" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" />

            <asp:BoundField HeaderText="Claims Pending" DataField="Claims_Pending" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" />

            <asp:BoundField HeaderText="Balance" DataField="Balance" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" />



        </Columns>
    </asp:GridView>

    <br />

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="gridview" HeaderStyle-CssClass="Divh" Width="100%" OnRowCommand="GridView1_RowCommand"
        OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" ShowFooter="true"  
        DataKeyNames="BILL_NO" OnRowCancelingEdit="GridView1_RowCancelingEdit" >
        <Columns>
            <asp:TemplateField HeaderText="Sl.No." ControlStyle-CssClass="col-center">
                <ItemTemplate>
                    <%# Container.DataItemIndex+1 %>
                </ItemTemplate>
                <EditItemTemplate><%# Container.DataItemIndex+1 %></EditItemTemplate>
                <ItemStyle CssClass="col-center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Bill No." ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                <ItemTemplate>
                     <%# Eval("BILL_NO") %>
                </ItemTemplate>
                <EditItemTemplate>
                     <asp:TextBox ID="txtBillNoe" runat="server" Style="text-align: right" CssClass="textbox" Text='<%# Eval("BILL_NO") %>'></asp:TextBox> 
                    <asp:RequiredFieldValidator ID="RFV_txtBillNoe" runat="server" ControlToValidate="txtBillNoe" ValidationGroup="vg2" ErrorMessage="*" 
                    ForeColor="Red"></asp:RequiredFieldValidator>
                </EditItemTemplate>
               
                <FooterTemplate>
                    <asp:TextBox ID="txtBillNo" runat="server" Style="text-align: right" CssClass="textbox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RFV_txtBillNo" runat="server" ControlToValidate="txtBillNo" ValidationGroup="vg1" ErrorMessage="*" 
                    ForeColor="Red"></asp:RequiredFieldValidator>

                </FooterTemplate>
                <FooterStyle Font-Bold="false"/>
                
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Bill Date" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                <ItemTemplate>
                     <%# Eval("BILL_DATE") %>
                </ItemTemplate>
               
                <EditItemTemplate>
                    <asp:TextBox ID="txtBillDatee" runat="server" ValidationGroup="vg2" 
                        Text='<%# Eval("BILL_DATE") %>' Width="199px" Style="letter-spacing: 1px; background: #ffffff url('../../images/CalenderIMG.png') no-repeat 99% 55% !important; border: 1px solid #666666; margin-bottom: 4px; margin-left: 2px; padding: 2px;"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="MEE_txtBillDatee" runat="server" AcceptNegative="Left"
                        CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                        MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError" TargetControlID="txtBillDatee" />
                    <cc1:CalendarExtender ID="CE_txtBillDatee" runat="server" Enabled="True" Format="dd/MM/yyyy"
                        TargetControlID="txtBillDatee">
                    </cc1:CalendarExtender>

                     <asp:RequiredFieldValidator ID="RFV_txtBillDatee" runat="server" ControlToValidate="txtBillDatee" ValidationGroup="vg2" ErrorMessage="*" 
                    ForeColor="Red"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                 <FooterTemplate>
                    <asp:TextBox ID="txtBillDate" runat="server" ValidationGroup="vg2" Width="199px" Style="letter-spacing: 1px; background: #ffffff url('../../images/CalenderIMG.png') no-repeat 99% 55% !important; border: 1px solid #666666; margin-bottom: 4px; margin-left: 2px; padding: 2px;"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="MEE_txtBillDate" runat="server" AcceptNegative="Left"
                        CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                        MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError" TargetControlID="txtBillDate" />
                    <cc1:CalendarExtender ID="CE_txtBillDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                        TargetControlID="txtBillDate">
                    </cc1:CalendarExtender>
                     <asp:RequiredFieldValidator ID="RFV_txtBillDate" runat="server" ControlToValidate="txtBillDate" ValidationGroup="vg1" ErrorMessage="*" 
                    ForeColor="Red"></asp:RequiredFieldValidator>
               </FooterTemplate>
                <FooterStyle Font-Bold="false"/>
              
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Relationship" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                <ItemTemplate>
                      <%# Eval("RELATIONSHIP") %>
                </ItemTemplate>
                 
                <EditItemTemplate>
                     <asp:TextBox ID="txtRelationshipe" runat="server" Style="text-align: right" CssClass="textbox" Text='<%# Eval("RELATIONSHIP") %>'></asp:TextBox>
              <%--  <asp:RequiredFieldValidator ID="RFV_txtRelationshipe" runat="server" ControlToValidate="txtRelationshipe" ValidationGroup="vg2" ErrorMessage="Please enter Relationship" 
                    ForeColor="Red"></asp:RequiredFieldValidator>--%>
                </EditItemTemplate>
                  <FooterTemplate>
                    <asp:TextBox ID="txtRelationship" runat="server" Style="text-align: right" CssClass="textbox"></asp:TextBox>
                    <%--   <asp:RequiredFieldValidator ID="RFV_txtRelationship" runat="server" ControlToValidate="txtRelationship" ValidationGroup="vg1" ErrorMessage="Please enter Relationship" 
                    ForeColor="Red"></asp:RequiredFieldValidator>--%>
               </FooterTemplate>
                <FooterStyle Font-Bold="false"/>
                
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Amount" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify">
                <ItemTemplate>
                     <%# Eval("BILL_AMT") %>
                </ItemTemplate>
              
                <EditItemTemplate>
                     <asp:TextBox ID="txtAmounte" runat="server" Style="text-align: right" CssClass="textbox" Text='<%# Eval("BILL_AMT") %>'></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RFV_txtAmounte" runat="server" ControlToValidate="txtAmounte" ValidationGroup="vg2" ErrorMessage="*" 
                    ForeColor="Red"></asp:RequiredFieldValidator>
                </EditItemTemplate>
              <FooterTemplate>
                    <asp:TextBox ID="txtAmount" runat="server" Style="text-align: right" CssClass="textbox"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="RFV_txtAmount" runat="server" ControlToValidate="txtAmount" ValidationGroup="vg1" ErrorMessage="*" 
                    ForeColor="Red"></asp:RequiredFieldValidator>
                </FooterTemplate>
                 <FooterStyle Font-Bold="false"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Upload">
                <ItemTemplate>
                    <asp:LinkButton ID="Lbtndownload" runat="server" Text='<%#Eval("RECEIPT_FID") %>' Font-Bold="True" CommandName="DOWNLOAD" CommandArgument='<%# Eval("RECEIPT_FPATH") %>' CausesValidation="false" />
                
                </ItemTemplate>
                 
                <EditItemTemplate>
                    <asp:FileUpload ID="fuAttachmentse" runat="server"/><br />
                    <asp:Label ID="fuAttachmentsfnamee" runat="server" ></asp:Label>
                    <%--<asp:LinkButton ID="LbtnUploade" runat="server" Text="Upload" Visible="false"></asp:LinkButton>
                    <asp:LinkButton ID="LbtnDeletee" runat="server" Text="Delete" Visible="false"></asp:LinkButton>--%>
                </EditItemTemplate>
                 <FooterTemplate>
                    <asp:FileUpload ID="fuAttachments" runat="server"  ForeColor="Black"/><br />
                    <asp:Label ID="fuAttachmentsfname" runat="server"></asp:Label>
                    <%--<asp:LinkButton ID="LbtnUpload" runat="server" Text="Upload" Visible="false"></asp:LinkButton>
                    <asp:LinkButton ID="LbtnDelete" runat="server" Text="Delete" Visible="false"></asp:LinkButton>--%>
               </FooterTemplate>
            
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="EDIT"
                        CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>Edit</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="DELETE"
                        CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>Delete</asp:LinkButton>&nbsp;&nbsp;
                
                </ItemTemplate>
                
                <EditItemTemplate>
                      <asp:LinkButton ID="LinkButton1" runat="server" CommandName="UPDATE" ValidationGroup="vg2"
                        CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>Update</asp:LinkButton>&nbsp;&nbsp;
                    <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandName="CANCEL"
                        CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>Cancel</asp:LinkButton>

                </EditItemTemplate>
                
               <FooterTemplate >
                   
                    <asp:LinkButton ID="LbtnFbpClaimsView" runat="server" CommandName="ADD" ValidationGroup="vg1"
                        CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>Add</asp:LinkButton>
                   <asp:HiddenField id="HF_Fid" runat="server"/>
                </FooterTemplate>
                <FooterStyle Font-Bold="false"/>
            
            </asp:TemplateField>

        </Columns>
    </asp:GridView>

    <div class="buttonrow">
       &nbsp; <asp:Button ID="btnSubmitClaims" runat="server" Text="Submit" OnClick="btnSubmitClaims_Click" Visible="false"/>&nbsp;&nbsp;
         <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" Visible="false"/>
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Visible="false"/>
       
    </div>

</asp:Content>
