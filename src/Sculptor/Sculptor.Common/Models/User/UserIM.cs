﻿using System.ComponentModel.DataAnnotations;

namespace Sculptor.Common.Models.User;

public class UserIM
{
    [Required]
    [RegularExpression("^(?=.*[A-ZА-Яа-яa-z])([A-ZА-Яа-яa-z])([A-ZА-Яа-яa-z]{2,29})+(?<![_.])$", ErrorMessage = "Username is not valid")]
    public string Username { get; set; } = string.Empty;

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "First name is required")]
    [RegularExpression("^(?=.*[A-ZА-Яа-яa-z])([A-ZА-Я])([a-zа-я]{2,29})+(?<![_.])$", ErrorMessage = "First name is not valid")]
    [Display(Name = "First name")]
    public string FirstName { get; set; } = string.Empty;


    [Required(ErrorMessage = "Last name is required")]
    [RegularExpression("^(?=.*[A-ZА-Яа-яa-z])([A-ZА-Я])([a-zа-я]{2,29})+(?<![_.])$", ErrorMessage = "Last name is not valid")]
    [Display(Name = "Last name")]
    public string LastName { get; set; } = string.Empty;

    //?
    [Required(ErrorMessage ="User role is required")]
    [StringLength(1, ErrorMessage ="The {0} must be exactly {1} character long.", MinimumLength = 1)]
    [RegularExpression("^(0|1|2)$")]
    [Display(Name ="User role")]
    public string Role { get; set; } = string.Empty;
}
