using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using Replacing.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using Replacing.Service;

namespace Replacing.DB
{
    public class DataContext : DbContext
    {
        public DataContext()
        {

        }
        public DataContext(DbContextOptions options) : base(options)
        {
            if (!(this.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
            {
                Database.EnsureCreated();
            }
        }
        public virtual DbSet<ReplaceWord> ReplaceWords { get; set; }
        public virtual DbSet<EncryptedMessage> EncryptedMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var EncryptedMessageID = Guid.NewGuid();

            modelBuilder.Entity<ReplaceWord>().HasData(new ReplaceWord
            {
                OldSymbol = "а",
                NewSymbol = "н",
            });

            modelBuilder.Entity<EncryptedMessage>().HasData(new EncryptedMessage
            {
                MessageId = EncryptedMessageID,
                OriginalMessage = " ",
                EncryptedMessageText = " ",
                ReceivedTime = DateTime.Now,
            });


            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=ReplaceWordTest;User=sa;Password=sa;TrustServerCertificate=true;");
        }
    }
}
