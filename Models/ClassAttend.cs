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
        public string Name { get; set; }
        public string Surname { get; set; }
        

        public ClassAttend(string identityUserId, string name, string surname, DateTime loggedTime)
        {
            IdentityUserId = identityUserId;
            Name = name;
            Surname = surname;
            LoggedTime = loggedTime;
         
            
        }
    }
}
