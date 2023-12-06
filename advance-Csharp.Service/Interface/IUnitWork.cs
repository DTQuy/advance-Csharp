namespace advance_Csharp.Service.Interface
{
    public interface IUnitWork
    {
        Task<bool> CompleteAsync(string email);
    }
}
