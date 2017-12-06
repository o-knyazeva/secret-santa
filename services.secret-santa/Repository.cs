namespace services
{
    public class Repository
    {
		public User[] Users { get; set; }
	    public Group[] Groups { get; set; }
	    public UserGroup[] UserGroups { get; set; }
	    public Exclusion[] Exclusions { get; set; }
	}
}