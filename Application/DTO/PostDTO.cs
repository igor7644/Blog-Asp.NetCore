using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTO
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public Category Category { get; set; }
    }
}
