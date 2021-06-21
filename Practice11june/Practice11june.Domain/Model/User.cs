using System;

namespace Practice11june.Domain.Model
{
    public class User : IEntity
    {
        public int User_Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime CreatedAt { get; set; }

        public int Country_Code { get; set; }
    }
}
