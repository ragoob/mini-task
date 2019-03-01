using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Extentions
{
    public interface ITaskFileProvider : IFileProvider
    {
        void WriteAllText(string path, string contents, Encoding encoding);

        void WriteAllBytes(string filePath, byte[] bytes);

        string ReadAllText(string path, Encoding encoding);

        byte[] ReadAllBytes(string filePath);

        string GetParentDirectory(string directoryPath);

        string[] GetFiles(string directoryPath, string searchPattern = "", bool topDirectoryOnly = true);

        string GetFileNameWithoutExtension(string filePath);


        string GetFileName(string path);


        string GetFileExtension(string filePath);

        string GetDirectoryName(string path);

        string[] GetDirectories(string path, string searchPattern = "", bool topDirectoryOnly = true);


        void FileCopy(string sourceFileName, string destFileName, bool overwrite = false);

        bool FileExists(string filePath);


        bool DirectoryExists(string path);


        void DeleteFile(string filePath);

        void DeleteDirectory(string path);

        void CreateFile(string path);

        void CreateDirectory(string path);

        string Combine(params string[] paths);

        string MapPath(string path);
    }
}
