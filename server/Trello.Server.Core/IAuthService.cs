namespace Trello.Server.Core;

public interface IAuthService {
	
	Task<LoginResponse> Login(string email, string password);
	Task Register(string firstname, string lastname, string email, string password);
	Task Logout();
}