using System;

namespace iPlanner.Core.Application.Interfaces
{
    public interface IFileService
    {
        string GetDataFilePath(string fileName);
        void EnsureDirectoryExists(string filePath);
        void SaveJsonData<T>(string filePath, T data);
        T? LoadJsonData<T>(string filePath) where T : new();
    }
}
