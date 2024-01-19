﻿using System.ComponentModel.DataAnnotations;

namespace ProbabilityX_API.Models
{
    public class UserModel
    {
        [Key]
        public int Id_User { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Role { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int Country { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool IsRgpdAccepted { get; set; }
        public int Language { get; set; }
    }

}
