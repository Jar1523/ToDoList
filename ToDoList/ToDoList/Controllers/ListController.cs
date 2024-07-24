using Microsoft.AspNetCore.Mvc;
using ToDoList.Entities;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListController : ControllerBase
    {
        private readonly ListContext _context;
        public ListController(ListContext myAPIContext)
        {
            _context = myAPIContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var queryAllUsers = _context.Users;

            throw new NotImplementedException("Error 1234");

            return Ok(queryAllUsers.ToList());
        }

        [HttpGet("getUserById/{id}")]
        public IActionResult GetUserById(int id)
        {
            var queryUserById = _context.Users.FirstOrDefault(user => user.Id == id);

            if (queryUserById == null)
            {
                throw new NotImplementedException("user not found");
            }

            return Ok(queryUserById);
        }

        public class userReq
        {
            public string UserName { get; set; }
        }
        [HttpPost("CreateUser")]
        public IActionResult Create(userReq userReq)
        {
            //todo
            //get all
            //check duplicate
            //if dup send error




            var newUser = new User()
            {
                UserName = userReq.UserName,
            };

            _context.Add(newUser);
            _context.SaveChanges();

            return Ok(newUser);
        }

        public class taskReq
        {
            public int UserId { get; set; }
            public string TaskName { get; set; }
            public string Description { get; set; }
        }
        [HttpPost("CreateTask")]
        public IActionResult CreateTask(taskReq taskReq)
        {
            var userById = _context.Users.FirstOrDefault(user => user.Id == taskReq.UserId);
            if (userById == null)
            {
                throw new NotImplementedException("user not found");
            }

            var newTask = new TaskList();
            newTask.TaskName = taskReq.TaskName;
            newTask.Description = taskReq.Description;
            newTask.UserId = taskReq.UserId;

            _context.Add(newTask);
            _context.SaveChanges();


            return Ok();
        }
        public class taskUpdate
        {
            public int UserId { get; set; }
            public string TaskName { get; set; }
            public string Description { get; set; }
            public int TaskId { get; set; }
        }
        [HttpPost("UpdateTask")]
        public IActionResult UpdateTask(taskUpdate taskUpdate)
        {
            var userById = _context.Users.FirstOrDefault(user => user.Id == taskUpdate.UserId);
            if (userById == null)
            {
                throw new NotImplementedException("user not found");
            }
            var taskByUserId = _context.TaskLists.Where(task => task.Id == taskUpdate.TaskId)
                                                 .Where(task => task.UserId == userById.Id)
                                                 .FirstOrDefault();


            taskByUserId.TaskName = taskUpdate.TaskName;
            taskByUserId.Description = taskUpdate.Description;

            _context.Update(taskByUserId);
            _context.SaveChanges();



            return Ok();
        }
        [HttpDelete("DeleteTaskById/{TaskId}")]
        public IActionResult DeleteTaskById(int TaskId)
        {
            var queryTaskById = _context.TaskLists.FirstOrDefault(task => task.Id == TaskId);

            if (queryTaskById == null)
            {
                throw new NotImplementedException("Task not found");
            }
            _context.Remove(queryTaskById);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet("GetAllTaskByUserId/{UserId}")]
        public IActionResult GetAllTaskByUserId(int UserId)
        {
            var userById = _context.Users.FirstOrDefault(user => user.Id == UserId);
            if (userById == null)
            {
                throw new NotImplementedException("user not found");
            }
            var GetAllTaskByUserId = _context.TaskLists.Where(task => task.UserId == UserId).ToList();

          
            return Ok(GetAllTaskByUserId);
        }


    }
}
