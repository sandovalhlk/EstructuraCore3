using System;
using System.Collections.Generic;

namespace AppGlobal.Core.Entidades
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }

        public virtual Post post { get; set; }
        public virtual User user { get; set; }
    }
}
