<%@ Page Title="Login" Language="C#" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="iEmpPower.Account.LoginNew" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="content-type" content="text/html; charset=windows-1252"/>
    <title>Login</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <meta content="admin" name="description"/>
    <meta content="Coderthemes" name="author"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <link rel="shortcut icon" href="../assets/images/favicon.ico"/>
    <link rel="stylesheet" href="../assets/css/styles.css"/>

    <!-- App css -->
    <link href="../assets/css/bootstrap.css" rel="stylesheet" type="text/css"/>
    <!-- App css -->

    <script type="text/javascript">
        if (!isIE() || edge > 0) {
            localStorage.openpages = Date.now();
            var onLocalStorageEvent = function (e) {
                if (e.key == "openpages") {
                    // Listen if anybody else opening the same page!
                    localStorage.page_available = Date.now();
                }
                if (e.key == "page_available") {
                    alert("One more page already open");
                    window.location.href = "/Error.html";
                }
            };
        }
        window.addEventListener('storage', onLocalStorageEvent, false);


        function isIE() {
            ua = navigator.userAgent;
            /* MSIE used to detect old browsers and Trident used to newer ones*/
            var is_ie = ua.indexOf("MSIE ") > -1 || ua.indexOf("Trident/") > -1;


            return is_ie;
        }


        var ua = window.navigator.userAgent;
        var edge = ua.indexOf('Edge/');
    </script>
</head>


<body class="authentication-bg authentication-bg-pattern">
    <div class="account-pages mt-5 mb-5">
        <div class="container" style="margin-top: 8%;">
            <div class="row justify-content-center">
                <div class="col-md-8 col-lg-6 col-xl-5">
                    <form id="form1" runat="server">
                        <div class="card bg-pattern">

                            <div class="card-body p-4">

                                <div class="text-center w-75 m-auto">
                                    <a href="index.html">
                                        <span><a href="Login.aspx" class="logo text-center">
                                            <img src="../assets/images/Itchamps_iemp.png" alt="" style="width: 125px; height: 60px;"/></a></span>
                                    </a>
                                    <p class="text-muted mb-4 mt-3">Enter your user id and password to login.</p>
                                </div>

                                <div class="form-group mb-3">

                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <asp:Login ID="Login1" runat="server" Width="100%" RememberMeSet="True">
                                        <LayoutTemplate>
                                            <div class="form-group mb-3">
                                                <label for="emailaddress">User ID</label>
                                                <asp:TextBox ID="UserName" runat="server" CssClass="form-control" placeholder="Enter your user id" Style="border-bottom: solid 2px #00cecb;" MaxLength="20"></asp:TextBox>                                               
                                                <asp:RegularExpressionValidator ID="REV_UserName" runat="server" Display="Dynamic"
                                                    CssClass="VdtrCls" ControlToValidate="UserName" ValidationExpression="^[a-z0-9]*$"
                                                    ValidationGroup="Login1" ForeColor="Red">Only lowercase letters and numbers are allowed</asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" Display="Dynamic" ControlToValidate="UserName"
                                                    ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1"
                                                    ForeColor="Red">*</asp:RequiredFieldValidator>

                                            </div>
                                            <div class="form-group mb-3">
                                                <label for="password">Password</label>
                                                <asp:TextBox ID="Password" class="form-control" runat="server" TextMode="Password" placeholder="Enter your password" Style="border-bottom: solid 2px #00cecb;"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" Display="Dynamic" ControlToValidate="Password"
                                                    ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1"
                                                    ForeColor="Red">*</asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group mb-3">
                                                <div class="custom-control custom-checkbox">
                                                    <input type="checkbox" class="custom-control-input" id="checkbox-signin">
                                                    <label class="custom-control-label" for="checkbox-signin"><small>Remember me</small></label>
                                                </div>
                                            </div>

                                            <div class="form-group mb-0 text-center">
                                                <asp:Button ID="LoginButton" runat="server" CssClass="btn btn-primary btn-block p-2" Text="Login"
                                                    ValidationGroup="Login1" CommandName="Login" OnClick="LoginButton_Click" />
                                            </div>

                                            <div class="form-group">
                                                <div style="text-align: left; margin-top: 5px">
                                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False" ></asp:Literal>
                                                    <asp:Label ID="LblMsg" runat="server" EnableViewState="False"></asp:Label>
                                                </div>
                                            </div>


                                        </LayoutTemplate>
                                    </asp:Login>
                                    <div class="form-group">
                                        <div class="col-sm-11">
                                            <p>
                                            </p>
                                            <!-- //copyright -->
                                        </div>
                                        <div class="col-sm-1" style="display: none">
                                            <i class=" pull-right">
                                                <asp:ImageButton ID="imgbtnAndriod" runat="server" Width="40px" Height="40px" ImageUrl="~/images/andriod_download.gif" OnClick="imgbtnAndriod_Click" /></i>
                                        </div>

                                    </div>



                                </div>

                            </div>
                            <!-- end card-body -->

                        </div>
                        <!-- end card -->

                        <div class="row mt-3">
                            <div class="col-12 text-center">
                                <p>
                                    <asp:LinkButton ID="LbtnResetPw" runat="server" CssClass="text-white-50 ml-1"
                                        Text="Forgot your Password?" OnClick="LbtnResetPw_Click"></asp:LinkButton>
                                </p>

                            </div>
                            <!-- end col -->
                        </div>
                        <!-- end row -->
                    </form>
                </div>
                <!-- end col -->
            </div>
            <!-- end row -->
        </div>
        <!-- end container -->
    </div>
    <!-- end page -->


    <footer class="footer footer-alt">
        <a href="https://itchamps.com/" class="text-white-50">© 2010 - <% = DateTime.Now.Year.ToString() %>
           <%-- <asp:Literal ID="ltrtoyear" runat="server"></asp:Literal>--%>
            ITChamps Software Pvt. Ltd. All rights reserved.
        </a>
    </footer>
   
</body>
</html>
