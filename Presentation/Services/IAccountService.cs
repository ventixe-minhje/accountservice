using Microsoft.AspNetCore.Identity.Data;
using Presentation.Repositories;

namespace Presentation.Services;

public interface IAccountService
{
    Task<RepositoryResult> RegisterAsync(string email, string password, string firstName, string lastName, string streetName, string? streetNumber, string postalCode, string city);
}
