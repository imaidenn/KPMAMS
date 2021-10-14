<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="KPMAMS.WebForm1" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <!-- Meta-->
   <meta charset="utf-8">
   <meta http-equiv="X-UA-Compatible" content="IE=edge">
   <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0">
   <meta name="description" content="">
   <meta name="keywords" content="">
   <meta name="author" content="">
   <title>Admin Master</title> 
   <!-- Bootstrap CSS-->
   <link rel="stylesheet" href="css/bootstrap.css">
   <!-- plugins CSS-->
   <link rel="stylesheet" href="plugins/fontawesome/css/font-awesome.min.css">
   <link rel="stylesheet" href="plugins/animo/animate+animo.css">
   <link rel="stylesheet" href="plugins/csspinner/csspinner.min.css"> 
   <!-- App CSS-->
   <link rel="stylesheet" href="css/app.css">
</head>
<body style="" runat="server">
   <!-- START Main wrapper-->
   <section class="wrapper">
      <!-- START Top Navbar-->
      <nav role="navigation" class="navbar navbar-default navbar-top navbar-fixed-top">
         <!-- START navbar header-->
         <div class="navbar-header">
            <a href="javascript:void(0);" class="navbar-brand">
               <div class="brand-logo">KPM Academy</div>
               <div class="brand-logo-collapsed">KPM</div>
            </a>
         </div>
         <!-- END navbar header-->
         <!-- START Nav wrapper-->
         <div class="nav-wrapper">
            <!-- START Left navbar-->
            <ul class="nav navbar-nav">
               <li>
                  <a href="javascript:void(0);" data-toggle="aside">
                     <em class="fa fa-align-left"></em>
                  </a>
               </li>
               <li>
                  <a href="javascript:void(0);" data-toggle="navbar-search">
                     <em class="fa fa-search"></em>
                  </a>
               </li>
            </ul>
            <!-- END Left navbar-->
            <!-- START Right Navbar-->
            <ul class="nav navbar-nav navbar-right">
               <!-- START Messages menu (dropdown-list)-->
               <li class="dropdown dropdown-list">
                  <a href="javascript:void(0);" data-toggle="dropdown" data-play="bounceIn" class="dropdown-toggle">
                     <em class="fa fa-envelope"></em>
                     <div class="label label-danger">300</div>
                  </a>
                  <!-- START Dropdown menu-->
                  <ul class="dropdown-menu">
                     <li class="dropdown-menu-header">You have 5 new messages</li>
                     <li>
                        <div class="scroll-viewport">
                           <!-- START list group-->
                           <div class="slimScrollDiv" style="position: relative; overflow: hidden; width: auto; height: 250px;"><div class="list-group scroll-content" style="overflow: hidden; width: auto; height: 250px;">
                              <!-- START list group item-->
                              <a href="javascript:void(0);" class="list-group-item">
                                 <div class="media">
                                    <div class="pull-left">
                                       <img style="width: 48px; height: 48px;" src="app/img/user/01.jpg" alt="Image" class="media-object img-rounded">
                                    </div>
                                    <div class="media-body clearfix">
                                       <small class="pull-right">2h</small>
                                       <strong class="media-heading text-primary">
                                          <div class="point point-success point-lg"></div>Mery</strong>
                                       <p class="mb-sm">
                                          <small>Cras sit amet nibh libero, in gravida nulla. Nulla...</small>
                                       </p>
                                    </div>
                                 </div>
                              </a>
                              <!-- END list group item-->
                              <!-- START list group item-->
                              <a href="javascript:void(0);" class="list-group-item">
                                 <div class="media">
                                    <div class="pull-left">
                                       <img style="width: 48px; height: 48px;" src="app/img/user/04.jpg" alt="Image" class="media-object img-rounded">
                                    </div>
                                    <div class="media-body clearfix">
                                       <small class="pull-right">3h</small>
                                       <strong class="media-heading text-primary">
                                          <div class="point point-success point-lg"></div>Michael</strong>
                                       <p class="mb-sm">
                                          <small>Cras sit amet nibh libero, in gravida nulla. Nulla...</small>
                                       </p>
                                    </div>
                                 </div>
                              </a>
                              <!-- END list group item-->
                              <!-- START list group item-->
                              <a href="javascript:void(0);" class="list-group-item">
                                 <div class="media">
                                    <div class="pull-left">
                                       <img style="width: 48px; height: 48px;" src="app/img/user/03.jpg" alt="Image" class="media-object img-rounded">
                                    </div>
                                    <div class="media-body clearfix">
                                       <small class="pull-right">4h</small>
                                       <strong class="media-heading text-primary">
                                          <div class="point point-danger point-lg"></div>Richa</strong>
                                       <p class="mb-sm">
                                          <small>Cras sit amet nibh libero, in gravida nulla. Nulla...</small>
                                       </p>
                                    </div>
                                 </div>
                              </a>
                              <!-- END list group item-->
                              <!-- START list group item-->
                              <a href="javascript:void(0);" class="list-group-item">
                                 <div class="media">
                                    <div class="pull-left">
                                       <img style="width: 48px; height: 48px;" src="app/img/user/05.jpg" alt="Image" class="media-object img-rounded">
                                    </div>
                                    <div class="media-body clearfix">
                                       <small class="pull-right">4h</small>
                                       <strong class="media-heading text-primary">
                                          <div class="point point-danger point-lg"></div>Robert</strong>
                                       <p class="mb-sm">
                                          <small>Cras sit amet nibh libero, in gravida nulla. Nulla...</small>
                                       </p>
                                    </div>
                                 </div>
                              </a>
                              <!-- END list group item-->
                              <!-- START list group item-->
                              <a href="javascript:void(0);" class="list-group-item">
                                 <div class="media">
                                    <div class="pull-left">
                                       <img style="width: 48px; height: 48px;" src="app/img/user/06.jpg" alt="Image" class="media-object img-rounded">
                                    </div>
                                    <div class="media-body clearfix">
                                       <small class="pull-right">4h</small>
                                       <strong class="media-heading text-primary">
                                          <div class="point point-danger point-lg"></div>Priya</strong>
                                       <p class="mb-sm">
                                          <small>Cras sit amet nibh libero, in gravida nulla. Nulla...</small>
                                       </p>
                                    </div>
                                 </div>
                              </a>
                              <!-- END list group item-->
                           </div><div class="slimScrollBar" style="background: rgb(0, 0, 0); width: 7px; position: absolute; top: 0px; opacity: 0.4; border-radius: 7px; z-index: 99; right: 1px; display: block;"></div><div class="slimScrollRail" style="width: 7px; height: 100%; position: absolute; top: 0px; display: none; border-radius: 7px; background: rgb(51, 51, 51); opacity: 0.2; z-index: 90; right: 1px;"></div></div>
                           <!-- END list group-->
                        </div>
                     </li>
                     <!-- START dropdown footer-->
                     <li class="p">
                        <a href="javascript:void(0);" class="text-center">
                           <small class="text-primary">READ ALL</small>
                        </a>
                     </li>
                     <!-- END dropdown footer-->
                  </ul>
                  <!-- END Dropdown menu-->
               </li>
               <!-- END Messages menu (dropdown-list)-->
               <!-- START Alert menu-->
               <li class="dropdown dropdown-list">
                  <a href="javascript:void(0);" data-toggle="dropdown" data-play="bounceIn" class="dropdown-toggle">
                     <em class="fa fa-bell"></em>
                     <div class="label label-info">120</div>
                  </a>
                  <!-- START Dropdown menu-->
                  <ul class="dropdown-menu">
                     <li>
                        <!-- START list group-->
                        <div class="list-group">
                           <!-- list item-->
                           <a href="javascript:void(0);" class="list-group-item">
                              <div class="media">
                                 <div class="pull-left">
                                    <em class="fa fa-envelope-o fa-2x text-success"></em>
                                 </div>
                                 <div class="media-body clearfix">
                                    <div class="media-heading">Unread messages</div>
                                    <p class="m0">
                                       <small>You have 10 unread messages</small>
                                    </p>
                                 </div>
                              </div>
                           </a>
                           <!-- list item-->
                           <a href="javascript:void(0);" class="list-group-item">
                              <div class="media">
                                 <div class="pull-left">
                                    <em class="fa fa-cog fa-2x"></em>
                                 </div>
                                 <div class="media-body clearfix">
                                    <div class="media-heading">New settings</div>
                                    <p class="m0">
                                       <small>There are new settings available</small>
                                    </p>
                                 </div>
                              </div>
                           </a>
  
                     </li>
                  </ul>
                  <!-- END Dropdown menu-->
               </li>
               <!-- END Alert menu-->
               <!-- START User menu-->
               <li class="dropdown">
                  <a href="javascript:void(0);" data-toggle="dropdown" data-play="bounceIn" class="dropdown-toggle">
                     <em class="fa fa-user"></em>
                  </a>
                  <!-- START Dropdown menu-->
                  <ul class="dropdown-menu">
                     <li>
                        <div class="p">
                           <p>Overall progress</p>
                           <div class="progress progress-striped progress-xs m0">
                              <div role="progressbar" aria-valuenow="80" aria-valuemin="0" aria-valuemax="100" style="width: 80%;" class="progress-bar progress-bar-success">
                                 <span class="sr-only">80% Complete</span>
                              </div>
                           </div>
                        </div>
                     </li>
                     <li class="divider"></li>
                     <li><a href="javascript:void(0);">Profile</a>
                     </li>
                     <li><a href="javascript:void(0);">Settings</a>
                     </li>
                     <li><a href="javascript:void(0);">Notifications<div class="label label-info pull-right">5</div></a>
                     </li>
                     <li><a href="javascript:void(0);">Messages<div class="label label-danger pull-right">10</div></a>
                     </li>
                     <li><a href="javascript:void(0);">Logout</a>
                     </li>
                  </ul>
                  <!-- END Dropdown menu-->
               </li>
               <!-- END User menu-->

            </ul>
            <!-- END Right Navbar-->
         </div>
         <!-- END Nav wrapper-->
         <!-- START Search form-->
         <form role="search" class="navbar-form">
            <div class="form-group has-feedback">
               <input type="text" placeholder="Type and hit Enter.." class="form-control">
               <div data-toggle="navbar-search-dismiss" class="fa fa-times form-control-feedback"></div>
            </div>
            <button type="submit" class="hidden btn btn-default">Submit</button>
         </form>
         <!-- END Search form-->
      </nav>
      <!-- END Top Navbar-->
      <!-- START aside-->
      <aside class="aside">
         <!-- START Sidebar (left)-->
         <nav class="sidebar">
            <ul class="nav">
               <!-- START user info-->
               <li>

                  <!-- START User links collapse-->
                  <ul class="nav collapse">
                     <li><a href="javascript:void(0);">Profile</a>
                     </li>
                     <li><a href="javascript:void(0);">Settings</a>
                     </li>
                     <li><a href="javascript:void(0);">Notifications<div class="label label-danger pull-right">120</div></a>
                     </li>
                     <li><a href="javascript:void(0);">Messages<div class="label label-success pull-right">300</div></a>
                     </li>
                     <li class="divider"></li>
                     <li><a href="javascript:void(0);">Logout</a>
                     </li>
                  </ul>
                  <!-- END User links collapse-->
               </li>
               <!-- END user info-->
               <!-- START Menu-->
               <li class="active">
                  <a href="index.html" title="Dashboard" data-toggle="collapse-next" class="has-submenu">
                     <em class="fa fa-dashboard"></em>
                     <div class="label label-primary pull-right">12</div>
                     <span class="item-text">Dashboard</span>
                  </a>
                  <!-- START SubMenu item-->
                  <ul class="nav collapse in">
                     <li class="active">
                        <a href="index.html" title="Default" data-toggle="" class="no-submenu">
                           <span class="item-text">Default</span>
                        </a>
                     </li>
                     <li>
                        <a href="dashboard-nosidebar.html" title="No Sidebar" data-toggle="" class="no-submenu"> 
                           <span class="item-text">No Sidebar</span>
                        </a>
                     </li>
                     <li>
                        <a href="dashboard-noprofile.html" title="No Profile" data-toggle="" class="no-submenu"> 
                           <span class="item-text">No Profile</span>
                        </a>
                     </li>
                  </ul>
                  <!-- END SubMenu item-->
               </li>




               <!-- END Menu-->
               <!-- Sidebar footer    -->
               <li class="nav-footer">
                  <div class="nav-footer-divider"></div>
                  <!-- START button group-->
                  <div class="btn-group text-center">
                     <button type="button" data-toggle="tooltip" data-title="Logout" class="btn btn-link" data-original-title="" title="">
                        <em class="fa fa-sign-out text-muted"></em>
                     </button>
                  </div>
                  <!-- END button group-->
               </li>
            </ul>
         </nav>
         <!-- END Sidebar (left)-->
      </aside>
      <!-- End aside-->

      <!-- START Main section-->
      <section>
         <!-- START Page content-->
         <section class="main-content">

         </section>
         <!-- END Page content-->
      </section>
      <!-- END Main section-->
   </section>
   <!-- END Main wrapper-->
   <!-- START Scripts-->
   <!-- Main plugins Scripts-->
   <script src="plugins/jquery/jquery.min.js"></script>
   <script src="plugins/bootstrap/js/bootstrap.min.js"></script>


   <!-- App Main-->
   <script src="app/js/app.js"></script>
   <!-- END Scripts-->


<div class="uk-notify uk-notify-top-right" style="display: none;"></div></body>
</html>
