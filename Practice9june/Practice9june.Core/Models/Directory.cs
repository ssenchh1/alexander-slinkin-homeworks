using System.Collections.Generic;

namespace Practice9june.Core.Models
{
    public class Directory : IEntity
    {
        public int Id { get; set; }

        public int? ParentDirectoryId { get; set; }

        public virtual Directory ParentDirectory { get; set; }

        public string Title { get; set; }

        public virtual ICollection<File> Files { get; set; }

        public virtual ICollection<Directory> Directories { get; set; }

        public virtual ICollection<DirectoryPermission> DirectoryPermissions { get; set; }
    }
}
