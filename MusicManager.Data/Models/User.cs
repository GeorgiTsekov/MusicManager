﻿using Microsoft.AspNetCore.Identity;
using MusicManager.Infrastructure.Models;
using MusicManager.Infrastructure.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicManager.Data.Models
{
    public class User : IdentityUser, IEntity, IDeletableEntity
    {
        public User()
        {
            Bands = new HashSet<Band>();
            Money = MMConstants.DEFAULT_MONEY;
        }

        public DateTime CreatedOn { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }

        public bool IsDeleted { get; set; }

        public string DeletedBy { get; set; }

        public DateTime? DeletedOn { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Money { get; set; }

        public virtual ICollection<Band> Bands { get; set; }
    }
}