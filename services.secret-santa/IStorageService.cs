namespace services
{
	public interface IStorageService
	{
		void SetRepository(Repository repository);
		
		Group[] GetGroups();
		User[] GetGroupUsers(int groupId);

		void SetExclusions(int userid, int groupId, int[] usersToExclude);

		Repository Load();
		void Save();

		void Generate(int groupId);
	}
}