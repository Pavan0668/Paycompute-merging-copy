<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.Master" AutoEventWireup="true" CodeBehind="Master_upload_from_excel.aspx.cs" Inherits="iEmpPower.UI.SPaycompute.Master_upload_from_excel" 
     MaintainScrollPositionOnPostback="true" Theme="SkinFile" Culture="en-GB"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>
<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls" TagPrefix="BDP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       <style>
 .HrCls
        {
            width: 100%;
            border: 0;
            height: 1px;
            background: #333;
            background-image: linear-gradient(to right, #333, #333, #ccc);
            padding: 0;
            margin: 3px 0;
        }
        .ccode {
            display:none;
        }
 
    </style>
    
    <%-- <script>
         function myFunction() {
             document.getElementById("MainContent_divForm").reset();
         }
    </script>

    <script>
        function myFunction() {
            document.getElementById("MainContent_divForm").reset();
        }
    </script>

    <script type ="text/javascript">

        var validFilesTypes = ["pdf", "jpeg", "jpg", "png"];

        function ValidateFile() {

            var file = document.getElementById("<%=file_docupload.ClientID%>");

            var label = document.getElementById("<%=lbldocupmssg.ClientID%>");

            var path = file.value;

            var ext = path.substring(path.lastIndexOf(".") + 1, path.length).toLowerCase();

            var isValidFile = false;

            for (var i = 0; i < validFilesTypes.length; i++) {

                if (ext == validFilesTypes[i]) {

                    isValidFile = true;

                    break;

                }

            }

            if (!isValidFile) {

                label.style.color = "red";

                label.innerHTML = "Invalid File. Please upload a file with pdf and image formats format";

            }

            return isValidFile;

        }

</script>--%>
    
     <div class="card-box">
            <div id="real_time_chart" class="row">
                <div style="width: 99%; margin: 0 auto; padding: 5px 0 40px 0;">
                     <div class="col-sm-12"  style="width:100%">
                    <div  style="width:100%">
                        <ul class="nav nav-pills navtab-bg" >
                            <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Tab1" class="nav-link p-2" OnClick="Tab1_Click" CausesValidation="false"><i class="fe-arrow-up-circle" ></i>
                            PR Upload</asp:LinkButton></li>

                            <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Tab2" class="nav-link p-2" OnClick="Tab2_Click" CausesValidation="false"><i class="fe-arrow-up-circle" ></i>
                            Iexpense Upload</asp:LinkButton></li>
                            

                            
                            <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Tab3" class="nav-link p-2" OnClick="Tab3_Click" CausesValidation="false"><i class="fe-arrow-up-circle" ></i>
                            FBP Upload</asp:LinkButton></li>

                             <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Tab5" class="nav-link p-2" OnClick="Tab5_Click" CausesValidation="false"><i class="fe-arrow-up-circle" ></i>
                     IT Upload</asp:LinkButton></li>

                             <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Tab4" class="nav-link p-2" OnClick="Tab4_Click" CausesValidation="false"><i class="fe-arrow-up-circle" ></i>
                         Common Master Upload</asp:LinkButton></li>
                            </ul>
                         <div class="tabcontents">
                                 <div id="view1" runat="server" visible="false"  style="width:100%">
                            <br />
                          <div class="header-title">&nbsp;&nbsp;Add PR</div>
                                 <hr class="HrCls"/>
                            <br />
                                <div class="row">
                                    <div class="md-2">&nbsp;&nbsp;&nbsp;&nbsp; Upload PR Details</div>
                                    <div class="col-md-3">
                                        <asp:FileUpload ID="uflPRData" runat="server" AllowMultiple="false" />
                                    </div>

                                    <div class="col-md-4">
                                        <asp:Button ID="btnUploadPRData" runat="server" Text="Check" OnClick="btnUploadPRData_Click" />
                                        <asp:Button ID="btnSave" runat="server" Visible="false" Text="Upload" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnClear" runat="server" Visible="false" Text="Cancel" OnClick="btnClear_Click"/>
                                    </div>
                                    <div class="col-md-3" style="text-align: right">
                                 <asp:UpdatePanel ID="b" runat="server">
                                 <ContentTemplate>
                                        <%--<asp:LinkButton ID="lnkTemplDwnld" runat="server" Text="Download Template" OnClick="lnkTemplDwnld_Click"></asp:LinkButton>--%>
                                      </ContentTemplate>
                                      <Triggers>
                                          <asp:PostBackTrigger ControlID="btnUploadPRData" />
                                           <asp:PostBackTrigger ControlID="btnSave" />
                                         <%--<asp:PostBackTrigger ControlID="lnkTemplDwnld" />--%>
                                       </Triggers>
                                  </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12" id="divgrds" runat="server" visible="false">
                                        <h4>MIS Group C</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_ZMM_MIS" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                      <asp:BoundField HeaderText="CID" DataField="CID" />
                                                    <asp:BoundField HeaderText="C_DESC" DataField="C_DESC" />
                                                    <asp:BoundField HeaderText="A_DESC" DataField="A_DESC" />
                                                    <asp:BoundField HeaderText="B_DESC" DataField="B_DESC" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <h4>Reqtr Region</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="gv_Reqtr_Region" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="EKGRP" DataField="EKGRP" />
                                                    <asp:BoundField HeaderText="EKNAM" DataField="EKNAM" />
                                                      </Columns>
                                            </asp:GridView>
                                        </div>
                                        <h4>Bill To Address </h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_Bill_to_address" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="WERKS" DataField="WERKS" />
                                                    <asp:BoundField HeaderText="NAME1" DataField="NAME1" />
                                                   
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                      <%--  <h4>ERP Code</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_ERP_Code" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="POSID" DataField="POSID" />
                                                    <asp:BoundField HeaderText="POST1" DataField="POST1" />
                                                    <asp:BoundField HeaderText="PSPHI" DataField="PSPHI" />
                                                    <asp:BoundField HeaderText="VERNR" DataField="VERNR" />
                                                    <asp:BoundField HeaderText="VERNA" DataField="VERNA" />
                                                    <asp:BoundField HeaderText="PBUKR" DataField="PBUKR" />
                                                    <asp:BoundField HeaderText="POSKI" DataField="POSKI" />
                                                    <asp:BoundField HeaderText="ZZDEL_HEAD" DataField="ZZDEL_HEAD" />
                                                    <asp:BoundField HeaderText="ZZDEL_HEADNAME" DataField="ZZDEL_HEADNAME" />
                                                    <asp:BoundField HeaderText="ZZPERNR01" DataField="ZZPERNR01" />
                                                    <asp:BoundField HeaderText="ZZENAME01" DataField="ZZENAME01" />
                                                    <asp:BoundField HeaderText="ZZROLE01" DataField="ZZROLE01" />
                                                    <asp:BoundField HeaderText="ZZPERNR02" DataField="ZZPERNR02" />
                                                    <asp:BoundField HeaderText="ZZENAME02" DataField="ZZENAME02" />
                                                    <asp:BoundField HeaderText="ZZROLE02" DataField="ZZROLE02" />
                                                    <asp:BoundField HeaderText="ZZPERNR03" DataField="ZZPERNR03" />
                                                    <asp:BoundField HeaderText="ZZENAME03" DataField="ZZENAME03" />
                                                    <asp:BoundField HeaderText="ZZROLE03" DataField="ZZROLE03" />
                                                    <asp:BoundField HeaderText="ZZPERNR04" DataField="ZZPERNR04" />
                                                    <asp:BoundField HeaderText="ZZENAME04" DataField="ZZENAME04" />
                                                    <asp:BoundField HeaderText="ZZROLE04" DataField="ZZROLE04" />
                                                    <asp:BoundField HeaderText="ZZPERNR05" DataField="ZZPERNR05" />
                                                    <asp:BoundField HeaderText="ZZENAME05" DataField="ZZENAME05" />
                                                    <asp:BoundField HeaderText="ZZROLE05" DataField="ZZROLE05" />
                                                    <asp:BoundField HeaderText="ZZPERNR06" DataField="ZZPERNR06" />
                                                    <asp:BoundField HeaderText="ZZENAME06" DataField="ZZENAME06" />
                                                     <asp:BoundField HeaderText="ZZROLE06" DataField="ZZROLE06" />
                                                    <asp:BoundField HeaderText="ZZPERNR07" DataField="ZZPERNR07" />
                                                    <asp:BoundField HeaderText="ZZENAME07" DataField="ZZENAME07" />
                                                    <asp:BoundField HeaderText="ZZROLE07" DataField="ZZROLE07" />
                                                    <asp:BoundField HeaderText="STAT" DataField="STAT" />
                                                    <asp:BoundField HeaderText="Created_By" DataField="Created_By" />
                                                    <asp:BoundField HeaderText="Created_on" DataField="Created_on" />
                                                    <asp:BoundField HeaderText="Company_Code" DataField="Company_Code" />
                                                    <asp:BoundField HeaderText="Start_Date" DataField="Start_Date" />
                                                    <asp:BoundField HeaderText="End_Date" DataField="End_Date" />
                                                    <asp:BoundField HeaderText="Updated_On" DataField="Updated_On" />
                                                    <asp:BoundField HeaderText="Updated_By" DataField="Updated_By" />
                                                    <asp:BoundField HeaderText="WBS_EXTNID" DataField="WBS_EXTNID" />
                                                    <asp:BoundField HeaderText="PSPNR" DataField="PSPNR" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>--%>
                                        <h4>Business Unit</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_Business_Unit" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="SPART" DataField="SPART" />
                                                    <asp:BoundField HeaderText="VTEXT" DataField="VTEXT" />
                                                 
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <h4>Region</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_Region" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="REGION_ID" DataField="REGION_ID" />
                                                    <asp:BoundField HeaderText="REGION_TEXT" DataField="REGION_TEXT" />
                                                  
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <h4>UOM</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_UOM" runat="server" AutoGenerateColumns="false" CssClass="Grid respovrflw" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="ISOTXT" DataField="ISOTXT" />
                                                   
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                        </div>

                              <div id="view2" runat="server" visible="false"  style="width:100%">
                            <br />
                          <div class="header-title">&nbsp;&nbsp;Add Iexpense</div>
                                 <hr class="HrCls"/>
                            <br />
                                <div class="row">
                                    <div class="md-2">&nbsp;&nbsp;&nbsp;&nbsp; Upload Iexpense Details</div>
                                    <div class="col-md-3">
                                        <asp:FileUpload ID="FileUploadiexpense" runat="server" AllowMultiple="false" />
                                    </div>

                                    <div class="col-md-4">
                                        <asp:Button ID="btnUploadiexpenseData" runat="server" Text="Check" OnClick="btnUploadiexpenseData_Click" />
                                        <asp:Button ID="btnSaveIexpense" runat="server" Visible="false" Text="Upload" OnClick="btnSaveIexpense_Click" />
                                        <asp:Button ID="btnCleaIexpense" runat="server" Visible="false" Text="Cancel" OnClick="btnCleaIexpense_Click"/>
                                    </div>
                                    <div class="col-md-3" style="text-align: right">
                                 <asp:UpdatePanel ID="UpdaateIexpense" runat="server">
                                 <ContentTemplate>
                                        <%--<asp:LinkButton ID="lnkTemplDwnld" runat="server" Text="Download Template" OnClick="lnkTemplDwnld_Click"></asp:LinkButton>--%>
                                      </ContentTemplate>
                                      <Triggers>
                                          <asp:PostBackTrigger ControlID="btnUploadiexpenseData" />
                                           <asp:PostBackTrigger ControlID="btnSaveIexpense" />
                                         <%--<asp:PostBackTrigger ControlID="lnkTemplDwnld" />--%>
                                       </Triggers>
                                  </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12" id="divgrdsiexpense" runat="server" visible="false">
                                        <h4>Expense Type</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="grdiexpense" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                      <asp:BoundField HeaderText="LGART" DataField="LGART" />
                                                    <asp:BoundField HeaderText="LGTXT" DataField="LGTXT" />
                                                   
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                       
                                        
                                    </div>
                                </div>
                        </div>

                                 <div id="view3" runat="server" visible="false"  style="width:100%">
                            <br />
                          <div class="header-title">&nbsp;&nbsp;Add FBP</div>
                                 <hr class="HrCls"/>
                            <br />
                                <div class="row">
                                    <div class="md-2">&nbsp;&nbsp;&nbsp;&nbsp; Upload FBP Details</div>
                                    <div class="col-md-3">
                                        <asp:FileUpload ID="upldfileFBP" runat="server" AllowMultiple="false" />
                                    </div>

                                    <div class="col-md-4">
                                        <asp:Button ID="btnUploadFBPData" runat="server" Text="Check" OnClick="btnUploadFBPData_Click" />
                                        <asp:Button ID="btnSaveFBP" runat="server" Visible="false" Text="Upload" OnClick="btnSaveFBP_Click"/>
                                        <asp:Button ID="btnClearFBP" runat="server" Visible="false" Text="Cancel" OnClick="btnClearFBP_Click"/>
                                    </div>
                                    <div class="col-md-3" style="text-align: right">
                                 <asp:UpdatePanel ID="UpdatePanelFBP" runat="server">
                                 <ContentTemplate>
                                        <%--<asp:LinkButton ID="lnkTemplDwnld" runat="server" Text="Download Template" OnClick="lnkTemplDwnld_Click"></asp:LinkButton>--%>
                                      </ContentTemplate>
                                      <Triggers>
                                          <asp:PostBackTrigger ControlID="btnUploadFBPData" />
                                           <asp:PostBackTrigger ControlID="btnSaveFBP" />
                                         <%--<asp:PostBackTrigger ControlID="lnkTemplDwnld" />--%>
                                       </Triggers>
                                  </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12" id="divgrdsFBP" runat="server" visible="false">
                                        <h4>Head of allowence</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_HOA" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                      <asp:BoundField HeaderText="LGART" DataField="LGART" />
                                                    <asp:BoundField HeaderText="LTEXT" DataField="LTEXT" />
                                                    <asp:BoundField HeaderText="Colid" DataField="Colid" />
                                                  
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <h4>Annual Entitlement</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_Annual_Entitlement" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="PERNR" DataField="PERNR" />
                                                    <asp:BoundField HeaderText="AN_FBP" DataField="AN_FBP" />
                                                      </Columns>
                                            </asp:GridView>
                                        </div>
                                        <h4>master_T7INA9 </h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_master_T7INA9" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="ALGRP" DataField="ALGRP" />
                                                    <asp:BoundField HeaderText="LGART" DataField="LGART" />
                                                     <asp:BoundField HeaderText="AMUNT" DataField="AMUNT" />
                                                   
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                      
                                        <h4>master_T7INA3</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_master_T7INA3" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="TRFAR" DataField="TRFAR" />
                                                    <asp:BoundField HeaderText="TRFGB" DataField="TRFGB" />
                                                     <asp:BoundField HeaderText="TRFGR" DataField="TRFGR" />
                                                    <asp:BoundField HeaderText="TRFST" DataField="TRFST" />
                                                     <asp:BoundField HeaderText="ALGRP" DataField="ALGRP" />
                                              
                                                 
                                                    
                                                 
                                                 
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <h4>BasketTotal</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_BasketTotal" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="PERNR" DataField="PERNR" />
                                                    <asp:BoundField HeaderText="TRFAR" DataField="TRFAR" />
                                                    <asp:BoundField HeaderText="TRFGB" DataField="TRFGB" />
                                                    <asp:BoundField HeaderText="TRFGR" DataField="TRFGR" />
                                                    <asp:BoundField HeaderText="TRFST" DataField="TRFST" />
                                                    <asp:BoundField HeaderText="BEGDA" DataField="BEGDA" />
                                                    <asp:BoundField HeaderText="ENDDA" DataField="ENDDA" />
                                                   
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                     
                                    </div>
                                </div>
                        </div>

                               <div id="view4" runat="server" visible="false"  style="width:100%">
                            <br />
                          <div class="header-title">&nbsp;&nbsp;Add Common Master</div>
                                 <hr class="HrCls"/>
                            <br />
                                <div class="row">
                                    <div class="md-2">&nbsp;&nbsp;&nbsp;&nbsp; Upload Common Master Details</div>
                                    <div class="col-md-3">
                                        <asp:FileUpload ID="upldfilecm" runat="server" AllowMultiple="false" />
                                    </div>

                                    <div class="col-md-4">
                                        <asp:Button ID="btnUploadCMData" runat="server" Text="Check" OnClick="btnUploadCMData_Click" />
                                        <asp:Button ID="btnSaveCM" runat="server" Visible="false" Text="Upload" OnClick="btnSaveCM_Click" />
                                        <asp:Button ID="btnClearCM" runat="server" Visible="false" Text="Cancel" OnClick="btnClearCM_Click"/>
                                    </div>
                                    <div class="col-md-3" style="text-align: right">
                                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                               
                                      <Triggers>
                                          <asp:PostBackTrigger ControlID="btnUploadCMData" />
                                           <asp:PostBackTrigger ControlID="btnSaveCM" />
                                         <%--<asp:PostBackTrigger ControlID="lnkTemplDwnld" />--%>
                                       </Triggers>
                                  </asp:UpdatePanel>
                                    </div>
                                
                                <div class="form-group">
                                    <div class="col-md-12" id="divgrdsCM" runat="server" visible="false">
                                        <h4>Master TCURR</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_TCURR" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                      <asp:BoundField HeaderText="FCURR" DataField="FCURR" />
                                                    <asp:BoundField HeaderText="TCURR" DataField="TCURR" />
                                                    <asp:BoundField HeaderText="UKURS" DataField="UKURS" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <h4>master.TCURT</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_TCURT" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="WAERS" DataField="WAERS" />
                                                    <asp:BoundField HeaderText="LTEXT" DataField="LTEXT" />
                                                      </Columns>
                                            </asp:GridView>
                                        </div>

                                          <h4>Country</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_Country" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                      <asp:BoundField HeaderText="LAND1" DataField="LAND1" />
                                                    <asp:BoundField HeaderText="LANDX" DataField="LANDX" />
                                                    <asp:BoundField HeaderText="NATIO" DataField="NATIO" />
                                                  
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <h4>Region</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_trvlRegion" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="LAND1" DataField="LAND1" />
                                                    <asp:BoundField HeaderText="RGION" DataField="RGION" />
                                                    <asp:BoundField HeaderText="TEXT25" DataField="TEXT25" />
                                                      </Columns>
                                            </asp:GridView>
                                        </div>
                                     
                                      <h4>ERP Code</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_PRPS" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="POSID" DataField="POSID" />
                                                    <asp:BoundField HeaderText="POST1" DataField="POST1" />
                                                    <asp:BoundField HeaderText="PSPHI" DataField="PSPHI" />
                                                    <asp:BoundField HeaderText="VERNR" DataField="VERNR" />
                                                    <asp:BoundField HeaderText="VERNA" DataField="VERNA" />
                                                    <asp:BoundField HeaderText="PBUKR" DataField="PBUKR" />
                                                    <asp:BoundField HeaderText="POSKI" DataField="POSKI" />
                                                   <asp:BoundField HeaderText="PSPNR" DataField="PSPNR" />
                                                    <asp:BoundField HeaderText="Created_By" DataField="Created_By" />
                                                    <asp:BoundField HeaderText="Created_on" DataField="Created_on" />
                                                    <asp:BoundField HeaderText="Company_Code" DataField="Company_Code" />
                                                    <asp:BoundField HeaderText="Start_Date" DataField="Start_Date" />
                                                    <asp:BoundField HeaderText="End_Date" DataField="End_Date" />
                                                    <asp:BoundField HeaderText="Updated_On" DataField="Updated_On" />
                                                    <asp:BoundField HeaderText="Updated_By" DataField="Updated_By" />
                                                    <asp:BoundField HeaderText="WBS_EXTNID" DataField="WBS_EXTNID" />
                                                   
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                       
                                   
                                      
                                    </div>
                                </div>
                                </div>
                                    </div>

                                <div id="view5" runat="server" visible="false"  style="width:100%">
                            <br />
                          <div class="header-title">&nbsp;&nbsp;Add IT</div>
                                 <hr class="HrCls"/>
                            <br />
                                <div class="row">
                                    <div class="md-2">&nbsp;&nbsp;&nbsp;&nbsp; Upload IT Details</div>
                                    <div class="col-md-3">
                                        <asp:FileUpload ID="upldfileIT" runat="server"  />
                                    </div>

                                    <div class="col-md-4">
                                        <asp:Button ID="btnUploadIT" runat="server" Text="Check" OnClick="btnUploadIT_Click"/>
                                        <asp:Button ID="btnSaveIT" runat="server" Visible="false" Text="Upload" OnClick="btnSaveIT_Click"/>
                                        <asp:Button ID="btnClearIT" runat="server" Visible="false" Text="Cancel" OnClick="btnClearIT_Click"/>
                                    </div>
                                    <div class="col-md-3" style="text-align: right">
                                 <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                 <ContentTemplate>
                                        <%--<asp:LinkButton ID="lnkTemplDwnld" runat="server" Text="Download Template" OnClick="lnkTemplDwnld_Click"></asp:LinkButton>--%>
                                      </ContentTemplate>
                                      <Triggers>
                                          <asp:PostBackTrigger ControlID="btnUploadIT" />
                                           <asp:PostBackTrigger ControlID="btnSaveIT" />
                                         <%--<asp:PostBackTrigger ControlID="lnkTemplDwnld" />--%>
                                       </Triggers>
                                  </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12" id="divgrdsIT" runat="server" visible="false">
                                        <h4>master T7INI3 </h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_T7INI3" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                      <asp:BoundField HeaderText="ICODE" DataField="ICODE" />
                                                    <asp:BoundField HeaderText="ITEXT" DataField="ITEXT" />
                                                 
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <h4> master T7INI9 </h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_T7INI9" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="SBSEC" DataField="SBSEC" />
                                                    <asp:BoundField HeaderText="SBDIV" DataField="SBDIV" />
                                                    <asp:BoundField HeaderText="SDVLT" DataField="SDVLT" />
                                                      <asp:BoundField HeaderText="TXEXM" DataField="TXEXM" />
                                                      </Columns>
                                            </asp:GridView>
                                        </div>

                                          <h4>  master T7INI8   </h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_T7INI8" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="SBSEC" DataField="SBSEC" />
                                                    <asp:BoundField HeaderText="SBDIV" DataField="SBDIV" />
                                                    <asp:BoundField HeaderText="SBDDS" DataField="SBDDS" />
                                                     
                                                      </Columns>
                                            </asp:GridView>
                                        </div>

                                          <h4> master T7INI4 </h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_T7INI4" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="ICODE" DataField="ICODE" />
                                                    <asp:BoundField HeaderText="ENDDA" DataField="ENDDA" />
                                                    <asp:BoundField HeaderText="BEGDA" DataField="BEGDA" />
                                                      <asp:BoundField HeaderText="CTGRY" DataField="CTGRY" />
                                                     <asp:BoundField HeaderText="WAEHI" DataField="WAEHI" />
                                                     <asp:BoundField HeaderText="ITLMT" DataField="ITLMT" />
                                                      </Columns>
                                            </asp:GridView>
                                        </div>
                                      
                                          <h4> master T7INI5 </h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_T7INI5" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="SBSEC" DataField="SBSEC" />
                                                    <asp:BoundField HeaderText="SBTDS" DataField="SBTDS" />
                                                   
                                                      </Columns>
                                            </asp:GridView>
                                        </div>

                                     
                                    </div>
                                </div>
                        </div>
                        
                            

                        </div>
                         </div>
                    </div>
                </div>
     
</asp:Content>
