using System;
using Practice11june.Domain.Model;
using Practice11june.Infrastructure.Repository;
using Practice11june.BLL.Services;

namespace Practice11june
{
    class Program
    {
        static void Main(string[] args)
        {
            DapperRepository<User> userRepo = new DapperRepository<User>();
            DapperRepository<Country> countryRepo = new DapperRepository<Country>();

            var user = new User {FullName = "Alex", Gender = "M", Email = "a", DateOfBirth = DateTime.Now, CreatedAt = DateTime.Now, Country_Code = 1};

            var country = new Country {Country_Code = 1, Name = "USA"};
            

            var hidingService = new HidingDataService();

            hidingService.HideInfo("a");
        }
    }
}
