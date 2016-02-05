<%@ Page Title="" Language="C#" MasterPageFile="~/CoreysListAdmin.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="CoreysList.Admin.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
    <link rel="stylesheet" href="Content/bootstrap.min.css" />
    <script>
        function confirmDelete(){
            return confirm("Do you want to delete this user and their listings?");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">

    <div class="userPageBanner">
        <div class="userHeader">View / Edit Users</div>
        <asp:Panel ID="pnlFindUser" CssClass="findUserFilter" runat="server">
            <label class="filterUserLabel">Active / Inactive: </label>
            <asp:DropDownList ID="ddActiveFilter" CssClass="userActiveDropDown" AutoPostBack="true" OnSelectedIndexChanged="btnsearchUser_Click" runat="server">
                <asp:ListItem Value="all"> All</asp:ListItem>
                <asp:ListItem Value="active">Active</asp:ListItem>
                <asp:ListItem Value="inactive">Inactive</asp:ListItem>
            </asp:DropDownList>
            <label class="filterUserLabel">Find User: </label>
            <asp:TextBox ID="tbFindUser" CssClass="findUserText" runat="server"></asp:TextBox>
            <asp:Button ID="btnsearchUser" CssClass="findUserBtn" Text="Search" OnClick="btnsearchUser_Click" runat="server" />
        </asp:Panel>
        <div class="clear"></div>
    </div>


    <asp:GridView ID="gvUsers" CssClass="table table-striped color-table custTable " AutoGenerateColumns="false" 
          OnRowDataBound="gvUsers_RowDataBound" AllowSorting="true" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvUsers_PageIndexChanging"
         OnSorting="gvUsers_Sorting" OnRowCommand="gvUsers_RowCommand" DataKeyNames="UserID" runat="server">
        <Columns>
            <asp:BoundField DataField="UserID" SortExpression="UserID" HeaderText="ID" />
            <asp:BoundField DataField="FullName" SortExpression="FullName" HeaderText="Name" />
            <asp:BoundField DataField="Email" SortExpression="Email" HeaderText="Email" />
            <asp:BoundField DataField="ListingCount" SortExpression="ListingCount" HeaderText="ListingCount" />
            <asp:TemplateField SortExpression="Admin" HeaderText="Admin" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# getIsAdminIcon( Convert.ToBoolean( Eval("Admin") ) )  %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="Active" HeaderText="Active" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# getIsActiveIcon( Convert.ToBoolean( Eval("Active") ) )  %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CreatedDate" DataFormatString="{0:d}" SortExpression="CreatedDate" HeaderText="Joined" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button CommandName="EditUser" CommandArgument='<%# Eval("UserID") %>' Text="Edit" runat="server" />
                    <asp:Button CommandName="DeleteUser" CommandArgument='<%# Eval("UserID") %>' OnClientClick="return confirmDelete()" Text="Delete" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            
        </Columns>
    </asp:GridView>

    <asp:Panel ID="pnlBackdrop" CssClass="pnlBackdrop" Visible="false" runat="server" ></asp:Panel>
    <asp:Panel ID="pnlUserEdit" Visible="false" CssClass="pnlEdit" runat="server">
        <div class="EditHeader">Edit User</div>
        <table class="modal_editTable">
            <tr>
                <td class="labelTd">
                    <label>UserID: </label>                  
                </td>
                <td class="contentTd">
                     <asp:Label ID="lbUserId" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="labelTd">
                    <label>First Name: </label>                  
                </td>
                <td class="contentTd">
                     <asp:TextBox ID="tbFirstName" CssClass="contentTextBox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="labelTd">
                    <label>Last Name: </label>                  
                </td>
                <td class="contentTd">
                     <asp:TextBox ID="tbLastName" CssClass="contentTextBox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="labelTd">
                    <label>Email: </label>                  
                </td>
                <td class="contentTd">
                     <asp:TextBox ID="tbEmail" CssClass="contentTextBox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="labelTd">
                    <label>Phone Number: </label>                  
                </td>
                <td class="contentTd">
                     <asp:TextBox ID="tbPhoneNum" CssClass="contentTextBox" TextMode="Phone" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="labelTd">
                    <label>Admin: </label>                  
                </td>
                <td class="contentTd">
                     <asp:CheckBox ID="cbIsAdmin" CssClass="contentTextBox" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="labelTd">
                    <label>Active: </label>                  
                </td>
                <td class="contentTd">
                     <asp:CheckBox ID="cbIsActive" CssClass="contentTextBox" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="buttonTd">
                    <asp:Button ID="btnSave" runat="server" CssClass="editTableBtn" OnClick="btnSave_Click" Text="Save" />
                    <asp:Button ID="btnCancel" CausesValidation="false" CssClass="editTableBtn" runat="server" OnClick="btnCancel_Click" Text="Cancel" />                
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
