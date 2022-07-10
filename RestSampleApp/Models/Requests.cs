namespace RestSampleApp.Models;

public record UserRequest(string Username, string Password);
public record ItemRequest(string Name, int Quantity);