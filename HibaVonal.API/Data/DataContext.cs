using HibaVonal.API.Models;
using HibaVonal.API.Models.Ticket;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HibaVonal.API.Data
{
    public class DataContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<MaintenanceTicket> MaintenanceTickets { get; set; }

        public DbSet<TicketComment> Comments { get; set; }

        public DbSet<TicketReview> Reviews { get; set; }

        override protected void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
            });

            builder.Entity<MaintenanceTicket>(entity =>
            {
                entity.HasOne(t => t.CreatedBy)
                      .WithMany()
                      .HasForeignKey(t => t.CreatedById)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.AssignedTo)
                      .WithMany()
                      .HasForeignKey(t => t.AssignedToId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<TicketComment>(entity =>
            {
                entity.HasOne(c => c.Ticket)
                      .WithMany(t => t.Comments)
                      .HasForeignKey(c => c.TicketId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<TicketReview>(entity =>
            {
                entity.HasOne(r => r.Ticket)
                      .WithOne(t => t.Review)
                      .HasForeignKey<TicketReview>(r => r.TicketId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
