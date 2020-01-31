using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Studenti.Models
{
    public class Godina
    {
        [Key]
        public string godinaID { get; set; }
        [Required]
        public string godinaBroj { get; set; }
    }
}