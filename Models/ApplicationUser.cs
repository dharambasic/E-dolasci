using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Studenti.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(36)]
        public override string Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Surname { get; set; }
        [MaxLength(10)]
        public string JMBAG { get; set; }
    }
}
