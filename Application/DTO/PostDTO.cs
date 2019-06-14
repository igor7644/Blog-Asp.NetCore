using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Business.DTO
{
    public class PostDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required!")]
        [MinLength(3, ErrorMessage = "Title must have at least 3 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required!")]
        public string Description { get; set; }
        
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public UserDTO User { get; set; }
        public CategoryDTO Category { get; set; }
        public IEnumerable<CommentDTO> Comments { get; set; }
    }
}
