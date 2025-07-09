using iPlanner.Application.Interfaces;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace iPlanner.Infrastructure.Common
{
    public class FileService : IFileService
    {
        private string _basePath;

        public FileService()
        {
            _basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }

        public string GetDataFilePath(string fileName)
        {
            string configFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "iPlanner", "config.txt");
            if (!File.Exists(configFilePath))
            {
                return Path.Combine(_basePath, "iPlanner", "SourceData", fileName);
            }
            return Path.Combine(File.ReadAllText(configFilePath), fileName);
        }


        public void EnsureDirectoryExists(string filePath)
        {
            string? directoryPath = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        public void SaveJsonData<T>(string filePath, T data)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(data, options);
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al guardar los datos: {ex.Message}");
            }
        }

        public T? LoadJsonData<T>(string filePath) where T : new()
        {
            if (!File.Exists(filePath)) return new T();

            try
            {
                string jsonString = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<T>(jsonString) ?? new T();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al cargar los datos: {ex.Message}");
            }
        }
    }
}
