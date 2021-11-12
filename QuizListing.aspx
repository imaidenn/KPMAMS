<%@ Page Title="" Language="C#" MasterPageFile="~/user.Master" AutoEventWireup="true" CodeBehind="QuizListing.aspx.cs" Inherits="KPMAMS.QuizListing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="panel panel-default">
               <div class="panel-heading"><h3>Quiz</h3></div>
               <div class="panel-body">
                   <div class="col-lg-12">
                        <fieldset>
                    
                        <div class="form-group">
                            <div style="float:right">
                                        <asp:Button ID="btnCreate" CssClass="btn btn-primary" runat="server" Text="Create" OnClick="btnCreate_Click" />
                            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnBack_Click"/>
                            </div>
                           

                        </div>
                     </fieldset>
                   </div>
    <div class="col-lg-12">
                  <div class="panel panel-default">
                     <div class="panel-heading">
                         <asp:Label ID="lblTotalQty" runat="server"></asp:Label>
                     </div>
                     <div class="panel-body">
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" CssClass="table table-bordered table-condensed table-responsive table-hover" Enabled="False" Width="100%" AutoGenerateColumns="False" DataKeyNames="QuizGUID" OnRowDataBound="GridView1_RowDataBound" > 
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlView" Text="View" runat="server" ></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="QuizGUID" HeaderText="QuizGUID" ReadOnly="True" SortExpression="QuizGUID" Visible="false"/>
                                    <asp:BoundField DataField="QuizTitle" HeaderText="QuizTitle" SortExpression="QuizTitle"/>
                                    <asp:BoundField DataField="TotalQuestion" HeaderText="TotalQuestion" SortExpression="TotalQuestion"/>
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
