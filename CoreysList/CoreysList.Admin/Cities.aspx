<%@ Page Title="" Language="C#" MasterPageFile="~/CoreysListAdmin.Master" AutoEventWireup="true" CodeBehind="Cities.aspx.cs" Inherits="CoreysList.Admin.Cities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">

    <link rel="stylesheet" href="Content/bootstrap.min.css" />
    <script>
        function confirmDelete(){
            return confirm("Do you want to delete this user and their listings?");
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">

    <div class="cityPageBanner">
        <div class="cityHeader">View / Edit Cities</div>
        <asp:Panel ID="pnlFindcity" CssClass="findCityFilter" runat="server">
            <label class="filterCityLabel">Major / NonMajor: </label>
            <asp:DropDownList ID="ddMajorFilter" CssClass="cityMajorDropDown" AutoPostBack="true" OnSelectedIndexChanged="ddMajorFilter_SelectedIndexChanged" runat="server">
                <asp:ListItem Value="all"> All</asp:ListItem>
                <asp:ListItem Value="major">Major</asp:ListItem>
                <asp:ListItem Value="nonMajor">NonMajor</asp:ListItem>
            </asp:DropDownList>
            <label class="filterUserLabel">Find City: </label>
            <asp:TextBox ID="tbFindCity" CssClass="findCityText" runat="server"></asp:TextBox>
            <asp:Button ID="btnsearchCity" CssClass="findCityBtn" Text="Search" OnClick="btnsearchCity_Click" runat="server" />
            <asp:ImageButton ID="imgAddCity" ImageUrl="~/Images/plusIcon.png" CssClass="addCityIcon" ToolTip="Add City" OnClick="imgAddCity_Click" runat="server" />
        </asp:Panel>
        <div class="clear"></div>
    </div>

    <asp:GridView ID="gvCities" CssClass="table table-striped color-table custTable " AutoGenerateColumns="false" 
          OnRowDataBound="gvCities_RowDataBound" AllowSorting="true" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvCities_PageIndexChanging"
         OnSorting="gvCities_Sorting" OnRowCommand="gvCities_RowCommand" DataKeyNames="CityID" runat="server" >
        <Columns>
            <asp:BoundField DataField="CityID" HeaderText="ID" SortExpression="CityID" HeaderStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CityName" HeaderText="City" SortExpression="CityName" HeaderStyle-HorizontalAlign="Center"  />
            <asp:BoundField DataField="StateName" HeaderText="State" SortExpression="StateName" HeaderStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ListingCount" SortExpression="ListingCount" HeaderText="ListingCount" HeaderStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="MajorCity" SortExpression="MajorCity" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                <ItemTemplate>
                    <asp:Label ID="lbMajorCityImg" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CreatedDate" DataFormatString="{0:d}" SortExpression="CreatedDate" HeaderText="Added" HeaderStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Button CommandName="EditCity" CommandArgument='<%# Eval("CityID") %>' Text="Edit" runat="server" />
                    <asp:Button CommandName="DeleteCity" CommandArgument='<%# Eval("CityID") %>' OnClientClick="return confirmDelete()" Text="Delete" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Panel ID="pnlBackdrop" CssClass="pnlBackdrop" Visible="false" runat="server" ></asp:Panel>
    
    <asp:Panel ID="pnlCityEdit" Visible="false" CssClass="pnlEdit custEditTable" runat="server">
        <div class="EditHeader">Edit City</div>
        <table class="modal_editTable">
            <tr>
                <td class="labelTd">
                    <label>City ID: </label>                  
                </td>
                <td class="contentTd">
                     <asp:Label ID="lbCityID" runat="server"></asp:Label>
                </td>
            </tr>
              <tr>
                <td class="labelTd">
                    <label>CityName: </label>                  
                </td>
                <td class="contentTd">
                     <asp:Label ID="lbCityName" CssClass="contentTextBox" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="labelTd">
                    <label>State ID: </label>                  
                </td>
                <td class="contentTd">
                     <asp:Label ID="lbStateID" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="labelTd">
                    <label>StateName: </label>                  
                </td>
                <td class="contentTd">
                    <asp:Label ID="lbStateName" CssClass="contentTextBox" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="labelTd">
                    <label>MajorCity: </label>                  
                </td>
                <td class="contentTd">
                     <asp:CheckBox ID="cbMajorCity" runat="server" />
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

    <asp:Panel ID="pnlAddCity" Visible="false" CssClass="pnlEdit custEditTable" runat="server">
        <div class="EditHeader">Add City</div>
        <table class="modal_editTable">
            <tr>
                <td class="labelTd">
                    <label>State: </label>                  
                </td>
                <td class="contentTd">
                     <asp:DropDownList ID="ddSelectNewState" CssClass="contentTextBox" runat="server"></asp:DropDownList>
                </td>
            </tr>
              <tr>
                <td class="labelTd">
                    <label>City Name: </label>                  
                </td>
                <td class="contentTd">
                     <asp:TextBox ID="tbNewCityName" CssClass="contentTextBox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="labelTd">
                    <label>Major City: </label>                  
                </td>
                <td class="contentTd">
                     <asp:CheckBox ID="cbNewMajorCity" CssClass="contentTextBox" runat="server" />
                </td>
            </tr>            
            <tr>
                <td colspan="2" class="buttonTd">
                    <asp:Button ID="btnAddCity" runat="server" CssClass="editTableBtn" OnClick="btnAddCity_Click" Text="Add" />
                    <asp:Button ID="btnCancelCityAdd" CausesValidation="false" CssClass="editTableBtn" runat="server" OnClick="btnCancelCityAdd_Click" Text="Cancel" />                
                </td>
            </tr>
        </table>
    </asp:Panel>

</asp:Content>
