using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sculptor.DAL.Models;

public class ProductOrder
{
    [Required]
    public int OrderId { get; set; }

    [Required]
    public int ProductId { get; set; }

    [ForeignKey(nameof(OrderId))]
    public Order Order { get; set; }

    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; }
}
