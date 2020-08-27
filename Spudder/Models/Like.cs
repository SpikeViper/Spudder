using SpookVooper.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Spudder.Models
{
    public class Like
    {
        [Key]
        public string Id { get; set; }
        public string Spud_Id { get; set; }
        public string Author_Id { get; set; }

        /// <summary>
        /// Returns the Spud this like belongs to
        /// </summary>
        public async Task<Spud> GetSpud(SpudderDB db)
        {
            return await db.Spuds.FindAsync(Spud_Id);
        }
    }
}
