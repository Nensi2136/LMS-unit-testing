using System;
using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class Subject
    {
        [Key]
        public int SubId { get; set; }

        public required string SubName { get; set; }

        public int SubCradit { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

    public class SubjectDto
    {
        public int SubId { get; set; }

        public required string SubName { get; set; }

        public int SubCradit { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
