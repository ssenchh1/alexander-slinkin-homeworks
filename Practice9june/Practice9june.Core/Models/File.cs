

using System.Collections.Generic;

namespace Practice9june.Core.Models
{
    public class File : IEntity
    {
        public int Id { get; set; }

        public int DirectoryId { get; set; }

        public string Title { get; set; }

        public string Extention { get; set; }

        public int Size { get; set; }

        public virtual Directory Directory { get; set; }

        public virtual ICollection<FilePermission> FilePermissions { get; set; }
    }
}
