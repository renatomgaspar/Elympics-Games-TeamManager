using Elympics_Games.API.Data;
using Elympics_Games.API.Data.Entities;
using Elympics_Games.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elympics_Games.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamRepository _teamRepository;

        public TeamsController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        // GET: api/Teams
        [HttpGet()]
        public IActionResult GetTeams()
        {
            return Ok(_teamRepository.GetAll().OrderBy(t => t.Id));
        }

        // GET: api/Teams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(Team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newTeam = new Team
            {
                Name = team.Name,
                Country = team.Country,
                ElementsNumber = team.ElementsNumber,
            };

            if (await _teamRepository.ExistsByCountryAsync(newTeam.Country))
            {
                return BadRequest("There is already a team created with that Country!");
            }

            await _teamRepository.CreateAsync(newTeam);

            return CreatedAtAction("GetTeam", new { id = newTeam.Id }, newTeam);
        }

        // PUT: api/Teams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, Team user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
                await _teamRepository.UpdateAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _teamRepository.ExistAsync(id))
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

        // DELETE: api/Teams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var user = await _teamRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _teamRepository.DeleteAsync(user);

            return NoContent();
        }
    }
}
