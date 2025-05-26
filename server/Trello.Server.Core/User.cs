using Microsoft.AspNetCore.Identity;

namespace Trello.Server.Core;

public class User : IdentityUser<Guid> {
	
	public string Firstname { get; set; } = String.Empty;
	public string Lastname { get; set; } = String.Empty;
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

	public string Fullname => $"{Firstname} {Lastname}";
}