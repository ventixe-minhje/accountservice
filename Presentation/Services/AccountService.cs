using Microsoft.AspNetCore.Identity;
using Models.Repositories;
using Presentation.Services;

public class AccountService(UserManager<IdentityUser> userManager) : IAccountService
{
    private readonly UserManager<IdentityUser> _userManager = userManager;

    public async Task<RepositoryResult> RegisterAsync(RegisterRequest request)
    {
        var user = new IdentityUser { UserName = request.Email, Email = request.Email };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
            return new RepositoryResult { Success = true };

        var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
        return new RepositoryResult { Success = false, Error = errorMessages };
    }
}
