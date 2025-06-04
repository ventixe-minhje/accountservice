using Models.Repositories;

namespace Presentation.Services;

public interface IAccountService
{
    Task<RepositoryResult> RegisterAsync(RegisterRequest request);
}