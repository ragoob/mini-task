using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
namespace Infrastructure.Extentions
{
    public class TaskFileProvider : PhysicalFileProvider, ITaskFileProvider
    {
        protected string BaseDirectory { get; }

        public TaskFileProvider(IHostingEnvironment hostingEnvironment)
         : base(File.Exists(hostingEnvironment.WebRootPath) ? Path.GetDirectoryName(hostingEnvironment.WebRootPath) : hostingEnvironment.WebRootPath)
        {
            var path = hostingEnvironment.ContentRootPath ?? string.Empty;
            if (File.Exists(path))
                path = Path.GetDirectoryName(path);

            BaseDirectory = path;
        }

        private static void DeleteDirectoryRecursive(string path)
        {
            Directory.Delete(path, true);
            const int maxIterationToWait = 10;
            var curIteration = 0;
            while (Directory.Exists(path))
            {
                curIteration += 1;
                if (curIteration > maxIterationToWait)
                    return;
                Thread.Sleep(100);
            }
        }

        public string Combine(params string[] paths)
        {
            return Path.Combine(paths);
        }

        public void CreateDirectory(string path)
        {
            if (!DirectoryExists(path))
                Directory.CreateDirectory(path);
        }

        public void CreateFile(string path)
        {
            if (FileExists(path))
                return;

            using (File.Create(path))
            {
            }
        }

        public void DeleteDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(path);

            //find more info about directory deletion
            //and why we use this approach at https://stackoverflow.com/questions/329355/cannot-delete-directory-with-directory-deletepath-true

            foreach (var directory in Directory.GetDirectories(path))
            {
                DeleteDirectory(directory);
            }

            try
            {
                DeleteDirectoryRecursive(path);
            }
            catch (IOException)
            {
                DeleteDirectoryRecursive(path);
            }
            catch (UnauthorizedAccessException)
            {
                DeleteDirectoryRecursive(path);
            }
        }

        public void DeleteFile(string filePath)
        {
            if (!FileExists(filePath))
                return;

            File.Delete(filePath);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public void FileCopy(string sourceFileName, string destFileName, bool overwrite = false)
        {
            File.Copy(sourceFileName, destFileName, overwrite);

        }

        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public string[] GetDirectories(string path, string searchPattern = "", bool topDirectoryOnly = true)
        {
            if (string.IsNullOrEmpty(searchPattern))
                searchPattern = "*";

            return Directory.GetDirectories(path, searchPattern,
                topDirectoryOnly ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories);
        }

        public string GetDirectoryName(string path)
        {
            return Path.GetDirectoryName(path);
        }

        public string GetFileExtension(string filePath)
        {
            return Path.GetExtension(filePath);
        }

        public string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }

        public string GetFileNameWithoutExtension(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }

        public string[] GetFiles(string directoryPath, string searchPattern = "", bool topDirectoryOnly = true)
        {
            if (string.IsNullOrEmpty(searchPattern))
                searchPattern = "*.*";

            return Directory.GetFiles(directoryPath, searchPattern,
                topDirectoryOnly ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories);
        }

        public string GetParentDirectory(string directoryPath)
        {
            return Directory.GetParent(directoryPath).FullName;
        }

        public byte[] ReadAllBytes(string filePath)
        {
            return File.Exists(filePath) ? File.ReadAllBytes(filePath) : new byte[0];

        }

        public string ReadAllText(string path, Encoding encoding)
        {
            return File.ReadAllText(path, encoding);

        }

        public void WriteAllBytes(string filePath, byte[] bytes)
        {
            File.WriteAllBytes(filePath, bytes);
        }

        public void WriteAllText(string path, string contents, Encoding encoding)
        {
            File.WriteAllText(path, contents, encoding);
        }

        public string MapPath(string path)
        {
            path = path.Replace("~/", string.Empty).TrimStart('/').Replace('/', '\\');
            return Path.Combine(BaseDirectory ?? string.Empty, path);
        }
    }
}
