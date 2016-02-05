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
    public partial class Listings1 : System.Web.UI.Page
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
                Session["SortExpr"] = "ListingID";
                BindDataToGridView();
            }
        }

        protected void BindDataToGridView()
        {
            //get data from database
            DataTable dt = AdminDb.GetAllListings( tbFindListing.Text, ddActiveFilter.SelectedValue);
            //cast to dataview for sorting and filtering
            DataView dv = (DataView)dt.DefaultView;
            //sort by session expr and direction
            dv.Sort = Session["SortExpr"].ToString() + Session["SortDirection"].ToString();
            //rebind with new sorted data
            gvListings.DataSource = dv;
            gvListings.DataBind();
        }

        protected void gvListings_Sorting(object sender, GridViewSortEventArgs e)
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

        protected void gvListings_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditListing")
            {
                int listingID = Convert.ToInt32(e.CommandArgument);
                DataTable listingTable = AdminDb.GetListingByID(listingID);
                DataRow row = listingTable.Rows[0];
                lbListingId.Text = listingID.ToString();
                tbHeadline.Text = row["Headline"].ToString();
                tbLocation.Text = row["Location"].ToString();
                lbUserID.Text = row["UserID"].ToString();
                tbPrice.Text = row["Price"].ToString();
                tbDescription.Text = row["Description"].ToString();
                cbIsActive.Checked = Convert.ToBoolean(row["IsActive"]);

                //Bind the categories dd
                ddCategory.DataSource = AdminDb.GetCategories();
                ddCategory.DataTextField = "CategoryName";
                ddCategory.DataValueField = "CategoryID";
                ddCategory.DataBind();

                ddCategory.SelectedValue = row["CategoryID"].ToString();

                int catID = Convert.ToInt32(row["CategoryID"].ToString());

                //Bind the state dropdown list
                ddState.DataSource = AdminDb.GetStates();
                ddState.DataTextField = "Statename";
                ddState.DataValueField = "StateID";
                ddState.DataBind();

                int stateId = Convert.ToInt32(row["StateID"]);

                ddState.SelectedValue = row["StateID"].ToString();

                //Bind the edit city drop down list 
                ddCity.DataSource = AdminDb.GetCitiesByState(stateId);
                ddCity.DataTextField = "CityName";
                ddCity.DataValueField = "CityID";
                ddCity.DataBind();

                ddCity.SelectedValue = row["CityID"].ToString();

                //Bind the edit subcategory drop down list
                ddSubCategory.DataSource = AdminDb.GetSubcategoriesByCategory(catID);
                ddSubCategory.DataTextField = "SubCategoryName";
                ddSubCategory.DataValueField = "SubCategoryID";
                ddSubCategory.DataBind();

                ddSubCategory.SelectedValue = row["SubCategoryID"].ToString();

                //make modal visible and backdrop
                pnlListingEdit.Visible = true;
                pnlBackdrop.Visible = true;

            }
            else if (e.CommandName == "DeleteListing")
            {
                int listingID = Convert.ToInt32(e.CommandArgument);
                AdminDb.DeleteListing(listingID);
                BindDataToGridView();
            }
        }

        protected void gvListings_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvListings.PageIndex = e.NewPageIndex;
            BindDataToGridView();
        }

        protected void gvListings_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void ddState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddState != null && ddState.SelectedIndex > 0 && ddCity != null)
            {
                int selStateId = Convert.ToInt32(ddState.SelectedValue);
                ddCity.DataSource = AdminDb.GetCitiesByState(selStateId);
                ddCity.DataTextField = "CityName";
                ddCity.DataValueField = "CityID";
                ddCity.DataBind();
            }
        }

        protected void ddCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddCategory != null && ddCategory.SelectedIndex > 0 && ddSubCategory != null)
            {
                int selCategoryId = Convert.ToInt32(ddCategory.SelectedValue);
                ddSubCategory.DataSource = AdminDb.GetSubcategoriesByCategory(selCategoryId);
                ddSubCategory.DataTextField = "SubCategoryName";
                ddSubCategory.DataValueField = "SubCategoryID";
                ddSubCategory.DataBind();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //clear form
            lbListingId.Text = "";
            tbHeadline.Text = "";
            tbLocation.Text = "";
            lbUserID.Text = "";
            tbPrice.Text = "";
            tbDescription.Text = "";
            cbIsActive.Checked = false; 

            pnlListingEdit.Visible = false;
            pnlBackdrop.Visible = false; 
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AdminDb.UpdateListing(
                Convert.ToInt32(lbListingId.Text),
                Convert.ToInt32(ddCity.SelectedValue),
                Convert.ToInt32(ddSubCategory.SelectedValue),
                tbHeadline.Text,
                tbLocation.Text,
                tbDescription.Text,
                Convert.ToDouble(tbPrice.Text),
                cbIsActive.Checked
                );

            //clear form
            lbListingId.Text = "";
            tbHeadline.Text = "";
            tbLocation.Text = "";
            lbUserID.Text = "";
            tbPrice.Text = "";
            tbDescription.Text = "";
            cbIsActive.Checked = false;

            BindDataToGridView();

            pnlListingEdit.Visible = false;
            pnlBackdrop.Visible = false;
        }

        protected void ddActiveFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindDataToGridView();
        }

        protected void btnsearchListing_Click(object sender, EventArgs e)
        {
            BindDataToGridView();
        }

        protected string getIsActiveIcon(bool isActive)
        {
           return isActive? "<img src='Images/greenCheck.png'/>" : "";
        }
    }
}