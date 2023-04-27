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
        public DbSet<FriendRequest> friendRequests { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupUser> groupUsers { get; set; }
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

            modelBuilder.Entity<FriendFollower>()
                .HasKey(x => new { x.UserId, x.OtherUserId });
            //modelBuilder.Entity<FriendFollower>()
            //    .HasOne

            modelBuilder.Entity<FriendRequest>()
                .HasKey(x => new { x.SenderId, x.ReceiverId });

            modelBuilder.Entity<GroupUser>()
                .HasKey(x => new { x.UserId, x.GroupId });
        }
    }
}
