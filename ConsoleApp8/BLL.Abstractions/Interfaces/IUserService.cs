﻿using System.Collections.Generic;
using Core.Models;

namespace BLL.Abstractions.Interfaces
{
    public interface IUserService
    {
        public List<User> LoadRecords();
    }
}