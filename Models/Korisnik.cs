using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Studenti.Models;

namespace Studenti.Models
{
    public class Korisnik : IdentityUser

    {
 
        [MaxLength(36)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Surname { get; set; }
        [MaxLength(10)]
        public string JMBAG { get; set; }

    
       
    




    }
}
//In Visual Studio, use the Package Manager Console to scaffold a new migration for these changes and apply them to the database:
//PM> Add-Migration[migration name]
//PM> Update-Database
//Alternatively, you can scaffold a new migration and apply it from a command prompt at your project directory:
//> dotnet ef migrations add[migration name]
//> dotnet ef database update 