<%@ Page Title="" Language="C#" MasterPageFile="~/user.Master" AutoEventWireup="true" CodeBehind="UploadResult.aspx.cs" Inherits="KPMAMS.UploadResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
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
                               <asp:DropDownList ID="ddlClass" CssClass="form-control m-b" runat="server">
                               </asp:DropDownList>
                           </div>
                           <label class="col-sm-1 control-label">Student Name</label>
                           <div class="col-sm-5">
                               <asp:DropDownList ID="ddlStudent" CssClass="form-control m-b" runat="server">
                               </asp:DropDownList>
                           </div>
                        </div>
                     </fieldset>
                      <br />
                      <fieldset>
<%--                        <legend>Classic inputs</legend>--%>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Exam Semester</label>
                           <div class="col-sm-5">
                               <asp:DropDownList ID="ddlSem" CssClass="form-control m-b" runat="server">
                                   <asp:ListItem>March</asp:ListItem>
                                   <asp:ListItem>Pertengahan Tahun</asp:ListItem>
                                   <asp:ListItem>August</asp:ListItem>
                                   <asp:ListItem>Akhir Tahun</asp:ListItem>
                               </asp:DropDownList>
                           </div>
                        </div>
                     </fieldset>
                      <br />
                      <fieldset>
                      <div class="col-lg-12">
                  <div class="panel panel-default">

                     <div class="panel-body">
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" CssClass="table table-bordered table-condensed table-responsive table-hover" Enabled="False" Width="100%" AutoGenerateColumns="False" DataKeyNames="SubjectGUID" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" > 
                                <Columns>                              

                                    <asp:BoundField DataField="SubjectGUID" HeaderText="SubjectGUID" SortExpression="SubjectGUID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="SubjectName" HeaderText="Subject Name" SortExpression="SubjectName" />
                                       <asp:TemplateField HeaderText ="Mark" SortExpression="Mark">
                                           <ItemTemplate>
                                               <input type="text" id="txtMark" runat="server"/>
<%--                                            <asp:TextBox ID="txtMark" runat="server" TextMode="Number" min="0" max="20" step="1" Enabled="true"></asp:TextBox>--%>
                                           </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText ="Grade" SortExpression="Grade">
                                           <ItemTemplate>
                                            <input type="text" id="txtGrade" runat="server"/>
                                           </ItemTemplate>

                                    </asp:TemplateField>
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
                                <asp:Button ID="btnSave" runat="server" CssClass="btn-success" Text="Save" OnClick="btnSave_Click" Width="100px"/>
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn-danger" Text="Cancel" OnClick="btnCancel_Click" Width="100px"/>
                            </div>
                           

                        </div>
                     </fieldset>
                     

                  </form>
               </div>
            </div>
</asp:Content>
