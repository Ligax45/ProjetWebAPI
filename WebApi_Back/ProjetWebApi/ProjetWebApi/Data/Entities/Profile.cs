﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetWebApi.Data.Entities
{
    [Table("profile")]
    public class Profile
    {
        [Key, Required]
        public int Id { get; set; }
        public virtual User User { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
