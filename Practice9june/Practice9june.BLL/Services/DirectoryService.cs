using System;
using System.Collections.Generic;
using System.Linq;
using Practice9june.Core.Models;
using Practice9june.Core.Specifications;
using Practice9june.Infrastructure.Repositories;

namespace Practice9june.BLL.Services
{
    public class DirectoryService
    {
        private readonly FileRepository fileRepository;
        private readonly DirectoryRepository directoryRepository;

        public DirectoryService(FileRepository file, DirectoryRepository directory)
        {
            fileRepository = file;
            directoryRepository = directory;
        }

        public IEnumerable<IEntity> GetFilesDirectories(string directoryName)
        {
            var directory = directoryRepository.Get(d => d.Title == directoryName).First();

            List<IEntity> entities = new List<IEntity>();

            entities.AddRange(directory.Files);
            entities.AddRange(directory.Directories);

            return entities;
        }

        public IEnumerable<IEntity> GetFilesDirectories(string directoryName, User user)
        {
            var directory = directoryRepository.Get(d => d.Title == directoryName).First();

            var canReadFileSpecification = new Specification<File>(f => f.FilePermissions.First(p => p.UserId == user.Id).CanRead && f.DirectoryId == directory.Id);
            var canReadDirectorySpecification = new Specification<Directory>(f => f.DirectoryPermissions.First(p => p.UserId == user.Id).CanRead && f.Id == directory.Id);

            List<IEntity> entities = new List<IEntity>();

            entities.AddRange(directory.Files.Where(canReadFileSpecification.Func));
            entities.AddRange(directory.Directories.Where(canReadDirectorySpecification.Func));

            return entities;
        }

        public IEnumerable<string> GetPathOfAllFiles(string directoryName)
        {
            var directory = directoryRepository.Get(d => d.Title == directoryName).First();

            List<string> paths = new List<string>();

            var directoryPath = GeneratePath(directory);

            foreach (var file in directory.Files)
            {
                paths.Add(directoryPath + $"\\{file.Title}.{file.Extention}");
            }

            foreach (var dir in directory.Directories)
            {
                paths.AddRange(GetPathOfAllFiles(dir.Title));
            }

            return paths;

            string GeneratePath(Directory directory)
            {
                var path = directory.Title;

                if (directory.ParentDirectoryId != null)
                {
                    path = GeneratePath(directoryRepository.GetById((int)directory.ParentDirectoryId)) + "\\" + path;
                }

                return path;
            }
        }

        public IEnumerable<string> GetPathOfAllFiles(string directoryName, User user)
        {
            var directory = directoryRepository.Get(d => d.Title == directoryName).First();

            List<string> paths = new List<string>();

            var directoryPath = GeneratePath(directory);

            var canReadFileSpecification = new Specification<File>(f => f.FilePermissions.First(p => p.UserId == user.Id).CanRead && f.DirectoryId == directory.Id);
            var canReadDirectorySpecification = new Specification<Directory>(f => f.DirectoryPermissions.First(p => p.UserId == user.Id).CanRead && f.Id == directory.Id);

            foreach (var file in fileRepository.Get(canReadFileSpecification.Expression))
            {
                paths.Add(directoryPath + $"\\{file.Title}.{file.Extention}");
            }

            foreach (var dir in directory.Directories.Where(canReadDirectorySpecification.Func))
            {
                paths.AddRange(GetPathOfAllFiles(dir.Title));
            }

            return paths;

            string GeneratePath(Directory directory)
            {
                var path = directory.Title;

                if (directory.ParentDirectoryId != null)
                {
                    path = GeneratePath(directoryRepository.GetById((int)directory.ParentDirectoryId)) + "\\" + path;
                }

                return path;
            }
        }

        public (int, int) CountFiles(string directoryName, User user)
        {
            var directory = directoryRepository.Get(d => d.Title == directoryName).First();

            var canReadSpecification = new Specification<File>(f => f.FilePermissions.First(p => p.UserId == user.Id).CanRead && f.DirectoryId == directory.Id);

            var allFilesCount = directory.Files.Count();
            var availableFilesCount = fileRepository.Get(canReadSpecification.Expression).Count();

            return (allFilesCount, availableFilesCount);
        }

        public void GetFilesReport()
        {
            var text = 0;
            var img = 0;
            var audio = 0;
            var video = 0;

            var directories = directoryRepository.Get();

            foreach (var directory in directories)
            {
                var result = CountFilesInDir(directory);
                text += result.Item1;
                img += result.Item2;
                audio += result.Item3;
                video += result.Item4;
            }

            Console.WriteLine("Вид файла\tКоличество");
            Console.WriteLine($"Текст      |\t{text}");
            Console.WriteLine($"Изображения|\t{img}");
            Console.WriteLine($"Аудио      |\t{audio}");
            Console.WriteLine($"Видео      |\t{video}");

            (int, int, int, int) CountFilesInDir(Directory dir)
            {
                var result = (0, 0, 0, 0);
                foreach (var directoryFile in dir.Files)
                {
                    switch (directoryFile.GetType().Name)
                    {
                        case "TextFile": result.Item1 += 1; break;
                        case "ImageFile": result.Item2 += 1; break;
                        case "AudioFile": result.Item3 += 1; break;
                        case "VideoFile": result.Item4 += 1; break;
                        default: break;
                    }
                }

                return result;
            }
        }
    }
}
