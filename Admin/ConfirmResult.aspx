<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="ConfirmResult.aspx.cs" Inherits="KPMAMS.Admin.ConfirmResult" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <style type="text/css">
     .hidden
     {
         display:none;
     }
</style>
    <div class="panel panel-default">
               <div class="panel-heading">Student Exam</div>
               <div class="panel-body">
                  <form method="get" action="/" class="form-horizontal">
                     <fieldset>
<%--                        <legend>Classic inputs</legend>--%>
                        <div class="form-group">
                            <label class="col-sm-1 control-label">Class</label>
                           <div class="col-sm-5">
                               
                            <asp:TextBox ID="txtClass" runat="server" CssClass="form-control form-control-rounded" Enabled="false"></asp:TextBox>
                           </div>
                           <label class="col-sm-1 control-label">Student Name</label>
                           <div class="col-sm-5">
                               
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control form-control-rounded" Enabled="false"></asp:TextBox>
                           </div>
                        </div>
                     </fieldset>
                      <br />
                      <fieldset>
<%--                        <legend>Classic inputs</legend>--%>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Exam Semester</label>
                           <div class="col-sm-5">
                            <asp:TextBox ID="txtSem" runat="server" CssClass="form-control form-control-rounded" Enabled="False"></asp:TextBox>
                           </div>
                        </div>
                          <div class="form-group">
                           <label class="col-sm-1 control-label">Uploaded By</label>
                           <div class="col-sm-5">
                            <asp:TextBox ID="txtCreatedBy" runat="server" CssClass="form-control form-control-rounded" Enabled="False"></asp:TextBox>
                           </div>
                        </div>
                     </fieldset>
                      <br />
                      <fieldset>
<%--                        <legend>Classic inputs</legend>--%>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Created Date</label>
                           <div class="col-sm-5">
                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control form-control-rounded" Enabled="False"></asp:TextBox>
                           </div>
                        </div>
                          <div class="form-group">
                           <label class="col-sm-1 control-label">Last Update Date</label>
                           <div class="col-sm-5">
                            <asp:TextBox ID="txtUpdate" runat="server" CssClass="form-control form-control-rounded" Enabled="False"></asp:TextBox>
                           </div>
                        </div>
                     </fieldset>
                      <br />
                      <fieldset>
                      <div class="col-lg-12">
                  <div class="panel panel-default">

                     <div class="panel-body">
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" CssClass="table table-bordered table-condensed table-responsive table-hover" Enabled="False" Width="100%" AutoGenerateColumns="False" DataKeyNames="SubjectGUID"  > 
                                <Columns>                              
                                    <asp:BoundField DataField="ExamGUID" HeaderText="ExamGUID" SortExpression="ExamGUID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="SubjectGUID" HeaderText="SubjectGUID" SortExpression="SubjectGUID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="SubjectName" HeaderText="Subject Name" SortExpression="SubjectName" />
                                       <asp:BoundField DataField="Mark" HeaderText="Mark" SortExpression="Mark" />
                                       <asp:BoundField DataField="Grade" HeaderText="Grade" SortExpression="Grade" />                   

                                </Columns>
                            </asp:GridView>
                            <asp:Label id="lblNoData" runat="server" Visible="false" Text="No data to Display"></asp:Label>
                     </div>
                  </div>
               </div>
                          </fieldset>
                      <br />
                      <fieldset>
                        <div class="form-group">
                            <div style="float:right">
                                <asp:Button ID="btnConfirm" runat="server" CssClass="btn-success" Text="Confirm" OnClick="btnConfirm_Click" Width="100px"/>
                            <asp:Button ID="btnReject" runat="server" CssClass="btn-danger" Text="Reject" OnClick="btnReject_Click" Width="100px"/>
                            </div>
                           <asp:TextBox ID="txtAvgMark" runat="server" CssClass="form-control form-control-rounded" Visible="false"></asp:TextBox>

                        </div>
                     </fieldset>
                     

                  </form>
               </div>
            </div>
</asp:Content>
