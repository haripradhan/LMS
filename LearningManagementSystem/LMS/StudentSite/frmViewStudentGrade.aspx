<%@ Page Title="" Language="C#" MasterPageFile="~/MyCourse.master" AutoEventWireup="true" CodeBehind="frmViewStudentGrade.aspx.cs" Inherits="LMS.InstructorSite.frmViewStudentGrade" %>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHeaderContentPlaceholder" runat="server">
    Grade (Out of 100)
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <asp:DataList ID="dlGradeAssignment" runat="server" Width="100%">
        <ItemTemplate>
            <div id="bodyContainer" style="height: 50px;">
                <div class="leftC" style="height: 50px;">
                    <div>
                        <asp:Label ID="lblAssign" CssClass="lbtnLecture" runat="server" Text='<%# Eval("ATitle") %>'
                            Visible="True" /> &nbsp;&nbsp;&nbsp;
                     </div>
                </div>
                <div class="rightC" style="margin-top:10px;">
                    <asp:Label ID="lblGrade" runat="server" Height="28px" Width="35px" Text='<%# Eval("Grade") %>' ></asp:Label>
                </div>
            </div>
        </ItemTemplate>
        <SeparatorTemplate>
        </SeparatorTemplate>
    </asp:DataList>
    <br/>
    </asp:Content>
