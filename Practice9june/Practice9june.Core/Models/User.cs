using System.Collections.Generic;

namespace Practice9june.Core.Models
{
    public class User : IEntity
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public ICollection<FilePermission> FilePermissions { get; set; }

        public ICollection<DirectoryPermission> DirectoryPermissions { get; set; }
    }
}
