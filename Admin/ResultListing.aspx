<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="ResultListing.aspx.cs" Inherits="KPMAMS.Admin.ResultListing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="panel panel-default">
               <div class="panel-heading">Student</div>
               <div class="panel-body">
                  <form method="get" action="/" class="form-horizontal">
                     <fieldset>
<%--                        <legend>Classic inputs</legend>--%>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Student Name</label>
                           <div class="col-sm-5">
                               <asp:TextBox ID="txtStudentName" runat="server" CssClass="form-control form-control-rounded" ></asp:TextBox>
                           </div>
                       
                           <label class="col-sm-1 control-label">Class</label>
                           <div class="col-sm-5">
                               <asp:TextBox ID="txtClass"  runat="server" CssClass="form-control form-control-rounded"></asp:TextBox>
                           </div>
                             </div>
                     </fieldset>
                      <br />
                      <fieldset>
                        <div class="form-group">

                           <label class="col-sm-1 control-label">Exam Semester</label>
                           <div class="col-sm-5">
                               <asp:DropDownList ID="ddlSem" runat="server" style="width:200px;"></asp:DropDownList>
                           </div>
                            <label class="col-sm-1 control-label">Year</label>
                           <div class="col-sm-5">
                               <asp:DropDownList ID="ddlYear" runat="server" style="width:200px;"></asp:DropDownList>
                           </div>
                        </div>
                     </fieldset>

                      <br />
             <fieldset>
                        <div class="form-group">
                            <div style="float:right">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-1"/>&nbsp
                                        <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="btn btn-1"/>&nbsp
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
                                    <asp:BoundField DataField="ExamSemester" HeaderText="ExamSemester" ReadOnly="True" SortExpression="ExamSemester" Visible="false"/>
                                    <asp:BoundField DataField="StudentGUID" HeaderText="StudentGUID" ReadOnly="True" SortExpression="StudentGUID" Visible="false"/>
                                    <asp:BoundField DataField="FullName" HeaderText="Student Name" SortExpression="FullName" />
                                    <asp:BoundField DataField="Class" HeaderText="Class" SortExpression="Class" />
                                    <asp:BoundField DataField="AverageMark" HeaderText="AverageMark" SortExpression="AverageMark" />
                                    <asp:BoundField DataField="GPA" HeaderText="GPA" SortExpression="GPA" />
                                    <asp:BoundField DataField="CGPA" HeaderText="CGPA" SortExpression="CGPA" />
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
