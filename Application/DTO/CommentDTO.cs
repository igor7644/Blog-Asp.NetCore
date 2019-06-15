using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Comment is required!")]
        [MinLength(3, ErrorMessage = "Comment must have at least 3 characters!")]
        public string CommentText { get; set; }
    }
}
