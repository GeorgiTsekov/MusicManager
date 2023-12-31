﻿using System.ComponentModel.DataAnnotations;

namespace MusicManager.API.Features.Users.Models
{
    public class RegisterUserRequest
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string RoleId { get; set; }
    }
}
