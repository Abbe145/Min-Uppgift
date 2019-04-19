using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication6.ViewModels.User
{
 
        public class UserIndexViewModel
        {
            public UserIndexViewModel()
            {
                Users = new List<UserListViewModel>();
            }

            public class UserListViewModel
            {
                public string UserId { get; set; }
                public string UserRoles { get; set; }
                public string Email { get; set; }
                public string Password { get; set; }
                public string ConfirmPassword { get; set; }
                public string UserName { get; set; }

            }

        public string UserRoles { get; set; }
        public List<UserListViewModel> Users { get; set; }
        }
    }

