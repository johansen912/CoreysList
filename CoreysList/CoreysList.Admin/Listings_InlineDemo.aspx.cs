using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreysList.Admin.DbConnections;
using System.Data;
using System.Data.SqlClient;

namespace CoreysList.Admin
{
    public partial class Listings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.Master.Page.Title = "Listings";
                BindDataToGridView();
            }

        }

        protected void gvListings_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
        }

        protected void gvListings_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvListings.EditIndex = e.NewEditIndex;
            BindDataToGridView();
        }

        protected void gvListings_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvListings.EditIndex = -1;
            BindDataToGridView();
        }

        protected void gvListings_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)gvListings.Rows[e.RowIndex];

            int CityId = Convert.ToInt32(((DropDownList)gvRow.FindControl("ddCity")).SelectedValue );

            //String ListingID = gvRow.Cells[0].Text;
            //String UserID = gvRow.Cells[1].Text;
            //String CityID = gvRow.Cells[2].Text;
            //String SubCategoryID = gvRow.Cells[3].Text;
            //String Headline = gvRow.Cells[4].Text;
            //String Location = gvRow.Cells[5].Text;
            //String Description = gvRow.Cells[6].Text;
            //String Price = gvRow.Cells[7].Text;

            //AdminDb.UpdateListing(ListingID, UserID, CityID, SubCategoryID, Headline, Location, Description, Price);
        }

        protected void BindDataToGridView()
        {
            //gvListings.DataSource = AdminDb.GetAllListings();
            //gvListings.DataBind();
        }

        protected void gvListings_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && ((e.Row.RowState & DataControlRowState.Edit) > 0))
            {
                DataRowView Row = (DataRowView)e.Row.DataItem;
                
                //Bind the categories dd
                DropDownList ddCategory = (DropDownList)e.Row.FindControl("ddCategory");
                ddCategory.DataSource = AdminDb.GetCategories();
                ddCategory.DataTextField = "CategoryName";
                ddCategory.DataValueField = "CategoryID";
                ddCategory.DataBind();

                ddCategory.SelectedValue = Row["CategoryID"].ToString();
                //Bind the state dropdown list
                DropDownList ddState = (DropDownList)e.Row.FindControl("ddState");

                ddState.DataSource = AdminDb.GetStates();
                ddState.DataTextField = "Statename";
                ddState.DataValueField = "StateID";
                ddState.DataBind();

                int stateId = Convert.ToInt32(Row["StateID"]);

                ddState.SelectedValue = Row["StateID"].ToString();
                //Bind the edit city drop down list 
                DropDownList ddCity = (DropDownList)e.Row.FindControl("ddCity");

                ddCity.DataSource = AdminDb.GetCitiesByState(stateId);
                ddCity.DataTextField = "CityName";
                ddCity.DataValueField = "CityID";
                ddCity.DataBind();

                ddCity.SelectedValue = Row["CityID"].ToString();

                //Bind the edit subcategory drop down list
                int CategoryID = Convert.ToInt32(Row["CategoryID"]);
                DropDownList ddSubcategory = (DropDownList)e.Row.FindControl("ddSubcategory");

                ddSubcategory.DataSource = AdminDb.GetSubcategoriesByCategory(CategoryID);
                ddSubcategory.DataTextField = "SubCategoryName";
                ddSubcategory.DataValueField = "SubCategoryID";
                ddSubcategory.DataBind();

                ddSubcategory.SelectedValue = Row["SubCategoryID"].ToString();

            }
        }

        protected void ddState_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddState = (DropDownList)sender;
            GridViewRow currentRow = (GridViewRow)ddState.NamingContainer;

            DropDownList ddCity = (DropDownList)currentRow.FindControl("ddCity");

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
            DropDownList ddCategory = (DropDownList)sender;
            GridViewRow currentRow = (GridViewRow)ddCategory.NamingContainer;

            DropDownList ddSubcategory = (DropDownList)currentRow.FindControl("ddSubcategory");

            if (ddCategory != null && ddCategory.SelectedIndex > 0 && ddSubcategory != null)
            {
                int selCategoryId = Convert.ToInt32(ddCategory.SelectedValue);
                ddSubcategory.DataSource = AdminDb.GetSubcategoriesByCategory(selCategoryId);
                ddSubcategory.DataTextField = "SubCategoryName";
                ddSubcategory.DataValueField = "SubCategoryID";
                ddSubcategory.DataBind();
            }
        }
    }
}