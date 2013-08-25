<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="LMS.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <link href="Styles/StyleLmsLogin.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/WaterMark.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("[id*=txtboxUserName], [id*=txtboxPassword]").WaterMark();
        });
    </script>
    <form id="frmLmsLogin" runat="server">
     <div id="content">
        <div class="leftContent">
            <p>
                <h3>
                    Learning Management System</h3>
                <hr style="color: #3766b5; width: 305px;" />
                Student can download notes, assignments, submit it and see the result whereas instructor
                can upload assignments, notes and grade the students work.
            </p>
        </div>
        <div class="rightContent">
            <div class="top">
            </div>
            <div class="middle">
                <h3 style="margin-right:180px;">
                    Sign in to LMS</h3>
                <hr style="color: #3766b5; width: 310px;" />
                <div>
                    <asp:TextBox ID="txtboxUserName" CssClass="unwatermarked" runat="server" Width="295px" ToolTip="UserName"></asp:TextBox>
                   
                </div>
                <div>
                    <asp:TextBox ID="txtboxPassword" CssClass="unwatermarked" runat="server" TextMode="Password" ToolTip="Password"
                        Width="180px"></asp:TextBox>
                    &nbsp;
                    <asp:Button ID="btnLmsLogin" runat="server" CommandName="LmsLogin" Text="Login" CssClass="button"
                        Width="100px" OnClick="btnLmsLogin_Click" />
                </div>
            </div>
            <div class="bottom" style="font-family: cursive; font-size: 12px;">
                <asp:Label ID="lblErrorMessage" runat="server" Text="Username / password incorrect. Please login again." Visible="False"></asp:Label>
            </div>
            <br />
        </div>
    </div>
    </form>
</asp:Content>
