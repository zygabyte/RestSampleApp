namespace RestSampleApp.Models;

public record UserResponse(long Id, string Username, string Password);
public record ItemResponse(long Id, string Name, int Quantity);
