using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Practice9june.Core.Models;

namespace Practice9june.Infrastructure
{
    public class FileSystemContext : DbContext
    {
        public FileSystemContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<FilePermission> FilePermissions { get; set; }
        public DbSet<DirectoryPermission> DirectoryPermissions { get; set; }
        public DbSet<Directory> Directories { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<AudioFile> AudioFiles { get; set; }
        public DbSet<VideoFile> VideoFiles { get; set; }
        public DbSet<ImageFile> ImageFiles { get; set; }
        public DbSet<TextFile> TextFiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = DESKTOP-3RQFHH4\SQLEXPRESS01; Initial Catalog = FileSystem; Integrated Security = True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<User>().HasData(
                new User {Id = 1, Email = "email@gmail.com", PasswordHash = "sdf3gh4j56432bnm,c.hgf", UserName = "ForstUser"},
                new User {Id = 2, Email = "email@gmail.com", PasswordHash = "fm,/", UserName = "SecondUser"},
                new User {Id = 3, Email = "email@gmail.com", PasswordHash = "fjzkvjdjkrbf", UserName = "ThirdUser"}
                );

            modelBuilder.Entity<Directory>().HasData(
                new Directory {Id = 1, Title = "FirstDirectory"},
                new Directory {Id = 2, Title = "SecondDir", ParentDirectoryId = 1},
                new Directory {Id = 3, Title = "ThirdDir", ParentDirectoryId = 1}
                );

            modelBuilder.Entity<TextFile>().HasData(
                new TextFile {Id = 1, Title = "C# file", Content = "using System...", Extention = "txt", Size = 5, DirectoryId = 2},
                new TextFile {Id = 2, Title = "EF tutorial", Content = "Entity Framework", Extention = "txt", Size = 5, DirectoryId = 3},
                new TextFile {Id = 3, Title = "Dapper tutorial", Content = "Dapper is ORM", Extention = "txt", Size = 5, DirectoryId = 3}
                );

            modelBuilder.Entity<VideoFile>().HasData(
                new VideoFile {Id = 4, Title = "Me at the zoo", Extention = "mp4", Duration = 20, Height = 720, Width = 1080, DirectoryId = 1},
                new VideoFile {Id = 5, Title = "C# Crash course", Extention = "mp4", Duration = 50, Height = 720, Width = 1080, DirectoryId = 2},
                new VideoFile {Id = 6, Title = "Despacito", Extention = "mp4", Duration = 5, Height = 720, Width = 1080, DirectoryId = 3}
                );

            modelBuilder.Entity<AudioFile>().HasData(
                new AudioFile {Id = 7, Title = "Lection1", Extention = "mp3", Duration = 3, Size = 3, Bitrate = 12, SampleRate = 5, ChannelCount = 3, DirectoryId = 2},
                new AudioFile {Id = 8, Title = "Lection2", Extention = "mp3", Duration = 3, Size = 4, Bitrate = 12, SampleRate = 5, ChannelCount = 3, DirectoryId = 3},
                new AudioFile {Id = 9, Title = "Lection3", Extention = "mp3", Duration = 3, Size = 5, Bitrate = 12, SampleRate = 5, ChannelCount = 3, DirectoryId = 1}
                );

            modelBuilder.Entity<ImageFile>().HasData(
                new ImageFile {Id = 10, Title = "Image1", Extention = "jpg", Height = 400, Width = 400, Size = 5, DirectoryId = 1},
                new ImageFile {Id = 11, Title = "Image2", Extention = "jpg", Height = 400, Width = 400, Size = 5, DirectoryId = 2},
                new ImageFile {Id = 12, Title = "Image3", Extention = "jpg", Height = 400, Width = 400, Size = 5, DirectoryId = 3}
                );

            modelBuilder.Entity<FilePermission>().HasData(
                new FilePermission {UserId = 1, FileId = 1, CanRead = true, CanWrite = false},
                new FilePermission {UserId = 1, FileId = 2, CanRead = false, CanWrite = false},
                new FilePermission {UserId = 2, FileId = 3, CanRead = true, CanWrite = true},
                new FilePermission {UserId = 3, FileId = 4, CanRead = true, CanWrite = false},
                new FilePermission {UserId = 2, FileId = 5, CanRead = false, CanWrite = true},
                new FilePermission {UserId = 3, FileId = 6, CanRead = true, CanWrite = true},
                new FilePermission {UserId = 1, FileId = 7, CanRead = true, CanWrite = true},
                new FilePermission {UserId = 2, FileId = 7, CanRead = false, CanWrite = false},
                new FilePermission {UserId = 3, FileId = 7, CanRead = true, CanWrite = false},
                new FilePermission {UserId = 3, FileId = 10, CanRead = true, CanWrite = true},
                new FilePermission {UserId = 2, FileId = 11, CanRead = true, CanWrite = false},
                new FilePermission {UserId = 1, FileId = 12, CanRead = false, CanWrite = true}
            );

            modelBuilder.Entity<DirectoryPermission>().HasData(
                new DirectoryPermission {UserId = 1, DirectoryId = 2, CanRead = true, CanWrite = false},
                new DirectoryPermission {UserId = 1, DirectoryId = 3, CanRead = false, CanWrite = false},
                new DirectoryPermission {UserId = 2, DirectoryId = 3, CanRead = true, CanWrite = true}
            );
        }
    }
}
