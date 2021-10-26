<%@ Page Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="StudentListing.aspx.cs" Inherits="KPMAMS.Admin.StudentListing" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <!-- Bootstrap -->
<script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
<script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js'></script>
<link rel="stylesheet" href='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css'
    media="screen" />
<!-- Bootstrap -->
<!-- Bootstrap DatePicker -->
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/css/bootstrap-datepicker.css" type="text/css"/>
<script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/js/bootstrap-datepicker.js" type="text/javascript"></script>
<!-- Bootstrap DatePicker -->
    <script type="text/javascript">
        $(function () {
            $('[id*=Calendar1]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd/mm/yyyy",
                language: "tr"
            });
        });
    </script>
<script type="text/javascript">
    $(function () {
        $('[id*=Calendar2]').datepicker({
            changeMonth: true,
            changeYear: true,
            format: "dd/mm/yyyy",
            language: "tr"
        });
    });
</script>
    <br />
    <div class="panel panel-default">
               <div class="panel-heading">Student</div>
               <div class="panel-body">
                  <form method="get" action="/" class="form-horizontal">
                     <fieldset>
<%--                        <legend>Classic inputs</legend>--%>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Student ID</label>
                           <div class="col-sm-5">
                               <asp:TextBox ID="txtStudentID" runat="server" CssClass="form-control form-control-rounded" ></asp:TextBox>
                           </div>
                       

                           <label class="col-sm-1 control-label">Student Name</label>
                           <div class="col-sm-5">
                               <asp:TextBox ID="txtStudentName"  runat="server" CssClass="form-control form-control-rounded"></asp:TextBox>
                           </div>
                             </div>
                     </fieldset>
                      <br />
                      <br />
                      <fieldset>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Class</label>
                           <div class="col-sm-5">
                               <asp:TextBox ID="txtClass"  runat="server" CssClass="form-control form-control-rounded"></asp:TextBox>
                           </div>

                           <label class="col-sm-1 control-label">Status</label>
                           <div class="col-sm-5">
                               <asp:DropDownList ID="ddlStatus" CssClass="form-control m-b" runat="server">
                                   <asp:ListItem>All</asp:ListItem>
                                   <asp:ListItem>Active</asp:ListItem>
                                   <asp:ListItem>Inactive</asp:ListItem>
                               </asp:DropDownList>
                           </div>
                        </div>
                     </fieldset>
                      <br />
                      <br />
                      <fieldset>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Date From</label>
                           <div class="col-sm-5">
                               <div class="datepicker input-group date mb-lg">
                                   <asp:TextBox ID="Calendar1" CssClass="form-control" runat="server"></asp:TextBox>
                                 <span class="input-group-addon">
                                    <span class="fa-calendar fa"></span>
                                 </span>
                              </div>
                           </div>

                           <label class="col-sm-1 control-label">Date To</label>
                           <div class="col-sm-5">
                               <div class="datepicker input-group date mb-lg">
                                   <asp:TextBox ID="Calendar2" CssClass="form-control" runat="server"></asp:TextBox>
                                 <span class="input-group-addon">
                                    <span class="fa-calendar fa"></span>
                                 </span>
                              </div>
                           </div>
                        </div>
                     </fieldset>
                      <br />
                      <br />
             <fieldset>
                        <div class="form-group">
                            <div style="float:right">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-1"/>&nbsp
                                        <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="btn btn-1"/>&nbsp
                                        <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CssClass="btn btn-1" />
                            </div>
                           

                        </div>
                     </fieldset>
                      <br />

                      <div class="col-lg-12">
                  <div class="panel panel-default">
                     <div class="panel-heading">
                         <asp:Label ID="lblTotalQty" runat="server"></asp:Label>
                     </div>
                     <div class="panel-body">
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" CssClass="table table-condensed table-hover" Enabled="False" Width="100%" AutoGenerateColumns="False" DataKeyNames="StudentGUID" OnRowDataBound="GridView1_RowDataBound" > 
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlView" Text="View" runat="server" ></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="StudentGUID" HeaderText="StudentGUID" ReadOnly="True" SortExpression="StudentGUID" Visible="false"/>
                                    <asp:BoundField DataField="StudentUserID" HeaderText="Student User ID" SortExpression="StudentUserID" />
                                    <asp:BoundField DataField="FullName" HeaderText="Student Name" SortExpression="FullName" />
                                    <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                                    <asp:BoundField DataField="Class" HeaderText="Class" SortExpression="Class" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                    <asp:BoundField DataField="JoinDate" HeaderText="Join Date" SortExpression="JoinDate" />
                                </Columns>
                            </asp:GridView>
                            <asp:Label id="lblNoData" runat="server" Visible="false" Text="No data to Display"></asp:Label>
                     </div>
                  </div>
               </div>

                      
                     
                  </form>
               </div>
            </div>
      


    </asp:Content>



