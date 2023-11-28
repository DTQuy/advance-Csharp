using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advance_Csharp.dto.Response.Product
{
    public class ProductDeleteResponse
    {
        public string Message { get; set; } = string.Empty;

        public ProductResponse DeletedProduct { get; set; }

        public ProductDeleteResponse(string message, ProductResponse deletedProduct) 
        {
            Message = message;
            DeletedProduct = deletedProduct;
        }

    }
}
