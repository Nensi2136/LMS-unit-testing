using System;
using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class User
    {
        [Key]
        public int UId { get; set; }

        public string UName { get; set; }

        public string UEmail { get; set; }

        public string UPhonenumber { get; set; }

        public bool IsAdmin { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

    public class UserDto
    {
        public int UId { get; set; }

        public string UName { get; set; }

        public string UEmail { get; set; }

        public string UPhonenumber { get; set; }

        public bool IsAdmin { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
