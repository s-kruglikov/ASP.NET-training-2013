using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcBlog.WebUI.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Please enter role name")]
        public string RoleName { get; set; }
    }
}