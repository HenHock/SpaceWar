using System;
using Cysharp.Threading.Tasks;
using Project.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using ILogger = Project.Infrastructure.Logger.ILogger;

namespace Project.Infrastructure.Services.SceneLoader
{
    public class SceneLoader : ISceneLoader, ILogger
    {
        public bool IsActiveLogger => true;
        public Color DefaultColor => Color.cyan;

        public void Load(int sceneID, Action onLoadedAction) => 
            LoadAsync(sceneID, onLoadedAction).Forget();

        private async UniTask LoadAsync(int sceneID, Action onLoadedAction = null)
        {
            this.Log($"Start loading the {sceneID} scene");

            var asyncOperation = SceneManager.LoadSceneAsync(sceneID);
            await UniTask.WaitUntil(() => asyncOperation.isDone);
            
            this.Log($"Finished loading the {sceneID} scene");
            onLoadedAction?.Invoke();
        }
    }
}