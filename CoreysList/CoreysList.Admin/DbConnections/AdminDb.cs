using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;





namespace CoreysList.Admin.DbConnections
{
    public class AdminDb
    {

        #region USER FUNCTIONS

                public static DataTable GetAllUsers( string searchTerm, string activeTerm)
                {
                    SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreysListEntities"].ConnectionString);
                    SqlDataAdapter myCommand = new SqlDataAdapter("spGetUsers", dbConnection);

                    myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.SelectCommand.Parameters.Add("@searchTerm", SqlDbType.VarChar,50).Value = searchTerm;
                    myCommand.SelectCommand.Parameters.Add("@activeTerm", SqlDbType.VarChar, 50).Value = activeTerm;

                    DataSet ds = new DataSet();
                    myCommand.Fill(ds);
                    dbConnection.Close();

                    return ds.Tables[0]; ;
                }

                public static DataTable GetUserByID( int UserID )
                {
                    SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreysListEntities"].ConnectionString);
                    SqlDataAdapter myCommand = new SqlDataAdapter("spGetUserByID", dbConnection);

                    myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.SelectCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;

                    DataTable dt = new DataTable();
                    myCommand.Fill(dt);
                    dbConnection.Close();

                    return dt;
                }

                public static void UpdateUser(int UserID, string FirstName, string LastName, string Email, 
                    string PhoneNum,  bool Admin, bool Active)
                {

                    SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreysListEntities"].ConnectionString);
                    SqlCommand myCommand = new SqlCommand("spUpdateUser", myConnection);

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                    myCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = FirstName;
                    myCommand.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = LastName;
                    myCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email;
                    myCommand.Parameters.Add("@PhoneNum", SqlDbType.NVarChar).Value = PhoneNum;
                    myCommand.Parameters.Add("@Admin", SqlDbType.Bit).Value = Admin;
                    myCommand.Parameters.Add("@Active", SqlDbType.Bit).Value = Active;

                    myConnection.Open();
                    myCommand.ExecuteNonQuery();
                    myConnection.Close();
                }

                public static void DeleteUser(int UserID)
                {

                    SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreysListEntities"].ConnectionString);
                    SqlCommand myCommand = new SqlCommand("spDeleteUser", myConnection);

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;

                    myConnection.Open();
                    myCommand.ExecuteNonQuery();
                    myConnection.Close();
                }

                //public static SqlDataReader GetAllUsers2()
                //{
                //    SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreysListEntities"].ConnectionString);
                //    SqlCommand myCommand = new SqlCommand("spGetUsers", myConnection);

                //    myCommand.CommandType = CommandType.StoredProcedure;

                //    myConnection.Open();
                //    SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                //    return result;
                //}


        #endregion

        #region LISTING FUNCTIONS

                public static DataTable GetAllListings(string searchTerm, string activeTerm)
                {
                    SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreysListEntities"].ConnectionString);
                    SqlDataAdapter myCommand = new SqlDataAdapter("spGetListings", dbConnection);

                    myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.SelectCommand.Parameters.Add("@searchTerm", SqlDbType.VarChar).Value = searchTerm;
                    myCommand.SelectCommand.Parameters.Add("@activeTerm", SqlDbType.VarChar).Value = activeTerm;

                    DataSet ds = new DataSet();
                    myCommand.Fill(ds);
                    dbConnection.Close();

                    return ds.Tables[0]; ;
                }

                public static DataTable GetListingByID(int ListingID)
                {
                    SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreysListEntities"].ConnectionString);
                    SqlDataAdapter myCommand = new SqlDataAdapter("spGetListingByID", dbConnection);

                    myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.SelectCommand.Parameters.Add("@ListingID", SqlDbType.Int).Value = ListingID;

                    DataTable dt = new DataTable();
                    myCommand.Fill(dt);
                    dbConnection.Close();

                    return dt;
                }

                public static void UpdateListing(int ListingID, int CityID, int SubCategoryID, string Headline,
                                                        string Location, string Description, double Price, bool IsActive)
                {

                    SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreysListEntities"].ConnectionString);
                    SqlCommand myCommand = new SqlCommand("spUpdateListing", myConnection);

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.Add("@ListingID", SqlDbType.Int).Value = ListingID;
                    myCommand.Parameters.Add("@CityID", SqlDbType.Int).Value = CityID;
                    myCommand.Parameters.Add("@SubCategoryID", SqlDbType.Int).Value = SubCategoryID;
                    myCommand.Parameters.Add("@Headline", SqlDbType.NVarChar).Value = Headline;
                    myCommand.Parameters.Add("@Location", SqlDbType.NVarChar).Value = Location;
                    myCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = Description;
                    myCommand.Parameters.Add("@Price", SqlDbType.Money).Value = Price;
                    myCommand.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;

                    myConnection.Open();
                    myCommand.ExecuteNonQuery();
                    myConnection.Close();
                }

                public static void DeleteListing(int ListingID)
                {

                    SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreysListEntities"].ConnectionString);
                    SqlCommand myCommand = new SqlCommand("spDeleteListing", myConnection);

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.Add("@ListingID", SqlDbType.Int).Value = ListingID;

                    myConnection.Open();
                    myCommand.ExecuteNonQuery();
                    myConnection.Close();
                }


                public static DataTable GetSubcategoriesByCategory(int categoryId)
                {
                    SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreysListEntities"].ConnectionString);
                    SqlDataAdapter myCommand = new SqlDataAdapter("spGetSubcategoriesByCategory", dbConnection);

                    myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = categoryId;

                    DataTable dt = new DataTable();
                    myCommand.Fill(dt);
                    dbConnection.Close();

                    return dt;
                }

                public static DataTable GetStates()
                {
                    SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreysListEntities"].ConnectionString);
                    SqlDataAdapter myCommand = new SqlDataAdapter("spGetStates", dbConnection);

                    myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    myCommand.Fill(dt);
                    dbConnection.Close();

                    return dt;
                }

                public static DataTable GetCitiesByState(int stateId)
                {
                    SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreysListEntities"].ConnectionString);
                    SqlDataAdapter myCommand = new SqlDataAdapter("spGetCitiesByState", dbConnection);

                    myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.SelectCommand.Parameters.Add("@StateId", SqlDbType.Int).Value = stateId;

                    DataTable dt = new DataTable();
                    myCommand.Fill(dt);
                    dbConnection.Close();

                    return dt;
                }

                public static DataTable GetCategories()
                {
                    SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreysListEntities"].ConnectionString);
                    SqlDataAdapter myCommand = new SqlDataAdapter("spCategories", dbConnection);

                    myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    myCommand.Fill(dt);
                    dbConnection.Close();

                    return dt;
                }

        #endregion

        #region DEFAULT FUNCTIONS

                //Check if admin password and email match, if so return the admins name and ID
                public static SqlDataReader GetAdminLogin( string email, string password)
                {
                    SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreysListEntities"].ConnectionString);
                    SqlCommand myCommand = new SqlCommand("spIsAdmin", myConnection);

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
                    myCommand.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;

                    myConnection.Open();
                    SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    return result;
                }

        #endregion

        #region DASHBOARD FUNCTIONS

                //Get Count of Users
                public static SqlDataReader GetUserCount()
                {
                    SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreysListEntities"].ConnectionString);
                    SqlCommand myCommand = new SqlCommand("spUserCount", myConnection);

                    myCommand.CommandType = CommandType.StoredProcedure;

                    myConnection.Open();
                    SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    return result;
                }

                //Get Count of Users signed up last 7 days
                public static SqlDataReader GetSevendayUserCount()
                {
                    SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreysListEntities"].ConnectionString);
                    SqlCommand myCommand = new SqlCommand("spSevendayUserCount", myConnection);

                    myCommand.CommandType = CommandType.StoredProcedure;

                    myConnection.Open();
                    SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    return result;
                }
                //Get Count of Users signed up last 30 days
                public static SqlDataReader GetThirtydayUserCount()
                {
                    SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreysListEntities"].ConnectionString);
                    SqlCommand myCommand = new SqlCommand("spThirtydayUserCount", myConnection);

                    myCommand.CommandType = CommandType.StoredProcedure;

                    myConnection.Open();
                    SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    return result;
                }
                //Get Count of listings posted
                public static SqlDataReader GetListingsCount()
                {
                    SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreysListEntities"].ConnectionString);
                    SqlCommand myCommand = new SqlCommand("spListingCount", myConnection);

                    myCommand.CommandType = CommandType.StoredProcedure;

                    myConnection.Open();
                    SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    return result;
                }
                //Get Count of listings posted in last seven days
                public static SqlDataReader GetSevendayListingsCount()
                {
                    SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreysListEntities"].ConnectionString);
                    SqlCommand myCommand = new SqlCommand("spSevendayListingCount", myConnection);

                    myCommand.CommandType = CommandType.StoredProcedure;

                    myConnection.Open();
                    SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    return result;
                }
                //Get Count of listings posted in last 30 days
                public static SqlDataReader GetThirtydayListingCount()
                {
                    SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreysListEntities"].ConnectionString);
                    SqlCommand myCommand = new SqlCommand("spThirtyDayListingCount", myConnection);

                    myCommand.CommandType = CommandType.StoredProcedure;

                    myConnection.Open();
                    SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    return result;
                }
                //Get Count of Listings by category
                public static DataTable GetListingCountByCategory()
                {
                    SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreysListEntities"].ConnectionString);
                    SqlDataAdapter myCommand = new SqlDataAdapter("spGetListingCountByCategory", dbConnection);

                    myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    myCommand.Fill(dt);
                    dbConnection.Close();

                    return dt;
                }
        #endregion

        #region City FUNCTIONS

             public static DataTable GetAllCities(string searchTerm, string majorTerm)
            {
                SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreysListEntities"].ConnectionString);
                SqlDataAdapter myCommand = new SqlDataAdapter("spGetCities", dbConnection);

                myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;
                myCommand.SelectCommand.Parameters.Add("@searchTerm", SqlDbType.VarChar, 50).Value = searchTerm;
                myCommand.SelectCommand.Parameters.Add("@majorTerm", SqlDbType.VarChar, 50).Value = majorTerm;

                DataSet ds = new DataSet();
                myCommand.Fill(ds);
                dbConnection.Close();

                return ds.Tables[0]; 
            }

            public static DataTable GetCityByID(int CityID)
            {
                SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreysListEntities"].ConnectionString);
                SqlDataAdapter myCommand = new SqlDataAdapter("spGetCityByID", dbConnection);

                myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;
                myCommand.SelectCommand.Parameters.Add("@CityID", SqlDbType.VarChar, 50).Value = CityID;

                DataSet ds = new DataSet();
                myCommand.Fill(ds);
                dbConnection.Close();

                return ds.Tables[0]; 
            }

            public static void UpdateCity(int CityID, bool MajorCity)
            {

                SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreysListEntities"].ConnectionString);
                SqlCommand myCommand = new SqlCommand("spUpdateCity", myConnection);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@CityID", SqlDbType.Int).Value = CityID;
                myCommand.Parameters.Add("@MajorCity", SqlDbType.Bit).Value = MajorCity;

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }

            public static void AddCity(int StateID, string CityName, bool MajorCity, string AdminName)
            {

                SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreysListEntities"].ConnectionString);
                SqlCommand myCommand = new SqlCommand("spAddCity", myConnection);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;
                myCommand.Parameters.Add("@CityName", SqlDbType.NVarChar).Value = CityName;
                myCommand.Parameters.Add("@MajorCity", SqlDbType.Bit).Value = MajorCity;
                myCommand.Parameters.Add("@AdminName", SqlDbType.NVarChar).Value = AdminName;

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        
        #endregion
    }
}