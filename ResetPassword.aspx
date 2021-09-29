<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="KPMAMS.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reset Password</title>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/app.css" rel="stylesheet" />
</head>
    <body style="">
            <form id="form1" runat="server" style="position:absolute; top:0; bottom:0">
   <!-- START wrapper-->
   <div style="height: 100%; padding: 50px 0; background-color: #2c3037" class="row row-table">
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
               <strong class="text-muted">PASSWORD RESET</strong>
            </p>
            <div class="panel-body">
               <form role="form">
                  <p class="text-center">Enter your user ID and your email to proceed to reset password.</p>
                   <div class="form-group has-feedback">
                     <asp:Label ID="lblUserID" runat="server" Text="User ID"></asp:Label>
                     <asp:TextBox runat="server" ID="txtUserID" placeholder="User ID" CssClass="form-control" ></asp:TextBox>
                     <span class="fa fa-user form-control-feedback text-muted"></span>
                  </div>
                  <div class="form-group has-feedback">
                     <asp:Label ID="lblEmail" runat="server" Text="Email Address"></asp:Label>
                     <asp:TextBox runat="server" ID="txtEmail" placeholder="Email" CssClass="form-control" ></asp:TextBox>
                     <span class="fa fa-envelope form-control-feedback text-muted"></span>
                  </div>
                   <asp:Button ID="btnReset" runat="server" type="submit" CssClass="btn btn-danger btn-block" Text="Reset" OnClick="btnReset_Click"/>
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
   <script src="../plugins/animo/animo.min.js"></script>
   <!-- Custom script for pages-->
   <script src="../app/js/pages.js"></script>
   <!-- END Scripts-->--%>


</body>
</html>
