using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMS.Data;
using LMS.Models;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectWiseFacultiesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SubjectWiseFacultiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/SubjectWiseFaculties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectWiseFacultyDto>>> GetSubjectWiseFaculties()
        {
            var subjectWiseFaculties = await _context.SubjectWiseFaculties.ToListAsync();
            var subjectWiseFacultyDtos = subjectWiseFaculties.Select(swf => new SubjectWiseFacultyDto
            {
                SwfId = swf.SwfId,
                SubId = swf.SubId,
                FacultyId = swf.FacultyId
            }).ToList();

            return subjectWiseFacultyDtos;
        }

        // GET: api/SubjectWiseFaculties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectWiseFacultyDto>> GetSubjectWiseFaculty(int id)
        {
            var subjectWiseFaculty = await _context.SubjectWiseFaculties.FindAsync(id);

            if (subjectWiseFaculty == null)
            {
                return NotFound();
            }

            var subjectWiseFacultyDto = new SubjectWiseFacultyDto
            {
                SwfId = subjectWiseFaculty.SwfId,
                SubId = subjectWiseFaculty.SubId,
                FacultyId = subjectWiseFaculty.FacultyId
            };

            return subjectWiseFacultyDto;
        }

        // PUT: api/SubjectWiseFaculties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubjectWiseFaculty(int id, SubjectWiseFacultyDto subjectWiseFacultyDto)
        {
            if (id != subjectWiseFacultyDto.SwfId)
            {
                return BadRequest();
            }

            var existingSubjectWiseFaculty = await _context.SubjectWiseFaculties.FindAsync(id);
            if (existingSubjectWiseFaculty == null)
            {
                return NotFound();
            }
            existingSubjectWiseFaculty.SubId = subjectWiseFacultyDto.SubId;
            existingSubjectWiseFaculty.FacultyId = subjectWiseFacultyDto.FacultyId;
            _context.Entry(existingSubjectWiseFaculty).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectWiseFacultyExists(id))
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

        // POST: api/SubjectWiseFaculties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubjectWiseFacultyDto>> PostSubjectWiseFaculty(SubjectWiseFacultyDto subjectWiseFacultyDto)
        {
            var subjectWiseFaculty = new SubjectWiseFaculty
            {
                SubId = subjectWiseFacultyDto.SubId,
                FacultyId = subjectWiseFacultyDto.FacultyId
            };
            _context.SubjectWiseFaculties.Add(subjectWiseFaculty);
            await _context.SaveChangesAsync();

            subjectWiseFacultyDto.SwfId = subjectWiseFaculty.SwfId;
            return CreatedAtAction("GetSubjectWiseFaculty", new { id = subjectWiseFaculty.SwfId }, subjectWiseFacultyDto);
        }

        // DELETE: api/SubjectWiseFaculties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubjectWiseFaculty(int id)
        {
            var subjectWiseFaculty = await _context.SubjectWiseFaculties.FindAsync(id);
            if (subjectWiseFaculty == null)
            {
                return NotFound();
            }

            _context.SubjectWiseFaculties.Remove(subjectWiseFaculty);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubjectWiseFacultyExists(int id)
        {
            return _context.SubjectWiseFaculties.Any(e => e.SwfId == id);
        }
    }
}
