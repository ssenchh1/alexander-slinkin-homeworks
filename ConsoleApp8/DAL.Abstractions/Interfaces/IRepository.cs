namespace DAL.Abstractions.Interfaces
{
    public interface IRepository
    {
        List<User> LoadRecords();
    }
}