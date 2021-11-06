<%@ Page Title="" Language="C#" MasterPageFile="~/user.Master" AutoEventWireup="true" CodeBehind="CreateQuiz.aspx.cs" Inherits="KPMAMS.CreateQuiz" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
     <div class="panel panel-default">
               <div class="panel-heading">Create Quiz</div>
               <div class="panel-body">
                  <fieldset>
                        <div class="form-group">
                           <label class="col-sm-1 control-label">Quiz Title</label>
                           <div class="col-sm-5">
                               <asp:TextBox ID="txtQuizTitle" runat="server" CssClass="form-control form-control-rounded"></asp:TextBox>
                           </div>
                           <label class="col-sm-1 control-label">Class</label>
                           <div class="col-sm-5">
                               <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control m-b"></asp:DropDownList>
                           </div>
                        </div>
                     </fieldset>
                   <br />
                    <fieldset>
                    
                        <div class="form-group">
                            <div style="float:right">
                                        <asp:Button ID="btnAdd" CssClass="btn btn-primary" runat="server" Text="Add Question" OnClick="btnAdd_Click" />
                            <asp:Button ID="btnDone" runat="server" CssClass="btn btn-primary" Text="Done" OnClick="btnDone_Click"/>
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
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" CssClass="table table-bordered table-condensed table-responsive table-hover" Enabled="False" Width="100%" AutoGenerateColumns="False" DataKeyNames="QuestionGUID" OnRowDataBound="GridView1_RowDataBound" > 
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlView" Text="View" runat="server" ></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="QuestionGUID" HeaderText="QuestionGUID" ReadOnly="True" SortExpression="QuestionGUID" Visible="false"/>
                                    <asp:BoundField DataField="Question" HeaderText="Question" SortExpression="Question"/>

                                </Columns>
                            </asp:GridView>
                            <asp:Label id="lblNoData" runat="server" Visible="false" Text="No data to Display"></asp:Label>
                     </div>
                  </div>
               </div>
               </div>
            </div>
</asp:Content>
