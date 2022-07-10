using RestSampleApp.Data;
using RestSampleApp.Models;
using RestSampleApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace RestSampleApp.Services.UserService;

public class UserService : IUserService
{
    private readonly AppDbContext appDbContext;

    public UserService(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<long> CreateAsync(UserRequest user)
    {
        if (await appDbContext.Users.AnyAsync(x => x.Username.ToLower() == user.Username.ToLower()))
        {
            Console.WriteLine($"{user.Username} already exists");
            return 0;
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

        var newUser = new User
        {
            Password = hashedPassword,
            Username = user.Username
        };

        await appDbContext.Users.AddAsync(newUser);
        await appDbContext.SaveChangesAsync();

        return newUser.Id;
    }

    public IEnumerable<UserResponse> GetAllAsync() => appDbContext.Users.Select(x => new UserResponse(x.Id, x.Username, x.Password));

    public async Task<bool> LoginAsync(UserRequest user)
    {
        var retrievedUser = await appDbContext.Users.FirstOrDefaultAsync(x => x.Username == user.Username);
        if (retrievedUser is null) return false;

        var password = BCrypt.Net.BCrypt.Verify(user.Password, retrievedUser.Password);
        return password;
    }
}