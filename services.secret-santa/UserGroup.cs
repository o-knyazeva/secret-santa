namespace services
{
    public class UserGroup
    {
	    public int UserGroupId { get; set; }
	    public int UserId { get; set; }
	    public int GroupId { get; set; }

		public string Desire { get; set; }
	    public int TargetFriendId { get; set; }
	}
}