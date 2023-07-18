<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ViewPassportVisaInsMembTD.aspx.cs" Inherits="iEmpPower.UI.Local_requisition.ViewPassportVisaInsMembTD" Theme="SkinFile"  EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" CssClass="msgboard"></asp:Label>
    

     <br />
   <div>
            <cc1:TabContainer ID="tcDefalut" runat="server" ActiveTabIndex="0" AutoPostBack="True" onactivetabchanged="tcDefalut_ActiveTabChanged" meta:resourcekey="tcDefalutResource1" >
         <%--PASSPORT--%>
           <cc1:TabPanel runat="server" HeaderText="Passport" ID="tabPPassport"  meta:resourcekey="TabPanel1Resource1">
            <HeaderTemplate>Passport</HeaderTemplate>
                <ContentTemplate>
                    <div>
                          <asp:Panel ID="pnlPassport" runat="server" >
                            <br />
                              <h3>Employee's Passport Details</h3>
                                <br />
                        

            <asp:linkbutton id="lnkExportAll" runat="server" tooltip="Export this List" text="Export to Excel"

                onclick="lnkExportAll_Click"></asp:linkbutton><br /><br />
                                <asp:GridView ID="grdPassport" runat="server" CssClass="gridview" AutoGenerateColumns="False"  Width="100%"
                                    AllowSorting="true" OnSorting="grdPassport_Sorting">
                                <Columns> 
                                     <asp:BoundField DataField="PERNR" HeaderText="PERNR" SortExpression="PERNR" />
                                     <asp:BoundField DataField="EMPNAME" HeaderText="EMPLOYEE NAME" SortExpression="EMPNAME" />
                                     <asp:BoundField DataField="PASNUM" HeaderText="PASSPORT NO" SortExpression="PASNUM" />
                                     <asp:BoundField DataField="DOI" HeaderText="DATE OF ISSUE "  SortExpression="DOI"/>
                                     <asp:BoundField DataField="DOE" HeaderText="DATE OF EXPIRE"  SortExpression="DOE"/>
                                     <asp:BoundField DataField="PLISS" HeaderText="PLACE OF ISSUE"  SortExpression="PLISS"/>
                                    
                                </Columns>
                             </asp:GridView>
                           
      

                             
             </asp:Panel>
                    </div>
                </ContentTemplate>
                </cc1:TabPanel>

           <%--VISA--%>
           <cc1:TabPanel runat="server" HeaderText="Visa" ID="tabPVisa" meta:resourcekey="TabPanel1Resource1">
            <HeaderTemplate>Visa</HeaderTemplate>
            <ContentTemplate>
                  <div>
                          <asp:Panel ID="pnlVisa" runat="server" >
                            <br />
                              <h3>Employee's Visa Details</h3>
                                <br />
                               
                                <asp:linkbutton id="lnkVisa" runat="server" tooltip="Export this List" text="Export to Excel"

                onclick="lnkVisa_Click"></asp:linkbutton><br /><br />
                                <asp:GridView ID="grdVisa" runat="server" CssClass="gridview" AutoGenerateColumns="False"  Width="100%">
                                <Columns> 
                                     <asp:BoundField DataField="PERNR" HeaderText="PERNR"  />
                                     <asp:BoundField DataField="VINUM" HeaderText="VISA NO"  />
                                    <asp:BoundField DataField="PASNUM" HeaderText="PASSPORT NO"  />
                                    <asp:BoundField DataField="EMPNAME" HeaderText="EMPLOYEE NAME"  />
                                    <asp:BoundField DataField="COUNTRY" HeaderText="COUNTRY"  />
                                    <asp:BoundField DataField="DOI" HeaderText="DATE OF ISSUE"  />
                                    <asp:BoundField DataField="DOE" HeaderText="DATE OF EXPIRE"  />
                                    <asp:BoundField DataField="VISA_TYPE" HeaderText="VISA TYPE"  />
                                     <%--<asp:BoundField DataField="ICTYPE" HeaderText="TYPE" ReadOnly="True" />
                                     <asp:BoundField DataField="ICNUM" HeaderText="Country Name" ReadOnly="True"  />
                                     <asp:BoundField DataField="CREATED_ON" HeaderText="Validity" ReadOnly="True"  />
                                     <asp:BoundField DataField="CREATED_ON" HeaderText="Visa Number" ReadOnly="True"  />
                                     <asp:BoundField DataField="CREATED_ON" HeaderText="Visa Type" ReadOnly="True"  /> --%>
                                </Columns>
                             </asp:GridView> 
                            
                         </asp:Panel>     
                  </div>
                </ContentTemplate>
                </cc1:TabPanel>

              <%--  Travel INS--%>
 <cc1:TabPanel runat="server" HeaderText="Travel Ins." ID="tabPTravelIns" meta:resourcekey="TabPanel1Resource1">
            <HeaderTemplate>Travel Ins.</HeaderTemplate>
            <ContentTemplate>
                  <div>
                          <asp:Panel ID="pnlTravelIns" runat="server" >
                            <br />
                              <h3>Employee's Travel Ins. Details</h3>
                                <br />
                             
                                <asp:linkbutton id="lnkTravelIns" runat="server" tooltip="Export this List" text="Export to Excel"

                onclick="lnkTravelIns_Click"></asp:linkbutton><br /><br />
                                <asp:GridView ID="grdTravelIns" runat="server" CssClass="gridview" AutoGenerateColumns="False"  Width="100%">
                                <Columns> 
                                     <asp:BoundField DataField="PERNR" HeaderText="PERNR"  />
                                     <asp:BoundField DataField="TRINSNO" HeaderText="TRAVEL INSURANCE NO"  />
                                     <asp:BoundField DataField="EMPNAME" HeaderText="EMPLOYEE NAME"  />
                                     <asp:BoundField DataField="DOI" HeaderText="DATE OF ISSUE"  />
                                     <asp:BoundField DataField="DOE" HeaderText="DATE OF EXPIRE"  />
                                     <asp:BoundField DataField="PLAN1" HeaderText="PLAN"  />
                                     <asp:BoundField DataField="PREMIUM" HeaderText="PREMIUM"  />
                                     <asp:BoundField DataField="AGENT_NAME" HeaderText="AGENT NAME"  />
                                     <%--<asp:BoundField DataField="ICTYPE" HeaderText="TYPE" ReadOnly="True" />
                                     <asp:BoundField DataField="ICNUM" HeaderText="Ins. Number" ReadOnly="True"  />
                                     <asp:BoundField DataField="CREATED_ON" HeaderText="Validity" ReadOnly="True"  /> --%>
                                </Columns>
                             </asp:GridView> 
                                 
                         </asp:Panel>     
                  </div>
                </ContentTemplate>
                </cc1:TabPanel>


                 <%--FLYER_NUMBER--%>
                 <cc1:TabPanel runat="server" HeaderText="FLYER_NUMBER" ID="tabPFLYER_NUMBER" meta:resourcekey="TabPanel1Resource1">
            <HeaderTemplate>FLYER_NUMBER.</HeaderTemplate>
            <ContentTemplate>
                  <div>
                        <asp:Panel ID="pnlFLYER_NUMBER" runat="server" >
                            <br />
                              <h3>Employee's Flyer Number Details</h3>
                                <br />
                           
                                <asp:linkbutton id="lnkFLYER" runat="server" tooltip="Export this List" text="Export to Excel"

                onclick="lnkFLYER_Click"></asp:linkbutton><br /><br />
                                <asp:GridView ID="grdFLYER_NUMBER" runat="server" CssClass="gridview" AutoGenerateColumns="False"  Width="100%">
                                <Columns> 
                                     <asp:BoundField DataField="PERNR" HeaderText="PERNR"  />
                                    <asp:BoundField DataField="FRFLYNUM" HeaderText="FR FLY NUM"  />
                                    <asp:BoundField DataField="EMPNAME" HeaderText="EMPLOYEE NAME"  />
                                    <asp:BoundField DataField="AIRLINE" HeaderText="AIRLINE"  />
                                    <asp:BoundField DataField="VALSTATUS" HeaderText="VALIDITY STATUS"  />
                                    <%-- <asp:BoundField DataField="ICTYPE" HeaderText="TYPE" ReadOnly="True" />
                                     <asp:BoundField DataField="ICNUM" HeaderText="Membership Number" ReadOnly="True"  />
                                     <asp:BoundField DataField="CREATED_ON" HeaderText="Validity" ReadOnly="True"  /> --%>
                                </Columns>
                             </asp:GridView> 
                              
                         </asp:Panel>    
                  </div>
                </ContentTemplate>
                </cc1:TabPanel>

                <%--Membership NUM--%>
                 <%--<cc1:TabPanel runat="server" HeaderText="Membership No." ID="tabPMembershipNum" meta:resourcekey="TabPanel1Resource1">
            <HeaderTemplate>Membership No.</HeaderTemplate>
            <ContentTemplate>
                  <div>
                        <asp:Panel ID="pnlMembrshp" runat="server" >
                            <br />
                              <h3>Employee's Membership Details</h3>
                                <br />
                                <asp:GridView ID="grdMembership" runat="server" CssClass="gridview" AutoGenerateColumns="False"  Width="100%">
                                <Columns> 
                                     <asp:BoundField DataField="PERNR" HeaderText="PERNR"  />
                                     <asp:BoundField DataField="ICTYPE" HeaderText="TYPE" ReadOnly="True" />
                                     <asp:BoundField DataField="ICNUM" HeaderText="Membership Number" ReadOnly="True"  />
                                     <asp:BoundField DataField="CREATED_ON" HeaderText="Validity" ReadOnly="True"  /> 
                                </Columns>
                             </asp:GridView> 
                         </asp:Panel>    
                  </div>
                </ContentTemplate>
                </cc1:TabPanel>--%>

           </cc1:TabContainer> 
    
    </div>
    <br />
</asp:Content>
