<%@ Page Title="" Language="C#" MasterPageFile="~/user.Master" AutoEventWireup="true" CodeBehind="StudentDetails.aspx.cs" Inherits="KPMAMS.StudentDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">

    <div class="panel panel-default">
               <div class="panel-heading">Profile Info</div>
               <div class="panel-body">
                  <form method="get" action="/" class="form-horizontal">
                                            <fieldset>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Profile Picture</label>
                           <div class="col-sm-5">
                               <div id="hiddenImg"><asp:Image ID="imgProfile" runat="server" width="200" height="200" class="img-thumbnail img-circle"></asp:Image></div>
                               <asp:FileUpload ID="imageUpload" runat="server" onchange="loadFile(event)" Enabled="true" />
                                
                           </div>
                        </div>
                     </fieldset>
                      <br />
                     <fieldset>
<%--                        <legend>Classic inputs</legend>--%>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Student ID</label>
                           <div class="col-sm-5">
                               <asp:TextBox ID="txtStudentID" runat="server" CssClass="form-control form-control-rounded" Enabled="False"></asp:TextBox>
                           </div>
                           <label class="col-sm-1 control-label">Full Name</label>
                           <div class="col-sm-5">
                               <asp:TextBox ID="txtName" runat="server" CssClass="form-control form-control-rounded" Enabled="False"></asp:TextBox>
                           </div>
                        </div>
                     </fieldset>
                      <br />
                      <fieldset>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">IC Number</label>
                           <div class="col-sm-5">
                               <asp:TextBox ID="txtICno" runat="server" CssClass="form-control form-control-rounded" Enabled="False"></asp:TextBox>
                           </div>

                           <label class="col-sm-1 control-label">Gender</label>
                           <div class="col-sm-5">
                               <asp:TextBox ID="txtGender" runat="server" CssClass="form-control form-control-rounded" Enabled="False"></asp:TextBox>
                           </div>
                        </div>
                     </fieldset>
                      <br />
                      
                      
                      
                      <fieldset>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Email</label>
                           <div class="col-sm-5">
                               <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control form-control-rounded"></asp:TextBox>
                           </div>

                           <label class="col-sm-1 control-label">Phone Number</label>
                           <div class="col-sm-5">
                               <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="form-control form-control-rounded"></asp:TextBox>
                           </div>
                        </div>
                     </fieldset>
                      <br />
                      <fieldset>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Address</label>
                           <div class="col-sm-11">
                               <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control form-control-rounded"></asp:TextBox>
                           </div>
                        </div>
                     </fieldset>
                      <br />
                      <fieldset>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Birth Date</label>
                            <div class="col-sm-5">
                                   <asp:TextBox ID="txtBirthDate" CssClass="form-control form-control-rounded" runat="server" Enabled="False"></asp:TextBox>

                           </div>

                           <label class="col-sm-1 control-label">Join Date</label>
                           <div class="col-sm-5">

                                   <asp:TextBox ID="txtJoinDate" CssClass="form-control form-control-rounded" runat="server" Enabled="False"></asp:TextBox>

                           </div>
                        </div>
                     </fieldset>
                      <br />
                      
                      <fieldset class="last-child">
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Class</label>
                           <div class="col-sm-5">
                               <asp:TextBox ID="txtClass" runat="server" CssClass="form-control form-control-rounded" Enabled="False"></asp:TextBox>
                           </div>
                        </div>
                     </fieldset>
                      <br />

             <fieldset>
                        <div class="form-group">
                            <div style="float:right">
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn-success" Text="Update" OnClick="btnUpdate_Click" Width="100px"/>
                            </div>
                          
                        </div>
                     </fieldset>    
                  </form>
               </div>
            </div>
</asp:Content>
