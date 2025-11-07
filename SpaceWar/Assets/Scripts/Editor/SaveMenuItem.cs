using Project.Infrastructure.Services.SaveSystem;
using Project.Infrastructure.Services.SaveSystem.StaticData;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class SaveMenuItem : MonoBehaviour
    {
        [MenuItem("Tools/ClearSave", priority = 999)]
        public static void ClearSave()
        {
            var progressFile = new ProgressFileReaderWriter(SaveStaticData.ProgressFilePath);
            progressFile.Delete();
        }
    }
}
