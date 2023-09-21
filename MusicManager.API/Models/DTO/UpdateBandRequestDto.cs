﻿using MusicManager.API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace MusicManager.API.Models.DTO
{
    public class UpdateBandRequestDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "Name shoult be betwenn 3 and 20 characters!")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Range(1, 3)]
        public Style Style { get; set; }
    }
}
