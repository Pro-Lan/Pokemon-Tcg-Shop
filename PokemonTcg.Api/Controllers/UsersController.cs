using Microsoft.AspNetCore.Mvc;
using PokemonTcg.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace PokemonTcg.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        // 修改測試用戶
        private static List<UserDto> _users = new List<UserDto>
        {
            new UserDto 
            { 
                UserId = 1, 
                Username = "prolan", 
                Password = "123", 
                Email = "prolan@example.com" 
            }
        };
        private static int _nextUserId = 2;

        [HttpPost("register")]
        public ActionResult<UserDto> Register([FromBody] UserDto user)
        {
            // 檢查用戶名是否已存在
            if (_users.Any(u => u.Username.ToLower() == user.Username.ToLower()))
            {
                return BadRequest(new { message = "用戶名已存在" });
            }

            // 檢查必要字段
            if (string.IsNullOrWhiteSpace(user.Username) || 
                string.IsNullOrWhiteSpace(user.Password) || 
                string.IsNullOrWhiteSpace(user.Email))
            {
                return BadRequest(new { message = "所有字段都必須填寫" });
            }

            // 創建新用戶
            var newUser = new UserDto
            {
                UserId = _nextUserId++,
                Username = user.Username.Trim(),
                Password = user.Password,
                Email = user.Email.Trim()
            };

            _users.Add(newUser);

            // 返回成功消息，不返回密碼
            return Ok(new { 
                message = "註冊成功",
                user = new { newUser.UserId, newUser.Username, newUser.Email }
            });
        }

        [HttpPost("login")]
        public ActionResult<UserDto> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                // 檢查必要字段
                if (string.IsNullOrWhiteSpace(loginDto?.Username) || 
                    string.IsNullOrWhiteSpace(loginDto?.Password))
                {
                    return BadRequest(new { message = "用戶名和密碼不能為空" });
                }

                // 查找用戶（不區分大小寫）
                var user = _users.FirstOrDefault(u => 
                    u.Username.ToLower() == loginDto.Username.ToLower() && 
                    u.Password == loginDto.Password);

                if (user == null)
                {
                    return BadRequest(new { message = "用戶名或密碼錯誤" });
                }

                // 返回成功消息，不返回密碼
                return Ok(new { 
                    message = "登入成功",
                    user = new { user.UserId, user.Username, user.Email }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"登入失敗: {ex.Message}" });
            }
        }

        // 用於測試的端點，獲取所有用戶（實際應用中不應該有這個）
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = _users.Select(u => new { u.UserId, u.Username, u.Email }).ToList();
            return Ok(users);
        }
    }

    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
} 