using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Data.Models
{
    public class Lead
    {

        [Key]
        public int LeadId { get; set; }
        /// <summary>
        /// Tie this lead to a user.
        /// </summary>
        public string LeadUser { get; set; }

        /// <summary>
        /// Personal Info...
        /// </summary>
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Email { get; set; }

        public Address Address { get; set; }

        public TelephoneNumber PhoneNumber { get; set; }

        /// <summary>
        /// Used for concurrency.
        /// </summary>
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }

    public class LeadContext : DbContext
    {
        public virtual DbSet<Lead> Leads { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Lead>().Property(p => p.RowVersion).IsConcurrencyToken();
        //}
    }
}