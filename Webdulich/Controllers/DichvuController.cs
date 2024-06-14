using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdulich.Models;
using Microsoft.EntityFrameworkCore;
using Webdulich.Model;
using Microsoft.AspNetCore.Authorization;

namespace Webdulich.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DichvuController : ControllerBase
    {
        private readonly IDichvuRepository _dichvuRepository;

        public DichvuController(IDichvuRepository dichvuRepository)
        {
            _dichvuRepository = dichvuRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Read,Write")]
        public async Task<ActionResult<IEnumerable<Dichvu>>> GetDichvus()
        {
            var dichvus = await _dichvuRepository.GetAllAsync();
            return Ok(dichvus);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Read,Write")]
        public async Task<ActionResult<Dichvu>> GetDichvu(int id)
        {
            var dichvu = await _dichvuRepository.GetByIdAsync(id);
            if (dichvu == null)
            {
                return NotFound();
            }
            return Ok(dichvu);
        }

        [HttpPost]
        [Authorize(Roles = "Write")]
        public async Task<ActionResult<Dichvu>> PostDichvu(Dichvu dichvu)
        {
            await _dichvuRepository.AddAsync(dichvu);
            return CreatedAtAction(nameof(GetDichvu), new { id = dichvu.DichvuId }, dichvu);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Write")]
        public async Task<IActionResult> PutDichvu(int id, Dichvu dichvu)
        {
            if (id != dichvu.DichvuId)
            {
                return BadRequest();
            }

            await _dichvuRepository.UpdateAsync(dichvu);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Write")]
        public async Task<IActionResult> DeleteDichvu(int id)
        {
            var dichvu = await _dichvuRepository.GetByIdAsync(id);
            if (dichvu == null)
            {
                return NotFound();
            }

            await _dichvuRepository.DeleteAsync(id);
            return NoContent();
        }
    }

}
