namespace advance_Csharp.Service.Interface
{
    public interface IUnitWork
    {
        /// <summary>
        /// CompleteAsync
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> CompleteAsync(string email);
    }
}
