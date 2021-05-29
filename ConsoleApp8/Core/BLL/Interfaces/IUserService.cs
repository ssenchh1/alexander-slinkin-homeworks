using System.Collections.Generic;
using Core.Models;

namespace Core.BLL.Interfaces
{
    public interface IUserService
    {
        public List<User> LoadRecords();
    }
}