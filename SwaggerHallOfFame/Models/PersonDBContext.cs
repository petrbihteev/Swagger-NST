﻿using Microsoft.EntityFrameworkCore;

namespace SwaggerHallOfFame
{
    #pragma warning disable CS1591
    public partial class PersonDBContext : DbContext
    {
        public PersonDBContext()
        {
        }

        public PersonDBContext(DbContextOptions<PersonDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ConPersonSkill> ConPersonSkills { get; set; } = null!;
        public virtual DbSet<Person> Persons { get; set; } = null!;
        public virtual DbSet<Skill> Skills { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=PersonDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConPersonSkill>(entity =>
            {
                entity.HasKey(e => e.IdPersonSkill);
                
                entity.HasIndex(e => e.PersonId, "IX_ConPersonSkills_PersonId");

                entity.HasIndex(e => e.SkillId, "IX_ConPersonSkills_SkillId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
    #pragma warning disable CS1591
}
