<%@ Page Title="" Language="C#" MasterPageFile="~/MyCourse.master" AutoEventWireup="true" CodeBehind="formAssignment.aspx.cs" Inherits="LMS.Presentation.formAssignment" %>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHeaderContentPlaceholder" runat="server">
    Assignment
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
<asp:DataList ID="dlistAssignment" runat="server" 
        OnItemCommand="ViewDownloadAssignment" 
        Width="100%">
        <ItemTemplate>
            <div id="bodyContainer">
                <div class="leftC">
                    <asp:Label ID="lblAssignmentID" runat="server" Text='<%# Eval("AssignmentID") %>' Visible="False" />
                    <asp:Label ID="lblAFileLocation" runat="server" Text='<%# Eval("AFileLocation") %>' Visible="False"/>
                    <asp:LinkButton ID="lnkbtnAssignment" Height="29px" CssClass="lbtnLecture" runat="server" Text='<%# Bind("AssignmentTitle") %>' CommandName="View"/>
                    <asp:Button ID="btnDownload" Height="29px" width="40px" CssClass="btnDownload" runat="server" Tooltip="Download" CommandName="Download" />
                </div>
                <div class="rightC">
                    <asp:Button CssClass="ubtnGoToAssignment" ID="btnGoToAssignment" runat="server" Text="Go To Assignment" CommandName="GoToAssignment" />
                </div>
            </div>
        </ItemTemplate>
        <SeparatorTemplate>
        </SeparatorTemplate>
    </asp:DataList>
</asp:Content>
