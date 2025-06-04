using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Data;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext(options)
{
    DbSet<UserEntity> User { get; set; } = null!;
}