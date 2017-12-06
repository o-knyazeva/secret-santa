using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace services
{
	public class StorageService : IStorageService
	{
		const string path = @"d:\SecretSanta.dat";

		private static Repository _repository;

		private readonly object _syncRoot = new object();

		public StorageService()
		{
			_repository = Load();
		}

		public void SetRepository(Repository repository)
		{
			_repository = repository;
		}

		public Group[] GetGroups()
		{
			lock (_syncRoot)
			{
				return _repository.Groups.ToArray();
			}
		}

		public User[] GetGroupUsers(int groupId)
		{
			lock (_syncRoot)
			{
				return _repository.UserGroups.Where(g => g.GroupId == groupId)
					.Select(ug => ug.UserId)
					.Select(userId => _repository.Users.First(uu => uu.UserId == userId)).ToArray();
			}
		}

		public void SetExclusions(int userid, int groupId, int[] usersToExclude)
		{
			lock (_syncRoot)
			{
				var existingExclusions = _repository.Exclusions.Where(e => e.UserId == userid && e.UserGroupId == groupId).ToDictionary(e => e.ExclusionId);

				foreach (var userToExclude in usersToExclude.Distinct())
				{
					if (!existingExclusions.ContainsKey(userToExclude))
					{
						var userGroupId = _repository.UserGroups.First(ug => ug.UserId == userid && ug.GroupId == groupId).UserGroupId;
						var exclusionId = _repository.Exclusions.Any() ? _repository.Exclusions.Max(e => e.ExclusionId) + 1 : 1;

						var newExclusion = new[]
						{
							new Exclusion
							{
								ExclusionId = exclusionId,
								UserGroupId = userGroupId,
								UserId = userToExclude
							}
						};
						_repository.Exclusions = _repository.Exclusions.Concat(newExclusion).ToArray();
					}
				}
			}
		}

		public Repository Load()
		{
			var content = File.ReadAllText(path);
			return JsonConvert.DeserializeObject<Repository>(content);
		}

		public void Save()
		{
			lock (_syncRoot)
			{
				var content = JsonConvert.SerializeObject(_repository);
				File.WriteAllText(path, content);
			}
		}

		public void Generate(int groupId)
		{
			lock (_repository)
			{
				var random = new Random((int) DateTime.UtcNow.Ticks);

				var users = _repository.UserGroups.Where(ug => ug.GroupId == groupId).ToArray();

				foreach (var user in users)
					user.TargetFriendId = default(int);
				
				foreach (var user in users)
				{
					var counterparties = users.Where(u =>
					{
						if (u.UserId == user.UserId)
							return false;

						if (u.TargetFriendId != default(int))
							return false;
						
						var userGroupId = _repository.UserGroups.First(ug => ug.UserId == u.UserId && ug.GroupId == groupId).UserGroupId;

						return !_repository.Exclusions.Any(e => e.UserId == user.UserId && e.UserGroupId == userGroupId);
					}).ToArray();

					var counterpartyIndex = random.Next(0, counterparties.Length);

					var counterparty = counterparties[counterpartyIndex];

					user.TargetFriendId = counterparty.UserId;
				}
			}
		}
	}
}