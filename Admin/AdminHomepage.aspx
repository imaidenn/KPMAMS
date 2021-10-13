<%@ Page Language="C#" MasterPageFile="~/admin.Master" AutoEventWireup="true" CodeBehind="AdminHomepage.aspx.cs" Inherits="KPMAMS.AdminHomepage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <link href='https://cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.8.2/fullcalendar.min.css' rel='stylesheet' />
<link href='https://cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.8.2/fullcalendar.print.css' media='print' />
      <style>
    #calendar {
      max-width: 900px;
      margin: 0 auto;
    }
</style>

<section class="main-content">

               <!-- START dashboard main content-->
               <div class="col-md-9">
                  <!-- START summary widgets-->
                  <!-- START Secondary Widgets-->
                  <div class="row">
                     <div class="col-md-4">
                        <!-- START widget-->
                        <div data-toggle="play-animation" data-play="fadeInLeft" data-offset="0" data-delay="1400" class="panel widget anim-running anim-done" style="">
                           <div class="panel-body">

                              <div class="text-right text-muted">
                                
                                 <em class="fa fa-users fa-2x"></em>
                              </div>
                              <p class="text-muted">Total new students added this month</p>
                               <div>
                                   <h3><asp:Label ID="lblTotalStudents" runat="server"></asp:Label></h3>
                               </div>
                           </div>
                        </div>
                        <!-- END widget-->
                     </div>
                     <div class="col-md-4">
                        <!-- START widget-->
                        <div data-toggle="play-animation" data-play="fadeInLeft" data-offset="0" data-delay="1400" class="panel widget anim-running anim-done" style="">
                           <div class="panel-body">
                              <div class="text-right text-muted">
                                 <em class="fa fa-users fa-2x"></em>
                              </div>
                              <p class="text-muted">Total new teachers added this month</p>
                               <div>
                                   <h3>
                                       <asp:Label ID="lblTotalTeachers" runat="server"></asp:Label>
                                   </h3>
                                   
                               </div>
                           </div>
                        </div>
                        <!-- END widget-->
                     </div>
                      <div class="col-md-4">
                        <!-- START widget-->
                        <div data-toggle="play-animation" data-play="fadeInLeft" data-offset="0" data-delay="1400" class="panel widget anim-running anim-done" style="">
                           <div class="panel-body">
                              <div class="text-right text-muted">
                                 <em class="fa fa-users fa-2x"></em>
                              </div>
                              <p class="text-muted">Total new parents added this month</p>
                               <div>
                                   <h3>
                                       <asp:Label ID="lblTotalParents" runat="server"></asp:Label>
                                   </h3>
                                   
                               </div>
                           </div>
                        </div>
                        <!-- END widget-->
                     </div>
                  </div>
                  <!-- END Secondary Widgets-->
                  <!-- END summary widgets-->
                  <!-- START chart-->
                  <div class="row">
                     <div class="col-lg-12">
                        <div class="panel panel-default">
                           <div class="panel-collapse">
                               <div class="panel-heading">
                         <asp:Label ID="lblTotalQty" runat="server"></asp:Label>
                                               <asp:button type="button" id="btnAdd" runat="server" class="btn btn-labeled btn-primary pull-right" OnClick="btnAdd_Click" Text="Add Announcement"></asp:button>
                     </div>

                              <div class="panel-body">
                                 <asp:GridView ID="GridView1" runat="server" AllowSorting="True" CssClass="table table-condensed table-hover" Enabled="False" Width="100%" AutoGenerateColumns="False" DataKeyNames="AnnouncementGUID" OnRowDataBound="GridView1_RowDataBound" > 
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlView" Text="View" runat="server" ></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="AnnouncementGUID" HeaderText="AnnouncementGUID" ReadOnly="True" SortExpression="AnnouncementGUID" Visible="false"/>
                                    <asp:BoundField DataField="AnnouncementTitle" HeaderText="Title" SortExpression="AnnouncementTitle" />
                                    <asp:BoundField DataField="CreateDate" HeaderText="Date" SortExpression="CreateDate" />
                                </Columns>
                            </asp:GridView>
                            <asp:Label id="lblNoData" runat="server" Visible="false" Text="No data to Display"></asp:Label>
                              </div>
                           </div>
                        </div>
                     </div>
                  </div>

                   <div class="row">
                     <div class="col-lg-12">
                        <div class="panel panel-default">
                           <div class="panel-collapse">
                              <div class="panel-body">
                                  <div class="card bg-default">
    <div class="card-body">
        <div class="chart">
            <!-- Chart wrapper -->
            <canvas id="chart-sales" class="chart-canvas"></canvas>
        </div>
    </div>
</div>
                              </div>
                           </div>
                        </div>
                     </div>
                  </div>
                  <!-- END chart-->
                  

               </div>
               <!-- END dashboard main content-->
    <div class="col-md-3">
                  <!-- START messages-->
                  <div class="panel panel-default">
                     <div class="panel-heading">
                        <div class="pull-right label label-info"></div>
                        <div class="panel-title">Calendar</div>
                     </div>
                     <div id='calendar'></div>

  <script src='https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.20.1/moment.min.js'></script>
  <script src='https://cdnjs.cloudflare.com/ajax/libs/jquery/3.3.1/jquery.js'></script>
  <script src='https://cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.8.2/fullcalendar.min.js'></script>
  <script type="text/javascript">
      $(document).ready(function () {

          $('#calendar').fullCalendar({
              header: {
                  right: 'today', //positions the the prev/next button on the right 
                  center: 'title', //sets the title of the month to center
                  left: 'prev,next ' //positions the the prev/next button on the left
              },

              navLinks: true, // click on day/week names to navigate views
              editable: true,
              eventLimit: true // add "more" link when there are too many events in a day

          });

      });
  </script>

 
                  </div>
                  <!-- END messages-->
                
               </div>
    
                  

         </section>


    </asp:Content>

