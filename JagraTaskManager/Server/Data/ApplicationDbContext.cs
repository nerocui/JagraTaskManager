using JagraTaskManager.Server.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace JagraTaskManager.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<User>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketWatch> TicketWatches { get; set; }
        public DbSet<TicketDependency> TicketDependencies { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationUser> OrganizationUsers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentReply> CommentReplies { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamUser> TeamUsers { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TicketTag> TicketTags { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<TicketStatus> TicketStatuses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Creator)
                .WithMany(u => u.CreatedTickets)
                .HasForeignKey(t => t.CreatorId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Assignee)
                .WithMany(u => u.AssignedTickets)
                .HasForeignKey(t => t.AssigneeId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TicketWatch>()
                .HasKey(tw => new { tw.UserId, tw.TicketId });
            modelBuilder.Entity<TicketWatch>()
                .HasOne(tw => tw.User)
                .WithMany(u => u.WatchedTickets)
                .HasForeignKey(tw => tw.UserId);
            modelBuilder.Entity<TicketWatch>()
                .HasOne(tw => tw.Ticket)
                .WithMany(t => t.Watchers)
                .HasForeignKey(tw => tw.TicketId);
            modelBuilder.Entity<TicketDependency>()
                .HasKey(td => new { td.DependeeId, td.DependerId });
            modelBuilder.Entity<TicketDependency>()
                .HasOne(td => td.Dependee)
                .WithMany(t => t.Dependees)
                .HasForeignKey(td => td.DependeeId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TicketDependency>()
                .HasOne(td => td.Depender)
                .WithMany(t => t.Dependers)
                .HasForeignKey(td => td.DependerId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CommentReply>()
                .HasKey(cr => new { cr.ReplyId, cr.ReplyToId });
            modelBuilder.Entity<CommentReply>()
                .HasOne(cr => cr.ReplyTo)
                .WithOne(c => c.ReplyTo);
            modelBuilder.Entity<CommentReply>()
                .HasOne(cr => cr.Reply)
                .WithMany(c => c.Replies)
                .HasForeignKey(cr => cr.ReplyId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ReplyTo)
                .WithOne(cr => cr.ReplyTo)
                .HasForeignKey<CommentReply>(cr => cr.ReplyToId);
            modelBuilder.Entity<OrganizationUser>()
                .HasKey(ou => new { ou.OrganizationId, ou.UserId });
            modelBuilder.Entity<OrganizationUser>()
                .HasOne(ou => ou.User)
                .WithMany(u => u.Organizations)
                .HasForeignKey(ou => ou.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<OrganizationUser>()
                .HasOne(ou => ou.Organization)
                .WithMany(o => o.Users)
                .HasForeignKey(ou => ou.OrganizationId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Invitation>()
                .HasKey(i => new { i.UserId, i.OrganizationId });
            modelBuilder.Entity<Invitation>()
                .HasOne(i => i.User)
                .WithMany(u => u.Invitations)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Invitation>()
                .HasOne(i => i.Organization)
                .WithMany(o => o.Invitations)
                .HasForeignKey(i => i.OrganizationId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Team>()
                .HasOne(t => t.Organization)
                .WithMany(o => o.Teams)
                .HasForeignKey(t => t.OrganizationId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TeamUser>()
                .HasOne(tu => tu.User)
                .WithMany(u => u.Teams)
                .HasForeignKey(tu => tu.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TeamUser>()
                .HasOne(tu => tu.Team)
                .WithMany(t => t.Users)
                .HasForeignKey(tu => tu.TeamId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TeamUser>()
                .HasKey(tu => new { tu.TeamId, tu.UserId });
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Team)
                .WithMany(t => t.Tickets)
                .HasForeignKey(t => t.TeamId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Organization)
                .WithMany(o => o.Tickets)
                .HasForeignKey(t => t.OrganizationId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Tag>()
                .HasOne(t => t.Organization)
                .WithMany(o => o.Tags)
                .HasForeignKey(t => t.OrganizationId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TicketTag>()
                .HasOne(tt => tt.Ticket)
                .WithMany(t => t.Tags)
                .HasForeignKey(tt => tt.TicketId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TicketTag>()
                .HasOne(tt => tt.Tag)
                .WithMany(t => t.Tickets)
                .HasForeignKey(tt => tt.TagId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TicketTag>()
                .HasKey(tt => new { tt.TicketId, tt.TagId });
            modelBuilder.Entity<Status>()
                .HasOne(s => s.Organization)
                .WithMany(o => o.Statuses)
                .HasForeignKey(s => s.OrganizationId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TicketStatus>()
                .HasOne(ts => ts.Ticket)
                .WithOne(t => t.Status)
                .HasForeignKey<TicketStatus>(ts => ts.TicketId);
            modelBuilder.Entity<TicketStatus>()
                .HasOne(ts => ts.Status)
                .WithMany(s => s.Tickets)
                .HasForeignKey(ts => ts.StatusId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TicketStatus>()
                .HasKey(ts => new { ts.TicketId, ts.StatusId });
        }
    }
}
