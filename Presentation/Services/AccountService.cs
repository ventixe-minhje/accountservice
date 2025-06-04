using Microsoft.AspNetCore.Identity;
using Presentation.Models;
using Presentation.Repositories;

namespace Presentation.Services;
public class AccountService(
    UserManager<IdentityUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IHttpClientFactory httpClientFactory) : IAccountService
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Profile");

    public async Task<RepositoryResult> RegisterAsync(string email, string password, string firstName, string lastName,
                                                      string streetName, string? streetNumber, string postalCode,
                                                      string city)
    {
        var user = new IdentityUser { UserName = email, Email = email };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            return new RepositoryResult { Success = false, Error = string.Join(", ", result.Errors.Select(e => e.Description)) };

        var profile = new UserProfileRequest { UserId = user.Id, FirstName = firstName, LastName = lastName };

        var response = await _httpClient.PostAsJsonAsync("api/profiles", profile);
        if (!response.IsSuccessStatusCode)
            return new RepositoryResult { Success = false, Error = "Failed to save profile." };

        var adress = new
        {
            UserId = user.Id,
            AdressTypeId = 1,
            StreetName = streetName,
            StreetNumber = streetNumber,
            PostalCode = postalCode,
            City = city
        };

        var adressResponse = await _httpClient.PostAsJsonAsync("api/adressinfo", adress);
        if (!adressResponse.IsSuccessStatusCode)
            return new RepositoryResult { Success = false, Error = "Failed to save adress." };

        return new RepositoryResult { Success = true };
    }
}
