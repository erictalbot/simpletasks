using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace $rootnamespace$.Entities
{
    /// <summary>
    /// User class for AltairisWebSecurity
    /// DO NOT REMOVE ANY PROPERTIES! AltairisWebSecurity depends on them.
    /// </summary>
    public class User
    {
        [Key]
        [Required(ErrorMessage = "User Name is required")]
        [Display(Name = "User Name")]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(64)]
        public byte[] PasswordHash { get; set; }

        [Required]
        [MaxLength(128)]
        public byte[] PasswordSalt { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(200)]
        public string Email { get; set; }

        [MaxLength(200)]
        public string Comment { get; set; }

        [Display(Name = "Approved?")]
        public bool IsApproved { get; set; }

        [Display(Name = "Crate Date")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Last Login Date")]
        public DateTime? DateLastLogin { get; set; }

        [Display(Name = "Last Activity Date")]
        public DateTime? DateLastActivity { get; set; }

        [Display(Name = "Last Password Change Date")]
        public DateTime DateLastPasswordChange { get; set; }

        public string FullName { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

    }

    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.HasMany(u => u.Roles)
             .WithMany(r => r.Users)
             .Map(m =>
             {
                 m.ToTable("RoleMemberships");
                 m.MapLeftKey("UserName");
                 m.MapRightKey("RoleName");
             });
        }
    }
}