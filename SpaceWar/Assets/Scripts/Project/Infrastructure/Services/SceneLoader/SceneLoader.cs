using System;
using Cysharp.Threading.Tasks;
using Project.Extensions;
using Project.Infrastructure.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using ILogger = Project.Infrastructure.Logger.ILogger;

namespace Project.Infrastructure.Services.SceneLoader
{
    public class SceneLoader : ISceneLoader, ILogger
    {
        public bool IsActiveLogger => true;
        public Color DefaultColor => Color.cyan;

        public void Load(GameScene sceneID, Action onLoadedAction = null) => 
            LoadAsync(sceneID, onLoadedAction).Forget();

        private async UniTask LoadAsync(GameScene sceneID, Action onLoadedAction = null)
        {
            this.Log($"Start loading the {sceneID} scene");

            await SceneManager.LoadSceneAsync((int)sceneID).ToUniTask();
            
            this.Log($"Finished loading the {sceneID} scene");
            onLoadedAction?.Invoke();
        }
    }
}