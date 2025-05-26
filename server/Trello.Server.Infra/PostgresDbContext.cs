using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Trello.Server.Core;

namespace Trello.Server.Infra;

public class PostgresDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid> {

	public PostgresDbContext(DbContextOptions<PostgresDbContext> options): base(options) { }

	protected override void OnModelCreating(ModelBuilder builder) {
		base.OnModelCreating(builder);
	}
	
}