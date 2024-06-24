<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuestLogin.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="content-type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <title>Guest Login</title>
    <meta name="viewport" content="width=device-width,initial-scale=1,maximum-scale=1.0">
    <link rel="stylesheet" href="css/bootstrap.css">
    <link rel="stylesheet" href="css/bootstrap.css">
    <link rel="stylesheet" href="css/plugins_1.css">
    <link rel="stylesheet" href="css/main_1.css">
    <link rel="stylesheet" href="css/themes.css">
    <script kabl="76029" style="display: none !important" src="../js/ga.js" async=""
        type="text/javascript"></script>
    <script src="js/modernizr-2.js"></script>
    <style type="text/css">
        #fi #fic {
            margin-right: 100px !important;
        }

        #fi #rh {
            margin-left: -115px !important;
            width: 95px !important;
        }

        #fi .rh {
            display: none !important;
        }

        body:not(.xE) div[role='main'] .Bu:not(:first-child) {
            display: none !important;
        }
    </style>
    <style type="text/css">
        .jqstooltip {
            width: auto !important;
            height: auto !important;
            position: absolute;
            left: 0px;
            top: 0px;
            visibility: hidden;
            background: #000000;
            color: white;
            font-size: 11px;
            text-align: left;
            white-space: nowrap;
            padding: 5px;
            z-index: 10000;
        }

        .jqsfield {
            color: white;
            font: 10px arial, san serif;
            text-align: left;
        }

        .block {
            margin-bottom: 13px;
        }

        .form-control {
            margin-bottom: 2px;
        }

        .input-lg {
            height: auto;
        }

        .col-xs-3 {
            width: 30%;
            padding: 0px;
            text-align: right;
        }

            .col-xs-3 .btn {
                width: 90%;
                font-size: 12px;
                background-color: #65B7E0;
                border-color: #5BA6CC;
                color: #FFF;
            }

                .col-xs-3 .btn:hover {
                    background-color: #5192B3;
                    border: 1px solid #5192B3;
                    color: #fff;
                }

        @media screen and (min-width:1024px) {
            .col-xs-12 {
            }
        }

        @media screen and (min-width:768px) {
            .form-control {
                padding: 6px 8px !important;
                font-size: 13px !important;
            }
        }
    </style>
</head>
<body style="background: #F2F2F2;">
    <form id="form1" runat="server">

        <div style="margin-top: 100px;">
        </div>
        <div class="wrapper">
            <div class="login-pannel" style="">
                <div class="inner_logo">
                    <img src="images/logo.png" width="320px" height="" />
                </div>
                <div class="col-xs-6 login-inner" style="">
                    <div class="label_signin">
                        <span>Guest Login</span>
                        <asp:Label ID="lblmsg" runat="server" ForeColor="Red" CssClass="Error_lbl"></asp:Label>
                    </div>

                    <div class="block col-xs-12" style="padding: 10px 0 0 0px; border: none; background-color: #F2F2F2;">
                        <div class="form-group">
                            <div class="col-xs-10">
                                <asp:TextBox ID="txtEmail" placeholder="Email" class="form-control input-lg"
                                    runat="server">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator2" runat="server"
                                    ControlToValidate="txtEmail" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div style="clear: both; height: 20px;">
                        </div>
                        <div class="form-group">
                            <div class="col-xs-10">
                                <div class="">
                                    <asp:TextBox ID="txtPassword" placeholder="Password" class="form-control input-lg"
                                        TextMode="Password" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="txtPassword" ForeColor="Red">*</asp:RequiredFieldValidator>
                                </div>
                                <div style="clear: both; height: 20px;">
                                </div>
                                <div class="col-xs-3" style="float: right;">
                                    <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary2" Text="Login"
                                        OnClick="btnLogin_Click" />
                                </div>
                            </div>
                        </div>
                        <div style="clear: both; height: 6px;">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div style="clear: both; height: 0px;">
        </div>
        <div class="wrapper">
            <div style="border-top: 1px solid #E8E8E8;">
                <div class="login-menu">
                    Other Login &nbsp;
                    <asp:Button ID="btnAdminLogin" runat="server" CssClass="btn btn-primary2" Text="Admin" CausesValidation="false"
                        OnClick="btnAdminLogin_Click" />
                     &nbsp;
                    <asp:Button ID="btnHospital" runat="server" CssClass="btn btn-primary2" Text="Hospital" CausesValidation="false"
                        OnClick="btnHospital_Click" />
                     &nbsp;
                    <asp:Button ID="btnPatient" runat="server" CssClass="btn btn-primary2" Text="Patient" CausesValidation="false"
                        OnClick="btnPatient_Click" />
                </div>
            </div>
        </div>
        <script src="js/jquery.js"></script>
        <script>        !window.jQuery && document.write(unescape('%3Cscript src="js/vendor/jquery-1.11.0.min.js"%3E%3C/script%3E'));</script>
        <script src="js/bootstrap.js"></script>
        <script src="js/plugins_1.js"></script>
        <script src="js/app_1.js"></script>
        <script src="js/login_1.js"></script>
        <script>        $(function () { Login.init(); });</script>
        <script>        var _gaq = _gaq || []; _gaq.push(["_setAccount", "UA-16158021-6"]), _gaq.push(["_setDomainName", "pixelcave.com"]), _gaq.push(["_trackPageview"]), function () { var t = document.createElement("script"); t.type = "text/javascript", t.async = !0, t.src = ("https:" == document.location.protocol ? "https://ssl" : "http://www") + ".google-analytics.com/ga.js"; var e = document.getElementsByTagName("script")[0]; e.parentNode.insertBefore(t, e) }();</script>
    </form>
</body>
</html>
