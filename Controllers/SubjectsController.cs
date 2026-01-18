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
    public class SubjectsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SubjectsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Subjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetSubjects()
        {
            var subjects = await _context.Subjects.ToListAsync();
            var subjectDtos = subjects.Select(s => new SubjectDto
            {
                SubId = s.SubId,
                SubName = s.SubName,
                SubCradit = s.SubCradit,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt
            }).ToList();

            return subjectDtos;
        }

        // GET: api/Subjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectDto>> GetSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);

            if (subject == null)
            {
                return NotFound();
            }

            var subjectDto = new SubjectDto
            {
                SubId = subject.SubId,
                SubName = subject.SubName,
                SubCradit = subject.SubCradit,
                CreatedAt = subject.CreatedAt,
                UpdatedAt = subject.UpdatedAt
            };

            return subjectDto;
        }

        // PUT: api/Subjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject(int id, SubjectDto subjectDto)
        {
            if (id != subjectDto.SubId)
            {
                return BadRequest();
            }

            var existingSubject = await _context.Subjects.FindAsync(id);
            if (existingSubject == null)
            {
                return NotFound();
            }
            existingSubject.SubName = subjectDto.SubName;
            existingSubject.SubCradit = subjectDto.SubCradit;
            existingSubject.UpdatedAt = DateTime.UtcNow;
            _context.Entry(existingSubject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(id))
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

        // POST: api/Subjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubjectDto>> PostSubject(SubjectDto subjectDto)
        {
            var subject = new Subject
            {
                SubName = subjectDto.SubName,
                SubCradit = subjectDto.SubCradit,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();

            subjectDto.SubId = subject.SubId;
            subjectDto.CreatedAt = subject.CreatedAt;
            subjectDto.UpdatedAt = subject.UpdatedAt;
            return CreatedAtAction("GetSubject", new { id = subject.SubId }, subjectDto);
        }

        // DELETE: api/Subjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubjectExists(int id)
        {
            return _context.Subjects.Any(e => e.SubId == id);
        }
    }
}
