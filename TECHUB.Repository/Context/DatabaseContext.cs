using Microsoft.EntityFrameworkCore;
using TECHUB.Repository.Models;

namespace TECHUB.Repository.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {

        }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Chat> Chats { get; set; }
        public DbSet<FriendFollower> FriendFollowers { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=192.168.20.33,1433; Initial Catalog=TECHub; TrustServerCertificate=True; User ID=sa; Password=Passw0rd;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<FriendFollower>(entity =>
            {
                entity.HasKey(x => new { x.UserId, x.OtherUserId });

                entity.HasOne(x => x.User)
                .WithMany(u => u.UserFriendFollowers)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(x => x.OtherUser)
                .WithMany(ou => ou.OtherUserFriendFollowers)
                .HasForeignKey(ou => ou.OtherUserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<FriendRequest>(entity =>
            {
                entity.HasKey(x => new { x.SenderId, x.ReceiverId });

                entity.HasOne(x => x.Sender)
                .WithMany(x => x.SentFriendRequests)
                .HasForeignKey(x => x.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(x => x.Receiver)
                .WithMany(x => x.ReceivedFriendRequests)
                .HasForeignKey(x => x.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            //modelBuilder.Entity<Picture>(entity =>
            //{
            //    entity.HasKey(x => x.PictureId);

            //    entity.HasMany(x => x.User)
            //    .WithMany(u => u.Pictures);
            //    //.HasForeignKey(p => p.UserId)
            //    //.OnDelete(DeleteBehavior.ClientSetNull);
            //});

            modelBuilder.Entity<User>()
            .HasOne(u => u.Picture)
            .WithMany(p => p.User)
            .HasForeignKey(u => u.ProfilePictureId)
            .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<FriendRequest>()
                .HasKey(x => new { x.SenderId, x.ReceiverId });

            modelBuilder.Entity<GroupUser>()
                .HasKey(x => new { x.UserId, x.GroupId });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.HasKey(x => new { x.UserId });

                entity.HasOne(x => x.User)
                .WithMany(x => x.Likes)                
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(x => x.Post)
                .WithMany(x => x.Likes)
                .HasForeignKey(x => x.PostId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(x => x.Comment)
                .WithMany(x => x.Likes)
                .HasForeignKey(x => x.CommentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
