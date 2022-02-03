namespace PF.App.Core.DAL.Contracts.Models
{
    public class Member
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
            
        public long? UserId { get; set; }
    }
}