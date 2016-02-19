using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using CoreysList.Admin.DbConnections;

namespace CoreysList.Admin
{
    public partial class DashBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //if no session is located send user to login screen
                if (Session["UserID"] == null)
                {
                    Response.Redirect("Default.aspx");

                }
                else // else init page
                {
                    // set up dashboard header
                    string userName = (string)(Session["UserName"]);
                    lbDashboardHeader.Text = "Welcome " + userName;
                    //set up user count
                    SqlDataReader userCountDr = AdminDb.GetUserCount();
                    if (userCountDr.Read())
                    {
                        lbUserTotal.Text = "Total: " + (userCountDr["UserCount"].ToString()) + "<br />";
                    }

                    //set up weekly user count
                    SqlDataReader weeklyUserCountDr = AdminDb.GetSevendayUserCount();
                    if (weeklyUserCountDr.Read())
                    {
                        lbUserLastSevenDays.Text = "Past 7 Days: " + (weeklyUserCountDr["UserCount"].ToString()) + "<br />";
                    }
                    //set up monthly user count
                    SqlDataReader monthlyUserCountDr = AdminDb.GetThirtydayUserCount();
                    if (monthlyUserCountDr.Read())
                    {
                        lbUserLastThirtyDays.Text = "Past 30 Days: " + (monthlyUserCountDr["UserCount"].ToString() + "<br />");
                    }

                    //set up listing count
                    SqlDataReader listingCountDr = AdminDb.GetListingsCount();
                    if (listingCountDr.Read())
                    {
                        lbListingTotal.Text = "Total: " + (listingCountDr["ListingCount"].ToString()) + "<br />";
                    }

                    //set up weekly listing count
                    SqlDataReader weeklyListingCountDr = AdminDb.GetSevendayListingsCount();
                    if (weeklyListingCountDr.Read())
                    {
                        lbListingLastSevenDays.Text = "Past 7 Days: " + (weeklyListingCountDr["ListingCount"].ToString()) + "<br />";
                    }
                    //set up monthly listing count
                    SqlDataReader monthlyListingCountDr = AdminDb.GetThirtydayListingCount();
                    if (monthlyListingCountDr.Read())
                    {
                        lbListingLastThirtyDays.Text = "Past 30 Days: " + (monthlyListingCountDr["ListingCount"].ToString() + "<br />");
                    }

                    //Set up pie chart demo
                    DataTable dtListingCount = AdminDb.GetListingCountByCategory();
                    foreach (DataRow row in dtListingCount.Rows)
                    {
                        DataPoint dp = new DataPoint(0, Double.Parse(row["ListingCount"].ToString()));
                        dp.Color = Color.FromName(row["ChartColor"].ToString());
                        dp.ToolTip = row["CategoryName"].ToString() + " has " + row["ListingCount"].ToString() + " listings";
                        pieListingByCat.Series["chartSeries"].Points.Add(dp);
                    }

                    pieListingByCat.Series["chartSeries"]["PointWidth"] = "0.1";
                    pieListingByCat.Series["chartSeries"]["DrawingStyle"] = "Cylinder";
                    pieListingByCat.Series["chartSeries"]["PieLabelStyle"] = "Outside";
                    pieListingByCat.Series["chartSeries"].Font = new Font("Arial", 12, FontStyle.Bold);

                    //pieListingByCat.ChartAreas["MainArea"].InnerPlotPosition.Width = 40;
                    //pieListingByCat.ChartAreas["MainArea"].InnerPlotPosition.Height = 90;
                    //pieListingByCat.ChartAreas["MainArea"].InnerPlotPosition.X = 25;
                    //pieListingByCat.ChartAreas["MainArea"].InnerPlotPosition.Y = 1;

                    // Set Up chart legend
                    rptChartLegend.DataSource = AdminDb.GetListingCountByCategory();
                    rptChartLegend.DataBind();
                }
            }
        }
    }
}