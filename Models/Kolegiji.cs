using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Studenti.Models
{
    public class Kolegiji
    {
        [Key]
        public string kolegijID { get; set; }
        [Required]
        public string kolegijIme { get; set; }
        [Required]

        public string kolegijGodinaID { get; set; }

        [ForeignKey("kolegijGodinaID")]
        public Godina kolegijGodina;


    }

}

