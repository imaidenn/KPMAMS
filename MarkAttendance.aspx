<%@ Page Title="" Language="C#" MasterPageFile="~/user.Master" AutoEventWireup="true" CodeBehind="MarkAttendance.aspx.cs" Inherits="KPMAMS.MarkAttendance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/css/bootstrap-datepicker.css" type="text/css"/>
<script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/js/bootstrap-datepicker.js" type="text/javascript"></script>
 <script type="text/javascript">
     $(function () {
         $('[id*=txtDate]').datepicker({
             changeMonth: true,
             changeYear: true,
             format: "dd-MM-yyyy",
             language: "tr"
         });
     });
 </script>
        <style type="text/css">
     .hidden
     {
         display:none;
     }
</style>

    <div class="panel panel-default">
               <div class="panel-heading">Student</div>
               <div class="panel-body">
                      <fieldset>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Meeting Date</label>
                           <div class="col-sm-5">     

           <asp:TextBox ID="txtDate" runat="server" ></asp:TextBox>
                               <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />


                           </div>
                            <label class="col-sm-1 control-label">Meeting Session</label>
                           <div class="col-sm-5">
                               <asp:DropDownList ID="ddlMeeting" runat="server" CssClass="form-control m-b" style="width:200px;" OnSelectedIndexChanged="ddlMeeting_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
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
                         <asp:Label ID="lblInfo" runat="server" Text="Tick the Checkbox to mark Students Attendance" Visible="false"></asp:Label>
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" CssClass="table table-condensed table-hover" Enabled="False" Width="100%" AutoGenerateColumns="False" DataKeyNames="AttendanceGUID" > 
                                <Columns>

                                    <asp:BoundField DataField="AttendanceGUID" HeaderText="AttendanceGUID" ReadOnly="True" SortExpression="AttendanceGUID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>

                                    <asp:BoundField DataField="StudentGUID" HeaderText="StudentGUID" ReadOnly="True" SortExpression="StudentGUID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                    <asp:BoundField DataField="FullName" HeaderText="Student Name" SortExpression="FullName" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                        <asp:TemplateField HeaderText ="Attendance" SortExpression="Mark">
                                        <ItemTemplate>
                                            <input type="checkbox" runat="server" id="cbAttendance" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Label id="lblNoData" runat="server" Visible="false" Text="No data to Display"></asp:Label>
                     </div>
                  </div>
               </div>
                      <br />

                          <div class="form-group">
                            <div style="float:right">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn-success" Text="Save" OnClick="btnSave_Click" Width="100px"/>
                            <asp:Button ID="btnBack" runat="server" CssClass="btn-danger" Text="Cancel" OnClick="btnBack_Click" Width="100px"/>
                            </div>
                           

                        </div>

               </div>
            </div>
</asp:Content>
