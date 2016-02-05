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
    public partial class Users : System.Web.UI.Page
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
                Session["SortExpr"] = "UserID"; 
                BindDataToGridView();
            }
        }

        protected void BindDataToGridView()
        {
            //get data from database
            DataTable dt = AdminDb.GetAllUsers( tbFindUser.Text, ddActiveFilter.SelectedValue );
            //cast to dataview for sorting and filtering
            DataView dv = (DataView)dt.DefaultView;
            //sort by session expr and direction
            dv.Sort = Session["SortExpr"].ToString() + Session["SortDirection"].ToString();
            //rebind with new sorted data
            gvUsers.DataSource = dv;
            gvUsers.DataBind();
        }

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvUsers_Sorting(object sender, GridViewSortEventArgs e)
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


        protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsers.PageIndex = e.NewPageIndex;
            BindDataToGridView();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AdminDb.UpdateUser(
                Convert.ToInt32(lbUserId.Text),
                tbFirstName.Text,
                tbLastName.Text,
                tbEmail.Text,
                tbPhoneNum.Text,
                cbIsAdmin.Checked,
                cbIsActive.Checked
                );

            //clear form
            lbUserId.Text = "";
            tbFirstName.Text = "";
            tbLastName.Text = "";
            tbEmail.Text = "";
            tbPhoneNum.Text = "";
            cbIsAdmin.Checked = false;
            cbIsActive.Checked = false;

            pnlUserEdit.Visible = false;
            pnlBackdrop.Visible = false; 

            BindDataToGridView();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //clear form
            lbUserId.Text = "";
            tbFirstName.Text = "";
            tbLastName.Text = "";
            tbEmail.Text = "";
            tbPhoneNum.Text = "";
            cbIsAdmin.Checked = false;
            cbIsActive.Checked = false;

            pnlUserEdit.Visible = false;
            pnlBackdrop.Visible = false; 
        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditUser")
            {
                int userID = Convert.ToInt32(e.CommandArgument);
                DataTable userTable = AdminDb.GetUserByID(userID);
                DataRow row = userTable.Rows[0];
                lbUserId.Text = userID.ToString();
                tbFirstName.Text = row["FirstName"].ToString();
                tbLastName.Text = row["LastName"].ToString();
                tbEmail.Text = row["Email"].ToString();
                tbPhoneNum.Text = row["PhoneNum"].ToString();
                cbIsActive.Checked = Convert.ToBoolean(row["Active"]);
                cbIsAdmin.Checked = Convert.ToBoolean(row["Admin"]);

                pnlUserEdit.Visible = true;
                pnlBackdrop.Visible = true; 
            }
            else if (e.CommandName == "DeleteUser")
            {
                int userID = Convert.ToInt32(e.CommandArgument);
                AdminDb.DeleteUser(userID);
                BindDataToGridView();
            }
        }

        protected void btnsearchUser_Click(object sender, EventArgs e)
        {
            BindDataToGridView();
        }

        protected string getIsAdminIcon(bool isActive)
        {
            return isActive ? "<img src='Images/greenCheck.png'/>" : "";
        }
        protected string getIsActiveIcon(bool isActive)
        {
            return isActive ? "<img src='Images/greenCheck.png'/>" : "";
        }
    }
}