using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Comment : BaseEntity
    {
        public string CommentText { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
