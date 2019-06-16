using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "First Name is required!")]
        [MinLength(3, ErrorMessage = "First Name must have at least 3 characters!")]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "Last Name is required!")]
        [MinLength(3, ErrorMessage = "Last Name must have at least 5 characters!")]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "Username is required!")]
        [MinLength(3, ErrorMessage = "Username must have at least 3 characters!")]
        public string Username { get; set; }

        public IEnumerable<PostDTO> Posts { get; set; }
    }
}
