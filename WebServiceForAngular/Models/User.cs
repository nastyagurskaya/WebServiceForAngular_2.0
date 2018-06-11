using System;
using System.Collections.Generic;

namespace WebServiceForAngular.Models
{
    public partial class User
    {
        public User()
        {
            Post = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public ICollection<Post> Post { get; set; }
        public string IdentityId { get; set; }
        public AppUser Identity { get; set; }
    }
}
