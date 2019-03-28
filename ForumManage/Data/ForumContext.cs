using ForumManage.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ForumManage.Data
{
    public class ForumContext : DbContext
    {
        public ForumContext(DbContextOptions<ForumContext> options) : base(options)
        {

        }
        public DbSet<Forum> Forums { get; set; }
        public DbSet<Engineer> Engineers { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Forum-Engineer M-M
            modelBuilder.Entity<ForumEngineer>()
                .HasKey(t => new { t.ForumId, t.EngineerId });

            modelBuilder.Entity<ForumEngineer>()
                .HasOne(fe => fe.Forum)
                .WithMany(f => f.ForumEngineers)
                .HasForeignKey(fe => fe.ForumId);

            modelBuilder.Entity<ForumEngineer>()
                .HasOne(fe => fe.Engineer)
                .WithMany(e => e.ForumEngineers)
                .HasForeignKey(fe => fe.EngineerId);
            #endregion

            #region Tag-Thread M-M
            modelBuilder.Entity<TagCase>()
                .HasKey(t => new { t.TagId, t.CaseId });

            modelBuilder.Entity<TagCase>()
                .HasOne(tt => tt.Tag)
                .WithMany(t => t.TagCases)
                .HasForeignKey(tt => tt.TagId);

            modelBuilder.Entity<TagCase>()
                .HasOne(tt => tt.Case)
                .WithMany(t => t.TagCases)
                .HasForeignKey(tt => tt.CaseId);
            #endregion

        }

        #region SaveChanges override
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            //var selectedEntityList = ChangeTracker.Entries()
            //                       .Where(x => x.Entity is BaseEntity &&
            //                       (x.State == EntityState.Added || x.State == EntityState.Modified));

            //foreach (var entity in selectedEntityList)
            //{
            //    if(entity.State == EntityState.Added)
            //    {
            //        ((BaseEntity)entity.Entity).AddedDate = DateTime.Now;
            //    }

            //    ((BaseEntity)entity.Entity).ModifiedDate = DateTime.Now;
                
            //}


            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        ((BaseEntity)entry.Entity).AddedDate = DateTime.Now;
                        ((BaseEntity)entry.Entity).ModifiedDate = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        ((BaseEntity)entry.Entity).ModifiedDate = DateTime.Now;
                        break;

                    case EntityState.Deleted:
                        if (entry.Entity is ILogicInterface)
                        {
                            entry.State = EntityState.Modified;
                            entry.CurrentValues["IsDeleted"] = true;
                        }                       
                        break;
                }
            }
        }
        #endregion
    }
}
