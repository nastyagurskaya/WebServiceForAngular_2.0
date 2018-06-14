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
    public class PostsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ClaimsPrincipal _caller;
        public PostsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _context = context;

        }
        //[HttpGet]
        //public async Task<List<Post>> GetAll()
        //{
        //    return await _context.Post.ToListAsync();
        //}
       private int GetId()
        {
            var userId = _caller.Claims.Single(c => c.Type == "id");
            var user = _context.User.Include(c => c.Identity).SingleAsync(c => c.Identity.Id == userId.Value);
            return user.Id;
        }
        [HttpGet]
        public async Task<List<Post>> Get()
        {
            //var userId = _caller.Claims.Single(c => c.Type == "id");
            //var user = await _context.User.Include(c => c.Identity).SingleAsync(c => c.Identity.Id == userId.Value);
            //int usId = user.Id;
            //GetId();
            var user = await _context.User.FindAsync(5);
            var posts = await _context.Post.ToListAsync();
            ////var user = await _context.User.Include(c => c.Identity).SingleAsync(c => c.Identity.Id == id);
            ////var posts = _context.Post.Where(p => p.UserId == user.Id).ToListAsync();
            //return await _context.Post.Where(p => p.UserId == id).ToListAsync();
            return posts;
        }
    }

}
