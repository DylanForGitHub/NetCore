using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using MySQL.Data.EntityFrameworkCore;

namespace MySite.Model
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
            //optionsBuilder.UseSqlite("Data Source=../../../../Core/blogging.db");
            // 这个 Data Source 会在运行时根据 当前目录 去定位，Update-Database 创建的 db 地址可能和运行时的不同，如果不同，会报 `no such table: table_name` 这样的错误
            // Update-Database 可能要修改成 "Data Source=blogging.db"
        //    optionsBuilder.UseMySQL("Server=localhost;database=madb;uid=root;pwd=1qaz2wsxE;SslMode=None");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>().ToTable("doctor");
        }

        public DbSet<Doctor> Doctors { get; set; }
    }   
}