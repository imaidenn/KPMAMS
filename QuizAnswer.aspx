<%@ Page Title="" Language="C#" MasterPageFile="~/user.Master" AutoEventWireup="true" CodeBehind="QuizAnswer.aspx.cs" Inherits="KPMAMS.QuizAnswer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
     <div class="panel panel-default">
               <div class="panel-heading"><h3>Answer Quiz</h3></div>
               <div class="panel-body">
               <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                   <ItemTemplate>
            <div style="float:left">
                <asp:Label ID="lblQuestionGUID" runat="server" Visible="false" Text='<%#Eval("QuestionGUID")%>' ></asp:Label>
            <asp:Label ID="lblQuestion" runat="server" class="col-lg-12 control-label" Text='<%#Eval("Question")%>' Font-Bold="true" Font-Size="X-Large"></asp:Label>
            </div>
                       <br />
                       <br />
            <div style="clear:both">

            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" CellPadding="3" CellSpacing="2"></asp:RadioButtonList>
            </div>
            <br />
            <hr></hr>
        </ItemTemplate>
               </asp:Repeater>   
                   <fieldset>
                    
                        <div class="form-group">
                            <div style="float:right">
                                 <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                            </div>
                           

                        </div>
                     </fieldset>

               


               </div>
            </div>
</asp:Content>
