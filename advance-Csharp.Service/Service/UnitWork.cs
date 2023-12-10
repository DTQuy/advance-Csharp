using advance_Csharp.Database;
using advance_Csharp.Service.Interface;

namespace advance_Csharp.Service.Service
{
    public class UnitWork : IUnitWork
    {
        private readonly AdvanceCsharpContext _context;
        public UnitWork(AdvanceCsharpContext context)
        {

            _context = context;
        }

        /// <summary>
        /// CompleteAsync
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> CompleteAsync(string email)
        {
            return await _context.SaveChangesAsync(email) > 0;
        }
    }

}
