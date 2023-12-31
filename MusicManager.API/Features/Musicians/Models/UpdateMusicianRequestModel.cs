﻿using System.ComponentModel.DataAnnotations;

namespace MusicManager.API.Features.Musicians.Models
{
    public class UpdateMusicianRequestModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "Name shoult be betwenn 3 and 20 characters!")]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
