using Microsoft.EntityFrameworkCore;

namespace courses_site_api.models
{
    public class courses_entitiy:DbContext
    {
        public courses_entitiy(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<Course_detailes> course_Detailes { get; set; }
        public virtual DbSet<Courses_videos> courses_Videos { get; set; }
        public virtual DbSet<Courses_category> courses_Categories { get; set; }
        public virtual DbSet<registration> registrations { get; set; }
        public virtual DbSet<comments> comments { get; set; }
        public virtual DbSet<chat> chats { get; set; }
        public virtual DbSet<purchasedcourses> purchasedcourses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            modelBuilder.Entity<Courses_category>().HasOne(p => p.Course_Detailes).WithMany(p => p.Courses_Categories).HasForeignKey(p => p.Course_Detailesid).HasConstraintName("ForeignKey-Courses_category-Course_Detailes");
            modelBuilder.Entity<comments>().HasOne(p => p.registration).WithMany(p => p.comments).HasForeignKey(p => p.registrationid).HasConstraintName("ForeignKey-comments-registration");
            modelBuilder.Entity<Courses_videos>().HasOne(p => p.Courses_Category).WithMany(p => p.Courses_Videos).HasForeignKey(p => p.Courses_Categoryid).HasConstraintName("ForeignKey-Courses_videos-Courses_category");
        }

    }
}
