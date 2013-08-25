<%@ Page Title="" Language="C#" MasterPageFile="~/MyCourse.master" AutoEventWireup="true"
    CodeBehind="UploadAssignment.aspx.cs" Inherits="LMS.InstructorSite.UploadAssignment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHeaderContentPlaceholder" runat="server">
    Manage Assignment
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <asp:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" ScriptMode="Debug" CombineScripts="false" />
    
    <div id="uploadContainer">
        <div>
            <asp:Label ID="lblAssignmentTitle" CssClass="lblManageLecture" runat="server" Text=" Assignment Title"
                Visible="True" />
            <asp:TextBox ID="txtboxAssignmentTitle" CssClass="tbManageLecture" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="lblDueDate" CssClass="lblManageLecture" runat="server" Text="Due Date"
                Visible="True" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtboxDueDate" CssClass="tbManageLecture" runat="server"></asp:TextBox>
            <asp:ImageButton runat="Server" ID="cldrImage" ImageUrl="~/images/calendar_scheduleHS.png" AlternateText="Click to show calendar" /><br />
            <asp:CalendarExtender ID="cExtenderDueData" runat="server" TargetControlID="txtboxDueDate" PopupButtonID="cldrImage">
            </asp:CalendarExtender>

        </div>
        <div>
            <asp:Label ID="lblLectureLocation" CssClass="lblManageLecture" runat="server" Text="Upload Assignment"
                Visible="True" />
            <asp:FileUpload CssClass="fuManageLecture" ID="fileuploadAssignment" 
                runat="server" />
        </div>
        <div>
            <asp:Button ID="btnUpload" CssClass="ubtnManageLecture" runat="server" Text="Upload"
                OnClick="btnUpload_Click" />
        </div>
    </div>
</asp:Content>
