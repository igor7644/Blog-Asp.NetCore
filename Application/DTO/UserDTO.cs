using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public IEnumerable<PostDTO> Posts { get; set; }
        public IEnumerable<UserDTO> Users { get; set; }
    }
}
