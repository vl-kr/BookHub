﻿using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs.Requests.Auth;

public class LoginRequest
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = "";

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";

    public bool RememberMe { get; set; }
}
