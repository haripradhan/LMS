<%@ Page Title="" Language="C#" MasterPageFile="~/MyCourse.master" AutoEventWireup="true" CodeBehind="frmViewGradeByInstructor.aspx.cs" Inherits="LMS.InstructorSite.frmViewGradeByInstructor" %>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHeaderContentPlaceholder" runat="server">
    Grade (Out of 100)
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
     <asp:DataList ID="dlGradeAssignment" runat="server" Width="100%" OnItemCommand="DlCommandItem">
        <ItemTemplate>
            <div id="bodyContainer" style="height: 50px;">
                <div class="leftC" style="height: 50px;">
                    <div>
                        <asp:Label ID="lblStudentID"  runat="server" Text='<%# Eval("StudentID") %>'
                            Visible="False" />
                        <asp:LinkButton ID="lbtnStudentName"  CssClass="lbtnLecture" runat="server" Text='<%# Eval("StudentName") %>'
                            Visible="True" CommandName="StudentDetailInfo" /> &nbsp;&nbsp;&nbsp;
                     </div>
                </div>
                <div class="rightC" style="margin-top:10px;">
                    <asp:Label ID="lblGrade" runat="server" Height="28px" Width="35px" Text='<%# Eval("AggregateGrade") %>' ToolTip="Aggregate Grade" ></asp:Label>
                </div>
            </div>
        </ItemTemplate>
        <SeparatorTemplate>
        </SeparatorTemplate>
    </asp:DataList>
</asp:Content>
