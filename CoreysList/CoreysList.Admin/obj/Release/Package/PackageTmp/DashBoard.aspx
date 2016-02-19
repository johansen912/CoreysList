<%@ Page Title="" Language="C#" MasterPageFile="~/CoreysListAdmin.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="CoreysList.Admin.DashBoard" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
    <div class="dashboard_content">
        <div class="clear"></div>
        <asp:Label ID="lbDashboardHeader" CssClass="dashboardHeader" runat="server"></asp:Label>
        <table class="dashboard_contentLayoutTable">
            <tr>
                <td>
                    <div class="dashboardSubHeader"> Users </div>
                    <table class="dashboard_content_userContent_SubTable">
                        <tr>
                            <td>
                                <asp:Label ID="lbUserTotal" CssClass="dashboard_contentData" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbUserLastSevenDays" CssClass="dashboard_contentData" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbUserLastThirtyDays" CssClass="dashboard_contentData" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <div class="dashboardSubHeader"> Listings </div>
                    <table class="dashboard_content_userContent_SubTable">
                        <tr>
                            <td>
                                <asp:Label ID="lbListingTotal" CssClass="dashboard_contentData" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbListingLastSevenDays" CssClass="dashboard_contentData" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbListingLastThirtyDays" CssClass="dashboard_contentData" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="lowerHeader">Listings By Category</td>
            </tr>
            <tr>
                <td>
                    <div class="dashboard_legendContainer">
                        <table class="dashboard_chartLegend">
                            <asp:Repeater ID="rptChartLegend" runat="server"> 
                                <HeaderTemplate>
                                    <tr>
                                        <td colspan="2" class="legendHeader">Legend</td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="legendLabelTd"><%#Eval("CategoryName")%></td>
                                        <td class="dashboardLegendColors" style="background-color: <%#Eval("ChartColor")%>">&nbsp;</td>
                                    </tr>
                                </ItemTemplate>                         
                            </asp:Repeater>                       
                        </table>
                    </div>
                </td>
                <td class="dashboard_contentLayoutTable_chartTd">
                    <asp:Chart ID="pieListingByCat" Height="350px" Width="350px" runat="server">
                        <series>
                            <asp:Series Name="chartSeries" ChartType="Pie" ChartArea="MainArea" IsValueShownAsLabel="true" Label="#PERCENT">
                                <SmartLabelStyle />
                            </asp:Series>
                        </series>
                        <chartareas>
                            <asp:ChartArea Name="MainArea" BorderWidth= "0"  Area3DStyle-Enable3D="true" Area3DStyle-Inclination="30" Area3DStyle-IsClustered="false">
                                <AxisX LineWidth="0" IsMarginVisible="false">
                                </AxisX>
                                <Position Height="100" Width="100" X="0" Y="0" />
                            </asp:ChartArea>
                        </chartareas>
                    </asp:Chart>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
