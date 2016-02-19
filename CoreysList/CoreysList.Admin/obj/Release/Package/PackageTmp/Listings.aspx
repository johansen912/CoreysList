<%@ Page Title="" Language="C#" MasterPageFile="~/CoreysListAdmin.Master" AutoEventWireup="true" CodeBehind="Listings.aspx.cs" Inherits="CoreysList.Admin.Listings1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
    <link rel="stylesheet" href="Content/bootstrap.min.css" />
    <script>
        function confirmDelete(){
            return confirm("Do you want to delete this listing and all of the listing's images?");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">

     <div class="listingPageBanner">
        <div class="listingHeader">View / Edit Listings</div>
        <asp:Panel ID="pnlFindListing" CssClass="findListingFilter" runat="server">
            <label class="filterListingLabel">Active / Inactive: </label>
            <asp:DropDownList ID="ddActiveFilter" CssClass="listingActiveDropDown" AutoPostBack="true" OnSelectedIndexChanged="ddActiveFilter_SelectedIndexChanged" runat="server">
                <asp:ListItem Value="all"> All</asp:ListItem>
                <asp:ListItem Value="active">Active</asp:ListItem>
                <asp:ListItem Value="inactive">Inactive</asp:ListItem>
            </asp:DropDownList>
            <label class="filterUserLabel">Find Listing: </label>
            <asp:TextBox ID="tbFindListing" CssClass="findListingText" runat="server"></asp:TextBox>
            <asp:Button ID="btnsearchListing" CssClass="findListingBtn" Text="Search" OnClick="btnsearchListing_Click" runat="server" />
        </asp:Panel>
        <div class="clear"></div>
    </div>

    <asp:GridView ID="gvListings" CssClass="table table-striped color-table custTable " AutoGenerateColumns="false" 
          OnRowDataBound="gvListings_RowDataBound" AllowSorting="true" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvListings_PageIndexChanging"
         OnSorting="gvListings_Sorting" OnRowCommand="gvListings_RowCommand" DataKeyNames="ListingID" runat="server">
        <Columns>
            <asp:BoundField DataField="ListingID" SortExpression="ListingID" HeaderText="ID" />
            <asp:BoundField DataField="UserID" SortExpression="UserID" HeaderText="User" />
            <asp:BoundField DataField="Headline" SortExpression="Headline" HeaderText="Headline" />
            <asp:BoundField DataField="Price" DataFormatString="{0:0.00}" SortExpression="Price" HeaderText="Price" />
            <asp:BoundField DataField="CityName" SortExpression="CityName" HeaderText="City" />
            <asp:BoundField DataField="Location" SortExpression="Location" HeaderText="Location" />
            <asp:BoundField DataField="SubCategoryName" SortExpression="SubCategoryName" HeaderText="SubCategory" />
            <asp:TemplateField SortExpression="IsActive" HeaderText="Active" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# getIsActiveIcon( Convert.ToBoolean( Eval("IsActive") ) )  %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CreatedDate" DataFormatString="{0:d}" SortExpression="CreatedDate" HeaderText="Posted" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button CommandName="EditListing" CommandArgument='<%# Eval("ListingID") %>' Text="Edit" runat="server" />
                    <asp:Button CommandName="DeleteListing" CommandArgument='<%# Eval("ListingID") %>' OnClientClick="return confirmDelete()" Text="Delete" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Panel ID="pnlBackdrop" CssClass="pnlBackdrop" Visible="false" runat="server" ></asp:Panel>
    <asp:Panel ID="pnlListingEdit" Visible="false" CssClass="pnlEdit custEditTable" runat="server">
        <div class="EditHeader">Edit Listing</div>
        <table class="modal_editTable">
            <tr>
                <td class="labelTd">
                    <label>Listing ID: </label>                  
                </td>
                <td class="contentTd">
                     <asp:Label ID="lbListingId" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="labelTd">
                    <label>User ID:  </label>                  
                </td>
                <td class="contentTd">
                      <asp:Label ID="lbUserID" CssClass="contentTextBox" runat="server"></asp:Label>
                </td>
            </tr>
              <tr>
                <td class="labelTd">
                    <label>Headline: </label>                  
                </td>
                <td class="contentTd">
                     <asp:TextBox ID="tbHeadline" CssClass="contentTextBox longBox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="labelTd">
                    <label>State: </label>                  
                </td>
                <td class="contentTd">
                     <asp:DropDownList ID="ddState" AutoPostBack="true" OnSelectedIndexChanged="ddState_SelectedIndexChanged" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="labelTd">
                    <label>City: </label>                  
                </td>
                <td class="contentTd">
                     <asp:DropDownList ID="ddCity" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="labelTd">
                    <label>Location: </label>                  
                </td>
                <td class="contentTd">
                     <asp:TextBox ID="tbLocation" CssClass="contentTextBox" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="labelTd">
                    <label>Category: </label>                  
                </td>
                <td class="contentTd">
                    <asp:DropDownList ID="ddCategory" AutoPostBack="true" OnSelectedIndexChanged="ddCategory_SelectedIndexChanged" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="labelTd">
                    <label>SubCategory: </label>                  
                </td>
                <td class="contentTd">
                     <asp:DropDownList ID="ddSubCategory" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="labelTd">
                    <label>Price: </label>                  
                </td>
                <td class="contentTd">
                     <asp:TextBox ID="tbPrice" TextMode="Number" CssClass="contentTextBox" runat="server" />
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
                <td class="labelTd">
                    <label>Description: </label>                  
                </td>
                <td class="contentTd">
                     <asp:TextBox ID="tbDescription" TextMode="MultiLine" Columns="50" Rows="5" runat="server"></asp:TextBox>
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
