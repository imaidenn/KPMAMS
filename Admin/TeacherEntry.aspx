<%@ Page Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="TeacherEntry.aspx.cs" Inherits="KPMAMS.Admin.TeacherEntry" EnableEventValidation="false" %>

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
            $('[id*=txtBirthDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd/mm/yyyy",
                language: "tr"
            });
        });
    </script>
<script type="text/javascript">
    $(function () {
        $('[id*=txtJoinDate]').datepicker({
            changeMonth: true,
            changeYear: true,
            format: "dd/mm/yyyy",
            language: "tr"
        });
    });
</script>

<div class="panel panel-default">
               <div class="panel-heading"><h3>Teacher Details</h3></div>
               <div class="panel-body">
                  <form method="get" action="/" class="form-horizontal">
                     <fieldset>
<%--                        <legend>Classic inputs</legend>--%>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Teacher ID</label>
                           <div class="col-sm-5">
                               <asp:TextBox ID="txtTeacherID" runat="server" CssClass="form-control form-control-rounded" Enabled="False"></asp:TextBox>
                           </div>
                           <label class="col-sm-1 control-label">Full Name</label>
                           <div class="col-sm-5">
                               <asp:TextBox ID="txtName" placeholder="Enter Teacher Name" runat="server" CssClass="form-control form-control-rounded"></asp:TextBox>
                           </div>
                        </div>
                     </fieldset>
                      <br />
                      <fieldset>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">IC Number</label>
                           <div class="col-sm-5">
                               <asp:TextBox ID="txtICno" placeholder="Enter Teacher IC number" runat="server" CssClass="form-control form-control-rounded"></asp:TextBox>
                           </div>

                           <label class="col-sm-1 control-label">Gender</label>
                           <div class="col-sm-5">
                               <asp:DropDownList ID="ddlGender" CssClass="form-control m-b" runat="server">
                                   <asp:ListItem>Male</asp:ListItem>
                                   <asp:ListItem>Female</asp:ListItem>
                               </asp:DropDownList>
                           </div>
                        </div>
                     </fieldset>
                      <br />
                      
                      
                      
                      <fieldset>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Email</label>
                           <div class="col-sm-5">
                               <asp:TextBox ID="txtEmail" placeholder="Enter Teacher Email" runat="server" CssClass="form-control form-control-rounded"></asp:TextBox>
                           </div>

                           <label class="col-sm-1 control-label">Phone Number</label>
                           <div class="col-sm-5">
                               <asp:TextBox ID="txtPhoneNo" placeholder="Enter Teacher Phone Number" runat="server" CssClass="form-control form-control-rounded"></asp:TextBox>
                           </div>
                        </div>
                     </fieldset>
                      <br />
                      <fieldset>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Address</label>
                           <div class="col-sm-11">
                               <asp:TextBox ID="txtAddress" placeholder="Enter Teacher Address" runat="server" CssClass="form-control form-control-rounded"></asp:TextBox>
                           </div>
                        </div>
                     </fieldset>
                      <br />
                      <fieldset>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Birth Date</label>
                            <div class="col-sm-5">
                               <div class="datetimepicker input-group date mb-lg">
                                   <asp:TextBox ID="txtBirthDate" CssClass="form-control" runat="server"></asp:TextBox>
                                 <span class="input-group-addon">
                                    <span class="fa-calendar fa"></span>
                                 </span>
                              </div>
                           </div>

                           <label class="col-sm-1 control-label">Join Date</label>
                           <div class="col-sm-5">
                               <div class="datetimepicker input-group date mb-lg">
                                   <asp:TextBox ID="txtJoinDate" CssClass="form-control" runat="server"></asp:TextBox>
                                 <span class="input-group-addon">
                                    <span class="fa-calendar fa"></span>
                                 </span>
                              </div>
                           </div>
                        </div>
                     </fieldset>
                      <br />
                      
                      <fieldset class="last-child">
                        <div class="form-group">

                           <label class="col-sm-1 control-label">Status</label>
                           <div class="col-sm-5">
                               <asp:DropDownList ID="ddlStatus" CssClass="form-control m-b" runat="server">
                                   <asp:ListItem>Active</asp:ListItem>
                                   <asp:ListItem>Inactive</asp:ListItem>
                               </asp:DropDownList>
                           </div>
                        </div>
                     </fieldset>
                      <br />
                      <fieldset>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Profile Picture</label>
                           <div class="col-sm-5">
                               <asp:FileUpload ID="imageUpload" runat="server" onchange="loadFile(event)" Enabled="true" />
                                <div id="hiddenImg"><asp:Image ID="imgProfile" runat="server"></asp:Image></div>
                           </div>
                        </div>
                     </fieldset>
                      <br />
             <fieldset>
                        <div class="form-group">
                            <div style="float:right">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn-success" Text="Save" OnClick="btnSave_Click" Width="100px"/>
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn-success" Visible="false" Text="Update" OnClick="btnUpdate_Click" Width="100px"/>
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn-danger" Text="Cancel" OnClick="btnCancel_Click" Width="100px"/>
                            </div>
                           

                        </div>
                     </fieldset>

                     
                  </form>
               </div>
            </div>

    </asp:Content>
