<%@ Page Title="" Language="C#" MasterPageFile="~/user.Master" AutoEventWireup="true" CodeBehind="CreateMeeting.aspx.cs" Inherits="KPMAMS.CreateMeeting" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.15.1/moment.min.js"></script>
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.43/css/bootstrap-datetimepicker.min.css"> 
    <%--<link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.43/css/bootstrap-datetimepicker-standalone.css">--%> 
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.43/js/bootstrap-datetimepicker.min.js"></script>


        <script type="text/javascript">

            $(function () {
                $('[id*=txtStart]').datetimepicker({
                    format: 'DD/MM/YYYY hh:mm A',
                    icons: {
                        time: "fa fa-clock-o",
                        date: "fa fa-calendar",
                        up: "fa fa-chevron-up",
                        down: "fa fa-chevron-down",
                        previous: 'fa fa-chevron-left',
                        next: 'fa fa-chevron-right',
                        today: 'fa fa-screenshot',
                        clear: 'fa fa-trash',
                        close: 'fa fa-remove'
                    },
                    minDate: moment()
                });
            });
        </script>



    <div class="panel panel-default">
        <div class="panel-heading">Create Meeting</div>
               <div class="panel-body">
                  <form method="get" action="/" class="form-horizontal">
                     <fieldset>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Class Meeting Title</label>
                           <div class="col-sm-5">
                               <asp:TextBox ID="txtTopic" placeholder="Enter Class Meeting Title" runat="server" CssClass="form-control form-control-rounded"></asp:TextBox>
                           </div>
                           <label class="col-sm-1 control-label">Meeting Duration</label>
                           <div class="col-sm-5">
                               <asp:TextBox ID="txtDuration" runat="server" CssClass="form-control form-control-rounded"></asp:TextBox>
                           </div>
                        </div>
                     </fieldset>
                      <br />
                      <fieldset>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Meeting Start Time & Date</label>
                           <div class="col-sm-5">
                               <asp:TextBox ID="txtStart" runat="server" CssClass="form-control form-control-rounded"></asp:TextBox>
                           </div>
                           <label class="col-sm-1 control-label">Class</label>
                           <div class="col-sm-5">
                               <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control m-b"></asp:DropDownList>
                        </div>
                     </fieldset>
                      <br />
                      
                      
                      <fieldset>
                    
                        <div class="form-group">
                            <div style="float:right">
                                        <asp:Button ID="btnCreate" CssClass="btn btn-primary" runat="server" Text="Create" OnClick="btnCreate_Click" />
                            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" Text="Cancel" OnClick="btnBack_Click"/>
                            </div>
                           

                        </div>
                     </fieldset>

                     
                  </form>
               </div>

        <div class="col-lg-10" >
            <asp:Label ID="Host" runat="server" Text="Link" Visible="false"></asp:Label>
            <asp:Label ID="Join" runat="server" Text="Link" Visible="false"></asp:Label>
            <asp:Label ID="Code" runat="server" Text="Code" Visible="false"></asp:Label>



         </div>


	</div>
</asp:Content>
