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
        public string Id { get; set; }
        public string Content { get; set; }
        public int Post_Time { get; set; }
        public string Parent { get; set; }
        public string Author { get; set; }

        /// <summary>
        /// Returns all children for this Spud
        /// </summary>

        public IQueryable<Spud> GetChildren(SpudderDB db)
        {
            return db.Spuds.AsQueryable().Where(spud => spud.Parent == this.Id);
        }

        /// <summary>
        /// Returns the parent for this spud
        /// </summary>
        public async Task<Spud> GetParent(SpudderDB db)
        {
            return await db.Spuds.AsQueryable().FirstOrDefaultAsync(spud => spud.Id == this.Parent);
        }

        public IQueryable<Like> GetLikes(SpudderDB db)
        {
            return db.Likes.Where(like => like.Spud_Id == this.Id);
        }

        public async Task<int> GetLikeCount(SpudderDB db)
        {
            return await db.Likes.CountAsync(like => like.Spud_Id == this.Id);
        }
    }
}
