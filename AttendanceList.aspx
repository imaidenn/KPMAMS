<%@ Page Title="" Language="C#" MasterPageFile="~/user.Master" AutoEventWireup="true" CodeBehind="AttendanceList.aspx.cs" Inherits="KPMAMS.AttendanceList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
        <div class="panel panel-default">
               <div class="panel-heading"><h3>Attendance</h3></div>
               <div class="panel-body">
                     <asp:Literal ID="litAttendancePanels" runat="server"></asp:Literal>
            <%--<div class="col-xs-6">
                <div class="panel panel-default">
                    <div class="panel-heading">Subject name<a class="pull-right" href="AttendanceDetails.aspx">To attendance details</a></div>
                    <div class="panel-body text-center">
                        <div class="row d-flex justify-content-center">
                            <div class="col-md-3">
                                <div data-label="85%" class="radial-bar radial-bar-85 radial-bar-lg"></div>
                            </div>
                            <div class="col-md-9">
                                <div class="col-md-6">
                                    <h3><span class="small text-gray">Total classes</span></h3>
                                    <div class="font-weight-bold mb-0">50</div>
                                </div>
                                <div class="col-md-6">
                                    <h3><span class="small text-gray">Attended classes</span></h3>
                                    <div class="font-weight-bold mb-0">45</div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>--%>
               <%--     <div class="col-lg-12">
                  <div class="panel panel-default">
                     <div class="panel-heading">
                         <asp:Label ID="lblTotalQty" runat="server"></asp:Label>
                     </div>
                     <div class="panel-body">
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" CssClass="table table-bordered table-condensed table-responsive table-hover" Enabled="False" Width="100%" AutoGenerateColumns="False" DataKeyNames="AttendanceGUID" OnRowDataBound="GridView1_RowDataBound" > 
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlView" Text="View" runat="server" ></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="AttendanceGUID" HeaderText="AttendanceGUID" ReadOnly="True" SortExpression="AttendanceGUID" Visible="false"/>
                                    <asp:BoundField DataField="SubjectGUID" HeaderText="SubjectGUID" ReadOnly="True" SortExpression="SubjectGUID" Visible="false"/>
                                    <asp:BoundField DataField="SubjectName" HeaderText="SubjectName" SortExpression="SubjectName" />

                                </Columns>

                            </asp:GridView>
                            <asp:Label id="lblNoData" runat="server" Visible="false" Text="No data to Display"></asp:Label>
                     </div>
                  </div>
               </div>--%>
                   </div>
            </div>

</asp:Content>
