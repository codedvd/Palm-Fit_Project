using Microsoft.AspNetCore.Identity;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Metrics;
using Palmfit.Data.EntityEnums;

namespace Palmfit.Data.Entities
{
    public class AppUser : IdentityUser
    {
        public string Title { get; set; } = string.Empty;
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }= string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public Gender Gender { get; set; } = Gender.Male;
        public DateTime DateOfBirth { get; set; }= DateTime.Now;
        public string Country { get; set; } = string.Empty;
        public bool IsLockedOut { get; set; }= false;
        public DateTime LastOnline { get; set; }=DateTime.Now;
        public bool IsVerified { get; set; }=false;
        public bool IsArchived { get; set; } = false;
        public bool Active { get; set; } = true;
        public string ReferralCode { get; set; } = string.Empty;
        public string InviteCode { get; set; } = string.Empty;
        public Health Health { get; set; } 
        public Setting Setting { get; set; }
        public Wallet Wallet { get; set; }

        public ICollection<Invite> Invities { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}