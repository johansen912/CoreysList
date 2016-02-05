<%@ Page Title="" Language="C#" MasterPageFile="~/CoreysListAdmin.Master" AutoEventWireup="true" CodeBehind="Listings_InlineDemo.aspx.cs" Inherits="CoreysList.Admin.Listings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
    <h1 class="tableHeading">Listings</h1>

    <asp:GridView ID="gvListings" CssClass="table table-striped color-table custTable " AutoGenerateColumns="false" 
         OnRowDeleting="gvListings_RowDeleting" OnRowEditing="gvListings_RowEditing" OnRowCancelingEdit="gvListings_RowCancelingEdit" 
        OnRowUpdating="gvListings_RowUpdating" OnRowDataBound="gvListings_RowDataBound" runat="server">
        <Columns>
            <asp:BoundField DataField="ListingID" HeaderText="ID" ReadOnly="true" />
            <asp:BoundField DataField="UserID" HeaderText="UserId" ReadOnly="true" />
            <asp:TemplateField>
                <HeaderTemplate>State</HeaderTemplate>
                <ItemTemplate>
                    <%# Eval("StateName") %>
                </ItemTemplate>
                <EditItemTemplate>
                   <asp:DropDownList ID="ddState" AutoPostBack="true" OnSelectedIndexChanged="ddState_SelectedIndexChanged" runat="server"></asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>City</HeaderTemplate>
                <ItemTemplate>
                    <%# Eval("CityName") %>
                </ItemTemplate>
                <EditItemTemplate>
                   <asp:DropDownList ID="ddCity" runat="server"></asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>Category</HeaderTemplate>
                <ItemTemplate>
                    <%# Eval("CategoryName") %>
                </ItemTemplate>
                <EditItemTemplate>
                   <asp:DropDownList ID="ddCategory" AutoPostBack="true" OnSelectedIndexChanged="ddCategory_SelectedIndexChanged" runat="server"></asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>SubCategory</HeaderTemplate>
                <ItemTemplate>
                    <%# Eval("SubCategoryName") %>
                </ItemTemplate>
                <EditItemTemplate>
                   <asp:DropDownList ID="ddSubcategory" runat="server"></asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>             
            <asp:BoundField DataField="Headline" HeaderText="Headline"/>
            <asp:BoundField DataField="Location" HeaderText="Location"/>
            <asp:BoundField DataField="Description" HeaderText="Description"/>
            <asp:BoundField DataField="Price" HeaderText="Price"/>
            <asp:CommandField ShowEditButton="true" />
            <asp:CommandField ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>
</asp:Content>
