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
    public class MaterialsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MaterialsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Materials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialDto>>> GetMaterials()
        {
            var materials = await _context.Materials.ToListAsync();
            var materialDtos = materials.Select(m => new MaterialDto
            {
                MId = m.MId,
                MName = m.MName,
                PdfPath = m.PdfPath,
                SubId = m.SubId,
                UploadedBy = m.UploadedBy,
                CreatedAt = m.CreatedAt,
                UpdatedAt = m.UpdatedAt
            }).ToList();

            return materialDtos;
        }

        // GET: api/Materials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialDto>> GetMaterial(int id)
        {
            var material = await _context.Materials.FindAsync(id);

            if (material == null)
            {
                return NotFound();
            }

            var materialDto = new MaterialDto
            {
                MId = material.MId,
                MName = material.MName,
                PdfPath = material.PdfPath,
                SubId = material.SubId,
                UploadedBy = material.UploadedBy,
                CreatedAt = material.CreatedAt,
                UpdatedAt = material.UpdatedAt
            };

            return materialDto;
        }

        // PUT: api/Materials/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaterial(int id, MaterialDto materialDto)
        {
            if (id != materialDto.MId)
            {
                return BadRequest();
            }

            var existingMaterial = await _context.Materials.FindAsync(id);
            if (existingMaterial == null)
            {
                return NotFound();
            }
            existingMaterial.MName = materialDto.MName;
            existingMaterial.PdfPath = materialDto.PdfPath;
            existingMaterial.SubId = materialDto.SubId;
            existingMaterial.UploadedBy = materialDto.UploadedBy;
            existingMaterial.UpdatedAt = DateTime.UtcNow;
            _context.Entry(existingMaterial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialExists(id))
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

        // POST: api/Materials
        [HttpPost]
        public async Task<ActionResult<MaterialDto>> PostMaterial(MaterialDto materialDto)
        {
            var subjectExists = await _context.Subjects.AnyAsync(s => s.SubId == materialDto.SubId);
            if (!subjectExists)
            {
                return BadRequest("Subject with the provided SubId does not exist.");
            }

            var uploaderExists = await _context.Users.AnyAsync(u => u.UId == materialDto.UploadedBy);
            if (!uploaderExists)
            {
                return BadRequest("Uploader with the provided UploadedBy ID does not exist.");
            }

            var material = new Material
            {
                MName = materialDto.MName,
                PdfPath = materialDto.PdfPath,
                SubId = materialDto.SubId,
                UploadedBy = materialDto.UploadedBy,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _context.Materials.Add(material);
            await _context.SaveChangesAsync();

            materialDto.MId = material.MId;
            materialDto.CreatedAt = material.CreatedAt;
            materialDto.UpdatedAt = material.UpdatedAt;
            return CreatedAtAction("GetMaterial", new { id = material.MId }, materialDto);
        }

        // DELETE: api/Materials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterial(int id)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material == null)
            {
                return NotFound();
            }

            _context.Materials.Remove(material);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MaterialExists(int id)
        {
            return _context.Materials.Any(e => e.MId == id);
        }
    }
}
