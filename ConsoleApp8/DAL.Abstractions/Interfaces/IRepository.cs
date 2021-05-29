using System.Collections.Generic;
using Core.Models;

namespace DAL.Abstractions.Interfaces
{
    public interface IRepository
    {
        public List<User> LoadRecords();
    }
}