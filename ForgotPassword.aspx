<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="KPMAMS.ForgotPassword" %>

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
               <strong>Create new password.</strong>
            </p>
            <div class="panel-body">
               <form role="form" class="mb-lg">
                  <div class="form-group has-feedback">
                      <asp:TextBox runat="server" ID="txtNewPassword" placeholder="New Password" CssClass="form-control" TextMode="Password"></asp:TextBox>
<%--                     <span class="fa fa-user form-control-feedback text-muted"></span>--%>
                  </div>
                  <div class="form-group has-feedback">
                      <asp:TextBox runat="server" ID="txtConfirmPassword" placeholder="Confirm Password" CssClass="form-control" TextMode="Password" ></asp:TextBox>
<%--                     <span class="fa fa-lock form-control-feedback text-muted"></span>--%>
                  </div>
                   <asp:Button ID="btnSubmit" type="submit" CssClass="btn btn-block btn-primary" Text="Submit" OnClick="btnSubmit_Click" runat="server"/>
               </form>
            </div>
         </div>

         <!-- END panel-->
      </div>
   </div>
     </form>



</body>
