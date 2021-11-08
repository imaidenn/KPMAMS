<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="TeacherClassroom.aspx.cs" Inherits="KPMAMS.Admin.TeacherClassroom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
        <style type="text/css">
     .hidden
     {
         display:none;
     }
</style>
     <div class="panel panel-default">
               <div class="panel-heading">Teacher Classroom</div>
               <div class="panel-body">
                  <form method="get" action="/" class="form-horizontal">
                     <fieldset>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Classroom</label>
                           <div class="col-sm-5">
                               <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control m-b" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                           </div>
                       

                           <label class="col-sm-1 control-label">Teacher Name</label>
                           <div class="col-sm-5">
                               <asp:DropDownList ID="ddlName" runat="server" CssClass="form-control m-b"></asp:DropDownList>
                           </div>
                             </div>
                     </fieldset>
                      <br />
                      <br />
                      <fieldset>
                        <div class="form-group">

                           <label class="col-sm-1 control-label">Teach Subject</label>
                           <div class="col-sm-5">
                               <asp:DropDownList ID="ddlSubject" CssClass="form-control m-b" runat="server"></asp:DropDownList>
                           </div>
                        </div>
                     </fieldset>
                      <br />
                      <br />
             <fieldset>
                        <div class="form-group">
                            <div style="float:right">
                            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" CssClass="btn btn-primary"/>&nbsp
                            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn btn-primary"/>
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
                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-condensed table-responsive table-hover" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="TeacherGUID" OnRowDeleting="GridView1_RowDeleting" OnRowCommand="GridView1_RowCommand" > 
                                <Columns>
                                    <asp:BoundField DataField="ClassroomGUID" HeaderText="ClassroomGUID" ReadOnly="True" SortExpression="ClassroomGUID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                    <asp:BoundField DataField="TeacherGUID" HeaderText="TeacherGUID" ReadOnly="True" SortExpression="TeacherGUID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                    <asp:BoundField DataField="SubjectGUID" HeaderText="SubjectGUID" ReadOnly="True" SortExpression="SubjectGUID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                    <asp:BoundField DataField="TeacherName" HeaderText="Teacher Name" SortExpression="TeacherName" />
                                    <asp:BoundField DataField="SubjectName" HeaderText="Subject Teach" SortExpression="SubjectName" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnDelete" runat="server" class="btn btn-link" Text="Delete" CommandName="DeleteTeacher" CommandArgument="<%# Container.DataItemIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
