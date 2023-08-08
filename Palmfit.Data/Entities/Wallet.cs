namespace Palmfit.Data.Entities
{
    public class Wallet : BaseEntity
    {
        public string WalletId { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}