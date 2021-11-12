<%@ Page Title="" Language="C#" MasterPageFile="~/user.Master" AutoEventWireup="true" CodeBehind="AttendanceDetails.aspx.cs" Inherits="KPMAMS.AttendanceDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
     <div class="panel panel-default">
               <div class="panel-heading"><h3>Attendance</h3></div>
               <div class="panel-body">
                   <div class="row">
                       <div class="col-lg-12">
                           <div class="panel-body">
                               <h1><asp:Label ID="lblSubject" runat="server"></asp:Label></h1>
                   <br/>
                   <asp:Label ID="lblTotalClass" runat ="server"></asp:Label>&nbsp&nbsp&nbsp
                   <asp:Label ID="lblTotalAttend" runat="server"></asp:Label>
                           </div>
                    
                   </div>
                       </div>

                    <div class="col-lg-12">
                  <div class="panel panel-default">
                     <div class="panel-heading">
                         <asp:Label ID="lblTotalQty" runat="server"></asp:Label>
                     </div>
                     <div class="panel-body">
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" CssClass="table table-bordered table-condensed table-responsive table-hover" Enabled="False" Width="100%" AutoGenerateColumns="False" DataKeyNames="AttendanceGUID" > 
                                <Columns>
                                    <asp:BoundField DataField="AttendanceGUID" HeaderText="AttendanceGUID" ReadOnly="True" SortExpression="AttendanceGUID" Visible="false"/>
                                    <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" DataFormatString="{0:dd/MM/yyyy}"/>
<%--                                    <asp:BoundField DataField="StartTime" HeaderText="StartTime" SortExpression="StartTime" />
                                    <asp:BoundField DataField="EndTime" HeaderText="EndTime" SortExpression="EndTime" />--%>
                                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />

                                </Columns>

                            </asp:GridView>
                            <asp:Label id="lblNoData" runat="server" Visible="false" Text="No data to Display"></asp:Label>
                     </div>
                  </div>
               </div>
                   </div>
         </div>
</asp:Content>
