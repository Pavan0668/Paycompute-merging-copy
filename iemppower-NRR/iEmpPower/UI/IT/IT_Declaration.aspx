<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="IT_Declaration.aspx.cs" 
    Inherits="iEmpPower.UI.IT.IT_Declaration" Theme="SkinFile" EnableEventValidation="false" Culture="en-GB"
    MaintainScrollPositionOnPostback="true"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <style type="text/css">
        
        /*#MainContent_GVITSec80
        {
             
            overflow: scroll !important;
            overflow-y:scroll !important;
            overflow-x:hidden !important;
        }

        #MainContent_PnlIT
        {
            overflow-y:scroll !important;
            overflow-x:hidden !important;
            overflow: scroll !important;
        }*/

       

    </style>
</asp:Content>--%>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="LblLockSts" runat="server" CssClass="msgboard"></asp:Label>
     <div id="DivSec80" runat="server">
        <h2>Section 80 Deductions : From April 1<sup>st</sup> <asp:Label ID="LblFromDate" runat="server"></asp:Label> To March 31<sup>st</sup> <asp:Label ID="LblToDate" runat="server"></asp:Label></h2>
       
     <%--    <div id="Div1" runat="server" style="width:60%;">
                 <h3 style="text-align:left;color:#004080;">/h3>
         </div>--%>
         <h3 style="text-align:left;color:black;"><asp:CheckBox ID="CB_ConsAct" runat="server" Text="Consider Actuals" OnCheckedChanged="CB_ConsAct_CheckedChanged" AutoPostBack="true" TabIndex="1"/></h3>
         
           <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard"></asp:Label>
         <%-- <asp:Panel ID="PnlIT" runat="server" Width="90%" Height="360px">--%>
           <asp:GridView ID="GVITSec80" runat="server" AutoGenerateColumns="False"  BorderStyle="None" Width="85%"   DataKeyNames="SBSEC,SBDIV,ID,LID"
                OnRowCommand="GVITSec80_RowCommand" OnRowDeleting="GVITSec80_RowDeleting" TabIndex="2">
                <Columns>
                   <%-- //select t8.SBSEC,t8.SBDIV,t8.SBDDS, t9.SDVLT,t9.TXEXM--%>
                    <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Contribution" DataField="SBDDS">
                        <ItemStyle HorizontalAlign="left" Width="450px"/>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Limit"  DataField="SDVLT">
                        <ItemStyle HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Tax EXEM%"  DataField="TXEXM">
                        <ItemStyle HorizontalAlign="right" />
                    </asp:BoundField>
                   
                    <asp:TemplateField HeaderText="Prop. Contr. (INR)">
                        <ItemStyle HorizontalAlign="right"/>
                        <ItemTemplate>
                            <asp:TextBox ID="txtPropContr" runat="server" Style="text-align: right" CssClass="textbox" Width="100px" Text='<%# Eval("PROPCONTR") %>'></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FTB_txtPropContr" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                    TargetControlID="txtPropContr" ValidChars=".">
                                </cc1:FilteredTextBoxExtender>
                             </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Act. Contr. (INR)">
                        <ItemStyle HorizontalAlign="right"/>
                        <ItemTemplate>
                            <asp:TextBox ID="txtActContr" runat="server" Style="text-align: right" CssClass="textbox"  Width="100px" Text='<%# Eval("ACTCONTR") %>'></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FTB_txtActContr" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                    TargetControlID="txtActContr" ValidChars=".">
                                </cc1:FilteredTextBoxExtender>
                              </ItemTemplate>
                    </asp:TemplateField>
                   <%-- <asp:BoundField HeaderText="Curr"  DataField="CURR">
                        <ItemStyle HorizontalAlign="right"/>
                    </asp:BoundField>--%>

                    <asp:TemplateField HeaderText="Attachments" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                  <div id="divFile" runat="server" style="width: 200px; overflow: hidden">
                                    <asp:FileUpload ID="fuAttachments" runat="server" ForeColor="Black" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "true" : "false"))%>'/><br />
                                    <asp:Label ID="fuAttachmentsfname" runat="server" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "true" : "false"))%>'></asp:Label>



                           <asp:LinkButton ID="Lbtndownload" runat="server" Text='<%#Eval("RECEIPT_FID") %>' Font-Bold="True" CommandName="download" CommandArgument='<%# Eval("RECEIPT_FPATH") %>' CausesValidation="false" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "false" : "true"))%>' />

                         <%-- <asp:LinkButton ID="LbtnUpload" runat="server" Text="Upload" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                            CommandName="Upload" CausesValidation="false" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "true" : "false"))%>'></asp:LinkButton>
                         --%>  
                                      <asp:LinkButton ID="LbtnDelete" runat="server" Text="Delete" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                            CommandName="Delete" CausesValidation="false" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "false" : "true"))%>'></asp:LinkButton>

                                </div>

                            </ItemTemplate>
                         <ItemStyle HorizontalAlign="right"/>
                     </asp:TemplateField>

                      
                    <asp:TemplateField HeaderText="Remarks">
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtRemarks" runat="server"  TextMode="MultiLine" Width="170px" Text='<%# Eval("EMPCOMMENTS") %>'></asp:TextBox>
                        </ItemTemplate>
                       
                    </asp:TemplateField>
                    </Columns>
                    
            </asp:GridView>

       <%--  </asp:Panel>--%>
         <br />
        <asp:Button ID="btnSubmitClaims" runat="server" Text="Submit" OnClick="btnSubmitITSec80_Click" TabIndex="3"/>
          <asp:Button ID="BtnUpdate" runat="server" Text="Update" OnClick="BtnUpdate_Click" TabIndex="4"/>
          <asp:Button ID="BtnEdit" runat="server" Text="Edit" OnClick="BtnEdit_Click" TabIndex="5"/>
         <asp:Button ID="BtnCancel" runat="server" Text="Cancel" OnClick="BtnCancel_Click" TabIndex="6"/>
     </div>
</asp:Content>
