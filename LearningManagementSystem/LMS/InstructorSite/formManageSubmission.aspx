<%@ Page Title="" Language="C#" MasterPageFile="~/MyCourse.master" AutoEventWireup="true" CodeBehind="formManageSubmission.aspx.cs" Inherits="LMS.InstructorSite.formManageSubmission" %>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHeaderContentPlaceholder" runat="server">
    Assignment
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <asp:DataList ID="dlGradeAssignmentByInstructor" runat="server" Width="100%" OnItemCommand="ViewDownloadFile">
        <ItemTemplate>
            <div id="bodyContainer" style="height: 50px;">
                <div class="leftC" style="height: 50px;">
                    <div>
                       <asp:Label ID="lblStudentID" runat="server" Text='<%# Eval("StudentID") %>'
                            Visible="False" />
                        
                        <asp:Label CssClass="lbtnLecture" ID="lblStudentName" runat="server" Text='<%# Eval("StudentName") %>'
                            Visible="True" />
                        
                    </div>
                    <div>
                        <asp:Label ID="lblFileLocation" runat="server" Text='<%# Eval("FileLocation") %>' Visible="False" ></asp:Label>
                        <asp:LinkButton  ID="lblAssign"  CssClass="lblManageAssignment" ForeColor="Blue" runat="server" Text='<%# Eval("ATitle") %>'
                            Visible="True" CommandName="Download" /> &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label1" CssClass="lblManageAssignment" runat="server" Text="Submitted Date: " />
                        <asp:Label ID="lblSubmissionDate" CssClass="lblManageAssignment" runat="server" Text='<%# Eval("SubmissionDate") %>'
                            Visible="True" />
                    </div>
                </div>
                <div class="rightC" style="margin-top:10px;">
                    <asp:TextBox ID="txtBoxGrade" runat="server" ToolTip="Enter a grade." Height="28px" Width="35px" Text='<%# Eval("Grade") %>' BackColor="#CCCCCC"></asp:TextBox>
                    <asp:RangeValidator ID="rngValidatorGrade" runat="server" ErrorMessage="Only 0 to 100" ControlToValidate="txtBoxGrade" MinimumValue="0" MaximumValue="100" Type="Double"></asp:RangeValidator>
                </div>
            </div>
        </ItemTemplate>
        <SeparatorTemplate>
        </SeparatorTemplate>
    </asp:DataList>
    <br/>
    <div class="bottomC">
        <asp:Label ID="lblNoAssignmentText" runat="server" Visible="False" ></asp:Label> <br/>
        <asp:Button CssClass="ubtnManageLecture" ID="btnSave" BorderStyle="Solid"
            Text="Save"  runat="server" ToolTip="Save" OnClick="btnSave_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button CssClass="ubtnManageLecture" ID="btnCancel" BorderStyle="Solid"
            Text="Cancel"  runat="server" ToolTip="Cancel" onclick="btnCancel_Click" />
    </div>
    
</asp:Content>
