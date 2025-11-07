using System.IO;
using UnityEngine;

namespace Project.Infrastructure.Services.SaveSystem.StaticData
{
    public static class SaveStaticData
    {
        private const string FileName = "game_progress.bin";
        public static string ProgressFilePath => Path.Combine(Application.persistentDataPath, FileName);
    }
}