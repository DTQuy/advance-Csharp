using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advance_Csharp.dto.Response.Product
{
    public class ProductCreateResponse
    {
        public string Message { get; set; } = string.Empty;
        public ProductResponse productResponse { get; set; }
    }
}
