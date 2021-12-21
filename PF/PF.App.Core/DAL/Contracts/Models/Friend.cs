namespace PF.App.Core.DAL.Contracts.Models
{
    public class Friend
    {
        public enum FriendshipStatus
        {
            Invitation = 0,
            Accepted = 1
        }
        
        public long FriendshipId { get; set; }
        public FriendshipStatus Status { get; set; }

        public long FriendId { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string AvatarSrc { get; set; }
    }
}