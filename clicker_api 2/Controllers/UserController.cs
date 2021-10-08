using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clicker.database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace clicker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<User> _logger;

        private MyDbContext _dbContext;
        public UserController(ILogger<User> logger , MyDbContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        private static List<User> Users = new List<User>{};

        [HttpGet]
        public List<User> Get()
        {
            return _dbContext.users.ToList();
        }

        [HttpGet]
        [Route("Connect")]
        public User GetConnect(string email, string password)
        {
            List<User> ListUser = _dbContext.users.ToList();
            User result = ListUser.Find(
            delegate(User usr)
            {
                return usr.Email == email && usr.Password == password;
            });
            return result;
        }

        [HttpGet]
        [Route("Leaderboard")]
        public List<User> GetLeaderboard()
        {
            List<User> ListUser = _dbContext.users.ToList();
            ListUser = ListUser.OrderByDescending(x=>x.Score).Take(10).ToList();
            return ListUser;
        }

        [HttpGet]
        [Route("Load")]
        public User GetLoad(int id)
        {
            List<User> ListUser = _dbContext.users.ToList();
            User result = ListUser.Find(
            delegate(User usr)
            {
                return usr.Id == id;
            });
            return result;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult PostRegister(User inputuser)
        {
            User userToAdd = new User(){Email = inputuser.Email, Password = inputuser.Password, Score = 0,ClickPower = 1, TimerPower = 1, Playtime = 0, Token = ""};
            _dbContext.users.Add(userToAdd);
            _dbContext.SaveChanges();
            return Ok("User Succesfully created");
        }

        [HttpPut]
        [Route("Save")]
        public IActionResult UpdateSave(User inputuser)
        {
            _dbContext.users.Update(inputuser);
            _dbContext.SaveChanges();
            return Ok($"Succesfully Saved User {inputuser.Email}");
        }

        [HttpDelete]
        [Route("Delete/{iduser}")]
        public IActionResult DeleteAccount(int iduser)
        {
            List<User> ListUser = _dbContext.users.ToList();
            User result = ListUser.Find(
            delegate(User usr)
            {
                return usr.Id == iduser;
            });
            _dbContext.users.Remove(result);
            _dbContext.SaveChanges();
            return Ok($"Succesfully Deleted User {result.Email}");
        }
    }
}
