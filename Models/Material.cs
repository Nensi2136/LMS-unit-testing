using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Models
{
    public class Material
    {
        [Key]
        public int MId { get; set; }

        [Required]
        public string MName { get; set; } = string.Empty;

        public string? PdfPath { get; set; }

        public int SubId { get; set; }

        public int UploadedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

    public class MaterialDto
    {
        public int MId { get; set; }

        [Required]
        public string MName { get; set; } = string.Empty;

        public string? PdfPath { get; set; }

        public int SubId { get; set; }

        public int UploadedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
