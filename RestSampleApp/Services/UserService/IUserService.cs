using RestSampleApp.Models;

namespace RestSampleApp.Services.UserService;

public interface IUserService
{
    Task<long> CreateAsync(UserRequest user);
    IEnumerable<UserResponse> GetAllAsync();
    Task<bool> LoginAsync(UserRequest user);
}