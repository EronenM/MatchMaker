namespace MatchMaker.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MatchMakerEntities : DbContext
    {
        public MatchMakerEntities()
            : base("name=MatchMakerEntities")
        {
        }

        public virtual DbSet<People> People { get; set; }
        public virtual DbSet<Preferences> Preferences { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<People>()
                .HasMany(e => e.Preferences)
                .WithRequired(e => e.People)
                .WillCascadeOnDelete(false);
        }
    }
}
