using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using CoreysList.Admin.DbConnections;

namespace CoreysList.Admin
{
    public partial class Cities : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("Default.aspx");
                }

                Session["SortDirection"] = " ASC";
                Session["SortExpr"] = "CityID";
                BindDataToGridView();
            }
        }

        protected void BindDataToGridView()
        {
            //get data from database
            DataTable dt = AdminDb.GetAllCities( tbFindCity.Text, ddMajorFilter.SelectedValue);
            //cast to dataview for sorting and filtering
            DataView dv = (DataView)dt.DefaultView;
            //sort by session expr and direction
            dv.Sort = Session["SortExpr"].ToString() + Session["SortDirection"].ToString();
            //rebind with new sorted data
            gvCities.DataSource = dv;
            gvCities.DataBind();
        }

        protected void gvCities_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow )
            {
                DataRowView row = (DataRowView)e.Row.DataItem;

                //Bind the categories dd
                Label lbMajorCityImg = (Label)e.Row.FindControl("lbMajorCityImg");
                if (Convert.ToBoolean(row["MajorCity"]))
                {
                    lbMajorCityImg.Text = "<img src='Images/greenCheck.png'/>";
                }
            }
        }

        protected void gvCities_Sorting(object sender, GridViewSortEventArgs e)
        {
            // if different col is sorted set session sort type to ascending
            if (Session["SortExpr"].ToString() != e.SortExpression)
            {
                Session["SortDirection"] = " ASC";
            }
            else // if same col toggle sort direction the set the sort type opposite
            {
                Session["SortDirection"] = (Session["SortDirection"].ToString() == " ASC" ? " DESC" : " ASC");
            }
            //then set which col is to be sorted
            Session["SortExpr"] = e.SortExpression;
            //rebind the data
            BindDataToGridView();
        }

        protected void gvCities_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditCity")
            {
                int cityID = Convert.ToInt32(e.CommandArgument);
                DataTable cityTable = AdminDb.GetCityByID(cityID);
                DataRow row = cityTable.Rows[0];
                lbCityID.Text = cityID.ToString();
                lbCityName.Text = row["CityName"].ToString();
                lbStateID.Text = row["StateID"].ToString();
                lbStateName.Text = row["StateName"].ToString();
                cbMajorCity.Checked = Convert.ToBoolean(row["MajorCity"]);

                //make modal visible and backdrop
                pnlCityEdit.Visible = true;
                pnlBackdrop.Visible = true;
            }
            else if (e.CommandName == "DeleteCity")
            {
                int cityID = Convert.ToInt32(e.CommandArgument);
                AdminDb.DeleteListing(cityID);
                BindDataToGridView();
            }
        }

        protected void gvCities_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCities.PageIndex = e.NewPageIndex;
            BindDataToGridView();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AdminDb.UpdateCity( Convert.ToInt32( lbCityID.Text ), cbMajorCity.Checked );

            //clear form
            lbCityID.Text = "";
            lbCityName.Text = "";
            lbStateID.Text = "";
            lbStateName.Text = "";
            cbMajorCity.Checked = false;

            BindDataToGridView();

            pnlCityEdit.Visible = false;
            pnlBackdrop.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //clear form
            lbCityID.Text = "";
            lbCityName.Text = "";
            lbStateID.Text = "";
            lbStateName.Text = "";
            cbMajorCity.Checked = false;

            pnlCityEdit.Visible = false;
            pnlBackdrop.Visible = false;
        }

        protected void ddMajorFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnsearchCity_Click(object sender, EventArgs e)
        {
            BindDataToGridView();
        }

        protected void imgAddCity_Click(object sender, ImageClickEventArgs e)
        {
            //Bind the state dropdown list
            ddSelectNewState.DataSource = AdminDb.GetStates();
            ddSelectNewState.DataTextField = "Statename";
            ddSelectNewState.DataValueField = "StateID";
            ddSelectNewState.DataBind();

            pnlAddCity.Visible = true;
            pnlBackdrop.Visible = true;
        }

        protected void btnAddCity_Click(object sender, EventArgs e)
        {           
            AdminDb.AddCity( Convert.ToInt32(ddSelectNewState.SelectedValue), tbNewCityName.Text, cbNewMajorCity.Checked, Session["UserName"].ToString() );
            pnlAddCity.Visible = false;
            pnlBackdrop.Visible = false;
            BindDataToGridView();
        }

        protected void btnCancelCityAdd_Click(object sender, EventArgs e)
        {           
            tbNewCityName.Text = "";
            cbNewMajorCity.Checked = false;

            pnlAddCity.Visible = false;
            pnlBackdrop.Visible = false; 
        }
    }
}