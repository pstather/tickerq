namespace WebApplication3;

using Microsoft.EntityFrameworkCore;

public class Context : DbContext
{
  public Context(DbContextOptions<Context> options)
    : base(options)
  {
  }
}