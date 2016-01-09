﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using CoreysList.Entity;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoreysList.Web.Models
{
    public class UserHomeViewModel
    {
        #region Constructors

        //constructor for the model
        public UserHomeViewModel(int tabId = 0)
        {
            CoreysListEntities Db = new CoreysListEntities();
            if(System.Web.HttpContext.Current.Session["UserId"] != null)
            {
                int userId = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"]);
                User user = Db.Users.FirstOrDefault(u => u.UserID == userId);
                UserName = user.FirstName + " " + user.LastName;
                AllUserListings = user.Listings.ToList();
                ActiveUserListings = user.Listings.Where(u => u.IsActive == true).ToList();
                InactiveUserListings = user.Listings.Where(u => u.IsActive == false).ToList();

                TabId = tabId;
            }
        }

        #endregion

        #region Properties

        public string UserName { get; set; }
        public int TabId { get; set; }
        public List<Listing> AllUserListings { get; set; }
        public List<Listing> ActiveUserListings { get; set; }
        public List<Listing> InactiveUserListings { get; set; }
 
        #endregion

    }

    
}