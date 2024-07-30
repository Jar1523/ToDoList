using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ToDoList.Entities;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ListContext _context;
            public UserController(ListContext myAPIContext)
        {
            _context = myAPIContext;
        }

        public class userReq
        {
            public string Password { get; set; }
            public string Email { get; set; }
        }

        [HttpPost("Create")]
        public IActionResult Create(userReq userReq)
        {
            var listAllUsers = _context.Users.ToList();

            var uesrDup = listAllUsers.FirstOrDefault(user => user.Email == userReq.Email);

            if (uesrDup != null)
            {
                throw new NotImplementedException("Deplicate Email");
            }

            var newUser = new User()
            {
                Password = userReq.Password,
                Email = userReq.Email,
            };

            _context.Add(newUser);
            _context.SaveChanges();

            return Ok(newUser);
        }


        [HttpPost("Login")]
        public IActionResult Login(userReq userReq)
        {
            var listAllUsers = _context.Users.ToList();

            var user = listAllUsers.FirstOrDefault(user => user.Email == userReq.Email);

            if (user == null)
            {
                throw new NotImplementedException("No email");

                // way ok but display error
                //return Ok(new { ErrorMessage = "No email" });
            }

            if (user.Password != userReq.Password)
            {
                throw new NotImplementedException("Invalid");
            }



            return Ok(new {ID = user.Id, Email = user.Email });
        }



    }
}
