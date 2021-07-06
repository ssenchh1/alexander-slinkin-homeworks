using System.Collections.Generic;
using System.Linq;
using Practice9june.Core.Models;
using Practice9june.Core.Specifications;
using Practice9june.Infrastructure;
using Practice9june.Infrastructure.Repositories;

namespace Practice9june.BLL.Services
{
    public class FileService
    {
        private readonly FileRepository fileRepository;
        private readonly DirectoryRepository directoryRepository;

        public FileService(FileRepository file, DirectoryRepository directory)
        {
            fileRepository = file;
            directoryRepository = directory;
        }

        public IEnumerable<File> GetFilesWithPermission(string directoryName, User user)
        {
            var directory = directoryRepository.Get(d => d.Title == directoryName).First();

            var canReadSpecification = new Specification<File>(f => f.FilePermissions.First(p=> p.UserId == user.Id).CanRead && f.DirectoryId == directory.Id);

            return fileRepository.Get(canReadSpecification.Expression);
        }
    }
}
