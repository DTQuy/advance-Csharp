using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advance_Csharp.Database.Models
{
    /// <summary>
    /// abstract class BaseEntity
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// CreatedAt
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// NewGuid, CreatedAt: UtcNow
        /// </summary>
        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
