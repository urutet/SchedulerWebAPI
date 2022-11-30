﻿using System.ComponentModel.DataAnnotations;

namespace Scheduler.Models.Auth;

public class RegisterUser
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}