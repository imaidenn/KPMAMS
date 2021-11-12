<%@ Page Title="" Language="C#" MasterPageFile="~/user.Master" AutoEventWireup="true" CodeBehind="ExamParent.aspx.cs" Inherits="KPMAMS.ExamParent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
     <div class="panel panel-default">
               <div class="panel-heading"><h2>Exam</h2></div>
               <div class="panel-body">
    <div class="col-lg-12">
                  <div class="panel panel-default">
                     <div class="panel-heading">
                         <asp:Label ID="lblTotalQty" runat="server"></asp:Label>
                     </div>
                     <div class="panel-body">
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" CssClass="table table-bordered table-condensed table-responsive table-hover" Enabled="False" Width="100%" AutoGenerateColumns="False" DataKeyNames="StudentGUID" OnRowDataBound="GridView1_RowDataBound" > 
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlView" Text="View" runat="server" ></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="StudentGUID" HeaderText="StudentGUID" ReadOnly="True" SortExpression="StudentGUID" Visible="false"/>
                                    <asp:BoundField DataField="FullName" HeaderText="FullName" SortExpression="FullName"/>
                                    <asp:BoundField DataField="ICNo" HeaderText="ICNo" SortExpression="ICNo"/>
                                    <asp:BoundField DataField="Class" HeaderText="Class" SortExpression="Class"/>

                                </Columns>
                            </asp:GridView>
                            <asp:Label id="lblNoData" runat="server" Visible="false" Text="No data to Display"></asp:Label>
                     </div>
                  </div>
               </div>
                   </div>
            </div>
</asp:Content>
