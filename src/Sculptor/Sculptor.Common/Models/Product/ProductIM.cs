using System.ComponentModel.DataAnnotations;

namespace Sculptor.Common.Models.Product;

public class ProductIM
{
    [RegularExpression("^(?=.*[A-ZА-Яа-яa-z])([A-ZА-Я])([a-zа-я ]{2,50})+(?<![_.])$", ErrorMessage = "Name is not valid")]
    public string Name { get; set; } = string.Empty;

    [RegularExpression("^[0-9]{13}$", ErrorMessage = "EAN is not valid")]
    public string EAN { get; set; } = string.Empty;
}
