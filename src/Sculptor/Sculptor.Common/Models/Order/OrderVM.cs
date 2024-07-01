using Sculptor.Common.Models.Product;

namespace Sculptor.Common.Models.Order;

public class OrderVM
{
    public int Id { get; set; }

    public string IsDelivered { get; set; } = string.Empty;

    public string ClientFirstName {  get; set; } = string.Empty;

    public string ClientLastName { get; set; } = string.Empty;

    public string ClientEmail { get; set; } = string.Empty;

    public string ClientAddress {  get; set; } = string.Empty;

    public string ClientArea { get; set; } = string.Empty;

    public List<ProductVM> Products { get; set; } = new List<ProductVM>();
}
