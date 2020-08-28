using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Spudder.Models
{
    public class Spud
    {
        [Key]
        public string Svid { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        //0: False
        //1: True
        public int Verified { get; set; }
        public string Avatar { get; set; }
    }
}
