using System.Collections.Generic;
using Core.Models;

namespace Core.DAL.Interfaces
{
    public interface IRepository
    {
        public List<User> LoadRecords();
    }
}