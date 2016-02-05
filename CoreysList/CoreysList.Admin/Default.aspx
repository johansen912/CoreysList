<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CoreysList.Admin.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="/Content/Site.css"/>
    <title>Coreys List</title>
</head>
<body class="defaultBody">
    <form id="form1" runat="server">
        <div class="defaultWrapper">
            <div class="defaultHeader">
                <div class="defaultHeader_Text">Corey's List</div>
            </div>
            <div class="defaultFormContainer">
                <div class="defaultFormContainer_header">Admin Login</div>
                <table class="defaultFormTable">
                    <tr>
                        <td>
                            <asp:Label ID="lbEmail" CssClass="defaultFormTable_labels" runat="server">Email: </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tbEmail" CssClass="defaultFormTable_textboxes" TextMode="Email" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbPassword" CssClass="defaultFormTable_labels" runat="server">Password: </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tbPassword" CssClass="defaultFormTable_textboxes" TextMode="Password" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="defaultFormTable_btnCell">
                            <asp:Button ID="btnLogin" OnClick="btnLogin_Click" Text="Login" runat="server" CssClass="defaultFormTable_SubmitBtn" />
                            <asp:Button ID="btnClear" OnClick="btnClear_Click" Text="Clear" runat="server" CssClass="defaultFormTable_ClearBtn" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pnlLoginErrorMessage" CssClass="defaultFormTable_ErrorMsg" runat="server">Incorrect Email / Password Combination.</asp:Panel>
            </div>
        </div>
    </form>
</body>
</html>
