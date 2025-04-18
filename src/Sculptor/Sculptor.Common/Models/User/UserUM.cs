﻿using System.ComponentModel.DataAnnotations;

namespace Sculptor.Common.Models.User;

public class UserUM
{
    [DisplayFormat(ConvertEmptyStringToNull = true)]
    [RegularExpression("^(?=.*[A-ZА-Яа-яa-z])([A-ZА-Яа-яa-z])([A-ZА-Яа-яa-z]{2,29})+(?<![_.])$", ErrorMessage = "Username is not valid")]
    public string? Username { get; set; }

    [DisplayFormat(ConvertEmptyStringToNull = true)]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [DisplayFormat(ConvertEmptyStringToNull = true)]
    [RegularExpression("^(?=.*[A-ZА-Яа-яa-z])([A-ZА-Я])([a-zа-я]{2,29})+(?<![_.])$", ErrorMessage = "First name is not valid")]
    [Display(Name = "First name")]
    public string? FirstName { get; set; }

    [DisplayFormat(ConvertEmptyStringToNull = true)]
    [RegularExpression("^(?=.*[A-ZА-Яа-яa-z])([A-ZА-Я])([a-zа-я]{2,29})+(?<![_.])$", ErrorMessage = "Last name is not valid")]
    [Display(Name = "Last name")]
    public string? LastName { get; set; }
}
