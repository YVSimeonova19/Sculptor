using System.ComponentModel.DataAnnotations;

namespace Sculptor.Common.Models.Order;

public class OrderIM
{
    [Required(ErrorMessage = "A first name is required")]
    [RegularExpression("^(?=.*[A-ZА-Яа-яa-z0-9 ])([A-ZА-Яа-яa-z])([A-ZА-Яа-яa-z0-9 ]{2,49})+(?<![_.])$", ErrorMessage = "Name is not valid")]
    public string ClientFirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "A last name is required")]
    [RegularExpression("^(?=.*[A-ZА-Яа-яa-z0-9 ])([A-ZА-Яа-яa-z])([A-ZА-Яа-яa-z0-9 ]{2,49})+(?<![_.])$", ErrorMessage = "Name is not valid")]
    public string ClientLastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "An email is required")]
    [RegularExpression("^(?=.*[A-ZА-Яа-яa-z0-9 ])([A-ZА-Яа-яa-z])([A-ZА-Яа-яa-z0-9 ]{2,49})+(?<![_.])$", ErrorMessage = "Name is not valid")]
    public string ClientEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "An address is required")]
    [RegularExpression("^(?=.*[A-ZА-Яа-яa-z0-9 ])([A-ZА-Яа-яa-z])([A-ZА-Яа-яa-z0-9 ]{2,49})+(?<![_.])$", ErrorMessage = "Name is not valid")]
    public string ClientAddress { get; set; } = string.Empty;

    [Required(ErrorMessage = "An area is required")]
    [RegularExpression("^(?=.*[A-ZА-Яа-яa-z0-9 ])([A-ZА-Яа-яa-z])([A-ZА-Яа-яa-z0-9 ]{2,49})+(?<![_.])$", ErrorMessage = "Name is not valid")]
    public string ClientArea { get; set; } = string.Empty;

    [Required(ErrorMessage = "At least one product is required")]
    //?
    public List<OrderVM> Items { get; set; } = new List<OrderVM>();
}
