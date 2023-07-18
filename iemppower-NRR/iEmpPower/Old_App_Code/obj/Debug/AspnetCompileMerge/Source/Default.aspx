<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" EnableEventValidation="false"  AutoEventWireup="true" Theme="SkinFile" Inherits="_Default" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" Codebehind="Default.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

   <div>
    <h2>
        Welcome to iEmpPower
    </h2>
      <span class="hidden"><asp:Button ID="Button1" runat="server" Text=""  /></span> 
    
    <div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:GridView ID="grdEmployeeSubOrdinates" runat="server" CssClass="gridview" 
                    AllowPaging="True"  AutoGenerateColumns="False" 
                    AllowSorting="True" Width="100%"  PageSize="5" 
                onpageindexchanging="grdEmployeeSubOrdinates_PageIndexChanging" 
                onsorting="grdEmployeeSubOrdinates_Sorting">
            <Columns>
                 <asp:BoundField DataField="PERNR" HeaderText="PERNR"  
                    SortExpression="PERNR" />
                 <asp:BoundField DataField="ENAME" HeaderText="ENAME" ReadOnly="True" 
                    SortExpression="ENAME"/>
                 <asp:BoundField DataField="USRID" HeaderText="USRID" ReadOnly="True" 
                    SortExpression="USRID" />
            </Columns>
         </asp:GridView>
         </ContentTemplate>
         </asp:UpdatePanel>

    </div>
    <br />
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div>
    <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard" 
           meta:resourcekey="lblMessageBoardResource1"></asp:Label>
            <asp:Label ID="lblMessage" runat="server" CssClass="msgboard" 
           ></asp:Label>
    <div class="clear"></div>
       

        
        <cc1:TabContainer ID="tcDefalut" runat="server" ActiveTabIndex="1" 
            AutoPostBack="True" onactivetabchanged="TabContainer1_ActiveTabChanged" 
            meta:resourcekey="tcDefalutResource1" >
            <cc1:TabPanel runat="server" HeaderText="Pending" ID="tabPending" 
                meta:resourcekey="TabPanel1Resource1">
            <HeaderTemplate>Pending</HeaderTemplate>
            <ContentTemplate>
            <div>
          
                <asp:GridView ID="grdPending" runat="server" CssClass="gridview" 
                    AllowPaging="True" onpageindexchanging="grdPending_PageIndexChanging" 
                    onsorting="grdPending_Sorting" AutoGenerateColumns="False" 
                    AllowSorting="True" Width="100%" onrowdatabound="grdPending_RowDataBound" 
                    onselectedindexchanged="grdPending_SelectedIndexChanged" PageSize="10" 
                    meta:resourcekey="grdPendingResource1">
                     <Columns>
                  
                 <asp:BoundField DataField="PERNR" HeaderText="PERNR" Visible="False" 
                    SortExpression="PERNR" meta:resourcekey="BoundFieldResource1"/>
                 <asp:BoundField DataField="PKEY" HeaderText="PKEY" Visible="False" 
                    SortExpression="PKEY" meta:resourcekey="BoundFieldResource2"/>
                <asp:BoundField DataField="MANAGER_APPROVAL" HeaderText="Manager Approval" ReadOnly="True" 
                    SortExpression="MANAGER_APPROVAL"  
                    HtmlEncode="False" meta:resourcekey="BoundFieldResource3"/>
                    <asp:BoundField DataField="CHANGE_APPROVAL" HeaderText="Change Approval" ReadOnly="True" 
                    SortExpression="CHANGE_APPROVAL" meta:resourcekey="BoundFieldResource4"/>
                    <asp:BoundField DataField="REVIEW" HeaderText="Review" ReadOnly="True" 
                    SortExpression="REVIEW" meta:resourcekey="BoundFieldResource5"/>
                    <asp:BoundField DataField="LAST_ACTIVITY_DATE" HeaderText="Last Activity Date" ReadOnly="True" 
                    SortExpression="LAST_ACTIVITY_DATE" meta:resourcekey="BoundFieldResource6"   />
            </Columns>

                </asp:GridView>
                 
            </div>
            </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="tabCompleted" runat="server" HeaderText="TabPanel2" 
                CssClass="gridview" meta:resourcekey="tabCompletedResource1">
            <HeaderTemplate>Completed</HeaderTemplate>
            <ContentTemplate>
            <div>
            
              </asp:Label>
                <asp:GridView ID="grdCompleted" runat="server" AutoGenerateColumns="False" 
                    OnPageIndexChanging="grdCompleted_PageIndexChanging" 
                    OnSorting="grdCompleted_Sorting" AllowSorting="True" AllowPaging="True" 
                    Width="100%" OnRowDataBound="grdCompleted_RowDataBound" PageSize="10" 
                    OnSelectedIndexChanged="grdCompleted_SelectedIndexChanged" 
                    meta:resourcekey="grdCompletedResource1">
                     <Columns>
                <asp:BoundField DataField="PERNR" HeaderText="PERNR" Visible="False" 
                    SortExpression="PERNR" meta:resourcekey="BoundFieldResource7"/>
                 <asp:BoundField DataField="PKEY" HeaderText="PKEY" Visible="False" 
                    SortExpression="PKEY" meta:resourcekey="BoundFieldResource8"/>
                <asp:BoundField DataField="MANAGER_APPROVAL" HeaderText="Manager Approval" ReadOnly="True" 
                    SortExpression="MANAGER_APPROVAL" meta:resourcekey="BoundFieldResource9"  
                     />
                    <asp:BoundField DataField="CHANGE_APPROVAL" HeaderText="Change Approval" ReadOnly="True" 
                    SortExpression="CHANGE_APPROVAL" meta:resourcekey="BoundFieldResource10"/>
                    <asp:BoundField DataField="REVIEW" HeaderText="Review" ReadOnly="True" 
                    SortExpression="REVIEW" meta:resourcekey="BoundFieldResource11"/>
                    <asp:BoundField DataField="LAST_ACTIVITY_DATE" HeaderText="Last Activity Date" ReadOnly="True" 
                    SortExpression="LAST_ACTIVITY_DATE" meta:resourcekey="BoundFieldResource12"   />
            </Columns>
                </asp:GridView>
            </div>
            
            </ContentTemplate>
            </cc1:TabPanel>

        </cc1:TabContainer>
       
    </div>
    <div>
                    <asp:Panel ID="pnlAddressInfo" runat="server" 
                        meta:resourcekey="pnlAddressInfoResource1"><br />
           <asp:Label ID="Label1" runat="server" Text="Address Type" CssClass="label" 
                            meta:resourcekey="Label1Resource1"></asp:Label> 
        <asp:Label ID="lblValidateIDType" runat="server" CssClass="label" 
                            meta:resourcekey="lblValidateIDTypeResource1"></asp:Label>
                             <asp:Label ID="lblValidateNewIDType" runat="server" CssClass="label" 
                            ></asp:Label>
        <br />

        
     <asp:Label ID="lblDateFrom" runat="server" Text="Valid from" CssClass="label" 
                            meta:resourcekey="lblDateFromResource1"></asp:Label> 
        <asp:Label ID="lblValidateDateFrom" runat="server" CssClass="label" 
                            meta:resourcekey="lblValidateDateFromResource1"></asp:Label>
              <asp:Label ID="lblValidateNewDateFrom" runat="server"  CssClass="label"></asp:Label>
 <br />

       
        <asp:Label ID="lblDateTo" runat="server" Text="Valid to" CssClass="label" 
                            meta:resourcekey="lblDateToResource1"></asp:Label>
         <asp:Label ID="lblValidateDateTo" runat="server" CssClass="label" 
                            meta:resourcekey="lblValidateDateToResource1"></asp:Label>
             <asp:Label ID="lblValidateNewDateTo" runat="server"  CssClass="label"></asp:Label>
    <br />

        
     <asp:Label ID="lblAddress1" runat="server" Text="House number & street" CssClass="label" 
                            meta:resourcekey="lblAddress1Resource1"></asp:Label> 
     <asp:Label ID="lblValidateAddress1" runat="server" CssClass="label" 
                            meta:resourcekey="lblValidateAddress1Resource1"></asp:Label>
        <asp:Label ID="lblValidateNewAddress1" runat="server"  CssClass="label" ></asp:Label>
  <br />

     
     <asp:Label ID="lblAddress2" runat="server" Text="Address line 2" CssClass="label" 
                            meta:resourcekey="lblAddress2Resource1"></asp:Label>  
     <asp:Label ID="lblValidateAddress2" runat="server" CssClass="label" 
                            meta:resourcekey="lblValidateAddress2Resource1"></asp:Label>
     <asp:Label ID="lblValidateNewAddress2" runat="server"  CssClass="label" ></asp:Label>
     <br />

          <asp:Label ID="lblCountry" runat="server" Text="Country" 
              CssClass="label" meta:resourcekey="lblCountryResource1"></asp:Label>
            <asp:Label ID="lblValidateCountry" runat="server" 
              CssClass="label" meta:resourcekey="lblValidateCountryResource1"></asp:Label>
                            <asp:Label ID="lblValidateNewCountry" runat="server" CssClass="label"  ></asp:Label>
  <br />
         
     
     <asp:Label ID="lblState" runat="server" Text="State" CssClass="label" 
                            meta:resourcekey="lblStateResource1"></asp:Label> 
            <asp:Label ID="lblValidateState" runat="server" CssClass="label" 
                            meta:resourcekey="lblValidateStateResource1"></asp:Label>
                                        <asp:Label ID="lblValidateNewState" runat="server" CssClass="label"  ></asp:Label>
            <br />
          
     <asp:Label ID="lblPostalCityCode" runat="server" Text="Postal Code/City" CssClass="label" 
                            meta:resourcekey="lblPostalCityCodeResource1"></asp:Label>  
     <asp:Label ID="lblValidateCity" runat="server" CssClass="label" 
                            meta:resourcekey="lblValidateCityResource1"></asp:Label>
<asp:Label ID="lblValidateNewCity" runat="server" CssClass="label"  ></asp:Label>     <br />

        
     <asp:Label ID="lblPhoneNo" runat="server" Text="Telephone number" CssClass="label" 
                            meta:resourcekey="lblPhoneNoResource1"></asp:Label>  
     <asp:Label ID="lblValidatePhoneNo" runat="server" CssClass="label" 
                            meta:resourcekey="lblValidatePhoneNoResource1"></asp:Label>
  <asp:Label ID="lblValidateNewPhoneNo" runat="server" CssClass="label" ></asp:Label>   <br />
                        <asp:Label ID="lblAddressInfoComments" runat="server" Text="Remarks" 
                            CssClass="label" meta:resourcekey="lblAddressInfoCommentsResource1" ></asp:Label>
                        <asp:Label ID="lblValidateAddressInfoComments" runat="server" CssClass="label" 
                            meta:resourcekey="lblValidateAddressInfoCommentsResource1"></asp:Label>
         </asp:Panel>     

      <asp:Panel ID="pnlBankInfo" runat="server" meta:resourcekey="pnlBankInfoResource1"><br />

      <asp:Label ID="lblBankType" runat="server" Text="Bank Type" CssClass="label" 
              meta:resourcekey="lblBankTypeResource1"></asp:Label>
      <asp:Label ID="lblValidateBankType" runat="server" CssClass="label" 
              meta:resourcekey="lblValidateBankTypeResource1"></asp:Label>
           <asp:Label ID="lblValidateNewBankType" runat="server" CssClass="label" ></asp:Label>
     <br />
              
            
        <asp:Label ID="lblPayee" runat="server" Text="Payee" CssClass="label" 
              meta:resourcekey="lblPayeeResource1"></asp:Label>
         <asp:Label ID="lblValidatePayee" runat="server" CssClass="label" 
              meta:resourcekey="lblValidatePayeeResource1"></asp:Label>
<asp:Label ID="lblValidateNewPayee" runat="server" CssClass="label" ></asp:Label>     <br />
       
        <asp:Label ID="lblPostalCode" runat="server" Text="Postal Code/City" 
              CssClass="label" meta:resourcekey="lblPostalCodeResource1"></asp:Label>      
         <asp:Label ID="lblValidatePostalCode" runat="server" CssClass="label" 
              meta:resourcekey="lblValidatePostalCodeResource1"></asp:Label>
   <asp:Label ID="lblValidateNewPostalCode" runat="server" CssClass="label" ></asp:Label>  <br />
         
        <asp:Label ID="lblBankCountry" runat="server" Text="Bank Country" 
              CssClass="label" meta:resourcekey="lblBankCountryResource1"></asp:Label>
               <asp:Label ID="lblValidateBankCountry" runat="server" CssClass="label" 
              meta:resourcekey="lblValidateBankCountryResource1"></asp:Label>
 <asp:Label ID="lblValidateNewBankCountry" runat="server" CssClass="label" ></asp:Label>    <br />
           
         
        <asp:Label ID="lblBankkey" runat="server" Text="Bank key" CssClass="label" 
              meta:resourcekey="lblBankkeyResource1"></asp:Label>
              <asp:Label ID="lblValidateBankKey" runat="server" CssClass="label" 
              meta:resourcekey="lblValidateBankKeyResource1"></asp:Label>
  <asp:Label ID="lblValidateNewBankKey" runat="server" CssClass="label" ></asp:Label>   <br />
           
    
               <asp:Label ID="lblBankAccount" runat="server" Text="Bank Account" 
              CssClass="label" meta:resourcekey="lblBankAccountResource1"></asp:Label>
               <asp:Label ID="lblValidateBankAccount" runat="server" CssClass="label" 
              meta:resourcekey="lblValidateBankAccountResource1"></asp:Label>
 <asp:Label ID="lblValidateNewBankAccount" runat="server" CssClass="label" ></asp:Label>    <br />
                     
     
     <asp:Label ID="lblPaymentMethod" runat="server" Text="Payment Method" 
              CssClass="label" meta:resourcekey="lblPaymentMethodResource1"></asp:Label>
             <asp:Label ID="lblValidatePaymentMethod" runat="server" CssClass="label" 
              meta:resourcekey="lblValidatePaymentMethodResource1"></asp:Label>
 <asp:Label ID="lblValidateNewPaymentMethod" runat="server" CssClass="label" ></asp:Label>    <br />
              
     
      <asp:Label ID="lblPurpose" runat="server" Text="Purpose" CssClass="label" 
              meta:resourcekey="lblPurposeResource1"></asp:Label>
                 <asp:Label ID="lblValidatePurpose" runat="server" CssClass="label" 
              meta:resourcekey="lblValidatePurposeResource1"></asp:Label>
 <asp:Label ID="lblValidateNewPurpose" runat="server" CssClass="label" ></asp:Label>    <br />

      <asp:Label ID="lblPaymentCurrency" runat="server" Text="Payment Currency" 
              CssClass="label" meta:resourcekey="lblPaymentCurrencyResource1" ></asp:Label>
              <asp:Label ID="lblValidateCurrency" runat="server" CssClass="label" 
              meta:resourcekey="lblValidateCurrencyResource1"></asp:Label>
   <asp:Label ID="lblValidateNewCurrency" runat="server" CssClass="label" ></asp:Label>  <br />
      <asp:Label ID="lblBankComments" runat="server" Text="Remarks" CssClass="label" meta:resourcekey="lblBankCommentsResource1" 
              ></asp:Label>  
                       
              <asp:Label ID="lblValidateBankComments" runat="server" CssClass="label" meta:resourcekey="lblValidateBankCommentsResource1" 
             ></asp:Label>  
              <br />             
 </asp:Panel>

 <asp:Panel ID="pnlCommunicationInfo" runat="server" 
                        meta:resourcekey="pnlCommunicationInfoResource1">
<div>
     <asp:Label ID="lblExtension" runat="server" Text="Extension" CssClass="label" 
         meta:resourcekey="lblExtensionResource1"></asp:Label>  
     <asp:Label ID="lblValidateExtension" runat="server" CssClass="label" 
         meta:resourcekey="lblValidateExtensionResource1"></asp:Label>
              <asp:Label ID="lblValidateNewExtension" runat="server" CssClass="label" ></asp:Label>
     </div>    
     <div>
     <asp:Label ID="lblEmail" runat="server" Text="E-mail" CssClass="label" 
         meta:resourcekey="lblEmailResource1"></asp:Label> 
      <asp:Label ID="lblValidateEmail" runat="server" CssClass="label-big" 
         meta:resourcekey="lblValidateEmailResource1"></asp:Label>
        
     <asp:Label ID="lblValidateNewEmail" runat="server" CssClass="label-big" ></asp:Label>    </div>
     <div> 
     
     <asp:Label ID="lblBuildingNumber" runat="server" Text="Building Number " 
         CssClass="label" meta:resourcekey="lblBuildingNumberResource1"></asp:Label>  
    <asp:Label ID="lblValidateBuildingNumber" runat="server" CssClass="label" 
         meta:resourcekey="lblValidateBuildingNumberResource1"></asp:Label>
    <asp:Label ID="lblValidateNewBuildingNumber" runat="server" CssClass="label" ></asp:Label> </div> 
     <div>
     <asp:Label ID="lblRoomNumber" runat="server" Text="Room Number " 
         CssClass="label" meta:resourcekey="lblRoomNumberResource1"></asp:Label>
     <asp:Label ID="lblValidateRoomNumber" runat="server" CssClass="label" 
         meta:resourcekey="lblValidateRoomNumberResource1"></asp:Label>
   <asp:Label ID="lblValidateNewRoomNumber" runat="server" CssClass="label"></asp:Label> </div>
      <div>  
     <asp:Label ID="lblLicencePlateNumber" runat="server" 
         Text="Licence Plate Number " CssClass="label" 
         meta:resourcekey="lblLicencePlateNumberResource1"></asp:Label> 
     <asp:Label ID="lblValidateLicencePlateNumber" runat="server" CssClass="label" 
         meta:resourcekey="lblValidateLicencePlateNumberResource1"></asp:Label>
   <asp:Label ID="lblValidateNewLicencePlateNumber" runat="server" CssClass="label" ></asp:Label>  </div>
   <div>
   <asp:Label ID="lblMobileNo" runat="server" 
         Text="Mobile Number " CssClass="label" ></asp:Label> 
     <asp:Label ID="lblValidateMobileNo" runat="server" CssClass="label" ></asp:Label>
     <asp:Label ID="lblValidateNewMobileNo" runat="server"  CssClass="label" ></asp:Label>
   </div>
     <div >
         <asp:Label ID="lblCommunicationInfoCommnets" runat="server" Text="Remarks" 
         CssClass="label" ></asp:Label>  
                       
          <asp:Label ID="lblValidateCommunicationInfoCommnets" runat="server" CssClass="label" 
                            ></asp:Label>  
    </div>
     </asp:Panel>

     <asp:Panel ID="pnlpersonaldataInfo" runat="server" 
                        meta:resourcekey="pnlpersonaldataInfoResource1"><br />
 <asp:Label ID="lblEmpPhoto" runat="server" Text="Photo"  CssClass="label" ></asp:Label>
 <asp:Label ID="Label2" runat="server" CssClass="label" ></asp:Label>
                <asp:Image ID="imagePhoto" runat="server" Height="35px" Width="35px" 
           ImageUrl="~/images/default-emp.jpg"/>
          
    <br /> 

     <asp:Label ID="lblPDTitle" runat="server" Text="Title"  CssClass="label" 
             meta:resourcekey="lblPDTitleResource1"></asp:Label>
     <asp:Label ID="lblValidatePDTitle" runat="server" CssClass="label" 
             meta:resourcekey="lblValidatePDTitleResource1"></asp:Label>
   <asp:Label ID="lblValidateNewPDTitle" runat="server" CssClass="label" ></asp:Label>  <br /> 
           
    <asp:Label ID="lblPDFirstName" runat="server" Text="First name " CssClass="label" 
             meta:resourcekey="lblPDFirstNameResource1"></asp:Label>  
        <asp:Label ID="lblValidatePDFirstName" runat="server" CssClass="label" 
             meta:resourcekey="lblValidatePDFirstNameResource1"></asp:Label>
   <asp:Label ID="lblValidateNewPDFirstName" runat="server" CssClass="label" ></asp:Label>  <br /> 
           
    <asp:Label ID="lblPDLastName" runat="server" Text="Last name " CssClass="label" 
             meta:resourcekey="lblPDLastNameResource1"></asp:Label>  
        <asp:Label ID="lblValidatePDLastName" runat="server" CssClass="label" 
             meta:resourcekey="lblValidatePDLastNameResource1"></asp:Label>
 <asp:Label ID="lblValidateNewPDLastName" runat="server" CssClass="label" ></asp:Label>    <br /> 
            
   <asp:Label ID="lblPDBirthName" runat="server" Text="Name at birth " CssClass="label" 
             meta:resourcekey="lblPDBirthNameResource1"></asp:Label> 
          <asp:Label ID="lblValidatePDBirthName" runat="server" CssClass="label" 
             meta:resourcekey="lblValidatePDBirthNameResource1"></asp:Label>
    <asp:Label ID="lblValidateNewPDBirthName" runat="server" CssClass="label" ></asp:Label> <br /> 
            
   <asp:Label ID="lblPDInitials" runat="server" Text="Initials" CssClass="label" 
             meta:resourcekey="lblPDInitialsResource1"></asp:Label>  
         <asp:Label ID="lblValidatePDInitials" runat="server" CssClass="label" 
             meta:resourcekey="lblValidatePDInitialsResource1"></asp:Label>
             <asp:Label ID="lblValidateNewPDInitials" runat="server" CssClass="label" ></asp:Label>
 <br />
           
    <asp:Label ID="lblPDKnownAs" runat="server" Text="Known as" CssClass="label" 
             meta:resourcekey="lblPDKnownAsResource1"></asp:Label>  
         <asp:Label ID="lblValidatePDKnownAs" runat="server" CssClass="label" 
             meta:resourcekey="lblValidatePDKnownAsResource1"></asp:Label>
    <asp:Label ID="lblValidateNewPDKnownAs" runat="server" CssClass="label" ></asp:Label> <br />
               
    <asp:Label ID="lblPDLanguage" runat="server" Text="Language" CssClass="label" 
             meta:resourcekey="lblPDLanguageResource1"></asp:Label> 
     <asp:Label ID="lblValidatePDLanguage" runat="server" CssClass="label" 
             meta:resourcekey="lblValidatePDLanguageResource1"></asp:Label>
     <asp:Label ID="lblValidateNewPDLanguage" runat="server" CssClass="label" ></asp:Label><br />
           
   <asp:Label ID="lblPDGender" runat="server" Text="Gender" CssClass="label" 
             meta:resourcekey="lblPDGenderResource1"></asp:Label>
     <asp:Label ID="lblValidatePDGender" runat="server" CssClass="label" 
             meta:resourcekey="lblValidatePDGenderResource1"></asp:Label>
   <asp:Label ID="lblValidateNewPDGender" runat="server" CssClass="label" ></asp:Label>  <br />
     
    <asp:Label ID="lblPDDOB" runat="server" Text="Date of birth " CssClass="label" 
             meta:resourcekey="lblPDDOBResource1"></asp:Label>  
          <asp:Label ID="lblValidatePDDOB" runat="server" CssClass="label" 
             meta:resourcekey="lblValidatePDDOBResource1"></asp:Label>
   <asp:Label ID="lblValidateNewPDDOB" runat="server" CssClass="label" ></asp:Label>  <br />
       
        <asp:Label ID="lblPDPOB" runat="server" Text="Place of birth " CssClass="label" 
             meta:resourcekey="lblPDPOBResource1"></asp:Label>  
         <asp:Label ID="lblValidatePDPOB" runat="server" CssClass="label" 
             meta:resourcekey="lblValidatePDPOBResource1"></asp:Label>
     <asp:Label ID="lblValidateNewPDPOB" runat="server" CssClass="label" ></asp:Label><br />
     
    
    <asp:Label ID="lblPDCOB" runat="server" Text="Country of birth " CssClass="label" 
             meta:resourcekey="lblPDCOBResource1"></asp:Label>  
       <asp:Label ID="lblValidatePDCOB" runat="server" CssClass="label" 
             meta:resourcekey="lblValidatePDCOBResource1"></asp:Label>
    <asp:Label ID="lblValidateNewPDCOB" runat="server" CssClass="label" ></asp:Label> <br />
          
            
    <asp:Label ID="lblPDNationality" runat="server" Text="Nationality" CssClass="label" meta:resourcekey="lblPDNationalityResource1"
           ></asp:Label> 
          <asp:Label ID="lblValidatePDNationality" runat="server" CssClass="label" 
             meta:resourcekey="lblValidatePDNationalityResource1"></asp:Label>
    <asp:Label ID="lblValidateNewPDNationality" runat="server" CssClass="label" ></asp:Label> <br />
          
        
    <asp:Label ID="lblPDState" runat="server" Text="State" CssClass="label" 
             meta:resourcekey="lblPDStateResource1"></asp:Label>  
        <asp:Label ID="lblValidatePDState" runat="server" CssClass="label" 
             meta:resourcekey="lblValidatePDStateResource1"></asp:Label>
     <asp:Label ID="lblValidateNewPDState" runat="server" CssClass="label" ></asp:Label><br />
           
   
   <asp:Label ID="lblPD2ndNtnlty" runat="server" Text="2nd nationality" CssClass="label" 
             meta:resourcekey="lblPD2ndNtnltyResource1"></asp:Label>
            <asp:Label ID="lblValidatePD2ndNtnlty" runat="server" CssClass="label" 
             meta:resourcekey="lblValidatePD2ndNtnltyResource1"></asp:Label>
     <asp:Label ID="lblValidateNewPD2ndNtnlty" runat="server" CssClass="label" ></asp:Label><br />
          
   <asp:Label ID="lblPD3rdNtnlty" runat="server" Text="3rd nationality" CssClass="label" 
             meta:resourcekey="lblPD3rdNtnltyResource1"></asp:Label>
    <asp:Label ID="lblValidatePD3rdNtnlty" runat="server" CssClass="label" 
             meta:resourcekey="lblValidatePD3rdNtnltyResource1"></asp:Label>
    <asp:Label ID="lblValidateNewPD3rdNtnlty" runat="server" CssClass="label" ></asp:Label> <br />
          
   <asp:Label ID="lblPDMaritalStatus" runat="server" Text="Marital Status" 
             CssClass="label" meta:resourcekey="lblPDMaritalStatusResource1"></asp:Label>  
       <asp:Label ID="lblValidatePDMaritalStatus" runat="server" CssClass="label" 
             meta:resourcekey="lblValidatePDMaritalStatusResource1"></asp:Label>
    <asp:Label ID="lblValidateNewPDMaritalStatus" runat="server" CssClass="label" ></asp:Label> <br />
           
    <asp:Label ID="lblPDMarriedSince" runat="server" Text="Married since" 
             CssClass="label" meta:resourcekey="lblPDMarriedSinceResource1"></asp:Label>  
        <asp:Label ID="lblValidatePDMarriedSince" runat="server" CssClass="label" 
             meta:resourcekey="lblValidatePDMarriedSinceResource1"></asp:Label>
    <asp:Label ID="lblValidateNewPDMarriedSince" runat="server" CssClass="label" ></asp:Label> <br />
        
        
    <asp:Label ID="lblPDNoOfChildren" runat="server" Text="Number of Children " 
             CssClass="label" meta:resourcekey="lblPDNoOfChildrenResource1"></asp:Label>  
       <asp:Label ID="lblValidatePDNoOfChildren" runat="server" CssClass="label" 
             meta:resourcekey="lblValidatePDNoOfChildrenResource1"></asp:Label>
    <asp:Label ID="lblValidateNewPDNoOfChildren" runat="server" CssClass="label" ></asp:Label> <br />
     
<asp:Label ID="lblPDReligion" runat="server" Text="Religion" CssClass="label" 
             meta:resourcekey="lblPDReligionResource1"></asp:Label>  
         <asp:Label ID="lblValidatePDReligion" runat="server" CssClass="label" 
             meta:resourcekey="lblValidatePDReligionResource1"></asp:Label>
    <asp:Label ID="lblValidateNewPDReligion" runat="server" CssClass="label" ></asp:Label> <br />
      <asp:Label ID="lblPDCommnets" runat="server" Text="Remarks" CssClass="label" meta:resourcekey="lblPDCommnetsResource1" 
             ></asp:Label>  
                        
          <asp:Label ID="lblValidatePDCommnets" runat="server" CssClass="label" meta:resourcekey="lblValidatePDCommnetsResource1" 
            ></asp:Label>      
      </asp:Panel>


        <asp:Panel ID="pnlPersonalIdInfo" runat="server" 
                        meta:resourcekey="pnlPersonalIdInfoResource1">
<div>
      <asp:Label ID="lblIDype" runat="server" Text="ID type"  CssClass="label" 
                meta:resourcekey="lblIDypeResource1"></asp:Label> 
            <asp:Label ID="lblValidateIDype" runat="server" CssClass="label" 
                meta:resourcekey="lblValidateIDypeResource1"></asp:Label>
     <asp:Label ID="lblValidateNewIDype" runat="server" CssClass="label" ></asp:Label> 
      </div> 
      <div> 
           <asp:Label ID="lblIDNo" runat="server" Text="ID number" CssClass="label" meta:resourcekey="lblIDNoResource1"
                ></asp:Label>  

             <asp:Label ID="lblValidateIDNo" runat="server" CssClass="label" 
                meta:resourcekey="lblValidateIDNoResource1"></asp:Label>
      <asp:Label ID="lblValidateNewIDNo" runat="server" CssClass="label" ></asp:Label>
      </div> 
      <div>
        <asp:Label ID="lblPIComments" runat="server" Text="Remarks" CssClass="label" meta:resourcekey="lblPICommentsResource1" 
               ></asp:Label>  
                 <asp:Label ID="lblValidatePIComments" runat="server" CssClass="label" meta:resourcekey="lblValidatePICommentsResource1" 
                ></asp:Label>  
     </div>

         </asp:Panel>

         
          <asp:Panel ID="pnlClockInOut" runat="server" meta:resourcekey="pnlClockInOutResource1">
             <asp:Label ID="lblRequestStatus" runat="server" Text="Request status"  CssClass="label" 
                 ></asp:Label>
        <asp:Label ID="lblValidateRequestStatus" runat="server" CssClass="label" 
                  ></asp:Label>
          <asp:Label ID="lblValidateNewRequestStatus" runat="server" CssClass="label" ></asp:Label>        <br />
        <asp:Label ID="lblDate" runat="server" Text="Date"  CssClass="label" 
                  meta:resourcekey="lblDateResource1"></asp:Label>
        <asp:Label ID="lblValidateDate" runat="server" CssClass="label" 
                  meta:resourcekey="lblValidateDateResource1"></asp:Label>
       <asp:Label ID="lblValidateNewDate" runat="server" CssClass="label" ></asp:Label> <br /> 
       
        <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="label" 
                  meta:resourcekey="lblTimeResource1"></asp:Label>
        <asp:Label ID="lblValidateTime" runat="server" CssClass="label" 
                  meta:resourcekey="lblValidateTimeResource1"></asp:Label>
      <asp:Label ID="lblValidateNewTime" runat="server" CssClass="label" ></asp:Label>  <br />

        <asp:Label ID="lblClockInOut" runat="server" Text="Clock-In/Out" CssClass="label" 
                  meta:resourcekey="lblClockInOutResource1"></asp:Label>
        <asp:Label ID="lblValidateClockInOut" runat="server" CssClass="label" 
                  meta:resourcekey="lblValidateClockInOutResource1"></asp:Label>
       <asp:Label ID="lblValidateNewClockInOut" runat="server" CssClass="label" ></asp:Label><br />

         <asp:Label ID="lblApprovedBy" runat="server" Text="Approved by" CssClass="label" 
                  meta:resourcekey="lblApprovedByResource1"></asp:Label>
        <asp:Label ID="lblValidateApprovedBy" runat="server" CssClass="label" 
                  meta:resourcekey="lblValidateApprovedByResource1"></asp:Label>
       <asp:Label ID="lblValidateNewApprovedBy" runat="server" CssClass="label" ></asp:Label><br />

        <asp:Label ID="lblNote" runat="server" Text="Note" CssClass="label" 
                  meta:resourcekey="lblNoteResource1"></asp:Label>
       <asp:Label ID="lblValidateNote" runat="server" CssClass="label" 
                  meta:resourcekey="lblValidateNoteResource1"></asp:Label>
       <asp:Label ID="lblValidateNewNote" runat="server" CssClass="label" ></asp:Label><br />
        <asp:Label ID="lblClockInOutComments" runat="server" Text="Remarks" 
                  CssClass="label" ></asp:Label>  
                         <asp:Label ID="lblValidateClockInOutComments" runat="server" Text="" 
                  CssClass="label" ></asp:Label>  
    </asp:Panel>

     <asp:Panel ID="pnlLeaveRequest" runat="server" meta:resourcekey="pnlLeaveRequestResource1">
      <asp:Label ID="lblTypeOfLeave" runat="server" Text="Type of leave" CssClass="label"
             meta:resourcekey="lblTypeOfLeaveResource1"></asp:Label>
     <asp:Label ID="lblValidateTypeOfLeave" runat="server" CssClass="label" 
             meta:resourcekey="lblValidateTypeOfLeaveResource1"></asp:Label>
       <br />
     
      <asp:Label ID="lblFromDate" runat="server" Text="From date" CssClass="label" 
             meta:resourcekey="lblFromDateResource1"></asp:Label>
      <asp:Label ID="lblValidateFromDate" runat="server" CssClass="label" 
             meta:resourcekey="lblValidateFromDateResource1"></asp:Label>
       <br />

        <asp:Label ID="lblToDate" runat="server" Text="To date" CssClass="label" 
             meta:resourcekey="lblToDateResource1"></asp:Label>
       <asp:Label ID="lblValidateToDate" runat="server" CssClass="label" 
             meta:resourcekey="lblValidateToDateResource1"></asp:Label>
       <br />

        <asp:Label ID="lblFromTime" runat="server" Text="From time" CssClass="label" 
             meta:resourcekey="lblFromTimeResource1"></asp:Label>
       <asp:Label ID="lblValidateFromTime" runat="server" CssClass="label" 
             meta:resourcekey="lblValidateFromTimeResource1"></asp:Label>
       <br />

        <asp:Label ID="lblToTime" runat="server" Text="To time" CssClass="label" 
             meta:resourcekey="lblToTimeResource1"></asp:Label>
        <asp:Label ID="lblValidateToTime" runat="server" CssClass="label" 
             meta:resourcekey="lblValidateToTimeResource1"></asp:Label>
       <br />

        <asp:Label ID="lblDuration" runat="server" Text="Duration" CssClass="label" 
             meta:resourcekey="lblDurationResource1"></asp:Label>
         <asp:Label ID="lblValidateDuration" runat="server" CssClass="label" 
             meta:resourcekey="lblValidateDurationResource1"></asp:Label>
       <br />

        <asp:Label ID="lblApprover" runat="server" Text="Approver" CssClass="label" 
             meta:resourcekey="lblApproverResource1"></asp:Label>
    <asp:Label ID="lblValidateApprover" runat="server" CssClass="label" 
             meta:resourcekey="lblValidateApproverResource1"></asp:Label>
       <br />

    <asp:Label ID="lblNoteForApprover" runat="server" Text="Note for approver" 
             CssClass="label" meta:resourcekey="lblNoteForApproverResource1"></asp:Label>
         <asp:Label ID="lblValidateNoteForApprover" runat="server" CssClass="label" 
             meta:resourcekey="lblValidateNoteForApproverResource1"></asp:Label>
       <br />
        <asp:Label ID="lblLRComments" runat="server" Text="Remarks" CssClass="label" 
                     ></asp:Label>  
                     
              <asp:Label ID="lblValidateLRComments" runat="server" CssClass="label" 
                     ></asp:Label>  
      </asp:Panel>
                  <asp:Panel ID="pnlFamilyMember" runat="server" 
                        meta:resourcekey="pnlFamilyMemberResource1">
            <asp:Label ID="lblFMMembertype" runat="server" Text="Member type" CssClass="label" 
                     meta:resourcekey="lblFMMembertypeResource1" ></asp:Label> 
         <asp:Label ID="lblValidateFMMembertype" runat="server" CssClass="label" 
                     meta:resourcekey="lblValidateFMMembertypeResource1" ></asp:Label>
     <asp:Label ID="lblValidateNewFMMembertype" runat="server" CssClass="label" ></asp:Label>      <br />      
         <asp:Label ID="lblFMFirstName" runat="server" Text="First name " CssClass="label" 
                     meta:resourcekey="lblFMFirstNameResource1"></asp:Label>  
           <asp:Label ID="lblValidateFMFirstName" runat="server" CssClass="label" 
                     meta:resourcekey="lblValidateFMFirstNameResource1"></asp:Label>
        <asp:Label ID="lblValidateNewFMFirstName" runat="server" CssClass="label"></asp:Label>        <br />
  
            <asp:Label ID="lblFMLastName" runat="server" Text="Last name " CssClass="label" 
                     meta:resourcekey="lblFMLastNameResource1"></asp:Label>  
            
                 <asp:Label ID="lblValidateFMLastName" runat="server" CssClass="label" 
                     meta:resourcekey="lblValidateFMLastNameResource1" ></asp:Label> 
     <asp:Label ID="lblValidateNewFMLastName" runat="server" CssClass="label" ></asp:Label>           <br />
   
            <asp:Label ID="lblFMNameOfBirth" runat="server" Text="Name of birth  " 
                     CssClass="label" meta:resourcekey="lblFMNameOfBirthResource1"></asp:Label>  
             <asp:Label ID="lblValidateFMNameOfBirth" runat="server" CssClass="label" 
                     meta:resourcekey="lblValidateFMNameOfBirthResource1"></asp:Label>
       <asp:Label ID="lblValidateNewFMNameOfBirth" runat="server" CssClass="label" ></asp:Label>     <br />
   
            <asp:Label ID="lbFMInitials" runat="server" Text="Initials  " CssClass="label" 
                     meta:resourcekey="lbFMInitialsResource1"></asp:Label> 
            
                 <asp:Label ID="lblValidateFMInitials" runat="server" CssClass="label" 
                     meta:resourcekey="lblValidateFMInitialsResource1" ></asp:Label>
 <asp:Label ID="lblValidateNewFMInitials" runat="server" CssClass="label" ></asp:Label>               <br />
   
            <asp:Label ID="lblFMOtherTitle" runat="server" Text="Other title" CssClass="label" 
                     meta:resourcekey="lblFMOtherTitleResource1"></asp:Label> 
                 <asp:Label ID="lblValidateFMOtherTitle" runat="server" CssClass="label" 
                     meta:resourcekey="lblValidateFMOtherTitleResource1" ></asp:Label>
     <asp:Label ID="lblValidateNewFMOtherTitle" runat="server" CssClass="label" ></asp:Label>           
                <br />
     
            <asp:Label ID="lblFMNamePrefix" runat="server" Text="Name prefix" CssClass="label" 
                     meta:resourcekey="lblFMNamePrefixResource1"></asp:Label> 
                <asp:Label ID="lblValidateFMNamePrefix" runat="server" CssClass="label" 
                     meta:resourcekey="lblValidateFMNamePrefixResource1"></asp:Label>
      <asp:Label ID="lblValidateNewFMNamePrefix" runat="server" CssClass="label" ></asp:Label>          
                <br />
  
            <asp:Label ID="lblFMGender" runat="server" Text="Gender" CssClass="label" 
                     meta:resourcekey="lblFMGenderResource1"></asp:Label> 
     <asp:Label ID="lblValidateFMGender" runat="server"  CssClass="label" 
                     meta:resourcekey="lblValidateFMGenderResource1"></asp:Label> 
   <asp:Label ID="lblValidateNewFMGender" runat="server"  CssClass="label" ></asp:Label>
  <br />

  
            <asp:Label ID="lblFMDOB" runat="server" Text="Date of birth " CssClass="label" 
                     meta:resourcekey="lblFMDOBResource1" ></asp:Label>  
                <asp:Label ID="lblValidateFMDOB" runat="server" CssClass="label" 
                     meta:resourcekey="lblValidateFMDOBResource1"></asp:Label> 
 <asp:Label ID="lblValidateNewFMDOB" runat="server" CssClass="label" ></asp:Label>       <br />
            <asp:Panel ID="pnlChildAllowance" runat="server" 
                     meta:resourcekey="pnlChildAllowanceResource1">
                <asp:Label ID="lblOtherAllowance" runat="server"  Text="Other allowances " 
                    CssClass="label" meta:resourcekey="lblOtherAllowanceResource1"/>
                <asp:Label ID="lblValidateOtherAllowance" runat="server" CssClass="label" 
                    meta:resourcekey="lblValidateOtherAllowanceResource1"/>
         <asp:Label ID="lblValidateNewOtherAllowance" runat="server" CssClass="label" ></asp:Label>       <br />
                <asp:Label ID="lblHostelAllowance" runat="server" CssClass="label"
                    Text="Child Hostel Allowance " 
                    meta:resourcekey="lblHostelAllowanceResource1"/>
                    <asp:Label ID="lblValidateHostelAllowance" runat="server" CssClass="label" 
                    meta:resourcekey="lblValidateHostelAllowanceResource1"/>
          <asp:Label ID="lblValidateNewHostelAllowance" runat="server" CssClass="label" ></asp:Label>          <br />
                <asp:Label ID="lblEducationalAllowance" runat="server" CssClass="label"
                    Text="Child Educational Allowance " 
                    meta:resourcekey="lblEducationalAllowanceResource1" />
                     <asp:Label ID="lblValidateEducationalAllowance" runat="server" 
                    CssClass="label" meta:resourcekey="lblValidateEducationalAllowanceResource1" />
     <asp:Label ID="lblValidateNewEducationalAllowance" runat="server" 
                    CssClass="label" ></asp:Label>               <br />
            </asp:Panel>
        
            <asp:Label ID="lblFMPOB" runat="server" Text="Place of birth " CssClass="label" 
                     meta:resourcekey="lblFMPOBResource1"></asp:Label>  
            
                <asp:Label ID="lblValidateFMPOB" runat="server" CssClass="label" 
                     meta:resourcekey="lblValidateFMPOBResource1"></asp:Label>
   <asp:Label ID="lblValidateNewFMPOB" runat="server" CssClass="label" ></asp:Label>             
                <br />
    
            <asp:Label ID="lblFMCOB" runat="server" Text="Country of birth" CssClass="label" 
                     meta:resourcekey="lblFMCOBResource1"></asp:Label>  
            <asp:Label ID="lblValidateFMCOB" runat="server" CssClass="label" 
                     meta:resourcekey="lblValidateFMCOBResource1" ></asp:Label>
        <asp:Label ID="lblValidateNewFMCOB" runat="server" CssClass="label" ></asp:Label>        <br />
                 <asp:Label ID="lblFMNationality" runat="server" Text="Nationality" 
                     CssClass="label" meta:resourcekey="lblFMNationalityResource1"></asp:Label>  
            <asp:Label ID="lblValidateFMNationality" runat="server" CssClass="label" 
                     meta:resourcekey="lblValidateFMNationalityResource1" ></asp:Label>
       <asp:Label ID="lblValidateNewFMNationality" runat="server" CssClass="label" ></asp:Label>         <br />
     
            <asp:Label ID="lblFMNationality2" runat="server" Text="Nationality2" 
                     CssClass="label" meta:resourcekey="lblFMNationality2Resource1"></asp:Label>  
            <asp:Label ID="lblValidateFMNationality2" runat="server" CssClass="label" 
                     meta:resourcekey="lblValidateFMNationality2Resource1"></asp:Label>
       <asp:Label ID="lblValidateNewFMNationality2" runat="server" CssClass="label" ></asp:Label>         
                <br />
            
            <asp:Label ID="lblFMNationality3" runat="server" Text="Nationality3" 
                     CssClass="label" meta:resourcekey="lblFMNationality3Resource1"></asp:Label>  
            <asp:Label ID="lblValidateFMNationality3" runat="server" CssClass="label" 
                     meta:resourcekey="lblValidateFMNationality3Resource1"></asp:Label>
             <asp:Label ID="lblValidateNewFMNationality3" runat="server" CssClass="label" ></asp:Label>   <br />
                 <asp:Label ID="lblFMComments" runat="server" Text="Remarks" CssClass="label" 
                     meta:resourcekey="lblFMCommentsResource1"></asp:Label>  
                     
              <asp:Label ID="lblValidateFMComments" runat="server" CssClass="label" 
                     meta:resourcekey="lblValidateFMCommentsResource1"></asp:Label>  

     <br />


          </asp:Panel>
          <asp:Panel ID="pnlRecordWorking" runat="server" >
            <asp:gridview ID="grdRecordTime"  runat="server"  ShowFooter="True"  width="100%"
            AutoGenerateColumns="False"  > 
           
        <Columns> 
         <asp:TemplateField HeaderText="Cost center"  HeaderStyle-CssClass="hd-small"
                > 
             <ItemTemplate> 
                <asp:Label ID="drpdwnCostCenter" runat="server"                  
                      > 
                </asp:Label>                
            </ItemTemplate> 
        </asp:TemplateField>
          <asp:TemplateField HeaderText="Order" 
                HeaderStyle-CssClass="hd-small" > 
             <ItemTemplate> 
                <asp:Label ID="drpdwnOrder" runat="server"  > 
                </asp:Label> 
                
            </ItemTemplate> 

<HeaderStyle CssClass="hd-small"></HeaderStyle>

<ItemStyle Width="30px"></ItemStyle>
        </asp:TemplateField> 
                <asp:TemplateField HeaderText="Attendance/Absence type" ItemStyle-Width="30" 
                HeaderStyle-CssClass="hd-small" > 
             <ItemTemplate> 
                <asp:Label ID="drpdwnAttabsType" runat="server" 
                     
                     > 
                </asp:Label> 
            </ItemTemplate> 

<HeaderStyle CssClass="hd-small"></HeaderStyle>

<ItemStyle Width="30px"></ItemStyle>
        </asp:TemplateField>
         

  <asp:TemplateField HeaderText="Staff Grade" HeaderStyle-CssClass="hd-small" 
                >
   <ItemTemplate>
    <asp:Label ID="txtStaffGrade" runat="server" Width="30px" 
           ></asp:Label>

   </ItemTemplate>
     <ItemStyle HorizontalAlign="Center" /> 
    <FooterStyle HorizontalAlign="Center" /> 
            <FooterTemplate> 
            <div>
            <asp:Label ID="lblStaffGrade" runat="server" Text="Actual" CssClass="label" 
                    Width="40px"  ></asp:Label>
            </div>
            </FooterTemplate> 

<HeaderStyle CssClass="hd-small"></HeaderStyle>

  </asp:TemplateField>
  <asp:TemplateField HeaderText="Hours" HeaderStyle-CssClass="hd-small" 
                HeaderStyle-Width="30"  >
   <ItemTemplate>
     <asp:Label ID="txtTotal" runat="server" Width="30px" ReadOnly="True" 
           ></asp:Label>
   </ItemTemplate>
     <ItemStyle HorizontalAlign="Center" /> 
   <FooterStyle HorizontalAlign="Center" /> 
            <FooterTemplate> 
            <div>
            <asp:Label ID="lblHours" runat="server" CssClass="label" Width="40px" style="text-align:right;"
                     ></asp:Label>
            </div>
   </FooterTemplate> 

<HeaderStyle CssClass="hd-small"></HeaderStyle>
  </asp:TemplateField>
          <asp:TemplateField HeaderText="SUN" HeaderStyle-CssClass="hd-small" 
                > 
            <ItemTemplate>
    <asp:Label ID="txtSUN" runat="server"  Width="30px"  
                      ></asp:Label>
      
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
                >
   <ItemTemplate>
       <asp:Label ID="txtMON" runat="server" Width="30px"  ></asp:Label>
      
   </ItemTemplate>
     <ItemStyle HorizontalAlign="Center" /> 
  <FooterStyle HorizontalAlign="Center" /> 
            <FooterTemplate> 
            <div>
            <asp:Label ID="lblMon" runat="server" CssClass="label" Width="40px" style="text-align:right;"
                     ></asp:Label>
            </div>
   </FooterTemplate> 

<HeaderStyle CssClass="hd-small"></HeaderStyle>
  </asp:TemplateField>
  <asp:TemplateField HeaderText="TUE" HeaderStyle-CssClass="hd-small" 
                >
   <ItemTemplate>
    <asp:Label ID="txtTUE" runat="server" Width="30px"
            ></asp:Label>
      
   </ItemTemplate>
     <ItemStyle HorizontalAlign="Center" /> 
  <FooterStyle HorizontalAlign="Center" /> 
            <FooterTemplate> 
            <div>
            <asp:Label ID="lblTues" runat="server" CssClass="label" Width="40px" style="text-align:right;"
                     ></asp:Label>
            </div>
   </FooterTemplate> 

<HeaderStyle CssClass="hd-small"></HeaderStyle>
  </asp:TemplateField>
  <asp:TemplateField HeaderText="WED" HeaderStyle-CssClass="hd-small" 
                >
   <ItemTemplate>
    <asp:Label ID="txtWED" runat="server" Width="30px" 
             ></asp:Label>
     
   </ItemTemplate>
     <ItemStyle HorizontalAlign="Center" /> 
 <FooterStyle HorizontalAlign="Center" /> 
            <FooterTemplate> 
            <div>
            <asp:Label ID="lblWed" runat="server" CssClass="label" Width="40px" style="text-align:right;"
                     ></asp:Label>
            </div>
   </FooterTemplate> 
                  
<HeaderStyle CssClass="hd-small"></HeaderStyle>
                  
  </asp:TemplateField>
  <asp:TemplateField HeaderText="THU" HeaderStyle-CssClass="hd-small" 
               >
   <ItemTemplate>
    <asp:Label ID="txtTHU" runat="server" Width="30px"   ></asp:Label>
     
   </ItemTemplate>
     <ItemStyle HorizontalAlign="Center" /> 
  <FooterStyle HorizontalAlign="Center" /> 
            <FooterTemplate> 
            <div>
            <asp:Label ID="lblThu" runat="server" CssClass="label" Width="40px" style="text-align:right;"
                    ></asp:Label>
            </div>
   </FooterTemplate> 

<HeaderStyle CssClass="hd-small"></HeaderStyle>

  </asp:TemplateField>
  <asp:TemplateField HeaderText="FRI" HeaderStyle-CssClass="hd-small" 
                >
   <ItemTemplate>
    <asp:Label ID="txtFRI" runat="server" Width="30px"  
             ></asp:Label>
      
   </ItemTemplate>
     <ItemStyle HorizontalAlign="Center" /> 
  <FooterStyle HorizontalAlign="Center" /> 
            <FooterTemplate> 
            <div>
            <asp:Label ID="lblFri" runat="server" CssClass="label" Width="40px" style="text-align:right;"
                     ></asp:Label>
            </div>
   </FooterTemplate> 

<HeaderStyle CssClass="hd-small"></HeaderStyle>

  </asp:TemplateField>
        <asp:TemplateField HeaderText="SAT" HeaderStyle-CssClass="hd-small" 
                > 
            <ItemTemplate>
    <asp:Label ID="txtSAT" runat="server"  Width="30px" 
                      ></asp:Label>
      
   </ItemTemplate>
     <ItemStyle HorizontalAlign="Center" /> 
       <FooterStyle HorizontalAlign="Center" /> 
            <FooterTemplate> 
            <div>
            <asp:Label ID="lblSAt" runat="server" CssClass="label" Width="40px" 
                     ></asp:Label>
            </div>
   </FooterTemplate>      

<HeaderStyle CssClass="hd-small"></HeaderStyle>
     </asp:TemplateField> 


        </Columns> 
</asp:gridview>
                     <br />
           <asp:Label ID="lblRWCommnets" runat="server" Text="Remarks" 
                  CssClass="label" ></asp:Label> 
               <asp:Label ID="lblValidateRWCommnets" runat="server" Text="" 
                  CssClass="label" ></asp:Label>  
        <br />
      </asp:Panel>
    <asp:Panel ID="pnlLeaveEncashment" runat="server" >
            <asp:Label ID="lblLeaveType" runat="server" Text="Leave Type"  CssClass="label"></asp:Label>
            <asp:Label ID="lblValidateLeaveType" runat="server" CssClass="label" ></asp:Label>
            <asp:Label ID="lblValidateNewLeaveType" runat="server" CssClass="label" ></asp:Label><br /> 
            <asp:Label ID="lblDaysOrHours" runat="server" Text="Days / Hours"  CssClass="label" ></asp:Label>
            <asp:Label ID="lblValidateDaysOrHours" runat="server" CssClass="label" ></asp:Label>
            <asp:Label ID="lblValidateNewDaysOrHours" runat="server" CssClass="label" ></asp:Label><br />
            <asp:Label ID="lblRequiredDate" runat="server" Text="Required by"  CssClass="label" ></asp:Label>
            <asp:Label ID="lblValidateRequiredDate" runat="server" CssClass="label" ></asp:Label>
            <asp:Label ID="lblValidateNewRequiredDate" runat="server" CssClass="label" ></asp:Label><br />
            <asp:Label ID="lblLeReview" runat="server" Text="Remarks" CssClass="label" ></asp:Label> 
            <asp:Label ID="lblValidateLEComments" runat="server" Text="" CssClass="label" ></asp:Label> 
     <br />
    </asp:Panel>
    <asp:Panel ID="pnlTravelRequest" runat="server" >
            <asp:Label ID="lblTripNo" runat="server" Text="Trip request No."  CssClass="label" ></asp:Label>
            <asp:Label ID="lblValidateTripNo" runat="server" CssClass="label" ></asp:Label><br /> 
            <asp:Label ID="lblTripStartDate" runat="server" Text="Trip start date"  CssClass="label" ></asp:Label>
            <asp:Label ID="lblValidateTripStartDate" runat="server" CssClass="label"></asp:Label><br />
            <asp:Label ID="lblTripEndDate" runat="server" Text="Trip end date" CssClass="label" ></asp:Label> 
            <asp:Label ID="lblValidateTripEndDate" runat="server" CssClass="label" ></asp:Label><br />
            <asp:Label ID="lblTripAdvance" runat="server" Text="Advance amount" CssClass="label" ></asp:Label> 
            <asp:Label ID="lblValidateAdvance" runat="server" CssClass="label" ></asp:Label><br />
            <asp:Label ID="lblTripRequiredByDate" runat="server" Text="Required by date" CssClass="label" ></asp:Label> 
            <asp:Label ID="lblValidateRequiredByDate" runat="server" CssClass="label" ></asp:Label><br /> 
            <asp:Label ID="lblTripEstimation" runat="server" Text="Estimated trip cost" CssClass="label" ></asp:Label> 
            <asp:Label ID="lblValidateEstimation" runat="server" CssClass="label" ></asp:Label><br /> 
            <asp:Label ID="lblTripComments" runat="server" Text="Name of the project" CssClass="label" ></asp:Label> 
            <asp:Label ID="lblValidateComments" runat="server" CssClass="label" ></asp:Label><br />
            <asp:Label ID="lblTrReview" runat="server" Text="Remarks" CssClass="label" ></asp:Label> 
            <asp:Label ID="lblValidateTrComments" runat="server" Text="" CssClass="label" ></asp:Label>  
     <br />
    </asp:Panel>



    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
            </div>
    </asp:Content>
