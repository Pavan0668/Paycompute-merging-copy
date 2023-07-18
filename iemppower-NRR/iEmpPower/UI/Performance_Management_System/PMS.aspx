<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"  EnableEventValidation="false" CodeFile="PMS.aspx.cs" Inherits="UI_Performance_Management_System_PMS"  culture="auto" meta:resourcekey="PageResource1" uiculture="auto" Theme="SkinFile"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script src="../../Utilities/ValidationMessages.js" type="text/javascript"></script>
    <script src="../../Utilities/Validations.js" type="text/javascript"></script>

    <asp:Label ID="lblMessageBoard" runat="server"  CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1"></asp:Label>

    <asp:Panel ID="pnlFill" runat="server">
   
    <h3>Appraisal</h3><br />
       <asp:GridView ID="grdSelfAppraisalReview" runat="server" CssClass="grid" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" PageSize="5"  
        onselectedindexchanged="grdSelfAppraisalReview_SelectedIndexChanged" 
        onrowdatabound="grdSelfAppraisalReview_RowDataBound">
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
               <asp:BoundField DataField ="APPRAISAL_ID" HeaderText="Document-ID" ReadOnly="true" />
               <asp:BoundField DataField="APPRAISAL_NAME"  HeaderText="Template-Name" />
               <asp:BoundField DataField="APPRAISEE_ID"  HeaderText="PERNR" ReadOnly="True"  />                
               <asp:BoundField DataField="APPRAISER_ID"  HeaderText="Appraiser-ID" ReadOnly="True"  />
               <asp:BoundField DataField="STATUSAPPR" HeaderText="Status" ReadOnly="True" />
            </Columns>

            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="12" Font-Names="verdana" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>       
       <hr />
       </asp:Panel>
       <br />
 <%--       <asp:Panel ID="pnlApprove" runat="server">
    <h3>Approve Appraisal</h3><br />

     <asp:Label ID="lblMessage" runat="server"  CssClass="msgboard" meta:resourcekey="lblMessageResource1"></asp:Label>

           <asp:GridView ID="grdAppraisalToApprove" runat="server" CssClass="grid" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" PageSize="5">
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:BoundField DataField ="APPRAISAL_ID" HeaderText="Document-ID" ReadOnly='true'/>
                <asp:BoundField DataField="APPRAISAL_NAME"  HeaderText="Template-Name" />
                <asp:BoundField DataField="APPRAISEE_ID"  HeaderText="PERNR" ReadOnly="True"  />                
                <asp:BoundField DataField="APPRAISER_ID"  HeaderText="Appraiser-ID" ReadOnly="True"  />
                <asp:BoundField DataField="STATUSAPPR" HeaderText="Status" ReadOnly="True" />
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="12" Font-Names="verdana" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" 
                HorizontalAlign="Center" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        
    </asp:Panel>--%>
</asp:Content>

