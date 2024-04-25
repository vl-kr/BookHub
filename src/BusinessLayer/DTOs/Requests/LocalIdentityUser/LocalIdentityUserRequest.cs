﻿namespace BusinessLayer.DTOs.Requests.LocalIdentityUser;

public class LocalIdentityUserRequest : BaseRequest
{
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
    public string? Role { get; set; } = "User";
}
