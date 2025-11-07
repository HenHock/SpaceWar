using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Editor
{
    /// <summary>
    /// Automatically switches to scene 0 when entering play mode, and back to selected scene when exiting play mode
    /// </summary>
    [InitializeOnLoad]
    public class AutoChangeScene
    {
        static AutoChangeScene() => Enable();

        [MenuItem("Tools/Auto Change Scene/Enable", priority = 0)]
        public static void Enable() => EditorSceneManager.playModeStartScene = 
            AssetDatabase.LoadAssetAtPath<SceneAsset>(SceneUtility.GetScenePathByBuildIndex(0));

        [MenuItem("Tools/Auto Change Scene/Disable", priority = 1)]
        public static void Disable() => EditorSceneManager.playModeStartScene = null;
    }
}