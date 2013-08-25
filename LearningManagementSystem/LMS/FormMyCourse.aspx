<%@ Page Title="" Language="C#" MasterPageFile="~/Lms.Master" AutoEventWireup="true"
    CodeBehind="FormMyCourse.aspx.cs" Inherits="LMS.FormMyCourse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCourseContainer" runat="server">
    <link href="Styles/StyleCourseContainer.css" rel="stylesheet" type="text/css" />
    <center>
        <div class="courseContainerHeading">
            <asp:Label ID="lblCourse" runat="server" CssClass="labelC1" Width="100%">Your Courses</asp:Label>
        </div>
        <div>
            <asp:DataList ID="dlCourseList" runat="server" Width="800px" OnItemCommand="SelectCourseItemCommand">
                <ItemTemplate>
                    <div id="courseContainer">
                        <div class="imageC">
                            <asp:Image ID="imgCourse" runat="server" ImageUrl="~/Images/crsImage01.jpg" />
                            <%--<asp:Image ID="imgCourse" runat="server" ImageUrl='<%#"~/Images/" + Eval("image") %>' />--%>
                        </div>
                        <div class="leftC">
                            <div>
                                <asp:LinkButton CssClass="linkbuttonC" ID="lbCourseName" runat="server" Text='<%# Bind("CourseName") %>'
                                    Visible="True" CommandName="courseName"></asp:LinkButton>
                            </div>
                            <div>
                                <asp:Label CssClass="labelC" ID="lblInstructorName" runat="server" Text='<%# Eval("InstructorName") %>'
                                    Visible="True" CommandName='<%# Eval("InstructorName") %>'></asp:Label></div>
                        </div>
                        <div class="rightC">
                            <div>
                                <asp:LinkButton CssClass="linkbuttonC2" ID="lbViewInfo" runat="server" Text='<%# Bind("ViewInfo") %>'
                                    Visible="True" CommandName='ViewCourse'></asp:LinkButton>
                                <asp:Label ID="lblCourseID" runat="server" Text='<%# Eval("CourseID") %>' Visible="False"></asp:Label></div>
                            <div style="margin-top: 55px;">
                                <asp:Label CssClass="labelC" ID="lblEnrollStatus" runat="server" Text="Enrolled"
                                    Visible="true"></asp:Label>
                                <%--<asp:Label CssClass=labelC" ID="lblEnrollStatus" runat="server"
                        Text='<%# Eval("EnrollStatus") %>' Visible="True" CommandName='<%# Eval("EnrollStatus") %>'></asp:Label>--%>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
                <SeparatorTemplate>
                    <br />
                </SeparatorTemplate>
            </asp:DataList>
        </div>
        <div id="footer">
            <p>
                © Copyright 2013, Learning Management System
                <br />
                Designed and developed by LMS team
            </p>
        </div>
    </center>
</asp:Content>
