using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Project.Extensions;
using Project.Infrastructure.Services.SaveSystem.Data;
using UnityEngine;
using ILogger = Project.Infrastructure.Logger.ILogger;

namespace Project.Infrastructure.Services.SaveSystem
{
    public class ProgressFileReaderWriter : ILogger
    {
        public Color DefaultColor => Color.orangeRed;
        
        private readonly string _filePath;

        public ProgressFileReaderWriter(string filePath)
        {
            _filePath = filePath;
        }

        public void Write(GameProgress progress)
        {
            try
            {
                BinaryFormatter formatter = new();
                
                using (var fileStream = File.Open(_filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(fileStream, progress);
                }
                
                this.Log($"Saved progress to file: {_filePath}");
            }
            catch (Exception ex)
            {
                this.Log($"Unexpected error on write in file: {ex.Message}");
            }
        }

        public GameProgress Read()
        {
            if (File.Exists(_filePath))
            {
                try
                {
                    BinaryFormatter formatter = new();
                    using (var fileStream = File.Open(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        return formatter.Deserialize(fileStream) as GameProgress;
                    }
                }
                catch (Exception ex)
                {
                    this.Log($"Unexpected error on read, starting new progress: {ex.Message}");
                    return null;
                }
            }

            return null;
        }

        public void Delete()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    File.Delete(_filePath);
                    this.Log("Progress file was deleted.");
                }
            }
            catch (Exception ex)
            {
                this.Log($"Unexpected error when deleting progress file: {ex.Message}");
            }
        }
    }
}