using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebApplication6.ViewModels.User
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "The Name field is required.")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "The Name field is required.")]
        public string UserRoles { get; set; }
        [Required(ErrorMessage = "The Name field is required.")]
        [EmailAddress(ErrorMessage = "You need to fill in a email address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The Name field is required.")]
        public string UserName { get; set; }

        public string UserDropDownHolder { get; set; }
        public List<SelectListItem> UserDropDownList { get; set; }
    }
}