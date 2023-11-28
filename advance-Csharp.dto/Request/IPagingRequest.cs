using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advance_Csharp.dto.Request
{
    public interface IPagingRequest
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
