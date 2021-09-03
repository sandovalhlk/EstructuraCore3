using System;

namespace AppGlobal.Core.Entidades
{
    public class Post
    {
        public int PostId { get; set; }

        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public String Description { get; set; }
        public String Image { get; set; }
        
    }
}
