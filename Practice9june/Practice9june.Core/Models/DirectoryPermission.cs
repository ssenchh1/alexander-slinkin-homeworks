namespace Practice9june.Core.Models
{
    public class DirectoryPermission : IEntity
    {
        public int DirectoryId { get; set; }

        public int UserId { get; set; }

        public bool CanRead { get; set; }

        public bool CanWrite { get; set; }

        public virtual Directory Directory { get; set; }

        public virtual User User { get; set; }
    }
}
