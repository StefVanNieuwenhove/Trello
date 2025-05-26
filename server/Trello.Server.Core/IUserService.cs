namespace Trello.Server.Core;

public interface IUserService {

	Task<IEnumerable<User>> GetAll();
	Task<User> GetById(Guid id);
	Task Update(Guid id, User user);
	Task Delete(Guid id);
}