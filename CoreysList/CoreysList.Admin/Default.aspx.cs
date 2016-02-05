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
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                pnlLoginErrorMessage.Visible = false; 
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            SqlDataReader dr = AdminDb.GetAdminLogin(tbEmail.Text, tbPassword.Text);
            if (dr.Read())
            {
                Session["UserID"] = dr["UserID"].ToString();
                Session["UserName"] = dr["FullName"].ToString();
                Response.Redirect("Dashboard.aspx");
            }
            else
            {
                tbEmail.Text = "";
                tbPassword.Text = "";
                pnlLoginErrorMessage.Visible = true; 
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            tbEmail.Text = "";
            tbPassword.Text = "";
        }
    }
}