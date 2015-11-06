using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class CommentModel
    {
        [Required(ErrorMessage = "Field is required!")]
        [StringLength(20, ErrorMessage = "LOGIN LENGTH should be between 4 and 20 characters",MinimumLength = 4)]
        public string Author { get; set; }


        [Required(ErrorMessage = "Field is required!")]
        [StringLength(200,ErrorMessage = "Comment should be up to 200 characters")]
        public string Text { get; set; }
    }
}