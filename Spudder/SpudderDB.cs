using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spudder
{
    public class SpudderDB : DbContext
    {

        public static string ConnectionString = $"server={DBConfig.instance.Host};port=3306;database=spudder;uid={DBConfig.instance.Username};pwd={DBConfig.instance.Password};SslMode=Required;";

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql(ConnectionString, options => options.EnableRetryOnFailure().CharSet(CharSet.Utf8Mb4).ServerVersion(new Version(8, 0, 20), ServerType.MySql));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
