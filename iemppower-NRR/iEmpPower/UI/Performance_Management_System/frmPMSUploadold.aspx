<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="UI_Performance_Management_System_frmPMSUpload" Codebehind="frmPMSUploadold.aspx.cs" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
  <%--<asp:Label ID="lblMessageBoard" runat="server"  CssClass="msgboard" ></asp:Label><br />

 <asp:Panel ID="pnlFill" runat="server">
 <h3>Upload Appraisal Form</h3>
     
    <div id="divEmployee" runat="server">
       <asp:Label ID="lblText" runat="server" CssClass="label" Text="Please Upload the Filled Appraisal Form" Width="250px"></asp:Label>
        <asp:TextBox ID="txtPath" runat="server" CssClass="textbox" Width="250px"></asp:TextBox>
        &nbsp;<asp:FileUpload ID="FileUpload1"  runat="server" Width="250px"/>
        &nbsp;<asp:Button ID="btnUpload" runat="server" Text="Upload" onclick="btnUpload_Click"/> 
   </div>    

</asp:Panel>--%>

     <h3>Upload Appraisal Form</h3>
  <asp:Label ID="lblMessageBoard" runat="server"  CssClass="msgboard" ></asp:Label><br />
     
    <div>
         <cc1:TabContainer ID="tcDefalut" runat="server" ActiveTabIndex="0" AutoPostBack="True">

       <cc1:TabPanel ID="tabSelfAppraisal" runat="server" HeaderText="TabPanel2" CssClass="gridview">
          <HeaderTemplate>Self Appraisal</HeaderTemplate>
            <ContentTemplate>
              <br />                
              
                         <div id="div1" runat="server">
                           <asp:Label ID="lbltxt1" runat="server" CssClass="label" Text="Please Upload the Filled Appraisal Form" Width="250px"></asp:Label> 
                            &nbsp;<asp:FileUpload ID="FileUploadSelf"  runat="server" Width="250px" />
                            &nbsp;<asp:Button ID="btnUploadSelf" runat="server" Text="Upload" OnClick="btnUploadSelf_Click" /> 
                       </div>             

            </ContentTemplate>
           </cc1:TabPanel>
     
       <cc1:TabPanel ID="tabEmployees" runat="server" HeaderText="TabPanel2" CssClass="gridview">
          <HeaderTemplate>Manager's Review</HeaderTemplate>
            <ContentTemplate>
             <br />  
                         <div id="div2" runat="server">
                           <asp:Label ID="lbltxt2" runat="server" CssClass="label" Text="Please Upload the Filled Appraisal Form" Width="250px"></asp:Label> 
                            &nbsp;<asp:FileUpload ID="FileUploadEmp"  runat="server" Width="250px" AllowMultiple="true" />
                            &nbsp;<asp:Button ID="btnUploadEmp" runat="server" Text="Upload" CssClass="btnStyle" OnClick="btnUploadEmp_Click" /> 
                        </div>     

            </ContentTemplate>
           </cc1:TabPanel>
         </cc1:TabContainer>
        </div>

</asp:Content>

