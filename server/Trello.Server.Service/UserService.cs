using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Trello.Server.Core;

namespace Trello.Server.Service;

public class UserService : IUserService {

	private readonly UserManager<User> _manager;

	public UserService(UserManager<User> manager) {
		_manager = manager;
	}

	public async Task<IEnumerable<User>> GetAll() {
		try {
			var users = await _manager.Users.ToListAsync();
			return users;
		}
		catch (Exception ex) {
			throw new Exception($"Failed to fetch all the users", ex);
		}
	}

	public async Task<User> GetById(Guid id) {
		try {
			var result = await _manager.FindByIdAsync(id.ToString());

			if (result == null) {
				throw new Exception($"User with id {id} not found");
			}

			return result;
		}
		catch (Exception ex) {
			throw new Exception($"Failed to fetch user with id: {id}", ex);
		}
	}

	public async Task Update(Guid id, User user) {
		try {
			var exists = await _manager.FindByIdAsync(id.ToString());

			if (exists == null) {
				throw new Exception($"User with id {id} not found");		
			}

			user.UpdatedAt = DateTime.UtcNow;
			var result = await _manager.UpdateAsync(user);

			if (result.Errors.Count() > 0) {
				string errors = result.Errors.ToList().Aggregate("", (current, error) => current + (error.Description + "\n"));
				throw new Exception($"Failed to update user with id: {id}\n{errors}");
			}
		}
		catch (Exception ex) {
			throw new Exception($"Failed to update user with id: {id}", ex);
		}
	}

	public async Task Delete(Guid id) {
		try {
			var user = await _manager.FindByIdAsync(id.ToString());

			if (user == null) {
				throw new Exception($"User with id {id} not found");
			}

			await _manager.DeleteAsync(user);
		}
		catch (Exception ex) {
			throw new Exception($"Failed to delete user with id: {id}", ex);
		}
	}
}