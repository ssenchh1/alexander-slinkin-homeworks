using System;
using Practice9june.BLL.Services;
using Practice9june.Core.Models;
using Practice9june.Infrastructure;
using Practice9june.Infrastructure.Repositories;

namespace Practice9june
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileRepo = new FileRepository(new FileSystemContext());
            var dirRepo = new DirectoryRepository(new FileSystemContext());

            var service = new DirectoryService(fileRepo, dirRepo);

            service.GetFilesReport();

        }
    }
}
