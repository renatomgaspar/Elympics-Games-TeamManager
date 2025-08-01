﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Elympics_Games.API.Data;
using Elympics_Games.API.Data.Entities;
using Elympics_Games.API.Repositories;
using Elympics_Games.API.DTOs.User;
using Elympics_Games.Mobile.Services;

namespace Elympics_Games.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordService<AuthUserDto> _passwordService;


        public UsersController(
            IUserRepository userRepository, 
            PasswordService<AuthUserDto> passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }


        // GET: api/Users
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_userRepository.GetAll().OrderBy(u => u.Id));
        }


        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost("auth")]
        public async Task<ActionResult<User>> AuthUser([FromBody] AuthUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepository.GetByEmailAsync(dto.Email);

            if (user == null)
            {
                return NotFound("User not found!");
            }

            var authUserDto = new AuthUserDto
            {
                Email = user.Email,
                Password = user.Password
            };

            var isValid = _passwordService.VerifyPassword(authUserDto, user.Password, dto.Password);

            if (!isValid)
            {
                return BadRequest("Incorrect password!");
            }

            return Ok();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(CreateUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
            };

            if (await _userRepository.ExistsByEmailAsync(user.Email))
            {
                return BadRequest("There is already an account created with that Email!");
            }

            await _userRepository.CreateAsync(user);

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }


        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
                await _userRepository.UpdateAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await _userRepository.ExistAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteAsync(user);

            return NoContent();
        }
    }
}
