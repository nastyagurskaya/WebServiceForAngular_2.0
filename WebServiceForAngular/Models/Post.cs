using System;
using System.Collections.Generic;

namespace WebServiceForAngular.Models
{
    public partial class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int? UserId { get; set; }

        public User User { get; set; }
    }
}
