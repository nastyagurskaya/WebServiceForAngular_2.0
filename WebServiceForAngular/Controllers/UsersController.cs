using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebServiceForAngular.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebServiceForAngular.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "ApiUser")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;

        }
        [HttpGet]
        public ActionResult<List<User>> GetAll()
        {
            return _context.User.ToList();
        }

        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<User> GetById(int id)
        {
            var item = _context.User.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }
        [HttpGet("{id}/posts")]
        public ActionResult<List<Post>> GetPosts(int id)
        {
            var posts = _context.Post;
            var po = posts.Where(p => p.UserId == id);
            return po.ToList();
        }

    }
    
}
