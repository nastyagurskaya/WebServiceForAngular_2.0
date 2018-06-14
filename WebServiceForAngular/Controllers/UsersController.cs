using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly ClaimsPrincipal _caller;
        public UsersController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _context = context;
        }
        //[HttpGet]
        //public ActionResult<List<User>> GetAll()
        //{
        //    return _context.User.ToList();
        //}

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
        [HttpGet("posts")]
        public async Task<List<Post>> GetPosts()
        {
            var userId = _caller.Claims.Single(c => c.Type == "id");
            var user = await _context.User.Include(c => c.Identity).SingleAsync(c => c.Identity.Id == userId.Value);
            int usId = user.Id;
            var posts = await _context.Post.Where(p => p.UserId == usId).ToListAsync();
            //return new OkObjectResult(new
            //{
            //   posts
            //});
            return posts;
        }
        //[HttpGet("posts", Name = "Posts")]
        //public ActionResult<List<Post>> GetAll()
        //{
        //    var userId = _caller.Claims.Single(c => c.Type == "id");
        //    var user = _context.User.Include(c => c.Identity).SingleAsync(c => c.Identity.Id == userId.Value);
        //    var posts = _context.Post.Where(p => p.UserId == user.Id).ToList();
        //    return posts;
        //}
        [HttpGet]
        public async Task<IActionResult> Home()
        {
            // retrieve the user info
            //HttpContext.User
            var userId = _caller.Claims.Single(c => c.Type == "id");
            var user = await _context.User.Include(c => c.Identity).SingleAsync(c => c.Identity.Id == userId.Value);

            return new OkObjectResult(new
            {
                user.Phone,
                user.Name,
                user.Identity.FirstName,
                user.Identity.LastName,
   
            });
        }

    }
    
}
