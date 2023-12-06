using advance_Csharp.Database;
using advance_Csharp.Database.Models;
using advance_Csharp.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advance_Csharp.Service.Service
{
    public class UnitWork : IUnitWork
    {
        private readonly AdvanceCsharpContext _context;
        public UnitWork(AdvanceCsharpContext context)
        {

            _context = context;
        }
        public async Task<bool> CompleteAsync(string email)
        {
            return await _context.SaveChangesAsync(email) > 0;
        }
    }
 
}
