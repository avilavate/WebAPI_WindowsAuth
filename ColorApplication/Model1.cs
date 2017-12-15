namespace ColorApplication
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<NotificationList> NotificationLists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NotificationList>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<NotificationList>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<NotificationList>()
                .Property(e => e.UserId)
                .HasPrecision(18, 0);
        }
    }
}
