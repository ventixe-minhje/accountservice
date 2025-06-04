using Data.Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UserRepository(DataContext context) : BaseRepository<UserEntity>(context), IUserRepository
{
    public async Task<UserEntity?> GetByEmailAsync(string email)
    {
        return await _table.FirstOrDefaultAsync(u => u.Email == email);
    }
}
public interface IUserRepository : IBaseRepository<UserEntity>
{
    Task<UserEntity?> GetByEmailAsync(string email);
}
