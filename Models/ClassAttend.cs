using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Studenti.Models
{
    public class ClassAttend
    {
        [Key]
        public int Id { get; set; }
        public string IdentityUserId { get; set; }
        public DateTime LoggedTime { get; set; }

        public ClassAttend(string identityUserId, DateTime loggedTime)
        {
            IdentityUserId = identityUserId;
            LoggedTime = loggedTime;
        }
    }
}
