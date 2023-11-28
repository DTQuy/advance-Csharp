using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advance_Csharp.dto.Response.Product
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public string Images { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; } 

        public ProductResponse() 
        {
            CreatedAt = DateTimeOffset.UtcNow;
        }

    }
}
