using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advance_Csharp.dto.Response.Product
{
    public class ProductUpdateResponse
    {
        public string Message { get; set; }
        public ProductResponse OldProduct { get; set; }
        public ProductResponse UpdatedProduct { get; set; }
    }
}
