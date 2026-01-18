using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Models
{
    public class SubjectWiseFaculty
    {
        [Key]
        public int SwfId { get; set; }

        public int SubId { get; set; }

        public int FacultyId { get; set; }
    }

    public class SubjectWiseFacultyDto
    {
        public int SwfId { get; set; }
        public int SubId { get; set; }
        public int FacultyId { get; set; }
    }
}
