﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="KPMAMS.Login"  EnableEventValidation="false"%>

<!DOCTYPE html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

    <title>LoginPage</title>
    <link href="Bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/app.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" integrity="sha512-iBBXm8fW90+nuLcSKlbmrPcLa0OT92xO1BIsZ+ywDWZCvqsWgccV3gFoRBv0z+8dLJgyAHIhR35VZc2oM/gI1w==" crossorigin="anonymous" referrerpolicy="no-referrer" />

</head>

<body style="">
   <form id="LoginForm" runat="server" style="position:absolute; top:0; bottom:0" >
      <div style="position:absolute; background-color: #2c3037"><a href="Admin/AdminLogin.aspx" class="text-muted" style="background-color: #2c3037">Admin access</a></div>
   <div style=" height: 100%; padding: 50px 0; background-color: #2c3037" class="row row-table">
      <div class="col-lg-3 col-md-6 col-sm-8 col-xs-12 align-middle">
         <!-- START panel-->
         <div data-toggle="play-animation" data-play="fadeInUp" data-offset="0" class="panel panel-default panel-flat anim-running anim-done" style="">
            <p class="text-center mb-lg">
               <br>
               <a href="#">
                  <img src="img/logoKPM.png" alt="Image" class="block-center img-rounded">
               </a>
            </p>
            <p class="text-center mb-lg">
               <strong>SIGN IN TO CONTINUE.</strong>
            </p>
            <div class="panel-body">
               <form role="form" class="mb-lg">
                  <%--<div class="text-right mb-sm"><a href="Register.aspx" class="text-muted">Need to Signup?</a>
                  </div>--%>
                  <div class="form-group has-feedback">
                      <asp:TextBox runat="server" ID="txtUserID" placeholder="User ID" CssClass="form-control" AutoCompleteType="Disabled" ></asp:TextBox>
                     <span class="fa fa-user form-control-feedback text-muted"></span>
                  </div>
                  <div class="form-group has-feedback">
                      <asp:TextBox runat="server" ID="txtPassword" placeholder="Password" CssClass="form-control" TextMode="Password" ></asp:TextBox>
                     <span class="fa fa-lock form-control-feedback text-muted"></span>
                  </div>
                  <div class="clearfix">
                     <div class="checkbox c-checkbox pull-left mt0">
                        <label>
                           <input id="cbShow" type="checkbox" value="" onchange="document.getElementById('txtPassword').type = this.checked ? 'text' : 'password'" />
                           <span class="fa fa-check"></span>Show Password</label>
                     </div>
                     <div class="pull-right"><a href="ResetPassword.aspx" class="text-muted">Forgot your password?</a>
                     </div>
                  </div>
                   <asp:Button ID="btnLogin" type="submit" CssClass="btn btn-block btn-primary" Text="Login" OnClick="btnLogin_Click" runat="server"/>
               </form>
            </div>
         </div>

         <!-- END panel-->
      </div>
   </div>
     </form>
   <!-- END wrapper-->
<%--   <!-- START Scripts-->
   <!-- Main plugins Scripts-->
   <script src="../plugins/jquery/jquery.min.js"></script>
   <script src="../plugins/bootstrap/js/bootstrap.min.js"></script>
   <!-- Animo-->
   <script src="animo/animo.min.js"></script>
   <!-- Custom script for pages-->
   <script src="../app/js/pages.js"></script>
   <!-- END Scripts-->--%>


</body>