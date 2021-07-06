namespace Practice9june.Core.Models
{
    public class FilePermission : IEntity
    {
        public int FileId { get; set; }

        public int UserId { get; set; }

        public bool CanRead { get; set; }

        public bool CanWrite { get; set; }

        public virtual File File { get; set; }

        public virtual User User { get; set; }
    }
}
