using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using services;

namespace secret_santa.tests
{
	[TestClass]
	public class UnitTest1
	{
		private static Repository CreateRepository()
		{
			return new Repository
			{
				Users = new[]
				{
					new User {UserId = 1, Name = "Vitaly"},
					new User {UserId = 2, Name = "Olya Knyazeva"},
					new User {UserId = 3, Name = "Oleg"},
					new User {UserId = 4, Name = "Olya Belevich"},
					new User {UserId = 5, Name = "Vasya"},
					new User {UserId = 6, Name = "Dasha"},
					new User {UserId = 7, Name = "Max"},
					new User {UserId = 8, Name = "Shapa"},
					new User {UserId = 9, Name = "Katya"}
				},
				Groups = new[]
				{
					new Group {GroupId = 1, Name = "NY2017"}
				},
				UserGroups = new[]
				{
					new UserGroup { UserGroupId = 1, GroupId = 1, UserId = 1 },
					new UserGroup { UserGroupId = 2, GroupId = 1, UserId = 2 },
					new UserGroup { UserGroupId = 3, GroupId = 1, UserId = 3 },
					new UserGroup { UserGroupId = 4, GroupId = 1, UserId = 4 },
					new UserGroup { UserGroupId = 5, GroupId = 1, UserId = 5 },
					new UserGroup { UserGroupId = 6, GroupId = 1, UserId = 6 },
					new UserGroup { UserGroupId = 7, GroupId = 1, UserId = 7 },
					new UserGroup { UserGroupId = 8, GroupId = 1, UserId = 8 },
					new UserGroup { UserGroupId = 9, GroupId = 1, UserId = 9 }
				},
				Exclusions = new Exclusion[0]
			};
		}

		[TestMethod]
		public void TestPersistence()
		{
			var repository = CreateRepository();

			var service = new StorageService();
			service.SetRepository(repository);

			Assert.AreEqual(1, service.GetGroups().Length);

			Assert.AreEqual(9, service.GetGroupUsers(service.GetGroups().First().GroupId).Length);

			service.Save();

			var copy = service.Load();

			Assert.AreEqual(1, copy.Groups.Length);

			Assert.AreEqual(9, copy.UserGroups.Length);

			service.SetRepository(copy);

			service.Generate(service.GetGroups().First().GroupId);

			Assert.IsTrue(copy.UserGroups.All(ug => ug.TargetFriendId != default(int)));
		}
	}
}