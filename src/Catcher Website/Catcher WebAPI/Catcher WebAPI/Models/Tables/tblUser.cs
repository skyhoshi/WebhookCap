using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
// ReSharper disable VirtualMemberCallInConstructor

namespace Catcher_WebAPI.Models.Tables
{
    [Table("tblUser")]
    public class tblUser : IdentityUser, IEquatable<string>
    {
        public tblUser(string userName) : base(userName)
        {
            UserId = userName;
            EmailConfirmed = false;
            PhoneNumberConfirmed = false;
            TwoFactorEnabled = false;
            LockoutEnabled = false;
            AccessFailedCount = 0;
        }

        public tblUser() : this($"DefaultUsername{(new Random(DateTime.Now.Millisecond)).Next(1, 1000)}")
        {
        }

        [Key]
        [Column("UserID")]
        [MaxLength(450)]
        [Required]
        public string UserId { get; set; }
        [Column("PWD")]
        public override string PasswordHash { get; set; }

        //[Column("PasswordSalt")] 
        //public string PasswordSalt { get; set; } = "";
        
        //[Column("PasswordSeed")]
        //public string PasswordSeed { get; set; } = "";

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [NotMapped]
        [MaxLength(450)]
        [Required]
        public override string Id { get => UserId; set => UserId = value; }
        [NotMapped]
        [MaxLength(450)]
        [Required]
        public override string UserName { get => UserId; set => UserId = value; }
        public override string? NormalizedUserName { get => base.NormalizedUserName; set => base.NormalizedUserName = value; }
        public override string? Email { get => base.Email; set => base.Email = value; }
        public override string? NormalizedEmail { get => base.NormalizedEmail; set => base.NormalizedEmail = value; }

        public override string? SecurityStamp { get => base.SecurityStamp; set => base.SecurityStamp = value; }
        public override string? ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }
        public override string? PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }
        public override DateTimeOffset? LockoutEnd { get => base.LockoutEnd; set => base.LockoutEnd = value; }
        public override bool EmailConfirmed { get => base.EmailConfirmed; set => base.EmailConfirmed = value; }
        public override bool PhoneNumberConfirmed { get => base.PhoneNumberConfirmed; set => base.PhoneNumberConfirmed = value; }
        public override bool TwoFactorEnabled { get => base.TwoFactorEnabled; set => base.TwoFactorEnabled = value; }
        public override bool LockoutEnabled { get => base.LockoutEnabled; set => base.LockoutEnabled = value; }
        public override int AccessFailedCount { get => base.AccessFailedCount; set => base.AccessFailedCount = value; }

        public string GetNewPasswordSeed()
        {
            return Guid.NewGuid().ToString("N");
        }

        public bool Equals(tblUser other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return UserId == other.UserId && PasswordHash == other.PasswordHash;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((tblUser)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserId, PasswordHash);
        }

        public bool Equals([AllowNull] string other)
        {
            return UserId.Equals(other);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public static bool operator ==(tblUser? left, tblUser? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(tblUser? left, tblUser? right)
        {
            return !Equals(left, right);
        }
    }


}
